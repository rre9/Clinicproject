using ClinicProject.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicProject.Models
{
    public class Doctor
    {


        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [ForeignKey("Speciality")]
        public int SpecialityNum { get; set; }

        public Speciality? Speciality { get; set; }

        
        public string NationalId { get; set; } = null!;


        public List<Appointment> Appointments { get; set; } = new();


        public DoctorVM ToDoctorVM()
        {
            return new DoctorVM
            {
                Id = Id,
                FirstName = FirstName,
                NationalId = NationalId,
                LastName = LastName,
                HireDate = HireDate,
            };


        }


        public DoctorUpdateVM ToDoctorUpdateVM()
        {
            return new DoctorUpdateVM
            {
                Id = Id,
                FirstName = FirstName,
                NationalId = NationalId,
                LastName = LastName,
                HireDate = HireDate,
            };
        }
    }
}
