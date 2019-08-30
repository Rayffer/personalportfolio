using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;

namespace Rayffer.PersonalPortfolio.UnityFactory.Classes.ServiceClientDependencies
{
    public class WcfClientServiceClientDepency : IServiceClientDependency
    {
        private readonly IServiceClientDependency serviceClientDependency;
        private readonly ILoggingDependency loggingDependency;

        public WcfClientServiceClientDepency(IServiceClientDependency serviceClientDependency,
            ILoggingDependency loggingDependency)
        {
            this.serviceClientDependency = serviceClientDependency;
            this.loggingDependency = loggingDependency;
        }
    }
}