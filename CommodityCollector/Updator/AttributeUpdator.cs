using CommodityCollector.Entity;
using CommodityCollector.Models;
using CommodityCollector.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Updator
{
    public class AttributeUpdator : BaseUpdator<AttributeRpository, ecs_attribute, JdModel>
    {
        public AttributeUpdator(AttributeRpository rpository) : base(rpository) { }

        public async Task<Dictionary<uint, string>> AddAttributes(ecs_goods_type goodsType, List<string> attributes)
        {
            var result = new Dictionary<uint, string>();
            if (attributes == null || !attributes.Any())
                return result;

            foreach(var arrtibute in attributes)
            {
                var entity = await this.Rpository.GetByName(arrtibute);
                if(entity != null)
                {
                    result[entity.attr_id] = arrtibute;
                    continue;
                }

                entity = new ecs_attribute
                {
                    cat_id = goodsType.cat_id,
                    attr_name = arrtibute,
                    attr_input_type = false,
                    attr_type = false,
                    attr_values = string.Empty,
                };
                await this.Rpository.InsertAsync(entity);
                result[entity.attr_id] = arrtibute;
            }
            return result;
        }
    }
}
