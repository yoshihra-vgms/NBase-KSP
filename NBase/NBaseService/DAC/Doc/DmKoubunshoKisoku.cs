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
        List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecords(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.DmKoubunshoKisoku DmKoubunshoKisoku_GetRecord(MsUser loginUser, string koubunshoKisokuID);
        
        [OperationContract]
        List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecordsByBunruiID(MsUser loginUser, string msDmBunruiID);
        
        [OperationContract]
        List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecordsByShoubunruiID(MsUser loginUser, string msDmShoubunruiID);

        [OperationContract]
        bool DmKoubunshoKisoku_InsertRecord(MsUser loginUser, DmKoubunshoKisoku info);

        [OperationContract]
        bool DmKoubunshoKisoku_UpdateRecord(MsUser loginUser, DmKoubunshoKisoku info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecords(MsUser loginUser)
        {
            return NBaseData.DAC.DmKoubunshoKisoku.GetRecords(loginUser);
        }

        public NBaseData.DAC.DmKoubunshoKisoku DmKoubunshoKisoku_GetRecord(MsUser loginUser, string koubunshoKisokuID)
        {
            return NBaseData.DAC.DmKoubunshoKisoku.GetRecord(loginUser, koubunshoKisokuID);
        }

        public List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecordsByBunruiID(MsUser loginUser, string msDmBunruiID)
        {
            return NBaseData.DAC.DmKoubunshoKisoku.GetRecordsByBunruiID(loginUser, msDmBunruiID);
        }

        public List<NBaseData.DAC.DmKoubunshoKisoku> DmKoubunshoKisoku_GetRecordsByShoubunruiID(MsUser loginUser, string msDmShoubunruiID)
        {
            return NBaseData.DAC.DmKoubunshoKisoku.GetRecordsByShoubunruiID(loginUser, msDmShoubunruiID);
        }

        public bool DmKoubunshoKisoku_InsertRecord(MsUser loginUser, DmKoubunshoKisoku info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool DmKoubunshoKisoku_UpdateRecord(MsUser loginUser, DmKoubunshoKisoku info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
