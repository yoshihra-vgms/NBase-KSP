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
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Forms
{
    /// <summary>
    /// コメントアイテム詳細画面
    /// </summary>
    public partial class CommentItemDetailForm : BaseForm
    {        
        /// <summary>
        /// コメント処理
        /// </summary>
        public CommentItemDetailForm() : base(EFormNo.PSCDetail, true)
        {
            InitializeComponent();

            //データ処理
            this.FData = new CommentItemDetailFormData();
            this.Logic = new CommentItemDetailFormLogic(this, this.FData);
        }


        /// <summary>
        /// コメントアイテム
        /// </summary>
        public class CommentItemDetailFormData
        {
            /// <summary>
            /// 設定データ
            /// </summary>
            public CommentItemDetailFormInitData InputData = null;



            /// <summary>
            /// 表示アイテム種別
            /// </summary>
            public ECommentItemKind ItemKind = ECommentItemKind.PSC_Inspection;



            /// <summary>
            /// 管理するComemntItem
            /// </summary>
            public BaseCommentItemManager CIManager = null;

            /// <summary>
            /// ヘッダー
            /// </summary>
            public BaseCommentItemHeaderControl HeaderControl = null;

        }

        /// <summary>
        /// 元データ
        /// </summary>
        public class CommentItemDetailFormInitData
        {
            /// <summary>
            /// 元アイテム
            /// </summary>
            public DcCommentItem SrcItem = null;
        }

        /// <summary>
        /// 画面処理
        /// </summary>
        private CommentItemDetailFormLogic Logic = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        private CommentItemDetailForm.CommentItemDetailFormData FData = null;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 更新処理 中でWaiteStateします
        /// </summary>
        private bool UpdateData()
        {
            try
            {

                using (WaitingState se = new WaitingState(this))
                {
                    //データの更新                    
                    this.FData.CIManager.UpdateData(this.FData.HeaderControl, this.commentDetailItemControl1.DetailControl);
                }

            }
            catch (ControlInputErrorException iex)
            {
                //エラー位置までスクロールする
                this.ScrollErrorPosition(this.FData.HeaderControl, this.commentDetailItemControl1.DetailControl);

                //入力に問題があったとき
                DcMes.ShowMessage(this, EMessageID.MI_22);
                this.FData.CIManager.ResetError(this.FData.HeaderControl, this.commentDetailItemControl1.DetailControl);
                return false;
            }
            catch (FileViewControlEx.FileDataException fex)
            {
                DcLog.WriteLog(fex, "PSC FileError");
                DcMes.ShowMessage(this, EMessageID.MI_63);                
                return false;
            }
            catch (Exception e)
            {
                //更新に失敗したとき
                DcLog.WriteLog(e, "UpdateData");
                DcMes.ShowMessage(this, EMessageID.MI_23);
                return false;
            }


            return true;
        }


        /// <summary>
        /// エラーとなった位置を表示する
        /// </summary>
        /// <param name="headercontrol"></param>
        /// <param name="detaillist"></param>
        private void ScrollErrorPosition(BaseCommentItemHeaderControl headercontrol, BaseCommentItemDetailControl detailcon)
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
                //程よい位置に表示できるように調整する
                if (this.tabPageItem.VerticalScroll.Value > 30)
                {
                    this.tabPageItem.VerticalScroll.Value -= 30;
                }
            }

        }


        /// <summary>
        /// データの削除 WaitStateします。
        /// </summary>
        /// <returns></returns>
        private bool DeleteData()
        {
            using (WaitingState se = new WaitingState(this))
            {
                try
                {
                    //データの削除
                    this.FData.CIManager.DeleteData();


                }
                catch (Exception e)
                {
                    //更新に失敗したとき
                    DcLog.WriteLog(e, "DeleteData");
                    DcMes.ShowMessage(this, EMessageID.MI_25);
                    return false;
                }
            }
            return true;
        }




        //______________________________________
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// 画面データの設定
        /// </summary>
        /// <param name="fsetdata">CommentItemDetailFormInitData</param>
        /// <returns></returns>
        public override bool SetFormSettingData(object fsetdata)
        {
            //
            this.FData.InputData = fsetdata as CommentItemDetailFormInitData;
            if (this.FData.InputData == null)
            {
                return false;
            }

            //初期設定をしておく
            this.FData.ItemKind = this.FData.InputData.SrcItem.CommentItemKind;

            return true;
        }

        /// <summary>
        /// 初期化されたとき
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //            

            //入力からComment管理の作成
            this.FData.CIManager = CommentItemCreator.CreateCommentItemManager(this.FData.ItemKind);
            this.FData.CIManager.Init(this.FData.InputData.SrcItem.comment_item_id);


            //各タブの初期化
            this.Logic.Init();

            //完了の場合、更新をできなくする
            bool ckcomp = this.FData.CIManager.CheckComplete();
            if (ckcomp == true)
            {
                this.buttonUpdate.Enabled = false;
                this.buttonDelete.Enabled = false;
            }
            

            return true;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        private void CommentItemDetailForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "CommentItemDetailForm_Load");
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
        /// 削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDelete_Click");

            //削除確認
            DialogResult dret = DcMes.ShowMessage(this, EMessageID.MI_24, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
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

        
    }
}
