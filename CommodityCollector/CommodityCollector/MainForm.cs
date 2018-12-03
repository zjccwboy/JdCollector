using CommodityCollector.Collector;
using CommodityCollector.FileCollector;
using CommodityCollector.Log;
using CommodityCollector.Models;
using CommodityCollector.Updator;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommodityCollector
{
    public partial class MainForm : Form
    {
        private bool IsRunOut { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ReadConfig();
            WinformLog.SetUp(this.txtLog);
        }

        private async void ReadConfig()
        {
            await StartReadConfig();
        }

        private async Task StartReadConfig()
        {
            var config = await FileHelper.ReadConfig();
            this.txtPath.Text = config.Path;
            this.txtDbAddress.Text = config.DbAddress;
            this.txtDbName.Text = config.DbName;
            this.txtDbUser.Text = config.DbUser;
            this.txtDbPassword.Text = config.DbPassword;
            this.txtDbPort.Text = string.IsNullOrWhiteSpace(config.DbPort) ? "3306" : config.DbPort;
            DbConnection.ConnectionString = FileHelper.GetConnectionString(config);
        }

        private async Task WriteConfig()
        {
            try
            {
                var config = new ConfigEntity();
                config.Path = this.txtPath.Text;
                config.DbAddress = this.txtDbAddress.Text;
                config.DbName = this.txtDbName.Text;
                config.DbUser = this.txtDbUser.Text;
                config.DbPassword = this.txtDbPassword.Text;
                config.DbPort = this.txtDbPort.Text;
                await FileHelper.WriteConfig(config);

                DbConnection.ConnectionString = FileHelper.GetConnectionString(config);

                MessageBox.Show("保存成功！", "提示！");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "失败！");
            }
        }

        private string SelectRootPath()
        {
            var result = string.Empty;
            var dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                result = dialog.SelectedPath;
            }
            return result;
        }

        private List<string> GetGoodsUrls()
        {
            if (string.IsNullOrWhiteSpace(this.txtGoodsUrls.Text))
                return null;

            var array = this.txtGoodsUrls.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return array.ToList();
        }

        private async Task StartCollect(List<string> urls)
        {
            this.btnStartCollect.Enabled = false;
            foreach (var url in urls)
            {
                await CollectOne(url, 0);
            }
        }

        private async Task CollectOne(string url, int retry)
        {
            JdModel model = null;
            try
            {
                var collect = new JdCollector(url);
                model = await collect.GetResult();
            }
            catch(Exception e)
            {
                ChromeWebDriver.WebDriver.Close();
                ChromeWebDriver.WebDriver.Quit();
                ChromeWebDriver.WebDriver = new ChromeDriver();
                if (retry > 3)
                    return;
                await CollectOne(url, ++retry);
            }

            var updatorModel = await DownLoad(model);
            await UpdateToDatabase(updatorModel);

            WinformLog.ShowLog(Environment.NewLine);
            WinformLog.ShowLog($"---------------------------------------------------------------------------------------------------");
            WinformLog.ShowLog(Environment.NewLine);
        }

        private async Task<UpdatorModel> DownLoad(JdModel model)
        {

            var updatorModel = new UpdatorModel
            {
                JdModel = model,
            };

            var path = this.txtPath.Text + "\\images\\goods";
            var goodsCollector = new GoodsDownloader(path);
            await goodsCollector.Download(model.GoodsPictures);

            //商品默认图片
            updatorModel.GoodsPicture = goodsCollector.GetGoodsPicture(model.GoodsPictures);

            //商品图片列表
            updatorModel.Pictures = goodsCollector.GetGoodsPictures(model.GoodsPictures);

            //商品缩略图
            updatorModel.Thumbnails = goodsCollector.GetThumbnails(model.GoodsPictures);


            WinformLog.ShowLog($"下载商品图片完成");
            WinformLog.ShowLog(null);


            var path2 = this.txtPath.Text + "\\images\\remarks";
            var remarksCollector = new RemarkDownloader(path2);
            await remarksCollector.Download(model.GoodsRemarks);

            //商品描述图片
            updatorModel.DescPictures = remarksCollector.GetDescPictures(model.GoodsRemarks);
            WinformLog.ShowLog($"下载商品描述页图片完成");

            return updatorModel;
        }

        private async Task UpdateToDatabase(UpdatorModel model)
        {
           await UpdatorManager.Update(model);
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            var path = SelectRootPath();
            if (string.IsNullOrWhiteSpace(path))
                return;

            this.txtPath.Text = path;
        }

        private async void btnSaveConfig_Click(object sender, EventArgs e)
        {
           await WriteConfig();
        }

        private async void btnStartCollect_Click(object sender, EventArgs e)
        {
            var urls = GetGoodsUrls();
            if (urls == null)
            {
                MessageBox.Show("请输入商品网址。", "提示！");
                return;
            }

            WinformLog.ShowLog("开始采集");
            this.IsRunOut = true;

            foreach (var url in urls)
            {
                if (!url.StartsWith("https://item.jd.com") && !url.StartsWith("http://item.jd.com"))
                {
                    MessageBox.Show($"商品网址：{url}不符合采集要求，只能输入京东的商品地址。","提示！");
                    return;
                }
            }
            await StartCollect(urls);

            WinformLog.ShowLog("采集完成");
            this.btnStartCollect.Enabled = true;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            WinformLog.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.IsRunOut)
                    return;

                ChromeWebDriver.WebDriver.Close();
                ChromeWebDriver.WebDriver.Quit();
            }
            catch { }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

            //文本框选中的起始点在最后
            this.txtLog.SelectionStart = this.txtLog.TextLength;

            //将控件内容滚动到当前插入符号位置
            this.txtLog.ScrollToCaret();
        }
    }
}
