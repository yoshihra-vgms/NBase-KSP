using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船詳細画面
    /// </summary>
    public partial class MoiDetailForm : BaseForm
    {
        public MoiDetailForm() : base(EFormNo.MoiDetail, true)
        {
            InitializeComponent();

            //作成
            this.FData = new MoiDetailFormData();
            this.Logic = new MoiDetailFormLogic(this, this.FData);
            
        }

        /// <summary>
        /// 画面入力
        /// </summary>
        public class MoiDetailFormInputData
        {
            /// <summary>
            /// これに表示するもの
            /// </summary>
            public int moi_observation_id = DcMoiObservation.EVal;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class MoiDetailFormData
        {
            /// <summary>
            /// 入力データ
            /// </summary>
            public MoiDetailFormInputData InputData = null;

            /// <summary>
            /// 元データ
            /// </summary>
            public MoiData SrcData = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public MoiDetailFormData FData = null;

        /// <summary>
        /// これの処理
        /// </summary>
        private MoiDetailFormLogic Logic = null;
     

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// データの登録
        /// </summary>
        /// <returns></returns>
        private bool UpdateData()
        {
            using (WaitingState es = new WaitingState(this))
            {
                try
                {

                    //入力確認
                    MoiManager.CheckError(this.moiHeaderControl1, this.moiDetailControl1);
                    

                    //更新処理
                    MoiManager.Update(this.FData.SrcData, this.moiHeaderControl1, this.moiDetailControl1);

                }
                catch (ControlInputErrorException ie)
                {
                    //エラー位置までスクロール
                    this.ScrollErrorPosition(this.moiHeaderControl1, this.moiDetailControl1);

                    DcMes.ShowMessage(this, EMessageID.MI_39);
                    MoiManager.ResetError(this.moiHeaderControl1, this.moiDetailControl1);
                    return false;
                }
                catch (FileViewControlEx.FileDataException fex)
                {
                    //添付ファイルエラー
                    DcLog.WriteLog(fex, "MOI FileError");
                    DcMes.ShowMessage(this, EMessageID.MI_76);
                    return false;
                }
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "UpdateData");
                    DcMes.ShowMessage(this, EMessageID.MI_40);
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// エラーとなった位置を表示する
        /// </summary>
        /// <param name="headercontrol"></param>
        /// <param name="detaillist"></param>
        private void ScrollErrorPosition(MoiHeaderControl headercontrol, MoiDetailControl detailcon)
        {
            //ヘッダーコントロール
            Control hcon = headercontrol.GetErrorFirstControl();
            if (hcon != null)
            {
                this.panelHeaderControl.ScrollControlIntoView(hcon);
            }

            //詳細コントロール
            Control decon = detailcon.GetErrorFirstControl();
            if (decon != null)
            {
                this.tabPageItem.ScrollControlIntoView(decon);
            }

        }


        /// <summary>
        /// 削除処理
        /// </summary>
        /// <returns></returns>
        private bool DeleteData()
        {
            using (WaitingState es = new WaitingState(this))
            {
                try
                {


                    ////削除
                    MoiManager.Delete(this.FData.SrcData);

                }
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "DeleteData");
                    DcMes.ShowMessage(this, EMessageID.MI_42);
                    return false;
                }

            }
            return true;
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 画面の初期化データの設定
        /// </summary>
        /// <param name="fsetdata">MoiDetailFormInputData</param>
        /// <returns></returns>
        public override bool SetFormSettingData(object fsetdata)
        {
            this.FData.InputData = fsetdata as MoiDetailFormInputData;

            return true;
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //今回のデータを取得する
            this.FData.SrcData = SvcManager.SvcMana.MoiData_GetDataByMoiObservationID(this.FData.InputData.moi_observation_id);

            //画面初期化
            this.Logic.Init();

            //Completeの場合は編集を不可とする
            if (this.FData.SrcData.Observation.Observation.moi_status_id == (int)EMoiStatus.Complete)
            {
                this.buttonUpdate.Enabled = false;
                this.buttonDelete.Enabled = false;                
            }

            return true;
        }



        /// <summary>
        /// ユーザー権限処理
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            //MsUserRole rdata = DcGlobal.Global.LoginUser.Role;

            //機能制御
            //this.buttonCreate.Enabled = rdata.;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiDetailForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "MoiDetailForm_Load");
        }

        /// <summary>
        /// 更新ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");


            //更新処理
            bool ret = this.UpdateData();
            if (ret == false)
            {
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDelete_Click");

            DialogResult dret = DcMes.ShowMessage(this, EMessageID.MI_41, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            //削除処理
            bool ret = this.DeleteData();
            if (ret == false)
            {
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        /// <summary>
        /// 閉じるボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClose_Click");

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
