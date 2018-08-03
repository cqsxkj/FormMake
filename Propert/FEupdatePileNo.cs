using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowMake.Propert
{
    public partial class FEupdatePileNo : Form
    {
        public FEupdatePileNo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 是否按X坐标递增
        /// </summary>
        public bool IsAdd { get { return checkBox1.Checked; } }

        /// <summary>
        /// 获取Left桩号文本框中的内容
        /// </summary>
        public string LeftPileNo
        {
            get
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    return textBox1.Text;
                }
                return "";
            }
        }

        /// <summary>
        /// 获取Right桩号文本框中的内容
        /// </summary>
        public string RightPileNo
        {
            get
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    return textBox2.Text;
                }
                return "";
            }
        }

        /// <summary>
        /// 获取设备距离本框中的内容
        /// </summary>
        public string EquDistance
        {
            get
            {
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    return textBox3.Text;
                }
                return "";
            }
        }

        /// <summary>
        /// 后台提示显示到文本框中
        /// </summary>
        /// <param name="str"></param>
        public void EquTypeSetShow(String str)
        {
            lb_equType.Text = str;
        }

        /// <summary>
        /// 后台提示显示到文本框中
        /// </summary>
        /// <param name="str"></param>
        public void DirectionSetShow(String str)
        {
            lb_direction.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
