using CommodityCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace CommodityCollector.Collector
{
    public class JdCollector : BaseCollector<JdModel>
    {
        public JdCollector(string url) : base(url) { }

        public override async Task<JdModel> GetResult()
        {

            await this.LoadWeb();

            var model = new JdModel();

            //商品价格
            model.Price = GetPrice();

            //商品名称
            model.GoodsName = GetGoodsName();

            //商品品牌
            model.Brand = GetBrand();

            //商品属性
            model.Attributes= GetGoodsAttributes();

            return await Task.FromResult(model);
        }

        /// <summary>
        /// 获取商品价格
        /// </summary>
        /// <returns></returns>
        private decimal GetPrice()
        {
            var element = this.WebDriver.FindElement(By.ClassName("p-price"));
            if (element == null)
                return 0;

            var text = element.Text.TrimStart('￥');
            return decimal.Parse(text);
        }

        /// <summary>
        /// 获取商品名称
        /// </summary>
        /// <returns></returns>
        private string GetGoodsName()
        {
            var element = this.WebDriver.FindElement(By.ClassName("sku-name"));
            return element.Text;
        }

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        private string GetBrand()
        {
            var element = this.WebDriver.FindElement(By.Id("parameter-brand"));
            var result = element.Text.TrimStart("品牌：".ToArray());
            return result;
        }

        /// <summary>
        /// 获取商品属性
        /// </summary>
        /// <returns></returns>
        private Dictionary<string,string> GetGoodsAttributes()
        {
            var result = new Dictionary<string, string>();
            for(var i = 2; i < 100; i++)
            {
                try
                {
                    var xpath = $"//*[@id=\"detail\"]/div[2]/div[1]/div[1]/ul[2]/li[{i}]";
                    var element = this.WebDriver.FindElement(By.XPath(xpath));
                    if (element == null)
                        break;

                    var attributes = element.Text.Split(new string[] { "：" }, StringSplitOptions.RemoveEmptyEntries);
                    result[attributes[0]] = attributes[1];
                }
                catch
                {
                    break;
                }
            }

            return result;
        }
    }
}
