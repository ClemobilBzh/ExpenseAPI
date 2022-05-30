using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using ExpenseApi.Models;
using ExpenseApi.Models.DTO;
using ExpenseApi.Repositories;

namespace ExpenseApi.Controllers
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
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses()
        {
            var expenses = await _expenseRepository.GetAll();
            if (expenses == null || expenses.Count() == 0)
            {
                return NotFound();
            }

            var expensesDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
            return Ok(expensesDto);
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
        public async Task<IActionResult> PutExpense(int id, ExpenseDto expenseDto)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Expense expense = _mapper.Map<Expense>(expenseDto);

                expense.User = await _userRepository.GetById(expenseDto.UserId);
                expense.Amount.Currency = await _currencyRepository.GetById(expenseDto.Amount.CurrencyId);

                expense = await _expenseRepository.Add(expense);

                return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, _mapper.Map<ExpenseDto>(expense));
            }
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            Expense expense = await _expenseRepository.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            _expenseRepository.Remove(expense);

            return NoContent();
        }
    }
}
