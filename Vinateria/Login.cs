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
   
    public partial class Login : Form
    {

        bool vai = false;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ConexionDB conectar = new ConexionDB();
            conectar.OpenConnection();

            string sqlSentencia = "SELECT * FROM [Empleados].[dbo].[infoEmpleado]  WHERE sUsuario = '" + textBox1.Text + "' AND sPassword = '" + textBox2.Text + "'";
            SqlDataReader reader = conectar.EjecutarConsulta(sqlSentencia);


            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                int puesto = reader.GetInt32(8);
                string sUsuario = reader.GetString(6);
            
                if(puesto == 1)
                {
                 Form formulario = new Ventas(id);
                 formulario.Show();
                }
                else
                {
                    Form formulario2 = new Menu(id);
                    formulario2.Show();
                }
                
                conectar.CloseConnection();
                this.Hide();
            }
            else
            {
               
                MessageBox.Show("Error, el usuario o contraseña incorrectos");
            }
            conectar.CloseConnection();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            vai = true;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if(vai == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            vai = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    } 
     
}
