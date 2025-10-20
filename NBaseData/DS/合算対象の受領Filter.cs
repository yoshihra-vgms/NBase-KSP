using System;
using System.Text;
using System.Runtime.Serialization;

namespace NBaseData.DS
{
    [DataContract()]
    public class 合算対象の受領Filter
    {
        [DataMember]
        public string MsCustomerID { get; set; }
        [DataMember]
        public string MsThiIraiSbtID { get; set; }
        [DataMember]
        public string MsThiIraiShousaiID { get; set; }
        [DataMember]
        public int MsVesselID { get; set; }
        [DataMember]
        public DateTime? JryDateFrom { get; set; }
        [DataMember]
        public DateTime? JryDateTo { get; set; }
        
        // チェック時のエラーメッセージ
        public string ErrMsg { get; set; }

        public 合算対象の受領Filter()
        {
            条件クリア();
        }

        public void 条件クリア()
        {
            MsCustomerID = null;
            MsThiIraiSbtID = null;
            MsThiIraiShousaiID = null;
            MsVesselID = int.MinValue;
            JryDateFrom = null;
            JryDateTo = null;
        }

        public bool チェック()
        {
            if (MsCustomerID == null || MsCustomerID.Length == 0)
            {
                ErrMsg = "取引先が選択されていません。";
                return false;
            }
            if (MsThiIraiSbtID == null || MsThiIraiSbtID.Length == 0)
            {
                ErrMsg = "種別が選択されていません。";
                return false;
            }
            if (MsVesselID == int.MinValue)
            {
                ErrMsg = "船が選択されていません。";
                return false;
            }
            if (JryDateFrom != null && JryDateTo != null && JryDateFrom > JryDateTo)
            {
                ErrMsg = "受領日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            return true;
        }
    }
}
