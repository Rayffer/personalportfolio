using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceClientDependencies
{
    public class RealServiceClientDependency : IServiceClientDependency
    {
        private readonly IDatabaseDependency databaseDependency;
        private readonly IMappingDependency mappingDependency;
        private readonly ILoggingDependency loggingDependency;

        public RealServiceClientDependency(IDatabaseDependency databaseDependency,
            IMappingDependency mappingDependency,
            ILoggingDependency loggingDependency)
        {
            this.databaseDependency = databaseDependency;
            this.mappingDependency = mappingDependency;
            this.loggingDependency = loggingDependency;
        }
    }
}
