using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;
using Rayffer.PersonalPortfolio.UnityFactory.Types;
using Unity;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceExamples
{
    public class ServiceExampleConstructorDefinedInjections : IServiceExample
    {
        private readonly IDatabaseDependency databaseDependency;
        private readonly IMappingDependency mappingDependency;
        private readonly IServiceClientDependency serviceClientDependency;
        private readonly ILoggingDependency loggingDependency;

        public ServiceExampleConstructorDefinedInjections(
            [Dependency(nameof(DatabaseDependencyTypes.Oracle))]IDatabaseDependency databaseDependency,
            [Dependency(nameof(MappingDependencyTypes.AutoMapper))]IMappingDependency mappingDependency,
            [Dependency(nameof(ServiceClientDependencyTypes.Real))]IServiceClientDependency serviceClientDependency,
            [Dependency(nameof(LoggingDependencyTypes.Log4Net))]ILoggingDependency loggingDependency)
        {
            this.databaseDependency = databaseDependency;
            this.mappingDependency = mappingDependency;
            this.serviceClientDependency = serviceClientDependency;
            this.loggingDependency = loggingDependency;
        }
    }
}