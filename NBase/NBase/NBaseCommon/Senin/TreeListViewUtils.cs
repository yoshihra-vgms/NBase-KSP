using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseCommon.Senin
{
    public class TreeListViewUtils
    {
        private TreeListViewUtils()
        {
        }


        public static List<MsSeninRow> CreateRowData(List<MsSenin> src, MsUser loginUser, SeninTableCache seninTableCache)
        {
            return CreateRowData(src, loginUser, seninTableCache, false);
        }


        public static List<MsSeninRow> CreateRowData(List<MsSenin> src, MsUser loginUser, SeninTableCache seninTableCache, bool only_乗船可能)
        {
            List<MsSeninRow> dst = new List<MsSeninRow>();
            Dictionary<int, MsSeninRow> dic = new Dictionary<int, MsSeninRow>();

            foreach (MsSenin s in src)
            {
                if (only_乗船可能)
                {
                    if ((s.StartDate != DateTime.MinValue && s.EndDate == DateTime.MinValue) || DateTimeUtils.ToFrom(DateTime.Now) < DateTimeUtils.ToFrom(s.EndDate))
                    {
                        continue;
                    }
                }

                // 「乗船」レコードの開始日を直近の「乗船休暇」の乗船日に設定する.
                if (dic.ContainsKey(s.MsSeninID))
                {
                    dic[s.MsSeninID].乗船日 = s.StartDate.ToShortDateString();
                }
                else
                {
                    MsSeninRow row = new MsSeninRow(s);

                    dst.Add(row);
                    dic[s.MsSeninID] = row;

                    if (s.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                    {
                        dic[s.MsSeninID].乗船日 = s.StartDate.ToShortDateString();
                    }
                }
            }

            return dst;
        }

        public static List<MsSeninPlusRow> CreateRowData(List<MsSeninPlus> src, MsUser loginUser, SeninTableCache seninTableCache)
        {
            List<MsSeninPlusRow> dst = new List<MsSeninPlusRow>();
            Dictionary<int, MsSeninPlusRow> dic = new Dictionary<int, MsSeninPlusRow>();

            foreach (MsSeninPlus s in src)
            {
                //// 「乗船」レコードの開始日を直近の「乗船休暇」の乗船日に設定する.
                //if (dic.ContainsKey(s.Senin.MsSeninID))
                //{
                //    dic[s.Senin.MsSeninID].乗船日 = s.Senin.StartDate.ToShortDateString();
                //}
                //else
                //{
                    MsSeninPlusRow row = new MsSeninPlusRow(s);

                    dst.Add(row);
                    //dic[s.Senin.MsSeninID] = row;

                    //if (s.Senin.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                    //{
                    //    dic[s.Senin.MsSeninID].乗船日 = s.Senin.StartDate.ToShortDateString();
                    //}
                //}
            }

            return dst;
        }



        public class MsSeninRow
        {
            public MsSenin senin;
            public string 乗船日 = string.Empty;

            public MsSeninRow(MsSenin senin)
            {
                this.senin = senin;
            }
        }

        public class MsSeninPlusRow
        {
            public MsSeninPlus senin;
            public string 乗船日 = string.Empty;

            public MsSeninPlusRow(MsSeninPlus senin)
            {
                this.senin = senin;
            }
        }

        public static List<SiCardRow> CreateRowData(List<SiCard> src, MsUser loginUser, SeninTableCache seninTableCache)
        {
            List<SiCardRow> dst = new List<SiCardRow>();
            Dictionary<int, SiCardRow> dic = new Dictionary<int, SiCardRow>();

            src.Sort(new SeninTableCache.SiCardComparer());

            foreach (SiCard c in src)
            {
                // 「乗船」レコードの開始日を直近の「乗船休暇」の乗船日に設定する.
                if (dic.ContainsKey(c.MsSeninID))
                {
                    dic[c.MsSeninID].乗船日 = c.StartDate.ToShortDateString();
                }
                else
                {
                    SiCardRow row = new SiCardRow(c);

                    dst.Add(row);
                    dic[c.MsSeninID] = row;

                    if (seninTableCache.Is_乗船中(loginUser, c.MsSiShubetsuID))
                    {
                        dic[c.MsSeninID].乗船日 = c.StartDate.ToShortDateString();
                    }
                }
            }

            return dst;
        }


        public class SiCardRow
        {
            public SiCard card;
            public string 乗船日 = string.Empty;

            public SiCardRow(SiCard card)
            {
                this.card = card;
            }
        }
    }
}
