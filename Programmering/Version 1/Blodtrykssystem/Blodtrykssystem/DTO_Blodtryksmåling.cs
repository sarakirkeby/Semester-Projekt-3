using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class DTO_Blodtryksmåling
    {
        private List<double> målinger_;

        public DTO_Blodtryksmåling(List<double> målinger)
        {
            målinger_ = målinger;
        }
        public List<double> getMålinger1()
        {
            return målinger_;
        }
    }
}
