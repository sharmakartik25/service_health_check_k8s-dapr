
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeathCheckAPI.Interfaces
{
    public interface IHealthz
    {
        Task<IActionResult> HealthCheck();
    }
}
