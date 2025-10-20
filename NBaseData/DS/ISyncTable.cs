using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.DS
{
    public interface ISyncTable
    {
        int VesselID { get; set; }
        Int64 DataNo { get; set; }
        int SendFlag { get; set; }
        string UserKey { get; set; }
        string RenewUserID { get; set; }
        string Ts { get; set; }

        bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser);

        bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser);

        //bool Exists(MsUser loginUser);
        bool Exists(DBConnect dbConnect, MsUser loginUser);
    }
    public interface ISyncTableDoc : ISyncTable
    {
        //bool Exists(DBConnect dbConnect, MsUser loginUser);
    }
}
