using CommodityCollector.Entity;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class GoodsRpository : BaseRpository<ecs_goods>
    {
        public async Task<List<ecs_goods>> GetAllGoods()
        {
            var sql = "select * from ecs_goods";
            var q = await sqlConnection.QueryAsync<ecs_goods>(sql);
            return q.ToList();
        }

        public async Task InsertAsync(ecs_goods entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if(key != null)
                entity.goods_id = (uint)key.Value;
        }

        public async Task<ecs_goods> GetByName(string name)
        {
            var sql = $"select * from ecs_goods where goods_name='{name}'";
            var q = await this.sqlConnection.QueryAsync<ecs_goods>(sql);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }

        public async Task<bool> UpdateSnAsync(uint goodsId, string goodsSn)
        {
            var sql = $"update ecs_goods set goods_sn='{goodsSn}' where goods_id={goodsId}";
            var q = await this.sqlConnection.ExecuteAsync(sql);
            return q > 0;
        }

        public async Task UpdateAsync(ecs_goods entity)
        {
            await this.sqlConnection.UpdateAsync(entity);
        }
    }
}
