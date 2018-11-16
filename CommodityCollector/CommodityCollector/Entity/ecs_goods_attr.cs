using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_goods_attr : IEntity
    {
        [Key]
        public uint goods_attr_id { get; set; }
        public uint goods_id { get; set; }
        public uint attr_id { get; set; }
        public string attr_value { get; set; }
        public string attr_price { get; set; }
    }
}
