using CommodityCollector.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Collector
{
    public abstract class BaseCollector<TModel> where TModel : IModel
    {
        protected string Url { get; set; }
        protected IWebDriver WebDriver { get; set; }
        public BaseCollector(string url)
        {
            this.Url = url;
            this.WebDriver = ChromeWebDriver.CreateWebDriver();
        }

        public void ReloadWebDriver()
        {
            try
            {
                this.WebDriver.Close();
            }
            catch { }

            ChromeWebDriver.WebDriver = null;
            this.WebDriver = ChromeWebDriver.CreateWebDriver();
        }

        /// <summary>
        /// 加载网页
        /// </summary>
        public async Task LoadWeb()
        {
            await Task.Run(() =>
            {
                try
                {
                    this.WebDriver.Navigate().GoToUrl(this.Url);
                }
                catch { }
            });
        }

        public abstract Task<TModel> GetResult();
    }
}
