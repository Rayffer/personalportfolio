using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Event
{
    public class EventFactory : IEventFactory
    {
        private readonly ITracing myITracing;
        private readonly IUnityContainer unityContainer;
        private readonly IEventTriggerFactory eventTriggerFactory;

        public EventFactory(IUnityContainer unityContainer,
            IEventTriggerFactory eventTriggerFactory,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.unityContainer = unityContainer.CreateChildContainer();

            ConfigureUnityContainer();
            this.eventTriggerFactory = eventTriggerFactory;
        }

        private void ConfigureUnityContainer()
        {
            this.unityContainer.RegisterType<IObserverEvent,
                ObserverDefaultEvent>(EventTypes.None.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger)));

            this.unityContainer.RegisterType<IObserverEvent,
                ObserverFileDateTimeChangedEvent>(EventTypes.FileDateTimeChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            this.unityContainer.RegisterType<IObserverEvent,
                ObserverIniFileValueChangedEvent>(EventTypes.IniFileValueChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            this.unityContainer.RegisterType<IObserverEvent,
                ObserverNewFileInDirectoryEvent>(EventTypes.NewFilesInDirectory.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger)));

            this.unityContainer.RegisterType<IObserverEvent,
                ObserverXmlFileValueChangedEvent>(EventTypes.XmlFileValueChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            this.unityContainer.RegisterType<IObserverEvent,
                ObserverDataBaseQueryResultChangedEvent>(EventTypes.SQLQueryResultChange.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    typeof(IObserverEventTrigger),
                    typeof(IDataBaseConnectionProvider),
                    new InjectionParameter<string>(null)));
        }

        public IObserverEvent GetObserverEvent(EventInformation eventInformation)
        {
            Validate(eventInformation);

            IEnumerable<ParameterOverride> parameterOverrides = GenerateParameterOverrides(eventInformation);
            IObserverEvent observerEvent = unityContainer.Resolve<IObserverEvent>(eventInformation.EventType.ToString(),
                parameterOverrides.ToArray());

            myITracing.Information($"Resolved a new instance of an observer event trigger of type {observerEvent.GetType()}.");

            return observerEvent;
        }

        private void Validate(EventInformation eventInformation)
        {
            if (eventInformation.EventType == EventTypes.NewFilesInDirectory && eventInformation.EventTriggerType != EventTriggerTypes.NewFilesInDirectory)
                throw new ArgumentException();
        }

        private IEnumerable<ParameterOverride> GenerateParameterOverrides(EventInformation eventInformation)
        {
            List<ParameterOverride> parameterOverrides = new List<ParameterOverride>();

            parameterOverrides.Add(CreateNewparameterOverride("eventTriggerIdentifier", eventInformation.Identifier));
            parameterOverrides.Add(CreateNewparameterOverride("proceedWithoutPreviousResult", eventInformation.ProceedWithoutPreviousResult));
            parameterOverrides.Add(CreateNewparameterOverride("observerEventTrigger", eventTriggerFactory.GetObserverEventTrigger(eventInformation)));
            switch (eventInformation.EventType)
            {
                case EventTypes.None:
                    break;

                case EventTypes.FileDateTimeChanged:
                    parameterOverrides.Add(CreateNewparameterOverride("fileDirectory", eventInformation.DirectoryToWatch ?? string.Empty));
                    parameterOverrides.Add(CreateNewparameterOverride("fileToWatch", eventInformation.FileToWatch ?? string.Empty));

                    break;

                case EventTypes.NewFilesInDirectory:
                    break;

                case EventTypes.SQLQueryResultChange:
                    parameterOverrides.Add(CreateNewparameterOverride("databaseQuery", eventInformation.DatabaseQuery ?? string.Empty));
                    break;

                case EventTypes.IniFileValueChanged:
                    parameterOverrides.Add(CreateNewparameterOverride("sectionToWatch", eventInformation.SectionToWatch ?? string.Empty));
                    parameterOverrides.Add(CreateNewparameterOverride("fileToWatch", eventInformation.FileToWatch ?? string.Empty));
                    parameterOverrides.Add(CreateNewparameterOverride("keyToWatch", eventInformation.KeyToWatch ?? string.Empty));
                    break;

                case EventTypes.XmlFileValueChanged:
                    parameterOverrides.Add(CreateNewparameterOverride("fileToWatch", eventInformation.FileToWatch ?? string.Empty));
                    parameterOverrides.Add(CreateNewparameterOverride("keyToWatch", eventInformation.KeyToWatch ?? string.Empty));
                    break;

                case EventTypes.Notdefined:
                default:
                    throw new InvalidOperationException();
            }
            return parameterOverrides;
        }

        private ParameterOverride eCreateNewparameterOverride(string parameterName, EventInformation info, Func<EventInformation, object> getParameterData)
        {
            return new ParameterOverride(parameterName, getParameterData);
        }

        private ParameterOverride CreateNewparameterOverride(string parameterName, object parameterData)
        {
            return new ParameterOverride(parameterName, parameterData);
        }
    }
}