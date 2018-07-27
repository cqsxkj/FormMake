using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Linq;

using WindowMake.Entity;
using WindowMake.Device;
using WindowMake.DB;
using WindowMake.Config;
using WindowMake.Tool;

namespace WindowMake
{
    public class DinoComparer : IComparer<MyObject>
    {
        public int Compare(MyObject x, MyObject y)
        {
            return x.equ.EquID.CompareTo(y.equ.EquID);
        }
    }

    public class SelectEventArgs : EventArgs
    {
        private bool bMultiSelect;
        private bool bMultiCopy;

        public SelectEventArgs(bool select, bool copy)
        {
            bMultiSelect = select;
            bMultiCopy = copy;
        }
        public bool bSelect
        {
            get { return bMultiSelect; }
            set { bMultiSelect = value; }
        }
        public bool bCopy
        {
            get { return bMultiCopy; }
            set { bMultiCopy = value; }
        }
    }
    public class MyPanel : Panel
    {
        //设备缓存列表
        public List<MyObject> m_ObjectList = new List<MyObject>();
        private Image mapBackgroundImage = null;
        public double scale = 0.7;//缩放比例
        //设备大小
        public Size objSize { get { return new Size(30, 30); } }
        //public List<MyObject> m_tempList = new List<MyObject>();
        public MyObject[] m_SelectList = new MyObject[8];
        private int m_MoveUnit = 2;//方向键移动时的步长
        private enum DrawMode : int
        {
            Unkown = 0,//未知模式
            Drawing = 1,//作图中
            Select = 2,//已选择对象、但未操作
            Move = 3,//选择对象后，进行拖动操作
            //Zoom = 4,//选择对象后，进行缩放操作
        }
        private bool m_bCtrlDown = false;
        private bool m_bAltDown = false;
        private Point m_StartPt, M_EndPt;//进行框选时的起始点
        public bool m_bMultiMove = false;//多对象移动
        private bool m_bCopy = false;//是否有对象可以复制
        private DrawMode m_DrawMode = DrawMode.Unkown;
        private Point m_oldMousePoint = new Point(0, 0);
        public List<MyObject> SelectedObject = new List<MyObject>();//选中对象
        public Map mapPro = new Map();//画面背景属性 
        Dictionary<int, Command> commandDic = new Dictionary<int, Command>();

        /// <summary>
        /// 当前对象
        /// </summary>
        public MyObject m_pCurrentObject = null;

        public MyPanel()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            for (int i = 0; i < 8; i++)
                m_SelectList[i] = new MyObject();
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
                throw e;
            }
        }

        #region 地图缩放操作
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
        private void RefreshWindow()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            if (mapBackgroundImage != null)
            {
                this.BackgroundImage = CreateBackgroundImage(mapBackgroundImage, scale);
                this.Size = BackgroundImage.Size;
            }
            foreach (MyObject childControl in m_ObjectList)
            {
                childControl.LocationInMap = LocationUtil.ConvertToOutLocation(new Point((int)Convert.ToDouble(childControl.equ.PointX), (int)Convert.ToDouble(childControl.equ.PointY)), scale);
            }
        }
        #endregion
        //保存到数据库 -1 代表有异常失败
        internal int SaveDocument()
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
                Log.WriteLog(e);
                return -1;
                throw e;
            }
            return 1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            MyObject tempobj = null;
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                }
            }
            if (e.Button == MouseButtons.Left)
            {//左键按下
                if (m_bAltDown)
                {
                    m_StartPt = e.Location;
                    M_EndPt = e.Location;
                    m_DrawMode = DrawMode.Move;
                }
                else if (gMain.drawType != MyObject.ObjectType.UnKnow)//画状态
                {
                    ClearSelectObject();
                    DrawObject(gMain.drawType.ToString(), e.Location);
                    m_DrawMode = DrawMode.Drawing;
                }
                else
                {//对象选择
                    if (m_bCtrlDown)
                    {
                        tempobj = null;
                        tempobj = SeachObject(e.Location);
                        if (tempobj != null)
                        {
                            tempobj.obj_bSelect = !tempobj.obj_bSelect;
                            m_pCurrentObject = tempobj;
                            m_DrawMode = DrawMode.Move;
                        }
                    }
                    else
                    {
                        tempobj = null;
                        if (m_pCurrentObject != null)
                        {
                            if (!m_bMultiMove)
                            {
                                m_pCurrentObject.obj_bSelect = false;
                            }
                        }
                        tempobj = SeachObject(e.Location);
                        if (tempobj != null)
                        {
                            m_pCurrentObject = tempobj;
                            tempobj.obj_bSelect = true;
                            m_DrawMode = DrawMode.Move;
                        }
                        else
                        {
                            m_DrawMode = DrawMode.Unkown;
                            m_pCurrentObject = null;
                            ClearSelectObject();
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                tempobj = SeachObject(e.Location);
                if (tempobj != null)
                {
                    ChangeCurrentObject(tempobj);
                    m_pCurrentObject = tempobj;
                    m_DrawMode = DrawMode.Select;
                }
                else
                {
                    m_DrawMode = DrawMode.Unkown;
                    ClearSelectObject();
                }
            }

            m_oldMousePoint = e.Location;
            base.OnMouseDown(e);
        }

        public MyObject DrawObject(string equtype, Point p)
        {
            MyObject m_object = null;
            m_object = CreateObject(equtype, p);
            if (m_object != null)
            {
                var location = LocationUtil.ConvertToMapLocation(p, scale);
                m_object.equ.PointX = location.X.ToString();
                m_object.equ.PointY = location.Y.ToString();
                ChangeCurrentObject(m_object);
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
                    m_pCurrentObject = m_object;
                    m_ObjectList.Add(m_object);
                }
            }
            return m_object;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                if (m_DrawMode == DrawMode.Drawing && m_pCurrentObject != null)
                {
                    MyObjectInvalidate(m_pCurrentObject.LocationInMap);
                    m_pCurrentObject.DrawOjbect(g);
                    m_DrawMode = DrawMode.Select;
                    gMain.drawType = MyObject.ObjectType.UnKnow;
                }
                else if (m_bAltDown && m_DrawMode == DrawMode.Move)
                {
                    CreateSelectedObjectArea(m_StartPt, M_EndPt);
                    PaintSelectObject();
                    Invalidate(new Rectangle(m_StartPt.X - 1, m_StartPt.Y - 1, M_EndPt.X + 2, M_EndPt.Y + 2));
                    m_DrawMode = DrawMode.Select;
                }// || m_DrawMode == DrawMode.Zoom)
                else if (m_DrawMode == DrawMode.Move && m_pCurrentObject != null)
                {
                    if (m_bCtrlDown)
                    {
                        PaintSelectObject();
                        m_DrawMode = DrawMode.Select;
                    }
                    else
                    {
                        m_pCurrentObject.DrawOjbect(g);
                        m_DrawMode = DrawMode.Select;
                    }
                }
                else
                {
                    m_DrawMode = DrawMode.Unkown;
                    ClearSelectObject();
                }
                this.Invalidate();
            }
            if (m_pCurrentObject != null)
            {
                MyObjectInvalidate(m_pCurrentObject.LocationInMap);
                m_pCurrentObject.DrawOjbect(g);
            }
            if (SelectChanged != null)
                SelectChanged(this, new SelectEventArgs(m_bMultiMove, m_bCopy));
            base.OnMouseUp(e);
        }

        public event EventHandler<SelectEventArgs> SelectChanged;
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
                    m_ObjectList[i].obj_bSelect = false;
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    m_ObjectList[i].DrawOjbect(g);
                }
            }
            m_bMultiMove = false;
        }
        /// <summary>
        /// 绘制选中对象
        /// </summary>
        private void PaintSelectObject()
        {
            Graphics g = CreateGraphics();
            int n = 0;
            m_bCopy = false;
            int index = 9999999;
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    n++;
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    m_ObjectList[i].DrawOjbect(g);
                    index = i;
                }
                if (m_ObjectList[i].obj_bCopy)
                    m_bCopy = true;
            }
            if (n > 1)
                m_bMultiMove = true;
            else
                m_bMultiMove = false;
            if (m_pCurrentObject == null && index < m_ObjectList.Count && index >= 0)
                m_pCurrentObject = m_ObjectList[index];
        }
        /// <summary>
        /// 移动对象
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (m_DrawMode == DrawMode.Drawing)//画状态
            {
            }
            else if (m_DrawMode == DrawMode.Move)
            {
                if (m_bAltDown)
                {
                    this.Invalidate(new Rectangle(m_StartPt.X, m_StartPt.Y, M_EndPt.X + 1, M_EndPt.Y + 1));
                    M_EndPt = e.Location;
                }
                else
                {
                    int x = e.Location.X - m_oldMousePoint.X;
                    int y = e.Location.Y - m_oldMousePoint.Y;
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
                                location = LocationUtil.ConvertToMapLocation(location, scale);
                                m_ObjectList[i].equ.PointX = location.X.ToString();
                                m_ObjectList[i].equ.PointY = location.Y.ToString();
                                m_ObjectList[i].DrawOjbect(g);
                            }
                        }
                        m_oldMousePoint = e.Location;
                    }
                }
            }
            else
            {
                MyObject tempobject = null;
                tempobject = SeachObject(e.Location);

                this.Cursor = Cursors.Default;
                if (m_DrawMode == DrawMode.Select)
                    this.Cursor = Cursors.Arrow;

            }
            base.OnMouseMove(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                m_ObjectList[i].DrawOjbect(e.Graphics);
                if (m_ObjectList[i].obj_bSelect)
                {
                    DrawSelectRect(e.Graphics, m_ObjectList[i]);
                    DrawSelectRect2(e.Graphics, m_ObjectList[i]);
                }
            }
            if (m_bAltDown && m_DrawMode == DrawMode.Move)
            {
                Pen myPen = new Pen(Color.Black, 0.1F);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(myPen, new Rectangle(m_StartPt.X, m_StartPt.Y, M_EndPt.X - m_StartPt.X, M_EndPt.Y - m_StartPt.Y));
            }
            PaintSelectObject();
        }
        /// <summary>
        /// 绘制选择框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="obj"></param>
        private void DrawSelectRect(Graphics g, MyObject obj)
        {
            //上边线
            int t = obj.LocationInMap.X + objSize.Height - 2;
            m_SelectList[0].LocationInMap = new Point(t, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), t, obj.LocationInMap.Y - 2, 4, 4);
            //下边线
            m_SelectList[1].LocationInMap = new Point(t, obj.LocationInMap.Y + objSize.Height - 2);
            g.DrawRectangle(new Pen(Color.Black), t, obj.LocationInMap.Y + objSize.Height - 2, 4, 4);
            //左边线
            t = obj.LocationInMap.Y + objSize.Width - 2;
            m_SelectList[2].LocationInMap = new Point(obj.LocationInMap.X - 2, t);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, t, 4, 4);
            //右边线
            m_SelectList[3].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, t);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, t, 4, 4);
            //左上角
            m_SelectList[4].LocationInMap = new Point(obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2, 4, 4);
            //右上角
            m_SelectList[5].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2, 4, 4);
            //右下角
            m_SelectList[6].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            //左下角
            m_SelectList[7].LocationInMap = new Point(obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
        }
        private void DrawSelectRect2(Graphics g, MyObject obj)
        {
            //上边线
            int t = (obj.LocationInMap.X + objSize.Width + obj.LocationInMap.X) / 2 - 2;
            m_SelectList[0].LocationInMap = new Point(t, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), t, obj.LocationInMap.Y - 2, 4, 4);
            //下边线
            m_SelectList[1].LocationInMap = new Point(t, obj.LocationInMap.Y + objSize.Width - 2);
            g.DrawRectangle(new Pen(Color.Black), t, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            //左边线
            t = (obj.LocationInMap.Y + objSize.Width + obj.LocationInMap.Y) / 2 - 2;
            m_SelectList[2].LocationInMap = new Point(obj.LocationInMap.X - 2, t);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, t, 4, 4);
            //右边线
            m_SelectList[3].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, t);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, t, 4, 4);
            //左上角
            m_SelectList[4].LocationInMap = new Point(obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2, 4, 4);
            g.FillRectangle(Brushes.Black, obj.LocationInMap.X - 2, obj.LocationInMap.Y - 2, 4, 4);
            //右上角
            m_SelectList[5].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2, 4, 4);
            g.FillRectangle(Brushes.Black, obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y - 2, 4, 4);
            //右下角
            m_SelectList[6].LocationInMap = new Point(obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            g.FillRectangle(Brushes.Black, obj.LocationInMap.X + objSize.Width - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            //左下角
            m_SelectList[7].LocationInMap = new Point(obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2);
            g.DrawRectangle(new Pen(Color.Black), obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
            g.FillRectangle(Brushes.Black, obj.LocationInMap.X - 2, obj.LocationInMap.Y + objSize.Width - 2, 4, 4);
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
                    return m_ObjectList[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 绘制选中对象的选中效果
        /// </summary>
        /// <param name="s">起始坐标</param>
        /// <param name="e">结束坐标</param>
        /// <returns></returns>
        private void CreateSelectedObjectArea(Point s, Point e)
        {
            //PLCEqu obj = null;
            for (int i = m_ObjectList.Count - 1; i >= 0; i--)
            {
                // || ((m_ObjectList[i].LocationInMap.X > e.X && m_ObjectList[i].LocationInMap.Y > e.Y) && (m_ObjectList[i].LocationInMap.X + objSize.Width < s.X && m_ObjectList[i].LocationInMap.Y < s.Y))
                if ((m_ObjectList[i].LocationInMap.X + objSize.Width > s.X && m_ObjectList[i].LocationInMap.Y + objSize.Height > s.Y) && (m_ObjectList[i].LocationInMap.X < e.X && m_ObjectList[i].LocationInMap.Y < e.Y))
                {
                    m_ObjectList[i].obj_bSelect = true;
                    //obj = m_ObjectList[i];
                }
            }
            //return obj;
        }

        /// <summary>
        /// 刷新制定矩形区域的画面, Point e
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void MyObjectInvalidate(Point s)
        {
            var e = s;
            e.X += objSize.Width;
            e.Y += objSize.Height;
            Rectangle r = new Rectangle(s.X - 3, s.Y - 3, e.X - s.X + 6, e.Y - s.Y + 6);
            this.Invalidate(r, false);
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
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Right)
                return true;
            if (keyData == Keys.Alt || keyData == Keys.Control)
                return true;
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
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
                                m_ObjectList.Remove(m_ObjectList[j]);
                                //MyObjectInvalidate(m_ObjectList[j].LocationInMap);
                            }
                        }
                        m_pCurrentObject = null;
                        //if (m_pCurrentObject != null)
                        //{
                        //    m_ObjectList.Remove(m_pCurrentObject);
                        //    MyObjectInvalidate(m_pCurrentObject.LocationInMap);
                        //    m_pCurrentObject = null;
                        //}
                    }
                    break;
                case Keys.Menu:
                    m_bAltDown = true; //框选状态
                    break;
                case Keys.ControlKey:
                    m_bCtrlDown = true;
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
            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    m_bCtrlDown = false;
                    break;
                case Keys.Menu:
                    m_bAltDown = false;
                    break;
            }
            base.OnKeyUp(e);
        }
        //保存到文件
        public void SaveAsDocument(string fname)
        {
            XmlDocument doc = new XmlDocument(); // 创建dom对象

            XmlElement root = doc.CreateElement("root"); // 创建根节点album
            root.SetAttribute("version", gMain.g_version); // 设置属性
            doc.AppendChild(root);    //  加入到xml document
            XmlElement bk = doc.CreateElement("BK"); // 背景颜色
            bk.SetAttribute("MapId", mapPro.MapID);
            bk.SetAttribute("MapName", mapPro.MapName);
            bk.SetAttribute("IsRoad", mapPro.IsRoad.ToString());
            bk.SetAttribute("MapAddress", mapPro.MapAddress);
            bk.SetAttribute("bkfile", @"BK/" + mapPro.MapName + ".png");
            root.AppendChild(bk);
            XmlElement obj = doc.CreateElement("objList"); // 创建根节点album
            obj.SetAttribute("objNum", m_ObjectList.Count.ToString());
            root.AppendChild(obj);//对象节点
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                XmlElement preview = m_ObjectList[i].SaveObject(doc);
                obj.AppendChild(preview);   // 添加到xml document
            }
            doc.Save(fname);   // 保存文件

        }
        /// <summary>
        /// 打开文件中的配置
        /// </summary>
        /// <param name="fname"></param>
        public Map OpenDocument(string fname)
        {
            //Map map = new Map();
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
                Log.WriteLog(e.Message);
                throw;
            }
            DinoComparer dc = new DinoComparer();
            m_ObjectList.Sort(dc);
            return mapPro;
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
                    //m_object.equ.EquID = equ.EquID;
                    //m_object.equ.EquName = equ.EquName;
                    //m_object.equ.IP = equ.IP;
                    //m_object.equ.Port = equ.Port;
                    //m_object.equ.Vendor = equ.Vendor;
                    //m_object.equ.RunMode = equ.RunMode;
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
                Log.WriteLog(e);
                throw;
            }
        }
        #region 字符串生成
        /// <summary>
        /// 根据点位自动生成遥信字符串和遥控字符串
        /// </summary>
        public void AutoCreateYX()
        {
            try
            {
                int stateid = 0;
                List<PLCEqu> insertEqu = new List<PLCEqu>();
                for (int i = 0; i < m_ObjectList.Count; i++)
                {
                    if (m_ObjectList[i] is PLCEqu)
                    {
                        var plcEqu = m_ObjectList[i] as PLCEqu;
                        //生成遥信字符串
                        if (plcEqu.plc_pro.yxList.Count > 0)
                        {
                            continue;
                        }
                        if (plcEqu.plc_pro.yxcfgList.Count == 0)
                        {
                            continue;
                        }
                        #region
                        switch (plcEqu.equtype)
                        {
                            case MyObject.ObjectType.P:
                                break;
                            case MyObject.ObjectType.P_AF:
                                break;
                            case MyObject.ObjectType.P_CL:
                                break;
                            case MyObject.ObjectType.P_CO:
                                break;
                            case MyObject.ObjectType.P_GJ:
                                break;
                            case MyObject.ObjectType.P_HL:
                                stateid = 75;
                                AddYXStr(PlcString.strYXhl, plcEqu, stateid);
                                break;
                            case MyObject.ObjectType.P_HL2:
                                stateid = 80;
                                AddYXStr(PlcString.strYXhl2, plcEqu, stateid);
                                break;
                            case MyObject.ObjectType.P_JF:
                                CreateFaulte(plcEqu, 66);
                                stateid = 62;
                                AddYXStr(PlcString.strYXjf, plcEqu, stateid);
                                break;
                            case MyObject.ObjectType.P_L:
                                stateid = 57;
                                CreateFaultAndState(plcEqu, PlcString.strYX2byte, stateid, 61);
                                break;
                            case MyObject.ObjectType.P_LJQ:
                                stateid = 162;
                                CreateFaultAndState(plcEqu, PlcString.strYX2byte, stateid, 166);
                                break;
                            case MyObject.ObjectType.P_LLDI:
                                break;
                            case MyObject.ObjectType.P_LYJ:
                                stateid = 167;
                                CreateFaultAndState(plcEqu, PlcString.strYX2byte, stateid, 171);
                                break;
                            case MyObject.ObjectType.P_P:
                                break;
                            case MyObject.ObjectType.P_TD:
                                stateid = 93;
                                AddYXStr(PlcString.strYXTD, plcEqu, stateid);
                                break;
                            case MyObject.ObjectType.P_TL2_Close:
                                stateid = 188;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL2_Down:
                                stateid = 121;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL2_Left:
                                break;
                            case MyObject.ObjectType.P_TL2_Right:
                                break;
                            case MyObject.ObjectType.P_TL2_UP:
                                stateid = 127;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL3_Down:
                                break;
                            case MyObject.ObjectType.P_TL3_Left:
                                break;
                            case MyObject.ObjectType.P_TL3_Right:
                                break;
                            case MyObject.ObjectType.P_TL3_UP:
                                break;
                            case MyObject.ObjectType.P_TL4_Down:
                                break;
                            case MyObject.ObjectType.P_TL4_Left:
                                break;
                            case MyObject.ObjectType.P_TL4_Right:
                                break;
                            case MyObject.ObjectType.P_TL4_UP:
                                break;
                            case MyObject.ObjectType.P_TL5_Down:
                                break;
                            case MyObject.ObjectType.P_TL5_Left:
                                stateid = 180;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL5_Right:
                                stateid = 184;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL5_UP:
                                stateid = 172;
                                AddTLStr(PlcString.strYXtl, plcEqu, stateid, 1);
                                break;
                            case MyObject.ObjectType.P_TL_Down:
                                break;
                            case MyObject.ObjectType.P_TL_Left:
                                break;
                            case MyObject.ObjectType.P_TL_Right:
                                break;
                            case MyObject.ObjectType.P_TL_UP:
                                break;
                            case MyObject.ObjectType.P_TW:
                                break;
                            case MyObject.ObjectType.P_VI:
                                break;
                            default:
                                break;
                        }
                        #endregion
                        insertEqu.Add(plcEqu);
                    }
                }
                if (insertEqu.Count > 0)
                {
                    DBOPs db = new DBOPs();
                    db.InsertYX(insertEqu);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据点位自动生成遥控字符串
        /// </summary>
        public void AutoCreateYK()
        {
            try
            {
                DBOPs db = new DBOPs();
                int commandid = 0;
                List<PLCEqu> insertEqu = new List<PLCEqu>();
                for (int i = 0; i < m_ObjectList.Count; i++)
                {
                    if (m_ObjectList[i] is PLCEqu)
                    {
                        //生成遥信字符串
                        var plcEqu = m_ObjectList[i] as PLCEqu;
                        if (plcEqu.plc_pro.ykList.Count > 0)
                        {
                            continue;
                        }
                        #region
                        switch (plcEqu.equtype)
                        {
                            case MyObject.ObjectType.P:
                                break;
                            case MyObject.ObjectType.P_AF:
                                break;
                            case MyObject.ObjectType.P_CL:
                                break;
                            case MyObject.ObjectType.P_CO:
                                break;
                            case MyObject.ObjectType.P_GJ:
                                break;
                            case MyObject.ObjectType.P_HL:
                                commandid = 47;
                                AddYKStr(PlcString.strYKhl, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_HL2:
                                commandid = 51;
                                AddYKStr(PlcString.strYKhl2, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_JF:
                                commandid = 40;
                                AddYKStr(PlcString.strYKjf, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_L:
                                commandid = 34;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_LJQ:
                                commandid = 36;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_LLDI:
                                break;
                            case MyObject.ObjectType.P_LYJ:
                                commandid = 38;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_P:
                                break;
                            case MyObject.ObjectType.P_TD:
                                commandid = 58;
                                AddYKStr(PlcString.strYKTD, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL2_Close:
                                commandid = 114;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL2_Down:
                                commandid = 67;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL2_Left:
                                break;
                            case MyObject.ObjectType.P_TL2_Right:
                                break;
                            case MyObject.ObjectType.P_TL2_UP:
                                commandid = 71;
                                AddYKStr(PlcString.stryk2byte, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL3_Down:
                                break;
                            case MyObject.ObjectType.P_TL3_Left:
                                break;
                            case MyObject.ObjectType.P_TL3_Right:
                                break;
                            case MyObject.ObjectType.P_TL3_UP:
                                break;
                            case MyObject.ObjectType.P_TL4_Down:
                                break;
                            case MyObject.ObjectType.P_TL4_Left:
                                break;
                            case MyObject.ObjectType.P_TL4_Right:
                                break;
                            case MyObject.ObjectType.P_TL4_UP:
                                break;
                            case MyObject.ObjectType.P_TL5_Down:
                                break;
                            case MyObject.ObjectType.P_TL5_Left:
                                commandid = 105;
                                AddYKStr(PlcString.strYKtl1, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL5_Right:
                                commandid = 109;
                                AddYKStr(PlcString.strYKtl1, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL5_UP:
                                commandid = 97;
                                AddYKStr(PlcString.strYKtl, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL_Down:
                                commandid = 85;
                                AddYKStr(PlcString.strYKtl, plcEqu, commandid);
                                break;
                            case MyObject.ObjectType.P_TL_Left:
                                break;
                            case MyObject.ObjectType.P_TL_Right:
                                break;
                            case MyObject.ObjectType.P_TL_UP:
                                break;
                            case MyObject.ObjectType.P_TW:
                                break;
                            case MyObject.ObjectType.P_VI:
                                break;
                            default:
                                break;
                        }
                        #endregion
                        insertEqu.Add(plcEqu);
                    }
                }

                if (insertEqu.Count > 0)
                {
                    db.InsertYK(insertEqu);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("AutoCreateYK:" + e);
            }
        }
        /// <summary>
        /// 生成故障和状态
        /// </summary>
        /// <param name="plcEqu"></param>
        /// <param name="strs"></param>
        /// <param name="stateid"></param>
        /// <param name="faultid"></param>
        private void CreateFaultAndState(PLCEqu plcEqu, string[] strs, int stateid, int faultid)
        {
            var pl = plcEqu.plc_pro.yxcfgList.Where(p => p.IsError == 1).FirstOrDefault();
            if (pl != null)//有故障
            {
                CreateFaulte(plcEqu, faultid);
                Add2OR1Str(strs, stateid, plcEqu, 2);
            }
            else
            {
                Add2OR1Str(strs, stateid, plcEqu, 1);
            }
        }
        /// <summary>
        /// 创建故障
        /// </summary>
        /// <param name="plcEqu"></param>
        /// <param name="faultid"></param>
        private static void CreateFaulte(PLCEqu plcEqu, int faultid)
        {
            yx yx = new yx();
            yx.EquID = plcEqu.equ.EquID;
            yx.IsState = 0;//故障
            yx.YXInfoMesg = "1";
            yx.EquStateID = faultid;
            plcEqu.plc_pro.yxList.Add(yx);
        }

        private void AddTLStr(string[] strs, PLCEqu plcEqu, int stateid, int bitCount)
        {
            if (plcEqu.plc_pro.yxcfgList.Count > bitCount)
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    yx yx = new yx();
                    yx.EquID = plcEqu.equ.EquID;
                    yx.IsState = 1;
                    yx.YXInfoMesg = strs[i];
                    yx.EquStateID = stateid++;
                    plcEqu.plc_pro.yxList.Add(yx);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    yx yx2 = new yx();
                    yx2.EquID = plcEqu.equ.EquID;
                    yx2.IsState = 1;
                    yx2.YXInfoMesg = i == 0 ? "1" : "0";
                    yx2.EquStateID = stateid++;
                    plcEqu.plc_pro.yxList.Add(yx2);
                    //stateid++;
                }
            }
        }

        private void AddYKStr(string[] strs, PLCEqu plcEqu, int commandid)
        {
            if (plcEqu.plc_pro.ykcfgList.Count == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    yk yk = new yk();
                    yk.EquID = plcEqu.equ.EquID;
                    yk.AreaID = plcEqu.plc_pro.ykcfgList[0].AreaID;
                    yk.CommandID = commandid++;
                    yk.Mesg = commandDic[(int)yk.CommandID].Name;
                    yk.Points = i == 0 ? "1" : "0";
                    plcEqu.plc_pro.ykList.Add(yk);
                }
            }
            else if (plcEqu.plc_pro.ykcfgList.Count > 1)
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    yk yk = new yk();
                    yk.EquID = plcEqu.equ.EquID;
                    yk.AreaID = plcEqu.plc_pro.ykcfgList[0].AreaID;
                    yk.CommandID = commandid++;
                    yk.Mesg = commandDic[(int)yk.CommandID].Name;
                    yk.Points = strs[i];
                    plcEqu.plc_pro.ykList.Add(yk);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs">字符串</param>
        /// <param name="plcEqu">实体</param>
        /// <param name="stateid">状态id</param>
        private void AddYXStr(string[] strs, PLCEqu plcEqu, int stateid)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                yx yx = new yx();
                yx.EquID = plcEqu.equ.EquID;
                yx.IsState = 1;
                yx.YXInfoMesg = strs[i];
                yx.EquStateID = stateid++;
                if (plcEqu.equtype == MyObject.ObjectType.P_JF)
                {
                    if (i == strs.Length - 2)
                    {
                        yx.EquStateID = 202;
                    }
                    else if (i == strs.Length - 1)
                    {
                        yx.EquStateID = 232;
                    }
                }
                plcEqu.plc_pro.yxList.Add(yx);
            }
        }
        /// <summary>
        /// 根据点位生成2位字符串或者1位字符串
        /// </summary>
        /// <param name="str2byte">字符串数组</param>
        /// <param name="stateid">状态id</param>
        /// <param name="plcEqu">设备实体</param>
        /// <param name="bitCount">点位个数</param>
        private void Add2OR1Str(string[] str2byte, int stateid, PLCEqu plcEqu, int bitCount)
        {
            if (plcEqu.plc_pro.yxcfgList.Count > bitCount)
            {
                for (int i = 0; i < 4; i++)
                {
                    yx yx = new yx();
                    yx.EquID = plcEqu.equ.EquID;
                    yx.IsState = 1;
                    yx.YXInfoMesg = str2byte[i];
                    yx.EquStateID = stateid++;
                    plcEqu.plc_pro.yxList.Add(yx);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    yx yx2 = new yx();
                    yx2.EquID = plcEqu.equ.EquID;
                    yx2.IsState = 1;
                    yx2.YXInfoMesg = i == 0 ? "1" : "0";
                    yx2.EquStateID = stateid++;
                    plcEqu.plc_pro.yxList.Add(yx2);
                    stateid++;
                }
            }

        }
        #endregion

        /// <summary>
        /// 设置背景图片及大小
        /// </summary>
        /// <param name="mappic"></param>
        public void SetBackgroud(string mappic)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            try
            {
                mapBackgroundImage = Image.FromFile(mappic);
                this.BackgroundImage = CreateBackgroundImage(Image.FromFile(mappic), scale);
                this.Size = BackgroundImage.Size;
            }
            catch (Exception)
            {
                var tempimg = new Bitmap(1920, 1080);
                this.BackgroundImage = CreateBackgroundImage(tempimg, scale);
            }
        }
        /// <summary>
        /// 设置背景图片及大小
        /// </summary>
        /// <param name="mappic">图片地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Size SetBackgroud(string mappic, int width, int height)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            try
            {
                mapBackgroundImage = Image.FromFile(mappic);
                this.BackgroundImage = CreateBackgroundImage(Image.FromFile(mappic), scale);
                this.Size = mapBackgroundImage.Size;
                return this.Size;
            }
            catch (Exception)
            {
                var tempimg = new Bitmap(1920, 1080);
                this.BackgroundImage = CreateBackgroundImage(tempimg, scale);
                return this.BackgroundImage.Size;
            }
        }
        #region 工具栏
        public void toolStripButton4_Click()
        {//左对齐
            Point location = new Point(10000, 10000);
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
        }
        /// <summary>
        /// 刷新对齐x坐标
        /// </summary>
        /// <param name="location"></param>
        private void FlushLocation2X(Point location)
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    Point tempLocation = new Point(location.X, m_ObjectList[i].LocationInMap.Y);
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

        public void toolStripButton6_Click()
        {//右对齐
            Point location = new Point(0, 0);
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
        }

        public void toolStripButton7_Click()
        {//顶对齐
            Point location = new Point(10000, 10000);
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
        }
        /// <summary>
        /// 刷新对齐Y坐标
        /// </summary>
        /// <param name="location"></param>
        private void FlushLocation2Y(Point location)
        {
            Graphics g = CreateGraphics();
            for (int i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bSelect)
                {
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    Point tempLocation = new Point(m_ObjectList[i].LocationInMap.X, location.Y);
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
            int start_x = 999999;
            int end_x = 0;
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
            int nlen = (end_x - start_x) / (num - 1);
            nlen += len;
            for (i = 0; i < temp_array.Count; i++)
            {
                MyObjectInvalidate(temp_array[i].LocationInMap);
                Point tempLocation = new Point(start_x + i * nlen, temp_array[i].LocationInMap.Y);
                temp_array[i].LocationInMap = tempLocation;
                tempLocation = LocationUtil.ConvertToMapLocation(tempLocation, scale);
                temp_array[i].equ.PointX = tempLocation.X.ToString();
                temp_array[i].equ.PointY = tempLocation.Y.ToString();
                temp_array[i].DrawOjbect(g);
            }
        }

        public void toolStripButton9_Click()
        {//底对齐
            Point location = new Point(0, 0);
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
        }

        private static int CompareObjectByPostionX(MyObject x, MyObject y)
        {
            return x.LocationInMap.X.CompareTo(y.LocationInMap.X);
        }

        public void toolStripButton14_Click()
        {//增加水平间距
            HorizontalSpace(4);
        }

        public void toolStripButton15_Click()
        {//减少水平间距
            HorizontalSpace(-4);
        }
        private static int CompareObjectByPostionY(MyObject x, MyObject y)
        {
            return x.LocationInMap.Y.CompareTo(y.LocationInMap.Y);
        }

        #endregion
        public void toolCopyObject()
        {//复制 
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
        //粘贴
        public void toolPasteObject()
        {//粘贴
            int i = 0;
            MyObject m_object = null;
            Point start;
            int iMovePix = 50;
            for (i = 0; i < m_ObjectList.Count; i++)
            {
                if (m_ObjectList[i].obj_bCopy)
                {
                    m_ObjectList[i].obj_bSelect = false;
                    m_ObjectList[i].obj_bCopy = false;
                    MyObjectInvalidate(m_ObjectList[i].LocationInMap);
                    start = new Point(m_ObjectList[i].LocationInMap.X + iMovePix, m_ObjectList[i].LocationInMap.Y + iMovePix);
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
                        }
                    }
                }
            }
            this.Invalidate(new Rectangle(m_StartPt.X + iMovePix, m_StartPt.Y + iMovePix, M_EndPt.X + iMovePix, M_EndPt.Y + iMovePix));
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="equtype">对象类型</param>
        /// <param name="m_object">对象实体</param>
        /// <param name="start">坐标</param>
        /// <returns></returns>
        public MyObject CreateObject(string equtype, Point start)
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
                e.equ.LocationMap = LocationUtil.ConvertToMapLocation(e.LocationInMap, MapScale);
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
                    m_pCurrentObject = obj;
                    m_pCurrentObject.obj_bSelect = true;
                }
            }
        }
        /// <summary>
        /// 底图绘制
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        private Image CreateBackgroundImage(Image originalImage, double scale)
        {
            var resultImage = new Bitmap((int)(originalImage.Width * scale) + LocationUtil.MapStartX * 2, (int)(originalImage.Height * scale) + LocationUtil.MapStartY * 2);

            Graphics gh = Graphics.FromImage(resultImage);
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gh.Clear(Color.White);
            int destX = LocationUtil.MapStartX;
            int destY = LocationUtil.MapStartY;
            int destWidth = (int)(originalImage.Width * scale);
            int destHeight = (int)(originalImage.Height * scale);

            int sourceX = 0;
            int sourceY = 0;
            int sourceWidth = originalImage.Width;
            int sourceHeight = originalImage.Height;

            gh.DrawImage(originalImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            gh.Dispose();

            return resultImage;
        }
    }
}
