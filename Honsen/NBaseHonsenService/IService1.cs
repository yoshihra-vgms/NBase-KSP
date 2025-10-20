using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NBaseHonsenService
{
    // メモ: ここでインターフェイス名 "IService1" を変更する場合は、Web.config で "IService1" への参照も更新する必要があります。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // タスク: ここにサービス操作を追加します。       
        [OperationContract]
        string データ同期(
            int vesselSchemaVersion,
            string xml,
            decimal maxDataNoOfVesselIdZero,
            decimal maxDataNo,
            int vesselId,
            string hostName,
            string modueVersion,
            string userId,
            DateTime vesselDate,
            int curNo,
            int maxNo
            ,int counter
            );
        //string データ同期(
        //    int vesselSchemaVersion,
        //    string xml,
        //    decimal maxDataNoOfVesselIdZero,
        //    decimal maxDataNo,
        //    int vesselId,
        //    string hostName,
        //    string modueVersion,
        //    string userId,
        //    DateTime vesselDate
        //    );

        //[OperationContract]
        //string 文書データ同期(
        //    string xml,
        //    List<string> requestHoukokushoIds,
        //    List<string> requestKanriKirokuIds,
        //    List<string> requestKoubunshoKisokuIds,
        //    int vesselId,
        //    string userId
        //    );

        [OperationContract]
        string 文書データ同期_送信(
            string xml,
            int vesselId,
            string hostName,
            string userId
            );

        [OperationContract]
        string 文書データ同期_受信(
            List<string> requestOdAttachFileIds,
            List<string> requestHoukokushoIds,
            List<string> requestKanriKirokuIds,
            List<string> requestKoubunshoKisokuIds,
            int vesselId,
            string hostName,
            string userId
            );


        [OperationContract]
        string 添付ファイル同期_送信(
            string xml,
            int vesselId,
            string hostName,
            string userId
            );

        [OperationContract]
        string SyncMaster(
            int syncTableNo,
            decimal maxDataNo,
            int vesselId,
            string hostName,
            string userId,
            DateTime vesselDate,
            int counter
            );


        [OperationContract]
        string SyncData(
            string xml,
            int syncTableNo,
            decimal maxDataNo,
            int vesselId,
            string hostName,
            string userId,
            DateTime vesselDate,
            int counter
            );



        [OperationContract]
        string SyncSnParameter(
            int vesselId,
            string hostName
            );
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
