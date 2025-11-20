using System.ComponentModel.DataAnnotations;

namespace ClinicProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        [MaxLength(100)]
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;

    }
}
