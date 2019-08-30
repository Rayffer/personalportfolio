using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceExamples
{
    public class ServiceExampleUnityDefaultResolution : IServiceExample
    {
        private readonly IServiceClientDependency serviceClientDependency;
        private readonly ILoggingDependency loggingDependency;

        public ServiceExampleUnityDefaultResolution(IServiceClientDependency serviceClientDependency,
            ILoggingDependency loggingDependency)
        {
            this.serviceClientDependency = serviceClientDependency;
            this.loggingDependency = loggingDependency;
        }
    }
}