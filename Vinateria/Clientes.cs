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
    public partial class Clientes : Form
    {
        public int id;

        public Clientes(int idemp)
        {
            InitializeComponent();

            id = idemp;
            getAll();
        }

        private void getAll()
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Select * from clientes;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //metodo de pago
                reader.GetString(3) //telefono
                );

            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Menu(id);
            form.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Agregar_Cliente();
            form.Show();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                string dato = "%" + textBox1.Text + "%";
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
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "Select * from clientes where nomclien like '"+dato+"';";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
          
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //metodo de pago
                reader.GetString(3) //telefono
                );

            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Visible = true;
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Seguro que desea eliminar el cliente?", "Eliminar cliente", MessageBoxButtons.YesNo);
            if (question == DialogResult.Yes)
            {
                ConexionDB conectar = new ConexionDB();
                NpgsqlConnection con = conectar.conexion();

                string sentencia = "Delete from clientes where idclien= " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ";";
                NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
                try { 
                   cmd.ExecuteScalar();
                   MessageBox.Show("Cliente eliminado");
                }
                catch {
                    MessageBox.Show("No puedes eliminar este clientes porque tiene apartados a su nombre");
                }

                
                cmd.Dispose();

                

                con.Close();

                string dato = "%" + textBox1.Text + "%";
                getBuscar(dato);

            }
            button3.Visible = false;
            button4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Form form = new Modificar_Cliente(id);
            form.Show();

            button3.Visible = false;
            button4.Visible = false;
        }
    }
}
