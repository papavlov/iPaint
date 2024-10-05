using iPaint.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPaint
{
    public partial class FormShapeProperties : Form
    {
        private IShape shape;

        public FormShapeProperties(IShape shape)
        {
            InitializeComponent();

            this.shape = shape;

            this.Text = shape.GetType().Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.ShowDialog();
            buttonColor.BackColor = cd.Color;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormShapeProperties_Load(object sender, EventArgs e)
        {
            textBox1.Text = shape.Area.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.shape.Fill = buttonColor.BackColor;

            Close();
        }
    }
}
