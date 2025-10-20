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
using ServiceReferences.NBaseService;
using Document.Contorol;
using NBaseCommon;
using NBaseData.DS;
using NBaseUtil;

namespace Document
{
    public partial class 状況確認詳細Form : ExForm
    {
        private string DIALOG_TITLE = "回覧状況";
        private List<JokyoKakuninTableRow> jokyoKakuninTableRows = null;

        private 状況確認一覧Form ParentWindow = null;
        public 状況確認一覧Row RowData = null;

        private bool IsAction = false;
        
        public 状況確認詳細Form(状況確認一覧Form parent)
        {
            InitializeComponent();
            ParentWindow = parent;
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", DIALOG_TITLE, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
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
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_削除_Click(object sender, EventArgs e)
        private void button_削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int linkSaki = -1;
            string linkSakiId = "";
            DmKanriKiroku kanriKiroku = null;
            DmKoubunshoKisoku koubunshoKisoku = null;
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
            List<DmKakuninJokyo> kakuninJokyos = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kakuninJokyos = serviceClient.DmKakuninJokyo_GetRecordsByLinkSaki(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId);

                if (linkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録)
                {
                    kanriKiroku = serviceClient.DmKanriKiroku_GetRecord(NBaseCommon.Common.LoginUser, linkSakiId);
                }
                else
                {
                    koubunshoKisoku = serviceClient.DmKoubunshoKisoku_GetRecord(NBaseCommon.Common.LoginUser, linkSakiId);
                }
            }
            bool is削除可 = true;
            foreach (DmKakuninJokyo kakuninJokyo in kakuninJokyos)
            {
                if (kakuninJokyo.KakuninDate == DateTime.MinValue)
                {
                    continue;
                }
                if (kakuninJokyo.MsVesselID > 0)
                {
                    // 船で確認している
                    is削除可 = false;
                    break;
                }
                if (kakuninJokyo.MsBumonID != NBaseCommon.Common.LoginUser.BumonID)
                {
                    // 他部署で確認している
                    is削除可 = false;
                    break;
                }
            }
            if (is削除可 == false)
            {
                //MessageBox.Show("他部門にて確認されているため削除できません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                if (MessageBox.Show("他部門にて確認されています。削除しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            // 削除処理
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (linkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録)
                {
                    kanriKiroku.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
                    kanriKiroku.RenewDate = DateTime.Now;
                    kanriKiroku.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    ret = serviceClient.DmKanriKiroku_UpdateRecord(NBaseCommon.Common.LoginUser, kanriKiroku);
                }
                else
                {
                    koubunshoKisoku.DeleteFlag = (int)NBaseData.DS.DocConstants.FlagEnum.ON;
                    koubunshoKisoku.RenewDate = DateTime.Now;
                    koubunshoKisoku.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    ret = serviceClient.DmKoubunshoKisoku_UpdateRecord(NBaseCommon.Common.LoginUser, koubunshoKisoku);
                }
            }
            if (ret == true)
            {
                IsAction = true;
                MessageBox.Show("削除しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("削除に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 「公開先」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_公開先_Click(object sender, EventArgs e)
        private void button_公開先_Click(object sender, EventArgs e)
        {
            公開先設定Form form = new 公開先設定Form();
            form.RowData = RowData;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Refresh();

                IsAction = true;
            }
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
            DmKakuninJokyo kakuninJokyo = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kakuninJokyo = serviceClient.DmKakuninJokyo_GetRecordByLinkSakiUser(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId, NBaseCommon.Common.LoginUser.MsUserID);
            }
            kakuninJokyo.KakuninDate = DateTime.Now;
            kakuninJokyo.MsUserID = NBaseCommon.Common.LoginUser.MsUserID;

            kakuninJokyo.DocFlag_CEO = NBaseCommon.Common.LoginUser.DocFlag_CEO;
            kakuninJokyo.DocFlag_Admin = NBaseCommon.Common.LoginUser.DocFlag_Admin;
            kakuninJokyo.DocFlag_MsiFerry = NBaseCommon.Common.LoginUser.DocFlag_MsiFerry;
            kakuninJokyo.DocFlag_CrewFerry = NBaseCommon.Common.LoginUser.DocFlag_CrewFerry;
            kakuninJokyo.DocFlag_TsiFerry = NBaseCommon.Common.LoginUser.DocFlag_TsiFerry;
            //kakuninJokyo.DocFlag_MsiCargo = NBaseCommon.Common.LoginUser.DocFlag_MsiCargo;
            //kakuninJokyo.DocFlag_CrewCargo = NBaseCommon.Common.LoginUser.DocFlag_CrewCargo;
            //kakuninJokyo.DocFlag_TsiCargo = NBaseCommon.Common.LoginUser.DocFlag_TsiCargo;
            kakuninJokyo.DocFlag_Officer = NBaseCommon.Common.LoginUser.DocFlag_Officer;
            //kakuninJokyo.DocFlag_SdManager = NBaseCommon.Common.LoginUser.DocFlag_SdManager;
            kakuninJokyo.DocFlag_GL = NBaseCommon.Common.LoginUser.DocFlag_GL;
            kakuninJokyo.DocFlag_TL = NBaseCommon.Common.LoginUser.DocFlag_TL;

            kakuninJokyo.MsBumonID = NBaseCommon.Common.LoginUser.BumonID;
            bool ret;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.DmKakuninJokyo_UpdateRecord(NBaseCommon.Common.LoginUser, kakuninJokyo);
            }
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

            if (MessageBox.Show("回覧終了とします。よろしいでしょうか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            
            bool ret;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                DmKanryoInfo kanryoInfo = new DmKanryoInfo();
                kanryoInfo.DmKanryoInfoID = System.Guid.NewGuid().ToString();
                kanryoInfo.RenewDate = DateTime.Now;
                kanryoInfo.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                kanryoInfo.KanryoDate = DateTime.Now;
                kanryoInfo.MsUserID = NBaseCommon.Common.LoginUser.MsUserID;
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

                ret = serviceClient.BLC回覧終了処理_登録(NBaseCommon.Common.LoginUser, kanryoInfo);

                if (ret == true)
                {
                    RowData.Status = (int)NBaseData.DS.DocConstants.StatusEnum.完了;
                    RowData.完了日 = kanryoInfo.KanryoDate;
                    RowData.完了者 = NBaseCommon.Common.LoginUser.FullName;
                }
            }
            if (ret == true)
            {
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

                button_公開先.Enabled = false;
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

            pictureBox1.Image = RowData.GetFileImg();
            linkLabel_BunshoName.Text = RowData.文書名;
            textBox_Bikou.Text = RowData.備考;
            #endregion

            #region ボタンの有効/無効をセットする
            SetButton();
            #endregion

            #region 状況一覧をセットする


            List<DmPublisher> dmPublishers = null;
            List<DmKakuninJokyo> dmKakuninJokyos = null;
            List<DmKoukaiSaki> dmKoukaisakis = null;
            List<DmDocComment> dmComments = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                dmPublishers = serviceClient.DmPublisher_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, linkSakiId);
                dmKakuninJokyos = serviceClient.DmKakuninJokyo_GetRecordsByLinkSaki(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId);
                dmKoukaisakis = serviceClient.DmKoukaiSaki_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, linkSakiId);
                dmComments = serviceClient.DmDocComment_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId);
            }

            int rowIndex = 0;

            jokyoKakuninTableRows[rowIndex].Set項目名("登録者");
            jokyoKakuninTableRows[rowIndex].Set状況(dmPublishers);
            rowIndex++;


            bool 要確認 = false;

            var userList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER);
            foreach(DocConstants.ClassItem item in userList)
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


            jokyoKakuninTableRows[rowIndex].Set項目名("本船");
            //jokyoKakuninTableRows[rowIndex].Set状況(dmPublishers); // 本船に登録者を表示しているが、これは・・・？

            要確認 = 要確認チェック_船(dmKoukaisakis, RowData.KoukaiSakiVesselId);
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
            jokyoKakuninTableRow.Size = new System.Drawing.Size(722, 70);
            jokyoKakuninTableRow.TabIndex = 20 + index;

            return jokyoKakuninTableRow;
        }

        /// <summary>
        /// ボタンの有効/無効化
        /// </summary>
        #region private void SetButton()
        private void SetButton()
        {
            // 一旦、無効にする
            button_削除.Enabled = false;
            button_公開先.Enabled = false;
            button_コメント登録.Enabled = false;
            button_内容確認.Enabled = false;
            button_回覧終了.Enabled = false;

            // 完了している場合、何もしない
            if (RowData.Status == (int)NBaseData.DS.DocConstants.StatusEnum.完了)
            {
                return;
            }

            //// 安全管理・海務Ｇのみが使用できるボタンの制御
            //if (NBaseCommon.Common.LoginUser.BumonID == ((int)MsBumon.MsBumonIdEnum.海務部).ToString())
            //{
            //    if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
            //    {
            //        button_公開先.Enabled = false;
            //    }
            //    else
            //    {
            //        button_公開先.Enabled = true;
            //    }
            //    button_削除.Enabled = true;
            //    button_回覧終了.Enabled = true;
            //}
            // 権限管理の設定にしたがってボタンを制御する
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "文書管理状況確認", "削除"))
            {
                button_削除.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "文書管理状況確認", "公開先設定"))
            {
                if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                {
                    button_公開先.Enabled = false;
                }
                else
                {
                    button_公開先.Enabled = true;
                }
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "文書管理", "文書管理状況確認", "完了・デフォルト設定"))
            {
                button_回覧終了.Enabled = true;
            }

            // 公開先に設定されていれば使用できる
            foreach (DmKoukaiSaki koukaisaki in RowData.KoukaisakiList)
            {
                if (koukaisaki.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    bool enable = false;
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者
                        && NBaseCommon.Common.LoginUser.DocFlag_CEO == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者
                        && NBaseCommon.Common.LoginUser.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督
                        && NBaseCommon.Common.LoginUser.DocFlag_MsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者
                        && NBaseCommon.Common.LoginUser.DocFlag_CrewFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督
                        && NBaseCommon.Common.LoginUser.DocFlag_TsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員
                        && NBaseCommon.Common.LoginUser.DocFlag_Officer == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        enable = true;
                    }
                    if (enable)
                    {
                        button_コメント登録.Enabled = true;
                        button_内容確認.Enabled = true;
                    }

                    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門 &&
                        koukaisaki.MsBumonID == NBaseCommon.Common.LoginUser.BumonID)
                    {
                        button_コメント登録.Enabled = true;
                        button_内容確認.Enabled = true;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 選択行の文書を表示しているかの確認
        /// </summary>
        /// <returns></returns>
        #region private bool 表示確認()
        private bool 表示確認()
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
            DmKakuninJokyo kakuninJokyo = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kakuninJokyo = serviceClient.DmKakuninJokyo_GetRecordByLinkSakiUser(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId, NBaseCommon.Common.LoginUser.MsUserID);
            }
            if (kakuninJokyo == null)
            {
                MessageBox.Show("対象となる文書は表示していません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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

            foreach (JokyoKakuninTableRow jokyoKakuninTableRow in jokyoKakuninTableRows)
            {
                jokyoKakuninTableRow.Clear();
            }
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

                    DmKanriKirokuFile kanriKirokuFile = null;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        kanriKirokuFile = serviceClient.DmKanriKirokuFile_GetRecordByKanriKirokuID(NBaseCommon.Common.LoginUser, RowData.DmKanriKirokuId);
                    }
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

                    DmKoubunshoKisokuFile koubunshoKisokuFile = null;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        koubunshoKisokuFile = serviceClient.DmKoubunshoKisokuFile_GetRecordByKoubunshoKisokuID(NBaseCommon.Common.LoginUser, RowData.DmKoubunshoKisokuId);
                    }
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
                DmKakuninJokyo kakuninJokyo = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    kakuninJokyo = serviceClient.DmKakuninJokyo_GetRecordByLinkSakiUser(NBaseCommon.Common.LoginUser, linkSaki, linkSakiId, NBaseCommon.Common.LoginUser.MsUserID);
                }
                if (kakuninJokyo == null)
                {
                    kakuninJokyo = new DmKakuninJokyo();
                    kakuninJokyo.ViewDate = DateTime.Now;
                    kakuninJokyo.MsUserID = NBaseCommon.Common.LoginUser.MsUserID;

                    kakuninJokyo.DocFlag_CEO = NBaseCommon.Common.LoginUser.DocFlag_CEO;
                    kakuninJokyo.DocFlag_Admin = NBaseCommon.Common.LoginUser.DocFlag_Admin;
                    kakuninJokyo.DocFlag_MsiFerry = NBaseCommon.Common.LoginUser.DocFlag_MsiFerry;
                    kakuninJokyo.DocFlag_CrewFerry = NBaseCommon.Common.LoginUser.DocFlag_CrewFerry;
                    kakuninJokyo.DocFlag_TsiFerry = NBaseCommon.Common.LoginUser.DocFlag_TsiFerry;
                    //kakuninJokyo.DocFlag_MsiCargo = NBaseCommon.Common.LoginUser.DocFlag_MsiCargo;
                    //kakuninJokyo.DocFlag_CrewCargo = NBaseCommon.Common.LoginUser.DocFlag_CrewCargo;
                    //kakuninJokyo.DocFlag_TsiCargo = NBaseCommon.Common.LoginUser.DocFlag_TsiCargo;
                    kakuninJokyo.DocFlag_Officer = NBaseCommon.Common.LoginUser.DocFlag_Officer;
                    //kakuninJokyo.DocFlag_SdManager = NBaseCommon.Common.LoginUser.DocFlag_SdManager;
                    kakuninJokyo.DocFlag_GL = NBaseCommon.Common.LoginUser.DocFlag_GL;
                    kakuninJokyo.DocFlag_TL = NBaseCommon.Common.LoginUser.DocFlag_TL;
                    if (kakuninJokyo.DocFlag_CEO == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者;
                    }
                    else if (kakuninJokyo.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者;
                    }
                    else if (kakuninJokyo.DocFlag_MsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.海務監督;
                    }
                    else if (kakuninJokyo.DocFlag_CrewFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者;
                    }
                    else if (kakuninJokyo.DocFlag_TsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.工務監督;
                    }
                    else if (kakuninJokyo.DocFlag_Officer == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.役員;
                    }
                    else
                    {
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.部門;
                    }
                    kakuninJokyo.MsBumonID = NBaseCommon.Common.LoginUser.BumonID;
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
                    kakuninJokyo.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        bool ret;
                        ret = serviceClient.DmKakuninJokyo_InsertRecord(NBaseCommon.Common.LoginUser, kakuninJokyo);
                    }
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
            int linkSaki = -1;
            string filePath = "";

            try
            {
                // 選択行のファイルデータの確認
                #region
                if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                {
                    linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                    linkSakiId = RowData.DmKanriKirokuId;

                    DmKanriKirokuFile kanriKirokuFile = null;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        kanriKirokuFile = serviceClient.DmKanriKirokuFile_GetRecordByKanriKirokuID(NBaseCommon.Common.LoginUser, RowData.DmKanriKirokuId);
                    }
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

                    DmKoubunshoKisokuFile koubunshoKisokuFile = null;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        koubunshoKisokuFile = serviceClient.DmKoubunshoKisokuFile_GetRecordByKoubunshoKisokuID(NBaseCommon.Common.LoginUser, RowData.DmKoubunshoKisokuId);
                    }
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
