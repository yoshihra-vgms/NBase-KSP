using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;

namespace Hachu.HachuManage
{
    public partial class 支払分割Form : Form
    {
        /// <summary>
        /// 対象支払の品目
        /// </summary>
        private List<Item支払品目> 支払品目s;

        /// <summary>
        /// 分割対象とマークされた品目
        /// </summary>
        public List<Item支払品目> 分割対象支払品目s;

        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView支払分割 品目TreeList;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="対象支払"></param>
        #region public 支払分割Form(List<Item支払品目> 支払品目s)
        public 支払分割Form(List<Item支払品目> 支払品目s)
        {
            this.支払品目s = 支払品目s;
            InitializeComponent();
        }
        #endregion

        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 支払分割Form_Load(object sender, EventArgs e)
        private void 支払分割Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「支払分割」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button支払分割_Click(object sender, EventArgs e)
        private void button支払分割_Click(object sender, EventArgs e)
        {
            分割対象支払品目s = 品目TreeList.GetCheckedNodes();
            if (分割対象支払品目s.Count == 0)
            {
                MessageBox.Show("分割対象となる仕様・型式/詳細品目がチェックされていません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (分割対象支払品目s.Count == 支払品目s.Count)
            {
                bool isAllCheck = true;
                for (int i = 0; i < 支払品目s.Count; i++)
                {
                    if (分割対象支払品目s[i].詳細品目s.Count != 支払品目s[i].詳細品目s.Count)
                    {
                        isAllCheck = false;
                        break;
                    }
                }
                if (isAllCheck)
                {
                    MessageBox.Show("すべての仕様・型式/詳細品目がチェックされています。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
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
            if (MessageBox.Show("支払分割を実施しません。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        /// <summary>
        /// 「すべて選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button選択_Click(object sender, EventArgs e)
        private void button選択_Click(object sender, EventArgs e)
        {
            品目TreeList.AllCheck();
        }
        #endregion

        /// <summary>
        /// 「すべて解除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button解除_Click(object sender, EventArgs e)
        private void button解除_Click(object sender, EventArgs e)
        {
            品目TreeList.AllUncheck();
        }
        #endregion


        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// 
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            this.Text = NBaseCommon.Common.WindowTitle("JM040902", "支払分割", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            InitItemTreeListView();
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」初期化
        /// </summary>
        #region private void InitItemTreeListView()
        private void InitItemTreeListView()
        {
            int noColumIndex = 1;
            bool viewHeader = true;
            object[,] columns = new object[,] {
                                               {"分割対象", 85, null, null},
                                               {"No", 35, null, HorizontalAlignment.Right},
                                               {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"数量", 50, null, HorizontalAlignment.Right},
                                               {"単価", 65, null, HorizontalAlignment.Right},
                                               {"金額", 80, null, HorizontalAlignment.Right},
                                               {"備考（品名、規格等）", 200, null, null}
                                             };
            品目TreeList = new ItemTreeListView支払分割(treeListView, true);
            品目TreeList.SetColumns(noColumIndex, columns);

            品目TreeList.AddNodes(viewHeader,支払品目s);
        }
        #endregion
    }
}
