using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Models
{
    public class JdModel : IModel
    {
        public string AttributeName { get; set; }
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> GoodsPictures { get; set; }
        public List<string> GoodsRemarks { get; set; }
    }
}
