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
    /// スケジュール会社入力コントロール
    /// </summary>
    public partial class ScheduleInputControlCompany : BaseScheduleInputControl
    {
        public ScheduleInputControlCompany() : base(EScheduleCategory.会社)
        {
            InitializeComponent();
        }



        /// <summary>
        /// これのデータ
        /// </summary>
        public class ScheduleInputControlCompanyData
        {
            /// <summary>
            /// 元データ。新規=null
            /// </summary>
            public DcScheduleCompany SrcData = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleInputControlCompanyData FData = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            //基底部の表示
            this.DispBaseData(this.FData.SrcData);
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
                                      //this.textBoxAccident,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));
                this.ErList.AddRange(this.GetBaseErrorControl());


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
        /// <param name="ms_vessel_id"></param>
        /// <returns></returns>
        public DcScheduleCompany GetInputData()
        {

            DcScheduleCompany ans = new DcScheduleCompany();
            if (this.FData.SrcData != null)
            {
                ans = (DcScheduleCompany)this.FData.SrcData.Clone();
            }

            //基底データ取得
            ans = this.GetBaseInputData<DcScheduleCompany>(ans);


            return ans;

        }


        /// <summary>
        /// データ
        /// </summary>
        /// <param name="inputdata">表示するDcScheduleCompany</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleInputControlCompanyData();
            this.FData.SrcData = inputdata as DcScheduleCompany;


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
        private void ScheduleInputControlCompany_Load(object sender, EventArgs e)
        {

        }
    }
}
