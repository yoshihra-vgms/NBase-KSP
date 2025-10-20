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

namespace NBaseData.BLC
{
    public class コメント登録処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool 登録(MsUser loginUser, DmDocComment docComment, DmKanriKirokuFile kanriKirokuFile, DmKoubunshoKisokuFile koubunshoKisokuFile)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            DmKanriKiroku kanriKiroku = null;
            DmKoubunshoKisoku koubunshoKisoku = null;
            DmKakuninJokyo kakuninJokyo = null;

            if (kanriKirokuFile != null)
            {
                kanriKiroku = DmKanriKiroku.GetRecord(loginUser, kanriKirokuFile.DmKanriKirokuID);
                kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiUser(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, kanriKirokuFile.DmKanriKirokuID, loginUser.MsUserID);
            }
            if (koubunshoKisokuFile != null)
            {
                koubunshoKisoku = DmKoubunshoKisoku.GetRecord(loginUser, koubunshoKisokuFile.DmKoubunshoKisokuID);
                kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiUser(loginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則, koubunshoKisokuFile.DmKoubunshoKisokuID, loginUser.MsUserID);
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // コメントの登録
                docComment.DmDocCommentID = 新規ID();
                docComment.RenewDate = 処理日時;
                docComment.RenewUserID = 更新者;

                ret = docComment.InsertRecord(dbConnect, loginUser);

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
                if (ret)
                {
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
                        //kakuninJokyo.DocFlag_SdManager = loginUser.DocFlag_SdManager;
                        kakuninJokyo.DocFlag_GL = loginUser.DocFlag_GL;
                        kakuninJokyo.DocFlag_TL = loginUser.DocFlag_TL;

                        kakuninJokyo.MsBumonID = loginUser.BumonID;

                        ret = kakuninJokyo.UpdateRecord(dbConnect, loginUser);
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

        private static bool 管理記録更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKanriKiroku kanriKiroku, DmKanriKirokuFile kanriKirokuFile)
        {
            bool ret = true;

            // 管理記録の更新
            kanriKiroku.FileName = kanriKirokuFile.FileName;
            kanriKiroku.FileUpdateDate = kanriKirokuFile.UpdateDate;
            kanriKiroku.RenewDate = 処理日時;
            kanriKiroku.RenewUserID = 更新者;
            ret = kanriKiroku.UpdateRecord(dbConnect, loginUser);

            if (ret)
            {
                // 管理記録ファイルの登録
                kanriKirokuFile.DmKanriKirokuFileID = 新規ID();
                kanriKirokuFile.DmKanriKirokuID = kanriKiroku.DmKanriKirokuID;
                kanriKirokuFile.RenewDate = 処理日時;
                kanriKirokuFile.RenewUserID = 更新者;
                ret = kanriKirokuFile.InsertRecord(dbConnect, loginUser);
            }

            if (ret)
            {
                // 事務所更新情報
                ret = 事務所更新情報処理.コメント登録(dbConnect, loginUser, kanriKiroku);
            }


            return ret;
        }

        private static bool 公文書規則更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile)
        {
            bool ret = true;

            // 公文書_規則の更新
            koubunshoKisoku.FileName = koubunshoKisokuFile.FileName;
            koubunshoKisoku.FileUpdateDate = koubunshoKisokuFile.UpdateDate;
            koubunshoKisoku.RenewDate = 処理日時;
            koubunshoKisoku.RenewUserID = 更新者;
            ret = koubunshoKisoku.UpdateRecord(dbConnect, loginUser);

            if (ret)
            {
                // 公文書_規則ファイルの登録
                koubunshoKisokuFile.DmKoubunshoKisokuFileID = 新規ID();
                koubunshoKisokuFile.DmKoubunshoKisokuID = koubunshoKisoku.DmKoubunshoKisokuID;
                koubunshoKisokuFile.RenewDate = 処理日時;
                koubunshoKisokuFile.RenewUserID = 更新者;
                ret = koubunshoKisokuFile.InsertRecord(dbConnect, loginUser);
            }

            if (ret)
            {
                // 事務所更新情報
                ret = 事務所更新情報処理.コメント登録(dbConnect, loginUser, koubunshoKisoku);
            }

            return ret;
        }
    }
}
