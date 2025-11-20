namespace ClinicApp.Services {
    public class AnotherService {

        private static int Count { get; set; }
        public AnotherService()
        {
            Count++;
            Console.WriteLine("AnotherService: " + Count);
        }
    }
}