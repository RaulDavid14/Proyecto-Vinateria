using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace Vinateria
{
    public partial class Agregar_Empleado : Form
    {
        public Agregar_Empleado()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            conectar.OpenConnection();

            //NpgsqlConnection con = conectar.conexion();
            
           
            string sentencia = "INSERT INTO [Empleados].[dbo].[infoEmpleado]" +
                                "( [sNombre] " +
                                ",[sApellidoPaterno] " +
                                ",[sApellidoMaterno] " +
                                ",[sRFC] " +
                                ",[sUsuario] " +
                                ",[sPassword] " +
                                ",[dFechaIngreso] " +
                                ",[catTipoEmpleado] ) VALUES" +
                                "( '" + tbNombres.Text + "'" +
                                ", '" + tbPaterno.Text + "'" +
                                ", '" + tbMaterno.Text + "'" +
                                ", '" + tbRFC.Text + "'" +
                                ", '" + tbUsuario.Text + 
                                ", '" + tbRFC + "'" +
                                ", GETDATE()" +
                                "1" +
                                ")";


            SqlDataReader reader = conectar.EjecutarConsulta(sentencia);
            
                MessageBox.Show("Empleado agregado con éxito.");

           // con.Close();
            conectar.CloseConnection();

            this.Close();
        }



        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
