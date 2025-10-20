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
    public class 公文書_規則処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool Honsen登録(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 公文書_規則の登録
                koubunshoKisoku.DmKoubunshoKisokuID = 新規ID();
                koubunshoKisoku.RenewDate = 処理日時;
                koubunshoKisoku.RenewUserID = 更新者;

                ret = SyncTableSaver.InsertOrUpdate2(koubunshoKisoku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                if (ret)
                {
                    // 公文書_規則ファイルの登録
                    koubunshoKisokuFile.DmKoubunshoKisokuFileID = 新規ID();
                    koubunshoKisokuFile.DmKoubunshoKisokuID = koubunshoKisoku.DmKoubunshoKisokuID;
                    koubunshoKisokuFile.RenewDate = 処理日時;
                    koubunshoKisokuFile.RenewUserID = 更新者;

                    ret = SyncTableSaver.InsertOrUpdate2(koubunshoKisokuFile, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                    
                    if (ret)
                    {
                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = koubunshoKisoku.DmKoubunshoKisokuID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = SyncTableSaver.InsertOrUpdate2(publisher, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                            if (!ret)
                                break;
                        }
                        if (ret)
                        {
                            // 公開先の登録
                            foreach (DmKoukaiSaki kouakiSaki in koukaiSakis)
                            {
                                kouakiSaki.DmKoukaiSakiID = 新規ID();
                                kouakiSaki.LinkSakiID = koubunshoKisoku.DmKoubunshoKisokuID;
                                kouakiSaki.RenewDate = 処理日時;
                                kouakiSaki.RenewUserID = 更新者;

                                ret = SyncTableSaver.InsertOrUpdate2(kouakiSaki, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                                if (!ret)
                                    break;
                            }
                        }
                    }
                }
                if (ret)
                {
                    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(koubunshoKisoku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.公文書_規則登録);
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
        public static bool Honsen更新(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 管理記録の登録
                koubunshoKisoku.RenewDate = 処理日時;
                koubunshoKisoku.RenewUserID = 更新者;

                ret = SyncTableSaver.InsertOrUpdate2(koubunshoKisoku, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                //if (ret)
                //{
                //    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.管理文書登録);
                //    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                //}

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
