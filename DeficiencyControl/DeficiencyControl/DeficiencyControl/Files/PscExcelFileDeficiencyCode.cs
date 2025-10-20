using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB.DAC;
using DcCommon.DB;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;
using System.Drawing;


namespace DeficiencyControl.Files
{
    /// <summary>
    /// PSCファイル出力 Deficiency集計タブ
    /// </summary>
    class PscExcelFileDeficiencyCode : BasePSCOutputExcelTab
    {

        public PscExcelFileDeficiencyCode(int sheetno)
            : base(sheetno)
        {
        }

        #region Excel変数名定義

        public const string VesselName = "**VesselName";

        public const string Year = "**Year";
        public const string PSCCount = "**PscCount";
        public const string DeficiencyCount = "**DeficiencyCount";


        public const string CategoryNo = "**C{0}_No";
        public const string CategoryText = "**C{0}_Text";
        public const string CategoryCount = "**C{0}_Y{1}";

        public const string CodeNo = "**C{0}_Code_{1}";
        public const string CodeText = "**C{0}_Text_{1}";
        
        public const string CYData = "**C{0}_Y{1}_Code{2}";

        /// <summary>
        /// 最大出力年数
        /// </summary>
        public const int MaxYearCount = 10;

        /// <summary>
        /// 最大カテゴリの数
        /// </summary>
        public const int MaxCategoryCount = 99;

        //横の最大数
        public const int MaxColCount = 24;

        #endregion


        /// <summary>
        /// 全データを対象として使用しているDeficiencyカテゴリのIDを一意に取得する
        /// </summary>
        /// <param name="odata"></param>
        /// <returns></returns>
        private List<int> CalcuDistinctUseDeficiencyCategoryID(PscOutputData odata)
        {
            List<int> cateidlist = new List<int>();

            //全ﾃﾞｰﾀ
            foreach (PscOutputAggregateData adata in odata.AggregateList)
            {
                //カテゴリIDを抽出
                List<PscOutputDeficiencyAggregateData> deflist = adata.DeficiencyCodeDic.Values.ToList();
                var n = from f in deflist select f.Code.deficiency_category_id;
                cateidlist.AddRange(n);
            }

            List<int> anslist = cateidlist.Distinct().ToList();
            return anslist;
        }
        

        /// <summary>
        /// 書き込むカテゴリ一覧のデータを作成する。使用しているカテゴリ一覧を算出する
        /// </summary>
        /// <param name="odata"></param>
        /// <returns></returns>
        private List<DeficiencyCategoryData> CreateUseCategoryList(PscOutputData odata)
        {
            //カテゴリ元情報
            List<DeficiencyCategoryData> catelist = SvcManager.SvcMana.DeficiencyCategoryData_GetDataList();
            List<DeficiencyCategoryData> anslist = new List<DeficiencyCategoryData>();
            
            //使用しているカテゴリIDを取得
            List<int> usecateidlist = this.CalcuDistinctUseDeficiencyCategoryID(odata);
        
            foreach (DeficiencyCategoryData cate in catelist)
            {
                //ID
                int id = cate.Category.deficiency_category_id;
                //使用している？
                bool extret = usecateidlist.Contains(id);
                if (extret == false)
                {
                    continue;
                }


                anslist.Add(cate);

            }

            

            return anslist;
        }

        /// <summary>
        /// 対象カテゴリの書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="cno">書き込み番号</param>
        /// <param name="cate">書き込みカテゴリ</param>
        /// <param name="odata">ﾃﾞｰﾀ</param>
        private void WriteCategory(XlsxCreator crea, int cno, DeficiencyCategoryData cate, PscOutputData odata)
        {
            string tag = "";

            //カテゴリヘッダーの書き込み
            {
                tag = string.Format(CategoryNo, cno);
                crea.Cell(tag).Value = cate.Category.deficiency_category_no;

                tag = string.Format(CategoryText, cno);
                crea.Cell(tag).Value = cate.Category.deficiency_category_name;

            }

            ///年度、指摘件数のまとめ。初期化として対象年度すべてをＡＤＤしておく
            Dictionary<int, int> countdic = new Dictionary<int, int>();
            odata.AggregateList.ForEach(x => countdic.Add(x.Year, 0));


            int no = 1;
            foreach (MsDeficiencyCode code in cate.CodeList)
            {

                //全年度の書き込み                
                bool writeflag = false;
                int yno = 0;
                foreach (PscOutputAggregateData adata in odata.AggregateList)
                {
                    yno++;

                    //この年のデータはあるか？
                    bool ret = adata.DeficiencyCodeDic.ContainsKey(code.deficiency_code_id);
                    if (ret == false)
                    {
                        continue;
                    }

                    //数を取得
                    int count = adata.DeficiencyCodeDic[code.deficiency_code_id].PscLsit.Count;

                    //年ごとの総計を出したいので数のカウント
                    countdic[adata.Year] = countdic[adata.Year] + count;

                    //書き込み                    
                    tag = string.Format(CYData, cno, yno, no);
                    crea.Cell(tag).Value = count;

                    //このCodeは書き込みがあった
                    writeflag = true;
                }

                //１つもデータを書き込んでいないならスキップ
                if (writeflag == false)
                {
                    continue;
                }

                //ここまで来たらデータがあったのでCodeを書き込み
                {
                    //Code
                    tag = string.Format(CodeNo, cno, no);
                    crea.Cell(tag).Value = code.deficiency_code_name;

                    //Text
                    tag = string.Format(CodeText, cno, no);
                    crea.Cell(tag).Value = code.defective_item;
                }

                no++;
            }

            //--------------------------------------------------

            {
                //不要な列を消す
                string starttag = string.Format(CodeNo, cno, no); 
                string endtag = string.Format(CodeNo, cno, MaxCategoryCount);

                //削除
                this.DeleteRows(crea, starttag, endtag);
                
            }


            //最後に年度ごとに総計を書き込む
            {
                int yno = 1;
                foreach (PscOutputAggregateData adata in odata.AggregateList)
                {

                    //計算した年度の総計を取得
                    int count = countdic[adata.Year];

                    //書き込み
                    tag = string.Format(CategoryCount, cno, yno);
                    crea.Cell(tag).Value = count;

                    yno++;
                }
            }

        }
        



        /// <summary>
        /// カテゴリのデータを書き込む
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="idata"></param>
        /// <param name="odata"></param>
        private void WriteCategoryList(XlsxCreator crea, PscOutputParameter idata, PscOutputData odata)
        {
            //使用している取得カテゴリ情報の取得
            List<DeficiencyCategoryData> catelist = this.CreateUseCategoryList(odata);



            //カテゴリで回す
            int cno = 1;
            foreach (DeficiencyCategoryData cate in catelist)
            {
             

                //対象カテゴリの書き込み
                this.WriteCategory(crea, cno, cate, odata);
                

                cno++;
            }


            //書き終わったら後ろを全て削除する
            string deltag = string.Format(CategoryNo, cno);

            //削除位置を取得
            Point delpos = crea.GetVarNamePos(deltag, 0);
            this.DeleteRows(crea, deltag, 10000);

            //見栄えを良くするため、削除後の最後の行は実線とする
            for (int i = 0; i < MaxColCount; i++)
            {
                crea.Pos(i, delpos.Y - 1).Attr.LineBottom(AdvanceSoftware.ExcelCreator.BorderStyle.Thin, AdvanceSoftware.ExcelCreator.xlColor.Auto);
            }


        }


        /// <summary>
        /// ヘッダー書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="odata"></param>
        private void WriteHeader(XlsxCreator crea, PscOutputData odata)
        {
            string tag = "";

            int yno = 1;
            foreach (PscOutputAggregateData adata in odata.AggregateList)
            {
                //年度
                tag = this.CreateTemplateNo(Year, yno);
                crea.Cell(tag).Value = adata.Year;

                //訪船回数
                tag = this.CreateTemplateNo(PSCCount, yno);
                crea.Cell(tag).Value = adata.PscCount;

                //指摘事項件数
                tag = this.CreateTemplateNo(DeficiencyCount, yno);
                crea.Cell(tag).Value = adata.DeficiencyCount;


                yno++;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="idata"></param>
        /// <param name="odata"></param>
        public override void Write(XlsxCreator crea, PscOutputParameter idata, PscOutputData odata)
        {
            //自分のシートへ移動
            crea.SheetNo = this.SheetNo;

            DBDataCache db = DcGlobal.Global.DBCache;

            //船名の書き込み
            this.WriteVessel(crea, VesselName, idata);
            
            //訪船と年度のヘッダー書き込み
            this.WriteHeader(crea, odata);

            //カテゴリのデータを書き込む
            this.WriteCategoryList(crea, idata, odata);
        }
    }
}
