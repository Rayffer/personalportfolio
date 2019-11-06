using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Data;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public class ObserverDataBaseQueryResultChangedEvent : ObserverEventBase
    {
        private readonly IDataBaseConnectionProvider dataBaseConnectionProvider;
        private readonly string databaseQuery;

        public ObserverDataBaseQueryResultChangedEvent(
            Guid eventTriggerIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger,
            IDataBaseConnectionProvider dataBaseConnectionProvider,
            string databaseQuery) : base(eventTriggerIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory, observerEventTrigger)
        {
            this.dataBaseConnectionProvider = dataBaseConnectionProvider;
            this.databaseQuery = databaseQuery;
        }

        protected override void EvaluateAndFireEvent()
        {
            myITracing.Information("Event Fired Start");
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
                    base.OnEvent();
                }
            }
            else
            {
                if (proceedWithoutPreviousResult)
                    base.OnEvent();
            }
            MemoryProvider.MemoryObject = currentDataReaderResult;
            myITracing.Information("Event Fired End");
        }
    }
}