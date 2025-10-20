using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

namespace DeficiencyControl.Grid
{
    /// <summary>
    /// コメント詳細attachmentタブグリッド管理クラス
    /// </summary>
    class CiDetailAttachmentGrid : BaseGrid
    {
        public CiDetailAttachmentGrid(DataGridView dv)
            : base(dv)
        {
        }

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="objlist">List DcAttachment</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<DcAttachment> datalist = objlist as List<DcAttachment>;
            if(datalist == null)
            {
                return false;
            }
            
            //----------------------------------------------------------
            //クリア
            this.Grid.Rows.Clear();

            //表示数設定
            this.Grid.RowCount = datalist.Count;

            int i = 0;
            foreach(DcAttachment data in datalist)
            {
                int pos = 0;

                //data
                this.Grid[pos, i].Value = data;
                pos++;

                //No
                //this.Grid[pos, i].Value = data.attachment_id;
                this.Grid[pos, i].Value = (i + 1);
                pos++;

                //日付
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(data.create_date);
                pos++;

                //分類
                this.Grid[pos, i].Value = data.AttachmentTypeName;
                pos++;

                //ファイル名
                this.Grid[pos, i].Value = data.filename;
                pos++;

                //ファイル更新日
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(data.update_date);
                pos++;

                i++;
            }

            return true;
        }
    }
}
