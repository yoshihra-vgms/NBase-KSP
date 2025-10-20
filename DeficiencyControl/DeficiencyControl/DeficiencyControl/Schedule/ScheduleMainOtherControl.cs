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
    /// スケジュールメイン画面 その他タブ
    /// </summary>
    public partial class ScheduleMainOtherControl : BaseScheduleMainTab
    {
        public ScheduleMainOtherControl()
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
        /// これのデータ
        /// </summary>
        public class ScheduleMainOtherControlData
        {
            /// <summary>
            /// 年度
            /// </summary>
            public MsYear Year = null;

            /// <summary>
            /// 検索元リスト
            /// </summary>
            public List<DcScheduleOther> SrcList = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleMainOtherControlData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        private void InitDisplayControl()
        {

        }


        /// <summary>
        /// 予定の追加
        /// </summary>
        /// <param name="oth">元データ 新規=null</param>
        private void AddSchedule(DcScheduleOther oth)
        {
            FlowLayoutPanel fpane = this.flowLayoutPanelOther;

            //コントロール作成と初期化
            ScheduleInputControlOther con = new ScheduleInputControlOther();
            {
                con.InitControl(oth);
            }

            int sno = 0;

            //ADD
            fpane.Controls.Add(con);
            fpane.Controls.SetChildIndex(con, sno);

            con.TabIndex = 0;
        }

        /// <summary>
        /// 会社の削除
        /// </summary>
        private void DeleteSchedule()
        {
            ComLogic.DeleteChildControl(this.flowLayoutPanelOther);
        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispScheduleData()
        {
            //全体クリア
            FlowLayoutPanel fpane = this.flowLayoutPanelOther;
            fpane.Controls.Clear();

            //追加
            foreach (DcScheduleOther src in this.FData.SrcList)
            {
                this.AddSchedule(src);
            }
        }


        /// <summary>
        /// データの検索 WaiteStateせよ
        /// </summary>
        private void Search()
        {

            MsYear year = this.FData.Year;

            //検索
            ScheduleOtherSearchData sdata = new ScheduleOtherSearchData();
            sdata.estimate_date_start = year.start_date;
            sdata.estimate_date_end = year.end_date;

            sdata.inspection_date_start = year.start_date;
            sdata.inspection_date_end = year.end_date;

            sdata.expiry_date_start = year.start_date;
            sdata.expiry_date_end = year.end_date;

            this.FData.SrcList = SvcManager.SvcMana.DcScheduleOther_GetRecordsBySearchData(sdata);

            //表示
            this.DispScheduleData();

        }

        /// <summary>
        /// 更新一覧を取得する 一括更新に備えてデータ取得部は一応分離 + publicで
        /// </summary>
        /// <returns></returns>
        public List<DcScheduleOther> GetUpdateList()
        {
            List<DcScheduleOther> anslist = new List<DcScheduleOther>();

            //現在のデータ取得
            List<ScheduleInputControlOther> othlist = this.GetInputControlList<ScheduleInputControlOther>(this.flowLayoutPanelOther);
            foreach (ScheduleInputControlOther oth in othlist)
            {
                //入力の取得
                DcScheduleOther ans = oth.GetInputData();

                ans.DataColor = Color.Orange;
                anslist.Add(ans);
            }


            //削除データをADD
            if (this.FData.SrcList != null)
            {
                anslist = ComLogic.CreateDBUpdateList<DcScheduleOther>(this.FData.SrcList, anslist);
            }

            return anslist;
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
                    DcMes.ShowMessage(this.ParentForm, EMessageID.MI_56);
                    this.ResetError();
                    return false;
                }


                //更新データ取得
                List<DcScheduleOther> datalist = this.GetUpdateList();

                //更新処理

                bool ret = SvcManager.SvcMana.DcScheduleOther_InsertUpdateList(datalist, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("DcScheduleOther_InsertUpdateList FALSE");
                }


                //再検索
                this.Search();


            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "UpdateData");
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_57);
                return true;
            }

            //登録成功
            DcMes.ShowMessage(this.ParentForm, EMessageID.MI_67);

            return true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                List<ScheduleInputControlOther> othlist = this.GetInputControlList<ScheduleInputControlOther>(this.flowLayoutPanelOther);
                foreach (ScheduleInputControlOther oth in othlist)
                {
                    bool ret = oth.CheckError(chcol);
                    if (ret == false)
                    {
                        this.ErList.Add(oth);
                    }
                }



                //データ無しは論外                
                if (othlist.Count <= 0)
                {
                    this.ErList.Add(this.flowLayoutPanelOther);
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
        /// 表示のクリア
        /// </summary>
        private void Clear()
        {
            this.flowLayoutPanelOther.Controls.Clear();
            this.FData.SrcList = new List<DcScheduleOther>();
        }


        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleMainOtherControlData();

            //入力の取得
            InitData idata = inputdata as InitData;
            this.FData.Year = idata.Year;

            //コントロールを作成
            this.InitDisplayControl();

            //クリア
            this.Clear();

            //検索
            this.Search();


            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleMainOtherControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonAdd_Click");

            this.AddSchedule(null);
        }

        /// <summary>
        /// 削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDelete_Click");


            //削除対象件数の確認
            int delc = ComLogic.CalcuDeleteChildControlCount(this.flowLayoutPanelOther);
            if (delc <= 0)
            {
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_70);
                return;
            }



            //確認
            DialogResult dret = DcMes.ShowMessage(this.ParentForm, EMessageID.MI_55, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != DialogResult.Yes)
            {
                return;
            }

            this.DeleteSchedule();
        }


        /// <summary>
        /// 更新ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");

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
                    this.UpdateDelegateProc(EScheduleCategory.その他);
                }
            }
        }
    }
}
