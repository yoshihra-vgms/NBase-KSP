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
    public class 船員交代予定表出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 船員交代予定表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }
        
        
        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache)
        {

            Dictionary<int, List<RowData>> rowDataDic = MakeRowData(loginUser, seninTableCache);


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

                int startRow = 5;
                int rowPerVessel = 4;
                int vesselCount = 0;

                foreach (MsVessel vessel in seninTableCache.GetMsVesselList(loginUser))
                {
                    if (rowDataDic.ContainsKey(vessel.MsVesselID) == false)
                        continue;

                    List<RowData> rowDataByVessel = rowDataDic[vessel.MsVesselID];

                    _CreateFile(xls, loginUser, seninTableCache, rowDataByVessel, (vesselCount * rowPerVessel) + startRow);
                    vesselCount++;
                }

                xls.CloseBook(true);
            }
        }

        public void _CreateFile(ExcelCreator.Xlsx.XlsxCreator xls, MsUser loginUser, SeninTableCache seninTableCache, List<RowData> rowDatas, int rowNo)
        {
            xls.Cell(ExcelUtils.ToCellName(0, rowNo)).Value = rowDatas[0].vesselName;

            foreach (RowData data in rowDatas)
            {
                int colNo = 1;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.off_shokumei;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.off_name;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.off_date.ToShortDateString();
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.off_reason;
                colNo++;



                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.on_shokumei;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.on_name;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.on_date.ToShortDateString();
                colNo++;



                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.basho;
                colNo++;

                xls.Cell(ExcelUtils.ToCellName(colNo, rowNo)).Value = data.bikou;
                colNo++;

                rowNo++;
            }
        }

        public Dictionary<int, List<RowData>> MakeRowData(MsUser loginUser, SeninTableCache seninTableCache)
        {
            Dictionary<int, List<RowData>> ret = new Dictionary<int, List<RowData>>();


            List<SiBoardingSchedule> boardingScheduleList = SiBoardingSchedule.GetRecordsByPlan(null, loginUser);
            foreach(SiBoardingSchedule schedule in boardingScheduleList)
            {
                SiCard offCard = SiCard.GetRecord(loginUser, schedule.SignOffSiCardID);
                MsSenin offSenin = MsSenin.GetRecord(loginUser, offCard.MsSeninID);

                RowData data = new RowData();


                data.vesselName = seninTableCache.GetMsVesselName(loginUser, offCard.MsVesselID);

                data.off_shokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, offCard.SiLinkShokumeiCards[0].MsSiShokumeiID);
                data.off_name = offSenin.Sei + " " + offSenin.Mei;
                data.off_date = schedule.SignOnDate;
                //data.off_reason

                data.on_shokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, schedule.MsSiShokumeiID);
                data.on_name = schedule.SignOnCrewName;
                data.on_date = schedule.SignOnDate;

                //data.basho
                //data.bikou

                if (ret.ContainsKey(schedule.MsVesselID) == false)
                {
                    List<RowData> datas = new List<RowData>();
                    datas.Add(data);
                    ret.Add(schedule.MsVesselID, datas);
                }
                else
                {
                    ret[schedule.MsVesselID].Add(data);
                }
            }

            return ret;
        }

        public class RowData
        {
            public string vesselName;
            public string off_shokumei;
            public string off_name;
            public DateTime off_date;
            public string off_reason;

            public string on_shokumei;
            public string on_name;
            public DateTime on_date;

            public string basho;
            public string bikou;
        }
    }
}
