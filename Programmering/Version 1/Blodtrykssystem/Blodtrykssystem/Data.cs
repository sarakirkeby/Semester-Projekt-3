using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blodtrykssystem;
using AsynchDAQ;

namespace NS_Data
{
    class Data
    {
        DAQklasse daq;
        Generator signal;
        public List<double> målinger;
        public Data()
        {
            signal = new Generator();
            daq = new DAQklasse();
            målinger = daq.målinger;
            daq.startMåling();
        }

        public double getTal(int værdi)
        {
            return signal.getTal(værdi);
        }

    }
}
