using System;
using System.Drawing;
using System.Xml;
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class MyObject
    {
        public enum ObjectType
        {
            UnKnow,
            A,
            CF = 21,
            CL,
            CM,
            E,
            EM,
            EM_CH4,
            EM_CO,
            EM_O2,
            EP,
            EP_R,
            EP_T,
            F,
            F_L,
            F_SB,
            F_YG,
            HL,
            I,
            P,
            PK,
            P_AF,
            P_CL,
            P_CO,
            P_EPM,
            P_GJ,
            P_GS,
            P_HL,
            P_HL2,
            P_JF,
            P_L,
            P_RL,
            P_LJQ,
            P_LLDI,
            P_LYJ,
            P_P,
            P_TD,
            P_TL2_Close,
            P_TL2_Down,
            P_TL2_Left,
            P_TL2_Right,
            P_TL2_UP,
            P_TL3_Down,
            P_TL3_Left,
            P_TL3_Right,
            P_TL3_UP,
            P_TL4_Down,
            P_TL4_Left,
            P_TL4_Right,
            P_TL4_UP,
            P_TL5_Down,
            P_TL5_Left,
            P_TL5_Right,
            P_TL5_UP,
            P_TL_Down,
            P_TL_Left,
            P_TL_Right,
            P_TL_UP,
            P_TW,
            P_VI,
            S,
            TV,
            TV_CCTV_Ball,
            TV_CCTV_E,
            TV_CCTV_Gun,
            VC,
            VI,
            toll,
            tunnel,
            bridge,
            services,
            slope
        }
        public Equ equ;
        private ObjectType equType;
        public ObjectType equtype
        {
            get { return equType; }
            set { equType = value; equ.EquTypeID = Enum.GetName(typeof(ObjectType), equtype); }
        }

        public string equTypeName { get; set; }
        public delegate void LocationInMapChangedEventHandler(object sender, MyObject e);
        public event LocationInMapChangedEventHandler LocationInMapChangeChanged;
        private Point locationInMap;
        public Point LocationInMap
        {
            get
            {
                return locationInMap;
            }
            set
            {
                locationInMap = value;
                if (LocationInMapChangeChanged != null)
                {
                    LocationInMapChangeChanged(this, this);
                }
            }
        }
        public string picName;
        public Image image;
        public bool obj_bSelect;
        public bool obj_bCopy;
        public MyObject()
        {
            equ = new Equ();
            LocationInMap = new Point(0, 0);
            picName = "";
            //if (LocationInMapChangeChanged!=null)
            //{
            //    this.LocationInMapChangeChanged += MyObject_LocationInMapChangeChanged;
            //}
        }
        ///// <summary>
        ///// 地图坐标改变对数据库位置进行改变
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MyObject_LocationInMapChangeChanged(object sender, EventArgs e)
        //{
        //    if (this.p != null && this.Parent.Parent is CustForm)
        //    {
        //        if (!(this.Parent.Parent as CustForm).ScaleChanging)
        //        {
        //            basicInfo.LocationInMap = LocationUtil.ConvertToMapLocation(this.LocationInMap, (this.Parent.Parent as CustForm).MapScale);
        //        }
        //    }
        //}

        public virtual void DrawOjbect(Graphics g)
        {
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Pic\\" + this.picName;
                image = Image.FromFile(filename);
                g.DrawImage(image, this.LocationInMap.X, this.LocationInMap.Y, 30, 30);
            }
            catch (Exception e)
            {
                Log.WriteLog("图片不存在" + e.Message);
            }
        }
        public virtual XmlElement SaveObject(XmlDocument doc)
        {
            XmlElement xmlElement = doc.CreateElement("obj");
            xmlElement.SetAttribute("equid", equ.EquID);
            xmlElement.SetAttribute("equtype", Convert.ToInt32(equtype).ToString());
            xmlElement.SetAttribute("pic", picName);
            xmlElement.SetAttribute("equName", equ.EquName);
            xmlElement.SetAttribute("pointX", equ.PointX.ToString());
            xmlElement.SetAttribute("pointY", equ.PointY.ToString());
            xmlElement.SetAttribute("PileNo", equ.PileNo);
            xmlElement.SetAttribute("DirectionId", equ.DirectionID.ToString());
            xmlElement.SetAttribute("Code", equ.Code);
            xmlElement.SetAttribute("AddressDiscribe", equ.AddressDiscribe);
            xmlElement.SetAttribute("AlarmMethod", equ.AlarmMethod);
            xmlElement.SetAttribute("IP", equ.IP);
            xmlElement.SetAttribute("Port", equ.Port.ToString());
            xmlElement.SetAttribute("FatherEquID", equ.FatherEquID);
            xmlElement.SetAttribute("TaskWV", equ.TaskWV.ToString());
            xmlElement.SetAttribute("msgTimeoutSec", equ.msgTimeoutSec.ToString());
            xmlElement.SetAttribute("Encode", equ.Encode);
            xmlElement.SetAttribute("Note", equ.Note);
            xmlElement.SetAttribute("plcStationAddress", equ.plcStationAddress);
            xmlElement.SetAttribute("Vendor", equ.Vendor);
            xmlElement.SetAttribute("RunMode", equ.RunMode);
            return xmlElement;
        }

        public virtual void OpenObject(XmlNode node)
        {

            if (equ == null)
            {
                equ = new Equ();
            }
            equ.EquID = ((XmlElement)node).GetAttribute("equid");
            equtype = (MyObject.ObjectType)Convert.ToInt32(((XmlElement)node).GetAttribute("equtype"));
            picName = ((XmlElement)node).GetAttribute("pic");
            equ.EquName = ((XmlElement)node).GetAttribute("equName");
            equ.PointX = ((XmlElement)node).GetAttribute("pointX");
            equ.PointY = ((XmlElement)node).GetAttribute("pointY");
            equ.PileNo = ((XmlElement)node).GetAttribute("PileNo");
            equ.DirectionID = Convert.ToInt32(((XmlElement)node).GetAttribute("DirectionId"));
            equ.Code = ((XmlElement)node).GetAttribute("Code");
            equ.AddressDiscribe = ((XmlElement)node).GetAttribute("AddressDiscribe");
            equ.AlarmMethod = ((XmlElement)node).GetAttribute("AlarmMethod");
            equ.IP = ((XmlElement)node).GetAttribute("IP");
            if (!string.IsNullOrEmpty(((XmlElement)node).GetAttribute("Port")))
            {
                equ.Port = Convert.ToInt32(((XmlElement)node).GetAttribute("Port"));
            }
            equ.FatherEquID = ((XmlElement)node).GetAttribute("FatherEquID");
            if (!string.IsNullOrEmpty(((XmlElement)node).GetAttribute("TaskWV")))
            {
                equ.TaskWV = Convert.ToInt32(((XmlElement)node).GetAttribute("TaskWV"));
            }
            if (!string.IsNullOrEmpty(((XmlElement)node).GetAttribute("msgTimeoutSec")))
            {
                equ.msgTimeoutSec = Convert.ToInt32(((XmlElement)node).GetAttribute("msgTimeoutSec"));
            }
            equ.Encode = ((XmlElement)node).GetAttribute("Encode");
            equ.Note = ((XmlElement)node).GetAttribute("Note");
            equ.plcStationAddress = ((XmlElement)node).GetAttribute("plcStationAddress");
            equ.Vendor = ((XmlElement)node).GetAttribute("Vendor");
            equ.RunMode = ((XmlElement)node).GetAttribute("RunMode");
        }
    }
}
