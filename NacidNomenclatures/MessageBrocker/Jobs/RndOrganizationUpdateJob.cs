using Infrastructure.AppSettings;
using MessageBroker.Jobs.Base;
using System.Text;
using System.Threading.Tasks;
using System;
using MessageBroker.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Models.Models.Nomenclatures.Institution;
using Newtonsoft.Json;
using MessageBroker.Services;

namespace MessageBroker.Jobs
{
    public class RndOrganizationUpdateJob: BaseConsumerJob
    {
        private readonly IServiceProvider serviceProvider;

        public RndOrganizationUpdateJob(
            IServiceProvider serviceProvider,
            NomenclaturesMbConsumer nomenclaturesMbConsumer
        ) : base(AppSettingsProvider.MessageBroker.NomenclaturesConsumer.RndOrganizationUpdateExchange, nomenclaturesMbConsumer)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task HandleBody(byte[] body)
        {
            using var scope = serviceProvider.CreateScope();
            var rndOrganizationUpdateService = scope.ServiceProvider
                .GetRequiredService<RndOrganizationUpdateService>();

            Institution organizationForUpdate = null;

            try
            {
                organizationForUpdate = JsonConvert.DeserializeObject<Institution>(Encoding.UTF8.GetString(body));
                await rndOrganizationUpdateService.UpdateOrganization(organizationForUpdate);
            }
            catch (Exception exception)
            {
                while (exception.InnerException != null)
                { exception = exception.InnerException; }
            }
        }
    }
}
