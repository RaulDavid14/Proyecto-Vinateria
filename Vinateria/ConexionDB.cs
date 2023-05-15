using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace Vinateria
{
    class ConexionDB
    {
        
        private string sServer;
        private string sDatabase;
        private string sUser;
        private string sPassword;

        private string sConectar;
        private string connectionString;
        private SqlConnection sqlConnection;
        // constructores

        public ConexionDB()
        {
            this.sServer = "DESKTOP-OG2TP2Q";
            this.sDatabase = "Empleados";
            this.sUser = "sa";
            this.sPassword = "12345678";

            this.sConectar = $"Data Source=" + this.sServer + ";Initial Catalog=" + this.sDatabase + ";User ID=" + this.sUser + ";Password=" + this.sPassword + ";";
            this.sqlConnection = new SqlConnection(this.sConectar);
        }

        public ConexionDB(string server, string database, string user, string password)
        {
            this.sConectar = $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};";
            this.sqlConnection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                this.sqlConnection.Open();
                Console.WriteLine("Conexión exitosa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            this.sqlConnection.Close();
        }

        // Ejemplo de método para ejecutar una consulta SELECT
        public SqlDataReader EjecutarConsulta(string sentencia)  // meotodo para ejecutar consultas. 
        {
            SqlCommand command = new SqlCommand(sentencia, this.sqlConnection);
            return command.ExecuteReader();
        }

        public NpgsqlConnection conexion()
        {
            //Conexion a base de datos
            NpgsqlConnection con = new NpgsqlConnection(
           "Server = localhost; " +
           "User Id = postgres;" + //empleado2
           "Password = Aguilas1804; " + //1234567890
           "Database = Vinateria;");
            //Abrir conexion
            //con.Open();
            
            return con;
        }
        
    }
}
