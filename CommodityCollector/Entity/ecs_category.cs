using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_category : IEntity
    {
        [Key]
        public uint cat_id { get; set; }
        public string cat_name { get; set; }
        public string keywords { get; set; }
        public string cat_desc { get; set; }
        public int parent_id { get; set; }
        public uint sort_order { get; set; }
        public string template_file { get; set; }
        public string measure_unit { get; set; }
        public bool show_in_nav { get; set; }
        public string style { get; set; }
        public bool is_show { get; set; }
        public bool grade { get; set; }
        public string filter_attr { get; set; }
    }
}
