using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blodtrykssystem
{
    class Filter
    {
        public List<double> filtrerSignal(List<double> signal)
        {
            List<double> filtreretSignal = new List<double>();
            for(int i = 0; i < 500; i++)
            {
                filtreretSignal.Add(0);
            }
            return filtreretSignal;
        }
    }
}
