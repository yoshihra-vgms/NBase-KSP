using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping;

namespace NBaseData.DAC
{
    public class Sequences
    {
        public static int GetBgYosanHeadId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_BG_YOSAN_HEAD_ID");
        }

        public static int GetBgVesselYosanId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_BG_VESSEL_YOSAN_ID");
        }

        public static int GetMsVesselId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_MS_VESSEL_ID");
        }

        public static int GetMsSeninId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_MS_SENIN_ID");
        }

        public static int GetMsSiMenjouKindId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_MS_SI_MENJOU_KIND_ID");
        }

        public static int GetMsCargoId(DBConnect dbConnect, MsUser loginUser)
        {
            return GetSequenceId(dbConnect, loginUser, "SEQ_MS_CARGO_ID");
        }

        public static int GetSequenceId(DBConnect dbConnect, MsUser loginUser, string sequenceName)
        {
            //string SQL = "SELECT " + sequenceName + ".currval FROM DUAL";

            //return Decimal.ToInt32((decimal)(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, new ParameterConnection())));
            string SQL = "SELECT currval('" + sequenceName + "')";

            object o = DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, new ParameterConnection());
            return Convert.ToInt32(o);
        }
    }
}
