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
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            // FormView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "FormView";
            this.ShowIcon = false;
            this.Text = "FormView";
            this.MdiChildActivate += new System.EventHandler(this.FormView_MdiChildActivate);
            this.DoubleClick += new System.EventHandler(this.FormView_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormView_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormView_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormView_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pictruePro;
        private System.Windows.Forms.ToolStripMenuItem objectPro;
    }
}