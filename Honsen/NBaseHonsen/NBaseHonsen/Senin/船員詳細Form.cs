using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;
using NBaseHonsen.Senin.util;
using NBaseUtil;
using NBaseData.DS;

namespace NBaseHonsen.Senin
{
    public partial class 船員詳細Form : Form
    {
        private MsSenin senin;
        private string seninName_下船;

        private TreeListViewDelegate船員カード treeListViewDelegate船員カード;
        private TreeListViewDelegate免状免許 treeListViewDelegate免状免許;
        private TreeListViewDelegate講習 treeListViewDelegate講習;


        public 船員詳細Form(MsSenin senin, bool is乗船)
            : this(senin, is乗船, null)
        {
        }


        public 船員詳細Form(MsSenin senin, bool is乗船, string seninName_下船)
        {
            this.senin = senin;
            this.seninName_下船 = seninName_下船;

            InitializeComponent();
            Init(is乗船);
        }


        private void Init(bool is乗船)
        {
            InitFields();

            if (is乗船)
            {
                button乗船.Visible = true;
            }
            else
            {
                button閉じる.Location = button乗船.Location;
            }

            treeListViewDelegate船員カード = new TreeListViewDelegate船員カード(treeListView船員カード);
            treeListViewDelegate免状免許 = new TreeListViewDelegate免状免許(treeListView免状免許);

            Search船員カード();
            Search免状免許();


            // 川崎近海版は、非表示
            //treeListViewDelegate講習 = new TreeListViewDelegate講習(treeListView講習);
            //Search講習();

            // 川崎近海版は、下記のタブは未利用
            tabControl1.TabPages.RemoveByKey("作業服");
            tabControl1.TabPages.RemoveByKey("講習");
           
        }


        private void InitFields()
        {
            textBox職名.Text = SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);

            textBox姓.Text = senin.Sei;
            textBox名.Text = senin.Mei;
            textBox姓カナ.Text = senin.SeiKana;
            textBox名カナ.Text = senin.MeiKana;
            textBox氏名コード.Text = senin.ShimeiCode.Trim();


            // 川崎近海版は、以下、非表示
            #region
            //textBox区分.Text = senin.KubunStr;
            //textBox性別.Text = senin.SexStr;
            //textBox保険番号.Text = senin.HokenNo.Trim();

            //if (senin.Birthday != DateTime.MinValue)
            //{
            //    textBox生年月日.Text = senin.Birthday.ToShortDateString();
            //    textBox年齢.Text = DateTimeUtils.年齢計算(senin.Birthday).ToString();
            //}

            //textBox年齢.Text = DateTimeUtils.年齢計算(senin.Birthday).ToString();
            //textBox年金番号.Text = senin.NenkinNo.Trim();

            //if (senin.NyuushaDate != DateTime.MinValue)
            //{
            //    textBox入社年月日.Text = senin.NyuushaDate.ToShortDateString();
            //}

            //textBoxその他.Text = senin.Sonota;

            //if (senin.Picture != null)
            //{
            //    ImageConverter conv = new ImageConverter();
            //    Image img = (Image)conv.ConvertFrom(senin.Picture);

            //    pictureBox1.Image = img;
            //    label写真更新日.Text = senin.PictureDate.ToShortDateString();
            //}

            //textBox作業服上.Text = senin.ClothUe.Trim();
            //textBox作業服下.Text = senin.ClothShita.Trim();
            //textBox作業服靴.Text = senin.ClothKutsu.Trim();

            //dateTimePicker乗船日.Value = DateTime.Now;
            #endregion


            // 「船員カード」タブ
            dateTimePicker船員カード開始.Value = DateTimeUtils.年度開始日(DateTime.Now);
            dateTimePicker船員カード終了.Value = DateTimeUtils.年度終了日(DateTime.Now);
        }


        private void button乗船_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                //職名選択Form form = new 職名選択Form(senin, dateTimePicker乗船日.Value, seninName_下船);
                職名選択Form form = new 職名選択Form(senin, seninName_下船);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
            }
        }


        private bool ValidateFields()
        {
            //if (dateTimePicker乗船日.Value > DateTimeUtils.ToTo(DateTime.Now).AddSeconds(-1))
            //{
            //    dateTimePicker乗船日.BackColor = Color.Pink;
            //    MessageBox.Show("乗船日に未来の日付は指定できません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dateTimePicker乗船日.BackColor = Color.White;
            //    return false;
            //}

            return true;
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void button船員カード検索_Click(object sender, EventArgs e)
        {
            Search船員カード();
        }


        private void Search船員カード()
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsSeninID = senin.MsSeninID;
            filter.Start = dateTimePicker船員カード開始.Value;
            filter.End = dateTimePicker船員カード終了.Value;

            List<SiCard> cards = SiCard.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

            treeListViewDelegate船員カード.SetRows(cards);
        }


        private void Search免状免許()
        {
            //List<SiMenjou> menjous = SiMenjou.GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            List<SiMenjou> menjous = SiMenjou.GetRecordsByMsSeninID(同期Client.LOGIN_USER, senin.MsSeninID);

            treeListViewDelegate免状免許.SetRows(menjous);
        }

        private void Search講習()
        {
            SiKoushuFilter filter = new SiKoushuFilter();
            filter.MsSeninID = senin.MsSeninID;
            List<SiKoushu> koushus = SiKoushu.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

            treeListViewDelegate講習.SetRows(koushus);
        }
    }
}
