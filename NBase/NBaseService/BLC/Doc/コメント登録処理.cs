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
        bool BLC_コメント登録処理_登録(MsUser loginUser, DmDocComment docComment, DmKanriKirokuFile kanriKirokuFile, DmKoubunshoKisokuFile koubunshoKisokuFile);
    }

    public partial class Service
    {
        public bool BLC_コメント登録処理_登録(MsUser loginUser, DmDocComment docComment, DmKanriKirokuFile kanriKirokuFile, DmKoubunshoKisokuFile koubunshoKisokuFile)
        {
            return コメント登録処理.登録(loginUser, docComment, kanriKirokuFile, koubunshoKisokuFile);
        }
    }

}
