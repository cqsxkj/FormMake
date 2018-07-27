using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMake.Device;

namespace WindowMake.Propert
{
    public partial class CreateAddDialog : Form
    {
        private Dictionary<MyObject.ObjectType, string> equList;
        public CreateAddDialog()
        {
            InitializeComponent();
            Init();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void Init()
        {
            #region 设备列表
            equList = new Dictionary<MyObject.ObjectType, string>();
            equList.Add(MyObject.ObjectType.P_TL5_Left, "双显向左");
            equList.Add(MyObject.ObjectType.P_TL5_Right, "双显向右");
            equList.Add(MyObject.ObjectType.P_L, "基本照明");
            equList.Add(MyObject.ObjectType.P_JF, "射流风机");
            equList.Add(MyObject.ObjectType.P_LJQ, "加强照明");
            equList.Add(MyObject.ObjectType.P_TL2_Close, "单显禁止");
            equList.Add(MyObject.ObjectType.P_TL2_Down, "单显向下");
            equList.Add(MyObject.ObjectType.P_TL2_UP, "单显向上");
            equList.Add(MyObject.ObjectType.P_TL3_Left, "2显向下（转弯灯）");
            equList.Add(MyObject.ObjectType.P_TL3_Right, "2显向上（转弯灯）");
            equList.Add(MyObject.ObjectType.EP_R, "紧急电话广播");
            equList.Add(MyObject.ObjectType.EP_T, "紧急电话");
            equList.Add(MyObject.ObjectType.F_L, "火灾光纤");
            equList.Add(MyObject.ObjectType.F_SB, "火灾手报");
            equList.Add(MyObject.ObjectType.F_YG, "火灾烟感");
            equList.Add(MyObject.ObjectType.P_CO, "P_CO检测");
            equList.Add(MyObject.ObjectType.P_GJ, "光强检测");
            equList.Add(MyObject.ObjectType.P_HL2, "四显交通灯");
            equList.Add(MyObject.ObjectType.P_HL, "三显交通灯");
            equList.Add(MyObject.ObjectType.P_LLDI, "液位检测");
            equList.Add(MyObject.ObjectType.P_LYJ, "应急照明");
            equList.Add(MyObject.ObjectType.CF, "F情报板");
            equList.Add(MyObject.ObjectType.CL, "立柱情报板");
            equList.Add(MyObject.ObjectType.CM, "门架情报板");
            equList.Add(MyObject.ObjectType.VC, "车检器");
            equList.Add(MyObject.ObjectType.VI, "气象仪");
            equList.Add(MyObject.ObjectType.P, "PLC主机");
            equList.Add(MyObject.ObjectType.P_P, "水泵");
            equList.Add(MyObject.ObjectType.P_TD, "横通门");
            equList.Add(MyObject.ObjectType.P_TW, "风速风向");
            equList.Add(MyObject.ObjectType.P_VI, "VI检测");
            equList.Add(MyObject.ObjectType.S, "限速标志");
            equList.Add(MyObject.ObjectType.TV_CCTV_Ball, "球机");
            equList.Add(MyObject.ObjectType.TV_CCTV_E, "事件检测摄像机");
            equList.Add(MyObject.ObjectType.TV_CCTV_Gun, "枪机");
            equList.Add(MyObject.ObjectType.TV, "流媒体服务器");
            equList.Add(MyObject.ObjectType.I, "凝冰喷洒");
            equList.Add(MyObject.ObjectType.PK, "停车场车位");
            equList.Add(MyObject.ObjectType.P_AF, "轴流风机");
            equList.Add(MyObject.ObjectType.P_EPM, "电力监控");
            equList.Add(MyObject.ObjectType.P_CL, "车行横通（旧）");
            equList.Add(MyObject.ObjectType.P_GS, "瓦斯检测");
            equList.Add(MyObject.ObjectType.HL, "直控交通灯");
            equList.Add(MyObject.ObjectType.E, "事件检测主机");
            equList.Add(MyObject.ObjectType.EM, "环境检测主机");
            equList.Add(MyObject.ObjectType.EM_CH4, "CH4检测");
            equList.Add(MyObject.ObjectType.EM_CO, "CO检测");
            equList.Add(MyObject.ObjectType.EM_O2, "氧气监测");
            equList.Add(MyObject.ObjectType.F, "火灾主机");
            equList.Add(MyObject.ObjectType.EP, "紧急电话主机");
            #endregion
            BindingSource typeList = new BindingSource();
            typeList.DataSource = equList;
            cb_equtype.DataSource = typeList;
            cb_equtype.ValueMember = "key";
            cb_equtype.DisplayMember = "value";
            //cb_equtype.DataSource = equList;
        }
    }
}
