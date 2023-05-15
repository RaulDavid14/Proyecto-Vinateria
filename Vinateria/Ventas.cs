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
    
    public partial class Ventas : Form
    {
        public int IDEmp;
        public List<Producto> listprod = new List<Producto>();
        public Producto prodtemp;
        public int total;
        public int existencia; //utilizado para que la venta no exceda a las existencias del inventario
        public int totalsindescuento;
        public int descuentototal;



        public Ventas(int id)
        {
            InitializeComponent();
            asignartexto(id);
            IDEmp = id;
            total = 0;

            getAll();
        }

        private void getAll()
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "SELECT * from productos order by idprod;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetFloat(2), //precio mayoreo
                reader.GetFloat(3), //precio menudeo
                reader.GetInt32(4), //existencias
                reader.GetString(5), //tipo
                reader.GetFloat(6) //descuento
                );
            }

            con.Close();
        }

        //Asignar texto en label 2 y muestre ID
        public void asignartexto(int id)
        {
            label2.Text = id.ToString();
        }

        //Cerrar Sesion
        private void button1_Click(object sender, EventArgs e)
        {
            Form log = new Login();
            log.Show();
            this.Close();
        }

        private void getBuscar(string dato)
        {
            dataGridView1.Rows.Clear();

            ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string sentencia1 = "SELECT * from productos where nomprod like '" + dato + "' or tipo like '"+dato+"' order by idprod;";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia1, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()){
                dataGridView1.Rows.Add(
                reader.GetInt32(0), //id
                reader.GetString(1), //nombre
                reader.GetFloat(2), //precio mayoreo
                reader.GetFloat(3), //precio menudeo
                reader.GetInt32(4), //existencias
                reader.GetString(5), //tipo
                reader.GetFloat(6) //descuento
                );
            }

            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodtemp.id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            prodtemp.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            prodtemp.precio = dataGridView1.Rows[e.RowIndex].Cells["Precio_menudeo"].Value.ToString();
            prodtemp.descuento = dataGridView1.Rows[e.RowIndex].Cells["Descuento"].Value.ToString();
            existencia = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["Existencias"].Value.ToString());
            label5.Visible = true;
            textBox2.Visible = true;
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                e.Handled = true;
            else
            {

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            prodtemp.cantidad = textBox2.Text;
            prodtemp.descuento = (Int32.Parse(prodtemp.descuento)).ToString(); 

            for(int i = 0; i < listprod.Count; i++)
            {
                if(listprod[i].id == prodtemp.id)
                {
                    MessageBox.Show("Este producto ya ha sido agregado a la venta");
                    textBox2.Visible = false;
                    textBox2.Clear();
                    label5.Visible = false;
                    button2.Visible = false;
                    return;
                }
            }

            listprod.Add(prodtemp);
            

            
                dataGridView2.Rows.Add(
                listprod[listprod.Count - 1].id,
                listprod[listprod.Count - 1].nombre,
                listprod[listprod.Count - 1].cantidad,
                listprod[listprod.Count - 1].precio,
                listprod[listprod.Count - 1].descuento
                );

                total += ((Int32.Parse(listprod[listprod.Count - 1].precio) * Int32.Parse(listprod[listprod.Count - 1].cantidad)) - (Int32.Parse(listprod[listprod.Count - 1].descuento) * Int32.Parse(listprod[listprod.Count - 1].cantidad)));
            totalsindescuento += (Int32.Parse(listprod[listprod.Count - 1].precio) * Int32.Parse(listprod[listprod.Count - 1].cantidad));
            descuentototal += Int32.Parse(listprod[listprod.Count - 1].descuento) * Int32.Parse(listprod[listprod.Count - 1].cantidad);

            label5.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();

            label7.Text = total.ToString(); //mostrar la suma en label 7


            getAll();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView2.RowCount <= 0)
            {
                MessageBox.Show("No hay datos agregados en la venta");
            }
          else
          {
             ConexionDB conectar = new ConexionDB();
            NpgsqlConnection con = conectar.conexion();

            string fecha = DateTime.Now.ToString("yyyy-M-dd HH:mm:ss");         
            string sentencia = "INSERT INTO Ventas(total, fecha, descuento, preciofin, idemp) values ("+totalsindescuento+ ",'"+fecha+"',"+descuentototal+"," + total+","+label2.Text+")";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, con);
            cmd.ExecuteScalar();
            cmd.Dispose();

            sentencia = "SELECT idventa from ventas where fecha = '" + fecha + "';";
            NpgsqlCommand cmd2 = new NpgsqlCommand(sentencia, con);
            int idventa = Convert.ToInt32(cmd2.ExecuteScalar());
            cmd2.Dispose();

            sentencia = "INSERT INTO contiene values ";
            

            for(int i = 0;i < listprod.Count; i++){
                
                sentencia += "(" + idventa + "," + listprod[i].id + "," + listprod[i].cantidad + ")";
                if (i == listprod.Count - 1)
                    sentencia += ";";
                else
                    sentencia += ", ";

                //sentencia2 = "Update productos set existencia = existencia - " + listprod[i].cantidad + " where idprod = " + listprod[i].id + ";";
               // NpgsqlCommand cmd4 = new NpgsqlCommand(sentencia2, con);
                //cmd4.ExecuteScalar();
                //cmd4.Dispose();
            }
            NpgsqlCommand cmd3 = new NpgsqlCommand(sentencia, con);
            cmd3.ExecuteNonQuery();
            cmd3.Dispose();
            con.Close();

            MessageBox.Show("Venta realizada");

            dataGridView2.Rows.Clear();
            listprod.Clear();
            total = 0;
            totalsindescuento = 0;
            descuentototal = 0;
            label7.Text = "0";
           }
           
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

            if (textBox2.TextLength > 0)
            {
                  int cantidad = int.Parse(textBox2.Text);
                    if (existencia >= cantidad)
                        button2.Visible = true;
                    else
                    {
                        MessageBox.Show("No puedes excederte a la cantidad en el stock");
                        textBox2.Clear();
                        button2.Visible = false;
                    }
            }
            else
            {
                button2.Visible = false;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           DialogResult question =  MessageBox.Show("Seguro que desea cancelar la venta?", "Cancelar venta", MessageBoxButtons.YesNo);
            if(question == DialogResult.Yes){
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            button2.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox2.Visible = false;
            label5.Visible = false;
            total = 0;
            totalsindescuento = 0;
            descuentototal = 0;
            listprod.Clear();
            label7.Text = "0";
            }

            getAll();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            
            for(int i = 0; i < listprod.Count; i++)
            {
                if(listprod[i].id == dataGridView2.CurrentRow.Cells[0].Value.ToString()) 
                {
                    total = total - (Int32.Parse(listprod[i].cantidad) * Int32.Parse(listprod[i].precio));
                listprod.RemoveAt(i);
                i = listprod.Count;
                }
                
            }
                dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
            label7.Text = total.ToString();

            button5.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount <= 0)
            {
                MessageBox.Show("No hay datos agregados en la venta");
            }
            else
            {

                Form form = new Realizar_Apartado(listprod, IDEmp, total);
                form.Show();

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                button2.Visible = false;
                textBox1.Clear();
                textBox2.Clear();
                textBox2.Visible = false;
                label5.Visible = false;
                total = 0;
                totalsindescuento = 0;
                descuentototal = 0;
                listprod.Clear();
                label7.Text = "0";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form log = new Contraseña(IDEmp);
            log.Show();
            this.Close();
        }
    }

    public struct Producto
    {
        public string id { get; set; }

        public string nombre { get; set; }

        public string cantidad { get; set; }

        public string precio { get; set; }

        public string descuento { get; set; }
    }
}
