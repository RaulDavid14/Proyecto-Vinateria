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
    public partial class Detalles_Ventas : Form
    {
        int id_venta;

        public Detalles_Ventas(int id)
        {
            InitializeComponent();
            id_venta = id;
            label2.Text = id.ToString();
            Mostrar();
        }

        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "select con.idprod,pro.nomprod,con.cantidad,pro.preciomen,pro.tipo,pro.descuento from "
              + "contiene con, productos pro where con.idventa = "+id_venta+" and con.idprod = pro.idprod; ";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //Nombre
                reader.GetInt32(2), //cantidad
                reader.GetFloat(3), //Precio
                reader.GetString(4), //tipo
                reader.GetFloat(5) //descuento
                );
            }
            cmd.Dispose();

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
