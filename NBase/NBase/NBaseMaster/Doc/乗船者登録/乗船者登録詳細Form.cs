using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseUtil;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseMaster.Doc.乗船者登録
{
    public partial class 乗船者登録詳細Form : Form
    {
        private NBaseData.DAC.MsVessel msVessel = null;
        private int 乗船Id = -1;
        private NBaseData.DAC.MsUser msUser = null;
        private NBaseData.DAC.SiCard card;
        private bool isNew;

        public 乗船者登録詳細Form(NBaseData.DAC.MsVessel msVessel, int jyousenId)
            :this(msVessel, jyousenId, new NBaseData.DAC.SiCard(), true)
        {
        }

        public 乗船者登録詳細Form(NBaseData.DAC.MsVessel msVessel, int jyousenId, NBaseData.DAC.SiCard card, bool isNew)
        {
            this.msVessel = msVessel;
            this.乗船Id = jyousenId;
            this.card = card;
            this.isNew = isNew;

            InitializeComponent();
        }

        private void 乗船者登録詳細Form_Load(object sender, EventArgs e)
        {
            InitFields();
        }

        private void InitFields()
        {
            textBoxVessel.Text = msVessel.VesselName;
            InitComboBox職名();
            if (!isNew)
            {
                InitMsUser();
                textBox船員.Text = card.SeninName;

                foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                {
                    for (int i = 0; i < checkedListBox職名.Items.Count; i++)
                    {
                        if (((MsSiShokumei)checkedListBox職名.Items[i]).MsSiShokumeiID == link.MsSiShokumeiID)
                        {
                            checkedListBox職名.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
        }
        private void InitMsUser()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msUser = serviceClient.MsUser_GetRecordsByUserID(NBaseCommon.Common.LoginUser, card.MsUserID);
            }
        }

        private void InitComboBox職名()
        {
            foreach (NBaseData.DAC.MsSiShokumei s in NBaseData.DS.SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                checkedListBox職名.Items.Add(s);
            }
        }


        /// <summary>
        /// 「船員検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button船員検索_Click(object sender, EventArgs e)
        private void button船員検索_Click(object sender, EventArgs e)
        {
            船員検索Form form = new 船員検索Form();
            form.ShowDialog();

            if (form.user != null)
            {
                msUser = form.user;
                textBox船員.Text = msUser.Sei + " " + msUser.Mei;
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
            this.Close();
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
            if (ValidateFields())
            {
                FillInstance();

                bool ret = true;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_船員カード登録(NBaseCommon.Common.LoginUser, card);
                }
                if (ret)
                {
                    MessageBox.Show(this, "更新しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "更新に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
            //2015.11 コメント対応
            card.EndDate = DateTimeUtils.ToTo(DateTime.Today).AddDays(-1).AddSeconds(-1);

            card.Days = 日数計算();

            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_船員カード登録(NBaseCommon.Common.LoginUser, card);
            }
            if (ret)
            {
                MessageBox.Show(this, "更新しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(this, "更新に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (msUser == null)
            {
                textBox船員.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船員.BackColor = Color.White;
                return false;
            }

            if (checkedListBox職名.CheckedItems.Count == 0)
            {
                checkedListBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を1つ以上選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkedListBox職名.BackColor = Color.White;
                return false;
            }
            return true;
        }
        #endregion


        #region 内部処理メソッド

        private void FillInstance()
        {
            card.MsSiShubetsuID = 乗船Id;
            card.MsVesselID = (short)this.msVessel.MsVesselID;
            card.StartDate = DateTimeUtils.ToFrom(DateTime.Today);
            card.EndDate = DateTime.MinValue;

            Dictionary<int, SiLinkShokumeiCard> linkDic = CreateLinkDic();
            for (int i = 0; i < checkedListBox職名.Items.Count; i++)
            {
                MsSiShokumei s = checkedListBox職名.Items[i] as MsSiShokumei;

                if (checkedListBox職名.GetItemChecked(i))
                {
                    if (!linkDic.ContainsKey(s.MsSiShokumeiID))
                    {
                        SiLinkShokumeiCard link = new SiLinkShokumeiCard();

                        link.MsSiShokumeiID = s.MsSiShokumeiID;

                        card.SiLinkShokumeiCards.Add(link);
                    }
                }
                else
                {
                    if (linkDic.ContainsKey(s.MsSiShokumeiID))
                    {
                        SiLinkShokumeiCard link = linkDic[s.MsSiShokumeiID];

                        link.DeleteFlag = 1;
                    }
                }
            }

            card.MsUserID = this.msUser.MsUserID;
        }

        private Dictionary<int, SiLinkShokumeiCard> CreateLinkDic()
        {
            Dictionary<int, SiLinkShokumeiCard> linkDic = new Dictionary<int, SiLinkShokumeiCard>();

            foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
            {
                linkDic[link.MsSiShokumeiID] = link;
            }

            return linkDic;
        }

        private int 日数計算()
        {
            int days = 0;
            try
            {
                days = int.Parse(StringUtils.ToStr(card.StartDate, card.EndDate));
            }
            catch
            {}
            return days;
        }

        #endregion
    }
}
