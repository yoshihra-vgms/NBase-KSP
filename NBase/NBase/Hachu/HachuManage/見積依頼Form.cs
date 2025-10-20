using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;
using Hachu.BLC;
using Hachu.Reports;
using NBaseData.DS;


namespace Hachu.HachuManage
{
    public partial class 見積依頼Form : BaseUserControl//2021/07/12 BaseForm
    {
        private OdThi 対象手配依頼;
        
        /// <summary>
        /// 対象見積依頼
        /// </summary>
        private OdMm 対象見積依頼;

        /// <summary>
        /// 対象見積依頼の品目
        /// </summary>
        private List<Item見積依頼品目> 見積品目s;

        List<見積依頼先> 依頼先s;

        /// <summary>
        /// 
        /// </summary>
        見積依頼先TreeListView 依頼先TreeList;

        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView見積依頼 品目TreeList;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 見積依頼Form(int windowStyle, ListInfo見積依頼 info)
        public 見積依頼Form(int windowStyle, ListInfo見積依頼 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info.parent;
            対象見積依頼 = info.info;

            EnableComponents();
        }


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "見積依頼"))
            {
                button更新.Enabled = true;
                button取消.Enabled = true;

                button見積依頼作成.Enabled = true;
                button見積依頼書出力.Enabled = true;

                textBox見積回答期限.Enabled = true;
                comboBox支払条件.Enabled = true;
                textBox送り先.Enabled = true;
                textBox手配内容.Enabled = true;
                textBox備考.Enabled = true;
                comboBox入渠科目.Enabled = true;
                textBox内容.Enabled = true;
            }
        }
        #endregion



        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 見積依頼Form_Load(object sender, EventArgs e)
        private void 見積依頼Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「見積依頼作成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼作成_Click(object sender, EventArgs e)
        private void button見積依頼作成_Click(object sender, EventArgs e)
        {
            // 2014.11.07 メール送信時に、事務担当者が必要になることから必須とする
            if (対象手配依頼.JimTantouID == null || 対象手配依頼.JimTantouID.Length == 0)
            {
                MessageBox.Show("手配依頼の事務担当者が選択されていません。" + System.Environment.NewLine + "手配依頼を確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // 2010.03.11:aki 
            // 入渠の場合、入渠科目の設定忘れを防止するために、このタイミングで、確認、更新をするように改造
            //if (panel入渠.Visible == true)
            //{
            //    if (事前更新処理() == false)
            //    {
            //        return;
            //    }
            //}
            // 2010.06.23:aki
            // 入渠だけでなく「更新」忘れを防ぐため、このタイミングで、確認、更新をするように改造
            if (事前更新処理() == false)
            {
                return;
            }


            // 見積依頼先設定画面を起動する
            対象見積依頼.MmKigen = textBox見積回答期限.Text;
            見積依頼先設定Form form = new 見積依頼先設定Form(対象手配依頼, 対象見積依頼, 見積品目s);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            OdMk mk = form.見積回答;
            mk.OdThiID     = 対象手配依頼.OdThiID;
            mk.OdThiNaiyou = 対象手配依頼.Naiyou;
            mk.OdThiBikou  = 対象手配依頼.Bikou;

            ListInfo見積依頼 info = new ListInfo見積依頼();
            info.AddNode = true;
            info.info = 対象見積依頼;
            info.child = mk;
            InfoUpdating(info);

            見積依頼先 mmSaki = new 見積依頼先();
            mmSaki.OdMkID = mk.OdMkID;
            mmSaki.CustomerName = mk.MsCustomerName;
            mmSaki.TantouMailAddress = mk.TantouMailAddress;
            mmSaki.CreateDate = mk.CreateDate;
            mmSaki.見積回答品目s = form.見積回答品目s;
            
            依頼先TreeList.AddNode(mmSaki);

            品目TreeList.AddCustomerData(mmSaki, 見積品目s, true);
            button取消.Visible = false;
            button見積依頼書出力.Enabled = true;
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            if (更新処理() == false)
            {
                return;
            }

            MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo見積依頼 info = new ListInfo見積依頼();
            info.parent = 対象手配依頼;
            info.info = 対象見積依頼;
            InfoUpdating(info);

            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「取消」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button取消_Click(object sender, EventArgs e)
        private void button取消_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この見積依頼を取消します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 取消し処理
            if (取消処理() == false)
            {
                return;
            }
            MessageBox.Show("取消しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo見積依頼 info = new ListInfo見積依頼();
            info.Remove = true;
            info.parent = 対象手配依頼;
            info.info = 対象見積依頼;
            InfoUpdating(info);

            // Formを閉じる
            //---------コメントアウト2021/07/12-----------
            //Close();
            //--------------------------------------------
        }
        #endregion

        /// <summary>
        /// 「見積依頼書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼書出力_Click(object sender, EventArgs e)
        private void button見積依頼書出力_Click(object sender, EventArgs e)
        {
            見積依頼先 依頼先 = 依頼先TreeList.GetSelectedInfo();
            if (依頼先 == null)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //見積り依頼書 依頼書 = new 見積り依頼書();
            //依頼書.Output(依頼先.OdMkID);
            KK発注帳票出力.見積依頼書Output(依頼先.OdMkID);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        #endregion

        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// 
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            List<MsShrJouken> shrJoukens = null;
            List<MsNyukyoKamoku> nyukyoKamokus = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 支払条件
                shrJoukens = serviceClient.MsShrJouken_GetRecords(NBaseCommon.Common.LoginUser);
                // 入渠科目
                nyukyoKamokus = serviceClient.MsNyukyoKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }

            //=========================================
            // 対象見積依頼の内容を画面にセットする
            //=========================================
            this.Text = NBaseCommon.Common.WindowTitle("JM040301", "見積依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            textBox見積依頼番号.Text = 対象見積依頼.MmNo;
            textBox船.Text = 対象手配依頼.VesselName;   // 手配依頼から
            textBox場所.Text = 対象手配依頼.Basho;        // 手配依頼から

            // 見積依頼者
            textBox作成者.Text = 対象見積依頼.MmSakuseishaName;
            // 見積回答期限
            textBox見積回答期限.Text = 対象見積依頼.MmKigen;
            // 支払条件
            comboBox支払条件.Items.Clear();
            foreach (MsShrJouken sj in shrJoukens)
            {
                comboBox支払条件.Items.Add(sj);
                if (sj.MsShrJoukenID == 対象見積依頼.MsShrJoukenID)
                {
                    comboBox支払条件.SelectedItem = sj;
                }
            }
            // 送り先
            textBox送り先.Text = 対象見積依頼.Okurisaki;

            // 入渠科目
            comboBox入渠科目.Items.Clear();
            foreach (MsNyukyoKamoku nk in nyukyoKamokus)
            {
                comboBox入渠科目.Items.Add(nk);
                if (nk.MsNyukyoKamokuID == 対象見積依頼.MsNyukyoKamokuID)
                {
                    comboBox入渠科目.SelectedItem = nk;
                }
            }
            // 2010.01.06 aki: 入渠科目はデフォルトは空表示で、登録時にチェックする
            //if (comboBox入渠科目.SelectedItem == null)
            //{
            //    comboBox入渠科目.SelectedIndex = 0;
            //}

            // 内容
            textBox内容.Text = 対象見積依頼.Naiyou;

            textBox手配内容.Text = 対象手配依頼.Naiyou;   // 手配依頼から
            textBox備考.Text = 対象手配依頼.Bikou;   // 手配依頼から


            // 画面のコンポーネントの表示/非表示
            if (対象手配依頼.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕) &&
                対象手配依頼.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.入渠))
            {
                panel入渠.Visible = true;
            }
            else
            {
                panel入渠.Visible = false;
            }

            InitCustomerTreeListView();
            InitItemTreeListView();

            if (依頼先TreeList.Count > 0)
            {
                button取消.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// 「見積依頼先」初期化
        /// </summary>
        #region private void InitCustomerTreeListView()
        private void InitCustomerTreeListView()
        {
            if (依頼先TreeList != null)
                依頼先TreeList.Clear();

            object[,] columns = new object[,] {
                                               {"依頼先", 270, null, null},
                                               {"メール送信状況", 100, null, null},
                                               {"送信日時", 100, null, null},
                                             };

            依頼先TreeList = new 見積依頼先TreeListView(treeListView見積依頼先);
            依頼先TreeList.SetColumns(columns);

            依頼先s = 見積依頼先.取得(対象見積依頼.OdMmID);
            依頼先TreeList.AddNodes(依頼先s);
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」初期化
        /// </summary>
        #region private void InitItemTreeListView()
        private void InitItemTreeListView()
        {
            if (品目TreeList != null)
                品目TreeList.Clear();

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;
            見積品目s = Item見積依頼品目.GetRecords(対象見積依頼.OdMmID);
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                viewHeader = false;
                columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"数量", 50, null, HorizontalAlignment.Right},
                                         };
            }
            else
            {
                viewHeader = true;
                columns = new object[,] {
                                           {"No", 85, null, HorizontalAlignment.Right},
                                           {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"依頼数", 52, null, HorizontalAlignment.Right},
                                           {"添付", 40, null, HorizontalAlignment.Center},
                                         };
            }
            // 2014.02 2013年度改造 ==>
            //品目TreeList = new ItemTreeListView見積依頼(treeListView);
            品目TreeList = new ItemTreeListView見積依頼(対象手配依頼.MsThiIraiSbtID, treeListView);
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                品目TreeList.Enum表示方式 = ItemTreeListView見積依頼.表示方式enum.Zero以外を表示;
            }
            // <==

            品目TreeList.SuspendUpdate();
            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.AddNodes(viewHeader, 見積品目s);
            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);

            foreach (見積依頼先 依頼先 in 依頼先s)
            {
                品目TreeList.AddCustomerData(依頼先, 見積品目s);
            }
            if (依頼先s.Count > 0)
            {
                if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "見積依頼"))
                {
                    button見積依頼書出力.Enabled = true;
                }
            }

            品目TreeList.ResumeUpdate();
        }
        #endregion

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <returns></returns>
        #region private bool 更新処理()
        private bool 更新処理()
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                // 更新
                bool ret = 見積依頼更新処理.更新(ref 対象見積依頼);
                if (ret == false)
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("更新に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 事前更新処理
        /// </summary>
        /// <returns></returns>
        #region private bool 事前更新処理()
        private bool 事前更新処理()
        {
            try
            {
                if (panel入渠.Visible == true)
                {
                    MsNyukyoKamoku nyukyoKamoku = null;
                    if (comboBox入渠科目.SelectedItem is MsNyukyoKamoku)
                    {
                        nyukyoKamoku = comboBox入渠科目.SelectedItem as MsNyukyoKamoku;
                    }
                    if (nyukyoKamoku == null)
                    {
                        MessageBox.Show("見積依頼を作成する前に、入渠科目を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // 更新
                if (入力情報の取得確認() == false)
                {
                    return false;
                }
                bool ret = 見積依頼更新処理.更新(ref 対象見積依頼);
                if (ret == false)
                {
                    MessageBox.Show("事前更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("事前更新に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <returns></returns>
        #region private bool 取消処理()
        private bool 取消処理()
        {
            try
            {
                bool ret = 見積依頼更新処理.取消(ref 対象手配依頼, ref 対象見積依頼);
                if (ret == false)
                {
                    MessageBox.Show("取消に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("取消に失敗しました。\n致命的なエラーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力情報の取得確認
        /// </summary>
        /// <returns></returns>
        #region private bool 入力情報の取得確認()
        private bool 入力情報の取得確認()
        {
            string errMessage = "";

            MsShrJouken shrJouken = null;
            if (comboBox支払条件.SelectedItem is MsShrJouken)
            {
                shrJouken = comboBox支払条件.SelectedItem as MsShrJouken;
            }
            string okurisaki = null;
            try
            {
                okurisaki = textBox送り先.Text;
            }
            catch
            {
            }
            MsNyukyoKamoku nyukyoKamoku = null;
            string naiyou = null;
            if (panel入渠.Visible == true)
            {
                if (comboBox入渠科目.SelectedItem is MsNyukyoKamoku)
                {
                    nyukyoKamoku = comboBox入渠科目.SelectedItem as MsNyukyoKamoku;
                }
                if (nyukyoKamoku == null)
                {
                    errMessage += "・入渠科目を選択してください。\n";
                }
                naiyou = textBox送り先.Text;
            }
            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            対象見積依頼.MmKigen = textBox見積回答期限.Text;           
            if ( shrJouken != null )
                対象見積依頼.MsShrJoukenID = shrJouken.MsShrJoukenID;
            対象見積依頼.Okurisaki = okurisaki;
            if (nyukyoKamoku != null)
                対象見積依頼.MsNyukyoKamokuID = nyukyoKamoku.MsNyukyoKamokuID;
            対象見積依頼.Naiyou = naiyou;

            return true;
        }
        #endregion

        public void View添付ファイル(string odAttachFileId)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;

            try
            {
                Cursor = Cursors.WaitCursor;

                OdAttachFile odAttachFile = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // サーバから添付データを取得する
                    odAttachFile = serviceClient.OdAttachFile_GetRecord(NBaseCommon.Common.LoginUser, odAttachFileId);
                }

                if (odAttachFile == null)
                {
                    MessageBox.Show("対象ファイルを開けません：添付ファイルがみつかりません", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fileName = odAttachFile.FileName;
                fileData = odAttachFile.Data;
                id = odAttachFile.OdAttachFileID;

                // ファイルの表示
                NBaseCommon.FileView.View(id, fileName, fileData);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
