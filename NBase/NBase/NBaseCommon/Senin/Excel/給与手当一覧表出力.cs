using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace NBaseCommon.Senin.Excel
{
    public class 給与手当一覧表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 給与手当一覧表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }
        
        
        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msVesselId)
        {
            Dictionary<int, List<RowData>> rowDataDic = MakeRowData(loginUser, seninTableCache, date, msVesselId);


            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
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

                int startRow = 2;
                int rowNo = startRow;

                foreach (MsSiShokumei shokumei in seninTableCache.GetMsSiShokumeiList(loginUser))
                {
                    if (rowDataDic.ContainsKey(shokumei.MsSiShokumeiID) == false)
                        continue;

                    List<RowData> rowDataByShokumei = rowDataDic[shokumei.MsSiShokumeiID];

                    var datas = rowDataByShokumei.Where(obj => obj.code != 0).OrderBy(obj => obj.code);
                    if (datas.Count() > 0)
                        _CreateFile(xls, loginUser, seninTableCache, datas.ToList(), ref rowNo);

                    datas = rowDataByShokumei.Where(obj => obj.code == 0).OrderBy(obj => obj.shimeiCode);
                    if (datas.Count() > 0)
                        _CreateFile(xls, loginUser, seninTableCache, datas.ToList(), ref rowNo);
                }

                xls.RowDelete(1, 1);

                xls.CloseBook(true);
            }
        }

        public void _CreateFile(ExcelCreator.Xlsx.XlsxCreator xls, MsUser loginUser, SeninTableCache seninTableCache, List<RowData> rowDatas, ref int rowNo)
        {
            foreach(RowData data in rowDatas)
            {
                xls.RowCopy(1, rowNo);

                int colNo = 0;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, data.shokumeiId);
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.shimeiCode;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.name;
                colNo++;

                if (data.teateList.Count() > 0)
                {
                    var vesselIds = data.teateList.Select(obj => obj.MsVesselID).Distinct();

                    foreach(int vesselId in vesselIds)
                    {
                        MsVessel vessel = seninTableCache.GetMsVessel(loginUser, vesselId);

                        //xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = vessel.KyuyoRenkeiNo;
                        colNo++;

                        xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = vessel.VesselName;
                        colNo++;

                        var teateByVessel = data.teateList.Where(obj => obj.MsVesselID == vesselId);
                        foreach(SiKyuyoTeate teate in teateByVessel)
                        {
                            xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = seninTableCache.GetMsSiKyuyoTeateName(loginUser, teate.MsSiKyuyoTeateID);
                            colNo++;

                            string kikan = "";
                            if (teate.StartDate != DateTime.MinValue)
                                kikan = teate.StartDate.ToShortDateString();

                            if (teate.StartDate != DateTime.MinValue || teate.EndDate != DateTime.MinValue)
                                kikan += "～";

                            if (teate.EndDate != DateTime.MinValue)
                                kikan += teate.EndDate.ToShortDateString();
                            xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = kikan;
                            colNo++;

                            xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = teate.Days;
                            colNo++;

                            xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = teate.Tanka;
                            colNo++;

                            xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = teate.Kingaku;
                            colNo++;
                        }

                        if (teateByVessel.Count() <= 5)
                        {
                            colNo = 4 + (5 * 5) + 1 + 1; // 職名 + 氏名ｺｰﾄﾞ + 氏名 + 船No + 船名 + (手当 × 5) + 計 の次
                        }
                    }
                }

                rowNo++;
            }
        }

        public Dictionary<int, List<RowData>> MakeRowData(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msVesselId)
        {
            Dictionary<int, List<RowData>> ret = new Dictionary<int, List<RowData>>();

            List<SiKyuyoTeate> kyuyoTeateList = SiKyuyoTeate.GetRecordsByYearMonth(loginUser, date.ToString("yyyyMM"));

            if (msVesselId > 0)
            {
                // 船が指定されている場合は、該当船のもののみ対象
                SiCardFilter filter = new SiCardFilter();
                filter.MsVesselIDs.Add(msVesselId);
                filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
                filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
                filter.Start = date;
                filter.End = date.AddMonths(1);
                filter.OrderByStr = "OrderByMsSiShokumeiId";
                List<SiCard> cardList = SiCard.GetRecordsByFilter(loginUser, filter);

                foreach(SiCard card in cardList)
                {
                    MsSenin senin = MsSenin.GetRecord(loginUser, card.MsSeninID);

                    RowData data = new RowData();

                    data.shokumeiId = senin.MsSiShokumeiID;
                    data.seninId = senin.MsSeninID;
                    data.shimeiCode = senin.ShimeiCode.Trim();
                    int intShimeiCode = 0;
                    int.TryParse(senin.ShimeiCode, out intShimeiCode);
                    data.code = intShimeiCode;
                    data.name = senin.Sei + " " + senin.Mei;

                    data.teateList = new List<SiKyuyoTeate>();
                    if (kyuyoTeateList.Any(obj => obj.MsSeninID == senin.MsSeninID && obj.MsVesselID == msVesselId && obj.CancelFlag == 0))
                    {
                        data.teateList = kyuyoTeateList.Where(obj => obj.MsSeninID == senin.MsSeninID && obj.MsVesselID == msVesselId && obj.CancelFlag == 0).ToList();

                        foreach(SiKyuyoTeate teate in data.teateList)
                        {
                            teate.Kingaku = teate.HonsenKingaku;
                        }
                    }

                    if (ret.ContainsKey(senin.MsSiShokumeiID) == false)
                    {
                        List<RowData> rowData = new List<RowData>();
                        rowData.Add(data);
                        ret.Add(senin.MsSiShokumeiID, rowData);
                    }
                    else
                    {
                        ret[senin.MsSiShokumeiID].Add(data);
                    }
                }
            }
            else
            {
                MsSeninFilter filter = new MsSeninFilter();
                filter.Kubuns.Add(0); // 社員のみ
                filter.職別海技免許等資格一覧対象 = true;
                filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;
                List<MsSenin> seninList = MsSenin.GetRecordsByFilter(loginUser, filter);

                foreach (MsSenin senin in seninList)
                {
                    RowData data = new RowData();

                    data.shokumeiId = senin.MsSiShokumeiID;
                    data.seninId = senin.MsSeninID;
                    data.shimeiCode = senin.ShimeiCode.Trim();
                    int intShimeiCode = 0;
                    int.TryParse(senin.ShimeiCode, out intShimeiCode);
                    data.code = intShimeiCode;
                    data.name = senin.Sei + " " + senin.Mei;

                    data.teateList = new List<SiKyuyoTeate>();
                    if (kyuyoTeateList.Any(obj => obj.MsSeninID == senin.MsSeninID && obj.CancelFlag == 0))
                    {
                        data.teateList = kyuyoTeateList.Where(obj => obj.MsSeninID == senin.MsSeninID && obj.CancelFlag == 0).ToList();
                    }

                    if (ret.ContainsKey(senin.MsSiShokumeiID) == false)
                    {
                        List<RowData> rowData = new List<RowData>();
                        rowData.Add(data);
                        ret.Add(senin.MsSiShokumeiID, rowData);
                    }
                    else
                    {
                        ret[senin.MsSiShokumeiID].Add(data);
                    }
                }
            }

            return ret;
        }

        public class RowData
        {
            public int shokumeiId;
            public int seninId;
            public int code;
            public string shimeiCode;
            public string name;
            public List<SiKyuyoTeate> teateList;
        }
    }
}
