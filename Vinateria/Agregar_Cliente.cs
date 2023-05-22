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
    public partial class Agregar_Cliente : Form
    {
        public Agregar_Cliente()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; //Cancelar escritura del combobox
            comboBox1.Text = "Efectivo"; //Texto Empleado por defecto en combobox
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConexionDB conectar = new ConexionDB();
            conectar.OpenConnection();
            
            string sqlSentencia = "INSERT INTO [Clientes].[dbo].[infoClientes]" +
                "([sNombreCliente]" +
                " ,[sMetodoPago]" +
                ",[telefono])" +
                "VALUES" +
                "(" +
                "'"+ textBox1.Text + "'" +
                "'" + comboBox1.Text + "'" +
                "'" + textBox2.Text +"'" +
                ")";

            conectar.EjecutarConsulta(sqlSentencia);

            MessageBox.Show("Cliente agregado");

            conectar.CloseConnection();
            //con.Close();
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
