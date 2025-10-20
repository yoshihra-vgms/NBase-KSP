using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;
using DeficiencyControl.Grid;


namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船詳細添付ファイルグリッド管理
    /// </summary>
    public class MoiDetailAttachmentGrid : BaseAttachmentGrid
    {
        public MoiDetailAttachmentGrid(DataGridView dv)
            : base(dv)
        {
        }
    }
}
