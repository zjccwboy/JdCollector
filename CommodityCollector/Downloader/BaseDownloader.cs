using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public abstract class BaseDownloader : IDownloader
    {
        public string SavePath { get; }

        public BaseDownloader(string savaPath)
        {
            this.SavePath = savaPath;

            if (string.IsNullOrEmpty(this.SavePath))
                return;

            FileHelper.NotExistPathCreate(this.SavePath);
        }

        public abstract Task Download(List<string> address);

        public List<string> GetPictures(List<string> address)
        {
            var result = new List<string>();
            if (address == null || !address.Any())
                return result;

            foreach(var addr in address)
            {
                var fileName = GetFileName(addr);
                result.Add($"/images/{fileName}");
            }

            return result;
        }

        public string GetFileName(string address)
        {
            var lastIndex = address.LastIndexOf('/');
            var name = address.Substring(lastIndex + 1, address.Length - lastIndex - 1);
            //var fullName = SavePath + "\\" + name;
            return name;
        }

        public void ExistDelete(string fileName)
        {
            FileHelper.ExistFileDelete(fileName);
        }
    }
}
