/* 功能：数据库数据读取
 * 时间：2017-11-29 14:25:28
 * 作者：张伟
 * 
 * 描述：
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data;
using WindowMake.DB;
using WindowMake.Entity;

namespace WindowMake
{
    public class DataStorage
    {
        #region 预案相关
        /// <summary>
        /// 获取单个预案触发设备
        /// </summary>
        /// <param name="gcid"></param>
        /// <returns></returns>
        public List<Gc_triggerequ> GetGctriggerEqu(string gcid)
        {
            List<Gc_triggerequ> gc_triggerequ = new List<Gc_triggerequ>();
            try
            {
                gc_triggerequ = (List<Gc_triggerequ>)DBHelper.Query<Gc_triggerequ>(string.Format("select * from gc_triggerequ where gcid='{0}' order by equid;", gcid));
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetGctriggerEqu：" + e);
            }
            return gc_triggerequ;
        }
        /// <summary>
        /// 获取单个预案控制设备
        /// </summary>
        /// <param name="gcid"></param>
        /// <returns></returns>
        public List<Gc_equ> GetGcEqu(string gcid)
        {
            List<Gc_equ> gc_equ = new List<Gc_equ>();
            try
            {
                gc_equ = (List<Gc_equ>)DBHelper.Query<Gc_equ>(string.Format("select * from gc_equ where gcid='{0}' order by equid;", gcid));
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetGcEqu：" + e);
            }
            return gc_equ;
        }

        /// <summary>
        /// 根据群控ID查询群控
        /// </summary>
        /// <param name="gcid"></param>
        /// <returns></returns>
        public Gc GetGcByGCID(string gcid)
        {
            List<Gc> gcs = new List<Gc>();
            try
            {
                gcs = (List<Gc>)DBHelper.Query<Gc>(string.Format("select * from gc where GCID='{0}' order by gcid;", gcid));
                if (gcs.Count > 0)
                {
                    return gcs[0];
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetGcByMapID：" + e);
            }
            return null;
        }
        /// <summary>
        /// 查询地图内群控
        /// </summary>
        /// <returns></returns>
        public List<Gc> GetGcByMapID(int mapid)
        {
            List<Gc> gcs = new List<Gc>();
            try
            {
                gcs = (List<Gc>)DBHelper.Query<Gc>(string.Format("select * from gc where mapid='{0}' order by gcid;", mapid));
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetGcByMapID：" + e);
            }
            return gcs;
        }

        /// <summary>
        /// 查询所有群控预案
        /// </summary>
        /// <returns></returns>
        public List<Gc> GetAllGc()
        {
            List<Gc> gcs = new List<Gc>();
            try
            {
                gcs = (List<Gc>)DBHelper.Query<Gc>("select * from gc order by gcid;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetAllGc：" + e);
            }
            return gcs;
        }
        #endregion
        /// <summary>
        /// 获取所有命令
        /// </summary>
        /// <returns></returns>
        public List<Command> GetAllCommand()
        {
            List<Command> commands = new List<Command>();
            try
            {
                commands = (List<Command>)DBHelper.Query<Command>("select * from command  order by id;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetAllCommand：" + e);
            }
            return commands;
        }
        /// <summary>
        /// 获取控制设备的命令及反馈状态
        /// </summary>
        /// <returns></returns>
        public List<Command2Type> GetCommandType()
        {
            List<Command2Type> commands = new List<Command2Type>();
            try
            {
                commands = (List<Command2Type>)DBHelper.Query<Command2Type>("SELECT command.CommandID,command.EquTypeID,command.`Name`,command.EquStateID,equ_rs_type.ImageUrl FROM command INNER JOIN equ_rs_type ON command.EquStateID = equ_rs_type.EquStateID; ");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetCommandType：" + e);
            }
            return commands;
        }
        /// <summary>
        /// 获取所有的分区信息
        /// </summary>
        /// <returns>分区表</returns>
        public List<p_area_cfg> GetAllArea()
        {
            List<p_area_cfg> areas = new List<p_area_cfg>();
            try
            {
                areas = (List<p_area_cfg>)DBHelper.Query<p_area_cfg>("select * from p_area_cfg  order by id;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetAllArea：" + e.Message);
            }
            return areas;
        }
        /// <summary>
        /// 以equid为查询条件，查询分区
        /// </summary>
        /// <param name="equid"></param>
        /// <returns></returns>
        public List<p_area_cfg> GetAreaByEquID(string equid)
        {
            List<p_area_cfg> areas = new List<p_area_cfg>();
            try
            {
                if (!string.IsNullOrEmpty(equid))
                {
                    areas = (List<p_area_cfg>)DBHelper.Query<p_area_cfg>(string.Format("select * from p_area_cfg where equid='{0}' order by id;", equid));
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetAreaByEquID：" + e.Message);
            }
            return areas;
        }

        /// <summary>
        /// 查询紧急电话配置
        /// </summary>
        /// <returns></returns>
        public List<ep_c_cfg> GetEpConfig()
        {
            List<ep_c_cfg> cfgs = new List<ep_c_cfg>();
            try
            {
                cfgs = (List<ep_c_cfg>)DBHelper.Query<ep_c_cfg>("SELECT * FROM `ep_c_cfg`;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetEpConfig" + e.Message);
                return null;
            }
            return cfgs;
        }
        /// <summary>
        /// 查询火灾配置
        /// </summary>
        /// <returns></returns>
        public List<f_c_cfg> GetFireConfig()
        {
            List<f_c_cfg> cfgs = new List<f_c_cfg>();
            try
            {
                cfgs = (List<f_c_cfg>)DBHelper.Query<f_c_cfg>("SELECT * FROM `f_c_cfg`;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetFireConfig" + e.Message);
                return null;
            }
            return cfgs;
        }
        /// <summary>
        /// 查询摄像机配置
        /// </summary>
        /// <returns></returns>
        public List<Tv_cctv_cfg> GetTVConfig()
        {
            List<Tv_cctv_cfg> cfgs = new List<Tv_cctv_cfg>();
            try
            {
                cfgs = (List<Tv_cctv_cfg>)DBHelper.Query<Tv_cctv_cfg>("SELECT * FROM `tv_cctv_cfg`;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetTVConfig" + e.Message);
                return null;
            }
            return cfgs;
        }
        /// <summary>
        /// 查询所有情报板配置
        /// </summary>
        /// <returns></returns>
        public List<c_cfg> GetCMSConfig()
        {
            List<c_cfg> cfgs = new List<c_cfg>();
            try
            {
                cfgs = (List<c_cfg>)DBHelper.Query<c_cfg>("SELECT * FROM `c_cfg`;");
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetCMSConfig" + e.Message);
                return null;
            }
            return cfgs;
        }
        /// <summary>
        /// 获取最大地图id
        /// </summary>
        /// <returns>the max mapid. -1表示异常，0表示表中无数据</returns>
        public string GetMaxMapID()
        {
            List<Map> mapid = new List<Map>();
            try
            {
                mapid = (List<Map>)DBHelper.Query<Map>("SELECT * FROM map ORDER BY MapID DESC LIMIT 1");
                if (mapid.Count == 1)
                {
                    return mapid[0].MapID;
                }
            }
            catch (Exception e)
            {
                return "-1";
                throw;
            }
            return "0";
        }
        /// <summary>
        /// 根据地图ID读取设备
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public List<Equ> ReadEqu(int mapid)
        {
            List<Equ> equs = new List<Equ>();
            try
            {
                equs = (List<Equ>)DBHelper.Query<Equ>(string.Format("select * from equ where MapId={0} order by equid", mapid));

            }
            catch (Exception e)
            {
                gMain.log.WriteLog("ReadEqu:" + e.Message);
                return null;
            }
            return equs;
        }
        /// <summary>
        /// 根据设备类型查询1个设备设备
        /// </summary>
        /// <param name="equType"></param>
        /// <returns></returns>
        public string GetFartherID(string equType)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = DBHelper.Query(string.Format("select EquID from equ where equtypeid ='{0}' order by equid limit 1", equType));
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog(e.Message);
                return null;
            }
            return null;
        }

        #region 结构物
        /// <summary>
        /// 根据地图ID查询隧道
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public List<tunnelInfo> GetTunnel(int mapid)
        {
            List<tunnelInfo> tunnels = new List<tunnelInfo>();
            try
            {
                tunnels = (List<tunnelInfo>)DBHelper.Query<tunnelInfo>(string.Format("select * from tunnel where MapId={0} order by BM", mapid));

            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetTunnel:" + e.Message);
                return null;
            }
            return tunnels;
        }
        /// <summary>
        /// 根据地图ID查询收费站
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public List<tollInfo> GetToll(int mapid)
        {
            List<tollInfo> tolls = new List<tollInfo>();
            try
            {
                tolls = (List<tollInfo>)DBHelper.Query<tollInfo>(string.Format("select * from toll where MapId={0} order by BM", mapid));

            }
            catch (Exception e)
            {
                gMain.log.WriteLog("GetToll:" + e.Message);
                return null;
            }
            return tolls;
        }
        #endregion
        #region plc设备信息
        //获取所有遥信
        public List<yx> GetYXs()
        {
            List<yx> yxs = new List<yx>();
            try
            {
                yxs = (List<yx>)DBHelper.Query<yx>("select * from yx");
            }
            catch (Exception e)
            {
                throw e;
            }
            return yxs;
        }
        //获取遥信配置
        public List<Yx_cfg> GetYXcfgs()
        {
            List<Yx_cfg> yxcfgs = new List<Yx_cfg>();
            try
            {
                yxcfgs = (List<Yx_cfg>)DBHelper.Query<Yx_cfg>("select * from yx_cfg");
            }
            catch (Exception e)
            {
                throw e;
            }
            return yxcfgs;
        }
        //获取所有遥控
        public List<yk> GetYks()
        {
            List<yk> yks = new List<yk>();
            try
            {
                yks = (List<yk>)DBHelper.Query<yk>("select * from yk");
            }
            catch (Exception e)
            {
                throw e;
            }
            return yks;
        }
        //获取遥控配置
        public List<Yk_cfg> GetYKcfgs()
        {
            List<Yk_cfg> ykcfgs = new List<Yk_cfg>();
            try
            {
                ykcfgs = (List<Yk_cfg>)DBHelper.Query<Yk_cfg>("select * from yk_cfg");
            }
            catch (Exception e)
            {
                throw e;
            }
            return ykcfgs;
        }
        //获取所有遥控
        public List<yc> GetYcs()
        {
            List<yc> ycs = new List<yc>();
            try
            {
                ycs = (List<yc>)DBHelper.Query<yc>("select * from yc");
            }
            catch (Exception e)
            {
                throw e;
            }
            return ycs;
        }
        //获取遥控配置
        public List<Yc_cfg> GetYccfgs()
        {
            List<Yc_cfg> yccfgs = new List<Yc_cfg>();
            try
            {
                yccfgs = (List<Yc_cfg>)DBHelper.Query<Yc_cfg>("select * from yc_cfg");
            }
            catch (Exception e)
            {
                throw e;
            }
            return yccfgs;
        }
        #endregion
    }
}
