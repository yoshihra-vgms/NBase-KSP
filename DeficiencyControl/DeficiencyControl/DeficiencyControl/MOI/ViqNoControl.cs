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

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// ViqNo選択コントロール
    /// </summary>
    public partial class ViqNoControl : BaseControl
    {
        public ViqNoControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 入力データ
        /// </summary>
        public class InitData
        {
            /// <summary>
            /// VIQ Code
            /// </summary>
            public MsViqCode Code = null;

            /// <summary>
            /// 表示データ
            /// </summary>
            public MsViqNo SrcData = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        class ViqNoControlData
        {
            public InitData InData = null;
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        ViqNoControlData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Codeの取得
            MsViqCode code = this.FData.InData.Code;

            //作成
            this.singleLineComboViqNo.Clear();
            if (code != null)
            {
                ControlItemCreator.CreateMsViqNo(this.singleLineComboViqNo, code.viq_code_id);
            }
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="no">表示データ nullでクリア</param>
        private void DispData(MsViqNo data)
        {
            if (data == null)
            {
                this.singleLineComboViqNo.Text = "";
                this.textBoxViqNoDescription.Text = "";
                return;
            }

            //表示
            this.singleLineComboViqNo.Text = data.ToString();
            this.textBoxViqNoDescription.Text = data.description;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inputdata">InitData nullだと失敗だがVIQCodeが定まってないなら許容すべきではあると思う</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ViqNoControlData();
            this.FData.InData = inputdata as InitData;
            if (this.FData.InData == null)
            {
                return false;
            }

            //コントロールの初期化
            this.InitDisplayControl();


            //初期選択
            this.DispData(this.FData.InData.SrcData);

            return true;
        }

        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      this.singleLineComboViqNo,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。


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
        /// 選択データの取得
        /// </summary>
        /// <returns></returns>
        public MsViqNo GetSelectData()
        {
            MsViqNo ans = this.singleLineComboViqNo.SelectedItem as MsViqNo;
            return ans;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViqNoControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboViqNo_selected(object sender, EventArgs e)
        {
            MsViqNo no = this.singleLineComboViqNo.SelectedItem as MsViqNo;
            this.DispData(no);
        }

        /// <summary>
        /// クリアするとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboViqNo_Cleared(object sender, EventArgs e)
        {
            this.DispData(null);
        }
    }
}
