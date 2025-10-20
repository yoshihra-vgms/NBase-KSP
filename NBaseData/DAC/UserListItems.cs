using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("USER_LIST_ITEMS")]
    public class UserListItems
    {

        #region データメンバ

        /// <summary>
        /// ユーザ表示項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_LIST_ITEMS_ID", true)]
        public int UserListItemsID { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_ID")]
        public string UserID { get; set; }

        /// <summary>
        /// 表示項目セット名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TITLE")]
        public string Title { get; set; }

        /// <summary>
        /// 表示項目マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LIST_ITEM_ID")]
        public int MsListItemID { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [DataMember]
        [ColumnAttribute("DISPLAY_ORDER")]
        public int DisplayOrder { get; set; }


        #region 共通

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        #endregion


        #endregion
        public MsListItem ListItem { get; set; }


        public UserListItems()
        {
            UserListItemsID = 0;
        }


        public static List<UserListItems> GetRecords(NBaseData.DAC.MsUser loginUser, int kind, string userId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(UserListItems), "GetRecords");

            List<UserListItems> ret = new List<UserListItems>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<UserListItems> mapping = new MappingBase<UserListItems>();

            Params.Add(new DBParameter("KIND", kind));
            Params.Add(new DBParameter("USER_ID", userId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            //Params.Add(new DBParameter("USER_LIST_ITEMS_ID", UserListItemsID));

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("USER_ID", UserID));
            Params.Add(new DBParameter("TITLE", Title));
            Params.Add(new DBParameter("MS_LIST_ITEM_ID", MsListItemID));
            Params.Add(new DBParameter("DISPLAY_ORDER", DisplayOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("USER_ID", UserID));
            Params.Add(new DBParameter("TITLE", Title));
            Params.Add(new DBParameter("MS_LIST_ITEM_ID", MsListItemID));
            Params.Add(new DBParameter("DISPLAY_ORDER", DisplayOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("USER_LIST_ITEMS_ID", UserListItemsID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool IsNew()
        {
            return UserListItemsID == 0;
        }
    }
}

