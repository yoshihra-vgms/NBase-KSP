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
    public partial class 動静詳細Form1 : Form
    {
        private int MsVesselId;
        public DjDousei Dousei;

        public List<MsVessel> MsVessel_List = null;
        private List<MsBasho> MsBasho_list = null;
        private List<MsKichi> MsKichi_list = null;
        private List<MsCargo> MsCargo_list = null;
        private List<MsDjTani> MsDjTani_list = null;
        private List<MsCustomer> MsCustomer_list = null;

        public 動静詳細Form1(int vesselId)
        {
            InitializeComponent();

            MsVesselId = vesselId;
        }

        #region private void 動静詳細Form1_Load(object sender, EventArgs e)
        private void 動静詳細Form1_Load(object sender, EventArgs e)
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
            douseiYoteiUserControl1.SetMode(DouseiYoteiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl2.SetMode(DouseiYoteiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl3.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl4.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl5.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl6.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            douseiYoteiUserControl7.SetMode(DouseiYoteiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);

            DouseiJissekiUserControl1.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl2.SetMode(DouseiJissekiUserControl.ModeEnum.積み, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl3.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl4.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl5.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl6.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);
            DouseiJissekiUserControl7.SetMode(DouseiJissekiUserControl.ModeEnum.揚げ, MsBasho_list, MsKichi_list, MsCargo_list, MsDjTani_list, MsCustomer_list);

            #region 積み
            DjDousei tsumi1 = Dousei.積み(1);
            if (tsumi1 != null)
            {
                douseiYoteiUserControl1.SetDousei(tsumi1);
                //douseiYoteiUserControl1.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl1.SetDousei(tsumi1);
            }
            else
            {
                //douseiYoteiUserControl1.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl1.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            DjDousei tsumi2 = Dousei.積み(2);
            if (tsumi2 != null)
            {
                douseiYoteiUserControl2.SetDousei(tsumi2);
                //douseiYoteiUserControl2.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl2.SetDousei(tsumi2);
            }
            else
            {
                //douseiYoteiUserControl2.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl2.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            #endregion

            #region 揚げ
            DjDousei age1 = Dousei.揚げ(1);
            if (age1 != null)
            {
                douseiYoteiUserControl3.SetDousei(age1);
                //douseiYoteiUserControl3.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl3.SetDousei(age1);
            }
            else
            {
                //douseiYoteiUserControl3.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl3.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            DjDousei age2 = Dousei.揚げ(2);
            if (age2 != null)
            {
                douseiYoteiUserControl4.SetDousei(age2);
                //douseiYoteiUserControl4.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl4.SetDousei(age2);
            }
            else
            {
                //douseiYoteiUserControl4.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl4.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            DjDousei age3 = Dousei.揚げ(3);
            if (age3 != null)
            {
                douseiYoteiUserControl5.SetDousei(age3);
                //douseiYoteiUserControl5.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl5.SetDousei(age3);
            }
            else
            {
                //douseiYoteiUserControl5.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl5.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            DjDousei age4 = Dousei.揚げ(4);
            if (age4 != null)
            {
                douseiYoteiUserControl6.SetDousei(age4);
                //douseiYoteiUserControl6.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl6.SetDousei(age4);
            }
            else
            {
                //douseiYoteiUserControl6.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl6.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            DjDousei age5 = Dousei.揚げ(5);
            if (age5 != null)
            {
                douseiYoteiUserControl7.SetDousei(age5);
                //douseiYoteiUserControl7.ReadOnly(); // 編集してもよい
                DouseiJissekiUserControl7.SetDousei(age5);
            }
            else
            {
                //douseiYoteiUserControl7.Enabled = false; // 予定は編集してもよい
                //DouseiJissekiUserControl7.Enabled = false; // 2013/05/14: 実績は編集してもよい
            }
            #endregion

            tabControl1.SelectedIndex = 1; // 実績タブを初期表示する
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
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
        #endregion

        /// <summary>
        /// 入力値の検証を行う
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                //======================================
                // 予定タブ
                //======================================
                if (douseiYoteiUserControl1.Validation() == false) // 積み１
                {
                    return false;
                }
                if (douseiYoteiUserControl2.Validation() == false) // 積み２
                {
                    return false;
                }
                if (douseiYoteiUserControl3.Validation() == false) // 揚げ１
                {
                    return false;
                }
                if (douseiYoteiUserControl4.Validation() == false) // 揚げ２
                {
                    return false;
                }
                if (douseiYoteiUserControl5.Validation() == false) // 揚げ３
                {
                    return false;
                }
                if (douseiYoteiUserControl6.Validation() == false) // 揚げ４
                {
                    return false;
                }
                if (douseiYoteiUserControl7.Validation() == false) // 揚げ５
                {
                    return false;
                }
            }
            else
            {
                //======================================
                // 実績タブ
                //======================================
                if (DouseiJissekiUserControl1.IsInputTime() == true && DouseiJissekiUserControl1.Validation() == false) // 積み１
                {
                    return false;
                }
                if (DouseiJissekiUserControl2.IsInputTime() == true && DouseiJissekiUserControl2.Validation() == false) // 積み２
                {
                    return false;
                }
                if (DouseiJissekiUserControl3.IsInputTime() == true && DouseiJissekiUserControl3.Validation() == false) // 揚げ１
                {
                    return false;
                }
                if (DouseiJissekiUserControl4.IsInputTime() == true && DouseiJissekiUserControl4.Validation() == false) // 揚げ２
                {
                    return false;
                }
                if (DouseiJissekiUserControl5.IsInputTime() == true && DouseiJissekiUserControl5.Validation() == false) // 揚げ３
                {
                    return false;
                }
                if (DouseiJissekiUserControl6.IsInputTime() == true && DouseiJissekiUserControl6.Validation() == false) // 揚げ４
                {
                    return false;
                }
                if (DouseiJissekiUserControl7.IsInputTime() == true && DouseiJissekiUserControl7.Validation() == false) // 揚げ５
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力値をクラスにセットする
        /// </summary>
        /// <returns></returns>
        #region private List<DjDousei> Fill()
        private List<DjDousei> Fill()
        {
            List<DjDousei> ret = new List<DjDousei>();

            DjDousei dousei = null;

            if (tabControl1.SelectedIndex == 0)
            {
                //======================================
                // 予定タブ
                //======================================
                #region

                // 積み１
                dousei = douseiYoteiUserControl1.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                    ret.Add(dousei);
                }
                // 積み２
                dousei = douseiYoteiUserControl2.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                    ret.Add(dousei);
                }

                // 揚げ１
                dousei = douseiYoteiUserControl3.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }
                // 揚げ２
                dousei = douseiYoteiUserControl4.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }
                // 揚げ３
                dousei = douseiYoteiUserControl5.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }
                // 揚げ４
                dousei = douseiYoteiUserControl6.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }
                // 揚げ５
                dousei = douseiYoteiUserControl7.GetInstance();
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                #endregion
            }
            else
            {
                //======================================
                // 実績タブ
                //======================================
                #region

                // 積み１
                if (DouseiJissekiUserControl1.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl1.GetInstance();
                }
                else
                {
                     dousei = DouseiJissekiUserControl1.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                    ret.Add(dousei);
                }


                // 積み２
                if (DouseiJissekiUserControl2.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl2.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl2.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                    ret.Add(dousei);
                }

                // 揚げ１
                if (DouseiJissekiUserControl3.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl3.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl3.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                // 揚げ２
                if (DouseiJissekiUserControl4.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl4.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl4.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                // 揚げ３
                if (DouseiJissekiUserControl5.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl5.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl5.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                // 揚げ４
                if (DouseiJissekiUserControl6.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl6.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl6.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                // 揚げ５
                if (DouseiJissekiUserControl7.IsInputTime() == true)
                {
                    dousei = DouseiJissekiUserControl7.GetInstance();
                }
                else
                {
                    dousei = DouseiJissekiUserControl7.GetInstanceNoCheck();
                }
                if (dousei != null)
                {
                    dousei.MsVesselID = MsVesselId;
                    dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                    ret.Add(dousei);
                }

                #endregion
            }
            return ret;
        }
        #endregion
    }
}
