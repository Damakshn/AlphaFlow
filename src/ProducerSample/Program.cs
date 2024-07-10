using Confluent.Kafka;
using LoremNET;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092,localhost:9093,localhost:9094",
    ClientId = "ProducerSample"
};

using var producer = new ProducerBuilder<string, string>(config).Build();

Console.WriteLine("------------- BEGIN -----------------");

while (true)
{
    var message = Lorem.Sentence(10);
    var key = Guid.NewGuid();
    var result = await producer.ProduceAsync("main.feed", new Message<string, string> { Value = message, Key = key.ToString() });
    
    Console.WriteLine($"Message {message} was pushed to Kafka, delivery status - {result.Status}, partition - {result.Partition.Value}");
    Console.Write("Press \"Enter\" to write more or ctrl+c to exit ");
    Console.ReadLine();
}

Console.WriteLine("------------- END -----------------");