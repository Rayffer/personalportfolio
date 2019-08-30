using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceExamples
{
    public class ServiceExampleUnityInjectionConstructor : IServiceExample
    {
        private readonly IMappingDependency mappingDependency;
        private readonly IServiceClientDependency serviceClientDependency;
        private readonly ILoggingDependency loggingDependency;

        public ServiceExampleUnityInjectionConstructor(IMappingDependency mappingDependency,
            IServiceClientDependency serviceClientDependency,
            ILoggingDependency loggingDependency)
        {
            this.mappingDependency = mappingDependency;
            this.serviceClientDependency = serviceClientDependency;
            this.loggingDependency = loggingDependency;
        }
    }
}