/* 功能：读取交大配置文件
 * 时间：2017年6月20日09:59:18
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using WindowMake.Config;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;

namespace WindowMake.Tool
{
    public class ReadDocument
    {
        DataStorage ds = new DataStorage();
        DBOPs dbop = new DBOPs();
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            string path = null;
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                }
                //OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Multiselect = true;//多选多个文件
                //ofd.Filter = "所有文件|*.*";
                //ofd.ValidateNames = true;
                //ofd.CheckPathExists = true;
                //ofd.CheckFileExists = true;
                //if ((bool)ofd.ShowDialog())
                //{
                //    path = ofd.FileNames;
                //}
            }
            catch (Exception)
            {
            }
            return path;
        }

        /// <summary>
        /// 读取地图中的配置
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="path">配置文件路径</param>
        /// <param name="mapHeight">地图高度</param>
        /// <param name="MapWidth">地图宽度</param>
        /// <param name="readEquType">该设备类型是否进行读取</param>
        /// <returns>读取是否成功</returns>
        public bool Read(string mapID, string path, int mapHeight, int MapWidth, ReadEquType readEquType)
        {
            XmlDocument doc = new XmlDocument();
            // 缓存设备
            List<MyObject> equList = new List<MyObject>();
            try
            {
                doc.Load(path);
                // 设备加载

                List<Equ> equs = ds.ReadEqu(Convert.ToInt32(mapID));
                List<yx> yxs = ds.GetYXs();
                List<Yx_cfg> yxcfgs = ds.GetYXcfgs();
                List<yk> yks = ds.GetYks();
                List<Yk_cfg> ykcfgs = ds.GetYKcfgs();
                List<yc> ycs = ds.GetYcs();
                List<Yc_cfg> yccfgs = ds.GetYccfgs();
                List<p_area_cfg> areas = ds.GetAllArea();
                Log.WriteLog("start readxml:" + DateTime.Now.ToString("HH:mm:ss.fff"));
                List<Yx_cfg> yxcfgdb = new List<Yx_cfg>();
                List<Yk_cfg> ykcfgdb = new List<Yk_cfg>();
                //交大地图的宽度和高度
                int width = Convert.ToInt32(doc.GetElementsByTagName("PicInfo")[0].Attributes["width"].InnerText);
                int hight = Convert.ToInt32(doc.GetElementsByTagName("PicInfo")[0].Attributes["hight"].InnerText);
                XmlNodeList xnl = doc.GetElementsByTagName("ob");
                if (xnl != null)
                {
                    //读取父设备信息
                    string tvFarther = ds.GetFartherID("TV");
                    string epFarther = ds.GetFartherID("EP");
                    string firFarther = ds.GetFartherID("F");
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        MyObject m_object = null;
                        //m_object = new MyObject();
                        string type = xnl[i].Attributes["Type"].Value;
                        XmlNode ConXml = xnl[i].SelectSingleNode("ControlObject");
                        XmlNode drawob = xnl[i].SelectSingleNode("DrawOb");
                        XmlNode name = drawob.SelectSingleNode("ObjectName");
                        XmlNode xy = drawob.SelectSingleNode("EndPoint");
                        if (Convert.ToInt32(xy.Attributes["X"].Value) < 0)
                        {
                            continue;
                        }
                        if (Convert.ToInt32(xy.Attributes["Y"].Value) < 0)
                        {
                            continue;
                        }

                        switch (type)
                        {
                            case "12836"://情报板
                                #region  情报板
                                if (!readEquType.CMS)
                                    continue;
                                m_object = new CMObject();
                                string strName = ConXml.Attributes["StrName"].Value;
                                if (strName.IndexOf("1") > 0)//门架
                                {
                                    m_object.equtype = MyObject.ObjectType.CM;
                                }
                                //else //if (strName.IndexOf("2") > 0)//限速标志
                                //{
                                //    m_object.equtype = "S";
                                //}
                                else //F板
                                {
                                    m_object.equtype = MyObject.ObjectType.CF;
                                }
                                if (strName.IndexOf("r") > 0)
                                {
                                    m_object.equ.DirectionID = 1;
                                }
                                if (strName.IndexOf("l") > 0)
                                {
                                    m_object.equ.DirectionID = 2;
                                }
                                #region 情报板配置信息读取
                                XmlDocument xd = new XmlDocument();
                                var cmsNode = ConXml.SelectSingleNode("ExtAttribute");
                                if (cmsNode == null)
                                {
                                    continue;
                                }
                                string cmsInfo = cmsNode.InnerText;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                xd.LoadXml(cmsInfo);
                                ((CMSEqu)m_object).cms_pro.CMSWidth = Convert.ToInt32(xd.GetElementsByTagName("CMS")[0].Attributes["Width"].InnerText);
                                ((CMSEqu)m_object).cms_pro.CMSHeight = Convert.ToInt32(xd.GetElementsByTagName("CMS")[0].Attributes["Height"].InnerText);
                                ((CMSEqu)m_object).cms_pro.EquID = m_object.equ.EquID;
                                ((CMSEqu)m_object).cms_pro.FontType = "楷体";
                                ((CMSEqu)m_object).cms_pro.FontColor = "黄";
                                ((CMSEqu)m_object).cms_pro.BackColor = "黑";
                                ((CMSEqu)m_object).cms_pro.FontSize = 24;
                                ((CMSEqu)m_object).cms_pro.ContentType = 1;
                                ((CMSEqu)m_object).cms_pro.CharBetween = 5;
                                ((CMSEqu)m_object).cms_pro.OutType = 1;
                                ((CMSEqu)m_object).cms_pro.OutSpeed = 20;
                                ((CMSEqu)m_object).cms_pro.StayTime = 200;
                                ((CMSEqu)m_object).cms_pro.BlankCount = 0;
                                ((CMSEqu)m_object).cms_pro.MinFontSize = 16;
                                ((CMSEqu)m_object).cms_pro.SupportPic = 1;
                                ((CMSEqu)m_object).cms_pro.PicLocation = "CmsListBitmap1";
                                #endregion
                                m_object.equ.TaskWV = 1;
                                #endregion
                                break;
                            case "12839"://限速标志
                                m_object = new MyObject();
                                if (!readEquType.S)
                                    continue;
                                m_object.equtype = MyObject.ObjectType.S;
                                m_object.equ.TaskWV = 1;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                break;
                            case "12838"://车检器
                                if (!readEquType.VC)
                                    continue;
                                m_object = new MyObject();
                                m_object.equtype = MyObject.ObjectType.VC;
                                m_object.equ.AlarmMethod = "NORMAL";
                                m_object.equ.TaskWV = 1;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                break;
                            case "12862"://气象仪
                                if (!readEquType.VI)
                                    continue;
                                m_object = new MyObject();
                                m_object.equtype = MyObject.ObjectType.VI;
                                m_object.equ.AlarmMethod = "YW";
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.TaskWV = 1;
                                break;
                            case "12834"://广播
                                continue;
                            case "12832"://紧急电话
                                if (!readEquType.EP)
                                    continue;
                                m_object = new EPEqu();
                                m_object.equtype = MyObject.ObjectType.EP_T;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                AddEPConfig((EPEqu)m_object, ConXml);
                                break;
                            case "12833"://摄像机
                                if (!readEquType.TV)
                                    continue;
                                m_object = new TVEqu();
                                switch (ConXml.Attributes["StrName"].Value)
                                {
                                    case "lcctv1_mov.bmp":
                                    case "rcctv1_mov.bmp":
                                        m_object.equtype = MyObject.ObjectType.TV_CCTV_Gun;
                                        break;
                                    case "rcctv2_mov.bmp":
                                    case "lcctv2_mov.bmp":
                                        m_object.equtype = MyObject.ObjectType.TV_CCTV_E;
                                        break;
                                    default:
                                        m_object.equtype = MyObject.ObjectType.TV_CCTV_Ball;
                                        break;
                                }
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                if (!ConXml.Attributes["Region"].Value.Equals("0"))
                                {
                                    AddCCTVConfig((TVEqu)m_object, ConXml.Attributes["Region"].Value);
                                }
                                break;
                            case "12835"://火灾
                                if (!readEquType.FIRE)
                                    continue;
                                //m_object = new MyObject();
                                XmlNode addressxy = drawob.SelectSingleNode("EndPoint");
                                int x = Convert.ToInt32(addressxy.Attributes["X"].Value) * MapWidth / width;
                                AddFireConfig(equList, ConXml, name.InnerText, x, ref firFarther, mapID);
                                continue;
                            #region plc
                            case "12837"://车道指示器
                                if (!readEquType.P_TL)
                                    continue;
                                m_object = new PLCEqu();
                                switch (ConXml.Attributes["StrName"].Value)
                                {
                                    case "lindicator_mov.bmp"://左
                                    case "uindicator_mov.bmp":
                                        m_object.equtype = MyObject.ObjectType.P_TL5_Left;
                                        break;
                                    case "rindicator_mov.bmp"://右
                                    case "dindicator_mov.bmp":
                                        m_object.equtype = MyObject.ObjectType.P_TL5_Right;
                                        break;
                                    case "sindicator_mov.bmp":
                                        m_object.equtype = MyObject.ObjectType.P_TL2_Close;
                                        break;
                                    default:
                                        continue;
                                }
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            case "12831"://交通灯
                                if (!readEquType.P_HL)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_HL2;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            case "12830"://照明
                                if (!readEquType.LIGHT)
                                    continue;
                                m_object = new PLCEqu();
                                if (name.InnerText.IndexOf("应") > 0 || name.InnerText.IndexOf("引") > 0)//基本
                                {
                                    m_object.equtype = MyObject.ObjectType.P_LYJ;
                                }
                                else if (name.InnerText.IndexOf("JQ") > 0 || name.InnerText.IndexOf("加") > 0)//加强
                                {
                                    m_object.equtype = MyObject.ObjectType.P_LJQ;
                                }
                                else//基本
                                {
                                    m_object.equtype = MyObject.ObjectType.P_L;
                                }
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            case "12829"://风机
                                if (!readEquType.P_JF)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_JF;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            case "12846"://GJ
                                if (!readEquType.GJ)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_GJ;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12843"://CO
                                if (!readEquType.CO)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_CO;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12844"://VI
                                if (!readEquType.VI)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_VI;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12848"://TW
                                if (!readEquType.TW)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_TW;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12842"://横通门
                                if (!readEquType.TD)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_TD;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            case "12845"://高位水池
                                if (!readEquType.LLID)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_LLDI;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12855"://水泵
                                if (!readEquType.P_P)
                                    continue;
                                m_object = new PLCEqu();
                                m_object.equtype = MyObject.ObjectType.P_P;
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddYCConfig(m_object as PLCEqu, ConXml, ycs, yccfgs, ref areas, equList, mapID);
                                break;
                            case "12840"://车行横通标志\转弯灯
                                if (!readEquType.P_TL2)
                                    continue;
                                m_object = new PLCEqu();
                                switch (ConXml.Attributes["StrName"].Value)
                                {
                                    case "uvehicleflag_mov.bmp"://向上
                                        m_object.equtype = MyObject.ObjectType.P_TL2_UP;
                                        break;
                                    case "dvehicleflag_mov.bmp"://向下
                                        m_object.equtype = MyObject.ObjectType.P_TL2_Down;
                                        break;
                                    default:
                                        continue;
                                }
                                m_object.equ.EquID = NameTool.CreateEquId(m_object.equtype);
                                m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                                m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;
                                AddConfig((PLCEqu)m_object, ConXml, yxcfgs, ykcfgs, equList, ref areas, mapID, yxcfgdb, ykcfgdb);
                                break;
                            #endregion
                            default:
                                continue;
                        }
                        m_object.equ.EquName = name.InnerText;
                        m_object.equ.PileNo = ConXml.Attributes["Badge"].Value;

                        m_object.equ.PointX = (Convert.ToInt32(xy.Attributes["X"].Value) * MapWidth / width).ToString();
                        m_object.equ.PointY = "-" + Convert.ToInt32(xy.Attributes["Y"].Value) * mapHeight / hight;

                        m_object.equ.MapID = mapID;
                        m_object.equ.plcStationAddress = ConXml.Attributes["Station"].Value;
                        m_object.equ.AddressDiscribe = xnl[i].Attributes["ID"].Value;
                        #region 子系统
                        if ("-1".Equals(ConXml.Attributes["Station"].Value) && m_object.equtype != MyObject.ObjectType.TV_CCTV_Gun)
                        {
                            if (m_object.equtype == MyObject.ObjectType.TV_CCTV_Gun || m_object.equtype == MyObject.ObjectType.TV_CCTV_E || m_object.equtype == MyObject.ObjectType.TV_CCTV_Ball || m_object.equtype == MyObject.ObjectType.EP_T)
                            {
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (m_object.equ.EquTypeID.StartsWith("TV_CCTV"))
                        {
                            if (tvFarther == null)
                            {
                                TVEqu tvInfo = new TVEqu();
                                tvInfo.equtype = MyObject.ObjectType.TV;
                                tvInfo.equ.EquID = NameTool.CreateEquId(tvInfo.equtype);
                                tvInfo.equ.EquName = "摄像机流媒体服务器";
                                tvInfo.equ.PointX = "0";
                                tvInfo.equ.PointY = "0";
                                tvInfo.equ.TaskWV = 1;
                                tvInfo.equ.MapID = mapID;
                                equList.Add(tvInfo);
                                tvFarther = tvInfo.equ.EquID;
                            }
                            m_object.equ.FatherEquID = tvFarther;
                        }
                        if (m_object.equ.EquTypeID.StartsWith("EP_"))
                        {
                            if (epFarther == null)
                            {
                                EPEqu epInfo = new EPEqu();
                                epInfo.equtype = MyObject.ObjectType.EP;
                                epInfo.equ.EquID = NameTool.CreateEquId(epInfo.equtype);
                                epInfo.equ.EquName = "紧急电话主机";
                                epInfo.equ.PointX = "50";
                                epInfo.equ.PointY = "0";
                                epInfo.equ.TaskWV = 1;
                                epInfo.equ.MapID = mapID;
                                equList.Add(epInfo);
                                epFarther = epInfo.equ.EquID;
                            }
                            m_object.equ.FatherEquID = epFarther;
                        }
                        if (m_object.equ.EquTypeID.StartsWith("F_"))
                        {

                        }
                        #endregion
                        equList.Add(m_object);
                    }
                    int InsertCount = 0;
                    if (equList.Count > 0)
                    {
                        InsertCount = dbop.InsertEqu(equList);
                        List<Tv_cctv_cfg> tvinfos = new List<Tv_cctv_cfg>();
                        List<f_c_cfg> fireinfos = new List<f_c_cfg>();
                        List<ep_c_cfg> epinfos = new List<ep_c_cfg>();
                        List<c_cfg> cms = new List<c_cfg>();
                        Log.WriteLog("yxcfgdb:" + DateTime.Now);
                        if (yxcfgdb.Count > 0)
                        {
                            dbop.InsertYXConfig(yxcfgdb);
                        }
                        Log.WriteLog("ykcfgdb:" + DateTime.Now);
                        if (ykcfgdb.Count > 0)
                        {
                            dbop.InsertYKConfig(ykcfgdb);
                        }
                        Log.WriteLog("子系统:" + DateTime.Now);
                        for (int i = 0; i < equList.Count; i++)
                        {
                            if (equList[i] is FireEqu)//插入火灾配置信息
                            {
                                fireinfos.Add((equList[i] as FireEqu).fire_pro);
                            }
                            else if (equList[i] is TVEqu)//插入摄像机配置信息
                            {
                                if ((equList[i] as TVEqu).tv_pro.CCTVID != null)
                                {
                                    tvinfos.Add((equList[i] as TVEqu).tv_pro);
                                }
                            }
                            else if (equList[i] is EPEqu)//插入紧急电话配置
                            {
                                epinfos.Add((equList[i] as EPEqu).ep_pro);
                            }
                            else if (equList[i] is CMSEqu)//插入plc配置信息
                            {
                                cms.Add((equList[i] as CMSEqu).cms_pro);
                            }
                        }
                        Log.WriteLog("火灾配置条数:" + dbop.InsertFire(fireinfos));
                        Log.WriteLog("摄像机配置条数：" + dbop.InsertCCTV(tvinfos));
                        Log.WriteLog("紧急电话配置条数：" + dbop.InsertEpList(epinfos));
                        Log.WriteLog("情报板配置条数：" + dbop.InsertC_cfgList(cms));
                    }
                    //Log.WriteLog("InsertCount:" + InsertCount);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置plc父设备信息配置
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="equList">设备列表</param>
        /// <param name="m_object">实体对象</param>
        /// <param name="note">YK,YX,YC  设置需要的分区类型</param>
        /// <returns>分区编号，-1表示失败</returns>
        private int SetPLCFartherInfo(string mapID, List<MyObject> equList, MyObject m_object, string note, ref List<p_area_cfg> areaList)
        {
            int areaID = -1;
            try
            {
                string farterEquid = string.Empty;
                farterEquid = (from a in equList where a.equtype == MyObject.ObjectType.P && a.equ.plcStationAddress == m_object.equ.plcStationAddress select a.equ.EquID).FirstOrDefault();
                if (string.IsNullOrEmpty(farterEquid))
                {
                    PLCEqu plcInfo = new PLCEqu();
                    plcInfo.equtype = MyObject.ObjectType.P;
                    plcInfo.equ.EquID = NameTool.CreateEquId(plcInfo.equtype);
                    plcInfo.equ.EquName = plcInfo.equ.EquID;
                    plcInfo.equ.plcStationAddress = m_object.equ.plcStationAddress;
                    plcInfo.equ.AddressDiscribe = m_object.equ.plcStationAddress;
                    plcInfo.equ.PointX = (Convert.ToInt32(m_object.equ.PointX) + 20).ToString();
                    plcInfo.equ.PointY = m_object.equ.PointY;
                    plcInfo.equ.TaskWV = 1;
                    plcInfo.equ.MapID = mapID;
                    equList.Add(plcInfo);
                    farterEquid = plcInfo.equ.EquID;
                }
                m_object.equ.FatherEquID = farterEquid;

                var temp = (from a in areaList where a.equid == farterEquid && a.note == note select a).FirstOrDefault();
                if (temp == null)
                {
                    p_area_cfg areainfo = new p_area_cfg();
                    areainfo = (from a in PlcString.p_config.areas where a.note == note select a).FirstOrDefault();
                    areainfo.equid = m_object.equ.FatherEquID;
                    if (areainfo != null)
                    {
                        areaID = dbop.InsertArea(areainfo);
                        areaList.Add(new p_area_cfg
                        {
                            equid = areainfo.equid,
                            id = areaID,
                            note = areainfo.note,
                            point = areainfo.point,
                            yclength = areainfo.yclength,
                            ycstartAddress = areainfo.ycstartAddress,
                            yxcommlength = areainfo.yxcommlength,
                            yxlength = areainfo.yxlength,
                            yxstartAddress = areainfo.yxstartAddress
                        });
                        //areaList.Clear();
                        //areaList = ds.GetAllArea();
                    }
                }
                else
                {
                    areaID = temp.id;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("SetPLCFartherInfo:" + e);
                return -1;
            }
            return areaID;
        }

        /// <summary>
        /// 事件检测摄像机编号配置
        /// </summary>
        /// <param name="m_object"></param>
        /// <param name="value"></param>
        private void AddCCTVConfig(TVEqu m_object, string value)
        {
            m_object.tv_pro.CCTVID = m_object.equ.EquID;
            m_object.tv_pro.EventLinkCameraID = value;
        }

        private void AddFireConfig(List<MyObject> m_objList, XmlNode ConXml, string name, int x, ref string fartherEquID, string mapid)
        {
            int numMax = 0, numMin = 0;
            XmlNodeList PointNode = ConXml.SelectSingleNode("SY").ChildNodes;
            if (PointNode != null)
            {
                bool down;
                if (ConXml.Attributes["StrName"].Value.IndexOf('u') < 0)
                {
                    down = false;
                }
                else
                {
                    down = true;
                }
                if (fartherEquID == null)
                {
                    FObject fire = new FObject();
                    fire.equ.EquTypeID = "F";
                    fire.equ.EquID = NameTool.CreateEquId(fire.equtype);
                    fire.equ.EquName = "火灾主机";
                    fire.equ.PointX = "100";
                    fire.equ.PointY = "0";
                    fire.equ.MapID = mapid;
                    m_objList.Add(fire);
                    fartherEquID = fire.equ.EquID;
                }
                if (PointNode[1].Attributes["YX"].Value != "65535")
                {
                    int temp = 0;
                    numMax = Convert.ToInt32(PointNode[1].Attributes["YX"].Value);
                    numMin = Convert.ToInt32(PointNode[0].Attributes["YX"].Value);
                    for (; numMin <= numMax; numMin++)
                    {
                        FsbObject equinfo = new FsbObject();
                        equinfo.equ.EquTypeID = "F_SB";
                        equinfo.equ.EquID = NameTool.CreateEquId(equinfo.equtype);
                        equinfo.equ.EquName = name + numMin;
                        equinfo.equ.PointX = (x + temp * 20).ToString();
                        equinfo.equ.PointY = down == true ? "-1100" : "100";
                        equinfo.fire_pro.EquID = equinfo.equ.EquID;
                        equinfo.equ.MapID = mapid;
                        equinfo.equ.AddressDiscribe = 2 + "" + numMin.ToString("0000");
                        equinfo.fire_pro = new f_c_cfg
                        {
                            EquID = equinfo.equ.EquID,
                            FireNum = 2 + "" + numMin.ToString("0000"),
                            Note = equinfo.equ.EquName
                        };
                        temp++;
                        m_objList.Add(equinfo);
                    }
                }
                if (PointNode[3].Attributes["YX"].Value != "65535")
                {
                    int temp = 0;
                    numMax = Convert.ToInt32(PointNode[3].Attributes["YX"].Value);
                    numMin = Convert.ToInt32(PointNode[2].Attributes["YX"].Value);
                    for (; numMin <= numMax; numMin++)
                    {
                        FlObject equinfo = new FlObject();
                        equinfo.equ.EquTypeID = "F_L";
                        equinfo.equ.EquID = NameTool.CreateEquId(equinfo.equtype);
                        equinfo.equ.EquName = name + numMin;
                        equinfo.equ.PointX = (x + temp * 20).ToString();
                        equinfo.equ.PointY = down == true ? "-1150" : "50";
                        equinfo.equ.MapID = mapid;
                        equinfo.equ.AddressDiscribe = 4 + "" + numMin.ToString("0000");
                        equinfo.fire_pro = new f_c_cfg
                        {
                            EquID = equinfo.equ.EquID,
                            FireNum = 4 + "" + numMin.ToString("0000"),
                            Note = equinfo.equ.EquName
                        };
                        temp++;
                        m_objList.Add(equinfo);
                    }
                }
                if (PointNode[5].Attributes["YX"].Value != "65535")
                {
                    int temp = 0;
                    numMax = Convert.ToInt32(PointNode[5].Attributes["YX"].Value);
                    numMin = Convert.ToInt32(PointNode[4].Attributes["YX"].Value);
                    for (; numMin <= numMax; numMin++)
                    {
                        FygObject equinfo = new FygObject();
                        equinfo.equtype = MyObject.ObjectType.F_YG;
                        equinfo.equ.EquID = NameTool.CreateEquId(equinfo.equtype);
                        equinfo.equ.EquName = 3 + name + numMin.ToString("0000");
                        equinfo.equ.PointX = (x + temp * 30).ToString();
                        equinfo.equ.PointY = down == true ? "-1200" : "0";
                        equinfo.equ.MapID = mapid;
                        equinfo.equ.AddressDiscribe = 3 + "" + numMin.ToString("0000");
                        equinfo.fire_pro = new f_c_cfg
                        {
                            EquID = equinfo.equ.EquID,
                            FireNum = 3 + "" + numMin,
                            Note = equinfo.equ.EquName
                        };
                        temp++;
                        m_objList.Add(equinfo);
                    }
                }
            }
        }
        /// <summary>
        /// 新增紧急电话配置信息
        /// </summary>
        /// <param name="m_object"></param>
        /// <param name="ConXml"></param>
        private void AddEPConfig(EPEqu m_object, XmlNode ConXml)
        {
            XmlNodeList PointNode = ConXml.SelectSingleNode("SY").ChildNodes;
            if (PointNode != null)
            {
                m_object.ep_pro.EquID = m_object.equ.EquID;
                m_object.ep_pro.EPNum = PointNode[0].Attributes["YX"].Value.PadLeft(4, '0');
                m_object.ep_pro.Mesg = m_object.equ.EquName;
            }
        }

        /// <summary>
        /// 新增遥测配置
        /// </summary>
        /// <param name="m_object"></param>
        /// <param name="ConXml"></param>
        /// <param name="container"></param>
        private void AddYCConfig(PLCEqu m_object, XmlNode ConXml, List<yc> ycs, List<Yc_cfg> yccfgs, ref List<p_area_cfg> areas, List<MyObject> equlist, string mapid)
        {
            try
            {
                XmlNodeList PointNode = ConXml.SelectSingleNode("SY").ChildNodes;
                m_object.equ.plcStationAddress = ConXml.Attributes["Station"].Value;
                int areaID = SetPLCFartherInfo(mapid, equlist, m_object, "YC", ref areas);
                if (PointNode != null)
                {
                    var equYc = PlcString.p_config.ycList.Where(p => p.YCField == m_object.equ.EquTypeID).FirstOrDefault();
                    if (equYc != null)
                    {
                        equYc.YCFun = float.Parse(PointNode[0].Attributes["Bb"].Value);
                        equYc.EquID = m_object.equ.EquID;
                        var ycinfo = (from a in ycs where a.EquID == m_object.equ.EquID select a).FirstOrDefault();
                        if (ycinfo == null)
                        {
                            //入库
                            equYc.YCID = dbop.InsertYC(equYc);
                            ycinfo = equYc;
                            ycs.Add(new yc
                            {
                                EquID = equYc.EquID,
                                YCID = equYc.YCID,
                                YCCollecDown = equYc.YCCollecDown,
                                YCCollecUP = equYc.YCCollecUP,
                                YCField = equYc.YCField,
                                YCFun = equYc.YCFun,
                                YCRealDown = equYc.YCRealDown,
                                YCRealUP = equYc.YCRealUP
                            });
                        }
                        var yccfg = (from a in yccfgs where a.EquID == m_object.equ.EquID select a).FirstOrDefault();
                        if (yccfg == null)
                        {
                            Yc_cfg yc_cfg = new Yc_cfg
                            {
                                AddrAndBit = Convert.ToInt32(PointNode[0].Attributes["YC"].InnerText).ToString("X"),
                                EquID = m_object.equ.EquID,
                                YcID = ycinfo.YCID,
                                AreaID = areaID
                            };
                            if (dbop.InsertYCCfg(yc_cfg) != -1)
                            {
                                yccfgs.Add(yc_cfg);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
            }
        }
        /// <summary>
        /// 新增PLC配置
        /// </summary>
        /// <param name="m_object">设备总实体类</param>
        /// <param name="ConXml">xml文件句柄</param>
        /// <param name="yx_cfgs">所有遥信配置</param>
        /// <param name="yk_cfgs">所有遥控配置</param>
        private void AddConfig(PLCEqu m_object, XmlNode ConXml, List<Yx_cfg> yx_cfgs, List<Yk_cfg> yk_cfgs, List<MyObject> equlist, ref List<p_area_cfg> areas, string mapid, List<Yx_cfg> yxcfgdb, List<Yk_cfg> ykcfgdb)
        {
            string yxTempNum, ykTempNum;
            m_object.equ.plcStationAddress = ConXml.Attributes["Station"].Value;
            XmlNodeList PointNode = ConXml.SelectSingleNode("SY").ChildNodes;
            List<Yx_cfg> yxlist = new List<Yx_cfg>();
            List<Yk_cfg> yklist = new List<Yk_cfg>();

            int yxareaID = SetPLCFartherInfo(mapid, equlist, m_object, "YX", ref areas);
            int ykareaID = SetPLCFartherInfo(mapid, equlist, m_object, "YK", ref areas);
            for (int ii = 0; ii < PointNode.Count; ii++)
            {
                yxTempNum = PointNode[ii].Attributes["YX"].Value;
                ykTempNum = PointNode[ii].Attributes["YK"].Value;
                if (!yxTempNum.Equals("65535"))
                {
                    var address = Convert.ToInt32(yxTempNum).ToString("X") + "." + Convert.ToInt32(PointNode[ii].Attributes["YXP"].Value).ToString("X");
                    // 去掉重复点位
                    if (yxlist.Where(p => p.AddrAndBit == address).FirstOrDefault() == null)
                    {
                        var contain = (from a in yx_cfgs where a.EquID == m_object.equ.EquID && a.AddrAndBit == address select a).FirstOrDefault();
                        if (contain == null)
                        {
                            yxlist.Add(new Yx_cfg
                            {
                                EquID = m_object.equ.EquID,
                                AddrAndBit = address,
                                IsError = 0,
                                AreaID = yxareaID
                            });
                        }
                    }
                }
                if (!ykTempNum.Equals("65535"))
                {
                    var address = Convert.ToInt32(ykTempNum).ToString("X") + "." + Convert.ToInt32(PointNode[ii].Attributes["YKP"].Value).ToString("X");
                    if (yklist.Where(p => p.AddrAndBit == address).FirstOrDefault() == null)
                    {
                        var contain = (from a in yk_cfgs where a.EquID == m_object.equ.EquID && a.AddrAndBit == address select a).FirstOrDefault();
                        if (contain == null)
                        {
                            yklist.Add(new Yk_cfg
                            {
                                EquID = m_object.equ.EquID,
                                AddrAndBit = address,
                                AreaID = ykareaID
                            });
                        }
                    }
                }
            }
            //点位排序
            var yxorder = yxlist.OrderBy(p => p.AddrAndBit).ToList();
            var ykorder = yklist.OrderBy(p => p.AddrAndBit).ToList();
            for (int i = 0; i < yxorder.Count(); i++)
            {
                yxorder[i].Order = i;
            }
            //if (((m_object.equtype.ToString()).StartsWith("P_L")) && yxorder.Count() > 1)
            //{
            //    yxorder[yxorder.Count() - 1].IsError = 1;
            //}
            if (((m_object.equtype.ToString()).StartsWith("P_JF")) && yxorder.Count() > 1)
            {
                yxorder[yxorder.Count() - 1].IsError = 1;
            }
            for (int i = 0; i < ykorder.Count(); i++)
            {
                ykorder[i].Order = i;
            }
            if (yxorder.Count > 0)//该设备有遥信配置进行添加
            {
                yx_cfgs.AddRange(yxorder);
                //数据库操作
                yxcfgdb.AddRange(yxorder);
                //dbop.InsertYXConfig(yxorder);
            }
            if (ykorder.Count > 0)//该设备有遥控配置进行添加
            {
                yk_cfgs.AddRange(ykorder);
                //数据库操作
                ykcfgdb.AddRange(ykorder);
                //dbop.InsertYKConfig(ykorder);
            }
        }
    }
}
