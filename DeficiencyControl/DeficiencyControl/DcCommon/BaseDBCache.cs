using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB.DAC;
using CIsl.DB.WingDAC;
using CIsl.DB;

namespace DcCommon
{
    public class BaseDBCache
    {
        #region メンバ変数
        //共通、または複数機能で利用するマスタ

        /// <summary>
        /// ユーザーリスト
        /// </summary>
        public List<MsUser> UserList = null;

        /// <summary>
        /// 船リスト
        /// </summary>
        public List<MsVessel> VesselList = null;

        /// <summary>
        /// 船カテゴリマスタ
        /// </summary>
        public List<MsVesselCategory> MsVesselCategoryList = null;

        /// <summary>
        /// 船種リスト
        /// </summary>
        public List<MsVesselType> VesselTypeList = null;


        /// <summary>
        /// CrewMatrixList
        /// </summary>
        public List<MsCrewMatrixType> MsCrewMatrixTypeList = null;

        /// <summary>
        /// 検査種別リスト
        /// </summary>
        public List<MsItemKind> ItemKindList = null;


        /// <summary>
        /// ステータスリスト
        /// </summary>
        public List<MsStatus> StatusList = null;

        /// <summary>
        /// アラーム色情報
        /// </summary>
        public List<MsAlarmColor> AlarmColorList = null;


        /// <summary>
        /// ポート場所リスト
        /// </summary>
        public List<MsBasho> BashoList = null;


        /// <summary>
        /// 国リスト
        /// </summary>
        public List<MsRegional> RegionalList = null;


        /// <summary>
        /// 検船会社リスト
        /// </summary>
        public List<MsCustomer> MsCustomerInspectionList = null;

        /// <summary>
        /// 申請先リスト
        /// </summary>
        public List<MsCustomer> MsCustomerAppointedList = null;


        #region コメント専用のマスタ

        /// <summary>
        /// MsDeficiencyCategoryのリスト
        /// </summary>
        public List<MsDeficiencyCategory> MsDeficiencyCategoryList = null;

        /// <summary>
        /// DeficiencyCodeリスト
        /// </summary>
        public List<MsDeficiencyCode> DeficiencyCodeList = null;

       

        /// <summary>
        /// ActionCodeリスト
        /// </summary>
        public List<MsActionCode> ActionCodeList = null;

      



        #endregion



        #region Accident専用のマスタ




        /// <summary>
        /// MsAccidentImportanceリスト
        /// </summary>
        public List<MsAccidentImportance> MsAccidentImportanceList = null;



        /// <summary>
        /// MsAccidentKindリスト
        /// </summary>
        public List<MsAccidentKind> MsAccidentKindList = null;
        
        /// <summary>
        /// MsAccidentSituationの一覧
        /// </summary>
        public List<MsAccidentSituation> MsAccidentSituationList = null;

        /// <summary>
        /// MsAccidentStatusの一覧
        /// </summary>
        public List<MsAccidentStatus> MsAccidentStatusList = null;

        /// <summary>
        /// MsKindOfAccidentの一覧
        /// </summary>
        public List<MsKindOfAccident> MsKindOfAccidentList = null;





        #endregion



        #region 検船専用のマスタ


        /// <summary>
        /// 検船種別一覧
        /// </summary>
        public List<MsInspectionCategory> MsInspectionCategoryList = null;

        /// <summary>
        /// VIQ Code一覧
        /// </summary>
        public List<MsViqCode> MsViqCodeList = null;

        /// <summary>
        /// VIQ名前
        /// </summary>
        public List<MsViqCodeName> MsViqCodeNameList = null;

        /// <summary>
        /// VIQ Version
        /// </summary>
        public List<MsViqVersion> MsViqVersionList = null;

        /// <summary>
        /// VIQ No一覧
        /// </summary>
        public List<MsViqNo> MsViqNoList = null;

        /// <summary>
        /// 検船状態一覧
        /// </summary>
        public List<MsMoiStatus> MsMoiStatusList = null;
        #endregion


        #region スケジュールのマスタ

        /// <summary>
        /// 年度一覧
        /// </summary>
        public List<MsYear> MsYearList = null;

        /// <summary>
        /// スケジュール区分
        /// </summary>
        public List<MsScheduleCategory> MsScheduleCategoryList = null;

        /// <summary>
        /// スケジュール種別
        /// </summary>
        public List<MsScheduleKind> MsScheduleKindList = null;

        /// <summary>
        /// スケジュール種別詳細
        /// </summary>
        public List<MsScheduleKindDetail> MsScheduleKindDetailList = null;

        #endregion
        #endregion


        /// <summary>
        /// 対象のマスタの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetMaster<T>(int id, List<T> datalist) where T : BaseDac
        {
            T ans = null;

            //名前の取得
            var v = from f in datalist where f.ID == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }



        #region 取得関数

        /// <summary>
        /// MsVesselの取得
        /// </summary>
        /// <param name="vesid"></param>
        /// <returns></returns>
        public MsVessel GetMsVessel(decimal vesid)
        {
            MsVessel ans = null;

            var v = from f in this.VesselList where f.ms_vessel_id == vesid select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsVesselの取得
        /// </summary>
        /// <param name="name">船名</param>
        /// <returns></returns>
        public MsVessel GetMsVessel(string name)
        {
            MsVessel ans = null;

            //名前の取得
            var v = from f in this.VesselList where f.vessel_name == name select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;

        }

        /// <summary>
        /// MsItemKindの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsItemKind GetMsItemKind(int id)
        {
            MsItemKind ans = null;

            var v = from f in this.ItemKindList where f.item_kind_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsUserの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsUser GetMsUser(string id)
        {
            MsUser ans = null;

            var v = from f in this.UserList where f.ms_user_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// 船カテゴリマスタ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsVesselCategory GetMsVesselCategory(string id)
        {
            MsVesselCategory ans = null;

            var v = from f in this.MsVesselCategoryList where f.ms_vessel_category_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// MsStatusの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsStatus GetMsStatus(int id)
        {
            MsStatus ans = null;

            var v = from f in this.StatusList where f.status_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsVesselTypeの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsVesselType GetMsVesselType(string id)
        {
            MsVesselType ans = null;

            var v = from f in this.VesselTypeList where f.ms_vessel_type_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsCrewMatrixTypeの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsCrewMatrixType GetMsCrewMatrixType(decimal id)
        {
            MsCrewMatrixType ans = null;

            var v = from f in this.MsCrewMatrixTypeList where f.ms_crew_matrix_type_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsBashoの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsBasho GetMsBasho(string id)
        {
            MsBasho ans = null;

            var v = from f in this.BashoList where f.ms_basho_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsRegionalの取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MsRegional GetMsRegional(string code)
        {
            MsRegional ans = null;

            var v = from f in this.RegionalList where f.ms_regional_code == code select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }






        /// <summary>
        /// MsDeficiencyCategory取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsDeficiencyCategory GetMsDeficiencyCategory(int id)
        {
            MsDeficiencyCategory ans = GetMaster<MsDeficiencyCategory>(id, this.MsDeficiencyCategoryList);
            return ans;
        }




        /// <summary>
        /// MsDeficiencyCodeの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsDeficiencyCode GetMsDeficiencyCode(int id)
        {
            MsDeficiencyCode ans = null;

            var v = from f in this.DeficiencyCodeList where f.deficiency_code_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

       

        /// <summary>
        /// MsActionCodeの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsActionCode GetMsActionCode(int id)
        {
            MsActionCode ans = null;

            var v = from f in this.ActionCodeList where f.action_code_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsAccidentImportanceの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsAccidentImportance GetMsAccidentImportance(int id)
        {
            MsAccidentImportance ans = null;

            var v = from f in this.MsAccidentImportanceList where f.accident_importance_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// MsAccidentKindの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsAccidentKind GetMsAccidentKind(int id)
        {
            MsAccidentKind ans = null;

            var v = from f in this.MsAccidentKindList where f.accident_kind_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// MsAccidentSituationの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsAccidentSituation GetMsAccidentSituation(int id)
        {
            MsAccidentSituation ans = null;

            var v = from f in this.MsAccidentSituationList where f.accident_situation_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// MsAccidentStatusの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsAccidentStatus GetMsAccidentStatus(int id)
        {
            MsAccidentStatus ans = null;

            var v = from f in this.MsAccidentStatusList where f.accident_status_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// MsKindOfAccidentの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsKindOfAccident GetMsKindOfAccident(int id)
        {
            MsKindOfAccident ans = null;

            var v = from f in this.MsKindOfAccidentList where f.kind_of_accident_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }

        /// <summary>
        /// 検船種別の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsInspectionCategory GetMsInspectionCategory(int id)
        {
            MsInspectionCategory ans = GetMaster<MsInspectionCategory>(id, this.MsInspectionCategoryList);
            return ans;
        }

        /// <summary>
        /// VIQCodeの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsViqCode GetMsViqCode(int id)
        {
            MsViqCode ans = GetMaster<MsViqCode>(id, this.MsViqCodeList);
            return ans;
        }

        /// <summary>
        /// VIQ Noの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsViqNo GetMsViqNo(int id)
        {
            MsViqNo ans = GetMaster<MsViqNo>(id, this.MsViqNoList);
            return ans;
        }


        /// <summary>
        /// 検船種別の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsMoiStatus GetMsMoiStatus(int id)
        {
            MsMoiStatus ans = GetMaster<MsMoiStatus>(id, this.MsMoiStatusList);
            return ans;
        }



        /// <summary>
        /// 検船会社の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsCustomer GetMsCustomerInspection(string id)
        {
            MsCustomer ans = null;

            var v = from f in this.MsCustomerInspectionList where f.ms_customer_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// 申請先の取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsCustomer GetMsCustomerAppointed(string id)
        {
            MsCustomer ans = null;

            var v = from f in this.MsCustomerAppointedList where f.ms_customer_id == id select f;
            if (v.Count() > 0)
            {
                ans = v.First();
            }

            return ans;
        }


        /// <summary>
        /// 年度取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsYear GetMsYear(int id)
        {
            MsYear ans = GetMaster<MsYear>(id, this.MsYearList);
            return ans;
        }


        /// <summary>
        /// スケジュール区分取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsScheduleCategory GetMsScheduleCategory(int id)
        {
            MsScheduleCategory ans = GetMaster<MsScheduleCategory>(id, this.MsScheduleCategoryList);
            return ans;
        }


        /// <summary>
        /// スケジュール種別取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsScheduleKind GetMsScheduleKind(int id)
        {
            MsScheduleKind ans = GetMaster<MsScheduleKind>(id, this.MsScheduleKindList);
            return ans;
        }


        /// <summary>
        /// スケジュール種別詳細取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsScheduleKindDetail GetMsScheduleKindDetail(int id)
        {
            MsScheduleKindDetail ans = GetMaster<MsScheduleKindDetail>(id, this.MsScheduleKindDetailList);
            return ans;
        }

        /// <summary>
        /// 対象スケジュール種別の子スケジュール種別詳細
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public List<MsScheduleKindDetail> GetMsScheduleKindDetailList(EScheduleKind kind)
        {
            List<MsScheduleKindDetail> anslist = new List<MsScheduleKindDetail>();

            var n = from f in this.MsScheduleKindDetailList where f.schedule_kind_id == (int)kind select f;
            anslist = n.ToList();

            return anslist;
        }


        /// <summary>
        /// MsViqCodeNameの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsViqCodeName GetMsViqCodeName(int id)
        {
            MsViqCodeName ans = GetMaster<MsViqCodeName>(id, this.MsViqCodeNameList);
            return ans;
        }




        

        #endregion

    }
}
