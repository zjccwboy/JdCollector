using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.FileCollector
{
    public abstract class BaseFileCollector : IFileCollector
    {
        public string SavePath { get; }

        public BaseFileCollector(string savaPath)
        {
            this.SavePath = savaPath;

            if (string.IsNullOrEmpty(this.SavePath))
                return;

            FileHelper.NotExistPathCreate(this.SavePath);
        }

        public abstract Task Collect(List<string> address);

        public string GetFileName(string address)
        {
            var lastIndex = address.LastIndexOf('/');
            var name = address.Substring(lastIndex + 1, address.Length - lastIndex - 1);
            var fullName = SavePath + "\\" + name;
            return fullName;
        }

        public void ExistDelete(string fileName)
        {
            FileHelper.ExistFileDelete(fileName);
        }
    }
}
