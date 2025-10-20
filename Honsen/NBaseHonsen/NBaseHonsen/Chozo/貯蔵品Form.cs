using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NBaseHonsen.BLC;
using NBaseData.DAC;
using SyncClient;
using NBaseData.BLC;

namespace NBaseHonsen.Chozo
{
    public partial class 貯蔵品Form : Form
    {        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 貯蔵品Form()
        {
            同期Client.SYNC_SUSPEND = true;

            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN040101", "貯蔵品一覧", WcfServiceWrapper.ConnectedServerID);

            //リスト管理クラスの初期化
            this.ListManager = new 貯蔵品FormTreeListManager(this.ChozoTreeList);
        }
        //public**********************************************

        //メンバ関数========================================
        private bool フォーム初期化()
        {

            this.SearchKindCombo初期化();
            this.SearchYearMonthCombo初期化();

            this.ListManager.TreeList初期化();


            //保存をできないようにしておく
            this.SaveButton.Enabled = false;
            this.OutputButton.Enabled = false;

            return true;
        }

        private void SearchKindCombo初期化()
        {
            //関係データのAdd
            foreach (string s in BLC.貯蔵品編集処理.KindData)
            {
                this.SearchKindCombo.Items.Add(s);
                this.SearchKindCombo.SelectedIndex = 0;
            }
        }

        private void SearchYearMonthCombo初期化()
        {
            int kind = this.SearchKindCombo.SelectedIndex;

            //選択されていないなら潤滑油にしておく
            if (kind < 0)
            {
                kind = BLC.貯蔵品編集処理.潤滑油種別NO;
            }
            
            //データの初期化
            BLC.貯蔵品編集処理.検索年ComboBox初期化(ref this.SearchYearCombo, kind);


            int selectindex = 0;

            //月の初期化
            foreach (検索月管理 data in BLC.貯蔵品編集処理.SearchMothData)
            {
                this.SearchMonthCombo.Items.Add(data.表示名);

                //今月を選択する
                if (data.StartMonth == DateTime.Now.Month &&
                    data.EndMonth == DateTime.Now.Month)
                {
                    selectindex = this.SearchMonthCombo.Items.Count - 1;
                }

                this.SearchMonthCombo.SelectedIndex = selectindex;
            }

         
            
        }

        private bool 検索総括()
        {               
            int yindex = this.SearchYearCombo.SelectedIndex;
            int mindex = this.SearchMonthCombo.SelectedIndex;
            int kind = this.SearchKindCombo.SelectedIndex;      //種別はIndexに対応している

            //何かしらが選択されていない。
            if (yindex < 0 || mindex < 0 || kind < 0)
            {
                MessageBox.Show("検索条件が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //カーソルを変更する
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            //船IDを設定する
            int id = SyncClient.同期Client.LOGIN_VESSEL.MsVesselID;
            

            int year = 0;
            int month = 0;
            int endmonth = 0;

            year = BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset;

            //とりあえず開始月
            month = BLC.貯蔵品編集処理.SearchMothData[mindex].StartMonth;
            endmonth = BLC.貯蔵品編集処理.SearchMothData[mindex].EndMonth;           

            bool shime = this.月次締め確認(year, endmonth);

            List<貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索(year, year, month, endmonth, id, kind);


            //検索条件の設定
            this.ListManager.Year = year;
            this.ListManager.Month = endmonth;

            this.ListManager.TreeList描画(datalist, shime);

            //カーソルをもとに戻す
            this.Cursor = System.Windows.Forms.Cursors.Default;

            //データがないときは保存できないようにする
            if (datalist.Count <= 0)
            {
                this.SaveButton.Enabled = false;
                this.OutputButton.Enabled = false;
                return false;
            }

            // 特定品もOutputの場合、こちら
            this.OutputButton.Enabled = true;

            // 現行リリースされているのはこちら
            //if (kind == 2)
            //{
            //    this.OutputButton.Enabled = false;
            //}
            //else
            //{
            //    this.OutputButton.Enabled = true;
            //}
            return true;
        }

        /// <summary>
        /// 今月が保存できるかをチェック
        /// 引数：確認年月
        /// 返り値：結局しめてるかどうか？
        /// </summary>
        private bool 月次締め確認(int year, int month)
        {
            //最後に保存ができるかをチェックする
            bool result = BLC.貯蔵品編集処理.Check保存(year, month);
            this.SaveButton.Enabled = result;
            label入力メッセージ.Visible = result;
            label月次確定メッセージ.Visible = !result;

            return result;
        }

        /// <summary>
        /// 保存ファイル選択
        /// 引数：選択ファイル名
        /// 返り値：選ばれたか？
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool SelectSaveFile(ref string filename)
        {
            //this.saveFileDialog1.DefaultExt = ".xls";
            //this.saveFileDialog1.CreatePrompt = true;       //新規作成確認
            this.saveFileDialog1.OverwritePrompt = true;    //上書き確認

            this.saveFileDialog1.Filter = 
                "xlsxファイル(*.xlsx)|*.xlsx" + 
                "|" + 
                "全てのファイル(*.*)|*.*";
                

            DialogResult ret = this.saveFileDialog1.ShowDialog();

            if (ret == DialogResult.Cancel)
            {
                return false;
            }

            filename = this.saveFileDialog1.FileName;

            return true;
        }
        
        private void 管理票出力Main()
        {
            //ファイル名を入力させる
            string filename = "";
            bool ret = false;

            //int no = this.SearchVesselCombo.SelectedIndex;
            int yindex = this.SearchYearCombo.SelectedIndex;
            int mindex = this.SearchMonthCombo.SelectedIndex;
            int kind = this.SearchKindCombo.SelectedIndex;      //種別はIndexに対応している

            //関連Vesselを取得する
            MsVessel msvessel = MsVessel.GetRecordByMsVesselID(NBaseCommon.Common.LoginUser,
                                        SyncClient.同期Client.LOGIN_VESSEL.MsVesselID);


            //何かしらが選択されていない。
            if (yindex < 0 || mindex < 0 || kind < 0 || msvessel == null)
            {
                MessageBox.Show("検索条件が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //保存するものがない
            if (this.ListManager.TREE_INFO_LIST.Count <= 0)
            {
                MessageBox.Show("保存するものがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //////////////////////////////////////////////////////////////////
            //カレントを取得
            string current = System.IO.Directory.GetCurrentDirectory();
            
            //保存ファイル選択            
            ret = this.SelectSaveFile(ref filename);
            if (ret == false)
            {
                return;
            }

            //もとへもどす
            System.IO.Directory.SetCurrentDirectory(current);
            current = System.IO.Directory.GetCurrentDirectory();


            //カーソルを変更する
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            
            ret = BLC.貯蔵品管理票処理.管理票保存総括(filename, msvessel,
                kind,
                BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset,
                BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset,
                BLC.貯蔵品編集処理.SearchMothData[mindex].StartMonth,
                BLC.貯蔵品編集処理.SearchMothData[mindex].EndMonth,
                this.ListManager.TREE_INFO_LIST);


            this.Cursor = System.Windows.Forms.Cursors.Default;
                       

            if (ret == false)
            {
                MessageBox.Show("管理表出力に失敗しました", "貯蔵品管理票出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message = "「" + filename + "」へ出力しました";
            MessageBox.Show(message, "貯蔵品管理票出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 年間管理票出力Main()
        {
            //ファイル名を入力させる
            string filename = "";
            bool ret = false;
            
            int yindex = this.SearchYearCombo.SelectedIndex;
            int kind = this.SearchKindCombo.SelectedIndex;      //種別はIndexに対応している

            //関連Vesselを取得する
            MsVessel msvessel = MsVessel.GetRecordByMsVesselID(NBaseCommon.Common.LoginUser,
                                        SyncClient.同期Client.LOGIN_VESSEL.MsVesselID);

            //何かしらが選択されていない。
            if (yindex < 0 || msvessel == null || kind < 0)
            {
                MessageBox.Show("条件が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ////////////////////////////////////////////////////////////////////
            //保存ファイル選択            
            ret = this.SelectSaveFile(ref filename);
            if (ret == false)
            {
                return;
            }

            /////////////////////////////////////////////////////////////////
            //カーソルを待ちに変更
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            ret = BLC.貯蔵品年間管理票処理.貯蔵品年間管理票出力総括(
                filename,
                msvessel,
                yindex + BLC.貯蔵品編集処理.START_YEAR,
                kind
                );


            //カーソルを通常に戻す            
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (ret == false)
            {
                MessageBox.Show("年間管理表出力に失敗しました", "貯蔵品年間管理票出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message = "「" + filename + "」へ出力しました";
            MessageBox.Show(message, "貯蔵品年間管理票出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //メンバ変数========================================


        //-------------------------------        
        /// <summary>
        /// 実態
        /// </summary>
        private static 貯蔵品Form Instance = null;

        //リスト管理クラス
        private 貯蔵品FormTreeListManager ListManager = null;

        //--------------------------------------------------------------
        //

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //イベント処理----------------------------------------
        private void 貯蔵品Form_Load(object sender, EventArgs e)
        {
            bool ret = this.フォーム初期化();

            //初期化失敗
            if (ret == false)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 検索ボタンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.検索総括();

            // 次期改造
            this.ListManager.ExpandAll();
        }

        /// <summary>
        /// 閉じる時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 貯蔵品Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            貯蔵品Form.Instance = null;
        }

        /// <summary>
        /// 保存ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool ret = BLC.貯蔵品編集処理.保存処理(this.ListManager.TREE_INFO_LIST);

            if (ret == false)
            {
                MessageBox.Show("保存に失敗しました", "保存", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("保存しました", "貯蔵品", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 管理票出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputButton_Click(object sender, EventArgs e)
        {
            this.管理票出力Main();
        }

        private void 貯蔵品Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            同期Client.SYNC_SUSPEND = false;
        }

        /// <summary>
        /// 年間管理票出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputYearButton_Click(object sender, EventArgs e)
        {
            this.年間管理票出力Main();
        }
    }
}
