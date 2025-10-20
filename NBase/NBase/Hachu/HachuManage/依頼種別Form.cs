using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Common;

namespace Hachu.HachuManage
{
    public partial class 依頼種別Form : Form
    {
        public enum FUNCTION_TYPE { 手配, 見積, 発注, 振替取立 }

        /// <summary>
        /// 
        /// </summary>
        private int functionType;

        /// <summary>
        /// 選択されている船
        /// </summary>
        public MsVessel SelectedVessel;

        /// <summary>
        /// 選択されている手配依頼種別
        /// </summary>
        public MsThiIraiSbt SelectedThiIraiSbt;

        /// <summary>
        /// 選択されている手配依頼詳細種別
        /// </summary>
        public MsThiIraiShousai SelectedThiIraiShousai;

        /// <summary>
        /// 選択されている見積もり有無
        /// </summary>
        public int SelectedMitsumoriUmu;


        private List<MsVessel> vessels = null;
        private List<MsThiIraiSbt> 手配依頼種別s = null;
        private List<MsThiIraiShousai> 手配依頼種別詳細s = null;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="functionType">依頼種別Form.FUNCTION_TYPE</param>
        /// <param name="vsl">発注状況一覧で選択されている船　選択なしならnull 2021/08/04 m.yoshihara</param>
        #region public 依頼種別Form(int functionType, MsVessel vsl)
        public 依頼種別Form(int functionType, MsVessel vsl)
        {
            this.functionType = functionType;
            SelectedVessel = vsl;//=null→引数で指定されたvesselを入れる 2021/08/04 m.yoshihara
            SelectedThiIraiSbt = null;
            SelectedThiIraiShousai = null;
            SelectedMitsumoriUmu = -1;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //vessels = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                //vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                List<MsVessel> wklist = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                vessels = new List<MsVessel>();//m.yoshihara 2017/05/16 miho
                foreach (MsVessel v in wklist)
                {
                    if (v.HachuEnabled == 1)
                    {
                        vessels.Add(v);
                    }
                }

                手配依頼種別s = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                手配依頼種別詳細s = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 手配依頼種別Form_Load(object sender, EventArgs e)
        private void 手配依頼種別Form_Load(object sender, EventArgs e)
        {
            string titleName = "";
            string messageStr = "";
            if (functionType == (int)FUNCTION_TYPE.手配)
            {
                titleName = "新規手配依頼";
                messageStr = "手配依頼を作成します。　船、種別を選択してください。";
            }
            else if (functionType == (int)FUNCTION_TYPE.見積)
            {
                titleName = "新規見積依頼";
                messageStr = "見積依頼を作成します。　船、種別を選択してください。";
            }
            else if (functionType == (int)FUNCTION_TYPE.発注)
            {
                titleName = "新規発注";
                messageStr = "発注を作成します。　船、種別を選択してください。";
            }
            else if (functionType == (int)FUNCTION_TYPE.振替取立)
            {
                titleName = "取立・振替";
                messageStr = "取立・振替を作成します。　船、種別を選択してください。";
            }
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", titleName, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            label_Message.Text = messageStr;

            panel詳細種別.Visible = false;
            panel見積有無.Visible = false;

            comboBox船.Items.Clear();
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }

            //-------------------------------------------------------------------
            //船の指定があれば選択する　2021/8/04 m.yoshihara 
            if (SelectedVessel != null)
            {
                int index = -1;
                for (int i = 0; i < comboBox船.Items.Count; i++)
                {
                    if (SelectedVessel.MsVesselID == (comboBox船.Items[i] as MsVessel).MsVesselID)
                    {
                        index = i;
                    }
                }
                //船を選択 
                if (index != -1)
                {
                    comboBox船.SelectedIndex = index;
                }
            }
            //--------------------------------------------------------------------

            comboBox種別.Items.Clear();
            if (手配依頼種別s != null)
            {
                foreach (MsThiIraiSbt tis in 手配依頼種別s)
                {
                    if (functionType == (int)FUNCTION_TYPE.振替取立)
                    {
                        if (tis.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        {
                            continue;
                        }
                    }
                    if (functionType == (int)FUNCTION_TYPE.発注)
                    {
                        if (tis.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        {
                            continue;
                        }
                    }
                    if (SelectedThiIraiSbt == null)
                    {
                        SelectedThiIraiSbt = tis;
                    }
                    comboBox種別.Items.Add(tis);
                }
                comboBox種別.SelectedIndex = 0;

                comboBox種別_SelectedIndexChanged();
            }
        }
        #endregion

        /// <summary>
        /// 「ＯＫ」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_OK_Click(object sender, EventArgs e)
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (comboBox船.SelectedItem is MsVessel)
            {
                SelectedVessel = comboBox船.SelectedItem as MsVessel;
            }
            else
            {
                MessageBox.Show("船を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                SelectedThiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
            }
            if (panel詳細種別.Visible == true)
            {
                if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
                {
                    SelectedThiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
                }
            }
            if (panel見積有無.Visible == true)
            {
                SelectedMitsumoriUmu = comboBox見積有無.SelectedIndex;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Cancel_Click(object sender, EventArgs e)
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        /// <summary>
        /// 手配依頼種別ComboBoxを選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (functionType == (int)FUNCTION_TYPE.発注)
            //    return;

            // 選択された手配依頼種別
           SelectedThiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;

            comboBox種別_SelectedIndexChanged();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        #region private void comboBox種別_SelectedIndexChanged()
        private void comboBox種別_SelectedIndexChanged()
        {
            //if (functionType == (int)FUNCTION_TYPE.発注)
            //    return;

            var vessel = comboBox船.SelectedItem as MsVessel;

            if (SelectedThiIraiSbt.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                // 修繕費
                panel詳細種別.Location = new Point(29, 107);
                panel詳細種別.Visible = true;
                panel見積有無.Visible = false;

                comboBox詳細種別.Items.Clear();
                if (手配依頼種別詳細s != null)
                {
                    foreach (MsThiIraiShousai tis in 手配依頼種別詳細s)
                    {
                        if (tis.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.荷役資材) && (vessel != null && vessel.NiyakuEnabled != 1))
                            continue;

                        comboBox詳細種別.Items.Add(tis);
                    }
                    comboBox詳細種別.SelectedIndex = 0;
                }
            }
            else if (SelectedThiIraiSbt.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                if (functionType == (int)FUNCTION_TYPE.見積)
                {
                    // 新規見積の場合、見積もりDDLは表示しない
                    panel詳細種別.Visible = false;
                    panel見積有無.Visible = false;
                    return;
                }

                // 潤滑油
                panel詳細種別.Visible = false;
                panel見積有無.Location = new Point(29, 107);
                panel見積有無.Visible = true;
                comboBox見積有無.SelectedIndex = 1;
            }
            else
            {
                // 船用品
                panel詳細種別.Visible = false;
                panel見積有無.Visible = false;
            }
        }
        #endregion

        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedThiIraiSbt == null)
                return;

            if (SelectedThiIraiSbt.MsThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_修繕ID)
                return;

            var vessel = comboBox船.SelectedItem as MsVessel;

            comboBox詳細種別.Items.Clear();
            if (手配依頼種別詳細s != null)
            {
                foreach (MsThiIraiShousai tis in 手配依頼種別詳細s)
                {
                    if (tis.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.荷役資材) && vessel.NiyakuEnabled != 1)
                        continue;

                    comboBox詳細種別.Items.Add(tis);
                }
                comboBox詳細種別.SelectedIndex = 0;
            }
        }
    }
}
