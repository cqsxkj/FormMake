/*描述：plc设备属性设置
 * 时间：2017-12-15 13:06:51
 * */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMake.Device;
using WindowMake.Entity;
using System.ComponentModel;
using WindowMake.DB;
using WindowMake.Config;
using System.Linq;

namespace WindowMake.Propert
{
    public partial class ObjectPro : Form
    {
        public PLCPropert m_pro;
        public string obj_id;
        public string obj_name;
        public MyObject m_obj;
        BindingList<Yx_cfg> Byxlist;
        BindingList<Yk_cfg> Byklist;
        BindingList<YCExt> Byclist;
        private ComboBox cmb_error = new ComboBox();
        public ObjectPro()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Int2Hex();
                m_obj.equ.EquName = this.tb_equname.Text;
                m_obj.equ.PileNo = this.tb_pileno.Text;
                if (this.Station1_Txt.Text.Length > 0)
                    m_obj.equ.plcStationAddress = this.Station1_Txt.Text;
                m_obj.equ.Note = this.tb_note.Text;
                m_obj.equ.FatherEquID = this.tb_fatherid.Text;
                m_obj.equ.AddressDiscribe = tb_address.Text;
                if (!string.IsNullOrEmpty(this.tb_ip.Text))
                {
                    m_obj.equ.IP = tb_ip.Text;
                }
                if (!string.IsNullOrEmpty(this.tb_port.Text))
                {
                    m_obj.equ.Port = Convert.ToInt32(tb_port.Text);
                }
                if (this.Vendor_Txt.Text.Length > 0)
                    m_obj.equ.Vendor = this.Vendor_Txt.Text;
                if (this.cb_RunMode.SelectedItem != null)
                    m_obj.equ.RunMode = this.cb_RunMode.SelectedItem.ToString();
                if (this.cb_activeUp.Checked)
                {
                    m_obj.equ.msgTimeoutSec = -1;
                }
                if (this.radio_all.Checked)
                    m_obj.equ.DirectionID = 3;
                else if (this.radio_down.Checked)
                    m_obj.equ.DirectionID = 2;
                else
                    m_obj.equ.DirectionID = 1;
                #region 属性

                #endregion
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        public void SetEP(ep_c_cfg ep)
        {
            tabPage2.CausesValidation = true;
            //Label label1 = new Label();
            //label1.Text = "电话号码:";
            //label1.Location = new System.Drawing.Point(4, 140);
            //label1.AutoSize = true;
            //this.tabPage1.Controls
        }
        //取消编辑按钮
        private void button2_Click(object sender, EventArgs e)
        {
            Int2Hex();
            DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 退出编辑10进制转换16进制
        /// </summary>
        private void Int2Hex()
        {
            if (m_obj is PLCEqu)
            {
                if (!checkBox1.Checked)
                {
                    for (int i = 0; i < Byxlist.Count; i++)
                    {
                        Byxlist[i].AddrAndBit = Int2Hex(Byxlist[i].AddrAndBit);
                    }
                    for (int i = 0; i < Byklist.Count; i++)
                    {
                        Byklist[i].AddrAndBit = Int2Hex(Byklist[i].AddrAndBit);
                    }
                    for (int i = 0; i < Byclist.Count; i++)
                    {
                        Byclist[i].AddrAndBit = Int2Hex(Byclist[i].AddrAndBit);
                    }
                }
            }
        }

        private void ObjectPro_Load(object sender, EventArgs e)
        {
            if (m_obj.equ != null)
            {
                #region 基础信息
                this.tb_equid.Text = m_obj.equ.EquID;
                this.tb_equname.Text = m_obj.equ.EquName;
                this.tb_pileno.Text = m_obj.equ.PileNo;
                this.pictureBox1.Image = m_obj.image;
                this.tb_fatherid.Text = m_obj.equ.FatherEquID;
                if (!string.IsNullOrEmpty(m_obj.equ.AddressDiscribe))
                {
                    tb_address.Text = m_obj.equ.AddressDiscribe;
                }
                if (!string.IsNullOrEmpty(m_obj.equ.plcStationAddress))
                    this.Station1_Txt.Text = m_obj.equ.plcStationAddress;
                if (!string.IsNullOrEmpty(m_obj.equ.Note))
                    this.tb_note.Text = m_obj.equ.Note;
                if (!string.IsNullOrEmpty(m_obj.equ.Vendor))
                    this.Vendor_Txt.Text = m_obj.equ.Vendor;
                this.cb_RunMode.SelectedItem = "server";
                if (!string.IsNullOrEmpty(m_obj.equ.RunMode))
                    this.cb_RunMode.SelectedItem = m_obj.equ.RunMode;
                if (!string.IsNullOrEmpty(m_obj.equ.IP))
                {
                    this.tb_ip.Text = m_obj.equ.IP;
                }
                if (m_obj.equ.Port != null)
                {
                    this.tb_port.Text = m_obj.equ.Port.ToString();
                }
                if (m_obj.equ.msgTimeoutSec == -1)//主动上传
                {
                    this.cb_activeUp.Checked = true;
                }
                else
                {
                    this.cb_activeUp.Checked = false;
                }
                switch (m_obj.equ.DirectionID)
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
                #endregion
                if (m_obj is PLCEqu)
                {
                    #region 分区
                    if (!string.IsNullOrEmpty(m_obj.equ.FatherEquID))
                    {
                        var areas = (from a in PlcString.p_area_cfg where a.equid == m_obj.equ.FatherEquID select a).ToList();
                        if (areas.Count > 0)
                        {
                            Dictionary<int, string> yxarea = new Dictionary<int, string>();
                            foreach (var item in areas)
                            {
                                yxarea.Add(item.id, item.point);
                            }
                            BindingSource yx = new BindingSource();
                            yx.DataSource = yxarea;
                            cb_yxarea.DataSource = yx;
                            cb_yxarea.ValueMember = "key";
                            cb_yxarea.DisplayMember = "value";
                            Dictionary<int, string> ykarea = new Dictionary<int, string>();
                            foreach (var item in areas)
                            {
                                ykarea.Add(item.id, item.point);
                            }
                            BindingSource yk = new BindingSource();
                            yk.DataSource = ykarea;
                            cb_ykarea.DataSource = yk;
                            cb_ykarea.ValueMember = "key";
                            cb_ykarea.DisplayMember = "value";
                            if (ykarea.Count > 1)
                            {
                                cb_ykarea.SelectedIndex = 1;
                            }
                            Dictionary<int, string> ycarea = new Dictionary<int, string>();
                            foreach (var item in areas)
                            {
                                ycarea.Add(item.id, item.point);
                            }
                            BindingSource yc = new BindingSource();
                            yc.DataSource = ycarea;
                            cb_ycarea.DataSource = yc;
                            cb_ycarea.ValueMember = "key";
                            cb_ycarea.DisplayMember = "value";
                            if (ycarea.Count > 2)
                            {
                                cb_ycarea.SelectedIndex = 2;
                            }
                        }
                    }
                    #endregion
                    Byxlist = new BindingList<Yx_cfg>((m_obj as PLCEqu).plc_pro.yxcfgList);
                    for (int i = 0; i < Byxlist.Count; i++)
                    {
                        Byxlist[i].AddrAndBit = Hex2Int(Byxlist[i].AddrAndBit);
                        cb_yxarea.SelectedValue = Byxlist[i].AreaID;
                    }
                    dataGridView1.DataSource = Byxlist;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[4].Width = 50;
                    dataGridView1.Columns[5].Visible = false;

                    Byklist = new BindingList<Yk_cfg>((m_obj as PLCEqu).plc_pro.ykcfgList);
                    for (int i = 0; i < Byklist.Count; i++)
                    {
                        Byklist[i].AddrAndBit = Hex2Int(Byklist[i].AddrAndBit);
                        cb_ykarea.SelectedValue = Byklist[i].AreaID;
                    }
                    dataGridView2.DataSource = Byklist;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    //dataGridView2.Columns[3].Visible = false;
                    dataGridView2.Columns[4].Visible = false;

                    Byclist = new BindingList<YCExt>((m_obj as PLCEqu).plc_pro.yclist);
                    for (int i = 0; i < Byclist.Count; i++)
                    {
                        Byclist[i].AddrAndBit = Hex2Int(Byclist[i].AddrAndBit);
                        cb_ycarea.SelectedValue = Byclist[i].AreaID;
                    }
                    dataGridView3.DataSource = Byclist;
                    dataGridView3.Columns[1].Visible = false;
                    dataGridView3.Columns[2].Visible = false;
                    dataGridView3.Columns[3].Visible = false;
                    dataGridView3.Columns[4].Visible = false;
                    dataGridView3.Columns[6].Visible = false;
                    dataGridView3.Columns[7].Visible = false;
                    dataGridView3.Columns[8].Visible = false;
                    dataGridView3.Columns[9].Visible = false;
                    dataGridView3.Columns[10].Visible = false;
                    dataGridView3.Columns[11].Visible = false;
                }
            }
        }

        private string Hex2Int(string addBit)
        {
            if (!string.IsNullOrEmpty(addBit))
            {
                string[] temp = addBit.Split('.');
                if (temp.Length > 1)
                {
                    return Convert.ToInt32(temp[0], 16) + "." + Convert.ToInt32(temp[1], 16);
                }
                else
                {
                    return Convert.ToInt32(temp[0], 16) + "";
                }
            }
            return null;
        }
        private string Int2Hex(string addBit)
        {
            if (!string.IsNullOrEmpty(addBit))
            {
                string[] temp = addBit.Split('.');
                if (temp.Length > 1)
                {
                    return Convert.ToInt32(temp[0]).ToString("X") + "." + Convert.ToInt32(temp[1]).ToString("X");
                }
                else
                {
                    return Convert.ToInt32(temp[0]).ToString("X");
                }
            }
            return null;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 勾选转换16进制，取消转换10进制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //IntORHex(groupBox1);//遥信
            //IntORHex(groupBox2);//遥控
            if (m_obj is PLCEqu)
            {

                for (int i = 0; i < (m_obj as PLCEqu).plc_pro.yxcfgList.Count; i++)
                {
                    if (checkBox1.Checked)
                    {
                        (m_obj as PLCEqu).plc_pro.yxcfgList[i].AddrAndBit = Int2Hex((m_obj as PLCEqu).plc_pro.yxcfgList[i].AddrAndBit);
                    }
                    else
                    {
                        (m_obj as PLCEqu).plc_pro.yxcfgList[i].AddrAndBit = Hex2Int((m_obj as PLCEqu).plc_pro.yxcfgList[i].AddrAndBit);
                    }
                }
                Byxlist = new BindingList<Yx_cfg>((m_obj as PLCEqu).plc_pro.yxcfgList);
                dataGridView1.DataSource = Byxlist;
                for (int i = 0; i < (m_obj as PLCEqu).plc_pro.ykcfgList.Count; i++)
                {
                    if (checkBox1.Checked)
                    {
                        (m_obj as PLCEqu).plc_pro.ykcfgList[i].AddrAndBit = Int2Hex((m_obj as PLCEqu).plc_pro.ykcfgList[i].AddrAndBit);
                    }
                    else
                    {
                        (m_obj as PLCEqu).plc_pro.ykcfgList[i].AddrAndBit = Hex2Int((m_obj as PLCEqu).plc_pro.ykcfgList[i].AddrAndBit);
                    }
                }
                Byklist = new BindingList<Yk_cfg>((m_obj as PLCEqu).plc_pro.ykcfgList);
                dataGridView2.DataSource = Byklist;
                for (int i = 0; i < (m_obj as PLCEqu).plc_pro.yclist.Count; i++)
                {
                    if (checkBox1.Checked)
                    {
                        (m_obj as PLCEqu).plc_pro.yclist[i].AddrAndBit = Int2Hex((m_obj as PLCEqu).plc_pro.yclist[i].AddrAndBit);
                    }
                    else
                    {
                        (m_obj as PLCEqu).plc_pro.yclist[i].AddrAndBit = Hex2Int((m_obj as PLCEqu).plc_pro.yclist[i].AddrAndBit);
                    }
                }
                Byclist = new BindingList<YCExt>((m_obj as PLCEqu).plc_pro.yclist);
                dataGridView3.DataSource = Byclist;
            }
        }
        #region 点位操作
        //删除遥信点位
        private void bt_yxdelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    var yx = r.DataBoundItem as Yx_cfg;
                    DBHelper.ExcuteTransactionSql(string.Format("delete from yx_cfg where id={0};", yx.ID));
                    this.Byxlist.RemoveAt(r.Index); //删除行
                }
            }
        }
        //增加遥信点位
        private void bt_yxadd_Click(object sender, EventArgs e)
        {
            Log.WriteLog("新增遥信");
            try
            {
                if ((m_obj.equtype.ToString()).StartsWith("P_"))
                {
                    Yx_cfg yxcfg = new Yx_cfg();
                    yxcfg.IsError = 0;
                    yxcfg.AreaID = (int)cb_yxarea.SelectedValue;
                    yxcfg.Order = Byxlist.Count;
                    yxcfg.EquID = m_obj.equ.EquID;
                    string sql = InsertYXcfg(yxcfg);
                    Log.WriteLog("新增遥信语句生成完成");
                    int i = DBHelper.ExcuteSql(sql);
                    Log.WriteLog("新增遥信执行完毕");
                    if (i > -1)
                    {
                        yxcfg.ID = i;
                        Byxlist.Add(yxcfg);
                    }
                    Log.WriteLog("新增遥信并添加到列表完毕");
                }
            }
            catch (Exception ee)
            {
                Log.WriteLog("新增遥信错误：" + ee.Message);
            }
        }
        /// <summary>
        /// 生成yx_cfg插入语句
        /// </summary>
        /// <param name="yxcfg"></param>
        /// <returns></returns>
        private string InsertYXcfg(Yx_cfg yxcfg)
        {
            return string.Format("insert into yx_cfg(equid,isError,`order`,areaId)values('{0}',{1},{2},{3});SELECT @@Identity", yxcfg.EquID, yxcfg.IsError, yxcfg.Order, cb_yxarea.SelectedValue);
        }

        /// <summary>
        /// 新增遥控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ykadd_Click(object sender, EventArgs e)
        {
            Log.WriteLog("新增遥控");
            try
            {
                if ((m_obj.equtype.ToString()).StartsWith("P_"))
                {
                    Yk_cfg ykcfg = new Yk_cfg();
                    ykcfg.AreaID = (int)cb_ykarea.SelectedValue;
                    ykcfg.Order = Byklist.Count;
                    ykcfg.EquID = m_obj.equ.EquID;
                    DBOPs ds = new DBOPs();
                    int i = ds.InsertYKConfig(ykcfg);
                    Log.WriteLog("新增遥控执行完毕");
                    if (i > -1)
                    {
                        ykcfg.ID = i;
                        Byklist.Add(ykcfg);
                    }
                    Log.WriteLog("新增遥控并添加到列表完毕");
                }
            }
            catch (Exception ee)
            {
                Log.WriteLog("新增遥控错误：" + ee.Message);
            }
        }
        /// <summary>
        /// 删除遥控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ykdelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView2.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    var yk = r.DataBoundItem as Yk_cfg;
                    int i = DBHelper.ExcuteTransactionSql(string.Format("delete from yk_cfg where id={0}", yk.ID));
                    if (i > 0)
                    {
                        this.Byklist.RemoveAt(r.Index); //删除行
                    }
                }
            }
        }
        //遥信字符串编辑
        private void bt_yxStr_Click(object sender, EventArgs e)
        {

        }
        //遥控字符串编辑
        private void bt_yk_Click(object sender, EventArgs e)
        {

        }
        //修改遥信点位
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (cb_yxarea.SelectedItem == null)
                {
                    MessageBox.Show("请选择分区");
                    return;
                }
                Yx_cfg yxcfg = dataGridView1.CurrentRow.DataBoundItem as Yx_cfg;

                string sql = string.Format("update yx_cfg set addrandbit='{0}',IsError={1},`order`='{2}',areaid={3} where(id={4})", yxcfg.AddrAndBit, yxcfg.IsError, yxcfg.Order, cb_yxarea.SelectedValue, yxcfg.ID);
                if (!checkBox1.Checked)
                {
                    sql = string.Format("update yx_cfg set addrandbit='{0}',IsError={1},`order`='{2}',areaid={3} where(id={4})", Int2Hex(yxcfg.AddrAndBit), yxcfg.IsError, yxcfg.Order, cb_yxarea.SelectedValue, yxcfg.ID);
                }
                int i = DBHelper.ExcuteTransactionSql(sql);

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }
        //修改遥控点位
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (cb_ykarea.SelectedItem == null)
                {
                    MessageBox.Show("请选择分区");
                    return;
                }
                Yk_cfg ykcfg = dataGridView2.CurrentRow.DataBoundItem as Yk_cfg;
                string sql = string.Format("update yk_cfg set addrandbit='{0}',`order`='{1}',areaid={2} where(id={3})", ykcfg.AddrAndBit, ykcfg.Order, cb_ykarea.SelectedValue, ykcfg.ID);
                if (!checkBox1.Checked)
                {
                    sql = string.Format("update yk_cfg set addrandbit='{0}',`order`='{1}',areaid={2} where(id={3})", Int2Hex(ykcfg.AddrAndBit), ykcfg.Order, cb_ykarea.SelectedValue, ykcfg.ID);
                }
                int i = DBHelper.ExcuteTransactionSql(sql);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }
        //修改遥测点位
        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (cb_ycarea.SelectedItem == null)
                {
                    MessageBox.Show("请选择分区");
                    return;
                }
                YCExt ycext = dataGridView3.CurrentRow.DataBoundItem as YCExt;
                string sql = string.Format("update yc set ycfun={0} where(ycid={1})", ycext.YCFun, ycext.YCID);
                int i = DBHelper.ExcuteTransactionSql(sql);
                if (i > 0)
                {
                    string sql1 = string.Format("update yc_cfg set addrandbit='{0}',areaid={1},`order`={2} where(id={3})", ycext.AddrAndBit, ycext.AreaID, ycext.Order, ycext.ID); ;
                    if (!checkBox1.Checked)
                    {
                        sql1 = string.Format("update yc_cfg set addrandbit='{0}',areaid={1},`order`={2} where(id={3})", Int2Hex(ycext.AddrAndBit), ycext.AreaID, ycext.Order, ycext.ID);
                    }
                    int j = DBHelper.ExcuteTransactionSql(sql1);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }
        //遥测新增
        private void tb_ycadd_Click(object sender, EventArgs e)
        {
            if ((m_obj.equtype.ToString()).StartsWith("P_"))
            {
                if (cb_ycarea.SelectedItem == null)
                {
                    MessageBox.Show("请选择分区");
                    return;
                }
                YCExt yccfg = new YCExt();
                yccfg.AreaID = (int)cb_ykarea.SelectedValue;
                yccfg.Order = Byklist.Count;
                yccfg.EquID = m_obj.equ.EquID;
                string sql = string.Format("insert into yc(equid,ycfield)values('{0}','{1}');SELECT @@Identity", yccfg.EquID, m_obj.equ.EquTypeID);
                int i = DBHelper.ExcuteSql(sql);
                if (i > -1)
                {
                    yccfg.YCID = i;

                    string sql1 = string.Format("insert into yc_cfg(equid,`order`,areaid,ycid)values('{0}',{1},{2},{3});SELECT @@Identity", yccfg.EquID, Byclist.Count, yccfg.AreaID, yccfg.YCID);
                    int j = DBHelper.ExcuteSql(sql1);
                    if (j > -1)
                    {
                        yccfg.ID = i;
                        Byclist.Add(yccfg);
                    }
                    else
                    {
                        DBHelper.ExcuteTransactionSql("delete from yc where YCID=" + i);
                    }
                }
            }
        }
        //遥测删除
        private void bt_ycdelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView3.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    var ycExt = r.DataBoundItem as YCExt;
                    int i = DBHelper.ExcuteTransactionSql(string.Format("delete from yc where YCID={0}", ycExt.YCID));
                    if (i > -1)
                    {
                        i = DBHelper.ExcuteTransactionSql(string.Format("delete from yc_cfg where id={0}", ycExt.ID));
                        if (i > -1)
                        {
                            this.Byklist.RemoveAt(r.Index); //删除行
                        }
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 窗口关闭控制
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //Windows系统消息，winuser.h文件中有WM_...的定义
            //十六进制数字，0x是前导符后面是真正的数字
            const int WM_SYSCOMMAND = 0x0112;
            //winuser.h文件中有SC_...的定义
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && ((int)m.WParam == SC_CLOSE))
            {
                Int2Hex();
                //return;//阻止了窗体关闭
            }
            base.WndProc(ref m);
        }
    }
}
