using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverConfigurationProvider
    {
        List<ServiceConfigurationInformation> GetServiceConfigurationInformation();

        event EventHandler ConfigurationChanged;
    }
}
