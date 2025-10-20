using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMapping
{
    class ORMappingUtils
    {
        private ORMappingUtils()
        {
        }
    
        public static object ValidateParamValue(object param)
        {
            if (param == null ||
            (param is string && (string)(param) == "") ||
            (param is short && (short)(param) == short.MinValue) ||
            (param is Int16 && (Int16)(param) == Int16.MinValue) ||
            (param is Int32 && (Int32)(param) == Int32.MinValue) ||
            (param is Int32 && (Int32)(param) == short.MinValue) ||
            (param is Int64 && (Int64)(param) == Int64.MinValue) ||
            (param is Decimal && (Decimal)(param) == Decimal.MinValue) ||
            (param is DateTime && (DateTime)(param) == DateTime.MinValue))
            {
                return DBNull.Value;
            }

            return param;
        }
    }
}
