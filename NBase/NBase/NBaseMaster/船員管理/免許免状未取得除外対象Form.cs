using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseMaster.船員管理
{
    public partial class 免許免状未取得除外対象Form : Form
    {

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        private MsSiMenjou msSiMenjou = null;
        private MsSiMenjouKind msSiMenjouKind = null;
        private List<MsSiMenjouKind> msSiMenjouKindList = null;
        private List<int> msSiMenjouKindSelectedList = null;


        public 免許免状未取得除外対象Form(MsSiMenjou msSiMenjou, MsSiMenjouKind msSiMenjouKind, List<MsSiMenjouKind> msSiMenjouKindList, List<int> msSiMenjouKindSelectedList)
        {
            InitializeComponent();

            this.msSiMenjou = msSiMenjou;
            this.msSiMenjouKind = msSiMenjouKind;
            this.msSiMenjouKindList = msSiMenjouKindList;
            this.msSiMenjouKindSelectedList = msSiMenjouKindSelectedList;
        }

        private void 免許免状未取得除外対象Form_Load(object sender, EventArgs e)
        {
            textBox免許免状名.Text = msSiMenjou.Name;

            foreach (MsSiMenjouKind kind in msSiMenjouKindList)
            {
                if (msSiMenjouKindSelectedList.Contains(kind.MsSiMenjouKindID))
                {
                    checkedListBox.Items.Add(kind, true);
                }
                else
                {
                    checkedListBox.Items.Add(kind, false);
                }
            }

            this.ChangeFlag = false;
        }

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
        #endregion

        /// <summary>
        /// 「未取得除外対象」一覧のチェックボックスのチェック操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.ChangeFlag = true;

            MsSiMenjouKind kind = msSiMenjouKindList[e.Index];
            if (e.NewValue == CheckState.Unchecked)
            {
                msSiMenjouKindSelectedList.Remove(kind.MsSiMenjouKindID);
            }
            else if (msSiMenjouKindSelectedList.Contains(kind.MsSiMenjouKindID) == false)
            {
                msSiMenjouKindSelectedList.Add(kind.MsSiMenjouKindID);
            }
        }
        #endregion
    }
}
