using DaprHHeathCheckAPI.Entities;
using HeathCheckAPI.Entities;
using HeathCheckAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace HeathCheckAPI.Helpers
{
    public class DependencyLocator : IDependancyLocator
    {
        public readonly ServiceDependencies dependencies;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantLocator"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DependencyLocator(IConfiguration configuration)
        {
            this.dependencies = configuration.Get<ServiceDependencies>();
        }

        public IEnumerable<APIConfig> FindDependantAPIs()
        {
            return this.dependencies?.Dependencies?.FirstOrDefault()?.APIS;
        }
    }
}
