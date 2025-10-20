using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Senin.util;
using NBaseUtil;
using System.IO;
using NBaseData.DS;

namespace Senin
{
    public partial class 船用金送金詳細Form : Form
    {
        private 船用金送金Form parentForm;
        private SiSoukin soukin;

        private TreeListViewDelegate船用金送金配乗情報 treeListViewDelegate;

        private DateTime getsujiShimeDate;


        public 船用金送金詳細Form(船用金送金Form parentForm) : this(parentForm, new SiSoukin())
        {
        }


        public 船用金送金詳細Form(船用金送金Form parentForm, SiSoukin soukin)
        {
            this.parentForm = parentForm;
            this.soukin = soukin;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            treeListViewDelegate = new TreeListViewDelegate船用金送金配乗情報(treeListView1);

            InitComboBox船();
            InitComboBox代理店();

            LoadGetsujiShime();
            
            InitFields();
        }


        private void LoadGetsujiShime()
        {
            SiGetsujiShime getsujiShime = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                getsujiShime = serviceClient.SiGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            if (getsujiShime == null)
            {
                MessageBox.Show("直近の月次締めレコードが見つかりません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            getsujiShimeDate = getsujiShime.月次締日.AddDays(1);
        }


        private void InitComboBox船()
        {
            //コメントアウト　m.yoshihara 2017/6/1
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            //{
            //    comboBox船.Items.Add(v)
            //}

            ////m.yoshihara 2017/6/1
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselListBySeninEnabled(NBaseCommon.Common.LoginUser))
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                if (v.SeninEnabled == 1)
                {
                    comboBox船.Items.Add(v);
                }
            }
        }


        private void InitComboBox代理店()
        {
            foreach (MsCustomer customer in SeninTableCache.instance().GetMsCustomerList(NBaseCommon.Common.LoginUser))
            {
                comboBox代理店.Items.Add(customer);
            }
        }


        private void InitFields()
        {
            if (!soukin.IsNew())
            {
                foreach (MsVessel m in comboBox船.Items)
                {
                    if (m.MsVesselID == soukin.MsVesselID)
                    {
                        comboBox船.SelectedItem = m;
                        break;
                    }
                }

                dateTimePicker送金日.Value = soukin.SoukinDate;

                LoadData();

                textBox食費.Text = soukin.Shokuhi.ToString();
                textBox旅費.Text = soukin.Ryohi.ToString();
                textBoxその他.Text = soukin.Sonotahi.ToString();
                textBox送金金額.Text = soukin.Kingaku.ToString();
                textBox備考.Text = soukin.Bikou;

                foreach (MsCustomer customer in comboBox代理店.Items)
                {
                    if (customer.MsCustomerID == soukin.MsCustomerID)
                    {
                        comboBox代理店.SelectedItem = customer;
                    }
                }
                // 2010.06.28 代理店が削除されている場合の対応
                if (soukin.MsCustomerID != null && soukin.MsCustomerID.Length > 0 && comboBox代理店.SelectedItem == null)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        MsCustomer customer = serviceClient.MsCustomer_GetRecord(NBaseCommon.Common.LoginUser, soukin.MsCustomerID);
                        if (customer != null)
                        {
                            comboBox代理店.Items.Add(customer);
                            comboBox代理店.SelectedItem = customer;
                        }
                    }
                }

                // 締めた月のレコード、または送金受入を行ったレコードは編集できない.
                if (soukin.SoukinDate < getsujiShimeDate || soukin.UkeireDate != DateTime.MinValue)
                {
                    comboBox船.Enabled = false;
                    dateTimePicker送金日.Enabled = false;
                    textBox食費.ReadOnly = true;
                    textBox旅費.ReadOnly = true;
                    textBoxその他.ReadOnly = true;
                    textBox送金金額.ReadOnly = true;
                    button計算.Enabled = false;
                    comboBox代理店.Enabled = false;
                    textBox備考.ReadOnly = true;

                    button更新.Enabled = false;
                    button削除.Enabled = false;
                }
            }
            else
            {
                soukin.AlarmInfoList = new List<PtAlarmInfo>();
                soukin.AlarmInfoList.Add(new PtAlarmInfo());

                dateTimePicker送金日.Value = DateTime.Now;
                button削除.Enabled = false;
            }
        }

        
        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    parentForm.Search船用金送金();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (comboBox船.SelectedItem == null)
            {
                comboBox船.BackColor = Color.Pink;
                MessageBox.Show("船を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox船.BackColor = Color.White;
                return false;
            }
            else if (textBox食費.Text.Length > 9)
            {
                textBox食費.BackColor = Color.Pink;
                MessageBox.Show("食費は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox食費.BackColor = Color.White;
                return false;
            }
            else if (textBox旅費.Text.Length > 9)
            {
                textBox旅費.BackColor = Color.Pink;
                MessageBox.Show("旅費は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox旅費.BackColor = Color.White;
                return false;
            }
            else if (textBoxその他.Text.Length > 9)
            {
                textBoxその他.BackColor = Color.Pink;
                MessageBox.Show("その他は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxその他.BackColor = Color.White;
                return false;
            }
            else if (textBox送金金額.Text.Length > 9)
            {
                textBox送金金額.BackColor = Color.Pink;
                MessageBox.Show("送金金額は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox送金金額.BackColor = Color.White;
                return false;
            }
            else if (comboBox代理店.SelectedItem == null)
            {
                comboBox代理店.BackColor = Color.Pink;
                MessageBox.Show("代理店を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox代理店.BackColor = Color.White;
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                textBox備考.BackColor = Color.Pink;
                MessageBox.Show("備考は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox備考.BackColor = Color.White;
                return false;
            }
            else if (dateTimePicker送金日.Value < getsujiShimeDate)
            {
                dateTimePicker送金日.BackColor = Color.Pink;
                MessageBox.Show("締日以前の日付は入力できません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker送金日.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            soukin.MsVesselID = soukin.VesselID = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            soukin.SoukinUserID = NBaseCommon.Common.LoginUser.MsUserID;
            soukin.SoukinDate = dateTimePicker送金日.Value;
            int amount;
            int.TryParse(textBox食費.Text, out amount);
            soukin.Shokuhi = amount;
            int.TryParse(textBox旅費.Text, out amount);
            soukin.Ryohi = amount;
            int.TryParse(textBoxその他.Text, out amount);
            soukin.Sonotahi = amount;
            int.TryParse(textBox送金金額.Text, out amount);
            soukin.Kingaku = amount;
            //soukin.Bikou = textBox備考.Text;
            soukin.Bikou = StringUtils.Escape(textBox備考.Text);
            soukin.MsCustomerID = (comboBox代理店.SelectedItem as MsCustomer).MsCustomerID;

            // PtAlarmInfo
            if (soukin.AlarmInfoList.Count > 0)
            {
                FillInstance_Alarm_新規(soukin.AlarmInfoList[0]);
            }
        }


        private void FillInstance_Alarm_新規(PtAlarmInfo alarm)
        {
            alarm.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
            alarm.MsPortalInfoKubunId = ((int)MsPortalInfoKubun.MsPortalInfoKubunIdEnum.遅延).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.送金受入).ToString();

            PtPortalInfoFormat infoFormat = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                infoFormat = serviceClient.PtPortalInfoFormat_GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId);
            }

            alarm.MsVesselId = soukin.MsVesselID;

            alarm.Naiyou = infoFormat.Naiyou;
            string vesselName = (comboBox船.SelectedItem as MsVessel).VesselName;
            alarm.Shousai = String.Format(infoFormat.Shousai, vesselName);

            alarm.HasseiDate = soukin.SoukinDate.AddDays(7);
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.BLC_新規送金(NBaseCommon.Common.LoginUser, soukin);
            }

            return result;
        }

        
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                soukin.DeleteFlag = 1;
                
                if (soukin.AlarmInfoList.Count > 0)
                {
                    soukin.AlarmInfoList[0].DeleteFlag = 1;
                }

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    parentForm.Search船用金送金();
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        
        private void button計算_Click(object sender, EventArgs e)
        {
            decimal shokuhi;
            decimal.TryParse(textBox食費.Text, out shokuhi);
            decimal ryohi;
            decimal.TryParse(textBox旅費.Text, out ryohi);
            decimal sonota;
            decimal.TryParse(textBoxその他.Text, out sonota);

            textBox送金金額.Text = (shokuhi + ryohi + sonota).ToString();
        }


        private void comboBox船_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadData();
        }

        
        private void dateTimePicker送金日_ValueChanged(object sender, EventArgs e)
        {
            if (treeListViewDelegate != null)
            {
                LoadData();
            }
        }

        
        private void LoadData()
        {
            if (comboBox船.SelectedItem != null)
            {
                List<SiCard> cards;
                decimal 先月末残高;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    int msVesselId = (comboBox船.SelectedItem as MsVessel).MsVesselID;
                    
                    SiCardFilter filter = new SiCardFilter();
                    filter.MsVesselIDs.Add(msVesselId);
                    filter.Start = filter.End = dateTimePicker送金日.Value;
                    filter.RetireFlag = 0;

                    cards = serviceClient.SiCard_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
                    先月末残高 = serviceClient.SiJunbikin_Get_先月末残高(NBaseCommon.Common.LoginUser, dateTimePicker送金日.Value, msVesselId);
                }

                treeListViewDelegate.SetRows(Create配乗情報RowData(cards));
                textBox繰越金額.Text = NBaseCommon.Common.金額出力(先月末残高);
            }
        }


        private Dictionary<int, int> Create配乗情報RowData(List<SiCard> cards)
        {
            Dictionary<int, int> rows = new Dictionary<int, int>();
            int rowCount = 0;
            
            foreach (SiCard c in cards)
            {
                foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
                {
                    if (!rows.ContainsKey(link.MsSiShokumeiID))
                    {
                        rows[link.MsSiShokumeiID] = 0;
                    }

                    rows[link.MsSiShokumeiID]++;
                    rowCount++;
                    break;
                }
            }

            label合計人数.Text = "合計 " + rowCount + "人";
            
            return rows;
        }


        private void button送金通知出力_Click(object sender, EventArgs e)
        {
            if(soukin.IsNew())
            {
                MessageBox.Show("更新して下さい", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                //2013/12/18 追加 m.y
                //サーバーエラー時のフラグ
                bool serverError = false;
                
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        //--------------------------------
                        //2013/12/18 コメントアウト m.y
                        //result = serviceClient.BLC_Excel_送金通知出力(NBaseCommon.Common.LoginUser, soukin.SiSoukinID);
                        //--------------------------------
                        //2013/12/18 変更: ServiceClientのExceptionを受け取る m.y
                        try
                        {
                            result = serviceClient.BLC_Excel_送金通知出力(NBaseCommon.Common.LoginUser, soukin.SiSoukinID);
                        }
                        catch (Exception exp) 
                        {
                            //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                        }
                        //--------------------------------
                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //--------------------------------
                //2013/12/18 追加 m.y 
                if (serverError == true)
                    return;
                
                if (result == null)
                {
                    MessageBox.Show("送金通知の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "講習管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return;
                }
                //--------------------------------

                System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(result, 0, result.Length);
                filest.Close();

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void comboBox代理店_SelectedValueChanged(object sender, EventArgs e)
        {
            MsCustomer c = comboBox代理店.SelectedItem as MsCustomer;

            textBox銀行名.Text = c.BankName;
            textBox支店名.Text = c.BranchName;
            textBox口座番号.Text = c.AccountNo;
            textBox口座名義.Text = c.AccountId;
        }


        private void comboBox代理店_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox代理店.SelectedItem == null)
            {
                textBox銀行名.Text = null;
                textBox支店名.Text = null;
                textBox口座番号.Text = null;
                textBox口座名義.Text = null;
            }
        }
    }
}
