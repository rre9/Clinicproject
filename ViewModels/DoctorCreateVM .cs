using ClinicProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicProject.ViewModels
{
    public class DoctorCreateVM

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

        
        public string NationalId { get; set; } = null!;


        public Doctor ToModel()
        {
            return new Doctor
            {
                Id = Id,
                FirstName = FirstName,
                NationalId = NationalId,
                LastName = LastName,
                HireDate = HireDate,
                SpecialityNum = SpecialityNum
            };
        }
    }
}