using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NBaseData.DAC;
using ORMapping;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DS;
using NBaseData.BLC;
using System.IO;
using NBaseUtil;
using NBaseCommon.Senin.Excel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        int BLC_船員登録(MsUser loginUser, MsSenin senin,
                            MsSeninAddress seninAddress, 
                            List<SiCard> cards, 
                            List<SiMenjou> menjous,
                            List<SiKazoku> kazokus, List<SiRireki> rirekis, List<SiShobyo> shobyos, List<SiKenshin> kenshins, List<SiShobatsu> shobatsus);


        [OperationContract]
        bool BLC_新規送金(MsUser loginUser, SiSoukin soukin);


        [OperationContract]
        bool BLC_配乗表配信(MsUser loginUser, SiHaijou haijou);


        [OperationContract]
        byte[] BLC_Excel_船用金送金表出力(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        byte[] BLC_Excel_船内収支報告書出力(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        byte[] BLC_Excel_科目別集計表出力(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        byte[] BLC_Excel_送金通知出力(MsUser loginUser, string siSoukinId);


        [OperationContract]
        bool BLC_月次計上確定(MsUser loginUser, string shimeNengetsu);


        [OperationContract]
        bool BLC_次年度休暇確定(MsUser loginUser, string shimeNen);


        [OperationContract]
        byte[] BLC_Excel_休日付与簿出力(MsUser loginUser, DateTime date);


        [OperationContract]
        byte[] BLC_Excel_休暇消化状況出力(MsUser loginUser, DateTime date);


        [OperationContract]
        byte[] BLC_Excel_乗下船記録書出力(MsUser loginUser, DateTime date);


        [OperationContract]
        byte[] BLC_Excel_乗下船カード出力(MsUser loginUser, DateTime date);


        [OperationContract]
        byte[] BLC_Excel_個人情報一覧出力(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        byte[] BLC_Excel_クルーリスト出力(MsUser loginUser, DateTime date, int msVesselId);


        [OperationContract]
        byte[] BLC_Excel_配乗表出力(MsUser loginUser, SiHaijou haijou);


        [OperationContract]
        bool BLC_免状免許_アラーム削除(MsUser loginUser, SiMenjou menjou);


        [OperationContract]
        List<MsSenin> BLC_船員検索(MsUser loginUser, MsSeninFilter filter);


        [OperationContract]
        List<SiCard> BLC_船員カード検索(MsUser loginUser, SiCardFilter filter);

        [OperationContract]
        List<SiCard> BLC_交代者検索(MsUser loginUser, SiCardFilter filter);

        [OperationContract]
        byte[] BLC_Excel_船員カード出力(MsUser loginUser, int msSeninId);


        [OperationContract]
        SiHaijou BLC_配乗表作成(MsUser loginUser, SiCardFilter filter);


        [OperationContract]
        List<SiSimulationDetail> BLC_GetCrewMatrix(MsUser loginUser, int msSeninId);


        [OperationContract]
        byte[] BLC_Excel_傷病一覧出力(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId);

        [OperationContract]
        byte[] BLC_Excel_健康診断一覧出力(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId);

        [OperationContract]
        byte[] BLC_Excel_職別海技免許等資格一覧出力(MsUser loginUser, int msSiShokumeiId, int msSeninId);


        //[OperationContract]
        //byte[] BLC_CSV_給与連携出力(MsUser loginUser, DateTime fromDate, DateTime toDate);

        [OperationContract]
        List<MsSeninPlus> BLC_船員検索Advanced(MsUser loginUser, SiAdvancedSearchFilter filter, List<SiAdvancedSearchConditionItem> conditionItems, List<SiAdvancedSearchConditionValue> conditionValues);

        [OperationContract]
        bool BLC_船員検索条件登録(MsUser loginUser, SiAdvancedSearchConditionHead conditionHead);

        [OperationContract]
        List<SiAdvancedSearchConditionHead> BLC_Get船員検索条件(MsUser loginUser);

        [OperationContract]
        byte[] BLC_Excel_給与手当一覧表出力(MsUser loginUser, DateTime date, int msVesselId);

        [OperationContract]
        byte[] BLC_Excel_船員交代予定表出力(MsUser loginUser);



        [OperationContract]
        int BLC_船員基本給登録(MsUser loginUser, MsSenin senin, SiSalary salary);
        [OperationContract]
        int BLC_船員既往歴登録(MsUser loginUser, MsSenin senin, SiKenshinPmhKa kenshinPmhKa);
        [OperationContract]
        int BLC_船員特記事項登録(MsUser loginUser, MsSenin senin, SiRemarks remarks);


        [OperationContract]
        List<SiCard> BLC_船員カード検索2(MsUser loginUser, SiCardFilter filter);

        [OperationContract]
        bool BLC_船員カード登録(MsUser loginUser, SiCard card);
    }

    public partial class Service
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="senin"></param>
        /// <param name="cards"></param>
        /// <returns>登録された MS_SENIN_ID.</returns>
        public int BLC_船員登録(MsUser loginUser,
                                MsSenin senin, MsSeninAddress seninAddress,
                                List<SiCard> cards, List<SiMenjou> menjous,
                                List<SiKazoku> kazokus, List<SiRireki> rirekis, List<SiShobyo> shobyos, List<SiKenshin> kenshins, List<SiShobatsu> shobatsus)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_船員登録(loginUser, seninTableCache, senin, seninAddress, cards, menjous, kazokus, rirekis, shobyos, kenshins, shobatsus);
        }


        public bool BLC_新規送金(MsUser loginUser, SiSoukin soukin)
        {
            return 船員.BLC_新規送金(loginUser, soukin);
        }


        public bool BLC_配乗表配信(MsUser loginUser, SiHaijou haijou)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_配乗表配信(loginUser, haijou, seninTableCache);
        }


        public byte[] BLC_Excel_船用金送金表出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "船用金送金表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 船用品送金表出力(templateFilePath, outputFilePath).CreateFile(loginUser, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_船内収支報告書出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "船内収支報告書";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 船内収支報告書出力(templateFilePath, outputFilePath).CreateFile(loginUser, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_科目別集計表出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "科目別集計表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 科目別集計表出力(templateFilePath, outputFilePath).CreateFile(loginUser, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_送金通知出力(MsUser loginUser, string siSoukinId)
        {
            string baseFileName = "送金通知";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 送金通知出力(templateFilePath, outputFilePath).CreateFile(loginUser, siSoukinId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public bool BLC_月次計上確定(MsUser loginUser, string shimeNengetsu)
        {
            //return 船員.BLC_月次計上確定(loginUser, shimeNengetsu);

            bool ret = true;

            List<Tajsinseiif> gaisanList = 船員.Get_月次計上Records(loginUser, shimeNengetsu);


#if DEBUG
            NBaseCommon.LogFile.Write(loginUser.FullName, "月次計上確定");

            string msg = "";
            msg += "集約管理番号";
            msg += ",";
            msg += "申請識別";
            msg += ",";
            msg += "船舶NO";
            msg += ",";
            msg += "基本摘要";
            msg += ",";
            msg += "行番号";
            msg += ",";
            msg += "勘定科目";
            msg += ",";
            msg += "内訳科目";
            msg += ",";
            msg += "相手勘定科目";
            msg += ",";
            msg += "相手内訳科目";
            msg += ",";
            msg += "消費税コード";
            msg += ","; 
            msg += "金額";
            msg += ",";
            msg += "消費税額";
            msg += ",";
            msg += "摘要";

            NBaseCommon.LogFile.Write(" ", msg);

            foreach (Tajsinseiif info in gaisanList)
            {
                msg = "";

                msg += info.SumKanriNo;
                msg += ",";
                msg += info.SinseiKanriNo;
                msg += ",";
                msg += info.FuneNo;
                msg += ",";
                msg += info.KihonTekiyo;
                msg += ",";
                msg += info.LineNo;
                msg += ",";
                msg += info.KanjoKmkCd;
                msg += ",";
                msg += info.UtiwakeKmkCd;
                msg += ",";
                msg += info.AiteKanjoKmkCd;
                msg += ",";
                msg += info.AiteUtiwakeKmkCd;
                msg += ",";
                msg += info.SyhzCd;
                msg += ","; 
                msg += info.Gaku.ToString();
                msg += ",";
                msg += info.SyohizeiGaku.ToString();
                msg += ",";
                msg += info.MeisaiTekiyo;

                NBaseCommon.LogFile.Write(" ", msg);
            }
#else
            //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //{
            //    ret = serviceClient.BLC_基幹システム連携書き込み処理_船員月次確定(loginUser, gaisanList);
            //}
            //if (ret)
            //{
            //    ret = 船員.BLC_月次計上確定(loginUser, shimeNengetsu);
            //}
#endif
            return ret;
        }

        public bool BLC_次年度休暇確定(MsUser loginUser, string shimeNen)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_次年度休暇確定(loginUser, shimeNen, seninTableCache);
        }


        public byte[] BLC_Excel_休日付与簿出力(MsUser loginUser, DateTime date)
        {
            string baseFileName = "休日付与簿";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 休日付与簿出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_休暇消化状況出力(MsUser loginUser, DateTime date)
        {
            string baseFileName = "休暇消化状況";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 休暇消化状況出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_乗下船記録書出力(MsUser loginUser, DateTime date)
        {
            string baseFileName = "乗下船記録書";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 乗下船記録書出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_乗下船カード出力(MsUser loginUser, DateTime date)
        {
            string baseFileName = "乗下船カード";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 乗下船カード出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_個人情報一覧出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "個人情報一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 個人情報一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_クルーリスト出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "クルーリスト";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new クルーリスト出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_配乗表出力(MsUser loginUser, SiHaijou haijou)
        {
            string baseFileName = "配乗表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 配乗表出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, haijou);

            return FileUtils.ToBytes(outputFilePath);
        }


        public bool BLC_免状免許_アラーム削除(MsUser loginUser, SiMenjou menjou)
        {
            return 船員.BLC_免状免許_アラーム削除(loginUser, menjou);
        }


        public List<MsSenin> BLC_船員検索(MsUser loginUser, MsSeninFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            if (filter.IncludeKyuka)
            {
                return 船員.BLC_船員休暇検索(loginUser, seninTableCache, filter);
            }
            else
            {
                return 船員.BLC_船員検索(loginUser, seninTableCache, filter);
            }
        }


        public List<SiCard> BLC_船員カード検索(MsUser loginUser, SiCardFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_船員カード検索(loginUser, seninTableCache, filter);
        }
        public List<SiCard> BLC_交代者検索(MsUser loginUser, SiCardFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_交代者検索(loginUser, seninTableCache, filter);
        }


        public byte[] BLC_Excel_船員カード出力(MsUser loginUser, int msSeninId)
        {
            string baseFileName = "船員カード";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 船員カード出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, msSeninId);

            return FileUtils.ToBytes(outputFilePath);
        }

        public SiHaijou BLC_配乗表作成(MsUser loginUser, SiCardFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            filter.includeSchedule = true;
            return 船員.BLC_配乗表作成(loginUser, seninTableCache, filter);
        }

        public List<SiSimulationDetail> BLC_GetCrewMatrix(MsUser loginUser, int msSeninId)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            return 船員.BLC_GetCrewMatrix(loginUser, seninTableCache, msSeninId);
        }

        public byte[] BLC_Excel_傷病一覧出力(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId)
        {
            string baseFileName = "傷病一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 傷病一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, fromDate, toDate, msSiShokumeiId, msSeninId);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_健康診断一覧出力(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShokumeiId, int msSeninId)
        {
            string baseFileName = "健康診断一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 健康診断一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, fromDate, toDate, msSiShokumeiId, msSeninId);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_職別海技免許等資格一覧出力(MsUser loginUser, int msSiShokumeiId, int msSeninId)
        {
            string baseFileName = "船員免状等職別資格一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 職別海技免許等資格一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, msSiShokumeiId, msSeninId);

            return FileUtils.ToBytes(outputFilePath);
        }

        //public byte[] BLC_CSV_給与連携出力(MsUser loginUser, DateTime fromDate, DateTime toDate)
        //{
        //    string baseFileName = "給与連携";
        //    string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
        //    string basePath = path + baseFileName + "_" + loginUser.FullName;
        //    string outputFilePath = basePath + "\\給与連携.zip";

        //    SeninTableCache seninTableCache = SeninTableCache.instance(false);
        //    seninTableCache.DacProxy = new DirectSeninDacProxy();

        //    new 給与連携出力(basePath, outputFilePath).CreateFile(loginUser, seninTableCache, fromDate, toDate);

        //    return FileUtils.ToBytes(outputFilePath);
        //}



        public List<MsSeninPlus> BLC_船員検索Advanced(MsUser loginUser, SiAdvancedSearchFilter filter, List<SiAdvancedSearchConditionItem> conditionItems, List<SiAdvancedSearchConditionValue> conditionValues)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員検索.AdvancedSearch(loginUser, seninTableCache, filter, conditionItems, conditionValues);
        }

        public bool BLC_船員検索条件登録(MsUser loginUser, SiAdvancedSearchConditionHead conditionHead)
        {
            return 船員検索.SaveCondition(loginUser, conditionHead);
        }

        public List<SiAdvancedSearchConditionHead> BLC_Get船員検索条件(MsUser loginUser)
        {
            return 船員検索.GetConditions(loginUser);
        }


        public byte[] BLC_Excel_給与手当一覧表出力(MsUser loginUser, DateTime date, int msVesselId)
        {
            string baseFileName = "給与手当一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 給与手当一覧表出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, date, msVesselId);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_船員交代予定表出力(MsUser loginUser)
        {
            string baseFileName = "船員交代予定表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 船員交代予定表出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache);

            return FileUtils.ToBytes(outputFilePath);
        }




        public int BLC_船員基本給登録(MsUser loginUser, MsSenin senin, SiSalary salary)
        {
            return 船員.BLC_船員基本給登録(loginUser, senin, salary);
        }

        public int BLC_船員既往歴登録(MsUser loginUser, MsSenin senin, SiKenshinPmhKa kenshinPmhKa)
        {
            return 船員.BLC_船員既往歴登録(loginUser, senin, kenshinPmhKa);
        }

        public int BLC_船員特記事項登録(MsUser loginUser, MsSenin senin, SiRemarks remarks)
        {
            return 船員.BLC_船員特記事項登録(loginUser, senin, remarks);
        }

        public List<SiCard> BLC_船員カード検索2(MsUser loginUser, SiCardFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            return 船員.BLC_船員カード検索2(loginUser, seninTableCache, filter);
        }

        public bool BLC_船員カード登録(MsUser loginUser, SiCard card)
        {
            return 船員.BLC_船員カード登録(loginUser, card);
        }
    }
}
