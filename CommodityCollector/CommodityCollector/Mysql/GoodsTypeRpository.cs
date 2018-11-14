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
        public override async Task InsertAsync(ecs_goods_type entity)
        {
            var sql = @"Insert into ecs_goods_type(cat_name, enabled, attr_group) values(@cat_name, 
@enabled, @attr_group)";

            var command = new CommandDefinition(sql, entity);
            var result = await this.sqlConnection.ExecuteAsync(command);
        }
    }
}
