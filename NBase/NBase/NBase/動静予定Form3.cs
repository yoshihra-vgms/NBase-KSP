using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using GrapeCity.Win.MultiRow.InputMan;
using NBase.util;
//using NBase.Controls;
using NBaseCommon;
using NBaseData.DAC;
using NBaseUtil;

namespace NBase
{
    public partial class 動静予定Form3 : Form
    {
        private static bool IsFirst = true;
        private static 動静予定Form3 instance;

        public List<MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_List;
        public List<MsVessel> MsVessel_List = null;
        public List<MsBasho> MsBasho_list = null;
        public List<MsKichi> MsKichi_list = null;
        public List<MsCargo> MsCargo_list = null;
        public List<MsDjTani> MsDjTani_list = null;
        public List<MsCustomer> MsCustomer_list = null;

        public List<MsCargo> VesselCargo_List = null;
        public List<MsCargo> DouseiCargo_List = null;

        public PtKanidouseiInfo KanidouseiInfo;
        private DjDousei Dousei;
        private int OrgMsVesselId = 0;


        private bool CopyFlag = false;

        //動静シミュレーションから呼ばれた時に使う
        private int FormKind; //簡易動静から呼ばれると1
        private List<DjDousei> SimDouseiList = null;//ｼﾐｭﾚｰｼｮﾝの動静が入る

        private enum ModeTumiAgeEnum { TUMI, AGE };
        private enum PlanResultEnum { PLAN, RESULT };

        private bool clearing = false;

        private string TextCellFormatStr = "9#^-+*/\\\\$";

        private class DouseiInfo
        {
            public ModeTumiAgeEnum TumiAge { set; get; }
            public int No { set; get; }
            public DjDousei Dousei { set; get; }

            public DouseiInfo(ModeTumiAgeEnum tumiAge, int no, DjDousei dousei)
            {
                TumiAge = tumiAge;
                No = no;
                Dousei = dousei;
            }
        }

        private List<DouseiInfo> DouseiInfoList;




        public static 動静予定Form3 Instance()
        {
            if (instance == null)
            {
                instance = new 動静予定Form3();
            }

            return instance;
        }

        private 動静予定Form3()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("", "動静予定", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            KanidouseiInfo = null;
        }

        /// <summary>
        /// 必要な情報をセット
        /// </summary>
        public void Set動静予定(PtKanidouseiInfo p)
        {
            Set動静予定(p, 0, null);
        }
        /// <summary>
        /// 必要な情報をセット
        /// </summary>
        /// <param name="p"></param>
        /// <param name="kind">1:動静シミュレーションFormから呼ばれる場合</param>
        public void Set動静予定(PtKanidouseiInfo p, int kind, List<DjDousei> dlist)
        {
            KanidouseiInfo = p;
            FormKind = kind;
            SimDouseiList = null;
            if (dlist != null)
            {
                SimDouseiList = new List<DjDousei>();
                SimDouseiList.AddRange(dlist);
            }
        }

        private void 動静予定Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            //instance = null;

        }

        private void 動静予定Form3_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            List<DjDousei> douseis = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (KanidouseiInfo != null)
                {
                    if (SimDouseiList ==null)
                    {
                        douseis = serviceClient.DjDousei_GetRecordsBySameVoaygeNo(NBaseCommon.Common.LoginUser, KanidouseiInfo.DjDouseiID);
                    }
                    else
                    {
                        douseis = new List<DjDousei>();
                        douseis.AddRange(SimDouseiList);
                    }
                    DouseiCargo_List = new List<MsCargo>();
                    if (douseis != null)
                    {
                        var cargoIds = new List<int>();
                        foreach (DjDousei dousei in douseis)
                        {
                            var tmpIds = dousei.DjDouseiCargos.Select(o => o.MsCargoID).Distinct();

                            foreach (int id in tmpIds)
                            {
                                if (cargoIds.Contains(id) == false)
                                    cargoIds.Add(id);
                            }
                        }
                        DouseiCargo_List = MsCargo_list.Where(o => cargoIds.Contains(o.MsCargoID)).ToList();
                    }
                }
            }

            //変更できないコントロールの制御
            コントロール制御();

            // DDL構築
            #region
            MakeDropDownList();
            #endregion


            // パネルの準備
            #region

            if (IsFirst)
            {
                // 既存のテンプレートの変更
                InitTemplate();

            }

            DouseiInfoList = new List<DouseiInfo>();
            for (int i = 0; i < 4; i++)
            {
                DouseiInfoList.Add(new DouseiInfo(ModeTumiAgeEnum.TUMI, i, null));
                DouseiInfoList.Add(new DouseiInfo(ModeTumiAgeEnum.AGE, i, null));
            }


            InitMultiRow(ModeTumiAgeEnum.TUMI, gcMultiRow1);

            InitMultiRow(ModeTumiAgeEnum.AGE, gcMultiRow2);

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
                }
                else
                {
                    comboBox_船.SelectedIndex = 0;
                }
                #endregion


                // 2014.11.25: 2014年11月度改造
                button_複製.Enabled = false;


                tabControl1.SelectedIndex = 0;
            }
            else
            {
                #region 更新

                Dousei = new DjDousei();
                Dousei.DjDouseis = douseis;


                foreach (MsVessel vessel in MsVessel_List)
                {
                    if (vessel.MsVesselID == douseis[0].MsVesselID)
                    {
                        comboBox_船.SelectedItem = vessel;
                        break;
                    }
                }
                OrgMsVesselId = douseis[0].MsVesselID;


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

                        SetRow(gcMultiRow1, PlanResultEnum.PLAN, tumiCount, dousei);
                        //DouseiInfoList.Add(new DouseiInfo(ModeTumiAgeEnum.TUMI, tumiCount, dousei));
                        var douseiInfo = DouseiInfoList.Where(o => o.TumiAge == ModeTumiAgeEnum.TUMI && o.No == tumiCount).First();
                        douseiInfo.Dousei = dousei;

                        tumiCount++;

                        #endregion
                    }
                    else if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                    {
                        #region 揚げ

                        SetRow(gcMultiRow2, PlanResultEnum.PLAN, ageCount, dousei);
                        //DouseiInfoList.Add(new DouseiInfo(ModeTumiAgeEnum.AGE, ageCount, dousei));
                        var douseiInfo = DouseiInfoList.Where(o => o.TumiAge == ModeTumiAgeEnum.AGE && o.No == ageCount).First();
                        douseiInfo.Dousei = dousei;

                        ageCount++;

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


                if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId
                    || KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.揚げ).MsKanidouseiInfoShubetuId)
                {
                    button_複製.Enabled = true;

                    if (KanidouseiInfo.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.GetRecordByKanidouseiShubetuName(MsKanidouseiInfoShubetu_List, MsKanidouseiInfoShubetu.積み).MsKanidouseiInfoShubetuId)
                    {
                        tabControl1.SelectedIndex = 0;
                    }
                    else
                    {
                        tabControl1.SelectedIndex = 1;
                    }
                }
                else
                {
                    button_複製.Enabled = false;
                }
            }

            CopyFlag = false;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// FormKindによって変更できないコントロールを使用できなくする
        /// </summary>
        #region private void コントロール制御()
        private void コントロール制御()
        {
            if (FormKind == 0)
            {
                comboBox_船.Enabled = true;
                groupBox1.Enabled = true;

                button_複製.Visible = true;
                button_削除.Visible = true;
                button_登録.Visible = true;
                button_シミュレーション.Visible = false;

                comboBox_港.Enabled = true;
                comboBox_基地.Enabled = true;
                textBox_備考.Enabled = true;
            }
            else
            {
                //ｼﾐｭﾚｰｼｮﾝ画面から呼ばれている
                comboBox_船.Enabled = false;
                groupBox1.Enabled = false;

                button_複製.Visible = false;
                button_削除.Visible = false;
                button_登録.Visible = false;
                button_シミュレーション.Visible = true;
                button_シミュレーション.Location = new Point(button_複製.Location.X, button_複製.Location.Y);

                comboBox_港.Enabled = false;
                comboBox_基地.Enabled = false;
                textBox_備考.Enabled = false;
            }

        }
        #endregion

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

            tabControl1.Visible = true;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Dock = DockStyle.Fill;

            //this.Size = new Size(1505, 660);
            this.Size = new Size(1516, 669);
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
            tabControl1.Visible = false;

            panel_TaikiNyukyoParge.Visible = true;
            panel_TaikiNyukyoParge.Location = new Point(0, 0);

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
        /// 「反映」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public List<DjDousei> 反映DouseiInfos = new List<DjDousei>();
        private void button_シミュレーション_Click(object sender, EventArgs e)
        {
            
            //変更したデータを取得。newされている
            反映DouseiInfos = Fill_積_揚();

            DialogResult = DialogResult.OK;
            Close();
        }


        //#region private void 反映(object sender, MouseEventArgs e)
        //private void 反映(object sender, MouseEventArgs e)
        //{

        //    HitTestInfo ht = c1FlexGrid1.HitTest(e.X, e.Y);

        //    int putrow = ht.Row;
        //    int putcol = ht.Column;

        //    if ((putrow > 0 && putcol > 0 && DragStartRow > 0 && DragStartCol > 0) == false)
        //        return;

        //    if (putrow == DragStartRow && putcol == DragStartCol)
        //        return;

        //    //Sim行ではない
        //    if (!RowMgTbl[1].Contains(putrow))
        //        return;

        //    DateTime 


        //    //過去
        //    if (c1FlexGrid1.GetCellStyle(putrow, putcol).Name == "過去")
        //        return;


        //    // 何をどう操作したか
        //    DRAG_SITUATION situation = DragSituation(putrow, putcol);



        //    // 移動後の日付
        //    DateTime changeDate = 列から日付を取得(putcol);

        //    // 移動日数
        //    var diff = putcol - DragStartCol;



        //    var checkDousei = GetCheckDousei(situation, changeDate);


        //    ////if (移動できるか(putrow, putcol))
        //    if (移動できるか(situation, diff, checkDousei))
        //    {



        //        ////移動元のセルのクリア
        //        //if (DragStartRow != putrow || DragStartCol != putcol)
        //        //{
        //        //    c1FlexGrid1[DragStartRow, DragStartCol] = null;
        //        //    c1FlexGrid1.SetCellStyle(DragStartRow, DragStartCol, "");

        //        //    //アラーム行クリア
        //        //    c1FlexGrid1.SetCellImage(RowMgAlaramList[1], DragStartCol, null);

        //        //}




        //        //DragData.KInfo.EventDate = DragData.KInfo.EventDate.AddDays(diff);
        //        //データセット(putrow, putcol, DragData);




        //        List<DjDousei> modDouseis = new List<DjDousei>(); // 変更されるDJ_DOUSEI
        //        List<DjDousei> douseis = new List<DjDousei>(); // 変更されるDJ_DOUSEI

        //        // 操作したコマに紐づくDJ_DOUSEIを変更
        //        foreach (DjDousei d in DragData.DInfos)
        //        {
        //            //if (d.DjDouseiID != DragData.KInfo.DjDouseiID)
        //            //    continue;

        //            d.DouseiDate = d.DouseiDate.AddDays(diff);
        //            if (d.PlanNyuko != DateTime.MinValue)
        //                d.PlanNyuko = d.PlanNyuko.AddDays(diff);
        //            if (d.PlanChakusan != DateTime.MinValue)
        //                d.PlanChakusan = d.PlanChakusan.AddDays(diff);
        //            if (d.PlanNiyakuStart != DateTime.MinValue)
        //                d.PlanNiyakuStart = d.PlanNiyakuStart.AddDays(diff);
        //            if (d.PlanNiyakuEnd != DateTime.MinValue)
        //                d.PlanNiyakuEnd = d.PlanNiyakuEnd.AddDays(diff);
        //            if (d.PlanRisan != DateTime.MinValue)
        //                d.PlanRisan = d.PlanRisan.AddDays(diff);
        //            if (d.PlanShukou != DateTime.MinValue)
        //                d.PlanShukou = d.PlanShukou.AddDays(diff);

        //            modDouseis.Add(d);
        //        }

        //        foreach (SimFormData data in DouseiInfoSimList)
        //        {
        //            foreach (DjDousei dousei in data.DInfos)
        //            {
        //                if (modDouseis.Any(o => o.DjDouseiID == dousei.DjDouseiID))
        //                {
        //                    var modDousei = modDouseis.Where(o => o.DjDouseiID == dousei.DjDouseiID).First();
        //                    dousei.DouseiDate = modDousei.DouseiDate;
        //                    dousei.PlanNiyakuStart = modDousei.PlanNiyakuStart;
        //                    dousei.PlanNyuko = modDousei.PlanNyuko;
        //                    dousei.PlanChakusan = modDousei.PlanChakusan;
        //                    dousei.PlanRisan = modDousei.PlanRisan;
        //                    dousei.PlanShukou = modDousei.PlanShukou;

        //                    if (data.KInfo.DjDouseiID == dousei.DjDouseiID)
        //                    {
        //                        data.KInfo.EventDate = dousei.DouseiDate;
        //                        data.EventDate = data.KInfo.EventDate;
        //                    }
        //                }
        //                if (douseis.Any(o => o.DjDouseiID == dousei.DjDouseiID) == false)
        //                {
        //                    douseis.Add(dousei);
        //                }
        //            }
        //        }



        //        ProgressDialog progressDialog = new ProgressDialog(delegate
        //        {
        //            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //            {
        //                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        //                AlarmInfos = serviceClient.BLC_GetDeviationInfos(NBaseCommon.Common.LoginUser, appName, Vessel.MsVesselID, ColStartDate, ColStartDate.AddDays(Days), douseis);
        //            }
        //        }, "アラーム算出中です...");
        //        progressDialog.ShowDialog();


        //        データ表示(1, DouseiInfoSimList);
        //        アラーム表示(1);

        //    }
        //    else
        //    {
        //        c1FlexGrid1.SetCellStyle(HoverPosRow, HoverPosCol, "");
        //    }


        //}
        //private enum DRAG_SITUATION { NA, TumiPast, TumiFuture, AgePast, AgeFuture };
        //private DRAG_SITUATION DragSituation(int row, int col)
        //{
        //    DRAG_SITUATION ret = DRAG_SITUATION.NA;

        //    // 移動後の日付


        //    DateTime changeDate = 列から日付を取得(col);

        //    // 移動データ確認
        //    var data = c1FlexGrid1.GetData(row, col) as SimFormData;

        //    string shubetuName = DragData.KInfo.KanidouseiInfoShubetuName;
        //    if (shubetuName == MsKanidouseiInfoShubetu.積み)
        //    {
        //        if (changeDate < DragData.KInfo.EventDate)
        //        {
        //            // 積みを過去方向へ移動
        //            ret = DRAG_SITUATION.TumiPast;
        //        }
        //        else
        //        {
        //            // 積みを未来方向へ移動
        //            ret = DRAG_SITUATION.TumiFuture;
        //        }
        //    }
        //    else if (shubetuName == MsKanidouseiInfoShubetu.揚げ)
        //    {
        //        if (changeDate < DragData.KInfo.EventDate)
        //        {
        //            // 揚げを過去方向へ移動
        //            ret = DRAG_SITUATION.AgePast;
        //        }
        //        else
        //        {
        //            // 揚げを未来方向へ移動
        //            ret = DRAG_SITUATION.AgeFuture;
        //        }
        //    }
        //    return ret;
        //}
        ////#endregion


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
                MessageBox.Show("日付を正しく選択して下さい", "動静予定Form3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox_港.SelectedIndex == 0)
            {
                MessageBox.Show("港を選択して下さい", "動静予定Form3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (comboBox_基地.SelectedIndex == 0)
            //{
            //    MessageBox.Show("基地を選択して下さい", "動静予定Form3", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            List<DjDousei> douseiInfos = Fill_積_揚();
            if (douseiInfos.Count == 0 || douseiInfos.Any(o => o.IsValid == false))
            {
                return false;
            }

            if (CopyFlag)
            {
                foreach(DjDousei dousei in douseiInfos)
                {
                    dousei.DjDouseiID = null;
                    dousei.VoyageNo = null;
                    foreach (DjDouseiCargo cargo in dousei.DjDouseiCargos)
                    {
                        cargo.DjDouseiCargoID = null;
                    }
                }
            }

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
        #region private bool Validation_積_揚(string target, DjDousei dousei)
        private bool Validation_積_揚(string target, DjDousei dousei)
        {
            bool IsInput = false;
            string ErrMsg = "";

            if (StringUtils.Empty(dousei.MsBashoID))
            {
                ErrMsg = "港を選択して下さい";
            }
            else
            {
                IsInput = true;
            }

            if (StringUtils.Empty(dousei.MsKichiID))
            {
                if (StringUtils.Empty(ErrMsg)) ErrMsg = "基地を選択して下さい";
            }
            else
            {
                IsInput = true;
            }

            if (dousei.PlanNyuko == DateTime.MinValue)
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "入港日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (dousei.PlanChakusan == DateTime.MinValue)
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "着桟日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (dousei.PlanNiyakuStart == DateTime.MinValue)
            {
                if (StringUtils.Empty(ErrMsg)) ErrMsg = "荷役開始日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (dousei.PlanNiyakuEnd == DateTime.MinValue)
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "荷役終了日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (dousei.PlanRisan == DateTime.MinValue)
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "離桟日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (dousei.PlanShukou == DateTime.MinValue)
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "出港日を選択して下さい";
            }
            else
            {
                IsInput = true;
            }

            if (StringUtils.Empty(dousei.DairitenID))
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "代理店を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (StringUtils.Empty(dousei.NinushiID))
            {
                //if (StringUtils.Empty(ErrMsg)) ErrMsg = "荷主を選択して下さい";
            }
            else
            {
                IsInput = true;
            }
            if (StringUtils.Empty(dousei.Bikou))
            {
            }
            else
            {
                IsInput = true;
            }
            for (int i = 0; i < dousei.DjDouseiCargos.Count; i++)
            {
                DjDouseiCargo douseiCargo = dousei.DjDouseiCargos[i];

                if (i > 0)
                {
                    // 積荷２以降は、品目が選択されていないければ無視する
                    if (douseiCargo.MsCargoID == int.MinValue)
                    {
                        continue;
                    }
                }

                if (douseiCargo.MsCargoID == int.MinValue)
                {
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "を選択して下さい";
                    }
                }
                else
                {
                    IsInput = true;
                }
                if (douseiCargo.Qtty == decimal.MinValue)
                {
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                    }
                }
                else
                {
                    if (NBaseCommon.Number.CheckValue((double)douseiCargo.Qtty, 4, 3) == false)
                    {
                        if (ErrMsg == "")
                        {
                            ErrMsg = "積荷" + (i + 1).ToString() + "の数量を正しく入力して下さい";
                        }
                    }
                    else
                    {
                        IsInput = true;
                    }
                }
                if (douseiCargo.MsDjTaniID == null)
                {
                    if (ErrMsg == "")
                    {
                        ErrMsg = "積荷" + (i + 1).ToString() + "の単位を選択して下さい";
                    }
                }
                else
                {
                    IsInput = true;
                }
            }
            dousei.IsValid = true;

            if (IsInput == true)
            {
                if(ErrMsg != "")
                {
                    dousei.IsValid = false;
                    MessageBox.Show(target + "の" + ErrMsg, "動静予定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return false;
                    return true;
                }
                return true;
            }
            else
            {
                if (StringUtils.Empty(dousei.DjDouseiID) == false)
                {
                    // 実績がない場合、予定情報を削除する
                    if (dousei.ResultNiyakuEnd == DateTime.MinValue)
                        dousei.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;

                    // 積荷情報は削除とする
                    foreach (DjDouseiCargo cargo in dousei.DjDouseiCargos)
                    {
                        cargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
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

            int tumiCount = 0;
            int ageCount = 0;
            foreach(DouseiInfo info in DouseiInfoList)
            {
                DjDousei dousei = null;

                if (info.TumiAge == ModeTumiAgeEnum.TUMI)
                {
                    dousei = GetInstance(info.TumiAge, gcMultiRow1, tumiCount);
                    tumiCount++;
                }
                if (info.TumiAge == ModeTumiAgeEnum.AGE)
                {
                    dousei = GetInstance(info.TumiAge, gcMultiRow2, ageCount);
                    ageCount++;
                }

                string label = info.TumiAge == ModeTumiAgeEnum.TUMI ? "積み" : "揚げ";
                label += (info.No + 1).ToString();
                //if (dousei != null && Validation_積_揚(label, dousei))
                //{
                //    dousei.MsVesselID = vessel.MsVesselID;
                //    dousei.MsKanidouseiInfoShubetuID = info.TumiAge == ModeTumiAgeEnum.TUMI ? MsKanidouseiInfoShubetu.積みID : MsKanidouseiInfoShubetu.揚げID;
                //    ret.Add(dousei);
                //}
                if (dousei != null)
                {
                    if(Validation_積_揚(label, dousei))
                    {
                        dousei.MsVesselID = vessel.MsVesselID;
                        dousei.MsKanidouseiInfoShubetuID = info.TumiAge == ModeTumiAgeEnum.TUMI ? MsKanidouseiInfoShubetu.積みID : MsKanidouseiInfoShubetu.揚げID;
                        ret.Add(dousei);

                        if (dousei.IsValid == false)
                            break;
                    }
                }
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




        private bool Validation(List<DjDousei> douseiInfos)
        {
            List<DjDousei> checkDouseis = null;
            DjDousei tumiDousei = null;
            DjDousei ageDousei = null;


            // 前次航とのチェック

            // 対象：この積み、ひとつ前の揚げ
            tumiDousei = douseiInfos.Where(o => o.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み).OrderBy(o => o.DouseiDate).First();
            for (int i = 1; i <= 5; i++)
            {
                if (ageDousei != null)
                    break;

                DateTime checkDate = tumiDousei.DouseiDate.AddDays(-i);
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    DateTime minDate = DateTime.MinValue;
                    List<DjDousei> douseis = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, tumiDousei.MsVesselID, checkDate);
                    foreach (DjDousei d in douseis)
                    {
                        if (d.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
                        {
                            if (d.DouseiDate > minDate)
                            {
                                minDate = d.DouseiDate;
                                ageDousei = d;
                            }
                        }
                    }
                }
            }
            checkDouseis = new List<DjDousei>();
            checkDouseis.Add(ageDousei);
            checkDouseis.Add(tumiDousei);



            return true;
        }

        //private bool 移動できるか(List<DjDousei> douseis)
        //{
        //    bool ret = true;

        //    var age = douseis[0];
        //    var tumi = douseis[1];

        //    if (age == null)
        //    {
        //        MessageBox.Show($"　前の揚げがない");
        //        return ret;
        //    }

        //    if (tumi == null)
        //    {
        //        MessageBox.Show($"　次の積みがない");
        //        return ret;
        //    }


        //    MessageBox.Show($"　距離確認：{age.BashoName} － {tumi.BashoName}");

        //    var minutes = DouseiProc.MinutesToNextPort(Vessel, age.MsBashoID, tumi.MsBashoID);

        //    TimeSpan s = new TimeSpan(0, (int)minutes, 0);
        //    MessageBox.Show($"移動時間：{s}");


        //    MessageBox.Show($"  揚げ：{age.DouseiDate} - 積み：{tumi.DouseiDate} ");

        //    if (situation == DRAG_SITUATION.TumiPast || situation == DRAG_SITUATION.AgePast)
        //    {
        //        MessageBox.Show($"  揚げ：{age.DouseiDate} - 揚げ+移動時間：{age.DouseiDate.AddMinutes(minutes)} - 移動した積み：{tumi.DouseiDate.AddDays(diff)} ");

        //        if (age.DouseiDate.AddMinutes(minutes) > tumi.DouseiDate.AddDays(diff))
        //        {
        //            MessageBox.Show($"間に合わないのでNG");
        //            ret = false;
        //        }
        //    }
        //    else if (situation == DRAG_SITUATION.TumiFuture || situation == DRAG_SITUATION.AgeFuture)
        //    {
        //        MessageBox.Show($"  揚げ：{age.DouseiDate} - 移動した揚げ：{age.DouseiDate.AddDays(diff)}- 移動した揚げ+移動時間：{age.DouseiDate.AddDays(diff).AddMinutes(minutes)} - 積み：{tumi.DouseiDate.AddDays(diff)} ");

        //        if (age.DouseiDate.AddDays(diff).AddMinutes(minutes) > tumi.DouseiDate)
        //        {
        //            MessageBox.Show($"間に合わないのでNG");
        //            ret = false;
        //        }
        //    }


        //    return ret;
        //}



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
            //douseiJissekiUserControl2Tumi1.ClearDousei();
            //douseiJissekiUserControl2Tumi1.ClearDate();
            //douseiJissekiUserControl2Tumi1.ClearTumini();

            //douseiYoteiUserControl2.ClearDousei();
            //douseiYoteiUserControl2.ClearDate();
            //douseiYoteiUserControl2.ClearTumini();

            //douseiYoteiUserControl3.ClearDousei();
            //douseiYoteiUserControl3.ClearDate();
            //douseiYoteiUserControl3.ClearTumini();

            //douseiYoteiUserControl4.ClearDousei();
            //douseiYoteiUserControl4.ClearDate();
            //douseiYoteiUserControl4.ClearTumini();

            //douseiYoteiUserControl5.ClearDousei();
            //douseiYoteiUserControl5.ClearDate();
            //douseiYoteiUserControl5.ClearTumini();

            //douseiYoteiUserControl6.ClearDousei();
            //douseiYoteiUserControl6.ClearDate();
            //douseiYoteiUserControl6.ClearTumini();

            //douseiYoteiUserControl7.ClearDousei();
            //douseiYoteiUserControl7.ClearDate();
            //douseiYoteiUserControl7.ClearTumini();

            // 新規と同扱いにするので、簡易動静情報はクリア
            KanidouseiInfo = null;

            // 新規と同扱いにするので、動静情報はクリア
            Dousei.DjDouseis = null;

            // 複製
            CopyFlag = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void 動静予定Form3_Shown(object sender, EventArgs e)
        {
            IsFirst = false;
        }





        private void comboBox_船_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vessel = (comboBox_船.SelectedItem as MsVessel);
            if (string.IsNullOrEmpty(vessel.Cargos) == false)
            {
                // 船に設定されている積載可能貨物を対象とする
                var cargos = vessel.Cargos.Split(',').ToList();
                VesselCargo_List = MsCargo_list.Where(o => cargos.Contains(o.MsCargoID.ToString())).ToList();
            }
            else
            {
                // 船に積載可能貨物の設定がない場合、全てとする
                VesselCargo_List = MsCargo_list;
            }
            
            CargoSettingsToVesselChange();
        }


        private void CargoSettingsToVesselChange()
        {
            for (int i = 0; i < 4; i++)
            {
                SetCargoItems(gcMultiRow1, i, VesselCargo_List);
                SetCargoItems(gcMultiRow2, i, VesselCargo_List);
                AddCargoItems(gcMultiRow1, i, DouseiCargo_List);
                AddCargoItems(gcMultiRow2, i, DouseiCargo_List);
            }
        }



        #region Templateのイベント


        private void InitTemplate()
        {
            // 既存のテンプレートの変更
            Template1 tmp = new Template1();

            GcComboBoxCell comboCell_Basho;
            comboCell_Basho = tmp.Row.Cells["gcComboBoxCell_港"] as GcComboBoxCell;
            {
                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsBasho basho in MsBasho_list)
                {
                    dl.Rows.Add(basho.MsBashoId, basho.BashoName);
                }
                dl.AcceptChanges();

                comboCell_Basho.DataSource = dl;

                InitGcCombo(comboCell_Basho);
            }

            GcComboBoxCell comboCell_Kichi;
            comboCell_Kichi = tmp.Row.Cells["gcComboBoxCell_基地"] as GcComboBoxCell;



            GcComboBoxCell comboCell_Dairiten;
            comboCell_Dairiten = tmp.Row.Cells["gcComboBoxCell_代理店"] as GcComboBoxCell;
            {
                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsCustomer customer in MsCustomer_list)
                {
                    if (customer.Is代理店())
                    {
                        dl.Rows.Add(customer.MsCustomerID, customer.CustomerName);
                    }
                }
                dl.AcceptChanges();

                comboCell_Dairiten.DataSource = dl;

                InitGcCombo(comboCell_Dairiten);
            }

            GcComboBoxCell comboCell_Ninushi;
            comboCell_Ninushi = tmp.Row.Cells["gcComboBoxCell_荷主"] as GcComboBoxCell;
            {
                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsCustomer customer in MsCustomer_list)
                {
                    if (customer.Is荷主())
                    {
                        dl.Rows.Add(customer.MsCustomerID, customer.CustomerName);
                    }
                }
                dl.AcceptChanges();

                comboCell_Ninushi.DataSource = dl;

                InitGcCombo(comboCell_Ninushi);
            }

            List<GcComboBoxCell> comboCell_Cargos = new List<GcComboBoxCell>();
            comboCell_Cargos.Add(tmp.Row.Cells["gcComboBoxCell_貨物1"] as GcComboBoxCell);
            comboCell_Cargos.Add(tmp.Row.Cells["gcComboBoxCell_貨物2"] as GcComboBoxCell);
            comboCell_Cargos.Add(tmp.Row.Cells["gcComboBoxCell_貨物3"] as GcComboBoxCell);
            comboCell_Cargos.Add(tmp.Row.Cells["gcComboBoxCell_貨物4"] as GcComboBoxCell);
            comboCell_Cargos.Add(tmp.Row.Cells["gcComboBoxCell_貨物5"] as GcComboBoxCell);
            foreach (GcComboBoxCell combo in comboCell_Cargos)
            {
                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsCargo cargo in MsCargo_list)
                {
                    dl.Rows.Add(cargo.MsCargoID, cargo.CargoName);
                }
                dl.AcceptChanges();

                combo.DataSource = dl;

                InitGcCombo(combo);
            }

            List<GcComboBoxCell> comboCell_Tanis = new List<GcComboBoxCell>();
            comboCell_Tanis.Add(tmp.Row.Cells["gcComboBoxCell_単位1"] as GcComboBoxCell);
            comboCell_Tanis.Add(tmp.Row.Cells["gcComboBoxCell_単位2"] as GcComboBoxCell);
            comboCell_Tanis.Add(tmp.Row.Cells["gcComboBoxCell_単位3"] as GcComboBoxCell);
            comboCell_Tanis.Add(tmp.Row.Cells["gcComboBoxCell_単位4"] as GcComboBoxCell);
            comboCell_Tanis.Add(tmp.Row.Cells["gcComboBoxCell_単位5"] as GcComboBoxCell);

            foreach (GcComboBoxCell combo in comboCell_Tanis)
            {
                InitGcCombo(combo);

                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsDjTani tani in MsDjTani_list)
                {
                    dl.Rows.Add(tani.MsDjTaniID, tani.TaniName);
                }
                dl.AcceptChanges();

                combo.DataSource = dl;
            }



            //var cellNames = new List<string>{ "数量1", "数量2", "数量3", "数量4", "数量5",
            //    "入港時刻", "着桟時刻", "荷役開始時刻", "荷役終了時刻", "離桟時刻", "出港時刻"};

            //foreach (string name in cellNames)
            //{
            //    Cell cell = tmp.Row.Cells[$"maskedTextBoxCell_{name}"];
            //    cell.Style.ImeMode = ImeMode.Off;
            //}




            // MultiRowの設定
            gcMultiRow1.Template = tmp;
            gcMultiRow2.Template = tmp;
        }

        private void InitMultiRow(ModeTumiAgeEnum tumiOrAge, GcMultiRow multiRow)
        {
            multiRow.Rows.Clear();

            multiRow.Rows.Add();
            multiRow.Rows.Add();
            multiRow.Rows.Add();
            multiRow.Rows.Add();

            //コントロールの背景色をうすグレー
            CellStyle style = new CellStyle();
            style.BackColor = Color.WhiteSmoke;

            // テンプレートの背景色
            for (int i = 0; i < multiRow.Rows.Count; i++)
            {
                if (tumiOrAge == ModeTumiAgeEnum.TUMI)
                {
                    multiRow.Rows[i].BackColor = NBaseCommon.Common.ColorTumi;
                    multiRow.Rows[i].Cells["labelCell_Title"].Value = "【荷役：積み】";
                }
                else
                {
                    multiRow.Rows[i].BackColor = NBaseCommon.Common.ColorAge;
                    multiRow.Rows[i].Cells["labelCell_Title"].Value = "【荷役：揚げ】";
                }
                multiRow.Rows[i].Cells["gcComboBoxCell_港"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_基地"].Style = style.Clone();

                multiRow.Rows[i].Cells["dateTimePickerCell_入港日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_入港時刻"].Style = style.Clone();
                multiRow.Rows[i].Cells["dateTimePickerCell_着桟日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_着桟時刻"].Style = style.Clone();
                multiRow.Rows[i].Cells["dateTimePickerCell_荷役開始日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_荷役開始時刻"].Style = style.Clone();
                multiRow.Rows[i].Cells["dateTimePickerCell_荷役終了日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_荷役終了時刻"].Style = style.Clone();
                multiRow.Rows[i].Cells["dateTimePickerCell_離桟日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_離桟時刻"].Style = style.Clone();
                multiRow.Rows[i].Cells["dateTimePickerCell_出港日"].Style = style.Clone();
                multiRow.Rows[i].Cells["maskedTextBoxCell_出港時刻"].Style = style.Clone();

                multiRow.Rows[i].Cells["gcComboBoxCell_代理店"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_荷主"].Style = style.Clone();
                multiRow.Rows[i].Cells["textBoxCell_備考"].Style = style.Clone();

                multiRow.Rows[i].Cells["gcComboBoxCell_貨物1"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcTextBoxCell_数量1"].Style = style.Clone();
                //multiRow.Rows[i].Cells["maskedTextBoxCell_数量1"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_単位1"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_貨物2"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcTextBoxCell_数量2"].Style = style.Clone();
                //multiRow.Rows[i].Cells["maskedTextBoxCell_数量2"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_単位2"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_貨物3"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcTextBoxCell_数量3"].Style = style.Clone();
                //multiRow.Rows[i].Cells["maskedTextBoxCell_数量3"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_単位3"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_貨物4"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcTextBoxCell_数量4"].Style = style.Clone();
                //multiRow.Rows[i].Cells["maskedTextBoxCell_数量4"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_単位4"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_貨物5"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcTextBoxCell_数量5"].Style = style.Clone();
                //multiRow.Rows[i].Cells["maskedTextBoxCell_数量5"].Style = style.Clone();
                multiRow.Rows[i].Cells["gcComboBoxCell_単位5"].Style = style.Clone();

                //数量入力セルの書式
                (multiRow.Rows[i].Cells["gcTextBoxCell_数量1"] as GcTextBoxCell).Format = TextCellFormatStr;
                (multiRow.Rows[i].Cells["gcTextBoxCell_数量2"] as GcTextBoxCell).Format = TextCellFormatStr;
                (multiRow.Rows[i].Cells["gcTextBoxCell_数量3"] as GcTextBoxCell).Format = TextCellFormatStr;
                (multiRow.Rows[i].Cells["gcTextBoxCell_数量4"] as GcTextBoxCell).Format = TextCellFormatStr;
                (multiRow.Rows[i].Cells["gcTextBoxCell_数量5"] as GcTextBoxCell).Format = TextCellFormatStr;


                //コントロール制御
                //ｼﾐｭﾚｰｼｮﾝから呼ばれたときに変更できない項目
                if (FormKind == 1)
                {
                    multiRow.Rows[i].Cells["gcComboBoxCell_港"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_基地"].Enabled = false;

                    multiRow.Rows[i].Cells["gcComboBoxCell_代理店"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_荷主"].Enabled = false;
                    multiRow.Rows[i].Cells["textBoxCell_備考"].Enabled = false;

                    multiRow.Rows[i].Cells["gcComboBoxCell_貨物1"].Enabled = false;
                    multiRow.Rows[i].Cells["gcTextBoxCell_数量1"].Enabled = false;
                    //multiRow.Rows[i].Cells["maskedTextBoxCell_数量1"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_単位1"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_貨物2"].Enabled = false;
                    multiRow.Rows[i].Cells["gcTextBoxCell_数量2"].Enabled = false;
                    //multiRow.Rows[i].Cells["maskedTextBoxCell_数量2"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_単位2"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_貨物3"].Enabled = false;
                    multiRow.Rows[i].Cells["gcTextBoxCell_数量3"].Enabled = false;
                    //multiRow.Rows[i].Cells["maskedTextBoxCell_数量3"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_単位3"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_貨物4"].Enabled = false;
                    multiRow.Rows[i].Cells["gcTextBoxCell_数量4"].Enabled = false;
                    //multiRow.Rows[i].Cells["maskedTextBoxCell_数量4"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_単位4"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_貨物5"].Enabled = false;
                    multiRow.Rows[i].Cells["gcTextBoxCell_数量5"].Enabled = false;
                    //multiRow.Rows[i].Cells["maskedTextBoxCell_数量5"].Enabled = false;
                    multiRow.Rows[i].Cells["gcComboBoxCell_単位5"].Enabled = false;
                }
            }

            multiRow.ClearSelection();
        }

        private void SetRow(GcMultiRow multiRow, PlanResultEnum planOrResult, int rowIndex, DjDousei dousei)
        {
            string bashoId = null;
            string kichiId = null;
            DateTime nyuko = DateTime.MinValue;
            DateTime chakusan = DateTime.MinValue;
            DateTime niyakuStart = DateTime.MinValue;
            DateTime niyakuEnd = DateTime.MinValue;
            DateTime risan = DateTime.MinValue;
            DateTime shukou = DateTime.MinValue;

            string dairitenId = null;
            string ninushiId = null;
            string bikou = null;

            List<DjDouseiCargo> cargos = null;

            if (planOrResult == PlanResultEnum.PLAN)
            {
                bashoId = dousei.MsBashoID;
                kichiId = dousei.MsKichiID;

                nyuko = dousei.PlanNyuko;
                chakusan = dousei.PlanChakusan;
                niyakuStart = dousei.PlanNiyakuStart;
                niyakuEnd = dousei.PlanNiyakuEnd;
                risan = dousei.PlanRisan;
                shukou = dousei.PlanShukou;

                dairitenId = Dousei.DairitenID;
                ninushiId = Dousei.NinushiID;

                bikou = Dousei.Bikou;

                cargos = dousei.DjDouseiCargos;
            }
            else
            {
                if (dousei.ResultNyuko == DateTime.MinValue && string.IsNullOrEmpty(dousei.ResultMsBashoID))
                {
                    bashoId = dousei.MsBashoID;
                }
                else
                {
                    bashoId = dousei.ResultMsBashoID;
                }
                if (dousei.ResultNyuko == DateTime.MinValue && string.IsNullOrEmpty(dousei.ResultMsKichiID))
                {
                    kichiId = dousei.MsKichiID;
                }
                else
                {
                    kichiId = dousei.ResultMsKichiID;
                } 
            }




            multiRow.SetValue(rowIndex, "gcComboBoxCell_港", bashoId);
            multiRow.SetValue(rowIndex, "gcComboBoxCell_基地", kichiId);

            if (nyuko == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_入港日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_入港日", nyuko);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_入港時刻", 時間Format(nyuko));

            if (chakusan == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_着桟日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_着桟日", chakusan);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_着桟時刻", 時間Format(chakusan));


            if (niyakuStart == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役開始日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役開始日", niyakuStart);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_荷役開始時刻", 時間Format(niyakuStart));

            if (niyakuEnd == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役終了日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役終了日", niyakuEnd);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_荷役終了時刻", 時間Format(niyakuEnd));

            if (risan == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_離桟日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_離桟日", risan);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_離桟時刻", 時間Format(risan));

            if (shukou == DateTime.MinValue)
                multiRow.SetValue(rowIndex, "dateTimePickerCell_出港日", null);
            else
                multiRow.SetValue(rowIndex, "dateTimePickerCell_出港日", shukou);

            multiRow.SetValue(rowIndex, "maskedTextBoxCell_出港時刻", 時間Format(shukou));


            multiRow.SetValue(rowIndex, "gcComboBoxCell_代理店", dairitenId);
            multiRow.SetValue(rowIndex, "gcComboBoxCell_荷主", ninushiId);

            multiRow.SetValue(rowIndex, "textBoxCell_備考", bikou);

            int index = 1;
            foreach (DjDouseiCargo douseiCargo in cargos)
            {
                var cargo = MsCargo_list.Where(o => o.MsCargoID == douseiCargo.MsCargoID).FirstOrDefault();
                if (cargo != null)
                {
                    multiRow.SetValue(rowIndex, $"gcComboBoxCell_貨物{index}", douseiCargo.MsCargoID);
                    multiRow.SetValue(rowIndex, $"gcTextBoxCell_数量{index}", douseiCargo.Qtty.ToString());
                    //multiRow.SetValue(rowIndex, $"maskedTextBoxCell_数量{index}", douseiCargo.Qtty.ToString().PadLeft(8, ' '));
                    multiRow.SetValue(rowIndex, $"gcComboBoxCell_単位{index}", douseiCargo.MsDjTaniID);
                }
                index++;
            }

        }

        private string 時間Format(DateTime datetime)
        {
            if (datetime == DateTime.MinValue)
            {
                return "";
            }

            //string TimeStr = datetime.ToShortTimeString();
            //string[] TimeStrArray = TimeStr.Split(':');
            //return (Convert.ToInt16(TimeStrArray[0])).ToString("00") + (Convert.ToInt16(TimeStrArray[1])).ToString("00");
            return datetime.ToString("HH:mm");
        }



        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            gcMultiRow_CellContentButtonClick(gcMultiRow1, e);
        }
        private void gcMultiRow2_CellContentButtonClick(object sender, CellEventArgs e)
        {
            gcMultiRow_CellContentButtonClick(gcMultiRow2, e);
        }
        private void gcMultiRow_CellContentButtonClick(GcMultiRow multiRow, CellEventArgs e)
        {
            Cell currentCell = multiRow.Rows[e.RowIndex].Cells[e.CellIndex];
            int r = e.RowIndex;

            if ((currentCell is ButtonCell) == false)
            {
                return;
            }

            var name = currentCell.Name;


            // クリア
            if (name == $"buttonCell_Clear")
            {
                #region 

                clearing = true;

                multiRow.SetValue(r, "gcComboBoxCell_港", null);
                multiRow.SetValue(r, "gcComboBoxCell_基地", null);

                List<string> kinds = new List<string>() { "入港", "着桟", "荷役開始", "荷役終了", "離桟", "出港" };
                foreach (var kind in kinds)
                {
                    multiRow.SetValue(r, $"dateTimePickerCell_{kind}日", null);
                    multiRow.SetValue(r, $"maskedTextBoxCell_{kind}時刻", "");
                }

                multiRow.SetValue(r, "gcComboBoxCell_代理店", null);
                multiRow.SetValue(r, "gcComboBoxCell_荷主", null);

                multiRow.SetValue(r, "textBoxCell_備考", null);

                for (int i = 1; i <= 5; i++)
                {
                    multiRow.SetValue(r, $"gcComboBoxCell_貨物{i}", null);
                    multiRow.SetValue(r, $"gcTextBoxCell_数量{i}", "");
                    //multiRow.SetValue(r, $"maskedTextBoxCell_数量{i}", "");
                    multiRow.SetValue(r, $"gcComboBoxCell_単位{i}", null);
                }

                clearing = false;

                #endregion
            }

            // 計算
            if (name.Contains("buttonCell_Calc"))
            {
                var kind = name.Replace("buttonCell_Calc", "");

                //時間の計算
                SetDateTime(kind, multiRow, e.RowIndex,0);


                MultiRowWithRow wk_mr = new MultiRowWithRow();
                wk_mr.MultiRow = multiRow;
                wk_mr.Row = r;
                次の動静の時間計算(wk_mr);

            }
        }

        public class MultiRowWithRow
        {
            public GcMultiRow MultiRow;
            public int Row;
        }

        private MultiRowWithRow 次の動静の時間計算(MultiRowWithRow mr)
        {

            //次の動静の時間の計算
            //GcMultiRow wk_mrow = null;
            string nextBashoId = "";
            //int wk_r = -1;

            MultiRowWithRow wk_mr = new MultiRowWithRow();
            wk_mr.MultiRow = null;
            wk_mr.Row = -1;

            if (mr.MultiRow == gcMultiRow1)
            {
                for (int i = mr.Row + 1; i <= 3; i++)
                {
                    nextBashoId = (string)gcMultiRow1.GetValue(i, "gcComboBoxCell_港");
                    if (nextBashoId != null && nextBashoId != "")
                    {
                        wk_mr.Row = i;
                        wk_mr.MultiRow = gcMultiRow1;
                        break;
                    }
                }
            }

            if (nextBashoId == null || nextBashoId == "")
            {
                int start_r = 0;
                if (mr.MultiRow == gcMultiRow2)
                {
                    start_r = mr.Row + 1;
                }
                for (int i = start_r; i <= 3; i++)
                {
                    nextBashoId = (string)gcMultiRow2.GetValue(i, "gcComboBoxCell_港");
                    if (nextBashoId != null && nextBashoId != "")
                    {
                        wk_mr.Row = i;
                        wk_mr.MultiRow = gcMultiRow2;
                        break;
                    }
                }
            }

            //次の動静が見つかったら入港日時計算
            if (wk_mr.Row != -1 && wk_mr.MultiRow != null)
            {
                入港日時の計算_セット(wk_mr.MultiRow, wk_mr.Row);

                if (SetDateTime("入港", wk_mr.MultiRow, wk_mr.Row, 1))
                {
                    return 次の動静の時間計算(wk_mr);
                }
                else
                { 
                    return new MultiRowWithRow();
                }
            }
            else
            {
                return new MultiRowWithRow();
            }
        }



        private void gcMultiRow1_CellValueChanged(object sender, CellEventArgs e)
        {
            gcMultiRow_CellValueChanged(gcMultiRow1, e);
        }
        private void gcMultiRow2_CellValueChanged(object sender, CellEventArgs e)
        {
            gcMultiRow_CellValueChanged(gcMultiRow2, e);
        }
        private void gcMultiRow_CellValueChanged(GcMultiRow multiRow, CellEventArgs e)
        {
            if (clearing)
                return;
            
            Cell currentCell = multiRow.Rows[e.RowIndex].Cells[e.CellIndex];
            int r = e.RowIndex;

            if (currentCell is GcComboBoxCell && currentCell.Name == "gcComboBoxCell_港")
            {
                #region 港決定時、基地Combo再設定、動静情報２つ目以降の場合、入港日時設定

                List<MsKichi> list = MsKichi_list;

                var val = currentCell.Value;
                MsBasho b = MsBasho_list.Where(obj => obj.MsBashoId == (string)val).FirstOrDefault();
                if (b != null && MsKichi_list.Any(obj => obj.MsBashoID == b.MsBashoId))
                {
                    list = MsKichi_list.Where(obj => obj.MsBashoID == b.MsBashoId).ToList();
                }

                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(string));
                dl.Columns.Add("Name", typeof(string));
                foreach (MsKichi k in list)
                {
                    dl.Rows.Add(k.MsKichiId, k.KichiName);
                }
                dl.AcceptChanges();

                GcComboBoxCell comboCell_Kichi = multiRow.Rows[r].Cells["gcComboBoxCell_基地"] as GcComboBoxCell;
                comboCell_Kichi.DataSource = null;
                comboCell_Kichi.DataSource = dl;
                InitGcCombo(comboCell_Kichi);

                multiRow.SetValue(r, "gcComboBoxCell_基地", null);


                // 港決定時、ひとつ前の動静との関係から入港日時を決定する
                // gcMultiRow1=積み　gcMultiRow2=揚げ
                if (r != 0 || multiRow == gcMultiRow2)
                {
                    入港日時の計算_セット(multiRow, r);

                    //DateTime shukou = DateTime.MinValue;
                    //string prevBashoId = null;

                    //if (r == 0)
                    //{
                    //    // 揚げの１つ目の場合、積みの最後を見つける
                    //    for(int i = 3; i >= 0; i--)
                    //    {
                    //        shukou = Get日時(gcMultiRow1, i, "出港");
                    //        if (shukou != DateTime.MinValue)
                    //        {
                    //            prevBashoId = (string)gcMultiRow1.GetValue(i, "gcComboBoxCell_港");
                    //            break;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    // 積み or 揚げ の２つ目以降
                    //    shukou = Get日時(multiRow, r - 1, "出港");
                    //    prevBashoId = (string)multiRow.GetValue(r - 1, "gcComboBoxCell_港");
                    //}

                    //if (shukou != DateTime.MinValue)
                    //{
                    //    SetNyukoDateTime(multiRow, r, c, prevBashoId, shukou);
                    //}
                }

                #endregion
            }

            if (currentCell is GcComboBoxCell && currentCell.Name == "gcComboBoxCell_基地")
            {
                #region 基地決定時、積荷Combo再構築

                List<MsCargo> targetCargoList = new List<MsCargo>();

                List<MsKichi> list = MsKichi_list;

                var val = currentCell.Value;
                MsKichi k = MsKichi_list.Where(obj => obj.MsKichiId == (string)val).FirstOrDefault();
                if (k != null && string.IsNullOrEmpty(k.Cargos) == false)
                {
                    // 基地に設定されている積載可能貨物を対象とする

                    var cargoids = new List<int>();
                    var cargoVals = k.Cargos.Split(',');

                    foreach (var cargoVal in cargoVals)
                    {
                        var vals = cargoVal.Split(':');
                        cargoids.Add(int.Parse(vals[0]));
                    }
                    targetCargoList = VesselCargo_List.Where(o => cargoids.Contains(o.MsCargoID)).ToList();
                }
                else
                {
                    // 基地に積載可能貨物の設定がない場合、船に積めるもの全てとする
                    targetCargoList = VesselCargo_List;
                }

                SetCargoItems(multiRow, r, targetCargoList);
                AddCargoItems(multiRow, r, DouseiCargo_List);

                #endregion
            }
        }

        private void 入港日時の計算_セット(GcMultiRow multiRow, int r)
        {
            DateTime shukou = DateTime.MinValue;
            string prevBashoId = null;

            if (r == 0)
            {
                // 揚げの１つ目の場合、積みの最後を見つける
                for (int i = 3; i >= 0; i--)
                {
                    shukou = Get日時(gcMultiRow1, i, "出港");
                    if (shukou != DateTime.MinValue)
                    {
                        prevBashoId = (string)gcMultiRow1.GetValue(i, "gcComboBoxCell_港");
                        break;
                    }
                }
            }
            else
            {
                // 積み or 揚げ の２つ目以降
                shukou = Get日時(multiRow, r - 1, "出港");
                prevBashoId = (string)multiRow.GetValue(r - 1, "gcComboBoxCell_港");
            }

            if (shukou != DateTime.MinValue)
            {
                SetNyukoDateTime(multiRow, r, prevBashoId, shukou);
            }
        }

        private void InitGcCombo(GcComboBoxCell combo)
        {
            combo.ListColumns.Clear();
            combo.ListColumns.Add("");
            combo.ListColumns.Add("");
            combo.ListColumns[0].DataPropertyName = "ID";
            combo.ListColumns[1].DataPropertyName = "Name";
            combo.ListColumns[0].Width = 0;
            combo.ListColumns[1].Width = combo.Width;
            combo.ValueSubItemIndex = 0;
            combo.TextSubItemIndex = 1;
        }

        private void gcMultiRow1_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            //Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];
            //if (currentCell is TextBoxCell && currentCell.Name == "textBoxCell_数量1")
            //{
            //    var val_bef = (string)currentCell.Value;
            //    var val = (string)currentCell.EditedFormattedValue;

            //    string setval = "";
            //    if (val_bef != null)
            //    {
            //        setval = val_bef;
            //    }
            //    decimal res_d;
            //    int res_i;
            //    if (!decimal.TryParse(val, out res_d))// && !int.TryParse(val, out res_i))
            //    {
            //        gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex].Value = setval;
            //    }
            //    else
            //    {
                    
            //    }
            //}

            //Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];
            //if (currentCell is MaskedTextBoxCell && currentCell.Name == "maskedTextBoxCell_入港時刻")
            //{
            //    var val = currentCell.EditedFormattedValue;

            //    //var val2 = ((string)val).Replace(":", "").Replace("_", "");
            //    //if (val2.Length == 4)

            //    if (TimeCheck((string)val))
            //        SetDateTime("入港", gcMultiRow1, e.RowIndex);
            //}
        }
        private void gcMultiRow2_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            return;//miho

            //Cell currentCell = gcMultiRow2.Rows[e.RowIndex].Cells[e.CellIndex];
            //if (currentCell is MaskedTextBoxCell && currentCell.Name == "maskedTextBoxCell_入港時刻")
            //{
            //    var val = currentCell.EditedFormattedValue;

            //    //var val2 = ((string)val).Replace(":", "").Replace("_", "");
            //    //if (val2.Length == 4)

            //    if (TimeCheck((string)val))
            //        SetDateTime("入港", gcMultiRow2, e.RowIndex);
            //}
        }

        private bool 日時が存在するか(GcMultiRow multiRow, int rowIndex, string cellname)
        {
            var vd = multiRow.GetValue(rowIndex, $"dateTimePickerCell_{cellname}日");
            var vt = multiRow.GetValue(rowIndex, $"maskedTextBoxCell_{cellname}時刻");
            if (vd == null || vt == null) return false;

            return true;
        }

        /// <summary>
        /// 日時を計算して表示
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="multiRow"></param>
        /// <param name="rowIndex"></param>
        /// <param name="callmode"></param>
        private bool SetDateTime(string kind, GcMultiRow multiRow, int rowIndex, int callmode)
        {
            var kichiId = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_基地");
            var kichi = MsKichi_list.Where(o => o.MsKichiId == kichiId).FirstOrDefault();
            if (kichi == null)
                return false;


            var nyuko = DateTime.MinValue;
            var chakusan = DateTime.MinValue;
            var niyakuStart = DateTime.MinValue;
            var niyakuEnd = DateTime.MinValue;
            var risan = DateTime.MinValue;
            var shukou = DateTime.MinValue;

            //日がない場合は抜ける
            if (multiRow.GetValue(rowIndex, $"dateTimePickerCell_{kind}日") == null) return false;

            Cell cell = multiRow.Rows[rowIndex].Cells.Where(o => o.Name == $"maskedTextBoxCell_{kind}時刻").FirstOrDefault();
            try
            {
                if (kind == "入港")
                {
                    var d = multiRow.GetValue(rowIndex, $"dateTimePickerCell_入港日");
                    if (string.IsNullOrEmpty((string)cell.EditedFormattedValue))
                    {
                        var t = multiRow.GetValue(rowIndex, $"maskedTextBoxCell_入港時刻");

                        var dateTimeStr = ((DateTime)d).ToShortDateString() + " " + t;
                        nyuko = DateTime.Parse(dateTimeStr);
                    }
                    else
                    {
                        var t = multiRow.GetEditedFormattedValue(rowIndex, $"maskedTextBoxCell_入港時刻");

                        var dateTimeStr = ((DateTime)d).ToShortDateString() + " " + t;
                        nyuko = DateTime.Parse(dateTimeStr);

                    }
                }
                else
                {
                    var d = multiRow.GetValue(rowIndex, $"dateTimePickerCell_{kind}日");
                    var t = multiRow.GetValue(rowIndex, $"maskedTextBoxCell_{kind}時刻");
                    var dateTimeStr = ((DateTime)d).ToShortDateString() + " " + t;

                    if (kind == "着桟")
                    {
                        chakusan = DateTime.Parse(dateTimeStr);
                    }
                    if (kind == "荷役開始")
                    {
                        niyakuStart = DateTime.Parse(dateTimeStr);
                    }
                    if (kind == "荷役終了")
                    {
                        niyakuEnd = DateTime.Parse(dateTimeStr);
                    }
                    if (kind == "離桟")
                    {
                        risan = DateTime.Parse(dateTimeStr);
                    }
                    if (kind == "出港")
                    {
                        shukou = DateTime.Parse(dateTimeStr);
                    }
                }
            }
            catch
            {
                MessageBox.Show($"{kind}の日時が正しくありません");
                return false;
            }

            //次の動静の計算をしているときは値が存在するときだけ変更していく
            if (callmode==1)
            {
                if (!日時が存在するか( multiRow,  rowIndex, "着桟")) return false;
            }

            if (nyuko != DateTime.MinValue && kichi.ForNyukoToChakusan > 0)
            {
                chakusan = nyuko.AddMinutes((double)kichi.ForNyukoToChakusan);

                var chakusanHm = decimal.Parse(chakusan.ToString("HHmm"));
                if (kichi.AvailableForChakusanFrom > 0)
                {
                    if (chakusanHm < kichi.AvailableForChakusanFrom)
                    {
                        chakusan = ConvertDate(chakusan.Date, kichi.AvailableForChakusanFrom);
                    }
                }
                if (kichi.AvailableForChakusanTo > 0)
                {
                    if (chakusanHm > kichi.AvailableForChakusanTo)
                    {
                        chakusan = ConvertDate(chakusan.Date.AddDays(1), kichi.AvailableForChakusanFrom);
                    }
                }

                multiRow.SetValue(rowIndex, "dateTimePickerCell_着桟日", chakusan);
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_着桟時刻", 時間Format(chakusan));
            }

            //次の動静の計算をしているときは値が存在するときだけ変更していく
            if (callmode == 1)
            {
                if (!日時が存在するか(multiRow, rowIndex, "荷役開始")) return false;
            }
            if (chakusan != DateTime.MinValue && kichi.ForChakusanToNiyaku > 0)
            {
                niyakuStart = chakusan.AddMinutes((double)kichi.ForChakusanToNiyaku);

                var niyakuStartHm = decimal.Parse(niyakuStart.ToString("HHmm"));
                if (kichi.AvailableForNiyakuFrom > 0)
                {
                    if (niyakuStartHm < kichi.AvailableForNiyakuFrom)
                    {
                        niyakuStart = ConvertDate(niyakuStart.Date, kichi.AvailableForNiyakuFrom);
                    }
                }
                if (kichi.AvailableForNiyakuTo > 0)
                {
                    if (niyakuStartHm > kichi.AvailableForNiyakuTo)
                    {
                        niyakuStart = ConvertDate(niyakuStart.Date.AddDays(1), kichi.AvailableForNiyakuFrom);
                    }
                }

                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役開始日", niyakuStart);
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_荷役開始時刻", 時間Format(niyakuStart));
            }

            //次の動静の計算をしているときは値が存在するときだけ変更していく
            if (callmode == 1)
            {
                if (!日時が存在するか(multiRow, rowIndex, "荷役終了")) return false;
            }
            if (niyakuStart != DateTime.MinValue)
            {
                decimal niyakuTime = 0; // 積荷と基地から算出する

                {
                    for (int i = 1; i <= 5; i++)
                    {
                        decimal cargoID = 0;
                        //decimal qtty = 0;
                        decimal qtty = decimal.MinValue;


                        var c = multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}");
                        var q = multiRow.GetValue(rowIndex, $"gcTextBoxCell_数量{i}");
                        //var q =multiRow.GetValue(rowIndex, $"maskedTextBoxCell_数量{i}");
                        var t = multiRow.GetDisplayText(rowIndex, $"gcComboBoxCell_単位{i}");

                        if (c != null && t != null & (q != null && decimal.TryParse((string)q, out qtty)))
                        {
                            cargoID = (int)c;
                            if ((string)t == "KL")
                            {
                                qtty = qtty * 1000;
                            }
                        }

                        //try
                        //{
                        //    if (multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}") != null)
                        //    {
                        //        cargoID = (int)multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}");
                        //        var val = (string)multiRow.GetValue(rowIndex, $"maskedTextBoxCell_数量{i}");
                        //        val = val.Replace(" ", "");
                        //        if (val[0] == '.' || val[val.Length - 1] == '.')
                        //        {
                        //            val = val.Replace(".", "");
                        //        }

                        //        qtty = Convert.ToDecimal(val);


                        //        //var tani = (string)multiRow.GetValue(rowIndex, $"gcComboBoxCell_単位{i}");
                        //        var tani = (string)multiRow.GetDisplayText(rowIndex, $"gcComboBoxCell_単位{i}");
                        //        MessageBox.Show($"数量[{qtty}]、単位[{tani}]");
                        //    }
                        //}
                        //catch
                        //{
                        //    cargoID = 0;
                        //    qtty = decimal.MinValue;
                        //}

                        if (cargoID > 0 && qtty > 0)
                        {
                            if (string.IsNullOrEmpty(kichi.Cargos) == false)
                            {
                                var cargoVals = kichi.Cargos.Split(',');

                                foreach (var cargoVal in cargoVals)
                                {
                                    var vals = cargoVal.Split(':');

                                    if (vals[0] == cargoID.ToString() && vals[1].Length > 0)
                                    {
                                        niyakuTime += (qtty / int.Parse(vals[1]));

                                        MessageBox.Show($"荷役時間[{(qtty / int.Parse(vals[1]))}] = 数量[{qtty}]、積み時間[{int.Parse(vals[1])}]");
                                    }
                                }

                            }

                        }

                    }
                }


                niyakuEnd = niyakuStart.AddMinutes((double)niyakuTime);

                multiRow.SetValue(rowIndex, "dateTimePickerCell_荷役終了日", niyakuEnd);
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_荷役終了時刻", 時間Format(niyakuEnd));
            }

            //次の動静の計算をしているときは値が存在するときだけ変更していく
            if (callmode == 1)
            {
                if (!日時が存在するか(multiRow, rowIndex, "離桟")) return false;
            }
            if (niyakuEnd != DateTime.MinValue && kichi.ForNiyakuToRisan > 0)
            {
                risan = niyakuEnd.AddMinutes((double)kichi.ForNiyakuToRisan);

                var risanHm = decimal.Parse(risan.ToString("HHmm"));
                if (kichi.AvailableForRisanFrom > 0)
                {
                    if (risanHm < kichi.AvailableForRisanFrom)
                    {
                        risan = ConvertDate(risan.Date, kichi.AvailableForRisanFrom);
                    }
                }
                if (kichi.AvailableForRisanTo > 0)
                {
                    if (risanHm > kichi.AvailableForRisanTo)
                    {
                        risan = ConvertDate(risan.Date.AddDays(1), kichi.AvailableForRisanFrom);
                    }
                }

                multiRow.SetValue(rowIndex, "dateTimePickerCell_離桟日", risan);
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_離桟時刻", 時間Format(risan));
            }

            //次の動静の計算をしているときは値が存在するときだけ変更していく
            if (callmode == 1)
            {
                if (!日時が存在するか(multiRow, rowIndex, "出港")) return false;
            }
            if (risan != DateTime.MinValue && kichi.ForRisanToShukou > 0)
            {
                shukou = risan.AddMinutes((double)kichi.ForRisanToShukou);

                var shukouHm = decimal.Parse(shukou.ToString("HHmm"));
                if (kichi.AvailableForShukouFrom > 0)
                {
                    if (shukouHm < kichi.AvailableForShukouFrom)
                    {
                        shukou = ConvertDate(shukou.Date, kichi.AvailableForShukouFrom);
                    }
                }
                if (kichi.AvailableForRisanTo > 0)
                {
                    if (shukouHm > kichi.AvailableForShukouTo)
                    {
                        shukou = ConvertDate(shukou.Date.AddDays(1), kichi.AvailableForShukouFrom);
                    }
                }

                multiRow.SetValue(rowIndex, "dateTimePickerCell_出港日", shukou);
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_出港時刻", 時間Format(shukou));
            }

            return true;
        }




        //private void SetNyukoDateTime(GcMultiRow multiRow, int rowIndex, int cellIndex, string prevBashoId, DateTime shukou)
        private void SetNyukoDateTime(GcMultiRow multiRow, int rowIndex,  string prevBashoId, DateTime shukou)
        {
            //// 速力（設定がない場合、何もしない）
            //var vessel = comboBox_船.SelectedItem as MsVessel;
            //var knot = vessel.Knot;
            //if (knot == 0)
            //    return;

            //// 
            //var val = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_港");
            //MsBasho b = MsBasho_list.Where(obj => obj.MsBashoId == (string)val).FirstOrDefault();

            //// 
            //List<MsBashoKyori> bashoKyoriList = null;
            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    bashoKyoriList = serviceClient.MsBashoKyori_GetRecordsByKyori1Kyori2(NBaseCommon.Common.LoginUser, prevBashoId, b.MsBashoId);
            //    bashoKyoriList = bashoKyoriList.Where(o => (o.MsBashoID1 == prevBashoId && o.BashoName2 == b.MsBashoId) || (o.MsBashoID1 == prevBashoId && o.BashoName2 == b.MsBashoId)).ToList();
            //}

            //// 港間の距離設定がない場合、何もしない
            //if (bashoKyoriList.Count == 0)
            //    return;

            //// 距離（Km)
            //var kyori = bashoKyoriList[0].Kyori;

            //// マイルに換算する
            //var mile = kyori * 1000 / 1852;

            //// 時間
            //var minutes = mile / (double)knot * 60;

            //multiRow.SetValue(rowIndex, "dateTimePickerCell_入港日", shukou.AddMinutes(minutes));
            //multiRow.SetValue(rowIndex, "maskedTextBoxCell_入港時刻", 時間Format(shukou.AddMinutes(minutes)));


            var vessel = comboBox_船.SelectedItem as MsVessel;
            var val = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_港");
            var minutes = DouseiProc.MinutesToNextPort(vessel, prevBashoId, val);
            if (minutes > 0)
            {
                multiRow.SetValue(rowIndex, "dateTimePickerCell_入港日", shukou.AddMinutes(minutes));
                multiRow.SetValue(rowIndex, "maskedTextBoxCell_入港時刻", 時間Format(shukou.AddMinutes(minutes)));
            }
        }


        private DateTime ConvertDate(DateTime date, decimal hhmm)
        {
            var hh = int.Parse(hhmm.ToString("0000").Substring(0, 2));
            var mm = int.Parse(hhmm.ToString("0000").Substring(2, 2));


            var ret = date.AddHours(hh).AddMinutes(mm);

            return ret;
        }




        private DjDousei GetInstance(ModeTumiAgeEnum tumiOrAge, GcMultiRow multiRow, int rowIndex)
        {
            DjDousei dousei = new DjDousei();
            if (DouseiInfoList.Any(o => o.TumiAge == tumiOrAge && o.No == rowIndex))
            {
                var org = DouseiInfoList.Where(o => o.TumiAge == tumiOrAge && o.No == rowIndex).First().Dousei;

                if (org != null)
                    dousei = Clone(org);
            }

            try
            {
                dousei.MsBashoID = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_港");
                dousei.MsKichiID = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_基地");

                dousei.PlanNyuko = Get日時(multiRow, rowIndex, "入港");
                dousei.PlanChakusan = Get日時(multiRow, rowIndex, "着桟");
                dousei.PlanNiyakuStart = Get日時(multiRow, rowIndex, "荷役開始");
                dousei.PlanNiyakuEnd = Get日時(multiRow, rowIndex, "荷役終了");
                dousei.PlanRisan = Get日時(multiRow, rowIndex, "離桟");
                dousei.PlanShukou = Get日時(multiRow, rowIndex, "出港");

                dousei.DouseiDate = dousei.PlanNiyakuStart;　// 予定の日時は、荷役開始日時

                dousei.DairitenID = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_代理店");
                dousei.NinushiID = (string)multiRow.GetValue(rowIndex, "gcComboBoxCell_荷主");

                dousei.Bikou = (string)multiRow.GetValue(rowIndex, "textBoxCell_備考");

                for (int i = 1; i <= 5; i++)
                {
                    DjDouseiCargo douseiCargo = new DjDouseiCargo();
                    {
                        if (dousei.DjDouseiCargos.Count >= i)
                        {
                            douseiCargo = dousei.DjDouseiCargos[i - 1];
                        }
                        else
                        {
                            douseiCargo = new DjDouseiCargo();
                        }
                    }

                    var c = multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}");
                    var q = multiRow.GetValue(rowIndex, $"gcTextBoxCell_数量{i}");
                    //var q = multiRow.GetValue(rowIndex, $"maskedTextBoxCell_数量{i}");
                    var t = multiRow.GetValue(rowIndex, $"gcComboBoxCell_単位{i}");
                    decimal qo = 0;
                    if (c != null && t != null && (q != null && decimal.TryParse((string)q, out qo)))
                    {
                        douseiCargo.MsCargoID = (int)c;
                        douseiCargo.Qtty = qo;
                        douseiCargo.LineNo = i.ToString();
                        douseiCargo.MsDjTaniID = (string)t;
                    }
                    else
                    {
                        douseiCargo.MsCargoID = 0;
                        douseiCargo.Qtty = decimal.MinValue;
                        douseiCargo.MsDjTaniID = null;
                    }
                    //try
                    //{
                    //    if (multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}") != null && multiRow.GetValue(rowIndex, $"maskedTextBoxCell_数量{i}") != null)
                    //    {
                    //        douseiCargo.MsCargoID = (int)multiRow.GetValue(rowIndex, $"gcComboBoxCell_貨物{i}");
                    //        douseiCargo.Qtty = Convert.ToDecimal((string)multiRow.GetValue(rowIndex, $"maskedTextBoxCell_数量{i}"));
                    //        douseiCargo.LineNo = i.ToString();
                    //    }
                    //    else
                    //    {
                    //        douseiCargo.MsCargoID = 0;
                    //        douseiCargo.Qtty = decimal.MinValue;
                    //    }
                    //}
                    //catch
                    //{
                    //    douseiCargo.MsCargoID = 0;
                    //    douseiCargo.Qtty = decimal.MinValue;
                    //}
                    //douseiCargo.MsDjTaniID = (string)multiRow.GetValue(rowIndex, $"gcComboBoxCell_単位{i}");


                    if (douseiCargo.MsCargoID > 0 && string.IsNullOrEmpty(douseiCargo.DjDouseiCargoID))
                    {
                        dousei.DjDouseiCargos.Add(douseiCargo);
                    }
                    else
                    {
                        if (douseiCargo.MsCargoID <= 0)
                        {
                            douseiCargo.DeleteFlag = NBaseCommon.Common.DeleteFlag_削除;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return dousei;
        }

        private DateTime Get日時(GcMultiRow multiRow, int rowIndex, string columnStr)
        {
            var d = multiRow.GetValue(rowIndex, $"dateTimePickerCell_{columnStr}日");
            var t = multiRow.GetValue(rowIndex, $"maskedTextBoxCell_{columnStr}時刻");

            if (d != null && t != null && TimeCheck((string)t))
            {
                string dateTimeStr = ((DateTime)d).ToShortDateString() + " " + t;
                return DateTime.Parse(dateTimeStr);
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        private bool TimeCheck(string timeStr)
        {
            try
            {
                int h = int.Parse(timeStr.Substring(0, 2));
                if (h >= 24)
                    return false;

                var tmp = timeStr;
                if (timeStr.IndexOf(':') > 0)
                {
                    tmp = timeStr.Replace(":", "");
                }
                int m = int.Parse(tmp.Substring(2, 2));
                if (m >= 60)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public DjDousei Clone(DjDousei src)
        {
            DjDousei ret = new DjDousei();

            ret.DjDouseiID = src.DjDouseiID;
            ret.MsKanidouseiInfoShubetuID = src.MsKanidouseiInfoShubetuID;
            ret.KikanNo = src.KikanNo;
            ret.KanidouseiInfoShubetuName = src.KanidouseiInfoShubetuName;

            ret.MsVesselID = src.MsVesselID;
            ret.MsVesselNo = src.MsVesselNo;
            ret.NaveCode = src.NaveCode;
            ret.VoyageNo = src.VoyageNo;
            ret.KikanRenkeiFlag = src.KikanRenkeiFlag;
            ret.MsBashoID = src.MsBashoID;
            ret.BashoName = src.BashoName;
            ret.MsKichiID = src.MsKichiID;
            ret.MsKichiNO = src.MsKichiNO;
            ret.KichiName = src.KichiName;
            ret.DouseiDate = src.DouseiDate;
            ret.PlanNyuko = src.PlanNyuko;
            ret.PlanChakusan = src.PlanChakusan;
            ret.PlanNiyakuStart = src.PlanNiyakuStart;
            ret.PlanNiyakuEnd = src.PlanNiyakuEnd;
            ret.PlanRisan = src.PlanRisan;
            ret.PlanShukou = src.PlanShukou;
            ret.ResultNyuko = src.ResultNyuko;
            ret.ResultChakusan = src.ResultChakusan;
            ret.ResultNiyakuStart = src.ResultNiyakuStart;
            ret.ResultNiyakuEnd = src.ResultNiyakuEnd;
            ret.ResultRisan = src.ResultRisan;
            ret.ResultShukou = src.ResultShukou;
            ret.RecordDateTime = src.RecordDateTime;
            ret.KikanVoyageNo = src.KikanVoyageNo;
            ret.KomaNo = src.KomaNo;
            ret.ResultMsBashoID = src.ResultMsBashoID;
            ret.ResultMsBashoNO = src.ResultMsBashoNO;
            ret.ResultMsKichiID = src.ResultMsKichiID;
            ret.ResultMsKichiNO = src.ResultMsKichiNO;
            ret.ResultKichiName = src.ResultKichiName;
            ret.DairitenID = src.DairitenID;
            ret.NinushiID = src.NinushiID;
            ret.Bikou = src.Bikou;
            ret.ResultDairitenID = src.ResultDairitenID;
            ret.ResultNinushiID = src.ResultNinushiID;
            ret.ResultBikou = src.ResultBikou;

            ret.DeleteFlag = src.DeleteFlag;
            ret.SendFlag = src.SendFlag;
            ret.VesselID = src.VesselID;
            ret.DataNo = src.DataNo;
            ret.UserKey = src.UserKey;

            ret.RenewDate = src.RenewDate;
            ret.RenewUserID = src.RenewUserID;
            ret.Ts = src.Ts;


            ret.DjDouseiCargos = new List<DjDouseiCargo>();
            foreach(DjDouseiCargo srcCargo in src.DjDouseiCargos)
            {
                DjDouseiCargo dstCargo = new DjDouseiCargo();

                dstCargo.DjDouseiCargoID = srcCargo.DjDouseiCargoID;
                dstCargo.DjDouseiID = srcCargo.DjDouseiID;
                dstCargo.MsCargoID = srcCargo.MsCargoID;
                dstCargo.MsCargoNo = srcCargo.MsCargoNo;
                dstCargo.MsCargoName = srcCargo.MsCargoName;
                dstCargo.Qtty = srcCargo.Qtty;
                dstCargo.LineNo = srcCargo.LineNo;
                dstCargo.MsDjTaniID = srcCargo.MsDjTaniID;
                dstCargo.PlanResultFlag = srcCargo.PlanResultFlag;

                dstCargo.DeleteFlag = srcCargo.DeleteFlag;
                dstCargo.SendFlag = srcCargo.SendFlag;
                dstCargo.VesselID = srcCargo.VesselID;
                dstCargo.DataNo = srcCargo.DataNo;
                dstCargo.UserKey = srcCargo.UserKey;
                dstCargo.RenewDate = srcCargo.RenewDate;
                dstCargo.RenewUserID = srcCargo.RenewUserID;
                dstCargo.Ts = srcCargo.Ts;


                ret.DjDouseiCargos.Add(dstCargo);
            }

            ret.ResultDjDouseiCargos = new List<DjDouseiCargo>();
            foreach (DjDouseiCargo srcCargo in src.ResultDjDouseiCargos)
            {
                DjDouseiCargo dstCargo = new DjDouseiCargo();

                dstCargo.DjDouseiCargoID = srcCargo.DjDouseiCargoID;
                dstCargo.DjDouseiID = srcCargo.DjDouseiID;
                dstCargo.MsCargoID = srcCargo.MsCargoID;
                dstCargo.MsCargoNo = srcCargo.MsCargoNo;
                dstCargo.MsCargoName = srcCargo.MsCargoName;
                dstCargo.Qtty = srcCargo.Qtty;
                dstCargo.LineNo = srcCargo.LineNo;
                dstCargo.MsDjTaniID = srcCargo.MsDjTaniID;
                dstCargo.PlanResultFlag = srcCargo.PlanResultFlag;

                dstCargo.DeleteFlag = srcCargo.DeleteFlag;
                dstCargo.SendFlag = srcCargo.SendFlag;
                dstCargo.VesselID = srcCargo.VesselID;
                dstCargo.DataNo = srcCargo.DataNo;
                dstCargo.UserKey = srcCargo.UserKey;
                dstCargo.RenewDate = srcCargo.RenewDate;
                dstCargo.RenewUserID = srcCargo.RenewUserID;
                dstCargo.Ts = srcCargo.Ts;


                ret.ResultDjDouseiCargos.Add(dstCargo);
            }

            return ret;
        }



        private void SetCargoItems(GcMultiRow multiRow, int index, IEnumerable<MsCargo> cargos)
        {
            if (multiRow.Rows.Count == 0)
                return;

            List<GcComboBoxCell> comboCell_Cargos = new List<GcComboBoxCell>();
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物1"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物2"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物3"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物4"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物5"] as GcComboBoxCell);
            foreach (GcComboBoxCell combo in comboCell_Cargos)
            {
                // コンボボックス型セル用データの作成
                DataTable dl = new DataTable("LIST");
                dl.Columns.Add("ID", typeof(int));
                dl.Columns.Add("Name", typeof(string));

                foreach (MsCargo cargo in cargos)
                {
                    dl.Rows.Add(cargo.MsCargoID, cargo.CargoName);
                }
                dl.AcceptChanges();

                combo.DataSource = null;
                combo.DataSource = dl;

                InitGcCombo(combo);
            }
        }
        private void AddCargoItems(GcMultiRow multiRow, int index, IEnumerable<MsCargo> cargos)
        {
            if (multiRow.Rows.Count == 0)
                return;

            List<GcComboBoxCell> comboCell_Cargos = new List<GcComboBoxCell>();
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物1"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物2"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物3"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物4"] as GcComboBoxCell);
            comboCell_Cargos.Add(multiRow.Rows[index].Cells["gcComboBoxCell_貨物5"] as GcComboBoxCell);
            foreach (GcComboBoxCell combo in comboCell_Cargos)
            {
                // コンボボックス型セル用データの作成
                DataTable dl = combo.DataSource as DataTable;

                if (cargos != null)
                { 
                    foreach (MsCargo cargo in cargos)
                    {
                        if (VesselCargo_List.Contains(cargo) == false)
                            dl.Rows.Add(cargo.MsCargoID, cargo.CargoName);
                    }
                }

                dl.AcceptChanges();
            }
        }



        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            ChangeImeMode(gcMultiRow1, e.RowIndex, e.CellIndex);
        }
        private void gcMultiRow2_CellEnter(object sender, CellEventArgs e)
        {
            ChangeImeMode(gcMultiRow2, e.RowIndex, e.CellIndex);
        }

        private void ChangeImeMode(GcMultiRow multiRow, int rowIndex, int cellIndex)
        {
            var cellNames = new List<string>{ "数量1", "数量2", "数量3", "数量4", "数量5",
                "入港時刻", "着桟時刻", "荷役開始時刻", "荷役終了時刻", "離桟時刻", "出港時刻"};

            Cell cell = multiRow.Rows[rowIndex].Cells[cellIndex];
            if (cell.Name == "gcComboBoxCell_港")
            {
                cell.Style.ImeMode = ImeMode.On;
            }
            else if (cell is MaskedTextBoxCell)
            {
                foreach (string name in cellNames)
                {
                    if (cell.Name == $"maskedTextBoxCell_{name}")
                    {
                        cell.Style.ImeMode = ImeMode.Off;
                        break;
                    }
                }
            }
        }



        private void gcMultiRow1_CellLeave(object sender, CellEventArgs e)
        {
            LeaveChangeImeMode(gcMultiRow1, e.RowIndex, e.CellIndex);
        }
        private void gcMultiRow2_CellLeave(object sender, CellEventArgs e)
        {
            LeaveChangeImeMode(gcMultiRow2, e.RowIndex, e.CellIndex);
        }
        private void LeaveChangeImeMode(GcMultiRow multiRow, int rowIndex, int cellIndex)
        {
            Cell currentCell = multiRow.Rows[rowIndex].Cells[cellIndex];
            if (currentCell.Name == "gcComboBoxCell_港")
            {
                foreach (Cell cell in multiRow.Rows[rowIndex].Cells)
                {
                    if (cell is MaskedTextBoxCell)
                    {
                        cell.Style.ImeMode = ImeMode.Off;
                    }
                }
            }
        }



        private void gcMultiRow1_RowEnter(object sender, CellEventArgs e)
        {
            Row currentRow = gcMultiRow1.Rows[e.RowIndex];
            foreach (Cell cell in currentRow.Cells)
            {
                if (cell.Name == "gcComboBoxCell_港")
                {
                    cell.Style.ImeMode = ImeMode.On;
                }
                else if(cell is MaskedTextBoxCell)
                {
                    cell.Style.ImeMode = ImeMode.Off;
                }
            }
        }






        #endregion

        
    }
}
