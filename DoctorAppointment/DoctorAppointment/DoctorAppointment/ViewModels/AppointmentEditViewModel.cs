using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.ViewModels
{
    public class AppointmentEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Patient Name is required")]
        [StringLength(100, ErrorMessage = "Patient Name must be less than or equal to 100.")]
        public string? PatientName { get; set; }
        [Required(ErrorMessage = "Appointment Date and Time is required")]
        public DateTime AppointmentDateTime { get; set; }
        [Required(ErrorMessage = "Doctor Id is required")]
        public int DoctorId { get; set; }
    }
}
