using CartAPI.Messages;

namespace CartAPI.RabbitMqSender
{
    public interface IRabbitMqCartSender
    {
        void SendMessage(CheckoutHeader checkoutHeader, String queewName);

    }
}
