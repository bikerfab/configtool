using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configtool
{
    [Serializable]
    public class bleData
    {
        public String uuid;
        public bool r;
        public bool w;
        public bool n;
        public bleData(String uuidval, bool rval, bool wval, bool nval)
        {
            uuid = uuidval;
            r = rval;
            w = wval;
            n = nval;
        }
    }
}
