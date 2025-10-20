using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WingData.DAC;
using WingData.BLC;
using WingUtil;
using WingCommon;

namespace Dousei
{
    public partial class 区分編集Form : Form
    {
        public DjDousei Dousei = null;
        public int MsVesselID;

        public 区分編集Form()
        {
            InitializeComponent();
        }

        private void 区分編集Form_Load(object sender, EventArgs e)
        {
            if (Dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            VoyageNo_textBox.Text = Dousei.VoyageNo;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            Dousei.VoyageNo = VoyageNo_textBox.Text;
            List<DjDousei> CheckBase;
            using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
            {
                CheckBase = serviceClient.DjDousei_GetRecords(WingCommon.Common.LoginUser, MsVesselID);
            }

            if (radioButton1.Checked == true)
            {
                Dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                #region バリデーション
                if (GetVoyageCount(CheckBase, Dousei, MsKanidouseiInfoShubetu.積みID) >= 2)
                {
                    MessageBox.Show("指定した次航海番号の積みが２港以上です", "動静詳細From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ;
                }
                #endregion
            }
            else
            {
                Dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                #region バリデーション
                if (GetVoyageCount(CheckBase, Dousei, MsKanidouseiInfoShubetu.揚げID) >= 5)
                {
                    MessageBox.Show("指定した次航海番号の揚げが５港以上です", "動静詳細From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
            }


            using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
            {
                serviceClient.DjDousei_UpdateDetailRecords(WingCommon.Common.LoginUser, Dousei);
            }

            MessageBox.Show("更新しました", "区分編集", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private int GetVoyageCount(List<DjDousei> CheckBase, DjDousei dousei, string MsKanidouseiInfoShubetuID)
        {
            var CheckData = from p in CheckBase
                            where p.DjDouseiID != dousei.DjDouseiID
                            && p.VoyageNo == dousei.VoyageNo
                            && p.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetuID
                            select p;

            if (CheckData == null)
                return 0;

            int cc = CheckData.Count<DjDousei>();
            return cc;
        }

        private bool Validation()
        {
            if (VoyageNo_textBox.Text == "")
            {
                MessageBox.Show("次航海番号を入力して下さい", "区分編集", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("閉じますか？", "区分編集", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            } 
        }
    }
}
