/* 描述：向已存在地图中写入数据
 *       包含对地图大小的设定，写入设备类型的限定
 *  时间：2018年3月9日15:56:38
 *  作者：阿里木客
 * */
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMake.DB;
using WindowMake.Entity;

namespace WindowMake
{
    public partial class ReadInit : Form
    {
        public ReadInit()
        {
            InitializeComponent();
        }

        private void Bind()
        {
            try
            {
                IList<Map> maps = DBHelper.Query<Map>("select * from map");
                comdrop_url.DataSource = maps;
                comdrop_url.DisplayMember = "MapName";
                comdrop_url.ValueMember = "MapID";
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 地图高度
        /// </summary>
        public int MapHeight
        {
            get { return int.Parse(tb_height.Text); }
        }

        /// <summary>
        /// 地图宽度
        /// </summary>
        public int MapWidth
        {
            get { return int.Parse(tb_width.Text); }
        }

        public string MapName
        {
            get { return comdrop_url.DisplayMember; }
        }

        public ReadEquType IsCreate
        {
            get
            {
                return new ReadEquType
                {
                    CMS = cb_cms.Checked,
                    CO = cb_co.Checked,
                    EP = cb_ep.Checked,
                    FIRE = cb_fire.Checked,
                    GJ = cb_gj.Checked,
                    LIGHT = cb_light.Checked,
                    LLID = cb_llid.Checked,
                    P_HL = cb_hl.Checked,
                    P_JF = cb_jf.Checked,
                    P_P = cb_p.Checked,
                    P_TL = cb_tl.Checked,
                    P_TL2 = cb_tl2.Checked,
                    P_VI = cb_pvi.Checked,
                    S = cb_s.Checked,
                    TD = cb_td.Checked,
                    TV = cb_tv.Checked,
                    TW = cb_tw.Checked,
                    VC = cb_vc.Checked,
                    VI = cb_vi.Checked
                };
            }
        }

        /// <summary>
        /// 地图ID
        /// </summary>
        public string GetMapID
        {
            get { return comdrop_url.SelectedValue == null ? null : comdrop_url.SelectedValue.ToString(); }
        }

        private void bt_ok_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void ReadInit_Load(object sender, System.EventArgs e)
        {
            try
            {
                IList<Map> maps = DBHelper.Query<Map>("select * from map");
                comdrop_url.DataSource = maps;
                comdrop_url.DisplayMember = "MapName";
                comdrop_url.ValueMember = "MapID";
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void cb_newMap_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cb_newMap.Checked==true)
            {
                comdrop_url.Visible = false;
            }
            else
            {
                comdrop_url.Visible = true;
            }
        }
    }
}
