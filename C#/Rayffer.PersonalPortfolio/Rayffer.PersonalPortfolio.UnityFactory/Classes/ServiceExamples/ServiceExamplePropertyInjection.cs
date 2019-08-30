using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;
using Rayffer.PersonalPortfolio.UnityFactory.Types;
using Unity;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceExamples
{
    public class ServiceExamplePropertyDefinedInjections : IServiceExample
    {
        [Dependency(nameof(DatabaseDependencyTypes.EntityFramework))]
        public IDatabaseDependency DatabaseDependency { get; set; }
        [Dependency(nameof(LoggingDependencyTypes.Log4Net))]
        public ILoggingDependency LoggingDependency { get; set; }

        public ServiceExamplePropertyDefinedInjections()
        {
        }
    }
}