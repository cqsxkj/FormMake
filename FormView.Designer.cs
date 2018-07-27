namespace WindowMake
{
    partial class FormView
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.objectPro = new System.Windows.Forms.ToolStripMenuItem();
            this.pictruePro = new System.Windows.Forms.ToolStripMenuItem();
            this.toTop = new System.Windows.Forms.ToolStripMenuItem();
            this.toBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.on = new System.Windows.Forms.ToolStripMenuItem();
            this.under = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new WindowMake.MyPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectPro,
            this.pictruePro});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            //this.contextMenuStrip1.VisibleChanged += new System.EventHandler(this.contextMenuStrip1_VisibleChanged);
            // 
            // objectPro
            // 
            this.objectPro.Name = "objectPro";
            this.objectPro.Size = new System.Drawing.Size(124, 22);
            this.objectPro.Text = "对象属性";
            // 
            // pictruePro
            // 
            this.pictruePro.Name = "pictruePro";
            this.pictruePro.Size = new System.Drawing.Size(124, 22);
            this.pictruePro.Text = "地图属性";
            // 
            // toTop
            // 
            this.toTop.Name = "toTop";
            this.toTop.Size = new System.Drawing.Size(32, 19);
            // 
            // toBottom
            // 
            this.toBottom.Name = "toBottom";
            this.toBottom.Size = new System.Drawing.Size(32, 19);
            // 
            // on
            // 
            this.on.Name = "on";
            this.on.Size = new System.Drawing.Size(32, 19);
            // 
            // under
            // 
            this.under.Name = "under";
            this.under.Size = new System.Drawing.Size(32, 19);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MapScale = 0.7D;
            this.panel1.Name = "panel1";
            this.panel1.ScaleChanging = false;
            this.panel1.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.TabIndex = 2;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.SelectChanged += new System.EventHandler<WindowMake.SelectEventArgs>(this.panel1_ObjectSelectChanged);
            this.panel1.DoubleClick += new System.EventHandler(this.context_DoubleClicked);
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1924, 1062);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "FormView";
            this.Text = "FormView";
            this.MdiChildActivate += new System.EventHandler(this.FormView_MdiChildActivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pictruePro;
        private MyPanel panel1;
        private System.Windows.Forms.ToolStripMenuItem objectPro;
        //private System.Windows.Forms.ToolStripMenuItem Sendto;
        private System.Windows.Forms.ToolStripMenuItem toTop;
        private System.Windows.Forms.ToolStripMenuItem toBottom;
        private System.Windows.Forms.ToolStripMenuItem on;
        private System.Windows.Forms.ToolStripMenuItem under;
    }
}