using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blodtrykssystem
{
    interface ISubject
    {
        void Subscribe(Form1 observer);
        void Unsubscribe(Form1 observer);
        void Notify();
    }
}
