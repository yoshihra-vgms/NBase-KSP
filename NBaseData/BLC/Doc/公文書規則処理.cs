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
    public class 公文書規則処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 公文書_規則を登録する
        /// 　　[Wing] - 「公文書_規則登録」画面からCallされる
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="koubunshoKisoku"></param>
        /// <param name="koubunshoKisokuFile"></param>
        /// <param name="publishers"></param>
        /// <param name="koukaiSakis"></param>
        /// <returns></returns>
        public static bool 登録(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
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

                ret = koubunshoKisoku.InsertRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 公文書_規則ファイルの登録
                    koubunshoKisokuFile.DmKoubunshoKisokuFileID = 新規ID();
                    koubunshoKisokuFile.DmKoubunshoKisokuID = koubunshoKisoku.DmKoubunshoKisokuID;
                    koubunshoKisokuFile.RenewDate = 処理日時;
                    koubunshoKisokuFile.RenewUserID = 更新者;

                    ret = koubunshoKisokuFile.InsertRecord(dbConnect, loginUser);
                    
                    if (ret)
                    {
                        // 発行元の登録
                        foreach (DmPublisher publisher in publishers)
                        {
                            publisher.DmPublisherID = 新規ID();
                            publisher.LinkSakiID = koubunshoKisoku.DmKoubunshoKisokuID;
                            publisher.RenewDate = 処理日時;
                            publisher.RenewUserID = 更新者;

                            ret = publisher.InsertRecord(dbConnect, loginUser);

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

                                ret = kouakiSaki.InsertRecord(dbConnect, loginUser);

                                if (!ret)
                                    break;
                            }
                        }
                    }
                }

                if (ret)
                {
                    // 事務所更新情報
                    ret = 事務所更新情報処理.公文書規則登録(dbConnect, loginUser, koubunshoKisoku, koukaiSakis);
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

        /// <summary>
        /// 公文書_規則の公開先を変更する
        /// 　　[Wing] - 「公開先設定」画面からCallされる
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="koubunshoKisokuId"></param>
        /// <param name="koukaiSakis"></param>
        /// <returns></returns>
        public static bool 公開先更新(MsUser loginUser, string koubunshoKisokuId, List<DmKoukaiSaki> koukaiSakis)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            DmKoubunshoKisoku koubunshoKisoku = DmKoubunshoKisoku.GetRecord(loginUser, koubunshoKisokuId);

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 更新前の公開先は、すべて削除とする
                ret = DmKoukaiSaki.LogicalDelete(dbConnect, loginUser, koubunshoKisokuId);

                if (ret)
                {
                    // 公開先の登録
                    foreach (DmKoukaiSaki kouakiSaki in koukaiSakis)
                    {
                        kouakiSaki.DmKoukaiSakiID = 新規ID();
                        kouakiSaki.LinkSakiID = koubunshoKisokuId;
                        kouakiSaki.RenewDate = 処理日時;
                        kouakiSaki.RenewUserID = 更新者;

                        ret = kouakiSaki.InsertRecord(dbConnect, loginUser);

                        if (!ret)
                            break;
                    }
                }

                if (ret)
                {
                    // 公文書_規則の更新
                    koubunshoKisoku.RenewDate = 処理日時;
                    koubunshoKisoku.RenewUserID = 更新者;

                    ret = koubunshoKisoku.UpdateRecord(dbConnect, loginUser);
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


        #region 使用していない

        //public static bool 更新(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, DmKoubunshoKisokuFile koubunshoKisokuFile, List<DmPublisher> publishers, List<DmKoukaiSaki> koukaiSakis)
        //{
        //    bool ret = true;
        //    DateTime 処理日時 = DateTime.Now;
        //    string 更新者 = loginUser.MsUserID;

        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        // 公文書_規則の更新
        //        koubunshoKisoku.RenewDate = 処理日時;
        //        koubunshoKisoku.RenewUserID = 更新者;

        //        ret = koubunshoKisoku.UpdateRecord(dbConnect, loginUser);

        //        if (ret)
        //        {
        //            // 公文書_規則ファイルの更新
        //            koubunshoKisokuFile.RenewDate = 処理日時;
        //            koubunshoKisokuFile.RenewUserID = 更新者;

        //            ret = koubunshoKisokuFile.UpdateRecord(dbConnect, loginUser);

        //            if (ret)
        //            {
        //                // 更新前の発行元は、すべて削除とする
        //                ret = DmPublisher.LogicalDelete(dbConnect, loginUser, koubunshoKisoku.DmKoubunshoKisokuID);

        //                if (ret)
        //                {
        //                    // 発行元の登録
        //                    foreach (DmPublisher publisher in publishers)
        //                    {
        //                        publisher.DmPublisherID = 新規ID();
        //                        publisher.LinkSakiID = koubunshoKisoku.DmKoubunshoKisokuID;
        //                        publisher.RenewDate = 処理日時;
        //                        publisher.RenewUserID = 更新者;

        //                        ret = publisher.InsertRecord(dbConnect, loginUser);

        //                        if (!ret)
        //                            break;
        //                    }
        //                    if (ret)
        //                    {
        //                        // 更新前の公開先は、すべて削除とする
        //                        ret = DmKoukaiSaki.LogicalDelete(dbConnect, loginUser, koubunshoKisoku.DmKoubunshoKisokuID);

        //                        if (ret)
        //                        {
        //                            // 公開先の登録
        //                            foreach (DmKoukaiSaki kouakiSaki in koukaiSakis)
        //                            {
        //                                kouakiSaki.DmKoukaiSakiID = 新規ID();
        //                                kouakiSaki.LinkSakiID = koubunshoKisoku.DmKoubunshoKisokuID;
        //                                kouakiSaki.RenewDate = 処理日時;
        //                                kouakiSaki.RenewUserID = 更新者;

        //                                ret = kouakiSaki.InsertRecord(dbConnect, loginUser);

        //                                if (!ret)
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (ret)
        //        {
        //            dbConnect.Commit();
        //        }
        //        else
        //        {
        //            dbConnect.RollBack();
        //        }

        //    }

        //    return ret;
        //}

        //public static bool 削除(MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku)
        //{
        //    bool ret = true;
        //    DateTime 処理日時 = DateTime.Now;
        //    string 更新者 = loginUser.MsUserID;

        //    DmKoubunshoKisokuFile koubunshoKisokuFile = DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(loginUser, koubunshoKisoku.DmKoubunshoKisokuID);

        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        // 公文書_規則の更新
        //        koubunshoKisoku.DeleteFlag = 1;
        //        koubunshoKisoku.RenewDate = 処理日時;
        //        koubunshoKisoku.RenewUserID = 更新者;

        //        ret = koubunshoKisoku.UpdateRecord(dbConnect, loginUser);

        //        if (ret)
        //        {
        //            // 公文書_規則ファイルの更新
        //            koubunshoKisokuFile.DeleteFlag = 1;
        //            koubunshoKisokuFile.RenewDate = 処理日時;
        //            koubunshoKisokuFile.RenewUserID = 更新者;

        //            ret = koubunshoKisokuFile.UpdateRecord(dbConnect, loginUser);

        //            if (ret)
        //            {
        //                // 発行元は、すべて削除とする
        //                ret = DmPublisher.LogicalDelete(dbConnect, loginUser, koubunshoKisoku.DmKoubunshoKisokuID);

        //                if (ret)
        //                {
        //                    // 公開先は、すべて削除とする
        //                    ret = DmKoukaiSaki.LogicalDelete(dbConnect, loginUser, koubunshoKisoku.DmKoubunshoKisokuID);
        //                }
        //            }
        //        }

        //        if (ret)
        //        {
        //            dbConnect.Commit();
        //        }
        //        else
        //        {
        //            dbConnect.RollBack();
        //        }

        //    }

        //    return ret;
        //}

        #endregion
    }
}
