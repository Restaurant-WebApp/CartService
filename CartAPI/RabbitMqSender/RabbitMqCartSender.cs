using CartAPI.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace CartAPI.RabbitMqSender
{
    public class RabbitMqCartSender : IRabbitMqCartSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private  IConnection _connection;

        public RabbitMqCartSender()
        {
            _hostname = "Localhost";
            _password = "guest";
            _username = "guest";
        }
        public void SendMessage(CheckoutHeader checkoutMessage, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                Password = _password,
                UserName = _username,
            };
            _connection = factory.CreateConnection();
            
            //creating channel
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue:queueName, false, false, false, arguments: null);
            var json = JsonConvert.SerializeObject(checkoutMessage);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);





        }
    }
}
