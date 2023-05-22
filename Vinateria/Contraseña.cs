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
    public partial class Contraseña : Form
    {
        public int IDEmp;

        public Contraseña(int id)
        {
            IDEmp = id;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form log = new Ventas(IDEmp);
            log.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string contraseña = "";
            
            if(textBox2.Text == textBox3.Text) //si las contraseñas nuevas son iguales
            {
                 
 

            string sentencia = "SELECT contraseña from empleados where idemp = "+IDEmp+";";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    contraseña = reader.GetString(0);
                }

                cmd.Dispose();
                con.Close();

                if(textBox1.Text == contraseña) //si la contraseña anterior es correcta
                {
                    con.Open();
                    sentencia = "UPDATE empleados SET contraseña = '" + textBox2.Text + "' WHERE idemp = " + IDEmp + ";";
                    NpgsqlCommand cm = new NpgsqlCommand(sentencia, con);
                    cm.ExecuteScalar();
                    cm.Dispose();
                    con.Close();

                    MessageBox.Show("Contraseña cambiada.");

                    Form log = new Ventas(IDEmp);
                    log.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta.");
                }
                
              con.Close();
            }
            else
            {
                MessageBox.Show("Error, las contraseñas no son iguales");
            }


            

        }
    }
}
