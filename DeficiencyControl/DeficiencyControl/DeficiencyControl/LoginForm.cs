using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

using DeficiencyControl.Forms;
using DeficiencyControl.Files;

using DcCommon.DB;
using DcCommon.DB.DAC;

namespace DeficiencyControl
{
    /// <summary>
    /// ログインフォーム
    /// </summary>
    public partial class LoginForm : BaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginForm() : base(EFormNo.Login)
        {            
            InitializeComponent();
        }


        

        /// <summary>
        /// フォームの初期化
        /// </summary>
        /// <returns></returns>
        private new bool InitForm()
        {
            //データの初期化

            //バージョン情報設定
            this.labelAppVersion.Text = Application.ProductVersion;
            

            //クリップワンスバージョン表示
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version cv = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.labelClickOnceVersion.Text = cv.ToString();

            }


            //裏モード表示
            this.DispDeficiencyMode();
            

            return true;
        }

        /// <summary>
        /// 裏モードを明示する
        /// </summary>
        private void DispDeficiencyMode()
        {
            string title = "";

            //青モード
            if (AppConfig.Config.ConfigData.DeficiencyControlBlueMode == true)
            {
                this.BackColor = DeficiencyControlColor.DeficiencyControlBlueModeColor;
                title += "|Blue Mode|";
                DcLog.WriteLog("*** DeficiencyControlBlueMode Enabled ***");
            }



            //最後にタイトルで明示する
            if (title.Length >= 0)
            {
                this.Text += " " + title;
            }

        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <returns></returns>
        private bool Login()
        {   
            //ログインIDとパスワード取得
            string lid = this.textBoxID.Text;
            string password = this.textBoxPassword.Text;
                       

            //ログインユーザーの取得
            //SvcManager.Init();
            //UserData udata = SvcManager.SvcMana.Login(lid, password);
            UserData udata = SvcManager.InitLogin(lid, password);
            if (udata == null)
            {
                string deg = string.Format("loginid={0} password={1}", lid, password);
                DcLog.WriteLog(deg);
                throw new Exception("ログイン失敗");
            }

            //ログインユーザー設定
            DcGlobal.Global.LoginUser = udata;


            

            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// ログインボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonLogin_Click");

            try
            {

                // プロキシを利用するかを保持する
                bool useProxy = this.checkBox_UserProxy.Checked;
                ProxySetting.SetUseProxy(useProxy);

                using (WaitingState ew = new WaitingState(this))
                {
                    bool loginret = this.Login();
                    if (loginret == false)
                    {
                        throw new Exception("");
                    }
                }                
            }
            catch(Exception ex)
            {
                DcLog.WriteLog(ex, "Login");

                //失敗した
                DcMes.ShowMessage(this, EMessageID.MI_4);
                return;
            }


            this.Visible = false;

            //ポータルフォーム起動
            PortalForm pf = new PortalForm();
            DialogResult dret = pf.ShowDialog();


            this.Close();
            
        }

        /// <summary>
        /// 閉じるボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClose_Click");

            this.Close();
        }

        /// <summary>
        /// プロキシ設定ラベルが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelProxy_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "linkLabelProxy_Click");

            Form_ProxySetting f = new Form_ProxySetting();
            f.ShowDialog();
        }

        /// <summary>
        /// テキストボックスでキーが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            //エンターだった
            if (e.KeyCode == Keys.Enter)
            {
                //ここでログイン
                this.buttonLogin_Click(null, null);
            }

        }

        /// <summary>
        /// 何かキーが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }

        }
    }
}
