using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hachu.Utils;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Hachu.HachuManage
{
    public partial class 支払合算作成Form : Form
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        合算対象の受領Filter 検索条件 = new 合算対象の受領Filter();

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;

        /// <summary>
        /// 
        /// </summary>
        private ItemTreeListView支払合算作成 TreeList = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        #region public 支払合算作成Form()
        public 支払合算作成Form()
        {
            InitializeComponent();
        }
        #endregion


        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 支払合算作成Form_Load(object sender, EventArgs e)
        private void 支払合算作成Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "合算作成", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            検索条件初期化();
            一覧初期化();
        }
        #endregion

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsCustomer> customers = null;
            List<MsThiIraiSbt> thiIraiSbts = null;
            List<MsVessel> vessels = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                customers = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
            }

            // 取引先
            comboBox取引先.Items.Clear();
            foreach (MsCustomer c in customers)
            {
                // 2013.08.07 : 取引先のみセットする
                //comboBox取引先.Items.Add(c);
                //comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
                //if (c.Shubetsu == (int)MsCustomer.種別.取引先)
                if (c.Is取引先())
                {
                    comboBox取引先.Items.Add(c);
                    comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
                }
            }

            // 手配依頼種別
            MsThiIraiSbt dmyThiIraiSbt = new MsThiIraiSbt();
            dmyThiIraiSbt.MsThiIraiSbtID = "";
            dmyThiIraiSbt.ThiIraiSbtName = "";
            comboBox種別.Items.Clear();
            comboBox種別.Items.Add(dmyThiIraiSbt);
            foreach (MsThiIraiSbt sbt in thiIraiSbts)
            {
                comboBox種別.Items.Add(sbt);
            }
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
            dmyThiIraiShousai.MsThiIraiShousaiID = "";
            dmyThiIraiShousai.ThiIraiShousaiName = "";
            comboBox詳細種別.Items.Clear();
            comboBox詳細種別.Items.Add(dmyThiIraiShousai);
            comboBox詳細種別.SelectedIndex = 0;

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

            // 受領日
            nullableDateTimePicker受領日From.Value = null;
            nullableDateTimePicker受領日To.Value = null;

            検索条件.条件クリア();
        }
        #endregion

        /// <summary>
        /// 一覧（TreeListView）を初期化する
        /// </summary>
        #region private void 一覧初期化()
        private void 一覧初期化()
        {
            object[,] columns = new object[,] {
                                           {"", 24, null, null},
                                           {"手配内容", 200, null, null},
                                           {"科目", 100, null, null},
                                           {"発注日", 90, null, null},
                                           {"発注番号", 100, null, null},
                                           {"業者名", 200, null, null},
                                       　　{"完了日", 90, null, null},
                                           {"金額", 90, null, HorizontalAlignment.Right}
                                         };
            TreeList = new ItemTreeListView支払合算作成(treeListView支払合算作成);
            TreeList.SetColumns(-2, columns);

            // EventHandlerを追加する
            TreeList.CheckedEvent += new ItemTreeListView支払合算作成.CheckedEventHandler(合計金額表示);
       }
        #endregion

        /// <summary>
        /// 「手配依頼種別」ＤＤＬが選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 詳細種別ＤＤＬをクリア
            comboBox詳細種別.Items.Clear();

            // 選択された種別が「修繕」の場合、詳細種別ＤＤＬを再構築
            MsThiIraiSbt selected = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (selected.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
                dmyThiIraiShousai.MsThiIraiShousaiID = "";
                dmyThiIraiShousai.ThiIraiShousaiName = "";
                comboBox詳細種別.Items.Add(dmyThiIraiShousai);

                foreach (MsThiIraiShousai shousai in thiIraiShousais)
                {
                    comboBox詳細種別.Items.Add(shousai);
                }
                comboBox詳細種別.SelectedIndex = 0;
            }
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
            // 取引先
            comboBox取引先.Text = "";

            // 手配依頼種別
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            comboBox詳細種別.Items.Clear();

            // 船
            comboBox船.SelectedIndex = 0;

            // 受領日
            nullableDateTimePicker受領日From.Value = null;
            nullableDateTimePicker受領日To.Value = null;
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
            #region 検索条件を検索条件クラスにセット
            if (comboBox取引先.SelectedItem is MsCustomer)
            {
                MsCustomer customer = comboBox取引先.SelectedItem as MsCustomer;
                検索条件.MsCustomerID = customer.MsCustomerID;
            }
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                MsThiIraiSbt thiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
                検索条件.MsThiIraiSbtID = thiIraiSbt.MsThiIraiSbtID;
            }
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
            {
                MsThiIraiShousai thiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
                検索条件.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
            }
            if (comboBox船.SelectedItem is MsVessel)
            {
                MsVessel vessel = comboBox船.SelectedItem as MsVessel;
                検索条件.MsVesselID = vessel.MsVesselID;
            }
            if (nullableDateTimePicker受領日From.Value != null)
            {
                検索条件.JryDateFrom = nullableDateTimePicker受領日From.Value as DateTime?;
            }
            if (nullableDateTimePicker受領日To.Value != null)
            {
                検索条件.JryDateTo = nullableDateTimePicker受領日To.Value as DateTime?;
            }
            #endregion
            if (検索条件.チェック() == false)
            {
                MessageBox.Show(検索条件.ErrMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Cursor = Cursors.WaitCursor;


            List<合算対象の受領> jrys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    jrys = serviceClient.BLC_合算対象の受領_GetRecordsByFilter(NBaseCommon.Common.LoginUser, 検索条件);
                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            TreeList.NodesClear();
            if (jrys != null)
            {
                TreeList.AddNodes(jrys);
            }

            this.Cursor = Cursors.Default;
        }
        #endregion


        /// <summary>
        /// 「合算」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button合算_Click(object sender, EventArgs e)
        {
            // 受領データの確認
            List<合算対象の受領> jrys = TreeList.GetCheckedNodes();
            if (jrys.Count == 0)
            {
                MessageBox.Show("受領データが選択されていません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isSameKamoku = true;
            string kamokuNo = "";
            string utiwakeKamokuNo = "";
            foreach (合算対象の受領 jry in jrys)
            {
                if (kamokuNo == "")
                {
                    kamokuNo = jry.KamokuNo;
                    utiwakeKamokuNo = jry.UtiwakeKamokuNo;
                    continue;
                }

                if (jry.KamokuNo != kamokuNo || jry.UtiwakeKamokuNo != utiwakeKamokuNo)
                {
                    isSameKamoku = false;
                    break;
                }              
            }
            if (isSameKamoku == false)
            {
                MessageBox.Show("選択された受領データは科目が違っているため合算できません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("受領データを合算します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 合算処理
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdShrGassanHead odShrGassanHead = new OdShrGassanHead();

                odShrGassanHead.OdShrGassanHeadID = Hachu.Common.CommonDefine.新規ID(false);
                odShrGassanHead.CancelFlag = NBaseCommon.Common.CancelFlag_有効;
                odShrGassanHead.MsCustomerID = 検索条件.MsCustomerID;
                odShrGassanHead.MsThiIraiSbtID = 検索条件.MsThiIraiSbtID;
                odShrGassanHead.MsThiIraiShousaiID = 検索条件.MsThiIraiShousaiID;
                odShrGassanHead.MsVesselID = 検索条件.MsVesselID;
                odShrGassanHead.Status = (int)OdShrGassanHead.StatusEnum.支払未作成;
                //odShrGassanHead.Bikou = textBox備考.Text;
                odShrGassanHead.Bikou = StringUtils.Escape(textBox備考.Text);
                odShrGassanHead.Amount = 0;
                odShrGassanHead.HachuNo = "";
                DateTime minDate = DateTime.MaxValue;
                foreach (合算対象の受領 jry in jrys)
                {
                    //odShrGassanHead.Amount += jry.Amount;
                    odShrGassanHead.Amount += (jry.Amount + jry.Carriage);
                    if (minDate > jry.HachuDate)
                    {
                        minDate = jry.HachuDate;
                        odShrGassanHead.HachuNo = jry.HachuNo;
                        odShrGassanHead.OdJryID = jry.OdJryID;
                    }
                }
                odShrGassanHead.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                odShrGassanHead.RenewDate = DateTime.Now;

                ret = serviceClient.BLC_支払合算処理(NBaseCommon.Common.LoginUser, odShrGassanHead, jrys);
            }

            if (ret)
            {
                MessageBox.Show("合算しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 閉じる
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("合算に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }


        /// <summary>
        /// 一覧（TreeListView）のチェックボックスのチェックイベントで合計金額を再表示する
        /// </summary>
        public void 合計金額表示()
        {
            decimal amount = 0;
            List<合算対象の受領> jrys = TreeList.GetCheckedNodes();
            if (jrys.Count != 0)
            {
                foreach (合算対象の受領 jry in jrys)
                {
                    //amount += jry.Amount;
                    amount += (jry.Amount + jry.Carriage);
                }
            }
            textBox合計金額.Text = NBaseCommon.Common.金額出力(amount);
        }
    }
}
