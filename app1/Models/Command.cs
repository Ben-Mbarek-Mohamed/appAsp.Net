using System.ComponentModel.DataAnnotations;

namespace app1.Models
{
    public class Command
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CarId { get; set; }

        [Required]
        public CStatus CommandStatus { get; set; }

        public enum CStatus { Validated,Canceled,InProgress}
    }
}
