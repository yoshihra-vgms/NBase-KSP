using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
//using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Containers;//miho
using Hachu.Models;
using Hachu.HachuManage;
using NBaseData.DAC;
using NBaseData.DS;

namespace Hachu.Controllers
{
    public class 船種別変更一覧情報Controller
    {
        private Form MdiForm;

        private SplitContainer 船種別変更一覧エリア = null;
        private VslSyubHenkoListControl 船種別変更一覧Control = null;

        private Dictionary<int, BaseUserControl> Dic_TabPages = new Dictionary<int, BaseUserControl>();
        private ListInfo手配依頼 Selected手配 = null;

        private 発注一覧検索条件 検索条件 = null;

        public int 検索結果数 = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 船種別変更一覧情報Controller(Form form, TabControl tabControl, SplitContainer splitContainer, HachuListControl hachuListControl)
        public 船種別変更一覧情報Controller(Form form, SplitContainer splitContainer, VslSyubHenkoListControl vslSyubHenkoListControl)
        {
            // MainForm
            MdiForm = form;

            // 一覧、および、詳細表示部分
            船種別変更一覧エリア = splitContainer;
            船種別変更一覧エリア.Panel2Collapsed = true;

            // 対象のTreeListView
            船種別変更一覧Control = vslSyubHenkoListControl;
            船種別変更一覧Control.ClickEvent += new VslSyubHenkoListControl.ClickEventHandler(詳細Formを開く);
        }
        #endregion

        /// <summary>
        /// 終了処理
        /// </summary>
        #region public void 終了()
        public void 終了()
        {
            //foreach (BaseForm form in 発注一覧NodeHash.Keys)
            //{
            //    form.FormClosingEvent -= new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
            //    form.Close();
            //}
        }
        #endregion

        /// <summary>
        /// 検索、一覧更新
        /// </summary>
        #region public void 一覧更新(発注一覧検索条件 検索条件)
        public void 一覧更新(発注一覧検索条件 検索条件)
        {
            this.検索条件 = 検索条件;

            検索結果数 = 0;


            OdThiFilter filter = new OdThiFilter();
            // 検索条件 → Filter にセット
            #region
            if (検索条件.Vessel != null && 検索条件.Vessel.MsVesselID != -1)
            {
                filter.MsVesselID = 検索条件.Vessel.MsVesselID;
            }
            if (検索条件.User != null && 検索条件.User.MsUserID != "")
            {
                filter.JimTantouID = 検索条件.User.MsUserID;
            }
            if (検索条件.ThiIraiSbt != null && 検索条件.ThiIraiSbt.MsThiIraiSbtID != "")
            {
                filter.MsThiIraiSbtID = 検索条件.ThiIraiSbt.MsThiIraiSbtID;
            }
            if (検索条件.ThiIraiShousai != null && 検索条件.ThiIraiShousai.MsThiIraiShousaiID != "")
            {
                filter.MsThiIraiShousaiID = 検索条件.ThiIraiShousai.MsThiIraiShousaiID;
            }
            if (検索条件.thiIraiDateFrom.Length > 0)
            {
                try
                {
                    filter.ThiIraiDateFrom = DateTime.Parse(検索条件.thiIraiDateFrom);
                }
                catch
                {
                }
            }
            if (検索条件.thiIraiDateTo.Length > 0)
            {
                try
                {
                    filter.ThiIraiDateTo = DateTime.Parse(検索条件.thiIraiDateTo);
                }
                catch
                {
                }
            }
            if (検索条件.jryDateFrom.Length > 0)
            {
                try
                {
                    filter.JryDateFrom = DateTime.Parse(検索条件.jryDateFrom);
                }
                catch
                {
                }
            }
            if (検索条件.jryDateTo.Length > 0)
            {
                try
                {
                    filter.JryDateTo = DateTime.Parse(検索条件.jryDateTo);
                }
                catch
                {
                }
            }
            if (検索条件.status未対応 == true)
            {
                filter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_未対応);
            }
            if (検索条件.status見積中 == true)
            {
                filter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_見積中);
            }
            if (検索条件.status発注済 == true)
            {
                filter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_発注済);
            }
            if (検索条件.status受領済 == true)
            {
                filter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_受領済);
            }
            if (検索条件.status完了 == true)
            {
                filter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_完了);
            }

            // 2014.02 2013年度改造
            if (検索条件.status船受領 == true)
            {
                filter.JryStatus = (int)OdJry.STATUS.船受領;
            }
            #endregion

            // 検索
            List<OdThi> OdThis = null;
            List<OdMm> OdMms = null;
            List<OdMk> OdMks = null;
            List<OdJry> OdJrys = null;
            List<OdShr> OdShrs = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    OdThis = serviceClient.OdThi_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                    OdMms = serviceClient.OdMm_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                    OdMks = serviceClient.OdMk_GetRecordsByFilter(NBaseCommon.Common.LoginUser, int.MinValue, filter);
                    OdJrys = serviceClient.OdJry_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, filter);
                    OdShrs = serviceClient.OdShr_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, filter);
                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            船種別変更一覧エリア.Panel2Collapsed = true;

            船種別変更一覧Control.IsContains船受領 = 検索条件.status船受領;
            検索結果数 = 船種別変更一覧Control.DrawList(OdThis, OdMms, OdMks, OdJrys, OdShrs);
        }
        #endregion


        /// <summary>
        /// 「発注一覧」Form の TreeListView クリック
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く(ListInfo手配依頼 thiInfo)
        {
            Selected手配 = thiInfo;

            船種別変更一覧エリア.Parent.Cursor = Cursors.WaitCursor;

            船種別変更Form form = new 船種別変更Form((int)BaseUserControl.WINDOW_STYLE.通常,thiInfo);


            form.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(詳細Form_InfoUpdateEvent);

            if (Dic_TabPages == null)
                Dic_TabPages = new Dictionary<int, BaseUserControl>();
            else
                Dic_TabPages.Clear();

            Dic_TabPages.Add(0, form);


            船種別変更一覧エリア.Panel2.Controls.Clear();
            船種別変更一覧エリア.Panel2.Controls.Add(form);

            船種別変更一覧エリア.SplitterDistance = 船種別変更一覧エリア.Width - form.Width;

            form.Dock = DockStyle.Fill;

            船種別変更一覧エリア.Panel2Collapsed = false;

            船種別変更一覧エリア.Parent.Cursor = Cursors.Default;

        }
        #endregion



        //===============================================================================
        // デリゲートを経由して呼び出されるメソッド郡
        //===============================================================================

        /// <summary>
        /// 詳細Formで情報を更新したときのイベント
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_InfoUpdateEvent(BaseUserControl senderForm, 一覧情報 senderInfo)
        public void 詳細Form_InfoUpdateEvent(BaseUserControl senderForm, ListInfoBase senderInfo)
        {
            一覧情報更新(senderForm, senderInfo);
        }
        #endregion


        /// <summary>
        /// 管理している詳細情報を詳細情報Formで更新された情報と置き換える
        /// ノードの文字列を更新する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 一覧情報更新(BaseUserControl form, 一覧情報 senderInfo)
        private void 一覧情報更新(BaseUserControl form, ListInfoBase senderInfo)
        {
            try
            {
                Selected手配.info = ((ListInfo手配依頼)senderInfo).info;

                // 一覧情報更新（一覧更新のみでクリックイベントは呼び出さない）
                船種別変更一覧Control.RedrawSelectedNode(Selected手配, true);


                // 詳細エリアを非表示にする
                船種別変更一覧エリア.Panel2Collapsed = true;


                Dic_TabPages.Remove(0);
            }
            catch
            {
            }
        }
        #endregion


        private ListInfoBase GetInfo(List<ListInfoBase> infos, int getIndex, ref int index)
        {
            ListInfoBase targetInfo = null;

            foreach (ListInfoBase info in infos)
            {
                index++;

                if (getIndex == index)
                {
                    targetInfo = info;
                    break;
                }
                if (info.Children != null && info.Children.Count != 0)
                    targetInfo = GetInfo(info.Children, getIndex, ref index);

                if (targetInfo != null)
                    break;
            }

            return targetInfo;
        }

        //private int GetParentIndex(ListInfoBase info, int getIndex, ref int index)
        //{
        //    int parentIndex = 0;

        //    parentIndex = index;

        //    foreach (ListInfoBase child in info.Children)
        //    {
        //        index++;

        //        if (getIndex <= index)
        //        {
        //            break;
        //        }

        //        if (child.Children != null && child.Children.Count != 0)
        //        {
        //            int ret = GetParentIndex(child, getIndex, ref index);
        //            if (getIndex == index)
        //            {
        //                parentIndex = ret;
        //                break;
        //            }
        //        }
        //    }

        //    return parentIndex;
        //}

        ///// <summary>
        ///// 子供たち全部数える　2021/08/26 m.yoshihara
        ///// </summary>
        ///// <returns></returns>
        //private int GetCountAllChildlen(ListInfoBase info, ref int index)
        //{
        //    if (info.Children != null && info.Children.Count != 0)
        //    {
        //        foreach (ListInfoBase child in info.Children)
        //        {
        //            index++;

        //            if (child.Children != null && child.Children.Count != 0)
        //            {
        //                int ret = GetCountAllChildlen(child, ref index);
        //            }
        //        }

        //    }
        //    return index;
        //}


        //private bool RemoveInfo(List<ListInfoBase> infos, int removeIndex, ref int index)
        //{
        //    bool ret = false;

        //    foreach (ListInfoBase info in infos)
        //    {
        //        index++;

        //        if (removeIndex == index)
        //        {
        //            infos.Remove(info);
        //            ret = true;
        //            break;
        //        }
        //        if (info.Children != null && info.Children.Count != 0)
        //            ret = RemoveInfo(info.Children, removeIndex, ref index);

        //        if (ret == true)
        //            break;
        //    }

        //    return ret;
        //}
    }

}

