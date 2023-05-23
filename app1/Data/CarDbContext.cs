using app1.Models;
using Microsoft.EntityFrameworkCore;

namespace app1.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
            
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Command> Commands { get; set; }
    }
}
