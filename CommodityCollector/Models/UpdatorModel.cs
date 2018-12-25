using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Models
{
    public class UpdatorModel
    {
        public JdModel JdModel { get; set; }
        public string GoodsPicture { get; set; }
        public List<string> Pictures { get; set; }
        public List<string> Thumbnails { get; set; }
        public List<string> DescPictures { get; set; }
    }
}
