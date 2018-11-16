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
    public class GoodsGalleryUpdator : BaseUpdator<GoodsGalleryRpository, ecs_goods_gallery, JdModel>
    {
        public GoodsGalleryUpdator(GoodsGalleryRpository rpository) : base(rpository) { }

        public async Task AddGoodsGallerys(List<string> pictures, List<string> thumbPictures, uint goodsId)
        {
            if (pictures == null || thumbPictures == null || !pictures.Any() || !thumbPictures.Any() || pictures.Count != thumbPictures.Count)
                return;

            var count = pictures.Count;
            for(int i=0; i< count; i++)
            {
                var picture = pictures[i];
                var thumb = thumbPictures[i];
                var entity = new ecs_goods_gallery
                {
                    goods_id = goodsId,
                    img_url = picture,
                    img_desc = string.Empty,
                    thumb_url = thumb,
                    img_original = picture,
                };
                await this.Rpository.InsertAsync(entity);
            }

        }
    }
}
