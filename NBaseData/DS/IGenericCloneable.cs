using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DS
{
    interface IGenericCloneable<T>
    {
        T Clone();
    }
}
