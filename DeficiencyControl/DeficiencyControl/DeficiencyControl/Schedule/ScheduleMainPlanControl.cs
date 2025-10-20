using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

using CIsl.DB.WingDAC;
using DcCommon;


namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュールメイン画面、予定実績タブコントロール
    /// </summary>
    public partial class ScheduleMainPlanControl : BaseScheduleMainTab
    {
        public ScheduleMainPlanControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// これの入力データ
        /// </summary>
        public class InitData
        {
            public MsYear Year = null;
        }

        /// <summary>
        /// これのデータクラス
        /// </summary>
        public class ScheduleMainPlanControlData
        {
            /// <summary>
            /// 年度
            /// </summary>
            public MsYear Year = null;

            /// <summary>
            /// 現在の選択船
            /// </summary>
            public MsVessel Vessel = null;


            /// <summary>
            /// 検索元リスト
            /// </summary>
            public List<DcSchedulePlan> SrcList = null;

            /// <summary>
            /// 現在の選択船のスケジュール有効可否情報 [schedule_kind_id, 種別に有効な詳細]
            /// </summary>
            public Dictionary<int, ScheduleVesselEnabledData> DetailEnabledDic = new Dictionary<int, ScheduleVesselEnabledData>();

            

        }
        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleMainPlanControlData FData = null;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        private void InitDisplayControl()
        {
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);
        }


        /// <summary>
        /// 検索条件の取得 必要に応じて専用検索クラスにすること
        /// </summary>
        /// <returns></returns>
        private MsVessel GetSearchData()
        {
            this.ErList = new List<Control>();
            try
            {
                //検索対象取得
                MsVessel ves = this.singleLineComboVessel.SelectedItem as MsVessel;
                if (ves == null)
                {
                    this.ErList.Add(this.singleLineComboVessel);                                        
                }

                if (this.ErList.Count <= 0)
                {
                    return ves;
                }

                throw new Exception("Input Error");
            }
            catch (Exception e)
            {

                this.DispError();
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_47);
                return null;
            }
            finally
            {
                this.ResetError();
            }
        }

        /// <summary>
        /// 予定の追加
        /// </summary>
        /// <param name="plan">元データ 新規=null</param>
        /// <returns>ADDしたコントロール</returns>
        private ScheduleInputControlPlan AddPlan(DcSchedulePlan plan)
        {
            FlowLayoutPanel fpane = this.flowLayoutPanelControl;

            //コントロール作成と初期化
            ScheduleInputControlPlan con = new ScheduleInputControlPlan();
            {
                ScheduleInputControlPlan.InitData idata = new ScheduleInputControlPlan.InitData();
                idata.Data = plan;
                idata.DetailEnabledDic = this.FData.DetailEnabledDic;

                con.InitControl(idata);
            }

            int sno = 0;         

            //ADD
            fpane.Controls.Add(con);
            fpane.Controls.SetChildIndex(con, sno);

            con.TabIndex = 0;

            return con;
        }


        /// <summary>
        /// 予定実績の削除
        /// </summary>
        private void DeletePlan()
        {
            ComLogic.DeleteChildControl(this.flowLayoutPanelControl);
        }


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispPlanData()
        {
            //全体クリア
            FlowLayoutPanel fpane = this.flowLayoutPanelControl;
            fpane.Controls.Clear();

            //追加
            foreach (DcSchedulePlan plan in this.FData.SrcList)
            {
                this.AddPlan(plan);
            }
        }

        

        /// <summary>
        /// データの検索 WaiteStateせよ
        /// </summary>
        private bool Search()
        {
            //条件の取得
            this.FData.Vessel = this.GetSearchData();
            if (this.FData.Vessel == null)
            {
                return false;
            }



            MsVessel ves = this.FData.Vessel;
            MsYear year = this.FData.Year;

            //検索
            SchedulePlanSearchData sdata = new SchedulePlanSearchData();
            sdata.estimate_date_start = year.start_date;
            sdata.estimate_date_end = year.end_date;

            sdata.inspection_date_start = year.start_date;
            sdata.inspection_date_end = year.end_date;

            sdata.expiry_date_start = year.start_date;
            sdata.expiry_date_end = year.end_date;

            sdata.ms_vessel_id = ves.ms_vessel_id;

            //検索
            this.FData.SrcList = SvcManager.SvcMana.DcSchedulePlan_GetRecordsBySearchData(sdata);

            //この検索した船に見合う種別有効可否データを取得し加工する
            List<MsVesselScheduleKindDetailEnable> enalist = SvcManager.SvcMana.MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID(ves.ms_vessel_id);            
            this.FData.DetailEnabledDic = ScheduleVesselEnabledData.CreateVesselData(enalist);

            //表示
            this.DispPlanData();

            return true;

        }

  
        /// <summary>
        /// 更新一覧を取得する 一括更新に備えてデータ取得部は一応分離 + publicで
        /// </summary>
        /// <returns></returns>
        public List<DcSchedulePlan> GetUpdateList()
        {
            List<DcSchedulePlan> anslist = new List<DcSchedulePlan>();

            //現在のデータ取得
            List<ScheduleInputControlPlan> pclist = this.GetInputControlList < ScheduleInputControlPlan>(this.flowLayoutPanelControl);
            foreach (ScheduleInputControlPlan pc in pclist)
            {
                //入力の取得
                DcSchedulePlan ans = pc.GetInputData(this.FData.Vessel.ms_vessel_id);
                anslist.Add(ans);
            }
            

            //削除データをADD
            if (this.FData.SrcList != null)
            {
                anslist = ComLogic.CreateDBUpdateList<DcSchedulePlan>(this.FData.SrcList, anslist);
            }

            return anslist;
        }



        /// <summary>
        /// 今回、実績が新たに追記されたデータを一式で取得する
        /// </summary>
        /// <returns></returns>
        private List<DcSchedulePlan> GetChangeInspectionControl()
        {
            List<DcSchedulePlan> anslist = new List<DcSchedulePlan>();
            
            //現在の一覧を取得
            List<ScheduleInputControlPlan> pclist = this.GetInputControlList < ScheduleInputControlPlan>(this.flowLayoutPanelControl);

            //新たに実績がついたデータか否かをチェックする
            var n = from f in pclist where f.CheckChangeInspection() == true select f.GetInputData(this.FData.Vessel.ms_vessel_id);
            anslist = n.ToList();


            return anslist;
            
        }


        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            this.ErList = new List<Control>();

            try
            {
                //子供のチェック
                List<ScheduleInputControlPlan> pclist = this.GetInputControlList<ScheduleInputControlPlan>(this.flowLayoutPanelControl);
                foreach (ScheduleInputControlPlan pc in pclist)
                {
                    bool ret = pc.CheckError(chcol);
                    if (ret == false)
                    {
                        this.ErList.Add(pc);
                    }
                }



                //データ無しは論外
                List<ScheduleInputControlPlan> ilist = this.GetInputControlList<ScheduleInputControlPlan>(this.flowLayoutPanelControl);
                if (ilist.Count <= 0)
                {
                    this.ErList.Add(this.flowLayoutPanelControl);
                }


                if (this.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputError");


            }
            catch (Exception e)
            {
                this.DispError();
            }


            return false;
            
        }

        /// <summary>
        /// データの更新 WaiteStateせよ
        /// </summary>
        private bool UpdateData()
        {
            try
            {
                //エラーチェック
                bool ckret = this.CheckError(true);
                if (ckret == false)
                {
                    DcMes.ShowMessage(this.ParentForm, EMessageID.MI_49);
                    this.ResetError();
                    return false;
                }
                

                //更新データ取得
                List<DcSchedulePlan> datalist = this.GetUpdateList();

                //実績が新たについたもの一覧を取得する
                List<DcSchedulePlan> piclist = this.GetChangeInspectionControl();

                //更新処理
                bool ret = SvcManager.SvcMana.DcSchedulePlan_InsertUpdateList(datalist, DcGlobal.Global.LoginMsUser);
                if(ret == false)
                {
                    throw new Exception("DcSchedulePlan_InsertUpdateList FALSE");
                }
               

                //再検索
                this.Search();

                //今回更新されたらその情報をADDする
                foreach (DcSchedulePlan pic in piclist)
                {
                    ScheduleInputControlPlan pl = this.AddPlan(null);
                    pl.DispScheduleKind(pic.schedule_kind_id, pic.schedule_kind_detail_id);
                }


            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "UpdateData");
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_50);

                return false;
            }

            //登録成功
            DcMes.ShowMessage(this.ParentForm, EMessageID.MI_65);


            return true;
        }

        /// <summary>
        /// 表示のクリア
        /// </summary>
        private void ClearPlan()
        {
            this.flowLayoutPanelControl.Controls.Clear();
            this.FData.SrcList = new List<DcSchedulePlan>();
            this.FData.Vessel = null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleMainPlanControlData();

            //入力の取得
            InitData idata = inputdata as InitData;            
            this.FData.Year = idata.Year;

            //コントロールを作成
            this.InitDisplayControl();

            //クリア
            this.singleLineComboVessel.Text = "";
            this.ClearPlan();


            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleMainPlanControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            using (WaitingState es = new WaitingState(this.ParentForm))
            {
                this.Search();
            }
        }

        /// <summary>
        /// アイテム追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {

            //船が選択されていない場合、船の検索を行う
            if (this.FData.Vessel == null)
            {
                //DcMes.ShowMessage(this.ParentForm, EMessageID.MI_51);
                //return;
                using (WaitingState es = new WaitingState(this.ParentForm))
                {
                    bool ret = this.Search();
                    if (ret == false)
                    {
                        return;
                    }

                }
            }

            this.AddPlan(null);

        }

        /// <summary>
        /// 削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //削除対象件数の確認
            int delc = ComLogic.CalcuDeleteChildControlCount(this.flowLayoutPanelControl);
            if (delc <= 0)
            {
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_68);
                return;
            }



            //確認
            DialogResult dret = DcMes.ShowMessage(this.ParentForm, EMessageID.MI_48, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != DialogResult.Yes)
            {
                return;
            }

            this.DeletePlan();
        }

        /// <summary>
        /// 更新ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (WaitingState es = new WaitingState(this.ParentForm))
            {
                bool ret = this.UpdateData();
                if (ret == false)
                {
                    return;
                }

                if (this.UpdateDelegateProc != null)
                {
                    //更新通知
                    this.UpdateDelegateProc(EScheduleCategory.予定実績);
                }
            }

        }

        /// <summary>
        /// 船の選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboVessel_selected(object sender, EventArgs e)
        {
            this.ClearPlan();
        }

        /// <summary>
        /// 船のクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboVessel_Cleared(object sender, EventArgs e)
        {
            this.ClearPlan();
        }
    }
}
