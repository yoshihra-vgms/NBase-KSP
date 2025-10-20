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

using DeficiencyControl.Forms;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// AccidentListForm処理
    /// </summary>
    public class AccidentListFormLogic : BaseFormLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">管理画面</param>
        /// <param name="fdata">管理データ</param>
        public AccidentListFormLogic(AccidentListForm f, AccidentListForm.AccidentListFormData fdata)
        {
            this.Form = f;
            this.FData = fdata;
        }

        /// <summary>
        /// これの画面
        /// </summary>
        private AccidentListForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        AccidentListForm.AccidentListFormData FData = null;


        //------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 検索コントロールのクリア
        /// </summary>
        public void ClearSearchControl()
        {
            AccidentListForm f = this.Form;

            f.singleLineComboVessel.Text = "";
            f.comboBoxAccidentKind.SelectedIndex = 0;

            f.singleLineComboPort.Text = "";
            f.singleLineComboCountry.Text = "";

            f.singleLineComboUser.Text = "";

            f.comboBoxKindOfAccident.SelectedIndex = 0;
            f.comboBoxSituation.SelectedIndex = 0;

            f.datePeriodControlDate.SetDate(DateTime.Now, DateTime.Now, false);

            f.textBoxKeyword.Text = "";

            
            //グリッドクリア
            this.FData.DispList = new List<DcAccident>();
            f.Grid.DispData(this.FData.DispList);

            this.Form.dataCountControl1.DataCount = 0;

        }



        /// <summary>
        /// 検索条件の作成
        /// </summary>
        /// <returns></returns>
        private AccidentSearchData CreateSearchData()
        {
            AccidentSearchData ans = new AccidentSearchData();

            AccidentListForm f = this.Form;

            //Vessel
            MsVessel ves = f.singleLineComboVessel.SelectedItem as MsVessel;
            if (ves != null)
            {
                ans.ms_vessel_id = ves.ms_vessel_id;
            }


            //kind
            MsAccidentKind ki = f.comboBoxAccidentKind.SelectedItem as MsAccidentKind;
            if (ki != null)
            {
                ans.accident_kind_id = ki.accident_kind_id;
            }

            //Port
            MsBasho ba = f.singleLineComboPort.SelectedItem as MsBasho;
            if (ba != null)
            {
                ans.ms_basho_id = ba.ms_basho_id;
            }

            //Country
            MsRegional reg = f.singleLineComboCountry.SelectedItem as MsRegional;
            if (reg != null)
            {
                ans.ms_regional_code = reg.ms_regional_code;
            }

            //PIC
            MsUser pic = f.singleLineComboUser.SelectedItem as MsUser;
            if (pic != null)
            {
                ans.ms_user_id = pic.ms_user_id;
            }


            //Kind of Accident
            MsKindOfAccident koa = f.comboBoxKindOfAccident.SelectedItem as MsKindOfAccident;
            if (koa != null)
            {
                ans.kind_of_accident_id = koa.kind_of_accident_id;
            }

            //Situation
            MsAccidentSituation si = f.comboBoxSituation.SelectedItem as MsAccidentSituation;
            if (si != null)
            {
                ans.accident_situation_id = si.accident_situation_id;
            }


            //Date 日付
            if (f.datePeriodControlDate.linkedDatetimePickerDateStart.Checked == true)
            {
                ans.date_start = f.datePeriodControlDate.linkedDatetimePickerDateStart.Value.Date;
                ans.date_end = f.datePeriodControlDate.linkedDatetimePickerDateEnd.Value.Date;
            }

            //キーワード
            ans.SearchKeyword = f.textBoxKeyword.Text.Trim();

            

            return ans;
        }



        /// <summary>
        /// データの検索 WaiteStateでくくること
        /// </summary>
        public void Search()
        {
            //条件収集
            AccidentSearchData sdata = this.CreateSearchData();

            //検索
            this.FData.DispList = SvcManager.SvcMana.DcAccident_GetRecordsBySearchData(sdata);

            this.Form.dataCountControl1.DataCount = this.FData.DispList.Count;

            //再描画
            this.Form.Grid.DispData(this.FData.DispList);

        }

    }
}
