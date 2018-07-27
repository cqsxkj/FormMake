using System;
using System.Windows.Forms;

namespace WindowMake.Propert
{
    public partial class ObjectBase : Form
    {
        public ObjectBase()
        {
            InitializeComponent();
        }

        public string FartherID
        {
            get { return tb_fartherid.Text; }
            set { tb_fartherid.Text = value; }
        }
        public bool AlertFartherid { get; set; }
        /// <summary>
        /// 上下行
        /// </summary>
        public int Direction
        {
            get
            {
                if (this.radio_all.Checked)
                    return 3;
                else if (this.radio_down.Checked)
                    return 2;
                else
                    return 1;
            }
            set
            {
                switch (value)
                {
                    case 1://上行
                        radio_up.Checked = true;
                        radio_down.Checked = false;
                        radio_all.Checked = false;
                        break;
                    case 2://下行
                        radio_up.Checked = false;
                        radio_down.Checked = true;
                        radio_all.Checked = false;
                        break;
                    default://双向
                        radio_up.Checked = false;
                        radio_down.Checked = false;
                        radio_all.Checked = true;
                        break;
                }
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
