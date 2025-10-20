using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Senin.util;
using NBaseUtil;

namespace Senin
{
    partial class 船員詳細Panel
    {
        private List<SiCard> cards = new List<SiCard>();
        private TreeListViewDelegate船員カード treeListViewDelegate船員カード;

        // 新規追加されたカード.
        private Dictionary<string, SiCard> newCards = new Dictionary<string, SiCard>();
        // 編集・削除されたカード.
        private Dictionary<string, SiCard> editedCards = new Dictionary<string, SiCard>();


        private void button船員カード追加_Click(object sender, EventArgs e)
        {
            乗船履歴詳細Form form = new 乗船履歴詳細Form(this);
            form.ShowDialog();
        }


        private void treeListView船員カード_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                
                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView船員カード.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiCard c = selected.Tag as SiCard;

                    乗船履歴詳細Form form = new 乗船履歴詳細Form(this, c, false);
                    form.ShowDialog();
                }
            }
        }


        private void button船員カード検索_Click(object sender, EventArgs e)
        {
            Search船員カード();
        }
       
        
        private void Search船員カード()
        {
            DateTime start = dateTimePicker船員カード開始.Value;
            DateTime end = dateTimePicker船員カード終了.Value;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                SiCardFilter filter = new SiCardFilter();
                filter.MsSeninID = senin.MsSeninID;
                filter.Start = start;
                filter.End = end;

                cards = serviceClient.SiCard_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);

                cards = cards.OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.EndDate).ThenBy(obj => obj.MsSiShubetsuID).ToList();

                SearchInNewCards(start, end);
                SearchInEditedCards();
            }

            treeListViewDelegate船員カード.SetRows(cards);
        }

        //2021/07/29 m.yoshihara 追加 
        internal void Refresh船員カード()
        {
            Search船員カード();
        }

        internal void Clear船員カード()
        {
            cards.Clear();
            newCards.Clear();
            editedCards.Clear();
            if (treeListViewDelegate船員カード != null)
                treeListViewDelegate船員カード.SetRows(cards);
        }


        private void SearchInNewCards(DateTime start, DateTime end)
        {
            DateTime s = DateTimeUtils.ToFrom(start);
            DateTime e = DateTimeUtils.ToTo(end).AddSeconds(-1);

            foreach (SiCard c in newCards.Values)
            {
                if (s < c.EndDate && c.StartDate < e || c.EndDate == DateTime.MinValue && c.StartDate < e)
                {
                    cards.Add(c);
                }
            }
        }


        private void SearchInEditedCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (editedCards.ContainsKey(cards[i].SiCardID))
                {
                    cards[i] = editedCards[cards[i].SiCardID];
                }
            }
        }


        internal void AddSiCard(SiCard card, bool isNew)
        {
            if (isNew)
            {
                cards.Add(card);
                card.SiCardID = Guid.NewGuid().ToString();

                newCards[card.SiCardID] = card;
            }
            else if(!newCards.ContainsKey(card.SiCardID))
            {
                editedCards[card.SiCardID] = card;
            }

        }

        internal bool SiCard_期間重複チェック(string siCardId, int msSiShubetsuId, SiCard card, DateTime start, DateTime end)
        {
            //if (!期間重複チェック_In_未セーブ_Cards(newCards.Values, card, start, end, msSiShubetsuId) || 
            //      !期間重複チェック_In_未セーブ_Cards(editedCards.Values, card, start, end, msSiShubetsuId))
            //{
            //    return false;
            //}

            List<SiCard> cards_重複 = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                cards_重複 = serviceClient.SiCard_Get_期間重複(NBaseCommon.Common.LoginUser, senin.MsSeninID, siCardId, start, end);
            }

            foreach (SiCard c in cards_重複)
            {
                if (!editedCards.ContainsKey(c.SiCardID) && !Is_乗船と乗船休暇の重複(msSiShubetsuId, c))
                {
                    return false;
                }
            }

            return true;
        }


        private bool Is_乗船と乗船休暇の重複(int msSiShubetsuId, SiCard card_重複)
        {
            return (msSiShubetsuId == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇) &&
                     card_重複.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)) ||
                     (msSiShubetsuId == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船) &&
                     card_重複.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
        }


        //private bool 期間重複チェック_In_未セーブ_Cards(Dictionary<string, SiCard>.ValueCollection cards, SiCard card, DateTime start, DateTime end, int msSiShubetsuId)
        //{
        //    DateTime s = DateTimeUtils.ToFrom(start);
        //    DateTime e = DateTimeUtils.ToTo(end).AddSeconds(-1);

        //    foreach (SiCard c in cards)
        //    {
        //        if (c == card || c.DeleteFlag == 1 || SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, c.MsSiShubetsuID))
        //        {
        //            continue;
        //        }

        //        if (c.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船))
        //        {
        //            // 転船のカードは、チェック対象外とする
        //            continue;
        //        }

        //        if ((s < c.EndDate && c.StartDate < e || c.EndDate == DateTime.MinValue && c.StartDate < e) && !Is_乗船と乗船休暇の重複(msSiShubetsuId, c))
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
