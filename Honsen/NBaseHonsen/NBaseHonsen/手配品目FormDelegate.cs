using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseUtil;
using System.Drawing;
using System.Windows.Forms;

namespace NBaseHonsen
{
    partial class 手配品目Form
    {
        private interface IFormDelegate
        {
            void 手配品目Form_Load(object sender, EventArgs e);

            void Set品目ToModel(OdThiItem odThiItem);

            void Set詳細品目ToModel(OdThiShousaiItem item);

            void Set詳細品目ToControl(OdThiShousaiItem item);

            void Focus詳細品目();

            bool ValidateFields();

            void Clear詳細品目();

            void ChangeForm詳細Delegate(IFormDelegate formDelegate);

            void CreateShousai(List<DataGridViewRow> rows);
        }

        private class FormDelegate_船用品 : IFormDelegate
        {
            private 手配品目Form form;
            private IFormDelegate 詳細FormDelegate;

            #region IFormDelegate メンバ

            internal FormDelegate_船用品(手配品目Form form)
            {
                this.form = form;

                if (form.odThiItem.ItemName == MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString())
                {
                    詳細FormDelegate = new FormDelegate_ペイント(form);
                }
                else
                {
                    詳細FormDelegate = new FormDelegate_ペイント以外(form);
                }
            }

            public void 手配品目Form_Load(object sender, EventArgs e)
            {
                form.textBox品目.Visible = true;
                form.buttonカテゴリ選択.Visible = true;
                form.multiLineCombo品目.Visible = false;
                form.button船用品選択.Visible = true;
                form.textBox品目.Text = form.odThiItem.ItemName;

                if (form.textBox品目.Text.Length > 0)
                {
                    form.buttonカテゴリ選択.Enabled = false;

                    List<MsVesselItemCategory> MsVesselItemCategorys = MsVesselItemCategory.GetRecords();
                    foreach (MsVesselItemCategory vic in MsVesselItemCategorys)
                    {
                        if (form.textBox品目.Text == vic.CategoryName)
                        {
                            form.msVesselItemCategoryNumber = vic.CategoryNumber;
                            break;
                        }
                    }
                }

                this.詳細FormDelegate.手配品目Form_Load(sender, e);
            }

            public void Set品目ToModel(OdThiItem odThiItem)
            {
                odThiItem.ItemName = StringUtils.Escape(form.textBox品目.Text);
            }

            public void Set詳細品目ToModel(OdThiShousaiItem item)
            {
                this.詳細FormDelegate.Set詳細品目ToModel(item);
            }

            public void Set詳細品目ToControl(OdThiShousaiItem item)
            {
                this.詳細FormDelegate.Set詳細品目ToControl(item);
            }

            public void Focus詳細品目()
            {
                this.詳細FormDelegate.Focus詳細品目();
            }

            public bool ValidateFields()
            {
                // 仕様・型式
                if (form.textBox品目.Text.Length == 0)
                {
                    MessageBox.Show("仕様・型式を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (form.textBox品目.Text.Length > 500)
                {
                    MessageBox.Show("仕様・型式は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // 詳細品目
                if (!this.詳細FormDelegate.ValidateFields())
                {
                    return false;
                }

                return true;
            }

            public void Clear詳細品目()
            {
                form.comboBox単位.SelectedIndex = -1;
                form.textBox在庫数.Text = "";
                form.textBox依頼数.Text = "";
                form.textBox備考.Text = "";

                this.詳細FormDelegate.Clear詳細品目();
            }


            public void ChangeForm詳細Delegate(IFormDelegate formDelegate)
            {
                this.詳細FormDelegate = formDelegate;
                formDelegate.ChangeForm詳細Delegate(null);
            }

            public void CreateShousai(List<DataGridViewRow> rows)
            {
                this.詳細FormDelegate.CreateShousai(rows);
            }

            #endregion
        }

        private class FormDelegate_船用品以外 : IFormDelegate
        {
            private 手配品目Form form;
            private IFormDelegate 詳細FormDelegate;

            #region IFormDelegate メンバ

            internal FormDelegate_船用品以外(手配品目Form form)
            {
                this.form = form;
                詳細FormDelegate = new FormDelegate_ペイント以外(form);
            }

            public void 手配品目Form_Load(object sender, EventArgs e)
            {
                form.textBox品目.Visible = false;
                form.buttonカテゴリ選択.Visible = false;
                form.multiLineCombo品目.Visible = true;
                form.button船用品選択.Visible = false;

                // 品目コンボボックス初期化
                form.multiLineCombo品目.Text = form.odThiItem.ItemName;

                List<MsShoushuriItem> shoushuriItems = MsShoushuriItem.GetRecords(NBaseCommon.Common.LoginUser);
                var sortedList = shoushuriItems.OrderBy(obj => obj.ItemName);
                foreach (MsShoushuriItem si in sortedList)
                {
                    if (form.multiLineCombo品目.AutoCompleteCustomSource.Contains(si.ItemName) == false)
                        form.multiLineCombo品目.AutoCompleteCustomSource.Add(si.ItemName);
                }

                this.詳細FormDelegate.手配品目Form_Load(sender, e);
            }

            public void Set品目ToModel(OdThiItem odThiItem)
            {
                //odThiItem.ItemName = form.multiLineCombo品目.Text;
                odThiItem.ItemName = StringUtils.Escape(form.multiLineCombo品目.Text);
            }

            public void Set詳細品目ToModel(OdThiShousaiItem item)
            {
                this.詳細FormDelegate.Set詳細品目ToModel(item);
            }

            public void Set詳細品目ToControl(OdThiShousaiItem item)
            {
                this.詳細FormDelegate.Set詳細品目ToControl(item);
            }

            public void Focus詳細品目()
            {
                this.詳細FormDelegate.Focus詳細品目();
            }

            public bool ValidateFields()
            {
                // 区分
                if (form.comboBox区分.SelectedItem == null)
                {
                    MessageBox.Show("区分を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // 品目
                if (form.multiLineCombo品目.Text.Length == 0)
                {
                    MessageBox.Show("仕様・型式を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (form.multiLineCombo品目.Text.Length > 500)
                {
                    MessageBox.Show("仕様・型式は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // 詳細品目
                if (!this.詳細FormDelegate.ValidateFields())
                {
                    return false;
                }

                return true;
            }

            public void Clear詳細品目()
            {
                form.comboBox単位.SelectedIndex = -1;
                form.textBox在庫数.Text = "";
                form.textBox依頼数.Text = "";
                form.textBox備考.Text = "";

                this.詳細FormDelegate.Clear詳細品目();
            }


            public void ChangeForm詳細Delegate(IFormDelegate formDelegate)
            {
                throw new NotImplementedException();
            }



            public void CreateShousai(List<DataGridViewRow> rows)
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }
}
