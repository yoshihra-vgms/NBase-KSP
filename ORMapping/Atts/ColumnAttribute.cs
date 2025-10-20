using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMapping.Attrs
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; private set; }
        public bool PrimaryKey { get; private set; }
        
        public ColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
        
        public ColumnAttribute(string columnName, bool primaryKey)
        {
            ColumnName = columnName;
            PrimaryKey = primaryKey;
        }
    }
}
