using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library
{
    public class Log4NetTracingFactory : ITracingFactory
    {
        public ITracing GetTracing(Type OwnerClassType)
        {
            if (OwnerClassType == null)
                throw (new ArgumentNullException("OwnerClassType"));

            ITracing tracing = new Log4NetTracing(OwnerClassType);
            return tracing;
        }
    }
}