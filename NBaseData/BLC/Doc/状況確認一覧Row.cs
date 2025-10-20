using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
using System.Drawing;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 状況確認一覧Row : IGenericCloneable<状況確認一覧Row>
    {
        #region データメンバ
        /// <summary>
        /// 管理記録ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRI_KIROKU_ID")]
        public string DmKanriKirokuId { get; set; }

        /// <summary>
        /// 公文書_規則ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KOUBUNSHO_KISOKU_ID")]
        public string DmKoubunshoKisokuId { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ISSUE_DATE")]
        public DateTime 発行日 { get; set; }

        /// <summary>
        /// 分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNRUI_NAME")]
        public string 分類名 { get; set; }

        /// <summary>
        /// 小分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUBUNRUI_NAME")]
        public string 小分類名 { get; set; }

        /// <summary>
        /// 文書番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NO")]
        public string 文書番号 { get; set; }

        /// <summary>
        /// 文書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NAME")]
        public string 文書名 { get; set; }
        
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID")]
        public string MsDmHoukokushoId { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_NAME")]
        public string FileName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string 備考 { get; set; }

        /// <summary>
        /// 状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

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
        /// 発行元
        /// </summary>
        [DataMember]
        public int PublisherFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        public int PublisherVesselId { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        public int KoukaiSakiVesselId { get; set; }

        /// <summary>
        /// 発行元
        /// </summary>
        [DataMember]
        public string 発行元 { get; set; }
        
        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        public string 船名 { get; set; }

        /// <summary>
        /// 完了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANRYO_DATE")]
        public DateTime 完了日 { get; set; }

        /// <summary>
        /// 完了者
        /// </summary>
        [DataMember]
        public string 完了者
        {
            get
            {
                if (finUserName == "")
                {
                    finUserName = Sei + " " + Mei;
                }
                return finUserName;
            }
            set
            {
                finUserName = value;
            }
        }
        private String finUserName = ""; 

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
        /// 公開先
        /// </summary>
        [DataMember]
        public List<DmKoukaiSaki> KoukaisakiList = null;
        #endregion

        public Bitmap GetFileImg()
        {
            // 1. 実行中のメイン・アセンブリのフル・パスを取得する
            Assembly asm = Assembly.GetEntryAssembly();
            string fullPath = asm.Location;

            // 2. フル・パスからディレクトリ・パス部分を抽出する
            string dirPath = System.IO.Path.GetDirectoryName(fullPath);

            Bitmap bmp = new Bitmap(dirPath + "\\Resources\\icon_txt.gif");
            try
            {
                string ext = FileName.Substring(FileName.LastIndexOf(".") + 1);
                if (ext != null && ext.Length > 0)
                    ext = ext.ToLower();

                if (ext == "doc" || ext == "docx")
                {
                    bmp = new Bitmap(dirPath + "\\Resources\\icon_doc.gif");
                }
                else if (ext == "xls" || ext == "xlsx")
                {
                    bmp = new Bitmap(dirPath + "\\Resources\\icon_xls.gif");
                }
                else if (ext == "ppt" || ext == "pptx")
                {
                    bmp = new Bitmap(dirPath + "\\Resources\\icon_pps.gif");
                }
                else if (ext == "pdf")
                {
                    bmp = new Bitmap(dirPath + "\\Resources\\icon_pdf.gif");
                }
            }
            catch
            {
            }
            return bmp;
        }


        public static List<状況確認一覧Row> SearchRecords(
            NBaseData.DAC.MsUser loginUser,
            List<string> bunruiIds,
            List<string> shoubunruiIds,
            List<int> vesselIds,
            string bunshoNo,
            string bunshoName,
            string bikou,
            DateTime issueFrom,
            DateTime issueTo,
            int vesselId,
            int role,
            string bumonId,
            int status,
            bool isKanryo,
            string keywords
            )
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "GetRecords");
            List<状況確認一覧Row> tmp = new List<状況確認一覧Row>();
            List<状況確認一覧Row> ret = new List<状況確認一覧Row>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<状況確認一覧Row> mapping = new MappingBase<状況確認一覧Row>();


            #region ベースとなる管理記録＆公文書規則をＤＢから取得
            string AppendSQL1 = "";
            string AppendSQL2 = "";

            string AppendBunruiSQL1 = "";
            string AppendBunruiSQL2 = "";
            if (bunruiIds.Count > 0)
            {
                AppendBunruiSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByMsDmBunruiIDs_1");
                AppendBunruiSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByMsDmBunruiIDs_2");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("q", bunruiIds.Count);
                AppendBunruiSQL1 = AppendBunruiSQL1.Replace("#INNER_SQL_MS_DM_BUNRUI_IDS#", innerSQLStr);
                AppendBunruiSQL2 = AppendBunruiSQL2.Replace("#INNER_SQL_MS_DM_BUNRUI_IDS#", innerSQLStr);
                Params.AddInnerParams("q", bunruiIds);
            }

            string AppendShoubunruiSQL1 = "";
            string AppendShoubunruiSQL2 = "";
            if (shoubunruiIds.Count > 0)
            {
                AppendShoubunruiSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByMsDmShoubunruiIDs_1");
                AppendShoubunruiSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByMsDmShoubunruiIDs_2");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", shoubunruiIds.Count);
                AppendShoubunruiSQL1 = AppendShoubunruiSQL1.Replace("#INNER_SQL_MS_DM_SHOUBUNRUI_IDS#", innerSQLStr);
                AppendShoubunruiSQL2 = AppendShoubunruiSQL2.Replace("#INNER_SQL_MS_DM_SHOUBUNRUI_IDS#", innerSQLStr);
                Params.AddInnerParams("p", shoubunruiIds);
            }

            if (AppendBunruiSQL1.Length > 0 && AppendShoubunruiSQL1.Length > 0)
            {
                AppendSQL1 += " AND ( " + AppendBunruiSQL1 + " OR " + AppendShoubunruiSQL1 + " )";
                AppendSQL2 += " AND ( " + AppendBunruiSQL2 + " OR " + AppendShoubunruiSQL2 + " )";
            }
            else if (AppendBunruiSQL1.Length > 0)
            {
                AppendSQL1 += " AND ( " + AppendBunruiSQL1 + " )";
                AppendSQL2 += " AND ( " + AppendBunruiSQL2 + " )";
            }
            else if (AppendShoubunruiSQL1.Length > 0)
            {
                AppendSQL1 += " AND ( " + AppendShoubunruiSQL1 + " )";
                AppendSQL2 += " AND ( " + AppendShoubunruiSQL2 + " )";
            }


            if (bunshoNo.Length > 0)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBunshoNo_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBunshoNo_2");
                Params.Add(new DBParameter("BUNSHO_NO", "%" + bunshoNo + "%"));
            }
            if (bunshoName.Length > 0)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBunshoName_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBunshoName_2");
                Params.Add(new DBParameter("BUNSHO_NAME", "%" + bunshoName + "%"));
            }
            if (bikou.Length > 0)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBikou_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeBikou_2");
                Params.Add(new DBParameter("BIKOU", "%" + bikou + "%"));
            }
            if (issueFrom != null && issueFrom != DateTime.MinValue &&
                issueTo != null && issueTo != DateTime.MaxValue)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateFromTo_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateFromTo_2");
                Params.Add(new DBParameter("ISSUE_DATE_FROM", issueFrom.ToShortDateString()));
                Params.Add(new DBParameter("ISSUE_DATE_TO", issueTo.ToShortDateString()));
            }
            else if (issueFrom != null && issueFrom != DateTime.MinValue)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateFrom_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateFrom_2");
                Params.Add(new DBParameter("ISSUE_DATE_FROM", issueFrom.ToShortDateString()));
            }
            else if (issueTo != null && issueTo != DateTime.MaxValue)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateTo_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "ByIssueDateTo_2");
                Params.Add(new DBParameter("ISSUE_DATE_TO", issueTo.ToShortDateString()));
            }
            if (isKanryo == false)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "IsStatus0_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "IsStatus0_2");
            }
            // 完了チェックありの場合に、完了のみを出力したい場合には、以下の行を有効とすること
            //else
            //{
            //    AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "IsStatus1_1");
            //    AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "IsStatus1_2");
            //}


            if (keywords.Length > 0)
            {
                AppendSQL1 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeKeywords_1");
                AppendSQL2 += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "LikeKeywords_2");
                Params.Add(new DBParameter("KEYWORDS", "%" + keywords + "%"));
            }


            SQL = SQL.Replace("#APPEND_WHERE_STR_1#", AppendSQL1);
            SQL = SQL.Replace("#APPEND_WHERE_STR_2#", AppendSQL2);
            #endregion

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "OrderBy");
            tmp = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);



            string innerSQL1 = SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "GetKanriKirokuIDs");
            innerSQL1 = innerSQL1.Replace("#APPEND_WHERE_STR#", AppendSQL1);

            string innerSQL2 = SqlMapper.SqlMapper.GetSql(typeof(状況確認一覧Row), "GetKoubunshoKisokuIDs");
            innerSQL2 = innerSQL2.Replace("#APPEND_WHERE_STR#", AppendSQL2);


            List<DmPublisher> allPublishers = new List<DmPublisher>();

            allPublishers.AddRange(DmPublisher.GetRecordsByLinkSakiIDs(loginUser, innerSQL1, Params));

            allPublishers.AddRange(DmPublisher.GetRecordsByLinkSakiIDs(loginUser, innerSQL2, Params));


            List<DmKoukaiSaki> allKoukaiSakis = new List<DmKoukaiSaki>();

            allKoukaiSakis.AddRange(DmKoukaiSaki.GetRecordsByLinkSakiIDs(loginUser, innerSQL1, Params));

            allKoukaiSakis.AddRange(DmKoukaiSaki.GetRecordsByLinkSakiIDs(loginUser, innerSQL2, Params));


            List<DmKakuninJokyo> allKakuninJokyos = new List<DmKakuninJokyo>();
            if (status != 0)  // "すべて" じゃない場合
            {
                ParameterConnection Params2 = new ParameterConnection();
                foreach (DBParameter p in Params)
                {
                    Params2.Add(p);
                }
                allKakuninJokyos.AddRange(DmKakuninJokyo.GetRecordsByLinkSaki(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, innerSQL1, Params));

                allKakuninJokyos.AddRange(DmKakuninJokyo.GetRecordsByLinkSaki(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則, innerSQL2, Params2));
            }


            // 本来は、文書管理対象の船を取得すればよいが
            // フラグがはずされる場合があるので、すべての船を取得しておく
            List<MsVessel> msVessels = MsVessel.GetRecords(loginUser);
            Hashtable msVesselHash = new Hashtable();
            foreach (MsVessel vessel in msVessels)
            {
                msVesselHash.Add(vessel.MsVesselID, vessel.VesselName);
            }


            // 文書の機能権限フラグ、実績フラグがともにないものは対象外とする
            var removeVesselIds = msVessels.Where(obj => obj.DocumentEnabled == 0 && obj.DocumentResults == 0).Select(obj => obj.MsVesselID);

            if (removeVesselIds.Count() > 0)
            {
                allKoukaiSakis = allKoukaiSakis.Where(obj => removeVesselIds.Contains(obj.MsVesselID) == false).ToList();
                allKakuninJokyos = allKakuninJokyos.Where(obj => removeVesselIds.Contains(obj.MsVesselID) == false).ToList();
            }



            foreach (状況確認一覧Row row in tmp)
            {
                if (row.完了日 != DateTime.MinValue)
                {
                    row.Status = (int)NBaseData.DS.DocConstants.StatusEnum.完了;
                }
                else
                {
                    row.Status = (int)NBaseData.DS.DocConstants.StatusEnum.未完了;
                }

                List<DmPublisher> publishers = null;
                if (row.DmKanriKirokuId != null && row.DmKanriKirokuId.Length > 0)
                {
                    var tmp1 = allPublishers.Where(obj => obj.LinkSakiID == row.DmKanriKirokuId);
                    if (tmp1.Count() > 0)
                    {
                        publishers = tmp1.ToList();
                    }
                }
                else
                {
                    var tmp1 = allPublishers.Where(obj => obj.LinkSakiID == row.DmKoubunshoKisokuId);
                    if (tmp1.Count() > 0)
                    {
                        publishers = tmp1.ToList();
                    }
                }
                if (publishers == null)
                {
                    publishers = new List<DmPublisher>();
                }

                List<DmKoukaiSaki> koukaisakis = null;
                if (row.DmKanriKirokuId != null && row.DmKanriKirokuId.Length > 0)
                {
                    var tmp1 = allKoukaiSakis.Where(obj => obj.LinkSakiID == row.DmKanriKirokuId);
                    if (tmp1.Count() > 0)
                    {
                        koukaisakis = tmp1.ToList();
                    }
                }
                else
                {
                    var tmp1 = allKoukaiSakis.Where(obj => obj.LinkSakiID == row.DmKoubunshoKisokuId);
                    if (tmp1.Count() > 0)
                    {
                        koukaisakis = tmp1.ToList();
                    }
                }
                if (koukaisakis == null)
                {
                    koukaisakis = new List<DmKoukaiSaki>();
                }
                row.KoukaisakiList = koukaisakis;


                // ================================================================
                // SQLでの条件にするのは難しいので、確認状況はフィルタリング
                // ================================================================
                #region
                if (status != 0)  // "すべて" じゃない場合
                {
                    List<DmKakuninJokyo> kakuninJokyos = null;
                    if (row.DmKanriKirokuId != null && row.DmKanriKirokuId.Length > 0)
                    {
                        var tmp1 = allKakuninJokyos.Where(obj => (obj.LinkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録 && obj.LinkSakiID == row.DmKanriKirokuId));
                        if (tmp1.Count() > 0)
                        {
                            kakuninJokyos = tmp1.ToList();
                        }
                    }
                    else
                    {
                        var tmp1 = allKakuninJokyos.Where(obj => (obj.LinkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則 && obj.LinkSakiID == row.DmKoubunshoKisokuId));
                        if (tmp1.Count() > 0)
                        {
                            kakuninJokyos = tmp1.ToList();
                        }
                    }
                    if (kakuninJokyos == null)
                    {
                        kakuninJokyos = new List<DmKakuninJokyo>();
                    }

                    bool is対象 = false;

                    #region 未確認：ログインユーザが確認していないもの
                    if (status == 1) // 未確認：ログインユーザが確認していないもの
                    {
                        is対象 = true;

                        var me = from p in kakuninJokyos
                                 where p.MsUserID == loginUser.MsUserID && p.KakuninDate != DateTime.MinValue
                                 select p;
                        if (me.Count<DmKakuninJokyo>() > 0)
                        {
                            // ログインユーザが確認済みなら、対象外
                            is対象 = false;
                        }
                    }
                    #endregion
                    #region 確認済：ログインユーザが確認したもの
                    if (status == 2) // 確認済：ログインユーザが確認したもの
                    {
                        var me = from p in kakuninJokyos
                                 where p.MsUserID == loginUser.MsUserID && p.KakuninDate != DateTime.MinValue
                                 select p;
                        if (me.Count<DmKakuninJokyo>() > 0)
                        {
                            // ログインユーザが確認済みなら、対象
                            is対象 = true;
                        }
                    }
                    #endregion
                    #region 他グループ未確認：自グループ以外が確認していないもの
                    if (status == 3) // 他グループ未確認：自グループ以外が確認していないもの
                    {
                        foreach (DmKoukaiSaki kks in koukaisakis)
                        {
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門
                                && kks.MsBumonID == loginUser.BumonID)
                            {
                                continue;
                            }

                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者
                                && loginUser.DocFlag_CEO == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者
                                && loginUser.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督
                                && loginUser.DocFlag_MsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者
                                && loginUser.DocFlag_CrewFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督
                                && loginUser.DocFlag_TsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員
                                && loginUser.DocFlag_Officer == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                            {
                                continue;
                            }


                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue &&
                                            p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船 &&
                                            (p.DocFlag_CEO == (int)NBaseData.DS.DocConstants.FlagEnum.ON)
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue
                                            && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                                            && p.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue
                                            && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                                            && p.DocFlag_MsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue
                                            && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                                            && p.DocFlag_CrewFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue
                                            && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                                            && p.DocFlag_TsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }
                            if (kks.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員)
                            {
                                var check = from p in kakuninJokyos
                                            where p.KakuninDate != DateTime.MinValue
                                            && p.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船
                                            && p.DocFlag_Officer == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                            select p;
                                if (check.Count<DmKakuninJokyo>() == 0)
                                {
                                    // 未確認なら、対象
                                    is対象 = true;
                                    break;
                                }
                            }

                            var bumonRoleList = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.BUMON);
                            foreach(DocConstants.ClassItem item in bumonRoleList)
                            {
                                if (kks.KoukaiSaki == (int)item.enumClass && kks.MsBumonID == item.bumonId)
                                {
                                    var check = from p in kakuninJokyos
                                                where p.KakuninDate != DateTime.MinValue
                                                && p.KoukaiSaki == (int)item.enumClass
                                                && p.MsBumonID == item.bumonId
                                                && p.DocFlag_GL == (int)NBaseData.DS.DocConstants.FlagEnum.ON
                                                select p;
                                    if (check.Count<DmKakuninJokyo>() == 0)
                                    {
                                        // 未確認なら、対象
                                        is対象 = true;
                                        break;
                                    }
                                }

                            }

                        }
                    }
                    #endregion
                    if (status == 4) // 未確認（確認不要を除く）：ログインユーザが確認していないもの
                    {
                        is対象 = true;

                        bool 要確認 = false;
                        if (loginUser.DocFlag_CEO == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者);
                        }
                        if (!要確認 && loginUser.DocFlag_Admin == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者);
                        }
                        if (!要確認 && loginUser.DocFlag_MsiFerry == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.海務監督);
                        }
                        if (!要確認 && loginUser.DocFlag_CrewFerry == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者);
                        }
                        if (!要確認 && loginUser.DocFlag_TsiFerry == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.工務監督);
                        }
                        if (!要確認 && loginUser.DocFlag_Officer == 1)
                        {
                            要確認 = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.役員);
                        }

                        if (!要確認)
                        {
                            要確認 = 要確認チェック_部門(koukaisakis, loginUser.BumonID);
                        }

                        if (!要確認)
                        {
                            // 確認不要なら、対象外
                            is対象 = false;
                        }
                        else
                        {
                            var me = from p in kakuninJokyos
                                     where p.MsUserID == loginUser.MsUserID && p.KakuninDate != DateTime.MinValue
                                     select p;
                            if (me.Count<DmKakuninJokyo>() > 0)
                            {
                                // ログインユーザが確認済みなら、対象外
                                is対象 = false;
                            }
                        }
                    }
                    if (is対象 == false)
                    {
                        continue;
                    }
                }
                #endregion

                Hashtable sameVessel = new Hashtable();
                foreach (DmPublisher publisher in publishers)
                {
                    bool is対象 = true;
                    // ================================================================
                    // SQLでの条件にするのは難しいので、発行元はフィルタリング
                    // ================================================================
                    #region
                    if (vesselId > 0)
                    {
                        if (publisher.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                        {
                            is対象 = false;
                        }
                        if (publisher.MsVesselID != vesselId)
                        {
                            is対象 = false;
                        }
                    }
                    //if (kaicho_shacho == 1)
                    //{
                    //    if (publisher.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者)
                    //    {
                    //        is対象 = false;
                    //    }
                    //}
                    //if (sekininsha == 1)
                    //{
                    //    if (publisher.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                    //    {
                    //        is対象 = false;
                    //    }
                    //}
                    //if (kaicho_shacho > 0)
                    //{
                    //    if (publisher.KoukaiSaki != kaicho_shacho)
                    //    {
                    //        is対象 = false;
                    //    }
                    //}
                    //if (sekininsha == 1)
                    //{
                    //    if (publisher.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                    //    {
                    //        is対象 = false;
                    //    }
                    //}

                    if (role > 0)
                    {
                        if (publisher.KoukaiSaki != role)
                        {
                            is対象 = false;
                        }
                    }

                    if (bumonId.Length > 0)
                    {
                        if (publisher.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                        {
                            is対象 = false;
                        }
                        if (publisher.MsBumonID != bumonId)
                        {
                            is対象 = false;
                        }
                    }
                    #endregion
                    if (is対象 == false)
                    {
                        continue;
                    }

                    状況確認一覧Row clone = row.Clone();
                    clone.PublisherFlag = publisher.KoukaiSaki;
                    clone.発行元 = publisher.PublisherName;

                    if (clone.PublisherFlag != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    {
                        // 発行元が船以外の場合
                        int 船公開先数 = 0;
                        foreach (DmKoukaiSaki koukaisaki in koukaisakis)
                        {
                            if (vesselIds.Count > 0)
                            {
                                if (koukaisaki.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                                    continue;

                                if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船 &&
                                   vesselIds.Contains(koukaisaki.MsVesselID) == false)
                                    continue;
                            }

                            if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                            {
                                状況確認一覧Row clone2 = clone.Clone();
                                clone2.KoukaiSakiVesselId = koukaisaki.MsVesselID;

                                try
                                {
                                    clone2.船名 = (msVesselHash[koukaisaki.MsVesselID]) as string;
                                }
                                catch
                                {
                                }
                                ret.Add(clone2);
                                船公開先数++;
                            }
                        }
                        //if (船公開先数 == 0)
                        if (vesselIds.Count == 0 && 船公開先数 == 0)
                        {
                            ret.Add(clone);
                        }
                    }
                    else
                    {
                        // 同じ船が発行元の場合は、無視する
                        // (船が発行元の場合、乗船リストで船員を複数発行者とすることができるため)
                        if (sameVessel.Contains(publisher.PublisherName))
                        {
                            continue;
                        }

                        int 船公開先数 = 0;
                        foreach (DmKoukaiSaki koukaisaki in koukaisakis)
                        {
                            if (vesselIds.Count > 0)
                            {
                                if (koukaisaki.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                                    continue;

                                if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船 &&
                                    vesselIds.Contains(koukaisaki.MsVesselID) == false)
                                    continue;
                            }
                            if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                            {
                                状況確認一覧Row clone2 = clone.Clone();
                                clone2.KoukaiSakiVesselId = koukaisaki.MsVesselID;

                                try
                                {
                                    clone2.船名 = (msVesselHash[koukaisaki.MsVesselID]) as string;
                                }
                                catch
                                {
                                }
                                ret.Add(clone2);
                                船公開先数++;
                            }
                        }
                        //if (船公開先数 == 0)
                        if (vesselIds.Count == 0 && 船公開先数 == 0)
                        {
                            ret.Add(clone);
                        }

                        sameVessel.Add(publisher.PublisherName, publisher.PublisherName);
                        clone.PublisherVesselId = publisher.MsVesselID;
                    }
                }

            }

            return ret;
        }

        private static bool 要確認チェック(List<DmKoukaiSaki> koukaisakis, int koukaisaki)
        {
            bool ret = false;

            var tmp = from p in koukaisakis
                      where p.KoukaiSaki == koukaisaki
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }

        private static bool 要確認チェック_部門(List<DmKoukaiSaki> koukaisakis, string bumonId)
        {
            bool ret = false;

            var tmp = from p in koukaisakis
                      where p.MsBumonID == bumonId
                      select p;

            if (tmp.Count<DmKoukaiSaki>() > 0)
            {
                ret = true;
            }

            return ret;
        }

        private static bool 要確認チェック_船(List<DmKoukaiSaki> koukaisakis, int vesselId)
        {
            bool ret = false;

            ret = 要確認チェック(koukaisakis, (int)NBaseData.DS.DocConstants.RoleEnum.船);
            if (ret)
            {
                var tmp = from p in koukaisakis
                          where p.MsVesselID == vesselId
                          select p;

                if (tmp.Count<DmKoukaiSaki>() > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }

        #region IGenericCloneable<状況確認一覧Row> メンバ

        public 状況確認一覧Row Clone()
        {
            状況確認一覧Row clone = new 状況確認一覧Row();

            clone.DmKanriKirokuId = DmKanriKirokuId;
            clone.DmKoubunshoKisokuId = DmKoubunshoKisokuId;
            clone.発行日 = 発行日;
            clone.分類名 = 分類名;
            clone.小分類名 = 小分類名;
            clone.文書番号 = 文書番号;
            clone.文書名 = 文書名;
            clone.MsDmHoukokushoId = MsDmHoukokushoId;
            clone.Status = Status;
            clone.FileName = FileName;
            clone.備考 = 備考;
            clone.PublisherFlag = PublisherFlag;
            clone.PublisherVesselId = PublisherVesselId;
            clone.KoukaiSakiVesselId = KoukaiSakiVesselId;
            clone.発行元 = 発行元;
            clone.船名 = 船名;
            clone.完了日 = 完了日;
            clone.完了者 = 完了者;

            if (KoukaisakiList != null)
            {
                clone.KoukaisakiList = new List<DmKoukaiSaki>();
                foreach (DmKoukaiSaki koukaisaki in KoukaisakiList)
                {
                    clone.KoukaisakiList.Add(koukaisaki);
                }
            }
            return clone;
        }

        #endregion
    }
}
