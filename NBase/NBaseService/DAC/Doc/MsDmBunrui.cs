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
        List<NBaseData.DAC.MsDmBunrui> MsDmBunrui_GetRecords(MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsDmBunrui> MsDmBunrui_GetRecordsByName(MsUser loginUser, string name);

        [OperationContract]
        NBaseData.DAC.MsDmBunrui MsDmBunrui_GetRecord(MsUser loginUser, string msDmBunruiId);

        [OperationContract]
        bool MsDmBunrui_InsertRecord(MsUser loginUser, MsDmBunrui info);

        [OperationContract]
        bool MsDmBunrui_UpdateRecord(MsUser loginUser, MsDmBunrui info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsDmBunrui> MsDmBunrui_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.MsDmBunrui.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.MsDmBunrui> MsDmBunrui_GetRecordsByName(MsUser loginUser, string name)
        {
            return NBaseData.DAC.MsDmBunrui.GetRecordsByName(loginUser, name);
        }

        public NBaseData.DAC.MsDmBunrui MsDmBunrui_GetRecord(MsUser loginUser, string msDmBunruiId)
        {
            return NBaseData.DAC.MsDmBunrui.GetRecord(loginUser, msDmBunruiId);
        }

        public bool MsDmBunrui_InsertRecord(MsUser loginUser, MsDmBunrui info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool MsDmBunrui_UpdateRecord(MsUser loginUser, MsDmBunrui info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
