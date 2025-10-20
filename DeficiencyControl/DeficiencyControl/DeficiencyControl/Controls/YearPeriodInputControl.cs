using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Controls
{
    /// <summary>
    /// 年度設定入力コントロール
    /// </summary>
    public partial class YearPeriodInputControl : BaseControl
    {
        public YearPeriodInputControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// これの初期化データ
        /// </summary>
        public class InitData
        {
            public int StartYear = 0;
            public int EndYear = 0;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            this.ErList = new List<Control>();

            try
            {
                //入力チェック
                Control[] ckvec = {
                                      this.textBoxStartYear,
                                      this.textBoxEndYear,                                      

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。
                {
                    int[] nv = { 0, 0 };


                    //年度のチェック
                    int i = 0;
                    foreach (Control c in ckvec)
                    {
                        try
                        {
                            int n = Convert.ToInt32(c.Text);

                            //どう見ても変な年度数は除外する
                            if (n < 1900 || n > 10000)
                            {
                                throw new Exception("Years Error n=" + n.ToString());
                            }

                            nv[i] = n;
                        }
                        catch
                        {
                            this.ErList.Add(c);
                        }

                        i++;
                    }

                    //最初と最後を意識する
                    if (nv[0] > nv[1])
                    {
                        this.ErList.Add(this.textBoxStartYear);
                        this.ErList.Add(this.textBoxEndYear);
                    }
                }

                //エラーがないなら終わり
                if (this.ErList.Count <= 0)
                {
                    return true;
                }

                throw new Exception("InputErrorException");
            }
            catch (Exception e)
            {
                //エラーの表示を行う
                if (chcol == true)
                {
                    this.DispError();
                }
            }
            
            return false;
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        /// <param name="inputdata">InitData null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            InitData idata = inputdata as InitData;
            if (idata == null)
            {
                return true;
            }

            this.textBoxStartYear.Text = idata.StartYear.ToString();
            this.textBoxEndYear.Text = idata.EndYear.ToString();

            return true;
        }



        /// <summary>
        /// 開始年度取得
        /// </summary>
        /// <returns></returns>
        public int GetStartYear()
        {
            int ans = 0;

            try
            {
                ans = Convert.ToInt32(this.textBoxStartYear.Text);
            }
            catch
            {
                return -1;
            }

            return ans;
        }

        /// <summary>
        /// 終了年度取得
        /// </summary>
        /// <returns></returns>
        public int GetEndYear()
        {
            int ans = 0;

            try
            {
                ans = Convert.ToInt32(this.textBoxEndYear.Text);
            }
            catch
            {
                return -1;
            }

            return ans;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YearPeriodInputControl_Load(object sender, EventArgs e)
        {

        }
    }
}
