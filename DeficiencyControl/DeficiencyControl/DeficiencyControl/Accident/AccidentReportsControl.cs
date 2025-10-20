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
using DeficiencyControl.Controls;


namespace DeficiencyControl.Accident
{
    /// <summary>
    /// Accident報告書提出先コントロール
    /// </summary>
    public partial class AccidentReportsControl : BaseChildItemControl
    {
        public AccidentReportsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 元データ
        /// </summary>
        class AccidentReportsControlData
        {
            public AccidentReportsData SrcData = null;
        }

        /// <summary>
        /// 画面データ
        /// </summary>
        private AccidentReportsControlData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <param name="chcol"></param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {

            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      //this.textBoxReports,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。


                //エラーがないなら終わり
                if (this.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputErrorException");

            }
            catch (Exception e)
            {
                //エラーの表示を行う
                if (chcol == true)
                {
                    this.DispError();
                }

            }

            return false;
        }


        /// <summary>
        /// 入力データの取得
        /// </summary>
        /// <param name="no">順番</param>
        /// <returns></returns>
        public AccidentReportsData GetInputData(int no)
        {
            AccidentReportsData ans = new AccidentReportsData();
            ans.Reports = new DcAccidentReports();
            if (this.FData.SrcData != null)
            {
                ans.Reports = (DcAccidentReports)this.FData.SrcData.Reports.Clone();
            }

            DcAccidentReports rep = ans.Reports;

            //提出先
            rep.reports = this.textBoxReports.Text.Trim();
            rep.order_no = no;
            

            //添付ファイル
            if (this.FData.SrcData == null)
            {
                ans.AttachmentList = this.fileViewControlExReports.CreateInsertUpdateAttachmentList(null, EAttachmentType.AC_Reports);
            }
            else
            {
                ans.AttachmentList = this.fileViewControlExReports.CreateInsertUpdateAttachmentList(this.FData.SrcData.AttachmentList, EAttachmentType.AC_Reports);
            }

            return ans;
        }


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            DcAccidentReports rep = this.FData.SrcData.Reports;


            //提出先
            this.textBoxReports.Text = rep.reports;

            #region 添付ファイル
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.AC_Reports, this.fileViewControlExReports);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, this.FData.SrcData.AttachmentList);
            #endregion


        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Controlの初期化
        /// </summary>
        /// <param name="inputdata">AccidentReportsData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new AccidentReportsControlData();
            this.FData.SrcData = inputdata as AccidentReportsData;

            if (this.FData.SrcData == null)
            {
                //新規
            }
            else
            {
                //更新
                this.DispData();
            }

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentReportsControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添付ファイルADD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttachmentReports_Click(object sender, EventArgs e)
        {
            //ファイルのADD
            ComLogic.OpenFileAttachment(this.openFileDialog1, this.fileViewControlExReports);
        }

        /// <summary>
        /// ファイルの取得
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool fileViewControlExReports_FileItemSelected(string text, object data)
        {

            return this.FileViewItemSelect(text, data, this.saveFileDialog1);

        }
    }
}
