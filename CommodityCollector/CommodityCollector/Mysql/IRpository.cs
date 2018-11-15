using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public interface IRpository<TEntity>
    {
        Task InsertAsync(TEntity entity);
    }
}
