using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        Dictionary<string, 年間管理表受入データ> BLC_貯蔵品年間管理表_潤滑油受入_取得(NBaseData.DAC.MsUser loginUser, int msVesselId, int year);
        
        [OperationContract]
        Dictionary<string, 年間管理表受入データ> BLC_貯蔵品年間管理表_船用品受入_取得(NBaseData.DAC.MsUser loginUser, int msVesselId, int year);
    }

    public partial class Service
    {
        public Dictionary<string, 年間管理表受入データ> BLC_貯蔵品年間管理表_潤滑油受入_取得(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            return 貯蔵品年間管理表データ.GetRecords潤滑油_受入(loginUser, msVesselId, year);
        }

        public Dictionary<string, 年間管理表受入データ> BLC_貯蔵品年間管理表_船用品受入_取得(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            return 貯蔵品年間管理表データ.GetRecords船用品_受入(loginUser, msVesselId, year);
        }
    }
}
