using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseCommon.Nyukyo
{
    public class NoDataException : Exception
    {
        public NoDataException(string message)
            : base(message)
        {
        }
    }
}
