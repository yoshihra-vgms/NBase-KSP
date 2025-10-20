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
using DeficiencyControl.Grid;

namespace DeficiencyControl.Accident
{

    /// <summary>
    /// Accident詳細画面 Attachment表示グリッド
    /// </summary>
    public class AccidentDetailAttachmentGrid : BaseAttachmentGrid
    {
        public AccidentDetailAttachmentGrid(DataGridView dv)
            : base(dv)
        {
        }
    }
}
