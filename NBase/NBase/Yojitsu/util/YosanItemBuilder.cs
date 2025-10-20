using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Yojitsu.util
{
    class YosanItemBuilder
    {
        private YosanItemBuilder()
        {
        }
        

        public static Dictionary<int, Dictionary<string, BgYosanItem>> Build_年(List<BgYosanItem> yosanItems)
        {
            // <MsHimokuID, <Nengetsu, BgYosanItem>>
            var yosanItemDic = new Dictionary<int, Dictionary<string, BgYosanItem>>();

            foreach (BgYosanItem i in yosanItems)
            {
                if (!yosanItemDic.ContainsKey(i.MsHimokuID))
                {
                    yosanItemDic[i.MsHimokuID] = new Dictionary<string, BgYosanItem>();
                }
                
                yosanItemDic[i.MsHimokuID][i.Nengetsu.Trim()] = i;
            }

            return yosanItemDic;
        }
    }
}
