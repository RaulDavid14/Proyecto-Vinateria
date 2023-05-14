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
    public partial class Historial_Apartados : Form
    {
        public int id;

        public Historial_Apartados(int idemp)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string dato = "%" + dateTimePicker1.Text + "%";
            getBuscar(dato);
        }

        private void getBuscar(string dato)
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "SELECT *,cast(fechaini as varchar),cast(fechafin as varchar) from apartados where cast(fechaini as varchar) like '" + dato + "' ;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(8), //fecha inicial
                reader.GetString(9), //fecha final
                reader.GetFloat(3), //anticipo
                reader.GetFloat(4), //total
                reader.GetChar(5), //Estado
                reader.GetInt32(6), //id del empleado
                reader.GetInt32(7)  //id del cliente
                );
            }
            cmd.Dispose();

            con.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Detalles" && e.RowIndex != -1)
            {
                Form form = new Detalles_Apartados(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                form.Show();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
