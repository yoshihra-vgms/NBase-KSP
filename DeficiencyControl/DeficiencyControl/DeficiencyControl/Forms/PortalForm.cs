using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DeficiencyControl.Util;
using DeficiencyControl.MasterMaintenance;
using DcCommon.DB.DAC;
using DeficiencyControl.Accident;
using DeficiencyControl.MOI;
using DeficiencyControl.Schedule;
using CIsl.DB.WingDAC;
using CIsl;

namespace DeficiencyControl.Forms
{

    /// <summary>
    /// ポータル画面
    /// </summary>
    public partial class PortalForm : BaseForm
    {
        public PortalForm() : base(EFormNo.Portal, true)
        {
            InitializeComponent();
        }

        //フォームデータまとめクラス
        public class PortalFormData
        {
        }

        

        #region メンバ変数

        /// <summary>
        /// フォームデータ
        /// </summary>
        private PortalFormData FData = new PortalFormData();

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ログインユーザー情報の表示
        /// </summary>
        private void DispLoginUser()
        {
            string sname = DcGlobal.Global.LoginUser.User.ToString();
            this.Text += string.Format(" [{0}]", sname);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// フォームの初期化        
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //全体に関する初期化
            {
                //マスターデータの取得
                DcGlobal.Global.DBCache = new DBDataCache();
                DcGlobal.Global.DBCache.Init();
                
            }

            //画面に関する初期化
            {
                //ログインユーザーの情報表示
                this.DispLoginUser();

                //その他必要な情報の設定

                //ポータルタブ初期化
                this.portalAlarmControl1.Init();
            }
                        
            return true;
        }
        
        /// <summary>
        /// ユーザー権限制御
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {


            //権限取得
            List<MsRole> rolelist = DcGlobal.Global.LoginUser.RoleList;

 
            List<RoleData> rdlist = new List<RoleData>();
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.PSC, ERoleName3.MAX, this.buttonPSC));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.事故トラブル, ERoleName3.MAX, this.buttonAccidentReport));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.検船, ERoleName3.MAX, this.buttonMOI));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.スケジュール, ERoleName3.MAX, this.buttonSchedule));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.マスタ, ERoleName3.MAX, this.buttonMaster));

            RoleController.ControlRole(rolelist, rdlist);
            

            
            return true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ポータル画面が読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortalForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "PortalForm_Load");
        }

        /// <summary>
        /// PSCボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPSC_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonPSC_Click");


            PscListForm f = new PscListForm();
            DialogResult dret = f.ShowDialog(this);
        }



        /// <summary>
        /// マスターが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMaster_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonMaster_Click");

            MasterMenuForm f = new MasterMenuForm();
            DialogResult dret = f.ShowDialog(this);



            //マスター管理から戻ってきたらマスターキャッシュ再作成
            using (WaitingState es = new WaitingState(this))
            {
                DcGlobal.Global.DBCache.Init();
            }

        }

    


        /// <summary>
        /// AccidentReportが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccidentReport_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonAccidentReport_Click");

            AccidentListForm f = new AccidentListForm();
            DialogResult dret = f.ShowDialog(this);
        }


        /// <summary>
        /// 検船ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMOI_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonMOI_Click");

            MoiListForm f = new MoiListForm();
            DialogResult dret = f.ShowDialog(this);
        }


        /// <summary>
        /// スケジュールボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonSchedule_Click");

            ScheduleMainForm f = new ScheduleMainForm();
            DialogResult dret = f.ShowDialog(this);

            //アラームの再検索
            using (WaitingState es = new WaitingState(this))
            {
                this.portalAlarmControl1.Search();
            }

        }

        
        

   
    }
}
