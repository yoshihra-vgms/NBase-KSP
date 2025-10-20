using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    public partial class 交代者検索Form : Form
    {
        private SiCard card = null;
        private SiCard replacementCard = null;


        public SiCard Selected
        {
            get
            {
                return replacementCard;
            }
        }
        public 交代者検索Form(SiCard card)
        {
            this.card = card;
            InitializeComponent();
            Init();
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            Search船員();

        }

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("交代した船員を選択してください");
                return;
            }
            replacementCard = dataGridView1.SelectedRows[0].Cells[0].Value as SiCard;

            DialogResult = DialogResult.Yes;
            Close();
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void Init()
        {
            comboBox職名.Items.Clear();
            comboBox職名.Items.Add("");
            comboBox職名.Items.AddRange(SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser).ToArray());


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("obj", typeof(object)));
            dt.Columns.Add(new DataColumn("氏名", typeof(string)));
            dt.Columns.Add(new DataColumn("職名", typeof(string)));
            dt.Columns.Add(new DataColumn("乗船職", typeof(string)));
            dt.Columns.Add(new DataColumn("乗船日", typeof(string)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
        }



        private void Clear()
        {
            comboBox職名.SelectedItem = null;
            textBox氏名.Text = null;
        }



        internal void Search船員()
        {
            SiCardFilter filter = CreateFilter();


            List<SiCard> results = null;


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                results = serviceClient.BLC_交代者検索(NBaseCommon.Common.LoginUser, filter);
            }



            if (results.Any(obj => obj.SiCardID == card.SiCardID))
            {
                results = results.Where(obj => obj.SiCardID != card.SiCardID).ToList(); // 自分自身を除く
            }

            SetRows(results);

        }

        /// <summary>
        /// 検索条件をセットする
        /// </summary>
        /// <returns></returns>
        #region private SiCardFilter CreateFilter()
        private SiCardFilter CreateFilter()
        {
            SiCardFilter filter = new SiCardFilter();

            // 検索条件：職名（乗船職）
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                filter.CardMsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            // 検索条件：氏名
            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text.ToUpper();
            }

            // 対象船に乗船している船員が対象
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsVesselIDs.Add(card.MsVesselID);

            // 対象者の下船日前後１０日間に乗船した船員
            filter.Start = card.EndDate.AddDays(-10);
            filter.End = card.EndDate.AddDays(10);

            return filter;
        }
        #endregion

        private void SetRows(List<SiCard> cards)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            dt.Clear();

            foreach (SiCard c in cards)
            {
                DataRow row = dt.NewRow();

                row[0] = c;
                row[1] = c.SeninName;
                row[2] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, c.SeninMsSiShokumeiID);
                row[3] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, c.CardMsSiShokumeiID);
                row[4] = c.StartDate.ToShortDateString();

                dt.Rows.Add(row);
            }
        }
    }
}
