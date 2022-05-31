using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using ExpenseApi.Controllers;
using ExpenseApi.Models;
using ExpenseApi.Models.DTO;
using ExpenseApi.Repositories;

using static ExpenseAPI.Tests.TestsHelper.TestsHelper;

using Moq;

using Xunit;

namespace ExpenseAPI.Tests.Controllers
{
    public class ExpensesControllerTests
    {
        private static IMapper _mapper;
        private readonly Mock<IExpenseRepository> _expenseRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICurrencyRepository> _currencyRepository;
        private readonly ExpensesController _expensesController;

        public ExpensesControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ExpenseProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _expenseRepository = new();
            _userRepository = new();
            _currencyRepository = new();
            _expensesController = new(_mapper, _expenseRepository.Object, _userRepository.Object, _currencyRepository.Object);
        }

        [Fact]
        public async void PostExpense_DateInFuture_ReturnsBadRequest()
        {
            ExpenseDto expenseDto = new();

            Expense expenseReturn = new Expense() { Id = 1 };

            _expensesController.ModelState.AddModelError("Date", "Date cannot be in the future");

            _expenseRepository.Setup(e => e.Add(It.IsAny<Expense>()))
                            .Returns(Task.FromResult(expenseReturn));

            var response = await _expensesController.PostExpense(expenseDto);

            ActionResult<Expense> badRequestResult = Assert.IsType<ActionResult<Expense>>(response);
            var result = Assert.IsType<BadRequestObjectResult>(badRequestResult.Result as BadRequestObjectResult);
            var errors = Assert.IsType<SerializableError>(result.Value as SerializableError);
            Assert.Single(errors);
            Assert.True(errors.ContainsKey("Date"));
        }

        [Fact]
        public async void PostExpense_ReturnsOk()
        {
            ExpenseDto expenseDto = new()
            {
                Amount = new AmountDto()
            };

            Expense expenseReturn = new Expense() { Id = 1 };
            User userReturn = new User();

            _expenseRepository.Setup(er => er.Add(It.IsAny<Expense>()))
                            .Returns(Task.FromResult(expenseReturn));

            _userRepository.Setup(ur => ur.GetById(It.IsAny<int>()))
                            .Returns(Task.FromResult(userReturn));

            _expensesController.ObjectValidator = GetObjectValidator().Object;

            var response = await _expensesController.PostExpense(expenseDto);

            ActionResult<Expense> okRequestResult = Assert.IsType<ActionResult<Expense>>(response);
            var result = Assert.IsType<CreatedAtActionResult>(okRequestResult.Result as CreatedAtActionResult);
            var content = Assert.IsType<ExpenseDto>(result.Value);
            Assert.Equal(1, content.Id);
        }
    }
}