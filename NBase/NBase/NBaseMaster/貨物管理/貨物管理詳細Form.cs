using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster.貨物管理
{
    public partial class 貨物管理詳細Form : Form
    {
        public MsCargo Cargo = null;
        public List<MsYusoItem> msYusoItems = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 貨物管理詳細Form()
        {
            InitializeComponent();
        }

        private void 貨物管理詳細Form_Load(object sender, EventArgs e)
        {
            //#region 輸送品目
            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    msYusoItems = serviceClient.MsYusoItem_GetRecords(NBaseCommon.Common.LoginUser);
            //}
            //YusoItem_comboBox.Items.Clear();
            //MsYusoItem dmy = new MsYusoItem();
            //dmy.MsYusoItemID = -1;
            //dmy.YusoItemName = "";
            //YusoItem_comboBox.Items.Add(dmy);
            //foreach (MsYusoItem msYusoItem in msYusoItems)
            //{
            //    YusoItem_comboBox.Items.Add(msYusoItem);
            //}
            //#endregion

            if (Cargo == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                CargoNo_textBox.Text = Cargo.CargoNo;
                CargoName_textBox.Text = Cargo.CargoName;
                Ninushi_textBox.Text = Cargo.Ninushi;
                CargoNo_textBox.ReadOnly = true;

                //for (int i = 0; i < YusoItem_comboBox.Items.Count; i++)
                //{
                //    MsYusoItem y = YusoItem_comboBox.Items[i] as MsYusoItem;
                //    if (y.MsYusoItemID == Cargo.MsYusoItemID)
                //    {
                //        YusoItem_comboBox.SelectedIndex = i;
                //        break;
                //    }
                //}
            }
            this.ChangeFlag = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            bool is新規作成;
            if (Cargo == null)
            {
                Cargo = new MsCargo();

                bool is重複;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    is重複 = serviceClient.MsCargo_ExistCargo(NBaseCommon.Common.LoginUser, CargoNo_textBox.Text);
                }

                if (is重複 == true)
                {
                    MessageBox.Show("貨物Noが重複しています", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            Cargo.CargoNo = CargoNo_textBox.Text;
            Cargo.CargoName = CargoName_textBox.Text;
            Cargo.Ninushi = Ninushi_textBox.Text;
            //if (YusoItem_comboBox.SelectedItem is MsYusoItem)
            //{
            //    MsYusoItem y = YusoItem_comboBox.SelectedItem as MsYusoItem;
            //    Cargo.MsYusoItemID = y.MsYusoItemID;
            //}

            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsCargo_InsertRecord(NBaseCommon.Common.LoginUser, Cargo);
                    //ret = serviceClient.BLC_貨物マスタ更新処理_追加処理(NBaseCommon.Common.LoginUser, Cargo);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsCargo_UpdateRecord(NBaseCommon.Common.LoginUser, Cargo);
                    //ret = serviceClient.BLC_貨物マスタ更新処理_更新処理(NBaseCommon.Common.LoginUser, Cargo);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Validation()
        {
            if (CargoNo_textBox.Text.Length != 3)
            {
                MessageBox.Show("貨物Noは3桁を入力して下さい", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (CargoName_textBox.Text == "")
            {
                MessageBox.Show("貨物名を入力して下さい", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 削除チェック
        /// 引数：チェック対象
        /// 返り値：true⇒削除を許す
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsCargo data)
        {
            //DJ_DOUSEI_CARGO
            //MS_CARGO_YUSO_ITEM

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region DjDouseiCargo

                List<DjDouseiCargo> calist = 
                    serviceClient.DjDouseiCargo_GetRecordsMsCargoID(NBaseCommon.Common.LoginUser, data.MsCargoID);

                if (calist.Count > 0)
                {
                    return false;
                }

                #endregion

                #region MsCargoYusoItem

                MsCargoYusoItem cargoYusoItem =
                    serviceClient.MsCargoYusoItem_GetRecordByMsCargoID(NBaseCommon.Common.LoginUser, data.MsCargoID);

                if (cargoYusoItem != null)
                {
                    return false;
                }

                #endregion
            }

            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", "貨物管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //削除チェック
                bool result = this.CheckDeleteUsing(this.Cargo);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsCargo_DeleteRecord(NBaseCommon.Common.LoginUser, Cargo);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", "貨物管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }
    }
}
