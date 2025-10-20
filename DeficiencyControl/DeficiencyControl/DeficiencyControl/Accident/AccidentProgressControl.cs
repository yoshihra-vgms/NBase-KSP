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
    /// Accident進捗コントロール
    /// </summary>
    public partial class AccidentProgressControl : BaseChildItemControl
    {
        public AccidentProgressControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        public class AccidentProgressControlData
        {
            /// <summary>
            /// 元データ
            /// </summary>
            public AccidentProgressData SrcData = null;
        }


        /// <summary>
        /// 画面データ
        /// </summary>
        private AccidentProgressControlData FData = null;

        
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
                                      //this.textBoxProgress,

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
        /// データの取得
        /// </summary>
        /// <returns></returns>
        public AccidentProgressData GetInputData()
        {
            AccidentProgressData ans = new AccidentProgressData();
            ans.Progress = new DcAccidentProgress();
            if (this.FData.SrcData != null)
            {
                ans.Progress = (DcAccidentProgress)this.FData.SrcData.Progress.Clone();
            }

            //進捗の入力を取得
            {
                DcAccidentProgress prog = ans.Progress;

                prog.date = this.dateTimePickerDate.Value.Date;
                prog.progress = this.textBoxProgress.Text.Trim();
            }

            //添付ファイルの取得
            if (this.FData.SrcData == null)
            {
                ans.AttachmentList = this.fileViewControlExProgress.CreateInsertUpdateAttachmentList(null, EAttachmentType.AC_Progress);
            }
            else
            {
                ans.AttachmentList = this.fileViewControlExProgress.CreateInsertUpdateAttachmentList(this.FData.SrcData.AttachmentList, EAttachmentType.AC_Progress);
            }

            return ans;
        }


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            DcAccidentProgress prog = this.FData.SrcData.Progress;

            //更新日
            this.dateTimePickerDate.Value = prog.date;

            //進捗
            this.textBoxProgress.Text = prog.progress;

            #region 添付ファイル
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.AC_Progress, this.fileViewControlExProgress);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, this.FData.SrcData.AttachmentList);
            #endregion


        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inputdata">AccidentProgressData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {

            this.FData = new AccidentProgressControlData();
            this.FData.SrcData = inputdata as AccidentProgressData;

            
            if (this.FData.SrcData == null)
            {
                //新規
            }
            else
            {
                //更新
                //データの表示を行う
                this.DispData();
            }


            return true;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentProgressControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添付ファイルADDボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttachmentProgress_Click(object sender, EventArgs e)
        {
            //ファイルのADD
            ComLogic.OpenFileAttachment(this.openFileDialog1, this.fileViewControlExProgress);
        }


        /// <summary>
        /// 添付ファイルの選択
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool fileViewControlExProgress_FileItemSelected(string text, object data)
        {
            return this.FileViewItemSelect(text, data, this.saveFileDialog1);

        }
    }
}
