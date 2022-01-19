using DaprHeathCheck.Interfaces;
using DaprHeathCheck.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaprHeathCheck.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Healthz : ControllerBase, IHealthz
    {
        private readonly IHealthCheck _healthCheck;
        public Healthz(IHealthCheck healthCheck)
        {
            this._healthCheck = healthCheck;
        }

        [HttpGet(Name = "healthz")]
        public async Task<IActionResult> HealthCheck()
        {
            if (Enumerable.Range(200, 400).Contains((int)this._healthCheck.GetHealth().Result.StatusCode))
            {
                return Ok(new StringContent("Services Healthy"));
            }

            else return StatusCode(503, new StringContent("Services UnHealthy"));
        }
    }
}
