using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using DeficiencyControl.Forms;

namespace DeficiencyControl
{
    /// <summary>
    /// フォーム番号 追加が頻繁に発生するため、これをint等変数として使うべからず
    /// </summary>
    public enum EFormNo
    {        
        Login,
        Portal,
        

        PSCList,
        PSCCreate,
        PSCDetail,
        PSCOutputSetting,


        AccidentList,
        AccidentCreate,
        AccidentDetail,
        AccidentOutputSetting,



        MoiList,
        MoiCreate,
        MoiDetail,
        MoiOutputExcelSetting,
        MoiOutputReportSetting,


        ScheduleMain,
        //------------------------------------------------------


        MasterMenu,

        PasswordChange,

        MsUserList,
        MsUserDetail,

        MsAlarmColorList,
        MsAlarmColorDetail,

        //------------------------------------------------------




        //------------------------------------------------------
        //------------------------------------------------------
        //------------------------------------------------------
        AdminMaintenanceMenu,
        AdminMaintenanceRegistCount,

        /// <summary>
        /// 最大値、無効値
        /// </summary>
        MAX,
    }

    /// <summary>
    /// フォーム基底クラス
    /// <remarks>
    /// 全てのフォームはこのクラスを派生させる
    /// DialogResultについて：適用、反映などはDialogResult.OK 削除はyes、閉じるなどはCancelにする。
    /// ボタンで閉じるときは基本的にDialogResultに値を設定することで返却する。
    /// </remarks>
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// フォーム番号
        /// </summary>
        public EFormNo FormNo = EFormNo.MAX;

        /// <summary>
        /// 待ち時間を実行するか
        /// </summary>
        protected bool UsingWait =　false;


       
        /// <summary>
        /// コンストラクタ 引数なしがないとこれないと怒られます
        /// </summary>
        public BaseForm()
        {
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// コンストラクタこちらを使用すること
        /// </summary>
        /// <param name="fno">EFormNoのフォーム番号</param>
        /// <param name="usingwait">初期化時、待ち表示をするか true:待ち表示する　false:しない</param>
        public BaseForm(EFormNo fno, bool usingwait = false)
        {
            InitializeComponent();
                        
            this.FormNo = fno;
            this.UsingWait = usingwait;
            this.DoubleBuffered = true;

        }

        /// <summary>
        /// フォーム初期化データ設定
        /// </summary>
        /// <param name="fsetdata"></param>
        /// <returns></returns>
        public virtual bool SetFormSettingData(object fsetdata)
        {
            return true;
        }

        /// <summary>
        /// 初期化関数 フォームの初期化 Form.Shownでコールされます。コンストラクタ引数usingwaitをtrueにするとwaitstateで囲います
        /// </summary>
        /// <returns></returns>
        protected virtual bool InitForm()
        {
            return true;
        }

        /// <summary>
        /// ユーザーロールに応じた画面ごとの処理、Loadのタイミングで呼ばれます。InitFormより前です。
        /// </summary>
        /// <returns></returns>
        protected virtual bool ControlUserRole()
        {
            return true;
        }


        /// <summary>
        /// 親フォーム番号の取得 EFromNo.FormMaxで特になし
        /// </summary>
        /// <returns></returns>
        protected EFormNo GetParentFormNo()
        {
            BaseForm f = this.Owner as BaseForm;
            if (f == null)
            {
                return EFormNo.MAX;
            }

            return f.FormNo;
        }


        /// <summary>
        /// 再帰的にDataGridViewを探し出して中身を変更する
        /// </summary>
        /// <param name="pcon"></param>
        private void NewLineDataGridViewHeaderReCall(Control pcon)
        {
            DataGridView grid = pcon as DataGridView;
            if (grid != null)
            {
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.HeaderText = col.HeaderText.Replace(@"\n", Environment.NewLine);
                }
                return;
            }


            foreach (Control con in pcon.Controls)
            {
                this.NewLineDataGridViewHeaderReCall(con);
            }
        }

        /// <summary>
        /// この画面に含まれるDataGridViewのヘッダーテキストの\nを任意の改行コードに置き換える
        /// </summary>
        protected void NewLineDataGridViewHeader()
        {
            foreach (Control con in this.Controls)
            {
                this.NewLineDataGridViewHeaderReCall(con);
            }
        }


        /// <summary>
        /// 画面初期化処理のまとめ
        /// </summary>
        private void InitAll()
        {   
            this.InitForm();
        }


        /// <summary>
        /// Deficiency特殊モードチェンジ
        /// </summary>
        protected virtual void ChangeDeficiencyMode()
        {
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 表示されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Shown(object sender, EventArgs e)
        {
            this.Refresh();

            try
            {

                if (this.UsingWait == true)
                {
                    using (WaitingState ew = new WaitingState(this))
                    {
                        this.InitAll();
                    }
                }
                else
                {
                    this.InitAll();
                }
            }
            catch (Exception ex)
            {
                DcLog.WriteLog(ex, "BaseForm_Shown InitForm Exception");
            }
        }
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.ControlUserRole();

            //裏モード置き換え
            this.ChangeDeficiencyMode();

            //改行コードの置き換え
            this.NewLineDataGridViewHeader();
        }
    }
}
