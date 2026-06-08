using DoctorAppointment.Data;
using DoctorAppointment.Entity;
using DoctorAppointment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DoctorAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DoctorAppointmentDbContext _context;

        public AppointmentController(DoctorAppointmentDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments =await _context.Appointments.Include(a => a.Doctor).ToListAsync();
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentCreateViewModels model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name");
                return View(model);
            }

            var appointment = new Appointment
            {
                PatientName = model.PatientName,
                AppointmentDateTime = model.AppointmentDateTime,
                DoctorId = model.DoctorId
            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
