using Xunit;
using ExpenseApi.Controllers;
using AutoMapper;
using ExpenseApi.Repositories;
using ExpenseApi.Models.DTO;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using ExpenseApi.Models;
using System.Net;

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
            // ExpenseDto expenseDto = new()
            // { Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1) };

            // var response = await _expensesController.PostExpense(expenseDto);

            // var badRequestResult = Assert.IsType<ActionResult<Expense>>(response);
            // Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}