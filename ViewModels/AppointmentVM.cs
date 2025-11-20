using System.ComponentModel.DataAnnotations;

namespace ClinicProject.ViewModels
{
    public class AppointmentVM
    {
        public int Id { get; set; }

        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; } = null!;

        [Display(Name = "Doctor ID")]
        public int DoctorId { get; set; }

        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; } = null!;

        public string FullName => DoctorName.Trim();

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = null!;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
    }
}

