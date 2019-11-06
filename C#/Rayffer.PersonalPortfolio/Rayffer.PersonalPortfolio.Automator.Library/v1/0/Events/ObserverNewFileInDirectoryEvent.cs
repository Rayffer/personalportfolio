using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public class ObserverNewFileInDirectoryEvent : ObserverEventBase
    {
        public ObserverNewFileInDirectoryEvent(
            Guid eventTriggerIdentifier,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger) : base(eventTriggerIdentifier, false, null, tracingFactory, observerEventTrigger)
        {
        }

        protected override void EvaluateAndFireEvent()
        {
            myITracing.Information("Event Fired Start");
            base.OnEvent();
            myITracing.Information("Event Fired Ended");
        }
    }
}