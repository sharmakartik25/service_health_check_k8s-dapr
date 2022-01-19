using HeathCheckAPI.Helpers;
using HeathCheckAPI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerHealthCheck
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            await builder.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<StartupHostedServiceHealthCheck>();

                    services.AddHealthChecks()
                .AddCheck<StartupHostedServiceHealthCheck>(
                    "hosted_service_startup",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "ready" });

                    services.AddSingleton<IDependancyLocator, DependencyLocator>();
                    services.AddSingleton<IHealthCheck, Healthz>();
                    services.AddSingleton<IHealthCheckService, HeathCheckAPI.Services.HealthCheckService>();

                    services.AddHealthChecks().AddCheck<Healthz>(name: "Dependant_APIs",
                            failureStatus: HealthStatus.Degraded,
                            tags: new[] { "ready" });

                    services.Configure<HealthCheckPublisherOptions>(options =>
                    {
                        options.Delay = TimeSpan.FromSeconds(2);
                        options.Predicate = (check) => check.Tags.Contains("ready");
                    });

                    services.AddSingleton<IHealthCheckPublisher, ReadinessPublisher>();
                });
    }
}
