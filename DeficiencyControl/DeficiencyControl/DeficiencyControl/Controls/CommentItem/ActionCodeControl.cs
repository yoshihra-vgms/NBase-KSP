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
using DeficiencyControl.Logic;

using DeficiencyControl.Util;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Controls.CommentItem
{
    /// <summary>
    /// ActionCodeをあらわすもの
    /// </summary>
    public partial class ActionCodeControl : BaseChildItemControl
    {
        public ActionCodeControl()
        {
            InitializeComponent();
        }


        public class InputData
        {
            /// <summary>
            /// 表示データ　新規null
            /// </summary>
            public DcActionCodeHistory SrcAc = null;

            /// <summary>
            /// 検索条件として使うフラグ
            /// </summary>
            public bool SearchFlag = false;

        }

        /// <summary>
        /// このコントロールのデータクラス
        /// </summary>
        class ActionCodeControlData
        {
            /// <summary>
            /// これがnullなら新規
            /// </summary>
            public DcActionCodeHistory SrcData = null;

            /// <summary>
            /// 検索条件として使うフラグ trueで検索として使う
            /// </summary>
            public bool SearchFlag = false;
        }

        /// <summary>
        /// このコントロールのデータ
        /// </summary>
        private ActionCodeControlData FData = null;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <returns></returns>
        private bool InitDisplayControl()
        {
            //ActionCodeの初期化
            ControlItemCreator.CreateActionCode(this.comboBoxActionCode, this.FData.SearchFlag);
            
            return true;
        }

        /// <summary>
        /// データの表示
        /// </summary>
        /// <returns></returns>
        private bool DispData()
        {
            //DB
            DBDataCache db = DcGlobal.Global.DBCache;


            //表示物
            DcActionCodeHistory data = this.FData.SrcData;

            //ActionCode
            {
                MsActionCode ac = db.GetMsActionCode(data.action_code_id);
                if (ac != null)
                {
                    this.comboBoxActionCode.SelectedItem = ac;
                }
            }
     
            this.textBoxActionCodeText.Text = data.action_code_text;

            return true;
        }


        /// <summary>
        /// ActionCodeの説明文表示
        /// </summary>
        private void DispActionCodeText()
        {
            this.textBoxActionCodeText.Text = "";
            this.textBoxActionCodeText.ReadOnly = true;

            //選択取得
            MsActionCode ac = this.comboBoxActionCode.SelectedItem as MsActionCode;
            if (ac == null)
            {
                return;
            }


            //Text設定
            this.textBoxActionCodeText.Text = ac.action_code_text;

            if (this.FData.SearchFlag == true)
            {
                return;
            }

            //ActionCode99 FreeTextが選択された場合は入力を有効にする。
            if (ac.action_code_id == AppConfig.Config.ConfigData.ms_action_code_99)
            {
                this.textBoxActionCodeText.ReadOnly = false;
            }



            
        }
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            return true;
        }

        /// <summary>
        /// このコントロールの初期化
        /// </summary>
        /// <param name="inputdata">InputData 新規はnull、もしくはInputData.SrcAcをnullにする</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ActionCodeControlData();


            //設定
            InputData idata = inputdata as InputData;            
            if (idata != null)
            {
                this.FData.SrcData = idata.SrcAc;
                this.FData.SearchFlag = idata.SearchFlag;
            }


            //画面初期化
            this.InitDisplayControl();

            //検索として使う場合の初期化
            if (this.FData.SearchFlag == true)
            {
                this.checkBoxDelete.Visible = false;
            }

            if (this.FData.SrcData == null)
            {
                //新規
               
            }
            else
            {
                //更新

                
                //表示
                this.DispData();
            }



            return true;
        }


        /// <summary>
        /// データ取得
        /// </summary>
        ///<param name="no">順番</param>
        /// <returns></returns>
        public DcActionCodeHistory GetInputData(int no)
        {
            DcActionCodeHistory ans = new DcActionCodeHistory();
            if (this.FData.SrcData != null)
            {
                ans = (DcActionCodeHistory)this.FData.SrcData.Clone();
            }

            //ActionCode
            ans.action_code_id = MsActionCode.EVal;
            MsActionCode ac = this.comboBoxActionCode.SelectedItem as MsActionCode;
            if (ac != null)
            {
                ans.action_code_id = ac.action_code_id;
            }


            //Text
            ans.action_code_text = this.textBoxActionCodeText.Text;


            //ORDER
            ans.order_no = no;

            return ans;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionCodeControl_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// ActionCodeが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxActionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DispActionCodeText();
        }


        
    }
}
