using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.BLC.合算対象の受領> BLC_合算対象の受領_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.合算対象の受領Filter filter);
        
        [OperationContract]
        bool BLC_支払合算処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrGassanHead odShrGassanHead, List<NBaseData.BLC.合算対象の受領> jrys);

        [OperationContract]
        bool BLC_支払合算解除(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrGassanHead odShrGassanHead);

        [OperationContract]
        bool BLC_支払合算_支払依頼作成(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr odShr, NBaseData.DAC.OdShrGassanHead odShrGassanHead, List<NBaseData.DAC.OdShrGassanItem> odShrGassanItems);
    }
    public partial class Service
    {
        public List<NBaseData.BLC.合算対象の受領> BLC_合算対象の受領_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.合算対象の受領Filter filter)
        {
            return NBaseData.BLC.合算対象の受領.GetRecordsByFilter(loginUser, filter);
        }

        public bool BLC_支払合算処理(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrGassanHead odShrGassanHead, List<NBaseData.BLC.合算対象の受領> jrys)
        {
            return NBaseData.BLC.支払合算処理.合算作成(loginUser, odShrGassanHead, jrys);
        }

        public bool BLC_支払合算解除(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrGassanHead odShrGassanHead)
        {
            return NBaseData.BLC.支払合算処理.合算解除(loginUser, odShrGassanHead);
        }

        public bool BLC_支払合算_支払依頼作成(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr odShr, NBaseData.DAC.OdShrGassanHead odShrGassanHead, List<NBaseData.DAC.OdShrGassanItem> odShrGassanItems)
        {
            return NBaseData.BLC.支払合算処理.支払依頼作成(loginUser, odShr, odShrGassanHead, odShrGassanItems);
        }
    }
}
