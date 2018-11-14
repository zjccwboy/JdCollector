using CommodityCollector.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public class RemarkPictureCollector : BaseFileCollector
    {
        public RemarkPictureCollector(string savePath) : base(savePath)
        {
            WinformLog.ShowLog($"开始下载商品描述页图片");
        }

        public override async Task Collect(List<string> address)
        {
            if (address == null || !address.Any())
                return;

            foreach (var addr in address)
            {
                var fileName = this.GetFileName(addr);
                this.ExistDelete(fileName);
                await HttpHelper.DownLoadAsync(addr, fileName);
                WinformLog.ShowLog($"下载商品描述页图片完成：{fileName}");
            }
        }
    }
}
