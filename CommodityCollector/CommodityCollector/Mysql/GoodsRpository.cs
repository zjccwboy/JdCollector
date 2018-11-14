using CommodityCollector.Entity;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public class GoodsRpository : BaseRpository<ecs_goods>
    {
        public async Task<List<ecs_goods>> GetAllGoods()
        {
            var sql = "select * from ecs_goods";
            var q = await sqlConnection.QueryAsync<ecs_goods>(sql);
            return q.ToList();
        }

        public override async Task InsertAsync(ecs_goods entity)
        {
            var sql = @"Insert into ecs_goods(cat_id, goods_sn, goods_name, goods_name_style,
click_count, brand_id, provider_name, goods_number, goods_weight, market_price, shop_price, promote_price,
promote_start_date, promote_end_date, warn_number, keywords, goods_brief, goods_desc, goods_thumb, goods_img,
original_img, is_real, extension_code, is_on_sale, is_alone_sale, is_shipping, integral, add_time, sort_order,
is_delete, is_best, is_new, is_hot, is_promote, bonus_type_id, last_update, goods_type, seller_note, give_integral,
rank_integral, suppliers_id, is_check) values(@cat_id, @goods_sn, @goods_name, @goods_name_style,
@click_count, @brand_id, @provider_name, @goods_number, @goods_weight, @market_price, @shop_price, @promote_price,
@promote_start_date, @promote_end_date, @warn_number, @keywords, @goods_brief, @goods_desc, @goods_thumb, @goods_img,
@original_img, @is_real, @extension_code, @is_on_sale, @is_alone_sale, @is_shipping, @integral, @add_time, @sort_order,
@is_delete, @is_best, @is_new, @is_hot, @is_promote, @bonus_type_id, @last_update, @goods_type, @seller_note, @give_integral,
@rank_integral, @suppliers_id, @is_check)";

            var command = new CommandDefinition(sql, entity);
            var result = await this.sqlConnection.ExecuteAsync(command);
        }
    }
}
