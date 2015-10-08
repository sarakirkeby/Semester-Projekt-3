using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blodtrykssystem;

namespace NS_Data
{
    class Data
    {
        Generator signal;
        public Data()
        {
            signal = new Generator();
        }
        public double getTal(int værdi)
        {
            return signal.getTal(værdi);
        }

    }
}
