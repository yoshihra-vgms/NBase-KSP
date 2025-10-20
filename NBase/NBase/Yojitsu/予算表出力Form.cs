using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yojitsu.DA;
using NBaseData.DAC;
using Yojitsu.util;
using Microsoft.Office.Interop.Excel;
using NBaseUtil;

namespace Yojitsu
{
    public partial class 予算表出力Form : Form
    {
        private List<BgYosanHead> yosanHeads;

        /// <summary>
        /// Constants.TANIに対応する数字Rate
        /// </summary>
        private static readonly decimal[] TaniComboRate = {
												   1000m,
												   1000000m,

											   };


        public 予算表出力Form(List<BgYosanHead> yosanHeads)
        {
            this.yosanHeads = yosanHeads;

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算表出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }

        #region 初期化
        private void Init()
        {
            // 年度
            InitComboBox年度();
            // 船
            //InitComboBox船();
            // 予算種別
            InitComboBox予算種別();
            // リビジョン
            InitComboBoxリビジョン();
            // 単位
            InitComboBox単位();

            comboBox範囲.SelectedIndex = 2; // デフォルトは、向こう３年
        }


        private void InitComboBox年度()
        {
            comboBox年度.Items.Clear();

            List<int> years = new List<int>();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (!years.Contains(h.Year))
                {
                    comboBox年度.Items.Add(h.Year);
                    years.Add(h.Year);
                }
            }

            if (comboBox年度.Items.Count > 0)
            {
                comboBox年度.SelectedIndex = 0;
            }
        }


        //private void InitComboBox船()
        //{
        //    comboBox船.Items.Clear();

        //    comboBox船.Items.Add("全社");
        //    comboBox船.Items.Add("--------------------");

        //    foreach (MsVesselType v in DbAccessorFactory.FACTORY.MsVesselType_GetRecords(NBaseCommon.Common.LoginUser))
        //    {
        //        comboBox船.Items.Add(v);
        //    }

        //    comboBox船.Items.Add("--------------------");

        //    foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser))
        //    {
        //        comboBox船.Items.Add(v);
        //    }

        //    comboBox船.SelectedIndex = 0;
        //}

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
                    }
                }
            }

            if (comboBox予算種別.Items.Count > 0)
            {
                comboBox予算種別.SelectedIndex = 0;
            }
        }


        private void InitComboBoxリビジョン()
        {
            comboBoxリビジョン.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text)
                {
                    string revStr = h.Revision.ToString();

                    if (!h.IsFixed())
                    {
                        revStr += " (未 Fix)";
                    }
                    else
                    {
                        revStr += " (" + h.FixDate.ToString("yyyy/MM/dd") + " Fix)";
                    }

                    comboBoxリビジョン.Items.Add(revStr);
                }
            }


            if (comboBoxリビジョン.Items.Count > 0)
            {
                comboBoxリビジョン.SelectedIndex = 0;
            }
        }

        private void InitComboBox単位()
        {
            foreach (string kikan in Yojitsu.DA.Constants.TANI)
            {
                comboBox単位.Items.Add(kikan);
            }

            comboBox単位.SelectedIndex = 0;
        }
        #endregion

        private BgYosanHead Get選択BgYosanHead()
        {
            int year = Int32.Parse(comboBox年度.Text);

            //予算頭選択
            foreach (BgYosanHead h in this.yosanHeads)
            {
                if (h.Year == year &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text &&
                    h.Revision == Int32.Parse(comboBoxリビジョン.Text.Split(' ')[0]))
                {
                    return h;
                }
            }

            return null;
        }

        private bool 予算表出力(string filename)
        {
            //List<MsVessel> vessels = DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);

            //予算頭選択             
            BgYosanHead selectedYosanHead = this.Get選択BgYosanHead();

            // 年数
            int years = comboBox範囲.SelectedIndex + 1;

            // 単位
            decimal unit = 1.0m;
            unit = 予算表出力Form.TaniComboRate[this.comboBox単位.SelectedIndex];

            string message = "";
            bool ret = true;
            try
            {
                byte[] excelData = null;

                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        excelData = serviceClient.BLC_Excel予算表_取得(NBaseCommon.Common.LoginUser, selectedYosanHead, years, unit);
                    }
                    if (excelData != null)
                    {
                        //バイナリをファイルに
                        System.IO.FileStream filest = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                        filest.Write(excelData, 0, excelData.Length);
                        filest.Close();

                        // 集計シートの処理
                        try
                        {
                            _集計シート処理(filename, years);
                        }
                        catch
                        {
                        }
                    }
                }, "予算表を作成中です...");
                progressDialog.ShowDialog();

                if (excelData == null)
                {
                    MessageBox.Show("予算表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "予算表出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return false;
                }

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
                MessageBox.Show("予算表の出力に失敗しました。\n (Err:" + message + ")", "予算表出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            string smes = "「" + filename + "」に出力しました。";
            MessageBox.Show(smes, "予算表", MessageBoxButtons.OK, MessageBoxIcon.Information);


            return true;
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.予算表出力(saveFileDialog1.FileName);

                Dispose();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
            InitComboBoxリビジョン();
        }

        private void comboBox予算種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComboBoxリビジョン();
        }

        private void _集計シート処理(string filePath, int years)
        {
            // Excel Object
            ApplicationClass ExcelApplication = new ApplicationClass();
            Workbook thisBook = null;
            Worksheet thisSheet = null;

            try
            {
                List<MsVessel> vessels = DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                
                
                // Excelオープン
                thisBook = ExcelApplication.Workbooks.Open(filePath,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                thisSheet = (Worksheet)thisBook.Worksheets.get_Item(1);


                // 予算データ
                int 集計SheetX_Base = 4;// D列
                int 集計SheetY_Base = 7;// 7行
                int 船SheetX_Base = 3;// D列：0オリジン
                int 船SheetY_Base = 13;// 14行：0オリジン

                for (int colIdx = 0; colIdx < (15 + years); colIdx++)
                {
                    if (colIdx == 6
                        || colIdx == 13
                        || colIdx == 14)
                        continue;

                    for (int rowIdx = 0; rowIdx < 39; rowIdx++)
                    {
                        if (rowIdx == 4
                            || rowIdx == 9
                            || rowIdx == 22
                            || rowIdx == 25
                            || rowIdx == 26
                            || rowIdx == 30
                            || rowIdx == 34
                            || rowIdx == 35
                            || rowIdx == 38)
                            continue;

                        StringBuilder sb = new StringBuilder();
                        foreach (MsVessel vessel in vessels)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append("+");
                            }
                            else if (sb.Length == 0)
                            {
                                sb.Append("=");
                            }
                            sb.Append("'" + vessel.VesselName + "'!" + ExcelUtils.ToCellName(船SheetX_Base + colIdx, 船SheetY_Base + rowIdx));
                        }
                        thisSheet.Cells[集計SheetY_Base + rowIdx, 集計SheetX_Base + colIdx] = sb.ToString(); 
                    }
                }

                // 外航
                int 船SheetX_外航Base = 17;// R列：0オリジン
                for (int colIdx = 0; colIdx <= years; colIdx++)
                {
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    StringBuilder sb3 = new StringBuilder();
                    foreach (MsVessel vessel in vessels)
                    {
                        if (vessel.MsVesselTypeID == Yojitsu.DA.Constants.MS_VESSEL_TYPE_ID_外航)
                        {
                            if (sb1.Length > 0)
                            {
                                sb1.Append("+");
                                sb2.Append("+");
                                sb3.Append("+");
                            }
                            else if (sb1.Length == 0)
                            {
                                sb1.Append("=");
                                sb2.Append("=");
                                sb3.Append("=");
                            }
                            sb1.Append("'" + vessel.VesselName + "'!" + ExcelUtils.ToCellName(船SheetX_外航Base + colIdx, 17)); // R18
                            sb2.Append("'" + vessel.VesselName + "'!" + ExcelUtils.ToCellName(船SheetX_外航Base + colIdx, 48)); // R49
                            sb3.Append("'" + vessel.VesselName + "'!" + ExcelUtils.ToCellName(船SheetX_外航Base + colIdx, 42)); // R43
                        }
                    }
                    thisSheet.Cells[1, 18 + colIdx] = sb1.ToString();
                    thisSheet.Cells[2, 18 + colIdx] = sb2.ToString();
                    thisSheet.Cells[3, 18 + colIdx] = sb3.ToString(); 
                }

                // Excel保存
                thisBook.Save();
                thisBook.Close(true, Type.Missing, Type.Missing);
                ExcelApplication.Quit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("_集計シート処理:" + ex.Message);
            }
            finally
            {
                ////開放
                releaseObj(thisSheet);
                releaseObj(thisBook);
                releaseObj(ExcelApplication);
            }
        }

        /// <summary>
        /// COMオブジェクトのリリース
        /// </summary>
        /// <param name="ExcelObject"></param>
        public void releaseObj(object ExcelObject)
        {
            try
            {
                if (ExcelObject != null || System.Runtime.InteropServices.Marshal.IsComObject(ExcelObject))
                {
                    int i;
                    do
                    {
                        i = System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelObject);
                    } while (i > 0);

                }
            }
            finally
            {
                ExcelObject = null;
                GC.Collect();
            }
        }
    }
}
