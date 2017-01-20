using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace configtool
{    
    class Timeout
    {
        Stopwatch sw;
        int time;

        public Timeout(int ms)
        {
            time = ms;
            sw = new Stopwatch();
            sw.Start();
        }

        public bool elapsed()
        {
            return sw.ElapsedMilliseconds > time;
        }

        public void check()
        {
            if (!Debugger.IsAttached)
            {
                if (sw.ElapsedMilliseconds > time) throw new TimeoutException();
            }
        }

        public void stop()
        {
            sw.Stop();
        }
    }
}
