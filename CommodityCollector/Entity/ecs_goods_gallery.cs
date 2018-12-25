using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_goods_gallery : IEntity
    {
        [Key]
        public uint img_id { get; set; }
        public uint goods_id { get; set; }
        public string img_url { get; set; }
        public string img_desc { get; set; }
        public string thumb_url { get; set; }
        public string img_original { get; set; }
    }
}
