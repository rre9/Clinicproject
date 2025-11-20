using ClinicProject.Models;
using ClinicProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ClinicProject.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly ClinicContextcs _db;

        public DoctorController(ClinicContextcs db)
        {
            _db = db;
        }

        public IActionResult Index(DoctorFilterVM vm)
        {
            vm ??= new DoctorFilterVM();

            var initQuery = _db.Doctors
                               .Where(d => vm.Id == null || d.Id == vm.Id)
                               .Where(d => vm.NationalId == null || d.NationalId == vm.NationalId)
                               .Where(d => vm.FullName == null || (d.FirstName + " " + d.LastName).Contains(vm.FullName));

            vm.TotalCount = initQuery.Count();

            var doctors = initQuery
                            .OrderBy(d => d.Id)
                            .Skip((vm.Page - 1) * vm.PageSize)
                            .Take(vm.PageSize)
                            .Select(d => d.ToDoctorVM())
                            .ToList();

            return View(new DoctorFilteredListVM { Doctors = doctors, Filter = vm });
        }

        public IActionResult Details(int id)
        {
            var doctor = _db.Doctors.Single(d => d.Id == id).ToDoctorVM();
            return View(doctor);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(DoctorCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var d = vm.ToModel();
            _db.Doctors.Add(d);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = d.Id });
        }

        public IActionResult Update(int id)
        {
            var doctor = _db.Doctors.Single(d => d.Id == id).ToDoctorUpdateVM();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, DoctorUpdateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var doctor = _db.Doctors.Single(d => d.Id == id);
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.HireDate = vm.HireDate;
            doctor.SpecialityNum = vm.SpecialityNum;
            doctor.NationalId = vm.NationalId;

            _db.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Delete(int id)
        {
            var doctor = _db.Doctors.Single(d => d.Id == id);
            _db.Doctors.Remove(doctor);
            _db.SaveChanges();

            return Ok();
        }
    }
}
