/* 功能描述：插入数据操作类
 * 时间：2017-11-29 10:41:17
 * 
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.Text;
using WindowMake.Device;
using WindowMake.Entity;

namespace WindowMake.DB
{
    public class DBOPs
    {
        /// <summary>
        /// 插入情报板配置
        /// </summary>
        /// <param name="cfgList"></param>
        /// <returns></returns>
        public int InsertC_cfgList(List<c_cfg> cfgList)
        {
            int value = -1; ;
            try
            {
                string sql = string.Empty;
                foreach (c_cfg cfg in cfgList)
                {
                    sql += CreateInsertC_cfgSql(cfg);
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    Log.WriteLog("InsertC_cfgList:" + sql);
                    value = DBHelper.ExcuteTransactionSql(sql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("InsertC_cfgList:" + e);
            }
            return value;
        }
        /// <summary>
        /// 插入情报板配置
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public int InsertC_cfg(c_cfg cfg)
        {
            int value = -1; ;
            try
            {
                string strSql = CreateInsertC_cfgSql(cfg);
                if (!string.IsNullOrEmpty(strSql))
                {
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }

        /// <summary>
        /// 插入火灾配置信息
        /// </summary>
        /// <param name="fire"></param>
        /// <returns></returns>
        public int InsertFire(f_c_cfg fire)
        {
            int value = -1; ;
            try
            {
                string strSql = CreateInsertFireSql(fire);
                if (!string.IsNullOrEmpty(strSql))
                {
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }
        /// <summary>
        /// 插入火灾配置信息
        /// </summary>
        /// <param name="fireList"></param>
        /// <returns></returns>
        public int InsertFire(List<f_c_cfg> fireList)
        {
            int value = -1; ;
            try
            {
                string strSql = string.Empty;
                foreach (f_c_cfg fire in fireList)
                {
                    strSql += CreateInsertFireSql(fire);
                }
                if (!string.IsNullOrEmpty(strSql))
                {
                    Log.WriteLog("InsertFire:" + strSql);
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("InsertFire:" + e);
            }
            return value;
        }

        /// <summary>
        /// 插入紧急电话配置信息
        /// </summary>
        /// <param name="fireList"></param>
        /// <returns></returns>
        public int InsertEpList(List<ep_c_cfg> epList)
        {
            int value = -1; ;
            try
            {
                string strSql = string.Empty;
                foreach (ep_c_cfg ep in epList)
                {
                    strSql += CreateInsertEpSql(ep);
                }
                if (!string.IsNullOrEmpty(strSql))
                {
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
            }
            return value;
        }
        /// <summary>
        /// 插入紧急电话配置信息
        /// </summary>
        /// <param name="fireList"></param>
        /// <returns></returns>
        public int InsertEp(ep_c_cfg ep)
        {
            int value = -1; ;
            try
            {
                string strSql = CreateInsertEpSql(ep);
                if (!string.IsNullOrEmpty(strSql))
                {
                    value = DBHelper.ExcuteSql(strSql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
            }
            return value;
        }
        /// <summary>
        /// 新增地图信息
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public int InsertMap(Map map)
        {
            int value = -1; ;
            try
            {
                string strSql = CreateInsertMapSql(map);
                value = DBHelper.ExcuteTransactionSql(strSql);
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }
        /// <summary>
        /// 新增摄像机配置信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int InsertCCTV(List<Tv_cctv_cfg> list)
        {
            int value = -1;
            try
            {
                if (list.Count > 0)
                {
                    string strSql = AddCCTV(list);
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }
        /// <summary>
        /// 更新海康indexcode
        /// </summary>
        /// <param name="tvs"></param>
        /// <returns></returns>
        public int UpdateCCTV(List<Tv_cctv_cfg> tvs)
        {
            int value = -1;
            try
            {
                if (tvs.Count > 0)
                {
                    string strSql = UpdateTVStringg(tvs);
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }
        /// <summary>
        /// 根据equid更新ip和port
        /// </summary>
        /// <param name="equs"></param>
        /// <returns></returns>
        public int UpdateEqu(List<Equ> equs)
        {
            int value = -1;
            try
            {
                if (equs.Count > 0)
                {
                    string strSql = UpdateEquString(equs);
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }

        /// <summary>
        /// 更新分区
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        public int UpdateArea(IList<p_area_cfg> areas)
        {
            int value = -1;
            try
            {
                if (areas.Count > 0)
                {
                    string strSql = UpdateAreaString(areas);
                    value = DBHelper.ExcuteTransactionSql(strSql);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }

        /// <summary>
        /// 对设备基础信息进行尝试插入
        /// </summary>
        /// <param name="equs"></param>
        /// <returns></returns>
        public int UpdateORInsertEqu(List<MyObject> equs)
        {
            int i = -1;
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (MyObject equ in equs)
                {
                    if (equ is tunnel)
                    {
                        sb.Append(InsertTunnelSql((tunnel)equ));
                    }
                    else
                    {
                        sb.Append(InsertEquSql(equ.equ));
                    }
                }
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    i = DBHelper.ExcuteTransactionSql(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return i;
                throw e;
            }
            return i;
        }
        /// <summary>
        /// 尝试插入设备基础信息语句
        /// </summary>
        /// <param name="current"></param>
        /// <returns>sql语句</returns>
        public string InsertEquSql(Equ current)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                #region 默认报警方法
                switch (current.EquTypeID)
                {
                    case "CM":
                    case "CF":
                    case "CL":
                        current.AlarmMethod = null;
                        break;
                    case "F":
                    case "E":
                    case "EP":
                        current.AlarmMethod = "123";
                        current.msgTimeoutSec = -1;
                        break;
                    case "P":
                        current.AlarmMethod = "32";
                        break;
                    case "VC":
                        current.AlarmMethod = "NORMAL";
                        break;
                    case "VI":
                        current.AlarmMethod = "YW";
                        break;
                    case "P_CO":
                        current.AlarmMethod = "COYCAlarm";
                        break;
                    case "P_GJ":
                        current.AlarmMethod = "GJYCAlarm";
                        break;
                    case "P_VI":
                        current.AlarmMethod = "VIYCAlarm";
                        break;
                    case "P_TW":
                        current.AlarmMethod = "TWYCAlarm";
                        break;
                    case "P_LLDI":
                        current.AlarmMethod = "LqdYCAlarm";
                        break;
                    case "P_JF":
                        current.AlarmMethod = "StFanYCAlarm";
                        break;
                    default:
                        break;
                }
                #endregion
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                #region SQL create
                if (!string.IsNullOrEmpty(current.EquID))
                {
                    sb2.Append("equid,");
                    sb3.Append("'" + current.EquID + "',");
                    sb4.Append("equid='" + current.EquID + "',");
                }
                if (!string.IsNullOrEmpty(current.EquName))
                {
                    sb2.Append("EquName,");
                    sb3.Append("'" + current.EquName + "',");
                    sb4.Append("EquName='" + current.EquName + "',");
                }
                sb2.Append("EquTypeID,");
                sb3.Append("'" + current.EquTypeID + "',");
                sb4.Append("EquTypeID='" + current.EquTypeID + "',");
                if (!string.IsNullOrEmpty(current.PointX))
                {
                    sb2.Append("PointX,");
                    sb3.Append("'" + current.PointX + "',");
                    sb4.Append("PointX='" + current.PointX + "',");
                }
                if (!string.IsNullOrEmpty(current.PointY))
                {
                    sb2.Append("PointY,");
                    sb3.Append("'" + current.PointY + "',");
                    sb4.Append("PointY='" + current.PointY + "',");
                }
                if (!string.IsNullOrEmpty(current.PileNo))
                {
                    sb2.Append("PileNo,");
                    sb3.Append("'" + current.PileNo + "',");
                    sb4.Append("PileNo='" + current.PileNo + "',");
                }
                if (!string.IsNullOrEmpty(current.Code))
                {
                    sb2.Append("`Code`,");
                    sb3.Append("'" + current.Code + "',");
                    sb4.Append("`Code`='" + current.Code + "',");
                }
                if (!string.IsNullOrEmpty(current.MapID))
                {
                    sb2.Append("MapID,");
                    sb3.Append("'" + current.MapID + "',");
                    sb4.Append("MapID='" + current.MapID + "',");
                }
                if (current.DirectionID != null)
                {
                    sb2.Append("DirectionID,");
                    sb3.Append(current.DirectionID + ",");
                    sb4.Append("DirectionID=" + current.DirectionID + ",");
                }
                if (!string.IsNullOrEmpty(current.AddressDiscribe))
                {
                    sb2.Append("AddressDiscribe,");
                    sb3.Append("'" + current.AddressDiscribe + "',");
                    sb4.Append("AddressDiscribe='" + current.AddressDiscribe + "',");
                }
                if (!string.IsNullOrEmpty(current.AlarmMethod))
                {
                    sb2.Append("AlarmMethod,");
                    sb3.Append("'" + current.AlarmMethod + "',");
                    sb4.Append("AlarmMethod='" + current.AlarmMethod + "',");
                }
                if (!string.IsNullOrEmpty(current.IP))
                {
                    sb2.Append("IP,");
                    sb3.Append("'" + current.IP + "',");
                    sb4.Append("IP='" + current.IP + "',");
                }
                if (current.Port != null)
                {
                    sb2.Append("`Port`,");
                    sb3.Append(current.Port + ",");
                    sb4.Append("`Port`=" + current.Port + ",");
                }
                sb2.Append("FatherEquID,");
                sb3.Append("'" + current.FatherEquID + "',");
                sb4.Append("FatherEquID='" + current.FatherEquID + "',");
                if (current.TaskWV != null)
                {
                    sb2.Append("TaskWV,");
                    sb3.Append(current.TaskWV + ",");
                    sb4.Append("TaskWV=" + current.TaskWV + ",");
                }
                if (current.msgTimeoutSec != null)
                {
                    sb2.Append("msgTimeoutSec,");
                    sb3.Append(current.msgTimeoutSec + ",");
                    sb4.Append("msgTimeoutSec=" + current.msgTimeoutSec + ",");
                }
                if (!string.IsNullOrEmpty(current.Encode))
                {
                    sb2.Append("Encode,");
                    sb3.Append("'" + current.Encode + "',");
                    sb4.Append("Encode='" + current.Encode + "',");
                }
                if (!string.IsNullOrEmpty(current.Note))
                {
                    sb2.Append("Note,");
                    sb3.Append("'" + current.Note + "',");
                    sb4.Append("Note='" + current.Note + "',");
                }
                if (!string.IsNullOrEmpty(current.plcStationAddress))
                {
                    sb2.Append("plcStationAddress,");
                    sb3.Append("'" + current.plcStationAddress + "',");
                    sb4.Append("plcStationAddress='" + current.plcStationAddress + "',");
                }
                if (!string.IsNullOrEmpty(current.Vendor))
                {
                    sb2.Append("Vendor,");
                    sb3.Append("'" + current.Vendor + "',");
                    sb4.Append("Vendor='" + current.Vendor + "',");
                }
                if (!string.IsNullOrEmpty(current.RunMode))
                {
                    sb2.Append("RunMode,");
                    sb3.Append("'" + current.RunMode + "',");
                    sb4.Append("RunMode='" + current.RunMode + "',");
                }
                int index = sb2.ToString().LastIndexOf(',');
                sb2.Remove(index, 1);
                index = sb3.ToString().LastIndexOf(',');
                sb3.Remove(index, 1);
                index = sb4.ToString().LastIndexOf(',');
                sb4.Remove(index, 1);
                sb1.AppendFormat("insert into equ({0})values({1}) on duplicate key update {2};", sb2, sb3, sb4);
                sb.Append(sb1);
                #endregion
            }
            catch (Exception)
            {
                return null;
            }
            return sb.ToString();
        }
        /// <summary>
        /// 尝试插入隧道信息语句
        /// </summary>
        /// <param name="current"></param>
        /// <returns>sql语句</returns>
        public string InsertTunnelSql(tunnel current)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                #region SQL create
                if (!string.IsNullOrEmpty(current.equ.EquID))
                {
                    sb2.Append("BM,");
                    sb3.Append("'" + current.equ.EquID + "',");
                    sb4.Append("BM='" + current.equ.EquID + "',");
                }
                if (!string.IsNullOrEmpty(current.equ.EquName))
                {
                    sb2.Append("Name,");
                    sb3.Append("'" + current.equ.EquName + "',");
                    sb4.Append("Name='" + current.equ.EquName + "',");
                }
                if (!string.IsNullOrEmpty(current.equ.PointX))
                {
                    sb2.Append("PointX,");
                    sb3.Append("'" + current.equ.PointX + "',");
                    sb4.Append("PointX='" + current.equ.PointX + "',");
                }
                if (!string.IsNullOrEmpty(current.equ.PointY))
                {
                    sb2.Append("PointY,");
                    sb3.Append("'" + current.equ.PointY + "',");
                    sb4.Append("PointY='" + current.equ.PointY + "',");
                }

                if (!string.IsNullOrEmpty(current.equ.PileNo))
                {
                    sb2.Append("CenterStake,");
                    sb3.Append("'" + current.equ.PileNo + "',");
                    sb4.Append("CenterStake='" + current.equ.PileNo + "',");
                }
                if (!string.IsNullOrEmpty(current.equ.Note))
                {
                    sb2.Append("Mesg,");
                    sb3.Append("'" + current.equ.Note + "',");
                    sb4.Append("Mesg='" + current.equ.Note + "',");
                }
                int index = sb2.ToString().LastIndexOf(',');
                sb2.Remove(index, 1);
                index = sb3.ToString().LastIndexOf(',');
                sb3.Remove(index, 1);
                index = sb4.ToString().LastIndexOf(',');
                sb4.Remove(index, 1);
                sb1.AppendFormat("insert into tunnel({0})values({1}) on duplicate key update {2};", sb2, sb3, sb4);
                sb.Append(sb1);
                #endregion
            }
            catch (Exception)
            {
                return null;
            }
            return sb.ToString();
        }

        private static void CreateEquSQL(MyObject current, StringBuilder sb, StringBuilder sb1, StringBuilder sb2, StringBuilder sb3, StringBuilder sb4)
        {
            
        }


        /// <summary>
        /// 插入设备基础信息，只操作equ表
        /// </summary>
        /// <param name="equ"></param>
        /// <returns></returns>
        public int InsertEqu(MyObject equ)
        {
            string sql = InsertEquSql(equ.equ);
            if (!string.IsNullOrEmpty(sql))
            {
                return DBHelper.ExcuteTransactionSql(sql);
            }
            return -1;
        }
        /// <summary>
        /// 插入设备基础信息，只操作equ表
        /// </summary>
        /// <param name="equList"></param>
        /// <returns></returns>
        public int InsertEqu(List<MyObject> equList)
        {
            string sql = string.Empty;
            for (int i = 0; i < equList.Count; i++)
            {
                sql += InsertEquSql(equList[i].equ);
            }
            if (!string.IsNullOrEmpty(sql))
            {
                return DBHelper.ExcuteTransactionSql(sql);
            }
            return -1;
        }
        /// <summary>
        /// 插入遥信
        /// </summary>
        /// <param name="plcEqu"></param>
        /// <returns>-1表示失败</returns>
        public int InsertYX(List<PLCEqu> plcEqu)
        {
            int isSuccess = -1;
            try
            {
                StringBuilder sql = new StringBuilder();
                for (int i = 0; i < plcEqu.Count; i++)
                {
                    sql.Append(CreateInsertYXSql(plcEqu[i].plc_pro.yxList));
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    string str = sql.ToString();
                    isSuccess = DBHelper.ExcuteTransactionSql(sql.ToString());
                }
            }
            catch (Exception e)
            {
                return isSuccess;
                throw e;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入遥信信息表
        /// </summary>
        /// <param name="yxList">遥信信息列表</param>
        /// <returns></returns>
        public int InsertYX(List<yx> yxList)
        {
            int isSuccess = -1;
            try
            {
                string str = CreateInsertYXSql(yxList);
                if (!string.IsNullOrEmpty(str))
                {
                    isSuccess = DBHelper.ExcuteTransactionSql(str);
                }
            }
            catch (Exception e)
            {
                return isSuccess;
                throw e;
            }
            return isSuccess;
        }
        /// <summary>
        /// 批量插入分区
        /// </summary>
        /// <param name="areas"></param>
        /// <returns>-1表示失败，0表示没有数据改变 1表示成功</returns>
        public int InsertAreas(List<p_area_cfg> areas)
        {
            int isSuccess = -1;
            string sql = string.Empty;
            try
            {
                for (int i = 0; i < areas.Count; i++)
                {
                    sql += CreateInsertAreaSql(areas[i]);
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    isSuccess = DBHelper.ExcuteTransactionSql(sql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("InsertAreas:" + e);
                throw;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入分区配置
        /// </summary>
        /// <param name="area"></param>
        /// <returns>自增量ID，-1表示失败</returns>
        public int InsertArea(p_area_cfg area)
        {
            int isSuccess = -1;
            try
            {
                isSuccess = DBHelper.ExcuteSql(CreateInsertAreaSql(area));
            }
            catch (Exception e)
            {
                Log.WriteLog("InsertArea:" + e.Message);
                return isSuccess;
                throw;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入yx_cfg信息表
        /// </summary>
        /// <param name="yxcfgList">遥信信息列表</param>
        /// <returns></returns>
        public int InsertYXConfig(List<Yx_cfg> yxcfgList)
        {
            int isSuccess = -1;
            try
            {
                string str = CreateInsertYXcfgSql(yxcfgList);
                if (!string.IsNullOrEmpty(str))
                {
                    isSuccess = DBHelper.ExcuteTransactionSql(str);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
            }
            return isSuccess;
        }
        #region YK
        /// <summary>
        /// 删除遥控信息表中的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deleteYKConfig(int id)
        {
            int isSuccess = -1;
            try
            {
                isSuccess = DBHelper.ExcuteTransactionSql(string.Format("delete from yk_cfg where id={0}", id)); ;
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入遥控
        /// </summary>
        /// <param name="plcEqu">设备信息</param>
        /// <returns>-1表示失败</returns>
        public int InsertYK(List<PLCEqu> plcEqu)
        {
            int isSuccess = -1;
            try
            {
                StringBuilder sql = new StringBuilder();
                for (int i = 0; i < plcEqu.Count; i++)
                {
                    foreach (yk item in plcEqu[i].plc_pro.ykList)
                    {
                        sql.Append(CreateInsertYKSql(item));
                    }
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    string str = sql.ToString();
                    isSuccess = DBHelper.ExcuteTransactionSql(sql.ToString());
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入遥控信息表
        /// </summary>
        /// <param name="ykList">遥控信息</param>
        /// <returns></returns>
        public int InsertYK(List<yk> ykList)
        {
            int isSuccess = -1;
            try
            {
                StringBuilder sql = new StringBuilder();
                for (int i = 0; i < ykList.Count; i++)
                {
                    sql.Append(CreateInsertYKSql(ykList[i]));
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    string str = sql.ToString();
                    isSuccess = DBHelper.ExcuteTransactionSql(sql.ToString());
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入yk_cfg信息表
        /// </summary>
        /// <param name="ykcfgList">遥控信息</param>
        /// <returns></returns>
        public int InsertYKConfig(List<Yk_cfg> ykcfgList)
        {
            int isSuccess = -1;
            try
            {
                StringBuilder sql = new StringBuilder();
                for (int i = 0; i < ykcfgList.Count; i++)
                {
                    sql.Append(CreateInsertYKcfgSql(ykcfgList[i]));
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    isSuccess = DBHelper.ExcuteTransactionSql(sql.ToString());
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
            }
            return isSuccess;
        }
        /// <summary>
        /// 插入yk_cfg信息表
        /// </summary>
        /// <param name="ykcfg">遥控信息</param>
        /// <returns></returns>
        public int InsertYKConfig(Yk_cfg ykcfg)
        {
            int isSuccess = -1;
            try
            {
                string sql = CreateInsertYKcfgSql(ykcfg);
                if (!string.IsNullOrEmpty(sql))
                {
                    isSuccess = DBHelper.ExcuteSql(sql);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
            }
            return isSuccess;
        }
        #endregion
        /// <summary>
        /// 插入单个遥测信息
        /// </summary>
        /// <param name="yc"></param>
        /// <returns></returns>
        public int InsertYC(yc yc)
        {
            int isSuccess = -1;
            try
            {
                isSuccess = DBHelper.ExcuteSql(CreateInsertYCSql(yc));
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return isSuccess;
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// 插入单个遥测配置信息信息
        /// </summary>
        /// <param name="yccfg"></param>
        /// <returns></returns>
        public int InsertYCCfg(Yc_cfg yccfg)
        {
            int isSuccess = -1;
            try
            {
                isSuccess = DBHelper.ExcuteSql(CreateInsertYCcfgSql(yccfg));
            }
            catch (Exception e)
            {
                Log.WriteLog("InsertYCCfg" + e);
                return isSuccess;
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="equs"></param>
        /// <returns></returns>
        public int DeleteEqu(List<Equ> equs)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < equs.Count; i++)
                {
                    switch (equs[i].EquTypeID)
                    {
                        case "UnKnow":
                            break;
                        case "CF":
                        case "CL":
                        case "CM":
                            sb.AppendFormat("delete from c_cfg where equid='{0}';", equs[i].EquID);
                            break;
                        case "EP_R":
                        case "EP_T":
                            sb.AppendFormat("delete from ep_c_cfg where equid='{0}';", equs[i].EquID);
                            break;
                        case "F_L":
                        case "F_SB":
                        case "F_YG":
                            sb.AppendFormat("delete from f_c_cfg where equid='{0}';", equs[i].EquID);
                            break;
                        case "P":
                            sb.AppendFormat("delete from p_area_cfg where equid='{0}';", equs[i].EquID);
                            break;
                        case "P_AF":
                        case "P_CL":
                        case "P_CO":
                        case "P_GJ":
                        case "P_HL":
                        case "P_HL2":
                        case "P_JF":
                        case "P_L":
                        case "P_LJQ":
                        case "P_LLDI":
                        case "P_LYJ":
                        case "P_P":
                        case "P_TD":
                        case "P_TL2_Close":
                        case "P_TL2_Down":
                        case "P_TL2_Left":
                        case "P_TL2_Right":
                        case "P_TL2_UP":
                        case "P_TL3_Down":
                        case "P_TL3_Left":
                        case "P_TL3_Right":
                        case "P_TL3_UP":
                        case "P_TL4_Down":
                        case "P_TL4_Left":
                        case "P_TL4_Right":
                        case "P_TL4_UP":
                        case "P_TL5_Down":
                        case "P_TL5_Left":
                        case "P_TL5_Right":
                        case "P_TL5_UP":
                        case "P_TL_Down":
                        case "P_TL_Left":
                        case "P_TL_Right":
                        case "P_TL_UP":
                        case "P_TW":
                        case "P_VI":
                        case "P_RL":
                            sb.AppendFormat("delete from yx where equid='{0}';", equs[i].EquID);
                            sb.AppendFormat("delete from yx_cfg where equid='{0}';", equs[i].EquID);
                            sb.AppendFormat("delete from yk where equid='{0}';", equs[i].EquID);
                            sb.AppendFormat("delete from yk_cfg where equid='{0}';", equs[i].EquID);
                            sb.AppendFormat("delete from yc where equid='{0}';", equs[i].EquID);
                            sb.AppendFormat("delete from yc_cfg where equid='{0}';", equs[i].EquID);
                            break;
                        case "S":
                            break;
                        case "TV_CCTV_Ball":
                        case "TV_CCTV_E":
                        case "TV_CCTV_Gun":
                            sb.AppendFormat("delete from tv_cctv_cfg where CCTVID='{0}';", equs[i].EquID);
                            break;
                        case "VC":
                            break;
                        default:
                            break;
                    }
                    sb.AppendFormat("delete from equ where equid='{0}';", equs[i].EquID);
                }
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    string sql = sb.ToString();
                    return DBHelper.ExcuteTransactionSql(sb.ToString());
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("DeleteEqu:" + e);
                return -1;
                throw e;
            }
            return 0;
        }

        /// <summary>
        /// 整个地图遥信点位取反
        /// </summary>
        /// <param name="mapid">地图id</param>
        private void TakeYXBack(int mapid)
        {
            try
            {
                IList<yx> yxlist = DBHelper.Query<yx>(string.Format("SELECT yx.YXInfoID,yx.YXInfoMesg,yx.EquStateID,yx.IsState,yx.EquID FROM equ INNER JOIN yx ON yx.EquID = equ.EquID WHERE equ.MapID = '{0}' AND equ.EquTypeID LIKE 'P_TL%'", mapid));
                for (int i = 0; i < yxlist.Count; i++)
                {
                    if (yxlist[i].IsState == 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < yxlist[i].YXInfoMesg.Length; j++)
                        {
                            if (yxlist[i].YXInfoMesg.Substring(j, 1) == "1")
                            {
                                sb.Append("0");
                            }
                            else
                            {
                                sb.Append("1");
                            }
                        }
                        yxlist[i].YXInfoMesg = sb.ToString();
                    }
                }
                StringBuilder sb1 = new StringBuilder();
                for (int i = 0; i < yxlist.Count; i++)
                {
                    sb1.AppendFormat("update yx set yxinfomesg='{0}' where(YXInfoID={1});", yxlist[i].YXInfoMesg, yxlist[i].YXInfoID);
                }
                string sql = sb1.ToString();
                int issucces = DBHelper.ExcuteSql(sql);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #region 私有方法

        private string CreateInsertC_cfgSql(c_cfg cfg)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            try
            {
                sb1.Append("EquID,");
                sb2.Append("'" + cfg.EquID + "',");
                if (cfg.CMSHeight.HasValue)
                {
                    sb1.Append("CMSHeight,");
                    sb2.Append(cfg.CMSHeight + ",");
                }
                if (cfg.CMSWidth != null)
                {
                    sb1.Append("CMSWidth,");
                    sb2.Append(cfg.CMSWidth + ",");
                }
                if (!string.IsNullOrEmpty(cfg.FontType))
                {
                    sb1.Append("FontType,");
                    sb2.Append("'" + cfg.FontType + "',");
                }
                if (!string.IsNullOrEmpty(cfg.FontColor))
                {
                    sb1.Append("FontColor,");
                    sb2.Append("'" + cfg.FontColor + "',");
                }
                if (!string.IsNullOrEmpty(cfg.BackColor))
                {
                    sb1.Append("BackColor,");
                    sb2.Append("'" + cfg.BackColor + "',");
                }
                if (cfg.FontSize != null)
                {
                    sb1.Append("FontSize,");
                    sb2.Append(cfg.FontSize + ",");
                }
                if (cfg.ContentType != null)
                {
                    sb1.Append("ContentType,");
                    sb2.Append(cfg.ContentType + ",");
                }
                if (cfg.CharBetween != null)
                {
                    sb1.Append("CharBetween,");
                    sb2.Append(cfg.CharBetween + ",");
                }
                if (cfg.OutType != null)
                {
                    sb1.Append("OutType,");
                    sb2.Append(cfg.OutType + ",");
                }
                if (cfg.OutSpeed != null)
                {
                    sb1.Append("OutSpeed,");
                    sb2.Append(cfg.OutSpeed + ",");
                }
                if (cfg.StayTime != null)
                {
                    sb1.Append("StayTime,");
                    sb2.Append(cfg.StayTime + ",");
                }
                if (cfg.BlankCount != null)
                {
                    sb1.Append("BlankCount,");
                    sb2.Append(cfg.BlankCount + ",");
                }
                if (!string.IsNullOrEmpty(cfg.Pno))
                {
                    sb1.Append("Pno,");
                    sb2.Append("'" + cfg.Pno + "',");
                }
                if (cfg.MinFontSize != null)
                {
                    sb1.Append("MinFontSize,");
                    sb2.Append(cfg.MinFontSize + ",");
                }
                if (cfg.MaxLength != null)
                {
                    sb1.Append("MaxLength,");
                    sb2.Append(cfg.MaxLength + ",");
                }
                if (cfg.SupportPic != null)
                {
                    sb1.Append("SupportPic,");
                    sb2.Append(cfg.SupportPic + ",");
                }
                if (!string.IsNullOrEmpty(cfg.PicLocation))
                {
                    sb1.Append("PicLocation,");
                    sb2.Append("'" + cfg.PicLocation + "',");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into c_cfg({0})values({1});", sb1, sb2);

            }
            catch (Exception e)
            {
                Log.WriteLog("CreateInsertC_cfgSql:" + e);
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建插入遥控信息语句
        /// </summary>
        /// <param name="ykList"></param>
        /// <returns></returns>
        private string CreateInsertYKcfgSql(Yk_cfg ykList)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append("EquID,");
                sb2.Append("'" + ykList.EquID + "',");
                if (!string.IsNullOrEmpty(ykList.AddrAndBit))
                {
                    sb1.Append("AddrAndBit,");
                    sb2.Append("'" + ykList.AddrAndBit + "',");
                }
                if (ykList.Order != null)
                {
                    sb1.Append("`Order`,");
                    sb2.Append(ykList.Order + ",");
                }
                if (ykList.AreaID != null)
                {
                    sb1.Append("AreaID,");
                    sb2.Append(ykList.AreaID + ",");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into yk_cfg({0})values({1});SELECT @@Identity;", sb1, sb2);

            }
            catch (Exception)
            {
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建插入遥控信息语句
        /// </summary>
        /// <param name="ykList"></param>
        /// <returns></returns>
        private string CreateInsertYKSql(yk ykList)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append("EquID,");
                sb2.Append("'" + ykList.EquID + "',");
                if (!string.IsNullOrEmpty(ykList.Mesg))
                {
                    sb1.Append("Mesg,");
                    sb2.Append("'" + ykList.Mesg + "',");
                }
                if (ykList.CommandID != null)
                {
                    sb1.Append("CommandID,");
                    sb2.Append(ykList.CommandID + ",");
                }
                if (ykList.AreaID != null)
                {
                    sb1.Append("AreaID,");
                    sb2.Append(ykList.AreaID + ",");
                }
                if (!string.IsNullOrEmpty(ykList.Points))
                {
                    sb1.Append("Points,");
                    sb2.Append("'" + ykList.Points + "',");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into yk({0})values({1});", sb1, sb2);

            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return null;
            }
            return sql.ToString();
        }
        private string CreateInsertAreaSql(p_area_cfg area)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            try
            {

                sb1.Append("EquID,");
                sb2.Append("'" + area.equid + "',");
                if (!string.IsNullOrEmpty(area.note))
                {
                    sb1.Append("note,");
                    sb2.Append("'" + area.note + "',");
                }
                if (!string.IsNullOrEmpty(area.point))
                {
                    sb1.Append("point,");
                    sb2.Append("'" + area.point + "',");
                }
                if (!string.IsNullOrEmpty(area.yxstartAddress))
                {
                    sb1.Append("yxstartAddress,");
                    sb2.Append("'" + area.yxstartAddress + "',");
                }
                if (area.yxlength != null)
                {
                    sb1.Append("yxlength,");
                    sb2.Append(area.yxlength + ",");
                }
                if (area.yxcommlength != null)
                {
                    sb1.Append("yxcommlength,");
                    sb2.Append(area.yxcommlength + ",");
                }
                if (!string.IsNullOrEmpty(area.ycstartAddress))
                {
                    sb1.Append("ycstartAddress,");
                    sb2.Append("'" + area.ycstartAddress + "',");
                }
                if (area.yclength != null)
                {
                    sb1.Append("yclength,");
                    sb2.Append(area.yclength + ",");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into p_area_cfg({0})values({1});SELECT @@identity;", sb1, sb2);
            }
            catch (Exception)
            {
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建yc表插入语句,该语句最后返回的是自增量ID
        /// </summary>
        /// <param name="yc"></param>
        /// <returns>自增量ID</returns>
        private string CreateInsertYCSql(yc yc)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append("EquID,");
                sb2.Append("'" + yc.EquID + "',");
                if (yc.YCCollecDown != null)
                {
                    sb1.Append("YCCollecDown,");
                    sb2.Append(yc.YCCollecDown + ",");
                }
                if (!string.IsNullOrEmpty(yc.YCField))
                {
                    sb1.Append("YCField,");
                    sb2.Append("'" + yc.YCField + "',");
                }
                if (yc.YCFun != null)
                {
                    sb1.Append("YCFun,");
                    sb2.Append(yc.YCFun + ",");
                }
                if (yc.YCRealDown != null)
                {
                    sb1.Append("YCRealDown,");
                    sb2.Append(yc.YCRealDown + ",");
                }
                if (yc.YCRealUP != null)
                {
                    sb1.Append("YCRealUP,");
                    sb2.Append(yc.YCRealUP + ",");
                }
                if (yc.YCCollecUP != null)
                {
                    sb1.Append("YCCollecUP,");
                    sb2.Append(yc.YCCollecUP + ",");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into yc({0})values({1});SELECT @@identity;", sb1, sb2);

            }
            catch (Exception e)
            {
                Log.WriteLog("创建yc表插入语句:" + e);
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建插入遥测配置信息语句
        /// </summary>
        /// <param name="yccfg"></param>
        /// <returns></returns>
        private string CreateInsertYCcfgSql(Yc_cfg yccfg)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb1.Append("EquID,");
                sb2.Append("'" + yccfg.EquID + "',");
                if (!string.IsNullOrEmpty(yccfg.AddrAndBit))
                {
                    sb1.Append("AddrAndBit,");
                    sb2.Append("'" + yccfg.AddrAndBit + "',");
                }
                if (yccfg.Order != null)
                {
                    sb1.Append("Order,");
                    sb2.Append(yccfg.Order + ",");
                }
                if (yccfg.AreaID != null)
                {
                    sb1.Append("AreaID,");
                    sb2.Append(yccfg.AreaID + ",");
                }
                if (yccfg.YcID != null)
                {
                    sb1.Append("YcID,");
                    sb2.Append(yccfg.YcID + ",");
                }
                int index = sb1.ToString().LastIndexOf(",");
                sb1.Remove(index, 1);
                index = sb2.ToString().LastIndexOf(",");
                sb2.Remove(index, 1);
                sql.AppendFormat("insert into yc_cfg({0})values({1});", sb1, sb2);
            }
            catch (Exception e)
            {
                Log.WriteLog("创建插入遥测配置信息语句:" + e);
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建yx_cfg表插入语句
        /// </summary>
        /// <param name="yxcfgList"></param>
        /// <returns></returns>
        private string CreateInsertYXcfgSql(List<Yx_cfg> yxcfgList)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                for (int j = 0; j < yxcfgList.Count; j++)
                {
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    sb1.Append("EquID,");
                    sb2.Append("'" + yxcfgList[j].EquID + "',");
                    if (!string.IsNullOrEmpty(yxcfgList[j].AddrAndBit))
                    {
                        sb1.Append("AddrAndBit,");
                        sb2.Append("'" + yxcfgList[j].AddrAndBit + "',");
                    }
                    if (yxcfgList[j].IsError != null)
                    {
                        sb1.Append("IsError,");
                        sb2.Append(yxcfgList[j].IsError + ",");
                    }
                    if (yxcfgList[j].Order != null)
                    {
                        sb1.Append("`Order`,");
                        sb2.Append(yxcfgList[j].Order + ",");
                    }
                    if (yxcfgList[j].AreaID != null)
                    {
                        sb1.Append("AreaID,");
                        sb2.Append(yxcfgList[j].AreaID + ",");
                    }
                    int index = sb1.ToString().LastIndexOf(",");
                    sb1.Remove(index, 1);
                    index = sb2.ToString().LastIndexOf(",");
                    sb2.Remove(index, 1);
                    sql.AppendFormat("insert into yx_cfg({0})values({1});", sb1, sb2);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("创建插入yx_cfg表语句错误：" + e.Message);
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 创建yx表插入语句
        /// </summary>
        /// <param name="yxList"></param>
        /// <returns></returns>
        private string CreateInsertYXSql(List<yx> yxList)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                for (int j = 0; j < yxList.Count; j++)
                {
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    sb1.Append("EquID,");
                    sb2.Append("'" + yxList[j].EquID + "',");
                    if (!string.IsNullOrEmpty(yxList[j].YXInfoMesg))
                    {
                        sb1.Append("YXInfoMesg,");
                        sb2.Append("'" + yxList[j].YXInfoMesg + "',");
                    }
                    if (yxList[j].EquStateID != null)
                    {
                        sb1.Append("EquStateID,");
                        sb2.Append(yxList[j].EquStateID + ",");
                    }
                    if (yxList[j].IsState != null)
                    {
                        sb1.Append("IsState,");
                        sb2.Append(yxList[j].IsState + ",");
                    }
                    int index = sb1.ToString().LastIndexOf(",");
                    sb1.Remove(index, 1);
                    index = sb2.ToString().LastIndexOf(",");
                    sb2.Remove(index, 1);
                    sql.AppendFormat("insert into yx({0})values({1});", sb1, sb2);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return sql.ToString();
        }
        /// <summary>
        /// 生成插入紧急电话配置语句
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        private string CreateInsertEpSql(ep_c_cfg ep)
        {
            string sql = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(ep.EquID))
                {
                    sql = string.Format("INSERT INTO `ep_c_cfg` (`EquID`, `EPNum`,`Mesg`) VALUES ('{0}', '{1}','{2}');", ep.EquID, ep.EPNum, ep.Mesg);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("生成插入紧急电话配置语句:" + e);
            }
            return sql;
        }
        /// <summary>
        /// 插入火灾配置信息语句
        /// </summary>
        /// <param name="fire"></param>
        /// <returns></returns>
        private string CreateInsertFireSql(f_c_cfg fire)
        {
            string sql = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(fire.EquID))
                {
                    sql = string.Format("INSERT INTO `f_c_cfg` (`EquID`, `FireNum`,`Note`) VALUES ('{0}', '{1}','{2}');", fire.EquID, fire.FireNum, fire.Note);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("插入火灾配置信息语句:" + e);
            }
            return sql;
        }
        /// <summary>
        /// 生成插入地图信息语句
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private string CreateInsertMapSql(Map map)
        {
            try
            {
                return string.Format("INSERT INTO `map` (`MapID`, `MapName`,`IsRoad`) VALUES ('{0}', '{1}',0);", map.MapID, map.MapName);
            }
            catch (Exception e)
            {
                Log.WriteLog("生成插入地图信息语句:" + e);
                return null;
            }
        }

        /// <summary>
        /// 新增摄像机配置信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string AddCCTV(List<Tv_cctv_cfg> list)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                foreach (var cctv in list)
                {
                    strSql.Append("insert into tv_cctv_cfg(CCTVID,OutsideAddr,Note,EventCheckID,EventLinkCameraID)value");
                    strSql.Append("('" + cctv.CCTVID + "',");
                    strSql.Append("'" + cctv.OutsideAddr + "',");
                    strSql.Append("'" + cctv.Note + "',");
                    strSql.Append("'" + cctv.EventCheckID + "',");
                    strSql.Append("'" + cctv.EventLinkCameraID + "');");
                }
                return strSql.ToString();
            }
            catch (Exception e)
            {
                Log.WriteLog("新增摄像机配置信息:" + e);
                return null;
            }
        }

        /// <summary>
        /// 更新海康indexcode
        /// </summary>
        /// <param name="tvs"></param>
        /// <returns></returns>
        private string UpdateTVStringg(List<Tv_cctv_cfg> tvs)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                foreach (Tv_cctv_cfg tv in tvs)
                {
                    if (!string.IsNullOrEmpty(tv.OutsideAddr))
                    {
                        strSql.AppendFormat("update tv_cctv_cfg set OutsideAddr='{0}' where CCTVID='{1}';", tv.OutsideAddr, tv.CCTVID);
                    }
                }
                return strSql.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据equid更新ip和port
        /// </summary>
        /// <param name="equs"></param>
        /// <returns></returns>
        private string UpdateEquString(List<Equ> equs)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                foreach (Equ equ in equs)
                {
                    if (!string.IsNullOrEmpty(equ.IP) && !string.IsNullOrEmpty(equ.Port.ToString()))
                    {
                        strSql.AppendFormat("update equ set ip='{0}',port='{1}' where equid='{2}';", equ.IP, equ.Port, equ.EquID);
                    }
                }
                return strSql.ToString();
            }
            catch (Exception e)
            {
                Log.WriteLog("根据equid更新ip和port:" + e);
                return null;
            }
        }

        /// <summary>
        /// 更新PLC分区表
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        private string UpdateAreaString(IList<p_area_cfg> areas)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            try
            {
                foreach (p_area_cfg area in areas)
                {
                    if (area.yclength.HasValue)
                    {
                        strSql.AppendFormat("yclength={0},", Obj2DbStr(area.yclength));
                    }
                    if (area.yxlength.HasValue)
                    {
                        strSql.AppendFormat("yxlength={0},", Obj2DbStr(area.yxlength));
                    }
                    if (!string.IsNullOrEmpty(area.ycstartAddress))
                    {
                        strSql.AppendFormat("ycstartAddress={0},", Obj2DbStr(area.ycstartAddress));
                    }
                    if (area.yxcommlength.HasValue)
                    {
                        strSql.AppendFormat("yxcommlength={0},", Obj2DbStr(area.yxcommlength));
                    }
                    if (!string.IsNullOrEmpty(area.yxstartAddress))
                    {
                        strSql.AppendFormat("yxstartAddress={0},", Obj2DbStr(area.yxstartAddress));
                    }
                    if (strSql.Length > 0)
                    {
                        int index = strSql.ToString().LastIndexOf(",");
                        strSql.Remove(index, 1);
                        strSql1.AppendFormat("update p_area_cfg set {0} where (id='{1}');", strSql, area.id);
                    }
                }
                return strSql1.ToString();
            }
            catch (Exception e)
            {
                Log.WriteLog("更新PLC分区表:" + e);
                return null;
            }
        }
        /// <summary>
        /// 拼接数据库字符串使用，根据str类型转换为字符串，值类型前后不加单引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Obj2DbStr(object str)
        {
            if (str == null)
            {
                return "null";
            }
            if (str.GetType().IsValueType)
            {
                return str.ToString();
            }
            else
            {
                return "'" + str.ToString() + "'";
            }
        }
        #endregion
    }
}
