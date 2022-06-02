using System;
using AutoMapper;
using ExpenseAPI.Models;
using ExpenseAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrenciesController(IMapper mapper, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            var currencies = await _currencyRepository.GetAll();
            if (currencies == null || currencies.Count() == 0)
            {
                return NotFound();
            }

            return Ok(currencies);
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _currencyRepository.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(Currency currency)
        {
            int repoReturn = await _currencyRepository.Update(currency);

            if (repoReturn == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Currencies
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            currency = await _currencyRepository.Add(currency);

            return CreatedAtAction(nameof(GetCurrency), new { id = currency.Id }, currency);
        }
    }
}
