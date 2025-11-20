using System.ComponentModel.DataAnnotations;

namespace ClinicProject.ViewModels
{
    public class DoctorVM
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "National ID")]
        public string NationalId { get; set; } = null!;

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Speciality Number")]
        public int SpecialityNum { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
