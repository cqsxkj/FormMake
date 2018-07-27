using System;
using System.Windows.Forms;


namespace WindowMake.Propert
{
    public partial class ReName : Form
    {
        public ReName()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否按X坐标递增
        /// </summary>
        public bool IsAdd { get { return checkBox1.Checked; } }
     
        /// <summary>
        /// 获取文本框中的内容
        /// </summary>
        public string NameStr
        {
            get
            {
                if (!string.IsNullOrEmpty(tb_desName.Text))
                {
                    return tb_desName.Text;
                }
                return "";
            }
        }

        /// <summary>
        /// 后台提示显示到文本框中
        /// </summary>
        /// <param name="str"></param>
        public void SetShow(String str)
        {
            tb_desName.Text = str;
        }

       /// <summary>
       /// 获取数字输入框中的数字
       /// </summary>
        public int NameCount { get { return (int)num_count.Value; } }

        private void bt_update_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.OK;
            //this.Close(); 
            this.Hide();
        }
    }
}
