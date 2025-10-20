using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseHonsen.Document.Contorol;
using SyncClient;
using ORMapping;
using NBaseHonsen.Document.BLC;
using NBaseCommon;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseHonsen.Document
{
    public partial class 状況確認詳細Form : ExForm
    {
        private string DIALOG_TITLE = "状況確認詳細";
        private List<JokyoKakuninTableRow> jokyoKakuninTableRows = null;

        private 状況確認一覧Form ParentWindow = null;
        public 状況確認一覧Row RowData = null;

        private bool IsAction = false;
        
        public 状況確認詳細Form(状況確認一覧Form parent)
        {
            InitializeComponent();
            ParentWindow = parent;
        }

        private void 状況確認詳細Form_Load(object sender, EventArgs e)
        {
            CreateRows();
            InitForm();

            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void 状況確認詳細Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// 「↑（１つ戻る）」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_１つ戻る_Click(object sender, EventArgs e)
        private void button_１つ戻る_Click(object sender, EventArgs e)
        {
            ParentWindow.戻る();

        }
        #endregion

        /// <summary>
        /// 「↓（１つ進む）」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_１つ進む_Click(object sender, EventArgs e)
        private void button_１つ進む_Click(object sender, EventArgs e)
        {
            ParentWindow.進む();

        }
        #endregion

        /// <summary>
        /// 「コメント登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_コメント登録_Click(object sender, EventArgs e)
        private void button_コメント登録_Click(object sender, EventArgs e)
        {
            if (表示確認() == false)
            {
                return;
            }
            コメント登録Form form = new コメント登録Form();
            form.parentForm = this; // 2012.02 : Add
            form.RowData = RowData;
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Normal; // 2012.02 : Add

                Refresh();

                IsAction = true;
            }
        }
        #endregion

        /// <summary>
        /// 「内容確認」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_内容確認_Click(object sender, EventArgs e)
        private void button_内容確認_Click(object sender, EventArgs e)
        {
            if (表示確認() == false)
            {
                return;
            }

            // 「乗船リスト」画面を表示して、確認者を取得する
            船員リストForm form = new 船員リストForm();
            form.ShowDialog();
            List<string> checkedUserIds = form.CheckedUserIds;

            // 「乗船リスト」画面でチェックされたユーザすべてに確認状況情報(DmKakuninJokyo)を作成する
            #region
            int linkSaki = -1;
            string linkSakiId = "";
            if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                linkSakiId = RowData.DmKanriKirokuId;
            }
            else
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                linkSakiId = RowData.DmKoubunshoKisokuId;
            }
            bool ret = 内容確認処理.Honsen登録(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID, linkSaki, linkSakiId, checkedUserIds);
            #endregion

            if (ret == true)
            {
                MessageBox.Show("内容確認しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Refresh();

                IsAction = true;
            }
            else
            {
                MessageBox.Show("内容確認に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 「回覧終了」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_回覧終了_Click(object sender, EventArgs e)
        private void button_回覧終了_Click(object sender, EventArgs e)
        {
            if (表示確認() == false)
            {
                return;
            }

            bool ret;

            DmKanryoInfo kanryoInfo = new DmKanryoInfo();
            kanryoInfo.DmKanryoInfoID = System.Guid.NewGuid().ToString();
            kanryoInfo.RenewDate = DateTime.Now;
            kanryoInfo.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

            kanryoInfo.KanryoDate = DateTime.Now;
            kanryoInfo.MsUserID = 同期Client.LOGIN_USER.MsUserID;
            if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            {
                kanryoInfo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                kanryoInfo.LinkSakiID = RowData.DmKanriKirokuId;
            }
            else
            {
                kanryoInfo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                kanryoInfo.LinkSakiID = RowData.DmKoubunshoKisokuId;
            }

            ret = NBaseHonsen.Document.BLC.回覧終了処理.Honsen登録(同期Client.LOGIN_USER, kanryoInfo);
            if (ret == true)
            {
                RowData.Status = (int)NBaseData.DS.DocConstants.StatusEnum.完了;
                RowData.完了日 = kanryoInfo.KanryoDate;
                RowData.完了者 = 同期Client.LOGIN_USER.FullName;

                ParentWindow.回覧終了(RowData);
                MessageBox.Show("回覧終了しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Refresh();

                IsAction = true;
            }
            else
            {
                MessageBox.Show("回覧終了に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        /// <summary>
        /// フォームに情報をセットする
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            int linkSaki = -1;
            string linkSakiId = "";
            if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                linkSakiId = RowData.DmKanriKirokuId;
            }
            else
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                linkSakiId = RowData.DmKoubunshoKisokuId;
            }

            #region 基本情報をセットする

            textBox_Publisher.Text = "： " + RowData.発行元;
            textBox_VesselName.Text = "： " + RowData.船名;
            textBox_IssueDate.Text = "： " + RowData.発行日.ToShortDateString();

            textBox_Bunrui.Text = "： " + RowData.分類名;
            textBox_Shoubunrui.Text = "： " + RowData.小分類名;
            textBox_BunshoNo.Text = "： " + RowData.文書番号;
            //textBox_BunshoName.Text = "： " + RowData.文書名;
            linkLabel_BunshoName.Text = RowData.文書名;

            pictureBox1.Image = RowData.GetFileImg();
            linkLabel_BunshoName.Text = RowData.文書名;
            textBox_Bikou.Text = RowData.備考;
            #endregion

            #region ボタンの有効/無効をセットする
            SetButton();
            #endregion

            #region 状況一覧をセットする

            foreach (JokyoKakuninTableRow jokyoKakuninTableRow in jokyoKakuninTableRows)
            {
                jokyoKakuninTableRow.Clear();
            }
            //jokyoKakuninTableRow1.Set項目名("登録者");
            //jokyoKakuninTableRow2.Set項目名("役員");
            //jokyoKakuninTableRow3.Set項目名("管理責任者");
            //jokyoKakuninTableRow4.Set項目名("安全運航","海技Ｔ");
            //jokyoKakuninTableRow5.Set項目名("船舶管理","技術Ｔ");
            //jokyoKakuninTableRow6.Set項目名("船員Ｔ");
            //jokyoKakuninTableRow9.Set項目名("IT・DX推進室");
            //jokyoKakuninTableRow10.Set項目名("本船");

            List<DmPublisher> dmPublishers = null;
            List<DmKakuninJokyo> dmKakuninJokyos = null;
            List<DmKoukaiSaki> dmKoukaisakis = null;
            List<DmDocComment> dmComments = null;
            dmPublishers = DmPublisher.GetRecordsByLinkSakiID(同期Client.LOGIN_USER, linkSakiId);
            dmKakuninJokyos = DmKakuninJokyo.GetRecordsByLinkSaki(同期Client.LOGIN_USER, linkSaki, linkSakiId);
            dmKoukaisakis = DmKoukaiSaki.GetRecordsByLinkSakiID(同期Client.LOGIN_USER, linkSakiId);
            dmComments = DmDocComment.GetRecordsByLinkSaki(同期Client.LOGIN_USER, linkSaki, linkSakiId);


            //jokyoKakuninTableRow1.Set状況(dmPublishers);

            int rowIndex = 0;

            jokyoKakuninTableRows[rowIndex].Set項目名("登録者");
            jokyoKakuninTableRows[rowIndex].Set状況(dmPublishers);
            rowIndex++;

            bool 要確認 = false;

            //要確認 = 要確認チェック(dmKoukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.会長社長);
            //var kaicyo_shacyo = from p in dmKakuninJokyos
            //                    where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長
            //                    && ( p.KaichoFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON || p.ShachoFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON )
            //                    orderby p.KaichoFlag descending, p.ShachoFlag descending, p.KakuninDate ascending
            //                    select p;
            //jokyoKakuninTableRow2.Set状況(要確認, kaicyo_shacyo, dmComments);

            //要確認 = 要確認チェック(dmKoukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者);
            //var sekininsha = from p in dmKakuninJokyos
            //                 where p.SekininshaFlag == (int)NBaseData.DS.DocConstants.FlagEnum.ON
            //                 && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
            //                 orderby p.KakuninDate ascending
            //                 select p;
            //jokyoKakuninTableRow3.Set状況(要確認, sekininsha, dmComments);

            var userList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER);
            foreach (DocConstants.ClassItem item in userList)
            {
                if (StringUtils.Empty(item.viewName2))
                {
                    jokyoKakuninTableRows[rowIndex].Set項目名(item.viewName1);
                }
                else
                {
                    jokyoKakuninTableRows[rowIndex].Set項目名(item.viewName1, item.viewName2);
                }
                要確認 = 要確認チェック(dmKoukaisakis, (int)item.enumRole);
                var jokyous = from p in dmKakuninJokyos
                              where p.KoukaiSaki == (int)item.enumRole
                              orderby p.DocFlag_CEO descending,
                                      p.DocFlag_Admin descending,
                                      p.DocFlag_MsiFerry descending,
                                      p.DocFlag_CrewFerry descending,
                                      p.DocFlag_TsiFerry descending,
                                      p.DocFlag_MsiCargo descending,
                                      p.DocFlag_CrewCargo descending,
                                      p.DocFlag_TsiCargo descending,
                                      p.DocFlag_Officer descending,
                                      p.DocFlag_SdManager descending,
                                      p.DocFlag_GL descending,
                                      p.DocFlag_TL descending,
                                      p.KakuninDate ascending
                              select p;
                jokyoKakuninTableRows[rowIndex].Set状況(要確認, jokyous, dmComments);
                rowIndex++;
            }


            //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.安全運航海技Ｔ).ToString());
            //var 安全運航海技Ｔs = from p in dmKakuninJokyos
            //                where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
            //                && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.安全運航海技Ｔ).ToString()
            //                orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
            //                select p;
            //jokyoKakuninTableRow4.Set状況(要確認, 安全運航海技Ｔs, dmComments);

            //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.船舶管理技術Ｔ).ToString());
            //var 船舶管理技術Ｔs = from p in dmKakuninJokyos

            //           where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
            //           && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.船舶管理技術Ｔ).ToString()
            //           orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
            //           select p;
            //jokyoKakuninTableRow5.Set状況(要確認, 船舶管理技術Ｔs, dmComments);
            //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.船員Ｔ).ToString());
            //var 船員Ｔs = from p in dmKakuninJokyos
            //           where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
            //           && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.船員Ｔ).ToString()
            //           orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
            //           select p;
            //jokyoKakuninTableRow6.Set状況(要確認, 船員Ｔs, dmComments);

            //要確認 = 要確認チェック_部門(dmKoukaisakis, ((int)MsBumon.MsBumonIdEnum.ITDX推進室).ToString());
            //var ITDX推進室s = from p in dmKakuninJokyos
            //               where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
            //               && p.MsBumonID == ((int)MsBumon.MsBumonIdEnum.ITDX推進室).ToString()
            //               orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending
            //               select p;
            //jokyoKakuninTableRow9.Set状況(要確認, ITDX推進室s, dmComments);

            var bumonList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.BUMON);
            foreach (DocConstants.ClassItem item in bumonList)
            {
                if (StringUtils.Empty(item.viewName2))
                {
                    jokyoKakuninTableRows[rowIndex].Set項目名(item.viewName1);
                }
                else
                {
                    jokyoKakuninTableRows[rowIndex].Set項目名(item.viewName1, item.viewName2);
                }
                要確認 = 要確認チェック_部門(dmKoukaisakis, item.bumonId);
                var jokyous = from p in dmKakuninJokyos
                              where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                              && p.MsBumonID == item.bumonId
                              orderby p.DocFlag_GL descending, p.DocFlag_TL descending, p.KakuninDate ascending
                              select p;
                jokyoKakuninTableRows[rowIndex].Set状況(要確認, jokyous, dmComments);
                rowIndex++;
            }


            //要確認 = 要確認チェック_船(dmKoukaisakis, 同期Client.LOGIN_VESSEL.MsVesselID);
            //var 船s = from p in dmKakuninJokyos
            //         where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船
            //         && p.MsVesselID == 同期Client.LOGIN_VESSEL.MsVesselID
            //         orderby p.GLFlag descending, p.TLFlag descending, p.KakuninDate ascending, p.ShowOrder ascending
            //         select p;
            //jokyoKakuninTableRow10.Set状況(要確認, 船s, dmComments);

            jokyoKakuninTableRows[rowIndex].Set項目名("本船");
            jokyoKakuninTableRows[rowIndex].Set状況(dmPublishers);

            要確認 = 要確認チェック_船(dmKoukaisakis, 同期Client.LOGIN_VESSEL.MsVesselID);
            var 船s = from p in dmKakuninJokyos
                     where p.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船
                     && p.MsVesselID == RowData.KoukaiSakiVesselId
                     orderby p.DocFlag_GL descending, p.DocFlag_TL descending, p.KakuninDate ascending, p.ShowOrder ascending
                     select p;
            jokyoKakuninTableRows[rowIndex].Set状況(要確認, 船s, dmComments);


            #endregion
        }
        #endregion

        private void CreateRows()
        {
            jokyoKakuninTableRows = new List<JokyoKakuninTableRow>();

            int height = 2;
            int rowCount = DocConstants.ClassItemList().Count + 2;
            for (int i = 0; i < rowCount; i++)
            {
                JokyoKakuninTableRow jokyoKakuninTableRow = CreateJokyoKakuninTableRow(i);

                height += jokyoKakuninTableRow.Height;
                this.flowLayoutPanel_table.Controls.Add(jokyoKakuninTableRow);
                jokyoKakuninTableRows.Add(jokyoKakuninTableRow);
                jokyoKakuninTableRow.Clear();
            }

            this.flowLayoutPanel_table.Size = new Size(this.flowLayoutPanel_table.Size.Width, height);
        }

        private JokyoKakuninTableRow CreateJokyoKakuninTableRow(int index)
        {
            JokyoKakuninTableRow jokyoKakuninTableRow = new Document.Contorol.JokyoKakuninTableRow();
            jokyoKakuninTableRow.BackColor = System.Drawing.Color.White;
            jokyoKakuninTableRow.Location = new System.Drawing.Point(0, 0);
            jokyoKakuninTableRow.Margin = new System.Windows.Forms.Padding(0);
            jokyoKakuninTableRow.Name = "jokyoKakuninTableRow" + index.ToString();
            jokyoKakuninTableRow.Size = new System.Drawing.Size(this.flowLayoutPanel_table.Size.Width, 70);
            jokyoKakuninTableRow.TabIndex = 20 + index;

            return jokyoKakuninTableRow;
        }

        /// <summary>
        /// 選択行の文書を表示しているかの確認
        /// </summary>
        /// <returns></returns>
        #region private bool 表示確認()
        private bool 表示確認()
        {
            int linkSaki = -1;
            string linkSakiId = "";
            int koukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
            if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                linkSakiId = RowData.DmKanriKirokuId;
            }
            else
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                linkSakiId = RowData.DmKoubunshoKisokuId;
            }
            DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(同期Client.LOGIN_USER, linkSaki, linkSakiId, koukaiSaki, 同期Client.LOGIN_USER.MsUserID);
            if (kakuninJokyo == null)
            {
                MessageBox.Show("対象となる文書は表示していません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// ボタンの有効/無効化
        /// </summary>
        #region private void SetButton()
        private void SetButton()
        {
            // 一旦、無効にする
            button_コメント登録.Enabled = false;
            button_内容確認.Enabled = false;
            button_回覧終了.Enabled = false;

            // 完了している場合、何もしない
            if (RowData.Status == (int)NBaseData.DS.DocConstants.StatusEnum.完了)
            {
                return;
            }

            // 安全管理・海務Ｇのみが使用できるボタンの制御
            if (同期Client.LOGIN_USER.BumonID == ((int)MsBumon.MsBumonIdEnum.海務部).ToString())
            {
                button_回覧終了.Enabled = true;
            }

            // 公開先に設定されていれば使用できる
            foreach (DmKoukaiSaki koukaisaki in RowData.KoukaisakiList)
            {
                if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    if (同期Client.LOGIN_VESSEL.MsVesselID == koukaisaki.MsVesselID)
                    {
                        button_コメント登録.Enabled = true;
                        button_内容確認.Enabled = true;
                        break;
                    }
                }
            }
        }
        #endregion

        private bool 要確認チェック(List<DmKoukaiSaki> koukaisakis, int koukaisaki)
        {
            bool ret = false;
            
            var tmp = from p in koukaisakis
                      where p.KoukaiSaki == koukaisaki
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }

        private bool 要確認チェック_部門(List<DmKoukaiSaki> koukaisakis, string bumonId)
        {
            bool ret = false;

            var tmp = from p in koukaisakis
                      where p.MsBumonID == bumonId
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }

        private bool 要確認チェック_船(List<DmKoukaiSaki> koukaisakis, int vesselId)
        {
            bool ret = false;

            ret = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.船);
            if (ret)
            {
                var tmp = from p in koukaisakis
                          where p.MsVesselID == vesselId
                          select p;

                if (tmp.Count<DmKoukaiSaki>() > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }



        public override void Refresh()
        {
            base.Refresh();

            InitForm();
        }

        private void 状況確認詳細Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAction)
            {
                ParentWindow.再検索();
            }
        }

        private void linkLabel_BunshoName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;
            int linkSaki = -1;
            string linkSakiId = "";

            try
            {
                Cursor = Cursors.WaitCursor;


                // 選択行のファイルデータの確認
                #region
                if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                {
                    linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                    linkSakiId = RowData.DmKanriKirokuId;

                    DmKanriKirokuFile kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, RowData.DmKanriKirokuId);
                    if (kanriKirokuFile == null)
                    {
                        MessageBox.Show("管理記録ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fileName = kanriKirokuFile.FileName;
                    fileData = kanriKirokuFile.Data;
                    id = kanriKirokuFile.DmKanriKirokuFileID;
                }
                else
                {
                    linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                    linkSakiId = RowData.DmKoubunshoKisokuId;

                    DmKoubunshoKisokuFile koubunshoKisokuFile = DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(同期Client.LOGIN_USER, RowData.DmKoubunshoKisokuId);
                    if (koubunshoKisokuFile == null)
                    {
                        MessageBox.Show("公文書_規則ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fileName = koubunshoKisokuFile.FileName;
                    fileData = koubunshoKisokuFile.Data;
                    id = koubunshoKisokuFile.DmKoubunshoKisokuFileID;
                }
                #endregion

                // ファイルの表示
                NBaseCommon.FileView.View(id, fileName, fileData);

                // 表示したことを確認状況（表示のみ）としてＤＢに登録
                #region
                DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(同期Client.LOGIN_USER, linkSaki, linkSakiId, (int)NBaseData.DS.DocConstants.RoleEnum.船, 同期Client.LOGIN_USER.MsUserID);
                if (kakuninJokyo == null)
                {
                    kakuninJokyo = new DmKakuninJokyo();
                    kakuninJokyo.ViewDate = DateTime.Now;
                    kakuninJokyo.MsUserID = 同期Client.LOGIN_USER.MsUserID;

                    kakuninJokyo.DocFlag_CEO = 同期Client.LOGIN_USER.DocFlag_CEO;
                    kakuninJokyo.DocFlag_Admin = 同期Client.LOGIN_USER.DocFlag_Admin;
                    kakuninJokyo.DocFlag_MsiFerry = 同期Client.LOGIN_USER.DocFlag_MsiFerry;
                    kakuninJokyo.DocFlag_CrewFerry = 同期Client.LOGIN_USER.DocFlag_CrewFerry;
                    kakuninJokyo.DocFlag_TsiFerry = 同期Client.LOGIN_USER.DocFlag_TsiFerry;
                    //kakuninJokyo.DocFlag_MsiCargo = 同期Client.LOGIN_USER.DocFlag_MsiCargo;
                    //kakuninJokyo.DocFlag_CrewCargo = 同期Client.LOGIN_USER.DocFlag_CrewCargo;
                    //kakuninJokyo.DocFlag_TsiCargo = 同期Client.LOGIN_USER.DocFlag_TsiCargo;
                    kakuninJokyo.DocFlag_Officer = 同期Client.LOGIN_USER.DocFlag_Officer;
                    //kakuninJokyo.DocFlag_SdManager = 同期Client.LOGIN_USER.DocFlag_SdManager;
                    kakuninJokyo.DocFlag_GL = 同期Client.LOGIN_USER.DocFlag_GL;
                    kakuninJokyo.DocFlag_TL = 同期Client.LOGIN_USER.DocFlag_TL;



                    kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                    kakuninJokyo.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                    if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                    {
                        kakuninJokyo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                        kakuninJokyo.LinkSakiID = RowData.DmKanriKirokuId;
                    }
                    else
                    {
                        kakuninJokyo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                        kakuninJokyo.LinkSakiID = RowData.DmKoubunshoKisokuId;
                    }

                    kakuninJokyo.DmKakuninJokyoID = System.Guid.NewGuid().ToString();
                    kakuninJokyo.RenewDate = DateTime.Now;
                    kakuninJokyo.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                    bool ret = kakuninJokyo.InsertRecord(同期Client.LOGIN_USER);
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;
            string linkSakiId = "";
            //int linkSaki = -1;
            string filePath = "";

            try
            {
                // 選択行のファイルデータの確認
                #region
                if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                {
                    //linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                    linkSakiId = RowData.DmKanriKirokuId;

                    DmKanriKirokuFile kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, RowData.DmKanriKirokuId);
                    if (kanriKirokuFile == null)
                    {
                        MessageBox.Show("管理記録ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    fileName = kanriKirokuFile.FileName;
                    fileData = kanriKirokuFile.Data;
                    id = kanriKirokuFile.DmKanriKirokuFileID;
                }
                else
                {
                    //linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                    linkSakiId = RowData.DmKoubunshoKisokuId;

                    DmKoubunshoKisokuFile koubunshoKisokuFile = DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(同期Client.LOGIN_USER, RowData.DmKoubunshoKisokuId);
                    if (koubunshoKisokuFile == null)
                    {
                        MessageBox.Show("公文書_規則ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    fileName = koubunshoKisokuFile.FileName;
                    fileData = koubunshoKisokuFile.Data;
                    id = koubunshoKisokuFile.DmKoubunshoKisokuFileID;
                }
                #endregion

                // ファイルの表示
                filePath = NBaseCommon.FileView.CreateFile(id, fileName, fileData);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ファイルドロップ形式でDataObjectオブジェクトを作成する
            DataObject dataObj = new DataObject();
            string[] fileNames = new string[1];
            fileNames[0] = filePath;
            dataObj.SetData(DataFormats.FileDrop, fileNames);

            //ドラッグを開始する
            pictureBox1.DoDragDrop(dataObj, DragDropEffects.Copy);
        }


        /// <summary>
        /// 「備考編集」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_備考編集_Click(object sender, EventArgs e)
        {
            NBaseData.DS.DocConstants.LinkSakiEnum linkSaki;
            string id;
            if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            {
                linkSaki = NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                id = RowData.DmKanriKirokuId;
            }
            else
            {
                linkSaki = NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                id = RowData.DmKoubunshoKisokuId;
            }

            備考編集Form form = new 備考編集Form(linkSaki, id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RowData.備考 = form.Get備考;

                ParentWindow.備考編集(RowData);
                Refresh();

                IsAction = true;

            }
        }
    }
}
