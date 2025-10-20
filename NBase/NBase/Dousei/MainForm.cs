using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseData.BLC.動静帳票;
using NBaseUtil;
using NBaseCommon;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DS;


namespace Dousei
{
    public partial class MainForm : Form
    {
        List<NBaseData.DAC.MsVessel> MsVessels;
        private Dousei.util.TreeListViewDelegate動静 treeListViewDelegate;
        private MsVessel SelectedVessel;


        public List<複数選択Form.ListItem> 複数選択_貨物情報;
        public List<複数選択Form.ListItem> 複数選択_場所情報;
        public List<複数選択Form.ListItem> 複数選択_基地情報;
        public List<複数選択Form.ListItem> 複数選択_代理店情報;
        public List<複数選択Form.ListItem> 複数選択_荷主情報;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            treeListViewDelegate = new Dousei.util.TreeListViewDelegate動静(treeListView1);
            Init();
            Search();

            EnableComponents();
        }


        /// <summary>
        /// 契約、権限による画面部品の表示/非表示
        /// </summary>
        #region private void EnableComponents()
        private void EnableComponents()
        {
            //==============================
            // 権限によるボタンの制御
            //==============================
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "動静管理", "動静一覧", ""))
            {
                Search_button.Enabled = true;
                Clear_button.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "動静管理", "新規動静追加", ""))
            {
                CreateNew_button.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "動静管理", "基幹連携", ""))
            {
                KikanRenkei_button.Enabled = true;
            }
        }
        #endregion

        /// <summary>
        /// データを検索し、一覧にセットする
        /// </summary>
        private void Search()
        {
            try
            {
                // 検索時の "船" を保持する
                SelectedVessel = Vessel_comboBox.SelectedItem as MsVessel;


                var selectedCargo = 複数選択_貨物情報.Where(obj => obj.Checked == true).Select(obj => obj.Value);
                var selectedBasho = 複数選択_場所情報.Where(obj => obj.Checked == true).Select(obj => obj.StrValue);
                var selectedkichi = 複数選択_基地情報.Where(obj => obj.Checked == true).Select(obj => obj.StrValue);
                var selectedDairiten = 複数選択_代理店情報.Where(obj => obj.Checked == true).Select(obj => obj.StrValue);
                var selectedNinushi = 複数選択_荷主情報.Where(obj => obj.Checked == true).Select(obj => obj.StrValue);


                Cursor = Cursors.WaitCursor;

                List<NBaseData.DAC.DjDousei> djDousei;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    int renkei;
                    if (Renkei_checkBox.Checked == false)
                    {
                        renkei = 0;
                    }
                    else
                    {
                        renkei = 1;
                    }
                    djDousei = serviceClient.DjDousei_GetRecordsByHead(NBaseCommon.Common.LoginUser,
                        Vessel_comboBox.SelectedItem as MsVessel, From_dateTimePicker.Value, To_dateTimePicker.Value, renkei,
                        selectedCargo.ToList(), selectedBasho.ToList(), selectedkichi.ToList(), selectedDairiten.ToList(), selectedNinushi.ToList()
                        );
                }

                //日付で並び返る
                //var result = from d in djDousei
                //             orderby d.DouseiDate
                //             select d;
                var result = from d in djDousei
                             where d.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID || d.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID
                             orderby d.DouseiDate
                             select d;

                treeListViewDelegate.SetRows(result.ToList<NBaseData.DAC.DjDousei>());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #region 初期化ロジック

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            InitComboBox船();
            Init日付();



            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //-----------------------------------------------------
                // 貨物情報
                //-----------------------------------------------------
                if (複数選択_貨物情報 == null)
                {
                    複数選択_貨物情報 = new List<複数選択Form.ListItem>();

                    List<MsCargo> cargoList = serviceClient.MsCargo_GetRecords(NBaseCommon.Common.LoginUser);

                    foreach (MsCargo cargo in cargoList)
                    {
                        複数選択Form.ListItem item = new 複数選択Form.ListItem(cargo.CargoName, cargo.MsCargoID, false);
                        複数選択_貨物情報.Add(item);
                    }
                }
                else
                {
                    foreach(複数選択Form.ListItem item in 複数選択_貨物情報)
                    {
                        item.Checked = false;
                    }
                }
                textBox_貨物.Text = "";

                //-----------------------------------------------------
                // 場所情報
                //-----------------------------------------------------
                if (複数選択_場所情報 == null)
                {
                    複数選択_場所情報 = new List<複数選択Form.ListItem>();

                    List<MsBasho> bashoList = serviceClient.MsBasho_GetRecords(NBaseCommon.Common.LoginUser);

                    foreach (MsBasho basho in bashoList)
                    {
                        複数選択Form.ListItem item = new 複数選択Form.ListItem(basho.BashoName, basho.MsBashoId, false);
                        複数選択_場所情報.Add(item);
                    }
                }
                else
                {
                    foreach (複数選択Form.ListItem item in 複数選択_場所情報)
                    {
                        item.Checked = false;
                    }
                }
                textBox_場所.Text = "";

                //-----------------------------------------------------
                // 基地情報
                //-----------------------------------------------------
                if (複数選択_基地情報 == null)
                {
                    複数選択_基地情報 = new List<複数選択Form.ListItem>();

                    List<MsKichi> kichiList = serviceClient.MsKichi_GetRecords(NBaseCommon.Common.LoginUser);

                    foreach (MsKichi kichi in kichiList)
                    {
                        複数選択Form.ListItem item = new 複数選択Form.ListItem(kichi.KichiName, kichi.MsKichiId, false);
                        複数選択_基地情報.Add(item);
                    }
                }
                else
                {
                    foreach (複数選択Form.ListItem item in 複数選択_基地情報)
                    {
                        item.Checked = false;
                    }
                }
                textBox_基地.Text = "";

                //-----------------------------------------------------
                // 代理店、荷主情報
                //-----------------------------------------------------
                if (複数選択_代理店情報 == null && 複数選択_荷主情報 == null)
                {
                    複数選択_代理店情報 = new List<複数選択Form.ListItem>();
                    複数選択_荷主情報 = new List<複数選択Form.ListItem>();

                    List<MsCustomer> customerList = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);

                    foreach (MsCustomer customer in customerList)
                    {
                        if (customer.Is代理店())
                        {
                            複数選択Form.ListItem item = new 複数選択Form.ListItem(customer.CustomerName, customer.MsCustomerID, false);
                            複数選択_代理店情報.Add(item);
                        }
                        if (customer.Is荷主())
                        {
                            複数選択Form.ListItem item = new 複数選択Form.ListItem(customer.CustomerName, customer.MsCustomerID, false);
                            複数選択_荷主情報.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (複数選択Form.ListItem item in 複数選択_代理店情報)
                    {
                        item.Checked = false;
                    }
                    foreach (複数選択Form.ListItem item in 複数選択_荷主情報)
                    {
                        item.Checked = false;
                    }
                }
                textBox_代理店.Text = "";
                textBox_荷主.Text = "";
            }
        }

        /// <summary>
        /// 「船名」を初期化
        /// </summary>
        private void InitComboBox船()
        {
            Vessel_comboBox.Items.Clear();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVessels = serviceClient.MsVessel_GetRecordsByKanidouseiEnabled(NBaseCommon.Common.LoginUser);
            }
            foreach (MsVessel v in MsVessels)
            {
                Vessel_comboBox.Items.Add(v);
            }
            Vessel_comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 「日付」を初期化
        /// </summary>
        private void Init日付()
        {
            DateTime FromDate,ToDate;

            FromDate = DateTime.Parse(DateTime.Today.ToString("yyyy/MM/") + "1");
            ToDate = FromDate.AddMonths(1);
            ToDate = ToDate.AddDays(-1);

            From_dateTimePicker.Value = FromDate;
            To_dateTimePicker.Value = ToDate;
        }
        #endregion

        /// <summary>
        /// 「検索」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Search_button_Click(object sender, EventArgs e)
        private void Search_button_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「検索条件クリア」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Clear_button_Click(object sender, EventArgs e)
        private void Clear_button_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Init();

            this.Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 行選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void treeListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        private void treeListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (treeListView1.SelectedNode == null)
                {
                    return;
                }
                TreeListViewNode node = treeListView1.GetNodeAt(me.Location);

                if (node != null)
                {
                    //MsVessel SelectedVessel = Vessel_comboBox.SelectedItem as MsVessel;
                    //動静詳細Form form = new 動静詳細Form();
                    //form.VesselName = SelectedVessel.VesselName;
                    //form.MsVesselID = SelectedVessel.MsVesselID;
                    動静詳細Form1 form = new 動静詳細Form1(SelectedVessel.MsVesselID);

                    util.TreeListViewDelegate動静.DouseiNode douseiNode = node.Tag as util.TreeListViewDelegate動静.DouseiNode;
                    //form.SelectedDousei = douseiNode.Dousei;

                    //if (form.SelectedDousei != null && form.ShowDialog() == DialogResult.OK)
                    //{
                    //    Search();
                    //}
                    form.Dousei = douseiNode.Dousei;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Search();
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;   
            }
        }
        #endregion

        /// <summary>
        /// 「新規動静追加」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void CreateNew_button_Click(object sender, EventArgs e)
        private void CreateNew_button_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //MsVessel SelectedVessel = Vessel_comboBox.SelectedItem as MsVessel;
                //動静詳細Form form = new 動静詳細Form();
                //form.VesselName = SelectedVessel.VesselName;
                //form.MsVesselID = SelectedVessel.MsVesselID;

                //form.SelectedDousei = new DjDousei();

                新規動静実績Form form = new 新規動静実績Form(SelectedVessel.MsVesselID);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Search();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion


        /// <summary>
        /// 基幹連携
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KikanRenkei_button_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Cursor = Cursors.WaitCursor;


            //    List<DjDousei> SelectedDouseis = new List<DjDousei>();
            //    foreach (TreeListViewNode node in treeListView1.Nodes)
            //    {
            //        CheckBox c = node.SubItems[0].Control as CheckBox;
            //        if (c != null && c.Checked == true)
            //        {
            //            util.TreeListViewDelegate動静.DouseiNode dn = node.Tag as util.TreeListViewDelegate動静.DouseiNode;

            //            SelectedDouseis.Add(dn.Dousei);
            //        }
            //    }
            //    #region バリデーション

            //    #region 次航海番号が入っているか調べる
            //    foreach (DjDousei ParentDousei in SelectedDouseis)
            //    {
            //        bool Find = false;
            //        foreach (DjDousei dousei in ParentDousei.DjDouseis)
            //        {
            //            if (dousei.VoyageNo.Length == 0)
            //            {
            //                Find = true;
            //                break;
            //            }
            //        }

            //        if (Find == true)
            //        {
            //            StringBuilder sb = new StringBuilder();
            //            sb.AppendFormat("次航海番号が入っていません\n 日付:{0} 場所:{1}", ParentDousei.DouseiDate.ToString("yyyy/MM/dd"), ParentDousei.BashoName);
            //            MessageBox.Show(sb.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    #endregion

            //    #region 選択された動静に積み情報が入っているか調べる
            //    foreach (DjDousei ParentDousei in SelectedDouseis)
            //    {
            //        bool L_Find = false;
            //        bool D_Find = false;
            //        foreach (DjDousei dousei in ParentDousei.DjDouseis)
            //        {
            //            if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID)
            //            {
            //                L_Find = true;
            //            }
            //            if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
            //            {
            //                D_Find = true;
            //            }
            //        }

            //        if (L_Find == false)
            //        {
            //            StringBuilder sb = new StringBuilder();
            //            sb.AppendFormat("選択した動静に積み情報が入っていません\n 日付:{0} 場所:{1}", ParentDousei.DouseiDate.ToString("yyyy/MM/dd"), ParentDousei.BashoName);
            //            MessageBox.Show(sb.ToString(), "積み", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        if (D_Find == false)
            //        {
            //            StringBuilder sb = new StringBuilder();
            //            sb.AppendFormat("選択した動静に揚げ情報が入っていません\n 日付:{0} 場所:{1}", ParentDousei.DouseiDate.ToString("yyyy/MM/dd"), ParentDousei.BashoName);
            //            MessageBox.Show(sb.ToString(), "揚げ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    #endregion


            //    #region 各動静情報の日付の並びが正しいかを調べる
            //    foreach (DjDousei ParentDousei in SelectedDouseis)
            //    {
            //        foreach (DjDousei dousei in ParentDousei.DjDouseis)
            //        {
            //            DateTime d1 = dousei.ResultNyuko;       // 入港
            //            DateTime d2 = dousei.ResultChakusan;    // 着桟
            //            DateTime d3 = dousei.ResultNiyakuStart; // 荷役開始
            //            DateTime d4 = dousei.ResultNiyakuEnd;   // 荷役終了
            //            DateTime d5 = dousei.ResultRisan;       // 離桟
            //            DateTime d6 = dousei.ResultShukou;      // 出港
            //            // 2014.12.19
            //            //if (!((d1 <= d2) && (d2 <= d3) && (d3 <= d4) && (d4 <= d5) && (d5 <= d6)))
            //            if (!((d1 <= d2) && (d2 <= d3) && (d3 <= d4) && (d4 <= d5) && (d6 == DateTime.MinValue || d5 <= d6)))
            //            {
            //                StringBuilder sb = new StringBuilder();
            //                sb.AppendFormat("入港〜出港の日時を確認して下さい\n 日付:{0} 場所:{1}", ParentDousei.DouseiDate.ToString("yyyy/MM/dd"), ParentDousei.BashoName);
            //                MessageBox.Show(sb.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                return;
            //            }
            //        }
            //    }
            //    #endregion


            //    if (SelectedDouseis.Count == 0)
            //    {
            //        MessageBox.Show("動静基幹連携を行う情報を\n1件以上選択して下さい", "選択", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    #endregion

            //    if (MessageBox.Show("動静基幹連携を行います。\nよろしいですか？", "動静基幹連携", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            //    {
            //        return;
            //    }

            //    #region 更新処理
            //    List<動静基幹連携.ResultMessage> resultMessages = new List<動静基幹連携.ResultMessage>();
            //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //    {
            //        resultMessages = serviceClient.BLC_動静基幹連携_Kick(NBaseCommon.Common.LoginUser, SelectedDouseis);
            //    }
            //    #endregion

            //    #region 結果表示
            //    int errorCount = 0;
            //    連携メッセージ MessageForm = new 連携メッセージ();
            //    foreach (動静基幹連携.ResultMessage rm in resultMessages)
            //    {
            //        if (rm.StatusType == 動静基幹連携.ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
            //        {
            //            MessageForm.Message += rm.StatusToString();
            //            MessageForm.Message += System.Environment.NewLine;
            //            errorCount++;
            //        }
            //    }
            //    if (errorCount == 0)
            //    {
            //        MessageForm.Message = "エラーはありません";
            //    }

            //    MessageForm.Show();
            //    #endregion

            //}
            //finally
            //{
            //    Cursor = Cursors.Default;
            //}
            Search();
        }





        private void button_貨物_Click(object sender, EventArgs e)
        {
            選択_Click("貨物選択", 複数選択_貨物情報, textBox_貨物);
        }

        private void button_場所_Click(object sender, EventArgs e)
        {
            選択_Click("場所選択", 複数選択_場所情報, textBox_場所);
        }

        private void button_基地_Click(object sender, EventArgs e)
        {
            選択_Click("基地選択", 複数選択_基地情報, textBox_基地);
        }

        private void button_代理店_Click(object sender, EventArgs e)
        {
            選択_Click("代理店選択", 複数選択_代理店情報, textBox_代理店);
        }

        private void button_荷主_Click(object sender, EventArgs e)
        {
            選択_Click("荷主選択", 複数選択_荷主情報, textBox_荷主);
        }



        private void 選択_Click(string title, List<複数選択Form.ListItem> checkedList, TextBox textBox)
        {
            複数選択Form form = new 複数選択Form(title, checkedList);
            if (form.ShowDialog() == DialogResult.OK)
            {
                checkedList = form.GetCheckedList();
                List<string> names = form.CheckedNames();


                textBox.Text = "";
                string t = "";
                foreach (string name in names)
                {
                    t += " , " + name;
                }
                if (t.Length > 0)
                {
                    textBox.Text = t.Substring(3);
                }
            }
        }

    }
}
