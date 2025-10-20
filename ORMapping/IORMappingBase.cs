using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMapping
{
    public interface IORMappingBase
    {
        string BaseSQL { get; set; }
        string WhereSQL { get; set; }
        string OrderSQL { get; set; }
    }
}
