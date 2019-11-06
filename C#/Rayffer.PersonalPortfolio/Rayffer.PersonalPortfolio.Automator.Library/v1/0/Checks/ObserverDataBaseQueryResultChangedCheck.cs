using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Data;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverDataBaseQueryResultChangedCheck : ObserverCheckBase
    {
        private readonly IDataBaseConnectionProvider dataBaseConnectionProvider;
        private readonly string databaseQuery;

        public ObserverDataBaseQueryResultChangedCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IDataBaseConnectionProvider dataBaseConnectionProvider,
            string databaseQuery) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
        {
            this.dataBaseConnectionProvider = dataBaseConnectionProvider;
            this.databaseQuery = databaseQuery;
        }

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            bool checkResult = false;

            IDataReader currentDataReaderResult = dataBaseConnectionProvider.ExecuteQuery(databaseQuery);
            if (MemoryProvider.MemoryObject != null)
            {
                IDataReader previousDataReaderResult = (IDataReader)MemoryProvider.MemoryObject;
                object[] previousObjects = new object[] { };
                previousDataReaderResult.GetValues(previousObjects);
                object[] currentObjects = new object[] { };
                currentDataReaderResult.GetValues(currentObjects);

                if (!currentObjects.SequenceEqual(previousObjects))
                {
                    checkResult = true;
                }
            }
            else
            {
                checkResult = proceedWithoutPreviousResult;
            }

            MemoryProvider.MemoryObject = currentDataReaderResult;
            myITracing.Information("Perform check End");
            return checkResult;
        }
    }
}