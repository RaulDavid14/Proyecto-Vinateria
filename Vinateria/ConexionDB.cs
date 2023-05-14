using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System;
using System.Data.SqlClient;


namespace ConexionSqlServer
{
    public class SqlServerConnection
    {
        private string connectionString;
        private SqlConnection connection;
        // constructores
        public SqlServerConnection()
        {

        }

        public SqlServerConnection(string server, string database, string user, string password)
        {
            connectionString = $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};";
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexión exitosa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }

}