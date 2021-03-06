﻿using CommodityCollector.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public class RemarkDownloader : BaseDownloader
    {
        public RemarkDownloader(string savePath) : base(savePath)
        {
            WinformLog.ShowLog($"开始下载商品描述页图片");
        }

        public override async Task Download(List<string> address)
        {
            if (address == null || !address.Any())
                return;

            var deletes = new List<string>();
            foreach (var addr in address)
            {
                var fileName = this.GetFileName(addr);
                var fullName = this.SavePath + "\\" + fileName;
                this.ExistDelete(fullName);
                if (!await HttpHelper.DownLoadAsync(addr, fullName))
                {
                    deletes.Add(addr);
                    continue;
                }
                WinformLog.ShowLog($"下载商品描述页图片完成：{fileName}");
            }

            foreach (var delete in deletes)
            {
                address.Remove(delete);
            }
        }

        public List<string> GetDescPictures(List<string> address)
        {
            var result = new List<string>();
            if (address == null || !address.Any())
                return result;

            foreach (var addr in address)
            {
                var fileName = this.GetFileName(addr);
                var prcture = $"/images/remarks/{fileName}";
                result.Add(prcture);
            }
            return result;
        }

    }
}
