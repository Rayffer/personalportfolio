using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public class ObserverIniFileValueChangedEvent : ObserverEventBase
    {
        private readonly string fileToWatch;
        private readonly string sectionToWatch;
        private readonly string keyToWatch;

        public ObserverIniFileValueChangedEvent(
            Guid eventTriggerIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger,
            string fileToWatch,
            string sectionToWatch,
            string keyToWatch) : base(eventTriggerIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory, observerEventTrigger)
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

        protected override void EvaluateAndFireEvent()
        {
            myITracing.Information("Event Fired Start");
            List<string> fileLines = File.ReadAllLines(fileToWatch).ToList();
            string fieldValue = string.Empty;
            if (MemoryProvider.MemoryObject != null)
            {
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
                        base.OnEvent();
                    }
                }
            }
            else
            {
                if (proceedWithoutPreviousResult)
                {
                    base.OnEvent();
                }
            }

            MemoryProvider.MemoryObject = fieldValue;
            myITracing.Information("Event Fired Ended");
        }
    }
}