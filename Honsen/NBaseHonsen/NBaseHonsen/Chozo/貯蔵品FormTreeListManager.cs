using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseHonsen.Chozo
{
    /// <summary>
    /// 貯蔵品Formのリストビュー管理クラス
    /// </summary>
    public class 貯蔵品FormTreeListManager
    {
        Font DefaultFont = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);

        //コンストラクタ        
        public 貯蔵品FormTreeListManager(TreeListView tlist)
        {
            //管理リストを設定する。
            this.ManageList = tlist;
        }

        //public*********************************
        /// <summary>
        /// 管理リストの初期化
        /// </summary>
        public void TreeList初期化()
        {
            //this.ManageList.Font = new Font("MS UI Gothic", 14.25f);           
           
            //先頭項目を初期化する
            foreach (CulInfo data in 貯蔵品FormTreeListManager.ColData)
            {
                TreeListViewColumn col = new TreeListViewColumn();

                col.StyleFromParent = false;

                col.ContentType = ColumnContentType.Control;
                col.HeaderText = data.Name;
                col.Width = data.Width;

                //0未満なら自動計算(横幅÷表示数)
                if (data.Width < 0)
                {
                    col.Width = (this.ManageList.Width / 貯蔵品FormTreeListManager.ColData.Length) - 1;
                    data.Width = col.Width;
                }
                col.FormatStyle.HeaderFont = DefaultFont;
                col.FormatStyle.HeaderTextAlign = data.HeaderAlign;
                col.FormatStyle.ContentAlign = data.TextAlign;
                
                this.ManageList.Columns.Add(col);         
                              
            }           
           
        }

        //引数：描画対象、編集が可能かどうか？
        public void TreeList描画(List<貯蔵品TreeInfo> datalist, bool shime)
        {
            if (datalist == null)
            {
                return;
            }

            //初期化
            this.ManageList.Nodes.Clear();

            //今回の描画データを保存しておく
            this.TreeInfoList = datalist;

            int no = 0;

			/////////////////////////////////////////////////////////////////////
			//描画をとめる
			this.ManageList.SuspendUpdate();

            //全データ描画

            // 次期改造
            string category = "";
            TreeListViewNode headerNode = null;
            bool needHeader = false;
            if (this.TreeInfoList.Any(obj => obj.カテゴリ.Length > 0))
            {
                needHeader = true;
            }

            foreach (貯蔵品TreeInfo data in this.TreeInfoList)
            {
                TreeListViewNode node = new TreeListViewNode();


                // 次期改造
                {
                    if (needHeader)
                    {
                        if (category != data.カテゴリ)
                        {
                            headerNode = new TreeListViewNode();
                            //TreeListViewSubItem headerItem = makeSubItem();
                            TreeListViewSubItem headerItem = makeSubItemForCategory();
                            headerItem.Text = data.カテゴリ;
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);
                            headerItem = makeSubItem();
                            headerItem.Text = "";
                            headerNode.SubItems.Add(headerItem);


                            category = data.カテゴリ;
                        }
                        headerNode.Nodes.Add(node);
                    }
                }


                //品名
                TreeListViewSubItem item = makeSubItem();
                item.Text = data.品名;
                node.SubItems.Add(item);

                //繰越
                item = makeSubItem();
                item.Text = data.繰越.ToString();
                node.SubItems.Add(item);

                //受け入れ
                item = makeSubItem();
                item.Text = data.受け入れ.ToString();
                node.SubItems.Add(item);

                //計
                item = makeSubItem();
                item.Text = data.計.ToString();
                node.SubItems.Add(item);

                //消費
                item = makeSubItem();
                item.Text = data.消費.ToString();
                node.SubItems.Add(item);

                //残量
                item = makeSubItem(!shime);
                item.Text = data.残量.ToString();
                //テキストボックス準備================================
                //編集可能ならテキストボックスを準備する
                if (shime == true)
                {
                    TextBox tb = new TextBox();

                    //自分の位置をtag記憶しておく
                    tb.Tag = no;

                    //データを入れる
                    tb.Text = item.Text;

                    //必要なイベント処理
                    tb.TextAlign = HorizontalAlignment.Right;
                    tb.GotFocus += new EventHandler(this.OnVisibleChangeEvent);
                    tb.Leave += new EventHandler(this.OnTextChangeEvent);
                    tb.MaxLength = 5;
                    item.Control = tb;
                }

                //================================================================                
                node.SubItems.Add(item);

                // 2010.05.18 金額表示なしに変更
                ////金額
                //item = makeSubItem();
                //item.Text = ((int)data.金額).ToString();
                //node.SubItems.Add(item);

                ///////////////////////////////////////////
                // 次期改造
                //this.ManageList.Nodes.Add(node);
                if (needHeader)
                {
                    this.ManageList.Nodes.Add(headerNode);
                }
                else
                {
                    this.ManageList.Nodes.Add(node);

                }

                no++;
            }

			//描画を許す
			this.ManageList.ResumeUpdate();
			/////////////////////////////////////////////////////////////////////
        }
        private TreeListViewSubItem makeSubItem()
        {
            return makeSubItem(true);
        }
        private TreeListViewSubItem makeSubItem(bool readOnly)
        {
            bool StyleFromParent = false;
            Color readOnlyBgColor = Color.FromArgb(240, 240, 240); // f0f0f0

            TreeListViewSubItem item = new TreeListViewSubItem();
            item.StyleFromParent = StyleFromParent;
            item.FormatStyle.Font = DefaultFont;
            if (readOnly)
            {
                item.NormalStyle.BackColor = readOnlyBgColor;
            }

            return item;
        }

        private TreeListViewSubItem makeSubItemForCategory(bool readOnly = true)
        {
            bool StyleFromParent = false;
            Color readOnlyBgColor = Color.FromArgb(240, 240, 240); // f0f0f0

            TreeListViewSubItem item = new TreeListViewSubItem();
            item.StyleFromParent = StyleFromParent;
            item.FormatStyle.Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold);

            item.NormalStyle.TextColor = Color.Blue;
            item.HoverStyle.TextColor = Color.Blue;
            item.SelectedStyle.TextColor = Color.Blue;

            if (readOnly)
            {
                item.NormalStyle.BackColor = readOnlyBgColor;
            }

            return item;
        }

        //引数：なし
        public void TreeList再描画()
        {            
            int no = 0;

            ///////////////////////////////////////////////////////////////////////
            ////描画をとめる
            //this.ManageList.SuspendUpdate();

            ////全データ描画
            //foreach (貯蔵品TreeInfo data in this.TreeInfoList)
            //{         

            //    //品名
            //    this.ManageList.Nodes[no].SubItems[0].Text = data.品名;                

            //    //繰越
            //    this.ManageList.Nodes[no].SubItems[1].Text = data.繰越.ToString();

            //    //受け入れ
            //    this.ManageList.Nodes[no].SubItems[2].Text = data.受け入れ.ToString();                

            //    //計                
            //    this.ManageList.Nodes[no].SubItems[3].Text = data.計.ToString();                

            //    //消費                
            //    this.ManageList.Nodes[no].SubItems[4].Text = data.消費.ToString();
                
            //    //残量                
            //    this.ManageList.Nodes[no].SubItems[5].Text = data.残量.ToString();
            //    this.ManageList.Nodes[no].SubItems[5].Control.Text = data.残量.ToString();

            //    // 2010.05.18 金額表示なしに変更
            //    //金額                
            //    //this.ManageList.Nodes[no].SubItems[6].Text = data.金額.ToString();                

            //    no++;
            //}

            ////描画を許す
            //this.ManageList.ResumeUpdate();
            ///////////////////////////////////////////////////////////////////////


            // 次期改造
            if (this.TreeInfoList.Any(obj => obj.カテゴリ.Length > 0))
            {
                string category = "";
                int headerNo = -1;

                foreach (貯蔵品TreeInfo data in this.TreeInfoList)
                {
                    if (category != data.カテゴリ)
                    {
                        category = data.カテゴリ;
                        headerNo++;
                        no = 0;
                    }

                    //品名
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[0].Text = data.品名;

                    //繰越
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[1].Text = data.繰越.ToString();

                    //受け入れ
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[2].Text = data.受け入れ.ToString();

                    //計                
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[3].Text = data.計.ToString();

                    //消費                
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[4].Text = data.消費.ToString();

                    //残量                
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[5].Text = data.残量.ToString();
                    this.ManageList.Nodes[headerNo].Nodes[no].SubItems[5].Control.Text = data.残量.ToString();
             
                    no++;
                }
            }
            else
            {
                foreach (貯蔵品TreeInfo data in this.TreeInfoList)
                {
                    //品名
                    this.ManageList.Nodes[no].SubItems[0].Text = data.品名;

                    //繰越
                    this.ManageList.Nodes[no].SubItems[1].Text = data.繰越.ToString();

                    //受け入れ
                    this.ManageList.Nodes[no].SubItems[2].Text = data.受け入れ.ToString();

                    //計                
                    this.ManageList.Nodes[no].SubItems[3].Text = data.計.ToString();

                    //消費                
                    this.ManageList.Nodes[no].SubItems[4].Text = data.消費.ToString();

                    //残量                
                    this.ManageList.Nodes[no].SubItems[5].Text = data.残量.ToString();
                    this.ManageList.Nodes[no].SubItems[5].Control.Text = data.残量.ToString();


                    no++;
                }
            }


        }


        public void ExpandAll()
        {
            this.ManageList.ExpandAll();
        }


        //private********************************
        /// <summary>
        /// テキストボックス入力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTextChangeEvent(object sender, EventArgs e)
        {
            //一応チェック
            if (sender is TextBox)
            {
               

                TextBox tb = (TextBox)sender;
                int no = (int)tb.Tag;

                int max = this.TreeInfoList[no].繰越 + this.TreeInfoList[no].受け入れ;

                try
                {
                    int data = Convert.ToInt32(tb.Text);

                    //マイナスの値は不正
                    if(data < 0)
                    {
                        //入力を元に戻す
                        tb.Text = this.TreeInfoList[no].残量.ToString();
                        MessageBox.Show("残量は正の値を入力して下さい", "貯蔵品編集", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (data > max)
                    {
                        //入力を元に戻す
                        tb.Text = this.TreeInfoList[no].残量.ToString();
                        MessageBox.Show("残量は（繰越＋受け入れ）の範囲の値を入力して下さい", "貯蔵品編集", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 消費 = 繰越 + 受け入れ - 残量
                    this.TreeInfoList[no].消費 = max - data;

                    // 2010.05.18 金額表示なしに変更
                    //// 金額の計算（先入先出）
                    //if (this.TreeInfoList[no].残量s != null)
                    //{
                    //    decimal 金額 = 0;
                    //    int work数量 = this.TreeInfoList[no].消費;
                    //    for (int i = 0; i < this.TreeInfoList[no].残量s.Count; i++)
                    //    {
                    //        NBaseData.BLC.貯蔵品編集RowData残量 残 = this.TreeInfoList[no].残量s[i];
                    //        if (残.残量 > work数量)
                    //        {
                    //            金額 = (残.残量 - work数量) * 残.金額;
                    //            for (int j = i + 1; j < this.TreeInfoList[no].残量s.Count; j++)
                    //            {
                    //                残 = this.TreeInfoList[no].残量s[j];
                    //                金額 += 残.残量 * 残.金額;
                    //            }
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            work数量 -= 残.残量;
                    //        }
                    //    }
                    //    this.TreeInfoList[no].金額 = 金額;
                    //}
                    this.TreeInfoList[no].残量 = data;
                    this.TreeInfoList[no].OdChozoShousaiData.Count = data;
                }
                catch (Exception)
                {
                    MessageBox.Show("不正な入力です", "貯蔵品編集", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tb.Leave -= new EventHandler(this.OnTextChangeEvent);

                // 2010.05.18 金額表示なしに変更
                //ここで金額の計算をする。
                //BLC.貯蔵品金額計算処理.貯蔵品金額計算総括(this.TreeInfoList, this.Year, this.Month); 
                this.TreeList再描画();
            
                tb.Leave += new EventHandler(this.OnTextChangeEvent);
            }
        }

        /// <summary>
        /// テキストボックス表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnVisibleChangeEvent(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                textBox.SelectAll();

            }
        }


        //メンバ変数=================================
        /// <summary>
        /// 現在表示中のデータリスト
        /// </summary>
        private List<貯蔵品TreeInfo> TreeInfoList = new List<貯蔵品TreeInfo>();
        public List<貯蔵品TreeInfo> TREE_INFO_LIST
        {
            get
            {
                return this.TreeInfoList;
            }           
        }
    

     
        
        
        /// <summary>
        /// 自分の管理リスト
        /// </summary>
        private TreeListView ManageList;

        public int Year;
        public int Month;

        //---------------------------------------------------
        //定義

        // 2010.05.18 金額表示なしに変更
        //カラムタイトル情報                        ﾀｲﾄﾙ        横幅
        //static CulInfo[] ColData = {                        //(0未満は自動計算)
        //                               new CulInfo( "品名",     261),
        //                               new CulInfo( "繰越",     57),
        //                               new CulInfo( "受け入れ", 59),
        //                               new CulInfo( "計",       61),
        //                               new CulInfo( "消費",     61),
        //                               new CulInfo( "残量",     70),
        //                               new CulInfo( "金額",     105),

        //                           };
        static CulInfo[] ColData = {                        //(0未満は自動計算)
                                       new CulInfo( "品名",     400, ContentAlignment.MiddleLeft,  HorizontalAlignment.Left),
                                       new CulInfo( "繰越",     75,  ContentAlignment.MiddleLeft, HorizontalAlignment.Right),
                                       new CulInfo( "受け入れ", 75,  ContentAlignment.MiddleLeft, HorizontalAlignment.Right),
                                       new CulInfo( "計",       75,  ContentAlignment.MiddleLeft, HorizontalAlignment.Right),
                                       new CulInfo( "消費",     75,  ContentAlignment.MiddleLeft, HorizontalAlignment.Right),
                                       new CulInfo( "残量",     90,  ContentAlignment.MiddleLeft, HorizontalAlignment.Right),
                                   };

        ////////////////////////////////////////////////
        //内包クラス
        /// <summary>
        /// カラミュ名情報管理
        /// </summary>
        private class CulInfo
        {
            public CulInfo(string name, int width, ContentAlignment headerAlign, HorizontalAlignment textAlign)
            {
                this.Name = name;
                this.Width = width;
                this.HeaderAlign = headerAlign;
                this.TextAlign = textAlign;
            }

            public string Name;
            public int Width;
            public ContentAlignment HeaderAlign;
            public HorizontalAlignment TextAlign;
        }       
        
        
    }
}

