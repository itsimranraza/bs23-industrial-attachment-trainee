using DoctorAppointment.Data;
using DoctorAppointment.Entity;
using DoctorAppointment.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
    }
}
