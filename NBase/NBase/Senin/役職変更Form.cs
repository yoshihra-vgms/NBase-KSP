using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using Senin.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin
{
    public partial class 役職変更Form : Form
    {
        private 船員詳細Panel parentPanel;
        private SiCard card;
        private SiCard newCard;
        private MsBasho 変更場所;

        public 役職変更Form(船員詳細Panel parentPanel, SiCard card)
        {
            this.parentPanel = parentPanel;
            this.card = card;
            this.newCard = new SiCard();

            InitializeComponent();
        }

        private void 役職変更Form_Load(object sender, EventArgs e)
        {
            InitComboBox職名();
            InitFields();
        }

        /// <summary>
        /// 「職名」DDLの初期化
        /// </summary>
        #region private void InitComboBox職名()
        private void InitComboBox職名()
        {
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }
        }
        #endregion

        private void InitFields()
        {
            {
                // 開始日は、１つ前の開始日＋１
                if (card.StartDate != DateTime.MinValue)
                {
                    nullableDateTimePicker開始.Value = card.StartDate.AddDays(1);
                }
                else
                {
                    nullableDateTimePicker開始.Value = null;
                }

                // 職名は、１つ前と同じ
                if (card.SiLinkShokumeiCards.Count == 0)
                {
                    comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, card.SeninMsSiShokumeiID);
                }
                else
                {
                    foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    {
                        comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, link.MsSiShokumeiID);
                    }
                }
            }
        }

        /// <summary>
        /// 「更新」クリック
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
        /// 「閉じる」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
        #endregion

        /// <summary>
        /// 保存処理
        /// （ＤＢへの実処理は親画面(船員詳細)の「Update」で実施）
        /// </summary>
        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();
                DialogResult = DialogResult.OK;

                if (parentPanel.InsertOrUpdate_乗船履歴(card, newCard))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //グリッド更新
                parentPanel.Refresh船員カード();

                Dispose();
            }
        }
        #endregion

        /// <summary>
        /// ヴァリデーション
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (nullableDateTimePicker開始.Value == null)
            {
                nullableDateTimePicker開始.BackColor = Color.Pink;
                MessageBox.Show("変更日を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始.BackColor = Color.White;
                return false;
            }

            if ((DateTime)(nullableDateTimePicker開始.Value) < card.StartDate)
            {
                nullableDateTimePicker開始.BackColor = Color.Pink;
                MessageBox.Show("変更日が開始日より前の日付です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始.BackColor = Color.White;
                return false;
            }

            //if (!Check_期間重複())
            //{
            //    nullableDateTimePicker開始.BackColor = Color.Pink;
            //    MessageBox.Show("入力された期間が他の船員カードの期間と重複しています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    nullableDateTimePicker開始.BackColor = Color.White;
            //    return false;
            //}

            if (comboBox職名.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }

            return true;
        }

        private bool Check_期間重複()
        {
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;

            if (nullableDateTimePicker開始.Value != null)
            {
                start = (DateTime)nullableDateTimePicker開始.Value;
            }
            return parentPanel.SiCard_期間重複チェック(newCard.SiCardID, SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船), newCard, start, end);
        }

        #endregion


        /// <summary>
        /// 画面データ→クラスにセット
        /// </summary>
        #region private void FillInstance()
        private void FillInstance()
        {
            {
                // 船員ID
                newCard.MsSeninID = card.MsSeninID;

                // 種別ID　<= "乗船"
                newCard.MsSiShubetsuID = card.MsSiShubetsuID;


                // 詳細種別ID
                newCard.MsSiShubetsuShousaiID = card.MsSiShubetsuShousaiID;
            }

            //========================================================
            // 入力情報
            //========================================================
            if (nullableDateTimePicker開始.Value is DateTime)
            {
                newCard.StartDate = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker開始.Value);
            }
            card.EndDate = newCard.StartDate.AddDays(-1);// 元のカードの下船日は、１日前とする

            newCard.EndDate = DateTime.MinValue;

            newCard.Days = int.Parse(StringUtils.ToStr(newCard.StartDate, DateTime.Now));

            MsSiShokumei shokumei = (comboBox職名.SelectedItem as MsSiShokumei);
            {
                SiLinkShokumeiCard link = new SiLinkShokumeiCard();
                link.MsSiShokumeiID = shokumei.MsSiShokumeiID;
                newCard.SiLinkShokumeiCards.Clear();
                newCard.SiLinkShokumeiCards.Add(link);

                newCard.CardMsSiShokumeiID = link.MsSiShokumeiID;
            }

            if (変更場所 != null)
            {
                card.SignOffBashoID = 変更場所.MsBashoId;

                newCard.SignOnBashoID = 変更場所.MsBashoId;
            }
            else
            {
                card.SignOffBashoID = null;

                newCard.SignOnBashoID = null;
            }


            // 船情報　→　元の値をコピー
            #region
            newCard.MsVesselID = card.MsVesselID;
            newCard.VesselName = card.VesselName;
            newCard.CompanyName = card.CompanyName;
            newCard.MsVesselTypeID = card.MsVesselTypeID;
            newCard.GrossTon = card.GrossTon;
            newCard.MsCrewMatrixTypeID = card.MsCrewMatrixTypeID;
            newCard.NavigationArea = card.NavigationArea;
            newCard.OwnerName = card.OwnerName;
            #endregion


            // 兼務通信長
            if (card.KenmTushincyo == 1)
            {
                DateTime s = card.KenmTushincyoStart;
                DateTime e = card.KenmTushincyoEnd;

                if (s < newCard.StartDate)
                {
                    if (e != DateTime.MinValue && e < newCard.StartDate)
                    {
                        // 役職変更前に兼務通信長の開始、終了があるので何もしない
                    }
                    else
                    {
                        // 役職変更前の兼務通信長の終了日に乗船終了日とする
                        card.KenmTushincyoEnd = card.EndDate;

                        // 役職変更後の兼務通信長の開始日は、乗船開始日とする
                        newCard.KenmTushincyo = 1;
                        newCard.KenmTushincyoStart = newCard.StartDate;
                    }
                }
                else
                {
                    // 役職変更前に兼務通信長の開始日が、役職変更後の開始日以降の場合
                    // 
                    // 役職変更前の兼務通信長の開始、終了を役職変更後にセット
                    newCard.KenmTushincyo = 1;
                    newCard.KenmTushincyoStart = s;
                    newCard.KenmTushincyoEnd = e;

                    // 役職変更前の兼務通信長の開始、終了はクリアする
                    card.KenmTushincyo = 0;
                    card.KenmTushincyoStart = DateTime.MinValue;
                    card.KenmTushincyoEnd = DateTime.MinValue;
                }
            }




        }

        #endregion

        private void button乗船場所_Click(object sender, EventArgs e)
        {
            場所検索Form form = new 場所検索Form();
            if (form.ShowDialog() == DialogResult.Yes)
            {
                変更場所 = form.Selected;
                form.Dispose();

                textBox乗船場所.Text = 変更場所.BashoName;
            }
        }
    }
}
