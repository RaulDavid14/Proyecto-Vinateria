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
    public partial class Empleados : Form
    {
        public int id;

        public Empleados(int idemp)
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

            string sentencia1 = "SELECT *,cast(fechaingreso as varchar),cast(fechasalida as varchar) from empleados;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            string fecha;

            while (reader.Read())
            {
                if (reader.IsDBNull(13))
                    fecha = "";
                else
                    fecha = reader.GetString(13);

                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //apellidos
                reader.GetString(3), //RFC
                reader.GetString(4), //Puesto
                reader.GetString(12), //Fecha de ingreso
                fecha, //fecha salida
                reader.GetInt32(7),   //sueldo
                reader.GetString(8),  //horario
                reader.GetChar(9),     //genero
                reader.GetString(10)  //usuario

                );

            }

            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Agregar_Empleado();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Form form = new Modificar_Empleado(id);
            form.Show();

            button3.Visible = true;
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Menu(id);
            form.Show();

            this.Hide();
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

            string sentencia1 = "SELECT *,cast(fechaingreso as varchar),cast(fechasalida as varchar) from empleados where nomemp like '" + dato + "' or apellidos like '" + dato + "';";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            string fecha;

            while (reader.Read())
            {
                if (reader.IsDBNull(13))
                    fecha = "";
                else
                    fecha = reader.GetString(13);

                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetString(2), //apellidos
                reader.GetString(3), //RFC
                reader.GetString(4), //Puesto
                reader.GetString(12), //Fecha de ingreso
                fecha, //fecha salida
                reader.GetInt32(7),   //sueldo
                reader.GetString(8),  //horario
                reader.GetChar(9),     //genero
                reader.GetString(10)  //usuario

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
            DialogResult question = MessageBox.Show("Seguro que desea eliminar al empleado?", "Eliminar empleado", MessageBoxButtons.YesNo);
            if (question == DialogResult.Yes)
            {
                ConexionDB conectar = new ConexionDB();
                NpgsqlConnection con = conectar.conexion();

                string sentencia = "Delete from empleados where idemp = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ";";
                NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);

                try { 
                   cmd.ExecuteScalar();
                   MessageBox.Show("Empleado eliminado");
                }
                catch {
                    MessageBox.Show("No puedes eliminar este empleado porque tiene ventas realizadas por el");
                }

                
                cmd.Dispose();

                

                con.Close();

                string dato = "%" + textBox1.Text + "%";
                getBuscar(dato);
            }
        }

       
    }
}
