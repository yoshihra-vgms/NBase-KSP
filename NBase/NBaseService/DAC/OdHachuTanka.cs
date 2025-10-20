using System;
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
        //[OperationContract]
        //List<OdHachuTanka> OdHachuTanka_GetRecords(MsUser loginUser);

        [OperationContract]
        OdHachuTanka OdHachuTanka_GetRecordByMsLoID_MsVesselItemID_Date(NBaseData.DAC.MsUser loginUser, string mslovesselid, string msvesselitemid, DateTime startdate, DateTime enddate);

        [OperationContract]
        List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID_Date_LO(NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate);

        [OperationContract]
        List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID_Date_VesselItem(NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate);

        [OperationContract]
        List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID(MsUser loginUser, string ms_vesselitem_id);

        [OperationContract]
        List<OdHachuTanka> OdHachuTanka_GetRecordsByMaLoID(MsUser loginUser, string ms_lo_id);
    }


    public partial class Service
    {
        public List<OdHachuTanka> OdHachuTanka_GetRecords(MsUser loginUser)
        {
            List<OdHachuTanka> ret = OdHachuTanka.GetRecords(loginUser);

            return ret;
        }

        /// <summary>
        /// 指定ＩＤの最終単価取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="mslovesselid"></param>
        /// <param name="msvesselitemid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public OdHachuTanka OdHachuTanka_GetRecordByMsLoID_MsVesselItemID_Date
            (NBaseData.DAC.MsUser loginUser, string mslovesselid, string msvesselitemid, DateTime startdate, DateTime enddate)
        {

            OdHachuTanka ret = OdHachuTanka.GetRecordByMsLoID_MsVesselItemID_Date
                (loginUser, mslovesselid, msvesselitemid, startdate, enddate);

            return ret;
        }


        /// <summary>
        /// 指定船と期間を指定して
        /// 指定船対応潤滑油の最終単価を取得する。
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msvesselid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID_Date_LO
            (NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate)
        {
            List<OdHachuTanka> ret = OdHachuTanka.GetRecordsByMsVesselItemID_Date_LO(loginUser, msvesselid,
                startdate, enddate);

            return ret;
        }

        /// <summary>
        /// 指定船と期間を指定して船用品の最終単価を取得する。
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msvesselid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID_Date_VesselItem
            (NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate)
        {
            List<OdHachuTanka> ret = OdHachuTanka.GetRecordsByMsVesselItemID_Date_VesselItem(loginUser, msvesselid,
                startdate, enddate);

            return ret;

        }

        public List<OdHachuTanka> OdHachuTanka_GetRecordsByMsVesselItemID(MsUser loginUser, string ms_vesselitem_id)
        {
            return OdHachuTanka.GetRecordsByMsVesselItemID(loginUser, ms_vesselitem_id);
        }


        public List<OdHachuTanka> OdHachuTanka_GetRecordsByMaLoID(MsUser loginUser, string ms_lo_id)
        {
            return OdHachuTanka.GetRecordsByMaLoID(loginUser, ms_lo_id);
        }
    }
}
