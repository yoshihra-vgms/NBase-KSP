using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<KsKensa> KsKensa_GetRecords(MsUser loginUser);

        [OperationContract]
        KsKensa KsKensa_GetRecord(MsUser loginUser, string kensa_id);

        [OperationContract]
        List<KsKensa> KsKensa_GetRecordsBy船ID(MsUser loginUser, int msvessel_id);

        [OperationContract]
        List<KsKensa> KsKensa_GetRecordsBy船ID予定データ(MsUser loginUser, int msvessel_id);

        [OperationContract]
        bool KsKensa_InsertRecord(MsUser loginUser, KsKensa data);

        [OperationContract]
        bool KsKensa_UpdateRecord(MsUser loginUser, KsKensa data);

        [OperationContract]
        List<KsKensa> KsKensa_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id);
    }

    public partial class Service
    {
        public List<KsKensa> KsKensa_GetRecords(MsUser loginUser)
        {
            return KsKensa.GetRecords(loginUser);
        }


        public KsKensa KsKensa_GetRecord(MsUser loginUser, string kensa_id)
        {
            return KsKensa.GetRecord(loginUser, kensa_id);
        }

        public List<KsKensa> KsKensa_GetRecordsBy船ID(MsUser loginUser, int msvessel_id)
        {
            return KsKensa.GetRecordsBy船ID(loginUser, msvessel_id);
        }

        /// <summary>
        /// 実績登録のない船ID指定の全予定データの取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public List<KsKensa> KsKensa_GetRecordsBy船ID予定データ(MsUser loginUser, int msvessel_id)
        {
            return KsKensa.GetRecordsBy船ID予定データ(loginUser, msvessel_id);
        }

        /// <summary>
        /// 挿入
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="data"></param>
        /// <returns></returns>        
        public bool KsKensa_InsertRecord(MsUser loginUser, KsKensa data)
        {
            return data.InsertRecord(loginUser);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool KsKensa_UpdateRecord(MsUser loginUser, KsKensa data)
        {
            return data.UpdateRecord(loginUser);
        }



        public List<KsKensa> KsKensa_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id)
        {
            return KsKensa.GetRecordsByMsKensaID(loginUser, ms_kensa_id);
        }
    }
}
