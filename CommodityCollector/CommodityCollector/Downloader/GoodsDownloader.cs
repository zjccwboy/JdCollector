using CommodityCollector.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public class GoodsDownloader : BaseDownloader
    {
        public GoodsDownloader(string savePath) : base(savePath)
        {
            WinformLog.ShowLog($"开始下载商品图片");
        }

        public override async Task Collect(List<string> address)
        {
            if (address == null || !address.Any())
                return;

            foreach (var addr in address)
            {
                var fileName = this.GetFileName(addr);
                var fullName = this.SavePath + "\\" + fileName;
                this.ExistDelete(fullName);
                await HttpHelper.DownLoadAsync(addr, fullName);
                WinformLog.ShowLog($"下载商品图片完成：{fileName}");
            }
        }

    }
}
