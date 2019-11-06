using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class ServiceConfigurationInformation
    {
        public IList<ActionInformation> Actions { get; set; }
        public IList<CheckInformation> Checks { get; set; }
        public IList<EventInformation> Events{ get; set; }
    }
}
