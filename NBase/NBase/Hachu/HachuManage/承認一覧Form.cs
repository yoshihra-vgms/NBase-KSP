using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Hachu.Controllers;
using Hachu.Models;
using NBaseData.DAC;

namespace Hachu.HachuManage
{
    public partial class 承認一覧Form : Form
    {
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 承認一覧Form instance = null;

        /// <summary>
        /// 承認一覧情報リスト
        /// </summary>
        private 承認一覧情報Controller 承認一覧情報 = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 承認一覧Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 承認一覧Form();
            }
            return instance;
        }
        
        /// <summary>
        /// コンストラクタ
        /// Windows フォームをシングルトン対応にするため private
        /// </summary>
        private 承認一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("JM040601", "承認一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 承認一覧Form_Load(object sender, EventArgs e)
        {
            //==================================
            // 初期化
            //==================================

            this.Location = new Point(0, 0);

            // Form幅を親Formの幅にする
            this.Width = Parent.ClientSize.Width;
            this.Height = Parent.ClientSize.Height;

            検索条件初期化();

            承認一覧情報 = new 承認一覧情報Controller(this.MdiParent, splitContainer1, syoninListControl1);
            syoninListControl1.Init(this.Width - 20);

            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 承認一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsVessel> vessels = null;
            List<MsUser> users = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                users = serviceClient.MsUser_GetRecordsByUserKbn事務所(NBaseCommon.Common.LoginUser);
            }

            // 船ComboBox初期化
            MsVessel dmyVessel = new MsVessel();
            dmyVessel.MsVesselID = -1;
            dmyVessel.VesselName = "";
            comboBox船.Items.Clear();
            comboBox船.Items.Add(dmyVessel);
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }

            // 事務所担当者
            MsUser dmyUser = new MsUser();
            dmyUser.MsUserID = "";
            dmyUser.Sei = "";
            dmyUser.Mei = "";
            comboBox事務担当者.Items.Clear();
            comboBox事務担当者.Items.Add(dmyUser);
            foreach (MsUser u in users)
            {
                comboBox事務担当者.Items.Add(u);
            }
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // 検索条件
            承認一覧検索条件 検索条件 = new 承認一覧検索条件();
            if ( comboBox船.SelectedItem is MsVessel )
                検索条件.Vessel = comboBox船.SelectedItem as MsVessel;
            if (comboBox事務担当者.SelectedItem is MsUser)
                検索条件.User = comboBox事務担当者.SelectedItem as MsUser;

            // 検索条件で、ＤＢを検索し、一覧に表示する
            承認一覧情報.一覧更新(検索条件);

            this.Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 「条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button条件クリア_Click(object sender, EventArgs e)
        private void button条件クリア_Click(object sender, EventArgs e)
        {
            // 船
            comboBox船.SelectedIndex = 0;

            // 事務担当者
            comboBox事務担当者.SelectedIndex = 0;
        }
        #endregion
    }
}
