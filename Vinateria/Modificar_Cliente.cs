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
    public partial class Modificar_Cliente : Form
    {
        public int idclien;

        public Modificar_Cliente(int id)
        {
            InitializeComponent();

            idclien = id;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; //Cancelar escritura del combobox
            comboBox1.Text = "Efectivo";

            Mostrar();
        }

        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "SELECT * FROM clientes where idclien = " + idclien + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                textBox1.Text = reader.GetInt32(0).ToString(); //id
                textBox2.Text = reader.GetString(1);  //nombre
                comboBox1.Text = reader.GetString(2); //metodo de pago
                textBox3.Text = reader.GetString(3); //telefono

            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "UPDATE clientes set nomclien = '" + textBox2.Text + "', metpago = '" + comboBox1.Text + "'," +
                "telefono = '" + textBox3.Text + "' where idclien = " + textBox1.Text + ";";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Cliente Modificado");

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
