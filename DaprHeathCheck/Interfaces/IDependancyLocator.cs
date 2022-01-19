using DaprHeathCheck.Entities;

namespace DaprHeathCheck.Interfaces
{
    public interface IDependancyLocator
    {
        public IEnumerable<APIConfig> FindDependantAPIs();
    }
}
