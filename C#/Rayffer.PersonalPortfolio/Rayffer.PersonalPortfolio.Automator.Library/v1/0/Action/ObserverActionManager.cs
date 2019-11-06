using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action
{
    public class ObserverActionManager<ProvidedType, SentType> : IObserverActionManager
    {
        private readonly ITracing myITracing;
        private readonly IObserverActionSender<SentType> actionSender;
        private readonly IObserverActionDataToSendProvider<ProvidedType> dataToSendProvider;
        private readonly IObserverActionDataTransformer<ProvidedType, SentType> dataTransformer;

        public ObserverActionManager(IObserverActionSender<SentType> actionSender,
            IObserverActionDataToSendProvider<ProvidedType> dataToSendProvider,
            IObserverActionDataTransformer<ProvidedType, SentType> dataTransformer,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.actionSender = actionSender;
            this.dataToSendProvider = dataToSendProvider;
            this.dataTransformer = dataTransformer;
        }

        public void PerformAction()
        {
            myITracing.Information("Perform Action Start");

            var dataToSend = dataToSendProvider.GetDataToSend();

            var transformedData = dataTransformer.TransformData(dataToSend);

            actionSender.Send(transformedData);

            myITracing.Information("Perform Action Ended");
        }
    }
}