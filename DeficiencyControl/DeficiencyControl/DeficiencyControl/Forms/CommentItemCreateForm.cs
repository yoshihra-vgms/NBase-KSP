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
    /// コメント新規登録画面
    /// </summary>
    public partial class CommentItemCreateForm : BaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CommentItemCreateForm() : base(EFormNo.PSCCreate)
        {
            InitializeComponent();
        }

        /// <summary>
        /// この画面のデータ
        /// </summary>
        public class CommentItemCreateFormData
        {
            /// <summary>
            /// 選択しているItemKind
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
        /// 画面データ
        /// </summary>
        public CommentItemCreateFormData FData = null;

        /// <summary>
        /// ロジッククラス
        /// </summary>
        public CommentItemCreateFormLogic Logic = null;

        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 登録処理 中でwaitstateします。
        /// </summary>
        private bool RegistData()
        {
            try
            {
                using (WaitingState es = new WaitingState(this))
                {
                    //登録処理
                    this.Logic.RegistData();
                }
            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "RegistData");
                return false;
            }
            

            return true;
        }

        

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //管理作成
            this.FData = new CommentItemCreateFormData();
            this.Logic = new CommentItemCreateFormLogic(this, this.FData);

            //PSCを指定
            this.FData.ItemKind = ECommentItemKind.PSC_Inspection;

            //HeaderをADDしておく
            this.Logic.ChangeItemKind(this.FData.ItemKind);
            
            

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
            //this.buttonUpdate.Enabled = rdata.comment_regist;

            return true;
        }



        

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentItemCreateForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "CommentItemCreateForm_Load");
        }


        /// <summary>
        /// 更新登録ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");

            //データ登録処理
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
