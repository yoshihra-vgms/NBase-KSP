using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WtmData;
using WtmModelBase;

namespace WTM
{
    public partial class コピーForm : Form
    {
        private Work Work;
        private List<MsSenin> SeninViewList = null;
        private List<SiCard> Cards = null;
        Dictionary<int, int> CrewIndexDic = null;

        private bool CloseFlag;

        int panelMessageHeight = 0;
        private class ListItem
        {
            public string Rank { set; get; }
            public string Name { set; get; }

            public ListItem(string rank, string name)
            {
                Rank = rank;
                Name = name;
            }
        }


        public コピーForm(Work Work)
        {
            InitializeComponent();

            this.Work = Work;
        }

        private void コピーForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            // 終了メッセージ非表示
            panelMessageHeight = panel_Message.Height;
            panel_Message.Visible = false;

            // 船員、乗船者検索
            GetSenin();

            var crew = NBaseCommon.Common.SeninList.Where(o => o.MsSeninID.ToString() == Work.CrewNo).FirstOrDefault();
            label_SrcCrew.Text = $"コピー元：{crew.FullName}";
            label_SrcWorkRange.Text = $"作業日:{Work.StartWork.ToString("yyyy年MM月dd日 HH:mm")} ～ {Work.FinishWork.ToString("HH:mm")}";


            CrewIndexDic = new Dictionary<int, int>();
            var items = new List<ListItem>();
            foreach(var s in SeninViewList)
            {
                CrewIndexDic.Add(items.Count, s.MsSeninID);
                items.Add(new ListItem(SeninTableCache.instance().GetMsSiShokumeiNameAbbr(Common.LoginUser, s.MsSiShokumeiID), s.FullName));
            }
            c1List1.DataSource = items;

            c1List1.Columns[0].Caption = "職名";
            c1List1.Columns[1].Caption = "氏名";

            //行リサイズ禁止
            c1List1.AllowRowSizing = C1.Win.C1List.RowSizingEnum.None;

            SetLabelYear(label_StartYear, DateTime.Today);
            SetLabelYear(label_FinishYear, DateTime.Today);
            SetLabelDay(label_StartDay, DateTime.Today);
            SetLabelDay(label_FinishDay, DateTime.Today);


            // 職位フィルタ
            groupBoxRankCategory.Visible = WtmCommon.FlgShowRankCategory;
            if (WtmCommon.FlgShowRankCategory)
            {
                foreach (var rc in WtmCommon.RankCategoryList)
                {
                    comboBoxRankCategory.Items.Add(rc);
                }
                comboBoxRankCategory.SelectedIndex = 0;
            }

            CloseFlag = false;

        }


        /// <summary>
        /// 船員、乗船者取得
        /// </summary>
        #region private void GetSenin()
        private void GetSenin()
        {
            WtmModelBase.Role role = null;
            //if (WtmCommon.VesselMode && Common.Senin != null)
            //{
            //    role = WtmCommon.RoleList.Where(o => o.Rank == Common.Senin.MsSiShokumeiID.ToString()).FirstOrDefault();
            //}
            if (WtmCommon.VesselMode && NBaseCommon.Common.siCard != null)
            {
                string shokumeiId = "";
                if (NBaseCommon.Common.siCard.SiLinkShokumeiCards != null && NBaseCommon.Common.siCard.SiLinkShokumeiCards.Count > 0)
                {
                    shokumeiId = NBaseCommon.Common.siCard.SiLinkShokumeiCards[0].MsSiShokumeiID.ToString();
                }
                role = WtmCommon.RoleList.Where(o => o.Rank == shokumeiId).FirstOrDefault();
            }

            SeninViewList = new List<MsSenin>();

            // 乗船者検索
            Cards = Common.GetOnSigner(int.Parse(Work.VesselID), DateTime.Today, DateTime.Today);

            // 乗船職に置き換える
            foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
            {
                var targetCards = Cards.Where(o => o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);
                var onCrewId = targetCards.Select(o => o.MsSeninID).Distinct();

                var senins = NBaseCommon.Common.SeninList.Where(o => onCrewId.Contains(o.MsSeninID));
                if (senins != null)
                {
                    foreach (MsSenin senin in senins)
                    {
                        //if (WtmCommon.VesselMode)
                        if (WtmCommon.VesselMode && Common.Senin != null)
                        {
                            if (senin.MsSeninID != Common.Senin.MsSeninID)
                            {
                                if (role == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (role.RankList.Any(o => o == shokumei.MsSiShokumeiID) == false)
                                        continue;
                                }
                            }
                        }

                        senin.MsSiShokumeiID = shokumei.MsSiShokumeiID;

                        if (SeninViewList.Any(o => o.MsSeninID == senin.MsSeninID) == false)
                            SeninViewList.Add(senin);
                    }

                }
            }
        }
        #endregion



        /// <summary>
        /// 「コピー」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonCopy_Click(object sender, EventArgs e)
        private void buttonCopy_Click(object sender, EventArgs e)
        {    
            // 開始日、終了日
            DateTime fromDay = monthCalendar1.SelectionStart;
            DateTime toDay = monthCalendar2.SelectionStart;
            if (fromDay > toDay)
            {
                MessageVisible("日付を正しく選択してください", Color.Red);
                return;
            }

            // コピー先船員
            var selectedIndexes = c1List1.SelectedIndices;
            if (selectedIndexes.Count == 0)
            {
                MessageVisible("船員を選択してください", Color.Red);
                return;
            }

            List<int> seninIds = new List<int>();
            foreach(var index in selectedIndexes)
            {
                var seninId = CrewIndexDic[(int)index];
                seninIds.Add(seninId);
            }

            // コピーできるかの確認
            string msg = "";
            foreach (int seninId in seninIds)
            {
                for (DateTime day = fromDay; day <= toDay; day = day.AddDays(1))
                {
                    var isApproval = ApprovalCheck(day, seninId);
                    if (isApproval)
                    {
                        var senin = SeninViewList.Where(o => o.MsSeninID == seninId).FirstOrDefault();

                        if (msg.Length > 0)
                            msg += System.Environment.NewLine;
                        msg += $" {senin.FullName}さん";

                        break;
                    }
                }
            }
            if (msg != "")
            {
                msg = "下記船員は" + System.Environment.NewLine + "承認済の日もしくは月が含まれているため、" + System.Environment.NewLine + "登録できません。" + System.Environment.NewLine + System.Environment.NewLine + msg;
                MessageVisible(msg, Color.Red);
                return;
            }

            List<int> errorSeninIds = null;
            var ret = WtmAccessor.Instance().CanCopyWork(Work, seninIds, fromDay, toDay, out errorSeninIds);
            if (errorSeninIds.Count > 0)
            {
                msg = "";
                foreach (var seninId in errorSeninIds)
                {
                    var senin = SeninViewList.Where(o => o.MsSeninID == seninId).FirstOrDefault();

                    if (msg.Length > 0)
                        msg += System.Environment.NewLine;
                    msg += $"{senin.FullName}さんが重複しています";
                }
                MessageVisible(msg, Color.Red);
                return;
            }

            ret = WtmAccessor.Instance().CopyWork(Work, seninIds, fromDay, toDay);
            if (ret)
            {
                CloseFlag = true;

                MessageVisible("コピーしました", Color.Black);
            }
        }
        #endregion

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonCancel_Click(object sender, EventArgs e)
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion


        #region カレンダーイベント

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var d = monthCalendar1.SelectionStart;

            SetLabelYear(label_StartYear, d);
            SetLabelDay(label_StartDay, d);
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            var d = monthCalendar2.SelectionStart;

            SetLabelYear(label_FinishYear, d);
            SetLabelDay(label_FinishDay, d);
        }

        private void SetLabelYear(Label l, DateTime d)
        {
            l.Text = d.ToString("yyyy");
        }
        private void SetLabelDay(Label l, DateTime d)
        {
            l.Text = d.ToString("MM/dd(ddd)");
        }

        #endregion


        /// <summary>
        /// 「職位フィルタ」選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBoxRankCategory_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBoxRankCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBoxRankCategory.SelectedItem is RankCategory))
            {
                return;
            }

            var rc = (comboBoxRankCategory.SelectedItem as RankCategory);

            CrewIndexDic = new Dictionary<int, int>();
            var items = new List<ListItem>();
            foreach (var s in SeninViewList)
            {
                if (rc.RankList.Contains(s.MsSiShokumeiID))
                {
                    CrewIndexDic.Add(items.Count, s.MsSeninID);
                    items.Add(new ListItem(SeninTableCache.instance().GetMsSiShokumeiNameAbbr(Common.LoginUser, s.MsSiShokumeiID), s.FullName));
                }
                else if (Common.Senin != null && Common.Senin.MsSeninID == s.MsSeninID)
                {
                    CrewIndexDic.Add(items.Count, s.MsSeninID);
                    items.Add(new ListItem(SeninTableCache.instance().GetMsSiShokumeiNameAbbr(Common.LoginUser, s.MsSiShokumeiID), s.FullName));
                }
            }
            c1List1.DataSource = items;
        }
        #endregion

        /// <summary>
        /// メッセージパネル：「確認」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_確認_Click(object sender, EventArgs e)
        private void button_確認_Click(object sender, EventArgs e)
        {
            MessageInvisible();
        }
        #endregion

        /// <summary>
        /// メッセージパネル表示
        /// </summary>
        /// <param name="msg"></param>
        #region private void MessageVisible(string msg, Color color)
        private void MessageVisible(string msg, Color fgColor)
        {
            // メッセージをセット
            label_Message.ForeColor = fgColor;
            label_Message.Text = msg;

            // メッセージラベルの高さ調整
            var splitMsg = msg.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var height = panelMessageHeight;
            if (splitMsg.Length > 0)
            {
                height += 19 * splitMsg.Length;
            }
            panel_Message.Height = height;

            // メッセージ位置の調整
            var l = (panel_Message.Width - label_Message.Width) / 2;
            var t = (panel_Message.Height - label_Message.Height) / 2;
            label_Message.Location = new Point(l, 40);


            // メッセージパネル位置の調整
            l = (this.Width - panel_Message.Width) / 2;
            t = (this.Height - panel_Message.Height) / 2;
            panel_Message.Location = new Point(l, t);

            panel_Message.Visible = true;
            panel_Base.Enabled = false;
        }
        #endregion

        /// <summary>
        /// メッセージパネル非表示
        /// </summary>
        #region private void MessageInvisible()
        private void MessageInvisible()
        {
            panel_Base.Enabled = true;
            panel_Message.Visible = false;

            if (CloseFlag)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        #endregion


        /// <summary>
        /// 船員チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Check_Click(object sender, EventArgs e)
        {
            var text = button_Check.Text;
            if (string.IsNullOrEmpty(text))
            {
                button_Check.Text = "✓";
                for (int i = 0; i < c1List1.ListCount; i++)
                {
                    c1List1.SetSelected(i, true);
                }
            }
            else
            {
                button_Check.Text = "";
                for (int i = 0; i < c1List1.ListCount; i++)
                {
                    c1List1.SetSelected(i, false);
                }
            }
        }

        private bool ApprovalCheck(DateTime day, int crewNo)
        {
            bool ret = false;

            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                // 月次承認済みの確認
                if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(day)) != null)
                {
                    ret = true;
                }

                var startDayList = WtmAccessor.Instance().GetVesselApprovalDay(Common.Vessel.MsVesselID, day.Date);
                var vad = startDayList.Where(o => o.ApprovedCrewNo == crewNo.ToString()).FirstOrDefault();
                if (vad != null)
                {
                    ret = true;
                }
            }

            return ret;
        }

    }
}