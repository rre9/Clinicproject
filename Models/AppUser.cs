using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Models {
    public class AppUser : IdentityUser {

        public byte[]? ProfilePicture { get; set; }

    }
}