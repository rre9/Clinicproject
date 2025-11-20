using ClinicProject.Models;
using ClinicProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClinicProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ClinicContextcs _db;

        public AppointmentController(ClinicContextcs db)
        {
            _db = db;
        }

        public IActionResult Index(AppointmentFilterVM vm)
        {
            vm ??= new AppointmentFilterVM();

            var initQuery = _db.Appointments
                               .Include(a => a.Patient)
                               .Include(a => a.Doctor)
                               .Where(a => vm.Id == null || a.Id == vm.Id)
                               .Where(a => vm.FullName == null || (a.Patient.FullName + " " + a.Doctor.FirstName + " " + a.Doctor.LastName).Contains(vm.FullName))
                               .Where(a => vm.NationalId == null || a.Patient.NationalId.Contains(vm.NationalId) || a.Doctor.NationalId.Contains(vm.NationalId));

            vm.TotalCount = initQuery.Count();

            var appointments = initQuery
                            .OrderBy(a => a.Id)
                            .Skip((vm.Page - 1) * vm.PageSize)
                            .Take(vm.PageSize)
                            .Select(a => a.ToAppointmentVM())
                            .ToList();

            return View(new AppointmentFilteredListVM { Appointments = appointments, Filter = vm });
        }

        public IActionResult Details(int id)
        {
            var appointment = _db.Appointments.Single(a => a.Id == id).ToAppointmentVM();
            return View(appointment);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AppointmentCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Always create new Doctor for this Appointment (even if one exists with same NationalId)
            // Get first available Speciality
            var speciality = _db.Specialities.FirstOrDefault();
            if (speciality == null)
            {
                ModelState.AddModelError(string.Empty, "No specialities available. Please add specialities first.");
                return View(vm);
            }

            // Create new Doctor for this Appointment
            var doctor = new Doctor
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                NationalId = vm.NationalId,
                HireDate = vm.HireDate,
                SpecialityNum = speciality.Code
            };
            _db.Doctors.Add(doctor);
            _db.SaveChanges();

            // Find first Patient or create default
            var patient = _db.Patients.FirstOrDefault();
            if (patient == null)
            {
                // Create default patient if none exists
                patient = new Patient
                {
                    FullName = "Default Patient",
                    NationalId = "1000000000",
                    Email = "patient@clinic.com",
                    PhoneNumber = "0500000000",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30))
                };
                _db.Patients.Add(patient);
                _db.SaveChanges();
            }

            // Create Appointment
            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                AppointmentDate = vm.HireDate,
                Status = "Pending"
            };
            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = appointment.Id });
        }

        public IActionResult Update(int id)
        {
            var appointment = _db.Appointments
                .Include(a => a.Doctor)
                .Single(a => a.Id == id);
            var vm = appointment.ToAppointmentUpdateVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, AppointmentUpdateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var appointment = _db.Appointments
                .Include(a => a.Doctor)
                .Single(a => a.Id == id);
            
            // Update Doctor information that was created with this Appointment
            var doctor = appointment.Doctor;
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.HireDate = vm.HireDate;
            doctor.NationalId = vm.NationalId;
            
            // Also update AppointmentDate if needed
            appointment.AppointmentDate = vm.HireDate;

            _db.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Delete(int id)
        {
            var appointment = _db.Appointments.Single(a => a.Id == id);
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            return Ok();
        }
    }
}

