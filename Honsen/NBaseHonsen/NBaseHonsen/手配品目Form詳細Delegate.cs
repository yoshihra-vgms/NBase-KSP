using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NBaseData.DAC;
using NBaseUtil;
using System.Windows.Forms;

namespace NBaseHonsen
{
    partial class 手配品目Form
    {
        private class FormDelegate_ペイント : IFormDelegate
        {
            private 手配品目Form form;
            
            #region IFormDelegate メンバ

            internal FormDelegate_ペイント(手配品目Form form)
            {
                this.form = form;
            }

            public void 手配品目Form_Load(object sender, EventArgs e)
            {
                EnableComponents();
            }

            private void EnableComponents()
            {
                form.multiLineCombo詳細品目.Visible = false;

                form.textBox船用品詳細品目.Visible = true;
                form.button船用品選択.Visible = true;
                form.button船用品選択.Location = new Point(741, 19);
                form.button詳細品目削除.Location = new Point(741, 63);
            }

            public void Set品目ToModel(NBaseData.DAC.OdThiItem odThiItem)
            {
                throw new NotImplementedException();
            }

            public void Set詳細品目ToModel(NBaseData.DAC.OdThiShousaiItem item)
            {
                item.ShousaiItemName = form.textBox船用品詳細品目.Text;
                item.MsVesselItemID = form.labelMsVesselItemId.Text;
            }

            public void Set詳細品目ToControl(NBaseData.DAC.OdThiShousaiItem item)
            {
                form.textBox船用品詳細品目.Text = item.ShousaiItemName;
            }

            public void Focus詳細品目()
            {
                form.button船用品選択.Focus();
            }

            public bool ValidateFields()
            {
                for (int i = 0; i < form.odThiItem.OdThiShousaiItems.Count; i++)
                {
                    OdThiShousaiItem si = form.odThiItem.OdThiShousaiItems[i];

                    if (si.ShousaiItemName.Length == 0)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (si.ShousaiItemName.Length > 500)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (si.Bikou != null && si.Bikou.Length > 500)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目の備考（品名、規格等）は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return true;
            }

            public void Clear詳細品目()
            {
                form.textBox船用品詳細品目.Text = "";
                form.labelMsVesselItemId.Text = "";
            }


            public void ChangeForm詳細Delegate(IFormDelegate formDelegate)
            {
                EnableComponents();
            }

            #endregion

            #region IFormDelegate メンバ


            public void Set_ペイント(bool isPaint)
            {
                throw new NotImplementedException();
            }

            #endregion

            public void CreateShousai(List<DataGridViewRow> rows)
            {
                throw new NotImplementedException();
            }
        }


        private class FormDelegate_ペイント以外 : IFormDelegate
        {
             private 手配品目Form form;
            
            #region IFormDelegate メンバ

            internal FormDelegate_ペイント以外(手配品目Form form)
            {
                this.form = form;
            }

            public void 手配品目Form_Load(object sender, EventArgs e)
            {
                EnableComponents();

                List<OdThiShousaiItem> odThiShousaiItems = OdThiShousaiItem.GetRecordByThiIraiSbtID(NBaseCommon.Common.LoginUser, form.msThiIraiSbtId, SyncClient.同期Client.LOGIN_VESSEL.MsVesselID);
                var sortedList = odThiShousaiItems.OrderBy(obj => obj.ShousaiItemName);

                foreach (OdThiShousaiItem si in sortedList)
                {
                    if (form.multiLineCombo詳細品目.AutoCompleteCustomSource.Contains(si.ShousaiItemName) == false)
                        form.multiLineCombo詳細品目.AutoCompleteCustomSource.Add(si.ShousaiItemName);
                }
                form.multiLineCombo詳細品目.selected += new MultiLineCombo.SelectedEventHandler(form.詳細品目選択);
            }

            private void EnableComponents()
            {
                form.multiLineCombo詳細品目.Visible = true;
                form.multiLineCombo詳細品目.Text = null;

                form.textBox船用品詳細品目.Visible = false;
                form.button船用品選択.Visible = false;
            }

            public void Set品目ToModel(NBaseData.DAC.OdThiItem odThiItem)
            {
                throw new NotImplementedException();
            }

            public void Set詳細品目ToModel(NBaseData.DAC.OdThiShousaiItem item)
            {
                item.ShousaiItemName = StringUtils.Escape(form.multiLineCombo詳細品目.Text);
                if (form.labelMsVesselItemId.Text.Length > 0)
                {
                    item.MsVesselItemID = form.labelMsVesselItemId.Text;
                }
            }

            public void Set詳細品目ToControl(NBaseData.DAC.OdThiShousaiItem item)
            {
                form.multiLineCombo詳細品目.Text = item.ShousaiItemName;
                if (item.MsVesselItemID != null && item.MsVesselItemID.Length > 0)
                {
                    form.labelMsVesselItemId.Text = item.MsVesselItemID;
                    form.multiLineCombo詳細品目.ReadOnly = true;
                }
                else
                {
                    form.labelMsVesselItemId.Text = "";
                    form.multiLineCombo詳細品目.ReadOnly = false;
                }
            }

            public void Focus詳細品目()
            {
                form.multiLineCombo詳細品目.Focus();
            }

            public bool ValidateFields()
            {
                for (int i = 0; i < form.odThiItem.OdThiShousaiItems.Count; i++)
                {
                    OdThiShousaiItem si = form.odThiItem.OdThiShousaiItems[i];

                    if (si.ShousaiItemName != null && si.ShousaiItemName.Length == 0)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (si.ShousaiItemName != null && si.ShousaiItemName.Length > 500)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (si.Bikou != null && si.Bikou.Length > 500)
                    {
                        MessageBox.Show("No. " + (i + 1) + " の詳細品目の備考（品名、規格等）は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return true;
            }

            public void Clear詳細品目()
            {
                form.multiLineCombo詳細品目.Text = "";
            }


            public void ChangeForm詳細Delegate(IFormDelegate formDelegate)
            {
                EnableComponents();
            }

            #endregion

            #region IFormDelegate メンバ

            public void Set_ペイント(bool isPaint)
            {
                throw new NotImplementedException();
            }

            #endregion

            public void CreateShousai(List<DataGridViewRow> rows)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MsVesselItemVessel vesselItem = row.Cells["obj"].Value as MsVesselItemVessel;
                    OdThiShousaiItem shousaiItem = new OdThiShousaiItem();
                    shousaiItem.OdThiShousaiItemID = "";
                    shousaiItem.ShousaiItemName = "";
                    shousaiItem.MsTaniID = "";
                    shousaiItem.MsTaniName = "";
                    shousaiItem.ZaikoCount = int.MinValue;
                    shousaiItem.Count = int.MinValue;
                    shousaiItem.Sateisu = int.MinValue;
                    shousaiItem.Bikou = "";
                    shousaiItem.OdAttachFileID = null;
                    shousaiItem.OdAttachFileName = null;

                    // 特定船用品をセット
                    shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
                    shousaiItem.MsVesselItemID = vesselItem.MsVesselItemID;
                    shousaiItem.ShousaiItemName = vesselItem.VesselItemName;
                    shousaiItem.MsTaniID = vesselItem.MsTaniID;
                    shousaiItem.MsTaniName = vesselItem.MsTaniName;

                    int zaikoCount = 0;
                    if (row.Cells["在庫数"] != null)
                    {
                        if (int.TryParse((row.Cells["在庫数"].Value as string), out zaikoCount))
                        {
                            shousaiItem.ZaikoCount = zaikoCount;
                        }
                    }
                    int count = 0;
                    if (row.Cells["数量"].Value != null)
                    {
                        if (int.TryParse((row.Cells["数量"].Value as string), out count))
                        {
                            shousaiItem.Count = count;
                        }
                    }

                    shousaiItem.Bikou = vesselItem.Bikou;


                    form.AddOdThiShousaiItem(shousaiItem);
                }
            }

        }
    }
}
