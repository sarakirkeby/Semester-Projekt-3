using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS_Logik
{
    class Blodtryksberegning
    {
        public double calcPuls(List<double> blodtryk, double periode)
        {
            double baseline = 150;
            double maxSpændingOverBaseline = baseline;
            List<double> blodtrykliste = new List<double>();
            blodtrykliste = blodtryk;
            double baselineoverskredet = 0;

            List<double> pulsslag = new List<double>();


            for (int i = 0; i < blodtrykliste.Count; i++)
            {
                if (blodtrykliste[i] > maxSpændingOverBaseline)
                {

                    maxSpændingOverBaseline = blodtrykliste[i];
                    baselineoverskredet++;
                }
                if (blodtrykliste[i] < maxSpændingOverBaseline && baselineoverskredet > 0)
                {
                    pulsslag.Add(i);
                    baselineoverskredet = 0;
                    maxSpændingOverBaseline = baseline;
                    i = i + 50;
                }
            }
            //return (pulsslag.Count - 1) / (pulsslag[pulsslag.Count - 1] * periode - pulsslag[0] * periode) * 60;
            return pulsslag.Count / (periode * blodtrykliste.Count) * 60;
        }

        public double calsSystole(List<double> blodtryk)
        {
            List<double> liste = blodtryk;

            double baseline = 150;
            double maxTrykOverBaseline = baseline;
            List<double> blodtrykliste = new List<double>();
            blodtrykliste = blodtryk;
            double baselineoverskredet = 0;
            List<double> toppunkter = new List<double>();

            for (int i = 0; i < liste.Count - 1; i++)
            {
                if (liste[i] > maxTrykOverBaseline)
                {
                    maxTrykOverBaseline = liste[i];
                    baselineoverskredet++;
                }
                else
                {
                    if (liste[i] < maxTrykOverBaseline && baselineoverskredet > 0)
                    {
                        toppunkter.Add(liste[i-1]);
                        baselineoverskredet = 0;
                        maxTrykOverBaseline = baseline;
                        i = i + 50;
                    }
                }

            }
            if (toppunkter.Count != 0)
            {
                return toppunkter.Average();
            }
            return 0;
        }
        public double calsDiastole(List<double> blodtryk)
        {
            List<double> liste = blodtryk;

            double baseline = 150;
            double maxTrykOverBaseline = baseline;
            List<double> blodtrykliste = new List<double>();
            blodtrykliste = blodtryk;
            double baselineoverskredet = 0;
            List<double> toppunkter = new List<double>();

            for (int i = 0; i < liste.Count - 1; i++)
            {
                if (liste[i] < maxTrykOverBaseline)
                {
                    maxTrykOverBaseline = liste[i];
                    baselineoverskredet++;
                }
                else
                {
                    if (liste[i] > maxTrykOverBaseline && baselineoverskredet > 0)
                    {
                        toppunkter.Add(liste[i - 1]);
                        baselineoverskredet = 0;
                        maxTrykOverBaseline = baseline;
                        i = i + 100;
                    }
                }

            }
            if (toppunkter.Count != 0)
            {
                return toppunkter.Average();
            }
            return 0;
        }
    }
}
