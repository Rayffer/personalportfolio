using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class CheckInformation
    {
        public Guid Identifier { get; set; }
        public CheckTypes CheckType { get; set; }
        public bool ProceedWithoutPreviousResult { get; set; }
        public bool IsMandatory { get; set; }
        public PriorityTypes Priority { get; set; }
        public string FilePath { get; set; }
        public string KeyToWatch { get; set; }
        public string SectionToWatch{ get; set; }
        public string FileToWatch { get; set; }
        public string ConnectionString { get; set; }
        public HashingTypes HashType { get; set; }
        public string FileFilter { get; set; }
        public string DatabaseQuery { get; set; }
        public string DirectoryToWatch { get; set; }
    }
}