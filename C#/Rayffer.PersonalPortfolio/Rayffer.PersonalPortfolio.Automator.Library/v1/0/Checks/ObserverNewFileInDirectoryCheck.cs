using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverNewFileInDirectoryCheck : ObserverCheckBase
    {
        private readonly string directoryToWatch;
        private readonly string fileFilter;

        public ObserverNewFileInDirectoryCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string directoryToWatch,
            string fileFilter = "") : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(directoryToWatch))
            {
                throw new DirectoryNotFoundException(dataValidator.GetErrorString());
            }
            this.directoryToWatch = directoryToWatch;

            this.fileFilter = fileFilter;
            if (string.IsNullOrEmpty(fileFilter))
            {
                this.fileFilter = "*.*";
            }
            else
            {
                if (!dataValidator.ValidateData(fileFilter))
                {
                    throw new ArgumentException(dataValidator.GetErrorString());
                }
            }
        }

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            bool checkResult = false;
            var currentDirectoryFiles = System.IO.Directory.GetFiles(directoryToWatch, fileFilter);

            if (MemoryProvider.MemoryObject != null)
            {
                string[] previousDirectoryFiles = (string[])MemoryProvider.MemoryObject;

                if (!previousDirectoryFiles.SequenceEqual(currentDirectoryFiles))
                {
                    checkResult = true;
                }
            }
            else
            {
                checkResult = proceedWithoutPreviousResult;
            }

            MemoryProvider.MemoryObject = currentDirectoryFiles;

            myITracing.Information("Perform check End");
            return checkResult;
        }
    }
}