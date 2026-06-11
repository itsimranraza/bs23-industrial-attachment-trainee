using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.ViewModels
{
    public class AppointmentCreateViewModels
    {
        [Required(ErrorMessage = "Patient Name is required")]
        [StringLength(100, ErrorMessage = "Patient Name must be less than or equal to 100.")]
        public string? PatientName { get; set; }

        [Required(ErrorMessage = "Appointment Date and Time is required")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        public int DoctorId { get; set; }
    }
}
