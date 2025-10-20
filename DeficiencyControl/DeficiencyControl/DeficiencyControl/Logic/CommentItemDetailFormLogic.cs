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

using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Forms;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;


namespace DeficiencyControl.Logic
{

    /// <summary>
    /// コメントアイテム詳細
    /// </summary>
    public class CommentItemDetailFormLogic : BaseFormLogic
    {
        public CommentItemDetailFormLogic(CommentItemDetailForm f, CommentItemDetailForm.CommentItemDetailFormData fdata)
        {
            this.Form = f;
            this.FData = fdata;
        }


        /// <summary>
        /// コメント詳細画面
        /// </summary>
        private CommentItemDetailForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        private CommentItemDetailForm.CommentItemDetailFormData FData = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            CommentItemDetailForm f = this.Form;

            //ヘッダーの作成と表示用のADD
            this.FData.HeaderControl = CommentItemCreator.CreateCommentItemHeadControl(this.FData.ItemKind);
            this.FData.HeaderControl.InitControl(this.FData.CIManager);
            this.FData.HeaderControl.TabIndex = 0;
            f.panelHeaderControl.Controls.Add(this.FData.HeaderControl);

            //Itemタブの初期化
            {
                CommentDetailItemControl.InputData idata = new CommentDetailItemControl.InputData();
                idata.Header = this.FData.HeaderControl;
                idata.CIManager = this.FData.CIManager;

                this.Form.commentDetailItemControl1.InitControl(idata);
            }

            //Attachmentタブの初期化
            {
                CommentDetailAttachmentControl.InputData idata = new CommentDetailAttachmentControl.InputData();
                idata.ComItem = this.FData.InputData.SrcItem;
                f.commentDetailAttachmentControl1.InitControl(idata);
            }
        }


    }
}
