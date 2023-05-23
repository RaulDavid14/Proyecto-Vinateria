using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Npgsql;

namespace Vinateria
{
    public partial class Empleados : Form
    {
        public int id;

        public Empleados(int idemp)
        {
            InitializeComponent();

            id = idemp;
            getAll();
        }

        private void getAll()
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            conectar.OpenConnection();

            string sqlSentencia = "SELECT * FROM [Empleados].[dbo].[infoEmpleado]  WHERE sUsuario = '" + tbBuscar.Text + "' AND sPassword = ''";
          
            SqlDataReader reader = conectar.EjecutarConsulta(sqlSentencia);
           

            string fecha;

            while (reader.Read())
            {
                if (reader.IsDBNull(8))
                    fecha = "";
                else
                    fecha = reader.GetString(8);
                    

                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombres
                reader.GetString(2), //paterno
                reader.GetString(3), //materno
                reader.GetString(4), //RFC
                reader.GetString(5), // USUARIO
                reader.GetString(7), //FECHA INGRESO
                reader.GetInt32(8), //Puesto
                reader.GetChar(9)     //genero
                );

            }
            conectar.CloseConnection();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Agregar_Empleado();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Form form = new Modificar_Empleado(id);
            form.Show();

            button3.Visible = true;
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Menu(id);
            form.Show();

            this.Hide();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbBuscar.TextLength > 0)
            {
                string dato = "%" + tbBuscar.Text + "%";
                getBuscar(dato);
            }
            else
            {
                getAll();
            }
        }

        private void getBuscar(string dato)
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            conectar.OpenConnection();

            string sentencia1 = "SELECT *,cast(fechaingreso as varchar),cast(fechasalida as varchar) from empleados where nomemp like '" + dato + "' or apellidos like '" + dato + "';";
            
            string sConsulta = "SELECT  *" +
                " ,CONVERT (NVARCHAR(10), [dFechaIngreso])" +
                " FROM [Empleados].[dbo].[infoEmpleado]" +
                " WHERE 1=1" +
                "AND [sNombre] LIKE '" + dato + "'" +
                "OR [sApellidoPaterno] LIKE '" + dato +"'" +
                "OR [sApellidoMaterno] LIKE '" + dato + "'";

            SqlDataReader reader = conectar.EjecutarConsulta(sConsulta);

            
            while (reader.Read())
            {
                
                    

                dataGridView1.Rows.Add(
                reader.GetInt32(0) //id
                ,reader.GetString(1)
                ,reader.GetString(2)
                ,reader.GetString(3)
                ,reader.GetString(4)
                ,reader.GetDateTime(7)
                ,reader.GetString(9)
                ,reader.GetString(6)
                );

            }
            conectar.CloseConnection();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Visible = true;
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Seguro que desea eliminar al empleado?", "Eliminar empleado", MessageBoxButtons.YesNo);
            if (question == DialogResult.Yes)
            {
                ConexionDB conectar = new ConexionDB();
                NpgsqlConnection con = conectar.conexion();

                string sentencia = "Delete from empleados where idemp = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ";";
                NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);

                try { 
                   cmd.ExecuteScalar();
                   MessageBox.Show("Empleado eliminado");
                }
                catch {
                    MessageBox.Show("No puedes eliminar este empleado porque tiene ventas realizadas por el");
                }

                
                cmd.Dispose();


                conectar.CloseConnection();
                

                string dato = "%" + tbBuscar.Text + "%";
                getBuscar(dato);
            }
        }

       
    }
}
