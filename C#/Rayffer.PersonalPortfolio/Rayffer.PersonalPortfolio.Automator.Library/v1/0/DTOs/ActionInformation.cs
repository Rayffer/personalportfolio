using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class ActionInformation
    {
        public Guid Identifier { get; set; }
        public ActionDataSenderTypes DataSenderType { get; set; }
        public ActionDataTransformTypes DataTransformType { get; set; }
        public ActionDataProvidersTypes DataProvidersType { get; set; }
        public string SourceFilePath{ get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseUserName{ get; set; }
        public string DatabasePassword{ get; set; }
        public string ElementIdentifier { get; set; }
        public string ElementFunction { get; set; }
        public string[] ElementDataIdentifiers { get; set; }
    }
}
