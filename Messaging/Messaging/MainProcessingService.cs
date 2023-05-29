using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Messaging
{
    internal class MainProcessingService
    {
        private const string BootstrapServers = "localhost:9092";
        private const string Topic = "data-capture-topic";
        private const string StoragePath = @"C:\\Users\\Aziza_Abdurakhmonova\\source\\repos\\MessagingKafka\\Result";
        public void Start()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = BootstrapServers,
                GroupId = "main-processing-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(Topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    var filePath = consumeResult.Message.Value;
                    Console.WriteLine($"Received file: {filePath}");

                    // Store the file in the result storage
                    var fileName = Path.GetFileName(filePath);
                    var destinationPath = Path.Combine(StoragePath, fileName);

                    File.Move(filePath, destinationPath);

                    Console.WriteLine($"File stored in Result Storage: {destinationPath}");

                    consumer.Commit(consumeResult);
                }
            }
        }

    }
}
