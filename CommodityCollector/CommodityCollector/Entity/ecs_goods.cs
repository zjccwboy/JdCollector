using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Entity
{
    public class ecs_goods : IEntity
    {
        [Key]
        public uint goods_id { get; set; }
        public uint cat_id { get; set; }
        public string goods_sn { get; set; }
        public string goods_name { get; set; }
        public string goods_name_style { get; set; }
        public int click_count { get; set; }
        public uint brand_id { get; set; }
        public string provider_name { get; set; }
        public int goods_number { get; set; }
        public decimal goods_weight { get; set; }
        public decimal market_price { get; set; }
        public decimal shop_price { get; set; }
        public decimal promote_price { get; set; }
        public uint promote_start_date { get; set; }
        public uint promote_end_date { get; set; }
        public int warn_number { get; set; }
        public string keywords { get; set; }
        public string goods_brief { get; set; }
        public string goods_desc { get; set; }
        public string goods_thumb { get; set; }
        public string goods_img { get; set; }
        public string original_img { get; set; }
        public bool is_real { get; set; }
        public string extension_code { get; set; }
        public bool is_on_sale { get; set; }
        public bool is_alone_sale { get; set; }
        public bool is_shipping { get; set; }
        public int integral { get; set; }
        public uint add_time { get; set; }
        public int sort_order { get; set; }
        public bool is_delete { get; set; }
        public bool is_best { get; set; }
        public bool is_new { get; set; }
        public bool is_hot { get; set; }
        public bool is_promote { get; set; }
        public int bonus_type_id { get; set; }
        public uint last_update { get; set; }
        public int goods_type { get; set; }
        public string seller_note { get; set; }
        public int give_integral { get; set; } = -1;
        public int rank_integral { get; set; } = -1;
        public int? suppliers_id { get; set; }
        public bool? is_check { get; set; }
    }
}
