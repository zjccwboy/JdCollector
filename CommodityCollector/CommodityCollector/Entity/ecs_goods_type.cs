using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_goods_type : IEntity
    {
        public uint cat_id { get; set; }
        public string cat_name { get; set; }
        public bool enabled { get; set; } = true;
        public string attr_group { get; set; }
    }
}
