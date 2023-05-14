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
    public partial class Agregar_Producto : Form
    {
        public Agregar_Producto()
        {
            InitializeComponent();

            getAll();
            
        }

        private void getAll()
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Select * from proveedores;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //direccion
                reader.GetString(3) //telefono
                );
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Insert into productos (nomprod,preciomay,preciomen,existencia,tipo,descuento,idprov) values ('"+textBox1.Text+"',"+textBox2.Text+","+textBox3.Text+","+textBox4.Text+",'"+textBox5.Text+"',"+textBox6.Text+","+textBox7.Text+");";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            
            try
            {
              cmd.ExecuteNonQuery();
              MessageBox.Show("Producto agregado");
            }
            catch
            {
                MessageBox.Show("Error al agregar producto");
            }

            con.Close();
            this.Close();

        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();


            if(textBox8.TextLength > 0)
            {
             dataGridView1.Rows.Clear();

           
            string sentencia1 = "Select * from proveedores where nomprov like '%"+ textBox8.Text +"%';";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

              while (reader.Read())
              {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //direccion
                reader.GetString(3) //telefono
                );
              }

            }
            else
            {
                getAll();
            }
            
            con.Close();

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
