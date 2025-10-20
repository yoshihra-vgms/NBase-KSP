using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Logic;


namespace DeficiencyControl.Grid
{
    /// <summary>
    /// ポータル画面アラーム表示グリッド管理
    /// </summary>
    public class PortalAlarmGrid : BaseGrid
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dv"></param>
        public PortalAlarmGrid(DataGridView dv)
            : base(dv)
        {
        }


        public const string AlarmString = "{0} [1} {2}がまもなく{3}ヶ月前となります";



        /// <summary>
        /// ヶ月数を計算する
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int CalcuMonth(int day)
        {
            int ans = day / 30;
            return ans;
        }


        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="objlist">List DcSchedulePlan</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<DcSchedulePlan> datalist = objlist as List<DcSchedulePlan>;
            if (datalist == null)
            {
                return false;
            }

            DateTime basedate = DateTime.Now.Date;
            DBDataCache db = DcGlobal.Global.DBCache;
            

            //----------------------------------------------------------
            //クリア
            this.Grid.Rows.Clear();

            
            //表示数設定
            this.Grid.RowCount = datalist.Count;
            
            int i = 0;
            foreach (DcSchedulePlan data in datalist)
            {
                int pos = 0;
                                 
                MsAlarmColor acol = ComLogic.GetAlarmColor(basedate, data.expiry_date);
                if (acol == null)
                {
                    //アラーム色が取得できないということは、アラムー対象で無い。よってこれはデータに含めない
                    continue;
                }


                //data
                this.Grid[pos, i].Value = data;
                pos++;


                //No
                this.Grid[pos, i].Value = (i + 1);
                pos++;

                //Vessel
                string vesname = "";
                this.Grid[pos, i].Value = "";
                MsVessel ves = db.GetMsVessel(data.ms_vessel_id);
                if (ves != null)
                {
                    vesname = ves.ToString();
                    this.Grid[pos, i].Value = vesname;
                }

                pos++;

                //種別
                this.Grid[pos, i].Value = "";
                MsScheduleKind kind = db.GetMsScheduleKind(data.schedule_kind_id);
                if (kind != null)
                {
                    this.Grid[pos, i].Value = kind.ToString();
                }
                pos++;

                //詳細
                this.Grid[pos, i].Value = "";
                MsScheduleKindDetail detail = db.GetMsScheduleKindDetail(data.schedule_kind_detail_id);
                if (detail != null)
                {
                    this.Grid[pos, i].Value = detail.ToString();
                }
                pos++;

                //アラーム文字列
                int moc = this.CalcuMonth(acol.day_count);

                string hs = vesname + " " + kind.ToString() + " " + detail.ToString();
                this.Grid[pos, i].Value = String.Format("{0}がまもなく{1}ヶ月前となります", hs, moc);
                
                //なぜか四つ目以降の文字を{3}を指定するとフォーマットエラーになる。
                //後ほど調査予定
                /*object[] alvec = {
                                   ves.ToString(),
                                   kind.ToString(),
                                   detail.ToString(),
                                   mo
                               };
                this.Grid[pos, i].Value = string.Format(AlarmString, alvec);*/

                
                pos++;


                //Schedule
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(data.estimate_date);
                pos++;

                //ExpiryDate
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(data.expiry_date);
                pos++;
                
                //色の設定
                this.Grid.Rows[i].DefaultCellStyle.BackColor = acol.AlarmColor;
                

                
                i++;
            }

            //最終表示数を決定
            this.Grid.RowCount = i;

            return true;
        }
    }
}
