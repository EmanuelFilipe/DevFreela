using DevFreela.Core.Interfaces;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;

        public MessageBusService()
        {
            _factory = new ConnectionFactory { HostName = "localhost"};
            //se estiver em outro servidor, será necessário passar o username e password
            //nesse caso deve ser usado o objeto configuration para isso.
        }

        /// <summary>
        /// will publish the message
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        public void Publish(string queue, byte[] message)
        {
            using(var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //guarantee the queue will be created
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false,
                                         autoDelete: false, arguments: null);

                    //publish the message
                    channel.BasicPublish(exchange: "", routingKey: queue,
                                         basicProperties: null, body: message);
                }
            }
        }
    }
}
