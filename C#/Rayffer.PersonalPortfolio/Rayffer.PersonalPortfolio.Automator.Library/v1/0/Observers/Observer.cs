using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Observers
{
    public class Observer : IObserver
    {
        public Observer(IObserverEvent observerEvent,
            IObserverCheckManager observerCheckManager,
            IList<IObserverActionManager> observerActions)
        {
            ObserverEvent = observerEvent;
            ObserverCheckManager = observerCheckManager;
            ObserverActions = observerActions;

            ObserverEvent.ObserverEvent -= Trigger_OnTriggerEvent;
            ObserverEvent.ObserverEvent += Trigger_OnTriggerEvent;
        }

        ~Observer()
        {
            ObserverEvent.ObserverEvent -= Trigger_OnTriggerEvent;
        }

        private void Trigger_OnTriggerEvent(object sender, EventArgs e)
        {
            if (!ObserverCheckManager.PerformChecks())
            {
                return;
            }

            foreach (IObserverActionManager action in ObserverActions)
            {
                action.PerformAction();
            }
        }

        public void StartObserver()
        {
            ObserverEvent.StartEventTrigger();
        }

        public void StopObserver()
        {
            ObserverEvent.StopEventTrigger();
        }

        public IObserverEvent ObserverEvent { get; set; }
        public IList<IObserverActionManager> ObserverActions { get; set; }
        public IObserverCheckManager ObserverCheckManager { get; set; }
    }
}