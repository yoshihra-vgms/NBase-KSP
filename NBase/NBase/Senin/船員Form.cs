using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Senin.util;
using NBaseData.DS;
using NBaseUtil;
using System.IO;
using NBaseCommon.Senin;
using NBaseData.BLC;
using NBaseCommon;

namespace Senin
{
    public partial class 船員Form : Form
    {
        private static 船員Form instance;

        private static readonly int MsSiShubetsuID_種別無し = -1;




        船員詳細検索Form advancedSearchForm = null;

        private List<SiAdvancedSearchConditionItem> advancedSearchConditionItemList;
        private List<SiAdvancedSearchConditionValue> advancedSearchConditionValueList;
        //private List<SiPresentaionItem> presentationItemList;

        ListSettingForm listSettingForm = null;

        船員詳細Panel detailPanel;

        // 検索結果
        List<MsSenin> resultSenin = null;
        List<MsSeninPlus> resultSeninPlus = null;




        private 船員Form()
        {
            InitializeComponent();
            Init();
        }


        public static 船員Form Instance()
        {
            if (instance == null)
            {
                instance = new 船員Form();
            }

            return instance;
        }

        public static bool IsActivated()
        {
            return instance == null ? false : true;
        }

        private void 船員Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;

            船員詳細検索Form.Dispose();
        }

        //================================================================
        //
        // 「初期化」関連
        //
        //================================================================
        #region

        private void Init()
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船員一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            InitComboBox船();
            InitComboBox所属会社();
            InitComboBox職名();
            InitComboBox住所();
            InitCheckedListBox種別();
            InitComboBox表示情報();


            // リスト項目設定の準備
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseCommon.Common.SeninListItemList = serviceClient.MsListItem_GetRecords(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.船員);

                NBaseCommon.Common.SeninListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.船員, NBaseCommon.Common.LoginUser.MsUserID);

                foreach (UserListItems uItem in NBaseCommon.Common.SeninListItemUserList)
                {
                    uItem.ListItem = NBaseCommon.Common.SeninListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                }
            }
            listSettingForm = new ListSettingForm();
            listSettingForm.SelectEvent += new ListSettingForm.SelectEventHandler(SelectListSetting);
            listSettingForm.RegistEvent += new ListSettingForm.RegistEventHandler(RegistListSetting);
            listSettingForm.RemoveEvent += new ListSettingForm.RemoveEventHandler(RemoveListSetting);

            // リスト項目対応一覧の準備
            settingListControl1.ClickEvent += new SettingListControl.ClickEventHandler(SeninListClick);


            // 詳細検索画面に、自身（船員Form）、リスト項目設定Formを紐付ける
            advancedSearchForm = 船員詳細検索Form.Instance();
            advancedSearchForm.SetMainForm(this);
            advancedSearchForm.SetListSettingForm(listSettingForm);



            // 船員詳細は一旦非表示
            splitContainer1.Panel2Collapsed = true;


            // リスト項目を一覧にセットする
            settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.DefaultListTitle).ToList();


            SetSettingList();
        }


        #region private void InitComboBox船()
        private void InitComboBox船()
        {
            comboBox船.Items.Add(string.Empty);

            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }

            comboBox船.SelectedIndex = 0;
        }
        #endregion

        #region private void InitComboBox所属会社()
        private void InitComboBox所属会社()
        {
            comboBox所属会社.Items.Add(string.Empty);

            foreach (MsSeninCompany s in SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser))
            {
                comboBox所属会社.Items.Add(s);
            }
        }
        #endregion

        #region private void InitComboBox職名()
        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }
        #endregion

        #region private void InitComboBox住所()
        private void InitComboBox住所()
        {
            comboBox住所.Items.Add(string.Empty);

            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県))
            {
                comboBox住所.Items.Add(o);
            }
            comboBox住所.SelectedIndex = 0;
        }
        #endregion

        #region private void InitCheckedListBox種別()
        private void InitCheckedListBox種別()
        {
            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                if (s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.休暇買上 &&
                    s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数 &&
                    s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数)
                {
                    checkedListBox種別.Items.Add(s);
                    checkedListBox種別.SetItemChecked(checkedListBox種別.Items.Count - 1, true);
                }
            }

            MsSiShubetsu ns = new MsSiShubetsu();
            ns.MsSiShubetsuID = MsSiShubetsuID_種別無し;
            ns.Name = "種別なし";

            checkedListBox種別.Items.Add(ns);
            checkedListBox種別.SetItemChecked(checkedListBox種別.Items.Count - 1, true);
        }
        #endregion

        #region private void InitComboBox表示情報()
        private void InitComboBox表示情報()
        {
            comboBox表示情報.Items.Clear();
            foreach (string name in 船員詳細Panel.TabNames)
            {
                //2021/08/03 現在使わないタブ名はリストに含めない
                if (船員詳細Panel.RemoveTabNames.Contains(name)) continue;

                comboBox表示情報.Items.Add(name);
            }
           
            comboBox表示情報.SelectedIndex = 0;
        }
        #endregion


        #endregion


        //================================================================
        //
        // 「検索操作」関連
        //
        //================================================================
        #region

        private void button検索_Click(object sender, EventArgs e)
        {
            if (CurrentlyEditing())
            {
                return;
            }

            // AdvancedSearchでの検索結果をクリア
            resultSeninPlus = null;

            // AdvancedSearchでの検索条件をクリア
            advancedSearchConditionItemList = null;
            advancedSearchConditionValueList = null;


            Search船員();

        }


        private void button詳細検索_Click(object sender, EventArgs e)
        {
            if (CurrentlyEditing())
            {
                return;
            }

            // 通常検索の結果をクリア
            resultSenin = null;

            advancedSearchForm.ShowDialog();
        }



        /// <summary>
        /// 現在、船員情報を編集中か
        /// </summary>
        /// <returns>true : 編集中</returns>
        #region private bool CurrentlyEditing()
        private bool CurrentlyEditing()
        {
            bool ret = false;

            foreach (Control con in splitContainer1.Panel2.Controls)
            {
                if (con is 船員詳細Panel)
                {
                    船員詳細Panel sp = con as 船員詳細Panel;
                    if (sp.ChangeFlag == true)
                    {
                        if (MessageBox.Show("編集中の詳細情報があります。検索しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                        {
                            ret = true;
                            break;
                        }
                        else
                        {
                            sp.ChangeFlag = false;
                            break;
                        }
                    }
                    break;
                }
            }

            return ret;
        }
        #endregion


        /// <summary>
        /// 詳細検索の検索条件をセット
        /// </summary>
        /// <param name="advancedSearchConditionItemList"></param>
        /// <param name="advancedSearchConditionValueList"></param>
        #region public void SetAdvancedSearchConditions(List<SiAdvancedSearchConditionItem> advancedSearchConditionItemList, List<SiAdvancedSearchConditionValue> advancedSearchConditionValueList)
        public void SetAdvancedSearchConditions(List<SiAdvancedSearchConditionItem> advancedSearchConditionItemList,List<SiAdvancedSearchConditionValue> advancedSearchConditionValueList)
        {
            this.advancedSearchConditionItemList = advancedSearchConditionItemList;
            this.advancedSearchConditionValueList = advancedSearchConditionValueList;
        }
        #endregion




        public void Search船員()
        {
            if (advancedSearchConditionItemList != null && advancedSearchConditionValueList != null)
            {
                AdvancedSearch();
            }
            else
            {
                NormalSearch();
            }

            // 船員詳細は一旦非表示
            splitContainer1.Panel2Collapsed = true;
        }


        private void NormalSearch()
        {
            // リスト項目を一覧にセットする
            settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.DefaultListTitle).ToList();



            List<MsSenin> result = null;

            if (checkedListBox種別.CheckedItems.Count == 0)
            {
                result = new List<MsSenin>();
            }
            else
            {
                MsSeninFilter filter = CreateMsSeninFilter();

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        result = serviceClient.BLC_船員検索(NBaseCommon.Common.LoginUser, filter);
                    }
                }, "データ取得中です...");

                progressDialog.ShowDialog();
            }
            resultSenin = result;



            // 一覧表示
            SetSettingList();
        }



        private void AdvancedSearch()
        {
            // リスト項目を一覧にセットする
            settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.SeninListTitle).ToList();


            List<MsSeninPlus> result = null;

            if (advancedSearchConditionItemList.Count() == 0 || advancedSearchConditionValueList.Count() == 0 || advancedSearchConditionValueList.Any(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE) == false)
            {
                result = new List<MsSeninPlus>();
            }
            else
            {
                SiAdvancedSearchFilter filter = new SiAdvancedSearchFilter();
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "menjouK" || o.ListItem.ClassName == "menjouS" || o.ListItem.ClassName == "menjouM")))
                    filter.includeMenjou = true;
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "emg1_kazoku" || o.ListItem.ClassName == "emg2_kazoku" || o.ListItem.ClassName == "kazoku")))
                    filter.includeKazoku = true;
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "kenshin")))
                    filter.includeKenshin = true;
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "shobatsu")))
                    filter.includeShobatsu = true;
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "remarks")))
                    filter.includeRemarks = true;
                if (settingListControl1.SelectedUserListItemsList.Any(o => (o.ListItem.ClassName == "salary")))
                    filter.includeSalary = true;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    // 検索
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        result = serviceClient.BLC_船員検索Advanced(NBaseCommon.Common.LoginUser, filter, advancedSearchConditionItemList, advancedSearchConditionValueList);
                    }

                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }
            resultSeninPlus = result;


            // 一覧表示
            SetSettingList();


            // 本画面の検索条件はクリアする
            Clear();
        }


        public MsSeninFilter CreateMsSeninFilter()
        {
            MsSeninFilter filter = new MsSeninFilter();

            if (textBox氏名コード.Text.Length > 0)
            {
                filter.ShimeiCode = textBox氏名コード.Text;
            }

            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text;
            }

            if (textBox氏名カナ.Text.Length > 0)
            {
                filter.NameKana = textBox氏名カナ.Text;
            }

            if (comboBox住所.SelectedItem is MsSiOptions)
            {
                filter.Juusho = (comboBox住所.SelectedItem as MsSiOptions).MsSiOptionsID;
            }

            if (textBox保険番号.Text.Length > 0)
            {
                filter.HokenNo = textBox保険番号.Text;
            }

            if (comboBox所属会社.SelectedItem is MsSeninCompany)
            {
                filter.MsSeninCompanyID = (comboBox所属会社.SelectedItem as MsSeninCompany).MsSeninCompanyID;
            }

            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            if (comboBox船.SelectedItem is MsVessel)
            {
                filter.MsVesselID = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            }

            if (checkBox社員.Checked)
            {
                filter.Kubuns.Add(0);
            }

            if (checkBox派遣.Checked)
            {
                filter.Kubuns.Add(1);
            }

            if (!checkBox社員.Checked && !checkBox派遣.Checked)
            {
                filter.Kubuns.Add(-1);
            }


            if (radioButton退職者除く.Checked)
            {
                filter.RetireFlag = (int)MsSeninFilter.enumRetire.EXCEPT;
            }
            else if (radioButton退職者含む.Checked)
            {
                filter.RetireFlag = (int)MsSeninFilter.enumRetire.INCLUDE;
            }
            else
            {
                filter.RetireFlag = (int)MsSeninFilter.enumRetire.ONLY;
            }


            filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;

            foreach (MsSiShubetsu s in checkedListBox種別.CheckedItems)
            {
                if (s.MsSiShubetsuID == MsSiShubetsuID_種別無し)
                {
                    filter.種別無し = true;
                }
                else
                {
                    filter.MsSiShubetsuIDs.Add(s.MsSiShubetsuID);
                }
            }

            return filter;
        }


        #endregion


        //================================================================
        //
        // 「一覧」関連
        //
        //================================================================
        #region

        /// <summary>
        /// 一覧クリック
        /// </summary>
        /// <param name="seninId"></param>
        #region private void SeninListClick(object id)
        private void SeninListClick(object id)
        {
            this.Cursor = Cursors.WaitCursor;

            MsSenin senin = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                senin = serviceClient.MsSenin_GetRecord(NBaseCommon.Common.LoginUser, int.Parse((string)id));
            }

            詳細表示(senin);

            this.Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 詳細情報を表示する
        /// </summary>
        /// <param name="senin"></param>
        #region private void 詳細表示(MsSenin senin)
        private void 詳細表示(MsSenin senin)
        {
            bool addSenin = false;
            if (senin == null)
                addSenin = true;


            if (detailPanel == null)
            {
                船員詳細Panel form = null;
                if (addSenin)
                    form = new 船員詳細Panel(this);
                else
                    form = new 船員詳細Panel(this, senin);

                detailPanel = form;

                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(form);
                splitContainer1.SplitterDistance = splitContainer1.Width - (form.Width + 23);

            }
            else
            {
                detailPanel.SetSenin(senin);

                if (addSenin)
                    detailPanel.SelectTab(船員詳細Panel.TabNames[0], true);
                else
                    detailPanel.SelectTab(comboBox表示情報.SelectedItem as string);
            }

            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }
        }
        #endregion

        /// <summary>
        /// 詳細情報更新→一覧の対象行の更新
        /// </summary>
        /// <param name="senin"></param>
        /// <param name="seninAddress"></param>
        #region public void 詳細更新(MsSenin senin, MsSeninAddress seninAddress)
        public void 詳細更新(MsSenin senin, MsSeninAddress seninAddress)
        {
            // 一覧の選択行に設定されている元情報を取得
            SettingListRowInfo rowInfo = settingListControl1.GetSelectedInfo();

            //  選択行があれば更新
            if (rowInfo != null && rowInfo.senin.MsSeninID == senin.MsSeninID)
            {
                if (senin.RetireFlag == 1 && radioButton退職者除く.Checked)
                {
                    settingListControl1.RemoveSelectedRow();
                    if (settingListControl1.RowCount == 0)
                        splitContainer1.Panel2Collapsed = true;
                }
                else if (senin.RetireFlag == 0 && radioButton退職者のみ.Checked)
                {
                    settingListControl1.RemoveSelectedRow();
                    if (settingListControl1.RowCount == 0)
                        splitContainer1.Panel2Collapsed = true;
                }
                else
                {
                    // 更新情報で入れ替えをする
                    rowInfo.senin = senin;
                    rowInfo.address = seninAddress;
                    settingListControl1.UpdateSelectedRow(rowInfo);
                }
            }
            else
            {
                //選択行がなければ新規登録
                //IDが違う場合、新規登録後なので、更新処理とする
                NormalSearch();

                rowInfo = settingListControl1.RowInfoList.Where(o => o.senin.MsSeninID == senin.MsSeninID).FirstOrDefault();
                if (rowInfo != null)
                {
                    settingListControl1.Select(rowInfo);
                }
            }
        }
        #endregion


        /// <summary>
        /// 検索結果を一覧にセットする
        /// </summary>
        #region private void SetSettingList()
        private void SetSettingList()
        {
            // 検索結果を一覧用の情報に変換
            List<SettingListRowInfo> RowInfoList = ConvertRowInfo();

            // 一覧情報を一覧にセットする
            settingListControl1.RowInfoList = RowInfoList;
            settingListControl1.DrawList();
        }
        #endregion

        public List<SettingListRowInfo> ConvertRowInfo()
        {
            // 検索結果を一覧用の情報に変換
            List<SettingListRowInfo> RowInfoList = new List<SettingListRowInfo>();
            {
                if (resultSenin != null)
                {
                    foreach (MsSenin s in resultSenin)
                    {
                        if (RowInfoList.Any(o => o.senin.MsSeninID == s.MsSeninID)) // 既に船員データがある場合、先優先として後続は無視する
                            continue;

                        SettingListRowInfo rowInfo = new SettingListRowInfo();
                        rowInfo.senin = s;

                        RowInfoList.Add(rowInfo);
                    }
                }
                else if (resultSeninPlus != null)
                {
                    foreach (MsSeninPlus s in resultSeninPlus)
                    {
                        SettingListRowInfo rowInfo = new SettingListRowInfo();
                        rowInfo.senin = s.Senin;
                        rowInfo.address = s.Address;
                        if (rowInfo.address != null)
                            rowInfo.address.MakeFullAddress(SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県));
                        rowInfo.career = s.Career;
                        rowInfo.etc = s.Etc;
                        rowInfo.card = s.Card;
                        rowInfo.menjouK = s.Menjou_K;
                        rowInfo.menjouS = s.Menjou_S;
                        rowInfo.menjouM = s.Menjou_M;

                        rowInfo.emg1_kazoku = s.KazokuEmg1;
                        if (rowInfo.emg1_kazoku != null)
                            rowInfo.emg1_kazoku.MakeFullAddress(SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県));
                        rowInfo.emg2_kazoku = s.KazokuEmg2;
                        if (rowInfo.emg2_kazoku != null)
                            rowInfo.emg2_kazoku.MakeFullAddress(SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県));
                        rowInfo.kazoku = s.Kazoku;
                        if (rowInfo.kazoku != null)
                            rowInfo.kazoku.MakeFullAddress(SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県));

                        RowInfoList.Add(rowInfo);
                    }
                }
            }

            RowInfoList.ForEach(o =>
            {
                if (SeninTableCache.instance().Is_乗船(NBaseCommon.Common.LoginUser, o.senin.MsSiShubetsuID) == false)
                {
                    o.senin.StartDate = DateTime.MinValue;
                }
            });

            return RowInfoList;
        }

        public List<SettingListRowInfo> ConvertRowInfo(List<MsSenin> seninList)
        {
            List<MsSenin> tmp = null;
            if (resultSenin != null)
            {
                tmp = new List<MsSenin>();
                tmp.AddRange(resultSenin);

                resultSenin.Clear();
            }
            else
            {
                resultSenin = new List<MsSenin>();
            }
            resultSenin.AddRange(seninList);


            List<SettingListRowInfo> RowInfoList = ConvertRowInfo();


            if (tmp != null)
            {
                resultSenin.Clear();
                resultSenin.AddRange(tmp);
            }

            return RowInfoList;
        }

        #endregion


        //================================================================
        //
        // 「検索結果出力」関連
        //
        //================================================================
        #region
        private void button出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "船員一覧_" + DateTime.Today.ToString("yyyyMMdd") + ".csv";
            FileUtils.SetDesktopFolder(saveFileDialog1);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                #region 旧コード
                //try
                //{
                //    this.Cursor = Cursors.WaitCursor;

                //    string header = "";
                //    header = "No,種別,職名,社員区分,氏名,氏名（カナ）,従業員番号,保険番号,船,合計乗船,合計休暇,乗船日数,休暇日数,乗船日";
                //    #region
                //    foreach (SiPresentaionItem item in presentationItemList)
                //    {
                //        if (item.SetNo > 1 && item.OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_ON)
                //        {
                //            if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_OF_TANKER)
                //            {
                //                foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                //                {
                //                    header += "," + item.Name + "(" + type.TypeName + ")";
                //                }
                //            }
                //            else
                //            {
                //                header += "," + item.Name;
                //            }
                //        }
                //    }
                //    #endregion

                //    StringBuilder data = new StringBuilder();

                //    if (resultSenin != null)
                //    {
                //        List<TreeListViewUtils.MsSeninRow> rows = TreeListViewUtils.CreateRowData(resultSenin, NBaseCommon.Common.LoginUser, SeninTableCache.instance());
                //        int i = 0;
                //        foreach (TreeListViewUtils.MsSeninRow r in rows)
                //        {
                //            // 固定カラム分
                //            #region
                //            // No
                //            data.Append((++i).ToString());
                //            data.Append(",");
                //            // 種別
                //            data.Append(SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.senin.MsSiShubetsuID));
                //            data.Append(",");
                //            // 職名
                //            data.Append(SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.MsSiShokumeiID));
                //            data.Append(",");
                //            // 区分
                //            data.Append(r.senin.KubunStr);
                //            data.Append(",");
                //            // 氏名
                //            data.Append(r.senin.Sei + " " + r.senin.Mei);
                //            data.Append(",");
                //            // 氏名（カナ）
                //            data.Append(r.senin.SeiKana + " " + r.senin.MeiKana);
                //            data.Append(",");
                //            // 氏名ｺｰﾄﾞ
                //            data.Append(r.senin.ShimeiCode);
                //            data.Append(",");
                //            // 保険番号
                //            data.Append(r.senin.HokenNo);
                //            data.Append(",");
                //            // 船名
                //            data.Append(SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, r.senin.MsVesselID));
                //            data.Append(",");

                //            int 合計乗船 = 0;
                //            if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)))
                //            {
                //                合計乗船 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)];
                //            }
                //            data.Append(合計乗船.ToString());
                //            data.Append(",");

                //            int 合計休暇 = 0;
                //            if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)))
                //            {
                //                合計休暇 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)];
                //            }
                //            data.Append(合計休暇.ToString());
                //            data.Append(",");

                //            // 乗船日数
                //            if (r.senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
                //            {
                //                data.Append(StringUtils.ToStr(r.senin.StartDate, DateTime.Now));
                //            }
                //            data.Append(",");

                //            // 休暇日数
                //            if (r.senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                //            {
                //                data.Append(StringUtils.ToStr(r.senin.StartDate, DateTime.Now));
                //            }
                //            data.Append(",");

                //            data.Append(r.乗船日);
                //            data.Append(",");


                //            #endregion

                //            data.Append(System.Environment.NewLine);
                //        }
                //    }
                //    else if (resultSeninPlus != null)
                //    {

                //        List<TreeListViewUtils.MsSeninPlusRow> rows = TreeListViewUtils.CreateRowData(resultSeninPlus, NBaseCommon.Common.LoginUser, SeninTableCache.instance());
                //        int i = 0;
                //        foreach (TreeListViewUtils.MsSeninPlusRow r in rows)
                //        {
                //            // 固定カラム分
                //            #region
                //            // No
                //            data.Append((++i).ToString());
                //            data.Append(",");
                //            // 種別
                //            data.Append(SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsSiShubetsuID));
                //            data.Append(",");
                //            // 職名
                //            data.Append(SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsSiShokumeiID));
                //            data.Append(",");
                //            // 区分
                //            data.Append(r.senin.Senin.KubunStr);
                //            data.Append(",");
                //            // 氏名
                //            data.Append(r.senin.Senin.Sei + " " + r.senin.Senin.Mei);
                //            data.Append(",");
                //            // 氏名（カナ）
                //            data.Append(r.senin.Senin.SeiKana + " " + r.senin.Senin.MeiKana);
                //            data.Append(",");
                //            // 氏名ｺｰﾄﾞ
                //            data.Append(r.senin.Senin.ShimeiCode);
                //            data.Append(",");
                //            // 保険番号
                //            data.Append(r.senin.Senin.HokenNo);
                //            data.Append(",");
                //            // 船名
                //            data.Append(SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsVesselID));
                //            data.Append(",");

                //            int 合計乗船 = 0;
                //            if (r.senin.Senin.合計日数 != null && r.senin.Senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)))
                //            {
                //                合計乗船 = r.senin.Senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)];
                //            }
                //            data.Append(合計乗船.ToString());
                //            data.Append(",");

                //            int 合計休暇 = 0;
                //            if (r.senin.Senin.合計日数 != null && r.senin.Senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)))
                //            {
                //                合計休暇 = r.senin.Senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)];
                //            }
                //            data.Append(合計休暇.ToString());
                //            data.Append(",");

                //            // 乗船日数
                //            if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
                //            {
                //                data.Append(StringUtils.ToStr(r.senin.Senin.StartDate, DateTime.Now));
                //            }
                //            data.Append(",");

                //            // 休暇日数
                //            if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                //            {
                //                data.Append(StringUtils.ToStr(r.senin.Senin.StartDate, DateTime.Now));
                //            }
                //            data.Append(",");

                //            //data.Append(r.乗船日);
                //            if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
                //            {
                //                data.Append(r.senin.Senin.StartDate.ToShortDateString());
                //            }
                //            data.Append(",");

                //            #endregion

                //            // 条件に応じて対応
                //            #region
                //            foreach (SiPresentaionItem item in presentationItemList)
                //            {
                //                if (item.SetNo == 1 || item.OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_OFF)
                //                    continue;


                //                if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_AGE)
                //                {
                //                    data.Append(DateTimeUtils.年齢計算(r.senin.Senin.Birthday).ToString());
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_ON_RANK)
                //                {
                //                    data.Append(SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.Card.CardMsSiShokumeiID));
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_ON)
                //                {
                //                    if (r.senin.Card != null)
                //                    {
                //                        data.Append(r.senin.Card.StartDate.ToShortDateString());
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_OFF)
                //                {
                //                    if (r.senin.Card != null)
                //                    {
                //                        if (r.senin.Card.EndDate != DateTime.MinValue)
                //                        {
                //                            data.Append(r.senin.Card.EndDate.ToShortDateString());
                //                        }
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_VESSEL_ALL)
                //                {
                //                    if (r.senin.Card != null)
                //                    {
                //                        data.Append(r.senin.Card.VesselName);
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_DAYS)
                //                {
                //                    if (r.senin.Card != null)
                //                    {
                //                        data.Append(r.senin.Card.Days.ToString());
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TYPE)
                //                {
                //                    if (r.senin.Menjou != null)
                //                    {
                //                        data.Append(SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, r.senin.Menjou.MsSiMenjouID));
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_GRADE)
                //                {
                //                    if (r.senin.Menjou != null)
                //                    {
                //                        data.Append(SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, r.senin.Menjou.MsSiMenjouKindID));
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_ISSUE_DATE)
                //                {
                //                    string dateStr = "";
                //                    if (r.senin.Menjou != null)
                //                    {
                //                        dateStr = r.senin.Menjou.ShutokuDate != DateTime.MinValue ? r.senin.Menjou.ShutokuDate.ToShortDateString() : "";
                //                    }
                //                    data.Append(dateStr);
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_EXPIRY_DATE)
                //                {
                //                    string dateStr = "";
                //                    if (r.senin.Menjou != null)
                //                    {
                //                        dateStr = r.senin.Menjou.Kigen != DateTime.MinValue ? r.senin.Menjou.Kigen.ToShortDateString() : "";
                //                    }
                //                    data.Append(dateStr);
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_OPERATOR)
                //                {
                //                    if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_OPERATOR))
                //                    {
                //                        var cm = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_OPERATOR).First();
                //                        data.Append(船員.CalcYears(cm.Days).ToString("0.0") + " Y");
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_RANK)
                //                {
                //                    if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_RANK))
                //                    {
                //                        var cm = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_RANK).First();
                //                        data.Append(船員.CalcYears(cm.Days).ToString("0.0") + " Y");
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_OF_TANKER)
                //                {
                //                    bool isFirst = true;

                //                    if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_ON_THIS_TYPE_OF_TANKER))
                //                    {
                //                        var tmplist = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_ON_THIS_TYPE_OF_TANKER);

                //                        foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                //                        {
                //                            if (!isFirst)
                //                            {
                //                                data.Append(",");
                //                            }
                //                            if (tmplist.Any(obj => obj.TypeOfTanker == type.TypeName))
                //                            {
                //                                var cm = tmplist.Where(obj => obj.TypeOfTanker == type.TypeName).First();
                //                                data.Append(船員.CalcYears(cm.Days).ToString("0.0") + " Y");
                //                            }
                //                            else
                //                            {
                //                                data.Append("");
                //                            }
                //                            isFirst = false;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                //                        {
                //                            if (!isFirst)
                //                            {
                //                                data.Append(",");
                //                            }
                //                            data.Append("");
                //                            isFirst = false;
                //                        }
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_KIND)
                //                {
                //                    string kind = "";
                //                    if (r.senin.Shobyo != null)
                //                    {
                //                        kind = SiShobyo.KIND[r.senin.Shobyo.Kind];
                //                    }
                //                    data.Append(kind);
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_STATUS)
                //                {
                //                    string status = "";
                //                    if (r.senin.Shobyo != null)
                //                    {
                //                        status = SiShobyo.STATUS[r.senin.Shobyo.Kind];
                //                    }
                //                    data.Append(status);
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_DATE)
                //                {
                //                    string dateStr = r.senin.Shobyo.FromDate.ToShortDateString() + "～";
                //                    if (r.senin.Shobyo.ToDate != DateTime.MinValue)
                //                    {
                //                        dateStr += r.senin.Shobyo.ToDate.ToShortDateString();
                //                    }
                //                    data.Append(dateStr);
                //                }

                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING)
                //                {
                //                    if (r.senin.Koushu != null)
                //                    {
                //                        data.Append(SeninTableCache.instance().GetMsSiKoushuName(NBaseCommon.Common.LoginUser, r.senin.Koushu.MsSiKoushuID));
                //                    }
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING_START)
                //                {
                //                    string dateStr = "";
                //                    if (r.senin.Koushu != null)
                //                    {
                //                        dateStr = r.senin.Koushu.JisekiFrom != DateTime.MinValue ? r.senin.Koushu.JisekiFrom.ToShortDateString() : "";
                //                    }
                //                    data.Append(dateStr);
                //                }
                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING_END)
                //                {
                //                    string dateStr = "";
                //                    if (r.senin.Koushu != null)
                //                    {
                //                        dateStr = r.senin.Koushu.JisekiTo != DateTime.MinValue ? r.senin.Koushu.JisekiTo.ToShortDateString() : "";
                //                    }
                //                    data.Append(dateStr);
                //                }


                //                else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_EXPERIENCE_CARGO)
                //                {
                //                    if (r.senin.experienceCargoList != null)
                //                    {
                //                        string expStr = "";
                //                        foreach (SiExperienceCargo expCargo in r.senin.experienceCargoList)
                //                        {
                //                            if (expStr.Length > 0)
                //                            {
                //                                expStr += " ";
                //                            }
                //                            expStr += SeninTableCache.instance().GetMsCargoGroupName(NBaseCommon.Common.LoginUser, expCargo.MsCargoGroupID) + "(" + expCargo.Count.ToString() + ")";
                //                        }
                //                        data.Append(expStr);
                //                    }
                //                }


                //                //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_KIND)
                //                //{
                //                //    if (r.senin.Kenshin != null)
                //                //    {
                //                //        data.Append(SiKenshin.KIND[r.senin.Kenshin.Kind]);
                //                //    }
                //                //}
                //                //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_CONSULTATION)
                //                //{
                //                //    string dateStr = "";
                //                //    if (r.senin.Kenshin != null)
                //                //    {
                //                //        dateStr = r.senin.Kenshin.ConsultationDate != DateTime.MinValue ? r.senin.Kenshin.ConsultationDate.ToShortDateString() : "";
                //                //    }
                //                //    data.Append(dateStr);
                //                //}
                //                //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_EXPIRE)
                //                //{
                //                //    string dateStr = "";
                //                //    if (r.senin.Kenshin != null)
                //                //    {
                //                //        dateStr = r.senin.Kenshin.ExpirationDate != DateTime.MinValue ? r.senin.Kenshin.ExpirationDate.ToShortDateString() : "";
                //                //    }
                //                //    data.Append(dateStr);
                //                //}
                //                //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_RESULT)
                //                //{
                //                //    if (r.senin.Kenshin != null)
                //                //    {
                //                //        data.Append(SiKenshin.RESULT[r.senin.Kenshin.Result]);
                //                //    }
                //                //}
                //                data.Append(",");
                //            }
                //            #endregion

                //            data.Append(System.Environment.NewLine);
                //        }
                //    }

                //    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
                //    sw.WriteLine(header);
                //    sw.Write(data.ToString());
                //    sw.Close();

                //    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(this, "ファイルの出力に失敗しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //finally
                //{
                //    this.Cursor = Cursors.Default;
                //}
                #endregion

                settingListControl1.OutputList(saveFileDialog1.FileName);
            }
        }
        #endregion


        //================================================================
        //
        // 「検索条件クリア」関連
        //
        //================================================================
        #region

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            textBox氏名コード.Text = null;
            textBox氏名.Text = null;
            textBox氏名カナ.Text = null;
            textBox保険番号.Text = null;

            comboBox所属会社.SelectedItem = null;
            comboBox職名.SelectedItem = null;
            comboBox船.SelectedItem = null;
            comboBox住所.SelectedItem = null;

            checkBox社員.Checked = true;
            checkBox派遣.Checked = true;

            for (int i = 0; i < checkedListBox種別.Items.Count; i++)
            {
                checkedListBox種別.SetItemChecked(i, true);
            }

            radioButton退職者除く.Checked = true;
        }

        #endregion




        //================================================================
        //
        // 「船員追加」関連
        //
        //================================================================
        #region
        /// <summary>
        /// 「船員追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button船員追加_Click(object sender, EventArgs e)
        {
            //船員詳細Panel form = new 船員詳細Panel(this);
            //詳細表示(form);

            settingListControl1.ClearSelect();
            詳細表示(null);
        }
        #endregion


        //================================================================
        //
        // 「リスト項目」設定関連
        //
        //================================================================
        #region

        /// <summary>
        /// リスト項目設定画面での「選択」イベント
        /// </summary>
        /// <param name="title"></param>
        private void SelectListSetting(string title)
        {
            // 選択されたユーザリスト項目名を保持する
            if (NBaseCommon.Common.SeninListItemUserList.Any(o => o.Title == title))
            {
                NBaseCommon.Common.SeninListTitle = title;

                InsertOrUpdateSeninListTitle(title);

                advancedSearchForm.SetSettingList();
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
                    o.Kind = (int)MsListItem.enumKind.船員;
                    o.UserID = NBaseCommon.Common.LoginUser.MsUserID;
                    o.Title = title;
                });
            }
            else
            {
                userListItemsList.ForEach(o =>
                {
                    o.UserListItemsID = 0; // 新規として登録
                    o.Kind = (int)MsListItem.enumKind.船員;
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
                    NBaseCommon.Common.SeninListTitle = title;

                    InsertOrUpdateSeninListTitle(title);

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
                NBaseCommon.Common.SeninListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.船員, NBaseCommon.Common.LoginUser.MsUserID);

                foreach (UserListItems uItem in NBaseCommon.Common.SeninListItemUserList)
                {
                    uItem.ListItem = NBaseCommon.Common.SeninListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                }

                // 現在、選択されている設定がない場合
                if (NBaseCommon.Common.SeninListItemUserList.Any(o => o.Title == NBaseCommon.Common.SeninListTitle) == false)
                {
                    NBaseCommon.Common.SeninListTitle = NBaseCommon.Common.SeninListItemUserList.Select(o => o.Title).FirstOrDefault();
                }

            }
            listSettingForm.Init(NBaseCommon.Common.SeninListTitle, NBaseCommon.Common.SeninListItemList, NBaseCommon.Common.SeninListItemUserList);


            advancedSearchForm.SetSettingList();

        }

        #endregion


        private bool InsertOrUpdateSeninListTitle(string title)
        {
            bool ret = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                serviceClient.MsUserSettings_InsertOrUpdateRecords(NBaseCommon.Common.LoginUser, "SeninListTitle", title);
            }

            return ret;
        }
    }
}
