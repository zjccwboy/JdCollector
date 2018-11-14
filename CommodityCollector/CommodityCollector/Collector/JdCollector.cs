using CommodityCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CommodityCollector.Collector
{
    public class JdCollector : BaseCollector<JdModel>
    {
        public JdCollector(string url) : base(url) { }

        public override async Task<JdModel> GetResult()
        {


            var model = new JdModel();

            //商品价格
            model.Price = GetPrice(doc);

            //商品名称
            model.GoodsName = GetGoodsName(doc);

            //商品品牌
            model.Brand = GetBrand(doc);

            return model;
        }

        /// <summary>
        /// 获取商品价格
        /// </summary>
        /// <returns></returns>
        private decimal GetPrice()
        {
            var xpath = "/html/body//*[@class=\"w\"]//*[@class=\"itemInfo-wrap\"]//*[@class=\"dd\"][1]";
            var element = this.WebDriver.FindElement(By.Id("app"));



            return 0;
        }

        private string GetGoodsName()
        {
            return string.Empty;
        }

        private string GetBrand()
        {
            return string.Empty;
        }
    }
}
