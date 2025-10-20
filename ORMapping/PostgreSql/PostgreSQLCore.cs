using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data;
using System.Reflection;
using ORMapping.Attrs;

namespace ORMapping.PostgreSql
{
    class PostgreSqlCore<T> where T : new()
    {
        private static DataTable SchemaTable = null;
        private List<MappingData> MappingDatas = new List<MappingData>();
        private List<PropertyInfoCache> _ColumnAttribute = new List<PropertyInfoCache>();

        public List<T> FillRecrods(string ConnectionString, string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect(ConnectionString))
            {
                return FillRecrods(Connection, UserName, SQL, Params);
            }
        }
        public List<T> FillRecrods(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return FillRecrods(Connection, UserName, SQL, Params);
            }
        }
        public List<T> FillRecrods(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            List<T> ret = new List<T>();
            DateTime start = DateTime.Now;

            if (Common.EXECUTE_READER_LOG == true)
            {
                //ログ保存
                LogWriter.Write(Connection, SQL, Params, UserName);
            }

            using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, Connection.NpgsqlConnection))
            {
                cmd.Transaction = Connection.NpgsqlTrans;
                foreach (DBParameter Parameter in Params)
                {
                    NpgsqlParameter p = new NpgsqlParameter(Parameter.ParameterName, Parameter.Param);
                    cmd.Parameters.Add(p);
                }

                //cmd.BindByName = true;
                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    MakeMappingTable(dr, typeof(T));
                    while (dr.Read())
                    {
                        T l = new T();
                        Mapping(dr, l);
                        ret.Add(l);
                    }
                }
            }
            TimeSpan ts = DateTime.Now - start;

            return ret;
        }


        /// <summary>
        /// マッピングの前処理を行う
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mappingType"></param>
        private void MakeMappingTable(NpgsqlDataReader dr, Type mappingType)
        {
            SchemaTable = dr.GetSchemaTable();
            for (int index = 0; index < SchemaTable.Rows.Count; index++)
            {
                DataRow SchemaRow = SchemaTable.Rows[index];

                MappingData md = new MappingData();

                md.ColumnName = SchemaRow.ItemArray[0] as string;
                md.type = SchemaRow.ItemArray[11] as System.Type;
                MappingDatas.Add(md);
            }

            PropertyInfo[] propertyInfos = mappingType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                ColumnAttribute testAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnAttribute)) as ColumnAttribute;

                if (testAttribute != null)
                {
                    _ColumnAttribute.Add(new PropertyInfoCache(propertyInfo,testAttribute));
                }
            }

        }

        public class PropertyInfoCache
        {
            public ColumnAttribute testAttribute;
            public PropertyInfo propertyInfo;

            public PropertyInfoCache(PropertyInfo p, ColumnAttribute c)
            {
                propertyInfo = p;
                testAttribute = c;
            }
        }
        /// <summary>
        /// マッピング処理を行う
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mappingObject"></param>
        private void Mapping(NpgsqlDataReader dr, object mappingObject)
        {
            int index;
            List<PropertyInfoCache> tmpPropertyInfos = new List<PropertyInfoCache>();

            //テンポラリーで使うPropertyInfoCacheを作成する
            foreach (PropertyInfoCache tt in _ColumnAttribute)
            {
                tmpPropertyInfos.Add(tt);
            }

            for (index = 0; index < SchemaTable.Rows.Count; index++)
            {

                foreach (PropertyInfoCache propertyInfoCache in tmpPropertyInfos)
                {
                    ColumnAttribute testAttribute = propertyInfoCache.testAttribute;
                    PropertyInfo propertyInfo = propertyInfoCache.propertyInfo;

                    if (testAttribute.ColumnName == MappingDatas[index].ColumnName.ToUpper())
                    {
                        PropertyInfo pi = mappingObject.GetType().GetProperty(propertyInfo.Name);
                        if (MappingDatas[index].type.FullName == "System.Int16")
                        {
                            pi.SetValue(mappingObject, GetInt16(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Int32")
                        {
                            pi.SetValue(mappingObject, GetInt32(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Int64")
                        {
                            pi.SetValue(mappingObject, GetInt64(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Decimal")
                        {
                            //pi.SetValue(mappingObject, GetDecimal(dr, index), null);
                            //tmpPropertyInfos.Remove(propertyInfoCache);
                            //break;

                            Decimal val = GetDecimal(dr, index);
                            if (pi.PropertyType.FullName == "System.Decimal" || pi.PropertyType.FullName == "System.Single")
                            {
                                pi.SetValue(mappingObject, val, null);
                            }
                            else if (pi.PropertyType.FullName == "System.Int64")
                            {
                                if (val == Decimal.MinValue)
                                {
                                    pi.SetValue(mappingObject, Int64.MinValue, null);
                                }
                                else
                                {
                                    pi.SetValue(mappingObject, (Int64)val, null);
                                }
                            }
                            else if (pi.PropertyType.FullName == "System.Int32")
                            {
                                if (val == Decimal.MinValue)
                                {
                                    pi.SetValue(mappingObject, Int32.MinValue, null);
                                }
                                else
                                {
                                    pi.SetValue(mappingObject, (Int32)val, null);
                                }
                            }
                            else if (pi.PropertyType.FullName == "System.Int16")
                            {
                                if (val == Decimal.MinValue)
                                {
                                    pi.SetValue(mappingObject, Int16.MinValue, null);
                                }
                                else
                                {
                                    pi.SetValue(mappingObject, (Int16)val, null);
                                }
                            }
                            else if (pi.PropertyType.FullName == "System.Double")
                            {
                                if (val == Decimal.MinValue)
                                {
                                    pi.SetValue(mappingObject, Double.MinValue, null);
                                }
                                else
                                {
                                    pi.SetValue(mappingObject, (Double)val, null);
                                }
                            }
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.String")
                        {
                            pi.SetValue(mappingObject, GetString(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.DateTime")
                        {
                            pi.SetValue(mappingObject, GetDateTime(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Single")
                        {
                            pi.SetValue(mappingObject, GetDecimal(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Double")
                        {
                            pi.SetValue(mappingObject, GetDouble(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Byte[]")
                        {
                            pi.SetValue(mappingObject, GetBytes(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Boolean")
                        {
                            pi.SetValue(mappingObject, GetBoolean(dr, index), null);
                            tmpPropertyInfos.Remove(propertyInfoCache);
                            break;
                        }
                        else
                        {
                            throw new Exception("フェッチするメソッドがありません: " + MappingDatas[index].type.FullName);
                        }
                    }
                }
            }
        }

        #region GetBoolean
        private bool GetBoolean(NpgsqlDataReader dr, int index)
        {
            bool ret = false;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetBoolean(index);
            }
            return ret;
        }

        private bool GetBoolean(NpgsqlDataReader dr, ref int index)
        {
            bool ret = false;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetBoolean(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetInt16
        private short GetInt16(NpgsqlDataReader dr, int index)
        {
            short ret = short.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt16(index);
            }
            return ret;
        }

        private short GetInt16(NpgsqlDataReader dr, ref int index)
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
        private int GetInt32(NpgsqlDataReader dr, int index)
        {
            int ret = int.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt32(index);
            }
            return ret;
        }

        private int GetInt32(NpgsqlDataReader dr, ref int index)
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
        private Int64 GetInt64(NpgsqlDataReader dr, int index)
        {
            int ret = int.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetInt64(index);
            }
            return ret;
        }

        private Int64 GetInt64(NpgsqlDataReader dr, ref int index)
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
        private decimal GetDecimal(NpgsqlDataReader dr, int index)
        {
            decimal ret = decimal.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetDecimal(index);
            }
            return ret;
        }

        private decimal GetDecimal(NpgsqlDataReader dr, ref int index)
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
        private string GetString(NpgsqlDataReader dr, int index)
        {
            string ret = "";
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetString(index);
            }
            return ret;
        }

        private string GetString(NpgsqlDataReader dr, ref int index)
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
        private DateTime GetDateTime(NpgsqlDataReader dr, int index)
        {
            DateTime ret = DateTime.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetDateTime(index);
            }
            return ret;
        }

        private DateTime GetDateTime(NpgsqlDataReader dr, ref int index)
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

        #region GetDouble
        private double GetDouble(NpgsqlDataReader dr, int index)
        {
            double ret = double.MinValue;
            if (dr.IsDBNull(index) == false)
            {
                return dr.GetDouble(index);
            }
            return ret;
        }

        private double GetDouble(NpgsqlDataReader dr, ref int index)
        {
            double ret = double.MinValue;
            int ref_index = index;
            index++;
            if (dr.IsDBNull(ref_index) == false)
            {
                return dr.GetDouble(ref_index);
            }
            return ret;
        }
        #endregion

        #region GetBytes
        private byte[] GetBytes(NpgsqlDataReader dr, int index)
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

        private byte[] GetBytes(NpgsqlDataReader dr, ref int index)
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

        private string GetExecuteSQL(IORMappingBase orMappingBase)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} ", orMappingBase.BaseSQL.Replace('\n', ' '), orMappingBase.OrderSQL);

            return sb.ToString();
        }
    }
}
