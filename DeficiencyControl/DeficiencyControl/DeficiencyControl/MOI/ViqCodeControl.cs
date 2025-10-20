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
    /// VIQ Code選択コントロール
    /// </summary>
    public partial class ViqCodeControl : BaseControl
    {
        public ViqCodeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// VIQ Version
        /// </summary>
        public MsViqVersion ViqVersion = null;

        /// <summary>
        /// データが選択されたときのイベント
        /// </summary>
        /// <param name="code"></param>
        public delegate void SelectDataDelegate(MsViqCode code);


        /// <summary>
        /// データが選択されたときに発生するもの
        /// </summary>
        public SelectDataDelegate DataSelecDelegatetProc = null;


        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitDisplayControl()
        {
            int viq_version_id = MsViqCode.EVal;

            if (ViqVersion != null)
            {
                // VIQ Version7対応（以下をコメントにすれば以前と同じように動く）
                viq_version_id = ViqVersion.viq_version_id;
            }
            ControlItemCreator.CreateMsViqCode(this.comboBoxViqCode, viq_version_id);
        }



        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="code">表示物 nullでクリア</param>
        private void DispData(MsViqCode code)
        {
            if (code == null)
            {
                //this.singleLineComboViqCode.Text = "";
                //this.textBoxViqCodeDescription.Text = "";               
                return;
            }

            this.comboBoxViqCode.SelectedItem = code;


            //表示
            this.textBoxViqCodeDescription.Text = code.description;

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
                                      //this.singleLineComboViqCode,

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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inputdata">表示MsViqCode 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            //画面初期化
            this.InitDisplayControl();

            MsViqCode data = inputdata as MsViqCode;
            this.DispData(data);

            return true;
        }

        /// <summary>
        /// 選択データの取得
        /// </summary>
        /// <returns></returns>
        public MsViqCode GetSelectData()
        {
            MsViqCode ans = this.comboBoxViqCode.SelectedItem as MsViqCode;
            return ans;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViqCodeControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ComboBoxが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxViqCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選択データの取得
            MsViqCode code = this.GetSelectData();
            this.DispData(code);

            //通知
            if (this.DataSelecDelegatetProc != null)
            {
                this.DataSelecDelegatetProc(code);
            }
        }
    }
}
