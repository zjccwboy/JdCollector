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

        public async Task<ecs_brand> GetByName(string name)
        {
            var sql = $"select * from ecs_brand where brand_name='{name}'";
            var command = new CommandDefinition(sql);
            var q = await this.sqlConnection.QueryAsync<ecs_brand>(command);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }
    }
}
