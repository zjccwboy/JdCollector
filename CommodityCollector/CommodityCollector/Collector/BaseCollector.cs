using CommodityCollector.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Collector
{
    public abstract class BaseCollector<TModel> : IDisposable where TModel : IModel
    {
        protected string Url { get; set; }
        protected IWebDriver WebDriver { get; set; }
        public BaseCollector(string url)
        {
            this.Url = url;
            this.WebDriver = ChromeDriver.WebDriver;
            LoadWeb();
        }

        /// <summary>
        /// 加载网页
        /// </summary>
        public void LoadWeb()
        {
            this.WebDriver.Navigate().GoToUrl(this.Url);
        }

        public abstract Task<TModel> GetResult();

        public void Dispose()
        {
            this.WebDriver.Quit();
        }
    }
}
