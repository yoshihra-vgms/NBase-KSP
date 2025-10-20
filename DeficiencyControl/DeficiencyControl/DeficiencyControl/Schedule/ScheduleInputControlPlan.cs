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
using DeficiencyControl.Controls;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュール予定実績入力コントロール
    /// </summary>
    public partial class ScheduleInputControlPlan : BaseScheduleInputControl
    {
        public ScheduleInputControlPlan() : base(EScheduleCategory.予定実績)
        {
            InitializeComponent();
        }

        /// <summary>
        /// このクラスの初期化データ
        /// </summary>
        public class InitData
        {
            public DcSchedulePlan Data = null;

            public Dictionary<int, ScheduleVesselEnabledData> DetailEnabledDic = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class ScheduleInputControlPlanData
        {
            /// <summary>
            /// 元データ。新規=null
            /// </summary>
            public DcSchedulePlan SrcData = null;

            /// <summary>
            /// 詳細情報
            /// </summary>
            public Dictionary<int, ScheduleVesselEnabledData> DetailEnabledDic = new Dictionary<int, ScheduleVesselEnabledData>();
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleInputControlPlanData FData = null;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            //基底部の表示
            this.DispBaseData(this.FData.SrcData);
        }

        /// <summary>
        /// コントロールの作成
        /// </summary>
        protected override void InitDisplayControl()
        {
            //親の作成
            List<MsScheduleKind> klist= null;

            var n = from f in this.FData.DetailEnabledDic.Values select f.Kind;
            klist = n.ToList();

            ControlItemCreator.CreateComboBox<MsScheduleKind>(this.comboBoxScheduleKind, klist, true);
        }


        /// <summary>
        /// 詳細コントロールの再作成
        /// </summary>
        protected override void ReCreateKindDetailControl()
        {
            //選択種別の取得
            MsScheduleKind kind = this.comboBoxScheduleKind.SelectedItem as MsScheduleKind;
            if (kind == null)
            {
                this.comboBoxScheduleKindDetail.Items.Clear();
                return;
            }

            ControlItemCreator.CreateComboBox<MsScheduleKindDetail>(this.comboBoxScheduleKindDetail, this.FData.DetailEnabledDic[kind.schedule_kind_id].DetailList, true);
        }
        //----------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// エラーのチェック
        /// </summary>
        /// <param name="chcol">色変更可否</param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      //this.textBoxAccident,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));
                this.ErList.AddRange(this.GetBaseErrorControl());

                


                //エラーがないなら終わり
                if (this.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputErrorException");

            }
            catch (Exception e)
            {
                //エラーの表示を行う
                if (chcol == true)
                {
                    this.DispError();
                }

            }

            return false;
        }
        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <param name="ms_vessel_id"></param>
        /// <returns></returns>
        public DcSchedulePlan GetInputData(decimal ms_vessel_id)
        {

            DcSchedulePlan ans = new DcSchedulePlan();
            if (this.FData.SrcData != null)
            {
                ans = (DcSchedulePlan)this.FData.SrcData.Clone();
            }

            //基底データ取得
            ans = this.GetBaseInputData<DcSchedulePlan>(ans);

            //設定            
            ans.ms_vessel_id = ms_vessel_id;

            return ans;

        }

        
        /// <summary>
        /// データ
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleInputControlPlanData();

            InitData idata = inputdata as InitData;

            this.FData.DetailEnabledDic = idata.DetailEnabledDic;
            this.FData.SrcData = idata.Data;


            //画面初期化
            this.InitDisplayControl();


            if (this.FData.SrcData == null)
            {
                //新規
            }
            else
            {
                //更新
                this.DispData();
            }

            return true;
        }


        /// <summary>
        /// このコントロールが、新たにCheckがついたデータか否かを判定する  true=新たに実績となった。
        /// </summary>
        /// <returns></returns>
        public bool CheckChangeInspection()
        {
            //新規では変わったもなにもないため、
            if (this.FData.SrcData == null)
            {
                return false;
            }

            //実績済みだったなら変わりようが無いため
            if (this.FData.SrcData.inspection_date != DcSchedulePlan.EDate)
            {
                return false;
            }

            //----------------------------------------------------------------------------
            //ここまで着たら、元データは実績なし。今回、実績チェックがついているかを判定する
            if (this.dateTimePickerInspectionDate.Checked == false)
            {
                return false;
            }

            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleInputControlPlan_Load(object sender, EventArgs e)
        {

        }
    }
}
