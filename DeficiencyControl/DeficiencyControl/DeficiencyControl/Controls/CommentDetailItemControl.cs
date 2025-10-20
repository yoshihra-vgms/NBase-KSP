using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Forms;
using DeficiencyControl.Controls.CommentItem;
using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;



namespace DeficiencyControl.Controls
{
    /// <summary>
    /// CommentItem詳細画面Itemタブコントロール
    /// </summary>
    public partial class CommentDetailItemControl : BaseControl
    {
        public CommentDetailItemControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// このクラスの入力
        /// </summary>
        public class InputData
        {
            /// <summary>
            /// コメント管理
            /// </summary>
            public BaseCommentItemManager CIManager = null;

            /// <summary>
            /// ヘッダーコントロール
            /// </summary>
            public BaseCommentItemHeaderControl Header = null;
        }

        /// <summary>
        /// これのデータ
        ///  </summary>
        public class CommentDetailItemControlData
        {
            /// <summary>
            /// コメント管理
            /// </summary>
            public BaseCommentItemManager CIManager = null;

            /// <summary>
            /// ヘッダーコントロール
            /// </summary>
            public BaseCommentItemHeaderControl Header = null;

            /// <summary>
            /// 詳細コントロール
            /// </summary>
            public BaseCommentItemDetailControl DetailControl = null;
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        private CommentDetailItemControlData FData = null;

        /// <summary>
        /// 詳細コントロールの取得
        /// </summary>
        public BaseCommentItemDetailControl DetailControl
        {
            get
            {
                return this.FData.DetailControl;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        //==================================================================================================
        /// <summary>
        /// このコントロールの初期化
        /// </summary>
        /// <param name="inputdata">CommentDetailItemControl.InputData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new CommentDetailItemControlData();

            //データ取得
            InputData indata = inputdata as InputData;
            if (indata == null)
            {
                return false;
            }

            //データの設定
            this.FData.CIManager = indata.CIManager;
            this.FData.Header = indata.Header;
                
            
            //詳細コントロールを作成してADDする
            this.FData.DetailControl = CommentItemCreator.CreateCommentItemDetailControl(this.FData.CIManager.ItemKind);
            BaseCommentItemDetailControl dcon = this.FData.DetailControl;


            dcon.Left = 0;
            dcon.Top = 0;

            this.Width = dcon.Width;
            this.Height = dcon.Height;

            //コントロールの初期化
            dcon.InitControl(this.FData.CIManager);

            //コントロールに追加
            this.Controls.Add(dcon);

            //0件データの場合、姿を消す
            if (this.FData.CIManager.DeficiencyCount <= 0)
            {
                this.Visible = false;
            }

            return true;
        }

        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {
            return this.FData.DetailControl.CheckError(chcol);
        }

        /// <summary>
        /// エラー表示リセット
        /// </summary>
        /// <returns></returns>
        public override bool ResetError()
        {
            return this.FData.DetailControl.ResetError();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
