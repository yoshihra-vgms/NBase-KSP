using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.util;
using Yojitsu.DA;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class 予算コピーForm : Form
    {
        private TreeListViewSubItem subItem;
        private Dictionary<TreeListViewSubItem, CellItem> hasYosanItemCellItems;

        private CurrencyFactory currency;
        
            
        public 予算コピーForm(TreeListViewSubItem subItem, Dictionary<TreeListViewSubItem, CellItem> hasYosanItemCellItems, BgYosanHead yosanHead)
        {
            this.subItem = subItem;
            this.hasYosanItemCellItems = hasYosanItemCellItems;
            this.currency = CurrencyFactory.Create(subItem);
            
            InitializeComponent();
            Init(hasYosanItemCellItems[subItem], yosanHead);
        }


        private void Init(CellItem yojitsu, BgYosanHead yosanHead)
        {
            // 年次
            if (yojitsu.GetNengetsu().Trim().Length == 4)
            {
                string year = yojitsu.GetNengetsu().Trim();

                textBoxコピー元年度.Text = year;

                int yearRange = NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID);
                for (int i = 0; i < yearRange; i++)
                {
                    if (int.Parse(year) < yosanHead.Year + i)
                    {
                        comboBoxコピー先開始年.Items.Add(yosanHead.Year + i);
                        comboBoxコピー先終了年.Items.Add(yosanHead.Year + i);
                    }
                }

                comboBoxコピー先開始年.SelectedIndex = 0;
                comboBoxコピー先終了年.SelectedIndex = comboBoxコピー先終了年.Items.Count - 1;

                comboBoxコピー先開始月.Enabled = false;
                comboBoxコピー先終了月.Enabled = false;
            }
            // 月次
            else if (yojitsu.GetNengetsu().Trim().Length == 6)
            {
                string year = yosanHead.Year.ToString();
                string month = yojitsu.GetNengetsu().Substring(4);

                textBoxコピー元年度.Text = year;

                comboBoxコピー先開始年.Items.Add(yosanHead.Year.ToString());
                comboBoxコピー先終了年.Items.Add(yosanHead.Year.ToString());

                bool f = false;
                for (int i = 0; i < DateTimeUtils.instance().MONTH.Length; i++)
                {
                    string m = DateTimeUtils.instance().MONTH[i].Trim();
                    if (m.Length == 1)
                    {
                        m = "0" + m;
                    }

                    if (month == m)
                    {
                        textBoxコピー元月.Text = DateTimeUtils.instance().MONTH[i];
                        f = true;
                        continue;
                    }

                    if (f)
                    {
                        comboBoxコピー先開始月.Items.Add(DateTimeUtils.instance().MONTH[i]);
                        comboBoxコピー先終了月.Items.Add(DateTimeUtils.instance().MONTH[i]);
                    }
                }

                comboBoxコピー先開始年.SelectedIndex = 0;
                comboBoxコピー先終了年.SelectedIndex = 0;

                comboBoxコピー先開始月.SelectedIndex = 0;
                comboBoxコピー先終了月.SelectedIndex = comboBoxコピー先終了月.Items.Count - 1;
            }

            textBoxコピー金額.Text = currency.金額出力(yojitsu);
        }

        
        private void button設定_Click(object sender, EventArgs e)
        {
            CellItem srcYojitsu = hasYosanItemCellItems[subItem];
            
            for(int i = 0; i < subItem.Parent.SubItems.Count; i++)
            {
                TreeListViewSubItem si = subItem.Parent.SubItems[i];

                if (hasYosanItemCellItems.ContainsKey(si))
                {
                    CellItem dstYojitsu = hasYosanItemCellItems[si];

                    int start = int.Parse(comboBoxコピー先開始年.SelectedItem.ToString());
                    int end = int.Parse(comboBoxコピー先終了年.SelectedItem.ToString());
                    
                    // 年次
                    if (srcYojitsu.GetNengetsu().Trim().Length == 4 && dstYojitsu.GetNengetsu().Trim().Length == 4)
                    {
                        int n = int.Parse(dstYojitsu.GetNengetsu().Trim());

                        if (start <= n && n <= end)
                        {
                            ((TextBox)si.Control).Text = currency.Amount(srcYojitsu).ToString();
                            si.Text = currency.金額出力(srcYojitsu);
                            hasYosanItemCellItems[si].SetNewAmount(currency.Amount(srcYojitsu));

                            hasYosanItemCellItems[si].NotifyParents();
                        }
                    }
                    // 月次
                    else if (srcYojitsu.GetNengetsu().Trim().Length == 6 && dstYojitsu.GetNengetsu().Trim().Length == 6)
                    {
                        int sm = int.Parse(comboBoxコピー先開始月.SelectedItem.ToString());
                        if (1 <= sm && sm <= 3)
                        {
                            start++;
                        }
                        
                        int em = int.Parse(comboBoxコピー先終了月.SelectedItem.ToString());
                        if (1 <= em && em <= 3)
                        {
                            end++;
                        }
                      
                        start = start * 100 + int.Parse(comboBoxコピー先開始月.SelectedItem.ToString());
                        end = end * 100 + int.Parse(comboBoxコピー先終了月.SelectedItem.ToString());
                        
                        int n = int.Parse(dstYojitsu.GetNengetsu().Trim());

                        if (start <= n && n <= end)
                        {
                            ((TextBox)si.Control).Text = currency.Amount(srcYojitsu).ToString();
                            si.Text = currency.金額出力(srcYojitsu);
                            hasYosanItemCellItems[si].SetNewAmount(currency.Amount(srcYojitsu));

                            hasYosanItemCellItems[si].NotifyParents();
                        }
                    }
                }
            }
            
            Dispose();
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
