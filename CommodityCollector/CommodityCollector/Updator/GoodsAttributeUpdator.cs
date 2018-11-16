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
    public class GoodsAttributeUpdator : BaseUpdator<GoodsAttributeRpository, ecs_goods_attr, JdModel>
    {
        public GoodsAttributeUpdator(GoodsAttributeRpository rpository) : base(rpository) { }

        public async Task AddGoodsAttributes(Dictionary<uint,string> goodsAttributes, uint goodsId)
        {
            foreach(var kv in goodsAttributes)
            {
                var entity = await this.Rpository.GetByAttrId(kv.Key);
                if (entity != null)
                    continue;

                entity = new ecs_goods_attr
                {
                    goods_id = goodsId,
                    attr_id = kv.Key,
                    attr_value = kv.Value,
                    attr_price = string.Empty,
                };
            }
        }
    }
}
