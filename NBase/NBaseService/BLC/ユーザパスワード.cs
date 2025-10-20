using System;
using System.Data;
using System.Configuration;
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
        ユーザパスワード.STATUS BLC_パスワード_パスワード変更(MsUser loginUser, MsUser ChangeUser, string Password);
    }

    public partial class Service
    {
        public ユーザパスワード.STATUS BLC_パスワード_パスワード変更(MsUser loginUser, MsUser ChangeUser, string Password)
        {
            ユーザパスワード logic = new ユーザパスワード();

            return logic.パスワード変更(loginUser, ChangeUser, Password);
        }
    }

}
