using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using System.Windows.Forms;
using System.Collections;
using NBase.util;
using NBaseCommon;
using NBaseData.DS;

namespace NBase
{
    partial class PortalForm
    {
        public Hashtable KoushinInfoHash = new Hashtable();
        private TreeListViewDelegate本船更新情報 treeListViewDelegate本船更新情報;
        public List<int> vesselIds_本船更新情報 = new List<int>();


        private void Init_本船更新情報()
        {
            Init_本船更新情報(CheckState.Checked);

            treeListViewDelegate本船更新情報 = new TreeListViewDelegate本船更新情報(treeListView_本船更新情報);
            MakeHonsenKoushinView();
        }
        private void Init_本船更新情報(CheckState cs)
        {
            // 種別
            checkBox_船員_本船更新情報.Checked = false;
            checkBox_発注_本船更新情報.Checked = false;
            checkBox_文書_本船更新情報.Checked = false;
            if (NBaseData.DS.MsRoleTableCache.instance().Is船員部(NBaseCommon.Common.LoginUser))
            {
                checkBox_船員_本船更新情報.Checked = true;
            }
            if (NBaseData.DS.MsRoleTableCache.instance().Is工務部(NBaseCommon.Common.LoginUser))
            {
                checkBox_発注_本船更新情報.Checked = true;
            }
            if (NBaseData.DS.MsRoleTableCache.instance().Is海務部(NBaseCommon.Common.LoginUser))
            {
                checkBox_文書_本船更新情報.Checked = true;
            }

            // 船
            string 選択船 = "";
            foreach (船選択Form.ListItem listItem in 船選択_本船更新情報)
            {
                if (listItem.Checked)
                {
                    vesselIds_本船更新情報.Add(listItem.Value);
                    選択船 += " , " + listItem.Text;
                }
            }
            textBox_選択船_本船更新情報.Text = "";
            if (選択船.Length > 0)
            {
                textBox_選択船_本船更新情報.Text = 選択船.Substring(3);
            }

            dateTimePicker_開始_本船更新情報.Value = DateTime.Now.AddDays(-7);
            dateTimePicker_終了_本船更新情報.Value = DateTime.Now.AddDays(0);

            //==============================
            // 契約によるボタンの制御
            //==============================
            checkBox_船員_本船更新情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員);
            checkBox_発注_本船更新情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注);
            checkBox_文書_本船更新情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書);
        }


        /// <summary>
        /// 検索ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_検索_本船更新情報_Click(object sender, EventArgs e)
        {
            this.MakeHonsenKoushinView();
        }


        /// <summary>
        /// 検索条件クリアボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_クリア_本船更新情報_Click(object sender, EventArgs e)
        {
            Init_本船更新情報(CheckState.Checked);
        }

        private void button_船選択_本船更新情報_Click(object sender, EventArgs e)
        {
            船選択Form form = new 船選択Form(船選択_本船更新情報);
            if (form.ShowDialog() == DialogResult.OK)
            {
                船選択_本船更新情報 = form.GetCheckedList();
                vesselIds_本船更新情報 = form.CheckedVesselIds();
                List<string> vesselNames = form.CheckedVesselNames();


                textBox_選択船_本船更新情報.Text = "";
                string t = "";
                foreach (string vesselName in vesselNames)
                {
                    t += " , " + vesselName;
                }
                if (t.Length > 0)
                {
                    textBox_選択船_本船更新情報.Text = t.Substring(3);
                }
            }
        }


        /// <summary>
        /// 本船更新情報TreeListViewのアイテムクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListView_本船更新情報_SubItemSelectionChanged(object sender, EventArgs e)
        {
            TreeListViewNode selectedNode = treeListView_本船更新情報.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            PtHonsenkoushinInfo ptHonsenkoushinInfo = (PtHonsenkoushinInfo)KoushinInfoHash[selectedNode];
            if (ptHonsenkoushinInfo == null)
            {
                return;
            }
        }


        /// <summary>
        /// 本船更新情報TreeListViewを作成する
        /// </summary>
        private void MakeHonsenKoushinView()
        {
            List<PtHonsenkoushinInfo> ptHonsenkoushinInfo_list = Search_本船更新情報();
            //-----------------------------------------------------------------------
            // 本船更新情報表示
            //-----------------------------------------------------------------------
            treeListViewDelegate本船更新情報.SetRows(ptHonsenkoushinInfo_list, KoushinInfoHash);
        }


        private List<PtHonsenkoushinInfo> Search_本船更新情報()
        {
            //------------------------------------------------------------------
            // 検索条件作成
            //------------------------------------------------------------------
            PtHonsenkoushinInfoCondition condition = new PtHonsenkoushinInfoCondition();

            // 日付From
            condition.EventDate_From = new DateTime(dateTimePicker_開始_本船更新情報.Value.Year, dateTimePicker_開始_本船更新情報.Value.Month, dateTimePicker_開始_本船更新情報.Value.Day, 00, 00, 00);

            // 日付To
            condition.EventDate_To = new DateTime(dateTimePicker_終了_本船更新情報.Value.Year, dateTimePicker_終了_本船更新情報.Value.Month, dateTimePicker_終了_本船更新情報.Value.Day, 23, 59, 59);


            // 種別
            if (checkBox_船員_本船更新情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.船員);
            }
            if (checkBox_発注_本船更新情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.発注);
            }
            if (checkBox_文書_本船更新情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.文書);
            }

            // 船
            foreach (int id in vesselIds_本船更新情報)
            {
                condition.Vessel_list.Add(id);
            }

            //----------------------------------------------------------------------
            // 検索
            //----------------------------------------------------------------------
            List<PtHonsenkoushinInfo> ptHonsenkoushinInfo_list = null;
            
            // 種別も船も必ず１つづつはチェックされていないとダメ
            if (condition.Shubetu_list.Count > 0 && condition.Vessel_list.Count > 0)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ptHonsenkoushinInfo_list = serviceClient.PtHonsenkoushinInfo_GetRecordsByCondition(new MsUser(), condition);
                }
            }
            return ptHonsenkoushinInfo_list;
        }
    }
}
