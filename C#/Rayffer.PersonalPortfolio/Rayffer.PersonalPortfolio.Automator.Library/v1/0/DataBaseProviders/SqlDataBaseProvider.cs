using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataBaseProviders
{
    public class SqlDataBaseProvider : IDataBaseConnectionProvider
    {
        private SqlConnection cnn;
        private readonly string connectionString;

        public SqlDataBaseProvider(string connectionString)
        {
            // TODO : Inyectar log
            this.connectionString = connectionString;
        }

        public IDataReader ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = query;
                sqlCommand.Connection = cnn;

                return sqlCommand.ExecuteReader();
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool OpenConnection()
        {
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                // TODO: Configurar el Log
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void CloseConnection()
        {
            cnn.Close();
        }
    }
}