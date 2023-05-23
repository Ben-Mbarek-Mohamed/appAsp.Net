using Microsoft.Build.Framework;

namespace WebApplication4.Models
{
    public class CarModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public double Price { get; set; }
        public status Status { get; set; }
        public type Type { get; set; }
        public enum status
        {
            New, Used
        }
        public enum type
        {
            Suv, Sprot, Electrique, Sedan
        }
    }
}
