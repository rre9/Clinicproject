using ClinicProject.ViewModels;
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

        public AppointmentVM ToAppointmentVM()
        {
            return new AppointmentVM
            {
                Id = Id,
                PatientId = PatientId,
                PatientName = Patient?.FullName ?? "Unknown",
                DoctorId = DoctorId,
                DoctorName = Doctor != null ? $"{Doctor.FirstName} {Doctor.LastName}".Trim() : "Unknown",
                AppointmentDate = AppointmentDate,
                Status = Status,
                CreatedAt = CreatedAt
            };
        }

        public AppointmentUpdateVM ToAppointmentUpdateVM()
        {
            return new AppointmentUpdateVM
            {
                Id = Id,
                FirstName = Doctor?.FirstName ?? "",
                LastName = Doctor?.LastName,
                NationalId = Doctor?.NationalId ?? "",
                HireDate = Doctor?.HireDate ?? DateTime.Now
            };
        }
    }
}
