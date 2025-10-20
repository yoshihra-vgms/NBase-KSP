using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC回覧終了処理_登録(MsUser loginUser, DmKanryoInfo kanryoInfo);
    }

    public partial class Service
    {
        public bool BLC回覧終了処理_登録(MsUser loginUser, DmKanryoInfo kanryoInfo)
        {
            return 回覧終了処理.登録(loginUser, kanryoInfo);
        }
    }

}
