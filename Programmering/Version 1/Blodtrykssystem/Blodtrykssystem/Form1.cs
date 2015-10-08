using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NS_Logik;

namespace Blodtrykssystem
{
    public partial class Form1 : Form
    {
        Logik logiklag;
        int counter;
            double nuværendeVærdi;
            double baseline;
            int baselineoverskredet = 0;
            double maxTrykOverBaseline;
            double ubaseline;
            int ubaselineoverskredet = 0;
            int antalsys;
            int antaldia;
            double umaxTrykOverBaseline;
            List<double> blodtryksværdier;
            int status;
            List<double> indkommendeSignal;
            int xværdi;
            int grafcount;
        int graf2;

        public Form1()
        {
            InitializeComponent();
            logiklag = new Logik();
            blodtryksværdier = new List<double>();
            counter = 0;
            antalsys = 0;
            antaldia = 0;
            baseline = 100;
            ubaseline = 100;
            xværdi = 100;
            timer1.Start();
            maxTrykOverBaseline = baseline;
            umaxTrykOverBaseline = ubaseline;
            status = 1;

            grafcount = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nuværendeVærdi = logiklag.getTal(grafcount) / 100;

            blodtryksværdier.Add(nuværendeVærdi);



            //chart1.ChartAreas[0].AxisX.Minimum = counter - 60;
            //chart1.ChartAreas[0].AxisX.Maximum = counter + 20;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = xværdi;

            chart1.ChartAreas[0].AxisY.Minimum = 7000 / 100;
            chart1.ChartAreas[0].AxisY.Maximum = 13000 / 100;
            chart1.ChartAreas[0].AxisY.Interval = 20;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#,##}";
            chart1.ChartAreas[0].AxisX.Interval = 5;
            chart1.Series["Series1"].Color = Color.Blue;
            chart1.Series["Series2"].Color = Color.Red;

            if (grafcount % (xværdi + 1) == 0)
            {
                counter = 0;
            }

            if (grafcount > xværdi)
            {

                chart1.Series["Series1"].Points.ElementAt(counter).SetValueY(nuværendeVærdi);
                if (counter < xværdi - 1)
                {
                    //chart1.Series["Series1"].Points.ElementAt(counter + 1).SetValueY(0);
                    //chart1.Series["Series1"].Points.ElementAt(counter + 2).SetValueY(0);
                    //chart1.Series["Series1"].Points.ElementAt(counter + 3).SetValueY(0);

                    chart1.Series["Series1"].Points.ElementAt(counter + 1).Color = Color.Transparent;
                    chart1.Series["Series1"].Points.ElementAt(counter + 2).Color = Color.Transparent;
                    chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;
                }
                if (counter == xværdi - 1)
                {
                    chart1.Series["Series1"].Points.ElementAt(counter + 1).Color = Color.Transparent;
                    chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;


                }
                if (counter == xværdi)
                {
                    chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;
                }
                chart1.Series["Series2"].Points.Clear();
                chart1.Series["Series2"].Points.AddXY(counter, nuværendeVærdi);


            }
            else
            {

                chart1.Series["Series1"].Points.AddXY(counter, nuværendeVærdi);
                chart1.Series["Series2"].Points.Clear();
                chart1.Series["Series2"].Points.AddXY(counter, nuværendeVærdi);
            }




            //tjek systole
            if (grafcount >= 0)
            {
                if (nuværendeVærdi > maxTrykOverBaseline)
                {
                    maxTrykOverBaseline = nuværendeVærdi;
                    baselineoverskredet++;
                }
                else
                {
                    if (nuværendeVærdi < maxTrykOverBaseline && baselineoverskredet > 1)
                    {
                        label1.Text = Convert.ToString(blodtryksværdier[grafcount - 1]);
                        antalsys++;
                        baselineoverskredet = 0;
                        maxTrykOverBaseline = baseline;
                    }
                }


                //tjek diastole

                if (nuværendeVærdi < umaxTrykOverBaseline)
                {
                    umaxTrykOverBaseline = nuværendeVærdi;
                    ubaselineoverskredet++;
                }
                else

                {
                    if (nuværendeVærdi > umaxTrykOverBaseline && ubaselineoverskredet > 1)
                    {
                        label2.Text = Convert.ToString(blodtryksværdier[grafcount - 1]);
                        antaldia++;
                        ubaselineoverskredet = 0;
                        umaxTrykOverBaseline = ubaseline;
                    }




                }
            }

            counter++;
            grafcount++;
            label4.Text = DateTime.Now.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 60;
            trackBar1.Maximum = 100;
            label9.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar2.Minimum = 60;
            trackBar2.Maximum = 100;
            label10.Text = trackBar2.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            trackBar4.Minimum = 60;
            trackBar4.Maximum = 100;
            label7.Text = trackBar4.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar3.Minimum = 60;
            trackBar3.Maximum = 100;
            label8.Text = trackBar3.Value.ToString();
        }

        private void startknap_Click(object sender, EventArgs e)
        {
            if (status == 1)
            {
                timer1.Stop();
                status = 0;
            }
            else
            {
                timer1.Start();
                status = 1;
            }
        }
    }
}
