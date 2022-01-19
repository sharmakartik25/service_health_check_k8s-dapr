using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerHealthCheck
{
    public class Worker : BackgroundService
    {
        private readonly int _delaySeconds = 15;
        private readonly ILogger<Worker> _logger;
        private readonly StartupHostedServiceHealthCheck _startupHostedServiceHealthCheck;


        public Worker(ILogger<Worker> logger,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck)
        {
            _logger = logger;
            _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Startup Background Service is starting.");

            await Task.Run(async () =>
            {
                await Task.Delay(_delaySeconds * 1000);

                _startupHostedServiceHealthCheck.StartupTaskCompleted = true;

                _logger.LogInformation("Startup Background Service has started.");
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
