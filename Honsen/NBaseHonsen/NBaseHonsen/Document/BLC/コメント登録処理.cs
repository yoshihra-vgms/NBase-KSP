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
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping.Atts;
using NBaseUtil;
using NBaseHonsen.util;

namespace NBaseHonsen.Document.BLC
{
    public class コメント登録処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool Honsen登録(MsUser loginUser, DmDocComment docComment, DmKanriKirokuFile kanriKirokuFile, DmKoubunshoKisokuFile koubunshoKisokuFile)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            //int koukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
            int koukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;

            DmKanriKiroku kanriKiroku = null;
            DmKoubunshoKisoku koubunshoKisoku = null;
            DmKakuninJokyo kakuninJokyo = null;


            if (kanriKirokuFile != null)
            {
                kanriKiroku = DmKanriKiroku.GetRecord(loginUser, kanriKirokuFile.DmKanriKirokuID);
                kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, kanriKirokuFile.DmKanriKirokuID, koukaiSaki, loginUser.MsUserID);
            }
            if (koubunshoKisokuFile != null)
            {
                koubunshoKisoku = DmKoubunshoKisoku.GetRecord(loginUser, koubunshoKisokuFile.DmKoubunshoKisokuID);
                kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則, koubunshoKisokuFile.DmKoubunshoKisokuID, koukaiSaki, loginUser.MsUserID);
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // コメントの登録
                docComment.DmDocCommentID = 新規ID();
                docComment.RenewDate = 処理日時;
                docComment.RenewUserID = 更新者;

                ret = SyncTableSaver.InsertOrUpdate2(docComment, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                
                if (ret)
                {
                    if (kanriKirokuFile != null)
                    {
                        ret = 管理記録更新(dbConnect, 処理日時, 更新者, loginUser, kanriKiroku, kanriKirokuFile);
                    }
                    if (koubunshoKisokuFile != null)
                    {
                        ret = 公文書規則更新(dbConnect, 処理日時, 更新者, loginUser, koubunshoKisoku, koubunshoKisokuFile);
                    }
                }
                // 確認状況の更新
                if (kakuninJokyo != null)
                {
                    kakuninJokyo.KakuninDate = 処理日時;
                    kakuninJokyo.MsUserID = loginUser.MsUserID;

                    kakuninJokyo.DocFlag_CEO = loginUser.DocFlag_CEO;
                    kakuninJokyo.DocFlag_Admin = loginUser.DocFlag_Admin;
                    kakuninJokyo.DocFlag_MsiFerry = loginUser.DocFlag_MsiFerry;
                    kakuninJokyo.DocFlag_CrewFerry = loginUser.DocFlag_CrewFerry;
                    kakuninJokyo.DocFlag_TsiFerry = loginUser.DocFlag_TsiFerry;
                    //kakuninJokyo.DocFlag_MsiCargo = loginUser.DocFlag_MsiCargo;
                    //kakuninJokyo.DocFlag_CrewCargo = loginUser.DocFlag_CrewCargo;
                    //kakuninJokyo.DocFlag_TsiCargo = loginUser.DocFlag_TsiCargo;
                    kakuninJokyo.DocFlag_Officer = loginUser.DocFlag_Officer;
                    kakuninJokyo.DocFlag_GL = loginUser.DocFlag_GL;
                    kakuninJokyo.DocFlag_TL = loginUser.DocFlag_TL;
                    //kakuninJokyo.DocFlag_SdManager = loginUser.DocFlag_SdManager;

                    kakuninJokyo.MsBumonID = loginUser.BumonID;

                    ret = SyncTableSaver.InsertOrUpdate2(kakuninJokyo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                }
                if (ret)
                {
                    PtHonsenkoushinInfo honsenkoushinInfo = null;
                    if (kanriKirokuFile != null)
                    {
                        honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.コメント登録);
                    }
                    if (koubunshoKisokuFile != null)
                    {
                        honsenkoushinInfo = 本船更新情報.CreateInstance(koubunshoKisoku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.コメント登録);
                    }
                    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
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

        private static bool 管理記録更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile)
        {
            bool ret = true;

            kanriKiroku.FileName = kanriKirokuFile.FileName;
            kanriKiroku.FileUpdateDate = kanriKirokuFile.UpdateDate;
            kanriKiroku.RenewDate = 処理日時;
            kanriKiroku.RenewUserID = 更新者;
            ret = SyncTableSaver.InsertOrUpdate2(kanriKiroku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

            if (ret)
            {
                kanriKirokuFile.DmKanriKirokuFileID = 新規ID();
                kanriKirokuFile.RenewDate = 処理日時;
                kanriKirokuFile.RenewUserID = 更新者;
                ret = SyncTableSaver.InsertOrUpdate2(kanriKirokuFile, loginUser, StatusUtils.通信状況.未同期, dbConnect);
            }

            return ret;
        }

        private static bool 公文書規則更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile)
        {
            bool ret = true;

            koubunshoKisoku.FileName = koubunshoKisokuFile.FileName;
            koubunshoKisoku.FileUpdateDate = koubunshoKisokuFile.UpdateDate;
            koubunshoKisoku.RenewDate = 処理日時;
            koubunshoKisoku.RenewUserID = 更新者;
            ret = SyncTableSaver.InsertOrUpdate2(koubunshoKisoku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

            if (ret)
            {
                koubunshoKisokuFile.DmKoubunshoKisokuFileID = 新規ID();
                koubunshoKisokuFile.RenewDate = 処理日時;
                koubunshoKisokuFile.RenewUserID = 更新者;
                ret = SyncTableSaver.InsertOrUpdate2(koubunshoKisokuFile, loginUser, StatusUtils.通信状況.未同期, dbConnect);
            }

            return ret;
        }
    }
}
