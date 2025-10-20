using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using NBaseData.DS;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System.Drawing;
using NBaseUtil;
using System.Globalization;
using NBaseData.BLC;

namespace NBaseCommon.Senin.Excel
{
    public class 船員カード出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 船員カード出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, int msSeninId)
        {
            MsSenin senin = MsSenin.GetRecord(loginUser, msSeninId);


            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------
                //2013/12/18 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);
                //-----------------------
                //2013/12/18 変更:OpenBookエラーをなげる m.y
                int xlsRet = xls.OpenBook(outputFilePath, templateFilePath);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    if (xls.ErrorNo == ExcelCreator.ErrorNo.TempCreate)
                    {
                        xlsEx = new Exception("ディスクに十分な空き領域がありません。");
                    }
                    else
                    {
                        xlsEx = new Exception("サーバでエラーが発生しました。");
                    }
                    throw xlsEx;
                }
                //-----------------------

                _CreateFile(loginUser, xls, seninTableCache, senin);

                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, MsSenin senin)
        {
            List<MsVesselType> vesselTypes = MsVesselType.GetRecords(loginUser);

            MsSeninAddress address = MsSeninAddress.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);
            if (address != null)
                address.MakeFullAddress(seninTableCache.GetMsSiOptionsList(loginUser));
            MsSeninCareer career = MsSeninCareer.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);
            MsSeninEtc etc = MsSeninEtc.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);
            List<SiMenjou> menjous = SiMenjou.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);
            List<SiKazoku> kazokus = SiKazoku.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);
            List<SiKenshin> kenshins = SiKenshin.GetRecordsByMsSeninID(loginUser, senin.MsSeninID);

            SiCardFilter filter = new SiCardFilter();
            filter.MsSeninID = senin.MsSeninID;
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);


            // 自社名
            xls.Cell("**自社名").Value = NBaseCommon.Common.船員_自社名;



            // 従業員番号
            xls.Cell("**従業員番号").Value = senin.ShimeiCode.Trim();

            // 氏名
            xls.Cell("**船員_氏名").Value = senin.Sei + " " + senin.Mei;
            xls.Cell("**船員_氏名カナ").Value = senin.SeiKana + " " + senin.MeiKana;

            // 生年月日
            xls.Cell("**船員_生年月日").Value = senin.Birthday;


            // 現職
            xls.Cell("**船員_職名").Value = seninTableCache.GetMsSiShokumeiName(loginUser, senin.MsSiShokumeiID);

            // 血液型
            xls.Cell("**船員_血液型").Value = seninTableCache.GetMsSiOptionsName(loginUser, senin.BloodType);

            // 入社年月日
            xls.Cell("**船員_入社").Value = DateTimeUtils.ToString(senin.NyuushaDate);

            // 退社年月日
            xls.Cell("**船員_退社").Value = DateTimeUtils.ToString(senin.RetireDate);


            // 電話
            xls.Cell("**船員_電話番号").Value = senin.Tel.Replace(" ", "");

            // 携帯
            xls.Cell("**船員_携帯番号").Value = senin.Keitai.Replace(" ", "");

            // メール/Fax
            xls.Cell("**船員_メールFAX").Value = StringUtils.Empty(senin.Mail) ? senin.Fax.Replace(" ", "") : senin.Mail;


            if (address != null)
            {
                // 郵便番号
                xls.Cell("**船員_現住所_郵便番号").Value = "〒" + address.ZipCode;

                // 現住所
                xls.Cell("**船員_現住所").Value = address.PrefecturesStr;

                // 本籍
                xls.Cell("**船員_本籍都道府県").Value = seninTableCache.GetMsSiOptionsName(loginUser, address.P_Prefectures);
            }


            if (kazokus != null && kazokus.Count > 0)
            {
                // 緊急連絡先
                var emg = kazokus.Where(o => o.EmergencyKind == 船員.船員_連絡先_一次).OrderBy(o => o.ShowOrder).FirstOrDefault();
                if (emg != null)
                    xls.Cell("**船員_緊急連絡先").Value = emg.Tel;
            }

            if (career != null)
            {
                // 最終学歴
                xls.Cell("**船員_最終学歴").Value = career.AcademicBackground;

                // 卒業年月
                xls.Cell("**船員_卒業年月").Value = career.YearOfGraduation;

                // 他社歴
                xls.Cell("**船員_他社歴1").Value = career.Company1;
                xls.Cell("**船員_他社入社1").Value = career.Joined1;
                xls.Cell("**船員_他社退社1").Value = career.Leave1;

                xls.Cell("**船員_他社歴2").Value = career.Company2;
                xls.Cell("**船員_他社入社2").Value = career.Joined2;
                xls.Cell("**船員_他社退社2").Value = career.Leave2;

                xls.Cell("**船員_他社歴3").Value = career.Company3;
                xls.Cell("**船員_他社入社3").Value = career.Joined3;
                xls.Cell("**船員_他社退社3").Value = career.Leave3;

                xls.Cell("**船員_他社歴4").Value = career.Company4;
                xls.Cell("**船員_他社入社4").Value = career.Joined4;
                xls.Cell("**船員_他社退社4").Value = career.Leave4;

                xls.Cell("**船員_他社歴5").Value = career.Company5;
                xls.Cell("**船員_他社入社5").Value = career.Joined5;
                xls.Cell("**船員_他社退社5").Value = career.Leave5;
            }


            if (etc != null)
            {
                // 作業服
                xls.Cell("**船員_身長").Value = etc.Height;
                xls.Cell("**船員_体重").Value = etc.Weight;
                xls.Cell("**船員_ウエスト").Value = etc.Waist;
                xls.Cell("**船員_股下").Value = etc.Inseam;
                xls.Cell("**船員_安全靴").Value = etc.ShoeSize;
                xls.Cell("**船員_つなぎ").Value = etc.Workwear;

                // 口座
                xls.Cell("**船員_銀行名1").Value = etc.BankName1;
                xls.Cell("**船員_支店名1").Value = etc.BranchName1;
                xls.Cell("**船員_口座1").Value = etc.AccountNo1;

                xls.Cell("**船員_銀行名2").Value = etc.BankName2;
                xls.Cell("**船員_支店名2").Value = etc.BranchName2;
                xls.Cell("**船員_口座2").Value = etc.AccountNo2;
            }

            // 備考
            xls.Cell("**船員_備考").Value = senin.Sonota;



            int index = 0;

            #region 免許・免状(最大１２行）

            index = 0;

            var mlist = menjous.OrderBy(o => o.MsSiMenjouID).ThenBy(o => o.MsSiMenjouKindID);
            foreach (SiMenjou m in mlist)
            {
                if (m.DeleteFlag == 1)
                    continue;

                if (m.ChouhyouFlag == 0)
                    continue;

                index++;

                xls.Cell("**船員_免許免状" + index.ToString()).Value = m.MsSiMenjouKindID > 0 ? seninTableCache.GetMsSiMenjouKindName(loginUser, m.MsSiMenjouKindID) : seninTableCache.GetMsSiMenjouName(loginUser, m.MsSiMenjouID);
                xls.Cell("**船員_免許免状_年月" + index.ToString()).Value = DateTimeUtils.ToString(m.ShutokuDate);
                xls.Cell("**船員_免許免状_交付年月" + index.ToString()).Value = DateTimeUtils.ToString(m.ShutokuDate);
                xls.Cell("**船員_免許免状_番号" + index.ToString()).Value = m.No;
                xls.Cell("**船員_免許免状_有効期限" + index.ToString()).Value = DateTimeUtils.ToString(m.Kigen);



                if (m.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser))
                {
                    if (kenshins.Any(o => o.KensaName == "船員手帳健康診断"))
                    {
                        var k = kenshins.Where(o => o.KensaName == "船員手帳健康診断").OrderByDescending(o => o.ExpiryDate).First();

                        xls.Cell("**船員_免許免状_備考" + index.ToString()).Value = "  健康診断  " + DateTimeUtils.ToString(k.ExpiryDate);
                    }
                }
                else
                {
                    xls.Cell("**船員_免許免状_備考" + index.ToString()).Value = m.Bikou;
                }

                if (index == 12)
                    break;

            }

            #endregion

            #region 家族(最大６行）

            index = 0;
            var klist = kazokus.OrderBy(obj => obj.ShowOrder).ThenBy(obj => obj.SiKazokuID);
            foreach (SiKazoku k in klist)
            {
                if (k.DeleteFlag == 1)
                    continue;

                index++;

                k.MakeFullAddress(seninTableCache.GetMsSiOptionsList(loginUser));

                xls.Cell("**家族" + index.ToString()).Value = k.FullName;
                xls.Cell("**家族_カナ" + index.ToString()).Value = k.FullNameKana;
                xls.Cell("**家族_生年月日" + index.ToString()).Value = DateTimeUtils.ToString(k.Birthday);
                xls.Cell("**家族_続柄" + index.ToString()).Value = seninTableCache.GetMsSiOptionsName(loginUser, k.Tuzukigara);
                xls.Cell("**家族_扶養" + index.ToString()).Value = k.SeamensInsuranceDependent == 1 ? "有" : "";
                xls.Cell("**家族_住所" + index.ToString()).Value = k.PrefecturesStr;

                if (index == 6)
                    break;
            }

            #endregion

            #region 乗下船情報(最大３５行）

            index = 0;
            var clist = cards.Where(o => o.DeleteFlag == 0).OrderBy(o => o.StartDate).ThenBy(o => o.EndDate).ToList();
            if (clist.Count() > 35)
            {
                clist = clist.GetRange((clist.Count() - 35), 35);
            }

            foreach (var c in clist)
            {
                index++;

                int days = 0;
                if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                {
                    days = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
                }
                else
                {
                    days = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
                }

                xls.Cell("**乗下船履歴_船名" + index.ToString()).Value = c.VesselName;
                xls.Cell("**乗下船履歴_船種" + index.ToString()).Value = vesselTypes.Any(o => o.MsVesselTypeID == c.MsVesselTypeID) ? vesselTypes.Where(o => o.MsVesselTypeID == c.MsVesselTypeID).First().VesselTypeName : "";
                xls.Cell("**乗下船履歴_総トン数" + index.ToString()).Value = c.GrossTon;
                xls.Cell("**乗下船履歴_職名" + index.ToString()).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, c.SiLinkShokumeiCards[0].MsSiShokumeiID);
                xls.Cell("**乗下船履歴_航行区域" + index.ToString()).Value =  c.NavigationArea;
                xls.Cell("**乗下船履歴_乗船日" + index.ToString()).Value = c.StartDate;
                xls.Cell("**乗下船履歴_下船日" + index.ToString()).Value = c.EndDate;
                xls.Cell("**乗下船履歴_日数" + index.ToString()).Value = days;
                xls.Cell("**乗下船履歴_船主" + index.ToString()).Value = c.OwnerName;
                xls.Cell("**乗下船履歴_下船理由" + index.ToString()).Value = SiCard.ConvertSignOffReasonStrings(c.SignOffReason);
            }
            #endregion


        }
    }
}
