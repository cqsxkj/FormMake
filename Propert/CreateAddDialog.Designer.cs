namespace WindowMake.Propert
{
    partial class CreateAddDialog
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
            this.cb_equtype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nd_equNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_nameFirst = new System.Windows.Forms.TextBox();
            this.tb_startNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_cfgnum = new System.Windows.Forms.TextBox();
            this.checkbox_way = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nd_equNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备类型：";
            // 
            // cb_equtype
            // 
            this.cb_equtype.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_equtype.FormattingEnabled = true;
            this.cb_equtype.Location = new System.Drawing.Point(125, 38);
            this.cb_equtype.Name = "cb_equtype";
            this.cb_equtype.Size = new System.Drawing.Size(255, 27);
            this.cb_equtype.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数  量：";
            // 
            // nd_equNum
            // 
            this.nd_equNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nd_equNum.Location = new System.Drawing.Point(125, 95);
            this.nd_equNum.Name = "nd_equNum";
            this.nd_equNum.Size = new System.Drawing.Size(120, 29);
            this.nd_equNum.TabIndex = 3;
            this.nd_equNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "命名前缀：";
            // 
            // tb_nameFirst
            // 
            this.tb_nameFirst.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_nameFirst.Location = new System.Drawing.Point(125, 158);
            this.tb_nameFirst.Name = "tb_nameFirst";
            this.tb_nameFirst.Size = new System.Drawing.Size(255, 29);
            this.tb_nameFirst.TabIndex = 5;
            // 
            // tb_startNum
            // 
            this.tb_startNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_startNum.Location = new System.Drawing.Point(125, 219);
            this.tb_startNum.Name = "tb_startNum";
            this.tb_startNum.Size = new System.Drawing.Size(120, 29);
            this.tb_startNum.TabIndex = 7;
            this.tb_startNum.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "起始编号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(221, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "火灾号组成：隧道号+设备类型+逻辑地址";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "(2表示手报，3表示温感、烟感，4表示光纤)";
            // 
            // tb_cfgnum
            // 
            this.tb_cfgnum.Location = new System.Drawing.Point(56, 349);
            this.tb_cfgnum.Name = "tb_cfgnum";
            this.tb_cfgnum.Size = new System.Drawing.Size(156, 21);
            this.tb_cfgnum.TabIndex = 19;
            this.tb_cfgnum.Text = "230001";
            // 
            // checkbox_way
            // 
            this.checkbox_way.AutoSize = true;
            this.checkbox_way.Checked = true;
            this.checkbox_way.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_way.Location = new System.Drawing.Point(243, 354);
            this.checkbox_way.Name = "checkbox_way";
            this.checkbox_way.Size = new System.Drawing.Size(96, 16);
            this.checkbox_way.TabIndex = 18;
            this.checkbox_way.Text = "随设备号递增";
            this.checkbox_way.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "火灾或紧急电话配置编号：";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(224, 379);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(68, 379);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 15;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // CreateAddDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 446);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_cfgnum);
            this.Controls.Add(this.checkbox_way);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tb_startNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_nameFirst);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nd_equNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_equtype);
            this.Controls.Add(this.label1);
            this.Name = "CreateAddDialog";
            this.Text = "CreateAddDialog";
            ((System.ComponentModel.ISupportInitialize)(this.nd_equNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cb_equtype;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown nd_equNum;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tb_nameFirst;
        public System.Windows.Forms.TextBox tb_startNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox tb_cfgnum;
        public System.Windows.Forms.CheckBox checkbox_way;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
    }
}