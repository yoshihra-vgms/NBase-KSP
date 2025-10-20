using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseData.BLC
{
    public class 配乗シミュレーション
    {
        #region データメンバ

        /// <summary>
        /// 船員カードID
        /// </summary>
        [DataMember]
        public string SiCardID { get; set; }

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 船員名
        /// </summary>
        [DataMember]
        public string SeninName { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 種別ID
        /// </summary>
        [DataMember]
        public int MsSiShubetsuID { get; set; }



        /// <summary>
        /// 乗船日数
        /// </summary>
        [DataMember]
        public int WorkDays { get; set; }

        /// <summary>
        /// 休暇日数
        /// </summary>
        [DataMember]
        public int HoliDays { get; set; }

        /// <summary>
        /// 船の表示順
        /// </summary>
        [DataMember]
        public int VesselShowOrder { get; set; }

        /// <summary>
        /// 職名の表示順
        /// </summary>
        [DataMember]
        public int ShokumeiShowOrder { get; set; }


        /// <summary>
        /// 積荷経験
        /// </summary>
        [DataMember]
        public List<SiExperienceCargo> experienceCargos { get; set; }

        /// <summary>
        /// 外航経験
        /// </summary>
        [DataMember]
        public List<SiExperienceForeign> experienceForeigns { get; set; }

        /// <summary>
        /// 交代者情報
        /// </summary>
        [DataMember]
        public SiBoardingSchedule BoardingSchedule { get; set; }

        #endregion



        public static List<配乗シミュレーション> BLC_下船者検索(MsUser loginUser, SeninTableCache seninTableCache, DateTime baseDate, int vesselId, int shokumeiId, int days, bool personalScheduleCheck)
        {
            Dictionary<int, int> vesselShowOrderDic = new Dictionary<int, int>();
            foreach(MsVessel vessel in seninTableCache.GetMsVesselList(loginUser))
            {
                vesselShowOrderDic.Add(vessel.MsVesselID, vessel.ShowOrder);
            }
            Dictionary<int, int> shokumeiShowOrderDic = new Dictionary<int, int>();
            foreach (MsSiShokumei shokumei in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                shokumeiShowOrderDic.Add(shokumei.MsSiShokumeiID, shokumei.ShowOrder);
            }

            List<SiBoardingSchedule> boardingScheduleList = SiBoardingSchedule.GetRecordsByPlan(null, loginUser);

            // 現在の配乗データ
            SiCardFilter filter = new SiCardFilter();
            filter.IncludeNullVessel = true;
            filter.Start = filter.End = DateTime.Now;
            filter.RetireFlag = 0;
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            // 船指定されている場合
            if (vesselId > 0)
            {
                filter.MsVesselIDs.Add(vesselId);
            }
            filter.includeSchedule = false;
            SiHaijou haijou = 船員.BLC_配乗表作成(loginUser, seninTableCache, filter);

            // 乗船のもの
            var allHaijouItems = haijou.SiHaijouItems.Where(obj => obj.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            var haijouItems = allHaijouItems;

            //// 船指定されている場合（配乗表作成時にフィルタリングされているのでここではやらない）
            //if (vesselId > 0)
            //{
            //    haijouItems = haijouItems.Where(obj => obj.MsVesselID == vesselId);
            //}
            // 職名指定されている場合
            if (shokumeiId > 0)
            {
                haijouItems = haijouItems.Where(obj => obj.MsSiShokumeiID == shokumeiId);
            }

            // 乗船日数が指定日以上のもの
            int plusDays = int.Parse(StringUtils.ToStr(DateTime.Today, baseDate));　// 基準日までの日数
            haijouItems = haijouItems.Where(obj => obj.WorkDays + plusDays > days);　// 乗船日数（今日まで）＋　基準日までの日数　が　指定日数以上のもの

            // 個人予定チェックされている場合
            if (personalScheduleCheck)
            {
                // 基準日(baseDate)が乗船不可（個人予定）が含まれている場合、対象とする
                List<SiPersonalSchedule> personalSchedules = SiPersonalSchedule.SearchRecords(loginUser, shokumeiId, null, baseDate, baseDate);
                var NGSeninIds = personalSchedules.Select(obj => obj.MsSeninID);

                var signOffItems = allHaijouItems.Where(obj => NGSeninIds.Contains(obj.MsSeninID));

                haijouItems = haijouItems.Union(signOffItems);
            }

            List<配乗シミュレーション> ret = new List<配乗シミュレーション>();
            foreach(SiHaijouItem item in haijouItems)
            {
                配乗シミュレーション info = new 配乗シミュレーション();

                info.SiCardID = item.SiCardID;
                info.MsSeninID = item.MsSeninID;
                info.SeninName = item.SeninName;
                info.MsSiShokumeiID = item.MsSiShokumeiID;
                info.MsSiShubetsuID = item.MsSiShubetsuID;
                info.MsVesselID = item.MsVesselID;
                info.WorkDays = item.WorkDays + plusDays;

                // 他に必要な情報があれば、追加する
                info.VesselShowOrder = vesselShowOrderDic[item.MsVesselID];
                info.ShokumeiShowOrder = shokumeiShowOrderDic[item.MsSiShokumeiID];

                // 交代者情報
                if (boardingScheduleList.Any(obj => obj.SignOffSiCardID == item.SiCardID))
                {
                    info.BoardingSchedule = boardingScheduleList.Where(obj => obj.SignOffSiCardID == item.SiCardID).First();
                }

                ret.Add(info);
            }

            return ret.OrderBy(obj => obj.VesselShowOrder).ThenBy(obj => obj.ShokumeiShowOrder).ThenByDescending(obj => obj.WorkDays).ToList();   
        }



        public static List<配乗シミュレーション> BLC_乗船者検索(MsUser loginUser, SeninTableCache seninTableCache, DateTime baseDate, int vesselId, int shokumeiId, int days, 
                                                                List<int> shubetsuIds,
                                                                bool vesselCheck,
                                                                bool shokumeiCheck,
                                                                bool menjouCheck,
                                                                bool koushuCheck,
                                                                bool personalScheduleCheck,
                                                                bool fellowPassengersCheck,
                                                                bool experienceCheck)
        {
            List<SiExperienceCargo> experienceCargoList = SiExperienceCargo.GetRecords(loginUser);
            List<SiExperienceForeign> experienceForeignList = SiExperienceForeign.GetRecords(loginUser);
            List<SiBoardingSchedule> boardingScheduleList = SiBoardingSchedule.GetRecordsByPlan(null, loginUser);

            // 現在の配乗データ
            SiCardFilter filter = new SiCardFilter();
            filter.IncludeNullVessel = true;
            filter.Start = filter.End = DateTime.Now;
            filter.RetireFlag = 0;
            foreach (int id in shubetsuIds)
            {
                filter.MsSiShubetsuIDs.Add(id);
            }

            List<SiHaijouItem> allHaijouItems = 船員.BLC_乗船候補者検索(loginUser, seninTableCache, filter);


            var haijouItems = allHaijouItems.Select(obj => obj);

            // 休暇日数が指定日以上のもの
            int plusDays = 0;
            if (days != 0)
            {
                plusDays = int.Parse(StringUtils.ToStr(DateTime.Today, baseDate));　// 基準日までの日数
                haijouItems = haijouItems.Where(obj => obj.WorkDays + plusDays > days);　// 休暇日数（今日まで）＋　基準日までの日数　が　指定日数以上のもの
            }

            // 乗船が指定されている場合
            if (shubetsuIds.Contains(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船)))
            {
                var boardingItems = allHaijouItems.Where(obj => obj.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));

                haijouItems = haijouItems.Union(boardingItems);
            }



            // 船指定されている場合(船が指定されている場合、乗船中の船員が対象　⇒　転船させる候補者を検索）
            if (vesselCheck && vesselId > 0)
            {
                haijouItems = haijouItems.Where(obj => obj.MsVesselID == vesselId);
            }

            // 職名指定されている場合
            if (shokumeiCheck && shokumeiId > 0)
            {
                haijouItems = haijouItems.Where(obj => obj.MsSiShokumeiID == shokumeiId);
            }


            // 免許/免状チェックされている場合
            // チェックされていても、船、職名が指定されていない場合、免許/免状の乗船資格を確認できない
            if (menjouCheck && vesselId > 0 && shokumeiId > 0)
            {
                List<MsVesselRankLicense> allVesselRankLicenses = MsVesselRankLicense.GetRecords(loginUser);
                var vesselRankLicenses = allVesselRankLicenses.Where(obj => obj.MsVesselID == vesselId && obj.MsSiShokumeiID == shokumeiId);
                if (vesselRankLicenses.Count() > 0)
                {
                    SiMenjouFilter menjouFilter = new SiMenjouFilter();
                    foreach(MsVesselRankLicense license in vesselRankLicenses)
                    {
                        if (license.MsSiMenjouKindID > 0)
                        {
                            if (menjouFilter.MsSiMenjouIDs.Contains(license.MsSiMenjouID) == false)
                                menjouFilter.MsSiMenjouIDs.Add(license.MsSiMenjouID);

                            menjouFilter.MsSiMenjouKindIDs.Add(license.MsSiMenjouKindID);
                        }
                        else
                        {
                            menjouFilter.MsSiMenjouIDs.Add(license.MsSiMenjouID);
                        }
                    }
                    menjouFilter.MsSiShokumeiIDs.Add(shokumeiId);
                    menjouFilter.is取得済 = true;
                    menjouFilter.Is退職者を除く = true;

                    List<SiMenjou> menjous = 免許免状管理.BLC_免許免状管理_検索(loginUser, seninTableCache, menjouFilter);

                    var OkSeninIds = menjous.Select(obj => obj.MsSeninID).Distinct();

                    haijouItems = haijouItems.Where(obj => OkSeninIds.Contains(obj.MsSeninID));
                }
            }

            // 講習チェックされている場合
            if (koushuCheck)
            {
                SiKoushuFilter koushuFilter = new SiKoushuFilter();
                koushuFilter.MsSiShokumeiID = shokumeiId;
                koushuFilter.YoteiFrom = baseDate;
                koushuFilter.YoteiTo = baseDate.AddDays(90);

                List<SiKoushu> koushuList = 講習管理.BLC_講習管理_検索(loginUser, seninTableCache, koushuFilter);

                var NGSeninIds = koushuList.Select(obj => obj.MsSeninID).Distinct();

                haijouItems = haijouItems.Where(obj => NGSeninIds.Contains(obj.MsSeninID) == false);
            }

            // 個人予定チェックされている場合
            if (personalScheduleCheck)
            {
                // 基準日(baseDate)から９０日（基本の乗船日数）の間に乗船不可（個人予定）が含まれている場合、対象外とする
                List<SiPersonalSchedule> personalSchedules = SiPersonalSchedule.SearchRecords(loginUser, shokumeiId, null, baseDate, baseDate.AddDays(90));
                var NGSeninIds = personalSchedules.Select(obj => obj.MsSeninID);

                haijouItems = haijouItems.Where(obj => NGSeninIds.Contains(obj.MsSeninID) == false);
            }

            // 乗り合わせチェックされている場合
            if (fellowPassengersCheck)
            {
                // 乗り合わせ情報
                List<SiFellowPassengers> fellowPassengers = SiFellowPassengers.GetRecords(loginUser);

                // 基準日時点で乗船している船員
                SiCardFilter cardFilter = new SiCardFilter();
                cardFilter.MsVesselIDs.Add(vesselId);
                cardFilter.Start = cardFilter.End = baseDate;
                List<SiCard> boardingCards = SiCard.GetRecordsByFilter(loginUser, cardFilter);


                List<int> NGSeninIds = new List<int>();
                foreach(SiFellowPassengers fp in fellowPassengers)
                {
                    if (haijouItems.Any(obj => obj.MsSeninID == fp.MsSeninID1) && boardingCards.Any(obj => obj.MsSeninID == fp.MsSeninID2))
                    {
                        NGSeninIds.Add(fp.MsSeninID1);
                    }
                    if (haijouItems.Any(obj => obj.MsSeninID == fp.MsSeninID2) && boardingCards.Any(obj => obj.MsSeninID == fp.MsSeninID1))
                    {
                        NGSeninIds.Add(fp.MsSeninID2);
                    }
                }
                haijouItems = haijouItems.Where(obj => NGSeninIds.Contains(obj.MsSeninID) == false);
            }

            // 経験チェックされている場合
            if (experienceCheck)
            {
                List<int> OKSeninId = new List<int>();

                List<MsBoardingExperience> boardingExperiences = MsBoardingExperience.GetRecords(null, loginUser);

                // 乗船経験
                var tmp1 = boardingExperiences.Where(obj => obj.Kubun == 1 && obj.MsSiShokumeiID == shokumeiId && obj.MsVesselID == vesselId);
                if (tmp1.Count() > 0)
                {


                }

                // 積荷経験
                var tmp2 = boardingExperiences.Where(obj => obj.Kubun == 2 && obj.MsSiShokumeiID == shokumeiId && obj.MsVesselID == vesselId);
                if (tmp2.Count() > 0)
                {
                    var list = experienceCargoList.Where(obj => obj.MsSiShokumeiID == shokumeiId);
                    foreach(MsBoardingExperience be in tmp2)
                    {
                        var work = list.Where(obj => obj.MsCargoGroupID == be.MsCargoGroupID && obj.Count >= be.Count).Select(obj => obj.MsSeninID);
                        if (work.Count() > 0)
                        {
                            if (OKSeninId.Count() == 0)
                            {
                                OKSeninId = work.ToList();
                            }
                            else
                            {
                                var work1 = OKSeninId.Select(obj => obj);
                                var work2 = work1.Intersect(work);
                                OKSeninId = work2.ToList();
                            }
                        }
                    }
                }

                // 外航経験
                var tmp3 = boardingExperiences.Where(obj => obj.Kubun == 3 && obj.MsSiShokumeiID == shokumeiId);
                if (tmp3.Count() > 0)
                {
                    var list = experienceForeignList.Where(obj => obj.MsSiShokumeiID == shokumeiId);
                    foreach (MsBoardingExperience be in tmp3)
                    {
                        var work = list.Where(obj => obj.Count >= be.Count).Select(obj => obj.MsSeninID);
                        if (work.Count() > 0)
                        {
                            if (OKSeninId.Count() == 0)
                            {
                                OKSeninId = work.ToList();
                            }
                            else
                            {
                                var work1 = OKSeninId.Select(obj => obj);
                                var work2 = work1.Intersect(work);
                                OKSeninId = work2.ToList();
                            }
                        }
                    }
                }

                if (tmp1.Count() > 0 || tmp2.Count() > 0 || tmp3.Count() > 0)
                {
                    haijouItems = haijouItems.Where(obj => OKSeninId.Contains(obj.MsSeninID));
                }
            }

            List<配乗シミュレーション> ret = new List<配乗シミュレーション>();
            foreach (SiHaijouItem item in haijouItems)
            {
                配乗シミュレーション info = new 配乗シミュレーション();

                info.SiCardID = item.SiCardID;
                info.MsSeninID = item.MsSeninID;
                info.SeninName = item.SeninName;
                info.MsSiShokumeiID = item.MsSiShokumeiID;
                info.MsSiShubetsuID = item.MsSiShubetsuID;
                info.MsVesselID = item.MsVesselID;
                info.HoliDays = item.WorkDays + plusDays;

                // 他に必要な情報があれば、追加する
                if (experienceCargoList.Any(obj => obj.MsSeninID == item.MsSeninID && obj.MsSiShokumeiID == item.MsSiShokumeiID))
                {
                    info.experienceCargos = experienceCargoList.Where(obj => obj.MsSeninID == item.MsSeninID && obj.MsSiShokumeiID == item.MsSiShokumeiID).OrderBy(obj => obj.MsCargoGroupID).ToList();
                }
                if (experienceForeignList.Any(obj => obj.MsSeninID == item.MsSeninID && obj.MsSiShokumeiID == item.MsSiShokumeiID))
                {
                    info.experienceForeigns = experienceForeignList.Where(obj => obj.MsSeninID == item.MsSeninID && obj.MsSiShokumeiID == item.MsSiShokumeiID).OrderBy(obj => obj.C5_Flag).ToList();
                }

                // 交代者情報
                if (boardingScheduleList.Any(obj => obj.MsSeninID == item.MsSeninID))
                {
                    info.BoardingSchedule = boardingScheduleList.Where(obj => obj.MsSeninID == item.MsSeninID).First();
                }

                ret.Add(info);
            }

            return ret.OrderBy(obj => obj.VesselShowOrder).ThenBy(obj => obj.ShokumeiShowOrder).ThenByDescending(obj => obj.WorkDays).ToList();
        }
    }
}
