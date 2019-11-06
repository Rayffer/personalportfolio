using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Timers;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers
{
    public class ObserverIntervalEventTrigger : ObserverEventTriggerBase
    {
        private Timer timer;

        public ObserverIntervalEventTrigger(
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            int intervalSeconds) : base(dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(intervalSeconds))
            {
                throw new ArgumentOutOfRangeException(dataValidator.GetErrorString());
            }
            timer = new Timer(intervalSeconds * 1000)
            {
                AutoReset = true
            };
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            myITracing.Information("Event Fired Start");
            base.OnEvent();
            myITracing.Information("Event Fired End");
        }

        public override void SubscribeAndStartEventTrigger()
        {
            SubscribeEvent();
            timer.Start();
        }

        public override void StopAndUnsuscribeEventTrigger()
        {
            timer.Stop();
            UnsubscribeEvent();
        }

        protected override void UnsubscribeEvent()
        {
            timer.Elapsed -= Timer_Elapsed;
        }

        protected override void SubscribeEvent()
        {
            timer.Elapsed += Timer_Elapsed;
        }
    }
}