using System.Data;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IDataBaseConnectionProvider
    {
        bool OpenConnection();

        void CloseConnection();

        IDataReader ExecuteQuery(string query);
    }
}