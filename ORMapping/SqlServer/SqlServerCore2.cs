using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlServerCe;
using ORMapping.Attrs;

namespace ORMapping.SqlServer
{
    class SqlServerCore2<T> where T : new()
    {
        private static DataTable SchemaTable = null;
        private List<MappingData> MappingDatas = new List<MappingData>();
        private PropertyInfo[] propertyInfos;

        public List<T> FillRecrods(string UserName, string SQL,ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return FillRecrods(Connection, UserName, SQL, Params);
            }
        }


        public List<T> FillRecrods(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            List<T> ret = new List<T>();

            using (SqlCeCommand cmd = new SqlCeCommand(SQL, Connection.SqlServerConnection))
            {
                cmd.Transaction = Connection.SqlTransaction;
                foreach (DBParameter Parameter in Params)
                {
                    SqlCeParameter p = new SqlCeParameter(Parameter.ParameterName, Parameter.Param);
                    cmd.Parameters.Add(p);
                }
                try
                {
                    using (SqlCeDataReader dr = cmd.ExecuteReader())
                    {
                        Dictionary<string, PropertyInfo> propDic = CreatePropertyInfoDic(typeof(T).GetProperties());

                        while (dr.Read())
                        {
                            T l = new T();

                            Mapping(dr, l, propDic);

                            ret.Add(l);
                        }
                    }
                }
                catch (Exception E)
                {
                    Console.WriteLine("Error：" + E.Message);
                }
            }

            return ret;
        }


        private static Dictionary<string, PropertyInfo> CreatePropertyInfoDic(PropertyInfo[] propertyInfos)
        {
            Dictionary<string, PropertyInfo> propDic = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                ColumnAttribute colAttr = Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnAttribute)) as ColumnAttribute;

                if (colAttr != null)
                {
                    propDic[colAttr.ColumnName] = propertyInfo;
                }
            }

            return propDic;
        }


        /// <summary>
        /// マッピング処理を行う
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mappingObject"></param>
        private void Mapping(SqlCeDataReader dr, object mappingObject, Dictionary<string, PropertyInfo> propDic)
        {
             for (int i = 0; i < dr.GetSchemaTable().Rows.Count; i++)
            {
                 string columnName = dr.GetSchemaTable().Rows[i].ItemArray[0] as string;
                 SqlDbType type = (dr.GetSchemaTable().Rows[i].ItemArray[11] as SqlCeType).SqlDbType;

                if (propDic.ContainsKey(columnName))
                {
                    if (type == SqlDbType.NVarChar)
                    {
                        propDic[columnName].SetValue(mappingObject, GetString(dr, i), null);
                    }
                    else if (type == SqlDbType.Int)
                    {
                        propDic[columnName].SetValue(mappingObject, GetInt32(dr, i), null);
                    }
                    else if (type == SqlDbType.DateTime)
                    {
                        propDic[columnName].SetValue(mappingObject, GetDateTime(dr, i), null);
                    }
                    else if (type == SqlDbType.Decimal)
                    {
                        propDic[columnName].SetValue(mappingObject, GetDecimal(dr, i), null);
                    }
                    else if (type == SqlDbType.BigInt)
                    {
                        propDic[columnName].SetValue(mappingObject, GetInt64(dr, i), null);
                    }
                    else if (type == SqlDbType.SmallInt)
                    {
                        propDic[columnName].SetValue(mappingObject, GetInt16(dr, i), null);
                    }
                    else if (type == SqlDbType.Image)
                    {
                        propDic[columnName].SetValue(mappingObject, GetBytes(dr, i), null);
                    }
                }
            }
        }

        #region GetInt16
        private short GetInt16(SqlCeDataReader dr, int index)
        {
            short ret = short.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt16(index);
            }
            return ret;
        }

        private short GetInt16(SqlCeDataReader dr, ref int index)
        {
            short ret = short.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetInt16(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetInt32
        private int GetInt32(SqlCeDataReader dr, int index)
        {
            int ret = int.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt32(index);
            }
            return ret;
        }

        private int GetInt32(SqlCeDataReader dr, ref int index)
        {
            int ret = int.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetInt32(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetInt64
        private Int64 GetInt64(SqlCeDataReader dr, int index)
        {
            int ret = int.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt64(index);
            }
            return ret;
        }

        private Int64 GetInt64(SqlCeDataReader dr, ref int index)
        {
            int ret = int.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetInt64(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetDecimal
        private decimal GetDecimal(SqlCeDataReader dr, int index)
        {
            decimal ret = decimal.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetDecimal(index);
            }
            return ret;
        }

        private decimal GetDecimal(SqlCeDataReader dr, ref int index)
        {
            decimal ret = decimal.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetDecimal(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetString
        private string GetString(SqlCeDataReader dr, int index)
        {
            string ret = "";
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetString(index);
            }
            return ret;
        }

        private string GetString(SqlCeDataReader dr, ref int index)
        {
            string ret = "";
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetString(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetDateTime
        private DateTime GetDateTime(SqlCeDataReader dr, int index)
        {
            DateTime ret = DateTime.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetDateTime(index);
            }
            return ret;
        }

        private DateTime GetDateTime(SqlCeDataReader dr, ref int index)
        {
            DateTime ret = DateTime.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetDateTime(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetBytes
        private byte[] GetBytes(SqlCeDataReader dr, int index)
        {
            if (dr.IsDBNull(index) == false)
            {
                //byte[] byteArray = new byte[Common.MAX_BINARY_SIZE];
                //long b = dr.GetBytes(index, 0, byteArray, 0, byteArray.Length);
                //return byteArray;
                byte[] tmp = new byte[Common.MAX_BINARY_SIZE];
                long b = dr.GetBytes(index, 0, tmp, 0, tmp.Length);
                byte[] byteArray = new byte[b];
                Array.Copy(tmp, byteArray, b);
                return byteArray;
            }

            return null;
        }

        private byte[] GetBytes(SqlCeDataReader dr, ref int index)
        {
            int ref_index = index;
            index++;

            if (dr.IsDBNull(ref_index) == false)
            {
                //byte[] byteArray = new byte[Common.MAX_BINARY_SIZE];
                //long b = dr.GetBytes(index, 0, byteArray, 0, byteArray.Length);
                //return byteArray;
                byte[] tmp = new byte[Common.MAX_BINARY_SIZE];
                long b = dr.GetBytes(index, 0, tmp, 0, tmp.Length);
                byte[] byteArray = new byte[b];
                Array.Copy(tmp, byteArray, b);
                return byteArray;
            }

            return null;
        }
        #endregion
    }
}
