using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverFileHashCheck : ObserverCheckBase
    {
        private readonly string filePath;
        private readonly IFileProvider fileProvider;
        private readonly IHashingProvider hashProvider;

        public ObserverFileHashCheck(
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
        }

        public override bool PerformCheck()
        {
            try
            {
                myITracing.Information("Perform check Start");
                string allText = fileProvider.GetInfo(filePath);
                string[] content = allText.Split(new string[] { "signature-" }, StringSplitOptions.RemoveEmptyEntries);
                string value = content[0];
                string key = content[1].Trim();

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