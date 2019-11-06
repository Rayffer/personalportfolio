using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataProviders
{
    public class ObserverFileDataProvider : IObserverActionDataToSendProvider<string>
    {
        private readonly string filePath;

        public ObserverFileDataProvider(string filePath)
        {
            this.filePath = filePath;

            // TODO : Validacion
        }

        public string GetDataToSend()
        {
            return filePath;
        }
    }
}