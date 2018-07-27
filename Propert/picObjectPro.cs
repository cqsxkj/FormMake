using System;
using System.Windows.Forms;

namespace WindowMake.Propert
{
    public partial class picObjectPro : Form
    {
        public string m_filename;
        public picObjectPro()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdialog = new OpenFileDialog();
            fdialog.InitialDirectory = System.Windows.Forms.Application.StartupPath+"\\bitmap\\";
            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                m_filename = fdialog.SafeFileName;
                this.pictureBox1.ImageLocation = System.Windows.Forms.Application.StartupPath + "\\bitmap\\" + m_filename;
            }
        }

        private void picObjectPro_Load(object sender, EventArgs e)
        {
            this.pictureBox1.ImageLocation = System.Windows.Forms.Application.StartupPath + "\\bitmap\\" + m_filename;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
