using System;
using System.Data;
using System.Configuration;
using System.Linq;
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
		List<MsCargoYusoItem> MsCargoYusoItem_GetRecords(MsUser loginUser);

        [OperationContract]
        MsCargoYusoItem MsCargoYusoItem_GetRecord(MsUser loginUser, int MsCargoYusoItemID);

        [OperationContract]
        MsCargoYusoItem MsCargoYusoItem_GetRecordByMsCargoID(MsUser loginUser, int MsCargoID);

        [OperationContract]
        bool MsCargoYusoItem_InsertRecord(MsUser loginUser, MsCargoYusoItem yusoItem);
    }

    public partial class Service
    {
        public List<MsCargoYusoItem> MsCargoYusoItem_GetRecords(MsUser loginUser)
        {
            return MsCargoYusoItem.GetRecords(loginUser);
        }

        public MsCargoYusoItem MsCargoYusoItem_GetRecord(MsUser loginUser, int MsCargoYusoItemID)
        {
            return MsCargoYusoItem.GetRecord(loginUser, MsCargoYusoItemID);
        }

        public MsCargoYusoItem MsCargoYusoItem_GetRecordByMsCargoID(MsUser loginUser, int MsCargoID)
        {
            return MsCargoYusoItem.GetRecordByMsCargoID(loginUser, MsCargoID);
        }

        public bool MsCargoYusoItem_InsertRecord(MsUser loginUser, MsCargoYusoItem yusoItem)
        {
            return yusoItem.InsertRecord(loginUser);
        }
    }
}
