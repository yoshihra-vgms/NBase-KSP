using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseCommon;
using System.Net.Mail;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBaseMaster.MsVessel
{
    public partial class 船管理新規登録Form : Form
    {
        private NBaseData.DAC.MsVessel msVessel;
        private List<NBaseData.DAC.MsVesselType> vesselTypeList;
        private List<NBaseData.DAC.MsCrewMatrixType> crewMatrixTypeList;
        private List<NBaseData.DAC.MsVesselSection> vesselSectionList;

        private List<NBaseData.DAC.MsVesselKind> vesselKindList;
        private List<NBaseData.DAC.MsVesselCategory> vesselCategoryList;
        private List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList;

        public 船管理新規登録Form()
        {
            InitializeComponent();

            msVessel = new NBaseData.DAC.MsVessel();
            vessellScheduleKindDetailEnableList = new List<NBaseData.DAC.MsVesselScheduleKindDetailEnable>();

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船管理新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            this.MakeDropDownList();
            CompletionDate_nullableDateTimePicker.Value = null;

            SetVesselScheduleKindDetails();

            Size size = this.Size;
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘))
            {
                size.Width = 972;
            }
            else
            {
                size.Width = 506;
            }
            this.Size = size;
            this.MinimumSize = size;
            this.MaximumSize = size;


            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.予実) == false)
            {
                予実_checkBox.Enabled = false;
                予実R_checkBox.Enabled = false;
            }
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                発注_checkBox.Enabled = false;
                発注R_checkBox.Enabled = false;
            }
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false)
            {
                船員_checkBox.Enabled = false;
                船員R_checkBox.Enabled = false;
            }
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書) == false)
            {
                文書_checkBox.Enabled = false;
                文書R_checkBox.Enabled = false;
            }
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘) == false)
            {
                検査_checkBox.Enabled = false;
                検査R_checkBox.Enabled = false;
            }
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静) == false)
            {
                動静_checkBox.Enabled = false;
                動静R_checkBox.Enabled = false;
            }
        }

        /// <summary>
        /// 登録ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Btn_Click(object sender, EventArgs e)
        {
            if (!入力値チェック())
            {
                return;
            }
            

            //----------------------------------------------
            // Insert
            try
            {
                #region 入力値を取得する
                msVessel.VesselNo = VesselNo_textBox.Text;
                msVessel.VesselName = VesselName_textBox.Text;
                msVessel.DWT = Convert.ToInt32(Dwt_textBox.Text);
                msVessel.Capacity = Convert.ToInt32(Capacity_textBox.Text);
                msVessel.HpTel = HpTell_textBox.Text;
                msVessel.Tel = Tell_textBox.Text;
                msVessel.MsVesselTypeID = ((ListItem)VesselType_comboBox.SelectedItem).Value;
                msVessel.RenewDate = DateTime.Today;
                msVessel.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msVessel.SendFlag = 0;
                msVessel.VesselID = 0;

                msVessel.OfficialNumber = OfficialNumber_textBox.Text;
                if (CargoWeight1_textBox.Text.Length == 0 && CargoWeight2_textBox.Text.Length == 0)
                {
                    msVessel.CargoWeight = decimal.MinValue;
                }
                else
                {
                    msVessel.CargoWeight = Convert.ToDecimal(CargoWeight1_textBox.Text + "." + CargoWeight2_textBox.Text);
                }
                if (GRT1_textBox.Text.Length == 0 && GRT2_textBox.Text.Length == 0)
                {
                    msVessel.GRT = decimal.MinValue;
                }
                else
                {
                    msVessel.GRT = Convert.ToDecimal(GRT1_textBox.Text + "." + GRT2_textBox.Text);
                } 
                
                if (ShowOrder_textBox.Text == "")
                {
                    msVessel.ShowOrder = NBaseCommon.Number.MaxValue(9);
                }
                else
                {
                    msVessel.ShowOrder = Convert.ToInt32(ShowOrder_textBox.Text);
                }


                #region 機能権限

                //機能権限------------------------------------------------------
                if (発注_checkBox.Checked == false)
                {
                    msVessel.HachuEnabled = 0;
                }
                else
                {
                    msVessel.HachuEnabled = 1;
                }
                if (予実_checkBox.Checked == false)
                {
                    msVessel.YojitsuEnabled = 0;
                }
                else
                {
                    msVessel.YojitsuEnabled = 1;
                }
                if (本船_checkBox.Checked == false)
                {
                    msVessel.HonsenEnabled = 0;
                }
                else
                {
                    msVessel.HonsenEnabled = 1;
                }
                if (動静_checkBox.Checked == false)
                {
                    msVessel.KanidouseiEnabled = 0;
                }
                else
                {
                    msVessel.KanidouseiEnabled = 1;
                }
                if (文書_checkBox.Checked == false)
                {
                    msVessel.DocumentEnabled = 0;
                }
                else
                {
                    msVessel.DocumentEnabled = 1;
                }
                if (船員_checkBox.Checked == false)
                {
                    msVessel.SeninEnabled = 0;
                }
                else
                {
                    msVessel.SeninEnabled = 1;
                }
                if (検査_checkBox.Checked == false)
                {
                    msVessel.KensaEnabled = 0;
                }
                else
                {
                    msVessel.KensaEnabled = 1;
                }

                // 荷役は標準では利用なしとする
                msVessel.NiyakuEnabled = 0;

                #endregion

                #region 実績表示
                //実績表示---------------------------------------
                if (発注R_checkBox.Checked == false)
                {
                    msVessel.HachuResults = 0;
                }
                else
                {
                    msVessel.HachuResults = 1;
                }

                if (予実R_checkBox.Checked == false)
                {
                    msVessel.YojitsuResults = 0;
                }
                else
                {
                    msVessel.YojitsuResults = 1;
                }
                if (動静R_checkBox.Checked == false)
                {
                    msVessel.KanidouseiResults = 0;
                }
                else
                {
                    msVessel.KanidouseiResults = 1;
                }

                if (文書R_checkBox.Checked == false)
                {
                    msVessel.DocumentResults = 0;
                }
                else
                {
                    msVessel.DocumentResults = 1;
                }
                if (船員R_checkBox.Checked == false)
                {
                    msVessel.SeninResults = 0;
                }
                else
                {
                    msVessel.SeninResults = 1;
                }
                if (検査R_checkBox.Checked == false)
                {
                    msVessel.KensaResults = 0;
                }
                else
                {
                    msVessel.KensaResults = 1;
                }

                #endregion


                if (CompletionDate_nullableDateTimePicker.Value != null)
                {
                    msVessel.CompletionDate = (DateTime)CompletionDate_nullableDateTimePicker.Value;
                }
                else
                {
                    msVessel.CompletionDate = DateTime.MinValue;
                }

                if (maskedTextBox検査基準日.Text != null)
                {
                    DateTime d;
                    DateTime.TryParse(maskedTextBox検査基準日.Text, out d);
                    msVessel.AnniversaryDate = d;
                }
                else
                {
                    msVessel.AnniversaryDate = DateTime.MinValue;
                }

                msVessel.Nationality = Nationality_textBox.Text;
                msVessel.MailAddress = MailAddress_textBox.Text;
                if (CrewMatrix_comboBox.SelectedItem is NBaseData.DAC.MsCrewMatrixType)
                {
                    msVessel.MsCrewMatrixTypeID = (CrewMatrix_comboBox.SelectedItem as NBaseData.DAC.MsCrewMatrixType).MsCrewMatrixTypeID;
                }
                else
                {
                    msVessel.MsCrewMatrixTypeID = 0;
                }

                //=================================================
                // 配乗計画
                //=================================================
                {
                    msVessel.A = pictureBox1.BackColor.A;
                    msVessel.R = pictureBox1.BackColor.R;
                    msVessel.G = pictureBox1.BackColor.G;
                    msVessel.B = pictureBox1.BackColor.B;
                }


                msVessel.NavigationArea = comboBox_NavigationArea.Text;
                msVessel.OwnerName = textBox_OwnerName.Text;


                #region 標準モジュールでは未使用

                //if (改正省エネ法エネルギー報告書_checkBox.Checked == false)
                //{
                //    msVessel.DouseiReport1 = 0;
                //}
                //else
                //{
                //    msVessel.DouseiReport1 = 1;
                //}

                //if (内航海運輸送実績調査票_checkBox.Checked == false)
                //{
                //    msVessel.DouseiReport2 = 0;
                //}
                //else
                //{
                //    msVessel.DouseiReport2 = 1;
                //}

                //if (内航船舶輸送実績調査票_checkBox.Checked == false)
                //{
                //    msVessel.DouseiReport3 = 0;
                //}
                //else
                //{
                //    msVessel.DouseiReport3 = 1;
                //}

                //msVessel.KaikeiBumonCode = KaikeiBumonCode_textBox.Text;

                //msVessel.KyuyoRenkeiNo = KyuyoRenkeiNo_textBox.Text;

                #endregion


                #region 指摘事項

                if (VesselKind_comboBox.SelectedItem is NBaseData.DAC.MsVesselKind)
                {
                    msVessel.MsVesselKindID = (VesselKind_comboBox.SelectedItem as NBaseData.DAC.MsVesselKind).MsVesselKindID;
                }
                else
                {
                    msVessel.MsVesselKindID = null;
                }

                if (VesselCategory_comboBox.SelectedItem is NBaseData.DAC.MsVesselCategory)
                {
                    msVessel.MsVesselCategoryID = (VesselCategory_comboBox.SelectedItem as NBaseData.DAC.MsVesselCategory).MsVesselCategoryID;
                }
                else
                {
                    msVessel.MsVesselKindID = null;
                }

                if (ImoNo_textBox.Text != "")
                {
                    msVessel.ImoNO = Convert.ToInt32(ImoNo_textBox.Text);
                }
                else
                {
                    msVessel.ImoNO = -1;
                }

                if (DeficiencyOrder_textBox.Text != "")
                {
                    msVessel.DeficiencyOrder = Convert.ToInt32(DeficiencyOrder_textBox.Text);
                }
                else
                {
                    msVessel.DeficiencyOrder = Number.MaxValue(9);
                }
                #endregion


                #endregion

                bool ret = true;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_船マスタ更新処理_追加処理(NBaseCommon.Common.LoginUser, msVessel, vessellScheduleKindDetailEnableList);
                }
                if (ret)
                {
                    Message.Show確認("登録しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Message.Show確認("登録に失敗しました。");
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }

        /// <summary>
        /// 戻るボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Cancel_Btn_Click(object sender, EventArgs e)
        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool 入力値チェック()
        private bool 入力値チェック()
        {
            #region 必須入力確認
            //---------------------------------------------------
            // 必須入力確認
            //---------------------------------------------------

            // 船No
            if (VesselNo_textBox.Text.Length < 1)
            {
                Message.Showエラー("船Noを入力してください。");
                return false;
            }

            // 船名
            if (VesselName_textBox.Text.Length < 1)
            {
                Message.Showエラー("船名を入力してください。");
                return false;
            }

            // DWT
            if (Dwt_textBox.Text.Length < 1)
            {
                Message.Showエラー("DWT(L/T)を入力してください。");
                return false;
            }

            // 船タイプ
            if (VesselType_comboBox.Text.Length < 1)
            {
                Message.Showエラー("船タイプを選択してください。");
                return false;
            }
            #endregion
            #region 数値のみ確認
            //--------------------------------------------------
            // 数値のみ確認
            //--------------------------------------------------
            
            // 2010.03.11:aki 船Ｎｏは英数字とのこと
            //try
            //{
            //    long vesselNo = Convert.ToInt64(VesselNo_textBox.Text);
            //}
            //catch
            //{
            //    Message.Showエラー("船Noは数値を入力して下さい。");
            //    return false;
            //}

            try
            {
                long dwt = Convert.ToInt64(Dwt_textBox.Text);
            }
            catch
            {
                Message.Showエラー("DWT(L/T)は数値を入力して下さい。");
                return false;
            }

            try
            {
                long capacity = Convert.ToInt64(Capacity_textBox.Text);
            }
            catch
            {
                Message.Showエラー("定員数は数値を入力して下さい。");
                return false;
            }

            if (CargoWeight1_textBox.Text != "")
            {
                try
                {
                    long cw1 = Convert.ToInt64(CargoWeight1_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("DWT(M/T)は数値を入力して下さい。");
                    return false;
                }
            }

            if (CargoWeight2_textBox.Text != "")
            {
                try
                {
                    long cw2 = Convert.ToInt64(CargoWeight2_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("DWT(M/T)は数値を入力して下さい。");
                    return false;
                }
            }


            // 201410月度改造
            if (GRT1_textBox.Text != "")
            {
                try
                {
                    long cw1 = Convert.ToInt64(GRT1_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("GRTは数値を入力して下さい。");
                    return false;
                }
            }

            if (GRT2_textBox.Text != "")
            {
                try
                {
                    long cw2 = Convert.ToInt64(GRT2_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("GRTは数値を入力して下さい。");
                    return false;
                }
            }

            if (ShowOrder_textBox.Text != "")
            {
                try
                {
                    long so = Convert.ToInt64(ShowOrder_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("表示順序は数値を入力して下さい。");
                    return false;
                }
                if (Number.CheckValue(Convert.ToInt32(ShowOrder_textBox.Text), 9, 0) == false)
                {
                    Message.Showエラー("表示順序を正しく入力して下さい");
                    return false;
                }
            }
            if (KyuyoRenkeiNo_textBox.Text != "")
            {
                try
                {
                    long so = Convert.ToInt64(KyuyoRenkeiNo_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("給与連携Noは数値を入力して下さい。");
                    return false;
                }
            }
            #endregion
            #region 重複確認
            //--------------------------------------------------
            // 重複確認
            //--------------------------------------------------

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseData.DAC.MsVessel msVessel = null;

                // 船No
                msVessel = serviceClient.MsVessel_GetRecordsByVesselNo(NBaseCommon.Common.LoginUser, VesselNo_textBox.Text);
                if (msVessel != null)
                {
                    Message.Showエラー("入力された船Noは登録されています。");
                    return false;
                }
            }

            if (本船_checkBox.Checked == true)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // 契約数
                    int contractCount = NbaseContractFunctionTableCache.instance().HonsenContractCount(NBaseCommon.Common.LoginUser);
                    var vessels = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                    int checkCount = vessels.Where(o => o.HonsenEnabled == 1).Count(); // 本船のチェックが付いている数
                    if (checkCount >= contractCount)
                    {
                        Message.Showエラー("既に契約数分登録されています。");
                        return false;
                    }
                }
            }


            #endregion
            string mailAddress = MailAddress_textBox.Text;
            if (mailAddress != null && mailAddress.Length > 0)
            {
                try
                {
                    MailAddress ma = new MailAddress(mailAddress);
                }
                catch
                {
                    Message.Showエラー("メールアドレスが不正です。");
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// ドロップダウンリストに値を設定する
        /// </summary>
        #region private void MakeDropDownList()
        private void MakeDropDownList()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vesselTypeList = serviceClient.MsVesselType_GetRecords(NBaseCommon.Common.LoginUser);

                foreach (NBaseData.DAC.MsVesselType item in vesselTypeList)
                {
                    VesselType_comboBox.Items.Add(new ListItem(item.VesselTypeName, item.MsVesselTypeID));
                }
                VesselType_comboBox.SelectedIndex = 0;


                crewMatrixTypeList = serviceClient.MsCrewMatrixType_GetRecords(NBaseCommon.Common.LoginUser);

                CrewMatrix_comboBox.Items.Add("");
                foreach (NBaseData.DAC.MsCrewMatrixType item in crewMatrixTypeList)
                {
                    CrewMatrix_comboBox.Items.Add(item);
                }
                CrewMatrix_comboBox.SelectedIndex = 0;

                // 航行区域
                comboBox_NavigationArea.Items.Clear();
                comboBox_NavigationArea.Items.AddRange(NBaseData.DAC.MsVessel.NavigationAreaStrings.ToArray());



                #region 標準モジュールでは未使用

                vesselKindList = serviceClient.MsVesselKind_GetRecords(NBaseCommon.Common.LoginUser);

                VesselKind_comboBox.Items.Add("");
                foreach (NBaseData.DAC.MsVesselKind item in vesselKindList)
                {
                    VesselKind_comboBox.Items.Add(item);
                }
                VesselKind_comboBox.SelectedIndex = 0;

                vesselCategoryList = serviceClient.MsVesselCategory_GetRecords(NBaseCommon.Common.LoginUser);

                VesselCategory_comboBox.Items.Add("");
                foreach (NBaseData.DAC.MsVesselCategory item in vesselCategoryList)
                {
                    VesselCategory_comboBox.Items.Add(item);
                }
                VesselCategory_comboBox.SelectedIndex = 0;

                #endregion

            }
        }
        #endregion


        private void button営業検索_Click(object sender, EventArgs e)
        {
            msVessel.SalesPersonID = ユーザ検索(textBox営業担当);
        }

        private void button工務検索_Click(object sender, EventArgs e)
        {
            msVessel.MarineSuperintendentID = ユーザ検索(textBox工務監督);
        }

        private string ユーザ検索(System.Windows.Forms.TextBox textBox)
        {
            string userId = null;

            ユーザ検索Form form = new ユーザ検索Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NBaseData.DAC.MsUser user = form.SelectedUser;
                textBox.Text = user.FullName;

                userId = user.MsUserID;
            }

            return userId;
        }

        private void button営業クリア_Click(object sender, EventArgs e)
        {
            textBox営業担当.Text = "";
            msVessel.SalesPersonID = null;
        }

        private void button工務クリア_Click(object sender, EventArgs e)
        {
            textBox工務監督.Text = "";
            msVessel.MarineSuperintendentID = null;
        }

        private void SetVesselScheduleKindDetails()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<NBaseData.DAC.MsScheduleKindDetail> scheduleKindDetailList = serviceClient.MsScheduleKindDetail_GetRecords(NBaseCommon.Common.LoginUser);

                foreach(NBaseData.DAC.MsScheduleKindDetail scheduleKindDetail in scheduleKindDetailList)
                {
                    if (scheduleKindDetail.ScheduleCategoryID != (int)NBaseData.DAC.MsScheduleCategory.CATEGORY.予定実績)
                        continue;

                    NBaseData.DAC.MsVesselScheduleKindDetailEnable vessellScheduleKindDetailEnable = new NBaseData.DAC.MsVesselScheduleKindDetailEnable();

                    vessellScheduleKindDetailEnable.ScheduleKindDetailID = scheduleKindDetail.ScheduleKindDetailID;
                    vessellScheduleKindDetailEnable.ScheduleKindDetailName = scheduleKindDetail.ScheduleKindDetailName;
                    vessellScheduleKindDetailEnable.ScheduleKindName = scheduleKindDetail.ScheduleKindName;
                    vessellScheduleKindDetailEnable.Enabled = false;

                    vessellScheduleKindDetailEnableList.Add(vessellScheduleKindDetailEnable);
                }

            }

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "種別";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "詳細";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "有効";
                dataGridView1.Columns.Add(textColumn);

                int colIdx = 0;
                dataGridView1.Columns[colIdx].Width = 125;
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 125;
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 80;
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            #endregion

            dataGridView1.Rows.Clear();
            foreach (NBaseData.DAC.MsVesselScheduleKindDetailEnable vessellScheduleKindDetailEnable in vessellScheduleKindDetailEnableList)
            {
                int colNo = 0;
                object[] rowDatas = new object[3];

                rowDatas[colNo] = vessellScheduleKindDetailEnable.ScheduleKindName;
                colNo++;
                rowDatas[colNo] = vessellScheduleKindDetailEnable.ScheduleKindDetailName;
                colNo++;
                rowDatas[colNo] = "";
                colNo++;

                dataGridView1.Rows.Add(rowDatas);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            if (x != 2)
                return;
            if (y < 0)
                return;

            string val = (string)dataGridView1.Rows[y].Cells[x].Value;

            dataGridView1.Rows[y].Cells[x].Value = val == "✔" ? "" : "✔";


            vessellScheduleKindDetailEnableList[y].Enabled = val == "✔" ? false : true;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pbox = sender as PictureBox;
                colorDialog1.Color = pbox.BackColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    pbox.BackColor = colorDialog1.Color;
                }
            }
        }
    }
}
