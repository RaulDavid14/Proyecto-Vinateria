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
    public partial class Detalles_Apartados : Form
    {
        int id_apartado;

        public Detalles_Apartados(int id)
        {
            id_apartado = id;
            InitializeComponent();
            label2.Text = id.ToString();
            Mostrar();
        }
        private void Mostrar()
        {
            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "select con.idprod,pro.nomprod,con.cantidad,pro.preciomen,pro.tipo,pro.descuento from "
              + "aparece con, productos pro where con.idapart = " + id_apartado + " and con.idprod = pro.idprod; ";

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
