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

namespace Hachu.Controllers
{
    public class 振替取立一覧情報Controller
    {
        private Form MdiForm;
        private ItemTreeListView振替取立 TreeList = null;
        private TreeListView 振替取立一覧TreeListView = null;

        public Hashtable 振替取立一覧InfoHash = null;
        public Hashtable 振替取立一覧FormHash = null;
        public Hashtable 振替取立一覧NodeHash = null;
        public Hashtable 振替取立一覧ParentNodeHash = null;

        public int 検索結果数 = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 振替取立一覧情報Controller(Form form, TreeListView treeListView)
        public 振替取立一覧情報Controller(Form form, TreeListView treeListView)
        {
            // MainForm
            MdiForm = form;

            // 対象のTreeListView
            振替取立一覧TreeListView = treeListView;

            // 情報管理用
            振替取立一覧InfoHash = new Hashtable();
            振替取立一覧FormHash = new Hashtable();
            振替取立一覧NodeHash = new Hashtable();
            振替取立一覧ParentNodeHash = new Hashtable();
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        #region public void 初期化(int width)
        public void 初期化(int width)
        {
            object[,] columns = new object[,] {
                                           {"発注日", 75, null, null},
                                           {"項目", 225, null, null},
                                           {"区分", 105, null, null},
                                           {"業者", 200, null, null},
                                           {"数量", 50, null, HorizontalAlignment.Right},
                                           {"単価", 85, null, HorizontalAlignment.Right},
                                           {"金額", 90, null, HorizontalAlignment.Right},
                                           {"完了日", 75, null, null},
                                           {"請求書日", 75, null, null},
                                           {"起票日", 75, null, null},
                                       　　{"備考", 200, null, null}
                                         };
            TreeList = new ItemTreeListView振替取立(振替取立一覧TreeListView);
            TreeList.SetColumns(-2,columns);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void 一覧更新(振替取立一覧検索条件 検索条件)
        {
            検索結果数 = 0;
            振替取立一覧TreeListView.Nodes.Clear();

            振替取立一覧InfoHash.Clear();
            振替取立一覧FormHash.Clear();
            振替取立一覧NodeHash.Clear();
            振替取立一覧ParentNodeHash.Clear();

            OdFurikaeToritateFilter filter = new OdFurikaeToritateFilter();
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
            List<OdFurikaeToritate> OdFurikaeToritates = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    OdFurikaeToritates = serviceClient.OdFurikaeToritate_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            TreeList.SuspendUpdate();

            foreach (OdFurikaeToritate furikaeToritate in OdFurikaeToritates)
            {
                ListInfo振替取立 ftInfo = new ListInfo振替取立();
                ftInfo.info = furikaeToritate;

                //ノードを作成
                TreeListViewNode ftNode = TreeList.MakeNode(furikaeToritate);
                SetHash(null, ftNode, ftInfo);
                TreeList.AddNode(ftNode);

                検索結果数++;
            }
            TreeList.ResumeUpdate();
        }

        /// <summary>
        /// 「振替取立一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する詳細Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く()
        {
            TreeListViewNode selectedNode = 振替取立一覧TreeListView.SelectedNode;

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


        /// <summary>
        /// 「振替取立一覧」Form の 「追加」ボタンクリック
        /// ・振替取立Formを開く
        /// </summary>
        #region public void 新規Formを開く()
        public void 新規Formを開く()
        {
            // 手配依頼する種別を選択する
            依頼種別Form form = new 依頼種別Form((int)依頼種別Form.FUNCTION_TYPE.振替取立);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // 振替取立を用意する
            ListInfo振替取立 info = new ListInfo振替取立();
            OdFurikaeToritate fkTt = new OdFurikaeToritate();
            #region
            // 振替取立ID
            fkTt.OdFurikaeToritateID = null;
            // 船
            fkTt.MsVesselID = form.SelectedVessel.MsVesselID;
            fkTt.MsVesselName = form.SelectedVessel.VesselName;
            // 手配依頼種別
            MsThiIraiSbt thiItaiSbt = form.SelectedThiIraiSbt;
            fkTt.MsThiIraiSbtID = thiItaiSbt.MsThiIraiSbtID;
            fkTt.MsThiIraiSbtName = thiItaiSbt.ThiIraiSbtName;
            // 手配依頼詳細種別
            if (form.SelectedThiIraiShousai != null)
            {
                MsThiIraiShousai thiIraiShousai = form.SelectedThiIraiShousai;
                fkTt.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
                fkTt.MsThiIraiShousaiName = thiIraiShousai.ThiIraiShousaiName;
            }
            // 発注者
            fkTt.CreateUserID = NBaseCommon.Common.LoginUser.MsUserID;
            fkTt.CreateUserName = NBaseCommon.Common.LoginUser.FullName;
            // 更新者
            fkTt.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            fkTt.RenewUserName = NBaseCommon.Common.LoginUser.FullName;
            #endregion
            info.info = fkTt;

            // フォームを作成する
            BaseForm 詳細Form = Create詳細Form((ListInfoBase)info);
            詳細Form.MdiParent = MdiForm;
            詳細Form.Show();

            // EventHandlerをセットする
            詳細Form.InfoUpdateEvent += new BaseForm.InfoUpdateEventHandler(新規登録Event);
        }
        #endregion

        public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info)
        {
            if (振替取立一覧InfoHash.Contains(node) == false)
            {
                振替取立一覧InfoHash.Add(node, info);
            }
            if (振替取立一覧ParentNodeHash.Contains(node) == false)
            {
                振替取立一覧ParentNodeHash.Add(node, parentNode);
            }
        }
        public void SetHash(TreeListViewNode parentNode, TreeListViewNode node, ListInfoBase info, BaseForm form)
        {
            if (振替取立一覧InfoHash.Contains(node) == false)
            {
                振替取立一覧InfoHash.Add(node, info);
                振替取立一覧FormHash.Add(node, form);
                振替取立一覧NodeHash.Add(form, node);
            }
            if (振替取立一覧ParentNodeHash.Contains(node) == false)
            {
                振替取立一覧ParentNodeHash.Add(node, parentNode);
            }
        }

        /// <summary>
        /// ノードに対応する詳細Formがあるかを調べる
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsExistsForm(TreeListViewNode node)
        {
            return 振替取立一覧FormHash.Contains(node);
        }

        /// <summary>
        /// 詳細Formに対応するノードがあるかを調べる
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsExistsNode(BaseForm form)
        {
            return 振替取立一覧NodeHash.Contains(form);
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
                form = (BaseForm)振替取立一覧FormHash[node];
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
            ListInfoBase info = 振替取立一覧InfoHash[node] as ListInfoBase;
            BaseForm form = info.CreateForm((int)BaseForm.WINDOW_STYLE.通常);
            if (form == null)
                return null;
            振替取立一覧FormHash.Add(node, form);
            振替取立一覧NodeHash.Add(form, node);
            return form;
        }

        /// <summary>
        /// 一覧情報に対応する詳細Formを作成する（新規作成の場合に使用）
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BaseForm Create詳細Form(ListInfoBase info)
        {
            BaseForm form = info.CreateForm((int)BaseForm.WINDOW_STYLE.通常);
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

        /// <summary>
        /// 振替取立Formで「更新」ボタンをクリックしたときに呼び出すメソッド
        /// </summary>
        /// <param name="senderForm"></param>
        /// <param name="senderInfo"></param>
        #region public void 新規登録Event(詳細BaseForm senderForm, 一覧情報 senderInfo)
        public void 新規登録Event(BaseForm senderForm, ListInfoBase senderInfo)
        {
            if (IsExistsNode(senderForm) == true)
            {
                一覧情報更新(senderForm, senderInfo);
            }
            else
            {
                新規情報追加(senderForm, senderInfo);
            }
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
                TreeListViewNode node = 振替取立一覧NodeHash[form] as TreeListViewNode;
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
                TreeListViewNode node = 振替取立一覧NodeHash[form] as TreeListViewNode;
                TreeListViewNode parnetNode = 振替取立一覧ParentNodeHash[node] as TreeListViewNode;
               
                if (senderInfo.Remove == true)
                {
                    // それぞれの詳細画面で「取消」を実行
                    // 一覧(TreeListView)と管理情報から削除する
                    #region
                    振替取立一覧InfoHash.Remove(node);
                    振替取立一覧FormHash.Remove(node);
                    振替取立一覧NodeHash.Remove(form);

                    if (parnetNode == null)
                    {
                        振替取立一覧TreeListView.Nodes.Remove(node);
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
                    振替取立一覧InfoHash[node] = senderInfo;

                    TreeList.SuspendUpdate();
                    int idx = -1;
                    foreach (TreeListViewNode n in 振替取立一覧TreeListView.Nodes)
                    {
                        idx++;
                        if (n == node)
                        {
                            振替取立一覧TreeListView.Nodes.RemoveAt(idx);
                            break;
                        }
                    }

                    ListInfo振替取立 liFkTt = senderInfo as ListInfo振替取立;
                    TreeListViewNode newNode = TreeList.MakeNode(liFkTt.info);
                    node.SubItems.Clear();
                    foreach (TreeListViewSubItem subItem in newNode.SubItems)
                    {
                        node.SubItems.Add(subItem);
                    }
                    振替取立一覧TreeListView.Nodes.Insert(idx, node);
                    振替取立一覧TreeListView.Refresh();

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
                TreeListViewNode node = 振替取立一覧NodeHash[form] as TreeListViewNode;
                node.Selected = false;
                振替取立一覧NodeHash.Remove(form);
                振替取立一覧FormHash.Remove(node);
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 新規に作成された情報を管理情報に追加する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 新規情報追加(詳細BaseForm form, 一覧情報 senderInfo)
        private void 新規情報追加(BaseForm form, ListInfoBase senderInfo)
        {
            try
            {
                ListInfo振替取立 info = (ListInfo振替取立)senderInfo;

                //ノードを作成
                TreeListViewNode node = TreeList.MakeNode(info.info);

                // ノードと一覧情報を関連付ける
                SetHash(null, node, info, form);

                // 一覧部に表示
                TreeList.AddNode(node);

                // EventHandlerを追加する
                form.FormActiveEvent += new BaseForm.FormActiveEventHandler(詳細Form_FormActiveEvent);
                form.FormClosingEvent += new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
            }
            catch
            {
            }
        }
        #endregion

    }
}
