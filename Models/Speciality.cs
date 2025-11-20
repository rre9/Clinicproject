using System.ComponentModel.DataAnnotations;

namespace ClinicProject.Models
{
    public class Speciality
    {
        public int Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public List<Doctor> Doctors { get; set; } = new();
    }
}

