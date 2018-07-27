namespace WindowMake.Propert
{
    partial class PioCreate
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_cmd = new System.Windows.Forms.CheckBox();
            this.cb_vc = new System.Windows.Forms.CheckBox();
            this.cb_tl = new System.Windows.Forms.CheckBox();
            this.vb_ep = new System.Windows.Forms.CheckBox();
            this.cb_sb = new System.Windows.Forms.CheckBox();
            this.cb_fl = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(89, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 29);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "K101+200";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(403, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(101, 29);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "K105+200";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_fl);
            this.groupBox1.Controls.Add(this.cb_sb);
            this.groupBox1.Controls.Add(this.vb_ep);
            this.groupBox1.Controls.Add(this.cb_tl);
            this.groupBox1.Controls.Add(this.cb_vc);
            this.groupBox1.Controls.Add(this.cb_cmd);
            this.groupBox1.Location = new System.Drawing.Point(39, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 128);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置桩号";
            // 
            // cb_cmd
            // 
            this.cb_cmd.AutoSize = true;
            this.cb_cmd.Location = new System.Drawing.Point(25, 29);
            this.cb_cmd.Name = "cb_cmd";
            this.cb_cmd.Size = new System.Drawing.Size(60, 16);
            this.cb_cmd.TabIndex = 0;
            this.cb_cmd.Text = "情报板";
            this.cb_cmd.UseVisualStyleBackColor = true;
            // 
            // cb_vc
            // 
            this.cb_vc.AutoSize = true;
            this.cb_vc.Location = new System.Drawing.Point(105, 29);
            this.cb_vc.Name = "cb_vc";
            this.cb_vc.Size = new System.Drawing.Size(60, 16);
            this.cb_vc.TabIndex = 1;
            this.cb_vc.Text = "车检器";
            this.cb_vc.UseVisualStyleBackColor = true;
            // 
            // cb_tl
            // 
            this.cb_tl.AutoSize = true;
            this.cb_tl.Location = new System.Drawing.Point(210, 29);
            this.cb_tl.Name = "cb_tl";
            this.cb_tl.Size = new System.Drawing.Size(72, 16);
            this.cb_tl.TabIndex = 2;
            this.cb_tl.Text = "交通控制";
            this.cb_tl.UseVisualStyleBackColor = true;
            // 
            // vb_ep
            // 
            this.vb_ep.AutoSize = true;
            this.vb_ep.Location = new System.Drawing.Point(312, 29);
            this.vb_ep.Name = "vb_ep";
            this.vb_ep.Size = new System.Drawing.Size(72, 16);
            this.vb_ep.TabIndex = 3;
            this.vb_ep.Text = "紧急电话";
            this.vb_ep.UseVisualStyleBackColor = true;
            // 
            // cb_sb
            // 
            this.cb_sb.AutoSize = true;
            this.cb_sb.Location = new System.Drawing.Point(25, 74);
            this.cb_sb.Name = "cb_sb";
            this.cb_sb.Size = new System.Drawing.Size(48, 16);
            this.cb_sb.TabIndex = 4;
            this.cb_sb.Text = "手报";
            this.cb_sb.UseVisualStyleBackColor = true;
            // 
            // cb_fl
            // 
            this.cb_fl.AutoSize = true;
            this.cb_fl.Location = new System.Drawing.Point(105, 74);
            this.cb_fl.Name = "cb_fl";
            this.cb_fl.Size = new System.Drawing.Size(48, 16);
            this.cb_fl.TabIndex = 5;
            this.cb_fl.Text = "光纤";
            this.cb_fl.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PioCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 298);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "PioCreate";
            this.Text = "PioCreate";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_fl;
        private System.Windows.Forms.CheckBox cb_sb;
        private System.Windows.Forms.CheckBox vb_ep;
        private System.Windows.Forms.CheckBox cb_tl;
        private System.Windows.Forms.CheckBox cb_vc;
        private System.Windows.Forms.CheckBox cb_cmd;
        private System.Windows.Forms.Button button1;
    }
}