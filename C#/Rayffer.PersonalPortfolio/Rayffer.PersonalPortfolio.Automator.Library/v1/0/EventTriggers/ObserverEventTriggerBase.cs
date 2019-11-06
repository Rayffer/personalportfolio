using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers
{
    public abstract class ObserverEventTriggerBase : IObserverEventTrigger, IDisposable
    {
        protected readonly ITracing myITracing;
        protected readonly IDataValidator dataValidator;

        public ObserverEventTriggerBase(
            IDataValidator dataValidator,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.dataValidator = dataValidator;
        }

        public event EventHandler TriggerEvent;

        public abstract void SubscribeAndStartEventTrigger();

        public abstract void StopAndUnsuscribeEventTrigger();

        protected abstract void SubscribeEvent();

        protected abstract void UnsubscribeEvent();

        protected void OnEvent()
        {
            if (TriggerEvent != null)
            {
                myITracing.Verbose("Event Fired Start");
                TriggerEvent.Invoke(this, EventArgs.Empty);
                myITracing.Verbose("Event Fired End");
            }
            else
            {
                myITracing.Warning("The TriggerEvent has not been defined");
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
                if (!disposing)
                    return;

                StopAndUnsuscribeEventTrigger();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}