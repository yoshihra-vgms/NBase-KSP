using NBaseData.DAC;
using ORMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<UserListItems> BLC_GetUserListSetting(MsUser loginUser, int kind, string userId);

        [OperationContract]
        bool BLC_RegistUserListSetting(MsUser loginUser, List<UserListItems> userListItemsList);
    }
    public partial class Service
    {
        public List<UserListItems> BLC_GetUserListSetting(MsUser loginUser, int kind, string userId)
        {
            List<UserListItems> retList = new List<UserListItems>();

            // Default（userID = "0"）をセットする
            List<UserListItems> defList = UserListItems.GetRecords(loginUser, kind, "0");

            defList.ForEach(o =>
            {
                UserListItems u = new UserListItems();

                //u.UserListItemsID  // 新規としたいため、IDはセットしない
                u.Kind = o.Kind;
                u.UserID = userId;
                u.Title = o.Title;
                u.MsListItemID = o.MsListItemID;
                u.DisplayOrder = o.DisplayOrder;

                retList.Add(u);
            });  
            
            List<UserListItems> userListItemsList = UserListItems.GetRecords(loginUser, kind, userId);
            if (userListItemsList != null)
            {
                retList.AddRange(userListItemsList);
            }

            return retList;
        }


        public bool BLC_RegistUserListSetting(MsUser loginUser, List<UserListItems> userListItemsList)
        {
            bool ret = true;

            int all = userListItemsList.Count();
            int ins = userListItemsList.Where(o => o.IsNew()).Count();
            int del = userListItemsList.Where(o => o.DeleteFlag == 1).Count();

            if (ins == all)
            {
                ret = InsertUserListSetting(loginUser, userListItemsList);
            }
            else if (del == all)
            {
                ret = DeleteUserListSetting(loginUser, userListItemsList);
            }
            else
            {
                ret = UpdateUserListSetting(loginUser, userListItemsList);
            }
            return ret;
        }

        private bool InsertUserListSetting(MsUser loginUser, List<UserListItems> userListItemsList)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (UserListItems delItem in userListItemsList)
                {
                    delItem.RenewUserID = loginUser.MsUserID;
                    delItem.RenewDate = DateTime.Now;
                    ret = delItem.InsertRecord(dbConnect, loginUser);
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
        private bool UpdateUserListSetting(MsUser loginUser, List<UserListItems> userListItemsList)
        {
            bool ret = true;

            int kind = userListItemsList.Select(o => o.Kind).First();
            string userId = userListItemsList.Select(o => o.UserID).First();
            string title = userListItemsList.Select(o => o.Title).First();

            List<UserListItems> allList = UserListItems.GetRecords(loginUser, kind, userId);
            var targetList = allList.Where(o => o.Title == title);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                var regIds = userListItemsList.Select(o => o.MsListItemID);
                var dels = targetList.Where(o => regIds.Contains(o.MsListItemID) == false);


                // 対象から外れたもの
                foreach (UserListItems delItem in dels)
                {
                    if (delItem.IsNew() == false)
                    {
                        delItem.DeleteFlag = 1;
                        delItem.RenewUserID = loginUser.MsUserID;
                        delItem.RenewDate = DateTime.Now;
                        ret = delItem.UpdateRecord(dbConnect, loginUser); ;
                    }
                }

                // 対象のもの
                foreach (UserListItems userListItems in userListItemsList)
                {
                    userListItems.RenewUserID = loginUser.MsUserID;
                    userListItems.RenewDate = DateTime.Now;
                    if (userListItems.IsNew())
                    {
                        ret = userListItems.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        ret = userListItems.UpdateRecord(dbConnect, loginUser);
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
        private bool DeleteUserListSetting(MsUser loginUser, List<UserListItems> userListItemsList)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (UserListItems delItem in userListItemsList)
                {
                    delItem.DeleteFlag = 1;
                    delItem.RenewUserID = loginUser.MsUserID;
                    delItem.RenewDate = DateTime.Now;
                    ret = delItem.UpdateRecord(dbConnect, loginUser);
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