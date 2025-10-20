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
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {        
        [OperationContract]
        List<NBaseData.DAC.OdFurikaeToritate> OdFurikaeToritate_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdFurikaeToritateFilter filter);

        [OperationContract]
        NBaseData.DAC.OdFurikaeToritate OdFurikaeToritate_GetRecord(NBaseData.DAC.MsUser loginUser, string odFurikaeToritateID);

        [OperationContract]
        bool OdFurikaeToritate_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdFurikaeToritate info);

        [OperationContract]
        bool OdFurikaeToritate_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdFurikaeToritate info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdFurikaeToritate> OdFurikaeToritate_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdFurikaeToritateFilter filter)
        {
            List<NBaseData.DAC.OdFurikaeToritate> ret;
            ret = NBaseData.DAC.OdFurikaeToritate.GetRecordsByFilter(loginUser, filter);
            return ret;
        }

        public NBaseData.DAC.OdFurikaeToritate OdFurikaeToritate_GetRecord(NBaseData.DAC.MsUser loginUser, string odFurikaeToritateID)
        {
            NBaseData.DAC.OdFurikaeToritate ret;
            ret = NBaseData.DAC.OdFurikaeToritate.GetRecord(loginUser, odFurikaeToritateID);
            return ret;
        }

        public bool OdFurikaeToritate_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdFurikaeToritate info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdFurikaeToritate_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdFurikaeToritate info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
