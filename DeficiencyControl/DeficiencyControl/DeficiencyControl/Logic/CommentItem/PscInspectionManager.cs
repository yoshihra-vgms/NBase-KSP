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
using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;
using CIsl.DB.WingDAC;
using CIsl.DB;


namespace DeficiencyControl.Logic.CommentItem
{
    /// <summary>
    /// PSCコメント管理
    /// </summary>
    public class PscInspectionManager : BaseCommentItemManager
    {

        public PscInspectionManager() : base(ECommentItemKind.PSC_Inspection)
        {
        }

        #region メンバ変数

        /// <summary>
        /// 元データ　null=新規
        /// </summary>
        public PSCInspectionData SrcData = null;

        #endregion


        /// <summary>
        /// 指摘事項件数の取得
        /// </summary>
        public override int DeficiencyCount
        {
            get
            {
                return this.SrcData.PscInspection.deficinecy_count;
            }
        }



        /// <summary>
        /// ActionCodeのキーワード検索文字列の作成
        /// </summary>
        /// <param name="aclist">元ネタ</param>
        /// <returns></returns>
        private string CreateKeywordStringActionCode(List<DcActionCodeHistory> aclist)
        {
            string ans = "";

            foreach (DcActionCodeHistory ac in aclist)
            {
                //ActionCodeの検索文字列の作成                
                MsActionCode msac = DcGlobal.Global.DBCache.GetMsActionCode(ac.action_code_id);
                if (msac != null)
                {
                    ans += msac.ToString();
                }                
                ans += ac.action_code_text;
            }

            return ans;
        }

        /// <summary>
        /// 添付ファイルキーワード検索文字列の作成
        /// </summary>
        /// <param name="filelist"></param>
        /// <returns></returns>
        private string CreateKeywordStringAttachment(List<DcAttachment> filelist)
        {
            string ans = "";

            foreach (DcAttachment at in filelist)
            {
                ans += at.filename;
            }

            return ans;
        }

        /// <summary>
        /// データの作成
        /// </summary>
        /// <param name="srcdata">元ネタ</param>
        /// <param name="head">ヘッダーデータ</param>
        /// <param name="detail">詳細データ 無いとき=null</param>
        /// <returns></returns>
        public PSCInspectionData CreatePSCData(PSCInspectionData srcdata, PscHeaderControl.PscHeaderControlOutputData head, PscDetailControl.PscDetailControlOutputData detail)
        {
            const int EVAL = BaseDac.EVal;

            string keyword = "";

            
            PSCInspectionData ans = new PSCInspectionData();
            ans.AttachmentList = new List<DcAttachment>();
            ans.ActionCodeHistoryList = new List<DcActionCodeHistory>();

            ans.PscInspection = new DcCiPscInspection();            
            if (srcdata != null)
            {
                ans.PscInspection = (DcCiPscInspection)srcdata.PscInspection.Clone();
            }
            DcCiPscInspection psc = ans.PscInspection;


            #region ヘッダーデータの取得

            //日付 date
            psc.date = head.Date;

            //Kind種別
            psc.item_kind_id = EVAL;
            if (head.Kind != null)
            {
                psc.item_kind_id = head.Kind.item_kind_id;
                keyword += head.Kind.ToString();
            }

            //件数
            psc.deficinecy_count = head.DeficinecyCount;

            //船名
            psc.ms_vessel_id = EVAL;
            if (head.Vessel != null)
            {
                psc.ms_vessel_id = head.Vessel.ms_vessel_id;
                keyword += head.Vessel.ToString();
            }

            //VesselType ms_vessel_type_id
            psc.ms_crew_matrix_type_id = EVAL;
            if (head.CrewMatrixType != null)
            {
                psc.ms_crew_matrix_type_id = head.CrewMatrixType.ms_crew_matrix_type_id;
                keyword += head.CrewMatrixType.ToString();
            }

            //Port 港
            psc.ms_basho_id = "";
            if (head.Port != null)
            {
                psc.ms_basho_id = head.Port.ms_basho_id;
                keyword += head.Port.ToString();
            }

            //国
            psc.ms_regional_code = "";
            if (head.Country != null)
            {
                psc.ms_regional_code = head.Country.ms_regional_code;
                keyword += head.Country.ToString();
            }

            //Attachmentはヘッダーなので親コメントの方に紐づきます。
            keyword += this.CreateKeywordStringAttachment(head.AttachmentReportRecord);

            //会社横展開
            psc.share_to_our_fleet = head.ShareToOurFleet;
            psc.share_to_our_fleet_date = head.ShareToOurFleetDate;

            //Remarks
            psc.comment_remarks = head.Remarks;
            keyword += head.Remarks;

            #endregion

            //詳細が無いなら終わり
            if (detail == null)
            {
                ans.PscInspection.search_keyword = keyword;

                //その他最低限の情報を設定する。
                //ステータスは編集を有効にしたいため、とりあえずpendingとする
                ans.PscInspection.status_id = (int)EStatus.Pending;

                return ans;
            }

            #region 詳細データの取得

            //No
            psc.deficinecy_no = detail.No;

            //Status
            psc.status_id = (int)detail.Status;

            //PIC入力者
            psc.ms_user_id = "";
            if (detail.PIC != null)
            {
                psc.ms_user_id = detail.PIC.ms_user_id;
                keyword += detail.PIC.ToString();
            }

            //ActionCode
            ans.ActionCodeHistoryList.AddRange(detail.ActionCodeList);
            keyword += this.CreateKeywordStringActionCode(detail.ActionCodeList);

            //Deficiency code 指摘事項コード
            psc.deficiency_code_id = EVAL;
            if (detail.DeficiencyCode != null)
            {
                psc.deficiency_code_id = detail.DeficiencyCode.deficiency_code_id;
                keyword += detail.DeficiencyCode.ToString();
            }
            //指摘事項
            psc.deficiency = detail.Deficiency;
            keyword += detail.Deficiency;

            //原因
            psc.cause_of_deficiency = detail.CauseOfDeficiency;
            keyword += detail.CauseOfDeficiency;

            //Action taken by vessel 本船対応
            psc.action_taken_by_vessel = detail.ActionTakenByVessel;
            ans.AttachmentList.AddRange(detail.ActionTakenByVesselAttachmentList);

            keyword += detail.CauseOfDeficiency;
            keyword += this.CreateKeywordStringAttachment(detail.ActionTakenByVesselAttachmentList);

            //Action taken by company
            psc.action_taken_by_company = detail.ActionTakenByCompnay;
            ans.AttachmentList.AddRange(detail.ActionTakenByCompanyAttachmentList);

            keyword += detail.ActionTakenByCompnay;
            keyword += this.CreateKeywordStringAttachment(detail.ActionTakenByCompanyAttachmentList);

            //Class Involved
            //部署
            psc.class_involved_nk_department = detail.NkDepertment;
            keyword += detail.NkDepertment;

            //氏名
            psc.class_involved_nk_name = detail.NkName;
            keyword += detail.NkName;

            //コメント
            psc.class_involved = detail.ClassInvolved;
            ans.AttachmentList.AddRange(detail.ClassInvolvedAttachmentList);

            keyword += detail.ClassInvolved;
            keyword += this.CreateKeywordStringAttachment(detail.ClassInvolvedAttachmentList);
            
            //CorrectiveAction
            psc.corrective_action = detail.CorrectiveAction;
            ans.AttachmentList.AddRange(detail.CorrectiveActionAttachmentList);

            keyword += detail.CorrectiveAction;
            keyword += this.CreateKeywordStringAttachment(detail.CorrectiveActionAttachmentList);

            //Remarks
            psc.item_remarks = detail.Remarks;
            keyword += detail.Remarks;

            

            #endregion

            ans.AttachmentList.ForEach(x => keyword += x.filename);
            ans.PscInspection.search_keyword = keyword;
            return ans;
        }


        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <param name="headcon">ヘッダーコントロール</param>
        /// <param name="dcllist">詳細コントロール一式</param>
        /// <param name="anshead"></param>
        /// <param name="anslist"></param>
        private void GetInputData(BaseCommentItemHeaderControl headcon, List<BaseCommentItemDetailControl> dcllist, out CommentData anshead, out List<PSCInspectionData> anslist)
        {            

            //ヘッダーの変換と入力データの取得
            PscHeaderControl head = headcon as PscHeaderControl;
            PscHeaderControl.PscHeaderControlOutputData headdata = head.GetInputData() as PscHeaderControl.PscHeaderControlOutputData;


            //詳細リストの作成
            anslist = new List<PSCInspectionData>();

            //入力データ一式の取得
            if (dcllist.Count <= 0)
            {
                //詳細がないならヘッダーだけでデータを作成する
                PSCInspectionData ans = this.CreatePSCData(this.SrcData, headdata, null);
                anslist.Add(ans);
            }
            else
            {
                foreach (BaseCommentItemDetailControl dc in dcllist)
                {
                    PscDetailControl detail = dc as PscDetailControl;
                    PscDetailControl.PscDetailControlOutputData detaildata = detail.GetInputData() as PscDetailControl.PscDetailControlOutputData;

                    PSCInspectionData ans = this.CreatePSCData(this.SrcData, headdata, detaildata);
                    anslist.Add(ans);

                }
            }


            //ヘッダーデータ作成
            {
                anshead = new CommentData();                
                anshead.Comment = new DcComment();
                if (this.SrcData != null)
                {
                    anshead.Comment = (DcComment)this.SrcData.ParentData.Comment.Clone();                    
                }

                //添付ファイルのADD
                anshead.AttachmentList = new List<DcAttachment>();
                anshead.AttachmentList.AddRange(headdata.AttachmentReportRecord);
                
            }
        }


















        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="comment_item_id"></param>
        public override void Init(int comment_item_id)
        {
            if (comment_item_id == DcCiPscInspection.EVal)
            {
                //新規
            }
            else
            {
                //更新

                //データの取得
                this.SrcData = SvcManager.SvcMana.PSCInspectionData_GetDataByCommentItemID(comment_item_id);

            }
        }


        /// <summary>
        /// Completeか否かをチェックする
        /// </summary>
        /// <returns></returns>
        public override bool CheckComplete()
        {
        //    if (this.SrcData.PscInspection.status_id == (int)EStatus.Complete)
        //    {
        //        return true;
        //    }

        //    return false;

            //
            return this.SrcData.CheckComplete();
            
        }

        /// <summary>
        /// データの挿入処理
        /// </summary>
        /// <param name="headcon">ヘッダーコントロール</param>
        /// <param name="dcllist">詳細一式</param>
        public override void InsertData(BaseCommentItemHeaderControl headcon, List<BaseCommentItemDetailControl> dcllist)
        {
            DcLog.WriteLog(this, "InsertData");

            //エラーチェック
            bool ckret = this.CheckDataError(headcon, dcllist);
            if (ckret == false)
            {
                throw new ControlInputErrorException("PSC CheckDataError FALSE");
            }


            //データ取得
            CommentData pardata = new CommentData();
            List<PSCInspectionData> psclist = new List<PSCInspectionData>();
            this.GetInputData(headcon, dcllist, out pardata, out psclist);
            
            //挿入処理
            bool insret = SvcManager.SvcMana.PSCInspectionData_InsertList(pardata, psclist, DcGlobal.Global.LoginMsUser);
            if (insret == false)
            {
                throw new Exception("PSCInspectionData_InsertList FALSE");
            }

        }


        /// <summary>
        /// データの更新処理
        /// </summary>
        /// <param name="headcon"></param>
        /// <param name="detailcon"></param>
        public override void UpdateData(BaseCommentItemHeaderControl headcon, BaseCommentItemDetailControl detailcon)
        {
            DcLog.WriteLog(this, "UpdateData");

            //エラーチェック
            bool ckret = this.CheckDataError(headcon, detailcon);
            if (ckret == false)
            {
                throw new ControlInputErrorException("PSC CheckDataError FALSE");
            }


            //データ取得
            CommentData pardata = new CommentData();    //更新する親
            List<PSCInspectionData> psclist = new List<PSCInspectionData>();

            List<BaseCommentItemDetailControl> detaillist = new List<BaseCommentItemDetailControl>();
            detaillist.Add(detailcon);
            this.GetInputData(headcon, detaillist, out pardata, out psclist);
            if (psclist.Count != 1)
            {
                throw new Exception("PSCInspectionData UpdateCount");
            }

            //更新処理            
            bool upret = SvcManager.SvcMana.PSCInspectionData_UpdateWithSister(pardata, psclist[0], DcGlobal.Global.LoginMsUser);
            if (upret == false)
            {
                throw new Exception("PSCInspectionData_UpdateWithSister FALSE");
            }
            
        }


        /// <summary>
        /// 削除処理
        /// </summary>
        public override void DeleteData()
        {
            DcLog.WriteLog(this, "DeleteData");

            if (this.SrcData == null)
            {
                throw new Exception("PSC SrcData NULL");
            }

            //削除
            bool ret = SvcManager.SvcMana.PSCInspectionData_Delete(this.SrcData.PscInspection.comment_item_id, DcGlobal.Global.LoginMsUser);
            if (ret == false)
            {
                throw new Exception("PSCInspectionData_Delete FALSE");
            }

            
        }
    }
}
