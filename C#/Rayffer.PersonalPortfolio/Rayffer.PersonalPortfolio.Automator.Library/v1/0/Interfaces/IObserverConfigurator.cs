using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverConfigurator
    {
        IList<IObserver> Observers { get; set; }
        event EventHandler ConfigurationReadSuccesful;
        event EventHandler ConfigurationReadUnsuccesful;
        void LoadObserversFromConfiguration();

    }
}
