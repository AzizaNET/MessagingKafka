using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace DataCaptureService
{
    internal class DataCapturer
    {
        private const string BootstrapServers = "localhost:9092";
        private const string Topic = "data-capture-topic";
        private const string FolderPath = @"C:\\Users\\Aziza_Abdurakhmonova\\source\\repos\\MessagingKafka\\Local";

        public void Start()
        {
            var config = new ProducerConfig { BootstrapServers = BootstrapServers };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var watcher = new FileSystemWatcher(FolderPath);
                watcher.Created += OnFileCreated;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Data Capture Service started. Listening to the local folder...");

                Console.ReadLine();
            }
        }
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var filePath = e.FullPath;
            Console.WriteLine($"New file captured: {filePath}");

            var config = new ProducerConfig { BootstrapServers = BootstrapServers };

            //using (var producer = new ProducerBuilder<Null, string>(config).Build())
            //{
            //    var message = new Message<Null, string> { Value = filePath };

            //    producer.Produce(Topic, message, deliveryReport =>
            //    {
            //        Console.WriteLine("File sent to Main Processing Service.");
            //    });
            //}

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var message = new Message<Null, string> { Value = filePath };

                producer.Produce(Topic, message, deliveryReport =>
                {
                    Console.WriteLine("File sent to Main Processing Service.");
                });

                producer.Flush(TimeSpan.FromSeconds(10)); // Flush messages to ensure they are sent immediately
            }
        }
    }
}
