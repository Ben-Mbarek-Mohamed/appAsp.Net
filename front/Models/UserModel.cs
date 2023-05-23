using System.ComponentModel.DataAnnotations;

namespace front.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public URole Role { get; set; }

        public enum URole { Admin, User }
    }
}
