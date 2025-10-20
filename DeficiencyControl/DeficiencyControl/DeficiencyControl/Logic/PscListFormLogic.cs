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


namespace DeficiencyControl.Logic
{
    /// <summary>
    /// PSC一覧画面ロジック
    /// </summary>
    public class PscListFormLogic : BaseFormLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">管理画面</param>
        /// <param name="fdata">管理データ</param>
        public PscListFormLogic(PscListForm f, PscListForm.PscListFormData fdata)
        {
            this.Form = f;
            this.FData = fdata;
        }

        /// <summary>
        /// これの画面
        /// </summary>
        private PscListForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        PscListForm.PscListFormData FData = null;

        //___________________________________________________________________________________________________________
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// 検索コントロールのクリア
        /// </summary>
        public void ClearSearchControl()
        {
            PscListForm f = this.Form;

            f.singleLineComboVessel.Text = "";
            f.singleLineComboPort.Text = "";
            f.singleLineComboUser.Text = "";
            f.comboBoxItemKind.SelectedIndex = 0;
            f.singleLineComboCountry.Text = "";
            f.checkBoxStatusPending.Checked = true;
            f.checkBoxStatusComplete.Checked = true;

            f.checkBoxDeficiencyZero.Checked = false;

            f.datePeriodControlDate.SetDate(DateTime.Now, DateTime.Now, false);
            
            f.textBoxKeyword.Text = "";

            //DeficinecyControl
            f.deficiencyCodeSelectControl1.InitControl(null);

            //ActionCode
            ActionCodeControl.InputData idata = new ActionCodeControl.InputData() { SearchFlag = true };
            f.actionCodeControl1.InitControl(idata);

            //データクリア
            this.FData.DispList = new List<PSCInspectionData>();
            f.Grid.DispData(this.FData.DispList);

            f.dataCountControl1.DataCount = 0;
        }

        /// <summary>
        /// 検索条件の作成
        /// </summary>
        /// <returns></returns>
        private PscInspectionSearchData CreateSearchData()
        {
            PscListForm f = this.Form;
            
            PscInspectionSearchData ans = new PscInspectionSearchData();

            //Vessel
            MsVessel ves = f.singleLineComboVessel.SelectedItem as MsVessel;
            if (ves != null)
            {
                ans.ms_vessel_id = ves.ms_vessel_id;
            }

            //場所
            MsBasho po = f.singleLineComboPort.SelectedItem as MsBasho;
            if (po != null)
            {
                ans.ms_basho_id = po.ms_basho_id;
            }

            //PIC
            MsUser u = f.singleLineComboUser.SelectedItem as MsUser;
            if(u!= null)
            {
                ans.ms_user_id = u.ms_user_id;
            }

            //種別
            MsItemKind kind = f.comboBoxItemKind.SelectedItem as MsItemKind;
            if (kind != null)
            {
                ans.item_kind_id = kind.item_kind_id;
            }

            //国
            MsRegional reg = f.singleLineComboCountry.SelectedItem as MsRegional;
            if (reg != null)
            {
                ans.ms_regional_code = reg.ms_regional_code;
            }

            //Status
            ans.StatusPending = f.checkBoxStatusPending.Checked;
            ans.StatusComplete = f.checkBoxStatusComplete.Checked;
        
            //Date 日付
            if (f.datePeriodControlDate.linkedDatetimePickerDateStart.Checked == true)
            {
                ans.date_start = f.datePeriodControlDate.linkedDatetimePickerDateStart.Value.Date;
                ans.date_end = f.datePeriodControlDate.linkedDatetimePickerDateEnd.Value.Date;
            }

            //検索キーワード
            ans.SearchKeyword = f.textBoxKeyword.Text.Trim();

            //DeficiencyCode
            MsDeficiencyCode dco = f.deficiencyCodeSelectControl1.GetSelectData();
            if (dco != null)
            {
                ans.deficiency_code_id = dco.deficiency_code_id;
            }

            //ActionCodeID
            ans.ActionCodeIDList = new List<int>();
            DcActionCodeHistory ac = f.actionCodeControl1.GetInputData(0);
            if (ac.action_code_id != MsActionCode.EVal)
            {
                //有効かを確認
                ans.ActionCodeIDList.Add(ac.action_code_id);
            }

            //0件検索可否
            ans.DeficiencyCountZeroEnabledFlag = f.checkBoxDeficiencyZero.Checked;

            return ans;
        }



        /// <summary>
        /// 検索作成
        /// </summary>
        public void SearchData()
        {
            //検索データの収集
            PscInspectionSearchData sdata = this.CreateSearchData();

            //検索
            this.FData.DispList = SvcManager.SvcMana.PSCInspectionData_GetDataListBySearchData(sdata);

            //データ件数表示
            this.Form.dataCountControl1.DataCount = this.FData.DispList.Count;

            this.Form.Grid.DispData(this.FData.DispList);
        }
    }
}
