using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    /// <summary>
    /// This class will always fire the associated event without regarding any conditions
    /// </summary>
    public class ObserverDefaultEvent : ObserverEventBase
    {
        public ObserverDefaultEvent(
            Guid eventTriggerIdentifier,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger) : base(eventTriggerIdentifier, false, null, tracingFactory, observerEventTrigger)
        {
        }

        protected override void EvaluateAndFireEvent()
        {
            base.OnEvent();
        }
    }
}