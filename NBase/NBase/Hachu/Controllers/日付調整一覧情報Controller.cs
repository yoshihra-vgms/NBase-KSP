using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Hachu.Models;
using Hachu.HachuManage;
using Hachu.Utils;
using NBaseData.DAC;
using NBaseData.DS;
using System.Linq;

namespace Hachu.Controllers
{
    public class 日付調整一覧情報Controller
    {
        private Form MdiForm;
        private ItemTreeListView日付調整 TreeList = null;
        private TreeListView 日付調整一覧TreeListView = null;

        public Hashtable 日付調整一覧InfoHash = null;
        public Hashtable 日付調整一覧FormHash = null;
        public Hashtable 日付調整一覧NodeHash = null;
        public Hashtable 日付調整一覧ParentNodeHash = null;

        public int 検索結果数 = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 日付調整情報Controller(Form form, TreeListView treeListView)
        public 日付調整一覧情報Controller(Form form, TreeListView treeListView)
        {
            // MainForm
            MdiForm = form;

            // 対象のTreeListView
            日付調整一覧TreeListView = treeListView;

            // 情報管理用
            日付調整一覧InfoHash = new Hashtable();
            日付調整一覧FormHash = new Hashtable();
            日付調整一覧NodeHash = new Hashtable();
            日付調整一覧ParentNodeHash = new Hashtable();
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        #region public void 初期化(int width)
        public void 初期化(int width)
        {
            object[,] columns = new object[,] {
                                           {"No", 75, null, null},
                                           {"船名", 150, null, null},
                                           {"種別", 85, null, null},
                                           {"詳細種別", 85, null, null},
                                           {"件名", 350, null, null},
                                           {"手配依頼日", 80, null, null},
                                           {"見積依頼日", 80, null, null},
                                           {"回答日", 80, null, null},
                                           {"発注日", 80, null, null},
                                           {"納品日", 80, null, null},
                                           {"受領日", 80, null, null},
                                           {"請求書日", 80, null, null},
                                           {"支払日", 80, null, null}
                                        };
            TreeList = new ItemTreeListView日付調整(日付調整一覧TreeListView);
            TreeList.SetColumns(-2,columns);
        }
        #endregion
        
        /// <summary>
        /// 終了処理
        /// </summary>
        #region public void 終了()
        public void 終了()
        {
            foreach (BaseForm form in 日付調整一覧NodeHash.Keys)
            {
                form.FormClosingEvent -= new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
                form.Close();
            }
        }
        #endregion

        /// <summary>
        /// 検索、一覧更新
        /// </summary>
        #region public void 一覧更新(発注一覧検索条件 検索条件, bool is日付逆転)
        public void 一覧更新(発注一覧検索条件 検索条件, bool is日付逆転)
        {
            検索結果数 = 0;
            日付調整一覧TreeListView.Nodes.Clear();

            日付調整一覧InfoHash.Clear();
            日付調整一覧FormHash.Clear();
            日付調整一覧NodeHash.Clear();
            日付調整一覧ParentNodeHash.Clear();

            OdThiFilter filter = new OdThiFilter();
            // 検索条件 → Filter にセット
            #region
            if (検索条件.Vessel != null && 検索条件.Vessel.MsVesselID != -1)
            {
                filter.MsVesselID = 検索条件.Vessel.MsVesselID;
            }
            if (検索条件.ThiIraiSbt != null && 検索条件.ThiIraiSbt.MsThiIraiSbtID != "")
            {
                filter.MsThiIraiSbtID = 検索条件.ThiIraiSbt.MsThiIraiSbtID;
            }
            if (検索条件.ThiIraiShousai != null && 検索条件.ThiIraiShousai.MsThiIraiShousaiID != "")
            {
                filter.MsThiIraiShousaiID = 検索条件.ThiIraiShousai.MsThiIraiShousaiID;
            }
            if (検索条件.hachuDateFrom.Length > 0)
            {
                try
                {
                    filter.HachuDateFrom = DateTime.Parse(検索条件.hachuDateFrom);
                }
                catch
                {
                }
            }
            if (検索条件.hachuDateTo.Length > 0)
            {
                try
                {
                    filter.HachuDateTo = DateTime.Parse(検索条件.hachuDateTo);
                }
                catch
                {
                }
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


                    OdShrs = OdShrs.Where(obj => obj.Sbt == (int)OdShr.SBT.支払).ToList(); // 支払のみ対象

                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            //BaseForm form1 = new BaseForm();
            //form1.MdiParent = MdiForm;
            //form1.Show();
            //form1.Hide();

            var thiIds = OdThis.Select(obj => obj.OdThiID);
            if (is日付逆転)
            {
                var jryIds = (OdJrys.Where(obj => obj.JryDate != DateTime.MinValue).Join(OdMks, p => p.OdMkID, j => j.OdMkID, (p, j) => new { p.OdJryID, p.JryDate, j.HachuDate }).ToList()).Where(obj => DateTime.Parse(obj.JryDate.ToShortDateString()) < DateTime.Parse(obj.HachuDate.ToShortDateString())).Select(obj => obj.OdJryID);
                var thiIds1 = OdJrys.Where(obj => jryIds.Contains(obj.OdJryID)).Select(obj => obj.OdThiID);
                System.Diagnostics.Debug.WriteLine("Mk > Jry Count = " + thiIds1.Count());

                var mkIds = (OdMks.Where(obj => obj.HachuDate != DateTime.MinValue).Join(OdMms, p => p.OdMmID, j => j.OdMmID, (p, j) => new { p.OdMkID, p.HachuDate, j.MmDate }).ToList()).Where(obj => DateTime.Parse(obj.HachuDate.ToShortDateString()) < DateTime.Parse(obj.MmDate.ToShortDateString())).Select(obj => obj.OdMkID);
                var thiIds2 = OdMks.Where(obj => mkIds.Contains(obj.OdMkID)).Select(obj => obj.OdThiID);
                System.Diagnostics.Debug.WriteLine("Mm > Mk Count = " + thiIds2.Count());

                var mmIds = (OdMms.Where(obj => obj.MmDate != DateTime.MinValue).Join(OdThis, p => p.OdThiID, j => j.OdThiID, (p, j) => new { p.OdMmID, p.MmDate, j.ThiIraiDate }).ToList()).Where(obj => DateTime.Parse(obj.MmDate.ToShortDateString()) < DateTime.Parse(obj.ThiIraiDate.ToShortDateString())).Select(obj => obj.OdMmID);
                var thiIds3 = OdMms.Where(obj => mmIds.Contains(obj.OdMmID)).Select(obj => obj.OdThiID);
                System.Diagnostics.Debug.WriteLine("Thi > Mm Count = " + thiIds3.Count());

                thiIds = thiIds1.Union(thiIds2).Union(thiIds3);

                //var trgThis = OdThis.Where(obj => thiIds.Contains(obj.OdThiID));
                //OdThis = trgThis.ToList();
            }

            日付調整一覧TreeListView.SuspendUpdate();

            System.Diagnostics.Debug.WriteLine("Thi Count = " + OdThis.Count().ToString());
            int i = 0;
            #region
            if (thiIds.Count() > 0)
            {
                foreach (OdThi thi in OdThis)
                {
                    //System.Diagnostics.Debug.Write(i.ToString());
                    //i++;
                    if (thiIds.Contains(thi.OdThiID) == false)
                    {
                        //System.Diagnostics.Debug.WriteLine(" = false");
                        continue;
                    }
                    //System.Diagnostics.Debug.WriteLine(" = true");


                    if (OdMms.Any(obj => obj.OdThiID == thi.OdThiID))
                    {
                        var mms = OdMms.Where(obj => obj.OdThiID == thi.OdThiID);
                        foreach (OdMm mm in mms)
                        {
                            if (OdMks.Any(obj => obj.OdMmID == mm.OdMmID && obj.HachuDate != DateTime.MinValue))
                            {
                                var mks = OdMks.Where(obj => obj.OdMmID == mm.OdMmID && obj.HachuDate != DateTime.MinValue);
                                foreach (OdMk mk in mks)
                                {
                                    if (OdJrys.Any(obj => obj.OdMkID == mk.OdMkID))
                                    {
                                        var jrys = OdJrys.Where(obj => obj.OdMkID == mk.OdMkID);
                                        foreach (OdJry jry in jrys)
                                        {
                                            if (OdShrs.Any(obj => obj.OdJryID == jry.OdJryID))
                                            {
                                                var shrs = OdShrs.Where(obj => obj.OdJryID == jry.OdJryID);
                                                foreach (OdShr shr in shrs)
                                                {

                                                    ListInfo日付調整 info = new ListInfo日付調整();
                                                    info.no = ++検索結果数;
                                                    info.thi = thi;
                                                    info.mm = mm;
                                                    info.mk = mk;
                                                    info.jry = jry;
                                                    info.shr = shr;

                                                    TreeListViewNode ftNode = TreeList.MakeNode(info);
                                                    SetHash(null, ftNode, info);
                                                    TreeList.AddNode(ftNode);

                                                }
                                            }
                                            else
                                            {
                                                ListInfo日付調整 info = new ListInfo日付調整();
                                                info.no = ++検索結果数;
                                                info.thi = thi;
                                                info.mm = mm;
                                                info.mk = mk;
                                                info.jry = jry;

                                                TreeListViewNode ftNode = TreeList.MakeNode(info);
                                                SetHash(null, ftNode, info);
                                                TreeList.AddNode(ftNode);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ListInfo日付調整 info = new ListInfo日付調整();
                                        info.no = ++検索結果数;
                                        info.thi = thi;
                                        info.mm = mm;
                                        info.mk = mk;

                                        TreeListViewNode ftNode = TreeList.MakeNode(info);
                                        SetHash(null, ftNode, info);
                                        TreeList.AddNode(ftNode);
                                    }
                                }
                            }
                            //else
                            //{
                            //    ListInfo日付調整 info = new ListInfo日付調整();
                            //    info.no = ++検索結果数;
                            //    info.thi = thi;
                            //    info.mm = mm;

                            //    TreeListViewNode ftNode = TreeList.MakeNode(info);
                            //    SetHash(null, ftNode, info);
                            //    TreeList.AddNode(ftNode);
                            //}
                        }

                    }
                    //else
                    //{
                    //    ListInfo日付調整 info = new ListInfo日付調整();
                    //    info.no = ++検索結果数;
                    //    info.thi = thi;

                    //    TreeListViewNode ftNode = TreeList.MakeNode(info);
                    //    SetHash(null, ftNode, info);
                    //    TreeList.AddNode(ftNode);
                    //}
                }
            }

            #endregion

            System.Diagnostics.Debug.WriteLine("Data Add End");

            日付調整一覧TreeListView.ResumeUpdate();
        }
        #endregion

        /// <summary>
        /// 「日付調整一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する詳細Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く()
        {
            TreeListViewNode selectedNode = 日付調整一覧TreeListView.SelectedNode;

            BaseForm 詳細Form = null;
            if (IsExistsForm(selectedNode))
            {
                // 既存のフォームをアクティブにする
                詳細Form = Get詳細Form(selectedNode);
                詳細Form.Activate();
            }
            else
            {
                // フォームを作成する
                詳細Form = Create詳細Form(selectedNode);
                if (詳細Form == null)
                    return;
                //詳細Form.MdiParent = MdiForm;
                詳細Form.Show();

                // EventHandlerをセットする
                詳細Form.FormActiveEvent += new BaseForm.FormActiveEventHandler(詳細Form_FormActiveEvent);
                詳細Form.FormClosingEvent += new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
                詳細Form.InfoUpdateEvent += new BaseForm.InfoUpdateEventHandler(詳細Form_InfoUpdateEvent);
            }
        }
        #endregion


        #region public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info)
        public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info)
        {
            if (日付調整一覧InfoHash.Contains(node) == false)
            {
                日付調整一覧InfoHash.Add(node, info);
            }
            if (日付調整一覧ParentNodeHash.Contains(node) == false)
            {
                日付調整一覧ParentNodeHash.Add(node, parentNode);
            }
        }
        #endregion
        #region public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info, BaseForm form)
        public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info, BaseForm form)
        {
            if (日付調整一覧InfoHash.Contains(node) == false)
            {
                日付調整一覧InfoHash.Add(node, info);
                日付調整一覧FormHash.Add(node, form);
                日付調整一覧NodeHash.Add(form, node);
            }
            if (日付調整一覧ParentNodeHash.Contains(node) == false)
            {
                日付調整一覧ParentNodeHash.Add(node, parentNode);
            }
        }
        #endregion

        /// <summary>
        /// ノードに対応する詳細Formがあるかを調べる
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsExistsForm(TreeListViewNode node)
        {
            return 日付調整一覧FormHash.Contains(node);
        }

        /// <summary>
        /// 詳細Formに対応するノードがあるかを調べる
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsExistsNode(BaseForm form)
        {
            return 日付調整一覧NodeHash.Contains(form);
        }

        /// <summary>
        /// ノードに対応する詳細Formをかえす
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BaseForm Get詳細Form(TreeListViewNode node)
        {
            BaseForm form = null;
            try
            {
                form = (BaseForm)日付調整一覧FormHash[node];
            }
            catch
            {
            }
            return form;
        }

        /// <summary>
        /// ノードに対応する詳細Formを作成する
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BaseForm Create詳細Form(TreeListViewNode node)
        {
            ListInfoBase info = 日付調整一覧InfoHash[node] as ListInfoBase;
            BaseForm form = info.CreateForm(null);
            if (form == null)
                return null;
            日付調整一覧FormHash.Add(node, form);
            日付調整一覧NodeHash.Add(form, node);
            return form;
        }

        /// <summary>
        /// 一覧情報に対応する詳細Formを作成する（新規作成の場合に使用）
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BaseForm Create詳細Form(ListInfoBase info)
        {
            BaseForm form = info.CreateForm(null);
            return form;
        }


        //===============================================================================
        // デリゲートを経由して呼び出されるメソッド郡
        //===============================================================================

        /// <summary>
        /// 詳細Formがアクティブになったときのイベント
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_FormActiveEvent(詳細BaseForm senderForm)
        public void 詳細Form_FormActiveEvent(BaseForm senderForm)
        {
            ノード選択(senderForm);
        }
        #endregion

        /// <summary>
        /// 詳細Formがクローズされた時に発生するイベントを受け付ける
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_FormClosingEvent(詳細BaseForm senderForm)
        public void 詳細Form_FormClosingEvent(BaseForm senderForm)
        {
            管理情報削除(senderForm);
        }
        #endregion

        /// <summary>
        /// 詳細Formで情報を更新したときのイベント
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_InfoUpdateEvent(詳細BaseForm senderForm, 一覧情報 senderInfo)
        public void 詳細Form_InfoUpdateEvent(BaseForm senderForm, ListInfoBase senderInfo)
        {
            一覧情報更新(senderForm, senderInfo);
        }
        #endregion




        //===============================================================================
        // デリゲートを経由して呼び出されるメソッドから呼び出されるメソッド
        //===============================================================================
        
        /// <summary>
        /// 詳細Formに対応したノードを選択する
        /// </summary>
        /// <param name="form"></param>
        #region private void ノード選択(詳細BaseForm form)
        private void ノード選択(BaseForm form)
        {
            try
            {
                // フォームに対応するツリーノードを選択する
                TreeListViewNode node = 日付調整一覧NodeHash[form] as TreeListViewNode;
                node.Selected = true;
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 管理している詳細情報を詳細情報Formで更新された情報と置き換える
        /// ノードの文字列を更新する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 一覧情報更新(詳細BaseForm form, 一覧情報 senderInfo)
        private void 一覧情報更新(BaseForm form, ListInfoBase senderInfo)
        {
            try
            {
                TreeListViewNode node = 日付調整一覧NodeHash[form] as TreeListViewNode;
                TreeListViewNode parnetNode = 日付調整一覧ParentNodeHash[node] as TreeListViewNode;
               
                if (senderInfo.Remove == true)
                {
                    // それぞれの詳細画面で「取消」を実行
                    // 一覧(TreeListView)と管理情報から削除する
                    #region
                    日付調整一覧InfoHash.Remove(node);
                    日付調整一覧FormHash.Remove(node);
                    日付調整一覧NodeHash.Remove(form);

                    if (parnetNode == null)
                    {
                        日付調整一覧TreeListView.Nodes.Remove(node);
                    }
                    else
                    {
                        parnetNode.Nodes.Remove(node);
                    }
                    #endregion
                }
                else
                {
                    // 一覧(TreeListView)と管理情報を更新する
                    #region
                    日付調整一覧InfoHash[node] = senderInfo;

                    TreeList.SuspendUpdate();
                    int idx = -1;
                    foreach (TreeListViewNode n in 日付調整一覧TreeListView.Nodes)
                    {
                        idx++;
                        if (n == node)
                        {
                            日付調整一覧TreeListView.Nodes.RemoveAt(idx);
                            break;
                        }
                    }

                    ListInfo日付調整 info = senderInfo as ListInfo日付調整;
                    TreeListViewNode newNode = TreeList.MakeNode(info);
                    node.SubItems.Clear();
                    foreach (TreeListViewSubItem subItem in newNode.SubItems)
                    {
                        node.SubItems.Add(subItem);
                    }
                    日付調整一覧TreeListView.Nodes.Insert(idx, node);
                    日付調整一覧TreeListView.Refresh();

                    TreeList.ResumeUpdate();
                    #endregion
                }
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 詳細情報Formの管理情報を削除する
        /// </summary>
        /// <param name="form"></param>
        #region private void 管理情報削除(詳細BaseForm form)
        private void 管理情報削除(BaseForm form)
        {
            try
            {
                // ハッシュから削除する
                TreeListViewNode node = 日付調整一覧NodeHash[form] as TreeListViewNode;
                node.Selected = false;
                日付調整一覧NodeHash.Remove(form);
                日付調整一覧FormHash.Remove(node);
            }
            catch
            {
            }
        }
        #endregion
    }
}
