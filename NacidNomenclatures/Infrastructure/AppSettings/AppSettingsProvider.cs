using Infrastructure.AppSettings.MessageBroker;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.AppSettings
{
    public class AppSettingsProvider
    {
        public static string MainDbConnectionString { get; private set; }
        public static MessageBrokerSettings MessageBroker { get; set; }

        public static void AddAppSettings(IConfiguration configuration)
        {
            if (configuration.GetSection("mainDbConnectionString").Exists())
            {
                MainDbConnectionString = configuration.GetSection("mainDbConnectionString").Get<string>();
            }

            if (configuration.GetSection("messageBroker").Exists())
            {
                MessageBroker = configuration.GetSection("messageBroker").Get<MessageBrokerSettings>();
            }
        }
    }
}
