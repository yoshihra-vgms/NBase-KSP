using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Globalization;
using ORMapping.Attrs;

namespace NBaseData.DS
{
    public class MappingClass2
    {
        private static void Mapping(DataRow dr, object mappingObject, Dictionary<string, PropertyInfo> propDic)
        {
            foreach (KeyValuePair<string, PropertyInfo> pair in propDic)
            {
                if (dr.Table.Columns.Contains(pair.Key))
                {
                    if (!dr.IsNull(pair.Key))
                    {
                        //pair.Value.SetValue(mappingObject, dr[pair.Key], null);
                        if (dr.Table.Columns[pair.Key].DataType == typeof(decimal))
                        {
                            Decimal val = (decimal)dr[pair.Key];

                            if (pair.Value.PropertyType.FullName == "System.Decimal" || pair.Value.PropertyType.FullName == "System.Single")
                            {
                                pair.Value.SetValue(mappingObject, val, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int64")
                            {
                                pair.Value.SetValue(mappingObject, (Int64)val, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int32")
                            {
                                pair.Value.SetValue(mappingObject, (Int32)val, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int16")
                            {
                                pair.Value.SetValue(mappingObject, (Int16)val, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Double")
                            {
                                pair.Value.SetValue(mappingObject, (Double)val, null);
                            }
                        }
                        else
                        {
                            pair.Value.SetValue(mappingObject, dr[pair.Key], null);
                        }
                    }
                    else
                    {
                        if (dr.Table.Columns[pair.Key].DataType == typeof(string))
                        {
                            pair.Value.SetValue(mappingObject, string.Empty, null);
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(int))
                        {
                            pair.Value.SetValue(mappingObject, int.MinValue, null);
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(DateTime))
                        {
                            pair.Value.SetValue(mappingObject, DateTime.MinValue, null);
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(decimal))
                        {
                            //pair.Value.SetValue(mappingObject, decimal.MinValue, null);

                            if (pair.Value.PropertyType.FullName == "System.Decimal" || pair.Value.PropertyType.FullName == "System.Single")
                            {
                                pair.Value.SetValue(mappingObject, decimal.MinValue, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int64")
                            {
                                pair.Value.SetValue(mappingObject, Int64.MinValue, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int32")
                            {
                                pair.Value.SetValue(mappingObject, Int32.MinValue, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Int16")
                            {
                                pair.Value.SetValue(mappingObject, Int16.MinValue, null);
                            }
                            else if (pair.Value.PropertyType.FullName == "System.Double")
                            {
                                pair.Value.SetValue(mappingObject, Double.MinValue, null);
                            }
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(Int64))
                        {
                            pair.Value.SetValue(mappingObject, Int64.MinValue, null);
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(Int16))
                        {
                            pair.Value.SetValue(mappingObject, Int16.MinValue, null);
                        }
                        else if (dr.Table.Columns[pair.Key].DataType == typeof(Single) ||
                            dr.Table.Columns[pair.Key].DataType == typeof(double))
                        {
                            pair.Value.SetValue(mappingObject, decimal.MinValue, null);
                        }
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

            if (dt != null)
            {
                Type mappingType = Type.GetType("NBaseData.DAC." + ToClassName(dt.TableName));

                Dictionary<string, PropertyInfo> propDic = CreatePropertyInfoDic(mappingType.GetProperties());

                foreach (DataRow dr in dt.Rows)
                {
                    ISyncTable item = (ISyncTable)Activator.CreateInstance(mappingType);

                    Mapping(dr, item, propDic);

                    ret.Add(item);
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

        private static string ToClassName(string tableName)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            TextInfo ti = ci.TextInfo;

            return ti.ToTitleCase(tableName.ToLower()).Replace("_", "");
        }
    }
}
