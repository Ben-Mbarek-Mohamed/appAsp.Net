using app1.Data;
using app1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app1.Controllers

{

    public class UserController : Controller
    {
        private readonly CarDbContext _context;
        public UserController(CarDbContext context) => this._context = context;


        [HttpGet]
        [Route("api/[controller]/get_all_users")]

        public async Task<IEnumerable<User>> Get() => await _context.Users.ToListAsync();

        [HttpGet]
        [Route("api/[controller]/get_user_by_email/{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user == null ? NotFound() : Ok(user);

        }


        [HttpGet]
        [Route("api/[controller]/get_user_by_id/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTd(int id)
        {
            var command = await _context.Users.FindAsync(id);
            return command == null ? NotFound() : Ok(command);
        }


        [HttpPost]
        [Route("api/[controller]/create_user")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByTd), new { id = user.Id }, user);

        }


        [HttpPut]
        [Route("api/[controller]/update_user/{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id) { return BadRequest(); }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [Route("api/[controller]/delete_user/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { return NotFound(); }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();


        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
