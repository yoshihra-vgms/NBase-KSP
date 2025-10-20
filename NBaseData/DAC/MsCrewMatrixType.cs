using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    public class MsCrewMatrixType
    {
        #region データメンバ
        /// <summary>
        /// 船タイプID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CREW_MATRIX_TYPE_ID")]
        public int MsCrewMatrixTypeID { get; set; }

        /// <summary>
        /// 船タイプ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TYPE_NAME")]
        public string TypeName { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public String RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        #endregion

        public override string ToString()
        {
            return TypeName;
        }

        public MsCrewMatrixType()
        {
        }

        public static List<MsCrewMatrixType> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCrewMatrixType), MethodBase.GetCurrentMethod());
            List<MsCrewMatrixType> ret = new List<MsCrewMatrixType>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCrewMatrixType> mapping = new MappingBase<MsCrewMatrixType>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }
    }
}
