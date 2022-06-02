using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using ExpenseAPI.Models;
using ExpenseAPI.Models.DTO;
using ExpenseAPI.Repositories;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public ExpensesController(IMapper mapper, IExpenseRepository expenseRepository, IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesNoSort()
        {
            return await GetExpenses(null);
        }
        // GET: api/Expenses/sortByDate/asc
        [HttpGet("sortByDate/{sortByDate}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesSortbyDate(string? sortByDate)
        {
            return await GetExpenses(sortByDate);
        }

        // GET: api/Expenses/User/2
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByUserDefault(int userId)
        {
            return await GetExpensesByUser(userId, null, null);
        }

        // GET: api/Expenses/User/2/sortByDate/asc
        [HttpGet("user/{userId}/sortByDate/{sortByDate}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByUserSortByDate(int userId, string sortByDate)
        {
            return await GetExpensesByUser(userId, sortByDate, null);
        }

        // GET: api/Expenses/User/2/sortByAmount/desc
        [HttpGet("user/{userId}/sortByAmount/{sortByAmount}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByUserSortByAmount(int userId, string sortByAmount = " ")
        {
            return await GetExpensesByUser(userId, null, sortByAmount);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _expenseRepository.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExpenseDto>(expense);
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(ExpenseDto expenseDto)
        {
            Expense expense = _mapper.Map<Expense>(expenseDto);
            int repoReturn = await _expenseRepository.Update(expense);

            if (repoReturn == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDto expenseDto)
        {
            if (ModelState.IsValid)
            {
                Expense expense = _mapper.Map<Expense>(expenseDto);

                expense.User = await _userRepository.GetById(expenseDto.UserId);
                expense.AmountDetails.Currency = await _currencyRepository.GetById(expenseDto.Amount.CurrencyId);

                if (TryValidateModel(expense))
                {
                    expense = await _expenseRepository.Add(expense);

                    return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, _mapper.Map<ExpenseDto>(expense));
                }
            }
            return BadRequest(ModelState);
        }

        // Plusieurs chemins mènent à cette fonction pour qu'ils soient utilisables par le swagger.
        // Si on utilise une seule fonction publique avec plusieurs routes et des paramètres optionnels, Swagger els considère tous comme obligatoires
        private async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByUser(int userId, string? sortByDate, string? sortByAmount)
        {
            var expenses = await _expenseRepository.GetByUser(userId, sortByDate, sortByAmount);
            if (expenses == null || expenses.Count() == 0)
            {
                return NotFound();
            }

            var expensesDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
            return Ok(expensesDto);
        }

        private async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses(string? sortByDate)
        {
            IEnumerable<Expense> expenses;

            expenses = await _expenseRepository.GetAllSort(sortByDate);

            if (expenses == null || expenses.Count() == 0)
            {
                return NotFound();
            }

            var expensesDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
            return Ok(expensesDto);
        }
    }
}
