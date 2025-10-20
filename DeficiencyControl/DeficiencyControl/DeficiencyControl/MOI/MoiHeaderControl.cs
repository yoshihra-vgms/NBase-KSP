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
    /// 検船ヘッダーコントロール
    /// </summary>
    public partial class MoiHeaderControl : BaseControl
    {
        public MoiHeaderControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 指摘件数が変更されたときのイベント
        /// </summary>
        /// <param name="count"></param>
        public delegate int ChangeObservationCountDelegate(int count);

        /// <summary>
        /// 受検日が変更されたときのイベント
        /// </summary>
        /// <param name="dateTime"></param>
        public delegate void ChangeDateTimePickerDateDelegate(DateTime dateTime);

        

        /// <summary>
        /// このコントロールの出力
        /// </summary>
        public class MoiHeaderControlOutputData
        {
            public MsVessel Vessel = null;
            public MsUser PIC = null;
            public MsBasho Port = null;
            public string Terminal = "";
            public MsRegional Country = null;

            public DateTime Date;
            public DateTime ReceiptDate;

            public int Observation = 0;

            public MsInspectionCategory InspectionCategory = null;
            public MsCustomer AppointedCompany = null;
            public MsCustomer InspectionCompany = null;
            public string InspectionName = "";

            public List<DcAttachment> InspectionReportAttachmentList = new List<DcAttachment>();
            public string Remarks = "";
            public string Attend = "";



        }

        /// <summary>
        /// 画面データ
        /// </summary>
        public class MoiHeaderControlData
        {
            /// <summary>
            /// 元データ
            /// </summary>
            public MoiData SrcData = null;
        }

        /// <summary>
        /// コントロールデータ
        /// </summary>
        private MoiHeaderControlData FData = null;


        /// <summary>
        /// 指摘件数変更関数
        /// </summary>
        public ChangeObservationCountDelegate ChangeObservationCountDelegateProc = null;

        /// <summary>
        /// 受検日変更関数
        /// </summary>
        public ChangeDateTimePickerDateDelegate ChangeDateTimePickerDateDelegateProc = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 画面コントロールの初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);

            //PIC
            ControlItemCreator.CreateUser(this.singleLineComboUser);

            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);

            //Country
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);


            //検船種別
            ControlItemCreator.CreateMsInspectionCategory(this.comboBoxInspectionCategory);

            //申請先
            ControlItemCreator.CreateMsCustomerAppointed(this.singleLineComboAppointedCompany);

            //検船実施会社            
            ControlItemCreator.CreateMsCustomerInspection(this.singleLineComboInspectionCompany);



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
                anslist = fcon.CreateInsertUpdateAttachmentList(this.FData.SrcData.Header.AttachmentList, atype);
            }

            return anslist;
        }


        /// <summary>
        /// データの表示
        /// </summary>
        public void DispData()
        {
            MoiHeaderData hdata = this.FData.SrcData.Header;
            DBDataCache db = DcGlobal.Global.DBCache;
            DcMoi moi = hdata.Moi;

            //船
            {
                MsVessel ves = db.GetMsVessel(moi.ms_vessel_id);
                if (ves != null)
                {
                    this.singleLineComboVessel.Text = ves.ToString();
                }
            }

            //PIC
            {
                MsUser u = db.GetMsUser(moi.ms_user_id);
                if (u != null)
                {
                    this.singleLineComboUser.Text = u.ToString();
                }
            }


            //場所
            {
                MsBasho ba = db.GetMsBasho(moi.ms_basho_id);
                if (ba != null)
                {
                    this.singleLineComboPort.Text = ba.ToString();
                }
            }

            //Terminal
            this.textBoxTerminal.Text = moi.terminal;


            //国
            {
                MsRegional reg = db.GetMsRegional(moi.ms_regional_code);
                if (reg != null)
                {
                    this.singleLineComboCountry.Text = reg.ToString();
                }
            }

            //Date
            this.dateTimePickerDate.Value = moi.date;

            //レポート受領日
            this.dateTimePickerReceiptDate.Value = moi.receipt_date;

            //指摘事項件数
            this.numericUpDownObservation.Value = moi.observation;


            //検船種別
            {
                MsInspectionCategory inc = db.GetMsInspectionCategory(moi.inspection_category_id);
                if (inc != null)
                {
                    this.comboBoxInspectionCategory.SelectedItem = inc;
                }
            }

            //申請先
            {
                MsCustomer acus = db.GetMsCustomerAppointed(moi.appointed_ms_customer_id);
                if (acus != null)
                {
                    this.singleLineComboAppointedCompany.Text = acus.ToString();
                }
            }

            //検船実施会社
            {
                MsCustomer inco = db.GetMsCustomerInspection(moi.inspection_ms_customer_id);
                if (inco != null)
                {
                    this.singleLineComboInspectionCompany.Text = inco.ToString();
                }
            }

            //検船員
            this.textBoxInspectionName.Text = moi.inspection_name;

            //備考
            this.textBoxRemarks.Text = moi.remarks;

            //立会者
            this.textBoxAttend.Text = moi.attend;


            #region 添付ファイル
            //添付ファイルを一括表示
            Dictionary<EAttachmentType, FileViewControlEx> dic = new Dictionary<EAttachmentType, FileViewControlEx>();
            {
                dic.Add(EAttachmentType.MO_InspectionReport, this.fileViewControlExInspectionReport);
            }

            //表示
            CommentItemLogic.DispAttachment(dic, hdata.AttachmentList);

            #endregion

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                                      this.singleLineComboUser,

                                      this.singleLineComboPort,
                                      this.textBoxTerminal,
                                      this.singleLineComboCountry,
                                      
                                      this.singleLineComboInspectionCompany,
                                      this.textBoxInspectionName,
                                      
                                      


                                  };
                this.ErList.AddRange(ComLogic.CheckInput(ckvec));


                //個別のチェックはここで。

                
                //未来の日付での登録を抑制する
                DateTime dt = this.dateTimePickerDate.Value.Date;
                TimeSpan ts = dt - DateTime.Now.Date;
                if (ts.TotalMilliseconds > 0)
                {
                    this.ErList.Add(this.panelDateError);
                }


                //0件は検船でもありえる
                ////指摘0件を許さない
                //if (this.numericUpDownObservation.Value <= 0)
                //{
                //    this.ErList.Add(this.numericUpDownObservation);
                //}


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
        public MoiHeaderControlOutputData GetInputData()
        {
            MoiHeaderControlOutputData ans = new MoiHeaderControlOutputData();

            ans.Vessel = this.singleLineComboVessel.SelectedItem as MsVessel;
            ans.PIC = this.singleLineComboUser.SelectedItem as MsUser;
            ans.Port = this.singleLineComboPort.SelectedItem as MsBasho;
            ans.Terminal = this.textBoxTerminal.Text.Trim();
            ans.Country = this.singleLineComboCountry.SelectedItem as MsRegional;

            //--
            ans.Date = this.dateTimePickerDate.Value.Date;
            ans.ReceiptDate = this.dateTimePickerReceiptDate.Value.Date;
            ans.Observation = Convert.ToInt32(this.numericUpDownObservation.Value);

            ans.InspectionCategory = this.comboBoxInspectionCategory.SelectedItem as MsInspectionCategory;
            ans.AppointedCompany = this.singleLineComboAppointedCompany.SelectedItem as MsCustomer;
            ans.InspectionCompany = this.singleLineComboInspectionCompany.SelectedItem as MsCustomer;
            ans.InspectionName = this.textBoxInspectionName.Text.Trim();

            //
            ans.InspectionReportAttachmentList = this.GetFileAttachmentList(this.fileViewControlExInspectionReport, EAttachmentType.MO_InspectionReport);

            ans.Remarks = this.textBoxRemarks.Text.Trim();
            ans.Attend = this.textBoxAttend.Text.Trim();


            return ans;
        }
 



        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// コントロール初期化
        /// </summary>
        /// <param name="inputdata">MoiData 新規=null</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new MoiHeaderControlData();
            this.FData.SrcData = inputdata as MoiData;

            //画面初期化
            this.InitDisplayControl();

            if (this.FData.SrcData == null)
            {
                //新規
            }
            else
            {

                //コントロール制御
                this.numericUpDownObservation.Enabled = false;

                //更新
                this.DispData();
            }

                        
            return true;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiHeaderControl_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 指摘件数が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownObservation_ValueChanged(object sender, EventArgs e)
        {
            //通知
            int count = Convert.ToInt32(this.numericUpDownObservation.Value);
            if (this.ChangeObservationCountDelegateProc != null)
            {                
                count = this.ChangeObservationCountDelegateProc(count);
                this.numericUpDownObservation.Value = count;
            }
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

        /// <summary>
        /// ファイル挿入ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttachment_Click(object sender, EventArgs e)
        {
            //Buttonのタグと一致させること。
            FileViewControlEx[] contvec = {
                                              this.fileViewControlExInspectionReport,
                                              
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
        /// 受検日の値が変更するときに呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            // 通知
            DateTime dt = this.dateTimePickerDate.Value.Date;
            if (this.ChangeDateTimePickerDateDelegateProc != null)
            {
                this.ChangeDateTimePickerDateDelegateProc(dt);
            }
        }
    }
}
