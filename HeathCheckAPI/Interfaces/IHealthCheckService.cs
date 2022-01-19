using DaprHHeathCheckAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeathCheckAPI.Interfaces
{
    public interface IHealthCheckService
    {
        Task<IEnumerable<APIConfig>> GetHealth();
    }
}
