using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using Unity.Attributes;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public abstract class ObserverEventBase : IObserverEvent, IDisposable
    {
        protected readonly Guid eventTriggerIdentifier;
        protected readonly bool proceedWithoutPreviousResult;
        protected readonly IObserverEventTrigger observerEventTrigger;
        protected readonly IDataValidator dataValidator;
        protected readonly ITracing myITracing;

        [Dependency]
        protected IObserverMemoryProvider MemoryProvider { get; set; }

        public ObserverEventBase(Guid eventTriggerIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.proceedWithoutPreviousResult = proceedWithoutPreviousResult;
            this.dataValidator = dataValidator;
            this.observerEventTrigger = observerEventTrigger;

            this.eventTriggerIdentifier = eventTriggerIdentifier == Guid.Empty ? Guid.NewGuid() : eventTriggerIdentifier;

            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            observerEventTrigger.TriggerEvent += ObserverEventTrigger_TriggerEvent;
        }

        public event EventHandler ObserverEvent;

        private void ObserverEventTrigger_TriggerEvent(object sender, EventArgs e)
        {
            EvaluateAndFireEvent();
        }

        protected abstract void EvaluateAndFireEvent();

        public void StartEventTrigger()
        {
            observerEventTrigger.SubscribeAndStartEventTrigger();
        }

        public void StopEventTrigger()
        {
            observerEventTrigger.StopAndUnsuscribeEventTrigger();
        }

        protected void OnEvent()
        {
            if (ObserverEvent != null)
            {
                myITracing.Verbose("Event Fired Start");
                ObserverEvent.Invoke(this, EventArgs.Empty);
                myITracing.Verbose("Event Fired End");
            }
            else
            {
                myITracing.Warning("The ObserverEvent has not been defined");
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;

                if (!disposing)
                    return;

                UnsubscribeEvent();
            }
        }

        private void UnsubscribeEvent()
        {
            observerEventTrigger.TriggerEvent -= ObserverEventTrigger_TriggerEvent;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}