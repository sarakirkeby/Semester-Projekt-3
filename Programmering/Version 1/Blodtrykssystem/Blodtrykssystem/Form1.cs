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
using System.Threading;
using DTO;

namespace Blodtrykssystem
{
    public partial class Form1 : Form, IObserver
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

        Thread t1;

        int graf1;
        DTO_Blodtryksmåling målinger;
        Blodtryksberegning beregner;

        public Form1()
        {
            InitializeComponent();
            
            logiklag = new Logik();
            logiklag.Subscribe(this);
            
            blodtryksværdier = new List<double>();
            counter = 0;
            antalsys = 0;
            antaldia = 0;
            baseline = 100;
            ubaseline = 100;
            maxTrykOverBaseline = baseline;
            umaxTrykOverBaseline = ubaseline;
            status = 1;
            //initGraf();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 500;
            chart1.ChartAreas[0].AxisY.Minimum = 100;
            //Thread t1 = new Thread(opdaterGraf);
            logiklag.startMåling();
            //t1.Start();
            
        }

       
        public void Opdater()
        {
            opdaterGraf();
        }
        public bool IsRunning()
        {
            return logiklag.IsRunning();
        }
        private void initGraf()
        {
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = -10;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            //chart1.ChartAreas[0].AxisY.Interval = 20;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#,##}";
            chart1.ChartAreas[0].AxisX.Interval = 50;
            chart1.Series["Series1"].Color = Color.Blue;
            
            
        }

        private delegate void UpdateUICallback();

        private void opdaterGraf()
        {


            List<double> liste;
            liste = logiklag.grafdata;


            if (liste.Count > 0)
            {
                if (label1.InvokeRequired)
                {
                    UpdateUICallback d = new UpdateUICallback(opdaterGraf);
                    this.Invoke(d);
                }
                else
                {
                    
                    label14.Text = logiklag.getAntalMålinger().ToString();
                    //chart1.Series[0].Points.AddXY(counter, liste[counter]);
                    chart1.Series[0].Points.DataBindY(liste);
                    chart1.Series[1].Points.DataBindY(liste);
                    chart1.Series[1].Color = Color.Transparent;
                    chart1.Series[0].Color = Color.Blue;


                    if (counter > 0)
                    {
                        chart1.Series[1].Points[counter-1].Color = Color.Red;
                        chart1.Series[0].Points[counter].Color = Color.Transparent;

                    }
                    if (counter < logiklag.getAntalPåXAkse()-1)
                    {
                        chart1.Series[0].Points[counter + 1].Color = Color.Transparent;

                    }

                    counter++;
                    if (counter == logiklag.getAntalPåXAkse())
                    {
                        counter = 0;
                        beregner = new Blodtryksberegning();
                        label2.Text = "HR: " + beregner.calcPuls(liste, 0.01).ToString();
                        double systole = beregner.calsSystole(liste);
                        double diastole = beregner.calsDiastole(liste);
                        label1.Text = Convert.ToString(Math.Round(systole,0) + "/" + Convert.ToString(Math.Round(diastole)));
                    }
                }
                
            }











            //if (grafcount < antalmålinger)
            //{
            //    chart1.DataSource = logiklag.getMålinger();
            //    nuværendeVærdi = målinger.getMålinger1()[counter];
            //    chart1.Series[0].Points.AddXY(counter, nuværendeVærdi);
            //    counter++;
        }

            
  //  }

        //private void opdaterGraf(object obj)
        //{
            

        //    var chart = (System.Windows.Forms.DataVisualization.Charting.Chart)obj;

        //    while (IsRunning())
        //    {
        //        int antalmålinger = logiklag.getAntalMålinger();
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisX.Minimum = 0));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisX.Maximum = xværdi));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisY.Minimum = -1));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisY.Maximum = 1));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisY.Interval = 20));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisX.LabelStyle.Format = "{#,##}"));
        //        chart.Invoke(new Action(() => chart.ChartAreas[0].AxisX.Interval = 5));
        //        chart.Invoke(new Action(() => chart.Series["Series1"].Color = Color.Blue));
        //        chart.Invoke(new Action(() => chart.Series["Series2"].Color = Color.Red));


                

        //        if (grafcount < antalmålinger)
        //        {
                    
        //            målinger = logiklag.getMålinger();
        //            nuværendeVærdi = målinger.getMålinger1()[grafcount];
        //            label14.Invoke(new Action(() => label14.Text = antalmålinger.ToString()));
        //            //chart.Invoke(new Action(() => chart.Series[0].Points.AddXY(antalIgraf, nuværendeVærdi)));
        //            //antalIgraf++;

        //            //if (tal >= 1000)
        //            //{
        //            //    tal = 0;
        //            //    chart.Invoke(new Action(() => chart.Series[0].Points.Clear()));
        //            //}

        //            if (grafcount % (xværdi + 1) == 0)
        //            {
        //                counter = 0;
        //            }

        //            if (grafcount > xværdi)
        //            {

        //                chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter].SetValueY(nuværendeVærdi)));

        //                if (counter < xværdi - 1)
        //                {
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter + 1].SetValueY(0)));
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter + 2].SetValueY(0)));
        //                    //chart.Invoke(new Action(() => chart.Series["Series1"].Points.ElementAt(counter + 3).SetValueY(0)));

        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter + 1].Color = Color.Transparent));
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter + 2].Color = Color.Transparent));
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter].Color = Color.Blue));
        //                }
        //                if (counter == xværdi - 1)
        //                {
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter + 1].Color = Color.Transparent));
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter].Color = Color.Blue));


        //                }
        //                if (counter == xværdi)
        //                {
        //                    chart.Invoke(new Action(() => chart.Series["Series1"].Points[counter].Color = Color.Blue));
        //                }

        //                chart.Invoke(new Action(() => chart.Series["Series2"].Points.Clear()));
        //                chart.Invoke(new Action(() => chart.Series["Series2"].Points.AddXY(counter, nuværendeVærdi)));


        //            }
        //            else
        //            {

        //                chart.Invoke(new Action(() => chart.Series["Series1"].Points.AddXY(counter, nuværendeVærdi)));
        //                chart.Invoke(new Action(() => chart.Series["Series2"].Points.Clear()));
        //                chart.Invoke(new Action(() => chart.Series["Series2"].Points.AddXY(counter, nuværendeVærdi)));
        //            }

        //            counter++;
        //            grafcount++;
        //        }

        //    }

        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    DTO_Blodtryksmåling måling = logiklag.getMålinger();

        //    nuværendeVærdi = måling.getMålinger1()[1] / 100;

        //    blodtryksværdier.Add(nuværendeVærdi);

        //    listBox1.DataSource = null;
        //    listBox1.DataSource = måling.getMålinger1();
        //    label14.Text = logiklag.getAntalMålinger().ToString();

        //    chart1.ChartAreas[0].AxisX.Minimum = counter - 60;
        //    chart1.ChartAreas[0].AxisX.Maximum = counter + 20;

        //    chart1.ChartAreas[0].AxisX.Minimum = 0;
        //    chart1.ChartAreas[0].AxisX.Maximum = xværdi;

        //    chart1.ChartAreas[0].AxisY.Minimum = 7000 / 100;
        //    chart1.ChartAreas[0].AxisY.Maximum = 13000 / 100;
        //    chart1.ChartAreas[0].AxisY.Interval = 20;
        //    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#,##}";
        //    chart1.ChartAreas[0].AxisX.Interval = 5;
        //    chart1.Series["Series1"].Color = Color.Blue;
        //    chart1.Series["Series2"].Color = Color.Red;

        //    if (grafcount % (xværdi + 1) == 0)
        //    {
        //        counter = 0;
        //    }

        //    if (grafcount > xværdi)
        //    {

        //        chart1.Series["Series1"].Points.ElementAt(counter).SetValueY(nuværendeVærdi);
        //        if (counter < xværdi - 1)
        //        {
        //            chart1.Series["Series1"].Points.ElementAt(counter + 1).SetValueY(0);
        //            chart1.Series["Series1"].Points.ElementAt(counter + 2).SetValueY(0);
        //            chart1.Series["Series1"].Points.ElementAt(counter + 3).SetValueY(0);

        //            chart1.Series["Series1"].Points.ElementAt(counter + 1).Color = Color.Transparent;
        //            chart1.Series["Series1"].Points.ElementAt(counter + 2).Color = Color.Transparent;
        //            chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;
        //        }
        //        if (counter == xværdi - 1)
        //        {
        //            chart1.Series["Series1"].Points.ElementAt(counter + 1).Color = Color.Transparent;
        //            chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;


        //        }
        //        if (counter == xværdi)
        //        {
        //            chart1.Series["Series1"].Points.ElementAt(counter).Color = Color.Blue;
        //        }
        //        chart1.Series["Series2"].Points.Clear();
        //        chart1.Series["Series2"].Points.AddXY(counter, nuværendeVærdi);


        //    }
        //    else
        //    {

        //        chart1.Series["Series1"].Points.AddXY(counter, nuværendeVærdi);
        //        chart1.Series["Series2"].Points.Clear();
        //        chart1.Series["Series2"].Points.AddXY(counter, nuværendeVærdi);
        //    }




        //    //tjek systole
        //    if (grafcount >= 0)
        //    {
        //        if (nuværendeVærdi > maxTrykOverBaseline)
        //        {
        //            maxTrykOverBaseline = nuværendeVærdi;
        //            baselineoverskredet++;
        //        }
        //        else
        //        {
        //            if (nuværendeVærdi < maxTrykOverBaseline && baselineoverskredet > 1)
        //            {
        //                label1.Text = Convert.ToString(blodtryksværdier[grafcount - 1]);
        //                antalsys++;
        //                baselineoverskredet = 0;
        //                maxTrykOverBaseline = baseline;
        //            }
        //        }


        //        //tjek diastole

        //        if (nuværendeVærdi < umaxTrykOverBaseline)
        //        {
        //            umaxTrykOverBaseline = nuværendeVærdi;
        //            ubaselineoverskredet++;
        //        }
        //        else

        //        {
        //            if (nuværendeVærdi > umaxTrykOverBaseline && ubaselineoverskredet > 1)
        //            {
        //                label2.Text = Convert.ToString(blodtryksværdier[grafcount - 1]);
        //                antaldia++;
        //                ubaselineoverskredet = 0;
        //                umaxTrykOverBaseline = ubaseline;
        //            }




        //        }
        //    }

        //    counter++;
        //    grafcount++;
        //    label4.Text = DateTime.Now.ToString();
        //}

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
