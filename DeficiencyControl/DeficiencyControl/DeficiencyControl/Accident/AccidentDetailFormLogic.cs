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

using DeficiencyControl.Forms;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// 事故トラブル詳細画面ロジック
    /// </summary>
    public class AccidentDetailFormLogic : BaseFormLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f"></param>
        /// <param name="fd"></param>
        public AccidentDetailFormLogic(AccidentDetailForm f, AccidentDetailForm.AccidentDetailFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }

        /// <summary>
        /// 管理画面
        /// </summary>
        AccidentDetailForm Form = null;

        /// <summary>
        /// 管理データ
        /// </summary>
        AccidentDetailForm.AccidentDetailFormData FData = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //Itemタブの初期化
            this.Form.accidentHeaderControl1.InitControl(this.FData.SrcData);
            this.Form.accidentDetailControl1.InitControl(this.FData.SrcData);

            //ファイルタブの初期化
            this.Form.accidentDetailAttachmentControl1.InitControl(this.FData.SrcData);
        }
    }
}
