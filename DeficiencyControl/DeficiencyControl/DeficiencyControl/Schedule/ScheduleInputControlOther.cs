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
using DeficiencyControl.Controls;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュールその他入力コントロール
    /// </summary>
    public partial class ScheduleInputControlOther : BaseScheduleInputControl
    {
        public ScheduleInputControlOther() : base(EScheduleCategory.その他)
        {
            InitializeComponent();
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        public class ScheduleInputControlOtherData
        {
            /// <summary>
            /// 元データ。新規=null
            /// </summary>
            public DcScheduleOther SrcData = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleInputControlOtherData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            //基底部の表示
            this.DispBaseData(this.FData.SrcData);

            //メモ
            this.textBoxEventMemo.Text = this.FData.SrcData.event_memo;
        }


        //----------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// エラーのチェック
        /// </summary>
        /// <param name="chcol">色変更可否</param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      this.textBoxEventMemo,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));
                
                
                //BaseCheckを行わない。必要なら個別で確認を行うこと。
                //this.ErList.AddRange(this.GetBaseErrorControl());


                //エラーがないなら終わり
                if (this.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputErrorException");

            }
            catch (Exception e)
            {
                //エラーの表示を行う
                if (chcol == true)
                {
                    this.DispError();
                }

            }

            return false;
        }

        /// <summary>
        /// 入力の取得
        /// </summary>        
        /// <returns></returns>
        public DcScheduleOther GetInputData()
        {

            DcScheduleOther ans = new DcScheduleOther();
            if (this.FData.SrcData != null)
            {
                ans = (DcScheduleOther)this.FData.SrcData.Clone();
            }

            //基底データ取得
            ans = this.GetBaseInputData<DcScheduleOther>(ans);

            //イベントメモ取得
            ans.event_memo = this.textBoxEventMemo.Text.Trim();

            return ans;

        }


        /// <summary>
        /// データ
        /// </summary>
        /// <param name="inputdata">表示するDcScheduleOther</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleInputControlOtherData();
            this.FData.SrcData = inputdata as DcScheduleOther;


            //画面初期化
            this.InitDisplayControl();


            if (this.FData.SrcData == null)
            {
                //新規
            }
            else
            {
                //更新
                this.DispData();
            }



            return true;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleInputControlOther_Load(object sender, EventArgs e)
        {

        }
    }
}
