using System;
using System.Collections.Generic;
using System.Linq;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;

namespace WindowMake.Config
{
    /// <summary>
    /// 根据点位生成遥信遥控字符串
    /// </summary>
    public class AutoCreatString
    {
        public Dictionary<int, Command> commandDic = new Dictionary<int, Command>();
        #region 字符串生成
        /// <summary>
        /// 根据点位自动生成遥信字符串和遥控字符串
        /// </summary>
        public void AutoCreateYX(List<MyObject> m_ObjectList)
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
                    gMain.log.WriteLog("自动生成遥信字符串成功！");
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("生成遥信字符串错误：" + e);
            }
        }
        /// <summary>
        /// 根据点位自动生成遥控字符串
        /// </summary>
        public void AutoCreateYK(List<MyObject> m_ObjectList)
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
                    gMain.log.WriteLog("自动生成遥控字符串成功！");
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("AutoCreateYK:" + e);
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
    }
}
