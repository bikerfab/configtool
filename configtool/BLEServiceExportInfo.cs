using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configtool
{
    [Serializable]
    public class BLEServiceExportInfo
    {
        public String folder;
        public String srvName;
        public String srvUUID;
        public String charFirstUUID;
        public String baseName;
    }
}
