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


namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船詳細コントロール
    /// </summary>
    public partial class MoiDetailControl : BaseControl
    {
        public MoiDetailControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 出力データ
        /// </summary>
        public class MoiDetailControlOutputData
        {
            public int No;
            public EMoiStatus Status;
            public MsViqCode ViqCode = null;
            public MsViqNo ViqNo = null;
            public string Observation = "";

            public string RootCause = "";

            public bool Comment1stCheck = false;
            public string Comment1st = "";
            public List<DcAttachment> Comment1stAttachmentList = new List<DcAttachment>();

            public bool Comment2ndCheck = false;
            public string Comment2nd = "";
            public List<DcAttachment> Comment2ndAttachmentList = new List<DcAttachment>();


            public string PreventiveAction = "";
            public string SpecialNotes = "";
            
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class MoiDetailControlData
        {
            /// <summary>
            /// 元データ
            /// </summary>
            public MoiData SrcData = null;

        }

        

        /// <summary>
        /// データ出力
        /// </summary>
        private MoiDetailControlData FData = null;


        /// <summary>
        /// 指摘事項番号の設定
        /// </summary>
        public int ObservationNo
        {
            set
            {
                this.textBoxNo.Text = value.ToString();
            }
        }

        /// <summary>
        /// VIQ Versionの設定
        /// </summary>
        /// <param name="ver"></param>
        public void SetViqVersion(MsViqVersion ver)
        {
            if (ver != null)
            {
                this.viqCodeControl1.ViqVersion = ver;
            }
        }

        /// <summary>
        /// VIQ Versionの設定（変更イベント）
        /// </summary>
        /// <param name="ver"></param>
        public void ChangeViqVersion(MsViqVersion ver)
        {
            if (ver != null)
            {
                if (this.viqCodeControl1.ViqVersion != null)
                {
                    // VIQ Versionが変更されたときのみ、入れ替える
                    if (this.viqCodeControl1.ViqVersion.viq_version_id != ver.viq_version_id)
                    {
                        this.viqCodeControl1.ViqVersion = ver;
                        this.viqCodeControl1.InitControl(null);
                    }
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {
            this.viqCodeControl1.InitControl(null);
            this.viqCodeControl1.DataSelecDelegatetProc = this.ChangeSelectViq;

            ViqNoControl.InitData idata = new ViqNoControl.InitData();
            idata.Code = this.viqCodeControl1.GetSelectData();
            this.viqNoControl1.InitControl(idata);


            this.radioButtonComment1st.Checked = true;
        }


        /// <summary>
        /// VIQ Codeの選択が変更されたとき
        /// </summary>
        /// <param name="code"></param>
        private void ChangeSelectViq(MsViqCode code)
        {
            ViqNoControl.InitData idata = new ViqNoControl.InitData();
            idata.Code = code;
            this.viqNoControl1.InitControl(idata);
        }


        /// <summary>
        /// 対象添付ファイルの添付ファイル更新リストの取得
        /// </summary>
        /// <param name="fcon">コントロール</param>
        /// <param name="atype">対象添付ファイルタイプ</param>
        /// <returns></returns>
        private List<DcAttachment> GetFileAttachmentList(FileViewControlEx fcon, EAttachmentType atype)
        {
            List<DcAttachment> anslist = new List<DcAttachment>();
            //添付ファイル
            if (this.FData.SrcData == null)
            {
                anslist = fcon.CreateInsertUpdateAttachmentList(null, atype);
            }
            else
            {
                anslist = fcon.CreateInsertUpdateAttachmentList(this.FData.SrcData.Observation.AttachmentList, atype);
            }

            return anslist;
        }


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            MoiObservationData obdata = this.FData.SrcData.Observation;
            DcMoiObservation data = obdata.Observation;


            //No
            this.textBoxNo.Text = data.observation_no.ToString();

            //Status
            EStatus st = EStatus.Pending;
            if (data.moi_status_id == (int)EMoiStatus.Complete)
            {
                st = EStatus.Complete;
            }
            this.statusSelectControlMoiStatus.DispControl(st);


            {
                //VIQ Code
                MsViqCode vco = db.GetMsViqCode(data.viq_code_id);
                this.viqCodeControl1.InitControl(vco);

                //VIQ No
                MsViqNo vno = db.GetMsViqNo(data.viq_no_id);
                ViqNoControl.InitData idata = new ViqNoControl.InitData();
                idata.Code = vco;
                idata.SrcData = vno;
                this.viqNoControl1.InitControl(idata);
            }

            //指摘事項
            this.textBoxObservation.Text = data.observation;


            //--------------------------------------------------
            //--------------------------------------------------
            //根本原因
            this.textBoxRootCause.Text = data.root_cause;

            //1stComment
            {
                this.radioButtonComment1st.Checked = data.comment_1st_check;
                this.textBoxComment1st.Text = data.comment_1st;
            }

            //2ndComment
            {
                this.radioButtonComment2nd.Checked = data.comment_2nd_check;
                this.textBoxComment2nd.Text = data.comment_2nd;
            }

            //再発防止策
            this.textBoxPreventiveAction.Text = data.preventive_action;

            //特記事項
            this.textBoxSpecialNotes.Text = data.special_notes;

            #region 添付ファイル
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.MO_1stComemnt, this.fileViewControlExComment1st);
                dic.Add(EAttachmentType.MO_2ndtComemnt, this.fileViewControlExComment2nd);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, obdata.AttachmentList);
            #endregion

        }

        //------------------------------------------------------------------------------------------------------------------------------        

        /// <summary>
        /// 入力エラーチェック
        /// </summary>
        /// <param name="chcol">色変更可否</param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {

            try
            {
                bool ret = false;

                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {                                      
                                      this.textBoxObservation,                                      

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));



                //個別のチェックはここで。
                //VIQCode
                ret = this.viqCodeControl1.CheckError(chcol);
                if (ret == false)
                {
                    this.ErList.Add(this.viqCodeControl1);
                }
                
                //VIQNo
                ret = this.viqNoControl1.CheckError(chcol);
                if (ret == false)
                {
                    this.ErList.Add(this.viqNoControl1);
                }
      

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
        /// <returns></returns>
        public MoiDetailControlOutputData GetInputData()
        {
            MoiDetailControlOutputData ans = new MoiDetailControlOutputData();

            ans.No = Convert.ToInt32(this.textBoxNo.Text);

            //Status EStatusの変換を行う
            ans.Status = EMoiStatus.Pending;
            EStatus st = this.statusSelectControlMoiStatus.GetSelectData();
            if (st == EStatus.Complete)
            {
                ans.Status = EMoiStatus.Complete;
            }
            
            //VIQCode / No
            ans.ViqCode = this.viqCodeControl1.GetSelectData();
            ans.ViqNo = this.viqNoControl1.GetSelectData();

            //指摘事項
            ans.Observation = this.textBoxObservation.Text.Trim();

            //----------------------
            //根本原因
            ans.RootCause = this.textBoxRootCause.Text.Trim();

            //1stComment
            ans.Comment1stCheck = this.radioButtonComment1st.Checked;
            ans.Comment1st = this.textBoxComment1st.Text.Trim();
            ans.Comment1stAttachmentList = this.GetFileAttachmentList(this.fileViewControlExComment1st, EAttachmentType.MO_1stComemnt);

            //2ndComment
            ans.Comment2ndCheck = this.radioButtonComment2nd.Checked;
            ans.Comment2nd = this.textBoxComment2nd.Text.Trim();
            ans.Comment2ndAttachmentList = this.GetFileAttachmentList(this.fileViewControlExComment2nd, EAttachmentType.MO_2ndtComemnt);

            //
            ans.PreventiveAction = this.textBoxPreventiveAction.Text.Trim();
            ans.SpecialNotes = this.textBoxSpecialNotes.Text.Trim();

            return ans;
        }


        


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// コントロール初期化
        /// </summary>
        /// <param name="inputdata">MoiData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new MoiDetailControlData();
            this.FData.SrcData = inputdata as MoiData;

            //画面初期化
            this.InitDisplayControl();

            //
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiDetailControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ファイル添付ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttachment_Click(object sender, EventArgs e)
        {
            //Buttonのタグと一致させること。
            FileViewControlEx[] contvec = {
                                              this.fileViewControlExComment1st,
                                              this.fileViewControlExComment2nd
                                              
                                          };

            //ボタン？
            Button bu = sender as Button;
            if (bu == null)
            {
                return;
            }
            int no = Convert.ToInt32(bu.Tag);

            //ファイルのADD
            ComLogic.OpenFileAttachment(this.openFileDialog1, contvec[no]);
        }

        /// <summary>
        /// ファイルの選択
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool fileViewControlEx_FileItemSelected(string text, object data)
        {
            return this.FileViewItemSelect(text, data, this.saveFileDialog1);
        }
    }
}
