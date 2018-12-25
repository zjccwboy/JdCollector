using CommodityCollector.Mysql;
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
    public partial class SettingsForm : Form
    {
        public ConfigEntity Config { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private async void btnClearJdTitle_Click(object sender, EventArgs e)
        {
            var rpository = new GoodsRpository();
            var allGoods = await rpository.GetAllGoods();
            var count = 0;
            foreach(var goods in allGoods)
            {
                if (goods.goods_name.Contains("京东精选"))
                {
                    goods.goods_name = goods.goods_name.Replace("京东精选", string.Empty);
                    await rpository.UpdateAsync(goods);
                    count++;
                }
                else if (goods.goods_name.Contains("京东"))
                {
                    goods.goods_name = goods.goods_name.Replace("京东", string.Empty);
                    await rpository.UpdateAsync(goods);
                    count++;
                }
            }
            if(count > 0)
            {
                MessageBox.Show($"修改了{count}条！", "提示！");
            }
            else
            {
                MessageBox.Show($"未找到京东字样标题！", "提示！");
            }
        }
    }
}
