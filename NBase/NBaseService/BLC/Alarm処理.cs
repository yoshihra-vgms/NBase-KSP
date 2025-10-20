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
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool Alarm処理_検査証書_検査_90日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa);

        [OperationContract]
        bool Alarm処理_検査証書_検査_180日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa);

        //-------------------------------------------
        // 検査
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_検査_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_検査_更新(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_検査_停止(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_検査_削除(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName);

        //-------------------------------------------
        // 証書
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_証書_登録(NBaseData.DAC.MsUser logiuser, KsShousho shousho);
        [OperationContract]
        bool Alarm処理_検査証書_証書_更新(NBaseData.DAC.MsUser logiuser, KsShousho shousho);
        [OperationContract]
        bool Alarm処理_検査証書_証書_停止(NBaseData.DAC.MsUser logiuser, KsShousho shousho);
        [OperationContract]
        bool Alarm処理_検査証書_証書_削除(NBaseData.DAC.MsUser logiuser, KsShousho shousho);


        //-------------------------------------------
        // 審査
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_審査_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_審査_更新(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_審査_停止(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName);
        [OperationContract]
        bool Alarm処理_検査証書_審査_削除(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName);


        //-------------------------------------------
        // 救命設備
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_救命設備_登録(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei);
        [OperationContract]
        bool Alarm処理_検査証書_救命設備_更新(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei);
        [OperationContract]
        bool Alarm処理_検査証書_救命設備_停止(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei);
        [OperationContract]
        bool Alarm処理_検査証書_救命設備_削除(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei);


        //-------------------------------------------
        // 荷役安全設備
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_荷役安全設備_登録(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku);
        [OperationContract]
        bool Alarm処理_検査証書_荷役安全設備_更新(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku);
        [OperationContract]
        bool Alarm処理_検査証書_荷役安全設備_停止(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku);
        [OperationContract]
        bool Alarm処理_検査証書_荷役安全設備_削除(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku);

        //-------------------------------------------
        // 検船
        //-------------------------------------------
        [OperationContract]
        bool Alarm処理_検査証書_検船_登録(NBaseData.DAC.MsUser logiuser, KsKensen kensen);
        [OperationContract]
        bool Alarm処理_検査証書_検船_更新(NBaseData.DAC.MsUser logiuser, KsKensen kensen);
        [OperationContract]
        bool Alarm処理_検査証書_検船_停止(NBaseData.DAC.MsUser logiuser, KsKensen kensen);
        [OperationContract]
        bool Alarm処理_検査証書_検船_削除(NBaseData.DAC.MsUser logiuser, KsKensen kensen);
    }

    public partial class Service
    {
        //-------------------------------------------
        // 検査
        //-------------------------------------------
        public bool Alarm処理_検査証書_検査_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_登録(logiuser, kensa, kubunName);
        }

        public bool Alarm処理_検査証書_検査_更新(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_更新(logiuser, kensa, kubunName);
        }

        public bool Alarm処理_検査証書_検査_停止(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_停止(logiuser, kensa, kubunName);
        }

        public bool Alarm処理_検査証書_検査_削除(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_削除(logiuser, kensa, kubunName);
        }

        //-------------------------------------------
        // 証書
        //-------------------------------------------
        public bool Alarm処理_検査証書_証書_登録(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            return NBaseData.BLC.Alarm処理.検査証書_証書_登録(logiuser, shousho);
        }

        public bool Alarm処理_検査証書_証書_更新(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            return NBaseData.BLC.Alarm処理.検査証書_証書_更新(logiuser, shousho);
        }

        public bool Alarm処理_検査証書_証書_停止(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            return NBaseData.BLC.Alarm処理.検査証書_証書_停止(logiuser, shousho);
        }

        public bool Alarm処理_検査証書_証書_削除(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            return NBaseData.BLC.Alarm処理.検査証書_証書_削除(logiuser, shousho);
        }

        //-------------------------------------------
        // 審査
        //-------------------------------------------
        public bool Alarm処理_検査証書_審査_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_審査_登録(logiuser, shinsa, kubunName);
        }

        public bool Alarm処理_検査証書_審査_更新(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_審査_更新(logiuser, shinsa, kubunName);
        }

        public bool Alarm処理_検査証書_審査_停止(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_審査_停止(logiuser, shinsa, kubunName);
        }

        public bool Alarm処理_検査証書_審査_削除(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            return NBaseData.BLC.Alarm処理.検査証書_審査_削除(logiuser, shinsa, kubunName);
        }

        //-------------------------------------------
        // 救命設備
        //-------------------------------------------
        public bool Alarm処理_検査証書_救命設備_登録(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            return NBaseData.BLC.Alarm処理.検査証書_救命設備_登録(logiuser, kyumei);
        }

        public bool Alarm処理_検査証書_救命設備_更新(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            return NBaseData.BLC.Alarm処理.検査証書_救命設備_更新(logiuser, kyumei);
        }

        public bool Alarm処理_検査証書_救命設備_停止(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            return NBaseData.BLC.Alarm処理.検査証書_救命設備_停止(logiuser, kyumei);
        }

        public bool Alarm処理_検査証書_救命設備_削除(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            return NBaseData.BLC.Alarm処理.検査証書_救命設備_削除(logiuser, kyumei);
        }

        //-------------------------------------------
        // 荷役安全設備
        //-------------------------------------------
        public bool Alarm処理_検査証書_荷役安全設備_登録(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            return NBaseData.BLC.Alarm処理.検査証書_荷役安全設備_登録(logiuser, niyaku);
        }

        public bool Alarm処理_検査証書_荷役安全設備_更新(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            return NBaseData.BLC.Alarm処理.検査証書_荷役安全設備_更新(logiuser, niyaku);
        }

        public bool Alarm処理_検査証書_荷役安全設備_停止(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            return NBaseData.BLC.Alarm処理.検査証書_荷役安全設備_停止(logiuser, niyaku);
        }

        public bool Alarm処理_検査証書_荷役安全設備_削除(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            return NBaseData.BLC.Alarm処理.検査証書_荷役安全設備_削除(logiuser, niyaku);
        }

        //-------------------------------------------
        // 検船
        //-------------------------------------------
        public bool Alarm処理_検査証書_検船_登録(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検船_登録(logiuser, kensen);
        }

        public bool Alarm処理_検査証書_検船_更新(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検船_更新(logiuser, kensen);
        }

        public bool Alarm処理_検査証書_検船_停止(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検船_停止(logiuser, kensen);
        }

        public bool Alarm処理_検査証書_検船_削除(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検船_削除(logiuser, kensen);
        }

        //----------------------------------------------------------------------------------------------------

        public bool Alarm処理_検査証書_検査_90日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_90日前_登録(logiuser, kensa);
        }

        public bool Alarm処理_検査証書_検査_180日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa)
        {
            return NBaseData.BLC.Alarm処理.検査証書_検査_180日前_登録(logiuser, kensa);
        }
    }
}
