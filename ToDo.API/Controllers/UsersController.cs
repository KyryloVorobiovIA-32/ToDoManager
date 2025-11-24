using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public UsersController(ToDoDbContext context)
        {
            _context = context;
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User loginData)
        {
            // Шукаємо користувача за логіном і паролем
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginData.Username && u.PasswordHash == loginData.PasswordHash);

            if (user == null)
            {
                return Unauthorized("Невірний логін або пароль.");
            }

            return Ok(user);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User newUser)
        {
            // Перевіряємо, чи такий логін вже зайнятий
            if (await _context.Users.AnyAsync(u => u.Username == newUser.Username))
            {
                return BadRequest("Користувач з таким логіном вже існує.");
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Login), new { id = newUser.UserId }, newUser);
        }
        
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}