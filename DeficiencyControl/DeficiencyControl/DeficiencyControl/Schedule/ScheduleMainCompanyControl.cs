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
    /// スケジュールメイン画面、会社タブ
    /// </summary>
    /// <remarks>各タブ似たようなことをやっているが、拡張を考慮してそれぞれ作ることとする。</remarks>
    public partial class ScheduleMainCompanyControl : BaseScheduleMainTab
    {
        public ScheduleMainCompanyControl()
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
        public class ScheduleMainCompanyControlData
        {
            /// <summary>
            /// 年度
            /// </summary>
            public MsYear Year = null;

            /// <summary>
            /// 検索元リスト
            /// </summary>
            public List<DcScheduleCompany> SrcList = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleMainCompanyControlData FData = null;
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        private void InitDisplayControl()
        {
            
        }


        /// <summary>
        /// 予定の追加
        /// </summary>
        /// <param name="comp">元データ 新規=null</param>
        private void AddSchedule(DcScheduleCompany comp)
        {
            FlowLayoutPanel fpane = this.flowLayoutPanelCompany;

            //コントロール作成と初期化
            ScheduleInputControlCompany con = new ScheduleInputControlCompany();
            {
                con.InitControl(comp);
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
            ComLogic.DeleteChildControl(this.flowLayoutPanelCompany);
        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispScheduleData()
        {
            //全体クリア
            FlowLayoutPanel fpane = this.flowLayoutPanelCompany;
            fpane.Controls.Clear();

            //追加
            foreach (DcScheduleCompany comp in this.FData.SrcList)
            {
                this.AddSchedule(comp);
            }
        }



        /// <summary>
        /// データの検索 WaiteStateせよ
        /// </summary>
        private void Search()
        {

            MsYear year = this.FData.Year;

            //検索
            ScheduleCompanySearchData sdata = new ScheduleCompanySearchData();
            sdata.estimate_date_start = year.start_date;
            sdata.estimate_date_end = year.end_date;

            sdata.inspection_date_start = year.start_date;
            sdata.inspection_date_end = year.end_date;

            sdata.expiry_date_start = year.start_date;
            sdata.expiry_date_end = year.end_date;

            this.FData.SrcList = SvcManager.SvcMana.DcScheduleCompany_GetRecordsBySearchData(sdata);

            //表示
            this.DispScheduleData();

        }


        /// <summary>
        /// 更新一覧を取得する 一括更新に備えてデータ取得部は一応分離 + publicで
        /// </summary>
        /// <returns></returns>
        public List<DcScheduleCompany> GetUpdateList()
        {
            List<DcScheduleCompany> anslist = new List<DcScheduleCompany>();

            //現在のデータ取得
            List<ScheduleInputControlCompany> comlist = this.GetInputControlList<ScheduleInputControlCompany>(this.flowLayoutPanelCompany);
            foreach (ScheduleInputControlCompany com in comlist)
            {
                //入力の取得
                DcScheduleCompany ans = com.GetInputData();
                anslist.Add(ans);
            }


            //削除データをADD
            if (this.FData.SrcList != null)
            {
                anslist = ComLogic.CreateDBUpdateList<DcScheduleCompany>(this.FData.SrcList, anslist);
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
                    DcMes.ShowMessage(this.ParentForm, EMessageID.MI_53);
                    this.ResetError();
                    return false;
                }


                //更新データ取得
                List<DcScheduleCompany> datalist = this.GetUpdateList();
                
                //更新処理
                
                bool ret = SvcManager.SvcMana.DcScheduleCompany_InsertUpdateList(datalist, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("DcScheduleCompany_InsertUpdateList FALSE");
                }


                //再検索
                this.Search();


            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "UpdateData");
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_54);
                return false;

            }

            //登録成功
            DcMes.ShowMessage(this.ParentForm, EMessageID.MI_66);

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
                List<ScheduleInputControlCompany> comlist = this.GetInputControlList<ScheduleInputControlCompany>(this.flowLayoutPanelCompany);
                foreach (ScheduleInputControlCompany com in comlist)
                {
                    bool ret = com.CheckError(chcol);
                    if (ret == false)
                    {
                        this.ErList.Add(com);
                    }
                }



                //データ無しは論外                
                if (comlist.Count <= 0)
                {
                    this.ErList.Add(this.flowLayoutPanelCompany);
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
            this.flowLayoutPanelCompany.Controls.Clear();
            this.FData.SrcList = new List<DcScheduleCompany>();            
        }


        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleMainCompanyControlData();

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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleMainCompanyControl_Load(object sender, EventArgs e)
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
            int delc = ComLogic.CalcuDeleteChildControlCount(this.flowLayoutPanelCompany);
            if (delc <= 0)
            {
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_69);
                return;
            }


            //確認
            DialogResult dret = DcMes.ShowMessage(this.ParentForm, EMessageID.MI_52, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
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
                    this.UpdateDelegateProc(EScheduleCategory.会社);
                }
            }
        }
    }
}
