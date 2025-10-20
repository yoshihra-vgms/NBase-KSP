using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using WingData.DAC;
using Hachu.Models;
using Hachu.Utils;

namespace Hachu.HachuManage
{
    public partial class 品目追加変更Form : Form
    {
        // 新規 or 変更
        public enum EDIT_MODE { 新規, 変更 }
        private int EditMode;

        // 手配依頼種別ID
        private string ThiIraiSbtID;

        // 船ID
        private int MsVesselID;

        // 対象となる品目
        private Item手配依頼品目 対象品目;

        // 小修理品目マスタ（フリーメンテナンス）
        private Dictionary<string, string> MsShoushuriItemDic = null;

        //
        ItemTreeListView品目追加変更 詳細品目TreeList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="thiIraiSbtID"></param>
        /// <param name="vesselID"></param>
        /// <param name="editMode"></param>
        /// <param name="n"></param>
        #region public 品目追加変更Form(string thiIraiSbtID, int vesselID, int editMode, ref 手配品目 n)
        public 品目追加変更Form(string thiIraiSbtID, int vesselID, int editMode, ref Item手配依頼品目 n)
        {
            ThiIraiSbtID = thiIraiSbtID;
            MsVesselID = vesselID;
            EditMode = editMode;
            対象品目 = n;

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 品目追加変更Form_Load(object sender, EventArgs e)
        private void 品目追加変更Form_Load(object sender, EventArgs e)
        {
            this.Text = WingCommon.Common.WindowTitle("JM040202", "品目編集");
            
            List<MsItemSbt> MsItemSbts = null;
            List<MsShoushuriItem> MsShoushuriItems = null;
            using (WingServer.ServiceClient serviceClient = new WingServer.ServiceClient())
            {
                MsItemSbts = serviceClient.MsItemSbt_GetRecords(WingCommon.Common.LoginUser);
                MsShoushuriItems = serviceClient.MsShoushuriItem_GetRecordByMsVesselID(WingCommon.Common.LoginUser, MsVesselID);
            }

            // 区分ComboBox初期化
            comboBox区分.Items.Clear();
            foreach (MsItemSbt itemSbt in MsItemSbts)
            {
                comboBox区分.Items.Add(itemSbt);
            }

            // 品目ComboBox初期化
            // 小修理品目マスタ（フリーメンテナンス）から取得した品目をComboBoxにセットしておく
            MsShoushuriItemDic = new Dictionary<string, string>();
            foreach (MsShoushuriItem item in MsShoushuriItems)
            {
                comboBox品目.AutoCompleteCustomSource.Add(item.ItemName);
                if (MsShoushuriItemDic.ContainsKey(item.ItemName) == false)
                {
                    MsShoushuriItemDic.Add(item.ItemName, item.ItemName);
                }
            }

            InitTreeListView();
            if (EditMode == (int)EDIT_MODE.新規)
            {
                button削除.Enabled = false;
            }
            else
            {
                //textBoxヘッダ.Text = 
                foreach (MsItemSbt itemSbt in MsItemSbts)
                {
                    if (itemSbt.MsItemSbtID == 対象品目.品目.MsItemSbtID)
                    {
                        comboBox区分.SelectedItem = itemSbt;
                        break;
                    }
                }
                comboBox品目.Text = 対象品目.品目.ItemName;

                詳細品目TreeList.AddNodes(対象品目.詳細品目s);
            }
        }
        #endregion

        /// <summary>
        /// 「詳細品目一覧」の初期化
        /// </summary>
        #region private void InitTreeListView()
        private void InitTreeListView()
        {
            int noColumIndex = 0;
            object[,] columns = new object[,] {
                                               {"No", 40, null, null},
                                               {"詳細品目", 250, null, null},
                                               {"単位", 50, null, null},
                                               {"在庫数", 50, null, HorizontalAlignment.Right},
                                               {"依頼数", 50, null, HorizontalAlignment.Right},
                                               {"査定数", 50, null, HorizontalAlignment.Right},
                                               {"備考", 150, null, null}
                                             };

            詳細品目TreeList = new ItemTreeListView品目追加変更(treeListView);
            詳細品目TreeList.SetColumns(noColumIndex, columns);
        }
        #endregion

        /// <summary>
        /// 「詳細品目追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button詳細品目編集_Click(object sender, EventArgs e)
        private void button詳細品目編集_Click(object sender, EventArgs e)
        {
            // 詳細品目追加変更_手配Form を開く
            OdThiShousaiItem syousai = new OdThiShousaiItem();
            Form form = new 詳細品目追加変更_手配Form(ThiIraiSbtID, MsVesselID, (int)詳細品目追加変更_手配Form.EDIT_MODE.新規, ref syousai);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // ノードの追加
            対象品目.詳細品目s.Add(syousai);
            詳細品目TreeList.AddNode(syousai);
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button登録_Click(object sender, EventArgs e)
        private void button登録_Click(object sender, EventArgs e)
        {
            string errMessage = "";

            //　入力値の確認
            string header = textBoxヘッダ.Text;

            MsItemSbt itemSbt = null;
            if (comboBox区分.SelectedItem is MsItemSbt)
            {
                itemSbt = comboBox区分.SelectedItem as MsItemSbt;
            }
            string itemName = comboBox品目.Text;
            if (itemName == null || itemName.Length == 0)
            {
                errMessage += "・品目を入力してください。\n";
            }

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー");
                return;
            }

            // 品目クラスにデータをセットする
            if (EditMode == (int)EDIT_MODE.新規)
            {
                対象品目.品目.OdThiItemID = Hachu.Common.CommonDefine.新規ID();
            }
            else if (Hachu.Common.CommonDefine.Is新規(対象品目.品目.OdThiItemID) == false)
            {
                string id = Hachu.Common.CommonDefine.RemovePrefix(対象品目.品目.OdThiItemID);
                対象品目.品目.OdThiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
            }
            // header は？
            if (itemSbt != null && itemSbt.MsItemSbtID.Length > 0)
            {
                対象品目.品目.MsItemSbtID = itemSbt.MsItemSbtID;
                対象品目.品目.MsItemSbtName = itemSbt.ItemSbtName;
            }
            対象品目.品目.ItemName = itemName;
            if (MsShoushuriItemDic.ContainsKey(itemName) == false)
            {
                対象品目.品目.SaveDB = true;
            }

            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            // 削除の確認
            if (MessageBox.Show("この品目/詳細品目を削除します。よろしいですか？", "削除確認", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (Hachu.Common.CommonDefine.Is変更(対象品目.品目.OdThiItemID) == true)
            {
                対象品目.品目.OdThiItemID = Hachu.Common.CommonDefine.RemovePrefix(対象品目.品目.OdThiItemID);
            }
            対象品目.品目.OdThiItemID = Hachu.Common.CommonDefine.Prefix削除 + 対象品目.品目.OdThiItemID;

            // Formを閉じる
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
            // Formを閉じる
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        /// <summary>
        /// 「詳細品目一覧」ダブルクリック
        /// ・詳細品目の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void treeListView_DoubleClick(object sender, EventArgs e)
        private void treeListView_DoubleClick(object sender, EventArgs e)
        {
            詳細品目TreeList.DoubleClick(ThiIraiSbtID, MsVesselID, ref 対象品目.詳細品目s, ref 対象品目.削除詳細品目s);
        }
        #endregion
    }
}
