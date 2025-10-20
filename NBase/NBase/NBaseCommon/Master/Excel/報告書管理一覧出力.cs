using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseCommon.Master.Excel
{
    public class 報告書管理一覧出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public 報告書管理一覧出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }

        public void CreateFile(MsUser loginUser, List<MsDmHoukokusho> houkokushoList)
        {
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                xls.OpenBook(outputFilePath, templateFilePath);

                _CreateFile(loginUser, xls, houkokushoList);

                xls.CloseBook(true);
            }
        }

        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, List<MsDmHoukokusho> houkokushoList)
        {
            xls.SheetNo = 0;

            int rowNo = 1;
            int startRowIndex = 3;
            int rowIndex = startRowIndex + 1;
            foreach (MsDmHoukokusho info in houkokushoList)
            {
                List<DmPublisher> dmPublishers = DmPublisher.GetRecordsByLinkSakiID(loginUser, info.MsDmHoukokushoID);
                List<DmKoukaiSaki> dmKoukaiSakis =DmKoukaiSaki.GetRecordsByLinkSakiID(loginUser, info.MsDmHoukokushoID);





                xls.RowCopy(startRowIndex, rowIndex);


                int colIndex = 0;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BunruiName;
                colIndex++;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.ShoubunruiName;
                colIndex++;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BunshoNo;
                colIndex++;

                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.BunshoName;
                colIndex++;

                // 未提出チェック
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.CheckTarget == 1 ? "○" : "";
                colIndex++;

                // 雛形ファイル名
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.TemplateFileName;
                colIndex++;




                // 発行元
                //if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船))
                //{
                //    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                //}
                //colIndex++;
                if (dmPublishers.Any(o => o.MsVesselID == 2)) // ほくれん丸
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsVesselID == 4)) // 第二ほくれん丸
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsVesselID == 5)) // シルバークイーン
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsVesselID == 6)) // シルバープリンセス
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsVesselID == 7)) // シルバーティアラ
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;

                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;

                if (dmPublishers.Any(o => o.MsBumonID == "0")) 
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsBumonID == "1"))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmPublishers.Any(o => o.MsBumonID == "2"))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;


                // 公開先
                if (dmKoukaiSakis.Any(o => o.MsVesselID == 2)) // ほくれん丸
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsVesselID == 4)) // 第二ほくれん丸
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsVesselID == 5)) // シルバークイーン
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsVesselID == 6)) // シルバープリンセス
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsVesselID == 7)) // シルバーティアラ
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;

                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;

                if (dmKoukaiSakis.Any(o => o.MsBumonID == "0"))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsBumonID == "1"))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;
                if (dmKoukaiSakis.Any(o => o.MsBumonID == "2"))
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = "○";
                }
                colIndex++;




                // 提出周期
                xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Shuki;
                colIndex++;

                // 提出時期
                for (int i = 0; i < 12; i ++)
                {
                    xls.Cell(ExcelUtils.ToCellName(colIndex, rowIndex)).Value = info.Jiki[i] == '0' ? "" : "○";
                    colIndex++;
                }



                rowNo++;
                rowIndex++;
            }
            xls.RowDelete(startRowIndex, 1);
        }
    }



}
