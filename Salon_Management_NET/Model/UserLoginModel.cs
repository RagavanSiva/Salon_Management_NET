using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model
{
    public class UserLoginModel
    {
        [Required]
        public string Name { get; set; } // User's name (e.g., username or email)

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } // User's password
    }
}
