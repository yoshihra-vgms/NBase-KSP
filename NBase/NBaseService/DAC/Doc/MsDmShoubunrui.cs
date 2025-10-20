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
        List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecordsByBunruiID(NBaseData.DAC.MsUser loginUser, string msDmBunruiID);

        [OperationContract]
        List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecordsByNameAndBunruiID(NBaseData.DAC.MsUser loginUser, string name, string msDmBunruiID);

        [OperationContract]
        NBaseData.DAC.MsDmShoubunrui MsDmShoubunrui_GetRecord(NBaseData.DAC.MsUser loginUser, string msDmShoubunruiId);

        [OperationContract]
        bool MsDmShoubunrui_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsDmShoubunrui info);

        [OperationContract]
        bool MsDmShoubunrui_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsDmShoubunrui info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return NBaseData.DAC.MsDmShoubunrui.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecordsByBunruiID(NBaseData.DAC.MsUser loginUser, string msDmBunruiID)
        {
            return NBaseData.DAC.MsDmShoubunrui.GetRecordsByBunruiID(loginUser, msDmBunruiID);
        }

        public List<NBaseData.DAC.MsDmShoubunrui> MsDmShoubunrui_GetRecordsByNameAndBunruiID(NBaseData.DAC.MsUser loginUser, string name, string msDmBunruiID)
        {
            return NBaseData.DAC.MsDmShoubunrui.GetRecordsByNameAndBunruiID(loginUser, name, msDmBunruiID);
        }

        public NBaseData.DAC.MsDmShoubunrui MsDmShoubunrui_GetRecord(NBaseData.DAC.MsUser loginUser, string msDmShoubunruiId)
        {
            return NBaseData.DAC.MsDmShoubunrui.GetRecord(loginUser, msDmShoubunruiId);
        }

        public bool MsDmShoubunrui_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsDmShoubunrui info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool MsDmShoubunrui_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsDmShoubunrui info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
