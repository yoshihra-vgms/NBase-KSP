using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ORMapping;

namespace NBaseService
{
    // メモ: ここでクラス名 "Service" を変更する場合は、Web.config で "Service" への参照も更新する必要があります。
    public partial class Service : IService
    {
        public string Test()
        {
            return "test";
        }

    }
}
