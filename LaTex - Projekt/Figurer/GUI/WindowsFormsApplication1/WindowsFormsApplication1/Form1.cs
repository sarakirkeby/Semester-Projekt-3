using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            label8.Text = DateTime.Now.ToString();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //Pen silver = new Pen(Color.Silver);
        //System.Drawing.SolidBrush fillSilver = new System.Drawing.SolidBrush(Color.Silver);

        //Rectangle raktangel = new Rectangle(230, 520, 88, 75);
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //g.DrawRectangle(silver, raktangel);
            //g.FillRectangle(fillSilver, raktangel);
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            label8.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }
    }
}
