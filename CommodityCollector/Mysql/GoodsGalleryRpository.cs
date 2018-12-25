using CommodityCollector.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class GoodsGalleryRpository : BaseRpository<ecs_goods_gallery>
    {
        public async Task InsertAsync(ecs_goods_gallery entity)
        {
            var key = await this.sqlConnection.InsertAsync(entity);
            if (key != null)
                entity.img_id = (uint)key.Value;
        }
    }
}
