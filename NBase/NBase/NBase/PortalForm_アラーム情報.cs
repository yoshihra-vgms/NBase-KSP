using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using System.Windows.Forms;
using System.Collections;
using NBaseCommon;
using NBaseData.DS;

namespace NBase
{
    partial class PortalForm
    {
        public Hashtable AlarmInfoHash = new Hashtable();
        public List<int> vesselIds_アラーム情報 = new List<int>();


        private void Init_アラーム情報()
        {
            Init_アラーム情報(CheckState.Checked);

            MakeAlarmView();
        }

        private void Init_アラーム情報(CheckState cs)
        {
            // 種別
            checkBox_船員_アラーム情報.Checked = false;
            checkBox_発注_アラーム情報.Checked = false;
            checkBox_文書_アラーム情報.Checked = false;
            if (NBaseData.DS.MsRoleTableCache.instance().Is船員部(NBaseCommon.Common.LoginUser))
            {
                checkBox_船員_アラーム情報.Checked = true;
            }
            if (NBaseData.DS.MsRoleTableCache.instance().Is工務部(NBaseCommon.Common.LoginUser))
            {
                checkBox_発注_アラーム情報.Checked = true;
            }
            if (NBaseData.DS.MsRoleTableCache.instance().Is海務部(NBaseCommon.Common.LoginUser))
            {
                checkBox_文書_アラーム情報.Checked = true;
            }

            // 船
            string 選択船 = "";
            foreach (船選択Form.ListItem listItem in 船選択_アラーム情報)
            {
                if (listItem.Checked)
                {
                    vesselIds_アラーム情報.Add(listItem.Value);
                    選択船 += " , " + listItem.Text;
                }
            }
            textBox_選択船_アラーム情報.Text = "";
            if (選択船.Length > 0)
            {
                textBox_選択船_アラーム情報.Text = 選択船.Substring(3);
            }


            //==============================
            // 契約によるボタンの制御
            //==============================
            checkBox_船員_アラーム情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員);
            checkBox_発注_アラーム情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注);
            checkBox_文書_アラーム情報.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書);
        }


        /// <summary>
        /// 検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_検索_アラーム情報_Click(object sender, EventArgs e)
        {
            MakeAlarmView();
        }


        /// <summary>
        /// 検索条件クリアボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_クリア_アラーム情報_Click(object sender, EventArgs e)
        {
            Init_アラーム情報(CheckState.Checked);
        }


        private void button_船選択_アラーム情報_Click(object sender, EventArgs e)
        {
            船選択Form form = new 船選択Form(船選択_アラーム情報);
            if (form.ShowDialog() == DialogResult.OK)
            {
                船選択_アラーム情報 = form.GetCheckedList();
                vesselIds_アラーム情報 = form.CheckedVesselIds();
                List<string> vesselNames = form.CheckedVesselNames();


                textBox_選択船_アラーム情報.Text = "";
                string t = "";
                foreach (string vesselName in vesselNames)
                {
                    t += " , " + vesselName;
                }
                if (t.Length > 0)
                {
                    textBox_選択船_アラーム情報.Text = t.Substring(3);
                }
            }
        }

        private void MakeAlarmView()
        {
            List<PtAlarmInfo> PtAlarmInfo_List = Search_アラーム情報();
            //-----------------------------------------------------------------------
            // アラーム情報表示
            //-----------------------------------------------------------------------
            treeListView_アラーム情報.SuspendUpdate();
            treeListView_アラーム情報.Nodes.Clear();
            if (PtAlarmInfo_List != null)
            {
                foreach (PtAlarmInfo ptAlarmInfo in PtAlarmInfo_List)
                {
                    //ノードを作成
                    TreeListViewNode node = new TreeListViewNode();

                    if (ptAlarmInfo.HasseiDate == DateTime.MinValue)
                    {
                        node.SubItems.Add(AddSubItem(""));
                    }
                    else
                    {
                        node.SubItems.Add(AddSubItem(ptAlarmInfo.HasseiDate.ToString("yyyy/MM/dd")));
                    }
                    if (ptAlarmInfo.Yuukoukigen == DateTime.MinValue)
                    {
                        node.SubItems.Add(AddSubItem(""));
                    }
                    else
                    {
                        node.SubItems.Add(AddSubItem(ptAlarmInfo.Yuukoukigen.ToString("yyyy/MM/dd")));
                    }
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.VesselName));
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.PortalInfoSyubetuName));
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.Naiyou));
                    node.SubItems.Add(AddSubItem(ptAlarmInfo.Shousai));

                    // ノードを追加
                    treeListView_アラーム情報.Nodes.Add(node);

                    // ハッシュに追加
                    AlarmInfoHash.Add(node, ptAlarmInfo);
                }
            }
            treeListView_アラーム情報.ResumeUpdate();
        }


        private List<PtAlarmInfo> Search_アラーム情報()
        {
            //------------------------------------------------------------------
            // 検索条件作成
            //------------------------------------------------------------------
            PtAlarmInfoCondition condition = new PtAlarmInfoCondition();

            // 発生日
            condition.HasseiDate = DateTime.Today;
            condition.HasseiDate = condition.HasseiDate.AddDays(1);
            condition.HasseiDate = condition.HasseiDate.AddSeconds(-1);

            // 種別
            if (checkBox_船員_アラーム情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.船員);
            }
            if (checkBox_発注_アラーム情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.発注);
            }
            if (checkBox_文書_アラーム情報.Checked == true)
            {
                condition.Shubetu_list.Add(MsPortalInfoShubetu.文書);
            }

            // 船
            foreach (int id in vesselIds_アラーム情報)
            {
                condition.Vessel_list.Add(id);
            }

            //----------------------------------------------------------------------
            // 検索
            //----------------------------------------------------------------------
            List<PtAlarmInfo> PtAlarmInfo_List = null;
            if (condition.Shubetu_list.Count > 0)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    PtAlarmInfo_List = serviceClient.PtAlarmInfo_GetRecordByCondition(new MsUser(), condition);
                }
            }
            return PtAlarmInfo_List;
        }



        /// <summary>
        /// 行選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListView_アラーム情報_SubItemSelectionChanged(object sender, EventArgs e)
        {
            TreeListViewNode selectedNode = treeListView_アラーム情報.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                PtAlarmInfo ptAlarmInfo = (PtAlarmInfo)AlarmInfoHash[selectedNode];

                // 対象Formを起動する
                if (ptAlarmInfo.PortalInfoSyubetuName == MsPortalInfoShubetu.検査証書)
                {
                    #region
                    //if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.検査)
                    //{
                    //    Kensa.BLC.検査詳細起動 target = new Kensa.BLC.検査詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    //else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.証書)
                    //{
                    //    Kensa.BLC.証書詳細起動 target = new Kensa.BLC.証書詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    //else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.審査日)
                    //{
                    //    Kensa.BLC.審査詳細起動 target = new Kensa.BLC.審査詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    //else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.救命設備)
                    //{
                    //    Kensa.BLC.救命設備詳細起動 target = new Kensa.BLC.救命設備詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    //else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.荷役安全設備)
                    //{
                    //    Kensa.BLC.荷役安全設備詳細起動 target = new Kensa.BLC.荷役安全設備詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    //else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.検船)
                    //{
                    //    Kensa.BLC.検船詳細起動 target = new Kensa.BLC.検船詳細起動();
                    //    target.AlarmCall(ptAlarmInfo);
                    //}
                    #endregion
                }
                else if (ptAlarmInfo.PortalInfoSyubetuName == MsPortalInfoShubetu.発注)
                {
                    #region
                    if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.手配依頼)
                    {
                        Hachu.Utils.AlarmReceiver.手配依頼Form起動 target = new Hachu.Utils.AlarmReceiver.手配依頼Form起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.見積回答)
                    {
                        Hachu.Utils.AlarmReceiver.見積回答Form起動 target = new Hachu.Utils.AlarmReceiver.見積回答Form起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.承認)
                    {
                        if (ptAlarmInfo.PortalInfoKubunName == MsPortalInfoKubun.未承認)
                        {
                            Hachu.Utils.AlarmReceiver.承認Form起動 target = new Hachu.Utils.AlarmReceiver.承認Form起動();
                            target.AlarmCall(ptAlarmInfo);
                        }
                        else if (ptAlarmInfo.PortalInfoKubunName == MsPortalInfoKubun.未発注)
                        {
                            Hachu.Utils.AlarmReceiver.見積回答Form起動 target = new Hachu.Utils.AlarmReceiver.見積回答Form起動();
                            target.AlarmCall(ptAlarmInfo);
                        }
                    }
                    else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.発注)
                    {
                        Hachu.Utils.AlarmReceiver.受領Form起動 target = new Hachu.Utils.AlarmReceiver.受領Form起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.受領)
                    {
                        Hachu.Utils.AlarmReceiver.受領Form起動 target = new Hachu.Utils.AlarmReceiver.受領Form起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    else if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.支払)
                    {
                        Hachu.Utils.AlarmReceiver.支払Form起動 target = new Hachu.Utils.AlarmReceiver.支払Form起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    #endregion
                }
                else if (ptAlarmInfo.PortalInfoSyubetuName == MsPortalInfoShubetu.船員)
                {
                    #region
                    if (ptAlarmInfo.PortalInfoKoumokuName == MsPortalInfoKoumoku.免許免状)
                    {
                        Senin.util.AlarmReceiver.免状免許詳細起動 target = new Senin.util.AlarmReceiver.免状免許詳細起動();
                        target.AlarmCall(ptAlarmInfo);
                    }
                    #endregion
                }

                // 一覧の表示更新
                MakeAlarmView();
            }
            catch (Exception E)
            {
                string ErrMsg = E.Message;
                MessageBox.Show("対象情報の取得に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
    }
}
