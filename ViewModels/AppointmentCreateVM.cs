using ClinicProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicProject.ViewModels
{
    public class AppointmentCreateVM
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        public string NationalId { get; set; } = null!;

    }
}

