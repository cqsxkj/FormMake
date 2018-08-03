namespace WindowMake.Propert
{
    partial class AddTrigger2Group
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
            this.bt_save = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_triggerDelete = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(223, 387);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 15;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(49, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(429, 29);
            this.comboBox1.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_triggerDelete);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new System.Drawing.Point(63, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 305);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "触发设备";
            // 
            // bt_triggerDelete
            // 
            this.bt_triggerDelete.Location = new System.Drawing.Point(351, 38);
            this.bt_triggerDelete.Name = "bt_triggerDelete";
            this.bt_triggerDelete.Size = new System.Drawing.Size(58, 23);
            this.bt_triggerDelete.TabIndex = 1;
            this.bt_triggerDelete.Text = "移除";
            this.bt_triggerDelete.UseVisualStyleBackColor = true;
            this.bt_triggerDelete.Click += new System.EventHandler(this.bt_triggerDelete_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(21, 20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(318, 272);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // AddTrigger2Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 446);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox2);
            this.MinimizeBox = false;
            this.Name = "AddTrigger2Group";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddTrigger2Group";
            this.TopMost = true;
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_triggerDelete;
        private System.Windows.Forms.ListView listView1;
    }
}