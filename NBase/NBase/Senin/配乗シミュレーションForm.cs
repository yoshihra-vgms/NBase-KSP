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
using NBaseUtil;
using NBaseMaster;
using NBaseData.BLC;
using System.IO;

namespace Senin
{
    public partial class 配乗シミュレーションForm : Form
    {
        private DateTime 基準日 = DateTime.MinValue;
        private List<配乗シミュレーション> 下船者List = null;
        private List<配乗シミュレーション> 乗船者List = null;


        private Dictionary<int, string> cargoGroupDic = new Dictionary<int, string>();

        public 配乗シミュレーションForm()
        {
            InitializeComponent();
        }

        private void 配乗シミュレーションForm_Load(object sender, EventArgs e)
        {
            InitComboBox船(comboBox下船者_船);
            InitComboBox職名(comboBox下船者_職名);

            Init下船者検索条件();

            Init下船者一覧();

            button_交代者解除.Enabled = false;


            InitComboBox船(comboBox乗船者_船);
            InitComboBox職名(comboBox乗船者_職名);
            InitCheckedListBox種別();

            Init乗船者検索条件();

            Init乗船者一覧();

            nullableDateTimePicker_乗船予定日.Value = DateTime.Today.AddMonths(1);

            button_交代者決定.Enabled = false;


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsCargoGroup> cargoGroupList = serviceClient.MsCargoGroup_GetRecords(NBaseCommon.Common.LoginUser);
                foreach(MsCargoGroup cg in cargoGroupList)
                {
                    cargoGroupDic.Add(cg.MsCargoGroupID, cg.CargoGroupName);
                }
            }

        }

        #region private void Init下船者検索条件()
        private void Init下船者検索条件()
        {
            dateTimePicker基準日.Value = DateTime.Today.AddMonths(1);
            comboBox下船者_船.SelectedIndex = 0;
            comboBox下船者_職名.SelectedIndex = 0;
            textBox乗船日数.Text = "90";

            checkBox下船者_個人予定.Checked = true;
        }
        #endregion

        #region private void Init乗船者検索条件()
        private void Init乗船者検索条件()
        {
            comboBox乗船者_船.SelectedIndex = 0;
            comboBox乗船者_職名.SelectedIndex = 0;
            textBox休暇日数.Text = "30";

            checkBox免許免状.Checked = true;
            checkBox講習.Checked = false;
            checkBox乗船者_個人予定.Checked = true;
            checkBox乗船者_乗り合わせ.Checked = true;
            checkBox乗船者_経験.Checked = true;

            int index = 0;
            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                if (s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.休暇買上 &&
                    s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数 &&
                    s.KyuukaFlag != (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数)
                {
                    if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                    {
                        checkedListBox種別.SetItemChecked(index, false);
                    }
                    else
                    {
                        checkedListBox種別.SetItemChecked(index, true);
                    }
                    index++;
                }
            }
        }
        #endregion

        #region private void InitComboBox船(ComboBox combo)
        private void InitComboBox船(ComboBox combo)
        {
            combo.Items.Add(string.Empty);

            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                combo.Items.Add(v);
            }

            combo.SelectedIndex = 0;
        }
        #endregion

        #region private void InitComboBox職名(ComboBox combo)
        private void InitComboBox職名(ComboBox combo)
        {
            combo.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                combo.Items.Add(s);
            }

            combo.SelectedIndex = 0;
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
                    if (SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                    {
                        checkedListBox種別.SetItemChecked(checkedListBox種別.Items.Count - 1, false);
                    }
                    else
                    {
                        checkedListBox種別.SetItemChecked(checkedListBox種別.Items.Count - 1, true);
                    }
                }
            }
        }
        #endregion

        #region private void Init下船者一覧()
        private void Init下船者一覧()
        {
            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船名";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船種";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "乗船日数";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "交代者";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "交代日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "MsSeninID";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion
        }
        #endregion

        #region private void Init乗船者一覧()
        private void Init乗船者一覧()
        {
            dataGridView2.Rows.Clear();

            #region カラムの設定
            if (dataGridView2.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "種別";
                textColumn.Width = 120;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 90;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "休暇日数";
                textColumn.Width = 90;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "積荷経験";
                textColumn.Width = 175;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "外航経験";
                textColumn.Width = 100;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船名";
                textColumn.Width = 120;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "MsSeninID";
                textColumn.Visible = false;
                dataGridView2.Columns.Add(textColumn);

            }
            #endregion
        }
        #endregion




        #region private void button個人予定_Click(object sender, EventArgs e)
        private void button個人予定_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePicker基準日.Value;
            DateTime toDate = fromDate.AddDays(90);

            個人予定一覧Form form = 個人予定一覧Form.Instance();
            form.Show(fromDate, toDate);
            form.Activate();
        }
        #endregion

        #region private void button免許免状_Click(object sender, EventArgs e)
        private void button免許免状_Click(object sender, EventArgs e)
        {
            int vesselId = -1;
            int shokumeiId = -1;

            if (comboBox乗船者_船.SelectedItem is MsVessel)
            {
                vesselId = (comboBox乗船者_船.SelectedItem as MsVessel).MsVesselID;
            }
            if (comboBox乗船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox乗船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            乗船資格Form form = 乗船資格Form.Instance();
            form.Show(vesselId, shokumeiId);
            form.Activate();
        }
        #endregion

        #region private void button講習_Click(object sender, EventArgs e)
        private void button講習_Click(object sender, EventArgs e)
        {
            int shokumeiId = -1;

            if (comboBox乗船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox乗船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            DateTime fromDate = dateTimePicker基準日.Value;
            DateTime toDate = fromDate.AddDays(90);

            講習管理Form form = 講習管理Form.Instance();
            form.Show(shokumeiId, fromDate, toDate);
            form.Activate();
        }
        #endregion

        #region private void button乗り合わせ_Click(object sender, EventArgs e)
        private void button乗り合わせ_Click(object sender, EventArgs e)
        {
            int shokumeiId = -1;
            
            if (checkBox乗船者_職名.Checked  && comboBox乗船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox乗船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            乗り合わせ一覧Form form = 乗り合わせ一覧Form.Instance();
            form.Show(shokumeiId);
            form.Activate();
        }
        #endregion


        private void button経験_Click(object sender, EventArgs e)
        {
            乗船経験Form form = new 乗船経験Form();
            form.Show();
            form.Activate();
        }


        /// <summary>
        /// 「下船者選択」の「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button下船者検索_Click(object sender, EventArgs e)
        private void button下船者検索_Click(object sender, EventArgs e)
        {
            if (NumberUtils.Validate(textBox乗船日数.Text) == false)
            {
                MessageBox.Show("乗船日数を正しく入力してください");
                return;
            }

            基準日 = dateTimePicker基準日.Value;

            int vesselId = -1;
            int shokumeiId = -1;
            int days = 0;
            bool personalScheduleCheck = false;

            if (comboBox下船者_船.SelectedItem is MsVessel)
            {
                vesselId = (comboBox下船者_船.SelectedItem as MsVessel).MsVesselID;
            }
            if (comboBox下船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox下船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            days = int.Parse(textBox乗船日数.Text);
            personalScheduleCheck = checkBox下船者_個人予定.Checked;


            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    下船者List = serviceClient.BLC_配乗シミュレーション_下船者検索(NBaseCommon.Common.LoginUser, 基準日, vesselId, shokumeiId, days, personalScheduleCheck);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            Set下船者一覧();


            dataGridView2.Rows.Clear();
        }
        #endregion

        /// <summary>
        /// 下船者一覧にデータをセット
        /// </summary>
        #region private void Set下船者一覧()
        private void Set下船者一覧()
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            if (下船者List != null)
            {
                int rowNo = 0;
                foreach (配乗シミュレーション info in 下船者List)
                {
                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[8];
                    rowDatas[colNo] = SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, info.MsVesselID);
                    colNo++;
                    rowDatas[colNo] = ""; // 船種？？
                    colNo++;
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, info.MsSiShokumeiID);
                    colNo++;
                    rowDatas[colNo] = info.SeninName;
                    colNo++;
                    rowDatas[colNo] = info.WorkDays.ToString();
                    colNo++;
                    rowDatas[colNo] = (info.BoardingSchedule != null && info.BoardingSchedule.SignOnCrewName != null) ? info.BoardingSchedule.SignOnCrewName : "";
                    colNo++;
                    rowDatas[colNo] = (info.BoardingSchedule != null && info.BoardingSchedule.SignOnDate != null) ? info.BoardingSchedule.SignOnDate.ToShortDateString() : "";
                    colNo++;
                    rowDatas[colNo] = info.MsSeninID.ToString();

                    dataGridView1.Rows.Add(rowDatas);

                    rowNo++;

                    #endregion
                }
            }
            dataGridView1.CurrentCell = null;

            button_交代者解除.Enabled = false;

            Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 下船者一覧をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 選択された下船者情報
                int seninId = Get下船予定者ID();
                var selectedInfo = 下船者List.Where(obj => obj.MsSeninID == seninId).First();


                //=============================================================
                // 乗船者選択の準備
                //=============================================================
                // 下船者の乗船している船をセット
                // 原則、未乗船者から候補者を探すので、チェックは外しておく
                checkBox乗船者_船.Checked = false;
                foreach(Object item in comboBox乗船者_船.Items)
                {
                    if (item is MsVessel && (item as MsVessel).MsVesselID == selectedInfo.MsVesselID)
                    {
                        comboBox乗船者_船.SelectedItem = item;
                        break;
                    }
                }

                // 下船者の職名をセット
                // 原則、下船者と同じ職名から候補者を探すので、チェックをする
                checkBox乗船者_職名.Checked = true;
                foreach (Object item in comboBox乗船者_職名.Items)
                {
                    if (item is MsSiShokumei && (item as MsSiShokumei).MsSiShokumeiID == selectedInfo.MsSiShokumeiID)
                    {
                        comboBox乗船者_職名.SelectedItem = item;
                        break;
                    }
                }

                // 原則、指定休暇数を消化している船員から候補者を探すので、チェックをする
                checkBox休暇日数.Checked = true;


                // 交代者がセットされている場合のみ、「解除」ボタンを有効とする
                if (selectedInfo.BoardingSchedule != null)
                {
                    button_交代者解除.Enabled = true;
                }
                else
                {
                    button_交代者解除.Enabled = false;
                }

            }
            catch(Exception ex)
            {

            }
        }
        #endregion





        /// <summary>
        /// 「乗船者選択」の「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button乗船者検索_Click(object sender, EventArgs e)
        private void button乗船者検索_Click(object sender, EventArgs e)
        {
            DateTime baseDate = 基準日;
            int vesselId = -1;
            int shokumeiId = -1;
            int days = 0;
            List<int> shubetsuIds = new List<int>();
            bool vesselCheck = false;
            bool shokumeiCheck = false;
            bool menjouCheck = false;
            bool koushuCheck = false;
            bool personalScheduleCheck = false;
            bool fellowPassengersCheck = false;
            bool experienceCheck = false;

            if (comboBox乗船者_船.SelectedItem is MsVessel)
            {
                vesselId = (comboBox乗船者_船.SelectedItem as MsVessel).MsVesselID;
            }
            if (checkBox乗船者_船.Checked && vesselId == -1)
            {
                MessageBox.Show("[乗船者選択]の[船]を選択してください");
                return;
            }
            if (comboBox乗船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox乗船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            if (checkBox乗船者_職名.Checked && shokumeiId == -1)
            {
                MessageBox.Show("[乗船者選択]の[職名]を選択してください");
                return;
            }

            if (checkBox休暇日数.Checked)
            {
                if (NumberUtils.Validate(textBox休暇日数.Text) == false)
                {
                    MessageBox.Show("休暇日数を正しく入力してください");
                    return;
                }
            }
            days = int.Parse(textBox休暇日数.Text);


            vesselCheck = checkBox乗船者_船.Checked;
            shokumeiCheck = checkBox乗船者_職名.Checked;
            menjouCheck = checkBox免許免状.Checked;
            koushuCheck = checkBox講習.Checked;
            personalScheduleCheck = checkBox乗船者_個人予定.Checked;
            fellowPassengersCheck = checkBox乗船者_乗り合わせ.Checked;
            experienceCheck = checkBox乗船者_経験.Checked;


            foreach (MsSiShubetsu s in checkedListBox種別.CheckedItems)
            {
                shubetsuIds.Add(s.MsSiShubetsuID);
            }

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    乗船者List = serviceClient.BLC_配乗シミュレーション_乗船者検索(NBaseCommon.Common.LoginUser, baseDate, vesselId, shokumeiId, days, shubetsuIds,
                                                                                    vesselCheck, shokumeiCheck, menjouCheck, koushuCheck, personalScheduleCheck, fellowPassengersCheck, experienceCheck);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            Set乗船者一覧();
        }
        #endregion

        /// <summary>
        /// 乗船者一覧にデータをセット
        /// </summary>
        #region private void Set乗船者一覧()
        private void Set乗船者一覧()
        {
            Cursor = Cursors.WaitCursor;

            dataGridView2.Rows.Clear();

            if (乗船者List != null)
            {
                int rowNo = 0;
                foreach (配乗シミュレーション info in 乗船者List)
                {
                    if (info.BoardingSchedule != null)
                        continue;


                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[8];
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, info.MsSiShubetsuID);
                    colNo++;
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, info.MsSiShokumeiID);
                    colNo++;
                    rowDatas[colNo] = info.SeninName;
                    colNo++;
                    rowDatas[colNo] = info.HoliDays.ToString();
                    colNo++;

                    string expStr = "";
                    if (info.experienceCargos != null)
                    {
                        foreach(SiExperienceCargo expCargo in info.experienceCargos)
                        {
                            if (expStr.Length > 0)
                            {
                                expStr += " ";
                            }
                            expStr += cargoGroupDic[expCargo.MsCargoGroupID] + "(" + expCargo.Count.ToString() + ")";
                        }
                    }
                    rowDatas[colNo] = expStr;
                    colNo++;

                    expStr = "";
                    if (info.experienceForeigns != null)
                    {
                        foreach (SiExperienceForeign expForeign in info.experienceForeigns)
                        {
                            if (expStr.Length > 0)
                            {
                                expStr += " ";
                            }
                            expStr += (expForeign.C5_Flag == 0 ? "外" : "C5外") + "(" + expForeign.Count.ToString() + ")";
                        }
                    }
                    rowDatas[colNo] = expStr;
                    colNo++;

                    rowDatas[colNo] = SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, info.MsVesselID);
                    colNo++;
                    rowDatas[colNo] = info.MsSeninID.ToString();

                    dataGridView2.Rows.Add(rowDatas);

                    rowNo++;

                    #endregion
                }
            }
            dataGridView2.CurrentCell = null;

            button_交代者決定.Enabled = false;

            Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 乗船者一覧をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 選択された乗船者情報
                int seninId = int.Parse(dataGridView2.SelectedRows[0].Cells[7].Value as string);
                var selectedInfo = 乗船者List.Where(obj => obj.MsSeninID == seninId).First();

                // 交代者としてセットされていない場合のみ、「決定」ボタンを有効とする
                if (selectedInfo.BoardingSchedule == null)
                {
                    nullableDateTimePicker_乗船予定日.Enabled = true;
                    button_交代者決定.Enabled = true;
                }
                else
                {
                    nullableDateTimePicker_乗船予定日.Enabled = false;
                    button_交代者決定.Enabled = false;
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion



        /// <summary>
        /// 「交代者決定」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_交代者決定_Click(object sender, EventArgs e)
        private void button_交代者決定_Click(object sender, EventArgs e)
        {
            int vesselId = -1;
            int shokumeiId = -1;
            if (comboBox乗船者_船.SelectedItem is MsVessel)
            {
                vesselId = (comboBox乗船者_船.SelectedItem as MsVessel).MsVesselID;
            }
            if (vesselId == -1)
            {
                MessageBox.Show("[乗船者選択]の[船]を選択してください");
                return;
            }
            if (comboBox乗船者_職名.SelectedItem is MsSiShokumei)
            {
                shokumeiId = (comboBox乗船者_職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            if (shokumeiId == -1)
            {
                MessageBox.Show("[乗船者選択]の[職名]を選択してください");
                return;
            }
            if (nullableDateTimePicker_乗船予定日.Value == null)
            {
                MessageBox.Show("乗船予定日を入力してください");
                return;
            }

            // 選択された下船者情報
            int signOffseninId = Get下船予定者ID();
            var signOffInfo = 下船者List.Where(obj => obj.MsSeninID == signOffseninId).First();

            int prevBoardingSeninId = -1;
            if (signOffInfo.BoardingSchedule != null)
            {
                if (MessageBox.Show("下船予定者[" + signOffInfo.SeninName + "]は、交代予定者が決定しています。" + System.Environment.NewLine + "交代者を変更します。よろしいですか","",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                prevBoardingSeninId = signOffInfo.BoardingSchedule.MsSeninID;
            }
            else if (MessageBox.Show("交代者を決定します。よろしいですか","",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            // 選択された乗船者情報
            int signOnSeninId = int.Parse(dataGridView2.SelectedRows[0].Cells[7].Value as string);
            var signOnInfo = 乗船者List.Where(obj => obj.MsSeninID == signOnSeninId).First();

            // 乗船予定日
            DateTime signOnDate = (DateTime)nullableDateTimePicker_乗船予定日.Value;


            SiBoardingSchedule boardingSchedule = null;
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    boardingSchedule = serviceClient.BLC_配乗シミュレーション_交代者決定(NBaseCommon.Common.LoginUser, signOnDate, signOffInfo.SiCardID, vesselId, shokumeiId, signOnSeninId);
                }
            }, "処理中です...");

            progressDialog.ShowDialog();

            if (boardingSchedule != null)
            {
                MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                boardingSchedule.SignOnCrewName = signOnInfo.SeninName;
                signOffInfo.BoardingSchedule = boardingSchedule;
                Set下船者一覧();

                if (prevBoardingSeninId > 0)
                {
                    if (乗船者List.Any(obj => obj.MsSeninID == prevBoardingSeninId))
                    {
                        var prevInfo = 乗船者List.Where(obj => obj.MsSeninID == prevBoardingSeninId).First();
                        prevInfo.BoardingSchedule = null;
                    }
                }
                signOnInfo.BoardingSchedule = boardingSchedule;
                Set乗船者一覧();
            }
            else
            {
                MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            button_交代者解除.Enabled = false;
            nullableDateTimePicker_乗船予定日.Enabled = false;
            button_交代者決定.Enabled = false;
            return;
        }
        #endregion


        /// <summary>
        /// 「交代者解除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_交代者解除_Click(object sender, EventArgs e)
        private void button_交代者解除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("交代者を解除します。よろしいですか", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // 選択された下船者情報
            int signOffseninId = Get下船予定者ID();
            var signOffInfo = 下船者List.Where(obj => obj.MsSeninID == signOffseninId).First();


            bool ret = true;
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_配乗シミュレーション_交代者解除(NBaseCommon.Common.LoginUser, signOffInfo.BoardingSchedule.SiBoardingScheduleID);
                }
            }, "処理中です...");

            progressDialog.ShowDialog();

            if (ret)
            {
                MessageBox.Show(this, "解除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (乗船者List.Any(obj => obj.MsSeninID == signOffInfo.BoardingSchedule.MsSeninID))
                {
                    var prevInfo = 乗船者List.Where(obj => obj.MsSeninID == signOffInfo.BoardingSchedule.MsSeninID).First();
                    prevInfo.BoardingSchedule = null;
                    Set乗船者一覧();
                }

                signOffInfo.BoardingSchedule = null;
                Set下船者一覧();

            }
            else
            {
                MessageBox.Show(this, "解除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            button_交代者解除.Enabled = false;
            return;
        }
        #endregion


        private int Get下船予定者ID()
        {
            return int.Parse(dataGridView1.SelectedRows[0].Cells[7].Value as string);
        }



        private void button_船員交代予定表_Click(object sender, EventArgs e)
        {
            FileUtils.SetDesktopFolder(saveFileDialog1);
            saveFileDialog1.FileName = "船員交代予定表_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                bool serverError = false;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {

                        try
                        {

                            result = serviceClient.BLC_Excel_船員交代予定表出力(NBaseCommon.Common.LoginUser);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            serverError = true;
                        }
                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //-----------------------
                //2013/12/17 追加 m.y 
                if (serverError == true)
                    return;

                if (result == null)
                {
                    MessageBox.Show("船員交代予定表の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //-----------------------

                System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(result, 0, result.Length);
                filest.Close();

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
