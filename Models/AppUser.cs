using Microsoft.AspNetCore.Identity;

namespace ClinicProject.Models {
    public class AppUser : IdentityUser {

        public byte[]? ProfilePicture { get; set; }

    }
}