using CommodityCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using CommodityCollector.Log;
using System.Text.RegularExpressions;

namespace CommodityCollector.Collector
{
    public class JdCollector : BaseCollector<JdModel>
    {
        public JdCollector(string url) : base(url) { }

        public override async Task<JdModel> GetResult()
        {
            await this.LoadWeb();
            WinformLog.ShowLog($"加载商品页成功：{this.Url}");

            var model = new JdModel();

            //商品价格
            model.Price = GetPrice();
            WinformLog.ShowLog($"商品价格分析结果:{model.Price}");

            //商品名称
            model.GoodsName = GetGoodsName();
            WinformLog.ShowLog($"商品名称分析结果：{model.GoodsName}");

            //商品品牌
            model.Brand = GetBrand();
            WinformLog.ShowLog($"商品品牌分析结果：{model.Brand}");

            //商品分类
            model.Category = GetCategory();
            WinformLog.ShowLog($"商品分类分析结果：{model.GoodsName}");

            //商品属性名
            model.AttributeName = GetAttributeNname();
            WinformLog.ShowLog($"商品属性标签分析结果:{model.AttributeName}");

            //商品属性
            model.Attributes= GetGoodsAttributes();
            WinformLog.ShowLog($"商品属性分析结果：{Newtonsoft.Json.JsonConvert.SerializeObject(model.Attributes)}");

            //商品图片
            model.GoodsPictures = await GetGoodsPictures();
            WinformLog.ShowLog("商品图片分析结果：");
            foreach (var picture in model.GoodsPictures)
            {
                WinformLog.ShowLog($"{picture}");
            }

            //商品描述
            model.GoodsRemarks = await GetGoodsRemarks();
            WinformLog.ShowLog("商品描述分析结果：");
            foreach (var remark in model.GoodsRemarks)
            {
                WinformLog.ShowLog($"{remark}");
            }

            WinformLog.ShowLog(null);
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
        /// 获取分类名字，用分类名字做商品属性主标签
        /// </summary>
        /// <returns></returns>
        private string GetAttributeNname()
        {
            var element = this.WebDriver.FindElement(By.CssSelector("#crumb-wrap > div > div.crumb.fl.clearfix > div:nth-child(3) > a"));
            if (element == null)
                return string.Empty;

            return element.Text;
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
                    if (attributes[1].Contains("店铺"))
                        continue;

                    result[attributes[0]] = attributes[1];
                }
                catch
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 商品图片
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetGoodsPictures()
        {
            var result = new List<string>();
            await Task.Run(() =>
            {
                for (var i = 1; i < 100; i++)
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
                        jqimg = jqimg.Split(new string[] { "!" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        jqimg = jqimg.Replace(@"/n0/", "/imgzone/");
                        result.Add(jqimg);
                    }
                    catch
                    {
                        break;
                    }
                }
            });
            return result;
        }

        /// <summary>
        /// 商品详情页图片
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>>  GetGoodsRemarks()
        {
            var result = new List<string>();
            await Task.Run(() =>
            {
                var regexStr = "data-lazyload=\".*.jpg\"";
                var regex = new Regex(regexStr);
                var source = this.WebDriver.PageSource;
                var matchs = regex.Matches(source);

                if (matchs.Count == 0)
                    return;

                foreach (Match match in matchs)
                {
                    var list = match.Value.Split(new string[] { "data-lazyload=\"" }, StringSplitOptions.RemoveEmptyEntries);

                    if (list == null)
                        return;

                    foreach (var imgUrl in list)
                    {
                        regex = new Regex("//img[\\d]{1,2}.*.jpg");
                        var matchNext = regex.Match(imgUrl);
                        if (!matchNext.Success)
                            continue;

                        var value = matchNext.Value;
                        value = value.StartsWith("http://") ? value : "http://" + value.TrimStart("//".ToArray());
                        if (value.Contains("width="))
                        {
                            value = value.Split(new string[] { "width=" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        }
                        result.Add(value);
                    }
                }
            });
            return result;
        }

        private string GetCategory()
        {
            var element = this.WebDriver.FindElement(By.CssSelector("#crumb-wrap > div > div.crumb.fl.clearfix > div:nth-child(3) > a"));
            if (element == null)
                return string.Empty;

            return element.Text;
        }

    }
}
