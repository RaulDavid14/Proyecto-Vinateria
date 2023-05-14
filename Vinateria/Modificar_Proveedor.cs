using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Vinateria
{
    public partial class Modificar_Proveedor : Form
    {
        public int idprov;

        public Modificar_Proveedor(int id)
        {
            InitializeComponent();

            idprov = id;

            Mostrar();
        }

        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "SELECT * FROM proveedores where idprov = " + idprov + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                textBox1.Text = reader.GetInt32(0).ToString(); //id
                textBox2.Text = reader.GetString(1);  //nombre
                textBox3.Text = reader.GetString(2); //direccion
                textBox4.Text = reader.GetString(3); //telefono

            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "UPDATE proveedores set nomprov = '" + textBox2.Text + "', direccion = '" + textBox3.Text + "'," +
                "telefono = '" + textBox4.Text + "' where idprov = " + textBox1.Text + ";";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Proveedor Modificado");

            con.Close();

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
    }
}
