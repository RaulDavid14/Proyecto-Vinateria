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
    public partial class Modificar_Producto : Form
    {
        public int idProd;

        public Modificar_Producto(int id)
        {
            InitializeComponent();

            idProd = id;

            Mostrar();
        }

        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "SELECT * FROM productos where idprod = " + idProd + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                textBox1.Text = reader.GetInt32(0).ToString(); //id
                textBox2.Text = reader.GetString(1);  //nombre
                textBox3.Text = reader.GetInt32(2).ToString(); //precio mayoreo
                textBox4.Text = reader.GetInt32(3).ToString(); //precio menudeo
                textBox5.Text = reader.GetInt32(4).ToString(); //existencias
                textBox6.Text = reader.GetString(5);   //Tipo
                textBox7.Text = reader.GetInt32(6).ToString(); //descuento

            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "UPDATE productos set nomprod = '"+textBox2.Text+"', preciomay = "+textBox3.Text+"," +
                "preciomen = "+textBox4.Text +", existencia = "+textBox5.Text+", tipo = '"+textBox6.Text+"', descuento = "+textBox7.Text+" where idprod = "+textBox1.Text+";";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Producto Modificado");

            con.Close();
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
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
