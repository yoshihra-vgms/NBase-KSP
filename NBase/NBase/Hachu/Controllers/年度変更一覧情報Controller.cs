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
    public class 年度変更一覧情報Controller
    {
        private Form MdiForm;
        private ItemTreeListView年度変更 TreeList = null;
        private TreeListView 年度変更一覧TreeListView = null;

        public Hashtable 年度変更一覧InfoHash = null;
        public Hashtable 年度変更一覧FormHash = null;
        public Hashtable 年度変更一覧NodeHash = null;
        public Hashtable 年度変更一覧ParentNodeHash = null;

        public int 検索結果数 = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 年度変更情報Controller(Form form, TreeListView treeListView)
        public 年度変更一覧情報Controller(Form form, TreeListView treeListView)
        {
            // MainForm
            MdiForm = form;

            // 対象のTreeListView
            年度変更一覧TreeListView = treeListView;

            // 情報管理用
            年度変更一覧InfoHash = new Hashtable();
            年度変更一覧FormHash = new Hashtable();
            年度変更一覧NodeHash = new Hashtable();
            年度変更一覧ParentNodeHash = new Hashtable();
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        #region public void 初期化(int width)
        public void 初期化(int width)
        {
            object[,] columns = new object[,] {
                                           {"", 50, null, null},
                                           {"船名", 150, null, null},
                                           {"種別", 85, null, null},
                                           {"詳細種別", 85, null, null},
                                           {"件名", 350, null, null},
                                           {"手配依頼日", 80, null, null},
                                           {"見積依頼日", 80, null, null},
                                           {"回答日", 80, null, null},
                                           {"発注日", 80, null, null},
                                           {"回答番号", 80, null, null},
                                           {"状況", 150, null, null}
                                         };
            TreeList = new ItemTreeListView年度変更(年度変更一覧TreeListView);
            TreeList.SetColumns(-2,columns);
        }
        #endregion
        
        /// <summary>
        /// 終了処理
        /// </summary>
        #region public void 終了()
        public void 終了()
        {
            foreach (BaseForm form in 年度変更一覧NodeHash.Keys)
            {
                form.FormClosingEvent -= new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
                form.Close();
            }
        }
        #endregion

        /// <summary>
        /// 検索、一覧更新
        /// </summary>
        #region public void 一覧更新(発注一覧検索条件 検索条件)
        public void 一覧更新(発注一覧検索条件 検索条件)
        {
            検索結果数 = 0;
            年度変更一覧TreeListView.Nodes.Clear();

            年度変更一覧InfoHash.Clear();
            年度変更一覧FormHash.Clear();
            年度変更一覧NodeHash.Clear();
            年度変更一覧ParentNodeHash.Clear();

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
            filter.Nendo = 検索条件.nendo;
            #endregion

            // 検索
            List<MsVessel> Vessels = null;
            List<OdThi> OdThis = null;
            List<OdMm> OdMms = null;
            List<OdMk> OdMks = null;
            List<OdJry> OdJrys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    Vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);

                    OdThis = serviceClient.OdThi_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                    OdMms = serviceClient.OdMm_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                    OdMks = serviceClient.OdMk_GetRecordsByFilter(NBaseCommon.Common.LoginUser, int.MinValue, filter);
                    OdJrys = serviceClient.OdJry_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, filter);

                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            年度変更一覧TreeListView.SuspendUpdate();

            DateTime nextBisunessYearStart = DateTime.Parse(filter.Nendo + "/" + NBaseCommon.Common.FiscalYearStartMonth.ToString("00") + "/01").AddYears(1);

            #region
            foreach (MsVessel vessel in Vessels)
            {
                var ohthis = OdThis.Where(obj => obj.MsVesselID == vessel.MsVesselID);
                if (ohthis.Count() == 0)
                    continue;

                foreach (OdThi thi in ohthis)
                {
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
                                            if (jry.JryDate == DateTime.MinValue || jry.JryDate >= nextBisunessYearStart)
                                            {
                                                // 受領データがあるが、受領日がセットされていない
                                                // 受領受領日がセットされているが、次年度
                                                ListInfo年度変更 info = new ListInfo年度変更();
                                                info.no = ++検索結果数;
                                                info.thi = thi;
                                                info.mm = mm;
                                                info.mk = mk;
                                                info.jry = jry;

                                                if (jry.JryDate == DateTime.MinValue)
                                                    jry.UserKey = "未受領";
                                                else
                                                    jry.UserKey = jry.JryDate.ToShortDateString() + " >= " + nextBisunessYearStart.ToShortDateString();


                                                TreeListViewNode ftNode = TreeList.MakeNode(info);
                                                SetHash(null, ftNode, info);
                                                TreeList.AddNode(ftNode);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        // 受領データがない = 未受領
                                        ListInfo年度変更 info = new ListInfo年度変更();
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
                        }

                    }
                }
            }
            #endregion

            年度変更一覧TreeListView.ResumeUpdate();
        }
        #endregion

        /// <summary>
        /// 「年度変更一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する詳細Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く()
        {
            TreeListViewNode selectedNode = 年度変更一覧TreeListView.SelectedNode;

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
                詳細Form.MdiParent = MdiForm;
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
            if (年度変更一覧InfoHash.Contains(node) == false)
            {
                年度変更一覧InfoHash.Add(node, info);
            }
            if (年度変更一覧ParentNodeHash.Contains(node) == false)
            {
                年度変更一覧ParentNodeHash.Add(node, parentNode);
            }
        }
        #endregion
        #region public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info, BaseForm form)
        public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info, BaseForm form)
        {
            if (年度変更一覧InfoHash.Contains(node) == false)
            {
                年度変更一覧InfoHash.Add(node, info);
                年度変更一覧FormHash.Add(node, form);
                年度変更一覧NodeHash.Add(form, node);
            }
            if (年度変更一覧ParentNodeHash.Contains(node) == false)
            {
                年度変更一覧ParentNodeHash.Add(node, parentNode);
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
            return 年度変更一覧FormHash.Contains(node);
        }

        /// <summary>
        /// 詳細Formに対応するノードがあるかを調べる
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsExistsNode(BaseForm form)
        {
            return 年度変更一覧NodeHash.Contains(form);
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
                form = (BaseForm)年度変更一覧FormHash[node];
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
            //ListInfoBase info = 年度変更一覧InfoHash[node] as ListInfoBase;
            //BaseForm form = info.CreateForm((int)BaseForm.WINDOW_STYLE.通常);
            //if (form == null)
            //    return null;
            //年度変更一覧FormHash.Add(node, form);
            //年度変更一覧NodeHash.Add(form, node);
            //return form;
            return null;
        }

        /// <summary>
        /// 一覧情報に対応する詳細Formを作成する（新規作成の場合に使用）
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BaseForm Create詳細Form(ListInfoBase info)
        {
            //BaseForm form = info.CreateForm((int)BaseForm.WINDOW_STYLE.通常);
            //return form;
            return null;
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
                TreeListViewNode node = 年度変更一覧NodeHash[form] as TreeListViewNode;
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
                TreeListViewNode node = 年度変更一覧NodeHash[form] as TreeListViewNode;
                TreeListViewNode parnetNode = 年度変更一覧ParentNodeHash[node] as TreeListViewNode;
               
                if (senderInfo.Remove == true)
                {
                    // それぞれの詳細画面で「取消」を実行
                    // 一覧(TreeListView)と管理情報から削除する
                    #region
                    年度変更一覧InfoHash.Remove(node);
                    年度変更一覧FormHash.Remove(node);
                    年度変更一覧NodeHash.Remove(form);

                    if (parnetNode == null)
                    {
                        年度変更一覧TreeListView.Nodes.Remove(node);
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
                    年度変更一覧InfoHash[node] = senderInfo;

                    TreeList.SuspendUpdate();
                    int idx = -1;
                    foreach (TreeListViewNode n in 年度変更一覧TreeListView.Nodes)
                    {
                        idx++;
                        if (n == node)
                        {
                            年度変更一覧TreeListView.Nodes.RemoveAt(idx);
                            break;
                        }
                    }

                    ListInfo年度変更 info = senderInfo as ListInfo年度変更;
                    TreeListViewNode newNode = TreeList.MakeNode(info);
                    node.SubItems.Clear();
                    foreach (TreeListViewSubItem subItem in newNode.SubItems)
                    {
                        node.SubItems.Add(subItem);
                    }
                    年度変更一覧TreeListView.Nodes.Insert(idx, node);
                    年度変更一覧TreeListView.Refresh();

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
                TreeListViewNode node = 年度変更一覧NodeHash[form] as TreeListViewNode;
                node.Selected = false;
                年度変更一覧NodeHash.Remove(form);
                年度変更一覧FormHash.Remove(node);
            }
            catch
            {
            }
        }
        #endregion



        public List<OdMk> GetCheckedOdMk()
        {
            List<OdMk> ret = new List<OdMk>();

            List<TreeListViewNode> checkedNodes = TreeList.GetCheckedNodes();
            foreach(TreeListViewNode node in checkedNodes)
            {
                ListInfo年度変更 info = 年度変更一覧InfoHash[node] as ListInfo年度変更;

                ret.Add(info.mk);
            }


            return ret;
        }
    }
}
