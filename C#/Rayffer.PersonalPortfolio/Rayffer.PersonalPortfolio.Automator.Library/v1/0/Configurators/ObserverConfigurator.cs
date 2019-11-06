using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Observers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Configurators
{
    public class ObserverConfigurator : IObserverConfigurator, IDisposable
    {
        private readonly IObserverConfigurationProvider configurationProvider;
        private readonly IEventFactory eventFactory;
        private readonly ICheckFactory checkFactory;
        private readonly IActionFactory actionFactory;
        private readonly ITracing myITracing;
        private bool configuredOnce;

        public ObserverConfigurator(IObserverConfigurationProvider configurationProvider,
            IEventFactory eventFactory,
            ICheckFactory checkFactory,
            IActionFactory actionFactory,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());

            this.configurationProvider = configurationProvider;
            this.eventFactory = eventFactory;
            this.checkFactory = checkFactory;
            this.actionFactory = actionFactory;

            configuredOnce = false;

            SubcribeToEvent(configurationProvider);
        }

        private void SubcribeToEvent(IObserverConfigurationProvider configurationProvider)
        {
            configurationProvider.ConfigurationChanged -= ConfigurationProvider_ConfigurationChanged;
            configurationProvider.ConfigurationChanged += ConfigurationProvider_ConfigurationChanged;
        }

        private void ConfigurationProvider_ConfigurationChanged(object sender, EventArgs e)
        {
            myITracing.Information("The service configuration has been changed, starting the service reconfiguration.");
            LoadObserversFromConfiguration();
            myITracing.Information("Service reconfiguration ended");
        }

        public IList<IObserver> Observers { get; set; }

        public event EventHandler ConfigurationReadSuccesful;

        public event EventHandler ConfigurationReadUnsuccesful;

        public void LoadObserversFromConfiguration()
        {
            try
            {
                myITracing.Information("Retrieving new service configuration information");
                List<ServiceConfigurationInformation> configurationInformationCollection = configurationProvider.GetServiceConfigurationInformation();

                Observers = new List<IObserver>();
                foreach (ServiceConfigurationInformation configurationInformation in configurationInformationCollection)
                {
                    List<IObserverActionManager> observerActions = new List<IObserverActionManager>();
                    List<IObserverCheck> observerChecks = new List<IObserverCheck>();

                    observerActions = configurationInformation.Actions.Select(action =>
                        actionFactory.GetObserverAction(action)).ToList();

                    observerChecks = configurationInformation.Checks.Select(check =>
                        checkFactory.GetObserverCheck(check)).ToList();

                    IObserverCheckManager observerCheckManager = checkFactory.GetObserverChecksManager(observerChecks);

                    configurationInformation.Events.ToList().ForEach(eventTrigger =>
                    {
                        IObserverEvent observerEvent = eventFactory.GetObserverEvent(eventTrigger);
                        Observer currentObserver = new Observer(observerEvent,
                            observerCheckManager,
                            observerActions);

                        Observers.Add(currentObserver);
                        myITracing.Information($"new observer added to observer list, current amount of observers {Observers.Count}");
                    });
                }

                myITracing.Information("The new service configuration is valid, proceeding to reconfigure the service");
                configuredOnce = true;
                ConfigurationReadSuccesful(this, null);
            }
            catch (Exception ex)
            {
                // Este if se usa para controlar la primera carga del servicio, si la configuración no es válida, se cerrará el servicio
                // porque no tiene sentido dejarlo en ejecución
                if (!configuredOnce)
                {
                    myITracing.Error("An error has ocurred while initializing the service for the first time", ex);
                }
                else
                {
                    myITracing.Warning("An error has ocurred while parsing the new configuration of the service", ex);
                }
                ConfigurationReadUnsuccesful?.Invoke(this, null);
            }
        }

        private void UnsuscribeFromEvent()
        {
            configurationProvider.ConfigurationChanged -= ConfigurationProvider_ConfigurationChanged;
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UnsuscribeFromEvent();
                }
                disposedValue = true;
            }
        }

        ~ObserverConfigurator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}