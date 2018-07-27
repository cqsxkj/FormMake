namespace WindowMake.Propert
{
    partial class ObjectBase
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_fartherid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_OK = new System.Windows.Forms.Button();
            this.radio_all = new System.Windows.Forms.RadioButton();
            this.radio_down = new System.Windows.Forms.RadioButton();
            this.radio_up = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(67, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "父设备：";
            // 
            // tb_fartherid
            // 
            this.tb_fartherid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_fartherid.Location = new System.Drawing.Point(129, 19);
            this.tb_fartherid.Name = "tb_fartherid";
            this.tb_fartherid.Size = new System.Drawing.Size(184, 26);
            this.tb_fartherid.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(67, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "上下行：";
            // 
            // bt_OK
            // 
            this.bt_OK.Location = new System.Drawing.Point(152, 181);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(75, 23);
            this.bt_OK.TabIndex = 4;
            this.bt_OK.Text = "确定";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // radio_all
            // 
            this.radio_all.AutoSize = true;
            this.radio_all.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio_all.Location = new System.Drawing.Point(152, 132);
            this.radio_all.Name = "radio_all";
            this.radio_all.Size = new System.Drawing.Size(65, 23);
            this.radio_all.TabIndex = 46;
            this.radio_all.Text = "双向";
            this.radio_all.UseVisualStyleBackColor = true;
            // 
            // radio_down
            // 
            this.radio_down.AutoSize = true;
            this.radio_down.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio_down.Location = new System.Drawing.Point(152, 107);
            this.radio_down.Name = "radio_down";
            this.radio_down.Size = new System.Drawing.Size(65, 23);
            this.radio_down.TabIndex = 45;
            this.radio_down.Text = "下行";
            this.radio_down.UseVisualStyleBackColor = true;
            // 
            // radio_up
            // 
            this.radio_up.AutoSize = true;
            this.radio_up.Checked = true;
            this.radio_up.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio_up.Location = new System.Drawing.Point(152, 83);
            this.radio_up.Name = "radio_up";
            this.radio_up.Size = new System.Drawing.Size(65, 23);
            this.radio_up.TabIndex = 44;
            this.radio_up.TabStop = true;
            this.radio_up.Text = "上行";
            this.radio_up.UseVisualStyleBackColor = true;
            // 
            // ObjectBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 256);
            this.Controls.Add(this.radio_all);
            this.Controls.Add(this.radio_down);
            this.Controls.Add(this.radio_up);
            this.Controls.Add(this.bt_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_fartherid);
            this.Controls.Add(this.label1);
            this.Name = "ObjectBase";
            this.Text = "ObjectBase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_fartherid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.RadioButton radio_all;
        private System.Windows.Forms.RadioButton radio_down;
        private System.Windows.Forms.RadioButton radio_up;
    }
}