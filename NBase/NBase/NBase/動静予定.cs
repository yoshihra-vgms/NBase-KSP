using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using NBase.Controls;
using NBaseCommon;
using NBaseData.DAC;

namespace NBase
{
    public partial class 動静予定 : Form
    {
        private static 動静予定 instance;

        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;
        public List<MsVessel> MsVessel_List = null;
        public List<MsBasho> MsBasho_list = null;
        public List<MsKichi> MsKichi_list = null;
        public List<MsCargo> MsCargo_list = null;
        public List<MsDjTani> MsDjTani_list = null;
        public List<MsCustomer> MsCustomer_list = null;

        public PtKanidouseiInfo KanidouseiInfo;
        private DjDousei Dousei;
        private int OrgMsVesselId = 0;

        public static 動静予定 Instance()
        {
            if (instance == null)
            {
                instance = new 動静予定();
            }

            return instance;
        }

        private 動静予定()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("", "動静予定", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            KanidouseiInfo = null;
        }

        public void Set動静予定(PtKanidouseiInfo p)
        {
            KanidouseiInfo = p;
        }

        private void 動静予定_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }

        private void 動静予定_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            List<DjDousei> douseis = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                if (KanidouseiInfo != null)
                {
                    douseis = serviceClient.DjDousei_GetRecordsBySameVoaygeNo(NBaseCommon.Common.LoginUser, KanidouseiInfo.DjDouseiID);
                }
            }

            // パネルの準備
            #region
            douseiYoteiUserControl1.SetMode(DouseiYoteiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl2.SetMode(DouseiYoteiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl3.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl4.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl5.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl6.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl7.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            #endregion

            // DDL構築
            #region
            MakeDropDownList();
            #endregion

            // 初期化
            if (douseis == null)
            {
                #region 新規登録
                Dousei = new DjDousei();
                
                // デフォルト表示
                button_登録.Enabled = true;
                button_削除.Enabled = false;

                // パネルは積／揚
                radioButton_TumiAge.Checked = true;

                if (KanidouseiInfo != null)
                {
                    // 船は、クリックしたセルの船を選択
                    foreach (MsVessel vessel in MsVessel_List)
                    {
                        if (vessel.MsVesselID == KanidouseiInfo.MsVesselID)
                        {
                            comboBox_船.SelectedItem = vessel;
                            break;
                        }
                    }

                    // 待機/入渠/パージ の日付は、クリックしたセルの日付
                    dateTimePicker1.Value = KanidouseiInfo.EventDate;
                    dateTimePicker2.Value = KanidouseiInfo.EventDate;


                    // 2014年11月度改造
                    // 積みの日付は、クリックしたセルの日付
                    douseiYoteiUserControl1.SetDate(KanidouseiInfo.EventDate);

                    // 2014.12.17
                    // 積み１のみ日付をセットしたいとコメントありのため、コードをコメントアウト
                    //douseiYoteiUserControl2.SetDate(KanidouseiInfo.EventDate);
                }
                #endregion


                // 2014.11.25: 2014年11月度改造
                button_複製.Enabled = false;
            }
            else
            {
                #region 更新
                OrgMsVesselId = douseis[0].MsVesselID;

                Dousei = new DjDousei();
                Dousei.DjDouseis = douseis;

                if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId)
                {
                    radioButton_TumiAge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                {
                    radioButton_TumiAge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Taiki.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Hihaku.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Purge.Checked = true;
                }
                else if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId)
                {
                    radioButton_Etc.Checked = true;
                }



                // 過去のデータは編集できないように修正
                //if (KanidouseiInfo.EventDate < DateTime.Today)
                //{
                //    button_登録.Enabled = false;
                //    button_削除.Enabled = false;
                //}
                // 2011.09.30 過去データもなんでもありとする
                button_登録.Enabled = true;
                button_削除.Enabled = true;

                int tumiCount = 0;
                int ageCount = 0;
                DateTime startDay = DateTime.MinValue;
                bool isSet = false;
                foreach (DjDousei dousei in douseis)
                {
                    if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId)
                    {
                        #region 積み
                        if (isSet == false)
                        {
                            foreach (MsVessel vessel in MsVessel_List)
                            {
                                if (vessel.MsVesselID == dousei.MsVesselID)
                                {
                                    comboBox_船.SelectedItem = vessel;
                                    break;
                                }
                            }
                        }

                        if (tumiCount == 0)
                        {
                            tumiCount++;
                            douseiYoteiUserControl1.SetDousei(dousei);
                        }
                        else
                        {
                            douseiYoteiUserControl2.SetDousei(dousei);
                        }
                        #endregion
                    }
                    else if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                    {
                        #region 揚げ
                        if (isSet == false)
                        {
                            foreach (MsVessel vessel in MsVessel_List)
                            {
                                if (vessel.MsVesselID == dousei.MsVesselID)
                                {
                                    comboBox_船.SelectedItem = vessel;
                                    break;
                                }
                            }
                        }

                        if (ageCount == 0)
                        {
                            ageCount++;
                            douseiYoteiUserControl3.SetDousei(dousei);
                        }
                        else if (ageCount == 1)
                        {
                            ageCount++;
                            douseiYoteiUserControl4.SetDousei(dousei);
                        }
                        else if (ageCount == 2)
                        {
                            ageCount++;
                            douseiYoteiUserControl5.SetDousei(dousei);
                        }
                        else if (ageCount == 3)
                        {
                            ageCount++;
                            douseiYoteiUserControl6.SetDousei(dousei);
                        }
                        else if (ageCount == 4)
                        {
                            ageCount++;
                            douseiYoteiUserControl7.SetDousei(dousei);
                        }
                        #endregion
                    }
                    else if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId
                            || dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId
                            || dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId
                            || dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId)
                    {
                        #region 待機/入渠/パージ/避泊
                        if (startDay == DateTime.MinValue)
                        {
                            dateTimePicker1.Value = dousei.DouseiDate;
                            dateTimePicker2.Value = dousei.DouseiDate;
                            startDay = dousei.DouseiDate;
                        }
                        else if (startDay < dousei.DouseiDate)
                        {
                            dateTimePicker2.Value = dousei.DouseiDate;
                        }
                        else
                        {
                            dateTimePicker1.Value = dousei.DouseiDate;
                            startDay = dousei.DouseiDate;
                        }
                        if (isSet == false)
                        {
                            foreach (MsVessel vessel in MsVessel_List)
                            {
                                if (vessel.MsVesselID == dousei.MsVesselID)
                                {
                                    comboBox_船.SelectedItem = vessel;
                                    break;
                                }
                            }
                            foreach (MsBasho basho in MsBasho_list)
                            {
                                if (basho.MsBashoId == dousei.MsBashoID)
                                {
                                    comboBox_港.SelectedItem = basho;
                                    break;
                                }
                            }
                            foreach (MsKichi kichi in MsKichi_list)
                            {
                                if (kichi.MsKichiId == dousei.MsKichiID)
                                {
                                    comboBox_基地.SelectedItem = kichi;
                                    break;
                                }
                            }
                            textBox_備考.Text = dousei.Bikou;
                            isSet = true;
                        }
                        #endregion
                    }
                }

                #endregion


                // 2014.11.25: 2014年11月度改造
                if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId
                    || KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                {
                    button_複製.Enabled = true;
                }
                else
                {
                    button_複製.Enabled = false;
                }
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// ドロップダウンリスト構築
        /// </summary>
        #region private void MakeDropDownList()
        private void MakeDropDownList()
        {
            // 船
            comboBox_船.Items.Clear();
            foreach (MsVessel vessel in MsVessel_List)
            {
                comboBox_船.Items.Add(vessel);
                comboBox_船.AutoCompleteCustomSource.Add(vessel.VesselName);
            }
            comboBox_船.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox_船.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox_船.SelectedIndex = 0;

            // 港
            comboBox_港.Items.Clear();
            comboBox_港.Items.Add("");
            foreach (MsBasho basho in MsBasho_list)
            {
                comboBox_港.Items.Add(basho);
                comboBox_港.AutoCompleteCustomSource.Add(basho.BashoName);
            }
            comboBox_港.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox_港.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox_港.SelectedIndex = 0;

            // 基地
            comboBox_基地.Items.Clear();
            comboBox_基地.Items.Add("");
            foreach (MsKichi kichi in MsKichi_list)
            {
                comboBox_基地.Items.Add(kichi);
                comboBox_基地.AutoCompleteCustomSource.Add(kichi.KichiName);
            }
            comboBox_基地.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox_基地.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox_基地.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「積／揚」ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void radioButton_TumiAge_CheckedChanged(object sender, EventArgs e)
        private void radioButton_TumiAge_CheckedChanged(object sender, EventArgs e)
        {
            panel_TaikiNyukyoParge.Visible = false;

            tableLayoutPanel_TumiAge.Visible = true;
            tableLayoutPanel_TumiAge.Location = new Point(0, 0);

            this.Size = new Size(1263, 768);

        }
        #endregion

        /// <summary>
        /// 「待機」　ラジオボタンクリック
        /// 「入渠」　ラジオボタンクリック
        /// 「パージ」ラジオボタンクリック
        /// 「避泊」　ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void radioButton_TaikiNyukyoParge_CheckedChanged(object sender, EventArgs e)
        private void radioButton_TaikiNyukyoParge_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel_TumiAge.Visible = false;

            panel_TaikiNyukyoParge.Visible = true;
            panel_TaikiNyukyoParge.Location = new Point(0, 0);

            //this.Size = new Size(980, 200);
            this.Size = new Size(1025, 200);
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
        private void button_登録_Click(object sender, EventArgs e)
        {
            bool ret = true;
            if (panel_TaikiNyukyoParge.Visible)
            {
                ret = 待機_入渠_パージ_登録();
            }
            else
            {
                ret = 積_揚_登録();
            }
            if (ret == true)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
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
            if (KanidouseiInfo == null)
            {
                return;
            }

            // 削除処理
            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_動静予定削除(NBaseCommon.Common.LoginUser, KanidouseiInfo);
            }
            if (ret == true)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        #endregion

        /// <summary>
        /// 登録処理（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private bool 待機_入渠_パージ_登録()
        private bool 待機_入渠_パージ_登録()
        {
            if (Validation_待機_入渠_パージ() == false)
            {
                return false;
            }
            List<DjDousei> douseiInfos = Fill_待機_入渠_パージ();

            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_動静予定登録(NBaseCommon.Common.LoginUser, douseiInfos);
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 入力値の検証を行う（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private bool Validation_待機_入渠_パージ()
        private bool Validation_待機_入渠_パージ()
        {
            DateTime date1 = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            DateTime date2 = DateTime.Parse(dateTimePicker2.Value.ToShortDateString());
            if (date1 > date2)
            {
                MessageBox.Show("日付を正しく選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox_港.SelectedIndex == 0)
            {
                MessageBox.Show("港を選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (comboBox_基地.SelectedIndex == 0)
            //{
            //    MessageBox.Show("基地を選択して下さい", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            return true;
        }
        #endregion

        /// <summary>
        /// 入力データをクラスにセットする（待機/入渠/パージ/避泊）
        /// </summary>
        /// <returns></returns>
        #region private List<DjDousei> Fill_待機_入渠_パージ()
        private List<DjDousei> Fill_待機_入渠_パージ()
        {
            List<DjDousei> ret = new List<DjDousei>();

            MsVessel vessel = comboBox_船.SelectedItem as MsVessel;
            DateTime date1 = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            DateTime date2 = DateTime.Parse(dateTimePicker2.Value.ToShortDateString());
            MsBasho basho = comboBox_港.SelectedItem as MsBasho;
            string kichiId = null;
            if (comboBox_基地.SelectedItem is MsKichi)
            {
                MsKichi kichi = comboBox_基地.SelectedItem as MsKichi;
                kichiId = kichi.MsKichiId;
            }
            string bikou = textBox_備考.Text;

            int nowDouseiCount = Dousei.DjDouseis.Count();
            int setDouseiCount = 0;
            for (DateTime setDay = date1; setDay <= date2; setDay = setDay.AddDays(1))
            {
                DjDousei dousei = null;
                if (setDouseiCount >= nowDouseiCount)
                {
                    dousei = new DjDousei();

                    Dousei.DjDouseis.Add(dousei);
                }
                else
                {
                    dousei = Dousei.DjDouseis[setDouseiCount];
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.DouseiDate = setDay;
                dousei.MsKanidouseiInfoShubetuID = GrtMsKanidouseiInfoShubetuId();
                dousei.MsBashoID = basho.MsBashoId;
                dousei.MsKichiID = kichiId;
                dousei.Bikou = bikou;

                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }

                setDouseiCount++;
            }
            if (nowDouseiCount > setDouseiCount)
            {
                for (int i = setDouseiCount; i < nowDouseiCount; i++)
                {
                    DjDousei dousei = Dousei.DjDouseis[setDouseiCount];
                    dousei.DeleteFlag = 1;
                }
            }

            return Dousei.DjDouseis;
        }
        #endregion


        /// <summary>
        /// 登録処理（積み/揚げ）
        /// </summary>
        /// <returns></returns>
        #region private bool 積_揚_登録()
        private bool 積_揚_登録()
        {
            if (Validation_積_揚() == false)
            {
                return false;
            }
            List<DjDousei> douseiInfos = Fill_積_揚();

            #region 20150825add 入力データ数チェック
            if (douseiInfos.Count == 0)
            {
                MessageBox.Show("データを入力してください", "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            #endregion

            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_動静予定登録(NBaseCommon.Common.LoginUser, douseiInfos);
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 入力値の検証を行う（積み/揚げ）
        /// </summary>
        /// <returns></returns>
        #region private bool Validation_積_揚()
        private bool Validation_積_揚()
        {
            if (douseiYoteiUserControl1.Validation() == false) // 積み１
            {
                return false;
            }
            if (douseiYoteiUserControl2.Validation() == false) // 積み２
            {
                return false;
            }
            if (douseiYoteiUserControl3.Validation() == false) // 揚げ１
            {
                return false;
            }
            if (douseiYoteiUserControl4.Validation() == false) // 揚げ２
            {
                return false;
            }
            if (douseiYoteiUserControl5.Validation() == false) // 揚げ３
            {
                return false;
            }
            if (douseiYoteiUserControl6.Validation() == false) // 揚げ４
            {
                return false;
            }
            if (douseiYoteiUserControl7.Validation() == false) // 揚げ５
            {
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 入力データをクラスにセットする（積み/揚げ）
        /// </summary>
        /// <returns></returns>
        #region private List<DjDousei> Fill_積_揚()
        private List<DjDousei> Fill_積_揚()
        {
            List<DjDousei> ret = new List<DjDousei>();

            MsVessel vessel = comboBox_船.SelectedItem as MsVessel;

            DjDousei dousei = null;

            // 積み１
            dousei = douseiYoteiUserControl1.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }
            // 積み２
            dousei = douseiYoteiUserControl2.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }

            // 揚げ１
            dousei = douseiYoteiUserControl3.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }
            // 揚げ２
            dousei = douseiYoteiUserControl4.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }
            // 揚げ３
            dousei = douseiYoteiUserControl5.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }
            // 揚げ４
            dousei = douseiYoteiUserControl6.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }
            // 揚げ５
            dousei = douseiYoteiUserControl7.GetInstance();
            if (dousei != null)
            {
                if (vessel.MsVesselID != OrgMsVesselId)
                {
                    dousei.VoyageNo = "";
                }
                dousei.MsVesselID = vessel.MsVesselID;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId;
                ret.Add(dousei);
            }

            return ret;
        }
        #endregion


        /// <summary>
        /// ラジオボタンに対応する種別IDを取得する
        /// </summary>
        /// <returns></returns>
        #region private string GrtMsKanidouseiInfoShubetuId()
        private string GrtMsKanidouseiInfoShubetuId()
        {
            if (radioButton_TumiAge.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚積).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Taiki.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.待機).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Etc.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.その他).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Purge.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.パージ).MsKanidouseiInfoShubetuId;
            }
            else if (radioButton_Hihaku.Checked == true)
            {
                return MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.避泊).MsKanidouseiInfoShubetuId;
            }
            else
            {
                return "";
            }
        }
        #endregion




        private void button_複製_Click(object sender, EventArgs e)
        {
            // １度複製をクリックしたら、ボタンは無効とする
            button_複製.Enabled = false;

            // 削除
            button_削除.Enabled = false;

            // 種別の変更もできないようにする
            radioButton_Taiki.Enabled = false;
            radioButton_Hihaku.Enabled = false;
            radioButton_Purge.Enabled = false;
            radioButton_Etc.Enabled = false;


            // 画面の日付、積荷をクリア
            douseiYoteiUserControl1.ClearDousei();
            douseiYoteiUserControl1.ClearDate();
            douseiYoteiUserControl1.ClearTumini();

            douseiYoteiUserControl2.ClearDousei();
            douseiYoteiUserControl2.ClearDate();
            douseiYoteiUserControl2.ClearTumini();

            douseiYoteiUserControl3.ClearDousei();
            douseiYoteiUserControl3.ClearDate();
            douseiYoteiUserControl3.ClearTumini();

            douseiYoteiUserControl4.ClearDousei();
            douseiYoteiUserControl4.ClearDate();
            douseiYoteiUserControl4.ClearTumini();

            douseiYoteiUserControl5.ClearDousei();
            douseiYoteiUserControl5.ClearDate();
            douseiYoteiUserControl5.ClearTumini();

            douseiYoteiUserControl6.ClearDousei();
            douseiYoteiUserControl6.ClearDate();
            douseiYoteiUserControl6.ClearTumini();

            douseiYoteiUserControl7.ClearDousei();
            douseiYoteiUserControl7.ClearDate();
            douseiYoteiUserControl7.ClearTumini();

            // 新規と同扱いにするので、簡易動静情報はクリア
            KanidouseiInfo = null;

            // 新規と同扱いにするので、動静情報はクリア
            Dousei.DjDouseis = null;
        }

    }
}
