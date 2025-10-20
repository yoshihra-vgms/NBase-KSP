using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseHonsen
{
    class MasterTable
    {
        private static readonly MasterTable INSTANCE = new MasterTable();

        private List<MsTani> msTaniList;
        public List<MsTani> MsTaniList
        {
            get
            {
                if (msTaniList == null)
                {
                    msTaniList = MsTani.GetRecords(NBaseCommon.Common.LoginUser);

                    // 空のレコード.
                    MsTani t = new MsTani();
                    t.MsTaniID = string.Empty;
                    t.TaniName = string.Empty;
                    msTaniList.Insert(0, t);
                }
                
                return msTaniList;
            }
        }
        
        private Dictionary<string, MsTani> msTaniDic;
        
        private MasterTable()
        {
        }

        public static MasterTable instance()
        {
            return INSTANCE;
        }

        public MsTani GetMsTani(string msTaniId)
        {
            if (msTaniDic == null)
            {
                msTaniDic = new Dictionary<string, MsTani>();
                foreach (MsTani t in MsTaniList)
                {
                    msTaniDic.Add(t.MsTaniID, t);
                }
            }

            return msTaniDic[msTaniId];
        }
    }
}
