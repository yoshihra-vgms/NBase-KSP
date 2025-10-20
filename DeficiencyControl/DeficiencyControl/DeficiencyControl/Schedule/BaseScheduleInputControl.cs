using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
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

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュール入力基底コントロール
    /// </summary>
    public partial class BaseScheduleInputControl : BaseChildItemControl
    {
        public BaseScheduleInputControl()
        {
            InitializeComponent();
        }
        public BaseScheduleInputControl(EScheduleCategory cate) 
        {
            InitializeComponent();

            this.Category = cate;
        }

        /// <summary>
        /// これのカテゴリ
        /// </summary>
        public EScheduleCategory Category = EScheduleCategory.MAX;


        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="cate">今回のカテゴリ</param>
        protected virtual void InitDisplayControl()
        {
            //種別
            ControlItemCreator.CreateMsScheduleKind(this.Category, this.comboBoxScheduleKind, true);

            //詳細・・・選択にあわせて作成
            this.ReCreateKindDetailControl();
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
        /// 基底部データ表示
        /// </summary>
        /// <param name="data"></param>
        protected void DispBaseData(DcSchedule data)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //種別
            MsScheduleKind ki = db.GetMsScheduleKind(data.schedule_kind_id);
            if (ki != null)
            {
                this.comboBoxScheduleKind.SelectedItem = ki;
                //this.ReCreateKindDetailControl();
            }

            //種別詳細
            MsScheduleKindDetail de = db.GetMsScheduleKindDetail(data.schedule_kind_detail_id);
            if (de != null)
            {
                this.comboBoxScheduleKindDetail.SelectedItem = de;

            }

            //予定日
            this.dateTimePickerEstimateDate.Value = data.estimate_date;
            
            //実績日
            this.dateTimePickerInspectionDate.Checked = false;
            if (data.inspection_date != DcSchedule.EDate)
            {
                this.dateTimePickerInspectionDate.Checked = true;
                this.dateTimePickerInspectionDate.Value = data.inspection_date;
            }

            //有効期限
            this.dateTimePickerExpiryDate.Checked = false;
            if (data.expiry_date != DcSchedule.EDate)
            {
                this.dateTimePickerExpiryDate.Checked = true;
                this.dateTimePickerExpiryDate.Value = data.expiry_date;
            }

            //実績メモ
            this.textBoxRecordMemo.Text = data.record_memo;
        }


        /// <summary>
        /// スケジュール種別の表示
        /// </summary>
        /// <param name="schedule_kind_id">表示種別ID</param>
        /// <param name="schedule_kind_detail_id">表示種別詳細ID</param>
        public void DispScheduleKind(int schedule_kind_id, int schedule_kind_detail_id)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //種別
            MsScheduleKind ki = db.GetMsScheduleKind(schedule_kind_id);
            if (ki != null)
            {
                this.comboBoxScheduleKind.SelectedItem = ki;
                //this.ReCreateKindDetailControl();
            }

            //種別詳細
            MsScheduleKindDetail de = db.GetMsScheduleKindDetail(schedule_kind_detail_id);
            if (de != null)
            {
                this.comboBoxScheduleKindDetail.SelectedItem = de;

            }
        }

        /// <summary>
        /// 基底部のエラーコントロール一覧取得 色は変えません。
        /// </summary>
        /// <returns></returns>
        protected List<Control> GetBaseErrorControl()
        {
            List<Control> anslist = new List<Control>();

            //種別
            MsScheduleKind sk = this.comboBoxScheduleKind.SelectedItem as MsScheduleKind;
            if (sk == null)
            {
                anslist.Add(this.panelErrorScheduleKind);
            }
            //詳細
            MsScheduleKindDetail sd = this.comboBoxScheduleKindDetail.SelectedItem as MsScheduleKindDetail;
            if (sd == null)
            {
                anslist.Add(this.panelErrorScheduleKindDetail);
            }

            return anslist;
        }

        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">取得元の場所</param>
        /// <returns></returns>
        protected virtual T GetBaseInputData<T>(T src) where T : DcSchedule
        {
            T ans = src;

            //カテゴリ
            ans.schedule_category_id = (int)this.Category;

            //種別
            MsScheduleKind ki = this.comboBoxScheduleKind.SelectedItem as MsScheduleKind;
            if (ki != null)
            {
                ans.schedule_kind_id = ki.schedule_kind_id;
            }

            //詳細
            MsScheduleKindDetail de = this.comboBoxScheduleKindDetail.SelectedItem as MsScheduleKindDetail;
            if (de != null)
            {
                ans.schedule_kind_detail_id = de.schedule_kind_detail_id;
                ans.DataColor = de.DataColor;
            }

            //予定日
            ans.estimate_date = this.dateTimePickerEstimateDate.Value.Date;            

            //実績日
            ans.inspection_date = DcSchedule.EDate;
            if (this.dateTimePickerInspectionDate.Checked == true)
            {
                ans.inspection_date = this.dateTimePickerInspectionDate.Value.Date;
            }

            //有効期限
            ans.expiry_date = DcSchedule.EDate;
            if (this.dateTimePickerExpiryDate.Checked == true)
            {
                ans.expiry_date = this.dateTimePickerExpiryDate.Value.Date;
            }

            //実績メモ
            ans.record_memo = this.textBoxRecordMemo.Text.Trim();

            return ans;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseScheduleInputControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 種別が選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScheduleKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            //再作成
            this.ReCreateKindDetailControl();
        }


        /// <summary>
        /// 種別詳細が選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScheduleKindDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsScheduleKindDetail de = this.comboBoxScheduleKindDetail.SelectedItem as MsScheduleKindDetail;
            if (de == null)
            {
                return;
            }

            //this.BackColor = de.DataColor;
            this.Refresh();
        }

        /// <summary>
        /// チェックが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerInspectionDate_ValueChanged(object sender, EventArgs e)
        {
            //実績日が入ったら削除できない
            this.checkBoxDelete.Enabled = true;

            if (this.dateTimePickerInspectionDate.Checked == true)
            {
                this.checkBoxDelete.Checked = false;
                this.checkBoxDelete.Enabled = false;
            }
            
        }
    }
}
