using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Yojitsu.DA;

namespace Yojitsu.util
{
    class JisekiBuilder
    {
        private JisekiBuilder()
        {
        }


        public static Dictionary<int, BgJiseki> Build_年(List<BgJiseki> jisekis)
        {
            // <MsHimokuID, Total of BgJiseki>
            var himokuJisekiDic = new Dictionary<int, BgJiseki>();

            // <MsKamokuID, BgJiseki>
            var kamokuJisekiDic = BuildKamokuJisekiDictionary_年(jisekis);

            foreach (HimokuTreeNode n in HimokuTreeReader.GetHimokuTree())
            {
                foreach (MsKamoku k in n.MsHimoku.MsKamokus)
                {
                    if (kamokuJisekiDic.ContainsKey(k.MsKamokuId))
                    {
                        BgJiseki j;

                        if (!himokuJisekiDic.ContainsKey(n.MsHimoku.MsHimokuID))
                        {
                            j = new BgJiseki();

                            j.MsVesselID = kamokuJisekiDic[k.MsKamokuId].MsVesselID;
                            j.MsKamokuID = kamokuJisekiDic[k.MsKamokuId].MsKamokuID;
                            j.JisekiDate = kamokuJisekiDic[k.MsKamokuId].JisekiDate;
                            j.KikanNo = kamokuJisekiDic[k.MsKamokuId].KikanNo;

                            himokuJisekiDic[n.MsHimoku.MsHimokuID] = j;
                        }

                        j = himokuJisekiDic[n.MsHimoku.MsHimokuID];

                        if (k.Fugou == 1)
                        {
                            j.Amount += kamokuJisekiDic[k.MsKamokuId].Amount;
                            j.DollerAmount += kamokuJisekiDic[k.MsKamokuId].DollerAmount;
                            j.YenAmount += kamokuJisekiDic[k.MsKamokuId].YenAmount;
                        }
                        else
                        {
                            j.Amount -= kamokuJisekiDic[k.MsKamokuId].Amount;
                            j.DollerAmount -= kamokuJisekiDic[k.MsKamokuId].DollerAmount;
                            j.YenAmount -= kamokuJisekiDic[k.MsKamokuId].YenAmount;
                        }
                    }
                }
            }

            return himokuJisekiDic;
        }


        private static Dictionary<int, BgJiseki> BuildKamokuJisekiDictionary_年(List<BgJiseki> jisekis)
        {
            var kamokuJisekiDic = new Dictionary<int, BgJiseki>();

            foreach (BgJiseki j in jisekis)
            {
                kamokuJisekiDic[j.MsKamokuID] = j;
            }

            return kamokuJisekiDic;
        }


        public static Dictionary<int, Dictionary<string, BgJiseki>> Build_月(List<BgJiseki> jisekis)
        {
            // <MsHimokuID, <Nengetsu, Total of BgJiseki>>
            var himokuJisekiDic = new Dictionary<int, Dictionary<string, BgJiseki>>();

            // <MsKamokuID, <Nengetsu, BgJiseki>>
            var kamokuJisekiDic = BuildKamokuJisekiDictionary_月(jisekis);

            foreach (HimokuTreeNode n in HimokuTreeReader.GetHimokuTree())
            {
                foreach (MsKamoku k in n.MsHimoku.MsKamokus)
                {
                    if (kamokuJisekiDic.ContainsKey(k.MsKamokuId))
                    {
                        Dictionary<string, BgJiseki> monthJisekis = kamokuJisekiDic[k.MsKamokuId];

                        foreach (KeyValuePair<string, BgJiseki> pair in monthJisekis)
                        {
                            if (!himokuJisekiDic.ContainsKey(n.MsHimoku.MsHimokuID))
                            {
                                himokuJisekiDic[n.MsHimoku.MsHimokuID] = new Dictionary<string, BgJiseki>();
                            }

                            BgJiseki j;

                            if (!himokuJisekiDic[n.MsHimoku.MsHimokuID].ContainsKey(pair.Key))
                            {
                                j = new BgJiseki();

                                himokuJisekiDic[n.MsHimoku.MsHimokuID][pair.Key] = j;
                            }

                            j = himokuJisekiDic[n.MsHimoku.MsHimokuID][pair.Key];

                            if (k.Fugou == 1)
                            {
                                j.Amount += kamokuJisekiDic[k.MsKamokuId][pair.Key].Amount;
                                j.DollerAmount += kamokuJisekiDic[k.MsKamokuId][pair.Key].DollerAmount;
                                j.YenAmount += kamokuJisekiDic[k.MsKamokuId][pair.Key].YenAmount;
                            }
                            else
                            {
                                j.Amount -= kamokuJisekiDic[k.MsKamokuId][pair.Key].Amount;
                                j.DollerAmount -= kamokuJisekiDic[k.MsKamokuId][pair.Key].DollerAmount;
                                j.YenAmount -= kamokuJisekiDic[k.MsKamokuId][pair.Key].YenAmount;
                            }
                        }
                    }
                }
            }

            return himokuJisekiDic;
        }


        private static Dictionary<int, Dictionary<string, BgJiseki>> BuildKamokuJisekiDictionary_月(List<BgJiseki> jisekis)
        {
            var kamokuJisekiDic = new Dictionary<int, Dictionary<string, BgJiseki>>();

            foreach (BgJiseki j in jisekis)
            {
                if (!kamokuJisekiDic.ContainsKey(j.MsKamokuID))
                {
                    kamokuJisekiDic[j.MsKamokuID] = new Dictionary<string, BgJiseki>();
                }

                kamokuJisekiDic[j.MsKamokuID][j.JisekiDate] = j;
            }

            return kamokuJisekiDic;
        }
    }
}
