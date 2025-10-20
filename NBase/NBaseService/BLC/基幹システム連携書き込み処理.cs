using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ORMapping;
using NBaseData.DAC;
using NBaseData.DS;


namespace NBaseService
{
    public partial interface IService
    {
        // 外からは呼ばないので・・・
        //[OperationContract]
        //string BLC_基幹システム連携書き込み処理_支払連携(MsUser loginUser, string odShrId);

        // 外からは呼ばないので・・・
        //[OperationContract]
        //bool BLC_基幹システム連携書き込み処理_概算連携(MsUser loginUser, List<string> list_odJryID, string gaisan_yyyymm);
    }

    public partial class Service
    {
        public string BLC_基幹システム連携書き込み処理_支払連携(MsUser loginUser, string odShrId)
        {
            string ret = "00";
            Tajsinseiif tajsinseiif = new Tajsinseiif();

            NBaseCommon.LogFile.Write(loginUser.FullName, "BLC_基幹システム連携書き込み処理_支払連携");

            using (DBConnect Connect = new DBConnect())
            {
                //Connect.BeginTransaction();


                // 連携するデータを取得する
                OdShr odShr = OdShr.GetRecordsFor基幹連携(Connect, loginUser, odShrId);

                // 2013.03: 2013年度改造
                if (odShr.OdThi_MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕) &&
                    odShr.OdThi_MsThiIraiShousaiID != MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.部品購入))
                {
                    // 入渠費の科目を置き換える
                    // nyukyoKamokuId = null にすると、入渠費以外の科目を取得できる
                    MsKamoku kamoku = MsKamoku.GetRecordByHachuKamoku(Connect, loginUser, odShr.OdThi_MsThiIraiSbtID, odShr.OdThi_MsThiIraiShousaiID, null);

                    odShr.KamokuNo = kamoku.KamokuNo;
                    odShr.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;
                }

                NBaseCommon.LogFile.Write(loginUser.FullName, "ShrTantou = " + odShr.ShrTantou);
                MsUser tntsy = MsUser.GetRecordsByUserID(loginUser, odShr.ShrTantou);
                if (tntsy == null)
                {
                    NBaseCommon.LogFile.Write(loginUser.FullName, "ShrTantou = null");
                }

                #region インターフェイスサーバーにInsertする

                // 集約管理番号
                tajsinseiif.SumKanriNo = "S" + odShr.ShrNo;
                // 申請識別
                tajsinseiif.SinseiKanriNo = odShr.ShrNo;
                // サブシステムコード
                tajsinseiif.SubsystemCd = tajsinseiif.支払_サブシステムコード;
                // 業務コード
                tajsinseiif.GyomuCd = tajsinseiif.支払_業務コード;
                // 取引コード
                tajsinseiif.TrhkCd = tajsinseiif.支払_取引コード;
                // 処理コード
                tajsinseiif.SyoriCd = tajsinseiif.支払_処理コード;
                // 運用会社コード
                tajsinseiif.KisyCd = tajsinseiif.支払_運用会社コード;
                // 作成担当コード
                tajsinseiif.DatamkTntsyCd = odShr.ShrTantou;
                // 船舶NO
                if (odShr.MsVessel_VesselNo == Tajsinseiif.全社共通船コード)
                {
                    tajsinseiif.FuneNo = null;
                }
                else
                {
                    tajsinseiif.FuneNo = odShr.MsVessel_VesselNo;
                }
                // 申請担当者コード
                tajsinseiif.TnsyCd = odShr.ShrTantou;
                // 計上日付
                tajsinseiif.KeijoYmd = DateTime.Now;
                // 入出金予定年月日
                tajsinseiif.KinYoteiYmd = odShr.ShrIraiDate;
                // 取引先コード
                tajsinseiif.TrhkskCd = odShr.OdMk_MsCustomerID;
                // 通貨コード
                tajsinseiif.TukaCd = tajsinseiif.支払_通貨コード;
                // 基本摘要
                tajsinseiif.KihonTekiyo = odShr.Tekiyou;
                // 承認パターンコード
                tajsinseiif.SyoninPatternCd = tajsinseiif.支払_承認パターンコード;
                // 行番号
                tajsinseiif.LineNo = "0001";
                // 勘定科目
                tajsinseiif.KanjoKmkCd = odShr.KamokuNo;
                // 内訳科目
                tajsinseiif.UtiwakeKmkCd = odShr.UtiwakeKamokuNo;
                // 消費税コード
                tajsinseiif.SyhzCd = tajsinseiif.支払_消費税コード; // 対象外
                // 金額
                //tajsinseiif.Gaku = odShr.Amount - odShr.NebikiAmount + odShr.Tax;
                tajsinseiif.Gaku = odShr.Amount + odShr.Carriage - odShr.NebikiAmount + odShr.Tax;
                // 発生期間（開始）
                tajsinseiif.HsiKikanFrom = odShr.OdJry_JryDate.ToString("yyyyMMddhhmm");
                // 発生期間（終了）
                tajsinseiif.HsiKikanTo = odShr.OdJry_JryDate.ToString("yyyyMMddhhmm");
                // 作成日時
                tajsinseiif.CreateDate = DateTime.Now;
                // 状態
                tajsinseiif.SyoriStatus = "00";
                // 外部システム識別コード
                tajsinseiif.ExtSystemCd = tajsinseiif.支払_外部システム識別コード;
                // 更新プログラムID
                tajsinseiif.Updpgmid = tajsinseiif.支払_更新プログラムID;
                // 更新ユーザーID
                tajsinseiif.Upduserid = loginUser.LoginID;
                // 更新日時
                string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");// TODO : ミリ秒までなので、"yyyyMMddhhmmssfff"　では？
                tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);


                // 2015.12 基幹連携対応
                tajsinseiif.TnsyLoginId = tntsy != null ? tntsy.LoginID : null;
                NBaseCommon.LogFile.Write(loginUser.FullName, "TnsyLoginId = " + tajsinseiif.TnsyLoginId);


                #endregion
                try
                {
                    //bool flag = tajsinseiif.InsertRecord(Connect, loginUser);
                    //Connect.Commit();

                    //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
                    //{
                    //    ret = serviceClient.BLC_基幹システム連携書き込み処理_支払連携(loginUser, tajsinseiif);                                                               
                    //}
                }
                catch( Exception E )
                {
                    //Connect.RollBack();
                    // インサート失敗
                    // その他エラー = 99
                    return "99";
                }
            }
            //using (DBConnect Connect = new DBConnect())
            //{
            //    // ストアドプロシージャのコール
            //    ret = Tajsinseiif.CallSP_支払い(Connect, loginUser, tajsinseiif);
            //    if (ret != "0")
            //    {
            //        // ストアドプロシージャ失敗
            //        // その他エラー = 99
            //        return "99";
            //    }

            //    // インターフェイスサーバーから処理結果を取得する
            //    Tajsinseiif resTajsinseiif = Tajsinseiif.GetRecord(Connect, loginUser,
            //        tajsinseiif.SumKanriNo, tajsinseiif.SinseiKanriNo, tajsinseiif.SubsystemCd, tajsinseiif.GyomuCd,
            //        tajsinseiif.TrhkCd, tajsinseiif.SyoriCd, tajsinseiif.LineNo, tajsinseiif.ExtSystemCd);
            //    ret = resTajsinseiif.SyoriStatus;
            //}

            return ret;
        }

        public bool BLC_基幹システム連携書き込み処理_概算連携(DBConnect dbConnect, MsUser loginUser, List<OdGaisanKeijo> odGaisanKeijos, string gaisan_yyyymm, List<MsVessel> msVesselList, List<NBaseData.BLC.貯蔵品リスト> 潤滑油リストALL, List<NBaseData.BLC.貯蔵品リスト> 船用品リストALL)
        {
            bool ret = true;

            try
            {
                List<Tajsinseiif> gaisanList = new List<Tajsinseiif>();
                List<Tajsinseiif> loList = new List<Tajsinseiif>();
                List<Tajsinseiif> itemList = new List<Tajsinseiif>();

                string 概算担当者 = System.Configuration.ConfigurationManager.AppSettings["概算担当者"];
                string 貯蔵品担当者 = 概算担当者;

                MsParameter msParam = MsParameter.GetRecord(dbConnect, loginUser, "概算担当者");
                if (msParam != null)
                {
                    概算担当者 = msParam.Value;
                }
                msParam = MsParameter.GetRecord(dbConnect, loginUser, "貯蔵品担当者");
                if (msParam != null)
                {
                    貯蔵品担当者 = msParam.Value;
                }

                // 2014.03: 2013年度改造
                List<MsTax> msTaxList = MsTax.GetRecords(dbConnect, loginUser);


                string sumKanriNo = "";
                int lineNo = 0;
                int maxLineNo = 15; // 1つの集約管理番号につきレコードは15とする


                #region 概算データをIFサーバへ

                var Kamokus = (from p in odGaisanKeijos
                               select p.KamokuNo).Distinct();
                foreach (string kmk in Kamokus)
                {
                    var tmp = from p in odGaisanKeijos
                              where p.KamokuNo == kmk
                              orderby p.UtiwakeKamokuNo
                              select p;

                    var UtiwakeKamokus = (from p in tmp
                                          select p.UtiwakeKamokuNo).Distinct();

                    foreach (string uwkmk in UtiwakeKamokus)
                    {
                        var tmp2 = from p in tmp
                                   where p.UtiwakeKamokuNo == uwkmk
                                   select p;

                        sumKanriNo = "";
                        lineNo = 0;
                        foreach (OdGaisanKeijo odGaisanKeijo in tmp2)
                        {
                            string odJryID = "";
                            string sinseiKanriNo = "";
                            string kamokuNo = "";
                            string utiwakeKmkCd = "";
                            string AiteKamokuNo = "";
                            string AiteUtiwakeKmkCd = "";
                            decimal amount = 0;
                            decimal nebikiAmount = 0;
                            decimal tax = 0;
                            string tekiyou = "";
                            string naiyou = "";

                            // データの準備
                            #region

                            // 支払
                            if (odGaisanKeijo.OdShrID != null)
                            {
                                OdShr odShr = OdShr.GetRecord(loginUser, odGaisanKeijo.OdShrID);
                                odJryID = odShr.OdJryID;

                                sinseiKanriNo = odShr.ShrNo;
                                kamokuNo = odGaisanKeijo.KamokuNo;
                                utiwakeKmkCd = odGaisanKeijo.UtiwakeKamokuNo;
                                amount = odGaisanKeijo.Amount;
                                nebikiAmount = odGaisanKeijo.NebikiAmount;
                                tax = odGaisanKeijo.Tax;
                                naiyou = odGaisanKeijo.Naiyou;
                            }
                            // 受領
                            else
                            {
                                OdJry Jry = OdJry.GetRecord(loginUser, odGaisanKeijo.OdJryID);
                                odJryID = odGaisanKeijo.OdJryID;

                                sinseiKanriNo = Jry.JryNo;
                                kamokuNo = odGaisanKeijo.KamokuNo;
                                utiwakeKmkCd = odGaisanKeijo.UtiwakeKamokuNo;
                                amount = odGaisanKeijo.Amount;
                                nebikiAmount = odGaisanKeijo.NebikiAmount;
                                tax = odGaisanKeijo.Tax;
                                naiyou = odGaisanKeijo.Naiyou;
                            }
                            if (odGaisanKeijo.ThiIraiSbtID == NBaseData.DAC.MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
                            {
                                tekiyou = "船用品概算計上";
                            }
                            else if (odGaisanKeijo.ThiIraiSbtID == NBaseData.DAC.MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油))
                            {
                                tekiyou = "潤滑油概算計上";
                            }
                            else if (odGaisanKeijo.ThiIraiShousaiID == NBaseData.DAC.MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.部品購入))
                            {
                                tekiyou = "小修理概算計上";
                            }
                            else
                            {
                                tekiyou = "入渠概算計上";
                            }


                            if (tax == decimal.MinValue)
                            {
                                tax = 0;
                            }
                            MsKamoku kamoku = MsKamoku.GetRecordByFurikaeKamoku(loginUser, kamokuNo, utiwakeKmkCd);
                            if (kamoku != null)
                            {
                                AiteKamokuNo = kamoku.KamokuNo;
                                AiteUtiwakeKmkCd = kamoku.UtiwakeKamokuNo;
                            }

                            OdJry odJry = OdJry.GetRecordsFor基幹連携(loginUser, odJryID);

                            #endregion

                            // インターフェイスサーバーにInsertする
                            #region

                            // 集約管理番号の採番
                            // 1つの集約管理番号につきレコードは15とする           
                            if (lineNo == 0 || lineNo == maxLineNo)
                            {
                                //sumKanriNo = Tajsinseiif.GetSumKanriNo(dbConnect, loginUser, "G" + DateTime.Now.ToString("yyyyMMdd"));
                                sumKanriNo = System.Guid.NewGuid().ToString();
                                lineNo = 0;
                            }
                            lineNo += 1;

                            Tajsinseiif tajsinseiif = new Tajsinseiif();

                            // 集約管理番号
                            tajsinseiif.SumKanriNo = sumKanriNo;
                            // 申請識別
                            tajsinseiif.SinseiKanriNo = sinseiKanriNo;
                            // サブシステムコード
                            tajsinseiif.SubsystemCd = tajsinseiif.概算_サブシステムコード;
                            // 業務コード
                            tajsinseiif.GyomuCd = tajsinseiif.概算_業務コード;
                            // 取引コード
                            tajsinseiif.TrhkCd = tajsinseiif.概算_取引コード;
                            // 処理コード
                            tajsinseiif.SyoriCd = tajsinseiif.概算_発注_処理コード;
                            // 運用会社コード
                            tajsinseiif.KisyCd = tajsinseiif.概算_運用会社コード;
                            // 作成担当コード
                            tajsinseiif.DatamkTntsyCd = 概算担当者;
                            // 船舶NO
                            if (odJry.MsVessel_VesselNo == Tajsinseiif.全社共通船コード)
                            {
                                tajsinseiif.FuneNo = null;
                            }
                            else
                            {
                                tajsinseiif.FuneNo = odJry.MsVessel_VesselNo;
                            }
                            // 申請担当者コード
                            tajsinseiif.TnsyCd = 概算担当者;
                            // 計上日付
                            tajsinseiif.KeijoYmd = GetGaisanDate(gaisan_yyyymm);
                            // 取引先コード
                            tajsinseiif.TrhkskCd = odJry.OdMk_MsCustomerID;
                            // 通貨コード
                            tajsinseiif.TukaCd = tajsinseiif.概算_通貨コード;
                            // 基本摘要
                            tajsinseiif.KihonTekiyo = gaisan_yyyymm.Substring(4, 2) + "月分 " + tekiyou;
                            // 承認パターンコード
                            tajsinseiif.SyoninPatternCd = tajsinseiif.概算_承認パターンコード;
                            // 行番号
                            tajsinseiif.LineNo = String.Format("{0:0000}", lineNo);
                            // 勘定科目
                            tajsinseiif.KanjoKmkCd = kamokuNo;
                            // 内訳科目
                            tajsinseiif.UtiwakeKmkCd = utiwakeKmkCd;
                            // 相手勘定科目
                            tajsinseiif.AiteKanjoKmkCd = AiteKamokuNo;
                            // 相手内訳科目
                            tajsinseiif.AiteUtiwakeKmkCd = AiteUtiwakeKmkCd;
                            // 消費税コード
                            if (tax == 0)
                            {
                                tajsinseiif.SyhzCd = tajsinseiif.概算_消費税コード_免税;
                            }
                            else
                            {
                                // 2014.03: 2013年度改造
                                //tajsinseiif.SyhzCd = tajsinseiif.概算_消費税コード_外税;
                                var tmpTaxList = msTaxList.Where(obj => obj.StartDate <= odJry.JryDate).OrderByDescending(obj => obj.StartDate);
                                if (tmpTaxList.Count() > 0)
                                {
                                    tajsinseiif.SyhzCd = tmpTaxList.First().TaxCode;
                                }
                            }
                            // 金額
                            tajsinseiif.Gaku = amount - nebikiAmount;
                            // 消費税額
                            tajsinseiif.SyohizeiGaku = tax;
                            // 摘要
                            tajsinseiif.MeisaiTekiyo = gaisan_yyyymm.Substring(4, 2) + "月分 " + tekiyou + " " + naiyou;
                            // 作成日時
                            tajsinseiif.CreateDate = DateTime.Now;
                            // 状態
                            tajsinseiif.SyoriStatus = "00";
                            // 外部システム識別コード
                            tajsinseiif.ExtSystemCd = tajsinseiif.概算_更新プログラムID;
                            // 更新プログラムID
                            tajsinseiif.Updpgmid = tajsinseiif.概算_更新プログラムID;
                            // 更新ユーザーID
                            tajsinseiif.Upduserid = loginUser.LoginID;
                            // 更新日時
                            string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                            tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);

                            //bool flag = tajsinseiif.InsertRecord(dbConnect, loginUser);

                            #endregion

                            gaisanList.Add(tajsinseiif);
                        }


                    }
                }

                #endregion

                #region 貯蔵品データ（潤滑油）をIFサーバへ

                int year = int.Parse(gaisan_yyyymm.Substring(0, 4));
                int month = int.Parse(gaisan_yyyymm.Substring(4, 2));
                sumKanriNo = "";
                lineNo = 0;

                // 貯蔵品データは船毎に１レコードとしてIFサーバへ
                foreach (MsVessel msVessel in msVesselList)
                {
                    // 合計金額を算出する
                    decimal kingaku = 0;
                    #region
                    var 潤滑油リスト = from p in 潤滑油リストALL
                                 where p.MS_VESSEL_ID == msVessel.MsVesselID
                                 select p;
                    foreach (var row in 潤滑油リスト)
                    {
                        if (row.支払単価 > 0)
                        {
                            kingaku += (row.支払数 - row.消費) * row.支払単価;
                        }
                        else
                        {
                            kingaku += (row.受領数 - row.消費) * row.受領単価;
                        }
                    }
                    #endregion

                    // インターフェイスサーバーにInsertする
                    #region
                    //// 集約管理番号の採番
                    //// 1つの集約管理番号につきレコードは15とする           
                    //if (lineNo == 0 || lineNo == maxLineNo)
                    //{
                    //    sumKanriNo = Tajsinseiif.GetSumKanriNo(dbConnect, loginUser, "T" + DateTime.Now.ToString("yyyyMMdd"));
                    //    lineNo = 0;
                    //}
                    //lineNo += 1;

                    Tajsinseiif tajsinseiif = new Tajsinseiif();

                    //// 集約管理番号
                    //tajsinseiif.SumKanriNo = sumKanriNo;
                    //// 申請識別
                    //tajsinseiif.SinseiKanriNo = sumKanriNo + lineNo.ToString("00");
                    // サブシステムコード
                    tajsinseiif.SubsystemCd = tajsinseiif.概算_サブシステムコード;
                    // 業務コード
                    tajsinseiif.GyomuCd = tajsinseiif.概算_業務コード;
                    // 取引コード
                    tajsinseiif.TrhkCd = tajsinseiif.概算_取引コード;
                    // 処理コード
                    tajsinseiif.SyoriCd = tajsinseiif.概算_貯蔵品_処理コード;
                    // 運用会社コード
                    tajsinseiif.KisyCd = tajsinseiif.概算_運用会社コード;
                    // 作成担当コード
                    tajsinseiif.DatamkTntsyCd = 貯蔵品担当者;
                    // 船舶NO
                    if (msVessel.VesselNo == Tajsinseiif.全社共通船コード)
                    {
                        tajsinseiif.FuneNo = null;
                    }
                    else
                    {
                        tajsinseiif.FuneNo = msVessel.VesselNo;
                    }
                    // 申請担当者コード
                    tajsinseiif.TnsyCd = 貯蔵品担当者;
                    // 計上日付
                    tajsinseiif.KeijoYmd = GetGaisanDate(gaisan_yyyymm);
                    // 取引先コード
                    tajsinseiif.TrhkskCd = tajsinseiif.概算_取引先コード;
                    // 通貨コード
                    tajsinseiif.TukaCd = tajsinseiif.概算_通貨コード;
                    // 基本摘要
                    tajsinseiif.KihonTekiyo = "潤滑油 " + month.ToString("00") + "月末在庫振替";
                    // 承認パターンコード
                    tajsinseiif.SyoninPatternCd = tajsinseiif.概算_承認パターンコード;
                    // 行番号
                    tajsinseiif.LineNo = String.Format("{0:0000}", lineNo);
                    // 勘定科目
                    tajsinseiif.KanjoKmkCd = tajsinseiif.棚卸_勘定科目コード;
                    // 内訳科目
                    tajsinseiif.UtiwakeKmkCd = tajsinseiif.棚卸_内訳科目コード_潤滑油;
                    // 相手勘定科目
                    tajsinseiif.AiteKanjoKmkCd = tajsinseiif.棚卸_相手勘定科目コード_潤滑油;
                    // 相手内訳科目
                    tajsinseiif.AiteUtiwakeKmkCd = "";
                    // 消費税コード
                    tajsinseiif.SyhzCd = tajsinseiif.概算_消費税コード_対象外;
                    // 金額
                    tajsinseiif.Gaku = kingaku;
                    // 摘要
                    tajsinseiif.MeisaiTekiyo = tajsinseiif.KihonTekiyo;
                    // 作成日時
                    tajsinseiif.CreateDate = DateTime.Now;
                    // 状態
                    tajsinseiif.SyoriStatus = "00";
                    // 外部システム識別コード
                    tajsinseiif.ExtSystemCd = tajsinseiif.概算_更新プログラムID;
                    // 更新プログラムID
                    tajsinseiif.Updpgmid = tajsinseiif.概算_更新プログラムID;
                    // 更新ユーザーID
                    tajsinseiif.Upduserid = loginUser.LoginID;
                    // 更新日時
                    string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                    tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);

                    //bool flag = tajsinseiif.InsertRecord(dbConnect, loginUser);

                    #endregion

                    loList.Add(tajsinseiif);
                }

                #endregion

                #region 貯蔵品データ（船用品）をIFサーバへ

                sumKanriNo = "";
                lineNo = 0;

                // 貯蔵品データは船毎に１レコードとしてIFサーバへ
                foreach (MsVessel msVessel in msVesselList)
                {
                    // 合計金額を算出する
                    decimal kingaku = 0;
                    #region
                    var 船用品リスト = from p in 船用品リストALL
                                 where p.MS_VESSEL_ID == msVessel.MsVesselID
                                 select p;
                    foreach (var row in 船用品リスト)
                    {
                        if (row.支払単価 > 0)
                        {
                            kingaku += (row.支払数 - row.消費) * row.支払単価;
                        }
                        else
                        {
                            kingaku += (row.受領数 - row.消費) * row.受領単価;
                        }
                    }
                    #endregion

                    // インターフェイスサーバーにInsertする
                    #region
                    //// 集約管理番号の採番
                    //// 1つの集約管理番号につきレコードは15とする           
                    //if (lineNo == 0 || lineNo == maxLineNo)
                    //{
                    //    sumKanriNo = Tajsinseiif.GetSumKanriNo(dbConnect, loginUser, "T" + DateTime.Now.ToString("yyyyMMdd"));
                    //    lineNo = 0;
                    //}
                    //lineNo += 1;

                    Tajsinseiif tajsinseiif = new Tajsinseiif();

                    //// 集約管理番号
                    //tajsinseiif.SumKanriNo = sumKanriNo;
                    //// 申請識別
                    //tajsinseiif.SinseiKanriNo = sumKanriNo + lineNo.ToString("00");
                    // サブシステムコード
                    tajsinseiif.SubsystemCd = tajsinseiif.概算_サブシステムコード;
                    // 業務コード
                    tajsinseiif.GyomuCd = tajsinseiif.概算_業務コード;
                    // 取引コード
                    tajsinseiif.TrhkCd = tajsinseiif.概算_取引コード;
                    // 処理コード
                    tajsinseiif.SyoriCd = tajsinseiif.概算_貯蔵品_処理コード;
                    // 運用会社コード
                    tajsinseiif.KisyCd = tajsinseiif.概算_運用会社コード;
                    // 作成担当コード
                    tajsinseiif.DatamkTntsyCd = 貯蔵品担当者;
                    // 船舶NO
                    if (msVessel.VesselNo == Tajsinseiif.全社共通船コード)
                    {
                        tajsinseiif.FuneNo = null;
                    }
                    else
                    {
                        tajsinseiif.FuneNo = msVessel.VesselNo;
                    }
                    // 申請担当者コード
                    tajsinseiif.TnsyCd = 貯蔵品担当者;
                    // 計上日付
                    tajsinseiif.KeijoYmd = GetGaisanDate(gaisan_yyyymm);
                    // 取引先コード
                    tajsinseiif.TrhkskCd = tajsinseiif.概算_取引先コード;
                    // 通貨コード
                    tajsinseiif.TukaCd = tajsinseiif.概算_通貨コード;
                    // 基本摘要
                    tajsinseiif.KihonTekiyo = "船用品 " + month.ToString("00") + "月末在庫振替";
                    // 承認パターンコード
                    tajsinseiif.SyoninPatternCd = tajsinseiif.概算_承認パターンコード;
                    // 行番号
                    tajsinseiif.LineNo = String.Format("{0:0000}", lineNo);
                    // 勘定科目
                    tajsinseiif.KanjoKmkCd = tajsinseiif.棚卸_勘定科目コード;
                    // 内訳科目
                    tajsinseiif.UtiwakeKmkCd = tajsinseiif.棚卸_内訳科目コード_船用品;
                    // 相手勘定科目
                    tajsinseiif.AiteKanjoKmkCd = tajsinseiif.棚卸_相手勘定科目コード_船用品;
                    // 相手内訳科目
                    tajsinseiif.AiteUtiwakeKmkCd = "";
                    // 消費税コード
                    tajsinseiif.SyhzCd = tajsinseiif.概算_消費税コード_対象外;
                    // 金額
                    tajsinseiif.Gaku = kingaku;
                    // 摘要
                    tajsinseiif.MeisaiTekiyo = tajsinseiif.KihonTekiyo;
                    // 作成日時
                    tajsinseiif.CreateDate = DateTime.Now;
                    // 状態
                    tajsinseiif.SyoriStatus = "00";
                    // 外部システム識別コード
                    tajsinseiif.ExtSystemCd = tajsinseiif.概算_更新プログラムID;
                    // 更新プログラムID
                    tajsinseiif.Updpgmid = tajsinseiif.概算_更新プログラムID;
                    // 更新ユーザーID
                    tajsinseiif.Upduserid = loginUser.LoginID;
                    // 更新日時
                    string nowTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                    tajsinseiif.UpddateYmdhms = Int64.Parse(nowTime);

                    //bool flag = tajsinseiif.InsertRecord(dbConnect, loginUser);

                    #endregion

                    itemList.Add(tajsinseiif);
                }

                #endregion

                //ret = true;
                //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
                //{
                //    NBaseCommon.LogFile.Write(loginUser.FullName, "基幹システム連携書き込み処理_概算連携 Call");
                //    NBaseCommon.LogFile.Write(loginUser.FullName, "概算 = " + gaisanList.Count().ToString());
                //    NBaseCommon.LogFile.Write(loginUser.FullName, "潤滑油 = " + loList.Count().ToString());
                //    NBaseCommon.LogFile.Write(loginUser.FullName, "船用品 = " + itemList.Count().ToString());

                //    ret = serviceClient.BLC_基幹システム連携書き込み処理_概算連携(loginUser, gaisanList, loList, itemList);

                //    NBaseCommon.LogFile.Write(loginUser.FullName, "基幹システム連携書き込み処理_概算連携 Call ret = " + ret.ToString());
                //}

            }
            catch (Exception e)
            {
                NBaseCommon.LogFile.Write(loginUser.FullName, "Exception = " + e.Message);
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// yyyymmをDateTime型に変換する
        /// ddは月最終日
        /// </summary>
        /// <param name="gaisanZuki"></param>
        /// <returns></returns>
        private DateTime GetGaisanDate(string gaisanZuki)
        {
            int yyyy = int.Parse(gaisanZuki.Substring(0, 4));
            int MM = int.Parse(gaisanZuki.Substring(4, 2));

            DateTime gaisanDate = new DateTime(yyyy, MM, 1);
            gaisanDate = gaisanDate.AddMonths(1);
            gaisanDate = gaisanDate.AddDays(-1);

            return gaisanDate;
        }
    }
}
