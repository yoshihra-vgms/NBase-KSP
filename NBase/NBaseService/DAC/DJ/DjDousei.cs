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
        List<DjDousei> DjDousei_GetRecordsByHead(MsUser loginUser, MsVessel Vessel, DateTime FromDate, DateTime ToDate, int KikanrenkeiJisseki,
                                                 List<int> cargoIds, List<string> bashoIds, List<string> kichiIds, List<string> dairitenIds, List<string> ninushiIds);

        /// <summary>
        /// 新規挿入
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="djDousei"></param>
        /// <returns></returns>
        [OperationContract]
        bool DjDousei_InsertRecord(MsUser loginUser,DjDousei djDousei);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="djDousei"></param>
        /// <returns></returns>
        [OperationContract]
        bool DjDousei_UpdateRecord(MsUser loginUser, DjDousei djDousei);

        /// <summary>
        /// 詳細更新
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="djDousei"></param>
        /// <returns></returns>
        [OperationContract]
        bool DjDousei_UpdateDetailRecords(MsUser loginUser, DjDousei djDousei);

        [OperationContract]
        List<DjDousei> DjDousei_GetRecords(MsUser loginUser, int MsVesselID, DateTime? Date = null);

        [OperationContract]
        List<DjDousei> DjDousei_GetRecordsByMsKichiID(MsUser loginUser, string ms_kichi_id);

        [OperationContract]
        List<DjDousei> DjDousei_GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id);

        [OperationContract]
        List<DjDousei> DjDousei_GetRecordsBySameVoaygeNo(MsUser loginUser, string DjDouseiId);
    }

    public partial class Service
    {
        public List<DjDousei> DjDousei_GetRecordsByHead(MsUser loginUser, MsVessel Vessel, DateTime FromDate, DateTime ToDate, int KikanrenkeiJisseki,
                                                 List<int> cargoIds, List<string> bashoIds, List<string> kichiIds, List<string> dairitenIds, List<string> ninushiIds)
        {
            return DjDousei.GetRecordsByHead(loginUser, Vessel, FromDate, ToDate, KikanrenkeiJisseki,cargoIds, bashoIds, kichiIds, dairitenIds, ninushiIds);
        }

        public bool DjDousei_InsertRecord(MsUser loginUser, DjDousei djDousei)
        {
            return djDousei.InsertRecord(loginUser);
        }

        public bool DjDousei_UpdateRecord(MsUser loginUser, DjDousei djDousei)
        {
            return djDousei.UpdateRecord(loginUser);
        }

        public bool DjDousei_UpdateDetailRecords(MsUser loginUser, DjDousei djDousei)
        {
            return djDousei.UpdateDetailRecords(null, loginUser);
        }

        public List<DjDousei> DjDousei_GetRecords(MsUser loginUser, int MsVesselID, DateTime? Date = null)
        {
            if (Date == null)
            {
                return DjDousei.GetRecords(loginUser, MsVesselID);
            }
            else
            {
                return DjDousei.GetRecords(loginUser, MsVesselID, (DateTime)Date);
            }
        }

        public List<DjDousei> DjDousei_GetRecordsByMsKichiID(MsUser loginUser, string ms_kichi_id)
        {
            return DjDousei.GetRecordsByMsKichiID(loginUser, ms_kichi_id);
        }

        public List<DjDousei> DjDousei_GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id)
        {
            return DjDousei.GetRecordsByMsBashoID(loginUser, ms_basho_id);
        }

        public List<DjDousei> DjDousei_GetRecordsBySameVoaygeNo(MsUser loginUser, string djDouseiID)
        {
            DjDousei dousei = DjDousei.GetRecord(loginUser, djDouseiID);
            if (dousei == null)
            {
                return null;
            }
            return DjDousei.GetRecordsByVoaygeNo(loginUser, dousei);
        }

    }
}
