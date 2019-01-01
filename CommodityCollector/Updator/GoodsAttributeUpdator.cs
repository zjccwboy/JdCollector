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

        public async Task AddGoodsAttributes(Dictionary<uint,string> goodsAttributes, uint goodsId, Dictionary<uint, List<string>> maryAttributes)
        {
            await this.Rpository.DeleteByGoodsId(goodsId);

            foreach(var kv in goodsAttributes)
            {
                var entity = new ecs_goods_attr
                {
                    goods_id = goodsId,
                    attr_id = kv.Key,
                    attr_value = kv.Value,
                    attr_price = string.Empty,
                };

                await this.Rpository.InsertAsync(entity);
            }

            foreach(var kv in maryAttributes)
            {
                foreach(var d in kv.Value)
                {
                    var entity = new ecs_goods_attr
                    {
                        goods_id = goodsId,
                        attr_id = kv.Key,
                        attr_value = d,
                        attr_price = string.Empty,
                    };
                    await this.Rpository.InsertAsync(entity);
                }
            }

        }
    }
}
