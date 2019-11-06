using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0
{
    public class ObserverService : IObserverService
    {
        private readonly IObserverConfigurator observerConfigurator;
        private readonly ITracing myITracing;
        private IList<IObserver> observers;

        public ObserverService(IObserverConfigurator observerConfigurator,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());

            observers = new List<IObserver>();
            this.observerConfigurator = observerConfigurator;

            observerConfigurator.ConfigurationReadSuccesful -= ObserverConfigurator_ConfigurationReadSuccesful;
            observerConfigurator.ConfigurationReadSuccesful += ObserverConfigurator_ConfigurationReadSuccesful;
            observerConfigurator.ConfigurationReadUnsuccesful += ObserverConfigurator_ConfigurationReadUnsuccesful;
            observerConfigurator.LoadObserversFromConfiguration();
        }

        private void ObserverConfigurator_ConfigurationReadUnsuccesful(object sender, EventArgs e)
        {
            if (observers == null || !observers.Any())
            {
                throw new ApplicationException("An error has ocurred while initializing the service, shutting down");
            }
            else
            {
                myITracing.Information("The new configuration is not valid, discarding the old configuration in favor of the new one");
            }
        }

        private void ObserverConfigurator_ConfigurationReadSuccesful(object sender, EventArgs e)
        {
            // TODO: Eliminar scheduled tasks

            if (observers != null)
            {
                observers.ToList().ForEach(observer =>
                {
                    observer.StopObserver();
                });
                observers.Clear();
            }

            observers = observerConfigurator.Observers;
            observers.ToList().ForEach(observer =>
            {
                observer.StartObserver();
            });
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    observerConfigurator.ConfigurationReadSuccesful -= ObserverConfigurator_ConfigurationReadSuccesful;
                    observerConfigurator.ConfigurationReadUnsuccesful -= ObserverConfigurator_ConfigurationReadUnsuccesful;
                }
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~ObserverService()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(false);
        //}

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}