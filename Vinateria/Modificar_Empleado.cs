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
    public partial class Modificar_Empleado : Form
    {
        public int idEmp;

        public Modificar_Empleado(int id)
        {
            InitializeComponent();

            idEmp = id;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; //Cancelar escritura del combobox

            Mostrar();
        }

        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia = "SELECT *,cast(fechaingreso as varchar),cast(fechasalida as varchar) FROM empleados where idemp = " + idEmp + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            string fechain;
            string fechafin;

            while (reader.Read())
            {
                if (reader.IsDBNull(12))
                    fechain = "1753-01-01";
                else
                    fechain = reader.GetString(12);

                if (reader.IsDBNull(13))
                    fechafin = "1753-01-01";
                else
                    fechafin = reader.GetString(13);

                textBox1.Text = reader.GetInt32(0).ToString(); //id
                textBox2.Text = reader.GetString(1);  //nombre
                textBox3.Text = reader.GetString(2);  //apellidos
                textBox4.Text = reader.GetString(3); //RFC
                comboBox1.Text = reader.GetString(4); //Puesto
                dateTimePicker1.Text = fechain;
                dateTimePicker2.Text = fechafin;
                textBox5.Text = reader.GetInt32(7).ToString(); // sueldo
                textBox6.Text = reader.GetString(8).ToString(); //Horario
                textBox7.Text = reader.GetChar(9).ToString();
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string fecha;
            if (dateTimePicker2.Text == "1753-01-01")
                fecha = "NULL";
            else
                fecha = "'"+dateTimePicker2.Text+"'";

            string sentencia = "UPDATE empleados set nomemp = '" + textBox2.Text + "', apellidos = '" + textBox3.Text + "'," +
                "rfc = '" + textBox4.Text + "',puesto = '"+comboBox1.Text+"',fechaingreso = '"+dateTimePicker1.Text+"',fechasalida = "+fecha+",sueldo = "+textBox5.Text+",horario = '"+textBox6.Text+"', genero = '"+textBox7.Text+"' where idemp = " + textBox1.Text + ";";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Empleado Modificado");

            con.Close();

            this.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
