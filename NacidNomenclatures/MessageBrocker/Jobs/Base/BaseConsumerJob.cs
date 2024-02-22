using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MessageBroker.Consumer;

namespace MessageBroker.Jobs.Base
{
    public abstract class BaseConsumerJob : BackgroundService
    {
        private IModel channel;
        private string queueName;
        private readonly NomenclaturesMbConsumer nomenclaturesMbConsumer;

        public BaseConsumerJob(
            string exchangeName,
            NomenclaturesMbConsumer nomenclaturesMbConsumer
        )
        {
            this.nomenclaturesMbConsumer = nomenclaturesMbConsumer;
            InitializeChannel(exchangeName);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (model, bodyStream) =>
            {
                var body = bodyStream.Body.ToArray();
                try
                {
                    await HandleBody(body);
                    channel.BasicAck(bodyStream.DeliveryTag, false);
                }
                catch (Exception)
                {
                    channel.BasicReject(bodyStream.DeliveryTag, true);
                }
            };

            channel.BasicConsume(queue: queueName,
                                autoAck: false,
                                consumer: consumer);

            return Task.CompletedTask;
        }

        protected virtual async Task HandleBody(byte[] body)
        {
            await Task.CompletedTask;
        }

        private void InitializeChannel(string exchangeName)
        {
            channel = nomenclaturesMbConsumer.connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                             exchange: exchangeName,
                             routingKey: "");
        }
    }
}
