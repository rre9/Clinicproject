namespace ClinicProject.ViewModels
{
    public class AppointmentFilteredListVM
    {
        public List<AppointmentVM> Appointments { get; set; } = null!;

        public AppointmentFilterVM Filter { get; set; } = new();
    }
}

