using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping;

namespace NBaseData.BLC
{
    public class 講習管理
    {
        public static List<SiKoushu> BLC_講習管理_検索(MsUser loginUser, SeninTableCache seninTableCache, SiKoushuFilter filter)
        {
            List<SiKoushu> tmpRet = new List<SiKoushu>();

            List<SiKoushu> tmpAll = SiKoushu.GetRecordsByFilter(loginUser, filter);

            if (filter.Is期限切れ)
            {
                List<SiKoushu> tmp = SiKoushu.GetIdByJisekiToMax(loginUser);

                foreach (SiKoushu k in tmpAll)
                {
                    if (k.KoushuYukokigenDays == 0)
                    {
                        // 講習マスタの有効期限日数が０日の場合、対象外とする
                        continue;
                    }

                    var existsTmp = from t in tmp
                                    where t.SiKoushuID == k.SiKoushuID
                                    select t;
                    if (existsTmp.Count<SiKoushu>() > 0)
                    {
                        // 実績終了日＋有効期限日数が、今日より前なら対象
                        DateTime limitDate = DateTime.Parse(k.JisekiTo.ToShortDateString()).AddDays(k.KoushuYukokigenDays);
                        if (limitDate < DateTime.Now)
                        {
                            tmpRet.Add(k);
                        }
                    }
                }
            }
            else if (filter.Is未受講)
            {
                // SiKoushu のないものを構築する必要がある

                // 実績レコードをすべて取得
                SiKoushuFilter jisekiAllFilter = new SiKoushuFilter();
                jisekiAllFilter.JisekiFrom = filter.JisekiFrom;
                if (filter.JisekiFrom == DateTime.MinValue)
                    jisekiAllFilter.JisekiFrom = jisekiAllFilter.JisekiFrom.AddDays(1);
                jisekiAllFilter.JisekiTo = filter.JisekiTo;
                if (filter.JisekiTo == DateTime.MinValue)
                    jisekiAllFilter.JisekiTo = DateTime.MaxValue;
                List<SiKoushu> siJisekiAll = SiKoushu.GetRecordsByFilter(loginUser, jisekiAllFilter);
                
                // 予定レコードをすべて取得
                SiKoushuFilter yoteiAllFilter = new SiKoushuFilter();
                yoteiAllFilter.YoteiFrom = DateTime.Today.AddDays(1);
                List<SiKoushu> siYoteiAll = null;
                if (filter.JisekiTo != DateTime.MinValue && filter.JisekiTo < DateTime.Today)
                {
                    siYoteiAll = new List<SiKoushu>();
                }
                else
                {
                    siYoteiAll = SiKoushu.GetRecordsByFilter(loginUser, yoteiAllFilter);
                }

                // 対象になる船員を取得
                MsSeninFilter seninFilter = new MsSeninFilter();
                if (filter.Name != null)
                    seninFilter.Name = filter.Name;
                if (filter.NameKana != null)
                    seninFilter.NameKana = filter.NameKana;
                if (filter.MsSiShokumeiID > 0)
                    seninFilter.MsSiShokumeiID = filter.MsSiShokumeiID;
                if (filter.ShimeiCode != null)
                    seninFilter.ShimeiCode = filter.ShimeiCode;
                seninFilter.RetireFlag = 0;
                seninFilter.船員テーブルのみ対象 = true;
                seninFilter.joinSiCard = MsSeninFilter.JoinSiCard.NOT_JOIN;
                seninFilter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";
                List<MsSenin> msSeninAll = MsSenin.GetRecordsByFilter(loginUser, seninFilter);
                
                // 講習をすべて取得
                List<MsSiKoushu> msSiKoushuAll = MsSiKoushu.GetRecords(loginUser);
                if (filter.MsSiKoushuID > 0)
                {
                    var tmp = msSiKoushuAll.Where(obj => obj.MsSiKoushuID == filter.MsSiKoushuID);
                    msSiKoushuAll = tmp.ToList();
                }

                // SiKoushuのないものをリストに追加する
                foreach (MsSenin senin in msSeninAll)
                {
                    var 未受講koushuIDS = (from obj in tmpAll
                                           where obj.MsSeninID == senin.MsSeninID
                                           select obj.MsSiKoushuID).Distinct();

                    var 受講済koushuIDs = (from obj in siJisekiAll
                                           where obj.MsSeninID == senin.MsSeninID
                                           select obj.MsSiKoushuID).Distinct();

                    var 予定koushuIDs = (from obj in siYoteiAll
                                        where obj.MsSeninID == senin.MsSeninID
                                        select obj.MsSiKoushuID).Distinct();

                    foreach (MsSiKoushu koushu in msSiKoushuAll)
                    {
                        if (未受講koushuIDS.Contains(koushu.MsSiKoushuID) == false &&
                            受講済koushuIDs.Contains(koushu.MsSiKoushuID) == false &&
                            予定koushuIDs.Contains(koushu.MsSiKoushuID) == false)
                        {
                            SiKoushu 未受講koushu = new SiKoushu();
                            未受講koushu.MsSeninID = senin.MsSeninID;
                            未受講koushu.SeninShimeiCode = senin.ShimeiCode;
                            未受講koushu.SeninName = senin.Sei + " " + senin.Mei;
                            未受講koushu.SeninNameKana = senin.SeiKana + " " + senin.MeiKana;
                            未受講koushu.SeninShokumei = seninTableCache.GetMsSiShokumeiName(loginUser, senin.MsSiShokumeiID);
                            未受講koushu.SeninShokumeiID = senin.MsSiShokumeiID;
                            未受講koushu.MsSiKoushuID = koushu.MsSiKoushuID;
                            未受講koushu.KoushuName = koushu.Name;

                            tmpRet.Add(未受講koushu);
                        }
                    }
                }

                // 検索結果も対象になるので追加
                tmpRet.AddRange(tmpAll);
            }
            else
            {
                tmpRet.AddRange(tmpAll);
            }

            if (filter.Flag == 0)
            {
                var ret = from obj in tmpRet
                          orderby obj.KoushuName, obj.SeninShimeiCode, obj.YoteiFrom, obj.JisekiTo
                          select obj;
                return ret.ToList<SiKoushu>();
            }
            else
            {
                var ret = from obj in tmpRet
                          orderby obj.SeninShokumeiID, obj.SeninNameKana, obj.KoushuName, obj.JisekiFrom, obj.JisekiTo
                          select obj;
                return ret.ToList<SiKoushu>();
            }

        }
    }
}
