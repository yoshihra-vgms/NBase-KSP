using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.SiKyuyoTeate> SiKyuyoTeate_SearchRecords(NBaseData.DAC.MsUser loginUser, int vesselId, string ym);

        [OperationContract]
        NBaseData.DAC.SiKyuyoTeate SiKyuyoTeate_InsertOrUpdate(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.SiKyuyoTeate k);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.SiKyuyoTeate> SiKyuyoTeate_SearchRecords(NBaseData.DAC.MsUser loginUser, int vesselId, string ym)
        {
            List<NBaseData.DAC.SiKyuyoTeate> all = NBaseData.DAC.SiKyuyoTeate.GetRecords(loginUser);

            var ret = all;
            if (vesselId > 0)
            {
                ret = ret.Where(obj => obj.MsVesselID == vesselId).ToList();
            }
            if (ym != null)
            {
                if (ret.Any(obj => obj.YM == ym))
                {
                    ret = ret.Where(obj => obj.YM == ym).ToList();
                }
                else
                {
                    // 指定月のデータがない場合、１ヵ月前のデータをコピーする
                    string lastMonth = DateTime.Parse(ym.Substring(0, 4) + "/" + ym.Substring(4, 2) + "/01").AddMonths(-1).ToString("yyyyMM");
                    var tmp = ret.Where(obj => obj.YM == lastMonth).ToList();
                    ret.Clear();

                    foreach(NBaseData.DAC.SiKyuyoTeate info in tmp)
                    {
                        info.StartDate = DateTime.MinValue;
                        info.EndDate = DateTime.MinValue;
                        info.Days = int.MinValue;
                        info.HonsenKingaku = int.MinValue;
                        info.Kingaku = int.MinValue;
                        ret.Add(info);
                    }
                }
            }           
            
            return ret.ToList();
        }

        public NBaseData.DAC.SiKyuyoTeate SiKyuyoTeate_InsertOrUpdate(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.SiKyuyoTeate k)
        {
            bool ret = true;
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if (k.IsNew())
            {
                k.SiKyuyoTeateID = System.Guid.NewGuid().ToString();
                ret = k.InsertRecord(loginUser);
            }
            else
            {
                ret = k.UpdateRecord(loginUser);
            }
            if (ret)
            {
                if (k.DeleteFlag == 1)
                {
                    return k;
                }
                else
                {
                    NBaseData.DAC.SiKyuyoTeate retKoushu = NBaseData.DAC.SiKyuyoTeate.GetRecord(loginUser, k.SiKyuyoTeateID);
                    return retKoushu;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
