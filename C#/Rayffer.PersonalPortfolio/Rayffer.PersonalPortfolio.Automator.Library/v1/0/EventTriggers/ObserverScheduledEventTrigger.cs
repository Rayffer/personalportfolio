using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers
{
    public class ObserverScheduledEventTrigger : ObserverEventTriggerBase
    {
        public ObserverScheduledEventTrigger(
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string scheduleExpression) : base(dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(scheduleExpression))
            {
                throw new ArgumentException(dataValidator.GetErrorString());
            }
            ScheduleTime += ObserverScheduledEventTrigger_ScheduleTime;
        }

        private void ObserverScheduledEventTrigger_ScheduleTime(object sender, EventArgs e)
        {
            myITracing.Information("Event Fired Start");
            base.OnEvent();
            myITracing.Information("Event Fired Ended");
        }

        public event EventHandler ScheduleTime;

        public override void SubscribeAndStartEventTrigger()
        {
        }

        public override void StopAndUnsuscribeEventTrigger()
        {
        }

        protected override void UnsubscribeEvent()
        {
        }

        protected override void SubscribeEvent()
        {
        }
    }
}