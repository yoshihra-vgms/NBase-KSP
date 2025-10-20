using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace ORMapping.Oracle
{
    public class OracleBase
    {
        public static OracleDataReader ExecuteReader(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteReader(Connection, UserName, SQL, Params);
            }
        }
        public static OracleDataReader ExecuteReader(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                ////ログ保存
                //LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (OracleCommand cmd = new OracleCommand(SQL, Connection.OracleConnection))
            {
                cmd.Transaction = Connection.OracleTrans;
                foreach (DBParameter Parameter in Params)
                {
                    OracleParameter p = new OracleParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                cmd.BindByName = true;

                return cmd.ExecuteReader();
            }
        }

        public static int ExecuteNonQuery(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteNonQuery(Connection, UserName, SQL, Params);
            }

        }
        public static int ExecuteNonQuery(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                ////ログ保存 
                //LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (OracleCommand cmd = new OracleCommand(SQL, Connection.OracleConnection))
            {
                cmd.Transaction = Connection.OracleTrans;
                foreach (DBParameter Parameter in Params)
                {
                    OracleParameter p = new OracleParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                cmd.BindByName = true;

                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteScalar(Connection, UserName, SQL, Params);
            }

        }
        public static object ExecuteScalar(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                ////ログ保存 
                //LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (OracleCommand cmd = new OracleCommand(SQL, Connection.OracleConnection))
            {
                cmd.Transaction = Connection.OracleTrans;
                foreach (DBParameter Parameter in Params)
                {
                    OracleParameter p = new OracleParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                cmd.BindByName = true;

                return cmd.ExecuteScalar();
            }
        }

        //public static void StoredTest()
        //{
        //    string SQL = "KYDTI01010";
        //    using (DBConnect Connection = new DBConnect())
        //    {
        //        using (OracleCommand cmd = new OracleCommand(SQL, Connection.OracleConnection))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new OracleParameter("p_KISY_CD", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
        //            cmd.Parameters.Add(new OracleParameter("p_TNTSY_CD", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
        //            cmd.Parameters.Add(new OracleParameter("p_UPDUSERID", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
        //            cmd.Parameters.Add(new OracleParameter("ret", OracleDbType.Varchar2, 20, "", System.Data.ParameterDirection.Output));

        //            cmd.Parameters["p_KISY_CD"].Value = "180";
        //            cmd.Parameters["p_TNTSY_CD"].Value = "990389";
        //            cmd.Parameters["p_UPDUSERID"].Value = "horiok-y3";

        //            cmd.ExecuteNonQuery();

        //            string ret = cmd.Parameters["ret"].Value.ToString();
        //        }
        //    }
        //}


        public static string ExecuteProcedure(DBConnect Connection, string UserName, string SQL, ParameterConnection InParams)
        {
            string ret = null;
            using (OracleCommand cmd = new OracleCommand(SQL, Connection.OracleConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (DBParameter Parameter in InParams)
                {
                    OracleParameter inPrm = new OracleParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    inPrm.Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add(inPrm);
                }
                OracleParameter outPrm = new OracleParameter("ret", OracleDbType.Varchar2, 20);
                outPrm.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outPrm);

                cmd.ExecuteNonQuery();

                ret = cmd.Parameters["ret"].Value.ToString();
            }
            return ret;
        }
    }
}
