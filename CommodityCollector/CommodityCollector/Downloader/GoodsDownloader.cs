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

        public override async Task Download(List<string> address)
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

                var thumbnailName = MakeThumbnail(fullName, fileName);
                WinformLog.ShowLog($"生成缩略图完成：{thumbnailName}");
            }
        }

        public string GetGoodsPicture(List<string> address)
        {
            if (address == null || !address.Any())
                return string.Empty;

            var addr = address.FirstOrDefault();
            var fileName = this.GetFileName(addr);
            return $"/images/goods/{fileName}";
        }

        public List<string> GetGoodsPictures(List<string> address)
        {
            var result = new List<string>();
            if (address == null || !address.Any())
                return result;

            foreach(var addr in address)
            {
                var fileName = this.GetFileName(addr);
                var prcture = $"/images/goods/{fileName}";
                result.Add(prcture);
            }

            return result;
        }

        public List<string> GetThumbnails(List<string> address)
        {
            var result = new List<string>();
            if (address == null || !address.Any())
                return result;

            foreach (var addr in address)
            {
                var fileName = this.GetFileName(addr);
                var prcture = $"/images/goods/Thumbnail/{fileName}";
                result.Add(prcture);
            }
            return result;
        }

        private string MakeThumbnail(string sourceFileName, string fileName)
        {
            var path = this.SavePath + "\\Thumbnail";
            FileHelper.NotExistPathCreate(path);

            var newFileName = path + "\\" + fileName;
            FileHelper.ExistFileDelete(newFileName);
            MakeThumbnail(sourceFileName, newFileName, 150, 150);
            return newFileName;
        }

        /// <summary>
        /// 图片显示成缩略图
        /// </summary>
        /// <param name="sourcePath" >图片原路径</param>
        /// <param name="newPath">生成的缩略图新路径</param>
        /// <param name="width">生成的缩略图宽度</param>
        /// <param name="height">生成的缩略图高度</param>
        private void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            //sourcePath = System.Web.HttpContext.Current.Server.MapPath(sourcePath);
            //newPath = System.Web.HttpContext.Current.Server.MapPath(newPath);
            var ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            var bitmap = new System.Drawing.Bitmap(towidth, toheight);
            var g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}
