using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DS;
using ORMapping.Atts;

namespace NBaseData.DAC
{
    [DataContract()]
    public class HoukokushoKanriKiroku
    {
        #region データメンバ
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID", true)]
        public string MsDmHoukokushoID { get; set; }
        
        /// <summary>
        /// 分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_ID", true)]
        public string MsDmBunruiID { get; set; }
        
        /// <summary>
        /// 小分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_ID", true)]
        public string MsDmShoubunruiID { get; set; }

        /// <summary>
        /// 文書番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NO")]
        public string BunshoNo { get; set; }

        /// <summary>
        /// 文書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NAME")]
        public string BunshoName { get; set; }

        /// <summary>
        /// 提出周期
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHUKI")]
        public string Shuki { get; set; }

        /// <summary>
        /// 提出時期
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI")]
        public string Jiki { get; set; }

        /// <summary>
        /// 分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_NAME")]
        public string BunruiName { get; set; }

        /// <summary>
        /// 小分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_NAME")]
        public string ShoubunruiName { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRI_KIROKU_ISSUE_DATE")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// 雛形ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEMPLATE_FILE_NAME")]
        public string TemplateFileName { get; set; }

        /// <summary>
        /// 雛形ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_UPDATE_DATE")]
        public DateTime FileUpdateDate { get; set; }

        #endregion

        public HoukokushoKanriKiroku()
        {
        }

        //public static List<HoukokushoKanriKiroku> SearchRecords(NBaseData.DAC.MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, bool kaicho_shacho, bool sekininsha)
        public static List<HoukokushoKanriKiroku> SearchRecords(NBaseData.DAC.MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, int role)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetRecords");
            List<HoukokushoKanriKiroku> ret = new List<HoukokushoKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<HoukokushoKanriKiroku> mapping = new MappingBase<HoukokushoKanriKiroku>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByBumon");
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.部門));
            Params.Add(new DBParameter("MS_USER_ID", userId));

            if (bunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmBunruiID");
                Params.Add(new DBParameter("MS_DM_BUNRUI_ID", bunruiId));
            }
            if (shoubunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmShoubunruiID");
                Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", shoubunruiId));
            }
            if (bunshoNo.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoNo");
                Params.Add(new DBParameter("BUNSHO_NO", "%" + bunshoNo + "%"));
            }
            if (bunshoName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoName");
                Params.Add(new DBParameter("BUNSHO_NAME", "%" + bunshoName + "%"));
            }

            #region 20210824 下記に変更
            //if (kaicho_shacho)
            //{
            //    SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetRecords");
            //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByKaichoShacho");
            //    Params.Add(new DBParameter("KOUKAI_SAKI_KAICHO_SHACHO", (int)NBaseData.DS.DocConstants.RoleEnum.会長社長));

            //    if (bunruiId.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmBunruiID");
            //    }
            //    if (shoubunruiId.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmShoubunruiID");
            //    }
            //    if (bunshoNo.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoNo");
            //    }
            //    if (bunshoName.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoName");
            //    }
            //}

            //if (sekininsha)
            //{
            //    SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetRecords");
            //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "BySekininsha");
            //    Params.Add(new DBParameter("KOUKAI_SAKI_SEKININSHA", (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者));

            //    if (bunruiId.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmBunruiID");
            //    }
            //    if (shoubunruiId.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmShoubunruiID");
            //    }
            //    if (bunshoNo.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoNo");
            //    }
            //    if (bunshoName.Length > 0)
            //    {
            //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoName");
            //    }
            //}
            #endregion
            if (role > 0)
            {
                SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetRecords");
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByRole");
                Params.Add(new DBParameter("KOUKAI_SAKI_ROLE", role));

                if (bunruiId.Length > 0)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmBunruiID");
                }
                if (shoubunruiId.Length > 0)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmShoubunruiID");
                }
                if (bunshoNo.Length > 0)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoNo");
                }
                if (bunshoName.Length > 0)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoName");
                }
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            foreach (HoukokushoKanriKiroku hkk in ret)
            {
                //DmKanriKiroku kk = DmKanriKiroku.GetLatestRecord(loginUser, hkk.MsDmHoukokushoID, loginUser.MsUserID, kaicho_shacho, sekininsha);
                DmKanriKiroku kk = DmKanriKiroku.GetLatestRecord(loginUser, hkk.MsDmHoukokushoID, loginUser.MsUserID, role);
                if (kk != null)
                {
                    hkk.IssueDate = kk.IssueDate;
                }
                else
                {
                    hkk.IssueDate = DateTime.MinValue;
                }
            }

            return ret;
        }
        
        public static List<HoukokushoKanriKiroku> SearchRecords(NBaseData.DAC.MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetHonsenRecords");
            List<HoukokushoKanriKiroku> ret = new List<HoukokushoKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<HoukokushoKanriKiroku> mapping = new MappingBase<HoukokushoKanriKiroku>();

            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            if (bunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmBunruiID");
                Params.Add(new DBParameter("MS_DM_BUNRUI_ID", bunruiId));
            }
            if (shoubunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "ByMsDmShoubunruiID");
                Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", shoubunruiId));
            }
            if (bunshoNo.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoNo");
                Params.Add(new DBParameter("BUNSHO_NO", "%" + bunshoNo + "%"));
            }
            if (bunshoName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "LikeBunshoName");
                Params.Add(new DBParameter("BUNSHO_NAME", "%" + bunshoName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            foreach (HoukokushoKanriKiroku hkk in ret)
            {
                DmKanriKiroku kk = DmKanriKiroku.GetLatestRecord(loginUser, hkk.MsDmHoukokushoID, vesselId);
                if (kk != null)
                {
                    hkk.IssueDate = kk.IssueDate;
                }
                else
                {
                    hkk.IssueDate = DateTime.MinValue;
                }
            }

            return ret;
        }

        public static List<HoukokushoKanriKiroku> GetRecords未提出一覧(NBaseData.DAC.MsUser loginUser, int businessYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "GetRecords未提出一覧");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(HoukokushoKanriKiroku), "OrderBy");
            
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            //Params.Add(new DBParameter("JIKI_NEN", businessYear));
            //Params.Add(new DBParameter("JIKI_YOKU_NEN", businessYear+1));
           
            List<HoukokushoKanriKiroku> ret = new List<HoukokushoKanriKiroku>();
            MappingBase<HoukokushoKanriKiroku> mapping = new MappingBase<HoukokushoKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);



            return ret;
        }
    }
}



