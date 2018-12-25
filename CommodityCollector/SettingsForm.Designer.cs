namespace CommodityCollector
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClearJdTitle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClearJdTitle
            // 
            this.btnClearJdTitle.Location = new System.Drawing.Point(27, 29);
            this.btnClearJdTitle.Name = "btnClearJdTitle";
            this.btnClearJdTitle.Size = new System.Drawing.Size(122, 28);
            this.btnClearJdTitle.TabIndex = 0;
            this.btnClearJdTitle.Text = "去掉标题京东";
            this.btnClearJdTitle.UseVisualStyleBackColor = true;
            this.btnClearJdTitle.Click += new System.EventHandler(this.btnClearJdTitle_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 485);
            this.Controls.Add(this.btnClearJdTitle);
            this.Name = "SettingsForm";
            this.Text = "商城批量设置";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClearJdTitle;
    }
}