using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NBaseService
{
    // メモ: ここでインターフェイス名 "IService" を変更する場合は、Web.config で "IService" への参照も更新する必要があります。
    [ServiceContract]
    public partial interface IService
    {
        [OperationContract]
        string Test();
    }
}

