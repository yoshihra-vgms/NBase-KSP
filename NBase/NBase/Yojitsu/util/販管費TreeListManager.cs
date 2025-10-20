using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

using NBaseData.DAC;

namespace Yojitsu.util
{
    public class 販管費TreeListManager
    {
        /// <summary>
        /// コンストラクタ
        /// 引数：管理リスト
        /// </summary>
        /// <param name="tlist"></param>
        public 販管費TreeListManager(TreeListView tlist)
        {
            this.ManageList = tlist;
        }

        //public***************************************************
        //メンバ関数==============================================
        
        /// <summary>
        /// リストの基本的な初期化
        /// </summary>
        public void TreeList初期化()
        {
            //内容の削除
            this.ManageList.Columns.Clear();

            foreach (ColInfo coldata in util.販管費TreeListManager.ColData)
            {
                TreeListViewColumn col = new TreeListViewColumn();
                

                //データを設定
                col.HeaderText = coldata.ColName;
                col.Width = coldata.Width;
                col.FixedWidth = true;
                col.ContentType = ColumnContentType.Control;

                //マイナスのときは自動計算
                if (coldata.Width < 0)
                {
                    col.Width = (this.ManageList.Width / 販管費TreeListManager.ColData.Length) - 1;
                    coldata.Width = col.Width;
                }

                //ADDする
                this.ManageList.Columns.Add(col);

            }
        }


        /// <summary>
        /// 描画
        /// 引数：描画対象、総計を描画するか？
        /// </summary>
        public void TreeList描画( List<販菅費TreeListData> datalist, bool flag)
        {

            //現在のデータをクリアする
            this.ManageList.Nodes.Clear();
			this.ManageList.Nodes.Capacity = (datalist.Count + 1);

			///////////////////////////////////////////////////////////////
			//描画をとめる
			this.ManageList.SuspendUpdate();

				//描画をする
				foreach (販菅費TreeListData data in datalist)
				{
					this.AddListData(data);
				}

				if (flag == true)
				{
					//総計を描く
					this.AddListData(this.総計Data);
				}
			
			//描画を許可する
			this.ManageList.ResumeUpdate();
			///////////////////////////////////////////////////////////////
        }


        //private*****************************************************
        //メンバ関数==============================================
        
        /// <summary>
        /// データを管理リストに一行追加する
        /// </summary>
        /// <param name="data"></param>
        private void AddListData(販菅費TreeListData data)
        {

            TreeListViewNode node = new TreeListViewNode();
            TreeListViewSubItem item = null;


			decimal rate = 販管費TreeListManager.出力単位レート;

            //船名
            item = this.CreateSubItem(data);
            item.Text = data.船名;
            node.SubItems.Add(item);

            
            //DWT
            item = this.CreateSubItem(data);
            item.Text = data.DWT_割掛.ToString();            
            node.SubItems.Add(item);


            //営業基礎割掛
            item = this.CreateSubItem(data);
            item.Text = Math.Round(data.営業基礎割掛 / rate, MidpointRounding.AwayFromZero).ToString();
            this.ChangeTextColor(ref item, data.営業基礎割掛);
            node.SubItems.Add(item);


            //管理基礎割掛
            item = this.CreateSubItem(data);
            item.Text = Math.Round(data.管理基礎割掛 / rate, MidpointRounding.AwayFromZero).ToString();
			this.ChangeTextColor(ref item, data.管理基礎割掛);
            node.SubItems.Add(item);


            //差額DWT割掛
            item = this.CreateSubItem(data);
            item.Text = Math.Round(data.差額DWT割掛 / rate, MidpointRounding.AwayFromZero).ToString();
            this.ChangeTextColor(ref item, data.差額DWT割掛);
            node.SubItems.Add(item);

            //計
            item = this.CreateSubItem(data);
            item.Text = Math.Round(data.計 / rate, MidpointRounding.AwayFromZero).ToString();
            this.ChangeTextColor(ref item, data.計);
            node.SubItems.Add(item);
            

            //総額に対する割合
            item = this.CreateSubItem(data);
			item.Text = data.総額に対する割合.ToString(小数点表示書式);
            this.ChangeTextColor(ref item, data.総額に対する割合 * 10000.0m);
            node.SubItems.Add(item);


            //追加する
            this.ManageList.Nodes.Add(node);

        }

        /// <summary>
        /// サブアイテムを作成する
		/// 引数：関連データ
		/// 返り値：作成したさサブアイテム
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeListViewSubItem CreateSubItem(販菅費TreeListData data)
        {
            TreeListViewSubItem item = new TreeListViewSubItem();
            
            item.StyleFromParent = false;

            //色をつけるように設定されていた
            if (data.SpecialFlag == true)
            {
                //色を設定する                
                //item.FormatStyle.Font = UIConstants.DEFAULT_FONT;
                item.NormalStyle.BackColor = data.SpecialColor;
                item.NormalStyle.BorderColor = Color.Black;
            }

            return item;
        }

        /// <summary>
        /// データによって色を変える
        /// 引数：変えたいもの、データ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="d"></param>
        private void ChangeTextColor(ref TreeListViewSubItem item, decimal d)
        {
			//マイナスかをチェックする
            if (d < 0.0m)
            {
                item.NormalStyle.TextColor = 販管費TreeListManager.マイナス色;
            }
        }

        //メンバ変数==============================================

        /// <summary>
        /// 自分の管理リスト
        /// </summary>
        private TreeListView ManageList = null;


        /// <summary>
		/// 総計データ(最後に追加する自分で計算してだすデータ)
        /// </summary>
        private 販菅費TreeListData 総計Data = new 販菅費TreeListData("総計", true, 販管費TreeListManager.総計背景色);
        public 販菅費TreeListData 総計
        {
            get
            {
                return this.総計Data;
            }
            set
            {
                this.総計Data = value;
            }
        }
    

    
        //-----------------------------------------------------------
        //定義

        //カラムタイトル情報データ
        private static ColInfo[] ColData = {                        //ﾀｲﾄﾙ          横幅(-で自動計算)
                                                        new ColInfo(    "船名",     290),
                                                        new ColInfo(    "DWT(L/T)",     75),
                                                        new ColInfo(    "営業基礎割掛",     85),
                                                        new ColInfo(    "管理基礎割掛",     85),
                                                        new ColInfo(    "差額DWT割掛",     85),
                                                        new ColInfo(    "計",     65),
                                                        new ColInfo(    "総額に対する割合",     105),

                                                    };

        //総計の色
        private static readonly Color 総計背景色 = Color.Yellow;

        //マイナスになったとき色
        private static readonly Color マイナス色 = Color.Red;

		//データの出力書式
		private static readonly string 小数点表示書式 = "F3";

		//数字の単位(書かれているもの)
		private static readonly decimal 出力単位レート = 1000.0m;

        //////////////////////////////////////////////////////////////////
        //内包クラス
        private class ColInfo
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="name"></param>
            /// <param name="w"></param>
            public ColInfo(string name, int w)
            {
                this.ColName = name;
                this.Width = w;
            }

            /// <summary>
            /// カラミュ名
            /// </summary>
            public string ColName = "";

            /// <summary>
            /// 横幅
            /// </summary>
            public int Width = -1;
        }

        
    }

    



}
