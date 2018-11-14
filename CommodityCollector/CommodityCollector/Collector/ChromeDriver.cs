using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Collector
{
    public class ChromeDriver
    {
        public static IWebDriver WebDriver { get; set; }
        static ChromeDriver()
        {
            WebDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
        }
    }
}
