namespace ClinicProject.ViewModels
{
    public class DoctorFilteredListVM
    {
        public List<DoctorVM> Doctors { get; set; }

        public DoctorFilterVM Filter { get; set; } = new();
    }
}
