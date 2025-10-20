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

namespace NBaseMaster
{
    public partial class 国管理詳細Form : Form
    {
        public MsRegional msRegional = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 国管理詳細Form()
        {
            InitializeComponent();
        }

        private void 国管理詳細Form_Load(object sender, EventArgs e)
        {
            if (msRegional == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                RegionalCode_textBox.Text = msRegional.MsRegionalCode;
                RegionalName_textBox.Text = msRegional.RegionalName;

                RegionalCode_textBox.Enabled = false;
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
            if (msRegional == null)
            {
                msRegional = new MsRegional();
                msRegional.RenewDate = DateTime.Now;
                msRegional.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msRegional.UserKey = "1";
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msRegional.MsRegionalCode = RegionalCode_textBox.Text;
            msRegional.RegionalName = RegionalName_textBox.Text;

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsRegional_InsertRecord(NBaseCommon.Common.LoginUser, msRegional);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsRegional_UpdateRecord(NBaseCommon.Common.LoginUser, msRegional);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Validation()
        {
            if (RegionalCode_textBox.Text == "")
            {
                MessageBox.Show("Noを入力して下さい", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (RegionalName_textBox.Text == "")
            {
                MessageBox.Show("国名を入力して下さい", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            bool result = this.CheckDeleteUsing(this.msRegional);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("削除しますか？", "国管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msRegional.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsRegional_UpdateRecord(NBaseCommon.Common.LoginUser, msRegional);
                }

                 if (ret == true)
                {
                    MessageBox.Show("削除しました", "国管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private bool CheckDeleteUsing(MsRegional data)
        {
            ////MsRegionalは
            ////DJ_DOUSEIとPT_KANIDOUSEI_INFOとMS_BASHO_KYORIにリンクしている ER参照
            ////MS_BASHO_KYORIは場所1と場所2があるので両方チェックする

            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    #region DJ_DOUSEI
            //    List<DjDousei> douseilist = new List<DjDousei>();

            //    //関連データの取得
            //    douseilist = sc.DjDousei_GetRecordsByMsRegionalID(NBaseCommon.Common.LoginUser, data.MsRegionalId);

            //    if (douseilist.Count > 0)
            //    {
            //        return false;
            //    }                    

            //    #endregion

            //    #region PT_KANIDOUSEI_INFO
            //    List<PtKanidouseiInfo> kanilist = new List<PtKanidouseiInfo>();

            //    //関連データの取得
            //    kanilist = sc.PtKanidouseiInfo_GetRecordsByMsRegionalID(NBaseCommon.Common.LoginUser, data.MsRegionalId);

            //    //一致するデータが存在するなら消すことができない
            //    if (kanilist.Count > 0)
            //    {
            //        return false;
            //    }

            //    #endregion

            //    #region MS_BASHO_KYORI
            //    List<MsRegionalKyori> kyorilist = new List<MsRegionalKyori>();

            //    //一致でータが存在するかどうかをチェックする。
            //    kyorilist = sc.MsRegionalKyori_GetRecordsByMsRegionalID(NBaseCommon.Common.LoginUser, data.MsRegionalId);

            //    if (kyorilist.Count > 0)
            //    {
            //        return false;
            //    }

            //    #endregion
            //}
            return true;
        }
    }
}
