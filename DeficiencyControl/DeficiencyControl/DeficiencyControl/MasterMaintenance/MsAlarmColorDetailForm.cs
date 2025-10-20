using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Grid;

using DeficiencyControl.MasterMaintenance.Logic;
using DeficiencyControl.Logic;

namespace DeficiencyControl.MasterMaintenance
{
    /// <summary>
    /// MsAlarmColor詳細画面
    /// </summary>
    public partial class MsAlarmColorDetailForm : BaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsAlarmColorDetailForm() : base( EFormNo.MsAlarmColorDetail)
        {
            InitializeComponent();
                        
            this.FData = new MsAlarmColorDetailFormData();
            this.Logic = new BaseFormLogic();
        }

        /// <summary>
        /// このフォームのデータ
        /// </summary>
        class MsAlarmColorDetailFormData
        {
            /// <summary>
            /// データ nullなら新規、あるなら既存
            /// </summary>
            public MsAlarmColor SrcData = null;
        }


        /// <summary>
        /// このフォームのデータ
        /// </summary>
        private MsAlarmColorDetailFormData FData = null;

        /// <summary>
        /// 
        /// </summary>
        private BaseFormLogic Logic = null;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// データの表示
        /// </summary>
        /// <returns></returns>
        private bool DispData()
        {
            MsAlarmColor data = this.FData.SrcData;

            //ID
            this.textBoxID.Text = data.alarm_color_id.ToString();

            //Day
            this.textBoxDayCount.Text = data.day_count.ToString();

            //color
            this.buttonColor.BackColor = data.AlarmColor;

            //comment
            this.textBoxComment.Text = data.comment;


            return true;
        }

        /// <summary>
        /// 入力エラーのチェック
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            try
            {
                this.Logic.ErList.Clear();

                //エラーチェックするコントロール
                Control[] contvec = {
                                    this.textBoxDayCount,

                                };

                //入力チェック
                List<Control> erlist = ComLogic.CheckInput(contvec);
                this.Logic.ErList.AddRange(erlist);

                //数値変換チェック
                try
                {
                    Convert.ToInt32(this.textBoxDayCount.Text);
                }
                catch
                {
                    this.Logic.ErList.Add(this.textBoxDayCount);
                }

                if (this.Logic.ErList.Count <= 0)
                {
                    return true;
                }

                throw new Exception("入力エラー");

            }
            catch
            {
                this.Logic.DispError();
                DcMes.ShowMessage(this, EMessageID.MI_8);
            }
            finally
            {
                this.Logic.ResetError();
            }


            return false;
        }


        /// <summary>
        /// 入力データの取得
        /// </summary>
        /// <returns></returns>
        private MsAlarmColor GetInput()
        {           
            MsAlarmColor ans = new MsAlarmColor();
            if (this.FData.SrcData != null)
            {
                ans = (MsAlarmColor)this.FData.SrcData.Clone();
            }
            try
            {

                //Day
                ans.day_count = Convert.ToInt32(this.textBoxDayCount.Text);

                //Color
                Color col = this.buttonColor.BackColor;
                ans.color_r = col.R;
                ans.color_g = col.G;
                ans.color_b = col.B;

                //Comment
                ans.comment = this.textBoxComment.Text;


            }
            catch(Exception e)
            {
                DcLog.WriteLog(e, "GetInputエラー");
                return null;
            }
            return ans;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateData()
        {
            DcLog.WriteLog("Update START");

            //入力チェック
            bool cret = this.CheckInput();
            if (cret == false)
            {
                DcLog.WriteLog("CheckInput FALSE");
                return false;
            }

            //データ取得
            MsAlarmColor data = this.GetInput();

            try
            {
                if (this.FData.SrcData == null)
                {
                    //新規なので挿入
                    int id = SvcManager.SvcMana.MsAlarmColor_InsertRecord(data, DcGlobal.Global.LoginMsUser);
                    if (id == MsAlarmColor.EVal)
                    {
                        throw new Exception("MsAlarmColor_InsertRecord FALSE");
                    }
                }
                else
                {
                    //更新
                    bool ret = SvcManager.SvcMana.MsAlarmColor_UpdateRecord(data, DcGlobal.Global.LoginMsUser);
                    if (ret == false)
                    {
                        throw new Exception("MsAlarmColor_UpdateRecord FALSE");
                    }
                }
            }
            catch(Exception e)
            {
                DcLog.WriteLog(e, "Update Exception");
                DcMes.ShowMessage(this, EMessageID.MI_9);
                return false;
            }

            DcLog.WriteLog("Update END");
            return true;
        }


        /// <summary>
        /// データ削除処理
        /// </summary>
        /// <returns></returns>
        private bool Delete()
        {
            DcLog.WriteLog("Delete START");

            //削除確認
            DialogResult dret = DcMes.ShowMessage(this, EMessageID.MI_10, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != System.Windows.Forms.DialogResult.Yes)
            {
                return false;         
            }

            //削除でないなら返却
            if (this.FData.SrcData == null)
            {
                DcLog.WriteLog("SrcData NULL");
                return false;
            }

            //削除処理
            try
            {
                bool ret = SvcManager.SvcMana.MsAlarmColor_DeleteRecord(this.FData.SrcData, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("MsAlarmColor_DeleteRecord FALSE");
                }
            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "Delete Exception");
                DcMes.ShowMessage(this, EMessageID.MI_11);
                return false;
            }


            DcLog.WriteLog("Delete END");
            return true;
        }
            
        //-----------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// この画面に対するデータ設定  表示するMsAlarmColorを設定、新規はnull
        /// </summary>
        /// <param name="fsetdata"></param>
        /// <returns></returns>
        public override bool SetFormSettingData(object fsetdata)
        {
            //入力設定
            this.FData.SrcData = fsetdata as MsAlarmColor;
            return true;
        }

        /// <summary>
        /// この画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            if (this.FData.SrcData == null)
            {
                //新規の時

                //Deleteボタンをけす
                this.buttonDelete.Visible = false;
            }
            else
            {
                //更新の時
                //データ表示
                this.DispData();
            }


            return true;
        }

        /// <summary>
        /// ユーザーロールの制御。
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            //MsUserRole rdata = DcGlobal.Global.LoginUser.Role;

            //this.buttonUpdate.Enabled = rdata.master_regist;
            //this.buttonDelete.Enabled = rdata.master_regist;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsAlarmColorDetailForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Updateボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");

            using (WaitingState es = new WaitingState(this))
            {
                bool ret = this.UpdateData();
                if (ret == false)
                {
                    return;
                }
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

            using (WaitingState es = new WaitingState(this))
            {
                bool ret = this.Delete();
                if (ret == false)
                {
                    return;
                }
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

        /// <summary>
        /// 色選択ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColor_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonColor_Click");

            //色選択の表示
            this.colorDialog1.Color = this.buttonColor.BackColor;
            DialogResult dret = this.colorDialog1.ShowDialog();
            if (dret != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //選択色を設定
            this.buttonColor.BackColor = this.colorDialog1.Color;
            
        }
    }
}
