using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model
{
    public class Branch
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
