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

using DeficiencyControl.Logic;
using DeficiencyControl.Util;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Controls.CommentItem
{
    /// <summary>
    /// DeficiencyCode選択コントロール
    /// </summary>
    public partial class DeficiencyCodeSelectControl : BaseControl
    {
        public DeficiencyCodeSelectControl()
        {
            InitializeComponent();
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
                                      this.singleLineComboDeficiencyCode,

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
        /// DeficiencyCodeが選択されたとき
        /// </summary>
        /// <param name="code">選択したもの クリア=null</param>
        private void ChangeDeficiencyCode(MsDeficiencyCode code)
        {
            //ないならクリア
            if (code == null)
            {
                this.singleLineComboDeficiencyCode.Text = "";
                this.singleLineComboDeficiencyCodeText.Text = "";
                return;
            }

            //値の設定
            this.singleLineComboDeficiencyCode.Text = code.deficiency_code_name;
            this.singleLineComboDeficiencyCodeText.Text = code.defective_item;

        }



        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="dcode">表示データ null=clear</param>
        public void DispData(MsDeficiencyCode dcode)
        {
            this.ChangeDeficiencyCode(dcode);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inputdata">初期表示MsDeficiencyCode</param>
        public override bool InitControl(object inputdata = null)
        {
            //指摘事項コード
            ControlItemCreator.CreateDeficiencyCode(this.singleLineComboDeficiencyCode);

            //指摘事項コードテキスト
            ControlItemCreator.CreateDeficiencyCodeText(this.singleLineComboDeficiencyCodeText);


            //初期表示
            MsDeficiencyCode code = inputdata as MsDeficiencyCode;
            if (code != null)
            {
                this.ChangeDeficiencyCode(code);
            }

            return true;
        }

        


        /// <summary>
        /// データの取得
        /// </summary>
        /// <returns></returns>
        public MsDeficiencyCode GetSelectData()
        {
            MsDeficiencyCode ans = this.singleLineComboDeficiencyCode.SelectedItem as MsDeficiencyCode;
            return ans;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeficiencyCodeSelectControl_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// DeficiencyCode Textが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboDeficiencyCodeText_selected(object sender, EventArgs e)
        {
            DeficiencyCodeText code = this.singleLineComboDeficiencyCodeText.SelectedItem as DeficiencyCodeText;
            if (code != null)
            {
                this.ChangeDeficiencyCode(code.DeficienyCode);
            }
        }

        /// <summary>
        /// DeficiencyCode Codeが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void singleLineComboDeficiencyCode_selected(object sender, EventArgs e)
        {
            MsDeficiencyCode code = this.singleLineComboDeficiencyCode.SelectedItem as MsDeficiencyCode;
            this.ChangeDeficiencyCode(code);
        }


        /// <summary>
        /// クリアされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleLineComboDeficiencyCode_Cleared(object sender, EventArgs e)
        {
            this.ChangeDeficiencyCode(null);
        }
    }
}
