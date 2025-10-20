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
    [TableAttribute("MS_LIST_ITEM")]
    public class MsListItem
    {
        public enum enumKind { 船員, 発注 };


        #region データメンバ

        /// <summary>
        /// 表示項目マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LIST_ITEM_ID", true)]
        public int MsListItemID { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        [DataMember]
        [ColumnAttribute("DISP_NAME")]
        public string DispName { get; set; }

        /// <summary>
        /// 表示/非表示
        /// </summary>
        [DataMember]
        [ColumnAttribute("IS_VISIBLE")]
        public int IsVisible { get; set; }

        /// <summary>
        /// 常に表示対象か
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALWAYS_SELECTED")]
        public int AlwaysSelected { get; set; }

        /// <summary>
        /// クラス名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CLASS_NAME")]
        public string ClassName { get; set; }

        /// <summary>
        /// プロパティ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("PROPERTY_NAME")]
        public string PropertyName { get; set; }

        /// <summary>
        /// 表示幅
        /// </summary>
        [DataMember]
        [ColumnAttribute("WIDTH")]
        public int Width { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [DataMember]
        [ColumnAttribute("DISPLAY_ORDER")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// マスタクラス名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MASTER_NAME")]
        public string MasterName { get; set; }

        /// <summary>
        /// マスタクラスのプロパティ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MASTER_PROPERTY_NAME")]
        public string MasterPropertyName { get; set; }


        /// <summary>
        /// 横位置
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALIGNMENT")]
        public string Alignment { get; set; }



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


        public override string ToString()
        {
            return DispName;
        }

        public MsListItem()
        {
            MsListItemID = 0;
        }


        public static List<MsListItem> GetRecords(NBaseData.DAC.MsUser loginUser, int kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsListItem), "GetRecords");

            List<MsListItem> ret = new List<MsListItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsListItem> mapping = new MappingBase<MsListItem>();

            Params.Add(new DBParameter("KIND", kind));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }

    }
}

