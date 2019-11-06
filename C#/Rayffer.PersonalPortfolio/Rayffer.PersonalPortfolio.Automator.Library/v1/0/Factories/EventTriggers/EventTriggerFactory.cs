using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.EventTriggers
{
    public class EventTriggerFactory : IEventTriggerFactory
    {
        private readonly ITracing myITracing;
        private readonly IUnityContainer unityContainer;

        public EventTriggerFactory(IUnityContainer unityContainer,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(GetType());
            this.unityContainer = unityContainer.CreateChildContainer();

            ConfigureUnityContainer();
        }

        private void ConfigureUnityContainer()
        {
            this.unityContainer.RegisterType<IObserverEventTrigger,
                ObserverFileChangedEventTrigger>(EventTriggerTypes.FileChanged.ToString(),
                new InjectionConstructor(
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            this.unityContainer.RegisterType<IObserverEventTrigger,
                ObserverIntervalEventTrigger>(EventTriggerTypes.Interval.ToString(),
                new InjectionConstructor(
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<int>(0)));

            this.unityContainer.RegisterType<IObserverEventTrigger,
                ObserverNewFileInDirectoryEventTrigger>(EventTriggerTypes.NewFilesInDirectory.ToString(),
                new InjectionConstructor(
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            this.unityContainer.RegisterType<IObserverEventTrigger,
                ObserverScheduledEventTrigger>(EventTriggerTypes.Scheduled.ToString(),
                new InjectionConstructor(
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null)));
        }

        public IObserverEventTrigger GetObserverEventTrigger(EventInformation eventInformation)
        {
            IEnumerable<ParameterOverride> parameterOverrides = GenerateParameterOverrides(eventInformation);
            IObserverEventTrigger observerEventTrigger = unityContainer.Resolve<IObserverEventTrigger>(eventInformation.EventTriggerType.ToString(),
                parameterOverrides.ToArray());

            myITracing.Information($"Resolved a new instance of an observer event trigger of type {observerEventTrigger.GetType()}.");

            return observerEventTrigger;
        }

        private IEnumerable<ParameterOverride> GenerateParameterOverrides(EventInformation eventInformation)
        {
            List<ParameterOverride> parameterOverrides = new List<ParameterOverride>();

            switch (eventInformation.EventTriggerType)
            {
                case Types.EventTriggerTypes.FileChanged:
                    parameterOverrides.Add(new ParameterOverride("directoryToWatch", eventInformation.DirectoryToWatch ?? string.Empty));
                    parameterOverrides.Add(new ParameterOverride("fileToWatch", eventInformation.FileToWatch ?? string.Empty));
                    break;

                case Types.EventTriggerTypes.NewFilesInDirectory:
                    parameterOverrides.Add(new ParameterOverride("directoryToWatch", eventInformation.DirectoryToWatch ?? string.Empty));
                    parameterOverrides.Add(new ParameterOverride("fileFilter ", eventInformation.FileFilter ?? string.Empty));
                    break;

                case Types.EventTriggerTypes.Interval:
                    parameterOverrides.Add(new ParameterOverride("intervalSeconds", eventInformation.IntervalSeconds));
                    break;

                case Types.EventTriggerTypes.Scheduled:
                    parameterOverrides.Add(new ParameterOverride("scheduleExpression", eventInformation.DirectoryToWatch ?? string.Empty));
                    break;

                case Types.EventTriggerTypes.NotDefined:
                default:
                    throw new InvalidOperationException();
            }

            return parameterOverrides;
        }
    }
}