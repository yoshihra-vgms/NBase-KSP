using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NBaseData.DAC;

namespace NBaseMaster.Kensa
{
    public partial class 検査管理Form : Form
    {
        public 検査管理Form()
        {
            InitializeComponent();
        }
        
        //メンバ関数=========================================================

        /// <summary>
        /// フォームの初期化        
        /// </summary>
        private void Form初期化()
        {
            this.DataList.Clear();
            this.DataList表示(this.DataList);
        }

        /// <summary>
        /// 指定リストを表示する
        /// 引数：表示したいリスト
        /// 返り値：なし
        /// </summary>
        /// <param name="datalist"></param>
        private void DataList表示(List<MsKensa> datalist)
        {
            DataTable dt = new DataTable();

            //ヘッダー作成
            foreach (string s in 検査管理Form.HeaderName)
            {
                dt.Columns.Add(new DataColumn(s, typeof(string)));
            }

            //表示したいものを全てADDする
            foreach (MsKensa ken in datalist)
            {
                DataRow row = dt.NewRow();

                row[検査管理Form.Col検査名] = ken.KensaName;
                row[検査管理Form.Col間隔] = ken.Kankaku;

                dt.Rows.Add(row);
            }

            //グリッドへセット
            this.dataGridView1.DataSource = dt;

            // カラム幅の設定
            this.dataGridView1.Columns[検査管理Form.Col検査名].Width = 検査管理Form.Col検査名Width;
            //this.dataGridView1.Columns[検査管理Form.Col間隔].Width = 65;
        }

        /// <summary>
        /// データ検索
        /// 引数：検索検査名
        /// 返り値：発見したデータ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private List<MsKensa> 検査データ検索(string kname)
        {
            List<MsKensa> anslist = new List<MsKensa>();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //検索条件が空 = 全部
                if (kname.Length <= 0)
                {
                    anslist = serviceClient.MsKensa_GetRecords(NBaseCommon.Common.LoginUser);
                }
                //あるなら検索条件でデータを検索する
                else
                {
                    anslist = serviceClient.MsKensa_GetRecordsBy検査名(NBaseCommon.Common.LoginUser, kname);

                }
            }

            return anslist;
        }

        private void 検査検索総括()
        {
            //検索条件をもとにデータを検索する
            string s = this.SearchKensaText.Text;
            this.DataList = this.検査データ検索(s);

            //検索データの表示
            this.DataList表示(this.DataList);
        }

        //メンバ変数========================================================
        /// <summary>
        /// 表示検査リスト
        /// </summary>
        private List<MsKensa> DataList = new List<MsKensa>();

        //--------------------------------------
        private static readonly string Col検査名 = "検査名";
        private static readonly int Col検査名Width = 200;

        private static readonly string Col間隔 = "間隔";

        //表示ヘッダー名
        private static readonly string[] HeaderName = {
                                                         検査管理Form.Col検査名,
                                                         検査管理Form.Col間隔,
                                                      };
     
        //////////////////////////////////////////////////////////////////////
        //各種イベント処理---------------------------------------------------
        //フォームが読み込まれたとき
        private void 検査管理Form_Load(object sender, EventArgs e)
        {
            this.Form初期化();
        }

        //検索ボタンが押されたとき
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.検査検索総括();
        }

        //検索条件クリアボタンが押された時
        private void ClearButton_Click(object sender, EventArgs e)
        {
            //検索テキストを初期化する
            this.SearchKensaText.Text = "";
        }

        //新規追加ボタンが押されたとき
        private void AddButton_Click(object sender, EventArgs e)
        {
            //詳細画面を開く
            検査管理詳細Form form = new 検査管理詳細Form(null);
            form.ShowDialog();

            //再描画をする
            this.検査検索総括();
        }

        //閉じるボタンが押された時
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //データがダブルクリックされたとき
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*string row = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            foreach (NBaseData.DAC.MsLo lo in MsLoList)
            {
                if (lo.MsLoID == row)
                {
                    潤滑油詳細Form form = new　潤滑油詳細Form(lo);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        再表示();
                    }
                }
            }*/

            //選択行を取得する
            int index = dataGridView1.SelectedRows[0].Cells[0].RowIndex;
            
            //範囲外
            if (index >= this.DataList.Count || index < 0)
            {
                return;
            }

            //詳細画面を開く
            検査管理詳細Form form = new 検査管理詳細Form(this.DataList[index]);
            form.ShowDialog();

            //再描画をする
            this.検査検索総括();
        }
    }
}
