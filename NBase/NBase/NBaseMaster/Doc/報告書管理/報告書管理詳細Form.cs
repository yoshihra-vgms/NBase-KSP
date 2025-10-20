using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using NBaseCommon;
using ServiceReferences.NBaseService;

namespace NBaseMaster.Doc.報告書管理
{
    public partial class 報告書管理詳細Form : Form
    {
        private string DIALOG_TITLE = "報告書管理";
        public MsDmHoukokusho msDmHoukokusho = null;
        public MsDmTemplateFile msDmTemplateFile = null;
        public List<DmPublisher> dmPublishers = null;
        public List<DmKoukaiSaki> dmKoukaiSakis = null;

        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();
        List<船選択Form.ListItem> 発行元s = new List<船選択Form.ListItem>();
        List<船選択Form.ListItem> 公開先s = new List<船選択Form.ListItem>();

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public 報告書管理詳細Form()
        {
            InitializeComponent();
        }

        private void 報告書管理詳細Form_Load(object sender, EventArgs e)
        {
            this.ChangeFlag = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msDmBunruis = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
                msDmShoubunruis = serviceClient.MsDmShoubunrui_GetRecords(NBaseCommon.Common.LoginUser);

                if (msDmHoukokusho != null)
                {
                    if (msDmHoukokusho.TemplateFileName != null && msDmHoukokusho.TemplateFileName.Length > 0)
                    {
                        msDmTemplateFile = serviceClient.MsDmTemplateFile_GetRecordByHoukokushoID(NBaseCommon.Common.LoginUser, msDmHoukokusho.MsDmHoukokushoID);
                    }
                    dmPublishers = serviceClient.DmPublisher_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, msDmHoukokusho.MsDmHoukokushoID);
                    dmKoukaiSakis = serviceClient.DmKoukaiSaki_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, msDmHoukokusho.MsDmHoukokushoID);
                }
            }
            SetBunruiDDL();
            SetShoubunruiDDL();

            SetVessel();
            documentGroupCheckBox1.Text = "※発行元";
            documentGroupCheckBox2.Text = "※公開先";


            if (msDmHoukokusho != null)
            {
                var msDmBs = from p in msDmBunruis
                             where p.MsDmBunruiID == msDmHoukokusho.MsDmBunruiID
                             select p;
                if (msDmBs != null)
                {
                    comboBox_Bunrui.SelectedItem = msDmBs.First<MsDmBunrui>();
                }
                var msDmSBs = from p in msDmShoubunruis
                             where p.MsDmShoubunruiID == msDmHoukokusho.MsDmShoubunruiID
                             select p;
                if (msDmSBs != null && msDmSBs.Count<MsDmShoubunrui>() > 0)
                {
                    comboBox_Shoubunrui.SelectedItem = msDmSBs.First<MsDmShoubunrui>();
                }
                textBox_BunshoNo.Text = msDmHoukokusho.BunshoNo;
                textBox_BunshoName.Text = msDmHoukokusho.BunshoName;
                if (msDmTemplateFile != null)
                {
                    textBox_TemplateFileName.Text = msDmTemplateFile.TemplateFileName;
                }
                else
                {
                    textBox_TemplateFileName.Text = "";
                }
                documentGroupCheckBox1.Check(false);
                foreach (DmPublisher publisher in dmPublishers)
                {
                    #region  20210824 下記に変更
                    //if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長)
                    //{
                    //    documentGroupCheckBox1.Check会長社長(true);
                    //}
                    //if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                    //{
                    //    documentGroupCheckBox1.Check管理責任者(true);
                    //}
                    //if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    //{
                    //    documentGroupCheckBox1.Check船(true);
                    //    foreach (船選択Form.ListItem listItem in 発行元s)
                    //    {
                    //        if (listItem.Value == publisher.MsVesselID)
                    //        {
                    //            listItem.Checked = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                    //{
                    //    documentGroupCheckBox1.Check部門(publisher.MsBumonID, true);
                    //}
                    #endregion
                    if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    {
                        documentGroupCheckBox1.Check船(true);
                        foreach (船選択Form.ListItem listItem in 発行元s)
                        {
                            if (listItem.Value == publisher.MsVesselID)
                            {
                                listItem.Checked = true;
                                break;
                            }
                        }
                    }
                    else if (publisher.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                    {
                        documentGroupCheckBox1.Check部門(publisher.MsBumonID, true);
                    }
                    else
                    {
                        documentGroupCheckBox1.Check要員(publisher.KoukaiSaki, true);
                    }
                }
                documentGroupCheckBox1.Set船List = 発行元s;
                documentGroupCheckBox1.Refresh();
                
                documentGroupCheckBox2.Check(false);
                foreach (DmKoukaiSaki koukaiSaki in dmKoukaiSakis)
                {
                    #region 20210824 下記に変更
                    //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長)
                    //{
                    //    documentGroupCheckBox2.Check会長社長(true);
                    //}
                    //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                    //{
                    //    documentGroupCheckBox2.Check管理責任者(true);
                    //}
                    //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    //{
                    //    documentGroupCheckBox2.Check船(true);
                    //    foreach (船選択Form.ListItem listItem in 公開先s)
                    //    {
                    //        if (listItem.Value == koukaiSaki.MsVesselID)
                    //        {
                    //            listItem.Checked = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                    //{
                    //    documentGroupCheckBox2.Check部門(koukaiSaki.MsBumonID, true);
                    //}
                    #endregion

                    if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                    {
                        documentGroupCheckBox2.Check船(true);
                        foreach (船選択Form.ListItem listItem in 公開先s)
                        {
                            if (listItem.Value == koukaiSaki.MsVesselID)
                            {
                                listItem.Checked = true;
                                break;
                            }
                        }
                    }
                    else if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門)
                    {
                        documentGroupCheckBox2.Check部門(koukaiSaki.MsBumonID, true);
                    }
                    else
                    {
                        documentGroupCheckBox2.Check要員(koukaiSaki.KoukaiSaki, true);
                    }               
                }

                documentGroupCheckBox2.Set船List = 公開先s;
                documentGroupCheckBox2.Refresh();

                checkBox_CheckTarget.Checked = msDmHoukokusho.CheckTarget == 0 ? false : true;
                textBox_Shuki.Text = msDmHoukokusho.Shuki;
                writeMonth();

                List<CheckBox> JikiCheck = new List<CheckBox>();
                JikiCheck.Add(checkBox_Jiki1);
                JikiCheck.Add(checkBox_Jiki2);
                JikiCheck.Add(checkBox_Jiki3);
                JikiCheck.Add(checkBox_Jiki4);
                JikiCheck.Add(checkBox_Jiki5);
                JikiCheck.Add(checkBox_Jiki6);
                JikiCheck.Add(checkBox_Jiki7);
                JikiCheck.Add(checkBox_Jiki8);
                JikiCheck.Add(checkBox_Jiki9);
                JikiCheck.Add(checkBox_Jiki10);
                JikiCheck.Add(checkBox_Jiki11);
                JikiCheck.Add(checkBox_Jiki12);

                for (int i = 0; i < 12; i++)
                {
                    int idx = NBaseUtil.DateTimeUtils.GetMonthIndex(i + 1);
                    JikiCheck[idx].Checked = msDmHoukokusho.Jiki[i] == '0' ? false : true;
                }
            }
            else
            {
                documentGroupCheckBox1.Set船List = 発行元s;
                documentGroupCheckBox2.Set船List = 公開先s;
                writeMonth();

                button_削除.Enabled = false;
            }
        }
        private void writeMonth()
        {
            int i = 0;
            checkBox_Jiki1.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki2.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki3.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki4.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki5.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki6.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki7.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki8.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki9.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki10.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki11.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
            checkBox_Jiki12.Text = NBaseUtil.DateTimeUtils.instance().INT_MONTH[i++].ToString() + "月度";
        }

        /// <summary>
        /// 「参照」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_参照_Click(object sender, EventArgs e)
        private void button_参照_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "雛形ファイル(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                textBox_TemplateFileName.Text = openFileDialog1.FileName;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

        }
        #endregion

        /// <summary>
        /// 「クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_クリア_Click(object sender, EventArgs e)
        private void button_クリア_Click(object sender, EventArgs e)
        {
            textBox_TemplateFileName.Text = "";
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_更新_Click(object sender, EventArgs e)
        private void button_更新_Click(object sender, EventArgs e)
        {
            bool is新規作成;
            if (msDmHoukokusho == null)
            {
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            if (Validation() == false)
            {
                return;
            }

            FillInstance();

            bool ret;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (is新規作成 == true)
                {
                    ret = serviceClient.BLC_報告書処理_登録(NBaseCommon.Common.LoginUser, msDmHoukokusho, msDmTemplateFile, dmPublishers, dmKoukaiSakis);
                }
                else
                {
                    ret = serviceClient.BLC_報告書処理_更新(NBaseCommon.Common.LoginUser, msDmHoukokusho, msDmTemplateFile, dmPublishers, dmKoukaiSakis);
                }

            }

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.ChangeFlag = false;

        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_削除_Click(object sender, EventArgs e)
        private void button_削除_Click(object sender, EventArgs e)
        {
            //削除が可能かを調べる
            bool result = this.CheckDeleteUsing(this.msDmHoukokusho);
            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msDmHoukokusho.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // 2011.07.26 報告書のみしか削除していなかったので修正
                    //ret = serviceClient.MsDmHoukokusho_UpdateRecord(NBaseCommon.Common.LoginUser, msDmHoukokusho);
                    ret = serviceClient.BLC_報告書処理_削除(NBaseCommon.Common.LoginUser, msDmHoukokusho);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion




        /// <summary>
        /// 「分類」DDL選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetShoubunruiDDL();
        }
        #endregion

        /// <summary>
        /// 「分類」DDL構築
        /// </summary>
        #region private void SetBunruiDDL()
        private void SetBunruiDDL()
        {
            MsDmBunrui dmy = new MsDmBunrui();
            dmy.MsDmBunruiID = "";
            dmy.Name = "";

            comboBox_Bunrui.Items.Clear();
            comboBox_Bunrui.Items.Add(dmy);
            foreach (MsDmBunrui msDmBunrui in msDmBunruis)
            {
                comboBox_Bunrui.Items.Add(msDmBunrui);
            }
            comboBox_Bunrui.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「小分類」DDL構築
        /// </summary>
        #region private void SetShoubunruiDDL()
        private void SetShoubunruiDDL()
        {
            string bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
            var msDmSoubunruisBybunruiId = from p in msDmShoubunruis
                                           where p.MsDmBunruiID == bunruiId
                                           orderby p.Code, p.Name
                                           select p;

            MsDmShoubunrui dmy = new MsDmShoubunrui();
            dmy.MsDmShoubunruiID = "";
            dmy.Name = "";

            comboBox_Shoubunrui.Items.Clear();
            comboBox_Shoubunrui.Items.Add(dmy);
            foreach (MsDmShoubunrui msDmShoubunrui in msDmSoubunruisBybunruiId)
            {
                comboBox_Shoubunrui.Items.Add(msDmShoubunrui);
            }
            comboBox_Shoubunrui.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 発行元、公開先の船情報の初期化
        /// </summary>
        #region private void SetVessel()
        private void SetVessel()
        {
            bool checkInit = true;
            if (msDmHoukokusho != null)
            {
                checkInit = false;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //=========================
                // 船
                //=========================
                
                //コメントアウト　m.yoshihara 2017/5/30
                //List<NBaseData.DAC.MsVessel> msVessel_list = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);
                
                //-----------------------------------------------------------------------------------
                //m.yoshihara 2017/5/30
                List<NBaseData.DAC.MsVessel> wklist = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);//miho m.yoshihara 2017/5/17
                List<NBaseData.DAC.MsVessel> msVessel_list = new List<NBaseData.DAC.MsVessel>();
                foreach (NBaseData.DAC.MsVessel v in wklist)
                { 
                    if (v.DocumentEnabled == 1)
                    {
                        msVessel_list.Add(v);
                    } 
                }
                //-----------------------------------------------------------------------------------

                List<NBaseData.DAC.MsVessel> publisherVessel = new List<NBaseData.DAC.MsVessel>();
                List<NBaseData.DAC.MsVessel> koukaisakiVessel = new List<NBaseData.DAC.MsVessel>();
                List<int> vesselCheck = new List<int>();
                foreach (NBaseData.DAC.MsVessel vessel in msVessel_list)
                {
                    vesselCheck.Add(vessel.MsVesselID);
                    publisherVessel.Add(vessel);
                    koukaisakiVessel.Add(vessel);
                }

                // 発行元に指定されていて、船マスタの"文書"チェックがはずされているものを追加する
                if (dmPublishers != null)
                {
                    foreach (DmPublisher publiser in dmPublishers)
                    {
                        if (publiser.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                        {
                            if (vesselCheck.Contains(publiser.MsVesselID) == false)
                            {
                                NBaseData.DAC.MsVessel deletedVessel = serviceClient.MsVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, publiser.MsVesselID);
                                if (deletedVessel != null)
                                {
                                    publisherVessel.Add(deletedVessel);
                                }
                            }
                        }
                    }
                }
                // 公開先に指定されていて、船マスタの"文書"チェックがはずされているものを追加する
                if (dmKoukaiSakis != null)
                {
                    foreach (DmKoukaiSaki koukaisaki in dmKoukaiSakis)
                    {
                        if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                        {
                            if (vesselCheck.Contains(koukaisaki.MsVesselID) == false)
                            {
                                NBaseData.DAC.MsVessel deletedVessel = serviceClient.MsVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, koukaisaki.MsVesselID);
                                if (deletedVessel != null)
                                {
                                    koukaisakiVessel.Add(deletedVessel);
                                }
                            }
                        }
                    }
                }

                //-----------------------------------------------------
                // 発注元
                //-----------------------------------------------------
                var vesselList = from p in publisherVessel
                                 orderby p.ShowOrder
                                 select p;
                foreach (NBaseData.DAC.MsVessel vessel in vesselList)
                {
                    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, checkInit);
                    発行元s.Add(item);
                }

                //-----------------------------------------------------
                // 公開先
                //-----------------------------------------------------
                vesselList = from p in koukaisakiVessel
                                 orderby p.ShowOrder
                                 select p;
                foreach (NBaseData.DAC.MsVessel vessel in vesselList)
                {
                    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, checkInit);
                    公開先s.Add(item);
                }
            }
        }
        #endregion
        
        
        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            try
            {
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                if (msDmBunrui.MsDmBunruiID == "")
                {
                    MessageBox.Show("ドキュメント分類名を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("ドキュメント分類名を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_BunshoNo.Text.Length == 0)
            {
                MessageBox.Show("文書番号を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //try
            //{
            //    if (NBaseUtil.StringUtils.isHankaku(textBox_BunshoNo.Text) == false)
            //    {
            //        MessageBox.Show("文書番号は半角で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("文書番号は半角で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (textBox_BunshoName.Text.Length == 0)
            {
                MessageBox.Show("文書名を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool isChecked = false;
            foreach (DocumentGroupCheckBox.CheckItem item in documentGroupCheckBox1.Items)
            {
                if (item.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            if (isChecked == false)
            {
                MessageBox.Show("発行元を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            isChecked = false;
            if (documentGroupCheckBox1.Items[DocumentGroupCheckBox.船Pos].Checked)
            {
                foreach (船選択Form.ListItem item in documentGroupCheckBox1.船List)
                {
                    if (item.Checked)
                    {
                        isChecked = true;
                        break;
                    }
                }
                if (isChecked == false)
                {
                    MessageBox.Show("発行元（船）を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (documentGroupCheckBox1.IsChecked管理責任者() == true && documentGroupCheckBox1.IsCheckedOnly管理責任者() == false)
            {
                MessageBox.Show("発行元の管理責任者がチェックされている場合、他は設定できません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            isChecked = false;
            foreach (DocumentGroupCheckBox.CheckItem item in documentGroupCheckBox2.Items)
            {
                if (item.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            if (isChecked == false)
            {
                MessageBox.Show("公開先を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            isChecked = false;
            if (documentGroupCheckBox2.Items[DocumentGroupCheckBox.船Pos].Checked)
            {
                foreach (船選択Form.ListItem item in documentGroupCheckBox2.船List)
                {
                    if (item.Checked)
                    {
                        isChecked = true;
                        break;
                    }
                }
                if (isChecked == false)
                {
                    MessageBox.Show("公開先（船）を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (textBox_TemplateFileName.Text.Length > 0)
            {
                if (System.IO.Path.IsPathRooted(textBox_TemplateFileName.Text))
                {
                    if (System.IO.File.Exists(textBox_TemplateFileName.Text) == false)
                    {
                        MessageBox.Show("指定された雛形ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (NBaseCommon.FileView.CheckFileNameLength(textBox_TemplateFileName.Text) == false)
                    {
                        MessageBox.Show("指定された雛形ファイルのファイル名が長すぎます", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    int MaxSize = 1;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        SnParameter snParameter = serviceClient.SnParameter_GetRecord(NBaseCommon.Common.LoginUser);
                        MaxSize = int.Parse(snParameter.Prm3);
                    }
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(textBox_TemplateFileName.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        if (fs.Length > (MaxSize * 1024 * 1024))
                        {
                            MessageBox.Show("指定されたファイルのサイズが制限値 " + MaxSize + " MByteを超えています", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        if (fs.Length == 0)
                        {
                            MessageBox.Show("指定されたファイルのサイズが 0 Byteです", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                string msDmHoukokushoId = "";
                if (msDmHoukokusho != null)
                {
                    msDmHoukokushoId = msDmHoukokusho.MsDmHoukokushoID;
                }

                string bunruiId = "";
                string shoubunruiId = "";
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                bunruiId = msDmBunrui.MsDmBunruiID;
                if (comboBox_Shoubunrui.SelectedItem is MsDmShoubunrui)
                {
                    MsDmShoubunrui msDmShoubunrui = comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui;
                    shoubunruiId = msDmShoubunrui.MsDmShoubunruiID;
                }
                List<MsDmHoukokusho> check = serviceClient.MsDmHoukokusho_SearchRecords(NBaseCommon.Common.LoginUser, bunruiId, shoubunruiId, textBox_BunshoNo.Text, "");
                if (check != null || check.Count > 0)
                {
                    var sameName = from p in check
                                   where p.BunshoNo == textBox_BunshoNo.Text && p.MsDmHoukokushoID != msDmHoukokushoId
                                   select p;
                    if (sameName.Count<MsDmHoukokusho>() > 0)
                    {
                        //MessageBox.Show("同一な文書番号が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("同一の文書番号および文書名が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        #region private void FillInstance()
        private void FillInstance()
        {
            try
            {
                if (msDmHoukokusho == null)
                {
                    msDmHoukokusho = new MsDmHoukokusho();
                    dmPublishers = new List<DmPublisher>();
                    dmKoukaiSakis = new List<DmKoukaiSaki>();
                }
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                msDmHoukokusho.MsDmBunruiID = msDmBunrui.MsDmBunruiID;
                msDmHoukokusho.BunruiName = msDmBunrui.Name;
                MsDmShoubunrui msDmShoubunrui = comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui;
                msDmHoukokusho.MsDmShoubunruiID = msDmShoubunrui.MsDmShoubunruiID;
                msDmHoukokusho.BunshoNo = textBox_BunshoNo.Text;
                msDmHoukokusho.BunshoName = textBox_BunshoName.Text;
                msDmHoukokusho.CheckTarget = checkBox_CheckTarget.Checked ? 1 : 0;
                msDmHoukokusho.Shuki = textBox_Shuki.Text;

                List<CheckBox> JikiCheck = new List<CheckBox>();
                JikiCheck.Add(checkBox_Jiki1);
                JikiCheck.Add(checkBox_Jiki2);
                JikiCheck.Add(checkBox_Jiki3);
                JikiCheck.Add(checkBox_Jiki4);
                JikiCheck.Add(checkBox_Jiki5);
                JikiCheck.Add(checkBox_Jiki6);
                JikiCheck.Add(checkBox_Jiki7);
                JikiCheck.Add(checkBox_Jiki8);
                JikiCheck.Add(checkBox_Jiki9);
                JikiCheck.Add(checkBox_Jiki10);
                JikiCheck.Add(checkBox_Jiki11);
                JikiCheck.Add(checkBox_Jiki12);

                List<string> jikiStr = new List<string>();
                for (int i = 0; i < 12; i++)
                {
                    jikiStr.Add("0");
                }
                bool jikiChecked = false;
                for (int i = 0; i < 12; i++)
                {
                    int idx = NBaseUtil.DateTimeUtils.GetMonthIndex(i + 1);
                    jikiStr[i] = JikiCheck[idx].Checked ? "1" : "0";
                    if (JikiCheck[idx].Checked)
                    {
                        jikiChecked = true;
                    }
                }
                msDmHoukokusho.Jiki = String.Join("", jikiStr);

                FillTemplateFile();
                FillPublisherls();
                FillKoukaiSakis();

                if (msDmHoukokusho.CheckTarget == 1 && jikiChecked == false)
                {
                    MessageBox.Show(this, "提出時期が設定されていないため、アラーム情報は作成されません　", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
            return;
        }
        #endregion

        private void FillTemplateFile()
        {
            if (textBox_TemplateFileName.Text.Length == 0)
            {
                msDmHoukokusho.TemplateFileName = "";
                msDmHoukokusho.FileUpdateDate = DateTime.MinValue;
                msDmTemplateFile = null; // 登録処理時に削除フラグを立てるため、ここで一旦NULLにする
            }
            else if (textBox_TemplateFileName.Text.Length > 0 && System.IO.Path.IsPathRooted(textBox_TemplateFileName.Text))
            {
                msDmTemplateFile = new MsDmTemplateFile();

                string FullPath = textBox_TemplateFileName.Text;
                msDmHoukokusho.TemplateFileName = System.IO.Path.GetFileName(FullPath);
                msDmHoukokusho.FileUpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

                msDmTemplateFile.TemplateFileName = msDmHoukokusho.TemplateFileName;
                msDmTemplateFile.UpdateDate = msDmHoukokusho.FileUpdateDate;
                System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                msDmTemplateFile.Data = new byte[fs.Length];
                fs.Read(msDmTemplateFile.Data, 0, msDmTemplateFile.Data.Length);
                fs.Close();
            }
        }
        private void FillPublisherls()
        {
            dmPublishers = documentGroupCheckBox1.ConvertPublisherList(NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ);
        }
        private void FillKoukaiSakis()
        {
            dmKoukaiSakis = documentGroupCheckBox2.ConvertKoukaiSakiList(NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ);
        }



        /// <summary>
        /// 指定データが使用しているかを調べる
        /// 返り値：true→未使用削除可、false→使用削除不可
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsDmHoukokusho data)
        {
            // MsDmHoukokushoは
            // DM_KANRI_KIROKU
            // の１つのテーブルにリンクしている。(ER図参照)

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region DM_KANRI_KIROKU

                List<DmKanriKiroku> check1 = serviceClient.DmKanriKiroku_GetRecordsByHoukokushoID(NBaseCommon.Common.LoginUser, data.MsDmHoukokushoID);
                if (check1.Count > 0)
                {
                    return false;
                }

                #endregion

            }

            return true;
        }
    }
}
