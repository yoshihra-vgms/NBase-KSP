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
    /// Accident作成画面
    /// </summary>
    public partial class AccidentCreateForm : BaseForm
    {
        public AccidentCreateForm() : base(EFormNo.AccidentCreate, false)
        {
            InitializeComponent();
        }

        /// <summary>
        /// AccidentCreateForm画面データ
        /// </summary>
        public class AccidentCreateFormData
        {

        }

        /// <summary>
        /// 画面処理
        /// </summary>
        private AccidentCreateFormLogic Logic = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        private AccidentCreateFormData FData = null;

        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// 登録処理
        /// </summary>
        /// <returns></returns>
        public bool RegistData()
        {
            using (WaitingState es = new WaitingState(this))
            {
                try
                {

                    //登録処理
                    AccidentManager.CheckInputError(this.accidentHeaderControl1, this.accidentDetailControl1);

                    //挿入処理
                    AccidentManager.Insert(this.accidentHeaderControl1, this.accidentDetailControl1);

                }
                catch (ControlInputErrorException ie)
                {
                    //エラー位置を表示
                    this.ScrollErrorPosition(this.accidentHeaderControl1, this.accidentDetailControl1);

                    DcMes.ShowMessage(this, EMessageID.MI_26);
                    AccidentManager.ResetError(this.accidentHeaderControl1, this.accidentDetailControl1);
                    return false;
                }
                catch (FileViewControlEx.FileDataException fex)
                {
                    DcLog.WriteLog(fex, "Accident FileError");
                    DcMes.ShowMessage(this, EMessageID.MI_73);
                    return false;
                }
                catch (Exception e)
                {
                    DcLog.WriteLog(e, "RegistData");
                    DcMes.ShowMessage(this, EMessageID.MI_27);
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
                this.flowLayoutPanelDetail.ScrollControlIntoView(decon);
            }

        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            this.FData = new AccidentCreateFormData();
            this.Logic = new AccidentCreateFormLogic(this, this.FData);

            //初期化を行う
            this.Logic.Init();
            

            

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
        



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentCreateForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "AccidentCreateForm_Load");
        }


        /// <summary>
        /// 登録ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");

            //登録処理
            bool ret = this.RegistData();
            if (ret == false)
            {
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
