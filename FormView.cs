using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;
using WindowMake.Propert;
using System.Drawing;
using WindowMake.Config;
using WindowMake.Tool;

namespace WindowMake
{
    public partial class FormView : Form
    {
        private bool mouseIsDown = false;
        private Rectangle mouseRect = Rectangle.Empty;
        private Image mapBackgroundImage = null;
        public double scale = 0.7;//缩放比例
        //设备大小
        public Size objSize { get { return new Size(30, 30); } }
        private Point m_oldMousePoint = new Point(0, 0);
        private int m_MoveUnit = 4;//方向键移动时的步长
        private bool m_bCopy = false;//是否有对象可以复制
        //设备缓存列表
        public List<MyObject> m_ObjectList = new List<MyObject>();
        public Map mapPro = new Map();//画面背景属性 
        /// <summary>
        /// 多选
        /// </summary>
        public bool m_bMultiMove = false;
        public MyObject m_pCurrentObject = null;
        Dictionary<int, Command> commandDic = new Dictionary<int, Command>();
        public string fileName = "";
        private CreateAddDialog createAddDialog = new CreateAddDialog();
        private ObjectBase objBase = new ObjectBase();
        private ReName reName = new ReName();
        public FormView()
        {
            InitializeComponent();
            try
            {
                if (commandDic.Count == 0)
                {
                    IList<Command> coms = DBHelper.Query<Command>("SELECT * FROM Command;");
                    foreach (Command item in coms)
                    {
                        commandDic.Add(Convert.ToInt32(item.CommandID), item);
                    }
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("读取命令枚举错误" + e.Message);
            }
        }
        private void FormView_MdiChildActivate(object sender, EventArgs e)
        {
            gMain.CurrentPictrueBox = m_pCurrentObject;
        }
        #region 缩放相关
        public bool ScaleChanging
        {
            get;
            set;
        }

        public double MapScale
        {
            get
            {
                return scale;
            }
            set
            {
                var originalScale = scale;
                scale = value;
                if (value != originalScale)
                {
                    ScaleChanging = true;
                    RefreshWindow();
                    ScaleChanging = false;
                }
            }
        }

        /// <summary>
        /// Zoom in
        /// </summary>
        public void ZoomIn()
        {
            if (MapScale <= 0.9)
            {
                MapScale += 0.1;
            }
        }

        public void Zoomout()
        {
            if (MapScale > 0.5)
            {
                MapScale -= 0.1;
            }
        }
        #endregion
        private void RefreshWindow()
        {
            if (mapBackgroundImage != null)
            {
                BackGroundUtil bg = new BackGroundUtil();
                this.BackgroundImage = bg.CreateBackgroundImage(mapBackgroundImage, scale);
                this.Size = BackgroundImage.Size;
                this.Width = Size.Width;
                this.Height = Size.Height;
            }
            foreach (MyObject childControl in m_ObjectList)
            {
                childControl.LocationInMap = LocationUtil.ConvertToOutLocation(new Point((int)Convert.ToDouble(childControl.equ.PointX), (int)Convert.ToDouble(childControl.equ.PointY)), scale);
            }
        }
        /// <summary>
        /// 双击，没有选中双击批量生成设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_pCurrentObject == null)
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
                            gMain.log.WriteLog("配置号码格式不正确");
                            createAddDialog.Hide();
                        }
                        DBOPs db = new DBOPs();
                        gMain.log.WriteLog("批量生成设备：" + obj);
                        int parentWith = BackgroundImage.Size.Width;
                        for (int i = 0; i < count; i++)
                        {
                            var lacation = ((MouseEventArgs)e).Location;
                            int x = (int)(parentWith - 2 * lacation.X) / (count - 1) * i + lacation.X;
                            lacation = new System.Drawing.Point { X = x, Y = lacation.Y };
                            MyObject myObject = DrawObject(obj.ToString(), lacation);
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
                        DrawSelectRect2All();
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
                gMain.log.WriteLog(ex);
            }
        }
        /// <summary>
        /// 绘制对象
        /// </summary>
        /// <param name="equtype">设备类型</param>
        /// <param name="p">位置</param>
        /// <returns></returns>
        public MyObject DrawObject(string equtype, Point p)
        {
            MyObject m_object = null;
            m_object = CreateObject(equtype, p);
            if (m_object != null)
            {
                var location = LocationUtil.ConvertToMapLocation(p, scale);
                m_object.equ.PointX = location.X.ToString();
                m_object.equ.PointY = location.Y.ToString();
                m_object.equ.MapID = mapPro.MapID;
                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                DBOPs db = new DBOPs();
                if (db.InsertEqu(m_object) > 0)
                {
                    if (m_object is PObject)
                    {
                        var areas = (from a in PlcString.p_area_cfg where a.equid == m_object.equ.FatherEquID select a).ToList();
                        if (areas.Count <= 0)
                        {
                            foreach (p_area_cfg area_cfg in PlcString.p_config.areas)
                            {
                                area_cfg.equid = m_object.equ.EquID;
                            }
                            int i = db.InsertAreas(PlcString.p_config.areas);
                            if (i > 0)
                            {
                                PlcString.p_area_cfg.Clear();
                                DataStorage ds = new DataStorage();
                                PlcString.p_area_cfg = ds.GetAllArea();
                            }
                        }
                    }
                    m_object.obj_bSelect = true;
                    m_object.obj_bCopy = true;
                    m_pCurrentObject = m_object;
                    m_ObjectList.Add(m_object);
                }
            }
            return m_object;
        }

        /// <summary>
        /// 设置地图属性
        /// </summary>
        private void SetMapPro()
        {
            PicturePro mapinfo = new PicturePro();
            mapinfo.mapId_tb.Text = mapPro.MapID;
            mapinfo.mapName_tb.Text = mapPro.MapName;
            mapinfo.IsRoad_check.Checked = mapPro.IsRoad == 1;
            mapinfo.url_tb.Text = mapPro.MapAddress;
            mapinfo.text_filebk.Text = @"BK/" + mapPro.MapName + ".png";
            if (mapinfo.ShowDialog() == DialogResult.OK)
            {
                mapPro.MapName = mapinfo.mapName_tb.Text;
                mapPro.IsRoad = mapinfo.IsRoad_check.Checked == true ? 1 : 0;
                mapPro.MapAddress = mapinfo.url_tb.Text;
                mapPro.MapID = mapinfo.mapId_tb.Text;
                BackgroundImageLayout = ImageLayout.Stretch;
                if (!string.IsNullOrEmpty(mapinfo.text_filebk.Text))
                {
                    SetBackgroud(mapinfo.text_filebk.Text);
                }
                else
                    BackgroundImage = null;
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
                case "picturePro":
                    SetMapPro();
                    break;
                case "objectPro":
                    SetObjectPro();
                    break;
                case "rename":
                    MyObject.ObjectType equtype = MyObject.ObjectType.UnKnow;
                    List<MyObject> reNameEquList = new List<MyObject>();
                    for (int i = 0; i < m_ObjectList.Count; i++)
                    {
                        if (m_ObjectList[i].obj_bSelect)
                        {
                            if (MyObject.ObjectType.UnKnow == equtype)
                            {
                                equtype = m_ObjectList[i].equtype;

                            }
                            if (m_ObjectList[i].equtype == equtype)
                            {

                                reNameEquList.Add(m_ObjectList[i]);

                            }
                        }
                    }
                    ReNameSameTypeObj(reNameEquList);
                    break;
                case "updatePileNo":
                    MyObject.ObjectType equtype_1 = MyObject.ObjectType.UnKnow;
                    List<MyObject> updatePileNoEquList = new List<MyObject>();
                     for (int j = 0; j < m_ObjectList.Count; j++)
                    {
                        if (m_ObjectList[j].obj_bSelect)
                        {
                            if (MyObject.ObjectType.UnKnow == equtype_1)
                            {
                                equtype_1 = m_ObjectList[j].equtype;

                            }
                            if (m_ObjectList[j].equtype == equtype_1)
                            {

                                updatePileNoEquList.Add(m_ObjectList[j]);

                            }
                        }
                    }
                   // if(updatePileNoEquList[0].equtype =="")
                    break;
                default:
                    break;
            }
        }

        private void UpdatePileNo(List<MyObject> updatePileNoEquList)
        {

        }

        /// <summary>
        /// 修改同类型设备名称
        /// </summary>
        /// <param name="reNameEquList"></param>
        private void ReNameSameTypeObj(List<MyObject> reNameEquList)
        {
            string directiongStr = "";
            try
            {
                if (reNameEquList.Count > 0)
                {
                    if (string.IsNullOrEmpty(reName.NameStr))   //输入框中显示选中设备所在隧道和方向
                    {
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
                            string str = mapPro.MapName + directiongStr + typename;


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
                        var current = (from a in m_ObjectList where a.equ.EquID == reNameEquList[i].equ.EquID select a).FirstOrDefault();
                        current.equ.EquName = reNameEquList[i].equ.EquName;
                    }
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("修改同类型设备名称:" + e);
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

                ToolStripMenuItem updatePileNo = new System.Windows.Forms.ToolStripMenuItem();
                updatePileNo.Name = "updatePileNo";
                updatePileNo.Size = new System.Drawing.Size(124, 22);
                updatePileNo.Text = " 批量生成桩号 ";
                contextMenuStrip1.Items.Add(updatePileNo);

            }
            catch (Exception e)
            {
                gMain.log.WriteLog("设置右键菜单内容:" + e);
                throw e;
            }
        }

        /// <summary>
        /// 设置对象属性
        /// </summary>
        private void SetObjectPro()
        {
            if (m_pCurrentObject == null)
                return;
            ObjectPro objdialog = new ObjectPro();
            MyObject m_temp = m_pCurrentObject;

            if (m_bMultiMove)//选中多个进行批量编辑
            {
                objBase.FartherID = "";
                if (!string.IsNullOrEmpty(m_pCurrentObject.equ.FatherEquID))
                {
                    objBase.FartherID = m_pCurrentObject.equ.FatherEquID;
                }
                for (int i = 0; i < m_ObjectList.Count; i++)
                {
                    if (m_ObjectList[i].obj_bSelect)
                    {
                        if (!(objBase.FartherID).Equals(m_ObjectList[i].equ.FatherEquID))
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
                    for (int i = 0; i < m_ObjectList.Count; i++)
                    {
                        if (m_ObjectList[i].obj_bSelect)
                        {
                            if (objBase.AlertFartherid)
                            {
                                m_ObjectList[i].equ.FatherEquID = objBase.FartherID;
                            }
                            m_ObjectList[i].equ.DirectionID = objBase.Direction;
                        }
                    }
                }
            }
            else
            {
                if (m_pCurrentObject is PLCEqu)
                {
                    objdialog.m_pro = (m_pCurrentObject as PLCEqu).plc_pro;
                }
                else
                {
                    objdialog.groupBox1.Hide();
                    objdialog.groupBox2.Hide();
                    objdialog.groupBox3.Hide();
                    if (m_pCurrentObject is EptObject)
                    {
                        objdialog.SetEP((m_pCurrentObject as EptObject).ep_pro);
                    }
                }
                objdialog.m_obj = m_pCurrentObject;
                objdialog.StartPosition = FormStartPosition.CenterParent;
                if (objdialog.ShowDialog() == DialogResult.OK)
                {
                    if (m_pCurrentObject is PLCEqu)
                    {
                        (m_pCurrentObject as PLCEqu).plc_pro = objdialog.m_pro;
                    }
                }
            }
            DrawCurrentObject();
        }
        /// <summary>
        /// 重画当前对象
        /// </summary>
        public void DrawCurrentObject()
        {
            if (m_pCurrentObject != null)
            {
                Graphics g = this.CreateGraphics();
                m_pCurrentObject.DrawOjbect(g);
                DrawSelectRect2(g, m_pCurrentObject, Color.White, Brushes.Black);
            }
        }

        public event EventHandler<SelectEventArgs> SelectChanged;

        public void SaveAsDocument(string fname)
        {
            //this.panel1.SaveAsDocument(fname);
        }
        /// <summary>
        /// 保存到数据库
        /// </summary>
        public void SaveDocument()
        {
            try
            {
                DBOPs dbop = new DBOPs();
                DataStorage ds = new DataStorage();
                List<Equ> oldEqus = ds.ReadEqu(Convert.ToInt32(mapPro.MapID));
                List<Equ> delEqus = new List<Equ>();
                // 删除设备
                for (int i = 0; i < oldEqus.Count; i++)
                {
                    var temp = m_ObjectList.Where(p => p.equ.EquID == oldEqus[i].EquID).FirstOrDefault();
                    if (temp == null)
                    {
                        delEqus.Add(oldEqus[i]);
                    }
                }
                dbop.DeleteEqu(delEqus);

                // 存在则更新，否则插入
                dbop.UpdateORInsertEqu(m_ObjectList);
                m_ObjectList.Clear();
                OpenDB(mapPro);
                this.RefreshWindow();
            }
            catch (Exception e)
            {
                gMain.log.WriteLog(e);
                throw e;
            }
        }
        public Map OpenDocument(string fname)
        {
            try
            {
                ReadInit readInit = new ReadInit();
                if (readInit.ShowDialog() == DialogResult.OK)
                {
                    if (readInit.cb_newMap.Checked)//是否在数据库中新建地图
                    {
                        mapPro.MapName = fname.Split('\\').Last().Split('.').FirstOrDefault();
                        DataStorage ds = new DataStorage();
                        mapPro.MapID = (int.Parse(ds.GetMaxMapID()) + 1).ToString();
                        DBOPs insert = new DBOPs();
                        if (insert.InsertMap(mapPro) < 0)
                        { throw new Exception(); }
                    }
                    else
                    {
                        mapPro.MapName = readInit.comdrop_url.Text;
                        mapPro.MapID = readInit.GetMapID;
                    }
                    //读取地图图片获取大小比例
                    Size size = SetBackgroud(@"BK/" + mapPro.MapName + ".png", readInit.MapWidth, readInit.MapHeight);
                    ReadDocument rd = new ReadDocument();

                    rd.Read(mapPro.MapID, fname, size.Height, size.Width, readInit.IsCreate);
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog(e.Message);
                throw;
            }
            DinoComparer dc = new DinoComparer();
            m_ObjectList.Sort(dc);
            return mapPro;
        }
        /// <summary>
        /// 设置背景图片及大小
        /// </summary>
        /// <param name="mappic">图片地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Size SetBackgroud(string mappic, int width = 1920, int height = 1080)
        {
            BackGroundUtil bg = new BackGroundUtil();
            //this.BackgroundImageLayout = ImageLayout.Stretch;
            try
            {
                mapBackgroundImage = Image.FromFile(mappic);
                this.BackgroundImage = bg.CreateBackgroundImage(mapBackgroundImage, scale);
                this.Size = mapBackgroundImage.Size;
                return this.Size;
            }
            catch (Exception)
            {
                var tempimg = new Bitmap(width, height);
                this.BackgroundImage = bg.CreateBackgroundImage(tempimg, scale);
                return this.BackgroundImage.Size;
            }
        }
        /// <summary>
        /// 打开数据库中的地图数据
        /// </summary>
        /// <param name="map"></param>
        public void OpenDB(Map map)
        {
            try
            {
                mapPro.MapID = map.MapID;
                mapPro.MapName = map.MapName;
                mapPro.IsRoad = map.IsRoad;
                mapPro.MapAddress = map.MapAddress;
                string mappic = @"BK/" + map.MapName + ".png";
                SetBackgroud(mappic);
                // 设备加载
                DataStorage ds = new DataStorage();
                MyObject m_object = null;
                List<Equ> equs = ds.ReadEqu(Convert.ToInt32(map.MapID));
                List<yx> yxs = ds.GetYXs();
                List<Yx_cfg> yxcfgs = ds.GetYXcfgs();
                List<yk> yks = ds.GetYks();
                List<Yk_cfg> ykcfgs = ds.GetYKcfgs();
                List<yc> ycs = ds.GetYcs();
                List<Yc_cfg> yccfgs = ds.GetYccfgs();
                List<c_cfg> cmscfgs = ds.GetCMSConfig();
                List<tunnelInfo> tunnels = ds.GetTunnel(Convert.ToInt32(map.MapID));
                List<tollInfo> tolls = ds.GetToll(Convert.ToInt32(map.MapID));
                if (PlcString.p_area_cfg == null)
                {
                    PlcString.p_area_cfg = ds.GetAllArea();
                }
                foreach (Equ equ in equs)
                {
                    var localtion = LocationUtil.ConvertToOutLocation(new Point((int)Convert.ToDouble(equ.PointX), (int)Convert.ToDouble(equ.PointY)), scale);
                    m_object = CreateObject(equ.EquTypeID, localtion);
                    m_object.equ = equ;
                    #region PLC
                    if (m_object is PLCEqu)
                    {
                        var plcEqu = m_object as PLCEqu;
                        var yxcfg = yxcfgs.Where(p => p.EquID == equ.EquID).ToList();
                        if (yxcfg != null)
                        {
                            plcEqu.plc_pro.yxcfgList = yxcfg;
                        }
                        var yxl = yxs.Where(p => p.EquID == equ.EquID).ToList();
                        if (yxl != null)
                        {
                            plcEqu.plc_pro.yxList = yxl;
                        }
                        var ykcfg = ykcfgs.Where(p => p.EquID == equ.EquID).ToList();
                        if (ykcfg != null)
                        {
                            plcEqu.plc_pro.ykcfgList = ykcfg;
                        }
                        var ykl = yks.Where(p => p.EquID == equ.EquID).ToList();
                        if (ykl != null)
                        {
                            plcEqu.plc_pro.ykList = ykl;
                        }
                        var yccfg = yccfgs.Where(p => p.EquID == equ.EquID).ToList();
                        if (yccfg != null)
                        {
                            foreach (var item in yccfg)
                            {
                                var yc = ycs.Where(p => p.YCID == item.YcID).FirstOrDefault();
                                YCExt ycExt = new YCExt();
                                ycExt.EquID = yc.EquID;
                                ycExt.YCCollecDown = yc.YCCollecDown;
                                ycExt.YCCollecUP = yc.YCCollecUP;
                                ycExt.YCField = yc.YCField;
                                ycExt.YCFun = yc.YCFun;
                                ycExt.YCID = yc.YCID;
                                ycExt.YCRealDown = yc.YCRealDown;
                                ycExt.YCRealUP = yc.YCRealUP;
                                ycExt.ID = item.ID;
                                ycExt.Order = item.Order;
                                ycExt.AddrAndBit = item.AddrAndBit;
                                ycExt.AreaID = item.AreaID;
                                plcEqu.plc_pro.yclist.Add(ycExt);
                            }
                        }
                    }

                    #endregion
                    #region CMS
                    else if (m_object is CMSEqu)
                    {
                        var cmsEqu = m_object as CMSEqu;
                        cmsEqu.cms_pro = (from a in cmscfgs where a.EquID == equ.EquID select a).FirstOrDefault();
                    }
                    #endregion
                    #region TV
                    else if (m_object is TVEqu)
                    {

                    }
                    #endregion
                    m_ObjectList.Add(m_object);
                }
                #region 结构物
                foreach (tunnelInfo item in tunnels)
                {
                    var localtion = LocationUtil.ConvertToOutLocation(new Point((int)Convert.ToDouble(item.PointX), (int)Convert.ToDouble(item.PointY)), scale);
                    m_object = CreateObject("tunnel", localtion);
                    m_object.equ.EquID = item.BM;
                    m_object.equ.EquName = item.Name;
                    m_object.equ.Note = item.Mesg;
                    m_object.equ.PileNo = item.CenterStake;
                    m_object.equ.DirectionID = item.Direction;
                    m_ObjectList.Add(m_object);
                }
                foreach (var item in tolls)
                {
                    var localtion = LocationUtil.ConvertToOutLocation(new Point((int)Convert.ToDouble(item.PointX), (int)Convert.ToDouble(item.PointY)), scale);
                    m_object = CreateObject("toll", localtion);
                    m_object.equ.EquID = item.BM;
                    m_object.equ.EquName = item.Name;
                    m_object.equ.Note = item.Mesg;
                    m_object.equ.PileNo = item.Stake;
                    m_object.equ.IP = item.IP;
                    m_object.equ.Port = item.Port;
                    m_object.equ.PointX = item.PointX;
                    m_object.equ.PointY = item.PointY;
                    m_object.equ.DirectionID = item.Direction;
                    m_ObjectList.Add(m_object);
                }

                #endregion
                DinoComparer dc = new DinoComparer();
                m_ObjectList.Sort(dc);
                this.RefreshWindow();
            }
            catch (Exception e)
            {
                gMain.log.WriteLog(e);
                throw;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                m_ObjectList[i].DrawOjbect(e.Graphics);
                if (m_ObjectList[i].obj_bSelect)
                {
                    DrawSelectRect2(e.Graphics, m_ObjectList[i], Color.White, Brushes.Black);
                }
            }
            ////if (m_bAltDown && m_DrawMode == DrawMode.Move)
            ////{
            ////    Pen myPen = new Pen(Color.Black, 0.1F);
            ////    myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            ////    e.Graphics.DrawRectangle(myPen, new Rectangle(m_StartPt.X, m_StartPt.Y, M_EndPt.X - m_StartPt.X, M_EndPt.Y - m_StartPt.Y));
            ////}
            base.OnPaint(e);
        }

        /// <summary>
        /// 清除选中对象
        /// </summary>
        private void ClearSelectObject()
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    m_ObjectList[i].obj_bCopy = false;
                    m_ObjectList[i].obj_bSelect = false;
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    m_ObjectList[i].DrawOjbect(g);
                }
            }
            m_bMultiMove = false;
        }

        /// <summary>
        /// 区域选中设备的标记
        /// </summary>
        /// <param name="s">起始坐标</param>
        /// <param name="e">结束坐标</param>
        /// <returns></returns>
        private void CreateSelectedObjectArea(RectangleF rect)
        {
            Graphics g = CreateGraphics();
            int n = 0;
            for (int i = m_ObjectList.Count - 1; i >= 0; i--)
            {
                if ((m_ObjectList[i].LocationInMap.X + objSize.Width > rect.X && m_ObjectList[i].LocationInMap.Y + objSize.Height > rect.Y) && (m_ObjectList[i].LocationInMap.X < rect.X + rect.Width && m_ObjectList[i].LocationInMap.Y < rect.Y + rect.Height))
                {
                    n++;
                    m_ObjectList[i].obj_bCopy = true;
                    m_ObjectList[i].obj_bSelect = true;
                    DrawSelectRect2(g, m_ObjectList[i], Color.White, Brushes.Black);
                    m_pCurrentObject = m_ObjectList[i];
                }
            }
            if (n > 1)
            {
                m_bMultiMove = true;
            }
            else
            {
                m_bMultiMove = false;
            }
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="equtype">对象类型</param>
        /// <param name="m_object">对象实体</param>
        /// <param name="start">坐标</param>
        /// <returns></returns>
        public MyObject CreateObject(string equtype, PointF start)
        {
            MyObject m_object = null;
            #region 
            switch (equtype)
            {
                case "CF":
                    m_object = new CFObject(start);
                    break;
                case "CL":
                    m_object = new CLObject(start);
                    break;
                case "CM":
                    m_object = new CMObject(start);
                    break;
                case "F":
                    m_object = new FObject(start);
                    break;
                case "E":
                    m_object = new EObject(start);
                    break;
                case "EM":
                    m_object = new EMObject(start);
                    break;
                case "EM_CH4":
                    m_object = new EMCH4Object(start);
                    break;
                case "EM_CO":
                    m_object = new EMCOObject(start);
                    break;
                case "EM_O2":
                    m_object = new EMO2Object(start);
                    break;
                case "EP":
                    m_object = new EpObject(start);
                    break;
                case "EP_R":
                    m_object = new EprObject(start);
                    break;
                case "EP_T":
                    m_object = new EptObject(start);
                    break;
                case "F_L":
                    m_object = new FlObject(start);
                    break;
                case "F_SB":
                    m_object = new FsbObject(start);
                    break;
                case "F_YG":
                    m_object = new FygObject(start);
                    break;
                case "HL":
                    m_object = new HLObject(start);
                    break;
                case "I":
                    m_object = new IObject(start);
                    break;
                case "PK":
                    m_object = new PKObject(start);
                    break;
                case "P_AF":
                    m_object = new PafObject(start);
                    break;
                case "P_EPM":
                    m_object = new PEPMObject(start);
                    break;
                case "P_CL":
                    m_object = new PclObject(start);
                    break;
                case "P_GS":
                    m_object = new PGSObject(start);
                    break;
                case "P_CO":
                    m_object = new PcoObject(start);
                    break;
                case "P_GJ":
                    m_object = new PgjObject(start);
                    break;
                case "P_HL2":
                    m_object = new Phl2Object(start);
                    break;
                case "P_HL":
                    m_object = new PhlObject(start);
                    break;
                case "P_JF":
                    m_object = new PjfObject(start);
                    break;
                case "P_LJQ":
                    m_object = new PljqObject(start);
                    break;
                case "P_LLDI":
                    m_object = new PlldiObject(start);
                    break;
                case "P_L":
                    m_object = new PlObject(start);
                    break;
                case "P_RL":
                    m_object = new PrlObject(start);
                    break;
                case "P_LYJ":
                    m_object = new PlyjObject(start);
                    break;
                case "P":
                    m_object = new PObject(start);
                    break;
                case "P_P":
                    m_object = new PpObject(start);
                    break;
                case "P_TD":
                    m_object = new PtdObject(start);
                    break;
                case "P_TL2_Close":
                    m_object = new Ptl2CloseObject(start);
                    break;
                case "P_TL2_Down":
                    m_object = new Ptl2DownObject(start);
                    break;
                case "P_TL2_UP":
                    m_object = new Ptl2UpObject(start);
                    break;
                case "P_TL5_Left":
                    m_object = new Ptl5LeftObject(start);
                    break;
                case "P_TL5_Right":
                    m_object = new Ptl5RightObject(start);
                    break;
                case "P_TL3_Left":
                    m_object = new Ptl3LeftObject(start);
                    break;
                case "P_TL3_Right":
                    m_object = new Ptl3RightObject(start);
                    break;
                case "P_TW":
                    m_object = new PtwObject(start);
                    break;
                case "P_VI":
                    m_object = new PviObject(start);
                    break;
                case "S":
                    m_object = new SObject(start);
                    break;
                case "TV_CCTV_Ball":
                    m_object = new TvBallObject(start);
                    break;
                case "TV_CCTV_E":
                    m_object = new TvEObject(start);
                    break;
                case "TV_CCTV_Gun":
                    m_object = new TvGunObject(start);
                    break;
                case "TV":
                    m_object = new TvObject(start);
                    break;
                case "VC":
                    m_object = new VcObject(start);
                    break;
                case "VI":
                    m_object = new ViObject(start);
                    break;
                case "tunnel":
                    m_object = new tunnel(start);
                    break;
                case "toll":
                    m_object = new toll(start);
                    break;
                case "services":
                    m_object = new services(start);
                    break;
                case "bridge":
                    m_object = new bridge(start);
                    break;
                case "slope":
                    m_object = new slope(start);
                    break;
                default:
                    break;
            }
            #endregion
            if (m_object != null)
            {
                m_object.LocationInMapChangeChanged += M_object_LocationInMapChangeChanged;
            }
            return m_object;
        }
        /// <summary>
        /// 地图坐标改变对数据库位置进行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_object_LocationInMapChangeChanged(object sender, MyObject e)
        {
            if (!ScaleChanging)
            {
                PointF localtion = LocationUtil.ConvertToMapLocation(e.LocationInMap, MapScale);
                e.equ.PointX = localtion.X.ToString();
                e.equ.PointY = localtion.Y.ToString();
            }
        }

        /// <summary>
        /// 根据点位自动生成遥信字符串和遥控字符串
        /// </summary>
        public void AutoCreateYXYK()
        {
            try
            {
                AutoCreatString at = new AutoCreatString();
                at.commandDic = commandDic;
                at.AutoCreateYX(m_ObjectList);
                at.AutoCreateYK(m_ObjectList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 工具菜单
        /// </summary>
        /// <param name="type"></param>
        public void MultiObjectSet(int type)
        {
            switch (type)
            {
                case 4:
                    LeftAlignment();
                    break;
                case 5:
                    toolStripButton5_Click();
                    break;
                case 6:
                    RightAlignment();
                    break;
                case 7:
                    TopAlignment();
                    break;
                case 8:
                    toolStripButton8_Click();
                    break;
                case 9:
                    BottomAlignment();
                    break;
                case 14:
                    HorizontalSpace2Max();
                    break;
                case 15:
                    HorizontalSpace2Min();
                    break;
                case 19:
                    ZoomIn();
                    break;
                case 20:
                    Zoomout();
                    break;
                    //case 50://复制
                    //    this.panel1.toolCopyObject();
                    //    break;
                    //case 51://粘贴
                    //    this.panel1.toolPasteObject();
                    //    break;
            }
        }
        #region 工具栏
        /// <summary>
        /// 左对齐
        /// </summary>
        public void LeftAlignment()
        {//左对齐
            PointF location = new PointF(10000, 10000);
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    if (m_ObjectList[i].LocationInMap.X < location.X)
                    {
                        location = m_ObjectList[i].LocationInMap;
                    }
                }
            }
            FlushLocation2X(location);
            DrawSelectRect2All();
        }
        /// <summary>
        /// 刷新对齐x坐标
        /// </summary>
        /// <param name="location"></param>
        private void FlushLocation2X(PointF location)
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    PointF tempLocation = new PointF(location.X, m_ObjectList[i].LocationInMap.Y);
                    m_ObjectList[i].LocationInMap = tempLocation;
                    tempLocation = LocationUtil.ConvertToMapLocation(tempLocation, scale);
                    m_ObjectList[i].equ.PointX = tempLocation.X.ToString();
                    m_ObjectList[i].equ.PointY = tempLocation.Y.ToString();
                    m_ObjectList[i].DrawOjbect(g);
                }
            }
        }
        public void toolStripButton5_Click()
        {//水平居中
        }
        /// <summary>
        /// 右对齐
        /// </summary>
        public void RightAlignment()
        {
            PointF location = new PointF(0, 0);
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    if (m_ObjectList[i].LocationInMap.X > location.X)
                    {
                        location = m_ObjectList[i].LocationInMap;
                    }
                }
            }
            FlushLocation2X(location);
            DrawSelectRect2All();
        }
        /// <summary>
        /// 顶对齐
        /// </summary>
        public void TopAlignment()
        {
            PointF location = new PointF(10000, 10000);
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    if (m_ObjectList[i].LocationInMap.Y < location.Y)
                    {
                        location = m_ObjectList[i].LocationInMap;
                    }
                }
            }
            FlushLocation2Y(location);
            DrawSelectRect2All();
        }
        /// <summary>
        /// 刷新对齐Y坐标
        /// </summary>
        /// <param name="location"></param>
        private void FlushLocation2Y(PointF location)
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    PointF tempLocation = new PointF(m_ObjectList[i].LocationInMap.X, location.Y);
                    m_ObjectList[i].LocationInMap = tempLocation;
                    tempLocation = LocationUtil.ConvertToMapLocation(tempLocation, scale);
                    m_ObjectList[i].equ.PointX = tempLocation.X.ToString();
                    m_ObjectList[i].equ.PointY = tempLocation.Y.ToString();
                    m_ObjectList[i].DrawOjbect(g);
                }
            }
        }

        public void toolStripButton8_Click()
        {//水平间距相等
            HorizontalSpace();
        }
        /// <summary>
        /// 水平间距
        /// </summary>
        private void HorizontalSpace(int len = 0)
        {
            Graphics g = CreateGraphics();
            float start_x = 999999;
            float end_x = 0;
            int num = 0;
            List<MyObject> temp_array = new List<MyObject>();
            int i = 0;
            for (i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    num++;
                    temp_array.Add(m_ObjectList[i]);
                    if (m_ObjectList[i].LocationInMap.X > end_x)
                        end_x = m_ObjectList[i].LocationInMap.X;
                    if (m_ObjectList[i].LocationInMap.X < start_x)
                        start_x = m_ObjectList[i].LocationInMap.X;
                }
            }
            temp_array.Sort(CompareObjectByPostionX);
            float nlen = (end_x - start_x) / (num - 1);
            nlen += len;
            for (i = 0; i < temp_array.Count; i++)
            {
                MyObjectInvalidate(temp_array[i].LocationInMap);
                PointF tempLocation = new PointF(start_x + i * nlen, temp_array[i].LocationInMap.Y);
                temp_array[i].LocationInMap = tempLocation;
                tempLocation = LocationUtil.ConvertToMapLocation(tempLocation, scale);
                temp_array[i].equ.PointX = tempLocation.X.ToString();
                temp_array[i].equ.PointY = tempLocation.Y.ToString();
                temp_array[i].DrawOjbect(g);
            }
            DrawSelectRect2All();
        }
        /// <summary>
        /// 底对齐
        /// </summary>
        public void BottomAlignment()
        {
            PointF location = new PointF(0, 0);
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    if (m_ObjectList[i].LocationInMap.Y > location.Y)
                    {
                        location = m_ObjectList[i].LocationInMap;
                    }
                }
            }
            FlushLocation2Y(location);
            DrawSelectRect2All();
        }

        private static int CompareObjectByPostionX(MyObject x, MyObject y)
        {
            return x.LocationInMap.X.CompareTo(y.LocationInMap.X);
        }
        /// <summary>
        /// 增加水平间距
        /// </summary>
        public void HorizontalSpace2Max()
        {
            HorizontalSpace(4);
        }
        /// <summary>
        /// 减少水平间距
        /// </summary>
        public void HorizontalSpace2Min()
        {
            HorizontalSpace(-4);
        }
        private static int CompareObjectByPostionY(MyObject x, MyObject y)
        {
            return x.LocationInMap.Y.CompareTo(y.LocationInMap.Y);
        }

        #endregion
        /// <summary>
        /// 复制 
        /// </summary>
        public void toolCopyObject()
        {
            int i = 0;
            m_bCopy = false;
            for (i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    m_ObjectList[i].obj_bCopy = true;
                    m_bCopy = true;
                }
            }
            if (SelectChanged != null)
                SelectChanged(this, new SelectEventArgs(m_bMultiMove, m_bCopy));
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        public void toolPasteObject()
        {
            int i = 0;
            MyObject m_object = null;
            PointF start;
            int iMovePix = 50;
            Graphics g = CreateGraphics();
            for (i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bCopy)
                {
                    m_ObjectList[i].obj_bSelect = false;
                    m_ObjectList[i].obj_bCopy = false;
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    start = new PointF(m_ObjectList[i].LocationInMap.X + iMovePix, m_ObjectList[i].LocationInMap.Y + iMovePix);
                    m_object = CreateObject(m_ObjectList[i].equtype.ToString(), start);
                    if (m_object != null)
                    {
                        #region 基础信息
                        m_object.equ.EquName = m_ObjectList[i].equ.EquName;
                        m_object.equ.FatherEquID = m_ObjectList[i].equ.FatherEquID;
                        m_object.equ.IP = m_ObjectList[i].equ.IP;
                        m_object.equ.Port = m_ObjectList[i].equ.Port;
                        m_object.equ.PointX = m_ObjectList[i].equ.PointX;
                        m_object.equ.PointY = m_ObjectList[i].equ.PointY;
                        m_object.equ.MapID = m_ObjectList[i].equ.MapID;
                        m_object.equ.msgTimeoutSec = m_ObjectList[i].equ.msgTimeoutSec;
                        m_object.equ.plcStationAddress = m_ObjectList[i].equ.plcStationAddress;
                        m_object.equ.Vendor = m_ObjectList[i].equ.Vendor;
                        m_object.equ.TaskWV = m_ObjectList[i].equ.TaskWV;
                        m_object.equ.RunMode = m_ObjectList[i].equ.RunMode;
                        m_object.equ.DirectionID = m_ObjectList[i].equ.DirectionID;
                        m_object.equ.AddressDiscribe = m_ObjectList[i].equ.AddressDiscribe;
                        m_object.equ.AlarmMethod = m_ObjectList[i].equ.AlarmMethod;
                        m_object.equ.Note = m_ObjectList[i].equ.Note;
                        #endregion
                        #region plc配置信息
                        if (m_object is PObject)
                        {

                        }
                        if (m_object is PLCEqu)
                        {
                            PLCEqu plcEqu = m_ObjectList[i] as PLCEqu;
                            for (int j = 0; j < plcEqu.plc_pro.yxcfgList.Count; j++)
                            {
                                Yx_cfg yx = new Yx_cfg();
                                yx.AddrAndBit = plcEqu.plc_pro.yxcfgList[j].AddrAndBit;
                                yx.AreaID = plcEqu.plc_pro.yxcfgList[j].AreaID;
                                yx.IsError = plcEqu.plc_pro.yxcfgList[j].IsError;
                                yx.Order = plcEqu.plc_pro.yxcfgList[j].Order;
                                ((PLCEqu)m_object).plc_pro.yxcfgList.Add(yx);
                            }
                            for (int j = 0; j < plcEqu.plc_pro.ykcfgList.Count; j++)
                            {
                                Yk_cfg yk = new Yk_cfg();
                                yk.AddrAndBit = plcEqu.plc_pro.ykcfgList[j].AddrAndBit;
                                yk.AreaID = plcEqu.plc_pro.ykcfgList[j].AreaID;
                                yk.Order = plcEqu.plc_pro.ykcfgList[j].Order;
                                ((PLCEqu)m_object).plc_pro.ykcfgList.Add(yk);
                            }
                            for (int j = 0; j < plcEqu.plc_pro.yxcfgList.Count; j++)
                            {
                                Yx_cfg yx = new Yx_cfg();
                                yx.AddrAndBit = plcEqu.plc_pro.yxcfgList[j].AddrAndBit;
                                yx.AreaID = plcEqu.plc_pro.yxcfgList[j].AreaID;
                                yx.IsError = plcEqu.plc_pro.yxcfgList[j].IsError;
                                yx.Order = plcEqu.plc_pro.yxcfgList[j].Order;
                                ((PLCEqu)m_object).plc_pro.yxcfgList.Add(yx);
                            }
                            for (int j = 0; j < plcEqu.plc_pro.ykcfgList.Count; j++)
                            {
                                Yk_cfg yk = new Yk_cfg();
                                yk.AddrAndBit = plcEqu.plc_pro.ykcfgList[j].AddrAndBit;
                                yk.AreaID = plcEqu.plc_pro.ykcfgList[j].AreaID;
                                yk.Order = plcEqu.plc_pro.ykcfgList[j].Order;
                                ((PLCEqu)m_object).plc_pro.ykcfgList.Add(yk);
                            }
                        }

                        #endregion
                        m_object.picName = m_ObjectList[i].picName;
                        m_object.obj_bSelect = true;
                        m_object.equ.MapID = mapPro.MapID;
                        m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                        DBOPs db = new DBOPs();
                        if (db.InsertEqu(m_object) > 0)
                        {
                            m_ObjectList.Add(m_object);
                            m_pCurrentObject = m_object;
                            m_object.DrawOjbect(g);
                            //DrawSelectRect2(g, m_object, Color.White, Brushes.Black);
                        }
                    }
                }
            }
            DrawSelectRect2All();
        }

        /// <summary>
        /// 按键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)//复制
            {
                toolCopyObject();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)//粘贴
            {
                toolPasteObject();
            }
            Graphics g = CreateGraphics();
            int i = 0;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        for (int j = m_ObjectList.Count - 1; j >= 0; j--)
                        {
                            if (m_ObjectList[j].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[j].LocationInMap);
                                m_ObjectList.Remove(m_ObjectList[j]);
                            }
                        }
                        m_pCurrentObject = null;
                    }
                    break;
                //case Keys.Menu:
                //    m_bAltDown = true; //框选状态
                //    break;
                case Keys.ControlKey:
                    m_bMultiMove = true;
                    break;
                case Keys.Up:
                    {
                        for (i = 0; i < m_ObjectList.Count; i++)
                        {
                            if (m_ObjectList[i].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                                var location = m_ObjectList[i].LocationInMap;
                                location.Y -= m_MoveUnit;
                                m_ObjectList[i].LocationInMap = location;
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        for (i = 0; i < m_ObjectList.Count; i++)
                        {
                            if (m_ObjectList[i].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                                var location = m_ObjectList[i].LocationInMap;
                                location.Y += m_MoveUnit;
                                m_ObjectList[i].LocationInMap = location;
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    {
                        for (i = 0; i < m_ObjectList.Count; i++)
                        {
                            if (m_ObjectList[i].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                                var location = m_ObjectList[i].LocationInMap;
                                location.X -= m_MoveUnit;
                                m_ObjectList[i].LocationInMap = location;
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        for (i = 0; i < m_ObjectList.Count; i++)
                        {
                            if (m_ObjectList[i].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                                var location = m_ObjectList[i].LocationInMap;
                                location.X += m_MoveUnit;
                                m_ObjectList[i].LocationInMap = location;
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                    }
                    break;
            }
            DrawSelectRect2All();
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Right)
                return true;
            if (keyData == Keys.Alt || keyData == Keys.Control)
                return true;
            return base.IsInputKey(keyData);
        }
        private void FormView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    m_bMultiMove = false;
                    break;
                    //case Keys.Menu:
                    //    m_bAltDown = false;
                    //    break;
            }
        }

        private void DrawSelectRect2All()
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    DrawSelectRect2(g, m_ObjectList[i], Color.White, Brushes.Black);
                }
            }
        }

        private void FormView_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
            m_oldMousePoint = e.Location;
            MyObject temp = SeachObject(e.Location);
            if (temp != null)
            {
                if (!m_bMultiMove)
                {
                    if (m_pCurrentObject != null)
                    {
                        m_pCurrentObject = null;
                        ClearSelectObject();
                    }
                    ChangeCurrentObject(temp);
                }
            }
            else
            {
                if (m_pCurrentObject != null)
                {
                    ClearSelectObject();
                    m_pCurrentObject = null;
                }
                DrawStart(e.Location);
            }
        }

        private void FormView_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                if (m_pCurrentObject == null)//框选效果
                {
                    ResizeToRectangle(e.Location);
                }
                else //移动
                {
                    int x = e.Location.X - m_oldMousePoint.X;
                    int y = e.Location.Y - m_oldMousePoint.Y;
                    Graphics g = this.CreateGraphics();
                    if (Math.Abs(x) > m_MoveUnit || Math.Abs(y) > m_MoveUnit)
                    {
                        for (int i = 0; i < m_ObjectList.Count; i++)
                        {
                            if (m_ObjectList[i].obj_bSelect)
                            {
                                MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                                var location = m_ObjectList[i].LocationInMap;
                                location.X += x;
                                location.Y += y;
                                m_ObjectList[i].LocationInMap = location;
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                        DrawSelectRect2All();
                        m_oldMousePoint = e.Location;
                    }
                }
            }
        }

        private void FormView_MouseUp(object sender, MouseEventArgs e)
        {
            Capture = false;
            Cursor.Clip = Rectangle.Empty;
            mouseIsDown = false;
            DrawRectangle();
            if (mouseRect.Width > 0 || mouseRect.Height > 0)
            {
                CreateSelectedObjectArea(mouseRect);
            }
            mouseRect = Rectangle.Empty;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }

        /// <summary>
        /// 切换当前选中对象
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeCurrentObject(MyObject obj)
        {
            if (!m_bMultiMove)
            {
                if (null != m_pCurrentObject)
                {
                    m_pCurrentObject.obj_bSelect = false;
                    m_pCurrentObject.obj_bCopy = false;
                    MyObjectInvalidate(m_pCurrentObject.LocationInMap);
                }
                m_pCurrentObject = obj;
                m_pCurrentObject.obj_bSelect = true;
                m_pCurrentObject.obj_bCopy = true;
                DrawCurrentObject();
            }
        }
        /// <summary>
        /// 根据坐标查找选中对象
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private MyObject SeachObject(Point p)
        {
            for (int i = m_ObjectList.Count - 1; i >= 0; i--)
            {
                if (((p.X > m_ObjectList[i].LocationInMap.X && p.X < m_ObjectList[i].LocationInMap.X + objSize.Width) || (p.X < m_ObjectList[i].LocationInMap.X && p.X > m_ObjectList[i].LocationInMap.X + objSize.Width))
                    && ((p.Y > m_ObjectList[i].LocationInMap.Y && p.Y < m_ObjectList[i].LocationInMap.Y + objSize.Width) || (p.Y < m_ObjectList[i].LocationInMap.Y && p.Y > m_ObjectList[i].LocationInMap.Y + objSize.Width)))
                {
                    m_ObjectList[i].obj_bSelect = true;
                    m_ObjectList[i].obj_bCopy = true;
                    return m_ObjectList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 绘制选择框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        private void DrawSelectRect2(Graphics g, MyObject obj, Color color, Brush brush)
        {
            //上边线
            float t = (obj.LocationInMap.X + objSize.Width + obj.LocationInMap.X) / 2 - 2;
            g.DrawRectangle(new Pen(color), t, obj.LocationInMap.Y - 2, 4, 4);
            g.FillRectangle(brush, t, obj.LocationInMap.Y - 2, 3, 3);
            //下边线
            g.DrawRectangle(new Pen(color), t, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            g.FillRectangle(brush, t, obj.LocationInMap.Y + objSize.Width - 2, 3, 3);
            //左边线
            t = (obj.LocationInMap.Y + objSize.Width + obj.LocationInMap.Y) / 2 - 2;
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X - 2, t, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X - 2, t, 3, 3);
            //右边线
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X + objSize.Width - 2, t, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X + objSize.Width - 2, t, 3, 3);
            //左上角
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2, 3, 3);
            //右上角
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2, 3, 3);
            //右下角
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2, 3, 3);
            //左下角
            g.DrawRectangle(new Pen(color), obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            g.FillRectangle(brush, obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2, 3, 3);
        }

        /// <summary>
        /// 刷新制定矩形区域的画面, Point e
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void MyObjectInvalidate(PointF s)
        {
            var e = s;
            e.X += objSize.Width;
            e.Y += objSize.Height;
            Rectangle r = new Rectangle((int)(s.X - 3), (int)(s.Y - 3), (int)(e.X - s.X + 6) + 1, (int)(e.Y - s.Y + 6) + 1);
            this.Invalidate(r, false);
        }
        /// <summary>
        /// 初始化选择框
        /// </summary>
        /// <param name="StartPoint"></param>
        private void DrawStart(Point StartPoint)
        {
            this.Capture = true;
            //指定工作区为整个控件
            Cursor.Clip = RectangleToScreen(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            mouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }
        /// <summary>
        /// 在鼠标移动的时改变选择框的大小
        /// </summary>
        /// <param name="p">鼠标的位置</param>
        private void ResizeToRectangle(Point p)
        {
            DrawRectangle();
            mouseRect.Width = p.X - mouseRect.Left;
            mouseRect.Height = p.Y - mouseRect.Top;
            DrawRectangle();
        }
        /// <summary>
        /// 绘制选择框
        /// </summary>
        private void DrawRectangle()
        {
            Rectangle rect = RectangleToScreen(mouseRect);
            ControlPaint.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }
    }
}
