using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using NBaseData.DAC;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        NBaseData.DAC.OdChozoShousai OdChozoShousai_GetRecord(NBaseData.DAC.MsUser loginUser, string OdChozoShousaiID);

        [OperationContract]
        List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Date(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate);

        [OperationContract]
        List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Date_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, int syubetsu);

        [OperationContract]
        List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, int syubetsu);

        [OperationContract]
        List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_MsLoID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_lo_id);

        [OperationContract]
        List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_MsVesselItemID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_item_id);

        [OperationContract]
        bool OdChozoShousai_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdChozoShousai data);

        [OperationContract]
        List<OdChozoShousai> OdChozoShousai_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdChozoShousai> OdChozoShousai_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }

    public partial class Service
    {
        //id指定でデータを取得
        public NBaseData.DAC.OdChozoShousai OdChozoShousai_GetRecord(NBaseData.DAC.MsUser loginUser, string OdChozoShousaiID)
        {
            NBaseData.DAC.OdChozoShousai ret = NBaseData.DAC.OdChozoShousai.GetRecord(loginUser, OdChozoShousaiID);

            return ret;
        }

        /// <summary>
        /// 年月と船を指定して詳細品目を取得する。
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="vesselid"></param>
        /// <param name="sdate"></param>
        /// <returns></returns>
        public List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Date(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate)
        {
            List<NBaseData.DAC.OdChozoShousai> ret = NBaseData.DAC.OdChozoShousai.GetRecordsByVesselID_Date(loginUser, vesselid, sdate);

            return ret;
        }

        /// <summary>
        /// 船と年月と種別を指定してデータを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="vesselid"></param>
        /// <param name="sdate"></param>
        /// <param name="syubetsu"></param>
        /// <returns></returns>
        public List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Date_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, int syubetsu)
        {
            List<NBaseData.DAC.OdChozoShousai> ret = NBaseData.DAC.OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, vesselid, sdate, syubetsu);

            return ret;
        }

        /// <summary>
        /// 船と年月の期間と種別を指定してデータを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="vesselid"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="syubetsu"></param>
        /// <returns></returns>
        public List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, int syubetsu)
        {
            List<NBaseData.DAC.OdChozoShousai> ret
                = NBaseData.DAC.OdChozoShousai.GetRecordsByVesselID_Period_Shubetsu(loginUser, vesselid, start_date, end_date, syubetsu);

            return ret;
        }
        




        public List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_MsLoID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_lo_id)
        {
            return NBaseData.DAC.OdChozoShousai.GetRecordsByVesselID_Period_MsLoID(loginUser, vesselid, start_date, end_date, ms_lo_id);
        }


        public List<NBaseData.DAC.OdChozoShousai> OdChozoShousai_GetRecordsByVesselID_Period_MsVesselItemID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_item_id)
        {
            return NBaseData.DAC.OdChozoShousai.GetRecordsByVesselID_Period_MsVesselItemID(loginUser, vesselid, start_date, end_date, ms_item_id);
        }

        public bool OdChozoShousai_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdChozoShousai data)
        {
            return data.UpdateRecord(loginUser);
        }


        public List<OdChozoShousai> OdChozoShousai_GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            return OdChozoShousai.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }


        public List<OdChozoShousai> OdChozoShousai_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return OdChozoShousai.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
    }
}
