using DaprHHeathCheckAPI.Entities;
using System.Collections.Generic;

namespace HeathCheckAPI.Interfaces
{
    public interface IDependancyLocator
    {
        public IEnumerable<APIConfig> FindDependantAPIs();
    }
}
