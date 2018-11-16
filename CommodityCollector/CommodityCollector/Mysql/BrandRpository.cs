using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class BrandRpository : BaseRpository<ecs_brand>
    {
        public async Task InsertAsync(ecs_brand entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.brand_id = (uint)key.Value;
        }
    }
}
