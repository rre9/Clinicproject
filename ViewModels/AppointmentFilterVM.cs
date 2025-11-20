using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicProject.ViewModels
{
    public class AppointmentFilterVM
    {
        public int? Id { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        public string? NationalId { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 2;

        public int TotalCount { get; set; }

        public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}

