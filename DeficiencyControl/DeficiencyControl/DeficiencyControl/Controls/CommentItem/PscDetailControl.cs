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
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Controls.CommentItem
{
    /// <summary>
    /// PSC 詳細コントロール
    /// </summary>
    public partial class PscDetailControl : BaseCommentItemDetailControl
    {
        public PscDetailControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// PSCControl詳細出力データ
        /// </summary>
        public class PscDetailControlOutputData : BaseCommentItemControlDetailData
        {
            /// <summary>
            /// 番号
            /// </summary>
            public int No = 0;

            /// <summary>
            /// Status
            /// </summary>
            public EStatus Status;
            /// <summary>
            /// 入力者
            /// </summary>
            public MsUser PIC = null;
            /// <summary>
            /// ActionCode一式
            /// </summary>
            public List<DcActionCodeHistory> ActionCodeList = new List<DcActionCodeHistory>();

            //Action taken By Vessel 本船対応
            /// <summary>
            /// 本船対応
            /// </summary>
            public string ActionTakenByVessel = "";
            /// <summary>
            /// 本船対応添付資料
            /// </summary>
            public List<DcAttachment> ActionTakenByVesselAttachmentList = new List<DcAttachment>();


            //Class Involved NK窓口
            /// <summary>
            /// NK窓口 部署名
            /// </summary>
            public string NkDepertment = "";
            /// <summary>
            /// NK窓口　氏名
            /// </summary>
            public string NkName = "";
            /// <summary>
            /// NK対応コメント
            /// </summary>
            public string ClassInvolved = "";
            /// <summary>
            /// NK対応添付資料
            /// </summary>
            public List<DcAttachment> ClassInvolvedAttachmentList = new List<DcAttachment>();

            //Deficiency 指摘事項コード
            /// <summary>
            /// 指摘事項コード
            /// </summary>
            public MsDeficiencyCode DeficiencyCode = null;
            /// <summary>
            /// 指摘事項
            /// </summary>
            public string Deficiency = "";
            /// <summary>
            /// 原因
            /// </summary>
            public string CauseOfDeficiency = "";

            //Action taken By Company 会社対応
            /// <summary>
            /// 会社対応
            /// </summary>
            public string ActionTakenByCompnay = "";
            /// <summary>
            /// 会社対応添付資料
            /// </summary>
            public List<DcAttachment> ActionTakenByCompanyAttachmentList = new List<DcAttachment>();

            //Corrective Action 是正措置
            /// <summary>
            /// 是正措置
            /// </summary>
            public string CorrectiveAction = "";
            /// <summary>
            /// 是正措置添付資料
            /// </summary>
            public List<DcAttachment> CorrectiveActionAttachmentList = new List<DcAttachment>();

            /// <summary>
            /// 備考
            /// </summary>
            public string Remarks = "";
   

        }


        /// <summary>
        /// これのデータ
        /// </summary>
        public class PscDetailControlData
        {
            /// <summary>
            /// 管理
            /// </summary>
            public PscInspectionManager CIManager = null;

            /// <summary>
            /// 元データ
            /// </summary>
            public PSCInspectionData SrcData = null;
        }


        /// <summary>
        /// ロジック
        /// </summary>
        private PscDetailControlLogic Logic = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        private PscDetailControlData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //ユーザー
            ControlItemCreator.CreateUser(this.singleLineComboUser);

            //指摘事項コード
            this.deficiencyCodeSelectControl1.InitControl();
            
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
                                      this.singleLineComboUser,
                                      //this.textBoxDeficiency,
                                      

                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。
                bool defcret= this.deficiencyCodeSelectControl1.CheckError(chcol);
                if (defcret == false)
                {
                    this.ErList.Add(this.deficiencyCodeSelectControl1);
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
        /// データの取得 
        /// </summary>
        /// <returns></returns>
        public override BaseCommentItemControlDetailData GetInputData()
        {
            PscDetailControlOutputData ans = new PscDetailControlOutputData();

            //番号
            ans.No = this.DeficinecyNo;

            //Status
            ans.Status = this.statusSelectControl1.GetSelectData();

            //PIC
            ans.PIC = this.singleLineComboUser.SelectedItem as MsUser;

            //ActionCode
            ans.ActionCodeList = this.Logic.GetActionCodeList();

            {
                //指摘事項
                //DefficiencyCode
                ans.DeficiencyCode = this.deficiencyCodeSelectControl1.GetSelectData();

                //Deficiency
                ans.Deficiency = this.textBoxDeficiency.Text.Trim();

                //原因
                ans.CauseOfDeficiency = this.textBoxCauseOfDeficiency.Text.Trim();
            }
            {
                //本船対応

                //対応内容
                ans.ActionTakenByVessel = this.textBoxActionTakenByVessel.Text.Trim();

                //添付資料
                ans.ActionTakenByVesselAttachmentList = this.GetFileAttachmentList(this.fileViewControlExActionTakenByVessel, EAttachmentType.CI_ActionTakenByVessel);
            }
            {
                //会社対応
                //対応内容
                ans.ActionTakenByCompnay = this.textBoxActionTakenByCompany.Text.Trim();

                //添付資料
                ans.ActionTakenByCompanyAttachmentList = this.GetFileAttachmentList(this.fileViewControlExActionTakenByCompany, EAttachmentType.CI_ActionTakenByCompany);
            }
            {
                //Class Involved

                //部署
                ans.NkDepertment = this.textBoxNkDeportment.Text.Trim();

                //氏名
                ans.NkName = this.textBoxNkName.Text.Trim();

                //コメント
                ans.ClassInvolved = this.textBoxClassInvolved.Text.Trim();

                //添付資料
                ans.ClassInvolvedAttachmentList = this.GetFileAttachmentList(this.fileViewControlExClassInvolved, EAttachmentType.CI_ClassInvolved);
            }
            {
                //Corrective Action
                //是正結果
                ans.CorrectiveAction = this.textBoxCorrectiveAction.Text.Trim();

                //添付資料
                ans.CorrectiveActionAttachmentList = this.GetFileAttachmentList(this.fileViewControlExCorrectiveAction, EAttachmentType.CI_CorrectiveAction);

            }

            //Remarks
            ans.Remarks = this.textBoxRemarks.Text.Trim();

            
            return ans;
        }



        /// <summary>
        /// データの表示処理
        /// </summary>
        private void DispData()
        {
            DcCiPscInspection psc = this.FData.SrcData.PscInspection;
            DBDataCache db = DcGlobal.Global.DBCache;


            //No
            this.textBoxNo.Text = psc.deficinecy_no.ToString();
            this.SetDeficiencyNo(psc.deficinecy_no);    //自分の番号をControlに設定する

            //Status
            this.statusSelectControl1.DispControl((EStatus)psc.status_id);

            //PIC
            {
                MsUser user = db.GetMsUser(psc.ms_user_id);
                if (user != null)
                {
                    this.singleLineComboUser.Text = user.ToString();
                }
            }

            //ActionCodeの表示
            foreach (DcActionCodeHistory ac in this.FData.SrcData.ActionCodeHistoryList)
            {
                this.Logic.AddActionCode(ac);
            }

            
                        
            #region Deficinecy 指摘事項
            {
                //Code
                MsDeficiencyCode code = db.GetMsDeficiencyCode(psc.deficiency_code_id);
                this.deficiencyCodeSelectControl1.DispData(code);


                //Deficiency
                this.textBoxDeficiency.Text = psc.deficiency;

                //Cause of deficiency
                this.textBoxCauseOfDeficiency.Text = psc.cause_of_deficiency;

            }
            #endregion

            
            //Action taken by Vessel 本船対応
            this.textBoxActionTakenByVessel.Text = psc.action_taken_by_vessel;
            
            //Action taken by company 会社対応
            this.textBoxActionTakenByCompany.Text = psc.action_taken_by_company;

            #region Class Involved NK対応
            //部署
            this.textBoxNkDeportment.Text = psc.class_involved_nk_department;

            //氏名
            this.textBoxNkName.Text = psc.class_involved_nk_name;

            //コメント
            this.textBoxClassInvolved.Text = psc.class_involved;
            #endregion

            //Corrective action 是正結果
            this.textBoxCorrectiveAction.Text = psc.corrective_action;

            //Remark
            this.textBoxRemarks.Text = psc.item_remarks;
                        

            #region 添付ファイル            
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.CI_ActionTakenByVessel, this.fileViewControlExActionTakenByVessel);
                dic.Add(EAttachmentType.CI_ActionTakenByCompany, this.fileViewControlExActionTakenByCompany);
                dic.Add(EAttachmentType.CI_ClassInvolved, this.fileViewControlExClassInvolved);
                dic.Add(EAttachmentType.CI_CorrectiveAction, this.fileViewControlExCorrectiveAction);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, this.FData.SrcData.AttachmentList);
            #endregion

        }
        //-----------------------------------------------------------------------------------------------
        /// <summary>
        /// Deficiency番号の設定
        /// </summary>
        /// <param name="no"></param>
        public override void SetDeficiencyNo(int no)
        {
            base.SetDeficiencyNo(no);

            this.textBoxNo.Text = this.DeficinecyNo.ToString();
        }


        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">表示PSCInspectionManager 新規null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            //初期クラス作成
            this.FData = new PscDetailControlData();
            this.Logic = new PscDetailControlLogic(this, this.FData);

            this.FData.CIManager = inputdata as PscInspectionManager;
            


            //画面初期化
            this.InitDisplayControl();
            
            if (this.FData.CIManager == null)
            {
                //新規登録

                //初期登録
                this.Logic.AddActionCode(null);
                this.singleLineComboUser.Text = DcGlobal.Global.LoginMsUser.ToString();
            }
            else
            {
                //データ取得
                this.FData.SrcData = this.FData.CIManager.SrcData;

                //更新処理

                //初期表示をせよ
                this.DispData();
            }

            return true;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PscDetailControl_Load(object sender, EventArgs e)
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
                                              this.fileViewControlExActionTakenByVessel,
                                              this.fileViewControlExActionTakenByCompany,
                                              this.fileViewControlExClassInvolved,
                                              this.fileViewControlExCorrectiveAction,
                                              
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
        /// 対象のファイルが選択されたとき
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool fileViewControlEx_FileItemSelected(string text, object data)
        {
            return this.FileViewItemSelect(text, data, this.saveFileDialog1);
        }


        /// <summary>
        /// ActionCode追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddActionCode_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonAddActionCode_Click");

            //ActionCodeADD
            this.Logic.AddActionCode();
        }

        /// <summary>
        /// ActionCode削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteActionCode_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDeleteActionCode_Click");

            ComLogic.DeleteChildControl(this.flowLayoutPanelActionCode);
        }

    }
}
