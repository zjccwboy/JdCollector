using CommodityCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using CommodityCollector.Log;

namespace CommodityCollector.Collector
{
    public class JdCollector : BaseCollector<JdModel>
    {
        public JdCollector(string url) : base(url) { }

        public override async Task<JdModel> GetResult()
        {

            WinformLog.ShowLog($"开始加载商品页:{this.Url}");
            await this.LoadWeb();
            WinformLog.ShowLog($"加载商品页成功:{this.Url}");

            var model = new JdModel();

            //商品价格
            model.Price = GetPrice();
            WinformLog.ShowLog($"商品价格分析结果:{model.Price}");

            //商品名称
            model.GoodsName = GetGoodsName();
            WinformLog.ShowLog($"商品名称分析结果:{model.GoodsName}");

            //商品品牌
            model.Brand = GetBrand();
            WinformLog.ShowLog($"商品品牌分析结果:{model.Brand}");

            //商品属性
            model.Attributes= GetGoodsAttributes();
            WinformLog.ShowLog($"商品属性分析结果:{Newtonsoft.Json.JsonConvert.SerializeObject(model.Attributes)}");

            //商品图片
            model.GoodsPictures = GetGoodsPictures();
            WinformLog.ShowLog($"商品图片分析结果:{Newtonsoft.Json.JsonConvert.SerializeObject(model.GoodsPictures)}");

            //商品描述
            model.GoodsRemarks = GetGoodsRemarks();
            WinformLog.ShowLog($"商品描述分析结果:{Newtonsoft.Json.JsonConvert.SerializeObject(model.GoodsRemarks)}");

            WinformLog.ShowLog(Environment.NewLine);
            WinformLog.ShowLog($"---------------------------------------------------------------------------------------------------");
            WinformLog.ShowLog($"---------------------------------------------------------------------------------------------------");
            WinformLog.ShowLog(Environment.NewLine);

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
        private Dictionary<string, string> GetGoodsAttributes()
        {
            var result = new Dictionary<string, string>();
            for (var i = 2; i < 100; i++)
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

        private List<string> GetGoodsPictures()
        {
            var result = new List<string>();
            for(var i = 1; i < 100; i++)
            {
                try
                {
                    var xpath = $"//*[@id=\"spec-list\"]/ul/li[{i}]/img";
                    var element = this.WebDriver.FindElement(By.XPath(xpath));
                    if (element == null)
                        break;

                    //模拟点击一次页面才能抓到商品图片
                    element.Click();
                    var pictureElement = this.WebDriver.FindElement(By.Id("spec-img"));
                    var jqimg = pictureElement.GetAttribute("jqimg");
                    jqimg = jqimg.StartsWith("http://") ? jqimg : "http://" + jqimg.TrimStart("//".ToArray());
                    result.Add(jqimg);
                }
                catch
                {
                    break;
                }
            }
            return result;
        }

        private List<string> GetGoodsRemarks()
        {
            var result = new List<string>();
            for(var i = 1; i < 5; i++)
            {
                for(var j = 1; i < 20; j++)
                {
                    try
                    {
                        var xpath = $"//*[@id=\"J-detail-content\"]/div/div[{i}]/img[{j}]";
                        var element = this.WebDriver.FindElement(By.XPath(xpath));
                        if (element == null)
                            break;

                        //延迟加载的才是真正的src
                        var lazyload = element.GetAttribute("data-lazyload");
                        lazyload = lazyload.StartsWith("http://") ? lazyload : "http://" + lazyload.TrimStart("//".ToArray());
                        result.Add(lazyload);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            return result;
        }
    }
}
