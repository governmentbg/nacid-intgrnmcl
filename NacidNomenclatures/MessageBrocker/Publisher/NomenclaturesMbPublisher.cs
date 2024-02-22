using Infrastructure.AppSettings;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Security.Authentication;
using System.Text;
using MessageBroker.Dtos;
using MessageBroker.Enums;
using Models.Models.Nomenclatures.Base;

namespace MessageBroker.Publisher
{
    public class NomenclaturesMbPublisher
    {
        private readonly IConnection connection;

        public NomenclaturesMbPublisher()
        {
            var factory = new ConnectionFactory()
            {
                HostName = AppSettingsProvider.MessageBroker.Host,
                Port = AppSettingsProvider.MessageBroker.Port,
                UserName = AppSettingsProvider.MessageBroker.Username,
                Password = AppSettingsProvider.MessageBroker.Password,
                AutomaticRecoveryEnabled = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(AppSettingsProvider.MessageBroker.HeartbeatTimeout),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(AppSettingsProvider.MessageBroker.NetworkRecoveryInterval),
                DispatchConsumersAsync = true,
                Ssl = new SslOption
                {
                    Enabled = AppSettingsProvider.MessageBroker.SslEnabled,
                    ServerName = AppSettingsProvider.MessageBroker.SslServerName,
                    CertPath = AppSettingsProvider.MessageBroker.SslCertPath,
                    CertPassphrase = AppSettingsProvider.MessageBroker.SslCertPassphrase,
                    Version = SslProtocols.Tls12
                }
            };

            connection = factory.CreateConnection(AppSettingsProvider.MessageBroker.NomenclaturesPublisher.Name);
        }

        public void PublishNomenclatureChange(int? id, object nomenclature, NomenclatureType nomenclatureType, NomenclatureOperation nomenclatureOperation)
        {
            var nomenclatureExchangeDto = new NomenclatureExchangeDto
            {
                Id = id,
                Nomenclature = nomenclature,
                NomenclatureType = nomenclatureType,
                Operation = nomenclatureOperation
            };

            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: AppSettingsProvider.MessageBroker.NomenclaturesPublisher.NomenclatureUpdateExchange, type: ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(nomenclatureExchangeDto,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
                channel.BasicPublish(exchange: AppSettingsProvider.MessageBroker.NomenclaturesPublisher.NomenclatureUpdateExchange,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
