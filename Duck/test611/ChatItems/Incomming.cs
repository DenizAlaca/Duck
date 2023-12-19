using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace linkapp.ChatItems
{
    public partial class Incomming : UserControl
    {
        public Incomming()
        {
            InitializeComponent();
        }

        public string Message {
            get
         {
             return textBox1.Text;
         }

        set
         {
                textBox1.Text = value;
                AdJustHeight();
         }
        }
        void AdJustHeight()
        {

            textBox1.Height = Utils.GetTextHeight(textBox1) + 10;
            guna2GradientPanel2.Height = textBox1.Top + textBox1.Height;
            this.Height = guna2GradientPanel2.Bottom + 10;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
    
}
