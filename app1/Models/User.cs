using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace app1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public URole Role { get; set; }
        
        public enum URole { Admin,User}

    }
}
