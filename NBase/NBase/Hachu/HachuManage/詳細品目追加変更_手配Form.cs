using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WingData.DAC;

namespace Hachu.HachuManage
{
    public partial class 詳細品目追加変更_手配Form : Form
    {
        // 新規 or 変更
        public enum EDIT_MODE { 新規, 変更 }
        private int EditMode;

        // 手配依頼種別ID
        private string ThiIraiSbtID;

        // 船ID
        private int MsVesselID;

        // 対象となる詳細品目
        private OdThiShousaiItem 対象詳細品目;

        // 小修理詳細品目マスタ（フリーメンテナンス）
        private Dictionary<string, string> MsSsShousaiItemDic = null;
        
        // 船用品ID
        private string MsVesselItemID = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="thiIraiSbtID"></param>
        /// <param name="vesselID"></param>
        /// <param name="editMode"></param>
        /// <param name="s"></param>
        #region public 詳細品目追加変更_手配Form(string thiIraiSbtID, int vesselID, int editMode, ref OdThiShousaiItem s)
        public 詳細品目追加変更_手配Form(string thiIraiSbtID, int vesselID, int editMode, ref OdThiShousaiItem s)
        {
            ThiIraiSbtID = thiIraiSbtID;
            MsVesselID = vesselID;
            EditMode = editMode;
            対象詳細品目 = s;

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 詳細品目追加変更_手配Form_Load(object sender, EventArgs e)
        private void 詳細品目追加変更_手配Form_Load(object sender, EventArgs e)
        {
            this.Text = WingCommon.Common.WindowTitle("JM040203", "詳細品目編集");
            
            List<MsTani> MsTanis = null;
            List<MsSsShousaiItem> MsSsShousaiItems = null;
            using (WingServer.ServiceClient serviceClient = new WingServer.ServiceClient())
            {
                MsTanis = serviceClient.MsTani_GetRecords(WingCommon.Common.LoginUser);
                if (ThiIraiSbtID != Hachu.Common.CommonDefine.MsThiIraiSbt_船用品ID)
                {
                    MsSsShousaiItems = serviceClient.MsSsShousaiItem_GetRecordByMsVesselID(WingCommon.Common.LoginUser, MsVesselID);
                }
            }

            // 単位
            MsTani dmy = new MsTani();
            dmy.MsTaniID = "";
            dmy.TaniName = "";
            comboBox単位.Items.Add(dmy);
            foreach (MsTani t in MsTanis)
            {
                comboBox単位.Items.Add(t);
            }

            // 船用品の場合、品目はマスタから選択
            // それ以外の場合、品目は入力
            if (ThiIraiSbtID == Hachu.Common.CommonDefine.MsThiIraiSbt_船用品ID)
            {
                panel品目選択.Location = new Point(74, 17);
                panel品目選択.Visible = true;
                panel品目入力.Visible = false;
            }
            else
            {
                panel品目選択.Visible = false;
                panel品目入力.Location = new Point(74, 17);
                panel品目入力.Visible = true;

                // 詳細品目ComboBox初期化
                // 小修理詳細品目マスタ（フリーメンテナンス）から取得した品目をComboBoxにセットしておく
                MsSsShousaiItemDic = new Dictionary<string, string>();
                foreach (MsSsShousaiItem item in MsSsShousaiItems)
                {
                    comboBox詳細品目.AutoCompleteCustomSource.Add(item.ShousaiItemName);
                    if (MsSsShousaiItemDic.ContainsKey(item.ShousaiItemName) == false)
                    {
                        MsSsShousaiItemDic.Add(item.ShousaiItemName, item.ShousaiItemName);
                    }
                }
            }

            // 新規に追加する場合、削除ボタンは、使用不可とする
            if (EditMode == (int)EDIT_MODE.新規)
            {
                button削除.Enabled = false;
            }
            else if (EditMode == (int)EDIT_MODE.変更)
            {
                if (ThiIraiSbtID == Hachu.Common.CommonDefine.MsThiIraiSbt_船用品ID)
                {
                    MsVesselItemID = 対象詳細品目.MsVesselItemID;
                    textBox詳細品目.Text = 対象詳細品目.MsVesselItemName;
                }
                else
                {
                    comboBox詳細品目.Text = 対象詳細品目.ShousaiItemName;
                }
                foreach (MsTani t in MsTanis)
                {
                    if (t.MsTaniID == 対象詳細品目.MsTaniID)
                    {
                        comboBox単位.SelectedItem = t;
                    }
                }
                if (対象詳細品目.ZaikoCount >= 0)
                {
                    textBox在庫数.Text = 対象詳細品目.ZaikoCount.ToString();
                }
                textBox依頼数.Text = 対象詳細品目.Count.ToString();
                textBox備考.Text = 対象詳細品目.Bikou;
            }

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
            string itemName = "";

            // 入力値の確認
            if (ThiIraiSbtID == Hachu.Common.CommonDefine.MsThiIraiSbt_船用品ID)
            {
                itemName = textBox詳細品目.Text;
                if (itemName == null || itemName.Length == 0)
                {
                    errMessage = "・詳細品目を選択してください。\n";
                }
            }
            else
            {
                itemName = comboBox詳細品目.Text;
                if (itemName == null || itemName.Length == 0)
                {
                    errMessage = "・詳細品目を入力してください。\n";
                }
            }
            MsTani tani = null;
            if (comboBox単位.SelectedItem is MsTani)
            {
                tani = comboBox単位.SelectedItem as MsTani;
            }
            if (tani == null)
            {
                errMessage += "・単位を選択してください。\n";
            }
            string zaiko = textBox在庫数.Text;
            // 在庫数は、NULLあり
            //if (zaiko == null || zaiko.Length == 0)
            //{
            //    errMessage += "・在庫数を入力してください。\n";
            //}
            string count = textBox依頼数.Text;
            if (count == null || count.Length == 0)
            {
                errMessage += "・依頼数を入力してください。\n";
            }
            string bikou = textBox備考.Text;

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー");
                return;
            }

            // 詳細品目クラスにデータをセットする
            if (EditMode == (int)EDIT_MODE.新規)
            {
                対象詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
            }
            else if (Hachu.Common.CommonDefine.Is新規(対象詳細品目.OdThiShousaiItemID) == false)
            {
                string id = Hachu.Common.CommonDefine.RemovePrefix(対象詳細品目.OdThiShousaiItemID);
                対象詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
            }
            if (ThiIraiSbtID == Hachu.Common.CommonDefine.MsThiIraiSbt_船用品ID)
            {
                対象詳細品目.MsVesselItemID = MsVesselItemID;
                対象詳細品目.MsVesselItemName = itemName;
                対象詳細品目.ShousaiItemName = itemName;
            }
            else
            {
                対象詳細品目.ShousaiItemName = itemName;
                if (MsSsShousaiItemDic.ContainsKey(itemName) == false)
                {
                    対象詳細品目.SaveDB = true;
                }
            }
            対象詳細品目.MsTaniID = tani.MsTaniID;
            対象詳細品目.MsTaniName = tani.TaniName;
            if (zaiko.Length > 0)
            {
                対象詳細品目.ZaikoCount = int.Parse(zaiko);
            }
            else
            {
                対象詳細品目.ZaikoCount = -1;
            }
            対象詳細品目.Count = int.Parse(count);
            対象詳細品目.Bikou = bikou;
            対象詳細品目.RenewUserID = WingCommon.Common.LoginUser.MsUserID;

            // 画面を閉じる
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
            if (MessageBox.Show("この詳細品目を削除します。よろしいですか？", "削除確認", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (Hachu.Common.CommonDefine.Is変更(対象詳細品目.OdThiShousaiItemID) == true)
            {
                対象詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(対象詳細品目.OdThiShousaiItemID);
            }
            対象詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.Prefix削除 + 対象詳細品目.OdThiShousaiItemID;

            // 画面を閉じる
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
            // 画面を閉じる
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        /// <summary>
        /// 「選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button選択_Click(object sender, EventArgs e)
        private void button選択_Click(object sender, EventArgs e)
        {
            詳細品目選択Form form = new 詳細品目選択Form(MsVesselID);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // 詳細品目選択Formから選択された品目を取得して
            // textBox詳細品目.text
            // に品目名を表示する
            MsVesselItemVessel vesselItemVessel = form.SelectedVesselItemVessel;
            textBox詳細品目.Text = vesselItemVessel.VesselItemName;
            MsVesselItemID = vesselItemVessel.MsVesselItemID;
        }
        #endregion
    }
}
