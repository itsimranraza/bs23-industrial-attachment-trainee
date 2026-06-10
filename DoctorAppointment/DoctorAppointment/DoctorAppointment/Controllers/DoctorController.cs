using DoctorAppointment.Data;
using DoctorAppointment.Entity;
using DoctorAppointment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DoctorAppointment.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorAppointmentDbContext _context;

        public DoctorController(DoctorAppointmentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        } 

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var doctor = new Doctor
            {
                Name = model.Name,
                Specialization = model.Specialization
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Doctor created successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _context.Doctors
                .Include(d=>d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);

            if(doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if(doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorEditViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialization = doctor.Specialization
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DoctorEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Name = model.Name;
            doctor.Specialization = model.Specialization;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Doctor updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if(doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Doctor deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
