using CommodityCollector.Entity;
using CommodityCollector.Helper;
using CommodityCollector.Models;
using CommodityCollector.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Updator
{
    public class GoodsUpdator : BaseUpdator<GoodsRpository, ecs_goods, JdModel>
    {

        public GoodsUpdator(GoodsRpository rpository) : base(rpository) { }

        public async Task<ecs_goods> AddOne(UpdatorModel updatorModel, ecs_brand brand, uint goodsTypeId, uint categoryId)
        {
            var sn = string.Empty;
            var model = updatorModel.JdModel;
            var entity = await this.Rpository.GetByName(model.GoodsName);
            if (entity != null)
            {
                sn = GetGoodsSn(entity.goods_id);
                if (await this.Rpository.UpdateSnAsync(entity.goods_id, sn))
                {
                    entity.goods_sn = sn;
                }
                return entity;
            }

            entity = new ecs_goods
            {
                cat_id = categoryId,
                goods_sn = string.Empty,
                goods_name = model.GoodsName,
                goods_name_style = string.Empty,
                brand_id = brand.brand_id,
                provider_name = string.Empty,
                goods_number = new Random().Next(100,1000),
                goods_weight = 1000m,
                market_price = model.Price * 1.2m,
                shop_price = model.Price,
                promote_price = 0,
                promote_start_date = 0,
                promote_end_date = 0,
                warn_number = 1,
                keywords = model.GoodsName,
                goods_brief = string.Empty,
                goods_desc = GetGoodsDesc(updatorModel.DescPictures),
                goods_thumb = updatorModel.Thumbnails.FirstOrDefault(),
                goods_img = updatorModel.GoodsPicture,
                original_img = updatorModel.GoodsPicture,
                is_real = true,
                extension_code = string.Empty,
                is_on_sale = true,
                is_alone_sale = true,
                is_shipping = false,
                integral = 0,
                add_time = DateTimeHelper.DataTimeToUnixStamp(DateTime.Now),
                sort_order = 0,
                is_delete = false,
                is_best = false,
                is_new = true,
                is_hot = true,
                is_promote = false,
                bonus_type_id = 0,
                last_update = DateTimeHelper.DataTimeToUnixStamp(DateTime.Now),
                goods_type = goodsTypeId,
                seller_note = string.Empty,
                give_integral = -1,
                rank_integral = -1,
                suppliers_id = 0,
                is_check = false,
            };

            await this.Rpository.InsertAsync(entity);

            sn = GetGoodsSn(entity.goods_id);
            if(await this.Rpository.UpdateSnAsync(entity.goods_id, sn))
            {
                entity.goods_sn = sn;
            }

            return entity;
        }


        private string GetGoodsSn(uint goodsId)
        {
            var orign = goodsId.ToString();
            var complementCode = 6 - orign.Length;
            for(var i=0;i< complementCode; i++)
            {
                orign = "0" + orign;
            }
            var result = "SKU" + orign;
            return result;
        }

        private string GetGoodsDesc(List<string> remarks)
        {
            var builder = new StringBuilder();
            foreach(var remark in remarks)
            {
                var desc = $"<img align=\"absmiddle\" src =\"{remark}\">";
                builder.AppendLine(desc);
            }
            return builder.ToString();

        }
    }
}
