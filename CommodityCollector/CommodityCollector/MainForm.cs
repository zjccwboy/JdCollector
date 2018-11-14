using CommodityCollector.Collector;
using CommodityCollector.Log;
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

        private async void StartCollect(List<string> urls)
        {
            this.btnStartCollect.Enabled = false;
            foreach (var url in urls)
            {
                await CollectOne(url);
            }
            this.btnStartCollect.Enabled = true;
        }

        private async Task CollectOne(string url)
        {
            var collect = new JdCollector(url);
            var model = await collect.GetResult();
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

        private void btnStartCollect_Click(object sender, EventArgs e)
        {
            var urls = GetGoodsUrls();
            if (urls == null)
            {
                MessageBox.Show("请输入商品网址。", "提示！");
                return;
            }

            foreach(var url in urls)
            {
                if (!url.StartsWith("https://item.jd.com") && !url.StartsWith("http://item.jd.com"))
                {
                    MessageBox.Show($"商品网址：{url}不符合采集要求，只能输入京东的商品地址。","提示！");
                    return;
                }
            }

            StartCollect(urls);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            WinformLog.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ChromeWebDriver.WebDriver.Close();
                ChromeWebDriver.WebDriver.Quit();
            }
            catch { }
        }
    }
}
