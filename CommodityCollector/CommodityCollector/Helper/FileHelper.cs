using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector
{
    public class FileHelper
    {
        public static async Task<ConfigEntity> ReadConfig()
        {
            var path = Directory.GetCurrentDirectory() + "/Config.json";
            using (var sr = new StreamReader(File.Open(path, FileMode.OpenOrCreate)))
            {
                var json = await sr.ReadToEndAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigEntity>(json);
                return result = result == null ? new ConfigEntity() : result;
            }
        }      

        public static async Task WriteConfig(ConfigEntity config)
        {
            var path = Directory.GetCurrentDirectory() + "/Config.json";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(config);
            using(var sw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                await sw.WriteAsync(json);
            }
        }

        public static string GetConnectionString(ConfigEntity config)
        {
            return $"Database={config.DbName};Data Source={config.DbAddress};User Id={config.DbUser};Password={config.DbPassword};CharSet=utf8;port={config.DbPort}";
        }
        
        public static void NotExistPathCreate(string path)
        {
            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }

        public static void ExistFileDelete(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            File.Delete(fileName);                
        }
    }
}
