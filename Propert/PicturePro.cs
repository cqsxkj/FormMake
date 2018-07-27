using System;
using System.Windows.Forms;

namespace WindowMake.Propert
{
    public partial class PicturePro : Form
    {
        public PicturePro()
        {
            InitializeComponent();
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        

        private void btn_bkfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdialog = new OpenFileDialog();
            fdialog.InitialDirectory = "./BK/";
            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                text_filebk.Text = fdialog.FileName;
            }
        }
    }
}
