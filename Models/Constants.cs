namespace ClinicProject.Models
{
    public class Constants
    {
        public static List<Doctor> Doctors = [
         new Doctor
            {
                Id = 101,
                FirstName = "Alice ",
                LastName = "Johnson-Doe",
                HireDate = DateTime.Now,
                NationalId = "1115912766",
                SpecialityNum= 3,
                Appointments = new List<Appointment>()
            },
             new Doctor
            {
                Id = 102,
                FirstName = "Robert ",
                LastName = "Smith",
                HireDate = DateTime.Now,
                SpecialityNum= 5,
                NationalId = "1119999887",
                Appointments = new List<Appointment>()
            },
            new Doctor
            {
                Id = 103,
                FirstName = "Maria  ",
                LastName = "Rodriguez",
                HireDate = DateTime.Now,
                SpecialityNum= 5,
                NationalId = "1116667778",
                Appointments = new List<Appointment>()
            },



         ];
    }
}
