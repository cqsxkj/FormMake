namespace WindowMake.Propert
{
    partial class PicturePro
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_bkfile = new System.Windows.Forms.Button();
            this.text_filebk = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.IsRoad_check = new System.Windows.Forms.CheckBox();
            this.mapName_tb = new System.Windows.Forms.TextBox();
            this.name_lb = new System.Windows.Forms.Label();
            this.url_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mapId_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_bkfile);
            this.groupBox2.Controls.Add(this.text_filebk);
            this.groupBox2.Location = new System.Drawing.Point(8, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 56);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "背景图片";
            // 
            // btn_bkfile
            // 
            this.btn_bkfile.Location = new System.Drawing.Point(252, 20);
            this.btn_bkfile.Name = "btn_bkfile";
            this.btn_bkfile.Size = new System.Drawing.Size(23, 21);
            this.btn_bkfile.TabIndex = 5;
            this.btn_bkfile.Text = "…";
            this.btn_bkfile.UseVisualStyleBackColor = true;
            this.btn_bkfile.Click += new System.EventHandler(this.btn_bkfile_Click);
            // 
            // text_filebk
            // 
            this.text_filebk.Location = new System.Drawing.Point(16, 21);
            this.text_filebk.Name = "text_filebk";
            this.text_filebk.Size = new System.Drawing.Size(233, 21);
            this.text_filebk.TabIndex = 4;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(60, 235);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(56, 23);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(167, 235);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(62, 22);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // IsRoad_check
            // 
            this.IsRoad_check.AutoSize = true;
            this.IsRoad_check.Location = new System.Drawing.Point(209, 60);
            this.IsRoad_check.Name = "IsRoad_check";
            this.IsRoad_check.Size = new System.Drawing.Size(48, 16);
            this.IsRoad_check.TabIndex = 7;
            this.IsRoad_check.Text = "路段";
            this.IsRoad_check.UseVisualStyleBackColor = true;
            // 
            // mapName_tb
            // 
            this.mapName_tb.Location = new System.Drawing.Point(75, 22);
            this.mapName_tb.Name = "mapName_tb";
            this.mapName_tb.Size = new System.Drawing.Size(182, 21);
            this.mapName_tb.TabIndex = 4;
            // 
            // name_lb
            // 
            this.name_lb.AutoSize = true;
            this.name_lb.Location = new System.Drawing.Point(16, 22);
            this.name_lb.Name = "name_lb";
            this.name_lb.Size = new System.Drawing.Size(53, 12);
            this.name_lb.TabIndex = 3;
            this.name_lb.Text = "地图名称";
            // 
            // url_tb
            // 
            this.url_tb.Location = new System.Drawing.Point(75, 94);
            this.url_tb.Name = "url_tb";
            this.url_tb.Size = new System.Drawing.Size(203, 21);
            this.url_tb.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "地图url";
            // 
            // mapId_tb
            // 
            this.mapId_tb.Location = new System.Drawing.Point(75, 60);
            this.mapId_tb.Name = "mapId_tb";
            this.mapId_tb.Size = new System.Drawing.Size(96, 21);
            this.mapId_tb.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "地图id";
            // 
            // PicturePro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.mapId_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.url_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mapName_tb);
            this.Controls.Add(this.IsRoad_check);
            this.Controls.Add(this.name_lb);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.groupBox2);
            this.Name = "PicturePro";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "地图属性";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_bkfile;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.TextBox text_filebk;
        public System.Windows.Forms.CheckBox IsRoad_check;
        public System.Windows.Forms.TextBox mapName_tb;
        public System.Windows.Forms.Label name_lb;
        public System.Windows.Forms.TextBox url_tb;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox mapId_tb;
        public System.Windows.Forms.Label label1;
    }
}