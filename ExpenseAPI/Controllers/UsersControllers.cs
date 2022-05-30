using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using ExpenseApi.Models;
using ExpenseApi.Models.DTO;
using ExpenseApi.Repositories;

namespace ExpenseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            if (users == null || users.Count() == 0)
            {
                return NotFound();
            }

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersDto);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserDto>(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            if (await _currencyRepository.GetById(user.CurrencyId) == null)
            {
                return BadRequest();
            }

            int repoReturn = await _userRepository.Update(user);

            if (repoReturn == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Currency = await _currencyRepository.GetById(userDto.CurrencyId);

            user = await _userRepository.Add(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Expenses.Any())
            {
                return BadRequest("User with Expenses cannot be removed");
            }

            _userRepository.Remove(user);

            return NoContent();
        }
    }
}
