using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.BLC.状況確認一覧Row> BLC_状況確認一覧Row_SearchRecords(
            MsUser loginUser,
            List<string> bunruiIds,
            List<string> shoubunruiIds,
            List<int> vesselIds,
            string bunshoNo,
            string bunshoName,
            string bikou,
            DateTime issueFrom,
            DateTime issueTo,
            int vesselId,
            int role,
            string bumonId,
            int status,
            bool isKanryo,
            string keywords
            );
    }

    public partial class Service
    {
        public List<NBaseData.BLC.状況確認一覧Row> BLC_状況確認一覧Row_SearchRecords(MsUser loginUser,
            List<string> bunruiIds,
            List<string> shoubunruiIds,
            List<int> vesselIds,
            string bunshoNo,
            string bunshoName,
            string bikou,
            DateTime issueFrom,
            DateTime issueTo,
            int vesselId,
            int role,
            string bumonId,
            int status,
            bool isKanryo,
            string keywords
            )
        {
            return NBaseData.BLC.状況確認一覧Row.SearchRecords(loginUser, bunruiIds, shoubunruiIds, vesselIds, bunshoNo, bunshoName, bikou, issueFrom, issueTo, vesselId, role, bumonId, status, isKanryo, keywords);
        }
    }
}
