using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverFileDateTimeChangedCheck : ObserverCheckBase
    {
        private readonly string filePath;

        public ObserverFileDateTimeChangedCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string filePath) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
        {
            this.filePath = filePath;
            if (!dataValidator.ValidateData(this.filePath))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
        }

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            bool checkResult = false;
            DateTime fileLastModified = System.IO.File.GetLastWriteTimeUtc(filePath);

            if (MemoryProvider.MemoryObject != null)
            {
                DateTime memoryLastModified = (DateTime)MemoryProvider.MemoryObject;
                checkResult = fileLastModified > memoryLastModified;
            }
            else
            {
                checkResult = proceedWithoutPreviousResult;
            }
            MemoryProvider.MemoryObject = fileLastModified;

            myITracing.Information("Perform check End");
            return checkResult;
        }
    }
}