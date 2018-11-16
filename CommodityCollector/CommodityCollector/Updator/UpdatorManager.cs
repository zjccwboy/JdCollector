﻿using CommodityCollector.Entity;
using CommodityCollector.Models;
using CommodityCollector.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Updator
{
    public class UpdatorManager
    {
        public static async Task HandleJdModel(UpdatorModel model)
        {
            //写商品类型
            var goodsType = await AddGoodsType(model.JdModel);

            //写商品属性分类
            var attributes = await AddAttributes(goodsType, model.JdModel.Attributes.Keys.ToList());

            //获取商品属性ecs_goods_attr表对应的attr_id attr_value
            var goodsAttributes = GetGoodsAttributes(attributes, model.JdModel.Attributes);

            //写品牌信息
            var brand = await AddBrand(model.JdModel);

            //写商品信息
            var goodsEntity = await AddGoods(model, brand, goodsType.cat_id);

            //写商品属性
            await AddGoodsAttributes(goodsAttributes, goodsEntity);

        }

        private static Task<ecs_goods_type> AddGoodsType(JdModel model)
        {
            var updator = new GoodsTypeUpdator(new GoodsTypeRpository());
            return updator.AddOne(model);
        }

        private static async Task<Dictionary<uint, string>> AddAttributes(ecs_goods_type goodsType, List<string> attributes)
        {
            var updator = new AttributeUpdator(new AttributeRpository());
            var result = await updator.AddAttributes(goodsType, attributes);
            return result;
        }

        private static Dictionary<uint, string> GetGoodsAttributes(Dictionary<uint, string> attributes, Dictionary<string, string> sourceAttributes)
        {
            var goodsAttributes = new Dictionary<uint, string>();
            foreach(var kv in attributes)
            {
                goodsAttributes[kv.Key] = sourceAttributes[kv.Value];
            }
            return goodsAttributes;
        }

        private static async Task<ecs_brand> AddBrand(JdModel model)
        {
            var updator = new BrandUpdator(new BrandRpository());
            return await updator.AddOne(model);
        }

        private static async Task<ecs_goods> AddGoods(UpdatorModel model, ecs_brand brand, uint catId)
        {
            var updator = new GoodsUpdator(new GoodsRpository());
            var entity = await updator.AddOne(model, brand, catId);
            return entity;
        }

        private static async Task AddGoodsAttributes(Dictionary<uint,string> goodsAttributes, ecs_goods goods)
        {
            var updator = new GoodsAttributeUpdator(new GoodsAttributeRpository());
            await updator.AddGoodsAttributes(goodsAttributes, goods.goods_id);
        }
    }
}
