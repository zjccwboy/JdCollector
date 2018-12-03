using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class CategoryRpository : BaseRpository<ecs_category>
    {
        public async Task InsertAsync(ecs_category entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.cat_id = (uint)key.Value;
        }

        public async Task<ecs_category> GetByName(string categoryName)
        {
            var sql = $"select * from ecs_category where cat_name='{categoryName}'";
            var command = new CommandDefinition(sql);
            var q = await this.sqlConnection.QueryAsync<ecs_category>(command);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }

        public async Task Update(ecs_category entity)
        {
            await this.sqlConnection.UpdateAsync(entity);
        }

        public async Task<int> GetMaxSortOrder()
        {
            var sql = "select max(sort_order) from ecs_category";
            var command = new CommandDefinition(sql);
            try
            {
                var q = await this.sqlConnection.QueryAsync<uint>(command);
                if (q.Any())
                {
                    return (int)q.First();
                }
            }
            catch{}

            return -1;
        }
    }
}
