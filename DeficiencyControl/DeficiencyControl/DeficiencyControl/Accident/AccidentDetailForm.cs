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

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// Accident詳細画面
    /// </summary>
    public partial class AccidentDetailForm : BaseForm
    {
        public AccidentDetailForm() : base(EFormNo.AccidentDetail, true)
        {
            InitializeComponent();


            //データ、処理作成
            this.FData = new AccidentDetailFormData();
            this.Logic = new AccidentDetailFormLogic(this, this.FData);
        }


        /// <summary>
        /// 入力データ
        /// </summary>
        public class AccidentDetailFormInputData
        {
            /// <summary>
            /// 今回表示するAccidentのデータ
            /// </summary>
            public DcAccident Accident = null;
        }


        /// <summary>
        /// 画面データ
        /// </summary>
        public class AccidentDetailFormData
        {
            /// <summary>
            /// 今回のデータ
            /// </summary>
            public AccidentData SrcData = null;

            /// <summary>
            /// 入力データ
            /// </summary>
            public AccidentDetailFormInputData InputData = null;
        }

        /// <summary>
        /// 画面データ
        /// </summary>
        AccidentDetailFormData FData = null;

        /// <summary>
        /// 処理
        /// </summary>
        AccidentDetailFormLogic Logic = null;
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
                    AccidentManager.CheckInputError(this.accidentHeaderControl1, this.accidentDetailControl1);

                    //更新処理
                    AccidentManager.Update(this.FData.SrcData, this.accidentHeaderControl1, this.accidentDetailControl1);

                }
                catch (ControlInputErrorException ie)
                {
                    //エラー位置表示
                    this.ScrollErrorPosition(this.accidentHeaderControl1, this.accidentDetailControl1);

                    DcMes.ShowMessage(this, EMessageID.MI_32);
                    AccidentManager.ResetError(this.accidentHeaderControl1, this.accidentDetailControl1);
                    return false;
                }
                catch (FileViewControlEx.FileDataException fex)
                {
                    DcLog.WriteLog(fex, "Accident FileError");
                    DcMes.ShowMessage(this, EMessageID.MI_74);
                    return false;
                }
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "UpdateData");
                    DcMes.ShowMessage(this, EMessageID.MI_33);
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
        private void ScrollErrorPosition(AccidentHeaderControl headercontrol, AccidentDetailControl detailcon)
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
                    AccidentManager.Delete(this.FData.SrcData);

                }               
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "DeleteData");
                    DcMes.ShowMessage(this, EMessageID.MI_35);
                    return false;
                }

            }
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化データの設定
        /// </summary>
        /// <param name="fsetdata">AccidentDetailFormInputData</param>
        /// <returns></returns>
        public override bool SetFormSettingData(object fsetdata)
        {
            this.FData.InputData = fsetdata as AccidentDetailFormInputData;

            return true;
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //今回のデータを取得する
            this.FData.SrcData = SvcManager.SvcMana.AccidentData_GetDataByAccidentID(this.FData.InputData.Accident.accident_id);

            //画面の初期化
            this.Logic.Init();


            //Completeの場合は編集不可とする
            if (this.FData.SrcData.Accident.accident_status_id == (int)EAccidentStatus.Complete)
            {
                this.buttonUpdate.Enabled = false;
                this.buttonDelete.Enabled = false;
            }

            return true;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentDetailForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "AccidentDetailForm_Load");
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

            DialogResult dret = DcMes.ShowMessage(this, EMessageID.MI_34, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
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
