using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBaseData.DAC;

namespace NBaseData.DS
{
    /// <summary>
    /// 配乗計画Formの計画、実績を表現するクラス
    /// </summary>
    public class Appointment
    {

        /// <summary>
        /// 配乗計画ID
        /// </summary>
        public string SiCardPlanID { get; set; }

        /// <summary>
        /// 船員ID 
        /// </summary>
        public int MsSeninID { get; set; }

        /// <summary>
        /// 種別ID 
        /// </summary>
        public int MsSiShubetsuID { get; set; }

        /// <summary>
        /// 船ID 
        /// </summary>
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID 
        /// </summary>
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 職名詳細ID 
        /// </summary>
        public int MsSiShokumeiShousaiID { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 交代カード
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// 交代者（船員ID）
        /// </summary>
        public int ReplacementSeninID { get; set; }

        /// <summary>
        /// 交代場所
        /// </summary>
        public string MsBashoID { get; set; }

        /// <summary>
        /// 交代者連携
        /// </summary>
        public bool LinkageReplacement { get; set; }

        ///// <summary>
        ///// 開始日、画面用
        ///// </summary>
        //public DateTime DispStartDate { get; set; }

        ///// <summary>
        ///// 終了日、画面用
        ///// </summary>
        //public DateTime DispEndDate { get; set; }

        /// <summary>
        /// 開始日が午後なら1
        /// </summary>
        public int PmStart { get; set; }

        /// <summary>
        /// 終了日が午後なら1
        /// </summary>
        public int PmEnd { get; set; }

        /// <summary>
        /// ヘッダID
        /// </summary>
        public string SiCardPlanHeadID { get; set; }

        /// <summary>
        /// 締め
        /// </summary>
        public int HeadShimeFlag { get; set; }

        /// <summary> 
        /// 船員名
        /// </summary>
        public string SeninName { get; set; }


        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName { get; set; }

        /// <summary>
        /// 職名
        /// </summary>
        public string ShokuName { get; set; }

        /// <summary>
        /// 職名略称
        /// </summary>
        public string ShokuNameAbbr { get; set; }

        /// <summary>
        /// 職名(英語)
        /// </summary>
        public string ShokuNameEng { get; set; }

        ///// <summary>
        ///// 表示するかどうか 表示はtrue
        ///// </summary>
        //public bool DispFlg { get; set; }

        /// <summary>
        /// 実績なら1
        /// </summary>
        public int ActFlg { get; set; }

        /// <summary>
        /// (実績で) Endが決まっていない 決まって否ならtrue
        /// </summary>
        public bool OnGoing { get; set; }


        public override string ToString()
        {
            return ShokuNameEng;
        }

        public Appointment()
        {
            this.MsSeninID = 0;
            this.MsSiShubetsuID = 0;
            this.MsVesselID = 0;
            this.MsSiShokumeiID = 0;
            this.MsSiShokumeiShousaiID = 0;
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
            this.PmStart = 0;
            this.PmEnd = 0;
            this.ActFlg = 0;
            this.HeadShimeFlag = 0;
            this.Replacement = "";
            this.MsBashoID = "";

            this.ReplacementSeninID = 0;
            this.LinkageReplacement = true;
        }

        /// <summary>
        // SiCardPlan(計画)クラス→Appointmentクラス
        /// </summary>
        /// <param name="p"></param>
        public Appointment(SiCardPlan p)
        {
            this.SiCardPlanID = p.SiCardPlanID;
            this.SiCardPlanHeadID = p.SiCardPlanHeadID;
            this.HeadShimeFlag = p.HeadShimeFlag;
            this.MsSeninID = p.MsSeninID;
            this.MsSiShubetsuID = p.MsSiShubetsuID;
            this.MsVesselID = p.MsVesselID;
            this.MsSiShokumeiID = p.MsSiShokumeiID;
            this.MsSiShokumeiShousaiID = p.MsSiShokumeiShousaiID;
            this.StartDate = p.StartDate;
            this.EndDate = p.EndDate;
            this.SeninName = p.SeninName;
            this.ActFlg = 0;
            this.PmStart = p.LaborOnBoarding == (int)SiCardPlan.LABOR.半休 ? 1 : 0;
            this.PmEnd = p.LaborOnDisembarking == (int)SiCardPlan.LABOR.半休 ? 1 : 0;
            this.Replacement = p.Replacement;
            this.MsBashoID = p.MsBashoID;

            this.ReplacementSeninID = p.ReplacementSeninID;
            this.LinkageReplacement = p.LinkageReplacement;

            //計画は関係ないけど一応いれておく
            this.OnGoing = false;
        }

        /// <summary>
        // SiCardPlan(実績)クラス→Appointmentクラス
        /// </summary>
        /// <param name="c"></param>
        public Appointment(SiCard c)
        {
            this.MsSeninID = c.MsSeninID;
            this.MsSiShubetsuID = c.MsSiShubetsuID;
            this.MsVesselID = c.MsVesselID;
            this.StartDate = c.StartDate;
            this.EndDate = c.EndDate;
            this.SeninName = c.SeninName;
            this.OnGoing = false;

            //計画は関係ないけど一応いれておく
            this.SiCardPlanID = "";
            this.SiCardPlanHeadID = "";
            this.HeadShimeFlag = 1;

            this.PmStart = c.LaborOnBoarding == (int)SiCard.LABOR.半休 ? 1 : 0;
            this.PmEnd = c.LaborOnDisembarking == (int)SiCard.LABOR.半休 ? 1 : 0;

            this.Replacement = c.ReplacementID;
            this.MsBashoID = c.SignOffBashoID;

            this.ReplacementSeninID = 0; // 配乗計画画面側から設定される値
            this.LinkageReplacement = true; // 配乗計画画面側から設定される値(Defaultはtrue)
        }


        /// <summary>
        /// Appointmentクラス→SiCardPlan(計画)クラス
        /// </summary>
        /// <returns></returns>
        public SiCardPlan MakeSiCardPlan()
        {
            SiCardPlan ret = new SiCardPlan();

            ret.SiCardPlanID = SiCardPlanID;
            ret.SiCardPlanHeadID = SiCardPlanHeadID;
            ret.HeadShimeFlag = HeadShimeFlag;
            ret.MsSeninID = MsSeninID;
            ret.MsSiShubetsuID = MsSiShubetsuID;
            ret.MsVesselID = MsVesselID;
            ret.MsSiShokumeiID = MsSiShokumeiID;
            ret.MsSiShokumeiShousaiID = MsSiShokumeiShousaiID;
            ret.StartDate = StartDate;
            ret.EndDate = EndDate;
            ret.LaborOnBoarding = PmStart == 1 ? (int)SiCardPlan.LABOR.半休 : 0;
            ret.LaborOnDisembarking = PmEnd == 1 ? (int)SiCardPlan.LABOR.半休 : 0;
            ret.Replacement = Replacement;
            ret.MsBashoID = MsBashoID;

            ret.ReplacementSeninID = ReplacementSeninID;
            ret.LinkageReplacement = LinkageReplacement;

            return ret;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public Appointment Clone()
        {
            Appointment clone = new Appointment();

            clone.SiCardPlanID = SiCardPlanID;
            clone.MsSeninID = MsSeninID;
            clone.MsSiShubetsuID = MsSiShubetsuID;
            clone.MsVesselID = MsVesselID;
            clone.MsSiShokumeiID = MsSiShokumeiID;
            clone.MsSiShokumeiShousaiID = MsSiShokumeiShousaiID;
            clone.StartDate = StartDate;
            clone.EndDate = EndDate;
            clone.PmStart = PmStart;
            clone.PmEnd = PmEnd;
            clone.SiCardPlanHeadID = SiCardPlanHeadID;
            clone.HeadShimeFlag = HeadShimeFlag;
            clone.Replacement = Replacement;
            clone.MsBashoID = MsBashoID;

            clone.SeninName = SeninName;
            clone.ShokuName = ShokuName;
            clone.ShokuNameAbbr = ShokuNameAbbr;
            clone.ShokuNameEng = ShokuNameEng;
            clone.VesselName = VesselName;
            clone.ActFlg = ActFlg;
            clone.ReplacementSeninID = ReplacementSeninID;
            clone.LinkageReplacement = LinkageReplacement;

            return clone;
        }
        public bool IsNew()
        {
            return SiCardPlanID == null;
        }
    }

}
