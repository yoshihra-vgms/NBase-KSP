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


namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船一覧画面ロジック
    /// </summary>
    public class MoiListFormLogic : BaseFormLogic
    {
        public MoiListFormLogic(MoiListForm f, MoiListForm.MoiListFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }


        /// <summary>
        /// 画面
        /// </summary>
        private MoiListForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        private MoiListForm.MoiListFormData FData = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// 検索コントロールのクリア
        /// </summary>
        public void ClearSearchControl()
        {
            MoiListForm f = this.Form;

            f.singleLineComboVessel.Text = "";
            f.comboBoxInspectionCategory.SelectedIndex = 0;

            f.singleLineComboPort.Text = "";
            f.singleLineComboCountry.Text = "";

            f.singleLineComboInspectionCompany.Text = "";
            f.textBoxInspectionName.Text = "";

            f.datePeriodControlDate.SetDate(DateTime.Now, DateTime.Now, false);

            f.singleLineComboUser.Text = "";

            //VIQ
            f.comboBoxViqVersion.SelectedIndex = 0;
            //f.comboBoxViqCode.SelectedIndex = 0;
            //f.singleLineComboViqNo.Text = "";

            f.textBoxKeyword.Text = "";

            f.checkBoxObservationZero.Checked = false;

            //グリッドクリア
            this.FData.DispList = new List<MoiData>();
            f.Grid.DispData(this.FData.DispList);

            this.Form.dataCountControl1.DataCount = 0;
        }



        /// <summary>
        /// 検索条件の作成
        /// </summary>
        /// <returns></returns>
        private MoiSearchData CreateSearchData()
        {
            MoiListForm f = this.Form;
            MoiSearchData ans = new MoiSearchData();


            //船
            MsVessel ves = f.singleLineComboVessel.SelectedItem as MsVessel;
            if (ves != null)
            {
                ans.ms_vessel_id = ves.ms_vessel_id;
            }

            //検船種別
            MsInspectionCategory ins = f.comboBoxInspectionCategory.SelectedItem as MsInspectionCategory;
            if (ins != null)
            {
                ans.inspection_category_id = ins.inspection_category_id;
            }

            //Port
            MsBasho ba = f.singleLineComboPort.SelectedItem as MsBasho;
            if (ba != null)
            {
                ans.ms_basho_id = ba.ms_basho_id;
            } 

            //国
            MsRegional reg = f.singleLineComboCountry.SelectedItem as MsRegional;
            if (reg != null)
            {
                ans.ms_regional_code = reg.ms_regional_code;
            }

            //検船会社
            MsCustomer inco = f.singleLineComboInspectionCompany.SelectedItem as MsCustomer;
            if (inco != null)
            {
                ans.inspection_ms_customer_id = inco.ms_customer_id;
            }

            //Inspectio Name
            ans.inspection_name = f.textBoxInspectionName.Text.Trim();

            //Date
            if (f.datePeriodControlDate.linkedDatetimePickerDateStart.Checked == true)
            {
                ans.date_start = f.datePeriodControlDate.linkedDatetimePickerDateStart.Value.Date;
                ans.date_end = f.datePeriodControlDate.linkedDatetimePickerDateEnd.Value.Date;
            }


            //PIC
            MsUser pic = f.singleLineComboUser.SelectedItem as MsUser;
            if (pic != null)
            {
                ans.ms_user_id = pic.ms_user_id;
            }

            //VIQVersion
            MsViqVersion vve = f.comboBoxViqVersion.SelectedItem as MsViqVersion;
            if (vve != null)
            {
                ans.viq_version_id = vve.viq_version_id;
            }

            //VIQCode
            MsViqCode vco = f.comboBoxViqCode.SelectedItem as MsViqCode;
            if (vco != null)
            {
                ans.viq_code_id = vco.viq_code_id;
            }

            //VIQNo
            MsViqNo vno = f.singleLineComboViqNo.SelectedItem as MsViqNo;
            if (vno != null)
            {
                ans.viq_no_id = vno.viq_no_id;
            }

            //ゼロ件検索可否
            ans.ObservationZeroEnabledFlag = f.checkBoxObservationZero.Checked;


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
            MoiSearchData sdata = this.CreateSearchData();

            //検索
            this.FData.DispList = SvcManager.SvcMana.MoiData_GetDataListBySearchData(sdata);

            //VIQ Versionの検索条件のみここに入れる。（もう少し良い方法があれば）
            this.FData.DispList = GetDataListByViqVersion(this.FData.DispList, sdata);

            //件数
            this.Form.dataCountControl1.DataCount = this.FData.DispList.Count;

            //再描画
            this.Form.Grid.DispData(this.FData.DispList);

        }

        /// <summary>
        /// 引数の指摘事項リストから、VIQ Versionが該当するデータのみを抽出する。
        /// </summary>
        /// <param name="obslist"></param>
        /// <param name="sdata"></param>
        /// <returns></returns>
        private List<MoiData> GetDataListByViqVersion(List<MoiData> obslist, MoiSearchData sdata)
        {
            // 条件にVIQ Versionが入っているか。
            if (sdata.viq_version_id == null || sdata.viq_code_id != null)
            {
                return obslist;
            }

            // VIQ Versionのキャシュ
            // <viq_code_id, viq_version_id>
            Dictionary<int, int> viqvdic = new Dictionary<int, int>();
            for (int i = obslist.Count-1; i >= 0; i--)
            {
                MoiObservationData obs = obslist[i].Observation;

                bool ret = viqvdic.ContainsKey(obs.Observation.viq_code_id);
                if (ret == false)
                {
                    //無いなら取得してキャッシュ化
                    MsViqCode code = DcGlobal.Global.DBCache.GetMsViqCode(obs.Observation.viq_code_id);
                    viqvdic.Add(obs.Observation.viq_code_id, code.viq_version_id);
                }
                int viq_version_id = viqvdic[obs.Observation.viq_code_id];

                // 検索条件のVIQ Versionと違う場合は、リストから削除
                if (viq_version_id != sdata.viq_version_id)
                {
                    obslist.RemoveAt(i);
                }
            }

            return obslist;
        }
    }
}
