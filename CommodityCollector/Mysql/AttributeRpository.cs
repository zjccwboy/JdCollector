using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class AttributeRpository : BaseRpository<ecs_attribute>
    {
        public async Task InsertAsync(ecs_attribute entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.attr_id = (uint)key.Value;
        }

        public async Task<ecs_attribute> GetByName(string name)
        {
            var sql = $"select * from ecs_attribute where attr_name='{name}'";
            var command = new CommandDefinition(sql);
            var q = await this.sqlConnection.QueryAsync<ecs_attribute>(sql, command);
            if (q.Any())
            {
                return q.First();
            }
            return null;
        }
    }
}
