using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinateria
{
    //CLASE QUE DIRECCIONA A LAS OPCIONES DEL MENU
    public partial class Menu : Form
    {
        public int id;


        public Menu(int idemp)
        {
            InitializeComponent();

            id = idemp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formulario = new Empleados(id);
            formulario.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Clientes(id);
            form.Show();

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Login();
            form.Show();

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new Productos(id);
            form.Show();

            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new Historial_Ventas(id);
            form.Show();

            this.Close();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new Proveedores(id);
            form.Show();

            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form form = new Historial_Apartados(id);
            form.Show();

            this.Close();
        }
    }
}
