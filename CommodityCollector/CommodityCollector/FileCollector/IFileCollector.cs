using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public interface IFileCollector
    {
        Task Collect(List<string> address);
    }
}
