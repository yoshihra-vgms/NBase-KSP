using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon;
using NBaseData.DAC;


namespace Dousei
{
    public partial class 新規動静実績Form : Form
    {
        private int MsVesselId;
        private List<MsBasho> MsBasho_list = null;
        private List<MsKichi> MsKichi_list = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;
        private List<MsCustomer> MsCustomer_list = null;

        public 新規動静実績Form(int vesselId)
        {
            InitializeComponent();

            MsVesselId = vesselId;
        }

        private void 新規動静実績Form_Load(object sender, EventArgs e)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsBasho_list = serviceClient.MsBasho_GetRecordsBy港(NBaseCommon.Common.LoginUser);
                MsKichi_list = serviceClient.MsKiti_GetRecords(NBaseCommon.Common.LoginUser);
                MsCargo_list = serviceClient.MsCargo_GetRecords(NBaseCommon.Common.LoginUser);
                MsDjTani_list = serviceClient.MsDjTani_GetRecords(NBaseCommon.Common.LoginUser);
                MsCustomer_list = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
            }

            // パネルの準備
            DouseiJissekiUserControl1.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl2.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl3.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl4.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl5.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl6.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl7.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
        }

        private void button_閉じる_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }

        private void button_削除_Click(object sender, EventArgs e)
        {

        }

        private void button_登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }
            List<DjDousei> douseiInfos = Fill();
            if (douseiInfos.Count == 0)
            {
                return;
            }
            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_動静実績登録(NBaseCommon.Common.LoginUser, douseiInfos);
            }
            if (ret == true)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        /// <summary>
        /// 入力値の検証を行う
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            if (DouseiJissekiUserControl1.Validation() == false) // 積み１
            {
                return false;
            }
            if (DouseiJissekiUserControl2.Validation() == false) // 積み２
            {
                return false;
            }
            if (DouseiJissekiUserControl3.Validation() == false) // 揚げ１
            {
                return false;
            }
            if (DouseiJissekiUserControl4.Validation() == false) // 揚げ２
            {
                return false;
            }
            if (DouseiJissekiUserControl5.Validation() == false) // 揚げ３
            {
                return false;
            }
            if (DouseiJissekiUserControl6.Validation() == false) // 揚げ４
            {
                return false;
            }
            if (DouseiJissekiUserControl7.Validation() == false) // 揚げ５
            {
                return false;
            }

            return true;
        }
        #endregion

        #region private List<DjDousei> Fill()
        private List<DjDousei> Fill()
        {
            List<DjDousei> ret = new List<DjDousei>();


            DjDousei dousei = null;

            // 積み１
            dousei = DouseiJissekiUserControl1.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                ret.Add(dousei);
            }
            // 積み２
            dousei = DouseiJissekiUserControl2.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                ret.Add(dousei);
            }

            // 揚げ１
            dousei = DouseiJissekiUserControl3.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                ret.Add(dousei);
            }
            // 揚げ２
            dousei = DouseiJissekiUserControl4.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                ret.Add(dousei);
            }
            // 揚げ３
            dousei = DouseiJissekiUserControl5.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                ret.Add(dousei);
            }
            // 揚げ４
            dousei = DouseiJissekiUserControl6.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                ret.Add(dousei);
            }
            // 揚げ５
            dousei = DouseiJissekiUserControl7.GetInstance();
            if (dousei != null)
            {
                dousei.MsVesselID = MsVesselId;
                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                ret.Add(dousei);
            }
            return ret;
        }
        #endregion
    }
}
