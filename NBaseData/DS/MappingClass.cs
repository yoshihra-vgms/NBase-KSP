using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Globalization;

namespace NBaseData.DS
{
    public class MappingClass
    {

        #region データテーブル＞objectのマッピング
        class MappingData
        {
            public string ColumnName;
            public System.Type type;
        }

        private static void Mapping(DataRow dr, object mappingObject)
        {
            DataTable dt = dr.Table;

            #region 準備
            #region マッピングデータのリスト作成(テーブル側)
            List<MappingData> MappingDatas = new List<MappingData>();
            for (int index = 0; index < dt.Columns.Count; index++)
            {
                MappingData md = new MappingData();

                md.ColumnName = dt.Columns[index].ColumnName; //SchemaRow.ItemArray[0] as string;
                md.type = dt.Columns[index].DataType; //SchemaRow.ItemArray[11] as System.Type;
                MappingDatas.Add(md);
            }
            #endregion

            #region プロパティの取得(クラス側)
            PropertyInfo[] propertyInfos = mappingObject.GetType().GetProperties();
            #endregion

            #endregion

            for (int index = 0; index < dt.Columns.Count; index++)
            {

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    ORMapping.Attrs.ColumnAttribute testAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ORMapping.Attrs.ColumnAttribute)) as ORMapping.Attrs.ColumnAttribute;

                    // 一致するカラムの判定
                    if (testAttribute != null && testAttribute.ColumnName == MappingDatas[index].ColumnName)
                    {
                        // オブジェクトからPropertyInfoを取得 > プロパティの値の設定に使用
                        PropertyInfo pi = mappingObject.GetType().GetProperty(propertyInfo.Name);

                        #region マッピングするデータの型を見て代入する値の型別に処理する
                        if (MappingDatas[index].type.FullName == "System.Int32")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, int.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, Convert.ToInt32(dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Int64")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, Int64.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, Convert.ToInt64(dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Decimal")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, decimal.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, Convert.ToDecimal(dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Int16")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, Int16.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, Convert.ToInt16(dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.String")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, string.Empty, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, (string)dr[index], null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.DateTime")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, DateTime.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, (DateTime)dr[index], null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Single")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, decimal.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, (decimal)((float)dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Double")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, decimal.MinValue, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, (decimal)((double)dr[index]), null);
                            }
                            break;
                        }
                        else if (MappingDatas[index].type.FullName == "System.Byte[]")
                        {
                            if (dr.IsNull(index))
                            {
                                pi.SetValue(mappingObject, null, null);
                            }
                            else
                            {
                                pi.SetValue(mappingObject, (byte[])dr[index], null);
                            }
                            break;
                        }
                        else
                        {
                            throw new Exception("フェッチするメソッドがありません");
                        }
                        #endregion
                    }
                }
            }
        }

        public static IEnumerable<ISyncTable> ToModel(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                List<ISyncTable> models = ToModel(table);

                foreach (ISyncTable m in models)
                {
                    yield return m;
                }
            }
        }

        public static List<ISyncTable> ToModel(DataTable dt)
        {
            List<ISyncTable> ret = new List<ISyncTable>();
            Type mappingType = Type.GetType("NBaseData.DAC." + ToClassName(dt.TableName));

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ISyncTable item = (ISyncTable)Activator.CreateInstance(mappingType);

                    Mapping(dr, item);

                    ret.Add(item);
                }
            }

            return (ret);
        }

        private static string ToClassName(string tableName)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            TextInfo ti = ci.TextInfo;
            
            return ti.ToTitleCase(tableName.ToLower()).Replace("_", "");
        }

        #endregion

    }
}
