using System;
using System.Collections.Generic;
using System.Linq;
using WindowMake.DB;
using WindowMake.Device;
using WindowMake.Entity;

namespace WindowMake
{
    public class NameTool
    {
        /// <summary>
        /// 命名字典
        /// </summary>
        private static Dictionary<string, int> NameDic = new Dictionary<string, int>();
        static NameTool()
        {
            try
            {
                IList<Equ> equs = DBHelper.Query<Equ>("SELECT MAX(EquID) EquID, EquTypeID FROM `equ`  GROUP BY EquTypeID;");
                foreach (var item in equs)
                {
                    string[] ids = item.EquID.Split('_');
                    int max;
                    if (!int.TryParse(ids.Last(), out max))
                    {
                        //max = int.Parse(ids.Last(), System.Globalization.NumberStyles.HexNumber);
                    }
                    NameDic.Add(item.EquTypeID, max);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("nameTool:" + e.Message);
            }
        }
        /// <summary>
        /// create equid 
        /// </summary>
        /// <param name="equtypeid"></param>
        /// <returns></returns>
        public static string CreateEquId(MyObject.ObjectType equtypeid)
        {
            if (!NameDic.ContainsKey(equtypeid.ToString()))
            {
                NameDic.Add(equtypeid.ToString(), 0);
            }
            return equtypeid + "_" + (++NameDic[equtypeid.ToString()]).ToString("0000");
        }
    }
}
