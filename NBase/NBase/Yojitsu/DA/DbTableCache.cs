using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Yojitsu.DA
{
    class DbTableCache
    {
        private static readonly DbTableCache INSTANCE = new DbTableCache();

        private List<MsBumon> msBumonList;
        public List<MsBumon> MsBumonList
        {
            get
            {
                if (msBumonList == null)
                {
                    msBumonList = DbAccessorFactory.FACTORY.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);

                    // 空のレコード.
                    MsBumon b = new MsBumon();
                    b.MsBumonID = string.Empty;
                    msBumonList.Insert(0, b);
                }
                
                return msBumonList;
            }
        }

        private List<MsUserBumon> msUserBumonList;
        public List<MsUserBumon> MsUserBumonList
        {
            get
            {
                if (msUserBumonList == null)
                {
                    msUserBumonList = DbAccessorFactory.FACTORY.MsUserBumon_GetRecordsByUserID(NBaseCommon.Common.LoginUser,
                                                                                               NBaseCommon.Common.LoginUser.MsUserID);
                }

                return msUserBumonList;
            }
        }
        
        private Dictionary<string, MsBumon> msBumonDic;

        private Dictionary<int, List<BgRate>> bgRateDic;
      
        
        private DbTableCache()
        {
        }


        public static DbTableCache instance()
        {
            return INSTANCE;
        }

        
        public MsBumon GetMsBumon(string msBumonId)
        {
            if (msBumonDic == null)
            {
                msBumonDic = new Dictionary<string, MsBumon>();
                foreach (MsBumon t in MsBumonList)
                {
                    msBumonDic.Add(t.MsBumonID, t);
                }
            }

            return msBumonDic[msBumonId];
        }


        public List<BgRate> GetBgRateList(BgYosanHead yosanHead)
        {
            if (bgRateDic == null)
            {
                bgRateDic = new Dictionary<int, List<BgRate>>();
            }

            int yosanHeadId = yosanHead.YosanHeadID;

            if (!bgRateDic.ContainsKey(yosanHeadId))
            {
                bgRateDic[yosanHeadId] = new List<BgRate>();
                
                // 前年度予算
                BgYosanHead preYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecordByYear(NBaseCommon.Common.LoginUser, (yosanHead.Year - 1).ToString());

                if (preYosanHead != null)
                {
                    List<BgRate> preRates = 
                      DbAccessorFactory.FACTORY.
                      BgRate_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser,
                                                    preYosanHead.YosanHeadID
                                                   );
                    bgRateDic[yosanHeadId].Add(preRates[0]);
                }

                bgRateDic[yosanHeadId].AddRange(DbAccessorFactory.FACTORY.BgRate_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser, yosanHeadId));
            }
            
            return bgRateDic[yosanHeadId];
        }


        public void ClearBgRateDic()
        {
            bgRateDic = null;
        }


        public BgNrkKanryou GetBgNrkKanryou(BgYosanHead yosanHead)
        {
            List<BgNrkKanryou> kanryous = DbAccessorFactory.FACTORY.BgNrkKanryou_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser,
                                                                                                         yosanHead.YosanHeadID);

            foreach (BgNrkKanryou k in kanryous)
            {
                if (Constants.BelongMsBumon(k.MsBumonID))
                {
                    return k;
                }
            }

            return null;
        }
    }
}
