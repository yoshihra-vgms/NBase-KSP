using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseMaster.船員管理
{
    public partial class 免許免状種別管理詳細Form : Form
    {
        private MsSiMenjouKind menjouKind;

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        private List<MsSiMenjouKind> msSiMenjouKindList = null;
        private List<int> msSiMenjouKindSelectedList = null;



        public 免許免状種別管理詳細Form(List<MsSiMenjou> menjous) : this(new MsSiMenjouKind(), menjous)
        {
        }


        public 免許免状種別管理詳細Form(MsSiMenjouKind menjou, List<MsSiMenjou> menjous)
        {
            this.menjouKind = menjou;

            InitializeComponent();
            Init(menjous);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="menjous"></param>
        #region private void Init(List<MsSiMenjou> menjous)
        private void Init(List<MsSiMenjou> menjous)
        {
            InitComboBox_免許免状名(menjous);

            if (!menjouKind.IsNew())
            {
                foreach (MsSiMenjou m in menjous)
                {
                    if (m.MsSiMenjouID == menjouKind.MsSiMenjouID)
                    {
                        comboBox免許免状名.SelectedItem = m;
                        break;
                    }
                }
                
                textBox種別名.Text = menjouKind.Name;
                textBox略称.Text = menjouKind.NameAbbr;
                textBox表示順序.Text = menjouKind.ShowOrder.ToString();

                // 2013.02 2013年度改造
                // 未取得除外対象を一覧
                foreach (MsSiMenjouKind kind in msSiMenjouKindList)
                {
                    foreach (MsSiExcludeMenjouKind obj in menjouKind.ExcludeMenjouKinds)
                    {
                        if (obj.ExcludeMenjouKindID == kind.MsSiMenjouKindID)
                        {
                            msSiMenjouKindSelectedList.Add(kind.MsSiMenjouKindID);
                            listBox未取得除外対象.Items.Add(kind);
                            break;
                        }
                    }
                }
            }
            else
            {
                button削除.Enabled = false;
            }

            this.ChangeFlag = false;
        }
        #endregion

        /// <summary>
        /// 「免許/免状名」DDL初期化
        /// </summary>
        /// <param name="menjous"></param>
        #region private void InitComboBox_免許免状名(List<MsSiMenjou> menjous)
        private void InitComboBox_免許免状名(List<MsSiMenjou> menjous)
        {
            comboBox免許免状名.Items.Add(string.Empty);

            foreach (MsSiMenjou m in menjous)
            {
                comboBox免許免状名.Items.Add(m);
            }

            comboBox免許免状名.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
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
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {

                //削除チェック
                bool result = this.CheckDeleteUsing(this.menjouKind);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                menjouKind.DeleteFlag = 1;
                Save();
            }
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
    　　/// 登録処理
        /// </summary>
        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// バリデーション
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (comboBox免許免状名.SelectedIndex == 0)
            {
                comboBox免許免状名.BackColor = Color.Pink;
                MessageBox.Show("免許／免状名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox免許免状名.BackColor = Color.White;
                return false;
            }
            else if (textBox種別名.Text.Length == 0)
            {
                textBox種別名.BackColor = Color.Pink;
                MessageBox.Show("種別名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox種別名.BackColor = Color.White;
                return false;
            }
            else if (textBox種別名.Text.Length > 20)
            {
                textBox種別名.BackColor = Color.Pink;
                MessageBox.Show("種別名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox種別名.BackColor = Color.White;
                return false;
            }
            else if (textBox略称.Text.Length > 20)
            {
                textBox略称.BackColor = Color.Pink;
                MessageBox.Show("略称は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox略称.BackColor = Color.White;
                return false;
            }
            else if (!NumberUtils.Validate(textBox表示順序.Text) || textBox表示順序.Text.Length > 3)
            {
                textBox表示順序.BackColor = Color.Pink;
                MessageBox.Show("表示順序は半角数字3文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox表示順序.BackColor = Color.White;
                return false;
            }

            return true;
        }
        #endregion
        

        /// <summary>
        /// 入力値→クラスへセット
        /// </summary>
        private void FillInstance()
        {
            menjouKind.MsSiMenjouID = (comboBox免許免状名.SelectedItem as MsSiMenjou).MsSiMenjouID;
            menjouKind.Name = textBox種別名.Text;
            menjouKind.NameAbbr = textBox略称.Text;
            menjouKind.ShowOrder = int.Parse(textBox表示順序.Text);
        }

        /// <summary>
        /// サーバ処理コール
        /// </summary>
        /// <returns></returns>
        #region private bool InsertOrUpdate()
        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiMenjouKind_InsertOrUpdate(NBaseCommon.Common.LoginUser, menjouKind);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 削除しようチェック
        /// 引数：データ
        /// 返り値：true削除可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        #region private bool CheckDeleteUsing(MsSiMenjouKind data)
        private bool CheckDeleteUsing(MsSiMenjouKind data)
        {
            //SI_MENJOU

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region SI_MENJOU
                List<SiMenjou> menlist =
                    serviceClient.SiMenjou_GetRecordsByMsSiMenjouKindID(NBaseCommon.Common.LoginUser, data.MsSiMenjouKindID);

                //一個以上ある＝使ってる
                if (menlist.Count > 0)
                {
                    return false;
                }
                   
                #endregion
            }

            return true;
        }
        #endregion

        /// <summary>
        /// データを編集した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void DataChange(object sender, EventArgs e)
        private void DataChange(object sender, EventArgs e)
        {
            //this.ChangeFlag = true;
            DataChange();
        }
        #endregion
        #region private void DataChange()
        private void DataChange()
        {
            this.ChangeFlag = true;
        }
        #endregion


        // 2014.02 2013年度改造
        /// <summary>
        /// 「免許/免状名」DDL選択時の処理
        ///  ・未取得除外対象をリセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox免許免状名_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox免許免状名_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataChange();

            listBox未取得除外対象.Items.Clear();

            msSiMenjouKindList = null;
            msSiMenjouKindSelectedList = null;


            if (comboBox免許免状名.SelectedItem is MsSiMenjou)
            {
                MsSiMenjou menjou = (comboBox免許免状名.SelectedItem as MsSiMenjou);
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    msSiMenjouKindList = serviceClient.MsSiMenjouKind_SearchRecords(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouID, null, null);

                    foreach (MsSiMenjouKind kind in msSiMenjouKindList)
                    {
                        if (kind.MsSiMenjouKindID == menjouKind.MsSiMenjouKindID)
                        {
                            msSiMenjouKindList.Remove(kind); // 自分自身なら選択対象としない
                            break;
                        }
                    }
                }
                msSiMenjouKindSelectedList = new List<int>();
            }

        }
        #endregion

        /// <summary>
        /// 「参照」ボタンクリック
        /// ・未取得除外対象を選択・決定する画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button未取得除外対象参照_Click(object sender, EventArgs e)
        {
            // 選択されている「免許/免状」に種別がない場合、何もしない
            if (msSiMenjouKindList == null)
                return;

            // 「未取得除外対象」画面を開く
            免許免状未取得除外対象Form form = new 免許免状未取得除外対象Form((comboBox免許免状名.SelectedItem as MsSiMenjou), menjouKind, msSiMenjouKindList, msSiMenjouKindSelectedList);

            if (form.ShowDialog() == DialogResult.Cancel)
                return;

            // 以下、「未取得除外対象」画面で、「更新」クリックされていたら
            DataChange();

            // 選択されたもので、画面表示、内部リストを更新する
            listBox未取得除外対象.Items.Clear();
            foreach (MsSiExcludeMenjouKind obj in menjouKind.ExcludeMenjouKinds)
            {
                obj.DeleteFlag = 1; // 一旦、すべて削除としておく
            }
            foreach (MsSiMenjouKind kind in msSiMenjouKindList)
            {
                if (msSiMenjouKindSelectedList.Contains(kind.MsSiMenjouKindID))
                {
                    listBox未取得除外対象.Items.Add(kind);

                    bool check = false;
                    foreach (MsSiExcludeMenjouKind obj in menjouKind.ExcludeMenjouKinds)
                    {
                        if (obj.ExcludeMenjouKindID == kind.MsSiMenjouKindID)
                        {
                            obj.DeleteFlag = 0; // いかす
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        MsSiExcludeMenjouKind obj = new MsSiExcludeMenjouKind();
                        obj.NewFlag = true;
                        obj.MsSiMenjouKindID = menjouKind.MsSiMenjouKindID;
                        obj.ExcludeMenjouKindID = kind.MsSiMenjouKindID;
                        menjouKind.ExcludeMenjouKinds.Add(obj);
                    }
                }
            }
        }
    }
}
