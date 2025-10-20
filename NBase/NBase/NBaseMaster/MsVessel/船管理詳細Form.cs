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
using NBaseData.DAC;
using System.Net.Mail;
using NBaseData.DS;
using GrapeCity.Win.MultiRow;

namespace NBaseMaster.MsVessel
{
    public partial class 船管理詳細Form : Form
    {
        private NBaseData.DAC.MsVessel MsVessel;
        private List<NBaseData.DAC.MsVesselType> vesselTypeList;
        private List<NBaseData.DAC.MsCrewMatrixType> crewMatrixTypeList;
        private List<NBaseData.DAC.MsVesselSection> vesselSectionList;

        private List<NBaseData.DAC.MsVesselKind> vesselKindList;
        private List<NBaseData.DAC.MsVesselCategory> vesselCategoryList;
        private List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList;
        private List<MsCargo> cargoList;

        public 船管理詳細Form(NBaseData.DAC.MsVessel target, List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            MsVessel = target;
            this.vessellScheduleKindDetailEnableList = vessellScheduleKindDetailEnableList;

            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船管理詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            MakeDropDownList();
            SetItems(MsVessel);
            SetVesselScheduleKindDetails(vessellScheduleKindDetailEnableList);


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




            // 既存のテンプレートの変更
            Template1 test = new Template1();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                cargoList = serviceClient.MsCargo_GetRecords(NBaseCommon.Common.LoginUser);
            }
            ComboBoxCell comboCell_Cargo;
            comboCell_Cargo = test.Row.Cells["comboBoxCell_Cargo"] as ComboBoxCell;

            foreach (MsCargo c in cargoList)
            {
                comboCell_Cargo.Items.Add(c);
            }

            // MultiRowの設定
            gcMultiRow1.Template = test;


            SetCargos(MsVessel);


            //Size size = this.Size;
            //if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘))
            //{
            //    size.Width = 972;
            //}
            //else
            //{
            //    size.Width = 506;
            //}
            //this.Size = size;
            //this.MinimumSize = size;
            //this.MaximumSize = size;
        }

        /// <summary>
        /// 更新ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!入力値チェック())
                {
                    return;
                }

                //--------------------------------------------------------
                // UpDate処理
                //--------------------------------------------------------
                #region 入力値を取得する
                // 2010.03.16:aki 対象は決まっているので new しない
                //NBaseData.DAC.MsVessel MsVessel = new NBaseData.DAC.MsVessel();
                //MsVessel.MsVesselID = msVessel.MsVesselID;
                MsVessel.VesselNo = VesselNo_textBox.Text;
                MsVessel.VesselName = VesselName_textBox.Text;
                MsVessel.DWT = Convert.ToInt32(Dwt_textBox.Text);
                MsVessel.Capacity = Convert.ToInt32(Capacity_textBox.Text);
                MsVessel.HpTel = HpTell_textBox.Text;
                MsVessel.Tel = Tell_textBox.Text;
                MsVessel.MsVesselTypeID = ((ListItem)VesselType_comboBox.SelectedItem).Value;
                MsVessel.RenewDate = DateTime.Now;
                MsVessel.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                //MsVessel.Ts = msVessel.Ts;

                MsVessel.OfficialNumber = OfficialNumber_textBox.Text;
                if (CargoWeight1_textBox.Text.Length == 0 && CargoWeight2_textBox.Text.Length == 0)
                {
                    MsVessel.CargoWeight = 0;
                }
                else
                {
                    MsVessel.CargoWeight = Convert.ToDecimal(CargoWeight1_textBox.Text + "." + CargoWeight2_textBox.Text);
                }
                if (GRT1_textBox.Text.Length == 0 && GRT2_textBox.Text.Length == 0)
                {
                    MsVessel.GRT = 0;
                }
                else
                {
                    MsVessel.GRT = Convert.ToDecimal(GRT1_textBox.Text + "." + GRT2_textBox.Text);
                }

                if (ShowOrder_textBox.Text != "")
                {
                    MsVessel.ShowOrder = Convert.ToInt32(ShowOrder_textBox.Text);
                }
                else
                {
                    MsVessel.ShowOrder = Number.MaxValue(9);
                }

                #region 機能権限
                //機能権限---------------------------------------
                if (発注_checkBox.Checked == false)
                {
                    MsVessel.HachuEnabled = 0;
                }
                else
                {
                    MsVessel.HachuEnabled = 1;
                }

                if (予実_checkBox.Checked == false)
                {
                    MsVessel.YojitsuEnabled = 0;
                }
                else
                {
                    MsVessel.YojitsuEnabled = 1;
                }
                if (本船_checkBox.Checked == false)
                {
                    MsVessel.HonsenEnabled = 0;
                }
                else
                {
                    MsVessel.HonsenEnabled = 1;
                }
                if (動静_checkBox.Checked == false)
                {
                    MsVessel.KanidouseiEnabled = 0;
                }
                else
                {
                    MsVessel.KanidouseiEnabled = 1;
                }

                if (文書_checkBox.Checked == false)
                {
                    MsVessel.DocumentEnabled = 0;
                }
                else
                {
                    MsVessel.DocumentEnabled = 1;
                }
                if (船員_checkBox.Checked == false)
                {
                    MsVessel.SeninEnabled = 0;
                }
                else
                {
                    MsVessel.SeninEnabled = 1;
                }
                if (検査_checkBox.Checked == false)
                {
                    MsVessel.KensaEnabled = 0;
                }
                else
                {
                    MsVessel.KensaEnabled = 1;
                }

                // 荷役は標準では利用なしとする
                MsVessel.NiyakuEnabled = 0;

                #endregion

                #region 実績表示

                //実績表示---------------------------------------
                if (発注R_checkBox.Checked == false)
                {
                    MsVessel.HachuResults = 0;
                }
                else
                {
                    MsVessel.HachuResults = 1;
                }

                if (予実R_checkBox.Checked == false)
                {
                    MsVessel.YojitsuResults = 0;
                }
                else
                {
                    MsVessel.YojitsuResults = 1;
                }
                if (動静R_checkBox.Checked == false)
                {
                    MsVessel.KanidouseiResults = 0;
                }
                else
                {
                    MsVessel.KanidouseiResults = 1;
                }

                if (文書R_checkBox.Checked == false)
                {
                    MsVessel.DocumentResults = 0;
                }
                else
                {
                    MsVessel.DocumentResults = 1;
                }
                if (船員R_checkBox.Checked == false)
                {
                    MsVessel.SeninResults = 0;
                }
                else
                {
                    MsVessel.SeninResults = 1;
                }
                if (検査R_checkBox.Checked == false)
                {
                    MsVessel.KensaResults = 0;
                }
                else
                {
                    MsVessel.KensaResults = 1;
                }

                #endregion

                if (CompletionDate_nullableDateTimePicker.Value != null)
                {
                    MsVessel.CompletionDate = (DateTime)CompletionDate_nullableDateTimePicker.Value;
                }
                else
                {
                    MsVessel.CompletionDate = DateTime.MinValue;
                }

                if (maskedTextBox検査基準日.Text != null)
                {
                    DateTime d;
                    DateTime.TryParse(maskedTextBox検査基準日.Text, out d);
                    MsVessel.AnniversaryDate = d;
                }
                else
                {
                    MsVessel.AnniversaryDate = DateTime.MinValue;
                }

                MsVessel.Nationality = Nationality_textBox.Text;
                MsVessel.MailAddress = MailAddress_textBox.Text;
                if (CrewMatrix_comboBox.SelectedItem is MsCrewMatrixType)
                {
                    MsVessel.MsCrewMatrixTypeID = (CrewMatrix_comboBox.SelectedItem as MsCrewMatrixType).MsCrewMatrixTypeID;
                }
                else
                {
                    MsVessel.MsCrewMatrixTypeID = 0;
                }

                //=================================================
                // 配乗計画
                //=================================================
                {
                    MsVessel.A = pictureBox1.BackColor.A;
                    MsVessel.R = pictureBox1.BackColor.R;
                    MsVessel.G = pictureBox1.BackColor.G;
                    MsVessel.B = pictureBox1.BackColor.B;
                }

                MsVessel.NavigationArea = comboBox_NavigationArea.Text;
                MsVessel.OwnerName = textBox_OwnerName.Text;



                if (Knot1_textBox.Text.Length == 0 && Knot2_textBox.Text.Length == 0)
                {
                    MsVessel.Knot = 0;
                }
                else
                {
                    MsVessel.Knot = Convert.ToDecimal(Knot1_textBox.Text + "." + Knot2_textBox.Text);
                }
                string cargos = null;
                foreach (var row in gcMultiRow1.Rows)
                {
                    var cell = row.Cells["comboBoxCell_Cargo"];
                    var index = cell.Value;
                    if ((int)index >= 0)
                    {
                        var c = cargoList[(int)index];
                        cargos += $",{c.MsCargoID}";
                    }
                }
                if (string.IsNullOrEmpty(cargos) == false)
                    cargos = cargos.Substring(1);
                MsVessel.Cargos = cargos;


                #region 標準モジュールでは未使用

                //MsVessel.KaikeiBumonCode = KaikeiBumonCode_textBox.Text;

                //MsVessel.KyuyoRenkeiNo = KyuyoRenkeiNo_textBox.Text;

                //if (改正省エネ法エネルギー報告書_checkBox.Checked == false)
                //{
                //    MsVessel.DouseiReport1 = 0;
                //}
                //else
                //{
                //    MsVessel.DouseiReport1 = 1;
                //}

                //if (内航海運輸送実績調査票_checkBox.Checked == false)
                //{
                //    MsVessel.DouseiReport2 = 0;
                //}
                //else
                //{
                //    MsVessel.DouseiReport2 = 1;
                //}

                //if (内航船舶輸送実績調査票_checkBox.Checked == false)
                //{
                //    MsVessel.DouseiReport3 = 0;
                //}
                //else
                //{
                //    MsVessel.DouseiReport3 = 1;
                //}

                #endregion


                #region 指摘事項

                if (VesselKind_comboBox.SelectedItem is MsVesselKind)
                {
                    MsVessel.MsVesselKindID = (VesselKind_comboBox.SelectedItem as MsVesselKind).MsVesselKindID;
                }
                else
                {
                    MsVessel.MsVesselKindID = null;
                }

                if (VesselCategory_comboBox.SelectedItem is MsVesselCategory)
                {
                    MsVessel.MsVesselCategoryID = (VesselCategory_comboBox.SelectedItem as MsVesselCategory).MsVesselCategoryID;
                }
                else
                {
                    MsVessel.MsVesselKindID = null;
                }

                if (ImoNo_textBox.Text != "")
                {
                    MsVessel.ImoNO = Convert.ToInt32(ImoNo_textBox.Text);
                }
                else
                {
                    MsVessel.ImoNO = -1;
                }

                if (DeficiencyOrder_textBox.Text != "")
                {
                    MsVessel.DeficiencyOrder = Convert.ToInt32(DeficiencyOrder_textBox.Text);
                }
                else
                {
                    MsVessel.DeficiencyOrder = Number.MaxValue(9);
                }

                #endregion

                #endregion

                bool ret = true;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_船マスタ更新処理_更新処理(NBaseCommon.Common.LoginUser, MsVessel, vessellScheduleKindDetailEnableList);
                }

                if (ret)
                {
                    Message.Show確認("更新しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Message.Show確認("更新に失敗しました。");
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }



        //削除チェック
        //引数：対象
        //返り値：true→削除可能である。
        #region private bool CheckDeleteUsing(NBaseData.DAC.MsVessel data)
        private bool CheckDeleteUsing(NBaseData.DAC.MsVessel data)
        {
            //OD_THI-
            //MS_VESSEL_ITEM_VESSEL-
            //MS_LO_VESSEL-

            //BG_VESSEL_YOSAN-
            //BG_KADOU_VESSEL-
            //SN_MESSAGE_LOG	//使ってないどころかクラスがない。

            //OD_CHOZO-
            //MS_SHOUSHURI_ITEM-
            //MS_SS_SHOUSAI_ITEM-

            //OD_NYUKYO_ITEM		//DB上にテーブルがない。
            //BG_JISEKI-
            //PT_KANIDOUSEI_INFO-

            //KS_KENSA-
            //KS_SHOUSHO-
            //KS_KENSEN-

            //KS_SHINSA-
            //KS_KYUMEISETUBI-
            //KS_NIYAKU-

            //SI_CARD-
            //SI_JUNBIKIN-
            //SI_SOUKIN-

            //OD_FURIKAE_TORITATE-	/*OdFurikaeToritateFilterにMsVesselIDだけ指定して検索*/
            //SI_HAIJOU_ITEM-
            //DJ_DOUSEI- /*GetRecordsが該当*/

            //BG_YOSAN_BIKOU	


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                {
                    #region OdThi
                    List<OdThi> thilist = serviceClient.OdThi_GetRecordsByVesselId(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    //発見
                    if (thilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region MsVesselItemVessel
                    List<NBaseData.DAC.MsVesselItemVessel> veilist =
                        serviceClient.MsVesselItemVessel_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (veilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region MsLoVessel
                    List<NBaseData.DAC.MsLoVessel> lolist =
                        serviceClient.MsLoVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (lolist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //BG_VESSEL_YOSAN-
                //BG_KADOU_VESSEL-
                //SN_MESSAGE_LOG	//使ってないどころかクラスがない。
                {
                    #region BgVesselYosan
                    List<BgVesselYosan> bvylist =
                        serviceClient.BgVesselYosan_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (bvylist.Count > 0)
                    {
                        return false;
                    }
                    #endregion


                    #region BgKadouVessel
                    List<BgKadouVessel> kadoulist =
                        serviceClient.BgKadouVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (kadoulist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //OD_CHOZO-
                //MS_SHOUSHURI_ITEM-
                //MS_SS_SHOUSAI_ITEM-
                {

                    #region OdChozo
                    List<OdChozo> cholist =
                        serviceClient.OdChozo_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (cholist.Count > 0)
                    {
                        return true;
                    }
                    #endregion

                    #region MsShoushuriItem
                    List<MsShoushuriItem> shoilist =
                        serviceClient.MsShoushuriItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (shoilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region MsSsShousaiItem
                    List<MsSsShousaiItem> sslist =
                        serviceClient.MsSsShousaiItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (sslist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //OD_NYUKYO_ITEM		//DB上にテーブルがない。
                //BG_JISEKI-
                //PT_KANIDOUSEI_INFO-
                {
                    #region BgJiseki
                    List<BgJiseki> bjlist =
                        serviceClient.BgJiseki_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (bjlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region PtKanidouseiInfo
                    List<PtKanidouseiInfo> kanlist =
                        serviceClient.PtKanidouseiInfo_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (kanlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //KS_KENSA-
                //KS_SHOUSHO-
                //KS_KENSEN-
                {
                    #region KsKensa
                    List<KsKensa> kelist =
                        serviceClient.KsKensa_GetRecordsBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (kelist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region KsShousho
                    List<KsShousho> sholist =
                        serviceClient.KsShousho_GetRecordsBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (sholist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region KsKensen
                    List<KsKensen> kenlist =
                        serviceClient.KsKensen_GetRecordBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (kenlist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }


                //KS_SHINSA-
                //KS_KYUMEISETUBI-
                //KS_NIYAKU-
                {
                    #region KsShinsa
                    List<KsShinsa> shilist =
                        serviceClient.KsShinsa_GetRecordsBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (shilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region KsKyumeisetsubi
                    List<KsKyumeisetsubi> kyulist =
                        serviceClient.KsKyumeisetsubi_GetRecordsBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (kyulist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region KsNiyaku
                    List<KsNiyaku> nilist =
                        serviceClient.KsNiyaku_GetRecordsBy船ID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (nilist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //SI_CARD-
                //SI_JUNBIKIN-
                //SI_SOUKIN-
                {
                    #region SiCard
                    List<SiCard> calist =
                        serviceClient.SiCard_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (calist.Count > 0)
                    {
                        return false;
                    }
                    #endregion

                    #region SiJunbikin
                    List<SiJunbikin> julist =
                        serviceClient.SiJunbikin_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (julist.Count > 0)
                    {
                        return false;
                    }

                    #endregion

                    #region SiSoukin
                    List<SiSoukin> solist =
                        serviceClient.SiSoukin_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                    if (solist.Count > 0)
                    {
                        return false;
                    }
                    #endregion
                }

                //OD_FURIKAE_TORITATE-	/*OdFurikaeToritateFilterにMsVesselIDだけ指定して検索*/
                //SI_HAIJOU_ITEM-
                //DJ_DOUSEI- /*GetRecordsが該当*/

                //BG_YOSAN_BIKOU	

                #region OdFurikaeToritate

                //船IDのみを指定
                NBaseData.DS.OdFurikaeToritateFilter fil = new NBaseData.DS.OdFurikaeToritateFilter();
                fil.MsVesselID = data.MsVesselID;

                List<OdFurikaeToritate> fulist =
                    serviceClient.OdFurikaeToritate_GetRecordsByFilter(NBaseCommon.Common.LoginUser, fil);

                if (fulist.Count > 0)
                {
                    return false;
                }
                    
                #endregion

                #region SiHaijouItem
                List<SiHaijouItem> hailist =
                    serviceClient.SiHaijouItem_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                if (hailist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region DjDousei
                List<DjDousei> doulist =
                    serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, data.MsVesselID, null);

                if (doulist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region BgYosanBikou
                List<BgYosanBikou> bikoulist =
                    serviceClient.BgYosanBikou_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, data.MsVesselID);

                if (bikoulist.Count > 0)
                {
                    return false;
                }                     
                #endregion             
            }


            return true;
        }
        #endregion

        /// <summary>
        /// 削除ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Delete_Btn_Click(object sender, EventArgs e)
        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------
                // 確認
                //-------------------------------------------------------
                if (Message.Show問合せ("この船を削除します。よろしいですか？") == false)
                {
                    return;
                }


                /////////////////////////////////////////////////////////////////////////////////////////////
                //削除チェック
                //使用しているものは削除をしない、できない
                bool result = this.CheckDeleteUsing(this.MsVessel);

                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                /////////////////////////////////////////////////////////////////////////////////////////////

                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                // 2010.03.16:aki 対象は決まっているので new しない
                //NBaseData.DAC.MsVessel MsVessel = new NBaseData.DAC.MsVessel();
                //MsVessel.MsVesselID = msVessel.MsVesselID;
                //MsVessel.Ts = msVessel.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsVessel_DeleteFlagRecord(NBaseCommon.Common.LoginUser, MsVessel);

                    Message.Show確認("削除しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }
        #endregion

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
        /// 更新アイテムをGUIへセットする
        /// </summary>
        /// <param name="msVessel"></param>
        #region SetItems(NBaseData.DAC.MsVessel msVessel)
        private void SetItems(NBaseData.DAC.MsVessel msVessel)
        {
            //-----------------------------------------------------
            // GUIの表示設定
            //-----------------------------------------------------

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            VesselNo_textBox.Text = msVessel.VesselNo;
            VesselName_textBox.Text = msVessel.VesselName;
            Dwt_textBox.Text = msVessel.DWT.ToString();
            Capacity_textBox.Text = msVessel.Capacity.ToString();
            HpTell_textBox.Text = msVessel.HpTel;
            Tell_textBox.Text = msVessel.Tel;  
            VesselType_comboBox.Text = msVessel.VesselTypeName;
            OfficialNumber_textBox.Text = msVessel.OfficialNumber;
            if (msVessel.CargoWeight == decimal.MinValue)
            {
                CargoWeight1_textBox.Text = "";
                CargoWeight2_textBox.Text = "";
            }
            else
            {
                string cwtmp = msVessel.CargoWeight.ToString("#####0.000");
                string[] cw = cwtmp.Split('.');
                CargoWeight1_textBox.Text = cw[0];
                CargoWeight2_textBox.Text = cw[1];
            }
            if (msVessel.GRT == decimal.MinValue)
            {
                GRT1_textBox.Text = "";
                GRT2_textBox.Text = "";
            }
            else
            {
                string cwtmp = msVessel.GRT.ToString("#####0.000");
                string[] cw = cwtmp.Split('.');
                GRT1_textBox.Text = cw[0];
                GRT2_textBox.Text = cw[1];
            }

            if (msVessel.ShowOrder < Number.MaxValue(9))
            {
                ShowOrder_textBox.Text = msVessel.ShowOrder.ToString();
            }
            else
            {
                ShowOrder_textBox.Text = "";
            }



			//関連チェックリストへの表示
			//発注---------------------------------------------------
			if (msVessel.HachuEnabled == 1)
			{
				this.発注_checkBox.Checked = true;
			}
			else
			{
				this.発注_checkBox.Checked = false;
			}

			//予実---------------------------------------------------
			if (msVessel.YojitsuEnabled == 1)
			{
				this.予実_checkBox.Checked = true;
			}
			else
			{
				this.予実_checkBox.Checked = false;
			}

			//本船---------------------------------------------------
			if (msVessel.HonsenEnabled == 1)
			{
				this.本船_checkBox.Checked = true;
			}
			else
			{
				this.本船_checkBox.Checked = false;
			}

			//動静---------------------------------------------------
			if (msVessel.KanidouseiEnabled == 1)
			{
				this.動静_checkBox.Checked = true;
			}
			else
			{
				this.動静_checkBox.Checked = false;
			}

            //文書---------------------------------------------------
            if (msVessel.DocumentEnabled == 1)
            {
                this.文書_checkBox.Checked = true;
            }
            else
            {
                this.文書_checkBox.Checked = false;
            }
            //船員---------------------------------------------------
            if (msVessel.SeninEnabled == 1)
            {
                this.船員_checkBox.Checked = true;
            }
            else
            {
                this.船員_checkBox.Checked = false;
            }
            //検査---------------------------------------------------
            if (msVessel.KensaEnabled == 1)
            {
                this.検査_checkBox.Checked = true;
            }
            else
            {
                this.検査_checkBox.Checked = false;
            }

            //発注(実績表示)---------------------------------------------------
            if (msVessel.HachuResults == 1)
            {
                this.発注R_checkBox.Checked = true;
            }
            else
            {
                this.発注R_checkBox.Checked = false;
            }

            //予実(実績表示)---------------------------------------------------
            if (msVessel.YojitsuResults == 1)
            {
                this.予実R_checkBox.Checked = true;
            }
            else
            {
                this.予実R_checkBox.Checked = false;
            }

            //動静(実績表示)---------------------------------------------------
            if (msVessel.KanidouseiResults == 1)
            {
                this.動静R_checkBox.Checked = true;
            }
            else
            {
                this.動静R_checkBox.Checked = false;
            }

            //文書(実績表示)---------------------------------------------------
            if (msVessel.DocumentResults == 1)
            {
                this.文書R_checkBox.Checked = true;
            }
            else
            {
                this.文書R_checkBox.Checked = false;
            }
            //船員(実績表示)---------------------------------------------------
            if (msVessel.SeninResults == 1)
            {
                this.船員R_checkBox.Checked = true;
            }
            else
            {
                this.船員R_checkBox.Checked = false;
            }
            //検査(実績表示)---------------------------------------------------
            if (msVessel.KensaResults == 1)
            {
                this.検査R_checkBox.Checked = true;
            }
            else
            {
                this.検査R_checkBox.Checked = false;
            }

            if (msVessel.CompletionDate != DateTime.MinValue)
            {
                CompletionDate_nullableDateTimePicker.Value = msVessel.CompletionDate;
            }
            else
            {
                CompletionDate_nullableDateTimePicker.Value = null;
            }
            if (msVessel.AnniversaryDate != DateTime.MinValue)
            {
                maskedTextBox検査基準日.Text = msVessel.AnniversaryDate.ToString("MM/dd");
            }

            Nationality_textBox.Text = msVessel.Nationality;
            MailAddress_textBox.Text = msVessel.MailAddress;

            textBox営業担当.Text = msVessel.SalesPersonName;
            textBox工務監督.Text = msVessel.MarineSuperintendentName;

            if (msVessel.MsCrewMatrixTypeID == 0)
            {
                CrewMatrix_comboBox.SelectedIndex = 0;
            }
            else
            {
                int index = 0;
                foreach(MsCrewMatrixType crewMatrixType in crewMatrixTypeList)
                {
                    index++;
                    if (crewMatrixType.MsCrewMatrixTypeID == msVessel.MsCrewMatrixTypeID)
                    {
                        CrewMatrix_comboBox.SelectedIndex = index;
                        break;
                    }
                }
            }

            comboBox_NavigationArea.Text = msVessel.NavigationArea;
            textBox_OwnerName.Text = msVessel.OwnerName;


            if (msVessel.Knot == decimal.MinValue)
            {
                Knot1_textBox.Text = "";
                Knot2_textBox.Text = "";
            }
            else
            {
                string cwtmp = msVessel.Knot.ToString("###0.00");
                string[] cw = cwtmp.Split('.');
                Knot1_textBox.Text = cw[0];
                Knot2_textBox.Text = cw[1];
            }

            //=================================================
            // 配乗計画
            //=================================================
            pictureBox1.BackColor = Color.FromArgb(msVessel.A, msVessel.R, msVessel.G, msVessel.B);


            #region 標準モジュールでは未使用

            //KaikeiBumonCode_textBox.Text = msVessel.KaikeiBumonCode;

            //KyuyoRenkeiNo_textBox.Text = msVessel.KyuyoRenkeiNo;


            //if (msVessel.DouseiReport1 == 0)
            //{
            //    改正省エネ法エネルギー報告書_checkBox.Checked = false;
            //}
            //else
            //{
            //    改正省エネ法エネルギー報告書_checkBox.Checked = true;
            //}

            //if (msVessel.DouseiReport2 == 0)
            //{
            //    内航海運輸送実績調査票_checkBox.Checked = false;
            //}
            //else
            //{
            //    内航海運輸送実績調査票_checkBox.Checked = true;
            //}

            //if (msVessel.DouseiReport3 == 0)
            //{
            //    内航船舶輸送実績調査票_checkBox.Checked = false;
            //}
            //else
            //{
            //    内航船舶輸送実績調査票_checkBox.Checked = true;
            //}

            #endregion

            //=================================================
            // 指摘事項管理
            //=================================================
            #region
            if (msVessel.MsVesselKindID == null)
            {
                VesselKind_comboBox.SelectedIndex = 0;
            }
            else
            {
                int index = 0;
                foreach (MsVesselKind vesselKind in vesselKindList)
                {
                    index++;
                    if (vesselKind.MsVesselKindID == msVessel.MsVesselKindID)
                    {
                        VesselKind_comboBox.SelectedIndex = index;
                        break;
                    }
                }
            }

            if (msVessel.MsVesselCategoryID == null)
            {
                VesselCategory_comboBox.SelectedIndex = 0;
            }
            else
            {
                int index = 0;
                foreach (MsVesselCategory vesselCategory in vesselCategoryList)
                {
                    index++;
                    if (vesselCategory.MsVesselCategoryID == msVessel.MsVesselCategoryID)
                    {
                        VesselCategory_comboBox.SelectedIndex = index;
                        break;
                    }
                }
            }

            if (msVessel.ImoNO > 0)
            {
                ImoNo_textBox.Text = msVessel.ImoNO.ToString("");
            }
            else
            {
                ImoNo_textBox.Text = "";
            }

            if (msVessel.DeficiencyOrder > 0 && msVessel.DeficiencyOrder < Number.MaxValue(9))
            {
                DeficiencyOrder_textBox.Text = msVessel.DeficiencyOrder.ToString();
            }
            else
            {
                DeficiencyOrder_textBox.Text = "";
            }
            #endregion
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


                // 航行区域
                comboBox_NavigationArea.Items.Clear();
                comboBox_NavigationArea.Items.AddRange(NBaseData.DAC.MsVessel.NavigationAreaStrings.ToArray());

            }
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

            if (ImoNo_textBox.Text != "")
            {
                try
                {
                    long imoNo = Convert.ToInt64(ImoNo_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("IMO番号は数値を入力して下さい。");
                    return false;
                }
            }
            if (DeficiencyOrder_textBox.Text != "")
            {
                try
                {
                    long so = Convert.ToInt64(DeficiencyOrder_textBox.Text);
                }
                catch
                {
                    Message.Showエラー("表示順序は数値を入力して下さい。");
                    return false;
                }
                if (Number.CheckValue(Convert.ToInt32(DeficiencyOrder_textBox.Text), 9, 0) == false)
                {
                    Message.Showエラー("表示順序を正しく入力して下さい");
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
                    // 更新対象のレコード自身なら。。
                    if (msVessel.MsVesselID == MsVessel.MsVesselID)
                    {
                        // 自身なら、更新対象。
                    }
                    else
                    {
                        Message.Showエラー("入力された船Noは登録されています。");
                        return false;
                    }   
                }
            }

            //
            // 今回、"本船"のチェックがついていて、元々はついていない場合、確認の対象とする
            //
            if (本船_checkBox.Checked == true)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    NBaseData.DAC.MsVessel msVessel = null;
                    msVessel = serviceClient.MsVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, MsVessel.MsVesselID);

                    if (msVessel.HonsenEnabled == 0)
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





        private void button営業検索_Click(object sender, EventArgs e)
        {
            MsVessel.SalesPersonID = ユーザ検索(textBox営業担当);
        }

        private void button工務検索_Click(object sender, EventArgs e)
        {
            MsVessel.MarineSuperintendentID = ユーザ検索(textBox工務監督);
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
            MsVessel.SalesPersonID = null;
        }

        private void button工務クリア_Click(object sender, EventArgs e)
        {
            textBox工務監督.Text = "";
            MsVessel.MarineSuperintendentID = null;
        }





        private void SetVesselScheduleKindDetails(List<NBaseData.DAC.MsVesselScheduleKindDetailEnable> vessellScheduleKindDetailEnableList)
        {
            if (vessellScheduleKindDetailEnableList == null || vessellScheduleKindDetailEnableList.Count == 0)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    List<NBaseData.DAC.MsScheduleKindDetail> scheduleKindDetailList = serviceClient.MsScheduleKindDetail_GetRecords(NBaseCommon.Common.LoginUser);

                    foreach (NBaseData.DAC.MsScheduleKindDetail scheduleKindDetail in scheduleKindDetailList)
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
            foreach(NBaseData.DAC.MsVesselScheduleKindDetailEnable vessellScheduleKindDetailEnable in vessellScheduleKindDetailEnableList)
            {
                int colNo = 0;
                object[] rowDatas = new object[3];
              
                rowDatas[colNo] = vessellScheduleKindDetailEnable.ScheduleKindName;
                colNo++;                
                rowDatas[colNo] = vessellScheduleKindDetailEnable.ScheduleKindDetailName;
                colNo++;                
                rowDatas[colNo] = vessellScheduleKindDetailEnable.Enabled ? "✔" : "";
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





        private void SetCargos(NBaseData.DAC.MsVessel msVessel)
        {
            if (string.IsNullOrEmpty(msVessel.Cargos) == false)
            {
                var cargos = msVessel.Cargos.Split(',');

                foreach(var cargo in cargos)
                {

                    var index = cargoList.FindIndex(o => o.MsCargoID.ToString() == cargo);
                    if (index > 0)
                    {
                        gcMultiRow1.Rows.Add();
                        gcMultiRow1.SetValue(gcMultiRow1.Rows.Count - 1, "comboBoxCell_Cargo", index);
                    }
                }

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            gcMultiRow1.Rows.Add();
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];
            int r = e.RowIndex;

            //削除
            if (currentCell.Name == "buttonCell_Remove")
            {
                gcMultiRow1.Rows.RemoveAt(r);
            }
        }
    }
}
