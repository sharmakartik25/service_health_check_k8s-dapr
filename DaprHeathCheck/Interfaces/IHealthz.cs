
using Microsoft.AspNetCore.Mvc;

namespace DaprHeathCheck.Interfaces
{
    public interface IHealthz
    {
        Task<IActionResult> HealthCheck();
    }
}
