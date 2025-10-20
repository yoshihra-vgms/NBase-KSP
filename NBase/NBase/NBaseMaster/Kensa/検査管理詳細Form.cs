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
    public partial class 検査管理詳細Form : Form
    {

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        /// <summary>
        /// コンストラクタ
        /// 引数：今回関連するデータ NULL→新規追加
        /// </summary>
        /// <param name="kensa"></param>
        public 検査管理詳細Form(MsKensa kensa)
        {
            this.ChangeFlag = false;

            InitializeComponent();

            this.AddFlag = false;
            this.KensaData = kensa;

            //新規追加だった
            if (this.KensaData == null)
            {
                this.KensaData = new MsKensa();
                this.AddFlag = true;
				this.DeleteButton.Enabled = false;
            }
        }


        //メンバ関数============================================================
        /// <summary>
        /// データの表示
        /// 引数：表示データ NULL→初期化
        /// 返り値：なし
        /// </summary>
        private void Data表示(MsKensa kensa)
        {
            if(this.AddFlag == true || kensa == null)
            {
                this.KensaNameText.Text = "";   //検査名
                this.KankakuText.Text = "";     //間隔

                return;
            }
            

            this.KensaNameText.Text = kensa.KensaName;                  //検査名
            this.KankakuText.Text = kensa.Kankaku.ToString();          //間隔

        }



        /// <summary>
        /// 入力データのエラーチェック
        /// 返り値：問題あり→false
        /// </summary>
        /// <returns></returns>
        private bool CheckDataError()
        {
            //検査名の入力があるかをチェック
            try
            {
                //入力がある？
                if (this.KensaNameText.Text.Length <= 0)
                {
                    Message.Showエラー("検査名を入力して下さい");
                    return false;
                }
            }
            catch (Exception e)
            {
                Message.Showエラー("検査名を入力して下さい");
                return false;
            }

            ////////////////////////////////////////////////////////////////////////////
            //間隔のチェック
            try
            {
                Convert.ToInt32(this.KankakuText.Text);
            }
            catch (Exception e)
            {
                Message.Showエラー("間隔を入力して下さい");
                return false;
            }

            return true;
        }

        private bool 更新処理()
        {
            //データ入力をチェックする
            bool ret = this.CheckDataError();
            if (ret == false)
            {
                return false;
            }

            //データを作成する
            #region データ作成
			if (this.AddFlag == true)
			{
				this.KensaData.MsKensaID = Guid.NewGuid().ToString();
			}
            this.KensaData.KensaName = this.KensaNameText.Text;
            this.KensaData.Kankaku = Convert.ToInt32(this.KankakuText.Text);


            this.KensaData.DeleteFlag = 0;
            this.KensaData.DataNo = 0;
            this.KensaData.RenewDate = DateTime.Now;
            this.KensaData.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            this.KensaData.SendFlag = 0;
            //this.KensaData.Ts = "0";
            this.KensaData.UserKey = "1";
            this.KensaData.VesselID = 0;

            #endregion

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //新規登録だった
                if (this.AddFlag == true)
                {
                    //Insert
					ret = serviceClient.MsKensa_InsertRecord(NBaseCommon.Common.LoginUser, this.KensaData);

                }
                //更新だった
                else
                {
                    //Update
					ret = serviceClient.MsKensa_UpdateRecord(NBaseCommon.Common.LoginUser, this.KensaData);
                }
            }

			if (ret == false)
			{
				Message.Showエラー("更新に失敗しました。");
			}

            return true;
        }

		private bool 削除処理()
		{
			//削除フラグを立てる
			this.KensaData.DeleteFlag = 1;

			//更新
			this.KensaData.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
			this.KensaData.RenewDate = DateTime.Now;

			bool ret;
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				ret = serviceClient.MsKensa_UpdateRecord(NBaseCommon.Common.LoginUser, this.KensaData);
			}

			if (ret == false)
			{
				Message.Showエラー("削除に失敗しました。");
			}

			return true;
		
        }


        /// <summary>
        /// 削除ヶ時ノ問題提起
        /// 引数：対象
        /// 返り値：削除可能true　削除叶わぬfalse
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsKensa data)
        {
            //リンク先は
            //KS_KENSA
            //KS_SHOUSHO_KENSA_LINK
            //KS_NIYAKU_KENSA_LINK

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region KsKensa
                //検査チェック
                List<KsKensa> kenlist = serviceClient.KsKensa_GetRecordsByMsKensaID(NBaseCommon.Common.LoginUser, data.MsKensaID);

                if (kenlist.Count > 0)
                {
                    return false;
                }
                #endregion

                #region KsShoushoKensaLink

                List<KsShoushoKensaLink> kslist =
                    serviceClient.KsShoushoKensaLink_GetRecordsByMsKensaID(NBaseCommon.Common.LoginUser, data.MsKensaID);
                
                //使用中です。
                if (kslist.Count > 0)
                {
                    return false;
                }

                #endregion

                #region KsNiyakuKensaLink
                List<KsNiyakuKensaLink> nilist =
                    serviceClient.KsNiyakuKensaLink_GetRecordsByMsKensaID(NBaseCommon.Common.LoginUser, data.MsKensaID);

                if (nilist.Count > 0)
                {
                    return false;
                }

                #endregion
            }

            return true;
        }

        //メンバ変数============================================================
        /// <summary>
        /// 新規に追加かどうか？
        /// </summary>
        private bool AddFlag;

        //関連検査データ
        private MsKensa KensaData;



        ///////////////////////////////////////////////////////////////////////////
        //各種イベント処理========================================================
        //読み込まれた時
        private void 検査管理詳細Form_Load(object sender, EventArgs e)
        {
            this.Data表示(this.KensaData);
        }

        //更新ボタンが押されたとき
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            bool ret = this.更新処理();
			if (ret == true)
			{
				this.Close();
			}
        }
        
        //削除ボタンが押されたとき
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            bool result = this.CheckDeleteUsing(this.KensaData);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


			bool ret = this.削除処理();
			if (ret == true)
			{
				this.Close();
			}

        }

        //閉じるボタンが押された時
        private void CloseButton_Click(object sender, EventArgs e)
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
            this.Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }
    }
}
