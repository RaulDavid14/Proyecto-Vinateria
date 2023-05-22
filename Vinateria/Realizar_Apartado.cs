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
    public partial class Realizar_Apartado : Form
    {
        public int IDEmp;
        public List<Producto> listprod = new List<Producto>();
        public int total;

        public Realizar_Apartado(List<Producto> lista,int id,int t)
        {
            listprod = lista.ToList();
            IDEmp = id;
            total = t;
            double anticipo = total * 0.2;
            
            InitializeComponent();


            textBox2.Text = Convert.ToString(anticipo);
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                string dato = "%" + textBox1.Text + "%";
                getBuscar(dato);
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
        }

        private void getBuscar(string dato)
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Select * from clientes where nomclien like '" + dato + "';";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1) //nombre
                );

            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string fecha = DateTime.Now.ToString("yyyy-M-dd HH:mm:ss");
            string sentencia = "INSERT INTO apartados(fechaini, fechafin, anticipo, total, estado, idemp, idclien) values ('" + fecha + "','" + dateTimePicker1.Text + "',"+textBox2.Text+"," + total + ",'i',"+IDEmp+","+textBox1.Text+");";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);

            try
            {
                cmd.ExecuteScalar();
                cmd.Dispose();

            sentencia = "SELECT idapart from apartados where fechaini = '" + fecha + "';";
            NpgsqlCommand cmd2 = new NpgsqlCommand(sentencia, con);
            int idapart = Convert.ToInt32(cmd2.ExecuteScalar());
            cmd2.Dispose();

            sentencia = "INSERT INTO aparece values ";

            for (int i = 0; i < listprod.Count; i++)
            {

                sentencia += "(" + idapart + "," + listprod[i].id + "," + listprod[i].cantidad + ")";
                if (i == listprod.Count - 1)
                    sentencia += ";";
                else
                    sentencia += ", ";
            }
            NpgsqlCommand cmd3 = new NpgsqlCommand(sentencia, con);
            cmd3.ExecuteScalar();
            cmd3.Dispose();
            con.Close();

            MessageBox.Show("Apartado realizado");
            }
            catch
            {
                MessageBox.Show("Error al realizar apartado");
            }
            
            

            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
