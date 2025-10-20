using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yojitsu.util;
using Yojitsu.DA;
using NBaseData.DAC;

namespace Yojitsu
{
    public partial class 経常収支実績出力Form : Form
    {
        
        
        
        public 経常収支実績出力Form(List<BgYosanHead> yosanHeads)
        {
            this.yosanHeads = yosanHeads;
            
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "経常収支実績出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
		}

		#region 初期化する関数たち
		/// <summary>
		/// フォーム初期化総括
		/// </summary>
        private void Init()
        {
            // 年度
            InitComboBox年度();
            // 期間
            InitComboBox期間();
            // 予算種別
            InitComboBox予算種別();
            // 単位
            InitComboBox単位();
        }

		/// <summary>
		/// 年度コンボボックスの初期化
		/// </summary>
        private void InitComboBox年度()
        {
            comboBox年度.Items.Clear();

            
			//選択年を追加する。
            foreach (BgYosanHead h in yosanHeads)
            {
				if (!this.YearList.Contains(h.Year))
                {
                    comboBox年度.Items.Add(h.Year);
                    this.YearList.Add(h.Year);
                }
            }

            if (comboBox年度.Items.Count > 0)
            {
                comboBox年度.SelectedIndex = 0;
            }
        }

		/// <summary>
		/// 期間選択コンボボックス初期化
		/// </summary>
        private void InitComboBox期間()
        {
            foreach (string kikan in Constants.KIKAN)
            {
                comboBox期間.Items.Add(kikan);
            }

            comboBox期間.SelectedIndex = 0;
        }

        /// <summary>
        /// 予算種別コンボボックスの初期化
        /// </summary>
        private void InitComboBox予算種別()
        {
            comboBox予算種別.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text))
                {
                    if (!comboBox予算種別.Items.Contains(BgYosanSbt.ToName(h.YosanSbtID)))
                    {
                        comboBox予算種別.Items.Add(BgYosanSbt.ToName(h.YosanSbtID));
                        YosanSbtIdList.Add(h.YosanSbtID);
                    }
                }
            }

            if (comboBox予算種別.Items.Count > 0)
            {
                comboBox予算種別.SelectedIndex = 0;
            }
        }

		/// <summary>
		/// 単位コンボボックス初期化
		/// </summary>
        private void InitComboBox単位()
        {
            foreach (string kikan in Constants.TANI)
            {
                comboBox単位.Items.Add(kikan);
            }

            comboBox単位.SelectedIndex = 0;
		}
		#endregion

		/// <summary>
		/// 保存ファイル選択
		/// 引数：選択ファイル名
		/// 返り値：選ばれたか？
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		private bool SelectSaveFile(ref string filename)
		{
			this.saveFileDialog1.OverwritePrompt = true;    //上書き確認

            this.saveFileDialog1.FileName = "経常収支実績.xls";
            this.saveFileDialog1.Filter =
				"xlsファイル(*.xls)|*.xls" +
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


		/// <summary>
		/// 帳票出力するとき
		/// </summary>
		/// <returns></returns>
		private bool 帳票出力総括()
		{
			string filename = "";
					

			int yearindex = this.comboBox年度.SelectedIndex;
			int preindex = this.comboBox期間.SelectedIndex;
			int sbtindex = this.comboBox予算種別.SelectedIndex;
			int unitindex = this.comboBox単位.SelectedIndex;

			//選択されていないものがあった。
			if (yearindex < 0 || preindex < 0 || sbtindex < 0 ||
				unitindex < 0)
			{
				MessageBox.Show("検索条件が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			

			//選択年
			int year = this.YearList[yearindex];
			期間ValueData prival = 経常収支実績出力Form.PriodData[preindex];

			//選択期間
			int sy = year + prival.StartYearOffset;
			int sm = prival.StartMonth;
			int ey = year + prival.EndYearOffset;
			int em = prival.EndMonth;
            

			//種別
			int sbtid = this.YosanSbtIdList[sbtindex];
			
			//単位
			decimal rate = 経常収支実績出力Form.TaniComboRate[unitindex];
			string unit = DA.Constants.TANI[unitindex];

			//関連BgYosanHead
			BgYosanHead head = this.GetSelectYosanHeadData(year, sbtid);

			if (head == null)
			{
				MessageBox.Show("予算データがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}


			//-------------------------------------------------------------
			//保存ファイルの選択
			bool ret = this.SelectSaveFile(ref filename);

			if (ret == false)
			{
				return false;
			}
			//-------------------------------------------------------------

			this.Cursor = Cursors.WaitCursor;

			//管理表を出力する
			//ret = writer.経常収支管理表作成(
			//	filename, sy, sm, ey, em, unit, rate, head);
			string message = "";
			try
			{
				byte[] excelData = null;

                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
				    {
					    excelData = serviceClient.BLC_Excel経常収支管理表_取得(
						    sy, sm, ey, em, unit, rate, head, NBaseCommon.Common.LoginUser);
				    }
                }, "経常収支実績を出力中です...");
                progressDialog.ShowDialog();

				if (excelData == null)
				{
					MessageBox.Show("経常収支実績の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "経常収支", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Cursor = System.Windows.Forms.Cursors.Default;
					return false;
				}

				//バイナリをファイルに
				System.IO.FileStream filest = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
				filest.Write(excelData, 0, excelData.Length);
				filest.Close();

				this.Cursor = System.Windows.Forms.Cursors.Default;
			}
			catch (Exception ex)
			{
				//カーソルを通常に戻す
				this.Cursor = System.Windows.Forms.Cursors.Default;
				message = ex.Message;
				ret = false;
			}

			
			if (ret == false)
			{
				MessageBox.Show("経常収支実績の出力に失敗しました。\n (Err:" + message + ")", "経常収支", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			string smes = "「" + filename + "」に出力しました。";
			MessageBox.Show( smes, "経常収支", MessageBoxButtons.OK, MessageBoxIcon.Information);

			return true;
		}

		/// <summary>
		/// 現在選択されてる状態から関連するBgYosanHeadを取得する
		/// 引数：選択年、種別
		/// 返り値：関連データ
		/// </summary>
		/// <returns></returns>
		private BgYosanHead GetSelectYosanHeadData(int year, int sbtid)
		{
			BgYosanHead ans = null;

			List<BgYosanHead> revlist = new List<BgYosanHead>();
			revlist.Clear();

			//データ検索
			foreach (BgYosanHead data in this.yosanHeads)
			{
				//年月と種別IDに一致するものを選択する
				if (data.Year == year &&
					data.YosanSbtID == sbtid)
				{
					revlist.Add(data);
				}
			}

			int rev = -1;

			//最大のRevを探す
			foreach (BgYosanHead data in revlist)
			{
				if (rev < data.Revision)
				{
					rev = data.Revision;
					ans = data;
				}				
			}

			return ans;
		}
		

		//******************************************************************
		//メンバ変数
		private List<BgYosanHead> yosanHeads;

		/// <summary>
		/// 予算種別リストたち
		/// </summary>
		private List<int> YosanSbtIdList = new List<int>();

		/// <summary>
		/// 今回選択できる年
		/// </summary>
		List<int> YearList = new List<int>();
		//-------------------------------------------------------------
		//定義

		/// <summary>
		/// Constants.TANIに対応する数字Rate
		/// </summary>
		private static readonly decimal[] TaniComboRate = {
												   1000m,
												   1000000m,

											   };





		/// <summary>
		/// Constants.KIKANに対応する数値データ
		/// </summary>
		private static readonly 期間ValueData[] PriodData = {
															  new 期間ValueData(0, 4, 1, 3),	//年度

															  new 期間ValueData(0, 4, 0, 4),	//4月
															  new 期間ValueData(0, 5, 0, 5),	//5月
															  new 期間ValueData(0, 6, 0, 6),	//6月
															  new 期間ValueData(0, 7, 0, 7),	//7月
															  new 期間ValueData(0, 8, 0, 8),	//8月
															  new 期間ValueData(0, 9, 0, 9),	//9月
															  new 期間ValueData(0, 10, 0, 10),	//10月
															  new 期間ValueData(0, 11, 0, 11),	//11月
															  new 期間ValueData(0, 12, 0, 12),	//12月
															  new 期間ValueData(1, 1, 1, 1),	//1月
															  new 期間ValueData(1, 2, 1, 2),	//2月
															  new 期間ValueData(1, 3, 1, 3),	//3月

															  new 期間ValueData(0, 4, 0, 6),	//四半期 4～6
															  new 期間ValueData(0, 7, 0, 9),	//四半期 7～9
															  new 期間ValueData(0, 10, 0, 12),	//四半期 10～12
															  new 期間ValueData(1, 1, 1, 3),	//四半期 1～3

															  new 期間ValueData(0, 4, 0, 9),	//上期
															  new 期間ValueData(0, 10, 1, 3),	//下期

														  };

		//クラス---------------------------------------------------------------------------

		private class 期間ValueData
		{
			/// <summary>
			/// 引数：開始年オフセット、開始月、終了年オフセット、終了月
			/// </summary>
			/// <param name="ofsy"></param>
			/// <param name="sm"></param>
			/// <param name="ofey"></param>
			/// <param name="em"></param>
			public 期間ValueData(int ofsy, int sm, int ofey, int em)
			{
				this.StartYearOffset = ofsy;
				this.StartMonth = sm;
				
				this.EndYearOffset = ofey;
				this.EndMonth = em;
			}

			public int StartYearOffset = 0;
			public int StartMonth = 0;

			public int EndYearOffset = 0;
			public int EndMonth = 0;

		}


		////////////////////////////////////////////////////////////
        /// <summary>
        /// 出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
			this.帳票出力総括();
            //Dispose();
        }

        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
        }

		
		
    }
}

