namespace DataCaptureService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data Capture Service!");

            var dataCaptureService = new DataCapturer();

            Console.WriteLine("Data Capture Service");
            Console.WriteLine("Press any key to start...");
            Console.ReadLine();

            dataCaptureService.Start();
        }
    }
}