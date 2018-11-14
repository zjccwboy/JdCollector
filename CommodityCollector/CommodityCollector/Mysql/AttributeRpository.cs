using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class AttributeRpository : BaseRpository<ecs_attribute>
    {
        public override async Task InsertAsync(ecs_attribute entity)
        {
            var sql = @"Insert into ecs_attribute(cat_id, attr_name,attr_input_type, attr_type,
attr_values, attr_index, sort_order,is_linked, attr_group) values(@cat_id, @attr_name, @attr_input_type,
@attr_type, @attr_values, @attr_index, @sort_order, @is_linked, @attr_group)";

            var command = new CommandDefinition(sql, entity);
            var result = await this.sqlConnection.ExecuteAsync(command);
        }
    }
}
