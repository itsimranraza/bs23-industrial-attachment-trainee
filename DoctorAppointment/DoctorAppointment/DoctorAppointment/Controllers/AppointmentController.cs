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
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .ToListAsync();
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
            if (!ModelState.IsValid)
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentEditViewModel
            {
                Id = appointment.Id,
                PatientName = appointment.PatientName,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId
            };

            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name", model.DoctorId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AppointmentEditViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name", model.DoctorId);
                return View(model);
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if(appointment == null)
            {
                return NotFound();
            }

            appointment.PatientName = model.PatientName;
            appointment.AppointmentDateTime = model.AppointmentDateTime;
            appointment.DoctorId = model.DoctorId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments
                .FindAsync(id);
            
            if(appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
