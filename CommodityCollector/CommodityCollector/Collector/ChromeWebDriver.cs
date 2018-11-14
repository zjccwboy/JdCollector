using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Collector
{
    public class ChromeWebDriver
    {
        public static IWebDriver WebDriver { get; set; }
        static ChromeWebDriver()
        {
            WebDriver = new ChromeDriver();
        }
    }
}
