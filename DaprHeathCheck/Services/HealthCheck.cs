using DaprHeathCheck.Helpers;
using DaprHeathCheck.Interfaces;
using System.Diagnostics;

namespace DaprHeathCheck.Services
{
    public class HealthCheck : IHealthCheck
    {
        private readonly IDependancyLocator _dependancyLocator;

        private readonly HttpClient client = new HttpClient();

        private readonly ILogger<HealthCheck> _logger;

        public HealthCheck(ILogger<HealthCheck> logger, IDependancyLocator dependancyLocator)
        {
            this._dependancyLocator = dependancyLocator;
            this._logger = logger;
        }

        public async Task<HttpResponseMessage> GetHealth()
        {
            var timer = new Stopwatch();

            foreach (var api in this._dependancyLocator.FindDependantAPIs())
            {
                timer.Start();

                var result = CheckAPIHealthAsync(api.url).Result;

                while (!CheckAPIHealthAsync(api.url).Result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Dependant API {api.name} health not ready, will wait for 2 secs.");

                    Thread.Sleep(2000);

                    if (timer.ElapsedMilliseconds > 20000)
                    {
                        _logger.LogError($"Dependant API {api.name} health still not ready, waited for 20 secs. Now exiting.");
                        throw new Exception("Serice unhealthy. Dependencies not up.");
                    }
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }

        private async Task<HttpResponseMessage> CheckAPIHealthAsync(string url)
        {
            var response = await client.GetAsync(url);

            return response;
        }
    }
}
