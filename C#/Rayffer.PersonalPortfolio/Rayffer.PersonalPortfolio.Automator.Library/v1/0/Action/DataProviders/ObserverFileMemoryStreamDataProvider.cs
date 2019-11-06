using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataProviders
{
    public class ObserverFileMemoryStreamDataProvider : IObserverActionDataToSendProvider<MemoryStream>
    {
        private readonly string filePath;

        public ObserverFileMemoryStreamDataProvider(string filePath)
        {
            this.filePath = filePath;

            // TODO : Validacion
        }

        public MemoryStream GetDataToSend()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
            return memoryStream;
        }
    }
}