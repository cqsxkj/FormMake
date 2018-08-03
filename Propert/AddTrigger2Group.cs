using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowMake.Device;
using WindowMake.Entity;

namespace WindowMake.Propert
{
    public partial class AddTrigger2Group : Form
    {
        public AddTrigger2Group()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 绑定下拉菜单
        /// </summary>
        private void BindDrop()
        {
            DataStorage ds = new DataStorage();
            try
            {
                List<Gc> groupList = ds.GetAllGc();
                if(groupList.Count > 0)
                {
                    comboBox1.DataSource = groupList;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "GCID";
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("绑定预案下拉菜单错误！" + e.Message);
            }
        }

        /// <summary>
        /// 绑定已选触发设备到表
        /// </summary>
        /// <param name="selectedControls"></param>
        public void BindList(List<MyObject> selectedControls)
        {
            try
            {
                BindDrop();
                if (selectedControls.Count > 0)
                {
                    listView1.Clear();
                    listView1.GridLines = true; //显示表格线
                    listView1.View = View.Details;//显示表格细节
                    listView1.LabelEdit = true; //是否可编辑,ListView只可编辑第一列。
                    listView1.Scrollable = true;//有滚动条
                    listView1.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
                    listView1.FullRowSelect = true;//是否可以选择行

                    //添加表头
                    listView1.Columns.Add("equid", 50);
                    listView1.Columns.Add("设备名称", 150);
                    listView1.Columns.Add("报警类型", 80);
                    //添加各项
                    ListViewItem[] p = new ListViewItem[selectedControls.Count];
                    for (int i = 0; i < selectedControls.Count; i++)
                    {
                        switch (selectedControls[i].equ.EquTypeID)
                        {
                            case "F_L":
                                p[i] = new ListViewItem(new string[] { selectedControls[i].equ.EquID, selectedControls[i].equ.EquName, "63" });
                                break;
                            case "F_SB":
                                p[i] = new ListViewItem(new string[] { selectedControls[i].equ.EquID, selectedControls[i].equ.EquName, "64" });
                                break;
                            case "F_YG":
                                p[i] = new ListViewItem(new string[] { selectedControls[i].equ.EquID, selectedControls[i].equ.EquName, "65" });
                                break;
                            default:
                                break;
                        }
                    }
                    listView1.Items.AddRange(p);
                }
            }
            catch (Exception e)
            {
                gMain.log.WriteLog("添加触发设备到列表错误：" + e);
            }
        }
        private void bt_triggerDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            var gcid = comboBox1.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(gcid))
            {
                if (listView1.Items.Count > 0)
                {
                    try
                    {
                        DataStorage ds = new DataStorage();
                        List<Gc_triggerequ> triList = ds.GetGctriggerEqu(gcid);
                        //using (MapModelContainer container = new MapModelContainer())
                        //{
                        //    var trigList = (from a in container.gc_triggerequ where a.GCID == gcid select a).ToList();
                            for (int i = 0; i < listView1.Items.Count; i++)
                            {
                                var trigger = triList.Where(p => p.EquID == listView1.Items[i].SubItems[0].Text).FirstOrDefault();
                                if (trigger == null)
                                {
                                    int alarmID = int.Parse(listView1.Items[i].SubItems[2].Text);
                                    //container.gc_triggerequ.Add(new Gc_triggerequ { EquID = listView1.Items[i].SubItems[0].Text, GCID = gcid, AlarmTypeID = alarmID, EquRStateID = 0, IsAlarm = 0 });
                                }
                        }
                        //container.SaveChanges();
                        this.Hide();
                        //}
                    }
                    catch (Exception ex)
                    {
                        gMain.log.WriteLog("群控触发设备保存失败:" + ex);
                        MessageBox.Show("保存失败！");
                    }
                }
            }
        }
    }
}
