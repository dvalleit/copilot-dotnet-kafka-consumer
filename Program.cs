// Step 1: Install the Confluent.Kafka NuGet package
// Run the following command in the terminal:
// dotnet add package Confluent.Kafka

using System;
using Confluent.Kafka;

class Program
{
    public static void Main(string[] args)
    {
        // Step 2: Create a configuration object for the consumer
        var config = new ConsumerConfig
        {
            GroupId = "my-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        // Step 3: Create a consumer instance using the configuration
        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            // Step 4: Subscribe to the desired topic
            consumer.Subscribe("my-topic");

            // Step 5: Implement a loop to continuously consume messages
            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();

                    // Step 6: Handle message processing and errors
                    Console.WriteLine($"Consumed message '{consumeResult.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                consumer.Close();
            }
        }
    }
}