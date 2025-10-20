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
		List<MsCargo> MsCargo_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsCargo> MsCargo_GetRecordsByCargoNoAndCargoName(MsUser loginUser, string CargoNo, string CargoName, int MsYusoItemID);

        [OperationContract]
        bool MsCargo_ExistCargo(MsUser loginUser, string CargoNo);

        [OperationContract]
        bool MsCargo_InsertRecord(MsUser loginUser, MsCargo cargo);

        [OperationContract]
        bool MsCargo_UpdateRecord(MsUser loginUser, MsCargo cargo);

        [OperationContract]
        bool MsCargo_DeleteRecord(MsUser loginUse, MsCargo cargo);
        
    }

    public partial class Service
    {
        public List<MsCargo> MsCargo_GetRecords(MsUser loginUser)
        {
            return MsCargo.GetRecords(loginUser);
        }
        public List<MsCargo> MsCargo_GetRecordsByCargoNoAndCargoName(MsUser loginUser, string CargoNo, string CargoName, int MsYusoItemID)
        {
            return MsCargo.SearchRecords(loginUser, CargoNo, CargoName, MsYusoItemID);
        }

        public bool MsCargo_ExistCargo(MsUser loginUser, string CargoNo)
        {
            return MsCargo.ExistCargo(loginUser, CargoNo);
        }

        public bool MsCargo_InsertRecord(MsUser loginUser, MsCargo cargo)
        {
            cargo.UserKey = "0";
            cargo.RenewDate = DateTime.Now;
            cargo.RenewUserID = loginUser.MsUserID;
            return cargo.InsertRecord(loginUser);
        }
        public bool MsCargo_UpdateRecord(MsUser loginUser, MsCargo cargo)
        {
            cargo.RenewDate = DateTime.Now;
            cargo.RenewUserID = loginUser.MsUserID;
            return cargo.UpdateRecord(loginUser);
        }
        public bool MsCargo_DeleteRecord(MsUser loginUser, MsCargo cargo)
        {
            return cargo.DeleteRecord(loginUser);
        }

    }
}
