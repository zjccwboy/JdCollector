using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class GoodsAttributeRpository : BaseRpository<ecs_goods_attr>
    {
        public async Task<ecs_goods_attr> GetByAttrId(uint attrId)
        {
            var sql = $"select * from ecs_goods_attr where attr_id='{attrId}'";
            var q = await this.sqlConnection.QueryAsync<ecs_goods_attr>(sql);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }

        public async Task UpdateAsync(ecs_goods_attr entity)
        {
            await this.sqlConnection.UpdateAsync(entity);
        }

        public async Task InsertAsync(ecs_goods_attr entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.goods_attr_id = (uint)key.Value;
        }
    }
}
