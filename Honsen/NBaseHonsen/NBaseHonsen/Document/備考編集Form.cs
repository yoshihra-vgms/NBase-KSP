using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseHonsen.Document.BLC;
using SyncClient;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseHonsen.Document
{
    public partial class 備考編集Form : Form
    {
        private string DIALOG_TITLE = "備考編集";

        private DmKanriKiroku dmKanriKiroku = null;
        private DmKoubunshoKisoku dmKoubunshoKisoku = null;

        public string Get備考
        {
            get
            {
                if (dmKanriKiroku != null)
                {
                    return dmKanriKiroku.Bikou;
                }
                else if (dmKoubunshoKisoku != null)
                {
                    return dmKoubunshoKisoku.Bikou;
                }
                else
                {
                    return null;
                }
            }
        }

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 備考編集Form(NBaseData.DS.DocConstants.LinkSakiEnum linkSaki, string id)
        {
            InitializeComponent();

            if (linkSaki == NBaseData.DS.DocConstants.LinkSakiEnum.管理記録)
            {
                dmKanriKiroku = DmKanriKiroku.GetRecord(同期Client.LOGIN_USER, id);
                dmKoubunshoKisoku = null;
            }
            else
            {
                dmKoubunshoKisoku = DmKoubunshoKisoku.GetRecord(同期Client.LOGIN_USER, id);
                dmKanriKiroku = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 備考編集Form_Load(object sender, EventArgs e)
        private void 備考編集Form_Load(object sender, EventArgs e)
        {
            if (dmKanriKiroku != null)
            {
                textBox_Bikou.Text = dmKanriKiroku.Bikou;
            }
            else
            {
                textBox_Bikou.Text = dmKoubunshoKisoku.Bikou;
            }
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
        private void button_登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            FillInstance();
            bool ret = false;
            if (dmKanriKiroku != null)
            {
                ret = 管理記録処理.Honsen更新(同期Client.LOGIN_USER, dmKanriKiroku);
            }
            else
            {
                ret = 公文書_規則処理.Honsen更新(同期Client.LOGIN_USER, dmKoubunshoKisoku);
            }

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.ChangeFlag = false;

        }
        #endregion

        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            return true;
        }
        #endregion

        /// <summary>
        /// 情報をセットする
        /// </summary>  
        #region private void FillInstance()
        private void FillInstance()
        {
            if (dmKanriKiroku != null)
            {
                //dmKanriKiroku.Bikou = textBox_Bikou.Text;
                dmKanriKiroku.Bikou = StringUtils.Escape(textBox_Bikou.Text);
            }
            else
            {
                //dmKoubunshoKisoku.Bikou = textBox_Bikou.Text;
                dmKoubunshoKisoku.Bikou = StringUtils.Escape(textBox_Bikou.Text);
            }
        }
        #endregion
    }
}
