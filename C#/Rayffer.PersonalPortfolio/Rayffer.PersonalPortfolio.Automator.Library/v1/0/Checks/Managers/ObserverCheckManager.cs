using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks.Managers
{
    public class ObserverCheckManager : IObserverCheckManager
    {
        private readonly IList<IObserverCheck> checksToPerform;
        private readonly ITracing myITracing;
        // hacer una lista donde almacenar resultados

        public ObserverCheckManager(IList<IObserverCheck> checksToPerform,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.checksToPerform = checksToPerform;
        }

        public bool PerformChecks()
        {
            // limpiar lista de errores
            if (!CheckMandatory())
            {
                return false;
            }

            if ((checksToPerform.Count(check => !check.IsMandatory) > 0) && !CheckOptional())
            {
                return false;
            }

            //Si hay errores, logar y enviar donde toque

            return true;
        }

        protected virtual bool CheckMandatory()
        {
            bool mandatoryCheck =
                checksToPerform
                .Where(check => check.IsMandatory)
                .OrderBy(check => check.Priority)
                .All(check =>
                {
                    var result = check.PerformCheck();
                    //Lista.add(result);
                    return result;
                });

            if (!mandatoryCheck)
            {
                // TODO : Log
            }
            return mandatoryCheck;
        }

        protected virtual bool CheckOptional()
        {
            bool optionalCheck =
                checksToPerform
                .Where(check => !check.IsMandatory)
                .OrderBy(check => check.Priority)
                .AsParallel()
                .Any(check =>
                {
                    var result = check.PerformCheck();
                    //Lista.add(result);
                    return result;
                });

            if (!optionalCheck)
            {
                // TODO : Log
            }
            return optionalCheck;
        }
    }
}