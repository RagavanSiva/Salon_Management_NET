using System.ComponentModel.DataAnnotations;

namespace Salon_Management_NET.Model.RequestDTO
{
    public class ServiceCategoryRequest
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }
    }
}
