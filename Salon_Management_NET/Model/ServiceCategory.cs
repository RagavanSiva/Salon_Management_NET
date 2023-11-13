using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}
