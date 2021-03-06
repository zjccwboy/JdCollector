﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector
{
    public class HttpHelper
    {
        public static async Task<string> GetHtml(string url)
        {
            if (url.StartsWith("https"))
                SupportHttps();

            var webClient = new WebClient();
            var result = await webClient.DownloadStringTaskAsync(new Uri(url));
            return result;
        }

        private static void SupportHttps()
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
        }

        public static async Task<bool> DownLoadAsync(string address, string fileName)
        {
            if (address.StartsWith("https"))
                SupportHttps();

            var webClient = new WebClient();
            try
            {
                await webClient.DownloadFileTaskAsync(address, fileName);
                return true;
            }
            catch { }
            return false;
        }
    }
}
