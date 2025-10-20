using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hachu.BLC;
using NBaseData.DAC;
using NBaseData.BLC;

namespace Hachu.Chozo
{
    //シングルトン対応
    public partial class 貯蔵品Form : Form
    {        
        //public**********************************************
        /// <summary>
        /// 作成＆取得
        /// </summary>
        /// <returns></returns>
        public static 貯蔵品Form GetInstance()
        {            
            
            if (貯蔵品Form.Instance == null)
            {
                貯蔵品Form.Instance = new 貯蔵品Form();
            }

            return 貯蔵品Form.Instance;
        }

        //private*********************************************
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private 貯蔵品Form()
        {
            InitializeComponent();

            //リスト管理クラスの初期化
            this.ListManager = new 貯蔵品FormTreeListManager(this.ChozoTreeList);
        }

        //メンバ関数========================================
        private bool フォーム初期化()
        {

            this.SearchKindCombo初期化();
            this.SearchYearMonthCombo初期化();
            this.SearchVesselCombo初期化();

            this.ListManager.TreeList初期化();


            //保存をできないようにしておく
            this.SaveButton.Enabled = false;
            this.OutputButton.Enabled = false;

            return true;
        }

        /// <summary>
        /// 「種別」ＤＤＬ
        /// </summary>
        #region
        private void SearchKindCombo初期化()
        {
            //関係データのAdd
            foreach (string s in BLC.貯蔵品編集処理.KindData)
            {
                this.SearchKindCombo.Items.Add(s);
                this.SearchKindCombo.SelectedIndex = 0;
            }
        }
        #endregion

        /// <summary>
        /// 「対象年月」ＤＤＬ
        /// </summary>
        #region
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


            //月の初期化
            int selectindex = 0;
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
        #endregion

        /// <summary>
        /// 「船」ＤＤＬ
        /// </summary>
        #region
        private void SearchVesselCombo初期化()
        {
            // 中身のクリア
            this.SearchVesselCombo.Items.Clear();
                                   
            // 全データの取得
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                this.MsVesselList = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
            }
      
            // データを表示状態にする
            foreach (NBaseData.DAC.MsVessel data in this.MsVesselList)
            {                
                this.SearchVesselCombo.Items.Add(data);
            }

            // 初期選択をしておく
            this.SearchVesselCombo.SelectedIndex = 0;
        }
        #endregion


        private bool 検索総括()
        {   
            MsVessel msVessel = this.SearchVesselCombo.SelectedItem as MsVessel;
            int yindex = this.SearchYearCombo.SelectedIndex;
            int mindex = this.SearchMonthCombo.SelectedIndex;
            int kind = this.SearchKindCombo.SelectedIndex;      //種別はIndexに対応している
 
            //何かしらが選択されていない。
            //if (yindex < 0 || mindex < 0 || no < 0 || kind < 0)
            if (yindex < 0 || mindex < 0 || kind < 0)
            {
                MessageBox.Show("検索条件が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //カーソルを変更する
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            int year = 0;
            int month = 0;
            int endmonth = 0;

            year = BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset;

            //とりあえず開始月
            month = BLC.貯蔵品編集処理.SearchMothData[mindex].StartMonth;
            endmonth = BLC.貯蔵品編集処理.SearchMothData[mindex].EndMonth;          

            
            bool shime = this.月次締め確認(year, endmonth);
      

            //List<貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索(year, year, month, endmonth, id, kind);
            
            //ここで金額の計算をする。
            //貯蔵品金額計算処理.貯蔵品金額計算総括( datalist, year, endmonth);


            //List<貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索2(year, year, month, endmonth, id, kind);
            List<貯蔵品TreeInfo> datalist = new List<貯蔵品TreeInfo>();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                貯蔵品リスト.対象Enum kindEnum = 貯蔵品リスト.対象Enum.潤滑油;
                if (kind == 0)
                {
                    kindEnum = 貯蔵品リスト.対象Enum.潤滑油;
                }
                //else
                else if (kind == 1)
                {
                    kindEnum = 貯蔵品リスト.対象Enum.船用品;
                }
                else
                {
                    kindEnum = 貯蔵品リスト.対象Enum.特定品;
                } 
                List<貯蔵品編集RowData> tmp = serviceClient.BLC_Get貯蔵品(NBaseCommon.Common.LoginUser, msVessel, kindEnum, year, month, year, endmonth);

                foreach (貯蔵品編集RowData t in tmp)
                {
                    if (t.OdChozoShousaiData != null) // 2014.07.07 :OdChozoShousaiDataがない場合は、表示対象としない
                    datalist.Add(new 貯蔵品TreeInfo(t));
                }
            }

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

                MessageBox.Show("データがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            //// 特定品もOutputの場合、こちら
            this.OutputButton.Enabled = true;

            //// 現行リリースされているのはこちら
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
        private bool 月次締め確認( int year, int month)
        {
            //最後に保存ができるかをチェックする
            bool result = BLC.貯蔵品編集処理.Check保存(year, month);
            this.SaveButton.Enabled = result;
            label入力メッセージ.Visible = result;
            label月次確定メッセージ.Visible = !result;

            return result;
        }

        private void 管理票出力Main()
        {
            MsVessel msVessel = this.SearchVesselCombo.SelectedItem as MsVessel;
            int yindex = this.SearchYearCombo.SelectedIndex;
            int mindex = this.SearchMonthCombo.SelectedIndex;
            int kind = this.SearchKindCombo.SelectedIndex;      //種別はIndexに対応している
            int FromYear = BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset;
            int ToYear = BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset;
            int FromMonth = BLC.貯蔵品編集処理.SearchMothData[mindex].StartMonth;
            int ToMonth = BLC.貯蔵品編集処理.SearchMothData[mindex].EndMonth;
            貯蔵品リスト.対象Enum kindEnum;
            if (kind == (int)貯蔵品リスト.対象Enum.潤滑油)
            {
                kindEnum = 貯蔵品リスト.対象Enum.潤滑油;
            }
            else if (kind == (int)貯蔵品リスト.対象Enum.船用品)
            {
                kindEnum = 貯蔵品リスト.対象Enum.船用品;
            }
            else
            {
                kindEnum = 貯蔵品リスト.対象Enum.特定品;
            }

            Hachu.Reports.Data貯蔵品 貯蔵品出力 = new Hachu.Reports.Data貯蔵品("", "", kindEnum);
            貯蔵品出力.Output(msVessel, FromYear, FromMonth, ToYear, ToMonth);
        }

        //メンバ変数========================================


        //-------------------------------        
        /// <summary>
        /// 実態
        /// </summary>
        private static 貯蔵品Form Instance = null;

        //リスト管理クラス
        private 貯蔵品FormTreeListManager ListManager = null;
        
        //現在表示している船データ
        List<NBaseData.DAC.MsVessel> MsVesselList = null;

        //--------------------------------------------------------------
        //

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //イベント処理----------------------------------------
        private void 貯蔵品Form_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(5, 5);
            this.Location = new Point(0, 0);

            // Form幅を親Formの幅にする
            this.Width = Parent.ClientSize.Width;
            this.Height = Parent.ClientSize.Height;

            this.Text = NBaseCommon.Common.WindowTitle("JM041001", "貯蔵品編集", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            bool ret = this.フォーム初期化();

            //初期化失敗
            if (ret == false)
            {
                this.Close();
            }

            // 2012年度 VersionUp改造
            this.WindowState = FormWindowState.Maximized;

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
            //bool ret = BLC.貯蔵品編集処理.保存処理(this.ListManager.TREE_INFO_LIST);

            bool ret = false;
            //if (this.SearchKindCombo.SelectedIndex == 2)
            //{
            //    MsVessel msVessel = this.SearchVesselCombo.SelectedItem as MsVessel;
            //    int yindex = this.SearchYearCombo.SelectedIndex;
            //    int mindex = this.SearchMonthCombo.SelectedIndex;
            //    int year = BLC.貯蔵品編集処理.START_YEAR + yindex + BLC.貯蔵品編集処理.SearchMothData[mindex].YearOffset;
            //    int month = BLC.貯蔵品編集処理.SearchMothData[mindex].EndMonth;

            //    ret = BLC.貯蔵品編集処理.保存処理_特定品(msVessel.MsVesselID, year, month, this.ListManager.TREE_INFO_LIST);
            //}
            //else
            //{
                ret = BLC.貯蔵品編集処理.保存処理(this.ListManager.TREE_INFO_LIST);
            //}

            if (ret == false)
            {
                MessageBox.Show("保存に失敗しました", "貯蔵品", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
