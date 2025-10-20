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

namespace DeficiencyControl.Files
{
    /// <summary>
    /// スケジュール一覧Excelファイル、会社有効期限の書き込み
    /// </summary>
    public class ScheduleListExcelFileCompany : BaseScheduleListExcelFile
    {
        #region Excel変数名定義

        //DOC NK審査
        public const string DOCDetail = "**DOCDetail";
        public const string DOCDate = "**DOCDate";

        //ISM
        public const string ISMDetail = "**ISMDetail";
        public const string ISMDate = "**ISMDate";

        //ISO
        public const string ISODetail = "**ISODetail";
        public const string ISODate = "**ISODate";

        //TMSA
        public const string TMSDetail = "**TMSDetail";
        public const string TMSDate = "**TMSDate";

        #endregion


        /// <summary>
        /// 有効期限のデータを取得 基準日以後で一番近い有効期限のデータ
        /// </summary>
        /// <param name="basedate"></param>
        /// <param name="detail"></param>
        /// <param name="comlist"></param>
        /// <returns></returns>
        private DcScheduleCompany GetWriteData(DateTime basedate, MsScheduleKindDetail detail, List<DcScheduleCompany> comlist)
        {
            //対象種別詳細のデータ、なおかつ基準日以降のデータ
            var n = from f in comlist where f.schedule_kind_detail_id == detail.schedule_kind_detail_id && f.expiry_date >= basedate select f;            
            if (n.Count() <= 0)
            {
                return null;
            }

            //有効期限の順番に並べる ・・・基準日以降のデータを抽出済みなのでこれで最新が分かるはず。
            n = n.OrderBy(x => x.expiry_date);
            List<DcScheduleCompany> plist = n.ToList();

            //最初のデータを取得
            DcScheduleCompany ans = plist.First();

            return ans;

        }




        /// <summary>
        /// 対象種別の書き込み
        /// </summary>
        /// <param name="crea">XlsxCreator</param>
        /// <param name="wdata">書き込みデータ</param>
        /// <param name="detailtag">書き込み詳細名称タグ</param>
        /// <param name="datetag">有効期限タグ</param>
        /// <param name="kind">種別</param>
        private void WriteScheduleKind(XlsxCreator crea, ScheduleListWriteData wdata, string detailtag, string datetag, EScheduleKind kind)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            string tag = "";

            //対象カテゴリの取得
            List<MsScheduleKindDetail> detaillist = db.GetMsScheduleKindDetailList(kind);

            int no = 1;
            foreach (MsScheduleKindDetail detail in detaillist)
            {
                //詳細名
                tag = this.CreateTemplateNo(detailtag, no);
                crea.Cell(tag).Value = detail.ToString();

                //有効期限
                DcScheduleCompany data = this.GetWriteData(wdata.Date, detail, wdata.CompanyList);
                if (data != null)
                {
                    tag = this.CreateTemplateNo(datetag, no);
                    crea.Cell(tag).Value = DcGlobal.DateTimeToString(data.expiry_date);
                }

                no++;
            }
        }


        /// <summary>
        /// 会社グリッドの書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata"></param>
        public void WriteCompanyGrid(XlsxCreator crea, ScheduleListWriteData wdata)
        {
            //DOC
            this.WriteScheduleKind(crea, wdata, DOCDetail, DOCDate, EScheduleKind.DOC_NK審査);

            //ISMDate
            this.WriteScheduleKind(crea, wdata, ISMDetail, ISMDate, EScheduleKind.ISM);

            //ISO
            this.WriteScheduleKind(crea, wdata, ISODetail, ISODate, EScheduleKind.ISO);

            //TMSA
            this.WriteScheduleKind(crea, wdata, TMSDetail, TMSDate, EScheduleKind.TMSA);

        }
    }
}
