using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_ユーザ情報更新処理_新規作成(NBaseData.DAC.MsUser loingUser,NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin);

        [OperationContract]
        bool BLC_ユーザ情報更新処理_削除(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin);

        [OperationContract]
        bool BLC_ユーザ情報更新処理_更新(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin);

    }

    public partial class Service
    {
        public bool BLC_ユーザ情報更新処理_新規作成(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin)
        {
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = user.InsertRecord(dbConnect, loginUser);
                if (ret)
                {
                    if (Ubumon.MsBumonID == "")
                    {
                    }
                    else
                    {
                        ret = Ubumon.InsertRecord(dbConnect, loginUser);
                    }
                }
                if (ret)
                {
                    if (user.UserKbn == 1 && msSenin != null)
                    {
                        // 2013.09.27 Add:
                        msSenin = NBaseData.DAC.MsSenin.GetRecord(dbConnect, loginUser, msSenin.MsSeninID);

                        msSenin.MsUserID = user.MsUserID;
                        msSenin.RenewDate = DateTime.Now;
                        msSenin.RenewUserID = loginUser.MsUserID;
                        ret = msSenin.UpdateRecord(dbConnect, loginUser);
                    }
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }

            return ret;
        }

        public bool BLC_ユーザ情報更新処理_削除(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin)
        {
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = Ubumon.DeleteRecord(dbConnect, loginUser);

                // ユーザ情報削除時は、ログインID、パスワードをクリアしたほうが良いのでは？
                //user.LoginID = null;
                //user.Password = null;
                ret = user.DeleteFlagRecord(dbConnect, loginUser);
                if (ret && msSenin != null)
                {
                    // 2013.09.27 Add:
                    msSenin = NBaseData.DAC.MsSenin.GetRecord(dbConnect, loginUser, msSenin.MsSeninID);

                    if (user.UserKbn == 0)
                    {
                        msSenin.MsUserID = null;
                    }
                    // ユーザ削除時に、船員も削除するなら、以下をいかす
                    //else
                    //{
                    //    msSenin.DeleteFlag = 1;
                    //}
                    msSenin.RenewDate = DateTime.Now;
                    msSenin.RenewUserID = loginUser.MsUserID;
                    ret = msSenin.UpdateRecord(dbConnect, loginUser);
                }

                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        public bool BLC_ユーザ情報更新処理_更新(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsUser user, NBaseData.DAC.MsUserBumon Ubumon, NBaseData.DAC.MsSenin msSenin)
        {
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = Ubumon.UpdateRecord(dbConnect, loginUser);
                if (ret)
                {
                    ret = user.UpdateRecord(dbConnect, loginUser);
                }
                if (ret)
                {
                    if (user.UserKbn == 0)
                    {
                        // ユーザ区分が「事務所」の場合、船員との紐付けを外す
                        // その際、船員情報は、最新の情報を取得して実施する
                        System.Collections.Generic.List<NBaseData.DAC.MsSenin> ss = NBaseData.DAC.MsSenin.GetRecordsByMsUserID(dbConnect, loginUser, user.MsUserID);
                        foreach(NBaseData.DAC.MsSenin s in ss)
                        {
                            s.MsUserID = null;
                            s.RenewDate = DateTime.Now;
                            s.RenewUserID = loginUser.MsUserID;
                            ret = s.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                                break;
                        }
                    }
                    else if (user.UserKbn == 1)
                    {
                        // ユーザ区分が「船員」の場合
                        // 元々の紐付けを外す
                        System.Collections.Generic.List<NBaseData.DAC.MsSenin> ss = NBaseData.DAC.MsSenin.GetRecordsByMsUserID(dbConnect, loginUser, user.MsUserID);
                        foreach (NBaseData.DAC.MsSenin s in ss)
                        {
                            if ((msSenin == null) || (msSenin != null && s.MsSeninID != msSenin.MsSeninID))
                            {
                                s.MsUserID = null;
                                s.RenewDate = DateTime.Now;
                                s.RenewUserID = loginUser.MsUserID;
                                ret = s.UpdateRecord(dbConnect, loginUser);
                                if (ret == false)
                                    break;
                            }
                        }
                        if (msSenin != null)
                        {
                            // 2013.09.27 Add:
                            msSenin = NBaseData.DAC.MsSenin.GetRecord(dbConnect, loginUser, msSenin.MsSeninID);

                            // 引数の船員情報と紐付けを実施する
                            msSenin.MsUserID = user.MsUserID;
                            msSenin.RenewDate = DateTime.Now;
                            msSenin.RenewUserID = loginUser.MsUserID;
                            ret = msSenin.UpdateRecord(dbConnect, loginUser);
                        }
                    }
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
    }
}
