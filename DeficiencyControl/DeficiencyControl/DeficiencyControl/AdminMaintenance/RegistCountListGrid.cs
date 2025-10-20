using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Grid;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.AdminMaintenance
{
    /// <summary>
    /// RegistCountGrid管理
    /// </summary>
    public class RegistCountListGrid : BaseGrid
    {
        public RegistCountListGrid(DataGridView dv)
            : base(dv)
        {
        }



        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="objlist">List RegistCountData</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<RegistCountData> datalist = objlist as List<RegistCountData>;
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
            foreach (RegistCountData data in datalist)
            {

                int pos = 0;

                //data
                this.Grid[pos, i].Value = data;
                pos++;

                //no
                this.Grid[pos, i].Value = (i + 1).ToString();
                pos++;

                //Title
                this.Grid[pos, i].Value = data.Title;
                pos++;

                //Kind
                {
                    MsItemKind kind = db.GetMsItemKind(data.Kind);
                    if (kind != null)
                    {
                        this.Grid[pos, i].Value = kind.ToString();
                    }
                    pos++;
                }


                //Registcount
                this.Grid[pos, i].Value = data.RegistCount;
                pos++;


                i++;

            }

            return true;
        }
    }
}
