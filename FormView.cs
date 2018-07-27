using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;
using WindowMake.Propert;

namespace WindowMake
{
    public partial class FormView : Form
    {
        public MyObject m_pCurrentPic = null;
        public string fileName = "";
        private CreateAddDialog createAddDialog = new CreateAddDialog();
        private ObjectBase objBase = new ObjectBase();
        private ReName reName = new ReName();
        public FormView()
        {
            InitializeComponent();
        }
        private void FormView_MdiChildActivate(object sender, EventArgs e)
        {
            gMain.CurrentPictrueBox = m_pCurrentPic;
        }

        /// <summary>
        /// 双击，没有选中双击批量生成设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.panel1.m_pCurrentObject == null)
                {
                    #region create object
                    createAddDialog.StartPosition = FormStartPosition.CenterParent;
                    if (createAddDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        MyObject.ObjectType obj = (MyObject.ObjectType)createAddDialog.cb_equtype.SelectedValue;
                        int count = (int)createAddDialog.nd_equNum.Value;//需要生成对象的数量
                        int startNum = 1;
                        int.TryParse(createAddDialog.tb_startNum.Text, out startNum);
                        int cfgNum = 0;
                        try
                        {
                            cfgNum = int.Parse(createAddDialog.tb_cfgnum.Text);
                        }
                        catch (Exception)
                        {
                            Log.WriteLog("配置号码格式不正确");
                            createAddDialog.Hide();
                        }
                        DBOPs db = new DBOPs();
                        int parentWith = this.panel1.BackgroundImage.Size.Width;
                        for (int i = 0; i < count; i++)
                        {
                            var lacation = ((MouseEventArgs)e).Location;
                            int x = (int)(parentWith - 2 * lacation.X) / (count - 1) * i + lacation.X;
                            lacation = new System.Drawing.Point { X = x, Y = lacation.Y };
                            MyObject myObject = panel1.DrawObject(obj.ToString(), lacation);
                            if (createAddDialog.checkbox_way.Checked)
                            {
                                myObject.equ.EquName = createAddDialog.tb_nameFirst.Text + (startNum++);
                            }
                            else
                            {
                                myObject.equ.EquName = createAddDialog.tb_nameFirst.Text + (startNum--);
                            }
                            if (obj == MyObject.ObjectType.EP_T)
                            {
                                ep_c_cfg ep = new ep_c_cfg();
                                ep.EquID = myObject.equ.EquID;
                                ep.Mesg = myObject.equ.EquName;
                                if (createAddDialog.checkbox_way.Checked)
                                {
                                    ep.EPNum = (cfgNum++).ToString(); ;
                                }
                                else
                                {
                                    ep.EPNum = (cfgNum--).ToString();
                                }
                                db.InsertEp(ep);
                            }
                            else if (obj == MyObject.ObjectType.F_L || obj == MyObject.ObjectType.F_SB || obj == MyObject.ObjectType.F_YG)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    SetObjectPro();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }
        /// <summary>
        /// 设置地图属性
        /// </summary>
        private void SetMapPro()
        {
            PicturePro mapinfo = new PicturePro();
            mapinfo.mapId_tb.Text = panel1.mapPro.MapID;
            mapinfo.mapName_tb.Text = panel1.mapPro.MapName;
            mapinfo.IsRoad_check.Checked = panel1.mapPro.IsRoad == 1;
            mapinfo.url_tb.Text = panel1.mapPro.MapAddress;
            mapinfo.text_filebk.Text = @"BK/" + panel1.mapPro.MapName + ".png";
            if (mapinfo.ShowDialog() == DialogResult.OK)
            {
                this.panel1.mapPro.MapName = mapinfo.mapName_tb.Text;
                this.panel1.mapPro.IsRoad = mapinfo.IsRoad_check.Checked == true ? 1 : 0;
                this.panel1.mapPro.MapAddress = mapinfo.url_tb.Text;
                this.panel1.mapPro.MapID = mapinfo.mapId_tb.Text;
                this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
                if (!string.IsNullOrEmpty(mapinfo.text_filebk.Text))
                {
                    this.panel1.SetBackgroud(mapinfo.text_filebk.Text);
                }
                else
                    this.panel1.BackgroundImage = null;
            }
            mapinfo.Close();
        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "pictruePro":
                    SetMapPro();
                    break;
                case "objectPro":
                    SetMapPro();
                    break;
                case "rename":
                    MyObject.ObjectType equtype = MyObject.ObjectType.UnKnow;
                    List<MyObject> reNameEquList = new List<MyObject>();
                    for (int i = 0; i < panel1.m_ObjectList.Count; i++)
                    {
                        if (panel1.m_ObjectList[i].obj_bSelect)
                        {
                            if (MyObject.ObjectType.UnKnow == equtype)
                            {
                                equtype = panel1.m_ObjectList[i].equtype;

                            }
                            if (panel1.m_ObjectList[i].equtype == equtype)
                            {

                                reNameEquList.Add(panel1.m_ObjectList[i]);

                            }
                        }
                    }
                    ReNameSameTypeObj(reNameEquList);
                    break;
                default:
                    break;
            }
            //if (this.panel1.m_pCurrentObject == null)
            //{
            //    SetMapPro();
            //}
            //else
            //{
            //    SetObjectPro();
            //}
        }
        /// <summary>
        /// 修改同类型设备名称
        /// </summary>
        /// <param name="reNameEquList"></param>
        private void ReNameSameTypeObj(List<MyObject> reNameEquList)
        {
            string directiongStr ="";
            try
            {
                if (reNameEquList.Count > 0)
                {
                    if (string.IsNullOrEmpty(reName.NameStr))   //输入框中显示选中设备所在隧道和方向
                    {
                        string mapNameStr = panel1.mapPro.MapName;
                        if (reNameEquList.Count > 0)      //获取选中设备的设备类型
                        {
                            string typename = reNameEquList[0].equTypeName;

                            reName.lb_equtype.Text = typename;      //被选中设备类型名称显示到窗体中
                            if (null != reNameEquList[0].equ.DirectionID)
                            {
                                directiongStr = (Enum.Parse(typeof(DirectionEnum), reNameEquList[0].equ.DirectionID.ToString())).ToString();

                            }
                            else
                            {
                                directiongStr = "%行";
                            }
                            string str = mapNameStr + directiongStr + typename;
                            
                            
                            reName.SetShow(str);                    //被选中设备名称前缀显示到窗体输入框中
                        }
                    }
                    if (reName.ShowDialog(this) == DialogResult.OK) //点击修改按钮后，组合新的设备名称
                    {
                        List<MyObject> orderObject = new List<MyObject>();
                        if (reName.IsAdd)
                        {
                            orderObject = (from a in reNameEquList orderby a.LocationInMap.X ascending select a).ToList();
                        }
                        else
                        {
                            orderObject = (from a in reNameEquList orderby a.LocationInMap.X descending select a).ToList();
                        }
                        for (int i = 0; i < orderObject.Count; i++)
                        {
                            orderObject[i].equ.EquName = reName.NameStr + (reName.NameCount + i);
                        }
                    }
                    for (int i = 0; i < reNameEquList.Count; i++) //遍历设备，给选中设备名称赋值
                    {
                        //查询法
                        var current = (from a in panel1.m_ObjectList where a.equ.EquID == reNameEquList[i].equ.EquID select a).FirstOrDefault();
                        current.equ.EquName = reNameEquList[i].equ.EquName;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("修改同类型设备名称:" + e);
            }
        }

        private string InitBackground()
        {
            OpenFileDialog fdialog = new OpenFileDialog();
            fdialog.InitialDirectory = "./BK/";
            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                return fdialog.SafeFileName;
            }
            return null;
        }
        /// <summary>
        /// 设置右键菜单内容
        /// </summary>
        /// <param name="mapid"></param>
        public void SetContextMenu(int mapid)
        {
            try
            {
                ToolStripMenuItem rename = new System.Windows.Forms.ToolStripMenuItem();
                rename.Name = "rename";
                rename.Size = new System.Drawing.Size(124, 22);
                rename.Text = " 重命名 ";
                contextMenuStrip1.Items.Add(rename);
            }
            catch (Exception e)
            {
                Log.WriteLog("设置右键菜单内容:" + e);
                throw e;
            }
        }

        /// <summary>
        /// 设置对象属性
        /// </summary>
        private void SetObjectPro()
        {
            if (this.panel1.m_pCurrentObject == null)
                return;
            ObjectPro objdialog = new ObjectPro();
            MyObject m_temp = this.panel1.m_pCurrentObject;

            if (panel1.m_bMultiMove)//选中多个进行批量编辑
            {
                objBase.FartherID = "";
                if (!string.IsNullOrEmpty(panel1.m_pCurrentObject.equ.FatherEquID))
                {
                    objBase.FartherID = panel1.m_pCurrentObject.equ.FatherEquID;
                }
                for (int i = 0; i < panel1.m_ObjectList.Count; i++)
                {
                    if (panel1.m_ObjectList[i].obj_bSelect)
                    {
                        if (!(objBase.FartherID).Equals(panel1.m_ObjectList[i].equ.FatherEquID))
                        {
                            objBase.FartherID = "";
                            objBase.AlertFartherid = false;
                            break;
                        }
                        objBase.AlertFartherid = true;
                    }
                }
                objBase.StartPosition = FormStartPosition.CenterParent;
                if (!objBase.AlertFartherid)
                {
                    objBase.label1.Hide();
                    objBase.tb_fartherid.Hide();
                }

                if (objBase.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < panel1.m_ObjectList.Count; i++)
                    {
                        if (panel1.m_ObjectList[i].obj_bSelect)
                        {
                            if (objBase.AlertFartherid)
                            {
                                panel1.m_ObjectList[i].equ.FatherEquID = objBase.FartherID;
                            }
                            panel1.m_ObjectList[i].equ.DirectionID = objBase.Direction;
                        }
                    }
                }
            }
            else
            {
                if (m_temp is PLCEqu)
                {
                    objdialog.m_pro = (m_temp as PLCEqu).plc_pro;
                }
                else
                {
                    objdialog.groupBox1.Hide();
                    objdialog.groupBox2.Hide();
                    objdialog.groupBox3.Hide();
                    if (m_temp is EptObject)
                    {
                        objdialog.SetEP((m_temp as EptObject).ep_pro);
                    }
                }
                objdialog.m_obj = m_temp;
                objdialog.StartPosition = FormStartPosition.CenterParent;
                if (objdialog.ShowDialog() == DialogResult.OK)
                {
                    if (m_temp is PLCEqu)
                    {
                        (m_temp as PLCEqu).plc_pro = objdialog.m_pro;
                    }
                }
            }
            this.panel1.DrawCurrentObject();
        }
        public event EventHandler<SelectEventArgs> SelectChanged;
        private void panel1_ObjectSelectChanged(object sender, SelectEventArgs e)
        {
            if (SelectChanged != null)
                SelectChanged(this, new SelectEventArgs(e.bSelect, e.bCopy));
        }
        public void SaveAsDocument(string fname)
        {
            this.panel1.SaveAsDocument(fname);
        }
        //保存到数据库
        public void SaveDocument()
        {
            this.panel1.SaveDocument();
        }
        public Map OpenDocument(string fname)
        {
            return this.panel1.OpenDocument(fname);
        }
        public void OpenDB(Map map)
        {
            this.panel1.OpenDB(map);
        }
        /// <summary>
        /// 根据点位自动生成遥信字符串和遥控字符串
        /// </summary>
        public void AutoCreateYXYK()
        {
            try
            {
                this.panel1.AutoCreateYX();
                this.panel1.AutoCreateYK();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //工具菜单
        public void MultiObjectSet(int type)
        {
            switch (type)
            {
                case 4:
                    this.panel1.toolStripButton4_Click();
                    break;
                case 5:
                    this.panel1.toolStripButton5_Click();
                    break;
                case 6:
                    this.panel1.toolStripButton6_Click();
                    break;
                case 7:
                    this.panel1.toolStripButton7_Click();
                    break;
                case 8:
                    this.panel1.toolStripButton8_Click();
                    break;
                case 9:
                    this.panel1.toolStripButton9_Click();
                    break;
                case 14:
                    this.panel1.toolStripButton14_Click();
                    break;
                case 15:
                    this.panel1.toolStripButton15_Click();
                    break;
                case 19:
                    this.panel1.ZoomIn();
                    break;
                case 20:
                    this.panel1.Zoomout();
                    break;
                case 50://复制
                    this.panel1.toolCopyObject();
                    break;
                case 51://粘贴
                    this.panel1.toolPasteObject();
                    break;
            }
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)//复制
            {
                panel1.toolCopyObject();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)//粘贴
            {
                panel1.toolPasteObject();
            }
        }
    }
}
