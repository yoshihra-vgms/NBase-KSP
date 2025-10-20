using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using DcCommon.Files;
using CIsl.DB.WingDAC;

using DeficiencyControl.Logic;
using DeficiencyControl.WcfServiceDeficiency;
using DcCommon.Output;

namespace DeficiencyControl
{
    /// <summary>
    /// サーバー列挙
    /// </summary>
    public enum EServerID
    {
        Cloud,
        WingSV,



        //---------------------------
        Max
    }

    /// <summary>
    /// 検査管理 サービス接続管理クラス
    /// <remarks>
    /// 案件管理の時、自作のサービスクラスでラッパーしておくと何かと便利だったため、今回も実装を行う。
    /// 基本的にサービスクラスにスルーするだけだが、今後何かあったときのためにこのように行う。
    /// </remarks>
    /// </summary>
    public class SvcManager : IServiceDeficiency
    {
        private SvcManager()
        {
        }

        #region シングルトン
        /// <summary>
        /// 実体
        /// </summary>
        private static SvcManager Instance = null;

        /// <summary>
        /// 取得
        /// </summary>
        public static SvcManager SvcMana
        {
            get
            {
                return SvcManager.Instance;
            }
        }

        #endregion


        

        /// <summary>
        /// サービス接続情報データ
        /// </summary>
        public class SvcInfoData
        {
            /// <summary>
            /// endpoint設定名
            /// </summary>
            public string EpName = "";

            /// <summary>
            /// 識別ID
            /// </summary>
            public EServerID SID = EServerID.Max;
        }


        /// <summary>
        /// クラウド接続config名
        /// </summary>
        private const string WingCloudConfigName = "WcfServiceDeficiency_Config_Cloud";

        /// <summary>
        /// ローカルSVconfig名
        /// </summary>
        private const string WingSVConfigName = "WcfServiceDeficiency_Config_SV";

        /// <summary>
        /// デフォルト接続先情報
        /// </summary>
        private SvcInfoData DefaultSvcInfo = null;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*
        /// <summary>
        /// 初期化関数
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            SvcManager.Instance = new SvcManager();
            return true;
        }
        */


        /// <summary>
        /// ログイン処理・・・これは基本的に使用してはいけない。InitLoginを使用せよ
        /// </summary>
        /// <param name="loginid">ログインID</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public UserData Login(string loginid, string password)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            UserData ans = sc.Login(loginid, password);

            return ans;
        }


        /// <summary>
        /// 初期化と接続
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserData InitLogin(string loginid, string password)
        {
            UserData ans = null;

            //作成
            SvcManager.Instance = new SvcManager();

            {
                DcLog.WriteLog("クラウド接続確認開始");
                
                //ローカルWingSV用の接続データ作成
                SvcInfoData clinfo = new SvcInfoData() { EpName = WingCloudConfigName, SID = EServerID.Cloud };
                try
                {
                    ServiceDeficiencyClient sc = WcfSvcCreator.Create(clinfo);
                    ans = sc.Login(loginid, password);

                    if (ans != null)
                    {
                        //接続先設定
                        SvcManager.Instance.DefaultSvcInfo = clinfo;
                        DcLog.WriteLog("クラウドユーザー取得成功");
                        return ans;
                    }

                }
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "クラウド接続失敗");
                }
            }

            //ここまで来たらどのみち失敗
            return null;
        }


        /// <summary>
        /// サーバー日付取得
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerDate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            
            DateTime ans = sc.GetServerDate();
            return ans;
        }

        


        /// <summary>
        /// 船マスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVessel> MsVessel_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            return sc.MsVessel_GetRecords();
        }

        /// <summary>
        /// ユーザー一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsUser> MsUser_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            return sc.MsUser_GetRecords();
        }

        /// <summary>
        /// 検査種別一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsItemKind> MsItemKind_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            return sc.MsItemKind_GetRecords();
        }

        /// <summary>
        /// ステータス一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsStatus> MsStatus_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            return sc.MsStatus_GetRecords();
        }


        /// <summary>
        /// アラーム色情報一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAlarmColor> MsAlarmColor_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);

            return sc.MsAlarmColor_GetRecords();
        }





        /// <summary>
        /// 添付ファイルデータの取得
        /// </summary>
        /// <param name="attachment_id"></param>
        /// <returns></returns>
        public DcAttachment DcAttachment_DownloadAttachment(int attachment_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAttachment_DownloadAttachment(attachment_id);
        }

        
        /// <summary>
        /// MsActionCode一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsActionCode> MsActionCode_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsActionCode_GetRecords();
        }

        /// <summary>
        /// MsDeficiencyCode一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsDeficiencyCode> MsDeficiencyCode_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsDeficiencyCode_GetRecords();
        }

        /// <summary>
        /// MsVesselType一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselType> MsVesselType_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsVesselType_GetRecords();
        }

        /// <summary>
        /// MsBasho Port 一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsBasho> MsBasho_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsBasho_GetRecords();
        }

        /// <summary>
        /// MsRegional 国一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsRegional> MsRegional_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsRegional_GetRecords();
        }

        /// <summary>
        /// MsAlarmColorの挿入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int MsAlarmColor_InsertRecord(MsAlarmColor data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAlarmColor_InsertRecord(data, requser);
        }


        /// <summary>
        /// MsAlarmColorの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MsAlarmColor_UpdateRecord(MsAlarmColor data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAlarmColor_UpdateRecord(data, requser);
        }

        /// <summary>
        /// MsAlarmColorの削除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MsAlarmColor_DeleteRecord(MsAlarmColor data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAlarmColor_DeleteRecord(data, requser);
        }



        /// <summary>
        /// 対象PSCInspectionDataの取得
        /// </summary>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public PSCInspectionData PSCInspectionData_GetDataByCommentItemID(int comment_item_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.PSCInspectionData_GetDataByCommentItemID(comment_item_id);
        }


        /// <summary>
        /// PSCInspectionDataの挿入
        /// </summary>
        /// <param name="comhead"></param>
        /// <param name="datalist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool PSCInspectionData_InsertList(CommentData comhead, List<PSCInspectionData> datalist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.PSCInspectionData_InsertList(comhead, datalist, requser);
        }

        /// <summary>
        /// PSCInspectionDataの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<PSCInspectionData> PSCInspectionData_GetDataListBySearchData(PscInspectionSearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.PSCInspectionData_GetDataListBySearchData(sdata);
        }

        /// <summary>
        /// PSCInspectionDataの更新
        /// </summary>
        /// <param name="comhead"></param>
        /// <param name="pscdata"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool PSCInspectionData_UpdateWithSister(CommentData comhead, PSCInspectionData pscdata, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.PSCInspectionData_UpdateWithSister(comhead, pscdata, requser);
        }


        /// <summary>
        /// PSCInspectionDataの削除
        /// </summary>
        /// <param name="comment_item_id"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool PSCInspectionData_Delete(int comment_item_id, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.PSCInspectionData_Delete(comment_item_id, requser);
        }


        /// <summary>
        /// コメントアイテムに関するファイルを全て取得する
        /// </summary>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByCommentItemID(int comment_item_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAttachment_GetARecrodsAllByCommentItemID(comment_item_id);
        }


        /// <summary>
        /// MsAccidentSituationの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentSituation> MsAccidentSituation_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAccidentSituation_GetRecords();
        }

        /// <summary>
        /// MsAccidentStatusの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentStatus> MsAccidentStatus_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAccidentStatus_GetRecords();
        }

        /// <summary>
        /// MsKindOfAccident一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsKindOfAccident> MsKindOfAccident_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsKindOfAccident_GetRecords();
        }

        /// <summary>
        /// MsAccidentImportance一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentImportance> MsAccidentImportance_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAccidentImportance_GetRecords();
        }

        /// <summary>
        /// MsAccidentKind一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentKind> MsAccidentKind_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsAccidentKind_GetRecords();
        }


        /// <summary>
        /// DcAccidentの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcAccident> DcAccident_GetRecordsBySearchData(AccidentSearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAccident_GetRecordsBySearchData(sdata);
        }


        /// <summary>
        /// 対象AccidentDataの取得
        /// </summary>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public AccidentData AccidentData_GetDataByAccidentID(int accident_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.AccidentData_GetDataByAccidentID(accident_id);
        }

        /// <summary>
        /// AccidentDataの挿入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int AccidentData_InsertData(AccidentData data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.AccidentData_InsertData(data, requser);
        }

        /// <summary>
        /// AccidentDataの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool AccidentData_UpdateData(AccidentData data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.AccidentData_UpdateData(data, requser);
        }


        /// <summary>
        /// AccidentDataの削除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool AccidentData_DeleteData(int accident_id, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.AccidentData_DeleteData(accident_id, requser);
        }


        /// <summary>
        /// 事故トラブル報告書番号の作成
        /// </summary>
        /// <returns></returns>
        public string CreateAccidentReportNo()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.CreateAccidentReportNo();
        }


        /// <summary>
        /// 事故トラブルに関連するファイルをすべて取得する
        /// </summary>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByAccidentID(int accident_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAttachment_GetARecrodsAllByAccidentID(accident_id);
        }


        /// <summary>
        /// 検船種別の一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsInspectionCategory> MsInspectionCategory_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsInspectionCategory_GetRecords();
        }

        /// <summary>
        /// VIQ Code一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqCode> MsViqCode_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsViqCode_GetRecords();
        }

        /// <summary>
        /// VIQ No一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqNo> MsViqNo_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsViqNo_GetRecords();
        }

        /// <summary>
        /// viq_codeに関連するViqNOを一覧取得
        /// </summary>
        /// <param name="viq_code_id"></param>
        /// <returns></returns>
        public List<MsViqNo> MsViqNo_GetRecordsByViqCodeID(int viq_code_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsViqNo_GetRecordsByViqCodeID(viq_code_id);
        }


        /// <summary>
        /// 検船状態一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsMoiStatus> MsMoiStatus_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsMoiStatus_GetRecords();
        }


        /// <summary>
        /// 顧客一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsCustomer_GetRecords();
        }

        /// <summary>
        /// 検船会社一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecordsInspectionCompany()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsCustomer_GetRecordsInspectionCompany();
        }

        /// <summary>
        /// 申請先一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecordsAppointedCompany()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsCustomer_GetRecordsAppointedCompany();
        }


        /// <summary>
        /// 検船データの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<MoiData> MoiData_GetDataListBySearchData(MoiSearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MoiData_GetDataListBySearchData(sdata);
        }


        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool LogoutUser(MsUser user)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.LogoutUser(user);
        }


        /// <summary>
        /// 検船データ挿入処理
        /// </summary>
        /// <param name="hdata"></param>
        /// <param name="obslist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int Moi_Insert(MoiHeaderData hdata, List<MoiObservationData> obslist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.Moi_Insert(hdata, obslist, requser);
        }

        /// <summary>
        /// 対象の検船データを取得する
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public MoiData MoiData_GetDataByMoiObservationID(int moi_observation_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MoiData_GetDataByMoiObservationID(moi_observation_id);
        }


        /// <summary>
        /// 検船に関係するファイルを全て取得する
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByMoi(int moi_observation_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAttachment_GetARecrodsAllByMoi(moi_observation_id);
        }

        /// <summary>
        /// 検船データの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MoiData_UpdatetData(MoiData data, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MoiData_UpdatetData(data, requser);
        }

        /// <summary>
        /// 検船データの削除
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MoiData_DeleteData(int moi_observation_id, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MoiData_DeleteData(moi_observation_id, requser);
        }


        /// <summary>
        /// 事故トラブルエクセルテンプレートの取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetAccidentExcelTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetAccidentExcelTemplate();
        }

        /// <summary>
        /// 事故トラブル帳票データの検索と取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<AccidentOutputData> GetAccidentOutputData(AccidentOutputParameter param)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetAccidentOutputData(param);
        }

        /// <summary>
        /// MsDeficiencyCategory一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsDeficiencyCategory> MsDeficiencyCategory_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsDeficiencyCategory_GetRecords();
        }


        /// <summary>
        /// MsYear一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsYear> MsYear_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsYear_GetRecords();
        }

        /// <summary>
        /// MsScheduleCategoryの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleCategory> MsScheduleCategory_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsScheduleCategory_GetRecords();
        }

        /// <summary>
        /// MsScheduleKindの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleKind> MsScheduleKind_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsScheduleKind_GetRecords();
        }

        /// <summary>
        /// MsScheduleKindDetailの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleKindDetail> MsScheduleKindDetail_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsScheduleKindDetail_GetRecords();
        }




        /// <summary>
        /// DcSchedulePlanの一括更新
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcSchedulePlan_InsertUpdateList(List<DcSchedulePlan> datalist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcSchedulePlan_InsertUpdateList(datalist, requser);
        }

        /// <summary>
        /// DcSchedulePlanの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcSchedulePlan> DcSchedulePlan_GetRecordsBySearchData(SchedulePlanSearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcSchedulePlan_GetRecordsBySearchData(sdata);
        }


        /// <summary>
        /// DcScheduleCompanyの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcScheduleCompany> DcScheduleCompany_GetRecordsBySearchData(ScheduleCompanySearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcScheduleCompany_GetRecordsBySearchData(sdata);
        }

        /// <summary>
        /// DcScheduleOtherの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcScheduleOther> DcScheduleOther_GetRecordsBySearchData(ScheduleOtherSearchData sdata)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcScheduleOther_GetRecordsBySearchData(sdata);
        }

        /// <summary>
        /// DcScheduleCompanyの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcScheduleCompany_InsertUpdateList(List<DcScheduleCompany> datalist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcScheduleCompany_InsertUpdateList(datalist, requser);
        }

        /// <summary>
        /// DcScheduleOtherの検索
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcScheduleOther_InsertUpdateList(List<DcScheduleOther> datalist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcScheduleOther_InsertUpdateList(datalist, requser);
        }

        /// <summary>
        /// PSC帳票出力データの検索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PscOutputData GetPSCOutputData(PscOutputParameter param)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetPSCOutputData(param);
        }

        /// <summary>
        /// PSC帳票テンプレートの取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetPSCExcelTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetPSCExcelTemplate();
        }

        /// <summary>
        /// DeficiencyCategoryDataの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<DeficiencyCategoryData> DeficiencyCategoryData_GetDataList()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DeficiencyCategoryData_GetDataList();
        }


        /// <summary>
        /// 検船帳票 章別指摘数テンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiExcelCategoryTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiExcelCategoryTemplate();
        }

        /// <summary>
        /// 検船帳票 是正対応リストテンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiExcelListTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiExcelListTemplate();
        }

        /// <summary>
        /// 検船報告書 検船コメントリストテンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiReportCommentListTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiReportCommentListTemplate();
        }

        /// <summary>
        /// 検船報告書 改善報告書テンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiReportObservationTemplate()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiReportObservationTemplate();
        }

        /// <summary>
        /// MsViqCodeNameの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqCodeName> MsViqCodeName_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsViqCodeName_GetRecords();
        }

        /// <summary>
        /// MsViqVersionの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqVersion> MsViqVersion_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsViqVersion_GetRecords();
        }

        /// <summary>
        /// 検船Excel帳票 章別指摘数データの取得
        /// </summary>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        public MoiExcelOutputDataCategory GetMoiExcelDataCategory(MoiExcelOutputParameter param)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiExcelDataCategory(param);
        }

        /// <summary>
        /// 検船Excel帳票 章別指摘数データの取得
        /// </summary>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        public MoiExcelOutputDataCategory GetMoiExcelDataCategory2(MoiExcelOutputParameter param, MsViqVersion version)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetMoiExcelDataCategory2(param, version);
        }



        /// <summary>
        /// 対象ユーザーの取得
        /// </summary>
        /// <param name="ms_user_id"></param>
        /// <returns></returns>
        public UserData UserData_GetDataByMsUserID(string ms_user_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.UserData_GetDataByMsUserID(ms_user_id);
        }


        /// <summary>
        /// 対象船のスケジュール有効可否情報の取得
        /// </summary>
        /// <param name="ms_vessel_id"></param>
        /// <returns></returns>
        public List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID(decimal ms_vessel_id)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID(ms_vessel_id);
        }

        /// <summary>
        /// スケジュール一覧テンプレートの取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetScheduleListTemplateFile()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.GetScheduleListTemplateFile();
        }


        /// <summary>
        /// 検査有効可否の一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsVesselScheduleKindDetailEnable_GetRecords();
        }


        /// <summary>
        /// 船カテゴリの取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselCategory> MsVesselCategory_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsVesselCategory_GetRecords();
        }


        /// <summary>
        /// CrewMatrixの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCrewMatrixType> MsCrewMatrixType_GetRecords()
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.MsCrewMatrixType_GetRecords();
        }


        /// <summary>
        /// ステータス更新
        /// </summary>
        /// <param name="psclist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcCiPscInspection_UpdateStatus(List<DcCiPscInspection> psclist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcCiPscInspection_UpdateStatus(psclist, requser);
        }


        /// <summary>
        /// DcAccident ステータス更新
        /// </summary>
        /// <param name="aclist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcAccident_UpdateStatus(List<DcAccident> aclist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcAccident_UpdateStatus(aclist, requser);
        }


        /// <summary>
        /// DcMoiObservationステータス更新
        /// </summary>
        /// <param name="obslist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcMoiObservation_UpdateStatus(List<DcMoiObservation> obslist, MsUser requser)
        {
            ServiceDeficiencyClient sc = WcfSvcCreator.Create(this.DefaultSvcInfo);
            return sc.DcMoiObservation_UpdateStatus(obslist, requser);
        }
    }
}
