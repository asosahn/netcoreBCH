using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using test2.Entities;

namespace test2.Services
{
    public interface ISendQueue
    {
        Queue sendToQueue(Queue queue);
    }
    public class SendQueue : ISendQueue
    {
        public Queue sendToQueue(Queue queue)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queue.Name,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var message = queue.Message;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: queue.Name,
                                     basicProperties: properties,
                                     body: body);
                return queue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}