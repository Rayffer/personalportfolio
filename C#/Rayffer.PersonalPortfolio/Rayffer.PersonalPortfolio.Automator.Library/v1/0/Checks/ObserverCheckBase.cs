using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using Unity.Attributes;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverCheckBase : IObserverCheck
    {
        protected readonly ITracing myITracing;
        protected readonly Guid checkIdentifier;
        protected readonly bool proceedWithoutPreviousResult;
        protected readonly IDataValidator dataValidator;

        public bool IsMandatory { get; set; }
        public Types.PriorityTypes Priority { get; set; }

        [Dependency]
        protected IObserverMemoryProvider MemoryProvider { get; set; }

        public ObserverCheckBase(Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.proceedWithoutPreviousResult = proceedWithoutPreviousResult;
            this.dataValidator = dataValidator;
            this.checkIdentifier = checkIdentifier == Guid.Empty ? Guid.NewGuid() : checkIdentifier;
        }

        public virtual bool PerformCheck()
        {
            return true;
        }
    }
}