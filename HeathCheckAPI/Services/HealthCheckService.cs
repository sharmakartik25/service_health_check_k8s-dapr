using DaprHHeathCheckAPI.Entities;
using HeathCheckAPI.Entities;
using HeathCheckAPI.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HeathCheckAPI.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IDependancyLocator _dependancyLocator;

        private readonly HttpClient client = new HttpClient();

        private readonly ILogger<HealthCheckService> _logger;

        public HealthCheckService(ILogger<HealthCheckService> logger, IDependancyLocator dependancyLocator)
        {
            this._dependancyLocator = dependancyLocator;
            this._logger = logger;
        }

        public async Task<IEnumerable<APIConfig>> GetHealth()
        {
            var timer = new Stopwatch();

            var dependantAPIs = this._dependancyLocator.FindDependantAPIs();

            foreach (var api in dependantAPIs)
            {
                timer.Start();

                var result = await CheckAPIHealthAsync(api.url);

                while (!result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Dependant API {api.name} health not ready, will wait for 2 secs.");

                    Thread.Sleep(2000);

                    if (timer.ElapsedMilliseconds > 20000)
                    {
                        _logger.LogError($"Dependant API {api.name} health still not ready, waited for 20 secs. Now exiting.");
                        api.ishealthy = false;
                        break;
                    }
                    
                }

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Dependant API {api.name}, URL: {api.url} health is up and ready.");
                    api.ishealthy = true;
                }

            }
            return dependantAPIs;
        }
        private async Task<HttpResponseMessage> CheckAPIHealthAsync(string url)
        {
            var response = await client.GetAsync(url);

            return response;
        }
    }
}
