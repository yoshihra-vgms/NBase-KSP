using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseCommon;
using ServiceReferences.NBaseService;

namespace Document
{
    public partial class 公開先設定Form : Form
    {
        public 状況確認一覧Row RowData = null;

        private string DIALOG_TITLE = "公開先設定";
        
        private List<DmKoukaiSaki> dmKoukaiSakis = null;
        private List<船選択Form.ListItem> 公開先s = new List<船選択Form.ListItem>();

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public 公開先設定Form()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        private void 公開先設定Form_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
        private void button_登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            dmKoukaiSakis = documentGroupCheckBox1.ConvertKoukaiSakiList(NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則);
            
            bool ret;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_公文書規則処理_公開先更新(NBaseCommon.Common.LoginUser, RowData.DmKoubunshoKisokuId, dmKoukaiSakis);
            }

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// フォームに情報をセットする
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            label_Bunrui.Text = "： " + RowData.分類名;
            label_Shoubunrui.Text = "： " + RowData.小分類名;
            label_BunshoNo.Text = "： " + RowData.文書番号;
            label_BunshoName.Text = "： " + RowData.文書名;

            SetKoukaisaki();

        }
        #endregion

        /// <summary>
        /// 公開先の船情報の初期化
        /// </summary>
        #region private void SetKoukaisaki()
        private void SetKoukaisaki()
        {
            documentGroupCheckBox1.Text = "※公開先";
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //=========================
                // 船
                //=========================
                List<NBaseData.DAC.MsVessel> msVessel_list = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);

                //-----------------------------------------------------
                // 公開先
                //-----------------------------------------------------
                foreach (NBaseData.DAC.MsVessel vessel in msVessel_list)
                {
                    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, false);
                    公開先s.Add(item);
                }

                dmKoukaiSakis = serviceClient.DmKoukaiSaki_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, RowData.DmKoubunshoKisokuId);
            }

            documentGroupCheckBox1.Check(false);
            foreach (DmKoukaiSaki koukaiSaki in dmKoukaiSakis)
            {
                //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長社長)
                //{
                //    documentGroupCheckBox1.Check会長社長(true);
                //}
                //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                //{
                //    documentGroupCheckBox1.Check管理責任者(true);
                //}
                //if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                //{
                //    documentGroupCheckBox1.Check船(true);
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
                //    documentGroupCheckBox1.Check部門(koukaiSaki.MsBumonID, true);
                //}


                if (koukaiSaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    documentGroupCheckBox1.Check船(true);
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
                    documentGroupCheckBox1.Check部門(koukaiSaki.MsBumonID, true);
                }
                else
                {
                    documentGroupCheckBox1.Check要員(koukaiSaki.KoukaiSaki, true);
                }
            }
            documentGroupCheckBox1.Set船List = 公開先s;
            documentGroupCheckBox1.Refresh();
        }
        #endregion

        
        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
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
                MessageBox.Show("公開先を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("公開先（船）を設定してください", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<DmKakuninJokyo> check = serviceClient.DmKakuninJokyo_GetRecordsByLinkSaki(NBaseCommon.Common.LoginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則, RowData.DmKoubunshoKisokuId);
                isChecked = false;
                foreach (DmKakuninJokyo kj in check)
                {
                    if (kj.KakuninDate != DateTime.MinValue)
                    {
                        isChecked = true;
                        break;
                    }
                }
                if (isChecked)
                {
                    MessageBox.Show("すでに確認されているため、変更できません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
