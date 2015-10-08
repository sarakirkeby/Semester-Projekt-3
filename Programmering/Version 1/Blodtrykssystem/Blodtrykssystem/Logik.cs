using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS_Data;

namespace NS_Logik
{
    class Logik
    {
        Data datalag;
        public Logik()
        {
            datalag = new Data();
        }
        public double getTal(int værdi)
        {
            return datalag.getTal(værdi);
        }
    }


}
