using ClinicProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicProject.ViewModels
{
    public class AppointmentUpdateVM
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [MaxLength(10), RegularExpression("[12]\\d{9}", ErrorMessage = "The input should be in the form 1xxxxxxxxx or 2xxxxxxxxx")]
        [Display(Name = "National ID")]
        [Required]
        public string NationalId { get; set; } = null!;

        [Column(TypeName = "date")]
        [Display(Name = "Hire Date")]
        [Required]
        public DateTime HireDate { get; set; }
    }
}

