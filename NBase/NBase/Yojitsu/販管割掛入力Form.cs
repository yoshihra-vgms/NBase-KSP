using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;

using NBaseData.DAC;
using Yojitsu.DA;
using Yojitsu.util;

namespace Yojitsu
{
    public partial class 販管割掛入力Form : Form
    {
        private NenjiForm nenjiForm;
        private BgYosanHead yosanHead;
        private Dictionary<int, decimal> uriagedakaList;

        public 販管割掛入力Form(NenjiForm nenjiForm, BgYosanHead yosanHead, Dictionary<int, decimal> uriagedakaList)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.uriagedakaList = uriagedakaList;

            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("", "販管費割掛入力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            this.ListManager = new Yojitsu.util.販管費TreeListManager(this.treeListView1);

            EnableComponents();
        }


        private void EnableComponents()
        {
            if (yosanHead.IsFixed())
            {
                maskedTextBox営業基礎割掛.ReadOnly = true;
                maskedTextBox管理基礎割掛.ReadOnly = true;
                maskedTextBox販管費.ReadOnly = true;
                maskedTextBox経営指導料.ReadOnly = true;
                button割掛.Enabled = false;
                button設定.Enabled = false;
            }
        }


        //メンバ関数=========================================
        /// <summary>
        /// フォームの初期化
        /// </summary>
        /// <returns></returns>
        private bool フォーム初期化()
        {
            
            this.ListManager.TreeList初期化();            
            
            //年選択ボックスの初期化
            this.YearCombo初期化();
         
            //検索と描画
			//this.NowDataList = util.販菅費処理.Search販菅費Data(this.YosanHeadData, this.StartYear + this.YearCombo.SelectedIndex);

            //選択データの描画
            //this.SetSelectYearData(this.StartYear + this.YearCombo.SelectedIndex);

            return true;
        }

        /// <summary>
        /// 年選択初期化
        /// </summary>
        /// <returns></returns>
        private void YearCombo初期化()
        {
            this.YearCombo.Items.Clear();

            this.StartYear = YosanHeadData.Year;

            //追加する
            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(YosanHeadData.YosanSbtID); i++)
            {
                this.YearCombo.Items.Add(i + this.StartYear);
            }

            this.YearCombo.SelectedIndex = 0;
        }


        /// <summary>
        /// 入力された値を変換して取得する
        /// </summary>
        /// <param name="eigyobase"></param>
        /// <param name="kanribase"></param>
        /// <param name="hankan"></param>
        /// <returns></returns>
        private bool 割り掛け入力変換処理(ref int eigyobase, ref int kanribase, ref int butujin, ref int keiei, ref int hankan)
        {
            int butu = 0;
            int kei = 0;
            int nen = 0;
            int ei = 0;
            int kan = 0;

            try
            {
                //入力されたものを変換する
                butu = Convert.ToInt32(this.maskedTextBox物件費_人件費.Text);
                kei = Convert.ToInt32(this.maskedTextBox経営指導料.Text);
                nen = Convert.ToInt32(this.maskedTextBox販管費.Text);

                ei = Convert.ToInt32(this.maskedTextBox営業基礎割掛.Text);
                kan = Convert.ToInt32(this.maskedTextBox管理基礎割掛.Text);
                
            }
            catch (Exception e)
            {
                MessageBox.Show("必須入力項目が入力されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //テキスト変換に成功したらレートをかけてデータにする
            butujin = butu * Yojitsu.販管割掛入力Form.Rate;
            keiei = kei * Yojitsu.販管割掛入力Form.Rate;
            hankan = nen * Yojitsu.販管割掛入力Form.Rate;

            eigyobase = ei * Yojitsu.販管割掛入力Form.Rate;
            kanribase = kan * Yojitsu.販管割掛入力Form.Rate;

            return true;
        }

        /// <summary>
        /// 割り掛けボタンが押されたときの処理
        /// 引数：なし
        /// 返り値：成功したか？
        /// </summary>
        private bool 割掛処理()
        {
            int butujin = 0;
            int keiei = 0;
            int hankan = 0;
            int eigyo = 0;
            int kanri = 0;

            //入力を取得する
            bool ret = this.割り掛け入力変換処理(ref eigyo, ref kanri, ref butujin, ref keiei, ref hankan);

            if (ret == false)
            {
                return false;
            }

            //値を保存する
            this.物件費_人件費 = butujin;
            this.経営指導料 = keiei;
            this.年度販菅費 = hankan;
            this.営業割り掛け = eigyo;
            this.管理割り掛け = kanri;

            this.ListManager.総計.SetZero();

            //計算する
            this.ListManager.総計 = util.販菅費処理.割り掛け処理(eigyo, kanri, hankan, keiei, ref this.NowDataList,
                this.ListManager.総計);

            
            //再描画
            this.ListManager.TreeList描画(this.NowDataList, true);


            return true;
        
        }


        /// <summary>
        /// 割り掛けデータを設定する処理
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            #region 入力チェック
            int butujin = 0;
            int keiei = 0;
            int hankan = 0;
            int eigyo = 0;
            int kanri = 0;

            //入力を取得する
            bool result = this.割り掛け入力変換処理(ref eigyo, ref kanri, ref butujin, ref keiei, ref hankan);

            if (result == false)
            {
                return false;
            }

            //値を保存する
            this.物件費_人件費 = butujin;
            this.経営指導料 = keiei;
            this.年度販菅費 = hankan;
            this.営業割り掛け = eigyo;
            this.管理割り掛け = kanri;
            #endregion
                        
            //選択年を取得
            int year = this.StartYear + this.YearCombo.SelectedIndex;

            List<int> msVesselIds = new List<int>();
            List<decimal> amounts = new List<decimal>();
            
            foreach(販菅費TreeListData data in NowDataList)
            {
                msVesselIds.Add(data.VesselData.MsVesselID);
                amounts.Add(data.計);
            }
            
            bool saveResult = DbAccessorFactory.FACTORY.BLC_販管費保存(NBaseCommon.Common.LoginUser, year, this.YosanHeadData, this.営業割り掛け, this.管理割り掛け, this.物件費_人件費, this.経営指導料,
                                          msVesselIds, amounts);


            if (!saveResult)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            nenjiForm.Set販管費(year, msVesselIds, amounts);
            MessageBox.Show("割掛データを設定しました", "販菅費", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }


        /// <summary>
        /// 指定年の販菅費を設定する
        /// 指定年の割り掛けデータがあるなら、データをセットして再計算する。
        /// 引数：選択年
        /// </summary>
        /// <returns></returns>
        private void SetSelectYearData(int year)
        {
            //今回のデータを取得する
            BgHankanhi hankanhi = DA.DbAccessorFactory.FACTORY.BgHankanhi_GetRecordByYosanHeadIDYear(NBaseCommon.Common.LoginUser, this.YosanHeadData.YosanHeadID, year);

            // 経営指導料
            decimal Keieishidouryou = 0;
            if (uriagedakaList.ContainsKey(year))
            {
                decimal uriagedaka = uriagedakaList[year];

                Keieishidouryou = Calc経営指導料(uriagedaka);
            }


            //無い時は何もしない
            if (hankanhi == null)
            {
                //初期の描画
                this.NowDataList = util.販菅費処理.Search販菅費Data(this.YosanHeadData, year);
                this.ListManager.TreeList描画(this.NowDataList, false);

                this.経営指導料 = Convert.ToInt32(Keieishidouryou) / 販管割掛入力Form.Rate;

                this.maskedTextBox物件費_人件費.Text = "0";
                this.maskedTextBox経営指導料.Text = this.経営指導料.ToString(); // 自動計算したものを表示
                this.maskedTextBox販管費.Text = this.経営指導料.ToString();

                this.maskedTextBox営業基礎割掛.Text = "0";
                this.maskedTextBox管理基礎割掛.Text = "0";

                return;
            }

            
            //あったとき
			this.NowDataList = util.販菅費処理.Search販菅費Data(this.YosanHeadData, year);
			
			//テキストに入れるためにデータを各Rateで割っておく
            if (hankanhi.NendoHankanhi >= 0)
                this.物件費_人件費 = Convert.ToInt32(hankanhi.NendoHankanhi) / 販管割掛入力Form.Rate;
            
            // 経営指導料は常に最新の値を表示
            //if (hankanhi.Keieishidouryou > 0)
            //{
            //    this.経営指導料 = Convert.ToInt32(hankanhi.Keieishidouryou) / 販管割掛入力Form.Rate;
            //}
            //else
            //{
                hankanhi.Keieishidouryou = Keieishidouryou;
                this.経営指導料 = Convert.ToInt32(Keieishidouryou) / 販管割掛入力Form.Rate;
            //}

            if (hankanhi.NendoHankanhi + hankanhi.Keieishidouryou >= 0)
                this.年度販菅費 = Convert.ToInt32(hankanhi.NendoHankanhi + hankanhi.Keieishidouryou) / 販管割掛入力Form.Rate;


            if (hankanhi.EigyoKiso >= 0)
                this.営業割り掛け = Convert.ToInt32(hankanhi.EigyoKiso) / 販管割掛入力Form.Rate;
            if (hankanhi.KanriKiso >= 0)
                this.管理割り掛け = Convert.ToInt32(hankanhi.KanriKiso) / 販管割掛入力Form.Rate;

            //テキストへ設定            
            this.maskedTextBox物件費_人件費.Text = this.物件費_人件費.ToString();
            this.maskedTextBox経営指導料.Text = this.経営指導料.ToString();
            this.maskedTextBox販管費.Text = this.年度販菅費.ToString();

            this.maskedTextBox営業基礎割掛.Text = this.営業割り掛け.ToString();
            this.maskedTextBox管理基礎割掛.Text = this.管理割り掛け.ToString();

            
            //再計算を実行する
            this.割掛処理();
        }
            
           
   
        public static decimal Calc経営指導料(decimal uriagedaka)
        {
            return uriagedaka * 経営指導料計算率;
        }
     

        //メンバ変数=========================================

        /// <summary>
        /// リスト管理
        /// </summary>
        private util.販管費TreeListManager ListManager = null;

        /// <summary>
        /// 現在表示しているデータ
        /// </summary>
        private List<util.販菅費TreeListData> NowDataList = new List<Yojitsu.util.販菅費TreeListData>();

        /// <summary>
        /// 現在の割り掛け値
        /// </summary>
        private int 物件費_人件費 = 0;
        private int 経営指導料 = 0;
        private int 年度販菅費 = 0;

        private int 営業割り掛け = 0;
        private int 管理割り掛け = 0;

        /// <summary>
        /// 関係している予算Head
        /// </summary>
        private BgYosanHead YosanHeadData;
        public BgYosanHead YOSAN_HEAD
        {
            set
            {
                this.YosanHeadData = value;
            }
        }



        /// <summary>
        /// 年の開始
        /// </summary>
        private int StartYear = 0;
        //-------------------------------------------------------------
        //定義
        
        //テキストボックスのデータレート
        private const int Rate = 1000;
        
        
        //経営指導料の計算率
        private static decimal 経営指導料計算率 = 0.02m;


        /////////////////////////////////////////////////////////////////////
        //イベント処理-----------------------------------------------

        private void button設定_Click(object sender, EventArgs e)
        {
            bool result = this.Save();

            if (!result)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            DialogResult = DialogResult.OK;
            Dispose();
        }

        private void butt閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void treeListView1_DoubleClick(object sender, EventArgs e)
        {
            #region なぜか船稼働設定日時Formを起動しているのでとりあえずコメントにする
            /*if (sender is TreeListView)
            {
                TreeListView treeListView = (TreeListView)sender;
                船稼働設定日時Form form = new 船稼働設定日時Form(treeListView.SelectedNode);
                form.ShowDialog();
            }*/
            #endregion
        }

        /// <summary>
        /// 割りかけボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button割掛_Click(object sender, EventArgs e)
        {
            this.割掛処理();
        }

        /// <summary>
        /// フォームが読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 販管割掛入力Form_Load(object sender, EventArgs e)
        {
            bool ret = this.フォーム初期化();

            if (ret == false)
            {
                this.Close();
            }

        }

		//年選択が変更されたとき
        private void YearCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //現在の年をだす
            int year = this.StartYear + this.YearCombo.SelectedIndex;

			//選択された年を描画する
            this.SetSelectYearData(year);
        }

        private void maskedTextBox物件費_人件費_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.maskedTextBox物件費_人件費.Text.Length > 0)
            {
                this.物件費_人件費 = Convert.ToInt32(this.maskedTextBox物件費_人件費.Text);
            }
            else
            {
                this.物件費_人件費 = 0;
            }
            if (this.物件費_人件費 >0)
                this.物件費_人件費 = this.物件費_人件費 * Yojitsu.販管割掛入力Form.Rate;

            this.年度販菅費 = Convert.ToInt32(this.物件費_人件費 + this.経営指導料);
            if (this.年度販菅費 > 0)
                this.年度販菅費 = Convert.ToInt32(this.物件費_人件費 + this.経営指導料) / 販管割掛入力Form.Rate;

            this.maskedTextBox販管費.Text = this.年度販菅費.ToString();
        }
    }
}
