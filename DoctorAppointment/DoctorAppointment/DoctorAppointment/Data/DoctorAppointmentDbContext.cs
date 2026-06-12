using DoctorAppointment.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Data
{
    public class DoctorAppointmentDbContext : IdentityDbContext<ApplicationUser>
    {
        public DoctorAppointmentDbContext(DbContextOptions<DoctorAppointmentDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
