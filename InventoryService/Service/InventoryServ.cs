
using InventoryService.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InventoryService.Service
{
    public class InventoryServ : IHostedService
    {

        private IConnection _connection;
        private IModel _channel;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "orderQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Order>(message);
                ProcessOrder(order);
            };

            _channel.BasicConsume(queue: "orderQueue",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }


        private void ProcessOrder(Order order)
        {
            // Lógica para procesar el pedido
            Console.WriteLine($"Processing order {order.OrderId}: {order.ProductName} x {order.Quantity}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
