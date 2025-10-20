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
    public class 発注一覧情報Controller
    {
        private Form MdiForm;


        private LidorSystems.IntegralUI.Containers.TabControl 発注タブ = null;
        private SplitContainer 発注一覧エリア = null;
        private HachuListControl 発注一覧Control = null;

        private Dictionary<int, BaseUserControl> Dic_TabPages = new Dictionary<int, BaseUserControl>();
        private ListInfo手配依頼 Selected手配 = null;

        //public Hashtable 発注一覧InfoHash = null;
        //public Hashtable 発注一覧FormHash = null;
        //public Hashtable 発注一覧NodeHash = null;
        //public Hashtable 発注一覧ParentNodeHash = null;

        private 発注一覧検索条件 検索条件 = null;

        public int 検索結果数 = 0;

        private BaseForm 新規登録Form = null;
        private HachuManage.支払合算Form 支払合算Form = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 発注一覧情報Controller(Form form, TabControl tabControl, SplitContainer splitContainer, HachuListControl hachuListControl)
        //miho public 発注一覧情報Controller(Form form, LidorSystems.IntegralUI.Containers.TabControl tabControl, SplitContainer splitContainer, HachuListControl hachuListControl)
        public 発注一覧情報Controller(Form form, LidorSystems.IntegralUI.Containers.TabControl tabControl, SplitContainer splitContainer, HachuListControl hachuListControl)
        {
            // MainForm
            MdiForm = form;

            // タブ部分
            発注タブ = tabControl;
            tabControl.Visible = false;

            // 一覧、および、詳細表示部分
            発注一覧エリア = splitContainer;
            発注一覧エリア.Panel2Collapsed = true;

            // 対象のTreeListView
            発注一覧Control = hachuListControl;
            発注一覧Control.ClickEvent += new HachuListControl.ClickEventHandler(詳細Formを開く);
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

            発注タブ.Visible = false;
            発注一覧エリア.Panel2Collapsed = true;

            発注一覧Control.IsContains船受領 = 検索条件.status船受領;
            検索結果数 = 発注一覧Control.DrawList(OdThis, OdMms, OdMks, OdJrys, OdShrs);
        }
        #endregion


        private void AddPage(string kind)
        {
            this.AddPage(-1, kind);
        }
        /// <summary>
        /// タブにページを追加する　miho 
        /// </summary>
        /// <param name="pos">タブを入れる位置。0以下は無視</param>
        /// /// <param name="kind"></param>
        private void AddPage(int pos, string kind)
        {
            string 空白 = "    ";//だめだった　"<span style = \"margin-right: 4em; margin-left: 4em;\"></span>";

            LidorSystems.IntegralUI.Containers.TabPage p = new LidorSystems.IntegralUI.Containers.TabPage();
            
            p.StyleFromParent = false;
            p.NormalStyle.BackFadeColor = Color.White;
            p.HoverStyle.BackFadeColor = Color.White;
            p.FormatStyle.Padding=new Padding(5);

            string title = "";
            switch (kind)
            {
                case "手配依頼":
                    p.NormalStyle.BackColor = Color.FromArgb(255, 255, 192);
                    p.NormalStyle.BorderColor = Color.FromArgb(255, 255, 050);
                    p.SelectedStyle.BackColor = Color.FromArgb(255, 255, 050);
                    p.SelectedStyle.BorderColor = Color.FromArgb(255, 255, 050);
                    title = kind;
                    break;
                case "見積依頼":
                    p.NormalStyle.BackColor = Color.FromArgb(192, 255, 192);
                    p.NormalStyle.BorderColor = Color.FromArgb(050, 255, 050);
                    p.SelectedStyle.BackColor = Color.FromArgb(050, 255, 050);
                    p.SelectedStyle.BorderColor = Color.FromArgb(050, 255, 050);
                    title = kind;
                    break;
                case "見積回答":
                    p.NormalStyle.BackColor = Color.FromArgb(192, 192, 255);
                    p.NormalStyle.BorderColor = Color.FromArgb(050, 050, 255);
                    p.SelectedStyle.BackColor = Color.FromArgb(050, 050, 255);
                    p.SelectedStyle.BorderColor = Color.FromArgb(050, 050, 255);
                    title = kind;
                    break;
                case "受領":
                    p.NormalStyle.BackColor = Color.FromArgb(255, 192, 255);
                    p.NormalStyle.BorderColor = Color.FromArgb(255, 050, 255);
                    p.SelectedStyle.BackColor = Color.FromArgb(255, 050, 255);
                    p.SelectedStyle.BorderColor = Color.FromArgb(255, 050, 255);
                    title = 空白 + kind+ 空白;
                    break;
                case "支払":
                    p.NormalStyle.BackColor = Color.FromArgb(192, 255, 255);
                    p.NormalStyle.BorderColor = Color.FromArgb(050, 255, 255);
                    p.SelectedStyle.BackColor = Color.FromArgb(050, 255, 255);
                    p.SelectedStyle.BorderColor = Color.FromArgb(050, 255, 255);
                    title = 空白 + kind + 空白;
                    break;
            }
            p.HoverStyle.BackColor = LidorSystems.IntegralUI.Drawing.CommonMethods.ChangeColor(p.NormalStyle.BackColor, 35, true);

            //string tagText = "<div><table><tr><td rowspan=\"2\"><img style=\"align:topleft\" assemblypath=\"D:\\VG\\tool調査\\tab-control-multi-color-tabs\\Demo\\QuickStart.exe\" resource=\"Resources.search.ico\"></img></td><td><font face=\"ＭＳ Ｐゴシック\" size=\"15\"><b>Knowledge base</b></font></td></tr><tr><td>Searching ...</td></tr></table></div>";
            string tagText = "<div><font face=\"ＭＳ Ｐゴシック\" size=\"15\"><b>" + title + "</b></font></div>";
            p.TabContent = tagText;

            if (pos < 0)
            {
                発注タブ.Pages.Add(p);
            }
            else
            {
                発注タブ.Pages.Insert(pos, p);
            }
        }

        /// <summary>
        /// 「発注一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する新規登録Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く(ListInfo手配依頼 thiInfo)
        {

            Selected手配 = thiInfo;

            発注一覧エリア.Parent.Cursor = Cursors.WaitCursor;

            発注タブ.Visible = true;
            //発注タブ.TabPages.Clear(); miho
            //発注タブ.TabPages.Add("手配依頼");miho
            発注タブ.Pages.Clear();
            AddPage("手配依頼");

            if (thiInfo.Children != null)
            {
                foreach (ListInfoBase info in thiInfo.Children)
                {
                    if (info is ListInfo見積依頼)
                    {
                        //発注タブ.TabPages.Add("見積依頼"); miho
                        AddPage("見積依頼");

                        foreach (ListInfoBase mkInfo in info.Children)
                        {
                            //発注タブ.TabPages.Add("見積回答");miho
                            AddPage("見積回答");

                            foreach (ListInfoBase jryInfo in mkInfo.Children)
                            {
                                //発注タブ.TabPages.Add("　受領　");miho
                                AddPage("受領");

                                foreach (ListInfoBase shrInfo in jryInfo.Children)
                                {
                                    //発注タブ.TabPages.Add("　支払　");miho
                                    AddPage("支払");
                                }
                            }
                        }
                    }
                    else if (info is ListInfo見積回答)
                    {
                        //発注タブ.TabPages.Add("見積回答");miho
                        AddPage("見積回答");

                        foreach (ListInfoBase jryInfo in info.Children)
                        {
                            //発注タブ.TabPages.Add("　受領　");miho
                            AddPage("受領");

                            foreach (ListInfoBase shrInfo in jryInfo.Children)
                            {
                                //発注タブ.TabPages.Add("　支払　");miho
                                AddPage("支払");
                            }
                        }
                    }
                    else if (info is ListInfo受領)
                    {
                        //発注タブ.TabPages.Add("　受領　");miho
                        AddPage("受領");

                        foreach (ListInfoBase shrInfo in info.Children)
                        {
                            //発注タブ.TabPages.Add("　支払　");miho
                            AddPage("支払");
                        }
                    }
                }
            }


            BaseUserControl form = thiInfo.CreatePanel((int)BaseUserControl.WINDOW_STYLE.通常);
            form.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(詳細Form_InfoUpdateEvent);

            if (Dic_TabPages == null)
                Dic_TabPages = new Dictionary<int, BaseUserControl>();
            else
                Dic_TabPages.Clear();

            Dic_TabPages.Add(0, form);


            発注一覧エリア.Panel2.Controls.Clear();
            発注一覧エリア.Panel2.Controls.Add(form);

            発注一覧エリア.SplitterDistance = 発注一覧エリア.Width - form.Width;

            form.Dock = DockStyle.Fill;

            発注一覧エリア.Panel2Collapsed = false;

            発注一覧エリア.Parent.Cursor = Cursors.Default;

            //手配選択 m.yoshihara 
            発注タブ.SelectedPage = 発注タブ.Pages[0];
        }
        #endregion

        /// <summary>
        /// 「発注一覧」Form の 「新規手配」ボタンクリック
        /// ・新規手配依頼Formを開く
        /// </summary>
        /// <param name="vsl">発注状況一覧Formで選択された船　2021/08/04 m.ysohihara</param>
        #region public void 新規手配依頼Formを開く(MsVessel vsl)
        public void 新規手配依頼Formを開く(MsVessel vsl)
        {
            if (新規登録Form != null)
            {
                if (新規登録Form.IsDisposed == false)
                {
                    MessageBox.Show("新規登録操作中です。新規登録操作を完了してください。");
                    return;
                }
                else
                {
                    新規登録Form = null;
                }
            }

            // 手配依頼する種別を選択する
            //依頼種別Form form = new 依頼種別Form((int)依頼種別Form.FUNCTION_TYPE.手配);
            依頼種別Form form = new 依頼種別Form((int)依頼種別Form.FUNCTION_TYPE.手配, vsl);//引数追加　2021/08/04 m.ysohihara
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // 手配依頼を用意する
            ListInfo手配依頼 info = new ListInfo手配依頼();
            OdThi thi = new OdThi();
            #region
            // 手配依頼ID
            thi.OdThiID = Hachu.Common.CommonDefine.新規ID();
            // ステータス
            thi.Status = thi.OdStatusValue.Values[(int)OdThi.STATUS.事務所未手配].Value;
            // 手配依頼番号
            thi.TehaiIraiNo = "";
            // 船
            thi.MsVesselID = form.SelectedVessel.MsVesselID;
            thi.VesselName = form.SelectedVessel.VesselName;
            // 手配依頼種別
            MsThiIraiSbt thiItaiSbt = form.SelectedThiIraiSbt;
            thi.MsThiIraiSbtID = thiItaiSbt.MsThiIraiSbtID;
            thi.ThiIraiSbtName = thiItaiSbt.ThiIraiSbtName;
            // 手配依頼詳細種別
            if (form.SelectedThiIraiShousai != null)
            {
                MsThiIraiShousai thiIraiShousai = form.SelectedThiIraiShousai;
                thi.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
                thi.ThiIraiShousaiName = thiIraiShousai.ThiIraiShousaiName;
            }
            else if (form.SelectedMitsumoriUmu != -1)
            {
                thi.MmFlag = form.SelectedMitsumoriUmu;
            }
            // 手配依頼者
            thi.ThiUserID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.ThiUserName = NBaseCommon.Common.LoginUser.FullName;
            // 事務担当者
            thi.JimTantouID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.JimTantouName = NBaseCommon.Common.LoginUser.FullName;
            // 更新者
            thi.RenewUserID = "";
            #endregion
            info.info = thi;

            // フォームを作成する
            手配依頼Form baseControl = new 手配依頼Form((int)BaseUserControl.WINDOW_STYLE.通常, info, true);//引数追加　2021/08/04 m.ysohihara

            新規登録Form = info.CreateForm(baseControl);
            新規登録Form.MdiParent = MdiForm;
            新規登録Form.Show();

            // EventHandlerをセットする
            baseControl.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(新規手配Event);
        }
        #endregion

        /// <summary>
        /// 「発注一覧」Form の 「新規見積」ボタンクリック
        /// ・新規見積依頼Formを開く
        /// </summary>
        /// <param name="vsl">発注状況一覧Formで選択された船　2021/08/04 m.ysohihara</param>
        #region public void 新規見積依頼Formを開く(MsVessel vsl)
        public void 新規見積依頼Formを開く(MsVessel vsl)
        {
            if (新規登録Form != null)
            {
                if (新規登録Form.IsDisposed == false)
                {
                    MessageBox.Show("新規登録操作中です。新規登録操作を完了してください。");
                    return;
                }
                else
                {
                    新規登録Form = null;
                }
            }

            // 手配依頼する種別を選択する
            依頼種別Form form = new 依頼種別Form((int)依頼種別Form.FUNCTION_TYPE.見積, vsl);//引数追加　2021/08/04 m.ysohihara
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            // 手配依頼を用意する
            OdThi thi = new OdThi();
            #region
            thi.OdThiID = Hachu.Common.CommonDefine.新規ID(false);
            thi.Status = thi.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value;
            thi.TehaiIraiNo = "";
            thi.MsVesselID = form.SelectedVessel.MsVesselID;
            thi.VesselName = form.SelectedVessel.VesselName;
            MsThiIraiSbt thiItaiSbt = form.SelectedThiIraiSbt;
            thi.MsThiIraiSbtID = thiItaiSbt.MsThiIraiSbtID;
            thi.ThiIraiSbtName = thiItaiSbt.ThiIraiSbtName;
            if (form.SelectedThiIraiShousai != null)
            {
                MsThiIraiShousai thiIraiShousai = form.SelectedThiIraiShousai;
                thi.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
                thi.ThiIraiShousaiName = thiIraiShousai.ThiIraiShousaiName;
            }
            thi.ThiUserID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.ThiUserName = NBaseCommon.Common.LoginUser.FullName;
            thi.JimTantouID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.JimTantouName = NBaseCommon.Common.LoginUser.FullName;
            thi.VesselID = form.SelectedVessel.MsVesselID;
            #endregion

            // 見積依頼を用意する
            OdMm mm = new OdMm();
            #region
            mm.OdMmID = Hachu.Common.CommonDefine.新規ID(false);
            mm.Status = mm.OdStatusValue.Values[(int)OdMm.STATUS.見積依頼済].Value;
            mm.VesselID = thi.MsVesselID;
            mm.MmDate = RenewDate;
            mm.MmSakuseisha = RenewUserID;
            #endregion

            // 見積回答を用意する
            OdMk mk = new OdMk();
            #region
            mk.OdMkID = Hachu.Common.CommonDefine.新規ID(false);
            mk.Status = mk.OdStatusValue.Values[(int)OdMk.STATUS.未回答].Value;
            mk.VesselID = thi.MsVesselID;
            #endregion

            ListInfo見積依頼 info = new ListInfo見積依頼();
            info.parent = thi;
            info.info = mm;
            info.child = mk;

            // フォームを作成する
            新規見積依頼Form baseControl = new 新規見積依頼Form((int)BaseUserControl.WINDOW_STYLE.通常, info, true);//引数追加　2021/08/04 m.ysohihara

            新規登録Form = info.CreateForm(baseControl);
            新規登録Form.MdiParent = MdiForm;
            新規登録Form.Show();

            // EventHandlerをセットする
            baseControl.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(新規見積Event);
        }
        #endregion

        /// <summary>
        /// 「発注一覧」Form の 「新規発注」ボタンクリック
        /// ・新規発注Formを開く
        /// </summary>
        /// <param name="vsl">発注状況一覧Formで選択された船　2021/08/04 m.ysohihara</param>
        #region public void 新規発注Formを開く(MsVessel vsl)
        public void 新規発注Formを開く(MsVessel vsl)
        {
            if (新規登録Form != null)
            {
                if (新規登録Form.IsDisposed == false)
                {
                    MessageBox.Show("新規登録操作中です。新規登録操作を完了してください。");
                    return;
                }
                else
                {
                    新規登録Form = null;
                }
            }

            // 手配依頼する種別を選択する
            依頼種別Form form = new 依頼種別Form((int)依頼種別Form.FUNCTION_TYPE.発注, vsl);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }


            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            // 手配依頼を用意する
            OdThi thi = new OdThi();
            #region
            thi.OdThiID = Hachu.Common.CommonDefine.新規ID(false);
            thi.Status = thi.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value;
            thi.TehaiIraiNo = "";
            thi.MsVesselID = form.SelectedVessel.MsVesselID;
            thi.VesselName = form.SelectedVessel.VesselName;
            MsThiIraiSbt thiItaiSbt = form.SelectedThiIraiSbt;
            thi.MsThiIraiSbtID = thiItaiSbt.MsThiIraiSbtID;
            thi.ThiIraiSbtName = thiItaiSbt.ThiIraiSbtName;
            if (form.SelectedThiIraiShousai != null)
            {
                MsThiIraiShousai thiIraiShousai = form.SelectedThiIraiShousai;
                thi.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
                thi.ThiIraiShousaiName = thiIraiShousai.ThiIraiShousaiName;
            }
            thi.ThiUserID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.ThiUserName = NBaseCommon.Common.LoginUser.FullName;
            thi.JimTantouID = NBaseCommon.Common.LoginUser.MsUserID;
            thi.JimTantouName = NBaseCommon.Common.LoginUser.FullName;
            thi.MmFlag = (int)OdThi.MM_FLAG.なし;
            thi.VesselID = thi.MsVesselID;
            #endregion

            // 見積依頼を用意する
            OdMm mm = new OdMm();
            #region
            mm.OdMmID = Hachu.Common.CommonDefine.新規ID(false);
            mm.Status = mm.OdStatusValue.Values[(int)OdMm.STATUS.見積依頼済].Value;
            mm.VesselID = thi.MsVesselID;
            mm.MmDate = RenewDate;
            mm.MmSakuseisha = RenewUserID;
            #endregion

            // 見積回答を用意する
            OdMk mk = new OdMk();
            #region
            mk.OdMkID = Hachu.Common.CommonDefine.新規ID(false);
            mk.Status = mk.OdStatusValue.Values[(int)OdMk.STATUS.発注済み].Value;
            mk.Tax = 0;
            mk.VesselID = thi.MsVesselID;
            mk.MsThiIraiSbtID = thi.MsThiIraiSbtID;
            mk.MsVesselID = thi.MsVesselID;
            #endregion

            ListInfo新規発注 info = new ListInfo新規発注();
            info.parent = thi;
            info.info = mm;
            info.child = mk;

            // フォームを作成する
            新規発注Form baseControl = new 新規発注Form((int)BaseUserControl.WINDOW_STYLE.通常, info, true);//引数追加　2021/08/04 m.ysohihara

            新規登録Form = info.CreateForm(baseControl);
            新規登録Form.MdiParent = MdiForm;
            新規登録Form.Show();

            //EventHandlerをセットする
            baseControl.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(新規発注Event);
        }
        #endregion

        /// <summary>
        /// 「発注一覧」Form の 「支払合算」ボタンクリック
        /// ・支払合算Formを開く
        /// </summary>
        #region public void 支払合算Formを開く()
        public void 支払合算Formを開く()
        {
            支払合算Form = HachuManage.支払合算Form.GetInstance();
            支払合算Form.MdiParent = MdiForm;
            支払合算Form.Show();
            支払合算Form.Location = new Point(5, 5);
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
        /// 新規手配依頼Formで「手配依頼作成」「保存」ボタンをクリックしたときに呼び出すメソッド
        /// </summary>
        /// <param name="senderForm"></param>
        /// <param name="senderInfo"></param>
        #region public void 新規手配Event(BaseUserControl senderForm, 一覧情報 senderInfo)
        public void 新規手配Event(BaseUserControl senderForm, ListInfoBase senderInfo)
        {
            //if (IsExistsNode(senderForm) == true)
            //{
            //    一覧情報更新(senderForm, senderInfo);
            //}
            //else
            //{
            //    新規手配依頼情報追加(senderForm, senderInfo);
            //}
           

            if (新規登録Form != null)
            {
                新規登録Form = null;
            }

            //2021/08/06 検索条件に依頼種別で選択した船をセット　m.yoshihara 
            if (検索条件 == null || 検索条件.Vessel == null || (senderInfo != null && 検索条件.Vessel.MsVesselID != (senderInfo as ListInfo手配依頼).info.MsVesselID))
            {
                検索条件 = new 発注一覧検索条件();

                発注状況一覧Form frm = HachuManage.発注状況一覧Form.GetInstance();
                MsVessel vessel = frm.Set検索条件_船((senderInfo as ListInfo手配依頼).info.MsVesselID);

                if (vessel != null)
                {
                    検索条件.Vessel = vessel;
                }
            }

            if (senderInfo != null )//m.yoshihara && 検索条件.Vessel.MsVesselID == (senderInfo as ListInfo手配依頼).info.MsVesselID)
                一覧更新(検索条件);

        }
        #endregion

        /// <summary>
        /// 新規見積依頼Formで「見積依頼作成」ボタンをクリックしたときに呼び出すメソッド
        /// </summary>
        /// <param name="senderForm"></param>
        /// <param name="senderInfo"></param>
        #region public void 新規見積Event(BaseUserControl senderForm, 一覧情報 senderInfo)
        public void 新規見積Event(BaseUserControl senderForm, ListInfoBase senderInfo)
        {
            //新規見積依頼情報追加(senderForm, senderInfo);


            if (新規登録Form != null)
            {
                新規登録Form = null;
            }
            //2021/08/06 検索条件に依頼種別で選択した船をセット　m.yoshihara 
            if (検索条件 == null || 検索条件.Vessel == null || (senderInfo != null && 検索条件.Vessel.MsVesselID != (senderInfo as ListInfo見積依頼).parent.MsVesselID))
            {
                    検索条件 = new 発注一覧検索条件();

                発注状況一覧Form frm = HachuManage.発注状況一覧Form.GetInstance();
                frm.Set検索条件_船((senderInfo as ListInfo見積依頼).parent.MsVesselID);
                MsVessel vessel = frm.Set検索条件_船((senderInfo as ListInfo見積依頼).parent.MsVesselID);

                if (vessel != null)
                {
                    検索条件.Vessel = vessel;
                }
            }
            if (senderInfo != null )//m.yoshihar && 検索条件.Vessel.MsVesselID == (senderInfo as ListInfo見積依頼).parent.MsVesselID)
                一覧更新(検索条件);
        }
        #endregion

        /// <summary>
        /// 新規発注Formで「発注」ボタンをクリックしたときに呼び出すメソッド
        /// </summary>
        /// <param name="senderForm"></param>
        /// <param name="senderInfo"></param>
        #region public void 新規発注Event(BaseUserControl senderForm, 一覧情報 senderInfo)
        public void 新規発注Event(BaseUserControl senderForm, ListInfoBase senderInfo)
        {
            //新規発注依頼情報追加(senderForm, senderInfo);


            if (新規登録Form != null)
            {
                新規登録Form = null;
            }
            //2021/08/06 検索条件に依頼種別で選択した船をセット　m.yoshihara 
            if (検索条件 == null || 検索条件.Vessel == null || (senderInfo != null && 検索条件.Vessel.MsVesselID != (senderInfo as ListInfo手配依頼).info.MsVesselID))
            {
                    検索条件 = new 発注一覧検索条件();

                発注状況一覧Form frm = HachuManage.発注状況一覧Form.GetInstance();
                frm.Set検索条件_船((senderInfo as ListInfo手配依頼).info.MsVesselID);
                MsVessel vessel = frm.Set検索条件_船((senderInfo as ListInfo手配依頼).info.MsVesselID);

                if (vessel != null)
                {
                    検索条件.Vessel = vessel;
                }
            }

            if (senderInfo != null )//&& 検索条件.Vessel.MsVesselID == (senderInfo as ListInfo見積回答).info.MsVesselID)
                一覧更新(検索条件);
        }
        #endregion

 
        

        
        //===============================================================================
        // デリゲートを経由して呼び出されるメソッドから呼び出されるメソッド
        //===============================================================================

        /// <summary>
        /// 新規に作成された手配依頼情報を管理情報に追加する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 新規手配依頼情報追加(BaseUserControl form, 一覧情報 senderInfo)
        //private void 新規手配依頼情報追加(BaseUserControl form, ListInfoBase senderInfo)
        //{
        //    try
        //    {
        //        ListInfo手配依頼 info = (ListInfo手配依頼)senderInfo;

        //        //ノードを作成
        //        TreeListViewNode node = MakeTreeListViewNode("手配依頼", info.NormalColor(), info.SelectedColor(), info.BorderColor(), info.発注状況一覧用文字列());

        //        // ノードと一覧情報を関連付ける
        //        SetHash(null, node, info, form);

        //        // 一覧部に表示
        //        発注一覧TreeListView.Nodes.Add(node);

        //        // EventHandlerを追加する
        //        form.FormActiveEvent += new BaseForm.FormActiveEventHandler(詳細Form_FormActiveEvent);
        //        form.FormClosingEvent += new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
        //    }
        //    catch
        //    {
        //    }
        //}
        #endregion

        /// <summary>
        /// 新規に作成された見積依頼情報を管理情報に追加する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 新規見積依頼情報追加(BaseUserControl form, 一覧情報 senderInfo)
        //private void 新規見積依頼情報追加(BaseUserControl form, ListInfoBase senderInfo)
        //{
        //    try
        //    {
        //        ListInfo手配依頼 thiInfo = new ListInfo手配依頼();
        //        thiInfo.info = ((ListInfo見積依頼)senderInfo).parent;

        //        ListInfo見積依頼 mmInfo = new ListInfo見積依頼();
        //        mmInfo.parent = thiInfo.info;
        //        mmInfo.info = ((ListInfo見積依頼)senderInfo).info;

        //        ListInfo見積回答 mkInfo = new ListInfo見積回答();
        //        mkInfo.parent = mmInfo.info;
        //        mkInfo.info = ((ListInfo見積依頼)senderInfo).child;

        //        mmInfo.Children.Add(mkInfo);
        //        thiInfo.Children.Add(mmInfo);

        //        //ノードを作成
        //        TreeListViewNode thiNode = MakeTreeListViewNode("手配依頼", thiInfo.NormalColor(), thiInfo.SelectedColor(), thiInfo.BorderColor(), thiInfo.発注状況一覧用文字列());
        //        SetHash(null, thiNode, thiInfo);
        //        発注一覧TreeListView.Nodes.Add(thiNode);

        //        TreeListViewNode mmNode = MakeTreeListViewNode("見積依頼", mmInfo.NormalColor(), mmInfo.SelectedColor(), mmInfo.BorderColor(), mmInfo.発注状況一覧用文字列());
        //        SetHash(thiNode, mmNode, mmInfo);
        //        thiNode.Nodes.Add(mmNode);

        //        TreeListViewNode mkNode = MakeTreeListViewNode("見積回答", mkInfo.NormalColor(), mkInfo.SelectedColor(), mkInfo.BorderColor(), mkInfo.発注状況一覧用文字列());
        //        SetHash(mmNode, mkNode, mkInfo);
        //        mmNode.Nodes.Add(mkNode);

        //        // EventHandlerを追加する
        //        form.FormActiveEvent += new BaseForm.FormActiveEventHandler(詳細Form_FormActiveEvent);
        //        form.FormClosingEvent += new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
        //    }
        //    catch
        //    {
        //    }
        //}
        #endregion

        /// <summary>
        /// 新規に作成された見積依頼情報を管理情報に追加する
        /// </summary>
        /// <param name="form"></param>
        /// <param name="senderInfo"></param>
        #region private void 新規発注依頼情報追加(BaseUserControl form, 一覧情報 senderInfo)
        //private void 新規発注依頼情報追加(BaseUserControl form, ListInfoBase senderInfo)
        //{
        //    try
        //    {
        //        ListInfo手配依頼 thiInfo = new ListInfo手配依頼();
        //        thiInfo.info = ((ListInfo手配依頼)senderInfo).info;

        //        ListInfo受領 jryInfo = new ListInfo受領();
        //        jryInfo.parent = ((ListInfo手配依頼)senderInfo).child2;
        //        jryInfo.info = ((ListInfo手配依頼)senderInfo).child3;

        //        thiInfo.Children.Add(jryInfo);

        //        //ノードを作成
        //        TreeListViewNode thiNode = MakeTreeListViewNode("手配依頼", thiInfo.NormalColor(), thiInfo.SelectedColor(), thiInfo.BorderColor(), thiInfo.発注状況一覧用文字列());
        //        SetHash(null, thiNode, thiInfo);
        //        発注一覧TreeListView.Nodes.Add(thiNode);

        //        TreeListViewNode jryNode = MakeTreeListViewNode("受領", jryInfo.NormalColor(), jryInfo.SelectedColor(), jryInfo.BorderColor(), jryInfo.発注状況一覧用文字列());
        //        SetHash(thiNode, jryNode, jryInfo);
        //        thiNode.Nodes.Add(jryNode);
        //    }
        //    catch
        //    {
        //    }
        //}
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
                if (senderInfo.Remove == true)
                {
                    if (senderInfo is ListInfo手配依頼 || (senderInfo is ListInfo受領 && senderInfo.RemoveTop == true))
                    {
                        // 「手配依頼」の取消
                        // 「受領」の取消（新規発注の場合）

                        // タブを非表示にする
                        発注タブ.Visible = false;
                        //発注タブ.TabPages.RemoveAt(発注タブ.SelectedIndex);
                        発注タブ.Pages.RemoveAt(発注タブ.SelectedPage.Index);

                        // 詳細エリアを非表示にする
                        発注一覧エリア.Panel2Collapsed = true;

                        // 一覧から削除する
                        発注一覧Control.RemoveSelectedNode();
                    }
                    else
                    {
                        // 「手配依頼」以外の取消

                        // 現在選択されているタブの情報の親を選択する
                        //int selectedTabIndex = 発注タブ.SelectedIndex; miho
                        int selectedTabIndex = 発注タブ.SelectedPage.Index;

                        ListInfoBase parentInfo = Selected手配;
                        int index = 0;
                        int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);
                        if (parentIndex > 0)
                        {
                            index = 0;
                            parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);
                        }

                        // 親タブを選択状態にする
                        //発注タブ.SelectedIndex = parentIndex; miho
                        発注タブ.TabIndex = parentIndex;       //  Index = parentIndex;
                        詳細タブ変更(parentIndex);


                        // 現在選択されているタブの情報は削除
                        //発注タブ.TabPages.RemoveAt(selectedTabIndex);miho
                        発注タブ.Pages.RemoveAt(selectedTabIndex);
                        Dic_TabPages.Remove(selectedTabIndex);

                        index = 0;
                        RemoveInfo(Selected手配.Children, selectedTabIndex, ref index);


                        // 手配にぶら下がる最終ステータスが変わる場合
                        if (senderInfo is ListInfo見積依頼)
                        {
                            ((ListInfo手配依頼)parentInfo).info = ((ListInfo見積依頼)senderInfo).parent;
                        }
                        else if (senderInfo is ListInfo受領)
                        {
                            if (parentInfo is ListInfo見積回答)
                            {
                                ((ListInfo見積回答)parentInfo).info = ((ListInfo受領)senderInfo).parent;
                            }
                        }

                        発注一覧Control.RedrawSelectedNode(Selected手配);

                    }
                }
                else if (senderInfo.AddNode == true)// 一覧(TreeListView)と管理情報を更新する
                {
                    ノードを追加する(senderInfo);
                }
                else
                {
                    if (senderInfo.NextStatus == true)
                    {
                        次のステータス情報を作成し画面を表示する(senderInfo);
                    }

                    if (senderInfo.ChangeStatus == true)
                    {
                        Selected手配.info.MsThiIraiStatusID = senderInfo.SetStatus.MsThiIraiStatusID;
                        Selected手配.info.OrderThiIraiStatus = senderInfo.SetStatus.OrderThiIraiStatus;
                        発注一覧Control.RedrawSelectedNode(Selected手配);
                    }
                    //if (senderInfo.ChangeParent == true)
                    //{
                    //    if (senderInfo is ListInfo見積回答)
                    //    {
                    //        TreeListViewNode parentNode = 発注一覧ParentNodeHash[node] as TreeListViewNode;
                    //        ListInfo見積依頼 parentInfo = 発注一覧InfoHash[parentNode] as ListInfo見積依頼;
                    //        parentInfo.info = ((ListInfo見積回答)senderInfo).parent;  // 見積依頼
                    //        parentNode.SubItems[0].Content = parentInfo.発注状況一覧用文字列();
                    //    }
                    //}

                    //発注一覧InfoHash[node] = senderInfo;
                    //node.SubItems[0].Content = senderInfo.発注状況一覧用文字列();

                    //一覧情報更新（発注一覧クリックで保存を開き、手配依頼作成したとき）
                    //2021/08/06 m.yoshihara
                    if (senderInfo is ListInfo手配依頼)
                    {
                        発注一覧Control.RedrawSelectedNode(Selected手配);
                    }
                }
            }
            catch
            {
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="parentNode"></param>
        /// <param name="parentInfo"></param>
        private void ノードを追加する(ListInfoBase info)
        {
            //int selectedTabIndex = 発注タブ.SelectedIndex;
            int selectedTabIndex = 発注タブ.SelectedPage.Index;

            int index = 0;
            ListInfoBase selectedInfo = GetInfo(Selected手配.Children, selectedTabIndex, ref index);

            #region コメントアウト 2021/08/26 m.yoshihara
            //index = 0;
            ////int parentIndex = GetParentIndex(Selected手配.Children, selectedTabIndex, ref index);miho
            //int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);

            //index = 0;
            //ListInfoBase parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);

            #endregion

            //親情報取得
            //次のステータス情報を作成し画面を表示する()と同じ取得方法　2021/08/26 m.yoshihara 
            ListInfoBase parentInfo = Selected手配;
            index = 0;
            //int parentIndex = GetParentIndex(Selected手配.Children, selectedTabIndex, ref index);
            int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);
            if (parentIndex > 0)
            {
                index = 0;
                parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);
            }

            if (info is ListInfo見積依頼)
            {
                // 見積依頼に見積回答を追加する
                ListInfo見積回答 newInfo = new ListInfo見積回答();
                newInfo.parent = ((ListInfo見積依頼)info).info;  // 見積依頼
                newInfo.info = ((ListInfo見積依頼)info).child;   // 見積回答

                selectedInfo.Children.Add(newInfo);

                //見積回答のタブを追加する
                //発注タブ.TabPages.Insert(selectedTabIndex + selectedInfo.Children.Count(), "見積回答");miho
                AddPage(selectedTabIndex + selectedInfo.Children.Count(), "見積回答");

                発注一覧Control.RedrawSelectedNode(Selected手配);

                //タブページ開く m.yoshihara 
                詳細タブ変更(selectedTabIndex + selectedInfo.Children.Count());

            }
            else if (info is ListInfo見積回答)
            {
                ListInfo見積回答 newInfo = new ListInfo見積回答();
                newInfo.parent = ((ListInfo見積回答)info).parent;  // 見積依頼
                newInfo.info = ((ListInfo見積回答)info).info;   // 見積回答

                //発注タブ.TabPages.Insert(selectedTabIndex + 1, "見積回答");miho
                AddPage(selectedTabIndex + 1, "見積回答");
                parentInfo.Children.Add(newInfo);

                //タブ選択 m.yoshihara 
                詳細タブ変更(selectedTabIndex + selectedInfo.Children.Count());
            }
            else if (info is ListInfo受領)
            {
                // 分納の場合にここへ

                ListInfo受領 newInfo = new ListInfo受領();
                newInfo.parent = ((ListInfo受領)info).parent;  // 見積回答
                newInfo.info = ((ListInfo受領)info).info;   // 受領

                //発注タブ.TabPages.Insert(selectedTabIndex + 1, "　受領　");miho
                AddPage(selectedTabIndex + 1, "受領");
                parentInfo.Children.Add(newInfo);

                //タブ選択 m.yoshihara 
                詳細タブ変更(selectedTabIndex + selectedInfo.Children.Count());
            }
            else if (info is ListInfo支払)
            {
                // 支払分割の場合にここへ

                ListInfo支払 newInfo = new ListInfo支払();
                newInfo.parent = ((ListInfo支払)info).parent;  // 受領
                newInfo.info = ((ListInfo支払)info).info;   // 支払

                //発注タブ.TabPages.Insert(selectedTabIndex + 1, "　支払　");miho
                AddPage(selectedTabIndex + 1, "支払");
                parentInfo.Children.Add(newInfo);

                //タブ選択 m.yoshihara 
                詳細タブ変更(selectedTabIndex + selectedInfo.Children.Count());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="parentNode"></param>
        /// <param name="parentInfo"></param>
        private void 次のステータス情報を作成し画面を表示する(ListInfoBase info)
        {
            // 現在選択されている情報を親として子を追加表示する
            //int selectedTabIndex = 発注タブ.SelectedIndex;miho
            int selectedTabIndex = 発注タブ.SelectedPage.Index;


            ListInfoBase parentInfo = Selected手配;
            int index = 0;
            //int parentIndex = GetParentIndex(Selected手配.Children, selectedTabIndex, ref index);
            int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);
            if (parentIndex > 0)
            {
                index = 0;
                parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);
            }

            if (info is ListInfo手配依頼)
            {
                if (((ListInfo手配依頼)info).child3 != null)
                {
                    ListInfo受領 childInfo = new ListInfo受領();
                    childInfo.parent = ((ListInfo手配依頼)info).child2;
                    childInfo.info = ((ListInfo手配依頼)info).child3;

                    // 2014.03: 2013年度改造
                    childInfo.info.MsThiIraiSbtID = childInfo.parent.MsThiIraiSbtID;

                    //// 見積依頼ノードを作成
                    //TreeListViewNode node = MakeTreeListViewNode("受領", info.NormalColor(), info.SelectedColor(), info.BorderColor(), info.発注状況一覧用文字列());

                    //// ノードと一覧情報を関連付ける
                    //SetHash(parentNode, node, info);

                    //// 手配依頼ノードに追加
                    //parentNode.Nodes.Add(node);
                    parentInfo.Children.Add(childInfo);

                    //// 受領ノードを選択しFormを開く
                    //node.Selected = true;
                    //詳細Formを開く();
                    //発注タブ.TabPages.Insert(selectedTabIndex + 1, "　受領　");miho
                    AddPage(selectedTabIndex + 1, "受領");
                    詳細タブ変更(selectedTabIndex + 1);
                }
                else if (((ListInfo手配依頼)info).child != null)
                {
                    // 手配依頼情報の更新
                    ((ListInfo手配依頼)parentInfo).info = ((ListInfo手配依頼)info).info;

                    // 見積依頼情報を追加する
                    ListInfo見積依頼 childInfo = new ListInfo見積依頼();
                    childInfo.parent = ((ListInfo手配依頼)info).info;  // 手配依頼
                    childInfo.info = ((ListInfo手配依頼)info).child;   // 見積依頼

                    parentInfo.Children.Add(childInfo);

                    #region コメントアウト 2021/08/26 m.yoshihara
                    ////（親）手配依頼⇒（子）見積依頼のタブを追加する
                    ////発注タブ.TabPages.Insert(selectedTabIndex + parentInfo.Children.Count(), "見積依頼");miho
                    //AddPage(selectedTabIndex + parentInfo.Children.Count(), "見積依頼"); 

                    ////（子）見積依頼のタブを選択する
                    ////発注タブ.SelectedIndex = (selectedTabIndex + parentInfo.Children.Count());miho
                    //{
                    //    LidorSystems.IntegralUI.Containers.TabPage p = 発注タブ.Pages[selectedTabIndex + parentInfo.Children.Count()]; 
                    //    発注タブ.SelectedPage = p;
                    //}
                    ////（子）見積依頼の詳細を表示する
                    //詳細タブ変更(selectedTabIndex + parentInfo.Children.Count());
                    #endregion

                    //（親）手配依頼全部の子の数 2021/08/26 m.yoshihara
                    index = 0;
                    int countAllChildren = GetCountAllChildlen(parentInfo, ref index);

                    //（親）手配依頼⇒（子）見積依頼のタブを追加する
                    AddPage(parentIndex + countAllChildren, "見積依頼");

                    //（子）見積依頼のタブを選択する
                    {
                        LidorSystems.IntegralUI.Containers.TabPage p = 発注タブ.Pages[parentIndex + countAllChildren];
                        発注タブ.SelectedPage = p;
                    }
                    //（子）見積依頼の詳細を表示する
                    詳細タブ変更(parentIndex + countAllChildren);

                    // 一覧の表示を更新する 2021/08/26 外に移動した　m.yoshihara
                    //発注一覧Control.RedrawSelectedNode(Selected手配);
                }
            }
            else if (info is ListInfo見積回答)
            {
                index = 0;
                ListInfoBase currentInfo = GetInfo(Selected手配.Children, selectedTabIndex, ref index);

                // 見積回答の情報の更新
                ((ListInfo見積回答)currentInfo).info = ((ListInfo見積回答)info).info;

                // 受領情報を追加する
                ListInfo受領 childInfo = new ListInfo受領();
                childInfo.parent = ((ListInfo見積回答)info).info;  // 見積回答
                childInfo.info = ((ListInfo見積回答)info).child;   // 受領
                childInfo.info.MsThiIraiSbtID = childInfo.parent.MsThiIraiSbtID;

                currentInfo.Children.Add(childInfo);

                //（親）見積回答⇒（子）受領情報のタブを追加する
                //発注タブ.TabPages.Insert(selectedTabIndex + currentInfo.Children.Count(), "　受領　");miho
                AddPage(selectedTabIndex + currentInfo.Children.Count(), "受領");

                //（子）受領情報のタブを選択する
                //発注タブ.SelectedIndex = (selectedTabIndex + currentInfo.Children.Count());miho
                {
                    LidorSystems.IntegralUI.Containers.TabPage p = 発注タブ.Pages[selectedTabIndex + currentInfo.Children.Count()];
                    発注タブ.SelectedPage = p;
                }

                //（子）受領情報の詳細を表示する
                詳細タブ変更(selectedTabIndex + currentInfo.Children.Count());

            }
            else if (info is ListInfo受領)
            {
                index = 0;
                ListInfoBase currentInfo = GetInfo(Selected手配.Children, selectedTabIndex, ref index);

                // 受領の情報の更新
                ((ListInfo受領)currentInfo).info = ((ListInfo受領)info).info;

                // 支払情報を追加する
                ListInfo支払 childInfo = new ListInfo支払();
                childInfo.parent = ((ListInfo受領)info).info;  // 受領
                childInfo.info = ((ListInfo受領)info).child;   // 支払

                currentInfo.Children.Add(childInfo);

                //（親） ⇒（子）支払情報のタブを追加する
                if (childInfo.info.Sbt == (int)OdShr.SBT.落成)
                {
                    //発注タブ.TabPages.Insert(selectedTabIndex + currentInfo.Children.Count(), "　落成　");miho
                    AddPage(selectedTabIndex + currentInfo.Children.Count(), "落成");
                }
                else
                {
                    //発注タブ.TabPages.Insert(selectedTabIndex + currentInfo.Children.Count(), "　支払　");
                    AddPage(selectedTabIndex + currentInfo.Children.Count(), "支払");
                }

                //（子）支払情報のタブを選択する
                //発注タブ.SelectedIndex = (selectedTabIndex + currentInfo.Children.Count());miho
                {
                    LidorSystems.IntegralUI.Containers.TabPage p = 発注タブ.Pages[selectedTabIndex + currentInfo.Children.Count()];
                    発注タブ.SelectedPage = p;
                }

                //（子）支払情報の詳細を表示する
                詳細タブ変更(selectedTabIndex + currentInfo.Children.Count());
            }

            // 一覧の表示を更新する 2021/08/26 全部に必要？　m.yoshihara 
            発注一覧Control.RedrawSelectedNode(Selected手配);
        }



        /// <summary>
        /// 選択された詳細情報を表示する
        /// </summary>
        /// <param name="tabPageIndex"></param>
        #region public void 詳細タブ変更(int tabPageIndex)
        public void 詳細タブ変更(int tabPageIndex)
        {
            if (tabPageIndex < 0)
                return;

            if (Dic_TabPages.ContainsKey(tabPageIndex))
            {
                Dic_TabPages[tabPageIndex].BringToFront();
            }
            else
            {
                if (Selected手配 == null || Selected手配.Children == null)
                {
                    return;
                }

                int index = 0;
                ListInfoBase targetInfo = null;

                targetInfo = GetInfo(Selected手配.Children, tabPageIndex, ref index);

                BaseUserControl form = targetInfo.CreatePanel((int)BaseUserControl.WINDOW_STYLE.通常);
                form.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(詳細Form_InfoUpdateEvent);

                発注一覧エリア.Panel2.Controls.Add(form);

                form.Dock = DockStyle.Fill;

                Dic_TabPages.Add(tabPageIndex, form);

                Dic_TabPages[tabPageIndex].BringToFront();

                //m.yoshihara 2021/08/26 BringToFrontが効いていないようなので再描画
                Dic_TabPages[tabPageIndex].Invalidate();

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

        private int GetParentIndex(ListInfoBase info, int getIndex, ref int index)
        {
            int parentIndex = 0;

            parentIndex = index;

            foreach (ListInfoBase child in info.Children)
            {
                index++;

                if (getIndex <= index)
                {
                    break;
                }

                if (child.Children != null && child.Children.Count != 0)
                {
                    int ret = GetParentIndex(child, getIndex, ref index);
                    if (getIndex == index)
                    {
                        parentIndex = ret;
                        break;
                    }
                }
            }

            return parentIndex;
        }

        /// <summary>
        /// 子供たち全部数える　2021/08/26 m.yoshihara
        /// </summary>
        /// <returns></returns>
        private int GetCountAllChildlen(ListInfoBase info, ref int index)
        {
            if (info.Children != null && info.Children.Count != 0)
            {
                foreach (ListInfoBase child in info.Children)
                {
                    index++;

                    if (child.Children != null && child.Children.Count != 0)
                    {
                        int ret = GetCountAllChildlen(child, ref index);
                    }
                }

            }
            return index;
        }
        /// <summary>
        /// テストコード
        /// </summary>
        public void test()
        {
            if (Selected手配 == null) return;

            //int index = 0;
            //int a = GetCountAllChildlen(Selected手配, ref index);

            int selectedTabIndex = 発注タブ.SelectedPage.Index;
            //index = 0;
            //ListInfoBase targetInfo = GetInfo(Selected手配.Children, selectedTabIndex, ref index);

            //a = 0;
            //index = 0;
            //a = GetCountAllChildlen(targetInfo, ref index);
            int index = 0;
            {
                index = 0;
                ListInfoBase selectedInfo = GetInfo(Selected手配.Children, selectedTabIndex, ref index);

            }
            {
                index = 0;
                //1 int parentIndex = GetParentIndex(Selected手配.Children, selectedTabIndex, ref index);miho
                int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);
                //int parentIndex = GetParentIndex(selectedInfo, selectedTabIndex, ref index);

                index = 0;
                ListInfoBase parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);

            }
            {
                ListInfoBase parentInfo = Selected手配;
                index = 0;

                int parentIndex = GetParentIndex(Selected手配, selectedTabIndex, ref index);
                if (parentIndex > 0)
                {
                    index = 0;
                    parentInfo = GetInfo(Selected手配.Children, parentIndex, ref index);
                }
            }
        }
        private bool RemoveInfo(List<ListInfoBase> infos, int removeIndex, ref int index)
        {
            bool ret = false;

            foreach (ListInfoBase info in infos)
            {
                index++;

                if (removeIndex == index)
                {
                    infos.Remove(info);
                    ret = true;
                    break;
                }
                if (info.Children != null && info.Children.Count != 0)
                    ret = RemoveInfo(info.Children, removeIndex, ref index);

                if (ret == true)
                    break;
            }

            return ret;
        }
    }

}

