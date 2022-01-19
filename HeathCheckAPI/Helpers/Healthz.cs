using HeathCheckAPI.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeathCheckAPI.Helpers
{
    public class Healthz : IHealthCheck
    {
        private readonly IHealthCheckService _healthCheckService;
        public Healthz(IHealthCheckService healthCheckService)
        {
            this._healthCheckService = healthCheckService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthResult = await this._healthCheckService.GetHealth();
            return HealthCheckResult.Healthy(JsonConvert.SerializeObject(healthResult));
        }
    }
}
