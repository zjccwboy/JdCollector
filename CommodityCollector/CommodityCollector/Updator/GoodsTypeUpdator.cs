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
    public class GoodsTypeUpdator : BaseUpdator<GoodsTypeRpository, ecs_goods_type, JdModel>
    {

        public GoodsTypeUpdator(GoodsTypeRpository rpository) : base(rpository) { }

        public async Task<ecs_goods_type> AddOne(JdModel model)
        {
            var entity = await this.Rpository.GetByName(model.AttributeName);
            if (entity != null)
                return entity;

            entity = new ecs_goods_type
            {
                cat_name = model.AttributeName,
                enabled = true,
                attr_group = string.Empty,
            };
            await this.Rpository.InsertAsync(entity);

            return entity;
        }
    }
}
