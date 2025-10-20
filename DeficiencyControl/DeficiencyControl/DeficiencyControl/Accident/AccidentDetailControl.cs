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
    /// Accident詳細コントロール
    /// </summary>
    public partial class AccidentDetailControl : BaseControl
    {
        public AccidentDetailControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 出力データ
        /// </summary>
        public class AccidentDetailControlOutputData
        {
            public string AccidentReportNo = "";
            public MsAccidentImportance Importance = null;
            public EAccidentStatus Status = EAccidentStatus.Pending;

            public string Accident = "";
            public List<DcAttachment> AccidentAttachmentList = new List<DcAttachment>();


            public string SpotReport = "";
            public List<DcAttachment> SpotReportAttachmentList = new List<DcAttachment>();

            /// <summary>
            /// 進捗一式
            /// </summary>
            public List<AccidentProgressData> ProgressList = new List<AccidentProgressData>();

            public string CauseOfAccident = "";
            public List<DcAttachment> CauseOfAccidentAttachmentList = new List<DcAttachment>();


            public string PreventiveAction = "";
            public List<DcAttachment> PreventiveActionAttachmentList = new List<DcAttachment>();

            /// <summary>
            /// 報告書提出先一式
            /// </summary>
            public List<AccidentReportsData> ReportsList = new List<AccidentReportsData>();

            public string Influence = "";
            public string Remarks = "";
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        public class AccidentDetailControlData
        {
            /// <summary>
            /// 元データ
            /// </summary>
            public AccidentData SrcData = null;
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        private AccidentDetailControlData FData = null;


        /// <summary>
        /// ロジック
        /// </summary>
        private AccidentDetailControlLogic Logic = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //AcicdentImportance
            ControlItemCreator.CreateMsAccidentImportance(this.comboBoxAccidentImportance);
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
                anslist = fcon.CreateInsertUpdateAttachmentList(this.FData.SrcData.AttachmentList, atype);
            }

            return anslist;
        }


        /// <summary>
        /// 進捗の追加
        /// </summary>
        /// <param name="prog">新規=null</param>
        private void AddProgress(AccidentProgressData prog = null)
        {
            FlowLayoutPanel fpane = this.flowLayoutPanelProgress;

            //コントロール作成と初期化
            AccidentProgressControl con = new AccidentProgressControl();
            {
                con.InitControl(prog);                
            }

            int sno = 0;
            if (prog != null)
            {
                //sno = 
            }

            //ADD
            fpane.Controls.Add(con);
            fpane.Controls.SetChildIndex(con, sno);

            con.TabIndex = 0;
        }

        /// <summary>
        /// 進捗の削除
        /// </summary>
        private void DeleteProgress()
        {
            ComLogic.DeleteChildControl(this.flowLayoutPanelProgress);
        }

        /// <summary>
        /// 報告書提出のADD
        /// </summary>
        /// <param name="acrep"></param>
        private void AddReports(AccidentReportsData acrep = null)
        {
            FlowLayoutPanel fpane = this.flowLayoutPanelReports;

            //コントロール作成と初期化
            AccidentReportsControl con = new AccidentReportsControl();
            {
                con.InitControl(acrep);
            }

            int sno = 0;
            if (acrep != null)
            {
                sno = acrep.Reports.order_no;
            }

            //ADD
            fpane.Controls.Add(con);
            fpane.Controls.SetChildIndex(con, sno);

            con.TabIndex = 0;
        }

        /// <summary>
        /// 報告書提出の削除
        /// </summary>
        private void DeleteReports()
        {
            ComLogic.DeleteChildControl(this.flowLayoutPanelReports);
        }


        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            DcAccident ac = this.FData.SrcData.Accident;

            //ReportNo
            this.textBoxAccidentReportNo.Text = ac.accident_report_no;

            //重要度
            {
                MsAccidentImportance ai = db.GetMsAccidentImportance(ac.accident_importance_id);
                if (ai != null)
                {
                    this.comboBoxAccidentImportance.SelectedItem = ai;
                }
            }

            //Status
            EStatus st = EStatus.Pending;
            if (ac.accident_status_id == (int)EAccidentStatus.Complete)
            {
                st = EStatus.Complete;
            }
            this.statusSelectControl1.DispControl(st);


            //事故概要
            this.textBoxAccident.Text = ac.accident;

            //現場報告
            this.textBoxSpotReport.Text = ac.spot_report;

            //進捗
            foreach(AccidentProgressData pg in this.FData.SrcData.ProgressList)
            {
                this.AddProgress(pg);
            }

            //調査結果
            //原因
            this.textBoxCauseOfAccident.Text = ac.cause_of_accident;

            //再発防止対策
            this.textBoxPreventiveAction.Text = ac.preventive_action;
            
            //報告書
            foreach (AccidentReportsData ar in this.FData.SrcData.ReportsList)
            {
                this.AddReports(ar);
            }

            //運行への影響
            this.textBoxInfluence.Text = ac.influence;

            //備考
            this.textBoxRemarks.Text = ac.remarks;


            #region 添付ファイル
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.AC_Accident, this.fileViewControlExAccident);
                dic.Add(EAttachmentType.AC_SpotReport, this.fileViewControlExSpotReport);
                dic.Add(EAttachmentType.AC_CauseOfAccident, this.fileViewControlExCauseOfAccident);
                dic.Add(EAttachmentType.AC_PreventiveAction, this.fileViewControlExPreventiveAction);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, this.FData.SrcData.AttachmentList);
            #endregion
        }

        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        /// <summary>
        /// 入力エラーチェック
        /// </summary>
        /// <param name="chcol">色変更可否</param>
        /// <returns></returns>
        public override bool CheckError(bool chcol)
        {

            try
            {
                this.ErList = new List<Control>();

                //入力チェック
                Control[] ckvec = {
                                      //this.textBoxAccident,

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。
                {
                    //進捗チェック
                    List<AccidentProgressControl> proconlist = this.Logic.GetProgressControlList();
                    proconlist.ForEach(x => 
                    { 
                        bool f = x.CheckError(true);
                        if (f == false)
                        {
                            this.ErList.Add(x);
                        }
                    });

                    //報告チェック
                    List<AccidentReportsControl> repconlist = this.Logic.GetReportsControlList();
                    repconlist.ForEach(x =>
                    {
                        bool f = x.CheckError(true);
                        if (f == false)
                        {
                            this.ErList.Add(x);
                        }
                    });
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
        public AccidentDetailControlOutputData GetInputData()
        {
            AccidentDetailControlOutputData ans = new AccidentDetailControlOutputData();

            //
            ans.AccidentReportNo = this.textBoxAccidentReportNo.Text;
            ans.Importance = this.comboBoxAccidentImportance.SelectedItem as MsAccidentImportance;

            //Status Control使いまわしなのでEStatusの変換を行う
            ans.Status = EAccidentStatus.Pending;
            EStatus st = this.statusSelectControl1.GetSelectData();
            if (st == EStatus.Complete)
            {
                ans.Status = EAccidentStatus.Complete;
            }
            
            //事故概要
            ans.Accident = this.textBoxAccident.Text.Trim();
            ans.AccidentAttachmentList = this.GetFileAttachmentList(this.fileViewControlExAccident, EAttachmentType.AC_Accident);

            //現場報告
            ans.SpotReport = this.textBoxSpotReport.Text.Trim();
            ans.SpotReportAttachmentList = this.GetFileAttachmentList(this.fileViewControlExSpotReport, EAttachmentType.AC_SpotReport);

            //進捗
            ans.ProgressList = this.Logic.GetProgressInput();            


            //調査結果            
            ans.CauseOfAccident = this.textBoxCauseOfAccident.Text.Trim();
            ans.CauseOfAccidentAttachmentList = this.GetFileAttachmentList(this.fileViewControlExCauseOfAccident, EAttachmentType.AC_CauseOfAccident);

            ans.PreventiveAction = this.textBoxPreventiveAction.Text.Trim();
            ans.PreventiveActionAttachmentList = this.GetFileAttachmentList(this.fileViewControlExPreventiveAction, EAttachmentType.AC_PreventiveAction);

            //報告書提出先
            ans.ReportsList = this.Logic.GetReportsInput();

            //運航への影響
            ans.Influence = this.textBoxInfluence.Text.Trim();

            //備考
            ans.Remarks = this.textBoxRemarks.Text.Trim();

            return ans;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">AccidentData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new AccidentDetailControlData();
            this.Logic = new AccidentDetailControlLogic(this, this.FData);
            this.FData.SrcData = inputdata as AccidentData;
            

            //画面初期化
            this.InitDisplayControl();
            

            if (this.FData.SrcData == null)
            {
                //新規

                //最初のADDをしておく
                this.AddProgress();
                this.AddReports();

            }
            else
            {
                //更新

                //データ表示
                this.DispData();
            }


            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentDetailControl_Load(object sender, EventArgs e)
        {
            
        }


        /// <summary>
        /// 添付ファイル選択がクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttachment_Click(object sender, EventArgs e)
        {
            //Buttonのタグと一致させること。
            FileViewControlEx[] contvec = {
                                              this.fileViewControlExAccident,
                                              this.fileViewControlExSpotReport,
                                              this.fileViewControlExCauseOfAccident,
                                              this.fileViewControlExPreventiveAction,
                                              
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
        /// 進捗追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProgressAdd_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonProgressAdd_Click");

            //進捗追加
            this.AddProgress();
        }

        /// <summary>
        /// 進捗削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProgressDelete_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonProgressDelete_Click");

            //確認
            DialogResult dret = DcMes.ShowMessage(this.ParentForm, EMessageID.MI_29, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != DialogResult.Yes)
            {
                return;
            }

            this.DeleteProgress();
        }

        /// <summary>
        /// Reports追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReportsAdd_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonReportsAdd_Click");


            this.AddReports(null);
        }

        /// <summary>
        /// Reports削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReportsDelete_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonReportsDelete_Click");


            //確認
            DialogResult dret = DcMes.ShowMessage(this.ParentForm, EMessageID.MI_30, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dret != DialogResult.Yes)
            {
                return;
            }

            this.DeleteReports();
        }

        /// <summary>
        /// ファイルが選択されたとき
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
