using ORMapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.BLC
{
    public class 船員検索
    {
        private static List<int> statusList = null;


        private static List<SiCard> AndSiCardList = null;
        private static List<SiCard> OrSiCardList = null;
        private static Dictionary<int, List<string>> AndSiCardDic = null;
        private static Dictionary<int, List<string>> OrSiCardDic = null;

        private static List<SiMenjou> AndSiMenjouList = null;
        private static List<SiMenjou> OrSiMenjouList = null;
        private static Dictionary<int, List<string>> AndSiMenjouDic = null;
        private static Dictionary<int, List<string>> OrSiMenjouDic = null;

        private static List<SiShobyo> AndSiShobyoList = null;
        private static List<SiShobyo> OrSiShobyoList = null;
        private static Dictionary<int, List<string>> AndSiShobyoDic = null;
        private static Dictionary<int, List<string>> OrSiShobyoDic = null;

        private static List<CrewMatrix> AndCrewMatrixList = null;
        private static List<CrewMatrix> OrCrewMatrixList = null;
        private static Dictionary<int, List<int>> AndCrewMatrixDic = null;
        private static Dictionary<int, List<int>> OrCrewMatrixDic = null;

        private static List<SiKenshin> AndSiKenshinList = null;
        private static List<SiKenshin> OrSiKenshinList = null;
        private static Dictionary<int, List<string>> AndSiKenshinDic = null;
        private static Dictionary<int, List<string>> OrSiKenshinDic = null;

        private static List<SiExperienceCargo> AndSiExperienceCargoList = null;
        private static List<SiExperienceCargo> OrSiExperienceCargoList = null;
        private static Dictionary<int, List<int>> AndSiExperienceCargoDic = null;
        private static Dictionary<int, List<int>> OrSiExperienceCargoDic = null;

        private static List<SiKoushu> AndSiKoushuList = null;
        private static List<SiKoushu> OrSiKoushuList = null;
        private static Dictionary<int, List<string>> AndSiKoushuDic = null;
        private static Dictionary<int, List<string>> OrSiKoushuDic = null;

        public static string 船員_自社名;

        static 船員検索()
        {
            try
            {
                船員_自社名 = System.Configuration.ConfigurationManager.AppSettings["船員_自社名"];
            }
            catch
            {
                船員_自社名 = "不明";
            }
        }


        public static List<MsSeninPlus> AdvancedSearch(MsUser loginUser, 
            SeninTableCache seninTableCache,
            SiAdvancedSearchFilter advancedSearchFilter,
            List<SiAdvancedSearchConditionItem> conditionItems, 
            List<SiAdvancedSearchConditionValue> conditionValues)
        {
            Dictionary<int, List<int>> andSeninDic = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> andExceptSeninDic = new Dictionary<int, List<int>>();

            Dictionary<int, List<int>> orSeninDic = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> orExceptSeninDic = new Dictionary<int, List<int>>();

            // SiCard情報
            AndSiCardList = new List<SiCard>();
            OrSiCardList = new List<SiCard>();
            AndSiCardDic = new Dictionary<int, List<string>>();
            OrSiCardDic = new Dictionary<int, List<string>>();

            // SiMenjou情報
            AndSiMenjouList = new List<SiMenjou>();
            OrSiMenjouList = new List<SiMenjou>();
            AndSiMenjouDic = new Dictionary<int, List<string>>();
            OrSiMenjouDic = new Dictionary<int, List<string>>();

            // SiShobyo情報
            AndSiShobyoList = new List<SiShobyo>();
            OrSiShobyoList = new List<SiShobyo>();
            AndSiShobyoDic = new Dictionary<int, List<string>>();
            OrSiShobyoDic = new Dictionary<int, List<string>>();

            // CrewMatrix情報
            AndCrewMatrixList = new List<CrewMatrix>();
            OrCrewMatrixList = new List<CrewMatrix>();
            AndCrewMatrixDic = new Dictionary<int, List<int>>();
            OrCrewMatrixDic = new Dictionary<int, List<int>>();

            // SiKenshin情報
            AndSiKenshinList = new List<SiKenshin>();
            OrSiKenshinList = new List<SiKenshin>();
            AndSiKenshinDic = new Dictionary<int, List<string>>();
            OrSiKenshinDic = new Dictionary<int, List<string>>();

            // SiExperienceCargo情報
            AndSiExperienceCargoList = new List<SiExperienceCargo>();
            OrSiExperienceCargoList = new List<SiExperienceCargo>();
            AndSiExperienceCargoDic = new Dictionary<int, List<int>>();
            OrSiExperienceCargoDic = new Dictionary<int, List<int>>();

            // SiKoushu情報
            AndSiKoushuList = new List<SiKoushu>();
            OrSiKoushuList = new List<SiKoushu>();
            AndSiKoushuDic = new Dictionary<int, List<string>>();
            OrSiKoushuDic = new Dictionary<int, List<string>>();




            statusList = null;


            //=====================================
            // 条件を、AND条件と、OR条件に分ける
            //=====================================
            var andConditions = conditionItems.Where(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND);
            var orConditions = conditionItems.Where(obj => obj.AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_OR);


            //=====================================
            // AND条件　１つずつ検索する
            //=====================================
            if (andConditions.Count() > 0)
            {
                var orderList = andConditions.Select(obj => obj.ShowOrder).Distinct();
                foreach(int order in orderList)
                {
                    Search(loginUser, seninTableCache, SiAdvancedSearchConditionItem.AND_OR_FLAG_AND, andSeninDic, andExceptSeninDic, andConditions.Where(obj => obj.ShowOrder == order).ToList(), conditionValues.Where(obj => obj.ShowOrder == order).ToList());
                }

            }

            //=====================================
            // OR条件　１つずつ検索する
            //=====================================
            if (orConditions.Count() > 0)
            {
                var orderList = orConditions.Select(obj => obj.ShowOrder).Distinct();
                foreach (int order in orderList)
                {
                    Search(loginUser, seninTableCache, SiAdvancedSearchConditionItem.AND_OR_FLAG_OR, orSeninDic, orExceptSeninDic, orConditions.Where(obj => obj.ShowOrder == order).ToList(), conditionValues.Where(obj => obj.ShowOrder == order).ToList());
                }

            }


            MsSeninFilter filter = new MsSeninFilter();

            // Statusの指定がない場合、すべてのStatusを対象とする
            if (statusList == null)
            {
                var values = seninTableCache.GetMsSiShubetsuList(loginUser).Select(obj => obj.MsSiShubetsuID);
                statusList = values.ToList();
                filter.種別無し = true;
            }

            filter.MsSiShubetsuIDs.AddRange(statusList);
            filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;
            filter.OrderByStr = "OrderByMsSiShokumeiIdNameE_2";

            filter.詳細検索 = true;

            List<MsSenin> senins = MsSenin.GetRecordsByFilter(loginUser, filter);


            var seninIds = from s in senins
                           select s.MsSeninID;

            Dictionary<int, Dictionary<int, int>> 合計日数Dic = 船員.BLC_船員合計日数(loginUser, seninTableCache, seninIds);
            foreach (MsSenin s in senins)
            {
                if (合計日数Dic.ContainsKey(s.MsSeninID))
                {
                    s.合計日数 = 合計日数Dic[s.MsSeninID];
                }
            }


            List<CrewMatrix> allCrewMatrix = null;
            if( andConditions.Any(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_CREW_MATRIX) || 
                orConditions.Any(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_CREW_MATRIX) )
            {
                allCrewMatrix = GetAllCrewMatrix(loginUser, seninTableCache);
            }


            var allSenin = senins.Select(obj => obj.MsSeninID);
Debug.WriteLine("seninInfos: " + allSenin.Count().ToString());
            List<string> allCardIds = new List<string>();
            List<string> allMenjouIds = new List<string>();
            List<string> allShobyoIds = new List<string>();
            List<string> allKenshinIds = new List<string>();
            List<string> allKoushuIds = new List<string>();


            //============================================
            // And条件
            //============================================
            foreach (var key in andSeninDic.Keys)
            {
                allSenin = allSenin.Intersect(andSeninDic[key]).ToList();　// 両方にあるものが抽出される
                Debug.WriteLine("and key: " + key.ToString() + " = " + andSeninDic[key].Count().ToString() + " => " + allSenin.Count().ToString());
            }
            if (AndSiCardList.Count() > 0)
            {
                //allCardIds = ChooseIds(AndSiCardDic);
                if (AndSiCardDic.ContainsKey(MsSiAdvancedSearchCondition.ID_DAYS))
                {
                    allCardIds = AndSiCardDic[MsSiAdvancedSearchCondition.ID_DAYS];
                }
                if (AndSiCardDic.ContainsKey(MsSiAdvancedSearchCondition.ID_VESSEL))
                {
                    if (allCardIds.Count() == 0)
                    {
                        allCardIds = AndSiCardDic[MsSiAdvancedSearchCondition.ID_VESSEL];
                    }
                    else
                    {
                        allCardIds = allCardIds.Intersect(AndSiCardDic[MsSiAdvancedSearchCondition.ID_VESSEL]).ToList();　// 両方にあるものが抽出される
                    }
                }

                List<string> tmp = new List<string>();
                if (AndSiCardDic.ContainsKey(MsSiAdvancedSearchCondition.ID_VESSEL_ALL))
                {
                    tmp = AndSiCardDic[MsSiAdvancedSearchCondition.ID_VESSEL_ALL];
                }
                allCardIds = allCardIds.Union(tmp).ToList();
            }
            if (AndSiMenjouList.Count() > 0)
            {
                allMenjouIds = ChooseIds(AndSiMenjouDic);
            }
            if (AndSiShobyoList.Count() > 0)
            {
                allShobyoIds = ChooseIds(AndSiShobyoDic);
            } 
            if (AndSiKenshinList.Count() > 0)
            {
                allKenshinIds = ChooseIds(AndSiKenshinDic);
            }
            if (AndSiKoushuList.Count() > 0)
            {
                allKoushuIds = ChooseIds(AndSiKoushuDic);
            }

            //============================================
            // NotExistsのものを除く
            //============================================
            foreach (var key in andExceptSeninDic.Keys)
            {
                allSenin = allSenin.Except(andExceptSeninDic[key]).ToList();
                Debug.WriteLine("except key: " + key.ToString() + " = " + andExceptSeninDic[key].Count().ToString() + " => " + allSenin.Count().ToString());
            }


            //============================================
            // Or条件
            //============================================
            if (andSeninDic.Keys.Count == 0 && orSeninDic.Keys.Count > 0)
            {
                allSenin = orSeninDic[orSeninDic.Keys.ToList()[0]].ToList();
            }
            foreach (var key in orSeninDic.Keys)
            {
                allSenin = allSenin.Union(orSeninDic[key]).ToList();
                Debug.WriteLine("or key: " + key.ToString() + " = " + orSeninDic[key].Count().ToString() + " => " + allSenin.Count().ToString());
            }
            if (OrSiCardList.Count() > 0)
            {
                List<string> orIds = ChooseIds(OrSiCardDic);
                allCardIds = allCardIds.Union(orIds).ToList();
            }
            if (OrSiKenshinList.Count() > 0)
            {
                List<string> orIds = ChooseIds(OrSiKenshinDic);
                allKenshinIds = allKenshinIds.Union(orIds).ToList();
            }
            if (OrSiShobyoList.Count() > 0)
            {
                List<string> orIds = ChooseIds(OrSiShobyoDic);
                allShobyoIds = allShobyoIds.Union(orIds).ToList();
            }
            if (OrSiMenjouList.Count() > 0)
            {
                List<string> orIds = ChooseIds(AndSiMenjouDic);
                allMenjouIds = allMenjouIds.Union(orIds).ToList();
            }
            if (OrSiKoushuList.Count() > 0)
            {
                List<string> orIds = ChooseIds(OrSiKoushuDic);
                allKoushuIds = allKoushuIds.Union(orIds).ToList();
            }

            //============================================
            // 全体からNotExistsのものを除いて　Orとする
            //============================================
            foreach (var key in orExceptSeninDic.Keys)
            {
                var tmpSenin = senins.Select(obj => obj.MsSeninID);
                tmpSenin = tmpSenin.Except(orExceptSeninDic[key]).ToList();　// 全体からNotExistsのものを除いて
                allSenin = allSenin.Union(tmpSenin).ToList();                // Orとして結合する

                Debug.WriteLine("except key: " + key.ToString() + " = " + orExceptSeninDic[key].Count().ToString() + " => " + allSenin.Count().ToString());
            }

Debug.WriteLine("allSenin: " + allSenin.Distinct().Count().ToString());
Debug.WriteLine("allCard: " + allCardIds.Distinct().Count().ToString());
Debug.WriteLine("allMenjou: " + allMenjouIds.Distinct().Count().ToString());
Debug.WriteLine("allShobyo: " + allShobyoIds.Distinct().Count().ToString());
Debug.WriteLine("allKenshin: " + allKenshinIds.Distinct().Count().ToString());
Debug.WriteLine("allKoushu: " + allKoushuIds.Distinct().Count().ToString());


            //List<MsSenin> resultSenins = senins.Where(obj => allSenin.Contains(obj.MsSeninID) && statusList.Contains(obj.MsSiShubetsuID)).ToList();
            List<MsSenin> resultSenins = null;
            if (filter.種別無し)
            {
                resultSenins = senins.Where(obj => allSenin.Contains(obj.MsSeninID)).ToList();
            }
            else
            {
                resultSenins = senins.Where(obj => allSenin.Contains(obj.MsSeninID) && statusList.Contains(obj.MsSiShubetsuID)).ToList();
            }
            Debug.WriteLine("resultSenins: " + resultSenins.Count().ToString());


            // MsSenin情報
            List<MsSeninPlus> retList = new List<MsSeninPlus>();
            List<MsSeninPlus> tmpList = null;
            foreach(MsSenin senin in resultSenins)
            {
                MsSeninPlus seninPlus = new MsSeninPlus();
                seninPlus.Senin = senin;
                retList.Add(seninPlus);
            }

            #region 各検索結果を結合する

            // SiCard情報
            #region
            if (AndSiCardList.Count() > 0 || OrSiCardList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    if (AndSiCardList.Count() > 0)
                    {
                        var seninSiCards = AndSiCardList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allCardIds.Contains(obj.SiCardID)));
                        if (seninSiCards.Count() > 0)
                        {
                            foreach (SiCard card in seninSiCards)
                            {
                                if (retList.Any(obj => (obj.Card != null && obj.Card.SiCardID == card.SiCardID)) == false)
                                {
                                    MsSeninPlus newSeninPlus = new MsSeninPlus();
                                    newSeninPlus.Senin = seninPlus.Senin;
                                    newSeninPlus.Card = card;

                                    retList.Add(newSeninPlus);
                                }
                            }
                        }
                    }
                    if (OrSiCardList.Count() > 0)
                    {
                        var seninSiCards = OrSiCardList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allCardIds.Contains(obj.SiCardID)));
                        if (seninSiCards.Count() > 0)
                        {
                            foreach (SiCard card in seninSiCards)
                            {
                                if (retList.Any(obj => (obj.Card != null && obj.Card.SiCardID == card.SiCardID)) == false)
                                {
                                    MsSeninPlus newSeninPlus = new MsSeninPlus();
                                    newSeninPlus.Senin = seninPlus.Senin;
                                    newSeninPlus.Card = card;

                                    retList.Add(newSeninPlus);
                                }
                            }
                        }
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        // SignOnOff 情報がない船員
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion

            // SiMenjou情報
            #region
            if (AndSiMenjouList.Count() > 0 || OrSiMenjouList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    bool added = false;
                    if (AndSiMenjouList.Count() > 0)
                    {
                        var seninSiMenjous = AndSiMenjouList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allMenjouIds.Contains(obj.SiMenjouID)));
                        if (seninSiMenjous.Count() > 0)
                        {
                            //foreach (SiMenjou menjou in seninSiMenjous)
                            //{
                            //    MsSeninPlus newSeninPlus = new MsSeninPlus();
                            //    newSeninPlus.Senin = seninPlus.Senin;
                            //    newSeninPlus.Menjou = menjou;

                            //    retList.Add(newSeninPlus);
                            //}

                            MsSeninPlus newSeninPlus = CopyMsSeninPlus(seninPlus);

                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)))
                                newSeninPlus.Menjou_K = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)))
                                newSeninPlus.Menjou_S = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)))
                                newSeninPlus.Menjou_M = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)).FirstOrDefault();

                            retList.Add(newSeninPlus);

                            added = true;
                        }
                    }
                    if (OrSiMenjouList.Count() > 0)
                    {
                        var seninSiMenjous = OrSiMenjouList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allMenjouIds.Contains(obj.SiMenjouID)));
                        if (seninSiMenjous.Count() > 0)
                        {
                            //foreach (SiMenjou menjou in seninSiMenjous)
                            //{
                            //    MsSeninPlus newSeninPlus = new MsSeninPlus();
                            //    newSeninPlus.Senin = seninPlus.Senin;
                            //    newSeninPlus.Menjou = menjou;

                            //    retList.Add(newSeninPlus);
                            //}

                            MsSeninPlus newSeninPlus = CopyMsSeninPlus(seninPlus);

                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)))
                                newSeninPlus.Menjou_K = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)))
                                newSeninPlus.Menjou_S = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)))
                                newSeninPlus.Menjou_M = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)).FirstOrDefault();

                            retList.Add(newSeninPlus);

                            added = true;
                        }
                    }

                    //if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    if (added == false)
                    {
                        retList.Add(seninPlus);
                    }
                }
            }
            else if (advancedSearchFilter.includeMenjou)
            {
                List<SiMenjou> list = SearchSiMenjou("SearchLicense", loginUser, null, new ParameterConnection());
                if (list.Count() > 0)
                {
                    tmpList = retList.ToList();
                    retList = new List<MsSeninPlus>();
                    foreach (MsSeninPlus seninPlus in tmpList)
                    {
                        var seninSiMenjous = list.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID));
                        if (seninSiMenjous.Count() > 0)
                        {
                            MsSeninPlus newSeninPlus = CopyMsSeninPlus(seninPlus);

                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)))
                                newSeninPlus.Menjou_K = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_海技免状ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)))
                                newSeninPlus.Menjou_S = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_船員手帳ID(loginUser)).FirstOrDefault();
                            if (seninSiMenjous.Any(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)))
                                newSeninPlus.Menjou_M = seninSiMenjous.Where(o => o.MsSiMenjouID == seninTableCache.MsSiMenjou_無線免許ID(loginUser)).FirstOrDefault();

                            retList.Add(newSeninPlus);
                        }
                        else
                        {
                            retList.Add(seninPlus);
                        }
                    }
                }

            }
            #endregion

            // SiShobyo情報
            #region
            if (AndSiShobyoList.Count() > 0 || OrSiShobyoList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    if (AndSiShobyoList.Count() > 0)
                    {
                        var seninSiShobyos = AndSiShobyoList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allShobyoIds.Contains(obj.SiShobyoID)));
                        if (seninSiShobyos.Count() > 0)
                        {
                            foreach (SiShobyo shobyo in seninSiShobyos)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Shobyo = shobyo;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }
                    if (OrSiShobyoList.Count() > 0)
                    {
                        var seninSiShobyos = OrSiShobyoList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allShobyoIds.Contains(obj.SiShobyoID)));
                        if (seninSiShobyos.Count() > 0)
                        {
                            foreach (SiShobyo shobyo in seninSiShobyos)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Shobyo = shobyo;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion

            // CrewMatrix情報
            #region
            if (allCrewMatrix != null && allCrewMatrix.Count > 0)
            {
                foreach (MsSeninPlus seninPlus in retList)
                {
                    var seninCrewMatrixs = allCrewMatrix.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID));
                    if (seninCrewMatrixs.Count() > 0)
                    {
                        seninPlus.CrewMatrixList = seninCrewMatrixs.ToList();
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion

            // SiKenshin情報
            #region
            if (AndSiKenshinList.Count() > 0 || OrSiKenshinList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    if (AndSiKenshinList.Count() > 0)
                    {
                        var seninSiKenshins = AndSiKenshinList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allKenshinIds.Contains(obj.SiKenshinID)));
                        if (seninSiKenshins.Count() > 0)
                        {
                            foreach (SiKenshin kenshin in seninSiKenshins)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Kenshin = kenshin;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }
                    if (OrSiKenshinList.Count() > 0)
                    {
                        var seninSiKenshins = OrSiKenshinList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allKenshinIds.Contains(obj.SiKenshinID)));
                        if (seninSiKenshins.Count() > 0)
                        {
                            foreach (SiKenshin kenshin in seninSiKenshins)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Kenshin = kenshin;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion

            // SiExperienceCargo情報
            #region
            if (AndSiExperienceCargoList.Count() > 0 || OrSiExperienceCargoList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    if (AndSiExperienceCargoList.Count() > 0)
                    {
                        var seninSiExperienceCargos = AndSiExperienceCargoList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID));
                        if (seninSiExperienceCargos.Count() > 0)
                        {
                            MsSeninPlus newSeninPlus = new MsSeninPlus();
                            newSeninPlus.Senin = seninPlus.Senin;
                            newSeninPlus.experienceCargoList = seninSiExperienceCargos.ToList();

                            retList.Add(newSeninPlus);
                        }
                    }
                    if (OrSiExperienceCargoList.Count() > 0)
                    {
                        var seninSiExperienceCargos = OrSiExperienceCargoList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID));
                        if (seninSiExperienceCargos.Count() > 0)
                        {
                            foreach (SiExperienceCargo expCargo in seninSiExperienceCargos)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.experienceCargoList = seninSiExperienceCargos.ToList();

                                retList.Add(newSeninPlus);
                            }
                        }
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion

            // SiKoushu情報
            #region
            if (AndSiKoushuList.Count() > 0 || OrSiKoushuList.Count() > 0)
            {
                tmpList = retList.ToList();
                retList = new List<MsSeninPlus>();
                foreach (MsSeninPlus seninPlus in tmpList)
                {
                    if (AndSiKoushuList.Count() > 0)
                    {
                        var seninSiKoushus = AndSiKoushuList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allKoushuIds.Contains(obj.SiKoushuID)));
                        if (seninSiKoushus.Count() > 0)
                        {
                            foreach (SiKoushu kyouiku in seninSiKoushus)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Koushu = kyouiku;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }
                    if (OrSiKoushuList.Count() > 0)
                    {
                        var seninSiKoushus = OrSiKoushuList.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID && allKoushuIds.Contains(obj.SiKoushuID)));
                        if (seninSiKoushus.Count() > 0)
                        {
                            foreach (SiKoushu kyouiku in seninSiKoushus)
                            {
                                MsSeninPlus newSeninPlus = new MsSeninPlus();
                                newSeninPlus.Senin = seninPlus.Senin;
                                newSeninPlus.Koushu = kyouiku;

                                retList.Add(newSeninPlus);
                            }
                        }
                    }

                    if (retList.Any(obj => obj.Senin.MsSeninID == seninPlus.Senin.MsSeninID) == false)
                    {
                        MsSeninPlus newSeninPlus = new MsSeninPlus();
                        newSeninPlus.Senin = seninPlus.Senin;
                        newSeninPlus.Card = seninPlus.Card;

                        retList.Add(newSeninPlus);
                    }
                }
            }
            #endregion




            if (advancedSearchFilter.includeKazoku)
            {
                List<SiKazoku> list = SiKazoku.GetRecords(loginUser);
                if (list.Count() > 0)
                {
                    tmpList = retList.ToList();
                    retList = new List<MsSeninPlus>();
                    foreach (MsSeninPlus seninPlus in tmpList)
                    {
                        var seninSiKazokus = list.Where(obj => (obj.MsSeninID == seninPlus.Senin.MsSeninID));
                        if (seninSiKazokus.Count() > 0)
                        {
                            seninSiKazokus = seninSiKazokus.OrderBy(o => o.ShowOrder);
                            foreach (SiKazoku kazoku in seninSiKazokus)
                            {
                                MsSeninPlus newSeninPlus = CopyMsSeninPlus(seninPlus);

                                if (seninSiKazokus.Any(o => o.EmergencyKind == 船員.船員_連絡先_一次))
                                    newSeninPlus.KazokuEmg1 = seninSiKazokus.Where(o => o.EmergencyKind == 船員.船員_連絡先_一次).OrderBy(o => o.ShowOrder).FirstOrDefault();
                                if (seninSiKazokus.Any(o => o.EmergencyKind == 船員.船員_連絡先_二次))
                                    newSeninPlus.KazokuEmg2 = seninSiKazokus.Where(o => o.EmergencyKind == 船員.船員_連絡先_二次).OrderBy(o => o.ShowOrder).FirstOrDefault();
                                newSeninPlus.Kazoku = kazoku;

                                retList.Add(newSeninPlus);
                            }
                        }
                        else
                        {
                            retList.Add(seninPlus);
                        }
                    }
                }
            }



            List<MsSeninAddress> seninAddress = MsSeninAddress.GetRecords(loginUser);
            retList.ForEach(o =>
            {
                o.Address = seninAddress.Where(thisO => thisO.MsSeninID == o.Senin.MsSeninID).FirstOrDefault();
            });

            List<MsSeninCareer> seninCareer = MsSeninCareer.GetRecords(loginUser);
            retList.ForEach(o =>
            {
                o.Career = seninCareer.Where(thisO => thisO.MsSeninID == o.Senin.MsSeninID).FirstOrDefault();
            });

            List<MsSeninEtc> seninEtc = MsSeninEtc.GetRecords(loginUser);
            retList.ForEach(o =>
            {
                o.Etc = seninEtc.Where(thisO => thisO.MsSeninID == o.Senin.MsSeninID).FirstOrDefault();
            });


            #endregion

            return retList;

        }

        private static MsSeninPlus CopyMsSeninPlus(MsSeninPlus org)
        {
            MsSeninPlus dst = new MsSeninPlus();

            dst.Senin = org.Senin;
            dst.Address = org.Address;
            dst.Career = org.Career;
            dst.Etc = org.Etc;

            dst.Card = org.Card;

            dst.Menjou_K = org.Menjou_K;
            dst.Menjou_S = org.Menjou_S;
            dst.Menjou_M = org.Menjou_M;

            dst.KazokuEmg1 = org.KazokuEmg1;
            dst.KazokuEmg2 = org.KazokuEmg2;
            dst.Kazoku = org.Kazoku;



            dst.Kenshin = org.Kenshin;
            dst.Shobyo = org.Shobyo;
            dst.Koushu = org.Koushu;

            return dst;
        }

        private static List<string> ChooseIds(Dictionary<int, List<string>> dic)
        {
            List<string> Ids = new List<string>();
            foreach (var key in dic.Keys)
            {
                Debug.WriteLine("ChooseIds [key]: " + key + " [Count]:" + dic[key].Count);
                if (Ids.Count() == 0)
                {
                    Ids = dic[key];
                }
                else
                {
                    Ids = Ids.Intersect(dic[key]).ToList();　// 両方にあるものが抽出される
                }
            }
            return Ids;
        }

        private static void Search(MsUser loginUser, SeninTableCache seninTableCache, int AndOrFlag, Dictionary<int, List<int>> seninDic, Dictionary<int, List<int>> exceptSeninDic, List<SiAdvancedSearchConditionItem> conditionItems, List<SiAdvancedSearchConditionValue> conditionValues)
        {
            string conditionStr = null;
            ParameterConnection prmCollection = null;


            // AdvancedCondition画面の１行を処理する

            //=====================================
            // 基本情報
            //=====================================
            #region
            var basicItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_BASIC);
            if (basicItems.Count() > 0)
            {
                //=====================================
                // RANK
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_RANK);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                        string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("r", values.Count());

                        conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByRank");
                        conditionStr = conditionStr.Replace("#INNER_SQL_BY_RANK#", innerSQLStr);

                        prmCollection.AddInnerParams("r", values.ToList());

                        List<MsSenin> list = Search("SearchBasic", loginUser, conditionStr, prmCollection);
                        AddDic(AndOrFlag, seninDic, MsSiAdvancedSearchCondition.ID_RANK, list);
                    }
                }
                #endregion
                //=====================================
                // NAME
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();
                    List<MsSenin> allList = new List<MsSenin>();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_NAME);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string[] vals = v.Value.Split(' ');

                                for(int i = 0; i < vals.Count(); i ++)
                                {
                                    conditionStr = "";
                                    prmCollection = new ParameterConnection();

                                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByName");
                                    prmCollection.Add(new DBParameter("NAME", "%" + vals[i] + "%"));

                                    List<MsSenin> list = Search("SearchBasic", loginUser, conditionStr, prmCollection);
                                    allList.AddRange(list);
                                }
                            }
                        }
                        AddDic(AndOrFlag, seninDic, MsSiAdvancedSearchCondition.ID_NAME, allList);
                    }
                }
                #endregion
                //=====================================
                // AGE
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_AGE);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                        int age1 = -1;
                        int age2 = -1;
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string val = null;
                                if (v.Value[0] == 'F')
                                {
                                    val = v.Value.Substring(1);

                                    int.TryParse(val, out age1);
                                }
                                else
                                {
                                    val = v.Value.Substring(1);

                                    int.TryParse(val, out age2);
                                }
                            }
                            if (age1 > 0)
                            {
                                conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFromAge");
                                prmCollection.Add(new DBParameter("FROM_BIRTHDAY", DateTime.Today.AddYears(-age1).ToString("yyyyMMdd")));

                                Debug.WriteLine("FROM_BIRTHDAY: " + DateTime.Today.AddYears(-age1).ToString("yyyyMMdd"));
                            }
                            if (age2 > 0)
                            {
                                conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToAge");
                                //prmCollection.Add(new DBParameter("TO_BIRTHDAY", DateTime.Today.AddYears(-age2).ToString()));
                                prmCollection.Add(new DBParameter("TO_BIRTHDAY", DateTime.Today.AddYears(-(age2 + 1)).ToString("yyyyMMdd")));

                                //Debug.WriteLine("TO_BIRTHDAY: " + DateTime.Today.AddYears(-age2).ToString());
                                Debug.WriteLine("TO_BIRTHDAY: " + DateTime.Today.AddYears(-(age2 + 1)).ToString("yyyyMMdd"));
                            }

                            List<MsSenin> list = Search("SearchBasic", loginUser, conditionStr, prmCollection);
                            AddDic(AndOrFlag, seninDic, MsSiAdvancedSearchCondition.ID_AGE, list);
                        }
                    }
                }
                #endregion
            }
            #endregion

            //=====================================
            // 乗下船情報
            //=====================================
            #region
            var signOnOffItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_SIGN_ON_OFF);
            if (signOnOffItems.Count() > 0)
            {
                //=====================================
                // VESSEL(BOARDING)
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_VESSEL);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                        string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("bv", values.Count());

                        conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByBoardingVessel");
                        conditionStr = conditionStr.Replace("#INNER_SQL_BY_BORADING#", innerSQLStr);

                        prmCollection.AddInnerParams("bv", values.ToList());
                        prmCollection.Add(new DBParameter("START_DATE", DateTime.Today));
                        prmCollection.Add(new DBParameter("END_DATE", DateTime.Today));

                        conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByBoarding");
                        prmCollection.Add(new DBParameter("BOARDING_STATUS", seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)));

                        List<SiCard> list = SearchSiCard("SearchSignOnOff", loginUser, conditionStr, prmCollection);
                        if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                        {
                            AndSiCardList.AddRange(list);
                            AddDic(AndSiCardDic, MsSiAdvancedSearchCondition.ID_VESSEL, list);
                            AddDic(seninDic, MsSiAdvancedSearchCondition.ID_VESSEL, list);
                        }
                        else
                        {
                            OrSiCardList.AddRange(list);
                            AddDic(OrSiCardDic, MsSiAdvancedSearchCondition.ID_VESSEL, list);
                            AddDic(seninDic, MsSiAdvancedSearchCondition.ID_VESSEL, list);
                        }
                    }
                }
                #endregion
                
                //=====================================
                // VESSELALL
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_VESSEL_ALL);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                        string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("va", values.Count());

                        conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByVesselAll");
                        conditionStr = conditionStr.Replace("#INNER_SQL_BY_VESSEL_ALL#", innerSQLStr);

                        prmCollection.AddInnerParams("va", values.ToList());

                        conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByBoarding");
                        prmCollection.Add(new DBParameter("BOARDING_STATUS", seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)));

                        List<SiCard> list = SearchSiCard("SearchSignOnOff", loginUser, conditionStr, prmCollection);
                        if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                        {
                            AndSiCardList.AddRange(list);
                            AddDic(AndSiCardDic, MsSiAdvancedSearchCondition.ID_VESSEL_ALL, list);
                            AddDic(seninDic, MsSiAdvancedSearchCondition.ID_VESSEL_ALL, list);
                        }
                        else
                        {
                            OrSiCardList.AddRange(list);
                            AddDic(OrSiCardDic, MsSiAdvancedSearchCondition.ID_VESSEL_ALL, list);
                            AddDic(seninDic, MsSiAdvancedSearchCondition.ID_VESSEL_ALL, list);
                        }
                    }
                }
                #endregion 
             
                //=====================================
                // DAYS
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_DAYS);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                        int days1 = -1;
                        int days2 = -1;
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string val = null;
                                if (v.Value[0] == 'F')
                                {
                                    val = v.Value.Substring(1);

                                    int.TryParse(val, out days1);
                                }
                                else
                                {
                                    val = v.Value.Substring(1);

                                    int.TryParse(val, out days2);
                                }
                            }
                            if (days1 > 0)
                            {
                                conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFromDays");
                                prmCollection.Add(new DBParameter("FROM_DAYS", days1));

                            }
                            if (days2 > 0)
                            {
                                conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToDays");
                                prmCollection.Add(new DBParameter("TO_DAYS", days2));

                            }

                            List<SiCard> list = SearchSiCard("SearchSignOnOff", loginUser, conditionStr, prmCollection);
                            if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                            {
                                AndSiCardList.AddRange(list);
                                AddDic(AndSiCardDic, MsSiAdvancedSearchCondition.ID_DAYS, list);
                                AddDic(seninDic, MsSiAdvancedSearchCondition.ID_DAYS, list);
                            }
                            else
                            {
                                OrSiCardList.AddRange(list);
                                AddDic(OrSiCardDic, MsSiAdvancedSearchCondition.ID_DAYS, list);
                                AddDic(seninDic, MsSiAdvancedSearchCondition.ID_DAYS, list);
                            }
                        }
                    }

                }
                #endregion
                
            }
            #endregion

            //=====================================
            // 免許免状
            //=====================================
            #region
            var licenseItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE);
            if (licenseItems.Count() > 0)
            {
                conditionStr = "";
                prmCollection = new ParameterConnection();
                bool notExists = false;

                // Type
                #region
                var typeItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE_TYPE);
                if (typeItems.Count() > 0)
                {
                    var values = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && licenseItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("lt", values.Count());

                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByType");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_LICENSE_TYPE#", innerSQLStr);

                    prmCollection.AddInnerParams("lt", values.ToList());
                }
                #endregion
                // Grade
                #region
                var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE_GRADE);
                var gradeValues = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && typeItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));
                if (gradeValues.Count() > 0)
                {
                    // Gradeが選択されている場合、Typeは無視する
                    conditionStr = "";
                    prmCollection = new ParameterConnection();


                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("lg", gradeValues.Count());

                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByGrade");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_LICENSE_GRADE#", innerSQLStr);

                    prmCollection.AddInnerParams("lg", gradeValues.ToList());
                }
                #endregion

                // ここからは下記のどれか１つしか該当しない
                //===========
                // Existence
                //===========
                #region
                items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE_EXISTENCE);
                if (items.Count() > 0)
                {
                    //SiAdvancedSearchConditionValue.VALUE_EXISTENCE_EXISTS　の場合、　特別な処理はいらない
                    //SiAdvancedSearchConditionValue.VALUE_EXISTENCE_NOT_EXISTS　の場合
                    //この後のSQLで該当する船員を除く処理を実施（Existsな船員が抽出されてくるので、抽出された船員以外を対象としなければいけない） 

                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));
                    if (values.First().Value == SiAdvancedSearchConditionValue.VALUE_LICENSE_NOT_EXISTS)
                    {
                        notExists = true;
                    }

                }
                #endregion
                //===========
                // ExpiryDate
                //===========
                #region
                items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE_EXPIRE);
                if (items.Count() > 0)
                {
                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                    string days1 = null;
                    string days2 = null;
                    if (values.Count() > 0)
                    {
                        foreach (SiAdvancedSearchConditionValue v in values)
                        {
                            if (v.Value == null)
                                continue;

                            if (v.Value[0] == 'F')
                            {
                                days1 = v.Value.Substring(1);
                            }
                            else
                            {
                                days2 = v.Value.Substring(1);
                            }
                        }
                        if (days1 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFormExpiryDate");
                            prmCollection.Add(new DBParameter("FROM_EXPIRY_DATE", days1));

                        }
                        if (days2 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToExpiryDate");
                            prmCollection.Add(new DBParameter("TO_EXPIRY_DATE", days2));
                        }
                    }
                }
                #endregion
                //===========
                // IssueDate
                //===========
                #region
                items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE_ISSUE);
                if (items.Count() > 0)
                {
                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                    string days1 = null;
                    string days2 = null;
                    if (values.Count() > 0)
                    {
                        foreach (SiAdvancedSearchConditionValue v in values)
                        {
                            if (v.Value == null)
                                continue;

                            if (v.Value[0] == 'F')
                            {
                                days1 = v.Value.Substring(1);
                            }
                            else
                            {
                                days2 = v.Value.Substring(1);
                            }
                        }
                        if (days1 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFormIssueDate");
                            prmCollection.Add(new DBParameter("FROM_ISSUE_DATE", days1));

                        }
                        if (days2 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToIssueDate");
                            prmCollection.Add(new DBParameter("TO_ISSUE_DATE", days2));
                        }
                    }
                }
                #endregion

                //List<MsSenin> list = Search("SearchLicense", loginUser, conditionStr, prmCollection);
                List<SiMenjou> list = SearchSiMenjou("SearchLicense", loginUser, conditionStr, prmCollection);
                if (notExists)
                {
                    AddDic(exceptSeninDic, MsSiAdvancedSearchCondition.ID_LICENSE, list);
                }
                else
                {
                    if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                    {
                        AndSiMenjouList.AddRange(list);
                        AddDic(AndSiMenjouDic, MsSiAdvancedSearchCondition.ID_LICENSE, list);
                    }
                    else
                    {
                        OrSiMenjouList.AddRange(list);
                        AddDic(OrSiMenjouDic, MsSiAdvancedSearchCondition.ID_LICENSE, list);
                    }

                    AddDic(seninDic, MsSiAdvancedSearchCondition.ID_LICENSE, list);
                }
            }
            #endregion

            //=====================================
            // 傷病
            //=====================================
            #region
            var injuriesItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_INJURIES);
            if (injuriesItems.Count() > 0)
            {
                //===========
                // 有無
                //===========
                string baseSQL = "";
                conditionStr = "";
                prmCollection = new ParameterConnection();

                var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_INJURIES_EXISTENCE);
                if (items.Count() > 0)
                {
                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                    if (values.First().Value == SiAdvancedSearchConditionValue.VALUE_EXISTENCE_EXISTS)
                    {
                        baseSQL = "SearchShobyo";
                        conditionStr = "";
                    }
                    else if (values.First().Value == SiAdvancedSearchConditionValue.VALUE_EXISTENCE_NOT_EXISTS)
                    {
                        baseSQL = "SearchNotExistsShobyo";
                        conditionStr = "";
                    }

                    List<SiShobyo> list = SearchSiShobyo(baseSQL, loginUser, conditionStr, prmCollection);
                    if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                    {
                        AndSiShobyoList.AddRange(list);
                        AddDic(AndSiShobyoDic, MsSiAdvancedSearchCondition.ID_INJURIES, list);
                    }
                    else
                    {
                        OrSiShobyoList.AddRange(list);
                        AddDic(OrSiShobyoDic, MsSiAdvancedSearchCondition.ID_INJURIES, list);
                    }
                    AddDic(seninDic, MsSiAdvancedSearchCondition.ID_INJURIES, list);
                }   


            }
            #endregion

            //=====================================
            // CrewMatrix
            //=====================================
            #region
            var crewMatrixItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_CREW_MATRIX);
            if (crewMatrixItems.Count() > 0)
            {
                //=====================================
                // YearsInRank 
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_YEARS_IN_RANK);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                        decimal fromYears = -1;
                        decimal toYears = -1;
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string val = null;
                                if (v.Value[0] == 'F')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out fromYears);
                                }
                                else if (v.Value[0] == 'T')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out toYears);
                                }
                            }
                            if (fromYears > 0 || toYears > 0)
                            {
                                conditionStr = "";
                                prmCollection = new ParameterConnection();

                                if (fromYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFromCrewMatrix");
                                    prmCollection.Add(new DBParameter("FROM_DAYS", CalcDays(fromYears)));

                                }
                                if (toYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToCrewMatrix");
                                    prmCollection.Add(new DBParameter("TO_DAYS", CalcDays(toYears)));

                                }

                                //List<MsSenin> list = Search("SearchYearsInRank", loginUser, conditionStr, prmCollection);
                                //AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_RANK, list);

                                List<CrewMatrix> list = SearchCrewMatrix("SearchYearsInRank", loginUser, conditionStr, prmCollection);
                                if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                                {
                                    AndCrewMatrixList.AddRange(list);
                                    AddDic(AndCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_RANK, list);
                                }
                                else
                                {
                                    OrCrewMatrixList.AddRange(list);
                                    AddDic(OrCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_RANK, list);
                                }
                                AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_RANK, list);
                            }
                        }
                    }
                }
                #endregion
                //=====================================
                // YearsOfThisTypeOfTanker
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                        List<SiAdvancedSearchConditionValue> typeValues = new List<SiAdvancedSearchConditionValue>();
                        decimal fromYears = -1;
                        decimal toYears = -1;
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string val = null;
                                if (v.Value[0] == 'F')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out fromYears);
                                }
                                else if (v.Value[0] == 'T')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out toYears);
                                }
                                else
                                {
                                    typeValues.Add(v);
                                }


                            }

                            if (typeValues.Count() > 0 && (fromYears > 0 || toYears > 0))
                            {
                                conditionStr = "";
                                prmCollection = new ParameterConnection();

                                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("tt", typeValues.Count());
                                conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByTypeOfTanker");
                                conditionStr = conditionStr.Replace("#INNER_SQL_BY_TYPE_OF_TANKER#", innerSQLStr);


                                // パラメータ渡しでCASTｴﾗｰになるので、文字列に置き換える
                                //prmCollection.AddInnerParams("tt", typeValues.ToList());
                                int pidx = 0;
                                foreach(SiAdvancedSearchConditionValue v in typeValues)
                                {
                                    conditionStr = conditionStr.Replace(":tt" + pidx.ToString(), "'" + v.Value + "'");
                                    pidx++;
                                }

                                if (fromYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFromCrewMatrix");
                                    prmCollection.Add(new DBParameter("FROM_DAYS", CalcDays(fromYears)));

                                }
                                if (toYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToCrewMatrix");
                                    prmCollection.Add(new DBParameter("TO_DAYS", CalcDays(toYears)));

                                }

                                //List<MsSenin> list = Search("SearchYearsOfTanker", loginUser, conditionStr, prmCollection);
                                //AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER, list);

                                List<CrewMatrix> list = SearchCrewMatrix("SearchYearsOfTanker", loginUser, conditionStr, prmCollection);
                                if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                                {
                                    AndCrewMatrixList.AddRange(list);
                                    AddDic(AndCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER, list);
                                }
                                else
                                {
                                    OrCrewMatrixList.AddRange(list);
                                    AddDic(OrCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER, list);
                                }
                                AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER, list);
                            }
                        }
                    }
                }
                #endregion
                //=====================================
                // YearsInOperator
                //=====================================
                #region
                {
                    conditionStr = "";
                    prmCollection = new ParameterConnection();

                    var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_YEARS_IN_OPERATOR);
                    if (items.Count() > 0)
                    {
                        var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                        decimal fromYears = -1;
                        decimal toYears = -1;
                        if (values.Count() > 0)
                        {
                            foreach (SiAdvancedSearchConditionValue v in values)
                            {
                                if (v.Value == null)
                                    continue;

                                string val = null;
                                if (v.Value[0] == 'F')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out fromYears);
                                }
                                else if (v.Value[0] == 'T')
                                {
                                    val = v.Value.Substring(1);

                                    decimal.TryParse(val, out toYears);
                                }
                            }
                            if (fromYears > 0 || toYears > 0)
                            {
                                conditionStr = "";
                                prmCollection = new ParameterConnection();


                                string companyName = 船員_自社名;

                                prmCollection.Add(new DBParameter("COMPANY_NAME", companyName));


                                if (fromYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFromCrewMatrix");
                                    prmCollection.Add(new DBParameter("FROM_DAYS", CalcDays(fromYears)));

                                }
                                if (toYears > 0)
                                {
                                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToCrewMatrix");
                                    prmCollection.Add(new DBParameter("TO_DAYS", CalcDays(toYears)));

                                }

                                //List<MsSenin> list = Search("SearchYearsInOperator", loginUser, conditionStr, prmCollection);
                                //AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_OPERATOR, list);

                                List<CrewMatrix> list = SearchCrewMatrix("SearchYearsInOperator", loginUser, conditionStr, prmCollection);
                                if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                                {
                                    AndCrewMatrixList.AddRange(list);
                                    AddDic(AndCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_OPERATOR, list);
                                }
                                else
                                {
                                    OrCrewMatrixList.AddRange(list);
                                    AddDic(OrCrewMatrixDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_OPERATOR, list);
                                }
                                AddDic(seninDic, MsSiAdvancedSearchCondition.ID_YEARS_IN_OPERATOR, list);
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            //=====================================
            // 健康診断
            //=====================================
            #region
            var medicalItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL);
            if (medicalItems.Count() > 0)
            {
                conditionStr = "";
                prmCollection = new ParameterConnection();
                bool notExists = false;

                // Kind
                #region
                var kindItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL_KIND);
                if (kindItems.Count() > 0)
                {
                    var values = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && medicalItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => obj.Value);

                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("mk", values.Count());

                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByKenshinKind");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_KENSHIN_KIND#", innerSQLStr);

                    prmCollection.AddInnerParams("mk", values.ToList());
                }
                #endregion

                // ここからは下記のどれか１つしか該当しない
                //===========
                // Result
                //===========
                #region
                var items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL_RESULT);
                var resultValues = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));
                if (resultValues.Count() > 0)
                {
                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("mr", resultValues.Count());

                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByKenshinResult");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_KENSHIN_RESULT#", innerSQLStr);

                    prmCollection.AddInnerParams("mr", resultValues.ToList());
                }
                #endregion
                //===========
                // ExpiryDate
                //===========
                #region
                items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL_EXPIRE);
                if (items.Count() > 0)
                {
                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                    string days1 = null;
                    string days2 = null;
                    if (values.Count() > 0)
                    {
                        foreach (SiAdvancedSearchConditionValue v in values)
                        {
                            if (v.Value == null)
                                continue;

                            if (v.Value[0] == 'F')
                            {
                                days1 = v.Value.Substring(1);
                            }
                            else
                            {
                                days2 = v.Value.Substring(1);
                            }
                        }
                        if (days1 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFormKenshinExpiryDate");
                            prmCollection.Add(new DBParameter("FROM_EXPIRY_DATE", days1));

                        }
                        if (days2 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToKenshinExpiryDate");
                            prmCollection.Add(new DBParameter("TO_EXPIRY_DATE", days2));
                        }
                    }
                }
                #endregion
                //===========
                // ConsulationDate
                //===========
                #region
                items = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_CONSULTATION_DATE);
                if (items.Count() > 0)
                {
                    var values = conditionValues.Where(obj => items.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));

                    string days1 = null;
                    string days2 = null;
                    if (values.Count() > 0)
                    {
                        foreach (SiAdvancedSearchConditionValue v in values)
                        {
                            if (v.Value == null)
                                continue;

                            if (v.Value[0] == 'F')
                            {
                                days1 = v.Value.Substring(1);
                            }
                            else
                            {
                                days2 = v.Value.Substring(1);
                            }
                        }
                        if (days1 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByFormKenshinConsultationDate");
                            prmCollection.Add(new DBParameter("FROM_CONSULTATION_DATE", days1));

                        }
                        if (days2 != null)
                        {
                            conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByToKenshinConsultationDate");
                            prmCollection.Add(new DBParameter("TO_CONSULTATION_DATE", days2));
                        }
                    }
                }
                #endregion


                List<SiKenshin> list = SearchSiKenshin("SearchKenshin", loginUser, conditionStr, prmCollection);
                if (notExists)
                {
                    AddDic(exceptSeninDic, MsSiAdvancedSearchCondition.ID_MEDICAL, list);
                }
                else
                {
                    if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                    {
                        AndSiKenshinList.AddRange(list);
                        AddDic(AndSiKenshinDic, MsSiAdvancedSearchCondition.ID_MEDICAL, list);
                    }
                    else
                    {
                        OrSiKenshinList.AddRange(list);
                        AddDic(OrSiKenshinDic, MsSiAdvancedSearchCondition.ID_MEDICAL, list);
                    }

                    AddDic(seninDic, MsSiAdvancedSearchCondition.ID_MEDICAL, list);
                }
            }
            #endregion

            //=====================================
            // 積荷経験
            //=====================================
            #region
            var expCargoItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO);
            if (expCargoItems.Count() > 0)
            {
                conditionStr = "";
                prmCollection = new ParameterConnection();

                // 積荷グループ
                #region
                var groupItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_CARGO_GROUP);
                if (groupItems.Count() > 0)
                {
                    var groupValues = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && expCargoItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("cg", groupValues.Count());

                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByCargoGroup");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_CARGO_GROUP#", innerSQLStr);

                    prmCollection.AddInnerParams("cg", groupValues.ToList());

                    var values = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && groupItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));
                    conditionStr += " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByExperienceCargo");
                    prmCollection.Add(new DBParameter("COUNT", values.First()));

                    List<SiExperienceCargo> list = SearchSiExperienceCargo("SearchExperienceCargo", loginUser, conditionStr, prmCollection);
                    if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                    {
                        AndSiExperienceCargoList.AddRange(list);
                        AddDic(AndOrFlag, AndSiExperienceCargoDic, MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO, list);
                    }
                    else
                    {
                        OrSiExperienceCargoList.AddRange(list);
                        AddDic(AndOrFlag, OrSiExperienceCargoDic, MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO, list);
                    }

                    AddDic(AndOrFlag, seninDic, MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO, list);
                }
                #endregion
            }
            #endregion

            //=====================================
            // 講習
            //=====================================
            #region
            var traningItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_TRAINING);
            if (traningItems.Count() > 0)
            {
                conditionStr = "";
                prmCollection = new ParameterConnection();

                // 講習名
                #region
                var nameItems = conditionItems.Where(obj => obj.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_TRAINING_NAME);
                if (nameItems.Count() > 0)
                {
                    var nameValues = conditionValues.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && traningItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID)).Select(obj => int.Parse(obj.Value));

                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("kn", nameValues.Count());

                    conditionStr = " " + SqlMapper.SqlMapper.GetSql(typeof(船員検索), "ByKoushu");
                    conditionStr = conditionStr.Replace("#INNER_SQL_BY_KOUSHU#", innerSQLStr);

                    prmCollection.AddInnerParams("kn", nameValues.ToList());

                    List<SiKoushu> list = SearchSiKoushu("SearchKoushu", loginUser, conditionStr, prmCollection);

                    //===========
                    // 受講の有無
                    //===========
                    ////SiAdvancedSearchConditionValue.VALUE_EXISTENCE_EXISTS　の場合、　特別な処理はいらない
                    ////SiAdvancedSearchConditionValue.VALUE_EXISTENCE_NOT_EXISTS　の場合
                    ////この後のSQLで該当する船員を除く処理を実施（Existsな船員が抽出されてくるので、抽出された船員以外を対象としなければいけない） 

                    var values2 = conditionValues.Where(obj => nameItems.Select(t => t.SiAdvancedSearchConditionItemID).Contains(obj.SiAdvancedSearchConditionItemID));
                    if (values2.Last().Value == SiAdvancedSearchConditionValue.VALUE_TRAINING_NOT_EXISTS)
                    {
                        AddDic(exceptSeninDic, MsSiAdvancedSearchCondition.ID_TRAINING, list);
                    }
                    else
                    {
                        if (AndOrFlag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                        {
                            AndSiKoushuList.AddRange(list);
                            AddDic(AndSiKoushuDic, MsSiAdvancedSearchCondition.ID_TRAINING, list);
                        }
                        else
                        {
                            OrSiKoushuList.AddRange(list);
                            AddDic(OrSiKoushuDic, MsSiAdvancedSearchCondition.ID_TRAINING, list);
                        }

                        AddDic(seninDic, MsSiAdvancedSearchCondition.ID_TRAINING, list);
                    }
                }
                #endregion
            }
            #endregion

        }

        #region 各クラスごとの検索処理およびDictonary作成

        private static List<MsSenin> Search(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<MsSenin> mapping = new MappingBase<MsSenin>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<MsSenin>();
            }

        }
        private static void AddDic(int flag, Dictionary<int, List<int>> dic, int key, List<MsSenin> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                if (flag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                {
                    dic[key] = dic[key].Intersect(list.Select(obj => obj.MsSeninID)).ToList();　// 両方にあるものが抽出される
                }
                else
                {
                    dic[key].AddRange(list.Select(obj => obj.MsSeninID).ToList());
                }
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).ToList());
            }
        }

        private static List<SiCard> SearchSiCard(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiCard> mapping = new MappingBase<SiCard>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<SiCard>();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<SiCard> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }
        private static void AddDic(Dictionary<int, List<string>> dic, int key, List<SiCard> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.SiCardID).ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.SiCardID).ToList());
            }
        }
        private static List<SiMenjou> SearchSiMenjou(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<SiMenjou>();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<SiMenjou> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }
        private static void AddDic(Dictionary<int, List<string>> dic, int key, List<SiMenjou> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.SiMenjouID).ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.SiMenjouID).ToList());
            }
        }

        private static List<SiShobyo> SearchSiShobyo(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiShobyo> mapping = new MappingBase<SiShobyo>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return new List<SiShobyo>();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<SiShobyo> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }
        private static void AddDic(Dictionary<int, List<string>> dic, int key, List<SiShobyo> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.SiShobyoID).ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.SiShobyoID).ToList());
            }
        }

        private static List<CrewMatrix> SearchCrewMatrix(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<CrewMatrix> mapping = new MappingBase<CrewMatrix>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<CrewMatrix> ();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<CrewMatrix> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }

        private static List<SiKenshin> SearchSiKenshin(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<SiKenshin>();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<SiKenshin> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }
        private static void AddDic(Dictionary<int, List<string>> dic, int key, List<SiKenshin> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.SiKenshinID).ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.SiKenshinID).ToList());
            }
        }

        private static List<SiExperienceCargo> SearchSiExperienceCargo(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiExperienceCargo> mapping = new MappingBase<SiExperienceCargo>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<SiExperienceCargo>();
            }
        }
        private static void AddDic(int flag, Dictionary<int, List<int>> dic, int key, List<SiExperienceCargo> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                if (flag == SiAdvancedSearchConditionItem.AND_OR_FLAG_AND)
                {
                    dic[key] = dic[key].Intersect(list.Select(obj => obj.MsSeninID)).ToList();　// 両方にあるものが抽出される
                }
                else
                {
                    dic[key].AddRange(list.Select(obj => obj.MsSeninID).ToList());
                }
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }

        private static List<SiKoushu> SearchSiKoushu(string baseName, NBaseData.DAC.MsUser loginUser, string conditionStr, ParameterConnection Params)
        {
            try
            {
                string BaseSQL = SqlMapper.SqlMapper.GetSql(typeof(船員検索), baseName);

                if (conditionStr != null)
                {
                    BaseSQL += " " + conditionStr;
                }
                Debug.WriteLine("SQL: " + BaseSQL);

                MappingBase<SiKoushu> mapping = new MappingBase<SiKoushu>();
                return mapping.FillRecrods(loginUser.MsUserID, BaseSQL, Params);
            }
            catch
            {
                return new List<SiKoushu>();
            }
        }
        private static void AddDic(Dictionary<int, List<int>> dic, int key, List<SiKoushu> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.MsSeninID).Distinct().ToList());
            }
        }
        private static void AddDic(Dictionary<int, List<string>> dic, int key, List<SiKoushu> list)
        {
            if (list == null)
                return;

            if (dic.ContainsKey(key))
            {
                dic[key].AddRange(list.Select(obj => obj.SiKoushuID).ToList());
            }
            else
            {
                dic.Add(key, list.Select(obj => obj.SiKoushuID).ToList());
            }
        }


        #endregion



        private static decimal CalcDays(decimal val)
        {
            string strVal = val.ToString();
            string strYears = strVal.Split('.')[0];
            string strMonths = "0";
            if (strVal.IndexOf('.') > 0)
                strMonths = strVal.Split('.')[1];

            int y = int.Parse(strYears);
            int m = int.Parse(strMonths);
            return y * 365 + m * 31;
        }


        private static List<CrewMatrix> GetAllCrewMatrix(NBaseData.DAC.MsUser loginUser, SeninTableCache seninTableCache)
        {
            List<CrewMatrix> ret = new List<CrewMatrix>();

            string conditionStr = "";
            ParameterConnection prmCollection = new ParameterConnection();


            //=====================================
            // YearsInRank 
            //=====================================
            {
                List<CrewMatrix> tmp = SearchCrewMatrix("SearchYearsInRank", loginUser, conditionStr, prmCollection);
                foreach(CrewMatrix cm in tmp)
                {
                    cm.Type = CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_RANK;

                    ret.Add(cm);
                }
            }
            //=====================================
            // YearsOfThisTypeOfTanker
            //=====================================
            {
                List<CrewMatrix> tmp = SearchCrewMatrix("SearchYearsOfTanker", loginUser, conditionStr, prmCollection);
                foreach(CrewMatrix cm in tmp)
                {
                    cm.Type = CrewMatrix.CREW_MATRIX_TYPE.YEARS_ON_THIS_TYPE_OF_TANKER;

                    ret.Add(cm);
                }
            }
            //=====================================
            // YearsInOperator
            //=====================================
            {
                string companyName = 船員_自社名;
                prmCollection.Add(new DBParameter("COMPANY_NAME", companyName));

                List<CrewMatrix> tmp = SearchCrewMatrix("SearchYearsInOperator", loginUser, conditionStr, prmCollection);
                foreach(CrewMatrix cm in tmp)
                {
                    cm.Type = CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_OPERATOR;

                    ret.Add(cm);
                }
            }
            
            return ret;
        }




        //======================================================================
        // 検索条件登録処理
        //======================================================================
        #region

        /// <summary>
        /// 検索条件登録処理
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="conditionHead"></param>
        /// <returns></returns>
        public static bool SaveCondition(MsUser loginUser, SiAdvancedSearchConditionHead conditionHead)
        {
            bool ret = true;

            bool isNew = conditionHead.IsNew();

            List<SiAdvancedSearchConditionItem> orgItems = new List<SiAdvancedSearchConditionItem>();
            List<SiAdvancedSearchConditionValue> orgValues = new List<SiAdvancedSearchConditionValue>();

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    ret = Head_InsertOrUpdate(dbConnect, loginUser, conditionHead);
                    if (ret)
                    {
                        // 更新の場合、洗い替えするので、一度削除する
                        if (isNew == false)
                        {
                            //ret = SiAdvancedSearchConditionItem.DeleteRecords(dbConnect, loginUser, conditionHead.SiAdvancedSearchConditionHeadID);
                            //ret = SiAdvancedSearchConditionValue.DeleteRecords(dbConnect, loginUser, conditionHead.SiAdvancedSearchConditionHeadID);

                            orgItems = SiAdvancedSearchConditionItem.GetRecords(dbConnect, loginUser, conditionHead.SiAdvancedSearchConditionHeadID);
                            orgValues = SiAdvancedSearchConditionValue.GetRecords(dbConnect, loginUser, conditionHead.SiAdvancedSearchConditionHeadID);
                        }
                    }

                    if (ret)
                    {
                        foreach (SiAdvancedSearchConditionItem item in conditionHead.ConditionItemList)
                        {
                            item.SiAdvancedSearchConditionHeadID = conditionHead.SiAdvancedSearchConditionHeadID;
                            if (conditionHead.DeleteFlag == 1)
                            {
                                item.DeleteFlag = 1;
                            }
                            ret = Item_InsertOrUpdate(dbConnect, loginUser, item);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                    }
                    if (ret)
                    {
                        foreach (SiAdvancedSearchConditionValue value in conditionHead.ConditionValueList)
                        {
                            value.SiAdvancedSearchConditionHeadID = conditionHead.SiAdvancedSearchConditionHeadID;
                            if (conditionHead.DeleteFlag == 1)
                            {
                                value.DeleteFlag = 1;
                            }
                            ret = Value_InsertOrUpdate(dbConnect, loginUser, value);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                    }

                    if (orgItems.Count() > 0)
                    {
                        foreach(SiAdvancedSearchConditionItem item in orgItems)
                        {
                            if (conditionHead.ConditionItemList.Any(obj => obj.SiAdvancedSearchConditionItemID == item.SiAdvancedSearchConditionItemID) == false)
                            {
                                item.DeleteFlag = 1;
                                item.RenewDate = DateTime.Now;
                                item.RenewUserID = loginUser.MsUserID;

                                ret = Item_InsertOrUpdate(dbConnect, loginUser, item);
                                if (ret == false)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (orgValues.Count() > 0)
                    {
                        foreach (SiAdvancedSearchConditionValue value in orgValues)
                        {
                            if (conditionHead.ConditionValueList.Any(obj => obj.SiAdvancedSearchConditionValueID == value.SiAdvancedSearchConditionValueID) == false)
                            {
                                value.DeleteFlag = 1;
                                value.RenewDate = DateTime.Now;
                                value.RenewUserID = loginUser.MsUserID;

                                ret = Value_InsertOrUpdate(dbConnect, loginUser, value);
                                if (ret == false)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (ret)
                        dbConnect.Commit();
                    else
                        dbConnect.RollBack();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();

                    ret = false;
                }
            }

            return ret;
        }

        private static bool Head_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiAdvancedSearchConditionHead head)
        {
            head.RenewUserID = loginUser.MsUserID;
            head.RenewDate = DateTime.Now;

            if (head.IsNew())
            {
                return head.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return head.UpdateRecord(dbConnect, loginUser);
            }
        }
        private static bool Item_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiAdvancedSearchConditionItem item)
        {
            item.RenewUserID = loginUser.MsUserID;
            item.RenewDate = DateTime.Now;

            if (item.IsNew())
            {
                //item.SiAdvancedSearchConditionItemID = NBaseUtil.Common.NewID(); 　// ItemのIDは、Valueとの関係があるため、クライアントで付けられている

                return item.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return item.UpdateRecord(dbConnect, loginUser);
            }
        }
        private static bool Value_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiAdvancedSearchConditionValue value)
        {
            value.RenewUserID = loginUser.MsUserID;
            value.RenewDate = DateTime.Now;

            if (value.IsNew())
            {
                value.SiAdvancedSearchConditionValueID = NBaseUtil.Common.NewID();
                return value.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return value.UpdateRecord(dbConnect, loginUser);
            }
        }

        #endregion

        //======================================================================
        // 検索条件取得処理
        //======================================================================
        #region

        public static List<SiAdvancedSearchConditionHead> GetConditions(MsUser loginUser)
        {
            List<SiAdvancedSearchConditionHead> ret = new List<SiAdvancedSearchConditionHead>();

            ret = SiAdvancedSearchConditionHead.GetRecords(loginUser);
            foreach(SiAdvancedSearchConditionHead head in ret)
            {
                head.ConditionItemList = SiAdvancedSearchConditionItem.GetRecords(loginUser, head.SiAdvancedSearchConditionHeadID);
                head.ConditionValueList = SiAdvancedSearchConditionValue.GetRecords(loginUser, head.SiAdvancedSearchConditionHeadID);
            }

            return ret;
        }

        #endregion
    }
}
