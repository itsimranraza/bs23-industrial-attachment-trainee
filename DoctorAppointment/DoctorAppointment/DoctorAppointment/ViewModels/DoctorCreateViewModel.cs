using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.ViewModels
{
    public class DoctorCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Spercialization is required")]
        [StringLength(100, ErrorMessage = "Specialization must be less than or equal to 100.")]
        public string? Specialization { get; set; }
    }
}
