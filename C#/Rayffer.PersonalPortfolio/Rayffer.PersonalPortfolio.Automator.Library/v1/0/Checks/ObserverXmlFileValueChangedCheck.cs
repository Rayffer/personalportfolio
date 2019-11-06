using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;
using System.Xml;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks
{
    public class ObserverXmlFileValueChangedCheck : ObserverCheckBase
    {
        private readonly string fileToWatch;
        private readonly string keyToWatch;

        public ObserverXmlFileValueChangedCheck(
            Guid checkIdentifier,
            bool proceedWithoutPreviousResult,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string fileToWatch,
            string keyToWatch) : base(checkIdentifier, proceedWithoutPreviousResult, dataValidator, tracingFactory)
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

        public override bool PerformCheck()
        {
            myITracing.Information("Perform check Start");
            bool checkResult = false;
            XmlDocument docu = new XmlDocument();

            docu.Load(fileToWatch);
            XmlNode xmlNode = docu.SelectSingleNode(keyToWatch);
            if (MemoryProvider.MemoryObject != null)
            {
                XmlNode lastXmlNodeValue = (XmlNode)MemoryProvider.MemoryObject;
                if (!xmlNode.InnerText.Equals(lastXmlNodeValue.InnerText))
                {
                    checkResult = true;
                }
            }
            else
            {
                checkResult = proceedWithoutPreviousResult;
            }
            MemoryProvider.MemoryObject = xmlNode;

            myITracing.Information("Perform check End");
            return checkResult;
        }
    }
}