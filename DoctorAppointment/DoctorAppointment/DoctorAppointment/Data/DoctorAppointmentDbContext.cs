using DoctorAppointment.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Data
{
    public class DoctorAppointmentDbContext : DbContext
    {
        public DoctorAppointmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
