using app1.Data;
using app1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app1.Controllers
{




    public class CarsController : ControllerBase
    {
        private readonly CarDbContext _context;

        public CarsController(CarDbContext context) => this._context = context;

        [HttpGet]
        [Route("api/[controller]/get_all_cars")]

        public async Task<IEnumerable<Car>> Get() => await _context.Cars.ToListAsync();


        [HttpGet]
        [Route("api/[controller]/get_car_by_id/{id}")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTd(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            return car == null ? NotFound() : Ok(car);
        }
        [HttpPost]
        [Route("api/[controller]/create_car")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByTd), new { id = car.Id }, car);

        }
        [HttpPut]
        [Route("api/[controller]/update_car/{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Car car)
        {
            if (id != car.Id) { return BadRequest(); }
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [Route("api/[controller]/delete_car/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) { return NotFound(); }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();


        }
    }
}


