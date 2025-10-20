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
		List<MsYusoItem> MsYusoItem_GetRecords(MsUser loginUser);
        
        [OperationContract]
        List<MsYusoItem> MsYusoItem_GetRecordsByYusoItemName(MsUser loginUser, string yusoItemName);

        [OperationContract]
        MsYusoItem MsYusoItem_GetRecord(MsUser loginUser, int MsYusoItemID);

        [OperationContract]
        bool MsYusoItem_InsertRecord(MsUser loginUser, MsYusoItem yusoItem);

        [OperationContract]
        bool MsYusoItem_UpdateRecord(MsUser loginUser, MsYusoItem yusoItem);

        [OperationContract]
        bool MsYusoItem_DeleteRecord(MsUser loginUse, MsYusoItem yusoItem);
        
    }

    public partial class Service
    {
        public List<MsYusoItem> MsYusoItem_GetRecords(MsUser loginUser)
        {
            return MsYusoItem.GetRecords(loginUser);
        }

        public List<MsYusoItem> MsYusoItem_GetRecordsByYusoItemName(MsUser loginUser, string yusoItemName)
        {
            return MsYusoItem.GetRecordsByYusoItemName(loginUser, yusoItemName);
        }

        public MsYusoItem MsYusoItem_GetRecord(MsUser loginUser, int MsYusoItemID)
        {
            return MsYusoItem.GetRecord(loginUser, MsYusoItemID);
        }

        public bool MsYusoItem_InsertRecord(MsUser loginUser, MsYusoItem yusoItem)
        {
            return yusoItem.InsertRecord(loginUser);
        }
        public bool MsYusoItem_UpdateRecord(MsUser loginUser, MsYusoItem yusoItem)
        {
            return yusoItem.UpdateRecord(loginUser);
        }
        public bool MsYusoItem_DeleteRecord(MsUser loginUser, MsYusoItem yusoItem)
        {
            return yusoItem.DeleteRecord(loginUser);
        }

    }
}
