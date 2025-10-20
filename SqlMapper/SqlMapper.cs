using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Configuration;
using System.IO;
using ORMapping.Atts;
using ORMapping.Attrs;

namespace SqlMapper
{
    public class SqlMapper
    {
        private static readonly SqlMapper instance = new SqlMapper();
        public static string XML_DIR;

        static SqlMapper()
        {            
            XML_DIR = System.Configuration.ConfigurationManager.AppSettings["SqlMapperXmlDir"];
        }
        
        private SqlMapper()
        {
        }

        public static string GetSql(Type type, MethodBase methodBase)
        {
            return instance.InnerGetSql(type, methodBase.Name);
        }

        public static string GetSql(Type type, string name)
        {
            return instance.InnerGetSql(type, name);
        }

        private string InnerGetSql(Type type, string name)
        {
            if (XML_DIR == null)
            {
                XML_DIR = System.IO.Directory.GetCurrentDirectory() + "\\xml";
            }
            
            string mappingFileName = type.Name + "_" + DatabaseConfigManager.instance().Provider + ".xml";

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(XML_DIR + "\\" + mappingFileName, settings);

            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("sql"))
                        {
                            if (name.Equals(reader.GetAttribute("name")))
                            {
                                return reader.ReadString().Trim();
                            }
                        }
                    }
                }
            }

            return GetCommonSql(type, name);
        }

        private string GetCommonSql(Type type, string name)
        {
            string sql = null;

            string mappingFileName = "Common" + "_" + DatabaseConfigManager.instance().Provider + ".xml";

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(XML_DIR + "\\" + mappingFileName, settings);

            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("sql"))
                        {
                            if (name.Equals(reader.GetAttribute("name")))
                            {
                                sql = reader.ReadString().Trim();
                                break;
                            }
                        }
                    }
                }
            }

            if (sql != null)
            {
                sql = ReplaceTableName(type, sql);
                sql = ReplacePrimaryKey(type, sql);
            }
            
            return sql;
        }

        private static string ReplaceTableName(Type type, string sql)
        {
            TableAttribute tAttr = Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute;

            if (tAttr != null)
            {
                sql = sql.Replace("#tableName#", tAttr.TableName);
            }
            return sql;
        }

        private static string ReplacePrimaryKey(Type type, string sql)
        {
            foreach (PropertyInfo info in type.GetProperties())
            {
                ColumnAttribute cAttr = Attribute.GetCustomAttribute(info, typeof(ColumnAttribute)) as ColumnAttribute;

                if (cAttr != null && cAttr.PrimaryKey)
                {
                    sql = sql.Replace("#pk#", cAttr.ColumnName);
                }
            }
            return sql;
        }

        public static string CreateInnerSql(string prmPrefix, int paramLength)
        {
            return CreateInnerSql(prmPrefix, paramLength, 0);
        }
        
        private static string CreateInnerSql(string prmPrefix, int paramLength, int index)
        {
            //if (ORMapping.Common.DBTYPE == ORMapping.Common.DB_TYPE.POSTGRESQL)//(ORMapping.Common.DBTYPE == ORMapping.Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
            //{
            //    prmPrefix = ":" + prmPrefix;
            //}
            //else if (ORMapping.Common.DBTYPE == ORMapping.Common.DB_TYPE.POSTGRESQL_CLIENT)
            //{
            //    prmPrefix = ":" + prmPrefix;
            //}
            //else if (ORMapping.Common.DBTYPE == ORMapping.Common.DB_TYPE.SQLSERVER)
            //{
            //    prmPrefix = "@" + prmPrefix;
            //}
            prmPrefix = ":" + prmPrefix;
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < paramLength; i++)
            {
                sb.Append(i == 0 ? "" : ",");
                sb.Append(prmPrefix + (index + i).ToString());
            }
            return sb.ToString();
        }
    }
}
