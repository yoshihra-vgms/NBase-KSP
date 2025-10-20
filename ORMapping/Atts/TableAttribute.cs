using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMapping.Atts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }
        
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
