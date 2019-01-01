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

            //颜色
            const string colorAttributeName = "颜色";
            var entity = await this.Rpository.GetByNameAndCatId(colorAttributeName, goodsType.cat_id);
            if (entity != null)
            {
                result[entity.attr_id] = colorAttributeName;
            }
            else
            {
                entity = new ecs_attribute
                {
                    cat_id = goodsType.cat_id,
                    attr_name = colorAttributeName,
                    attr_input_type = false,
                    attr_type = true,
                    attr_values = string.Empty,
                };
                await this.Rpository.InsertAsync(entity);
                result[entity.attr_id] = colorAttributeName;
            }

            //尺寸
            const string sizeAttributeName = "尺寸";
            entity = await this.Rpository.GetByNameAndCatId(sizeAttributeName, goodsType.cat_id);
            if (entity != null)
            {
                result[entity.attr_id] = sizeAttributeName;
            }
            else
            {
                entity = new ecs_attribute
                {
                    cat_id = goodsType.cat_id,
                    attr_name = sizeAttributeName,
                    attr_input_type = false,
                    attr_type = true,
                    attr_values = string.Empty,
                };
                await this.Rpository.InsertAsync(entity);
                result[entity.attr_id] = sizeAttributeName;
            }

            foreach (var arrtibute in attributes)
            {
                entity = await this.Rpository.GetByNameAndCatId(arrtibute, goodsType.cat_id);
                if (entity != null)
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
