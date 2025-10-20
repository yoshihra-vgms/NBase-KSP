using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 貨物マスタ更新処理
    {
        public static bool 追加処理(MsUser loginUser, MsCargo Cargo)
        {
            // 運用上、貨物-輸送品目は前月分の処理をするので、開始日は処理日の月の前月とする
            DateTime startDay = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    //if (Cargo.MsYusoItemID > 0)
                    //{
                    //    // 貨物-輸送品目レコードを作成
                    //    MsYusoItem yusoItem = MsYusoItem.GetRecord(loginUser, Cargo.MsYusoItemID);
                    //    if (yusoItem != null)
                    //    {
                    //        MsCargoYusoItem cargoYusoItem = new MsCargoYusoItem();

                    //        cargoYusoItem.StartDay = startDay;
                    //        cargoYusoItem.EndDay = DateTime.MinValue;
                    //        cargoYusoItem.MsCargoID = Cargo.MsCargoID;
                    //        cargoYusoItem.MsYusoItemID = Cargo.MsYusoItemID;
                    //        cargoYusoItem.YusoItemCode = yusoItem.YusoItemCode;
                    //        cargoYusoItem.YusoItemName = yusoItem.YusoItemName;
                    //        cargoYusoItem.SenshuCode = yusoItem.SenshuCode;
                    //        cargoYusoItem.SenshuName = yusoItem.SenshuName;

                    //        // 貨物-輸送品目レコードの登録
                    //        ret = cargoYusoItem.InsertRecord(loginUser);
                    //    }
                    //}
                    //if (ret)
                    //{
                    //    // 貨物マスタレコードの登録
                    //    ret = Cargo.InsertRecord(loginUser);
                    //}

                    // 貨物マスタレコードの登録
                    Cargo.UserKey = "0";
                    Cargo.RenewDate = DateTime.Now;
                    Cargo.RenewUserID = loginUser.MsUserID;
                    ret = Cargo.InsertRecord(dbConnect, loginUser);
                    if (ret && Cargo.MsYusoItemID > 0)
                    {
                        Cargo.MsCargoID = Sequences.GetMsCargoId(dbConnect, loginUser);

                        // 貨物-輸送品目レコードを作成
                        MsYusoItem yusoItem = MsYusoItem.GetRecord(dbConnect, loginUser, Cargo.MsYusoItemID);
                        if (yusoItem != null)
                        {
                            MsCargoYusoItem cargoYusoItem = new MsCargoYusoItem();

                            cargoYusoItem.StartDay = startDay;
                            cargoYusoItem.EndDay = DateTime.MinValue;
                            cargoYusoItem.MsCargoID = Cargo.MsCargoID;
                            cargoYusoItem.MsYusoItemID = Cargo.MsYusoItemID;
                            cargoYusoItem.YusoItemCode = yusoItem.YusoItemCode;
                            cargoYusoItem.YusoItemName = yusoItem.YusoItemName;
                            cargoYusoItem.SenshuCode = yusoItem.SenshuCode;
                            cargoYusoItem.SenshuName = yusoItem.SenshuName;

                            // 貨物-輸送品目レコードの登録
                            ret = cargoYusoItem.InsertRecord(dbConnect, loginUser);
                        }
                    }

                    dbConnect.Commit();
                }
                catch(Exception ex)
                {
                    ret = false;
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        
        public static bool 更新処理(MsUser loginUser, MsCargo Cargo)
        {
            // 運用上、貨物-輸送品目は前月分の処理をするので、開始日は処理日の月の前月とする
            DateTime startDay = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
            // 以前の貨物-輸送品目は開始月の前月とする
            DateTime endDay = startDay.AddDays(-1);
            bool 輸送品目を追加する = true;
            
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    MsCargoYusoItem cargoYusoItem = MsCargoYusoItem.GetRecordByMsCargoID(dbConnect, loginUser, Cargo.MsCargoID);
                    if (cargoYusoItem != null)
                    {
                        if (Cargo.MsYusoItemID > 0)
                        {
                            MsYusoItem yusoItem = MsYusoItem.GetRecord(dbConnect, loginUser, Cargo.MsYusoItemID);
                            if (cargoYusoItem.YusoItemCode == yusoItem.YusoItemCode &&
                                 cargoYusoItem.YusoItemName == yusoItem.YusoItemName &&
                                 cargoYusoItem.SenshuCode == yusoItem.SenshuCode &&
                                 cargoYusoItem.SenshuName == yusoItem.SenshuName)
                            {
                                // 輸送品目の変更なし
                                輸送品目を追加する = false;
                            }
                            else
                            {
                                // 現在設定されている貨物-輸送品目レコードの終了日を埋める
                                // (過去のレコードとマークする)
                                if (cargoYusoItem.StartDay > endDay)
                                {
                                    // 日付の逆転が見られるときは、同じ月内に処理を繰り返されているので、
                                    // 削除フラグを立てる
                                    endDay = cargoYusoItem.StartDay.AddMonths(1).AddDays(-1);
                                    cargoYusoItem.DeleteFlag = 1;
                                }
                                cargoYusoItem.EndDay = endDay;
                                ret = cargoYusoItem.UpdateRecord(dbConnect, loginUser);
                            }
                        }
                        else
                        {
                            // 現在設定されている貨物-輸送品目レコードの終了日を埋める
                            // (過去のレコードとマークする)
                            if (cargoYusoItem.StartDay > endDay)
                            {
                                // 日付の逆転が見られるときは、同じ月内に処理を繰り返されているので、
                                // 削除フラグを立てる
                                endDay = cargoYusoItem.StartDay.AddMonths(1).AddDays(-1);
                                cargoYusoItem.DeleteFlag = 1;
                            }
                            cargoYusoItem.EndDay = endDay;
                            ret = cargoYusoItem.UpdateRecord(dbConnect, loginUser);
                        }
                    }

                    if (輸送品目を追加する == true && Cargo.MsYusoItemID > 0)
                    {
                        // 貨物-輸送品目レコードを作成
                        MsYusoItem yusoItem = MsYusoItem.GetRecord(dbConnect, loginUser, Cargo.MsYusoItemID);
                        if (yusoItem != null)
                        {
                            cargoYusoItem = new MsCargoYusoItem();

                            cargoYusoItem.StartDay = startDay;
                            cargoYusoItem.EndDay = DateTime.MinValue;
                            cargoYusoItem.MsCargoID = Cargo.MsCargoID;
                            cargoYusoItem.MsYusoItemID = Cargo.MsYusoItemID;
                            cargoYusoItem.YusoItemCode = yusoItem.YusoItemCode;
                            cargoYusoItem.YusoItemName = yusoItem.YusoItemName;
                            cargoYusoItem.SenshuCode = yusoItem.SenshuCode;
                            cargoYusoItem.SenshuName = yusoItem.SenshuName;

                            // 貨物-輸送品目レコードの登録
                            ret = cargoYusoItem.InsertRecord(dbConnect, loginUser);
                        }
                    }

                    if (ret)
                    {
                        // 貨物マスタレコードの登録
                        ret = Cargo.UpdateRecord(dbConnect, loginUser);
                    }

                    dbConnect.Commit();
                }
                catch(Exception ex)
                {
                    ret = false;
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
    }
}
