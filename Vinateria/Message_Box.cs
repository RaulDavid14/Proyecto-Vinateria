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
    public partial class Message_Box : Form
    {
        public Message_Box(string datos)
        {
            InitializeComponent();

            label1.Text = datos;
        }
    }
}
