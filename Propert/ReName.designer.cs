namespace WindowMake.Propert
{
    partial class ReName
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
            this.num_count = new System.Windows.Forms.NumericUpDown();
            this.bt_update = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tb_desName = new System.Windows.Forms.TextBox();
            this.lb_equtype = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_count)).BeginInit();
            this.SuspendLayout();
            // 
            // num_count
            // 
            this.num_count.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_count.Location = new System.Drawing.Point(478, 147);
            this.num_count.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_count.Name = "num_count";
            this.num_count.Size = new System.Drawing.Size(72, 31);
            this.num_count.TabIndex = 17;
            this.num_count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bt_update
            // 
            this.bt_update.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_update.Location = new System.Drawing.Point(233, 288);
            this.bt_update.Name = "bt_update";
            this.bt_update.Size = new System.Drawing.Size(75, 29);
            this.bt_update.TabIndex = 16;
            this.bt_update.Text = "修改";
            this.bt_update.UseVisualStyleBackColor = true;
            this.bt_update.Click += new System.EventHandler(this.bt_update_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(435, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "+";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(103, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "说明：只能重命名同类型设备";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(119, 223);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "X坐标递增方向修改";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tb_desName
            // 
            this.tb_desName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_desName.Location = new System.Drawing.Point(38, 148);
            this.tb_desName.Name = "tb_desName";
            this.tb_desName.Size = new System.Drawing.Size(377, 29);
            this.tb_desName.TabIndex = 12;
            // 
            // lb_equtype
            // 
            this.lb_equtype.AutoSize = true;
            this.lb_equtype.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_equtype.Location = new System.Drawing.Point(257, 44);
            this.lb_equtype.Name = "lb_equtype";
            this.lb_equtype.Size = new System.Drawing.Size(0, 20);
            this.lb_equtype.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(117, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "设备类型：";
            // 
            // ReName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 368);
            this.Controls.Add(this.num_count);
            this.Controls.Add(this.bt_update);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tb_desName);
            this.Controls.Add(this.lb_equtype);
            this.Controls.Add(this.label1);
            this.Name = "ReName";
            this.Text = "ReName";
            ((System.ComponentModel.ISupportInitialize)(this.num_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown num_count;
        private System.Windows.Forms.Button bt_update;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tb_desName;
        public System.Windows.Forms.Label lb_equtype;
        private System.Windows.Forms.Label label1;
    }
}