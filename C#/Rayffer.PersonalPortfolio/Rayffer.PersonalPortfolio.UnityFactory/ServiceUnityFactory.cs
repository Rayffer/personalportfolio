using Rayffer.PersonalPortfolio.UnityFactory.Interfaces;
using Rayffer.PersonalPortfolio.UnityFactory.Types;
using Unity;

namespace Rayffer.PersonalPortfolio.UnityFactory
{
    public class ServiceUnityFactory
    {
        private UnityContainer factoryUnityContainer;

        public ServiceUnityFactory()
        {
            factoryUnityContainer = new UnityContainer();
            factoryUnityContainer.AddExtension(new Diagnostic());
            ConfigureUnityFactory.Configure(factoryUnityContainer);
        }

        public IServiceExample GetServiceExample(ServiceExampleTypes serviceExampleType)
        {
            return factoryUnityContainer.Resolve<IServiceExample>(serviceExampleType.ToString());
        }
    }
}