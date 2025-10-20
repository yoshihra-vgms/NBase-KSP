using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        //List<NBaseData.DAC.HoukokushoKanriKiroku> HoukokushoKanriKiroku_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, bool kaicho_shacho, bool sekininsha);
        List<NBaseData.DAC.HoukokushoKanriKiroku> HoukokushoKanriKiroku_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, int role);
    }

    public partial class Service
    {
        //public List<NBaseData.DAC.HoukokushoKanriKiroku> HoukokushoKanriKiroku_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, bool kaicho_shacho, bool sekininsha)
        //{
        //    return NBaseData.DAC.HoukokushoKanriKiroku.SearchRecords(loginUser, bunruiId, shoubunruiId, bunshoNo, bunshoName, userId, kaicho_shacho, sekininsha);
        //}
        public List<NBaseData.DAC.HoukokushoKanriKiroku> HoukokushoKanriKiroku_SearchRecords(MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName, string userId, int role)
        {
            return NBaseData.DAC.HoukokushoKanriKiroku.SearchRecords(loginUser, bunruiId, shoubunruiId, bunshoNo, bunshoName, userId, role);
        }
    }
}
