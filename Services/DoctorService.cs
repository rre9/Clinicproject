using ClinicProject.Models;

namespace ClinicProject.Services {
    public class DoctorService {

        private static int Count { get; set; }

        public DoctorService(AnotherService anotherService, ClinicContextcs context)
        {
            Count++;
            Console.WriteLine("DoctorService: " + Count);
        }
    }
}