using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserver
    {
        IObserverEvent ObserverEvent { get; set; }
        IObserverCheckManager ObserverCheckManager { get; set; }
        IList<IObserverActionManager> ObserverActions { get; set; }

        void StartObserver();
        void StopObserver();
    }
}
