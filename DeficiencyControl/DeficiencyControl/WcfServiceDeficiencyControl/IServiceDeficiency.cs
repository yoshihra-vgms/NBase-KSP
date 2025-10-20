using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using DcCommon.Files;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using DcCommon.Output;


namespace WcfServiceDeficiencyControl
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IService1" を変更できます。
    [ServiceContract]
    public interface IServiceDeficiency
    {

        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);


        // TODO: ここにサービス操作を追加します。
        [OperationContract]
        DateTime GetServerDate();


        #region IGT Deficiency
        
        [OperationContract]
        UserData Login(string loginid, string password);

        [OperationContract]
        bool LogoutUser(MsUser user);

        [OperationContract]
        DcAttachment DcAttachment_DownloadAttachment(int attachment_id);
     
        [OperationContract]
        PSCInspectionData PSCInspectionData_GetDataByCommentItemID(int comment_item_id);


        [OperationContract]
        bool PSCInspectionData_InsertList(CommentData comhead, List<PSCInspectionData> datalist, MsUser requser);

        [OperationContract]
        List<PSCInspectionData> PSCInspectionData_GetDataListBySearchData(PscInspectionSearchData sdata);

        [OperationContract]
        bool PSCInspectionData_UpdateWithSister(CommentData comhead, PSCInspectionData pscdata, MsUser requser);

        [OperationContract]
        bool PSCInspectionData_Delete(int comment_item_id, MsUser requser);

        [OperationContract]
        bool DcCiPscInspection_UpdateStatus(List<DcCiPscInspection> psclist, MsUser requser);
        
        [OperationContract]
        List<DcAttachment> DcAttachment_GetARecrodsAllByCommentItemID(int comment_item_id);


        [OperationContract]
        List<DcAccident> DcAccident_GetRecordsBySearchData(AccidentSearchData sdata);

        //
        [OperationContract]
        AccidentData AccidentData_GetDataByAccidentID(int accident_id);

        [OperationContract]
        int AccidentData_InsertData(AccidentData data, MsUser requser);

        [OperationContract]
        bool AccidentData_UpdateData(AccidentData data, MsUser requser);

        [OperationContract]
        bool AccidentData_DeleteData(int accident_id, MsUser requser);

        [OperationContract]
        bool DcAccident_UpdateStatus(List<DcAccident> aclist, MsUser requser);

        [OperationContract]
        string CreateAccidentReportNo();

        [OperationContract]
        List<DcAttachment> DcAttachment_GetARecrodsAllByAccidentID(int accident_id);

        [OperationContract]
        List<MoiData> MoiData_GetDataListBySearchData(MoiSearchData sdata);

        [OperationContract]
        int Moi_Insert(MoiHeaderData hdata, List<MoiObservationData> obslist, MsUser requser);

        [OperationContract]
        MoiData MoiData_GetDataByMoiObservationID(int moi_observation_id);

        [OperationContract]
        List<DcAttachment> DcAttachment_GetARecrodsAllByMoi(int moi_observation_id);

        [OperationContract]
        bool MoiData_UpdatetData(MoiData data, MsUser requser);

        [OperationContract]
        bool MoiData_DeleteData(int moi_observation_id, MsUser requser);

        [OperationContract]
        bool DcMoiObservation_UpdateStatus(List<DcMoiObservation> obslist, MsUser requser);


        [OperationContract]
        byte[] GetAccidentExcelTemplate();

        [OperationContract]
        List<AccidentOutputData> GetAccidentOutputData(AccidentOutputParameter param);

        [OperationContract]
        List<DcSchedulePlan> DcSchedulePlan_GetRecordsBySearchData(SchedulePlanSearchData sdata);

        [OperationContract]
        bool DcSchedulePlan_InsertUpdateList(List<DcSchedulePlan> datalist, MsUser requser);


        [OperationContract]
        List<DcScheduleCompany> DcScheduleCompany_GetRecordsBySearchData(ScheduleCompanySearchData sdata);

        [OperationContract]
        List<DcScheduleOther> DcScheduleOther_GetRecordsBySearchData(ScheduleOtherSearchData sdata);


        [OperationContract]
        bool DcScheduleCompany_InsertUpdateList(List<DcScheduleCompany> datalist, MsUser requser);

        [OperationContract]
        bool DcScheduleOther_InsertUpdateList(List<DcScheduleOther> datalist, MsUser requser);

        [OperationContract]
        PscOutputData GetPSCOutputData(PscOutputParameter param);

        [OperationContract]
        byte[] GetPSCExcelTemplate();

        [OperationContract]
        byte[] GetMoiExcelCategoryTemplate();

        [OperationContract]
        byte[] GetMoiExcelListTemplate();

        [OperationContract]
        byte[] GetMoiReportCommentListTemplate();

        [OperationContract]
        byte[] GetMoiReportObservationTemplate();


        [OperationContract]
        MoiExcelOutputDataCategory GetMoiExcelDataCategory(MoiExcelOutputParameter param);

        [OperationContract]
        MoiExcelOutputDataCategory GetMoiExcelDataCategory2(MoiExcelOutputParameter param, MsViqVersion version);

        [OperationContract]
        byte[] GetScheduleListTemplateFile();
        #endregion
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //*********************************************************************************************************************************************************
        //マスタ
        #region マスタ
        [OperationContract]
        List<MsVessel> MsVessel_GetRecords();

        [OperationContract]
        List<MsUser> MsUser_GetRecords();

        [OperationContract]
        UserData UserData_GetDataByMsUserID(string ms_user_id);

        [OperationContract]
        List<MsItemKind> MsItemKind_GetRecords();

        [OperationContract]
        List<MsStatus> MsStatus_GetRecords();

        [OperationContract]
        List<MsAlarmColor> MsAlarmColor_GetRecords();


        [OperationContract]
        List<MsActionCode> MsActionCode_GetRecords();

        [OperationContract]
        List<MsDeficiencyCode> MsDeficiencyCode_GetRecords();

        [OperationContract]
        List<MsVesselType> MsVesselType_GetRecords();

        [OperationContract]
        List<MsBasho> MsBasho_GetRecords();

        [OperationContract]
        List<MsRegional> MsRegional_GetRecords();

        [OperationContract]
        int MsAlarmColor_InsertRecord(MsAlarmColor data, MsUser requser);

        [OperationContract]
        bool MsAlarmColor_UpdateRecord(MsAlarmColor data, MsUser requser);

        [OperationContract]
        bool MsAlarmColor_DeleteRecord(MsAlarmColor data, MsUser requser);

        

        [OperationContract]
        List<MsAccidentImportance> MsAccidentImportance_GetRecords();

        [OperationContract]
        List<MsAccidentKind> MsAccidentKind_GetRecords();

        [OperationContract]
        List<MsAccidentSituation> MsAccidentSituation_GetRecords();

        [OperationContract]
        List<MsAccidentStatus> MsAccidentStatus_GetRecords();

        [OperationContract]
        List<MsKindOfAccident> MsKindOfAccident_GetRecords();

        [OperationContract]
        List<MsInspectionCategory> MsInspectionCategory_GetRecords();

        [OperationContract]
        List<MsViqCode> MsViqCode_GetRecords();

        [OperationContract]
        List<MsViqNo> MsViqNo_GetRecords();


        [OperationContract]
        List<MsViqNo> MsViqNo_GetRecordsByViqCodeID(int viq_code_id);

        [OperationContract]
        List<MsMoiStatus> MsMoiStatus_GetRecords();

        [OperationContract]
        List<MsCustomer> MsCustomer_GetRecords();

        [OperationContract]
        List<MsCustomer> MsCustomer_GetRecordsInspectionCompany();

        [OperationContract]
        List<MsCustomer> MsCustomer_GetRecordsAppointedCompany();

        [OperationContract]
        List<MsDeficiencyCategory> MsDeficiencyCategory_GetRecords();

        [OperationContract]
        List<MsYear> MsYear_GetRecords();

        [OperationContract]
        List<MsScheduleCategory> MsScheduleCategory_GetRecords();

        [OperationContract]
        List<MsScheduleKind> MsScheduleKind_GetRecords();

        [OperationContract]
        List<MsScheduleKindDetail> MsScheduleKindDetail_GetRecords();

        [OperationContract]
        List<DeficiencyCategoryData> DeficiencyCategoryData_GetDataList();

        [OperationContract]
        List<MsViqCodeName> MsViqCodeName_GetRecords();

        [OperationContract]
        List<MsViqVersion> MsViqVersion_GetRecords();

        [OperationContract]
        List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecordsByMsVesselID(decimal ms_vessel_id);

        [OperationContract]
        List<MsVesselScheduleKindDetailEnable> MsVesselScheduleKindDetailEnable_GetRecords();

        [OperationContract]
        List<MsVesselCategory> MsVesselCategory_GetRecords();

        [OperationContract]
        List<MsCrewMatrixType> MsCrewMatrixType_GetRecords();

        #endregion

    }


    // サービス操作に複合型を追加するには、以下のサンプルに示すようにデータ コントラクトを使用します。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
