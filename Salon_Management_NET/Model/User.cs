using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
      
}
