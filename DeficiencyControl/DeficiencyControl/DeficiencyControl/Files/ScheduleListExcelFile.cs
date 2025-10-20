using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;
using DeficiencyControl.Schedule;
using DeficiencyControl.Schedule.ListGrid;
using System.Drawing;

namespace DeficiencyControl.Files
{
    
    /// <summary>
    /// スケジュール一覧Excel基底
    /// </summary>
    public class BaseScheduleListExcelFile : BaseExcelFile
    {

        public const string VesselName = "**VesselName";


        /// <summary>
        /// 船の最大値
        /// </summary>
        public const int MAX_Vessel = 30;


        /// <summary>
        /// ひとつの船の横のセル数
        /// </summary>
        public const int VesselDataWith = 6;

        

        /// <summary>
        /// スケジュール一覧書き込みデータのまとめ
        /// </summary>
        public class ScheduleListWriteData
        {
            /// <summary>
            /// 年度
            /// </summary>
            public MsYear Year = null;

            /// <summary>
            /// 基準日（作成日）
            /// </summary>
            public DateTime Date;

            /// <summary>
            /// 書き込み船一覧
            /// </summary>
            public List<MsVessel> AllVesselList = new List<MsVessel>();



            

            /// <summary>
            /// 船ごとの書き込み位置の情報を作成する   ようは内航、外航に分かれたとしてもこの船位置を元に全データを書けば問題ないということ これはテンプレートのVesse_1の部分にかかってくる = X位置ともいえる
            /// [船ID, 位置]
            /// </summary>
            public Dictionary<decimal, int> VesWritePosDic = new Dictionary<decimal, int>();


            /// <summary>
            /// 年間ごとにデータまとめ
            /// </summary>
            public Dictionary<int, MonthVesselData> MonthDic = new Dictionary<int, MonthVesselData>();


            /// <summary>
            /// 船ごとのSchedule有効可否   [船ID, [schedule_kind_id, 有効可否情報]]
            /// </summary>
            //public Dictionary<decimal, Dictionary<int, ScheduleVesselEnabledData>> EnabledDic = new Dictionary<decimal, Dictionary<int, ScheduleVesselEnabledData>>();
            public List<MsVesselScheduleKindDetailEnable> KindDetailEnabledList = new List<MsVesselScheduleKindDetailEnable>();

            /// <summary>
            /// 予定一覧
            /// </summary>
            public List<DcSchedulePlan> PlanList = new List<DcSchedulePlan>();


            /// <summary>
            /// 会社予定一覧
            /// </summary>
            public List<DcScheduleCompany> CompanyList = new List<DcScheduleCompany>();


            /// <summary>
            /// 基準日以後の有効期限をまとめたもの
            /// </summary>
            public List<DcSchedulePlan> ExpiryPlanList = new List<DcSchedulePlan>();

            #region 便利関数を定義しておく
            /// <summary>
            /// 対象船の書き込み位置の取得
            /// </summary>
            /// <param name="ms_vessel_id"></param>
            /// <returns></returns>
            public int GetVesselWritePos(decimal ms_vessel_id)
            {
                int ans = -1;
                ans = this.VesWritePosDic[ms_vessel_id];
                return ans;
            }
            #endregion

        }

    }

    /// <summary>
    /// スケジュールExcelファイル
    /// </summary>
    public class ScheduleListExcelFile : BaseScheduleListExcelFile
    {
        #region Excel変数名定義

        public const string CreateDate = "**CreateDate";
        
        #endregion

        

        /// <summary>
        /// テンプレートファイルの保存
        /// </summary>
        /// <param name="filename">保存名</param>
        private void DownloadTemplateFile(string filename)
        {
            DcLog.WriteLog("DownloadTemplateFile Start filename=" + filename);
            try
            {
                //テンプレート取得
                byte[] data = SvcManager.SvcMana.GetScheduleListTemplateFile();
                if (data == null)
                {
                    throw new Exception("GetScheduleListTemplateFile NULL");
                }

                DcGlobal.ByteArrayToFile(filename, data);

            }
            catch (Exception e)
            {
                throw new Exception("DownloadTemplateFile失敗", e);
            }

            DcLog.WriteLog("DownloadTemplateFile End");

            return;
        }


        /// <summary>
        /// 対象年度書き込みデータを全て収集する
        /// </summary>
        /// <param name="year">検索年度</param>
        /// <returns></returns>
        private ScheduleListWriteData SearchData(MsYear year)
        {
            ScheduleListWriteData ans = new ScheduleListWriteData();

            //条件設定
            ans.Year = year;

            //基準日=今日の日付
            ans.Date = DateTime.Now.Date;

            //対象船を取得
            {
                //内航と外航の区別をする場合はここで行う
                ans.AllVesselList = DcGlobal.Global.DBCache.VesselList;


                //船ごとの書き込み位置情報を作成する・・・ようは内航、外航に分かれたとしてもこの船位置を元に全データを書けば問題ないということ
                //もし船種別で分けるなら、外航船は指定位置までoffsetせよ
                ans.VesWritePosDic = new Dictionary<decimal, int>();

                int vpos = 1;
                foreach (MsVessel ves in ans.AllVesselList)
                {
                    ans.VesWritePosDic.Add(ves.ms_vessel_id, vpos);
                    vpos++;
                }
            }

            //船予定の検索・・・有効期限と分離したため、予定日だけを取得する
            SchedulePlanSearchData psdata = new SchedulePlanSearchData();
            psdata.estimate_date_start = year.start_date;
            psdata.estimate_date_end = year.end_date;
            //psdata.inspection_date_start = year.start_date;
            //psdata.inspection_date_end = year.end_date;

            //psdata.expiry_date_start = year.start_date;
            //psdata.expiry_date_end = year.end_date;
            List<DcSchedulePlan> plist = SvcManager.SvcMana.DcSchedulePlan_GetRecordsBySearchData(psdata);

            ans.PlanList = plist;

            //その他予定
            ScheduleOtherSearchData osdata = new ScheduleOtherSearchData();
            osdata.estimate_date_start = year.start_date;
            osdata.estimate_date_end = year.end_date;
            //osdata.inspection_date_start = year.start_date;
            //osdata.inspection_date_end = year.end_date;

            //osdata.expiry_date_start = year.start_date;
            //osdata.expiry_date_end = year.end_date;
            List<DcScheduleOther> otlist = SvcManager.SvcMana.DcScheduleOther_GetRecordsBySearchData(osdata);

            //データ形式の変換
            ans.MonthDic = MonthVesselData.CreateMonthVesselDataDic(year.year, plist, otlist, ans.AllVesselList);


            //会社予定検索・・・基準日以後の有効期限を全て取得
            ScheduleCompanySearchData csdata = new ScheduleCompanySearchData();
            //csdata.estimate_date_start = year.start_date;
            //csdata.estimate_date_end = year.end_date;
            //csdata.inspection_date_start = year.start_date;
            //csdata.inspection_date_end = year.end_date;
            //csdata.expiry_date_start = year.start_date;
            //csdata.expiry_date_end = year.end_date;
            csdata.output_expiry_date = ans.Date;
            ans.CompanyList = SvcManager.SvcMana.DcScheduleCompany_GetRecordsBySearchData(csdata);

            {
                
                //有効期限情報の検索　基準日以後のデータを検索する
                //結論として、入居予定のことまで考えると全データ取得しないと前回前々回や最新かどうかすらわからないため、条件なしで全てを取得する。                
                //メモリの不安があるがその場合は保持データの検討、さーばー側での処理などを検討すること。
                SchedulePlanSearchData exps = new SchedulePlanSearchData();
                //exps.output_expiry_date = ans.Date;
                ans.ExpiryPlanList = SvcManager.SvcMana.DcSchedulePlan_GetRecordsBySearchData(exps);
                
                
                //全有効可否情報を取得
                List<MsVesselScheduleKindDetailEnable> enabledlist = SvcManager.SvcMana.MsVesselScheduleKindDetailEnable_GetRecords();
                ans.KindDetailEnabledList = enabledlist;
            }

            return ans;
        }

        
        /// <summary>
        /// Excelの書き込み
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="wdata"></param>
        private void WriteExcel(string filename, ScheduleListWriteData wdata)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    string tag = "";


                    //初期化


                    //作成日の書き込み
                    tag = CreateDate;
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(wdata.Date);


                    //スケジュール一覧列の書き込み
                    {
                        ScheduleListExcelFileGrid gp = new ScheduleListExcelFileGrid();
                        gp.WriteSchedule(crea, wdata);
                    }

                    //会社の書き込み
                    {
                        ScheduleListExcelFileCompany cp = new ScheduleListExcelFileCompany();
                        cp.WriteCompanyGrid(crea, wdata);
                    }


                    //有効期限書き込み
                    {
                        ScheduleListExcelFileExpiry ep = new ScheduleListExcelFileExpiry();
                        ep.WriteExpiryGrid(crea, wdata);
                    }

                    //最後に不要な船列を消す
                    {
                        //削除開始位置・・・最後に書いた船の次の船
                        tag = this.CreateTemplateNo(VesselName, wdata.AllVesselList.Count + 1);
                        Point st = crea.GetVarNamePos(tag, 0);

                        //削除終了位置
                        tag = this.CreateTemplateNo(VesselName, MAX_Vessel);
                        Point ed = crea.GetVarNamePos(tag, 0);

                        //削除数の算出
                        int delcount = ed.X - st.X + VesselDataWith;
                        if (delcount > 0)
                        {
                            crea.ColumnDelete(st.X, delcount);
                        }
                    }



                    //シート名を変更
                    string sheetname = string.Format("{0}年度", wdata.Year.year);
                    crea.SheetName = sheetname;


                    crea.FullCalcOnLoad = true;

                }
                catch (Exception e)
                {
                    throw new Exception("WriteExcel Exception", e);
                }
                finally
                {
                    crea.CloseBook(true);
                }
            }
        }

        /// <summary>
        /// エクセルの出力 通信します waitestateせよ
        /// </summary>
        /// <param name="filename">出力ファイル名</param>
        /// <param name="year">出力年度</param>
        public void OutputExcel(string filename, MsYear year)
        {
            try
            {
                //情報収集
                ScheduleListWriteData wdata = this.SearchData(year);
                

                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel(filename, wdata);
                
            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("ScheduleListExcelFile Exception", e);
            }

        }
    }
}
