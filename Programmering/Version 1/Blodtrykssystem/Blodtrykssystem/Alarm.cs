using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS_Logik
{
    class Alarm
    {
        public double nedreSystole { get; set; }
        public double øvreSystole { get; set; }
        public double nedreDiastole { get; set; }
        public double øvreDiastole { get; set; }

        private bool status;

        public void setAlarm(double nSys, double øSys, double nDia, double øDia)
        {
            nedreSystole = nSys;
            øvreSystole = øSys;
            nedreDiastole = nDia;
            øvreSystole = øDia;
        }

        public bool checkAlarm(double nSys, double øSys, double nDia, double øDia)
        {
            if(nSys < nedreSystole || øSys > øvreSystole || nDia < nedreDiastole || øDia > øvreDiastole)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}
