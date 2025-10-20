using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Yojitsu.util;

namespace Yojitsu
{
    public partial class 船稼働設定日時Form : Form
    {
        private 船稼働設定Form parentForm;
        private TreeListViewSubItem subItem;
        
        private BgYosanHead yosanHead;
        private BgKadouVessel kadouVessel;


        public 船稼働設定日時Form(船稼働設定Form parentForm, TreeListViewSubItem subItem, BgYosanHead yosanHead, BgKadouVessel kadouVessel)
        {
            this.parentForm = parentForm;
            this.subItem = subItem;
            this.yosanHead = yosanHead;
            this.kadouVessel = kadouVessel;
            
            InitializeComponent();

            if (parentForm.configType == 船稼働設定Form.ConfigType.船稼働設定)
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "船稼働設定日時", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }
            else
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "検査設定日時", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }

            Init(kadouVessel.VesselName);
        }

        private void Init(string vesselName)
        {
            textBox船名.Text = vesselName;

            maskedTextBox稼働開始.Text = kadouVessel.KadouStartDate.ToShortDateString();
            comboBox稼働開始.SelectedItem = 船稼働設定Form.AmOrPm(kadouVessel.KadouStartDate);
            maskedTextBox稼働終了.Text = kadouVessel.KadouEndDate.ToShortDateString();
            comboBox稼働終了.SelectedItem = 船稼働設定Form.AmOrPm(kadouVessel.KadouEndDate);

            comboBox点検種別.SelectedItem = kadouVessel.NyukyoKind;
            comboBox入渠月.SelectedItem = kadouVessel.NyukyoMonth.ToString();
            textBox入渠前.Text = kadouVessel.Fukadoubi1.ToString();
            textBox入渠.Text = kadouVessel.Fukadoubi2.ToString();
            textBox出渠後.Text = kadouVessel.Fukadoubi3.ToString();

            if (comboBox点検種別.SelectedItem == null || ((string)comboBox点検種別.SelectedItem) == string.Empty)
            {
                comboBox入渠月.Enabled = false;
                textBox入渠前.ReadOnly = true;
                textBox入渠.ReadOnly = true;
                textBox出渠後.ReadOnly = true;
            }

            if (kadouVessel.KadouStartDate == DateTime.MinValue && kadouVessel.KadouEndDate == DateTime.MinValue)
            {
                checkBox不稼働.Checked = true;

                maskedTextBox稼働開始.Text = kadouVessel.Year + "/04/01";
                comboBox稼働開始.SelectedItem = "AM";
                maskedTextBox稼働終了.Text = (kadouVessel.Year + 1) + "/03/31";
                comboBox稼働終了.SelectedItem = "PM";
            }

            if (kadouVessel.EigyouKisoFlag == 1)
            {
                checkBox営業基礎.Checked = true;
            }

            if (kadouVessel.KanriKisoFlag == 1)
            {
                checkBox管理基礎.Checked = true;
            }

            if (yosanHead.IsFixed())
            {
                comboBox点検種別.Enabled = false;
                comboBox入渠月.Enabled = false;
                textBox入渠前.ReadOnly = true;
                textBox入渠.ReadOnly = true;
                textBox出渠後.ReadOnly = true;

                maskedTextBox稼働開始.ReadOnly = true;
                comboBox稼働開始.Enabled = false;
                maskedTextBox稼働終了.ReadOnly = true;
                comboBox稼働終了.Enabled = false;
                checkBox不稼働.Enabled = false;

                checkBox営業基礎.Enabled = false;
                checkBox管理基礎.Enabled = false;
            
                buttonOK.Enabled = false;
            }
            else if (parentForm.configType == 船稼働設定Form.ConfigType.船稼働設定)
            {
                comboBox点検種別.Enabled = false;
                comboBox入渠月.Enabled = false;
                textBox入渠前.ReadOnly = true;
                textBox入渠.ReadOnly = true;
                textBox出渠後.ReadOnly = true;
            }
            else
            {
                maskedTextBox稼働開始.ReadOnly = true;
                comboBox稼働開始.Enabled = false;
                maskedTextBox稼働終了.ReadOnly = true;
                comboBox稼働終了.Enabled = false;
                checkBox不稼働.Enabled = false;

                checkBox営業基礎.Enabled = false;
                checkBox管理基礎.Enabled = false;
            }
        }
        

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(ValidateFields())
            {
                if (checkBox不稼働.Checked)
                {
                    kadouVessel.KadouStartDate = DateTime.MinValue;
                    kadouVessel.KadouEndDate = DateTime.MinValue;
                    kadouVessel.NyukyoKind = null;
                    kadouVessel.NyukyoMonth = 0;
                    kadouVessel.Fukadoubi1 = 0;
                    kadouVessel.Fukadoubi2 = 0;
                    kadouVessel.Fukadoubi3 = 0;
                }
                else
                {
                    kadouVessel.KadouStartDate = ToDate(maskedTextBox稼働開始.Text, (string)comboBox稼働開始.SelectedItem);
                    kadouVessel.KadouEndDate = ToDate(maskedTextBox稼働終了.Text, (string)comboBox稼働終了.SelectedItem);

                    if (comboBox点検種別.SelectedItem == null || comboBox点検種別.Text == string.Empty)
                    {
                        kadouVessel.NyukyoKind = null;
                        kadouVessel.NyukyoMonth = 0;
                        kadouVessel.Fukadoubi1 = 0;
                        kadouVessel.Fukadoubi2 = 0;
                        kadouVessel.Fukadoubi3 = 0;
                    }
                    else
                    {
                        kadouVessel.NyukyoKind = (string)comboBox点検種別.SelectedItem;
                        kadouVessel.NyukyoMonth = Int32.Parse((string)comboBox入渠月.SelectedItem);
                        kadouVessel.Fukadoubi1 = decimal.Parse(textBox入渠前.Text);
                        kadouVessel.Fukadoubi2 = decimal.Parse(textBox入渠.Text);
                        kadouVessel.Fukadoubi3 = decimal.Parse(textBox出渠後.Text);
                    }
                }

                kadouVessel.EigyouKisoFlag = checkBox営業基礎.Checked ? 1 : 0;
                kadouVessel.KanriKisoFlag = checkBox管理基礎.Checked ? 1 : 0;
                
                subItem.Text = TreeListViewDelegate船稼働設定.Create船稼働String(kadouVessel);
                Dispose();
            }
        }

        private DateTime ToDate(string dateStr, string amOrPm)
        {
            if (amOrPm == "PM")
            {
                dateStr += " 12:00:00";
            }

            DateTime dateTime = DateTime.Parse(dateStr);
 
            return dateTime;
        }

        private bool ValidateFields()
        {
            DateTime startDate;

            if (!DateTime.TryParse(maskedTextBox稼働開始.Text, out startDate))
            {
                MessageBox.Show("稼働開始日の書式が正しくありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (startDate < DateTime.Parse(kadouVessel.Year + "/04/01"))
            {
                MessageBox.Show("稼働開始日は年度の最初日（4月1日）以降を指定してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DateTime endDate;

            if (!DateTime.TryParse(maskedTextBox稼働終了.Text, out endDate))
            {
                MessageBox.Show("稼働終了日の書式が正しくありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (DateTime.Parse((kadouVessel.Year + 1)+ "/03/31") < endDate)
            {
                MessageBox.Show("稼働終了日は年度の最終日（3月31日）以前を指定してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (endDate < startDate)
            {
                MessageBox.Show("稼働終了日は稼働開始日より後の日を指定してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (comboBox点検種別.SelectedIndex > 0 && comboBox入渠月.SelectedItem == null)
            {
                MessageBox.Show("入渠月を選択してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            decimal date;

            if (!decimal.TryParse(textBox入渠前.Text, out date) || date < 0 || 99 < date)
            {
                MessageBox.Show("入渠前日数は 0 ～ 99 の半角数字を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(textBox入渠.Text, out date) || date < 0 || 99 < date)
            {
                MessageBox.Show("入渠日数は 0 ～ 99 の半角数字を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(textBox出渠後.Text, out date) || date < 0 || 99 < date)
            {
                MessageBox.Show("出渠後日数は 0 ～ 99 の半角数字を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
           
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox点検種別_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox点検種別.SelectedIndex == 0)
            {
                comboBox入渠月.Enabled = false;
                textBox入渠前.ReadOnly = true;
                textBox入渠.ReadOnly = true;
                textBox出渠後.ReadOnly = true;
            }
            else
            {
                comboBox入渠月.Enabled = true;
                textBox入渠前.ReadOnly = false;
                textBox入渠.ReadOnly = false;
                textBox出渠後.ReadOnly = false;
            }
        }

        private void checkBox不稼働_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                maskedTextBox稼働開始.ReadOnly = true;
                comboBox稼働開始.Enabled = false;
                maskedTextBox稼働終了.ReadOnly = true;
                comboBox稼働終了.Enabled = false;

                comboBox点検種別.Enabled = false;

                comboBox入渠月.Enabled = false;
                textBox入渠前.ReadOnly = true;
                textBox入渠.ReadOnly = true;
                textBox出渠後.ReadOnly = true;
            }
            else
            {
                maskedTextBox稼働開始.ReadOnly = false;
                comboBox稼働開始.Enabled = true;
                maskedTextBox稼働終了.ReadOnly = false;
                comboBox稼働終了.Enabled = true;

                comboBox点検種別.Enabled = true;

                if (comboBox点検種別.SelectedIndex > 0)
                {
                    comboBox入渠月.Enabled = true;
                    textBox入渠前.ReadOnly = false;
                    textBox入渠.ReadOnly = false;
                    textBox出渠後.ReadOnly = false;
                }
            }
        }
    }
}
