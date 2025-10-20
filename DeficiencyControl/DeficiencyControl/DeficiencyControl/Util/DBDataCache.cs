using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

namespace DeficiencyControl.Util
{
    /// <summary>
    /// DBデータ一時変数
    /// <remarks>検索で使用するマスタなどを保持する</remarks>
    /// </summary>
    public class DBDataCache : BaseDBCache
    {
        /// <summary>
        /// コンストラクタ　
        /// </summary>
        public DBDataCache()
        {
        }

        



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化関数
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {

            //DB値の取得-----------------------------------------------------
            //全体共通----------------------------
            //ユーザー一覧
            this.UserList = SvcManager.SvcMana.MsUser_GetRecords();

            //船一覧
            this.VesselList = SvcManager.SvcMana.MsVessel_GetRecords();

            //船カテゴリの一覧取得
            this.MsVesselCategoryList = SvcManager.SvcMana.MsVesselCategory_GetRecords();

            //船種一覧
            this.VesselTypeList = SvcManager.SvcMana.MsVesselType_GetRecords();

            //CrewMatrixType一覧
            this.MsCrewMatrixTypeList = SvcManager.SvcMana.MsCrewMatrixType_GetRecords();

            //検査種別
            this.ItemKindList = SvcManager.SvcMana.MsItemKind_GetRecords();

            //ステータス
            this.StatusList = SvcManager.SvcMana.MsStatus_GetRecords();

            //アラーム色情報
            this.AlarmColorList = SvcManager.SvcMana.MsAlarmColor_GetRecords();

            //ポート場所
            this.BashoList = SvcManager.SvcMana.MsBasho_GetRecords();

            //国
            this.RegionalList = SvcManager.SvcMana.MsRegional_GetRecords();

            //検船会社
            this.MsCustomerInspectionList = SvcManager.SvcMana.MsCustomer_GetRecordsInspectionCompany();
            
            //申請先
            this.MsCustomerAppointedList = SvcManager.SvcMana.MsCustomer_GetRecordsAppointedCompany();
           


            //コメント----------------------------
            #region コメント PSC
           
            //DeficiencyCategory
            this.MsDeficiencyCategoryList = SvcManager.SvcMana.MsDeficiencyCategory_GetRecords();

            //DeficiencyCode
            this.DeficiencyCodeList = SvcManager.SvcMana.MsDeficiencyCode_GetRecords();


            //ActionCode
            this.ActionCodeList = SvcManager.SvcMana.MsActionCode_GetRecords();
            

            #endregion


            #region Accident


            //MsAccidentImportance
            this.MsAccidentImportanceList = SvcManager.SvcMana.MsAccidentImportance_GetRecords();

            //MsAccidentKind
            this.MsAccidentKindList = SvcManager.SvcMana.MsAccidentKind_GetRecords();

            //MsAccidentSituation
            this.MsAccidentSituationList = SvcManager.SvcMana.MsAccidentSituation_GetRecords();

            //MsAccidentStatus
            this.MsAccidentStatusList = SvcManager.SvcMana.MsAccidentStatus_GetRecords();

            //MsKindOfAccident
            this.MsKindOfAccidentList = SvcManager.SvcMana.MsKindOfAccident_GetRecords();


            #endregion

            #region 検船
            //検船種別
            this.MsInspectionCategoryList = SvcManager.SvcMana.MsInspectionCategory_GetRecords();

            //VIQCode名前
            this.MsViqCodeNameList = SvcManager.SvcMana.MsViqCodeName_GetRecords();

            //VIQ Version
            this.MsViqVersionList = SvcManager.SvcMana.MsViqVersion_GetRecords();

            //VIQ Code
            this.MsViqCodeList = SvcManager.SvcMana.MsViqCode_GetRecords();

            //VIQ No
            this.MsViqNoList = SvcManager.SvcMana.MsViqNo_GetRecords();

            //検船状態
            this.MsMoiStatusList = SvcManager.SvcMana.MsMoiStatus_GetRecords();

            #endregion


            #region スケジュール
            //年度
            this.MsYearList = SvcManager.SvcMana.MsYear_GetRecords();

            //区分
            this.MsScheduleCategoryList = SvcManager.SvcMana.MsScheduleCategory_GetRecords();

            //スケジュール種別
            this.MsScheduleKindList = SvcManager.SvcMana.MsScheduleKind_GetRecords();

            //スケジュール種別詳細
            this.MsScheduleKindDetailList = SvcManager.SvcMana.MsScheduleKindDetail_GetRecords();



            #endregion

            return true;
        }


        
    }
}
