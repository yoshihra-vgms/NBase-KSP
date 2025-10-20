using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class SeninTableCache
    {
        private static readonly SeninTableCache INSTANCE = new SeninTableCache();

        public ISeninDacProxy DacProxy { get; set; }

        private List<MsSiShokumei> msSiShokumeiList;
        private List<MsSiMenjou> msSiMenjouList;
        private List<MsSiMenjouKind> msSiMenjouKindList;
        private List<MsSiShubetsu> msSiShubetsuList;
        private List<MsSiShubetsuShousai> msSiShubetsuShousaiList;
        private List<MsVessel> msVesselList;
        private List<MsCustomer> msCustomerList;
        private List<MsSiHiyouKamoku> msSiHiyouKamokuList;
        private List<MsSiDaikoumoku> msSiDaikoumokuList;
        private List<MsSiMeisai> msSiMeisaiList;
        private List<MsSiMeisai> msSiMeisai削除含むList;
        private List<MsSiKamoku> msSiKamokuList;
        private List<MsSiKoushu> msSiKoushuList;
        private List<MsTax> msTaxList;
        private List<MsCrewMatrixType> msCrewMatrixTypeList;
        private List<MsSiKyuyoTeate> msSiKyuyoTeateList;
        private List<MsSiKyuyoTeateSet> msSiKyuyoTeateSetList;
        private List<MsCargoGroup> msCargoGroupList;


        private List<MsSiAdvancedSearchCondition> msSiAdvancedSearchConditionList;
        private List<MsSiPresentationItem> msSiPresentationItemList;

        private List<MsSeninCompany> msSeninCompanyList;

        private List<MsSiOptions> msSiOptionsList;
        private List<MsSiShokumeiShousai> msSiShokumeiShousaiList;

        private List<Shokumei> shokumeiList;





        public List<MsSiShokumei> GetMsSiShokumeiList(MsUser loginUser)
        {
            if (msSiShokumeiList == null)
            {
                msSiShokumeiList = DacProxy.MsSiShokumei_GetRecords(loginUser);
            }

            return msSiShokumeiList;
        }


        public List<MsSiMenjou> GetMsSiMenjouList(MsUser loginUser)
        {
            if (msSiMenjouList == null)
            {
                msSiMenjouList = DacProxy.MsSiMenjou_GetRecords(loginUser);
            }

            return msSiMenjouList;
        }


        public List<MsSiMenjouKind> GetMsSiMenjouKindList(MsUser loginUser)
        {
            if (msSiMenjouKindList == null)
            {
                msSiMenjouKindList = DacProxy.MsSiMenjouKind_GetRecords(loginUser);
            }

            return msSiMenjouKindList;
        }


        public List<MsSiShubetsu> GetMsSiShubetsuList(MsUser loginUser)
        {
            if (msSiShubetsuList == null)
            {
                msSiShubetsuList = DacProxy.MsSiShubetsu_GetRecords(loginUser);
            }

            return msSiShubetsuList;
        }

        public List<MsSiShubetsuShousai> GetMsSiShubetsuShousaiList(MsUser loginUser)
        {
            if (msSiShubetsuShousaiList == null)
            {
                msSiShubetsuShousaiList = DacProxy.MsSiShubetsuShousai_GetRecords(loginUser);
            }

            return msSiShubetsuShousaiList;
        }

        public List<MsVessel> GetMsVesselList(MsUser loginUser)
        {
            if (msVesselList == null)
            {
                msVesselList = DacProxy.MsVessel_GetRecordsBySeninEnabled(loginUser);
            }

            return msVesselList;
        }


        public List<MsCustomer> GetMsCustomerList(MsUser loginUser)
        {
            if (msCustomerList == null)
            {
                msCustomerList = DacProxy.MsCustomer_GetRecords_代理店(loginUser);
            }

            return msCustomerList;
        }


        public List<MsSiHiyouKamoku> GetMsSiHiyouKamokuList(MsUser loginUser)
        {
            if (msSiHiyouKamokuList == null)
            {
                msSiHiyouKamokuList = DacProxy.MsSiHiyouKamoku_GetRecords(loginUser);
            }

            return msSiHiyouKamokuList;
        }


        public List<MsSiDaikoumoku> GetMsSiDaikoumokuList(MsUser loginUser)
        {
            if (msSiDaikoumokuList == null)
            {
                msSiDaikoumokuList = DacProxy.MsSiDaikoumoku_GetRecords(loginUser);
            }

            return msSiDaikoumokuList;
        }


        public List<MsSiMeisai> GetMsSiMeisaiList(MsUser loginUser)
        {
            if (msSiMeisaiList == null)
            {
                msSiMeisaiList = DacProxy.MsSiMeisai_GetRecords(loginUser);
            }

            return msSiMeisaiList;
        }

        public List<MsSiMeisai> GetMsSiMeisaiList削除を含む(MsUser loginUser)
        {
            if (msSiMeisai削除含むList == null)
            {
                msSiMeisai削除含むList = DacProxy.MsSiMeisai_GetRecords削除を含む(loginUser);
            }

            return msSiMeisai削除含むList;
        }

        public List<MsSiKamoku> GetMsSiKamokuList(MsUser loginUser)
        {
            if (msSiKamokuList == null)
            {
                msSiKamokuList = DacProxy.MsSiKamoku_GetRecords(loginUser);
            }

            return msSiKamokuList;
        }

        public List<MsSiKoushu> GetMsSiKoushuList(MsUser loginUser)
        {
            if (msSiKoushuList == null)
            {
                msSiKoushuList = DacProxy.MsSiKoushu_GetRecords(loginUser);
            }

            return msSiKoushuList;
        }

        public List<MsSeninCompany> GetMsSeninCompanyList(MsUser loginUser)
        {
            if (msSeninCompanyList == null)
            {
                msSeninCompanyList = DacProxy.MsSeninCompany_GetRecords(loginUser);
            }

            return msSeninCompanyList;
        }


        private SeninTableCache()
        {
        }


        public static SeninTableCache instance()
        {
            return INSTANCE;
        }


        public static SeninTableCache instance(bool cached)
        {
            if (cached)
            {
                return INSTANCE;
            }
            else
            {
                return new SeninTableCache();
            }
        }




        //===================================================================
        //
        // 種別（MsSiShubetsu）のIDを特定する
        // 文字列が一致しているもののIDを返すようになっている
        //
        //===================================================================
        #region
        //public int MsSiShubetsu_乗船ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "乗船")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_有給休暇ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "有給休暇")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}




        //public int MsSiShubetsu_艤装員ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "艤装員")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_傷病ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "傷病")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_陸上勤務ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "陸上勤務")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_待機ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "待機")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_休職ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "休職")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}



        //public int MsSiShubetsu_乗船休暇ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "乗船休暇")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_その他ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "その他（講習等）")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}


        //public int MsSiShubetsu_請暇ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "請暇")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_旅行日ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        // 2014.08.06：201408月度改造
        //        //if (s.Name == "旅行日")
        //        if (s.Name == "旅行日(乗下船)")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}


        //public int MsSiShubetsu_旅行日_転船ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "旅行日(転船)")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public int MsSiShubetsu_旅行日_研修講習ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.Name == "旅行日(研修・講習)")
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}


        //public int MsSiShubetsu_休暇買上ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.KyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.休暇買上)
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}


        //public int MsSiShubetsu_本年度休暇日数ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.KyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.本年度休暇日数)
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }
        //    return 0;
        //}


        //public int MsSiShubetsu_前年度休暇繰越日数ID(MsUser loginUser)
        //{
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.KyuukaFlag == (int)MsSiShubetsu.KyuukaFlagKind.前年度休暇繰越日数)
        //        {
        //            return s.MsSiShubetsuID;
        //        }
        //    }

        //    return 0;
        //}

        //public List<int> MsSiShubetsu_休暇IDs(MsUser loginUser)
        //{
        //    List<int> ids = new List<int>();
            
        //    foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
        //    {
        //        if (s.KyuukaFlag == 1)
        //        {
        //            ids.Add(s.MsSiShubetsuID);
        //        }
        //    }

        //    return ids;
        //}

        #endregion



        //===================================================================
        //
        // 免許/免状（MsSiMenjou）のIDを特定する
        // 文字列が一致しているもののIDを返すようになっている
        //
        //===================================================================
        #region
        public int MsSiMenjou_海技免状ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "海技免状")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }


        public int MsSiMenjou_船員手帳ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "船員手帳")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }


        public int MsSiMenjou_健康診断ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "健康診断")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }


        public int MsSiMenjou_当直部員ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "当直部員")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }
        public int MsSiMenjou_無線免許ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "無線許可証")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }

        public int MsSiMenjou_危険物取扱者ID(MsUser loginUser)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.Name == "危険物取扱責任者")
                {
                    return s.MsSiMenjouID;
                }
            }

            return 0;
        }
        #endregion




        public int MsSiShubetsu_GetID(MsUser loginUser, MsSiShubetsu.SiShubetsu shubetsu)
        {
            return (int)shubetsu;
        }


        public string GetMsSiShokumeiName(MsUser loginUser, int msSiShokumeiId)
        {
            MsSiShokumei s = GetMsSiShokumei(loginUser, msSiShokumeiId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public string GetMsSiShokumeiNameAbbr(MsUser loginUser, int msSiShokumeiId)
        {
            MsSiShokumei s = GetMsSiShokumei(loginUser, msSiShokumeiId);

            if (s != null)
            {
                return s.NameAbbr;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetMsSiShokumeiNameEng(MsUser loginUser, int msSiShokumeiId)
        {
            MsSiShokumei s = GetMsSiShokumei(loginUser, msSiShokumeiId);

            if (s != null)
            {
                return s.NameEng;
            }
            else
            {
                return string.Empty;
            }
        }

        public MsSiShokumei GetMsSiShokumei(MsUser loginUser, int msSiShokumeiId)
        {
            foreach (MsSiShokumei s in GetMsSiShokumeiList(loginUser))
            {
                if (s.MsSiShokumeiID == msSiShokumeiId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiShubetsuName(MsUser loginUser, int msSiShubetsuId)
        {
            MsSiShubetsu s = GetMsSiShubetsu(loginUser, msSiShubetsuId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiShubetsu GetMsSiShubetsu(MsUser loginUser, int msSiShubetsuId)
        {
            foreach (MsSiShubetsu s in GetMsSiShubetsuList(loginUser))
            {
                if (s.MsSiShubetsuID == msSiShubetsuId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiShubetsuShousaiName(MsUser loginUser, int msSiShubetsuShousaiId)
        {
            MsSiShubetsuShousai s = GetMsSiShubetsuShousai(loginUser, msSiShubetsuShousaiId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiShubetsuShousai GetMsSiShubetsuShousai(MsUser loginUser, int msSiShubetsuShousaiId)
        {
            foreach (MsSiShubetsuShousai s in GetMsSiShubetsuShousaiList(loginUser))
            {
                if (s.MsSiShubetsuShousaiID == msSiShubetsuShousaiId)
                {
                    return s;
                }
            }

            return null;
        }







        public string GetMsVesselName(MsUser loginUser, int msVesselId)
        {
            MsVessel s = GetMsVessel(loginUser, msVesselId);

            if (s != null)
            {
                return s.VesselName;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsVessel GetMsVessel(MsUser loginUser, int msVesselId)
        {
            foreach (MsVessel s in GetMsVesselList(loginUser))
            {
                if (s.MsVesselID == msVesselId)
                {
                    return s;
                }
            }

            return null;
        }

        public string GetMsSiMenjouName(MsUser loginUser, int msSiMenjouId)
        {
            MsSiMenjou s = GetMsSiMenjou(loginUser, msSiMenjouId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiMenjou GetMsSiMenjou(MsUser loginUser, int msSiMenjouId)
        {
            foreach (MsSiMenjou s in GetMsSiMenjouList(loginUser))
            {
                if (s.MsSiMenjouID == msSiMenjouId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiMenjouKindName(MsUser loginUser, int msSiMenjouKindId)
        {
            MsSiMenjouKind s = GetMsSiMenjouKind(loginUser, msSiMenjouKindId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiMenjouKind GetMsSiMenjouKind(MsUser loginUser, int msSiMenjouKindId)
        {
            foreach (MsSiMenjouKind s in GetMsSiMenjouKindList(loginUser))
            {
                if (s.MsSiMenjouKindID == msSiMenjouKindId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiHiyouKamokuName(MsUser loginUser, int msSiHiyouKamokuId)
        {
            MsSiHiyouKamoku s = GetMsSiHiyouKamoku(loginUser, msSiHiyouKamokuId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiHiyouKamoku GetMsSiHiyouKamoku(MsUser loginUser, int msSiHiyouKamokuId)
        {
            foreach (MsSiHiyouKamoku s in GetMsSiHiyouKamokuList(loginUser))
            {
                if (s.MsSiHiyouKamokuID == msSiHiyouKamokuId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiDaikoumokuName(MsUser loginUser, int msSiDaikoumokuId)
        {
            MsSiDaikoumoku s = GetMsSiDaikoumoku(loginUser, msSiDaikoumokuId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiDaikoumoku GetMsSiDaikoumoku(MsUser loginUser, int msSiDaikoumokuId)
        {
            foreach (MsSiDaikoumoku s in GetMsSiDaikoumokuList(loginUser))
            {
                if (s.MsSiDaikoumokuID == msSiDaikoumokuId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiMeisaiName(MsUser loginUser, int msSiMeisaiId)
        {
            MsSiMeisai s = GetMsSiMeisai(loginUser, msSiMeisaiId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public MsSiMeisai GetMsSiMeisai(MsUser loginUser, int msSiMeisaiId)
        {
            // 2010.06.29 削除済みのものも対象とするように修正
            //foreach (MsSiMeisai s in GetMsSiMeisaiList(loginUser))
            foreach (MsSiMeisai s in GetMsSiMeisaiList削除を含む(loginUser))
            {
                if (s.MsSiMeisaiID == msSiMeisaiId)
                {
                    return s;
                }
            }

            return null;
        }


        public MsSiKamoku GetMsSiKamoku(MsUser loginUser, int msSiKamokuId)
        {
            foreach (MsSiKamoku s in GetMsSiKamokuList(loginUser))
            {
                if (s.MsSiKamokuId == msSiKamokuId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiKamokuName(MsUser loginUser, int msSiKamokuId)
        {
            MsSiKamoku s = GetMsSiKamoku(loginUser, msSiKamokuId);

            if (s != null)
            {
                return s.KamokuName;
            }
            else
            {
                return string.Empty;
            }
        }

        public MsSiKoushu GetMsSiKoushu(MsUser loginUser, int msSiKoushuId)
        {
            foreach (MsSiKoushu s in GetMsSiKoushuList(loginUser))
            {
                if (s.MsSiKoushuID == msSiKoushuId)
                {
                    return s;
                }
            }

            return null;
        }


        public string GetMsSiKoushuName(MsUser loginUser, int msSiKoushuId)
        {
            MsSiKoushu s = GetMsSiKoushu(loginUser, msSiKoushuId);

            if (s != null)
            {
                return s.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public List<MsSiMenjouKind> GetMsSiMenjouKindList(MsUser loginUser, int msSiMenjouId)
        {
            List<MsSiMenjouKind> result = new List<MsSiMenjouKind>();
            
            foreach (MsSiMenjouKind k in GetMsSiMenjouKindList(loginUser))
            {
                if (k.MsSiMenjouID == msSiMenjouId)
                {
                    result.Add(k);
                }
            }

            return result;
        }


        public string ToShokumeiStr(MsUser loginUser, List<SiLinkShokumeiCard> links)
        {
            string result = string.Empty;

            foreach (SiLinkShokumeiCard link in links)
            {
                if (result.Length > 0)
                {
                    result += "・";
                }

                result += GetMsSiShokumeiName(loginUser, link.MsSiShokumeiID);
            }

            return result;
        }


        public string ToShokumeiAbbrStr(MsUser loginUser, List<SiLinkShokumeiCard> links)
        {
            string result = string.Empty;

            foreach (SiLinkShokumeiCard link in links)
            {
                if (result.Length > 0)
                {
                    result += "・";
                }

                result += GetMsSiShokumeiNameAbbr(loginUser, link.MsSiShokumeiID);
            }

            return result;
        }


        public string ToTopShokumeiAbbrStr(MsUser loginUser, List<SiLinkShokumeiCard> links)
        {
            string result = string.Empty;

            if (links.Count > 0)
            {
                result = GetMsSiShokumeiNameAbbr(loginUser, links[0].MsSiShokumeiID);
            }

            return result;
        }


        public class SiCardComparer : IComparer<SiCard>
        {
            #region IComparer<SiCard> メンバ

            public int Compare(SiCard x, SiCard y)
            {
                if (x.SiLinkShokumeiCards.Count == 0 || y.SiLinkShokumeiCards.Count == 0)
                {
                    if (x.SiLinkShokumeiCards.Count == 0 && y.SiLinkShokumeiCards.Count != 0)
                    {
                        return 1;
                    }

                    if (x.SiLinkShokumeiCards.Count != 0 && y.SiLinkShokumeiCards.Count == 0)
                    {
                        return -1;
                    }

                    if (x.SiLinkShokumeiCards.Count == 0 && y.SiLinkShokumeiCards.Count == 0)
                    {
                        return 0;
                    }
                }

                if (x.SiLinkShokumeiCards[0].ShowOrder < y.SiLinkShokumeiCards[0].ShowOrder)
                {
                    return -1;
                }

                if (x.SiLinkShokumeiCards[0].ShowOrder > y.SiLinkShokumeiCards[0].ShowOrder)
                {
                    return 1;
                }

                return 0;
            }

            #endregion
        }




        public bool Is_乗船(MsUser loginUser, int msSiShubetsuId)
        {
            //return msSiShubetsuId == MsSiShubetsu_GetID(loginUser.乗船);
            return msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船);
        }


        public bool Is_乗船中(MsUser loginUser, int msSiShubetsuId)
        {
            //return Is_乗船(loginUser, msSiShubetsuId) || msSiShubetsuId == MsSiShubetsu_GetID(loginUser.乗船休暇);
            return Is_乗船(loginUser, msSiShubetsuId) || msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.乗船休暇);
        }


        public bool Is_休暇管理(MsUser loginUser, int msSiShubetsuId)
        {
            //return msSiShubetsuId == MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.休暇買上) ||
            //         msSiShubetsuId == MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数) ||
            //         msSiShubetsuId == MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数);
            return msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.休暇買上) ||
                     msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.本年度休暇日数) ||
                     msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.前年度休暇繰越日数);
        }

        public bool Is_旅行日(MsUser loginUser, int msSiShubetsuId)
        {
            //bool ret = false;

            //MsSiShubetsu s = GetMsSiShubetsu(loginUser, msSiShubetsuId);
            //if (s.Name == "旅行日(乗下船)" ||
            //    s.Name == "旅行日(研修・講習)" ||
            //    s.Name == "旅行日(転船)")
            //{
            //    ret = true;
            //}

            //return ret;
            return msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.移動日) ||
                     msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.転船) ||
                     msSiShubetsuId == MsSiShubetsu_GetID(loginUser, MsSiShubetsu.SiShubetsu.旅行日);
        }







        public int MsSiKamoku_船用金補給ID(MsUser loginUser)
        {
            foreach (MsSiKamoku s in GetMsSiKamokuList(loginUser))
            {
                if (s.KamokuName == "船用金補給")
                {
                    return s.MsSiKamokuId;
                }
            }

            return 0;
        }

        public int MsSiKamoku_修繕費ID(MsUser loginUser)
        {
            foreach (MsSiKamoku s in GetMsSiKamokuList(loginUser))
            {
                if (s.KamokuName == "修繕費")
                {
                    return s.MsSiKamokuId;
                }
            }

            return 0;
        }

        public int MsSiKamoku_船用品費ID(MsUser loginUser)
        {
            foreach (MsSiKamoku s in GetMsSiKamokuList(loginUser))
            {
                if (s.KamokuName == "船用品費")
                {
                    return s.MsSiKamokuId;
                }
            }

            return 0;
        }



        // 2014.03: 2013年度改造
        public List<MsTax> GetMsTaxList(MsUser loginUser)
        {
            if (msTaxList == null)
            {
                msTaxList = DacProxy.MsTax_GetRecords(loginUser);
            }

            return msTaxList;
        }



        // 2017.12: 2017年度改造
        public List<MsCrewMatrixType> GetMsCrewMatrixTypeList(MsUser loginUser)
        {
            if (msCrewMatrixTypeList == null)
            {
                msCrewMatrixTypeList = DacProxy.MsCrewMatrixType_GetRecords(loginUser);
            }

            return msCrewMatrixTypeList;
        }

        public MsCrewMatrixType GetMsCrewMatrixType(MsUser loginUser, int msCrewMatrixTypeId)
        {
            foreach (MsCrewMatrixType t in GetMsCrewMatrixTypeList(loginUser))
            {
                if (t.MsCrewMatrixTypeID == msCrewMatrixTypeId)
                {
                    return t;
                }
            }

            return null;
        }


        public string GetMsCrewMatrixTypeName(MsUser loginUser, int msCrewMatrixTypeId)
        {
            MsCrewMatrixType t = GetMsCrewMatrixType(loginUser, msCrewMatrixTypeId);

            if (t != null)
            {
                return t.TypeName;
            }
            else
            {
                return string.Empty;
            }
        }


        public List<MsSiKyuyoTeate> GetMsSiKyuyoTeateList(MsUser loginUser)
        {
            if (msSiKyuyoTeateList == null)
            {
                msSiKyuyoTeateList = DacProxy.MsSiKyuyoTeate_GetRecords(loginUser);
            }

            return msSiKyuyoTeateList;
        }

        public MsSiKyuyoTeate GetMsSiKyuyoTeate(MsUser loginUser, int msSiKyuyoTeateId)
        {
            foreach (MsSiKyuyoTeate t in GetMsSiKyuyoTeateList(loginUser))
            {
                if (t.MsSiKyuyoTeateID == msSiKyuyoTeateId)
                {
                    return t;
                }
            }

            return null;
        }

        public string GetMsSiKyuyoTeateName(MsUser loginUser, int msSiKyuyoTeateId)
        {
            MsSiKyuyoTeate t = GetMsSiKyuyoTeate(loginUser, msSiKyuyoTeateId);

            if (t != null)
            {
                return t.Name;
            }
            else
            {
                return string.Empty;
            }
        }


        public List<MsSiKyuyoTeateSet> GetMsSiKyuyoTeateSetList(MsUser loginUser)
        {
            if (msSiKyuyoTeateSetList == null)
            {
                msSiKyuyoTeateSetList = DacProxy.MsSiKyuyoTeateSet_GetRecords(loginUser);
            }

            return msSiKyuyoTeateSetList;
        }

        public MsSiKyuyoTeateSet GetMsSiKyuyoTeateSet(MsUser loginUser, int msSiKyuyoTeateSetId)
        {
            foreach (MsSiKyuyoTeateSet t in GetMsSiKyuyoTeateSetList(loginUser))
            {
                if (t.MsSiKyuyoTeateSetID == msSiKyuyoTeateSetId)
                {
                    return t;
                }
            }

            return null;
        }



        public List<MsCargoGroup> GetMsCargoGroupList(MsUser loginUser)
        {
            if (msCargoGroupList == null)
            {
                msCargoGroupList = DacProxy.MsCargoGroup_GetRecords(loginUser);
            }

            return msCargoGroupList;
        }

        public MsCargoGroup GetMsCargoGroup(MsUser loginUser, int msCargoGroupId)
        {
            foreach (MsCargoGroup o in GetMsCargoGroupList(loginUser))
            {
                if (o.MsCargoGroupID == msCargoGroupId)
                {
                    return o;
                }
            }

            return null;
        }

        public string GetMsCargoGroupName(MsUser loginUser, int msCargoGroupId)
        {
            MsCargoGroup o = GetMsCargoGroup(loginUser, msCargoGroupId);

            if (o != null)
            {
                return o.CargoGroupName;
            }
            else
            {
                return string.Empty;
            }
        }



        //===================================
        // MS_SI_ADVANCEDSEARCH_CONDITION
        #region

        public List<MsSiAdvancedSearchCondition> GetMsSiAdvancedSearchConditionList(MsUser loginUser)
        {
            if (msSiAdvancedSearchConditionList == null)
            {
                msSiAdvancedSearchConditionList = DacProxy.MsSiAdvancedSearchCondition_GetRecords(loginUser);
            }

            return msSiAdvancedSearchConditionList;
        }

        public MsSiAdvancedSearchCondition GetMsSiAdvancedSearchCondition(MsUser loginUser, int msSiAdvancedSearchConditionId)
        {
            foreach (MsSiAdvancedSearchCondition o in GetMsSiAdvancedSearchConditionList(loginUser))
            {
                if (o.MsSiAdvancedSearchConditionID == msSiAdvancedSearchConditionId)
                {
                    return o;
                }
            }

            return null;
        }

        public string GetMsSiAdvancedSearchConditionName(MsUser loginUser, int msSiAdvancedSearchConditionId)
        {
            MsSiAdvancedSearchCondition o = GetMsSiAdvancedSearchCondition(loginUser, msSiAdvancedSearchConditionId);

            if (o != null)
            {
                return o.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        //===================================
        // MS_SI_PRESENTAION_ITEM
        #region

        public List<MsSiPresentationItem> GetMsSiPresentationItemList(MsUser loginUser)
        {
            if (msSiPresentationItemList == null)
            {
                msSiPresentationItemList = DacProxy.MsSiPresentationItem_GetRecords(loginUser);
            }

            return msSiPresentationItemList;
        }

        public MsSiPresentationItem GetMsSiPresentationItem(MsUser loginUser, int msSiPresentationItemId)
        {
            foreach (MsSiPresentationItem o in GetMsSiPresentationItemList(loginUser))
            {
                if (o.MsSiPresentationItemID == msSiPresentationItemId)
                {
                    return o;
                }
            }

            return null;
        }

        public string GetMsSiPresentationItemName(MsUser loginUser, int msSiPresentationItemId)
        {
            MsSiPresentationItem o = GetMsSiPresentationItem(loginUser, msSiPresentationItemId);

            if (o != null)
            {
                return o.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion




        //===================================
        // MS_SI_OPTIONS
        #region


        public List<MsSiOptions> GetMsSiOptionsList(MsUser loginUser, int selecterId)
        {
            if (msSiOptionsList == null)
            {
                msSiOptionsList = GetMsSiOptionsList(loginUser);
            }
            var res = from list in msSiOptionsList
                      where list.SelecterID == selecterId
                      select list;

            return res.ToList<MsSiOptions>();
        }

        public List<MsSiOptions> GetMsSiOptionsList(MsUser loginUser)
        {
            if (msSiOptionsList == null)
            {
                msSiOptionsList = DacProxy.MsSiOptions_GetRecords(loginUser);
            }

            return msSiOptionsList;
        }

        public MsSiOptions GetMsSiOptions(MsUser loginUser, string MsSiOptionsID)
        {
            foreach (MsSiOptions o in GetMsSiOptionsList(loginUser))
            {
                if (o.MsSiOptionsID == MsSiOptionsID)
                {
                    return o;
                }
            }

            return null;
        }

        public string GetMsSiOptionsName(MsUser loginUser, string msSiOptionsId)
        {
            MsSiOptions o = GetMsSiOptions(loginUser, msSiOptionsId);

            if (o != null)
            {
                return o.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion


        //===================================
        // Shokumei
        #region
        public List<Shokumei> GetShokumeiList(MsUser loginUser, int kind)
        {
            if (msSiShokumeiList == null)
            {
                msSiShokumeiList = DacProxy.MsSiShokumei_GetRecords(loginUser);
            }

            if (kind == Shokumei.フェリー)
            {
                if (msSiShokumeiShousaiList == null)
                {
                    msSiShokumeiShousaiList = DacProxy.MsSiShokumeiShousai_GetRecords(loginUser);
                }
            }

            shokumeiList = new List<Shokumei>();

            foreach(MsSiShokumei ms in msSiShokumeiList)
            {
                if (kind == Shokumei.フェリー && msSiShokumeiShousaiList != null && msSiShokumeiShousaiList.Any(o => o.MsSiShokumeiID == ms.MsSiShokumeiID))
                {
                    var shousaiList = msSiShokumeiShousaiList.Where(o => o.MsSiShokumeiID == ms.MsSiShokumeiID);
                    foreach(var mss in shousaiList)
                    {
                        Shokumei s = new Shokumei();

                        s.MsSiShokumeiID = mss.MsSiShokumeiID;
                        s.MsSiShokumeiShousaiID = mss.MsSiShokumeiShousaiID;
                        s.Name = mss.Name;
                        s.NameAbbr = mss.NameAbbr;
                        s.NameEng = mss.NameEng;
                        s.ShowOrder = mss.ShowOrder;

                        shokumeiList.Add(s);
                    }
                }
                else
                {
                    Shokumei s = new Shokumei();

                    s.MsSiShokumeiID = ms.MsSiShokumeiID;
                    s.Name = ms.Name;
                    s.NameAbbr = ms.NameAbbr;
                    s.NameEng = ms.NameEng;
                    s.ShowOrder = ms.ShowOrder;

                    shokumeiList.Add(s);
                }
            }

            return shokumeiList;
        }

        public Shokumei GetShokumei(MsUser loginUser, int kind, int msSiShokumeiId, int msSiShokumeiShousaiId)
        {
            foreach (Shokumei s in GetShokumeiList(loginUser, kind))
            {
                if (s.MsSiShokumeiID == msSiShokumeiId && s.MsSiShokumeiShousaiID == msSiShokumeiShousaiId)
                {
                    return s;
                }
            }

            return null;
        }
        #endregion


        //===================================
        // MS_BASHO
        #region

        private List<MsBasho> msBashoList;

        public List<MsBasho> GetMsBashoList(MsUser loginUser)
        {
            if (msBashoList == null)
            {
                msBashoList = DacProxy.MsBasho_GetRecords(loginUser);
            }

            return msBashoList;
        }

        public MsBasho GetMsBasho(MsUser loginUser, string msBashoId)
        {
            foreach (MsBasho r in GetMsBashoList(loginUser))
            {
                if (r.MsBashoId == msBashoId)
                {
                    return r;
                }
            }

            return null;
        }

        public string GetMsBashoName(MsUser loginUser, string msBashoId)
        {
            MsBasho r = GetMsBasho(loginUser, msBashoId);

            if (r != null)
            {
                return r.BashoName;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion



        //===================================
        // MS_SI_ALLOWANCE
        #region

        private List<MsSiAllowance> msSiAllowanceList;

        public List<MsSiAllowance> GetMsSiAllowanceList(MsUser loginUser)
        {
            if (msBashoList == null)
            {
                msSiAllowanceList = DacProxy.MsSiAllowance_GetRecords(loginUser);
            }

            return msSiAllowanceList;
        }

        public MsSiAllowance GetMsSiAllowance(MsUser loginUser, int msSiAllowanceId)
        {
            foreach (MsSiAllowance o in GetMsSiAllowanceList(loginUser))
            {
                if (o.MsSiAllowanceID == msSiAllowanceId)
                {
                    return o;
                }
            }

            return null;
        }

        public string GetMsSiAllowanceName(MsUser loginUser, int msSiAllowanceId)
        {
            MsSiAllowance o = GetMsSiAllowance(loginUser, msSiAllowanceId);

            if (o != null)
            {
                return o.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
