using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverFileWithFileHashCheck : ObserverCheckBase
    {
        private readonly string filePath;
        private readonly string fileMd5Path;

        private readonly IFileProvider fileProvider;
        private readonly IHashingProvider hashProvider;

        public ObserverFileWithFileHashCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string filePath,
            IFileProvider fileProvider,
            IHashingProvider hashProvider) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
        {
            this.filePath = filePath;

            if (!dataValidator.ValidateData(this.filePath))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }

            this.fileProvider = fileProvider;
            this.hashProvider = hashProvider;

            this.fileMd5Path = Path.Combine(Path.GetFileNameWithoutExtension(filePath), ".md5");
            if (!dataValidator.ValidateData(this.fileMd5Path))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
        }

        public override bool PerformCheck()
        {
            try
            {
                myITracing.Information("Perform check Start");
                string value = fileProvider.GetInfo(filePath);
                string key = fileProvider.GetInfo(fileMd5Path);

                myITracing.Information("Perform check End");
                return hashProvider.CheckFilePerform(key, value);
            }
            catch (Exception ex)
            {
                myITracing.Error(string.Format("An error has ocurred in {0} - {1}", nameof(ObserverFileHashCheck), nameof(PerformCheck)), ex);
                return false;
            }
        }
    }
}