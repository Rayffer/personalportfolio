using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataProviders
{
    public class ObserverDatabaseBackupDataProvider : IObserverActionDataToSendProvider<string>
    {
        private readonly string databasePath;
        private readonly string username;
        private readonly string password;

        public ObserverDatabaseBackupDataProvider(string databasePath,
            string username,
            string password)
        {
            this.databasePath = databasePath;
            this.username = username;
            this.password = password;
        }

        public string GetDataToSend()
        {
            return string.Empty;
        }
    }
}