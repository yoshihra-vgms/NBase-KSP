using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Hachu.Controllers;
using Hachu.Models;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseCommon;
using NBaseUtil;

namespace Hachu.HachuManage
{
    public partial class 発注状況一覧Form : Form
    {
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 発注状況一覧Form instance = null;

        /// <summary>
        /// 発注一覧情報リスト
        /// </summary>
        private 発注一覧情報Controller 発注一覧情報 = null;

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;



        ListSettingForm listSettingForm = null;



        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 発注状況一覧Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 発注状況一覧Form();
            }
            return instance;
        }
        
        /// <summary>
        /// コンストラクタ
        /// Windows フォームをシングルトン対応にするため private
        /// </summary>
        private 発注状況一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("JM040101", "発注状況一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            EnableComponents();

            #region TabControlの設定 miho
            //タブの形
            tabControl2.TabShape = LidorSystems.IntegralUI.Containers.TabShape.Trapezoidal;
            //おおもとの色　透過
            tabControl2.ColorStyle.BorderColor = Color.Transparent;
            tabControl2.ColorStyle.BackColor = Color.Transparent;
            tabControl2.ColorStyle.BackFadeColor = Color.Transparent;
            //タブの色　透過
            tabControl2.TabStripStyle.BackColor = Color.Transparent;
            tabControl2.TabStripStyle.BackColor = Color.Transparent;
            tabControl2.TabStripStyle.BackFadeColor = Color.Transparent;

            tabControl2.ResetText();

            tabControl2.Height = 40;

            #endregion
        }

        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "新規手配"))
            {
                button新規手配.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "新規見積"))
            {
                button新規見積.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "新規発注"))
            {
                button新規発注.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "支払合算"))
            {
                button支払合算.Enabled = true;
            }
        }
        
        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 発注状況一覧Form_Load(object sender, EventArgs e)
        private void 発注状況一覧Form_Load(object sender, EventArgs e)
        {
            //==================================
            // 初期化
            //==================================
            
            this.Location = new Point(0, 0);

            // Form幅を親Formの幅にする
            this.Width = Parent.ClientSize.Width;
            this.Height = Parent.ClientSize.Height;

            検索条件初期化();





            // リスト項目設定の準備
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseCommon.Common.HachuListItemList = serviceClient.MsListItem_GetRecords(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.発注);

                NBaseCommon.Common.HachuListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.発注, NBaseCommon.Common.LoginUser.MsUserID);

                foreach (UserListItems uItem in NBaseCommon.Common.HachuListItemUserList)
                {
                    uItem.ListItem = NBaseCommon.Common.HachuListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                }
            }
            listSettingForm = new ListSettingForm();
            listSettingForm.SelectEvent += new ListSettingForm.SelectEventHandler(SelectListSetting);
            listSettingForm.RegistEvent += new ListSettingForm.RegistEventHandler(RegistListSetting);
            listSettingForm.RemoveEvent += new ListSettingForm.RemoveEventHandler(RemoveListSetting);


            発注一覧情報 = new 発注一覧情報Controller(this.MdiParent, tabControl2, splitContainer1, settingListControl1);


            // 詳細は一旦非表示
            splitContainer1.Panel2Collapsed = true;

            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 発注状況一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        private void 発注状況一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            発注一覧情報.終了();
            instance = null;
        }
        #endregion

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsVessel> vessels = null;
            List<MsUser> users = null;
            List<MsThiIraiSbt> thiIraiSbts = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                users = serviceClient.MsUser_GetRecordsByUserKbn事務所(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }

            // 船ComboBox初期化
            MsVessel dmyVessel = new MsVessel();
            dmyVessel.MsVesselID = -1;
            dmyVessel.VesselName = "";
            comboBox船.Items.Clear();
            comboBox船.Items.Add(dmyVessel);
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }

            // 事務所担当者
            MsUser dmyUser = new MsUser();
            dmyUser.MsUserID = "";
            dmyUser.Sei = "";
            dmyUser.Mei = "";
            comboBox事務担当者.Items.Clear();
            comboBox事務担当者.Items.Add(dmyUser);
            foreach (MsUser u in users)
            {
                comboBox事務担当者.Items.Add(u);
                if (u.MsUserID == NBaseCommon.Common.LoginUser.MsUserID)
                {
                    comboBox事務担当者.SelectedItem = u;
                }
            }

            // 手配依頼種別
            MsThiIraiSbt dmyThiIraiSbt = new MsThiIraiSbt();
            dmyThiIraiSbt.MsThiIraiSbtID = "";
            dmyThiIraiSbt.ThiIraiSbtName = "";
            comboBox種別.Items.Clear();
            comboBox種別.Items.Add(dmyThiIraiSbt);
            foreach (MsThiIraiSbt sbt in thiIraiSbts)
            {
                comboBox種別.Items.Add(sbt);
            }
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
            dmyThiIraiShousai.MsThiIraiShousaiID = "";
            dmyThiIraiShousai.ThiIraiShousaiName = "";
            comboBox詳細種別.Items.Clear();
            comboBox詳細種別.Items.Add(dmyThiIraiShousai);
            comboBox詳細種別.SelectedIndex = 0;

            // 手配依頼日
            maskedTextBox手配依頼日From.Text = "";
            maskedTextBox手配依頼日To.Text = "";

            // 受領日
            maskedTextBox受領日From.Text = "";
            maskedTextBox受領日To.Text = "";

            // 現状
            checkBox未対応.Checked = true;
            checkBox見積中.Checked = true;
            checkBox発注済.Checked = true;
            checkBox受領済.Checked = true;
            checkBox完了.Checked = false;

            // 2014.02 2013年度改造
            checkBox船受領.Checked = true;
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            string thiIraiDateFromStr = maskedTextBox手配依頼日From.Text;
            string thiIraiDateToStr = maskedTextBox手配依頼日To.Text;
            string jryDateFromStr = maskedTextBox受領日From.Text;
            string jryDateToStr = maskedTextBox受領日To.Text;
            DateTime thiIraiDateFrom = DateTime.MinValue;
            DateTime thiIraiDateTo = DateTime.MaxValue;
            DateTime jryDateFrom = DateTime.MinValue;
            DateTime jryDateTo = DateTime.MaxValue;

            #region 検索条件のチェック
            if (thiIraiDateFromStr.Replace("/", "").Trim().Length > 0)
            {
                try
                {
                    thiIraiDateFrom = DateTime.Parse(maskedTextBox手配依頼日From.Text);
                }
                catch
                {
                    MessageBox.Show("手配依頼日(from)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            if (thiIraiDateToStr.Replace("/", "").Trim().Length > 0)
            {
                try
                {
                    thiIraiDateTo = DateTime.Parse(maskedTextBox手配依頼日To.Text);
                }
                catch
                {
                    MessageBox.Show("手配依頼日(to)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            if (thiIraiDateFrom > thiIraiDateTo)
            {
                MessageBox.Show("手配依頼日(from～to)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (jryDateFromStr.Replace("/", "").Trim().Length > 0)
            {
                try
                {
                    jryDateFrom = DateTime.Parse(maskedTextBox受領日From.Text);
                }
                catch
                {
                    MessageBox.Show("受領日(from)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            if (jryDateToStr.Replace("/", "").Trim().Length > 0)
            {
                try
                {
                    jryDateTo = DateTime.Parse(maskedTextBox受領日To.Text);
                }
                catch
                {
                    MessageBox.Show("受領日(to)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            if (jryDateFrom > jryDateTo)
            {
                MessageBox.Show("受領日(from～to)が不正です。正しく入力してください。","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            // 2014.02 2013年度改造
            //if (checkBox未対応.Checked == false
            //    && checkBox見積中.Checked == false
            //    && checkBox発注済.Checked == false
            //    && checkBox受領済.Checked == false
            //    && checkBox完了.Checked == false)
            if (checkBox未対応.Checked == false
                && checkBox見積中.Checked == false
                && checkBox発注済.Checked == false
                && checkBox船受領.Checked == false
                && checkBox受領済.Checked == false
                && checkBox完了.Checked == false)
            {
                MessageBox.Show("状況を１つ以上チェックしてください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            this.Cursor = Cursors.WaitCursor;

            // 検索条件
            発注一覧検索条件 検索条件 = new 発注一覧検索条件();
            if ( comboBox船.SelectedItem is MsVessel )
                検索条件.Vessel = comboBox船.SelectedItem as MsVessel;
            if (comboBox事務担当者.SelectedItem is MsUser)
                検索条件.User = comboBox事務担当者.SelectedItem as MsUser;
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
                検索条件.ThiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
                検索条件.ThiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
            検索条件.thiIraiDateFrom = maskedTextBox手配依頼日From.Text;
            検索条件.thiIraiDateTo = maskedTextBox手配依頼日To.Text;
            検索条件.jryDateFrom = maskedTextBox受領日From.Text;
            検索条件.jryDateTo = maskedTextBox受領日To.Text;
            検索条件.status未対応 = checkBox未対応.Checked;
            検索条件.status見積中 = checkBox見積中.Checked;
            検索条件.status発注済 = checkBox発注済.Checked;
            検索条件.status受領済 = checkBox受領済.Checked;
            検索条件.status完了 = checkBox完了.Checked;

            検索条件.status船受領 = checkBox船受領.Checked;

            // 検索条件で、ＤＢを検索し、一覧に表示する
            発注一覧情報.一覧更新(検索条件);

            this.Cursor = Cursors.Default;

            if (発注一覧情報.検索結果数 == 0)
            {
                MessageBox.Show("対象データがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        /// <summary>
        /// 「条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button条件クリア_Click(object sender, EventArgs e)
        private void button条件クリア_Click(object sender, EventArgs e)
        {
            検索条件クリア();
        }
        #endregion


        public void 検索条件クリア()
        {
            // 船
            comboBox船.SelectedIndex = 0;

            // 事務担当者
            comboBox事務担当者.SelectedIndex = 0;

            // 手配依頼種別
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            comboBox詳細種別.Items.Clear();

            // 手配依頼日
            maskedTextBox手配依頼日From.Text = "";
            maskedTextBox手配依頼日To.Text = "";

            // 受領日
            maskedTextBox受領日From.Text = "";
            maskedTextBox受領日To.Text = "";

            // 現状
            checkBox未対応.Checked = true;
            checkBox見積中.Checked = true;
            checkBox発注済.Checked = true;
            checkBox受領済.Checked = true;
            checkBox完了.Checked = false;
        }








        /// <summary>
        /// 「新規手配」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button新規手配_Click(object sender, EventArgs e)
        private void button新規手配_Click(object sender, EventArgs e)
        {
            //選択されている船を依頼種別Formに渡す 2021/08/04　m.yoshihara
            MsVessel v = null;
            if (comboBox船.SelectedItem is MsVessel)
                v = comboBox船.SelectedItem as MsVessel;

            発注一覧情報.新規手配依頼Formを開く(v);
        }
        #endregion

        /// <summary>
        /// 「新規見積」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button新規見積_Click(object sender, EventArgs e)
        private void button新規見積_Click(object sender, EventArgs e)
        {
            //選択されている船を依頼種別Formに渡す 2021/08/04　m.yoshihara
            MsVessel v = null;
            if (comboBox船.SelectedItem is MsVessel)
                v = comboBox船.SelectedItem as MsVessel;

            発注一覧情報.新規見積依頼Formを開く(v);
        }
        #endregion

        /// <summary>
        /// 「新規発注」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button新規発注_Click(object sender, EventArgs e)
        private void button新規発注_Click(object sender, EventArgs e)
        {
            //選択されている船を依頼種別Formに渡す 2021/08/04　m.yoshihara
            MsVessel v = null;
            if (comboBox船.SelectedItem is MsVessel)
                v = comboBox船.SelectedItem as MsVessel;

            発注一覧情報.新規発注Formを開く(v);
        }
        #endregion


        private void button支払合算_Click(object sender, EventArgs e)
        {
            発注一覧情報.支払合算Formを開く();
        }

        /// <summary>
        /// 「手配依頼種別」ＤＤＬが選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 詳細種別ＤＤＬをクリア
            comboBox詳細種別.Items.Clear();

            // 選択された種別が「修繕」の場合、詳細種別ＤＤＬを再構築
            MsThiIraiSbt selected = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (selected.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
                dmyThiIraiShousai.MsThiIraiShousaiID = "";
                dmyThiIraiShousai.ThiIraiShousaiName = "";
                comboBox詳細種別.Items.Add(dmyThiIraiShousai);

                foreach (MsThiIraiShousai shousai in thiIraiShousais)
                {
                    comboBox詳細種別.Items.Add(shousai);
                }
                comboBox詳細種別.SelectedIndex = 0;
            }
        }
        #endregion



        #region rivate void treeListView発注一覧_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        private void treeListView発注一覧_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            try
            {
                // Expand された Node
                TreeListViewNode node = e.Object as TreeListViewNode;
            }
            catch
            {
            }
        }
        #endregion




        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            発注一覧情報.詳細タブ変更(((TabControl)sender).SelectedIndex);
        }

        private void tabControl2_SelectedPageChanged(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            try
            {
                if (((LidorSystems.IntegralUI.Containers.TabControl)sender).SelectedPage.Index >= 0)
                    発注一覧情報.詳細タブ変更(((LidorSystems.IntegralUI.Containers.TabControl)sender).SelectedPage.Index);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        //------------------------------------
        /// <summary>
        /// 他画面から船をセットする　2021/08/06 m.yoshihara
        /// </summary>
        /// <param name="id">船ID</param>
        /// <returns></returns>
        public MsVessel Set検索条件_船(int id)
        {
            int index = -1;
            for (int i = 0; i < comboBox船.Items.Count; i++)
            {
                if ((comboBox船.Items[i] as MsVessel).MsVesselID == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return null;
            comboBox船.SelectedIndex = index;

            return comboBox船.Items[index] as MsVessel;
        }

        public MsUser Set検索条件_事務担当(string id)
        {
            int index = -1;
            for (int i = 0; i < comboBox事務担当者.Items.Count; i++)
            {
                if ((comboBox事務担当者.Items[i] as MsUser).MsUserID == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return null;
            comboBox事務担当者.SelectedIndex = index;

            return comboBox事務担当者.Items[index] as MsUser;
        }



















        private void SetSettingList()
        {
        }


        //================================================================
        //
        // 「リスト項目」設定関連
        //
        //================================================================
        #region

        /// <summary>
        /// 「リスト項目設定」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button項目設定_Click(object sender, EventArgs e)
        {
            // リスト項目設定画面に、リスト項目（マスタ）、ユーザのリスト項目をセットして、画面を表示
            listSettingForm.Init(NBaseCommon.Common.HachuListTitle, NBaseCommon.Common.HachuListItemList, NBaseCommon.Common.HachuListItemUserList);
            listSettingForm.ShowDialog();

            // 画面を閉じたら、リスト項目対応一覧の表示
            SetSettingList();
        }

        /// <summary>
        /// リスト項目設定画面での「選択」イベント
        /// </summary>
        /// <param name="title"></param>
        private void SelectListSetting(string title)
        {
            // 選択されたユーザリスト項目名を保持する
            if (NBaseCommon.Common.HachuListItemUserList.Any(o => o.Title == title))
            {
                NBaseCommon.Common.HachuListTitle = title;

                InsertOrUpdateHachuListTitle(title);
            }
        }

        /// <summary>
        /// リスト項目設定画面での「登録」イベント
        /// </summary>
        /// <param name="isModify"></param>
        /// <param name="title"></param>
        /// <param name="userListItemsList"></param>
        /// <returns></returns>
        private bool RegistListSetting(bool isModify, string title, List<UserListItems> userListItemsList)
        {
            bool ret = true;

            if (isModify)
            {
                userListItemsList.ForEach(o =>
                {
                    o.Kind = (int)MsListItem.enumKind.発注;
                    o.UserID = NBaseCommon.Common.LoginUser.MsUserID;
                    o.Title = title;
                });
            }
            else
            {
                userListItemsList.ForEach(o =>
                {
                    o.UserListItemsID = 0; // 新規として登録
                    o.Kind = (int)MsListItem.enumKind.発注;
                    o.UserID = NBaseCommon.Common.LoginUser.MsUserID;
                    o.Title = title;
                });
            }


            // DBへ登録
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_RegistUserListSetting(NBaseCommon.Common.LoginUser, userListItemsList);

                if (ret)
                {
                    // 登録完了 = 選択とする
                    NBaseCommon.Common.HachuListTitle = title;

                    InsertOrUpdateHachuListTitle(title);

                    ResetSettingForm();
                }
            }

            return ret;
        }

        /// <summary>
        /// リスト項目設定画面での「削除」イベント
        /// </summary>
        /// <param name="userListItemsList"></param>
        /// <returns></returns>
        private bool RemoveListSetting(List<UserListItems> userListItemsList)
        {
            bool ret = true;

            // 削除フラグをたてる
            userListItemsList.ForEach(o =>
            {
                o.DeleteFlag = 1;
            });

            // DBへ登録
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_RegistUserListSetting(NBaseCommon.Common.LoginUser, userListItemsList);

                if (ret)
                {
                    ResetSettingForm();
                }
            }

            return ret;
        }

        /// <summary>
        /// リスト項目設定画面での「登録」「削除」イベント後に、リスト項目設定画面を再度初期化する
        /// </summary>
        private void ResetSettingForm()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseCommon.Common.HachuListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.発注, NBaseCommon.Common.LoginUser.MsUserID);

                foreach (UserListItems uItem in NBaseCommon.Common.HachuListItemUserList)
                {
                    uItem.ListItem = NBaseCommon.Common.HachuListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                }

                // 現在、選択されている設定がない場合
                if (NBaseCommon.Common.HachuListItemUserList.Any(o => o.Title == NBaseCommon.Common.HachuListTitle) == false)
                {
                    NBaseCommon.Common.HachuListTitle = NBaseCommon.Common.HachuListItemUserList.Select(o => o.Title).FirstOrDefault();
                }

            }
            listSettingForm.Init(NBaseCommon.Common.HachuListTitle, NBaseCommon.Common.HachuListItemList, NBaseCommon.Common.HachuListItemUserList);
        }







        private bool InsertOrUpdateHachuListTitle(string title)
        {
            bool ret = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                serviceClient.MsUserSettings_InsertOrUpdateRecords(NBaseCommon.Common.LoginUser, "HachuListTitle", title);
            }

            return ret;
        
        }
        #endregion




        private void button出力_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "発注一覧_" + DateTime.Today.ToString("yyyyMMdd") + ".csv";
            FileUtils.SetDesktopFolder(saveFileDialog);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                settingListControl1.OutputList(saveFileDialog.FileName);
            }
        }
    }
}
