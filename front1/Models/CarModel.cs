using System.ComponentModel.DataAnnotations;

namespace front1.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        
        public string Model { get; set; }
        
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
    }
    public enum Status
    {
        New, Used
    }
    public enum Type
    {
        Suv, Sprot, Electrique, Sedan
    }
}
