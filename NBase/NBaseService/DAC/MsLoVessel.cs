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
        List<MsLoVessel> MsLoVessel_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int vesselid);

        [OperationContract]
        bool MsLoVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLoVessel msLoVessel);

        [OperationContract]
        bool MsLoVessel_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLoVessel msLoVessel);

        [OperationContract]
        List<NBaseData.DAC.MsLoVessel> MsLoVessel_GetRecordsByMsVesselIDAndLoName(NBaseData.DAC.MsUser loginUser, int MsVesslID, string loName);

        [OperationContract]
        List<MsLoVessel> MsLoVessel_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id);
    }


    public partial class Service
    {
        //船ＩＤを指定し、関連するものを取得する
        public List<MsLoVessel> MsLoVessel_GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int vesselid)
        {
            List<MsLoVessel> ret = MsLoVessel.GetRecordsByMsVesselID(loginUser, vesselid);
             
            return ret;
        }

        public List<NBaseData.DAC.MsLoVessel> MsLoVessel_GetRecordsByMsVesselIDAndLoName(NBaseData.DAC.MsUser loginUser, int MsVesslID, string loName)
        {
            List<NBaseData.DAC.MsLoVessel> ret;
            ret = NBaseData.DAC.MsLoVessel.GetRecordsByMsVesselIDAndLoName(loginUser, MsVesslID, loName);
            return ret;
        }

        public bool MsLoVessel_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLoVessel msLoVessel)
        {
            msLoVessel.InsertRecord(loginUser);
            return true;
        }

        public bool MsLoVessel_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLoVessel msLoVessel)
        {
            msLoVessel.DeleteRecord(loginUser);
            return true;
        }

        public List<MsLoVessel> MsLoVessel_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            return MsLoVessel.GetRecordsByMsLoID(loginUser, ms_lo_id);
        }
    }

}