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
    /// AccidentCreateForm画面処理
    /// </summary>
    public class AccidentCreateFormLogic : BaseFormLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">管理画面</param>
        /// <param name="fdata">管理データ</param>
        public AccidentCreateFormLogic(AccidentCreateForm f, AccidentCreateForm.AccidentCreateFormData fdata)
        {
            this.Form = f;
            this.FData = fdata;
        }

        /// <summary>
        /// これの画面
        /// </summary>
        private AccidentCreateForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        AccidentCreateForm.AccidentCreateFormData FData = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //ヘッダーの初期化
            this.Form.accidentHeaderControl1.InitControl(null);

            //詳細の初期化
            this.Form.accidentDetailControl1.InitControl(null);
        }





        
    }
}
