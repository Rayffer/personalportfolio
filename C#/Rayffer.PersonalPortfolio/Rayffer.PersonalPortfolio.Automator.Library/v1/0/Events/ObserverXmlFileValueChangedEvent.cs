using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;
using System.Xml;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Events
{
    public class ObserverXmlFileValueChangedEvent : ObserverEventBase
    {
        private readonly string fileToWatch;
        private readonly string keyToWatch;

        public ObserverXmlFileValueChangedEvent(
            Guid eventTriggerIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            IObserverEventTrigger observerEventTrigger,
            string fileToWatch,
            string keyToWatch) : base(eventTriggerIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory, observerEventTrigger)
        {
            if (!dataValidator.ValidateData(fileToWatch))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
            if (!dataValidator.ValidateData(keyToWatch))
            {
                throw new ArgumentException(dataValidator.GetErrorString());
            }
            this.fileToWatch = fileToWatch;
            this.keyToWatch = keyToWatch;
        }

        protected override void EvaluateAndFireEvent()
        {
            myITracing.Information("Event Fired Start");
            XmlDocument docu = new XmlDocument();

            docu.Load(fileToWatch);
            XmlNode xmlNode = docu.SelectSingleNode(keyToWatch);
            if (MemoryProvider.MemoryObject != null)
            {
                XmlNode lastXmlNodeValue = (XmlNode)MemoryProvider.MemoryObject;
                if (!xmlNode.InnerText.Equals(lastXmlNodeValue.InnerText))
                {
                    base.OnEvent();
                }
            }
            else if (proceedWithoutPreviousResult)
            {
                base.OnEvent();
            }

            MemoryProvider.MemoryObject = xmlNode;
            myITracing.Information("Event Fired Ended");
        }
    }
}