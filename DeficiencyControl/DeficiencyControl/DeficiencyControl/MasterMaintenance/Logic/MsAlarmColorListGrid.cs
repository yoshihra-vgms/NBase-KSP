using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Grid;

namespace DeficiencyControl.MasterMaintenance.Logic
{
    /// <summary>
    /// MsAlarmColorList管理グリッド
    /// </summary>
    public class MsAlarmColorListGrid : BaseGrid
    {
        public MsAlarmColorListGrid(DataGridView dv)
            : base(dv)
        {
        }


        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="objlist">List MsAlarmColor</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<MsAlarmColor> datalist = objlist as List<MsAlarmColor>;
            if (datalist == null)
            {
                return false;
            }

            DBDataCache db = DcGlobal.Global.DBCache;

            //----------------------------------------------------------
            //クリア
            this.Grid.Rows.Clear();


            //表示数設定
            this.Grid.RowCount = datalist.Count;

            int i = 0;
            foreach (MsAlarmColor data in datalist)
            {
                int pos = 0;

                //data
                this.Grid[pos, i].Value = data;
                pos++;

                //ID
                this.Grid[pos, i].Value = data.alarm_color_id;
                pos++;

                //Day
                this.Grid[pos, i].Value = data.day_count;
                pos++;


                //comment
                this.Grid[pos, i].Value = data.comment;
                pos++;
                

                //このデータに相応しい色を設定する。
                this.Grid.Rows[i].DefaultCellStyle.BackColor = data.AlarmColor;



                i++;
            }

            return true;
        }
    }
}
