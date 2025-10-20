using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB.DAC;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;



namespace DeficiencyControl.Files
{
    /// <summary>
    /// PSCタブ出力のインターフェース
    /// </summary>
    abstract class BasePSCOutputExcelTab : BaseExcelFile
    {

        public BasePSCOutputExcelTab(int sheetno)
        {
            this.SheetNo = sheetno;
        }


        /// <summary>
        /// これの書き込みシート番号
        /// </summary>
        protected int SheetNo = 0;



        public abstract void Write(XlsxCreator crea, PscOutputParameter idata, PscOutputData odata);


        /// <summary>
        /// 船名の書き込み
        /// </summary>
        /// <param name="crea">書き込み場所</param>
        /// <param name="tagname">tag名</param>
        /// <param name="idata">元</param>
        protected void WriteVessel(XlsxCreator crea, string tagname, PscOutputParameter idata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //船名の書き込み
            {
                string vname = VesselALLName;
                if (idata.ms_vessel_id != null)
                {
                    MsVessel ves = DcGlobal.Global.DBCache.GetMsVessel(idata.ms_vessel_id.Value);
                    vname = ves.ToString();
                }
                crea.Cell(tagname).Value = vname;
            }
        }
    }


    /// <summary>
    /// PSC ExcelファイルActionCode集計タブの書き込みクラス
    /// </summary>
    class PscExcelFileActionCode : BasePSCOutputExcelTab
    {


        public PscExcelFileActionCode(int sheetno)
            : base(sheetno)
        {
        }


        #region Excel変数名定義

        public const string VesselName = "**VesselName";


        public const string ActionCode = "**ActionCode";
        public const string ActionCodeText = "**ActionCodeText";


        public const string Year = "**Year";
        public const string PSCCount = "**PscCount";
        public const string DeficiencyCount = "**DeficiencyCount";

        public const string AcData = "**AcData_A{0}_Y{1}";

        /// <summary>
        /// 最大出力年数
        /// </summary>
        public const int MaxYearCount = 10;
        #endregion



        /// <summary>
        /// ActionCodeのヘッダー書き込み
        /// </summary>
        /// <param name="crea">対象</param>
        /// <param name="aclist">書き込み順リスト</param>
        private void WriteActionCode(XlsxCreator crea, List<MsActionCode> aclist)
        {
            int i = 1;
            foreach (MsActionCode ac in aclist)
            {
                string tag = "";

                //Code
                tag = this.CreateTemplateNo(ActionCode, i);
                crea.Cell(tag).Value = ac.action_code_name;

                //Text
                tag = this.CreateTemplateNo(ActionCodeText, i);
                crea.Cell(tag).Value = ac.action_code_text;

                i++;
            }

        }


        /// <summary>
        /// 年度ごとのデータを書き込む
        /// </summary>
        /// <param name="crea">書き込み場所</param>
        /// <param name="aclist">書き込みActionCode順序</param>
        /// <param name="odata">データ</param>
        private void WriteYearCode(XlsxCreator crea, List<MsActionCode> aclist, PscOutputData odata)
        {
            //全データ
            string tag = "";
            int yno = 1;
            foreach (PscOutputAggregateData data in odata.AggregateList)
            {
                //年度
                tag = this.CreateTemplateNo(Year, yno);
                crea.Cell(tag).Value = data.Year;

                //訪船回数
                tag = this.CreateTemplateNo(PSCCount, yno);
                crea.Cell(tag).Value = data.PscCount;

                //指摘事項件数
                tag = this.CreateTemplateNo(DeficiencyCount, yno);
                crea.Cell(tag).Value = data.DeficiencyCount;


                int acno = 1;
                foreach (MsActionCode ac in aclist)
                {
                    //対象データの取得
                    List<DcActionCodeHistory> hislist = data.ActionCodeDic[ac.action_code_id];

                    //データ数を入れる
                    tag = string.Format(AcData, acno, yno);
                    crea.Cell(tag).Value = hislist.Count;

                    acno++;
                }




                yno++;
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Excel書き込み
        /// </summary>
        /// <param name="filename">書き込みファイル名</param>
        /// <param name="idata">パラメータ</param>
        /// <param name="odata">出力データ</param>
        public override void Write(XlsxCreator crea, PscOutputParameter idata, PscOutputData odata)
        {
            //自分のシートへ移動
            crea.SheetNo = this.SheetNo;

            DBDataCache db = DcGlobal.Global.DBCache;

            //船名の書き込み
            {   
                this.WriteVessel(crea, VesselName, idata);
            }

            List<MsActionCode> aclist = db.ActionCodeList;

            //ActionCodeの書き込み
            this.WriteActionCode(crea, aclist);
            
            //各年度のデータ書き込み
            this.WriteYearCode(crea, aclist, odata);


        }
    }
}
