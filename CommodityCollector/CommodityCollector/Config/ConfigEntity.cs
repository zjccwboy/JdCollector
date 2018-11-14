using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector
{
    public class ConfigEntity
    {
        public string Path { get; set; }
        public string DbAddress { get; set; }
        public string DbName { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DbPort { get; set; }
    }
}
