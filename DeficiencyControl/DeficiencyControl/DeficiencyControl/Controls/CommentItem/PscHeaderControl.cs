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
    /// PSCヘッダーコントロール
    /// </summary>
    public partial class PscHeaderControl : BaseCommentItemHeaderControl
    {
        public PscHeaderControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// PSC Headerコントロール出力データまとめ
        /// </summary>
        public class PscHeaderControlOutputData : BaseCommentItemControlHeaderData
        {
            public int DeficinecyCount = 0;
            public DateTime Date;
            public MsItemKind Kind = null;
            public MsVessel Vessel = null;
            public MsCrewMatrixType CrewMatrixType = null;
            public MsBasho Port = null;
            public MsRegional Country = null;
            public List<DcAttachment> AttachmentReportRecord = new List<DcAttachment>();
            public string Remarks = "";

            public bool ShareToOurFleet = false;
            public DateTime ShareToOurFleetDate;
        }

        

        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        /// <summary>
        /// 元データ
        /// </summary>
        private PSCInspectionData SrcData = null;

        /// <summary>
        /// 管理
        /// </summary>
        private PscInspectionManager CIManager = null;

        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Kind
            ControlItemCreator.CreateItemKind(this.comboBoxItemKind);

            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);

            //CrewMatrix
            ControlItemCreator.CreateCrewMatrixType(this.comboBoxCrewMatrix, true);

            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);

            //Country
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);
        }

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
                                      this.singleLineComboVessel,
                                      this.singleLineComboPort,
                                      this.singleLineComboCountry,

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
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public override BaseCommentItemControlHeaderData GetInputData()
        {
            PscHeaderControlOutputData ans = new PscHeaderControlOutputData();

            //Date
            ans.Date = this.dateTimePickerDate.Value.Date;

            //Kind
            ans.Kind = this.comboBoxItemKind.SelectedItem as MsItemKind;

            //Deficiency数
            ans.DeficinecyCount = Convert.ToInt32(this.numericUpDownDeficiencyCount.Value);

            //Vessel
            ans.Vessel = this.singleLineComboVessel.SelectedItem as MsVessel;

            //CrewMatrixType
            ans.CrewMatrixType = this.comboBoxCrewMatrix.SelectedItem as MsCrewMatrixType;

            //Port
            ans.Port = this.singleLineComboPort.SelectedItem as MsBasho;

            //Country
            ans.Country = this.singleLineComboCountry.SelectedItem as MsRegional;

            //Attachment report record
            //添付ファイル
            if (this.SrcData == null)
            {
                ans.AttachmentReportRecord = this.fileViewControlExReportRecord.CreateInsertUpdateAttachmentList(null, EAttachmentType.CI_Report_Record);
            }
            else
            {
                ans.AttachmentReportRecord = this.fileViewControlExReportRecord.CreateInsertUpdateAttachmentList(this.SrcData.ParentData.AttachmentList, EAttachmentType.CI_Report_Record);
            }

            //Remarks
            ans.Remarks = this.textBoxRemarks.Text.Trim();

            //会社横展開
            ans.ShareToOurFleetDate = DcCiPscInspection.EDate;
            ans.ShareToOurFleet = this.radioButtonShareToOurFleetOn.Checked;            
            if (ans.ShareToOurFleet == true)
            {
                ans.ShareToOurFleetDate = this.dateTimePickerShareToOurFleetDate.Value.Date;
            }

            

            return ans;
        }




        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            DcCiPscInspection psc = this.SrcData.PscInspection;
            DBDataCache db = DcGlobal.Global.DBCache;

            //Date
            this.dateTimePickerDate.Value = psc.date;

            //Kind
            {
                MsItemKind kind = db.GetMsItemKind(psc.item_kind_id);
                this.comboBoxItemKind.SelectedItem = kind;
            }
            //Deficiency数
            this.numericUpDownDeficiencyCount.Value = psc.deficinecy_count;

            //Vessel
            {
                MsVessel ves = db.GetMsVessel(psc.ms_vessel_id);
                this.singleLineComboVessel.Text = ves.ToString();
            }

            //VesselType
            {
                MsCrewMatrixType crew = db.GetMsCrewMatrixType(psc.ms_crew_matrix_type_id);
                this.comboBoxCrewMatrix.SelectedItem = crew;
            }

            //Port
            {
                MsBasho ba = db.GetMsBasho(psc.ms_basho_id);
                if (ba != null)
                {
                    this.singleLineComboPort.Text = ba.ToString();
                }
            }

            //Country
            {
                MsRegional reg = db.GetMsRegional(psc.ms_regional_code);
                if (reg != null)
                {
                    this.singleLineComboCountry.Text = reg.ToString();
                }
            }
            
            //Remarks
            this.textBoxRemarks.Text = psc.comment_remarks;

            //会社横展開
            this.radioButtonShareToOurFleetOff.Checked = true;
            this.radioButtonShareToOurFleetOn.Checked = psc.share_to_our_fleet;
            if (psc.share_to_our_fleet_date != DcCiPscInspection.EDate)
            {
                this.dateTimePickerShareToOurFleetDate.Value = psc.share_to_our_fleet_date;
            }

            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.CI_Report_Record, this.fileViewControlExReportRecord);                
            }

            //表示
            CommentItemLogic.DispAttachment(dic, this.SrcData.ParentData.AttachmentList);

        }


        //---------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">表示PscInspectionManager 新規null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.CIManager = inputdata as PscInspectionManager;            
            
            //画面の初期化
            this.InitDisplayControl();

            if (this.CIManager == null)
            {
                //新規
            }
            else
            {
                //扱いやすいようにデータ取得
                this.SrcData = this.CIManager.SrcData;


                //必要な場所を無効化
                this.comboBoxItemKind.Enabled = false;
                this.numericUpDownDeficiencyCount.Enabled = false;


                //更新
                this.DispData();
            }

            return true;
        }

        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PscHeaderControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 指摘事項件数が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownDeficiencyCount_ValueChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(this.numericUpDownDeficiencyCount.Value);
            if (this.ChangeDeficiencyCountDelegateProc != null)
            {
                count = this.ChangeDeficiencyCountDelegateProc(count);
                this.numericUpDownDeficiencyCount.Value = count;
            }

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
                                              this.fileViewControlExReportRecord,
                                              
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
        /// 会社横展開チェックが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonShareToOurFleet_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerShareToOurFleetDate.Enabled = this.radioButtonShareToOurFleetOn.Checked;
        }
    }

    
}
