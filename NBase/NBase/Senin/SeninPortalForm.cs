using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Senin.util;
using NBaseData.DS;
using NBaseData.DAC;
using NBaseUtil;
using NBaseCommon;

namespace Senin
{
    public partial class SeninPortalForm : ExForm
    {
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static SeninPortalForm instance = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static SeninPortalForm GetInstance()
        {
            if (instance == null)
            {
                instance = new SeninPortalForm();
            }
            return instance;
        }

        private SeninPortalForm()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "船員管理", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            EnableComponents();
        }


        private void EnableComponents()
        {
            // "表示" メニュー
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "配乗表"))
            {
                toolStripMenuItem配乗表.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "船員一覧"))
            {
                toolStripMenuItem船員.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "船用金管理"))
            {
                toolStripMenuItem船用金管理.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "講習管理"))
            {
                toolStripMenuItem講習管理.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "免許・免状一覧"))
            {
                免許免状一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "休暇一覧"))
            {
                休暇一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "配乗シミュレーション"))
            {
                配乗シミュレーションToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "個人予定"))
            {
                個人予定ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "乗り合わせ"))
            {
                乗り合わせToolStripMenuItem.Enabled = true;
            }



            toolStripMenuItem配乗計画短期.Visible = false;
            toolStripMenuItem配乗計画長期.Visible = false;

            List<MsPlanType> planTypeList = MsPlanType.GetRecords();
            foreach (MsPlanType pt in planTypeList)
            {
                string planTypeMenuStr = "配乗計画（" + pt.Name + "）";

                if (pt.Type == MsPlanType.PlanTypeOneMonth)
                {
                    if (NBaseCommon.Common.配乗計画TYPE == MsPlanType.PlanTypeOneMonth)
                    {
                        toolStripMenuItem配乗計画短期.Text = planTypeMenuStr;
                        toolStripMenuItem配乗計画短期.Visible = true;
                        if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", planTypeMenuStr))
                        {
                            toolStripMenuItem配乗計画短期.Enabled = true;
                        }
                    }
                }
                if (pt.Type == MsPlanType.PlanTypeHarfPeriod)
                {
                    if (NBaseCommon.Common.配乗計画TYPE == MsPlanType.PlanTypeHarfPeriod)
                    {
                        toolStripMenuItem配乗計画長期.Text = planTypeMenuStr;
                        toolStripMenuItem配乗計画長期.Visible = true;
                        if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", planTypeMenuStr))
                        {
                            toolStripMenuItem配乗計画長期.Enabled = true;
                        }
                    }
                }
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "手当一覧"))
            {
                手当一覧ToolStripMenuItem.Enabled = true;
            }



            // "船内収支帳票" メニュー
            #region
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船内収支帳票", "科目別集計表"))
            {
                科目別ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船内収支帳票", "船内収支報告書"))
            {
                船内収支報告書ToolStripMenuItem.Enabled = true;
            }
            #endregion

            // "船員帳票" メニュー
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "休日付与簿"))
            {
                休暇付与簿ToolStripMenuItem1.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "休暇消化状況"))
            {
                休暇消化状況ToolStripMenuItem1.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "乗下船記録書"))
            {
                乗下船記録書ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "個人情報一覧"))
            {
                個人情報一覧ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "クルーリスト"))
            {
                クルーリストToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "乗下船カード"))
            {
                乗下船カードToolStripMenuItem.Enabled = true;
            }


            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "傷病一覧"))
            {
                傷病一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "健康診断一覧"))
            {
                健康診断一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "職別海技免状等資格一覧"))
            {
                職別海技免許等資格一覧ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "船員帳票", "手当帳票"))
            {
                手当帳票ToolStripMenuItem.Enabled = true;
            }

            // "月次計上確定" メニュー
            #region
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "月次計上確定", ""))
            {
                月次計上確定ToolStripMenuItem.Enabled = true;
            }
            #endregion

            // "年度確定" メニュー
            #region
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "年度確定", ""))
            {
                年度確定ToolStripMenuItem.Enabled = true;
            }
            #endregion

        }

        
        private void SeninPortalForm_Load(object sender, EventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }


        private void toolStripMenuItem配乗表_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            Show配乗表();
        }

        private void Show配乗表()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "船員管理", "表示", "配乗表"))
            {
                配乗表Form form = 配乗表Form.Instance();
                form.MdiParent = this;
                form.Show();
                form.WindowState = FormWindowState.Normal;
                form.Activate();
            }
        }


        private void toolStripMenuItem配乗計画短期_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            配乗計画短期Form form = 配乗計画短期Form.Instance();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }

        private void toolStripMenuItem配乗計画長期_Click(object sender, EventArgs e)
        {
            配乗計画長期Form form = 配乗計画長期Form.Instance();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }



        private void toolStripMenuItem船員_Click(object sender, EventArgs e)
        {
            船員Form form = 船員Form.Instance();
            form.MdiParent = this;

            //--------------------画面いっぱいサイズで船員管理を開く　m.yoshihara 追加　2021/08/03  
            //MdiClientの取得
            System.Windows.Forms.MdiClient mc = GetMdiClient(this);
            form.Size = new Size(mc.ClientRectangle.Width, mc.ClientRectangle.Height);//幅、高さいっぱい 
            form.StartPosition = FormStartPosition.CenterScreen;//サイズから中央を割り出した位置
            //--------------------

            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }
        /// <summary>
        /// フォームのMdiClientコントロールを探して返す 2021/08/03 m.yoshihara 追加
        /// </summary>
        /// <param name="f">MdiClientコントロールを探すフォーム</param>
        /// <returns>見つかったMdiClientコントロール</returns>
        public static System.Windows.Forms.MdiClient
            GetMdiClient(System.Windows.Forms.Form f)
        {
            foreach (System.Windows.Forms.Control c in f.Controls)
                if (c is System.Windows.Forms.MdiClient)
                    return (System.Windows.Forms.MdiClient)c;
            return null;
        }

        private void toolStripMenuItem船用金管理_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            船内収支Form form = 船内収支Form.Instance();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }


        private void 科目別ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.科目別集計表);
            form.ShowDialog();
        }


        private void 船内収支報告書ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.船内収支報告書);
            form.ShowDialog();
        }


        private void 休暇付与簿ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            年度指定出力Form form = new 年度指定出力Form(年度指定出力Form.帳票種別.休日付与簿);
            form.ShowDialog();
        }


        private void 休暇消化状況ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            年度指定出力Form form = new 年度指定出力Form(年度指定出力Form.帳票種別.休暇消化状況);
            form.ShowDialog();
        }


        private void 乗下船記録書ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年度指定出力Form form = new 年度指定出力Form(年度指定出力Form.帳票種別.乗下船記録書);
            form.ShowDialog();
        }


        private void 個人情報一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年月日指定出力Form form = new 年月日指定出力Form(年月日指定出力Form.帳票種別.個人情報一覧);
            form.ShowDialog();
        }


        private void クルーリストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年月日指定出力Form form = new 年月日指定出力Form(年月日指定出力Form.帳票種別.クルーリスト);
            form.ShowDialog();
        }


        private void 乗下船カードToolStripMenuItem_Click(object sender, EventArgs e)
        {
            年度指定出力Form form = new 年度指定出力Form(年度指定出力Form.帳票種別.乗下船カード);
            form.ShowDialog();
        }

        private void 傷病一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            傷病検診一覧Form form = new 傷病検診一覧Form(傷病検診一覧Form.帳票種別.傷病一覧);
            form.ShowDialog();
        }

        private void 健康診断一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            傷病検診一覧Form form = new 傷病検診一覧Form(傷病検診一覧Form.帳票種別.健康診断一覧);
            form.ShowDialog();
        }

        private void 職別海技免許等資格一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            職別海技免許等資格一覧Form form = new 職別海技免許等資格一覧Form();
            form.ShowDialog();
        }



        private void 月次計上確定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiGetsujiShime getsujiShime = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                getsujiShime = serviceClient.SiGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            if (getsujiShime == null)
            {
                MessageBox.Show("直近の月次計上確定レコードが見つかりません。", "月次計上確定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime shimeNengetsu = DateTime.ParseExact(getsujiShime.NenGetsu + "01", "yyyyMMdd", null).AddMonths(1);

            if (MessageBox.Show("月次計上を確定します。よろしいですか？\n（対象年月： " + shimeNengetsu.ToString("yyyy年MM月") + "）",
                "月次計上確定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool result = true;
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        result = serviceClient.BLC_月次計上確定(NBaseCommon.Common.LoginUser, shimeNengetsu.ToString("yyyyMM"));
                    }
                }, "月次計上処理中です...");
                progressDialog.ShowDialog();

                if (result)
                {
                    MessageBox.Show("月次計上を確定しました。", "月次計上確定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("月次計上を確定できませんでした。", "月次計上確定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void 年度確定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiNenjiShime nenjiShime = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                nenjiShime = serviceClient.SiNenjiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            if (nenjiShime == null)
            {
                MessageBox.Show("直近の年度確定レコードが見つかりません。", "年度確定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime shimeNen = DateTime.ParseExact(nenjiShime.Nen, "yyyy", null).AddYears(1);

            if (MessageBox.Show("年度を確定します。よろしいですか\n（対象年： " + shimeNen.ToString("yyyy年") + "）",
                "年度確定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    bool result = serviceClient.BLC_次年度休暇確定(NBaseCommon.Common.LoginUser, shimeNen.ToString("yyyy"));

                    if (result)
                    {
                        MessageBox.Show("年度を確定しました。", "年度確定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("年度を確定できませんでした。", "年度確定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void 講習管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            講習管理Form form = 講習管理Form.Instance();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }

        private void 免許免状一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            免許免状一覧Form form = new 免許免状一覧Form();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }

        private void 休暇一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            休暇一覧Form form = new 休暇一覧Form();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }

        private void 船員Formの全画面表示を解除する()
        {
            if (船員Form.IsActivated())
            {
                船員Form.Instance().WindowState = FormWindowState.Normal;
                this.Refresh();
            }
        }

        private void 配乗シミュレーションToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            配乗シミュレーションForm form = new 配乗シミュレーションForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
            form.Activate();
        }

        private void 個人予定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            個人予定一覧Form form = 個人予定一覧Form.Instance();
            form.Show();
            form.Activate();
        }

        private void 乗り合わせToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            乗り合わせ一覧Form form = 乗り合わせ一覧Form.Instance();
            form.Show();
            form.Activate();
        }

        private void SeninPortalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            配乗計画短期Form.DisposeInstance();
            配乗計画長期Form.DisposeInstance();


            this.ChangeParentFormSize(FormWindowState.Normal);


            instance = null;
        }

        private void 管理情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            勤怠管理情報Form form = new 勤怠管理情報Form();
            form.ShowDialog();
        }

        private void 労務管理記録簿出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            労務管理記録簿出力Form form = new 労務管理記録簿出力Form();
            form.ShowDialog();
        }

        private void 手当一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船員Formの全画面表示を解除する();

            tek手当一覧Form form = tek手当一覧Form.Instance();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }

        private void 手当帳票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tek手当帳票出力Form form = new Tek手当帳票出力Form();
            form.ShowDialog();
        }
    }
}
