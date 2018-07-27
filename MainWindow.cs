using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMake.Config;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;
using WindowMake.Propert;
using WindowMake.Tool;

namespace WindowMake
{
    public partial class MainWindow : Form
    {
        private int FormCount;
        private FormView m_CurrentView;
        public MainWindow()
        {
            Log.WriteLog("【系统启动】");
            InitializeComponent();
            try
            {
                //读取厂家配置
                //ProtocalConfig.ProtocalFamilies = XmlUtil.XmlDeserializerEx<Collection<ProtocalFamily>>(AppDomain.CurrentDomain.BaseDirectory + @"Config\ProtocalConfig.xml");
                //读取配置文件中的plc点位配置
                PlcString.p_config = XmlUtil.XmlDeserializerEx<PLCConfig>(AppDomain.CurrentDomain.BaseDirectory + @"Config\PLCTemplate.xml");
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
            }
        }

        private void Menu_New_Click(object sender, EventArgs e)
        {
            CreateView();
        }
        /// <summary>
        /// 新建画面
        /// </summary>
        private void CreateView()
        {
            try
            {
                FormView frmTemp = new FormView(); //新建一个窗体对象，可根据需要新建自己设计的窗体 
                frmTemp.MdiParent = this; //设置窗口的MdiParent属性为当前主窗口，成为MDI子窗体 
                frmTemp.Text = "画面" + FormCount.ToString(); //设定MDI窗体的标题 
                frmTemp.SelectChanged += new System.EventHandler<SelectEventArgs>(this.FormView_ObjectSelectChanged);
                FormCount++; //FormCount是定义在主程序中的一个变量来记录产生的子窗口个数
                m_CurrentView = frmTemp;
                frmTemp.Show(); //把此MDI窗体显示出来
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void FormView_ObjectSelectChanged(object sender, SelectEventArgs e)
        {
            //if (e.bSelect)
            //{
            //    this.toolStripButton4.Enabled = true;
            //    this.toolStripButton5.Enabled = true;
            //    this.toolStripButton6.Enabled = true;
            //    this.toolStripButton7.Enabled = true;
            //    this.toolStripButton8.Enabled = true;
            //    this.toolStripButton9.Enabled = true;
            //    this.toolStripButton10.Enabled = true;
            //    this.toolStripButton11.Enabled = true;
            //    this.toolStripButton13.Enabled = true;
            //    this.toolStripButton14.Enabled = true;
            //    this.toolStripButton15.Enabled = true;
            //    this.CopyToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    this.toolStripButton4.Enabled = false;
            //    this.toolStripButton5.Enabled = false;
            //    this.toolStripButton6.Enabled = false;
            //    this.toolStripButton7.Enabled = false;
            //    this.toolStripButton8.Enabled = false;
            //    this.toolStripButton9.Enabled = false;
            //    this.toolStripButton10.Enabled = false;
            //    this.toolStripButton11.Enabled = false;
            //    this.toolStripButton13.Enabled = false;
            //    this.toolStripButton14.Enabled = false;
            //    this.toolStripButton15.Enabled = false;
            //    this.CopyToolStripMenuItem.Enabled = false;
            //}
            if (e.bCopy)
                this.PasteToolStripMenuItem.Enabled = true;
            else
                this.PasteToolStripMenuItem.Enabled = false;
        }
        private void Menu_Cascade_Click(object sender, EventArgs e)// MDI窗体的层叠操作
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void Menu_TileH_Click(object sender, EventArgs e)// MDI窗体的水平平铺操作
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Menu_TileV_Click(object sender, EventArgs e)// MDI窗体的垂直平铺操作
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void Menu_ArrangeIcon_Click(object sender, EventArgs e)// MDI窗体的垂直平铺操作
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        /// <summary>
        /// 选择设备类型后点击画面生成设备控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (1 < e.Node.Level)
            {
                gMain.drawType = (MyObject.ObjectType)Enum.Parse(typeof(MyObject.ObjectType), e.Node.Name);
               
            }
            else
                gMain.drawType = MyObject.ObjectType.UnKnow;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //CreateView();
            this.treeView1.ExpandAll();
        }
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            this.toolStripStatusLabel1.Text = e.Location.ToString();
        }
        //保存到数据库
        private void Menu_Save_Click(object sender, EventArgs e)
        {
            if (m_CurrentView != null)
            {
                m_CurrentView.SaveDocument();
                #region 保存文件
                //if (m_CurrentView.fileName.Length < 1)
                //{
                //    SaveFileDialog savedialog = new SaveFileDialog();
                //    savedialog.Filter = "zw files (*.zw)|*.zw|All files (*.*)|*.*";
                //    savedialog.FilterIndex = 1;
                //    savedialog.RestoreDirectory = true;
                //    savedialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                //    if (savedialog.ShowDialog() == DialogResult.OK)
                //    {
                //        m_CurrentView.SaveAsDocument(savedialog.FileName);
                //    }
                //}
                //else
                //{
                //    m_CurrentView.SaveAsDocument(m_CurrentView.fileName);
                //}
                #endregion
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Open_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Title = "请选择文件";
                opendialog.Filter = "所有文件(*.*)|*.*";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    CreateView();
                    if (m_CurrentView != null)
                    {
                        Map map = m_CurrentView.OpenDocument(opendialog.FileName);
                        m_CurrentView.Name = map.MapID;
                        m_CurrentView.Text = map.MapName;
                        m_CurrentView.fileName = opendialog.FileName;
                        m_CurrentView.OpenDB(map);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 另存为文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_SaveAs_Click(object sender, EventArgs e)
        {
            if (m_CurrentView != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Filter = "zw files (*.zw)|*.zw|All files (*.*)|*.*";
                savedialog.FilterIndex = 1;
                savedialog.RestoreDirectory = true;
                savedialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                try
                {
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        m_CurrentView.SaveAsDocument(savedialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #region 工具栏
        private void toolStripButton4_Click(object sender, EventArgs e)
        {//左对齐
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(4);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {//水平居中
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(5);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {//右对齐
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(6);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {//顶对齐
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(7);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {//垂直居中
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(8);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {//底对齐
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(9);
            }
        }
        
        private void toolStripButton14_Click(object sender, EventArgs e)
        {//增加水平间距
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(14);
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {//减少水平间距
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(15);
            }
        }

        //放大
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(19);
            }
        }
        //缩小
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(20);
            }
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {//复制对象
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(50);
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {//粘贴对象
            if (m_CurrentView != null)
            {
                m_CurrentView.MultiObjectSet(51);
            }
        }
        #endregion
        /// <summary>
        /// 打开数据库中的地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOpenMap_Click(object sender, EventArgs e)
        {
            OpenDBData();
        }
        /// <summary>
        /// 打开数据库数据
        /// </summary>
        private void OpenDBData()
        {
            try
            {
                OpenMap openMap = new OpenMap();
                openMap.BindMap();
                openMap.StartPosition = FormStartPosition.CenterParent;
                if (openMap.ShowDialog() == DialogResult.OK)
                {
                    Map map = (Map)openMap.comboBoxMapList.SelectedItem;
                    CreateView();
                    if (m_CurrentView != null)
                    {
                        m_CurrentView.Name = map.MapID;
                        m_CurrentView.Text = map.MapName;
                        m_CurrentView.SetContextMenu(int.Parse(map.MapID));
                        m_CurrentView.OpenDB(map);
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("打开数据库数据错误："+e);
            }
        }

        //根据点位自动生成遥信字符串和遥控字符串
        private void AutoCreateYXYKTool_Click(object sender, EventArgs e)
        {
            //生成遥信字符串
            //生成遥控字符串
            m_CurrentView.AutoCreateYXYK();
            MessageBox.Show("自动生成遥信、遥控字符串完成！");


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenDBData();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MDBHelper mdb = new MDBHelper(AppDomain.CurrentDomain.BaseDirectory + "/Config/Config.mdb");
                mdb.Open();
                List<火灾地址映射表> ds = (List<火灾地址映射表>)mdb.Query<火灾地址映射表>("select * from 火灾地址映射表;");
                for (int i = 0; i < ds.Count; i++)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void yX取反ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
