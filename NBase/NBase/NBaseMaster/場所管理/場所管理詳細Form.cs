using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster.場所管理
{
    public partial class 場所管理詳細Form : Form
    {
        public MsBasho msBasho = null;
        public List<MsBashoKubun> msBashoKubuns = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 場所管理詳細Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "管理詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        private void 場所管理詳細Form_Load(object sender, EventArgs e)
        {
            #region 場所区分
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msBashoKubuns = serviceClient.MsBashoKubun_GetRecords(NBaseCommon.Common.LoginUser);
            }
            BashoKubun_comboBox.Items.Clear();
            foreach (MsBashoKubun msBashoKubun in msBashoKubuns)
            {
                BashoKubun_comboBox.Items.Add(msBashoKubun);
            }
            BashoKubun_comboBox.SelectedIndex = 0;
            #endregion
            if (msBasho == null)
            {
                Delete_button.Enabled = false;
                radioButton1.Checked = true;
                BashoKubun_comboBox.SelectedIndex = 0;
            }
            else
            {
                BashoNo_textBox.Text = msBasho.MsBashoNo;
                BashoName_textBox.Text = msBasho.BashoName;

                //if (msBasho.GaichiFlag == 0)
                //{
                //    radioButton1.Checked = true;
                //}
                //else
                //{
                //    radioButton2.Checked = true;
                //}
                //for (int i = 0; i < BashoKubun_comboBox.Items.Count; i++)
                //{
                //    MsBashoKubun b = BashoKubun_comboBox.Items[i] as MsBashoKubun;
                //    if (b.MsBashoKubunId == msBasho.MsBashoKubunId)
                //    {
                //        BashoKubun_comboBox.SelectedIndex = i;
                //        break;
                //    }
                //}
            }

            this.ChangeFlag = false;
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

            bool is新規作成;
            if (msBasho == null)
            {
                msBasho = new MsBasho();
                msBasho.MsBashoId = Guid.NewGuid().ToString();
                msBasho.RenewDate = DateTime.Now;
                msBasho.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msBasho.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msBasho.MsBashoNo = BashoNo_textBox.Text;
            msBasho.BashoName = BashoName_textBox.Text;
            //if (radioButton1.Checked == true)
            //{
                msBasho.GaichiFlag = 0;
            //}
            //else
            //{
            //    msBasho.GaichiFlag = 1;
            //}
            MsBashoKubun b = BashoKubun_comboBox.SelectedItem as MsBashoKubun;
            msBasho.MsBashoKubunId = b.MsBashoKubunId;

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBasho_InsertRecord(NBaseCommon.Common.LoginUser, msBasho);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBasho_UpdateRecord(NBaseCommon.Common.LoginUser, msBasho);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "場所管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Validation()
        {
            //場所No
            if (BashoNo_textBox.Text == "")
            {
                MessageBox.Show("港Noを入力して下さい", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (BashoName_textBox.Text == "")
            {
                MessageBox.Show("港名を入力して下さい", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            //使用しているかどうかをチェックする
            bool result = this.CheckDeleteUsing(this.msBasho);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("削除しますか？", "港管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msBasho.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsBasho_UpdateRecord(NBaseCommon.Common.LoginUser, msBasho);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "港管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }

        /// <summary>
        /// 指定データが使用されているかをチェックする
        /// 引数：チェックするデータ
        /// 返り値：true→未使用削除可 false→使用削除不可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsBasho data)
        {
            //MsBashoは
            //DJ_DOUSEIとPT_KANIDOUSEI_INFOとMS_BASHO_KYORIにリンクしている ER参照
            //MS_BASHO_KYORIは場所1と場所2があるので両方チェックする
            
            //data.MsBashoId  string
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region DJ_DOUSEI
                List<DjDousei> douseilist = new List<DjDousei>();

                //関連データの取得
                douseilist = serviceClient.DjDousei_GetRecordsByMsBashoID(NBaseCommon.Common.LoginUser, data.MsBashoId);

                if (douseilist.Count > 0)
                {
                    return false;
                }                    

                #endregion

                #region PT_KANIDOUSEI_INFO
                List<PtKanidouseiInfo> kanilist = new List<PtKanidouseiInfo>();

                //関連データの取得
                kanilist = serviceClient.PtKanidouseiInfo_GetRecordsByMsBashoID(NBaseCommon.Common.LoginUser, data.MsBashoId);

                //一致するデータが存在するなら消すことができない
                if (kanilist.Count > 0)
                {
                    return false;
                }

                #endregion

                #region MS_BASHO_KYORI
                List<MsBashoKyori> kyorilist = new List<MsBashoKyori>();

                //一致でータが存在するかどうかをチェックする。
                kyorilist = serviceClient.MsBashoKyori_GetRecordsByMsBashoID(NBaseCommon.Common.LoginUser, data.MsBashoId);

                if (kyorilist.Count > 0)
                {
                    return false;
                }

                #endregion
            }
            return true;
        }
    }
}
