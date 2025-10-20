using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NBaseUtil;
using NBaseData.DAC;
using NBaseData.DS;
using System.IO;

namespace Senin
{
    public partial class 配乗計画出力Form : Form
    {
        private int PlanType;
        private int PeriodYear = 0;

        public 配乗計画出力Form(int planType, int period)
        {
            InitializeComponent();

            PeriodYear = period;

            // この配乗計画のタイプ
            PlanType = planType;
        }

        private void 配乗計画出力_Load(object sender, EventArgs e)
        {
            InitComboBox年();
            InitComboBox月();

        }

        /// <summary>
        /// 年コンボボックス作成
        /// </summary>
        #region private void InitComboBox年()
        private void InitComboBox年()
        {
            int thisYear = DateTime.Now.Year;

            for (int i = 1; i < PeriodYear; i++)
            {
                comboBox年.Items.Add(thisYear - PeriodYear + i);
            }
            for (int i = 0; i < PeriodYear; i++)
            {
                comboBox年.Items.Add(thisYear + i);
            }

            comboBox年.SelectedItem = thisYear;
        }
        #endregion

        /// <summary>
        /// 月コンボボックス作成
        /// </summary>
        #region private void InitComboBox月()
        private void InitComboBox月()
        {
            int thisMonth = DateTime.Now.Month;

            for (int i = 0; i < 12; i++)
            {

                comboBox月.Items.Add(i + 1);

            }

            comboBox月.SelectedItem = thisMonth;
        }
        #endregion

        /// <summary>
        /// 年月コンボボックスから年月+ついたち 0:00の　DateTimeを取得
        /// </summary>
        /// <returns></returns>
        private DateTime GetDateTimeCombobox()
        {
            //年月コンボボックスから取得
            int year = 0;
            int month = 0;

            if (comboBox年.SelectedItem is int)
            {
                year = (int)comboBox年.SelectedItem;
            }
            if (comboBox月.SelectedItem is int)
            {
                month = (int)comboBox月.SelectedItem;
            }

            //年月作成
            DateTime dt = new DateTime(year, month, 1);
            dt = NBaseUtil.DateTimeUtils.ToFrom(dt);

            return dt;
        }

        /// <summary>
        /// 年月変更したときRivisionListを更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SearchRevision();
        }

        /// <summary>
        /// 出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            DateTime dt = GetDateTimeCombobox();

            //計画か実績でメッセージ、ファイル名を分ける
            string filename = "";
            string title = "";

            List<MsPlanType> planTypeList = MsPlanType.GetRecords();
            string planTypeStr = planTypeList[PlanType].Name;


            if (radioButton計画.Checked)
            {
                title = "乗下船計画表(" + planTypeStr + ")";

                //
                //RevNoあれば取得したい
                //
                int revno = 0;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    List<SiCardPlanHead> headlist = serviceClient.SiCardPlanHead_GetRecordsByYearMonth(NBaseCommon.Common.LoginUser, dt, PlanType);
                    if (headlist.Count > 0)
                    {
                        revno = headlist.Max(obj => obj.RevNo);
                    }
                }

                filename = title + "_" + dt.ToString("yyyyMM") + "_" + revno.ToString("D") + ".xlsx";
            }
            else
            {
                title = "乗下船実績表(" + planTypeStr + ")";

                filename = title + "_" + dt.ToString("yyyyMM") + ".xlsx";
            }

            FileUtils.SetDesktopFolder(saveFileDialog1);

            saveFileDialog1.FileName = filename;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            if (PlanType == MsPlanType.PlanTypeOneMonth)
                                result = serviceClient.BLC_Excel_配乗計画表出力(NBaseCommon.Common.LoginUser, dt, radioButton計画.Checked);
                            else
                                result = serviceClient.BLC_Excel_配乗計画表内航乗下船出力(NBaseCommon.Common.LoginUser, dt, radioButton計画.Checked);

                        }
                    }, title + "を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show(title + "の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "配乗計画", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);

                    filest.Close();



                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(title + "の出力に失敗しました。\n (Err:" + ex.Message + ")", "配乗計画", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
