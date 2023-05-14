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
    public partial class Agregar_Proveedor : Form
    {
        public Agregar_Proveedor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Insert into proveedores (nomprov,direccion,telefono) values ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"');";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Proveedor agregado");

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
