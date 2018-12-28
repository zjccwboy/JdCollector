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
    public class CategoryUpdator : BaseUpdator<CategoryRpository, ecs_category, JdModel>
    {
        public CategoryUpdator(CategoryRpository rpository) : base(rpository) { }

        public async Task<ecs_category> AddOne(string gategoryName)
        {
            var maxSortOrder = await this.Rpository.GetMaxSortOrder();

            var entity = await this.Rpository.GetByName(gategoryName);
            if(entity != null)
            {
                if (maxSortOrder == entity.sort_order)
                    return entity;

                maxSortOrder++;
                entity.sort_order = (uint)maxSortOrder;
                await this.Rpository.Update(entity);
                return entity;
            }

            maxSortOrder++;
            entity = new ecs_category
            {
                cat_name = gategoryName,
                keywords = string.Empty,
                cat_desc = string.Empty,
                parent_id = 0,
                sort_order = (uint)maxSortOrder,
                template_file = string.Empty,
                measure_unit = string.Empty,
                show_in_nav = false,
                style = string.Empty,
                is_show = true,
                grade = true,
                filter_attr = string.Empty,
            };

            await this.Rpository.InsertAsync(entity);
            return entity;
        }
    }
}
