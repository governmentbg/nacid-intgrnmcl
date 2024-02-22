using Infrastructure.AppSettings;
using MessageBroker.Consumer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Models;
using System.IO;
using System;
using System.Linq;
using System.Reflection;
using Server.Filters;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using MessageBroker.Jobs;
using MessageBroker.Publisher;
using Models.Models.Nomenclatures.Country;

namespace Server.Extensions
{
    public static class InternalServicesExtensions
    {
        public static void ConfigureDbContextService(this IServiceCollection services)
        {
            services
                .AddDbContext<NomenclaturesDbContext>(o =>
                {
                    o.UseNpgsql(AppSettingsProvider.MainDbConnectionString,
                        e => e.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                });
        }

        public static void ConfigureServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddHttpClient();

            var assemblyRepositoryNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Where(e => e.Name == "Infrastructure"
                    || e.Name == "Services"
                    || e.Name == "Models"
                    || e.Name == "MessageBroker");

            foreach (var assemblyRepositoryName in assemblyRepositoryNames)
            {
                var repositoryAssemblyTypes = Assembly.Load(assemblyRepositoryName).GetTypes();

                var allServiceClasses = repositoryAssemblyTypes
                    .Where(e => e.Name.EndsWith("Service"))
                    .ToList();

                foreach (var item in allServiceClasses)
                {
                    services.AddScoped(item);
                }
            }

            services.AddHttpContextAccessor();

            if (AppSettingsProvider.MessageBroker.Enable)
            {
                services.AddSingleton<NomenclaturesMbConsumer>();
                services.AddSingleton<NomenclaturesMbPublisher>();
            }
        }

        public static void StartJobs(this IServiceCollection services)
        {
            if (AppSettingsProvider.MessageBroker.Enable)
            {
                services.AddHostedService<RndOrganizationUpdateJob>();
            }
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NomenclaturesSwagger",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectionMiddleware>();
        }
    }
}
