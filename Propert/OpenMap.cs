using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMake.DB;
using WindowMake.Entity;

namespace WindowMake.Propert
{
    public partial class OpenMap : Form
    {
        public OpenMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定地图下拉
        /// </summary>
        public void BindMap()
        {
            try
            {
                IList<Map> maps = DBHelper.Query<Map>("select * from map");
                comboBoxMapList.DataSource = maps;
                comboBoxMapList.DisplayMember = "MapName";
                comboBoxMapList.ValueMember = "MapID";
            }
            catch (Exception)
            {
            }
        }

        public string MapId
        {
            get { return comboBoxMapList.SelectedValue.ToString(); }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
