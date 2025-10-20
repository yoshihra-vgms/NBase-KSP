using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WtmModelBase;
using WtmModels;
using NBaseData.BLC;
using NBaseData.DAC;
using WtmData;

namespace WTM
{
    public partial class 出勤Form : Form
    {
        private 出退勤登録Form PForm;

        private DateTime TODAY = DateTime.MinValue;
        private DateTime preDate = DateTime.MinValue;

        private DateTime startDate = DateTime.MinValue;

        private Dictionary<TextBox, List<Label>> InputText_Dic = new Dictionary<TextBox, List<Label>>();

        public 出勤Form(出退勤登録Form f)
        {
            InitializeComponent();

            groupBox1.MouseClick += new MouseEventHandler(ClickEvent);
            groupBox2.MouseClick += new MouseEventHandler(ClickEvent);

            PForm = f;

            TODAY = DateTime.Now;
        }

        private void 出勤Form_Load(object sender, EventArgs e)
        {
            //フォームの制御
            if (PForm != null)
            {
                PForm.Visible = false;
            }
            this.TopMost = true;

            // 終了メッセージ非表示
            panel_Message.Visible = false;

            InputText_Dic.Add(textBox勤務開始日, new List<Label>() { label勤務開始日, label勤務開始日説明 });
            InputText_Dic.Add(textBox勤務開始時間, new List<Label>() { label勤務開始時間, label勤務開始時間説明 });

            //勤務開始時間
            label日付.Text = "　" + TODAY.ToString("yyyy/MM/dd（ddd）");
            label_Now.Text = TODAY.ToString("H:mm:ss");

            //勤務開始日、時間
            textBox勤務開始日.Text = TODAY.ToString("yyMMdd");
            textBox勤務開始時間.Text = TODAY.ToString("HHmm");


            timer1.Start();


            //フォーカス
            this.ActiveControl = textBox勤務開始日;
            textBox勤務開始日.Focus();
            textBox勤務開始日.SelectionStart = textBox勤務開始日.Text.Length;
            textBox_Enter(textBox勤務開始日, null);

        }

        #region ボタンクリックイベント
        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            DateTime dateStart = DateTime.MinValue;
            if (ValidateFields(out dateStart))
            {
                var ret = WtmAccessor.Instance().StartWork(PForm.Vessel.MsVesselID, PForm.SeninId, dateStart);
                if (ret)
                {
                    tableLayoutPanel1.Enabled = false;
                    panel_Message.Visible = true;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
            //フォームの制御
            if (PForm != null)
            {
                PForm.Visible = true;
            }
        }
        private void button_確認_Click(object sender, EventArgs e)
        {
            Close();
            //フォームの制御
            if (PForm != null)
            {
                PForm.Visible = true;
            }
        }
        #endregion


        private void textBox_Enter(object sender, EventArgs e)
        {
            foreach (var tb in InputText_Dic.Keys)
            {
                if ((sender as TextBox) == tb)
                {
                    InputText_Dic[tb].ForEach(o => { o.ForeColor = Color.Blue; });
                }
                else
                {
                    InputText_Dic[tb][0].ForeColor = SystemColors.ControlText;
                    InputText_Dic[tb][1].ForeColor = Color.DimGray;
                }
            }
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            InputText_Dic[(sender as TextBox)][0].ForeColor = SystemColors.ControlText;
            InputText_Dic[(sender as TextBox)][1].ForeColor = Color.DimGray;
        }
        private void ClickEvent(object sender, EventArgs e)
        {
            buttonStartWork.Focus();
        }

        /// <summary>
        /// 終了日、時間の入力の制限をする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox勤務終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        private bool ValidateFields(out DateTime dt)
        {
            dt = DateTime.MinValue;

            if (textBox勤務開始日.Text.Length != 6)
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }
            if (textBox勤務開始時間.Text.Length != 4)
            {
                MessageBox.Show("正しい時間を入力してください。");
                return false;
            }

            string str = textBox勤務開始日.Text.Substring(0, 2) + "/" + textBox勤務開始日.Text.Substring(2, 2) + "/" + textBox勤務開始日.Text.Substring(4, 2);
            DateTime outDateTime = DateTime.MinValue;

            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }
            dt = outDateTime.Date;

            str = textBox勤務開始時間.Text.Substring(0, 2) + ":" + textBox勤務開始時間.Text.Substring(2, 2);
            outDateTime = DateTime.MinValue;
            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい時間を入力してください。");
                return false;
            }

            dt = dt.AddHours(outDateTime.Hour).AddMinutes(outDateTime.Minute);

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_Now.Text = DateTime.Now.ToString("H:mm:ss");


            if (preDate == DateTime.MinValue)
            {
                preDate = TODAY;
            }

            if (preDate.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                TODAY = DateTime.Today;


                preDate = TODAY;

                label日付.Text = "　" + TODAY.ToString("yyyy/MM/dd（ddd）");

            }
        }

    }
}
