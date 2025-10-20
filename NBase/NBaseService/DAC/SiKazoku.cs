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
        List<SiKazoku> SiKazoku_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId);

        [OperationContract]
        List<SiKazoku> SiKazoku_InsertOrUpdate(MsUser loginUser, int msSeninId, List<SiKazoku> kazokus);
    }

    public partial class Service
    {
        public List<SiKazoku> SiKazoku_GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            List<SiKazoku> retKazokus = SiKazoku.GetRecordsByMsSeninID(loginUser, msSeninId);
            if (retKazokus.Count > 0)
            {
                retKazokus = retKazokus.OrderBy(obj => obj.ShowOrder).ThenBy(obj => obj.SiKazokuID).ToList();
            }
            return retKazokus;
        }
        public List<SiKazoku> SiKazoku_InsertOrUpdate(MsUser loginUser, int msSeninId, List<SiKazoku> kazokus)
        {
            return NBaseData.BLC.船員.BLC_家族表示順序更新(loginUser, msSeninId, kazokus);
        }
    }
}
