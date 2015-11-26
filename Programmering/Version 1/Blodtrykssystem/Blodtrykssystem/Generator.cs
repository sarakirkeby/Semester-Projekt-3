using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blodtrykssystem
{
    class Generator
    {
        private List<double> sinusværdier;
        int counter = 0;
        public double minVærdi
        {
            get; set;
        }
        public double maxVærdi
        {
            get; set;
        }

        public Generator()
        {
            sinusværdier = new List<double>();
            indlæs(@"bp3.txt");
        }
        private void indlæs(string sti)
        {

                string[] lines = System.IO.File.ReadAllLines(sti);

                foreach (string line in lines)
                {
                    sinusværdier.Add(Convert.ToDouble(line));
                }
            minVærdi = sinusværdier.Min();
            maxVærdi = sinusværdier.Max();

        }
        public double getTal(int værdi)
        {
            double tal = sinusværdier[værdi];
            counter++;
            return tal;
        }
    }

    }
