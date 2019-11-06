using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    /// <summary>
    /// This check always returns true
    /// </summary>
    public class ObserverDefaultCheck : ObserverCheckBase
    {
        public ObserverDefaultCheck(Guid checkIdentifier,
            bool proceedWithoutPreviousResult) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator: null, tracingFactory: null)
        {

        }

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            myITracing.Information("Perform check End");
            return true;
        }
    }
}