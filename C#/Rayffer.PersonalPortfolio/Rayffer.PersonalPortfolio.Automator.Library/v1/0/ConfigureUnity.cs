using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks.Managers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.ConfigurationProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Configurators;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataBaseProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataValidators;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Action;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Checks;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Event;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.EventTriggers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Mapper;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Observers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.ActionInformationProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.FileProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.MemoryProviders;
using System.Collections.Generic;
using Unity;
using Unity.Injection;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0
{
    public static class ConfigureUnity
    {
        public static void Configure(IUnityContainer container)
        {
            //#if WITH_DIAGNOSTIC_EXTENSION
            //  ContainerManager.AddNewExtension<Diagnostic>();
            //#endif
            container.RegisterType<IObserverMemoryProvider, NativeCacheMemoryProvider>();

            container.RegisterType<IDataValidator, DataValidator>();

            container.RegisterType<IDataValidator, ActionInformationValidator>("ActionDataValidator");

            container.RegisterType<ITracingFactory, Log4NetTracingFactory>();

            container.RegisterType<IFileProvider, FileProvider>();

            container.RegisterType<IDataBaseConnectionProvider, SqlDataBaseProvider>();

            container.RegisterType<IActionMembersCompatibilityProvider, ActionMembersCompatibilityProvider>();

            container.RegisterType<IObserverMapper, ObserverMapper>();

            container.RegisterType<IActionFactory, ActionFactory>(
                new InjectionConstructor
                (
                    typeof(IUnityContainer),
                    typeof(IObserverMapper),
                    new ResolvedParameter(typeof(IDataValidator), "ActionDataValidator"),
                    typeof(ITracingFactory)
                ));

            container.RegisterType<IEventFactory, EventFactory>();

            container.RegisterType<IEventTriggerFactory, EventTriggerFactory>();

            container.RegisterType<IObserverCheckManager, ObserverCheckManager>();

            container.RegisterType<ICheckFactory, CheckFactory>();

            container.RegisterType<IObserver, Observer>(
                new InjectionConstructor
                (
                    typeof(IObserverEvent),
                    typeof(IObserverCheckManager),
                    typeof(IList<IObserverActionManager>)
                ));

            container.RegisterType<IObserverConfigurationProvider, ObserverDummyConfigurationProvider>();

            container.RegisterType<IObserverConfigurator, ObserverConfigurator>(
                new InjectionConstructor
                (
                    typeof(IObserverConfigurationProvider),
                    typeof(IEventFactory),
                    typeof(ICheckFactory),
                    typeof(IActionFactory),
                    typeof(ITracingFactory)
                ));

            container.RegisterType<IObserverService, ObserverService>();
        }
    }
}