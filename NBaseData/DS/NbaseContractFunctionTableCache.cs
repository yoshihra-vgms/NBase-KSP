using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class NbaseContractFunctionTableCache
    {
        private static readonly NbaseContractFunctionTableCache INSTANCE = new NbaseContractFunctionTableCache();

        public INbaseContractFunctionDacProxy DacProxy { get; set; }

        private List<NbaseContractFunction> NbaseContractFunctionList;

        private Dictionary<string, NbaseContractFunction> NbaseContractFunctionDic;


        private NbaseContractFunctionTableCache()
        {
        }


        public static NbaseContractFunctionTableCache instance()
        {
            return INSTANCE;
        }


        private void LoadRecords(MsUser loginUser)
        {
            if (NbaseContractFunctionList == null)
            {
                NbaseContractFunctionList = DacProxy.NbaseContractFunction_GetRecords(loginUser);
            }

            NbaseContractFunctionDic = new Dictionary<string, NbaseContractFunction>();

            foreach (NbaseContractFunction wingContractFunction in NbaseContractFunctionList)
            {
                string key = wingContractFunction.FunctionName;
                NbaseContractFunctionDic[key] = wingContractFunction;
            }
        }


        public bool IsContract(MsUser loginUser, NbaseContractFunction.EnumFunction enumFunc)
        {
            LoadRecords(loginUser);

            string key = enumFunc.ToString();

            if (!NbaseContractFunctionDic.ContainsKey(key))
            {
                return false;
            }

            return NbaseContractFunctionDic[key].IsContract == 1;
        }

        public int HonsenContractCount(MsUser loginUser)
        {
            LoadRecords(loginUser);

            string key = NbaseContractFunction.EnumFunction.船.ToString();

            if (!NbaseContractFunctionDic.ContainsKey(key))
            {
                return 0;
            }

            return NbaseContractFunctionDic[key].IsContract;
        }
    }
}
