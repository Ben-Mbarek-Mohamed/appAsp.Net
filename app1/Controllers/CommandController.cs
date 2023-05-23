using app1.Data;
using app1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app1.Controllers
{
    
    public class CommandController : Controller
    {
        private readonly CarDbContext _context;
        public CommandController(CarDbContext context) => this._context = context;

        [HttpGet]
        [Route("api/[controller]/get_all_commands")]

        public async Task<IEnumerable<Command>> Get() => await _context.Commands.ToListAsync();

        [HttpGet]
        [Route("api/[controller]/get_command_by_id/{id}")]
        [ProducesResponseType(typeof(Command), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTd(int id)
        {
            var command = await _context.Commands.FindAsync(id);
            return command == null ? NotFound() : Ok(command);
        }

        [HttpPost]
        [Route("api/[controller]/create_command")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Command command)
        {
            await _context.Commands.AddAsync(command);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByTd), new { id = command.Id }, command);

        }


        [HttpPut]
        [Route("api/[controller]/update_command/{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Command command)
        {
            if (id != command.Id) { return BadRequest(); }
            _context.Entry(command).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }


        [Route("api/[controller]/delete_command/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = await _context.Commands.FindAsync(id);
            if (command == null) { return NotFound(); }
            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();
            return NoContent();


        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
