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
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Grid;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Controls
{
    /// <summary>
    /// コメント詳細 添付ファイルタブコントロール
    /// </summary>
    public partial class CommentDetailAttachmentControl : BaseControl
    {
        public CommentDetailAttachmentControl() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 入力データ
        /// </summary>
        public class InputData
        {
            public DcCommentItem ComItem = null;
        }

        /// <summary>
        /// データまとめ
        /// </summary>
        public class CommentDetailAttachmentControlData
        {
            /// <summary>
            /// 初期化するコメントアイテム
            /// </summary>
            public DcCommentItem ComItem = null;

            /// <summary>
            /// 表示しているデータ
            /// </summary>
            public List<DcAttachment> AttachmentList = null;
        }

        /// <summary>
        /// データ
        /// </summary>
        private CommentDetailAttachmentControlData FData = null;

        /// <summary>
        /// グリッド管理
        /// </summary>
        private CiDetailAttachmentGrid Grid = null;
        //////////////////////////////////////////////////////////////////////////////////////////////////

        //================================================================================================
        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">初期化データ</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            InputData indata = inputdata as InputData;

            this.FData = new CommentDetailAttachmentControlData();
            this.FData.ComItem = indata.ComItem;

            //検索
            this.FData.AttachmentList = SvcManager.SvcMana.DcAttachment_GetARecrodsAllByCommentItemID(this.FData.ComItem.comment_item_id);

            
            //グリッド初期化と表示
            this.Grid = new CiDetailAttachmentGrid(this.dataGridViewAttachment);
            this.Grid.DispData(this.FData.AttachmentList);

            //件数
            this.dataCountControl1.DataCount = this.FData.AttachmentList.Count;

            return true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentDetailAttachmentControl_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// セルが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAttachment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DcLog.WriteLog(this, "dataGridViewAttachment_CellDoubleClick");

            //選択物取得
            DcAttachment fdata = this.Grid.GetSelectDataObject() as DcAttachment;
            if (fdata == null)
            {
                DcLog.WriteLog("Select NULL");
                return;
            }

            try
            {
                //ファイルダウンロード
                ComLogic.DownloadSaveAttachment(this.ParentForm, fdata, this.saveFileDialog1);
            }
            catch (Exception ex)
            {
                DcLog.WriteLog(ex, "DownloadSaveAttachment FALSE");
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_7);
                return;
            }
        }
    }
}
