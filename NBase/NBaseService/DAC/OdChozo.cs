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
        //List<NBaseData.DAC.OdChozo> OdChozo_GetRecords(NBaseData.DAC.MsUser loginUser, string Date);

        //[OperationContract]
        //List<NBaseData.DAC.OdChozo> OdChozo_GetRecordsByVesselID_Nengetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate);

        //[OperationContract]
        //NBaseData.DAC.OdChozo OdChozo_GetRecord_Date_VesselID_Kind(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, int kind);

        [OperationContract]
        List<NBaseData.DAC.OdChozo> OdChozo_GetRecordsByShubetsu(NBaseData.DAC.MsUser loginUser, int shubetsu);

        [OperationContract]
        List<OdChozo> OdChozo_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int ms_vessel_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdChozo> OdChozo_GetRecords(NBaseData.DAC.MsUser loginUser, string Date)
        {
            List<NBaseData.DAC.OdChozo> ret = NBaseData.DAC.OdChozo.GetRecords(loginUser);

            return ret;
        }


        public List<NBaseData.DAC.OdChozo> OdChozo_GetRecordsByVesselID_Nengetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate)
        {
            List<NBaseData.DAC.OdChozo> ret = NBaseData.DAC.OdChozo.GetRecordsByVesselID_Nengetsu(loginUser, vesselid, sdate);

            return ret;
        }
        
        //年月と船ＩＤと種別を指定して取得
        public NBaseData.DAC.OdChozo OdChozo_GetRecord_Date_VesselID_Kind(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, int kind)
        {
            NBaseData.DAC.OdChozo ret = NBaseData.DAC.OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselid, sdate, kind);

            return ret;
        }


        //種別を指定して必要なものを取得        
        public List<NBaseData.DAC.OdChozo> OdChozo_GetRecordsByShubetsu(NBaseData.DAC.MsUser loginUser, int shubetsu)
        {
            List<NBaseData.DAC.OdChozo> ret = NBaseData.DAC.OdChozo.GetRecordsByShubetsu(loginUser, shubetsu);
            return ret;
        }



        public List<OdChozo> OdChozo_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int ms_vessel_id)
        {
            return OdChozo.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
