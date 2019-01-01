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

        public static IWebDriver CreateWebDriver()
        {
            if (WebDriver != null)
                return WebDriver;

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = GetOptions();
            WebDriver = new ChromeDriver(service, options, TimeSpan.FromMinutes(3));
            return WebDriver;
        }

        public static ChromeOptions GetOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1440,900");
            options.AddUserProfilePreference("profile", new { default_content_setting_values = new { images = 2, javascript = 2 } });

            return options;
        }
    }
}
