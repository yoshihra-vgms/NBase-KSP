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

using DeficiencyControl.Util;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;



namespace DeficiencyControl.Accident
{
    /// <summary>
    /// 事故トラブル詳細画面 Attachmentタブ
    /// </summary>
    public partial class AccidentDetailAttachmentControl : BaseControl
    {
        public AccidentDetailAttachmentControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// このクラスのデータ
        /// </summary>
        class AccidentDetailAttachmentControlData
        {
            /// <summary>
            /// 元データ
            /// </summary>
            public AccidentData SrcData = null;

            /// <summary>
            /// 表示データ一式
            /// </summary>
            public List<DcAttachment> AttachmentList = new List<DcAttachment>();
        }


        /// <summary>
        /// 之のデータ
        /// </summary>
        private AccidentDetailAttachmentControlData FData = null;

        /// <summary>
        /// グリッド管理
        /// </summary>
        private AccidentDetailAttachmentGrid Grid = null;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">AccidentDataデータ</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {

            this.FData = new AccidentDetailAttachmentControlData();
            this.FData.SrcData = inputdata as AccidentData;


            //検索
            this.FData.AttachmentList = SvcManager.SvcMana.DcAttachment_GetARecrodsAllByAccidentID(this.FData.SrcData.Accident.accident_id);


            //グリッド初期化と表示
            this.Grid = new AccidentDetailAttachmentGrid(this.dataGridViewAttachment);
            this.Grid.DispData(this.FData.AttachmentList);


            this.dataCountControl1.DataCount = this.FData.AttachmentList.Count;

            return true;

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentDetailAttachmentControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// クリックされたとき
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
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_31);
                return;
            }
        }
    }
}
