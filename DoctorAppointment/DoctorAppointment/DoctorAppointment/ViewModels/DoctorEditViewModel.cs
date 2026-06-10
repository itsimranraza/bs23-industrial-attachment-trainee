using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.ViewModels
{
    public class DoctorEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be less than or equal to 100.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Sepecialization is required")]
        [StringLength(100, ErrorMessage = "Specialization must be less than or equal to 100.")]
        public string? Specialization { get; set; }

    }
}
