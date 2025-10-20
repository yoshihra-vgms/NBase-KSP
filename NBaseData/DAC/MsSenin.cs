using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SENIN")]
    public class MsSenin : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID", true)]
        public int MsSeninID { get; set; }




        /// <summary>
        /// ユーザID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }



        /// <summary>
        /// 所属会社ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_COMPANY_ID")]
        public string MsSeninCompanyID { get; set; }


        /// <summary>
        /// 姓
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI")]
        public string Sei { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI")]
        public string Mei { get; set; }

        /// <summary>
        /// 姓カナ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI_KANA")]
        public string SeiKana { get; set; }

        /// <summary>
        /// 名カナ
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI_KANA")]
        public string MeiKana { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("KUBUN")]
        public int Kubun { get; set; }

        /// <summary>
        /// 性別
        /// Int.MinValue→不明
        /// 0→男
        /// 1→女
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEX")]
        public int Sex { get; set; }

        /// <summary>
        /// 氏名コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHIMEI_CODE")]
        public string ShimeiCode { get; set; }

        /// <summary>
        /// 保険番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOKEN_NO")]
        public string HokenNo { get; set; }

        /// <summary>
        /// 生年月日
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIRTHDAY")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 年金番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENKIN_NO")]
        public string NenkinNo { get; set; }

        /// <summary>
        /// 入社年月日
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUUSHA_DATE")]
        public DateTime NyuushaDate { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("POSTAL_NO")]
        public string PostalNo { get; set; }

        /// <summary>
        /// 現住所
        /// </summary>
        [DataMember]
        [ColumnAttribute("GENJUUSHO")]
        public string Genjuusho { get; set; }

        /// <summary>
        /// 本籍
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSEKI")]
        public string Honseki { get; set; }

        /// <summary>
        /// Tel
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        [DataMember]
        [ColumnAttribute("FAX")]
        public string Fax { get; set; }

        /// <summary>
        /// Mail
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL")]
        public string Mail { get; set; }

        /// <summary>
        /// 携帯電話
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEITAI")]
        public string Keitai { get; set; }

        /// <summary>
        /// その他必須事項
        /// </summary>
        [DataMember]
        [ColumnAttribute("SONOTA")]
        public string Sonota { get; set; }

        /// <summary>
        /// 写真
        /// </summary>
        [DataMember]
        [ColumnAttribute("PICTURE")]
        public byte[] Picture { get; set; }

        /// <summary>
        /// 写真更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("PICTURE_DATE")]
        public DateTime PictureDate { get; set; }

        /// <summary>
        /// 最終学歴
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAKUREKI")]
        public string Gakureki { get; set; }

        /// <summary>
        /// 前歴
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZENREKI")]
        public string Zenreki { get; set; }

        /// <summary>
        /// 紹介者
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUKAISHA")]
        public string Shoukaisha { get; set; }

        /// <summary>
        /// 銀行名1
        /// </summary>
        [DataMember]
        [ColumnAttribute("BANK_NAME1")]
        public string BankName1 { get; set; }

        /// <summary>
        /// 支店名1
        /// </summary>
        [DataMember]
        [ColumnAttribute("BRANCH_NAME1")]
        public string BranchName1 { get; set; }

        /// <summary>
        /// 口座番号1
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCOUNT_NO1")]
        public string AccountNo1 { get; set; }

        /// <summary>
        /// 銀行名2
        /// </summary>
        [DataMember]
        [ColumnAttribute("BANK_NAME2")]
        public string BankName2 { get; set; }

        /// <summary>
        /// 支店名2
        /// </summary>
        [DataMember]
        [ColumnAttribute("BRANCH_NAME2")]
        public string BranchName2 { get; set; }

        /// <summary>
        /// 口座番号2
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCOUNT_NO2")]
        public string AccountNo2 { get; set; }

        /// <summary>
        /// ゆうちょ口座番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("POSTAL_ACCOUNT_NO")]
        public string PostalAccountNo { get; set; }

        /// <summary>
        /// 作業服上
        /// </summary>
        [DataMember]
        [ColumnAttribute("CLOTH_UE")]
        public string ClothUe { get; set; }

        /// <summary>
        /// 作業服下
        /// </summary>
        [DataMember]
        [ColumnAttribute("CLOTH_SHITA")]
        public string ClothShita { get; set; }

        /// <summary>
        /// 作業服靴
        /// </summary>
        [DataMember]
        [ColumnAttribute("CLOTH_KUTSU")]
        public string ClothKutsu { get; set; }

        /// <summary>
        /// 退職フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("RETIRE_FLAG")]
        public int RetireFlag { get; set; }

        /// <summary>
        /// 退職日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RETIRE_DATE")]
        public DateTime RetireDate { get; set; }






        /// <summary>
        /// 所属会社ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_COMPANY_ID")]
        public string MsSeninCompanyId { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEPARTMENT")]
        public string Department { get; set; }

        /// <summary>
        /// 血液型
        /// </summary>
        [DataMember]
        [ColumnAttribute("BLOOD_TYPE")]
        public string BloodType { get; set; }

        /// <summary>
        /// 入社区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUUSHA_CATEGORY")]
        public string NyuushaCategory { get; set; }

        /// <summary>
        /// 船員開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEAMEN_START_DATE")]
        public DateTime SeamenStartDate { get; set; }

        /// <summary>
        /// 加入組合
        /// </summary>
        [DataMember]
        [ColumnAttribute("PARTNERSHIP")]
        public string Partnership { get; set; }

        /// <summary>
        /// 保険区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("INSURANCE_CATEGORY")]
        public string InsuranceCategory { get; set; }

        /// <summary>
        /// 保険等級
        /// </summary>
        [DataMember]
        [ColumnAttribute("INSURANCE_GRADE")]
        public int InsuranceGrade { get; set; }


        /// <summary>
        /// 厚生年金等級
        /// </summary>
        [DataMember]
        [ColumnAttribute("PENSION_GRADE")]
        public int PensionGrade { get; set; }


        /// <summary>
        /// 姓かな
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI_HIRAGANA")]
        public string SeiHiragana { get; set; }

        /// <summary>
        /// 名かな
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI_HIRAGANA")]
        public string MeiHiragana { get; set; }


        /// <summary>
        /// 籍
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEMBER_OF")]
        public int MemberOf { get; set; }




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
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        /// <summary>
        /// 種別ID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHUBETSU_ID")]
        public int MsSiShubetsuID { get; set; }

        /// <summary>
        /// 船ID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 開始日 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 開始日 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }




        /// <summary>
        /// ログインID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("LOGIN_ID")]
        public string LoginID { get; set; }

        /// <summary>
        /// パスワード (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("PASSWORD")]
        public string Password { get; set; }




        /// <summary>
        /// 合計日数
        /// </summary>
        [DataMember]
        public Dictionary<int, int> 合計日数 { get; set; }
        #endregion


        public string FullName
        {
            get
            {
                return Sei + " " + Mei;
            }
        }
        public string FullNameKana
        {
            get
            {
                return SeiKana + " " + MeiKana;
            }
        }

        public string FullNameHiragana
        {
            get
            {
                return SeiHiragana + " " + MeiHiragana;
            }
        }

        public string KubunStr
        {
            get
            {
                return Kubun == 0 ? "社員" : "派遣";
            }
        }


        public string SexStr
        {
            get
            {
                return Sex == 0 ? "男" : "女";
            }
        }

        public string MemberOfStr
        {
            get
            {
                return MemberOf == 0 ? "フェリー" : "内航";
            }
        }


        public int IntShomeiCode
        {
            get
            {
                int outShimeiCode = 999999999;
                int.TryParse(ShimeiCode, out outShimeiCode);

                return outShimeiCode;       
            }
        }


        public string AgeStr
        {
            get
            {
                if (Birthday != DateTime.MinValue)
                    return DateTimeUtils.年齢計算(Birthday).ToString();
                else
                    return "";
            }
        }



        public override string ToString()
        {
            return FullName;
        }


        public MsSenin()
        {
            this.MsSiShokumeiID = Int32.MinValue;
        }

        
        

        public static List<MsSenin> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<MsSenin> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSenin), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "OrderByMsSiShokumeiIdSeiMei");
            

            List<MsSenin> ret = new List<MsSenin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSenin> mapping = new MappingBase<MsSenin>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //ユーザーマスタ
        public static List<MsSenin> GetRecordsByMsUserID(DBConnect dbConnect, MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSenin), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            //SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", "");
            SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "AppendFullSeninColumns"));

            List<MsSenin> ret = new List<MsSenin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSenin> mapping = new MappingBase<MsSenin>();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsSenin GetRecord(MsUser loginUser, int msSeninId)
        {
            return GetRecord(null, loginUser, msSeninId);
        }

        public static MsSenin GetRecord(DBConnect dbConnect, MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "AppendFullSeninColumns"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "ByMsSeninID");

            List<MsSenin> ret = new List<MsSenin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSenin> mapping = new MappingBase<MsSenin>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<MsSenin> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, MsSeninFilter filter)
        {
            string SQL = "";
            if (filter.船員テーブルのみ対象)
            {
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "GetRecordsByOnlyMsSenin");
            }
            else
            {
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "GetRecords");
            }



            if (filter.詳細検索)
            {
                SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "AppendFullSeninColumns"));
                SQL = SQL.Replace("MS_SENIN.PICTURE ,", "");
            }
            else if (filter.職別海技免許等資格一覧対象)
            {
                SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "Append職別海技免許等資格一覧Columns"));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_FULL_SENIN_COLUMNS#", "");
            }

            List<MsSenin> ret = new List<MsSenin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSenin> mapping = new MappingBase<MsSenin>();

            MsSeninFilter.JoinSiCard joinSiCard = filter.joinSiCard;

            string appendStr = string.Empty;

            if (joinSiCard == MsSeninFilter.JoinSiCard.LEFT_JOIN || joinSiCard == MsSeninFilter.JoinSiCard.INNER_JOIN)
            {
                string joinStr;

                if (joinSiCard == MsSeninFilter.JoinSiCard.LEFT_JOIN)
                {
                    joinStr = "AppendLeftJoinForSiCardFilter";
                }
                else
                {
                    joinStr = "AppendInnerJoinForSiCardFilter";
                }

                SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "AppendColumnForMsSeninFilter"));
                //SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(MsSenin), joinStr) +
                //                                     " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "ByStartEnd") +
                //                                     " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterBy_休暇管理以外"));
                appendStr += "" + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), joinStr) +
                            " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "ByStartEnd") +
                            " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterBy_休暇管理以外");

                DateTime now = DateTime.Now;

                Params.Add(new DBParameter("START_DATE", now));
                Params.Add(new DBParameter("END_DATE", now));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_COLUMN_STR#", string.Empty);
                //SQL = SQL.Replace("#APPEND_JOIN_STR#", string.Empty);
            }

            if (filter.Juusho != null)　// 住所を別テーブルとしたので、検索条件にある場合、JOIN を利用する
            {
                appendStr += "" + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "AppendInnerJoinMsSeninAddress");
                Params.Add(new DBParameter("PREFECTURES", filter.Juusho));
            }

            SQL = SQL.Replace("#APPEND_JOIN_STR#", appendStr);


            if (filter.MsSiShubetsuIDs.Count > 0 || filter.種別無し)
            {
                SQL += " AND (";

                if (filter.MsSiShubetsuIDs.Count > 0)
                {
                    SQL += " 1 = 1";
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMsSiShubetsuIDs");

                    string innerSQLStr0 = SqlMapper.SqlMapper.CreateInnerSql("q", filter.MsSiShubetsuIDs.Count);
                    SQL = SQL.Replace("#INNER_SQL_MS_SI_SHUBETSUS#", innerSQLStr0);
                    Params.AddInnerParams("q", filter.MsSiShubetsuIDs);
                }

                if (filter.種別無し)
                {
                    if (filter.MsSiShubetsuIDs.Count > 0)
                    {
                        SQL += " OR";
                    }

                    SQL += " SI_CARD.MS_SI_SHUBETSU_ID IS NULL";
                }

                SQL += ")";
            }

            if (filter.ShimeiCode != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByShimeiCode");
                Params.Add(new DBParameter("SHIMEI_CODE", filter.ShimeiCode));
            }

            if (filter.HokenNo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByHokenNo");
                Params.Add(new DBParameter("HOKEN_NO", filter.HokenNo));
            }

            if (filter.Name != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByName");
                Params.Add(new DBParameter("NAME", "%" + filter.Name + "%"));
            }

            if (filter.NameKana != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByNameKana");
                Params.Add(new DBParameter("NAME_KANA", "%" + filter.NameKana + "%"));
            }

            if (filter.SeiKana != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterBySeiKana");
                Params.Add(new DBParameter("SEI_KANA", "%" + filter.SeiKana + "%"));
            }
            if (filter.MeiKana != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMeiKana");
                Params.Add(new DBParameter("MEI_KANA", "%" + filter.MeiKana + "%"));
            }

            if (StringUtils.Empty(filter.MsSeninCompanyID) == false)
            {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMsSeninCompanyId");
                Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", filter.MsSeninCompanyID));
            }

            if (filter.MsSiShokumeiID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMsSiShokumeiId");
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", filter.MsSiShokumeiID));
            }

            if (filter.Kubuns.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByKubuns");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.Kubuns.Count);
                SQL = SQL.Replace("#INNER_SQL_KUBUNS#", innerSQLStr);
                Params.AddInnerParams("p", filter.Kubuns);
            }

            if (filter.RetireFlag != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByRetireFlag");

                Params.Add(new DBParameter("RETIRE_FLAG", filter.RetireFlag));
            }

            if (filter.MsSiMenjouKindIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMsSiMenjouKindFilter");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("r", filter.MsSiMenjouKindIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_SI_MENJOU_KINDS#", innerSQLStr);
                Params.AddInnerParams("r", filter.MsSiMenjouKindIDs);
            }

            if (filter.退職年度 != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterBy_現職および年度内の退職者");
                Params.Add(new DBParameter("年度開始日", DateTimeUtils.年度開始日(filter.退職年度)));
                Params.Add(new DBParameter("年度終了日", DateTimeUtils.年度終了日(filter.退職年度)));
            }

            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }

            if (filter.SeninID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "ByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", filter.SeninID));
            }

            if (filter.OrderByStr != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), filter.OrderByStr);
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSenin), "OrderBySeiMeiStartDate");
            }
            System.Diagnostics.Debug.WriteLine(SQL);
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }




        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            }

            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
           
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("KUBUN", Kubun));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("SHIMEI_CODE", ShimeiCode));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("NENKIN_NO", NenkinNo));
            Params.Add(new DBParameter("NYUUSHA_DATE", NyuushaDate));
            Params.Add(new DBParameter("POSTAL_NO", PostalNo));
            Params.Add(new DBParameter("GENJUUSHO", Genjuusho));
            Params.Add(new DBParameter("HONSEKI", Honseki));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("MAIL", Mail));
            Params.Add(new DBParameter("KEITAI", Keitai));
            Params.Add(new DBParameter("SONOTA", Sonota));
            Params.Add(new DBParameter("PICTURE", Picture));
            Params.Add(new DBParameter("PICTURE_DATE", PictureDate));
            Params.Add(new DBParameter("GAKUREKI", Gakureki));
            Params.Add(new DBParameter("ZENREKI", Zenreki));
            Params.Add(new DBParameter("SHOUKAISHA", Shoukaisha));
            Params.Add(new DBParameter("BANK_NAME1", BankName1));
            Params.Add(new DBParameter("BRANCH_NAME1", BranchName1));
            Params.Add(new DBParameter("ACCOUNT_NO1", AccountNo1));
            Params.Add(new DBParameter("BANK_NAME2", BankName2));
            Params.Add(new DBParameter("BRANCH_NAME2", BranchName2));
            Params.Add(new DBParameter("ACCOUNT_NO2", AccountNo2));
            Params.Add(new DBParameter("POSTAL_ACCOUNT_NO", PostalAccountNo));
            Params.Add(new DBParameter("CLOTH_UE", ClothUe));
            Params.Add(new DBParameter("CLOTH_SHITA", ClothShita));
            Params.Add(new DBParameter("CLOTH_KUTSU", ClothKutsu));
            Params.Add(new DBParameter("RETIRE_FLAG", RetireFlag));
            Params.Add(new DBParameter("RETIRE_DATE", RetireDate));


            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("DEPARTMENT", Department));
            Params.Add(new DBParameter("SEI_HIRAGANA", SeiHiragana));
            Params.Add(new DBParameter("MEI_HIRAGANA", MeiHiragana));
            Params.Add(new DBParameter("MEMBER_OF", MemberOf));

            Params.Add(new DBParameter("BLOOD_TYPE", BloodType));
            Params.Add(new DBParameter("NYUUSHA_CATEGORY", NyuushaCategory));
            Params.Add(new DBParameter("SEAMEN_START_DATE", SeamenStartDate));
            Params.Add(new DBParameter("PARTNERSHIP", Partnership));
            Params.Add(new DBParameter("INSURANCE_CATEGORY", InsuranceCategory));
            Params.Add(new DBParameter("INSURANCE_GRADE", InsuranceGrade));
            Params.Add(new DBParameter("PENSION_GRADE", PensionGrade));


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

            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            
            Params.Add(new DBParameter("SEI", Sei));
            Params.Add(new DBParameter("MEI", Mei));
            Params.Add(new DBParameter("SEI_KANA", SeiKana));
            Params.Add(new DBParameter("MEI_KANA", MeiKana));
            Params.Add(new DBParameter("KUBUN", Kubun));
            Params.Add(new DBParameter("SEX", Sex));
            Params.Add(new DBParameter("SHIMEI_CODE", ShimeiCode));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("BIRTHDAY", Birthday));
            Params.Add(new DBParameter("NENKIN_NO", NenkinNo));
            Params.Add(new DBParameter("NYUUSHA_DATE", NyuushaDate));
            Params.Add(new DBParameter("POSTAL_NO", PostalNo));
            Params.Add(new DBParameter("GENJUUSHO", Genjuusho));
            Params.Add(new DBParameter("HONSEKI", Honseki));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("MAIL", Mail));
            Params.Add(new DBParameter("KEITAI", Keitai));
            Params.Add(new DBParameter("SONOTA", Sonota));
            Params.Add(new DBParameter("PICTURE", Picture));
            Params.Add(new DBParameter("PICTURE_DATE", PictureDate));
            Params.Add(new DBParameter("GAKUREKI", Gakureki));
            Params.Add(new DBParameter("ZENREKI", Zenreki));
            Params.Add(new DBParameter("SHOUKAISHA", Shoukaisha));
            Params.Add(new DBParameter("BANK_NAME1", BankName1));
            Params.Add(new DBParameter("BRANCH_NAME1", BranchName1));
            Params.Add(new DBParameter("ACCOUNT_NO1", AccountNo1));
            Params.Add(new DBParameter("BANK_NAME2", BankName2));
            Params.Add(new DBParameter("BRANCH_NAME2", BranchName2));
            Params.Add(new DBParameter("ACCOUNT_NO2", AccountNo2));
            Params.Add(new DBParameter("POSTAL_ACCOUNT_NO", PostalAccountNo));
            Params.Add(new DBParameter("CLOTH_UE", ClothUe));
            Params.Add(new DBParameter("CLOTH_SHITA", ClothShita));
            Params.Add(new DBParameter("CLOTH_KUTSU", ClothKutsu));
            Params.Add(new DBParameter("RETIRE_FLAG", RetireFlag));
            Params.Add(new DBParameter("RETIRE_DATE", RetireDate));


            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("DEPARTMENT", Department));
            Params.Add(new DBParameter("SEI_HIRAGANA", SeiHiragana));
            Params.Add(new DBParameter("MEI_HIRAGANA", MeiHiragana));
            Params.Add(new DBParameter("MEMBER_OF", MemberOf));

            Params.Add(new DBParameter("BLOOD_TYPE", BloodType));
            Params.Add(new DBParameter("NYUUSHA_CATEGORY", NyuushaCategory));
            Params.Add(new DBParameter("SEAMEN_START_DATE", SeamenStartDate));
            Params.Add(new DBParameter("PARTNERSHIP", Partnership));
            Params.Add(new DBParameter("INSURANCE_CATEGORY", InsuranceCategory));
            Params.Add(new DBParameter("INSURANCE_GRADE", InsuranceGrade));
            Params.Add(new DBParameter("PENSION_GRADE", PensionGrade));


            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsSeninID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return MsSeninID == 0;
        }
    }
}
