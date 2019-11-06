using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public class ObserverFileDateTimeChangedEvent : ObserverEventBase
    {
        private readonly string fileDirectory;
        private readonly string fileToWatch;

        public ObserverFileDateTimeChangedEvent(
            Guid eventTriggerIdentifier,
            bool proceedWithoutPreviosResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger,
            string fileDirectory,
            string fileToWatch) : base(eventTriggerIdentifier, proceedWithoutPreviosResult, dataValidator, tracingFactory, observerEventTrigger)
        {
            if (!dataValidator.ValidateData(fileDirectory))
            {
                throw new DirectoryNotFoundException(dataValidator.GetErrorString());
            }
            if (!dataValidator.ValidateData(fileToWatch))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
            this.fileDirectory = fileDirectory;
            this.fileToWatch = fileToWatch;
        }

        protected override void EvaluateAndFireEvent()
        {
            myITracing.Information("Event Fired Start");
            if (MemoryProvider.MemoryObject != null)
            {
                DateTime lastModifiedDate = System.IO.File.GetLastWriteTimeUtc($@"{fileDirectory}\{fileToWatch}");
                DateTime memoryLastModifiedDate = (DateTime)MemoryProvider.MemoryObject;
                if (lastModifiedDate > memoryLastModifiedDate)
                {
                    base.OnEvent();
                }
            }
            else if (proceedWithoutPreviousResult)
            {
                base.OnEvent();
            }
            MemoryProvider.MemoryObject = System.IO.File.GetLastWriteTimeUtc($@"{fileDirectory}\{fileToWatch}");
            myITracing.Information("Event Fired End");
        }
    }
}