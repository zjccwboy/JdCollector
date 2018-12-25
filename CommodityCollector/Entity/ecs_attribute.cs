using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_attribute : IEntity
    {
        [Key]
        public uint attr_id { get; set; }
        public uint cat_id { get; set; }
        public string attr_name { get; set; }
        public bool attr_input_type { get; set; } = true;
        public bool attr_type { get; set; } = true;
        public string attr_values { get; set; }
        public bool attr_index { get; set; }
        public int sort_order { get; set; }
        public bool is_linked { get; set; }
        public bool attr_group { get; set; }
    }
}
