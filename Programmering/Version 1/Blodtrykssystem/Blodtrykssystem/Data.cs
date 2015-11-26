using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blodtrykssystem;
using AsynchDAQ;
using DTO;
using System.Threading;

namespace NS_Data
{
    class Data
    {
        DAQklasse daq;
        Generator signal;
        private List<double> målinger;
        Thread t1;
        DTO_Blodtryksmåling dto;
        public Data()
        {
            signal = new Generator();
            daq = new DAQklasse();
            målingStart();
        }
        private void sætIgang()
        {
            målingStart();
        }
        public DTO_Blodtryksmåling getMålinger()
        {
            return dto;
        }
        public int getAntalMålinger()
        {
            return daq.getAntalMålinger();
        }
        private void målingStart()
        {
            //object locker = new object();
            //lock(locker)
            //{

            daq.startMåling();

            målinger = daq.målinger;
                   
            dto = new DTO_Blodtryksmåling(målinger);
        }
      

        public double getTal(int værdi)
        {
                return signal.getTal(værdi);
        }
        public bool IsRunning()
        {
            return daq.IsRunning();
        }

        public DTO_Blodtryksmåling HentData()
        {
            return new DTO_Blodtryksmåling(daq.målinger);
        }

    }
}
