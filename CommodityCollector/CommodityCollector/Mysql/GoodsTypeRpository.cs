using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class GoodsTypeRpository : BaseRpository<ecs_goods_type>
    {
        public async Task InsertAsync(ecs_goods_type entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.cat_id = (uint)key.Value;
        }

        public async Task<ecs_goods_type> GetByName(string name)
        {
            var sql = $"select * from ecs_goods_type where cat_name='{name}'";
            var q = await this.sqlConnection.QueryAsync<ecs_goods_type>(sql);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }
    }
}
