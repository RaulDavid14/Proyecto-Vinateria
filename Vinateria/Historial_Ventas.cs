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
    public partial class Historial_Ventas : Form
    {

        public int id;

        public Historial_Ventas(int idemp)
        {
            InitializeComponent();

            id = idemp;

            string dato = "%" + dateTimePicker1.Text + "%";
            getBuscar(dato);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Menu(id);
            form.Show();

            this.Close();
        }


        private void getBuscar(string dato)
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();
            
            string sentencia1 = "SELECT *,cast(fecha as varchar) from ventas where cast(fecha as varchar) like '" + dato + "' ;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetFloat(1), //total
                reader.GetString(6), //fecha
                reader.GetFloat(3), //descuento
                reader.GetFloat(4), //precio final
                reader.GetInt32(5) //id del empleado
                );
            }
            cmd.Dispose();

            con.Close();
            
        }


        private void button2_Click(object sender, EventArgs e)
        {

                string dato = "%" + dateTimePicker1.Text + "%";
                getBuscar(dato);
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Detalles" && e.RowIndex != -1)
            {
                Form form = new Detalles_Ventas(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                form.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Ventas(id);
            form.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Seguro que desea eliminar la venta? ", "Eliminar venta", MessageBoxButtons.YesNo);
            if (question == DialogResult.Yes)
            {
                ConexionDB conectar = new ConexionDB();
                NpgsqlConnection con = conectar.conexion();

                string sentencia = "Delete from contiene where idventa = "+ dataGridView1.CurrentRow.Cells[0].Value.ToString() +";";
                NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
                cmd.ExecuteScalar();
                cmd.Dispose();

                sentencia = "Delete from ventas where idventa = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " ;";
                NpgsqlCommand cmd2 = new NpgsqlCommand(sentencia, con);
                cmd2.ExecuteScalar();
                cmd2.Dispose();

                MessageBox.Show("Venta eliminada");

                string dato = "%" + dateTimePicker1.Text + "%";
                getBuscar(dato);
            }
        }
    }
}
