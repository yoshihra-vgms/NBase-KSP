using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping;

namespace NBaseData.BLC
{
    public class 免許免状管理
    {
        public static List<SiMenjou> BLC_免許免状管理_検索(MsUser loginUser, SeninTableCache seninTableCache, SiMenjouFilter filter)
        {
            List<SiMenjou> tmpRet = new List<SiMenjou>();


            // 免許免状の条件なしに取得する
            // ※免許免状の条件はロジックで。
            List<SiMenjou> tmpAll = SiMenjou.GetRecordsByFilter(loginUser, filter);

            // 2018.01 ２０１７年度改造
            // ALLから筆記のみは除く
            List<SiMenjou> tmpWrittenAll = tmpAll.Where(obj => obj.WrittenTest == 1).ToList();
            tmpAll = tmpAll.Where(obj => obj.WrittenTest != 1).ToList();

            // 免許免状の条件は ＡＮＤ なので　すべての免許免状を持っている人をピックアップする
            Dictionary<int, List<SiMenjou>> 取得情報Dic = new Dictionary<int, List<SiMenjou>>();


            int index = 0;

            // 免許免状IDでフィルタ
            foreach (int id in filter.MsSiMenjouIDs)
            {
                var tmpList = tmpAll.Where(obj => obj.MsSiMenjouID == id);

                if (tmpList.Count() > 0)
                    取得情報Dic.Add(index, tmpList.ToList());
                else
                    取得情報Dic.Add(index, new List<SiMenjou>());

                index++;
            }


            // 上位の種別IDを含めフィルタ

            // 指定された種別IDから上位の種別IDを取得する
            List<MsSiExcludeMenjouKind> excludeMenjouKinds = MsSiExcludeMenjouKind.GetRecords(loginUser);
            foreach (int kindId in filter.MsSiMenjouKindIDs)
            {
                var tmpList = tmpAll.Where(obj => obj.MsSiMenjouKindID == kindId);
                if (tmpList.Count() > 0)
                    取得情報Dic.Add(index, tmpList.ToList());
                else
                    取得情報Dic.Add(index, new List<SiMenjou>());

                var 有効免許免状種別IDs = excludeMenjouKinds.Where(obj => obj.ExcludeMenjouKindID == kindId).Select(obj => obj.MsSiMenjouKindID).Distinct();
                foreach (int excludeKindId in 有効免許免状種別IDs)
                {
                    var tmpList2 = tmpAll.Where(obj => obj.MsSiMenjouKindID == excludeKindId);

                    if (tmpList2.Count() > 0)
                        (取得情報Dic[index]).AddRange(tmpList2);
                }

                index++;
            }

            List<int> すべて取得している船員 = (取得情報Dic[0]).Select(c => c.MsSeninID).Distinct().ToList();
            for(int i = 1; i < index; i ++)
            {
                var tmpList = すべて取得している船員.Intersect((取得情報Dic[i]).Select(c => c.MsSeninID).Distinct());
                if (tmpList.Count() == 0)
                {
                    すべて取得している船員 = null;
                    break;
                }
                else
                {
                    すべて取得している船員 = tmpList.ToList();
                }
            }


            if (filter.is取得済)
            {
                // 該当船員なし
                if (すべて取得している船員 == null)
                {
                    tmpRet = new List<SiMenjou>();
                }
                else
                {
                    // 有効期限でフィルタ
                    if (filter.Yukokigen == int.MinValue)
                    {
                        for (int i = 0; i < index; i++)
                        {
                            tmpRet.AddRange((取得情報Dic[i]).Where(obj => すべて取得している船員.Contains(obj.MsSeninID)));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < index; i++)
                        {
                            tmpRet.AddRange((取得情報Dic[i]).Where(obj => すべて取得している船員.Contains(obj.MsSeninID)).Where(obj => (obj.Kigen > DateTime.Today.AddMonths(filter.Yukokigen))));
                        }
                    }

                    var tmp = tmpRet.Distinct();
                    tmpRet = tmp.ToList();
               }

            }
            else // if (filter.is未取得)
            {
                // 対象になる船員を取得
                MsSeninFilter seninFilter = new MsSeninFilter();
                if (filter.Name != null)
                    seninFilter.Name = filter.Name;
                if (filter.ShimeiCode != null)
                    seninFilter.ShimeiCode = filter.ShimeiCode;
                seninFilter.RetireFlag = 0;
                seninFilter.船員テーブルのみ対象 = true;
                seninFilter.joinSiCard = MsSeninFilter.JoinSiCard.NOT_JOIN;
                seninFilter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";
                List<MsSenin> msSeninAll = MsSenin.GetRecordsByFilter(loginUser, seninFilter);
                
                // 職名でフィルタ
                if (filter.MsSiShokumeiIDs.Count > 0)
                {
                    var tmpList = msSeninAll.Where(obj => filter.MsSiShokumeiIDs.Contains(obj.MsSiShokumeiID));
                    msSeninAll = tmpList.ToList();
                }

                // すべて取得している船員を除く
                if (すべて取得している船員 != null && すべて取得している船員.Count > 0)
                {
                    var tmpList = msSeninAll.Where(obj => !すべて取得している船員.Contains(obj.MsSeninID));
                    msSeninAll = tmpList.ToList();
                }


                foreach(MsSenin senin in msSeninAll)
                {
                    foreach (int id in filter.MsSiMenjouIDs)
                    {
                        var tmpList = tmpAll.Where(obj => (obj.MsSeninID == senin.MsSeninID && obj.MsSiMenjouID == id));
                        if (tmpList.Count() == 0)
                        {
                            SiMenjou menjou = new SiMenjou();
                            menjou.MsSeninID = senin.MsSeninID;
                            menjou.SeninNameKana = senin.SeiKana + " " + senin.MeiKana;
                            menjou.SeninShokumeiID = senin.MsSiShokumeiID;
                            menjou.MsSiMenjouID = id;
                            menjou.MsSiMenjouShowOrder = seninTableCache.GetMsSiMenjou(loginUser, id).ShowOrder;

                            // 2018.01 ２０１７年度改造
                            tmpList = tmpWrittenAll.Where(obj => (obj.MsSeninID == senin.MsSeninID && obj.MsSiMenjouID == id));
                            if (tmpList.Count() > 0)
                            {
                                menjou.No = "筆記";
                                if (tmpList.First().No != null && tmpList.First().No.Length > 0)
                                {
                                    menjou.No += ":" + tmpList.First().No;
                                }
                            }
                            tmpRet.Add(menjou);
                        }

                    }
                    foreach (int id in filter.MsSiMenjouKindIDs)
                    {
                        var tmpList = tmpAll.Where(obj => (obj.MsSeninID == senin.MsSeninID && obj.MsSiMenjouKindID == id));
                        if (tmpList.Count() == 0)
                        {
                            bool is取得済 = false;
                            var 有効免許免状種別IDs = excludeMenjouKinds.Where(obj => obj.ExcludeMenjouKindID == id).Select(obj => obj.MsSiMenjouKindID).Distinct();
                            foreach (int excludeKindId in 有効免許免状種別IDs)
                            {
                                var tmpList2 = tmpAll.Where(obj => (obj.MsSeninID == senin.MsSeninID && obj.MsSiMenjouKindID == excludeKindId));

                                if (tmpList2.Count() > 0)
                                {
                                    is取得済 = true;
                                    break;
                                }
                            }
                            if (is取得済 == false)
                            {
                                MsSiMenjouKind kind = seninTableCache.GetMsSiMenjouKind(loginUser, id);
                                SiMenjou menjou = new SiMenjou();
                                menjou.MsSeninID = senin.MsSeninID;
                                menjou.SeninNameKana = senin.SeiKana + " " + senin.MeiKana;
                                menjou.SeninShokumeiID = senin.MsSiShokumeiID;
                                menjou.MsSiMenjouID = kind.MsSiMenjouID;
                                menjou.MsSiMenjouShowOrder = seninTableCache.GetMsSiMenjou(loginUser, kind.MsSiMenjouID).ShowOrder;
                                menjou.MsSiMenjouKindID = id;
                                menjou.MsSiMenjouKindShowOrder = kind.ShowOrder;

                                // 2018.01 ２０１７年度改造
                                tmpList = tmpWrittenAll.Where(obj => (obj.MsSeninID == senin.MsSeninID && obj.MsSiMenjouKindID == id));
                                if (tmpList.Count() > 0)
                                {
                                    menjou.No = "筆記";
                                    if (tmpList.First().No != null && tmpList.First().No.Length > 0)
                                    {
                                        menjou.No += ":" + tmpList.First().No;
                                    }
                                }

                                tmpRet.Add(menjou);
                            }
                        }
                    }
                }
            }
            if (filter.is取得済)
            {
                foreach (SiMenjou m in tmpRet)
                {
                    m.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(loginUser, m.SiMenjouID);

                    if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
                    {
                        m.AttachFiles = SiMenjouAttachFile.GetRecordByMenjouID(loginUser, m.SiMenjouID);
                    }
                }
            }

            var ret = from obj in tmpRet
                      orderby obj.SeninShokumeiID, obj.SeninNameKana, obj.MsSiMenjouShowOrder, obj.MsSiMenjouID, obj.MsSiMenjouKindShowOrder, obj.MsSiMenjouKindID, obj.ShutokuDate
                      select obj;

            return ret.ToList<SiMenjou>();
        }
    }
}
