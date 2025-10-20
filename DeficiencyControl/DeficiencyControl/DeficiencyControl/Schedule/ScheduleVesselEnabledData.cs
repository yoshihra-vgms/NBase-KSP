using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

using CIsl.DB.WingDAC;
using DcCommon;

namespace DeficiencyControl.Schedule
{
       /// <summary>
    /// 対象船のスケジュール有効可否データ
    /// </summary>
    public class ScheduleVesselEnabledData
    {
        /// <summary>
        /// 種類
        /// </summary>
        public MsScheduleKind Kind = null;

        /// <summary>
        /// 属する詳細
        /// </summary>
        public List<MsScheduleKindDetail> DetailList = new List<MsScheduleKindDetail>();


        /// <summary>
        /// 対象カテゴリの種別一覧を作成する
        /// </summary>
        /// <param name="cate"></param>
        /// <returns></returns>
        private static List<MsScheduleKind> GetCategoryKindList(EScheduleCategory cate)
        {
            var n = from f in DcGlobal.Global.DBCache.MsScheduleKindList where f.schedule_category_id == (int)cate select f;
            List<MsScheduleKind> anslist = n.ToList();
            return anslist;
        }

        /// <summary>
        /// 作成 返却 [有効なschedule_kind_id 有効なもの一式]
        /// </summary>
        /// <param name="enalist">対象船の有効可否リスト全部</param>
        /// <returns></returns>
        public static Dictionary<int, ScheduleVesselEnabledData> CreateVesselData(List<MsVesselScheduleKindDetailEnable> enalist)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //船に関係する予定実績の種別を取得
            List<MsScheduleKind> kindlist =  GetCategoryKindList(EScheduleCategory.予定実績);

            Dictionary<int, ScheduleVesselEnabledData> ansdic = new Dictionary<int, ScheduleVesselEnabledData>();
            foreach (MsScheduleKind kind in db.MsScheduleKindList)
            {
                ScheduleVesselEnabledData ans = new ScheduleVesselEnabledData();
                ans.Kind = kind;
                ans.DetailList = new List<MsScheduleKindDetail>();

                //詳細を取得
                var n = from f in db.MsScheduleKindDetailList where f.schedule_kind_id == kind.schedule_kind_id select f;
                List<MsScheduleKindDetail> detaillist = n.ToList();

                //詳細の有効可否を取得               
                foreach(MsScheduleKindDetail de in detaillist)
                {
                    #region 有効可否の確認を行い、有効なものはaddする
                    var en = from f in enalist where f.schedule_kind_detail_id == de.schedule_kind_detail_id select f;
                    if (en.Count() <= 0)
                    {
                        continue;
                    }

                    MsVesselScheduleKindDetailEnable e = en.First();
                    if (e.enabled == false)
                    {
                        continue;
                    }

                    ans.DetailList.Add(de);
                    #endregion

                    

                }

                //一つ以上データがない、この種別において有効な詳細が無い場合、種別ごと無視する
                if (ans.DetailList.Count <= 0)
                {
                    continue;
                }

                //追加
                ansdic.Add(kind.schedule_kind_id, ans);

            }
            return ansdic;
        }
    }
}
