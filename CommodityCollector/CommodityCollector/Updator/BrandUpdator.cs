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
    public class BrandUpdator : BaseUpdator<BrandRpository, ecs_brand, JdModel>
    {
        public BrandUpdator(BrandRpository rpository) : base(rpository) { }

        public async Task<ecs_brand> AddOne(JdModel model)
        {
            if (string.IsNullOrEmpty(model.Brand))
                return null;

            var entity = await this.Rpository.GetByName(model.Brand);
            if (entity != null)
                return entity;

            entity = new ecs_brand
            {
                brand_name = model.Brand,
                brand_logo = string.Empty,
                brand_desc = string.Empty,
                site_url = string.Empty,
            };
            await this.Rpository.InsertAsync(entity);
            return entity;
        }
    }
}
