namespace Messaging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Processing Service!");

            var mainProcessingService = new MainProcessingService();

            Console.WriteLine("Main Processing Service");
            Console.WriteLine("Press any key to start...");
            Console.ReadLine();

            mainProcessingService.Start();
        }
    }
}