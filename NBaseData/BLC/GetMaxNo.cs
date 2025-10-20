using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;
using ORMapping.Attrs;

namespace NBaseData.BLC
{
    [DataContract()]
    public class GetMaxNo
    {
        [DataMember]
        [ColumnAttribute("MAX_NO")]
        public string MaxNo { get; set; }

        /// <summary>
        /// 手配依頼番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxTehaiIraiNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_TEHAI_IRAI_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 見積依頼番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxMmNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_MM_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 見積回答番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxMkNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_MK_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 受領番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxJryNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_JRY_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 落成番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        //public static GetMaxNo GetMaxRksNo(NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
        //    List<GetMaxNo> ret = new List<GetMaxNo>();
        //    ParameterConnection Params = new ParameterConnection();
        //    MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

        //    Params.Add(new DBParameter("NO_LENGTH", NoLength));
        //    Params.Add(new DBParameter("BASE_SHR_NO", BaseNo));
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    if (ret.Count == 0)
        //        return null;
        //    return ret[0];
        //}

        /// <summary>
        /// 支払番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxShrNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_SHR_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 集約管理番号の最大値を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="BaseNo"></param>
        /// <returns></returns>
        public static GetMaxNo GetMaxSumKanriNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int NoLength, string BaseNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(GetMaxNo), MethodBase.GetCurrentMethod());
            List<GetMaxNo> ret = new List<GetMaxNo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<GetMaxNo> mapping = new MappingBase<GetMaxNo>();

            Params.Add(new DBParameter("NO_LENGTH", NoLength));
            Params.Add(new DBParameter("BASE_SHR_NO", BaseNo));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
    }
}
