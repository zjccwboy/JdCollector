using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_brand : IEntity
    {
        [Key]
        public uint brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_logo { get; set; }
        public string brand_desc { get; set; }
        public string site_url { get; set; }
        public int sort_order { get; set; }
        public bool is_show { get; set; }
    }
}
