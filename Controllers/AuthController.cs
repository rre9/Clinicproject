using ClinicProject.Models;
using ClinicProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // *********** Login (GET) ***********
        public IActionResult Login()
        {
            return View();
        }

        // *********** Login (POST) ***********
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or password are wrong");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, true, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password are wrong");
                return View(vm);
            }

            // Redirect based on roles
            if (await _userManager.IsInRoleAsync(user, "APP_ADMIN") ||
                await _userManager.IsInRoleAsync(user, "DOCTOR"))
            {
                return Redirect("/Doctor");
            }

            return Redirect("/");
        }

        // *********** Logout ***********
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        // *********** Create User (GET) ***********
        [Authorize(Roles = "APP_ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        // *********** Create User (POST) ***********
        [Authorize(Roles = "APP_ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = new AppUser
            {
                Email = vm.Email,
                UserName = vm.Email.Split("@")[0]
            };

            // Upload Image
            if (vm.ProfilePicture != null && vm.ProfilePicture.Length > 0)
            {
                if (vm.ProfilePicture.Length > 256 * 1024)
                {
                    ModelState.AddModelError("ProfilePicture", "Max image size is 256KB");
                    return View(vm);
                }

                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(vm.ProfilePicture.ContentType))
                {
                    ModelState.AddModelError("ProfilePicture", "Only JPG and PNG are allowed");
                    return View(vm);
                }

                using var memory = new MemoryStream();
                vm.ProfilePicture.CopyTo(memory);
                user.ProfilePicture = memory.ToArray();
            }

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Create user failed");
                return View(vm);
            }

            // Add Role
            result = await _userManager.AddToRoleAsync(user, vm.Role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Role", "Failed to assign role");
                return View(vm);
            }

            return Redirect("/");
        }
    }
}
