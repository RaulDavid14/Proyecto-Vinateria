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
    public partial class Agregar_Empleado : Form
    {
        public Agregar_Empleado()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; //Cancelar escritura del combobox
            comboBox1.Text = "Empleado"; //Texto Empleado por defecto en combobox
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();
            
            string sentencia = "INSERT INTO Empleados(nomemp, apellidos, rfc, puesto, fechaingreso, " +
                                "sueldo, horario, genero, usuario, contraseña) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + 
                                textBox3.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "',"+ 
                                textBox4.Text + ",'"+ textBox5.Text +"','"+ textBox6.Text +"','"+ textBox7.Text +"','"+ textBox8.Text +"');";

            if(textBox8.Text != textBox9.Text)
                MessageBox.Show("Las contraseñas no son iguales");
            else
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
                cmd.ExecuteReader();
                cmd.Dispose();
                MessageBox.Show("Empleado agregado");
            }

            con.Close();

            this.Close();
        }



        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
