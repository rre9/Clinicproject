using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicProject.ViewModels
{
    public class DoctorFilterVM
    {
        public int? Id { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        public string? NationalId { get; set; }

        public int Page { get; set; } = 1;

        // Change page size if you want more doctors per page
        public int PageSize { get; set; } = 2;

        public int TotalCount { get; set; }

        public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
