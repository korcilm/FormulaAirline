using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace FormulaAirline.API.Services
{
    public class MessageProducer : IMessageProducer
    {      
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            };
            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            channel.QueueDeclare("bookings", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "bookings", body: body);

            channel.Close();
            conn.Close();
        }

    }

}

