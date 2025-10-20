using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using DcCommon.Files;
using System.IO;
using CIsl.DB;
using CIsl.DB.WingDAC;
using WcfServiceDeficiencyControl.Logic;
using DcCommon.Output;
using WcfServiceDeficiencyControl.Output;
//using WcfServiceDeficiencyControl.Logic;



namespace WcfServiceDeficiencyControl
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "Service1" を変更できます。
    public class ServiceDeficiency : IServiceDeficiency
    {
        /// <summary>
        /// 接続文字列の取得
        /// </summary>
        public static string DBConnectString
        {
            get
            {
                string ans = "";                
                ans = WebConfigManager.DBConnectString;
                return ans;
            }
        }

        /// <summary>
        /// ShipsWing接続文字列の取得
        /// </summary>
        public static string ShipsWingDBConnectString
        {
            get
            {
                string ans = "";
                ans = WebConfigManager.ShipsWingDBConnectString;
                return ans;
            }
        }

        //--------------------------------------------

        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// サーバー日付の取得
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerDate()
        {
            DateTime atime;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                atime = DBEtc.CheckServerDate(cone);
            }

            return atime;
        }


        #region IGT Deficiency
        

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="loginid">ログインID</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public UserData Login(string loginid, string password)
        {
            UserData ans = new UserData();

            try
            {
                using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
                {
                    //OperationContext.Current.IncomingMessageProperties;

                    //ユーザーの取得
                    MsUser user = MsUser.GetLoginUser(cone.DBCone, loginid, password);
                    if (user == null)
                    {
                        throw new Exception("GetLoginUser FALSE");
                    }

                    //部門の取得
                    MsBumon bumon = MsBumon.GetRecordByMsUserID(cone.DBCone, user.ms_user_id);
                    /*
                    if (bumon == null)
                    {
                        throw new Exception("MsBumon FALSE");
                    }*/

                    
                    //権限の取得・・・とりあえずは指摘事項のみ
                    List<MsRole> rolelist = new List<MsRole>();
                    if (bumon != null)
                    {
                        rolelist = MsRole.GetRecordsByMsBumonIDAdminName1(cone.DBCone, bumon.ms_bumon_id, user.admin_flag, ERoleName1.指摘事項管理.ToString());
                    }


                    ans.User = user;                                        
                    ans.Bumon = bumon;
                    ans.RoleList = rolelist;


                }

                //ログイン処理
                using (DBConnect cone = new DBConnect(DBConnectString))
                {
                    OperationHistoryCreator.Login(cone, ans.User);
                }

            }
            catch (Exception e)
            {
                SvcLog.WriteLog("Login id=" + loginid + " pass=" + password);
                SvcLog.WriteLog("host=" + OperationHistoryCreator.CreateHostString());
                SvcLog.WriteLog(e, "Login", null);                
                return null;
            }

            return ans;
        }


        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool LogoutUser(MsUser user)
        {
            bool ret = false;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                ret = OperationHistoryCreator.Logout(cone, user);
            }
            return ret;
        }

        /// <summary>
        /// ファイルのダウンロードデータ付
        /// </summary>
        /// <param name="attachment_id">取得ID</param>
        /// <returns></returns>
        public DcAttachment DcAttachment_DownloadAttachment(int attachment_id)
        {
            DcAttachment ans = null;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    ans = DcAttachment.DownloadAttachment(cone.DBCone, attachment_id);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "DcAttachment_DownloadAttachment", null);
                    return null;
                }
            }

            return ans;
        }




        /// <summary>
        /// 対象のPSCInspectionデータの取得
        /// </summary>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public PSCInspectionData PSCInspectionData_GetDataByCommentItemID(int comment_item_id)
        {
            PSCInspectionData ans = null;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    ans = PSCInspectionData.GetDataByCommentItemID(cone.DBCone, comment_item_id);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "PSCInspectionData_GetDataByCommentItemID");
                }
            }

            return ans;
        }



        /// <summary>
        /// PSC Inspectionデータの挿入
        /// </summary>
        /// <param name="comhead">ヘッダー</param>
        /// <param name="datalist">挿入PSC一式</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool PSCInspectionData_InsertList(CommentData comhead, List<PSCInspectionData> datalist, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //親挿入
                    int comment_id = comhead.Insert(cone.DBCone, requser);
                    if (comment_id <= 0)
                    {
                        throw new Exception("DcComment ret=Eval");
                    }

                    //全データ挿入
                    foreach (PSCInspectionData data in datalist)
                    {
                        //コメントID設定
                        data.PscInspection.comment_id = comment_id;

                        int id = data.Insert(cone.DBCone, requser);
                        if (id == BaseDac.EVal)
                        {
                            throw new Exception("Insert ret=EVal");
                        }
                    }


                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "PSCInspectionData_InsertList", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// PSCデータの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<PSCInspectionData> PSCInspectionData_GetDataListBySearchData(PscInspectionSearchData sdata)
        {
            List<PSCInspectionData> anslist = new List<PSCInspectionData>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = PSCInspectionData.GetDataListBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "PSCInspectionData_GetDataListBySearchData");
                }
            }

            return anslist;
        }



        /// <summary>
        /// PSCのデータを姉妹とともに更新する
        /// </summary>
        /// <param name="comhead">更新親</param>
        /// <param name="pscdata">更新データ</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool PSCInspectionData_UpdateWithSister(CommentData comhead, PSCInspectionData pscdata, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //更新処理
                    PSCInspectionData.UpdateDataWithSister(cone.DBCone, comhead, pscdata, requser);


                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "PSCInspectionData_UpdateWithSister", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// PSCInspectionDataの削除をする
        /// </summary>        
        /// <param name="comment_item_id">削除対象ID</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool PSCInspectionData_Delete(int comment_item_id, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    PSCInspectionData.DeleteSafe(cone.DBCone, comment_item_id, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "PSCInspectionData_Delete", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// DcCiPscInspectionのStatusを更新する。基本的にDeficiency青モードでの使用を想定
        /// </summary>
        /// <param name="psclist">更新一覧</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool DcCiPscInspection_UpdateStatus(List<DcCiPscInspection> psclist, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    foreach (DcCiPscInspection psc in psclist)
                    {
                        psc.UpdateStatus(cone.DBCone, requser);
                    }

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "DcCiPscInspection_UpdateStatus", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// コメントに関するファイルを全て取得する
        /// </summary>
        /// <param name="comment_item_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByCommentItemID(int comment_item_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcAttachment.GetARecrodsAllByCommentItemID(cone.DBCone, comment_item_id);
                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "DcAttachment_GetARecrodsAllByCommentItemID");
                }
            }

            return anslist;
        }



        


        /// <summary>
        /// Accidentの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcAccident> DcAccident_GetRecordsBySearchData(AccidentSearchData sdata)
        {
            List<DcAccident> anslist = new List<DcAccident>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcAccident.GetRecordsBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "DcAccident_GetRecordsBySearchData");
                }
            }

            return anslist;
        }

        /// <summary>
        /// 対象のAccidentDataを取得する
        /// </summary>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public AccidentData AccidentData_GetDataByAccidentID(int accident_id)
        {
            AccidentData ans = null;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    ans = AccidentData.GetDataByAccidentID(cone.DBCone, accident_id);
                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "AccidentData_GetDataByAccidentID");
                    return null;
                }
            }

            return ans;
        }


        /// <summary>
        /// AccidentDataの挿入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int AccidentData_InsertData(AccidentData data, MsUser requser)
        {
            int ans = DcAccident.EVal;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //報告書番号の作成
                    data.Accident.accident_report_no = this.CreateAccidentReportNo();
                    if (data.Accident.accident_report_no.Length <= 0)
                    {
                        throw new Exception("CreateAccidentReportNo FALSE");
                    }

                    ans = data.InsertData(cone.DBCone, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "AccidentData_InsertData", requser);
                    return DcAccident.EVal;
                }
            }

            return ans;
        }


        /// <summary>
        /// AccidentDataの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool AccidentData_UpdateData(AccidentData data, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    ret = data.UpdateData(cone.DBCone, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "AccidentData_UpdateData", requser);
                    return false;
                }
            }

            return ret;
        }

        /// <summary>
        /// AccidentDataの削除
        /// </summary>
        /// <param name="accident_id"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool AccidentData_DeleteData(int accident_id, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //データ取得
                    AccidentData data = AccidentData.GetDataByAccidentID(cone.DBCone, accident_id);

                    //削除
                    ret = data.DeleteData(cone.DBCone, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "AccidentData_DeleteData", requser);
                    return false;
                }
            }

            return ret;
        }


        /// <summary>
        /// DcAccidentのStatusを更新する。基本的にDeficiency青モードでの使用を想定
        /// </summary>
        /// <param name="aclist">更新一覧</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool DcAccident_UpdateStatus(List<DcAccident> aclist, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    foreach (DcAccident ac in aclist)
                    {
                        ac.UpdateStatus(cone.DBCone, requser);
                    }

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "DcAccident_UpdateStatus", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 事故トラブル報告書Noの作成
        /// </summary>
        /// <returns></returns>
        public string CreateAccidentReportNo()
        {
            string ans = "";
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    //年度-通し番号

                    //日付を取得
                    DateTime date = DBEtc.CheckServerDate(cone);

                    int no = MsSeq.GetAccidentReportNo(cone.DBCone);

                    //年度を取得
                    int year = Logic.CommonLogic.CalcuYear(date);


                    //作成
                    ans = string.Format("{0:0000}-{1}", year, no);
                    

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "CreateAccidentReportNo");
                    return "";
                    
                }
            }

            return ans;
        }


        /// <summary>
        /// Accidentに関連するファイルをすべて取得する
        /// </summary>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByAccidentID(int accident_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {                    
                    anslist = DcAttachment.GetARecrodsAllByAccidentID(cone.DBCone, accident_id);                    
                }
                catch (Exception e)
                {                    
                    SvcLog.WriteLog(e, "DcAttachment_GetARecrodsAllByAccidentID");
                    return anslist;
                }
            }

            return anslist;
        }


        /// <summary>
        /// MoiDataの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<MoiData> MoiData_GetDataListBySearchData(MoiSearchData sdata)
        {
            List<MoiData> anslist = new List<MoiData>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MoiData.GetDataListBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {                    
                    SvcLog.WriteLog(e, "MoiData_GetDataListBySearchData");
                    return anslist;
                }
            }

            return anslist;
        }

        /// <summary>
        /// 検船の挿入
        /// </summary>
        /// <param name="hdata"></param>
        /// <param name="obslist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int Moi_Insert(MoiHeaderData hdata, List<MoiObservationData> obslist, MsUser requser)
        {
            int ans = DcMoi.EVal;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //ヘッダー挿入
                    ans = hdata.InsertData(cone.DBCone, requser);

                    //指摘事項挿入
                    foreach (MoiObservationData obs in obslist)
                    {
                        obs.InsertData(cone.DBCone, requser, ans);
                    }

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "Moi_Insert", requser);
                    return DcMoi.EVal;
                }
            }

            return ans;
        }

        /// <summary>
        /// 対象の検船データを取得する
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public MoiData MoiData_GetDataByMoiObservationID(int moi_observation_id)
        {
            MoiData ans = null;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    ans = MoiData.GetDataByMoiObservationID(cone.DBCone, moi_observation_id);
                }
                catch (Exception e)
                {                    
                    SvcLog.WriteLog(e, "MoiData_GetDataByMoiObservationID");
                    return null;
                }
            }

            return ans;
        }

        /// <summary>
        /// 検船に関係するファイルを全て取得する
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <returns></returns>
        public List<DcAttachment> DcAttachment_GetARecrodsAllByMoi(int moi_observation_id)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcAttachment.GetARecrodsAllByMoi(cone.DBCone, moi_observation_id);
                }
                catch (Exception e)
                {
                    
                    SvcLog.WriteLog(e, "DcAttachment_GetARecrodsAllByMoi");
                    return anslist;
                }
            }

            return anslist;
        }

        /// <summary>
        /// 検船データの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MoiData_UpdatetData(MoiData data, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    ret = data.UpdateData(cone.DBCone, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "MoiData_UpdatetData", requser);
                    return false;
                }
            }

            return ret;
        }


        /// <summary>
        /// 検船データの削除
        /// </summary>
        /// <param name="moi_observation_id"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MoiData_DeleteData(int moi_observation_id, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //データ取得
                    MoiData data = MoiData.GetDataByMoiObservationID(cone.DBCone, moi_observation_id);

                    //削除
                    ret = data.DeleteData(cone.DBCone, requser);

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "MoiData_DeleteData", requser);
                    return false;
                }
            }

            return ret;
        }



        /// <summary>
        /// DcMoiObservationのステータス更新
        /// </summary>
        /// <param name="obslist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcMoiObservation_UpdateStatus(List<DcMoiObservation> obslist, MsUser requser)
        {
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //
                    foreach (DcMoiObservation obs in obslist)
                    {
                        obs.UpdateStatus(cone.DBCone, requser);
                    }

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "DcMoiObservation_UpdateStatus", requser);
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 事故トラブル帳票テンプレートのダウンロード
        /// </summary>
        /// <returns></returns>
        public byte[] GetAccidentExcelTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.AccidentTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetAccidentExcelTemplate");
                return null;
            }

            return data;
        }


        /// <summary>
        /// 事故トラブル帳票データを収集
        /// </summary>
        /// <param name="param">対象条件</param>
        /// <returns></returns>
        public List<AccidentOutputData> GetAccidentOutputData(AccidentOutputParameter param)
        {
            List<AccidentOutputData> anslist = new List<AccidentOutputData>();
            try
            {
                using (DBConnect defcone = new DBConnect(DBConnectString))
                {
                    using (DBConnect wingcone = new DBConnect(ShipsWingDBConnectString))
                    {
                        //
                        AccidentOutputDataCollector ao = new AccidentOutputDataCollector(wingcone.DBCone, defcone.DBCone);
                        anslist = ao.Collect(param);
                    }
                }
            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetAccidentOutputData");
                return null;
            }

            return anslist;

        }


        /// <summary>
        /// 予定実績スケジュールの検索
        /// </summary>
        /// <param name="year_id"></param>
        /// <returns></returns>
        public List<DcSchedulePlan> DcSchedulePlan_GetRecordsBySearchData(SchedulePlanSearchData sdata)
        {
            List<DcSchedulePlan> anslist = new List<DcSchedulePlan>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcSchedulePlan.GetRecordsBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {

                    SvcLog.WriteLog(e, "DcSchedulePlan_GetRecordsBySearchData");
                }
            }


            return anslist;
        }


        /// <summary>
        /// DcSchedulePlanの更新処理
        /// </summary>
        /// <param name="datalist">更新物一式</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool DcSchedulePlan_InsertUpdateList(List<DcSchedulePlan> datalist, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //データ登録
                    foreach (DcSchedulePlan data in datalist)
                    {
                        //ID可否によって挿入、更新を決定
                        if (data.schedule_id == DcSchedulePlan.EVal)
                        {
                            int id = data.InsertRecord(cone.DBCone, requser);
                        }
                        else
                        {
                            ret = data.UpdateRecord(cone.DBCone, requser);
                        }
                    }


                    cone.ComitTransaction();
                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();

                    SvcLog.WriteLog(e, "DcSchedulePlan_InsertUpdateList");
                    return false;
                }
            }

            return ret;
        }


        


        /// <summary>
        /// 会社スケジュールの検索
        /// </summary>
        /// <param name="year_id"></param>
        /// <returns></returns>
        public List<DcScheduleCompany> DcScheduleCompany_GetRecordsBySearchData(ScheduleCompanySearchData sdata)
        {
            List<DcScheduleCompany> anslist = new List<DcScheduleCompany>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcScheduleCompany.GetRecordsBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {

                    SvcLog.WriteLog(e, "DcScheduleCompany_GetRecordsBySearchData");
                }
            }


            return anslist;
        }

        /// <summary>
        /// その他スケジュールの検索
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public List<DcScheduleOther> DcScheduleOther_GetRecordsBySearchData(ScheduleOtherSearchData sdata)
        {
            List<DcScheduleOther> anslist = new List<DcScheduleOther>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = DcScheduleOther.GetRecordsBySearchData(cone.DBCone, sdata);
                }
                catch (Exception e)
                {

                    SvcLog.WriteLog(e, "DcScheduleOther_GetRecordsBySearchData");
                }
            }


            return anslist;
        }


        /// <summary>
        /// DcScheduleCompanyの挿入更新
        /// </summary>
        /// <param name="datalist">挿入更新データ</param>
        /// <param name="requser">要求者</param>
        /// <returns></returns>
        public bool DcScheduleCompany_InsertUpdateList(List<DcScheduleCompany> datalist, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //データ登録
                    foreach (DcScheduleCompany data in datalist)
                    {
                        //ID可否によって挿入、更新を決定
                        if (data.schedule_id == DcSchedule.EVal)
                        {
                            int id = data.InsertRecord(cone.DBCone, requser);
                        }
                        else
                        {
                            ret = data.UpdateRecord(cone.DBCone, requser);
                        }
                    }


                    cone.ComitTransaction();
                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();

                    SvcLog.WriteLog(e, "DcScheduleCompany_InsertUpdateList");
                    return false;
                }
            }

            return ret;
        }

        /// <summary>
        /// DcScheduleOtherの挿入更新
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DcScheduleOther_InsertUpdateList(List<DcScheduleOther> datalist, MsUser requser)
        {
            bool ret = true;
            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();

                    //データ登録
                    foreach (DcScheduleOther data in datalist)
                    {
                        //ID可否によって挿入、更新を決定
                        if (data.schedule_id == DcSchedule.EVal)
                        {
                            int id = data.InsertRecord(cone.DBCone, requser);
                        }
                        else
                        {
                            ret = data.UpdateRecord(cone.DBCone, requser);
                        }
                    }


                    cone.ComitTransaction();
                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();

                    SvcLog.WriteLog(e, "DcScheduleOther_InsertUpdateList");
                    return false;
                }
            }

            return ret;
        }



        /// <summary>
        /// PSC帳票テンプレートの取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetPSCExcelTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.PSCTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetPSCExcelTemplate");
                return null;
            }

            return data;
        }



        /// <summary>
        /// PSC出力データの検索
        /// </summary>
        /// <param name="param">検索条件</param>
        /// <returns></returns>
        public PscOutputData GetPSCOutputData(PscOutputParameter param)
        {
            PscOutputData ans = null;
            try
            {
                using (DBConnect defcone = new DBConnect(DBConnectString))
                {
                    using (DBConnect wingcone = new DBConnect(ShipsWingDBConnectString))
                    {
                        //PSC帳票情報の検索
                        PscOutputDataCollector psc = new PscOutputDataCollector(wingcone.DBCone, defcone.DBCone);
                        ans = psc.Collect(param);
                    }
                }
            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetPSCOutputData");
                return null;
            }

            return ans;

        }


        /// <summary>
        /// 検船帳票 章別指摘数テンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiExcelCategoryTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.MoiExcelCateroryTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiExcelCategoryTemplate");
                return null;
            }

            return data;
        }



        /// <summary>
        /// 検船帳票 是正対応リストテンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiExcelListTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.MoiExcelListTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiExcelListTemplate");
                return null;
            }

            return data;
        }


        /// <summary>
        /// 検船報告書 検船コメントリストテンプレート取得
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiReportCommentListTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.MoiReportCommentListTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiReportCommentListTemplate");
                return null;
            }

            return data;
        }



        /// <summary>
        /// 検船報告書 改善報告書テンプレート
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoiReportObservationTemplate()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.MoiReportObservationTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiReportReportTemplate");
                return null;
            }

            return data;
        }


        /// <summary>
        /// 検船Excel帳票章別指摘数データの取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MoiExcelOutputDataCategory GetMoiExcelDataCategory(MoiExcelOutputParameter param)
        {
            MoiExcelOutputDataCategory ans = null;
            try
            {
                using (DBConnect defcone = new DBConnect(DBConnectString))
                {
                    using (DBConnect wingcone = new DBConnect(ShipsWingDBConnectString))
                    {
                        //検船章別指摘数エクセル情報の検索収集
                        MoiExcelCategoryCollector moi = new MoiExcelCategoryCollector(wingcone.DBCone, defcone.DBCone);
                        ans = moi.Collect(param);
                    }
                }
            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiExcelDataCategory");
                return null;
            }

            return ans;
        }

        /// <summary>
        /// 検船Excel帳票章別指摘数データの取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MoiExcelOutputDataCategory GetMoiExcelDataCategory2(MoiExcelOutputParameter param, MsViqVersion version)
        {
            MoiExcelOutputDataCategory ans = null;
            try
            {
                using (DBConnect defcone = new DBConnect(DBConnectString))
                {
                    using (DBConnect wingcone = new DBConnect(ShipsWingDBConnectString))
                    {
                        //検船章別指摘数エクセル情報の検索収集
                        MoiExcelCategoryCollector moi = new MoiExcelCategoryCollector(wingcone.DBCone, defcone.DBCone);
                        ans = moi.Collect2(param, version);
                    }
                }
            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetMoiExcelDataCategory");
                return null;
            }

            return ans;
        }




        /// <summary>
        /// スケジュール一覧テンプレートファイルパス
        /// </summary>
        /// <returns></returns>
        public byte[] GetScheduleListTemplateFile()
        {
            byte[] data = null;

            try
            {
                //テンプレートファイルの読み取り
                data = File.ReadAllBytes(WebConfigManager.ScheduleListTemplateFilePath);

            }
            catch (Exception e)
            {
                SvcLog.WriteLog(e, "GetScheduleListTemplateFile");
                return null;
            }

            return data;
        }

        #endregion

        

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //以下マスタ
        #region マスタ
        
        /// <summary>
        /// 船マスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVessel> MsVessel_GetRecords()
        {
            List<MsVessel> anslist = new List<MsVessel>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsVessel.GetRecords(cone.DBCone);

                }
                catch(Exception e)
                {
                    SvcLog.WriteLog(e, "MsVessel_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// ユーザーマスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsUser> MsUser_GetRecords()
        {
            List<MsUser> anslist = new List<MsUser>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsUser.GetRecords(cone.DBCone);

                }
                catch(Exception e)
                {
                    SvcLog.WriteLog(e, "MsUser_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// 対象ユーザーの取得
        /// </summary>
        /// <param name="ms_user_id"></param>
        /// <returns></returns>
        public UserData UserData_GetDataByMsUserID(string ms_user_id)
        {
            UserData ans = null;
            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    ans = UserData.GetDataByMsUserID(cone.DBCone, ms_user_id);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsUser_GetRecords");
                    return null;
                }
            }

            return ans;
        }

        /// <summary>
        /// 検査種別マスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsItemKind> MsItemKind_GetRecords()
        {
            List<MsItemKind> anslist = new List<MsItemKind>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsItemKind.GetRecords(cone.DBCone);

                }
                catch(Exception e)
                {
                    SvcLog.WriteLog(e, "MsItemType_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsStatus一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsStatus> MsStatus_GetRecords()
        {
            List<MsStatus> anslist = new List<MsStatus>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MsStatus.GetRecords(cone.DBCone);

                }
                catch(Exception e)
                {
                    SvcLog.WriteLog(e, "MsStatus_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsAlarmColor一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAlarmColor> MsAlarmColor_GetRecords()
        {
            List<MsAlarmColor> anslist = new List<MsAlarmColor>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MsAlarmColor.GetRecords(cone.DBCone);

                }
                catch(Exception e)
                {
                    SvcLog.WriteLog(e, "MsAlarmColor_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// ActionCode一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsActionCode> MsActionCode_GetRecords()
        {
            List<MsActionCode> anslist = new List<MsActionCode>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsActionCode.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsActionCode_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// Deficiency Codeの取得
        /// </summary>
        /// <returns></returns>
        public List<MsDeficiencyCode> MsDeficiencyCode_GetRecords()
        {
            List<MsDeficiencyCode> anslist = new List<MsDeficiencyCode>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsDeficiencyCode.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsDeficiencyCode_GetRecords");
                }
            }

            return anslist;
        }
            
        /// <summary>
        /// 船種マスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselType> MsVesselType_GetRecords()
        {
            List<MsVesselType> anslist = new List<MsVesselType>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsVesselType.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsVesselType_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// 場所ポートマスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsBasho> MsBasho_GetRecords()
        {
            List<MsBasho> anslist = new List<MsBasho>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsBasho.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsBasho_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// 国マスタ取得
        /// </summary>
        /// <returns></returns>
        public List<MsRegional> MsRegional_GetRecords()
        {
            List<MsRegional> anslist = new List<MsRegional>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsRegional.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsRegional_GetRecords");
                }
            }

            return anslist;
        }
        
        /// <summary>
        /// MsAlarmColor_InsertRecordの挿入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int MsAlarmColor_InsertRecord(MsAlarmColor data, MsUser requser)
        {
            int ans = MsAlarmColor.EVal;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();


                    ans = data.InsertRecord(cone.DBCone, requser);
                    

                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "MsAlarmColor_InsertRecord");
                    return MsAlarmColor.EVal;
                }
            }


            return ans;

        }

        /// <summary>
        /// MsAlarmColorの更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MsAlarmColor_UpdateRecord(MsAlarmColor data, MsUser requser)
        {
            bool ans = false;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();


                    ans = data.UpdateRecord(cone.DBCone, requser);


                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "MsAlarmColor_UpdateRecord");
                    return false;
                }
            }


            return ans;

        }

        /// <summary>
        /// MsAlarmColorの削除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool MsAlarmColor_DeleteRecord(MsAlarmColor data, MsUser requser)
        {
            bool ans = false;

            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    cone.BeginTransaction();


                    ans = data.DeleteRecord(cone.DBCone, requser);


                    cone.ComitTransaction();

                }
                catch (Exception e)
                {
                    cone.RollbackTransaction();
                    SvcLog.WriteLog(e, "MsAlarmColor_DeleteRecord");
                    return false;
                }
            }


            return ans;

        }



        /// <summary>
        /// MsAccidentImportance一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentImportance> MsAccidentImportance_GetRecords()
        {
            List<MsAccidentImportance> anslist = new List<MsAccidentImportance>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsAccidentImportance.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsAccidentImportance_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// MsAccidentKind一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentKind> MsAccidentKind_GetRecords()
        {
            List<MsAccidentKind> anslist = new List<MsAccidentKind>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsAccidentKind.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsAccidentKind_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsAccidentSituation一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentSituation> MsAccidentSituation_GetRecords()
        {
            List<MsAccidentSituation> anslist = new List<MsAccidentSituation>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsAccidentSituation.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsAccidentSituation_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// MsAccidentStatus一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsAccidentStatus> MsAccidentStatus_GetRecords()
        {
            List<MsAccidentStatus> anslist = new List<MsAccidentStatus>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MsAccidentStatus.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsAccidentStatus_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsKindOfAccident一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsKindOfAccident> MsKindOfAccident_GetRecords()
        {
            List<MsKindOfAccident> anslist = new List<MsKindOfAccident>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsKindOfAccident.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsKindOfAccident_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// 検船種別の取得
        /// </summary>
        /// <returns></returns>
        public List<MsInspectionCategory> MsInspectionCategory_GetRecords()
        {
            List<MsInspectionCategory> anslist = new List<MsInspectionCategory>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsInspectionCategory.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsInspectionCategory_GetRecords");
                }
            }

            return anslist;
        }



        /// <summary>
        /// VIQコードの取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqCode> MsViqCode_GetRecords()
        {
            List<MsViqCode> anslist = new List<MsViqCode>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsViqCode.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsViqCode_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// ViqNoの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqNo> MsViqNo_GetRecords()
        {
            List<MsViqNo> anslist = new List<MsViqNo>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsViqNo.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsViqNo_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// VIQ Codeに関連するViqNoの取得
        /// </summary>
        /// <param name="viq_code_id"></param>
        /// <returns></returns>
        public List<MsViqNo> MsViqNo_GetRecordsByViqCodeID(int viq_code_id)
        {
            List<MsViqNo> anslist = new List<MsViqNo>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsViqNo.GetRecordsByViqCodeID(cone.DBCone, viq_code_id);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsViqNo_GetRecordsByViqCodeID");
                }
            }

            return anslist;
        }


        /// <summary>
        /// 検船状態の取得
        /// </summary>
        /// <returns></returns>
        public List<MsMoiStatus> MsMoiStatus_GetRecords()
        {
            List<MsMoiStatus> anslist = new List<MsMoiStatus>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MsMoiStatus.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsMoiStatus_GetRecords");
                }
            }

            return anslist;
        }



        /// <summary>
        /// 顧客一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecords()
        {
            List<MsCustomer> anslist = new List<MsCustomer>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsCustomer.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsCustomer_GetRecords");
                }
            }

            return anslist;
        }



        /// <summary>
        /// 検船会社一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecordsInspectionCompany()
        {
            List<MsCustomer> anslist = new List<MsCustomer>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsCustomer.GetRecordsInspectionCompany(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsCustomer_GetRecordsInspectionCompany");
                }
            }

            return anslist;

        }

        /// <summary>
        /// 申請先一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCustomer> MsCustomer_GetRecordsAppointedCompany()
        {
            List<MsCustomer> anslist = new List<MsCustomer>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsCustomer.GetRecordsAppointedCompany(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsCustomer_GetRecordsAppointedCompany");
                }
            }

            return anslist;
        }

        /// <summary>
        /// MsDeficiencyCategoryの取得
        /// </summary>
        /// <returns></returns>
        public List<MsDeficiencyCategory> MsDeficiencyCategory_GetRecords()
        {
            List<MsDeficiencyCategory> anslist = new List<MsDeficiencyCategory>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsDeficiencyCategory.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsDeficiencyCategory_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsYearの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsYear> MsYear_GetRecords()
        {
            List<MsYear> anslist = new List<MsYear>();


            using (DBConnect cone = new DBConnect(DBConnectString))
            {
                try
                {
                    anslist = MsYear.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsYear_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// MsScheduleCategory一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleCategory> MsScheduleCategory_GetRecords()
        {
            List<MsScheduleCategory> anslist = new List<MsScheduleCategory>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsScheduleCategory.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsScheduleCategory_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsScheduleKind一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleKind> MsScheduleKind_GetRecords()
        {
            List<MsScheduleKind> anslist = new List<MsScheduleKind>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsScheduleKind.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsScheduleKind_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// MsScheduleKindDetailの一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsScheduleKindDetail> MsScheduleKindDetail_GetRecords()
        {
            List<MsScheduleKindDetail> anslist = new List<MsScheduleKindDetail>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsScheduleKindDetail.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsScheduleKindDetail_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// DeficiencyCategoryData一覧取得
        /// </summary>
        /// <returns></returns>
        public List<DeficiencyCategoryData> DeficiencyCategoryData_GetDataList()
        {
            List<DeficiencyCategoryData> anslist = new List<DeficiencyCategoryData>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = DeficiencyCategoryData.GetDataList(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "DeficiencyCategoryData_GetDataList");
                }
            }

            return anslist;
        }


        /// <summary>
        /// MsViqCodeNameの取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqCodeName> MsViqCodeName_GetRecords()
        {
            List<MsViqCodeName> anslist = new List<MsViqCodeName>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsViqCodeName.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsViqCodeName_GetRecords");
                }
            }

            return anslist;
        }
        
        /// <summary>
        /// MsViqVersionの取得
        /// </summary>
        /// <returns></returns>
        public List<MsViqVersion> MsViqVersion_GetRecords()
        {
            List<MsViqVersion> anslist = new List<MsViqVersion>();


            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsViqVersion.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsViqVersion_GetRecords");
                }
            }

            return anslist;
        }

        /// <summary>
        /// 対象の船の検査有効可否の取得
        /// </summary>
        /// <param name="ms_vessel_id"></param>
        /// <returns></returns>
        public List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID(decimal ms_vessel_id)
        {
            List<MsVesselScheduleKindDetailEnable> anslist = new List<MsVesselScheduleKindDetailEnable>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsVesselScheduleKindDetailEnable.GetRecordsByMsVesselID(cone.DBCone, ms_vessel_id);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID");
                }
            }

            return anslist;
        }


        /// <summary>
        /// 全検査有効可否の取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecords()
        {
            List<MsVesselScheduleKindDetailEnable> anslist = new List<MsVesselScheduleKindDetailEnable>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsVesselScheduleKindDetailEnable.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsVesselScheduleKindDetailEnable_GetRecords");
                }
            }

            return anslist;
        }


        /// <summary>
        /// 船カテゴリ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsVesselCategory> MsVesselCategory_GetRecords()
        {
            List<MsVesselCategory> anslist = new List<MsVesselCategory>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsVesselCategory.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsVesselCategory_GetRecords");
                }
            }

            return anslist;
        }



        /// <summary>
        /// CrewMatrixマスタ一覧取得
        /// </summary>
        /// <returns></returns>
        public List<MsCrewMatrixType> MsCrewMatrixType_GetRecords()
        {
            List<MsCrewMatrixType> anslist = new List<MsCrewMatrixType>();

            using (DBConnect cone = new DBConnect(ShipsWingDBConnectString))
            {
                try
                {
                    anslist = MsCrewMatrixType.GetRecords(cone.DBCone);

                }
                catch (Exception e)
                {
                    SvcLog.WriteLog(e, "MsCrewMatrixType_GetRecords");
                }
            }

            return anslist;
        }

        #endregion


    }
}

