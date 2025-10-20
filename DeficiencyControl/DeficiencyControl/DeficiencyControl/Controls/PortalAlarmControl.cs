using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Grid;

namespace DeficiencyControl.Controls
{
    /// <summary>
    /// ポータル画面アラームコントロール
    /// </summary>
    public partial class PortalAlarmControl : BaseControl
    {
        public PortalAlarmControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// このコントロールのデータ
        /// </summary>
        public class PortalAlarmControlData
        {
            /// <summary>
            /// データ保持
            /// </summary>
            public List<DcSchedulePlan> DataList = new List<DcSchedulePlan>();

        }


        /// <summary>
        /// フォームデータ
        /// </summary>
        public PortalAlarmControlData FData = null;


        /// <summary>
        /// グリッド管理
        /// </summary>
        private PortalAlarmGrid Grid = null;
        //---------------------------------------------------------------------------------------------------------------------------------
        //以下は長く可読性に劣るようなら、ロジッククラスへと分離すること
        /// <summary>
        /// 検索コントロールの初期化
        /// </summary>
        /// <returns></returns>
        private bool InitDisplayControl()
        {
            //船
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);


            //種別
            ControlItemCreator.CreateMsScheduleKind(EScheduleCategory.予定実績, this.comboBoxScheduleKind, true);


            //詳細
            this.ReCreateKindDetailControl();

            return true;
        }

        /// <summary>
        /// 詳細コントロールの再作成
        /// </summary>
        protected virtual void ReCreateKindDetailControl()
        {
            //選択種別の取得
            MsScheduleKind kind = this.comboBoxScheduleKind.SelectedItem as MsScheduleKind;
            if (kind == null)
            {
                this.comboBoxScheduleKindDetail.Items.Clear();
                return;
            }

            //再作成
            ControlItemCreator.CreateMsScheduleKindDetail(kind.schedule_kind_id, this.comboBoxScheduleKindDetail, true);

        }

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        /// <returns></returns>
        private bool ClearSearch()
        {
            //種別
            this.comboBoxScheduleKind.SelectedIndex = 0;

            //船
            this.singleLineComboVessel.Text = "";


            //種別詳細
            this.comboBoxScheduleKind.SelectedIndex = 0;

            //グリッドクリア
            this.FData.DataList = new List<DcSchedulePlan>();
            this.Grid.DispData(this.FData.DataList);


            this.dataCountControl1.DataCount = 0;

            return true;
        }


        /// <summary>
        /// 検索条件の作成
        /// </summary>
        /// <returns></returns>
        private SchedulePlanSearchData CreateSearchData()
        {
            SchedulePlanSearchData ans = new SchedulePlanSearchData();

            //Vessel
            {
                MsVessel ves = this.singleLineComboVessel.SelectedItem as MsVessel;
                if (ves != null)
                {
                    ans.ms_vessel_id = ves.ms_vessel_id;
                }
            }

            //種別
            {
                MsScheduleKind kind = this.comboBoxScheduleKind.SelectedItem as MsScheduleKind;
                if (kind != null)
                {
                    ans.schedule_kind_id = kind.schedule_kind_id;
                }

            }


            //詳細
            {
                MsScheduleKindDetail kide = this.comboBoxScheduleKindDetail.SelectedItem as MsScheduleKindDetail;
                if (kide != null)
                {
                    ans.schedule_kind_detail_id = kide.schedule_kind_detail_id;
                }
            }

            //実績が入っていないものだけ
            ans.InspectionEnabled = false;


            return ans;
        }


        /// <summary>
        /// 検索 waitstateせよ
        /// </summary>
        /// <returns></returns>
        public bool Search()
        {
            //ここの検索は、会社、その他も検索したいとなったらDcSchedulePlanでなく専用のクラスでラップせよ
            SchedulePlanSearchData sdata = this.CreateSearchData();

            //データの検索
            this.FData.DataList = SvcManager.SvcMana.DcSchedulePlan_GetRecordsBySearchData(sdata);

            //有効期限が古いもの順に
            this.FData.DataList = this.FData.DataList.OrderBy(x => x.expiry_date).ToList();

            //再描画
            this.Grid.DispData(this.FData.DataList);


            //件数
            this.dataCountControl1.DataCount = this.dataGridViewAlarm.RowCount;


            return true;
        }

 

        /// <summary>
        /// アラームから詳細画面の起動
        /// </summary>
        private void SetupDetail()
        {
            //保留
        }

        

        ///=================================================================================================================================
        ///=================================================================================================================================
        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            //データ作成
            this.FData = new PortalAlarmControlData();


            //グリッドの初期化
            this.Grid = new PortalAlarmGrid(this.dataGridViewAlarm);


            //コントロール初期化
            this.InitDisplayControl();


            //初期選択
            this.ClearSearch();


            //検索を行う
            this.Search();


            return true;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortalAlarmControl_Load(object sender, EventArgs e)
        {
            //
        }

        /// <summary>
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonSearch_Click");

            //検索
            using(WaitingState es = new WaitingState(this.ParentForm))
            {
                this.Search();
            }
        }

        /// <summary>
        /// クリアボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClear_Click");

            this.ClearSearch();
        }

        /// <summary>
        /// セルが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAlarm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DcLog.WriteLog(this, "dataGridViewAlarm_CellDoubleClick");

            //詳細画面の起動
            //this.SetupDetail();

        }

        /// <summary>
        /// スケジュール種別が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScheduleKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReCreateKindDetailControl();
        }
    }
}

