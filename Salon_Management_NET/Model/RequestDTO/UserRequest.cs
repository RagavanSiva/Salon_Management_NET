using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model.RequestDTO
{
    public class UserRequest
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }


    }
}
