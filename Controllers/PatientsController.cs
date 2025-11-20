using Microsoft.AspNetCore.Mvc;

namespace ClinicProject.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
