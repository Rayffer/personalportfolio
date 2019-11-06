using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverIniFileValueChangedCheck : ObserverCheckBase
    {
        private readonly string fileToWatch;
        private readonly string sectionToWatch;
        private readonly string keyToWatch;

        public ObserverIniFileValueChangedCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string fileToWatch,
            string sectionToWatch,
            string keyToWatch) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(fileToWatch))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
            if (!dataValidator.ValidateData(sectionToWatch))
            {
                throw new ArgumentException(dataValidator.GetErrorString());
            }
            if (!dataValidator.ValidateData(keyToWatch))
            {
                throw new ArgumentException(dataValidator.GetErrorString());
            }

            this.fileToWatch = fileToWatch;
            this.sectionToWatch = sectionToWatch;
            this.keyToWatch = keyToWatch;
        }

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            bool checkResult = false;
            List<string> fileLines = File.ReadAllLines(fileToWatch).ToList();
            string fieldValue = string.Empty;
            if (MemoryProvider.MemoryObject != null)
            {
                fieldValue = fileLines.FirstOrDefault(fileLine => fileLine.StartsWith(keyToWatch))?.Split('=').Last();
                string sectionHeader = fileLines.FirstOrDefault(fileLine => fileLine.StartsWith(sectionToWatch));
                if (string.IsNullOrEmpty(sectionHeader))
                {
                    myITracing.Information($"The section {sectionToWatch} is not present in the currently watched file");
                }
                else
                {
                    int sectionHeaderIndex = fileLines.IndexOf(sectionHeader);
                    int sectionFooterIndex =
                        fileLines
                        .Skip(sectionHeaderIndex + 1)
                        .ToList()
                        .IndexOf(
                            fileLines
                            .Skip(sectionHeaderIndex + 1)
                            .FirstOrDefault(fileLine => fileLine.Contains("[")));

                    List<string> fileSectionLines = fileLines.Skip(sectionHeaderIndex).Take(sectionFooterIndex).ToList();
                    fieldValue = fileSectionLines.FirstOrDefault(fileLine => fileLine.StartsWith(keyToWatch))?.Split('=').Last();
                }
                if (string.IsNullOrEmpty(fieldValue))
                {
                    myITracing.Information($"The key {keyToWatch} is not present in the currently watched file and/or section");
                }
                else
                {
                    string lastFieldValue = (string)MemoryProvider.MemoryObject;
                    if (!fieldValue.Equals(lastFieldValue))
                    {
                        checkResult = true;
                    }
                }
            }
            else
            {
                if (proceedWithoutPreviousResult)
                {
                    checkResult = true;
                }
            }

            MemoryProvider.MemoryObject = fieldValue;
            myITracing.Information("Perform check End");
            return checkResult;
        }
    }
}