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
    public partial class 船種別変更_依頼種別Form : Form
    {

        /// <summary>
        /// 選択されている船
        /// </summary>
        public int SelectedVesselID;

        /// <summary>
        /// 選択されている手配依頼種別
        /// </summary>
        public string SelectedThiIraiSbtID;

        /// <summary>
        /// 選択されている手配依頼詳細種別
        /// </summary>
        public string SelectedThiIraiShousaiID;


        private List<MsVessel> vessels = null;
        private List<MsThiIraiSbt> 手配依頼種別s = null;
        private List<MsThiIraiShousai> 手配依頼種別詳細s = null;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="functionType">依頼種別Form.FUNCTION_TYPE</param>
        /// <param name="vsl">発注状況一覧で選択されている船　選択なしならnull 2021/08/04 m.yoshihara</param>
        #region public 船種別変更_依頼種別Form(int functionType, MsVessel vsl)
        public 船種別変更_依頼種別Form(int vslID, string sbtID, string ssbtID)
        {
;
            SelectedVesselID = vslID;
            SelectedThiIraiSbtID = sbtID;
            SelectedThiIraiShousaiID = ssbtID;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsVessel> wklist = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                vessels = new List<MsVessel>();
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
        #region private void 船種別変更_依頼種別Form_Load(object sender, EventArgs e)
        private void 船種別変更_依頼種別Form_Load(object sender, EventArgs e)
        {
            string titleName = "";
            string messageStr = "";
           
            titleName = "船／種別変更";
            messageStr = "船、種別を変更します。船、種別を選択してください。";
            
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", titleName, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            label_Message.Text = messageStr;

            panel詳細種別.Visible = false;


            comboBox船.Items.Clear();
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }

            comboBox種別.Items.Clear();
            if (手配依頼種別s != null)
            {
                foreach (MsThiIraiSbt tis in 手配依頼種別s)
                {
                    if (tis.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        continue;
                    }
                    comboBox種別.Items.Add(tis);
                }
            }


            #region  コンボボックス選択
            
            int index = -1;
            
            //船を選択
            for (int i = 0; i < comboBox船.Items.Count; i++)
            {
                if (SelectedVesselID == (comboBox船.Items[i] as MsVessel).MsVesselID)
                {
                    index = i;
                }
            } 
            if (index != -1)
            {
                comboBox船.SelectedIndex = index;
            }

            //種別選択
            index = -1;
            for (int i = 0; i < comboBox種別.Items.Count; i++)
            {
                if (SelectedThiIraiSbtID == (comboBox種別.Items[i] as MsThiIraiSbt).MsThiIraiSbtID)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                comboBox種別.SelectedIndex = index;
                comboBox種別_SelectedIndexChanged();
            }

            //詳細種別
            index = -1;
            if (SelectedThiIraiShousaiID == "") return;
            for (int i = 0; i < comboBox詳細種別.Items.Count; i++)
            {
                if (SelectedThiIraiShousaiID == (comboBox詳細種別.Items[i] as MsThiIraiShousai).MsThiIraiShousaiID)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                comboBox詳細種別.SelectedIndex = index;
            }

            #endregion


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
                SelectedVesselID = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            }
            else
            {
                MessageBox.Show("船を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                SelectedThiIraiSbtID = (comboBox種別.SelectedItem as MsThiIraiSbt).MsThiIraiSbtID;
            }
            if (panel詳細種別.Visible == true)
            {
                if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
                {
                    SelectedThiIraiShousaiID = (comboBox詳細種別.SelectedItem as MsThiIraiShousai).MsThiIraiShousaiID;
                }
            }
            else
            {
                SelectedThiIraiShousaiID = null;
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
           SelectedThiIraiSbtID = (comboBox種別.SelectedItem as MsThiIraiSbt).MsThiIraiSbtID;

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

            if (SelectedThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                // 修繕費
                panel詳細種別.Location = new Point(29, 107);
                panel詳細種別.Visible = true;

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
            else if (SelectedThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                
                // 潤滑油
                panel詳細種別.Visible = false;

                SelectedThiIraiShousaiID = "";

            }
            else
            {
                // 船用品
                panel詳細種別.Visible = false;

                SelectedThiIraiShousaiID = "";

            }
        }
        #endregion

        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedThiIraiSbtID == null)
                return;

            if (SelectedThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_修繕ID)
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
