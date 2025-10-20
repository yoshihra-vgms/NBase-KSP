using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Hachu.Controllers;
using Hachu.Models;
using NBaseData.DAC;
using NBaseData.DS;

namespace Hachu.HachuManage
{
    public partial class 概算計上一覧Form : Form
    {
        private string DIALOG_TITLE = "概算計上一覧";
        
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 概算計上一覧Form instance = null;

        /// <summary>
        /// 概算計上一覧情報リスト
        /// </summary>
        private 概算計上一覧情報Controller 概算計上一覧情報 = null;
        private 概算計上一覧情報Controller 概算計上一覧情報_一般 = null;
        private 概算計上一覧情報Controller 概算計上一覧情報_管理者 = null;

        /// <summary>
        /// 検索条件
        /// </summary>
        概算計上一覧検索条件 検索条件 = null;
        概算計上一覧検索条件 概算計上対象検索条件 = null;

        /// <summary>
        /// 
        /// </summary>
        private DateTime 基幹連携対象年月;

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;

        /// <summary>
        /// 「概算データ一覧」の一覧情報
        /// </summary>
        //List<OdJry> 概算データ一覧情報 = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 概算計上一覧Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 概算計上一覧Form();
            }
            return instance;
        }

        /// <summary>
        /// コンストラクタ
        /// Windows フォームをシングルトン対応にするため private
        /// </summary>
        private 概算計上一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("JM041301", DIALOG_TITLE, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            EnableComponents();
        }

        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "概算データ一覧"))
            {
                maskedTextBox対象年月.Enabled = true;
                comboBox船.Enabled = true;
                comboBox事務担当者.Enabled = true;
                comboBox種別.Enabled = true;
                comboBox詳細種別.Enabled = true;
                radioButton計上済.Enabled = true;
                radioButton未計上.Enabled = true;
                button検索.Enabled = true;
                button条件クリア.Enabled = true;
                button概算データ出力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "管理者機能"))
            {
                button概算計上実行.Enabled = true;
                button月次計上確定.Enabled = true;
                button概算データ出力_管理者.Enabled = true;
            }
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 概算計上一覧Form_Load(object sender, EventArgs e)
        private void 概算計上一覧Form_Load(object sender, EventArgs e)
        {
            //==================================
            // 初期化
            //==================================

            this.Location = new Point(0, 0);

            // Form幅を親Formの幅にする
            this.Width = Parent.ClientSize.Width;
            this.Height = Parent.ClientSize.Height;

            初期化();

            概算計上一覧情報_一般 = new 概算計上一覧情報Controller(this.MdiParent, dataGridView概算計上一覧);
            概算計上一覧情報_一般.初期化(this.Width - 20);

            // 2012年度 VersionUp改造
            this.WindowState = FormWindowState.Maximized;

            // 2011.01.24
            //if (NBaseCommon.Common.LoginUser.AdminFlag != (int)MsUser.ADMIN_FLAG.管理者)
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "管理者機能") == false)
            {
                tabControl1.TabPages.RemoveAt(1);
            }
            else
            {
                概算計上一覧情報_管理者 = new 概算計上一覧情報Controller(this.MdiParent, dataGridView概算計上一覧);
                概算計上一覧情報_管理者.初期化(this.Width - 20);
            }

            概算計上一覧情報 = 概算計上一覧情報_一般;
        }
        #endregion

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 概算計上一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        private void 概算計上一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }
        #endregion

        /// <summary>
        /// 「概算データ一覧」「管理者機能」タブの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            概算計上一覧情報.クリア();

            if (tabControl1.SelectedIndex == 0)
            {
                if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "概算データ一覧"))
                {
                    // 概算データ一覧
                    概算計上一覧情報 = 概算計上一覧情報_一般;

                    一覧更新(null);
                }
            }
            else
            {
                if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "管理者機能"))
                {
                    // 管理者機能
                    概算計上一覧情報 = 概算計上一覧情報_管理者;

                    // 現在、計上対象となるデータを検索、表示しておく
                    一覧更新(概算計上対象検索条件);

                    if (dataGridView概算計上一覧.Rows.Count > 0)
                    {
                        button概算計上実行.Enabled = true;
                    }
                    else
                    {
                        button概算計上実行.Enabled = false;

                        MessageBox.Show("概算計上の対象になる、未支払の受領データがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    label合計金額.Text = "合計金額：" + NBaseCommon.Common.金額出力(概算計上一覧情報.Get合計金額());
                }
            }           
        }
        #endregion

        /// <summary>
        /// 初期化する
        /// </summary>
        #region private void 初期化()
        private void 初期化()
        {
            List<MsVessel> vessels = null;
            List<MsUser> users = null;
            List<MsThiIraiSbt> thiIraiSbts = null;
            OdGetsujiShime getsujiShime = null;
            string latestNengetsu = "";
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                users = serviceClient.MsUser_GetRecordsByUserKbn事務所(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
                getsujiShime = serviceClient.OdGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
                latestNengetsu = serviceClient.OdGaisanKeijo_GetLatestNengetsu(NBaseCommon.Common.LoginUser);
            }
            //======================================================
            // 概算データ一覧
            //======================================================
            #region
            検索条件 = new 概算計上一覧検索条件();

            // 船ComboBox初期化
            MsVessel dmyVessel = new MsVessel();
            dmyVessel.MsVesselID = -1;
            dmyVessel.VesselName = "";
            comboBox船.Items.Clear();
            comboBox船.Items.Add(dmyVessel);
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }

            // 事務所担当者
            MsUser dmyUser = new MsUser();
            dmyUser.MsUserID = "";
            dmyUser.Sei = "";
            dmyUser.Mei = "";
            comboBox事務担当者.Items.Clear();
            comboBox事務担当者.Items.Add(dmyUser);
            foreach (MsUser u in users)
            {
                comboBox事務担当者.Items.Add(u);
            }

            // 手配依頼種別
            MsThiIraiSbt dmyThiIraiSbt = new MsThiIraiSbt();
            dmyThiIraiSbt.MsThiIraiSbtID = "";
            dmyThiIraiSbt.ThiIraiSbtName = "";
            comboBox種別.Items.Clear();
            comboBox種別.Items.Add(dmyThiIraiSbt);
            foreach (MsThiIraiSbt sbt in thiIraiSbts)
            {
                comboBox種別.Items.Add(sbt);
            }
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
            dmyThiIraiShousai.MsThiIraiShousaiID = "";
            dmyThiIraiShousai.ThiIraiShousaiName = "";
            comboBox詳細種別.Items.Clear();
            comboBox詳細種別.Items.Add(dmyThiIraiShousai);
            comboBox詳細種別.SelectedIndex = 0;
            #endregion


            //======================================================
            // 管理者機能
            //======================================================
            #region
            基幹連携対象年月 = DateTime.Parse(latestNengetsu.Substring(0, 4) + "/" + latestNengetsu.Substring(4, 2) + "/01").AddMonths(1);
            
            概算計上対象検索条件 = new 概算計上一覧検索条件();
            概算計上対象検索条件.NextNenGetsu = DateTime.Parse(getsujiShime.NenGetsu.Substring(0, 4) + "/" + getsujiShime.NenGetsu.Substring(4, 2) + "/01").AddMonths(1);
            概算計上対象検索条件.YearMonthOnly = false;
            概算計上対象検索条件.YearMonth = DateTime.Parse(latestNengetsu.Substring(0, 4) + "/" + latestNengetsu.Substring(4, 2) + "/01").AddMonths(1).ToString("yyyyMM");
            概算計上対象検索条件.status未計上 = true;
            概算計上対象検索条件.status計上済 = false;

            対象年月セット();
            #endregion
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            if (maskedTextBox対象年月.Text.Length > 0)
            {
                try
                {
                    DateTime checkYM = DateTime.Parse(maskedTextBox対象年月.Text + "/01");
                }
                catch
                {
                    MessageBox.Show("対象年月が不正です。正しく入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // 検索条件
            検索条件 = new 概算計上一覧検索条件();
            if (maskedTextBox対象年月.Text.Length > 0)
                検索条件.YearMonth = maskedTextBox対象年月.Text.Replace("/", "").Trim();
            if (comboBox船.SelectedItem is MsVessel)
                検索条件.Vessel = comboBox船.SelectedItem as MsVessel;
            if (comboBox事務担当者.SelectedItem is MsUser)
                検索条件.User = comboBox事務担当者.SelectedItem as MsUser;
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
                検索条件.msThiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
                検索条件.msThiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;

            // 2010.12.17
            //検索条件.status未計上 = radioButton未計上.Checked;
            //検索条件.status計上済 = radioButton計上済.Checked;
            if (radioButton未計上.Checked == true)
            {
                if (int.Parse(検索条件.YearMonth) >= int.Parse(概算計上対象検索条件.YearMonth))
                {
                    // 対象年月が基幹システム連携の対象年月の場合、本当の意味で未計上のものが検索対象
                    検索条件.status未計上 = radioButton未計上.Checked;
                    検索条件.status計上済 = radioButton計上済.Checked;
                }
                else
                {
                    // 対象年月が過去の場合、「基幹ｼｽﾃﾑ連携」の実績を検索する
                    検索条件.status未計上 = false;
                    検索条件.status計上済 = true;
                }
            }
            else
            {
                // "計上済"
                検索条件.status未計上 = true;
                検索条件.status計上済 = true;
            }
    
            if (検索条件.YearMonth.Length == 0)
            {
                MessageBox.Show("対象年月を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            検索条件.YearMonthOnly = false;

            // 検索条件で、ＤＢを検索し、一覧に表示する
            一覧更新(検索条件);

            if (dataGridView概算計上一覧.Rows.Count == 0)
            {
                MessageBox.Show("対象データがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// 「条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button条件クリア_Click(object sender, EventArgs e)
        private void button条件クリア_Click(object sender, EventArgs e)
        {
            // 対象年月
            maskedTextBox対象年月.Text = "";

            // 船
            comboBox船.SelectedIndex = 0;

            // 事務担当者
            comboBox事務担当者.SelectedIndex = 0;

            // 手配依頼種別
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            comboBox詳細種別.Items.Clear();

            // 計上
            radioButton未計上.Checked = true;
            radioButton計上済.Checked = false;
        }
        #endregion

        /// <summary>
        /// 「概算データ出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void button概算データ出力_Click(object sender, EventArgs e)
        {
            List<NBaseData.BLC.概算計上一覧Row> rows = 概算計上一覧情報.GetRows();
            if (rows == null || rows.Count() == 0)
            {
                MessageBox.Show("概算データがありません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int year = 0;
            int month = 0;
            if (tabControl1.SelectedIndex == 0)
            {
                year = int.Parse(検索条件.YearMonth.Substring(0, 4));
                month = int.Parse(検索条件.YearMonth.Substring(4, 2));
            }
            else
            {
                year = int.Parse(概算計上対象検索条件.YearMonth.Substring(0, 4));
                month = int.Parse(概算計上対象検索条件.YearMonth.Substring(4, 2));
            }
            Hachu.Reports.概算計上一覧 reporter = new Hachu.Reports.概算計上一覧();
            reporter.Output(year, month, rows);
        }
        #endregion

        /// <summary>
        /// 「手配依頼種別」ＤＤＬが選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 詳細種別ＤＤＬをクリア
            comboBox詳細種別.Items.Clear();

            // 選択された種別が「修繕」の場合、詳細種別ＤＤＬを再構築
            MsThiIraiSbt selected = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (selected.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
                dmyThiIraiShousai.MsThiIraiShousaiID = "";
                dmyThiIraiShousai.ThiIraiShousaiName = "";
                comboBox詳細種別.Items.Add(dmyThiIraiShousai);

                foreach (MsThiIraiShousai shousai in thiIraiShousais)
                {
                    comboBox詳細種別.Items.Add(shousai);
                }
                comboBox詳細種別.SelectedIndex = 0;
            }
        }
        #endregion


        /// <summary>
        /// 行がダブルクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView概算計上一覧_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        private void dataGridView概算計上一覧_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                概算計上一覧情報.詳細Formを開く();
            }
            catch
            {
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// 「基幹システム連携」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button基幹システム連携_Click(object sender, EventArgs e)
        private void button基幹システム連携_Click(object sender, EventArgs e)
        {
            string message = "";


            // 基幹連携対象月の月次処理がされていない場合、エラーを表示
            if (基幹連携対象年月 == 概算計上対象検索条件.NextNenGetsu)
            {
                message = 概算計上対象検索条件.NextNenGetsu.ToString("yyyy/MM") + " の月次計上確定が実施されていません。";
                MessageBox.Show(message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // 実行確認
            //DateTime tmp1 = DateTime.Today;
            //DateTime tmp2 = DateTime.Parse(tmp1.Year.ToString() + "/" + tmp1.Month.ToString("00") + "/01");
            //string message = tmp2.AddDays(-1).ToShortDateString() + " 以前の未支払の受領データを概算計上します。よろしいですか？";
            message = 基幹連携対象年月.ToString("yyyy/MM") + " の概算データと貯蔵品データを基幹システムへ送信します。よろしいですか？";
            if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            // 表示されているデータを対象として、サーバ側で処理を実施する
            List<OdGaisanKeijo> OdGaisanKeijos = 概算計上一覧情報.概算計上対象取得();
            //List<string> OdJryIds = 概算計上一覧情報.未計上の受領ID();

            bool ret = false;
            NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC概算計上処理_実行(NBaseCommon.Common.LoginUser, 基幹連携対象年月.ToString("yyyyMM"), OdGaisanKeijos);
                }
            }, "基幹システム連携処理中です...");
            progressDialog.ShowDialog();
            if (ret == true)
            {
                MessageBox.Show("基幹システム連携をしました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                一覧更新(概算計上対象検索条件);
            }
            else
            {
                MessageBox.Show("基幹システム連携に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 「月次計上確定」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button月次計上確定_Click(object sender, EventArgs e)
        private void button月次計上確定_Click(object sender, EventArgs e)
        {
            string message = "";

            // 月次対象月の基幹連携処理がされていない場合、エラーを表示
            if (概算計上対象検索条件.NextNenGetsu != 基幹連携対象年月)
            {
                message = 基幹連携対象年月.ToString("yyyy/MM") + " の基幹システム連携が実施されていません。";
                MessageBox.Show(message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!貯蔵品チェック())
            {
                return;
            }

            // 実行確認
            message = "月次計上を確定します。　\n（月次を確定すると、確定した月次のデータは変更、修正できません。）\n よろしいですか？";
            if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            bool ret = false;
            NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC月次計上処理_実行(NBaseCommon.Common.LoginUser, 概算計上対象検索条件.NextNenGetsu.ToString("yyyyMM"));
                }

            }, "月次計上処理中です...");
            progressDialog.ShowDialog();
            if (ret == true)
            {
                MessageBox.Show("月次計上を確定しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                対象年月セット();
            }
            else
            {
                MessageBox.Show("月次計上に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 「管理者機能」タブページの月次確定対象となる年月をセットする
        /// </summary>
        #region private void 対象年月セット()
        private void 対象年月セット()
        {
            OdGetsujiShime getsujiShime = null;
            string latestNengetsu = "";
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                getsujiShime = serviceClient.OdGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
                latestNengetsu = serviceClient.OdGaisanKeijo_GetLatestNengetsu(NBaseCommon.Common.LoginUser);
            }
            概算計上対象検索条件.NextNenGetsu = DateTime.Parse(getsujiShime.NenGetsu.Substring(0, 4) + "/" + getsujiShime.NenGetsu.Substring(4, 2) + "/01").AddMonths(1);
            基幹連携対象年月 = DateTime.Parse(latestNengetsu.Substring(0, 4) + "/" + latestNengetsu.Substring(4, 2) + "/01").AddMonths(1);
            概算計上対象検索条件.YearMonth = 基幹連携対象年月.ToString("yyyyMM");

            if (maskedTextBox対象年月.Text == null || maskedTextBox対象年月.Text.Length == 0)
            {
                maskedTextBox対象年月.Text = 概算計上対象検索条件.NextNenGetsu.ToString("yyyy/MM");
            }
            if (maskedTextBox対象年月.Text.Trim().Replace("/","").Length == 0)
            {
                maskedTextBox対象年月.Text = 概算計上対象検索条件.NextNenGetsu.ToString("yyyy/MM");
            }
            label月次締め対象.Text = "（対象年月：" + 概算計上対象検索条件.NextNenGetsu.ToString("yyyy/MM") + "）";
            label基幹連携対象.Text = "（対象年月：" + 基幹連携対象年月.ToString("yyyy/MM") + "）";

            // 2017.04.05 該当年月が当月以降の場合、ボタンは無効とする
            button月次計上確定.Enabled = true;
            if (概算計上対象検索条件.NextNenGetsu >= DateTime.Parse(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("00") + "/01"))
            {
                button月次計上確定.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 一覧更新
        /// </summary>
        #region private void 一覧更新(概算計上一覧検索条件 検索条件)
        private void 一覧更新(概算計上一覧検索条件 検索条件)
        {
            if (検索条件 != null && 検索条件.YearMonth.Length == 0)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            対象年月セット();
            概算計上一覧情報.一覧更新(検索条件);

            this.Cursor = Cursors.Default;
        }
        #endregion


        private bool 貯蔵品チェック()
        {
            bool ret = true;
            string 対象年月 = 概算計上対象検索条件.YearMonth;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsVessel> vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);

                foreach( MsVessel vessel in vessels)
                {
                    List<OdChozoShousai> chozoShousai = serviceClient.OdChozoShousai_GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, vessel.MsVesselID, 対象年月, (int)OdChozo.ShubetsuEnum.潤滑油);

                    bool checkOk = false;
                    foreach (OdChozoShousai shousai in chozoShousai)
                    {
                        if (shousai.Count > 0)
                        {
                            checkOk = true;
                            break;
                        }
                    }
                    if (checkOk == false)
                    {
                        if (MessageBox.Show(vessel.VesselName + "の潤滑油残量が入力されていません。よろしいですか？", "潤滑油入力確認", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            ret = false;
                            break;
                        }
                    }

                }
            }
            return ret;
        }
    }
}
