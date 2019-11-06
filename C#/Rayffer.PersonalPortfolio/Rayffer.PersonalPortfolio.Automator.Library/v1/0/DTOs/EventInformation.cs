using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class EventInformation
    {
        public Guid Identifier { get; set; }
        public EventTriggerTypes EventTriggerType { get; set; }
        public EventTypes EventType { get; set; }
        public string DirectoryToWatch { get; set; }
        public string FileToWatch { get; set; }
        public string KeyToWatch{ get; set; }
        public string SectionToWatch { get; set; }
        public string FileFilter { get; set; }
        public string ConnectionString{ get; set; }
        public string DatabaseQuery { get; set; }
        public int IntervalSeconds { get; set; }
        public bool ProceedWithoutPreviousResult { get; set; }
    }
}
