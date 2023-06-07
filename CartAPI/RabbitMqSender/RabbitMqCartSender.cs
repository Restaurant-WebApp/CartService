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

        public RabbitMqCartSender(IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMQSettings");
            _hostname = rabbitMqSettings["HostName"];
            _password = rabbitMqSettings["Password"];
            _username = rabbitMqSettings["UserName"];
        }
        public void SendMessage(CheckoutHeader checkoutMessage, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null); //Type of channel
                var json = JsonConvert.SerializeObject(checkoutMessage);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }           

        }
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                //log exception
            }
        }
    }
}
