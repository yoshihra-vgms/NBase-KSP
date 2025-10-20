using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class 運航費入力Control : UserControl
    {
        public delegate void UpdateEventHandler(object sender, EventArgs e);
        public event UpdateEventHandler updated;

        public enum Amount { 円, ドル }

        private Amount amountType;

        private BlobUnkouhi.Store store;


        public 運航費入力Control()
        {
            InitializeComponent();

            // 貨物運賃
            運航費入力Control1_運賃1_1.SetId("1");
            運航費入力Control1_運賃1_2.SetId("2");
            運航費入力Control1_運賃1_3.SetId("3");
            運航費入力Control1_運賃1_4.SetId("4");

            運航費入力Control1_運賃1_1.calc += new 運航費入力Control_単数航.CalcEventHandler(貨物運賃1計算);
            運航費入力Control1_運賃1_2.calc += new 運航費入力Control_単数航.CalcEventHandler(貨物運賃1計算);
            運航費入力Control1_運賃1_3.calc += new 運航費入力Control_単数航.CalcEventHandler(貨物運賃1計算);
            運航費入力Control1_運賃1_4.calc += new 運航費入力Control_単数航.CalcEventHandler(貨物運賃1計算);

            textBox運賃2_固定費.TextChanged += new EventHandler(textBox運賃2_固定費_TextChanged);

            運航費入力Control2_運賃2_燃料費A1.SetId("A1");
            運航費入力Control2_運賃2_燃料費A2.SetId("A2");
            運航費入力Control2_運賃2_燃料費C1.SetId("C1");
            運航費入力Control2_運賃2_燃料費C2.SetId("C2");

            運航費入力Control2_運賃2_燃料費A1.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_燃料費A2.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_燃料費C1.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_燃料費C2.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_港費1.SetId("1");
            運航費入力Control2_運賃2_港費2.SetId("2");
            運航費入力Control2_運賃2_港費3.SetId("3");
            運航費入力Control2_運賃2_港費4.SetId("4");

            運航費入力Control2_運賃2_港費1.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_港費2.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_港費3.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);
            運航費入力Control2_運賃2_港費4.calc += new 運航費入力Control_単数.CalcEventHandler(貨物運賃2計算);

            // 2012.12 : Add 6Lines
            運航費入力Control_単数項1.SetId("1");
            運航費入力Control_単数項2.SetId("2");
            運航費入力Control_単数項1.calc += new 運航費入力Control_単数項.CalcEventHandler(貨物運賃2計算);
            運航費入力Control_単数項2.calc += new 運航費入力Control_単数項.CalcEventHandler(貨物運賃2計算);
            運航費入力Control_単数項1.textChange += new 運航費入力Control_単数項.TextChangeHandler(貨物運賃2項目入力);
            運航費入力Control_単数項2.textChange += new 運航費入力Control_単数項.TextChangeHandler(貨物運賃2項目入力);

            貨物運賃Checked();
            radioButton運賃1.CheckedChanged += new EventHandler(貨物運賃CheckedChanged);
            radioButton運賃2.CheckedChanged += new EventHandler(貨物運賃CheckedChanged);

            // 燃料費
            運航費入力Control2_燃料費1.SetId("A1");
            運航費入力Control2_燃料費2.SetId("A2");
            運航費入力Control2_燃料費3.SetId("C1");
            運航費入力Control2_燃料費4.SetId("C2");

            運航費入力Control2_燃料費1.calc += new 運航費入力Control_単数.CalcEventHandler(燃料費計算);
            運航費入力Control2_燃料費2.calc += new 運航費入力Control_単数.CalcEventHandler(燃料費計算);
            運航費入力Control2_燃料費3.calc += new 運航費入力Control_単数.CalcEventHandler(燃料費計算);
            運航費入力Control2_燃料費4.calc += new 運航費入力Control_単数.CalcEventHandler(燃料費計算);

            // 港費
            運航費入力Control2_港費1.SetId("1");
            運航費入力Control2_港費2.SetId("2");
            運航費入力Control2_港費3.SetId("3");
            運航費入力Control2_港費4.SetId("4");

            運航費入力Control2_港費1.calc += new 運航費入力Control_単数.CalcEventHandler(港費計算);
            運航費入力Control2_港費2.calc += new 運航費入力Control_単数.CalcEventHandler(港費計算);
            運航費入力Control2_港費3.calc += new 運航費入力Control_単数.CalcEventHandler(港費計算);
            運航費入力Control2_港費4.calc += new 運航費入力Control_単数.CalcEventHandler(港費計算);

            textBox貨物費_計.TextChanged += new EventHandler(textBox貨物費_計_TextChanged);
            textBoxその他運航費_計.TextChanged += new EventHandler(textBoxその他運航費_計_TextChanged);
        }

        
        internal void SetAmountType(Amount amountType)
        {
            this.amountType = amountType;

            運航費入力Control1_運賃1_1.SetAmountType(amountType);
            運航費入力Control1_運賃1_2.SetAmountType(amountType);
            運航費入力Control1_運賃1_3.SetAmountType(amountType);
            運航費入力Control1_運賃1_4.SetAmountType(amountType);

            運航費入力Control2_運賃2_燃料費A1.SetAmountType(amountType);
            運航費入力Control2_運賃2_燃料費A2.SetAmountType(amountType);
            運航費入力Control2_運賃2_燃料費C1.SetAmountType(amountType);
            運航費入力Control2_運賃2_燃料費C2.SetAmountType(amountType);

            運航費入力Control2_運賃2_港費1.SetAmountType(amountType);
            運航費入力Control2_運賃2_港費2.SetAmountType(amountType);
            運航費入力Control2_運賃2_港費3.SetAmountType(amountType);
            運航費入力Control2_運賃2_港費4.SetAmountType(amountType);

            // 2012.12 : Add 2Lines
            運航費入力Control_単数項1.SetAmountType(amountType);
            運航費入力Control_単数項2.SetAmountType(amountType);

            運航費入力Control2_燃料費1.SetAmountType(amountType);
            運航費入力Control2_燃料費2.SetAmountType(amountType);
            運航費入力Control2_燃料費3.SetAmountType(amountType);
            運航費入力Control2_燃料費4.SetAmountType(amountType);

            運航費入力Control2_港費1.SetAmountType(amountType);
            運航費入力Control2_港費2.SetAmountType(amountType);
            運航費入力Control2_港費3.SetAmountType(amountType);
            運航費入力Control2_港費4.SetAmountType(amountType);
        }


        private void textBox運賃2_固定費_TextChanged(object sender, EventArgs e)
        {
            貨物運賃2計算(sender, e);
        }


        private void textBox貨物費_計_TextChanged(object sender, EventArgs e)
        {
            Updated();
        }


        private void textBoxその他運航費_計_TextChanged(object sender, EventArgs e)
        {
            Updated();
        }


        private void Updated()
        {
            if (updated != null)
            {
                updated(this, new EventArgs());
            }
        }


        private void 貨物運賃1計算(object sender, EventArgs e)
        {
            decimal total = 0;

            total += 運航費入力Control1_運賃1_1.GetAmount();
            total += 運航費入力Control1_運賃1_2.GetAmount();
            total += 運航費入力Control1_運賃1_3.GetAmount();
            total += 運航費入力Control1_運賃1_4.GetAmount();

            textBox運賃1_計.Text = 金額出力(total);

            Updated();
        }


        private void 貨物運賃2計算(object sender, EventArgs e)
        {
            decimal total = 0;

            decimal a = 0;
            //decimal.TryParse(textBox運賃2_固定費.Text, out a);
            a = NBaseCommon.Common.金額表示を数値へ変換(textBox運賃2_固定費.Text);
            total += a;
            total += 運航費入力Control2_運賃2_燃料費A1.GetAmount();
            total += 運航費入力Control2_運賃2_燃料費A2.GetAmount();
            total += 運航費入力Control2_運賃2_燃料費C1.GetAmount();
            total += 運航費入力Control2_運賃2_燃料費C2.GetAmount();
            total += 運航費入力Control2_運賃2_港費1.GetAmount();
            total += 運航費入力Control2_運賃2_港費2.GetAmount();
            total += 運航費入力Control2_運賃2_港費3.GetAmount();
            total += 運航費入力Control2_運賃2_港費4.GetAmount();

            // 2012.12 : Add 2Lines
            total += 運航費入力Control_単数項1.GetAmount();
            total += 運航費入力Control_単数項2.GetAmount();
            
            textBox運賃2_計.Text = 金額出力(total);

            Updated();
        }

        private void 貨物運賃2項目入力(object sender, EventArgs e)
        {
            Updated();
        }

        private void 貨物運賃CheckedChanged(object sender, EventArgs e)
        {
            貨物運賃Checked();

            Updated();
        }


        private void 貨物運賃Checked()
        {
            bool check = radioButton運賃1.Checked ? false : true;

            // 運賃(1)
            運航費入力Control1_運賃1_1.SetReadOnly(check);
            運航費入力Control1_運賃1_2.SetReadOnly(check);
            運航費入力Control1_運賃1_3.SetReadOnly(check);
            運航費入力Control1_運賃1_4.SetReadOnly(check);

            // 運賃(2)
            textBox運賃2_固定費.ReadOnly = !check;
            運航費入力Control2_運賃2_燃料費A1.SetReadOnly(!check);
            運航費入力Control2_運賃2_燃料費A2.SetReadOnly(!check);
            運航費入力Control2_運賃2_燃料費C1.SetReadOnly(!check);
            運航費入力Control2_運賃2_燃料費C2.SetReadOnly(!check);
            運航費入力Control2_運賃2_港費1.SetReadOnly(!check);
            運航費入力Control2_運賃2_港費2.SetReadOnly(!check);
            運航費入力Control2_運賃2_港費3.SetReadOnly(!check);
            運航費入力Control2_運賃2_港費4.SetReadOnly(!check);

            // 2012.12 : Add 2Lines
            運航費入力Control_単数項1.SetReadOnly(!check);
            運航費入力Control_単数項2.SetReadOnly(!check);
        }


        private void 燃料費計算(object sender, EventArgs e)
        {
            decimal total = 0;

            total += 運航費入力Control2_燃料費1.GetAmount();
            total += 運航費入力Control2_燃料費2.GetAmount();
            total += 運航費入力Control2_燃料費3.GetAmount();
            total += 運航費入力Control2_燃料費4.GetAmount();

            textBox燃料費_計.Text = 金額出力(total);

            Updated();
        }


        private void 港費計算(object sender, EventArgs e)
        {
            decimal total = 0;

            total += 運航費入力Control2_港費1.GetAmount();
            total += 運航費入力Control2_港費2.GetAmount();
            total += 運航費入力Control2_港費3.GetAmount();
            total += 運航費入力Control2_港費4.GetAmount();

            textBox港費_計.Text = 金額出力(total);

            Updated();
        }


        private string 金額出力(decimal d)
        {
            if (amountType == Amount.円)
            {
                return NBaseCommon.Common.金額出力(d);
            }
            else
            {
                return NBaseCommon.Common.ドル金額出力(d);
            }
        }


        internal void SetData(NBaseData.DS.BlobUnkouhi.Store store)
        {
            this.store = store;

            // 運賃(1) or 運賃(2)
            if (store.運賃1_Checked)
            {
                radioButton運賃1.Checked = true;
            }
            else
            {
                radioButton運賃2.Checked = true;
            }
            
            // 運賃(1)
            運航費入力Control1_運賃1_1.SetData(store.運賃1[0]);
            運航費入力Control1_運賃1_2.SetData(store.運賃1[1]);
            運航費入力Control1_運賃1_3.SetData(store.運賃1[2]);
            運航費入力Control1_運賃1_4.SetData(store.運賃1[3]);

            // 運賃(2)
            textBox運賃2_固定費.Text = store.運賃2_固定費.ToString();
            運航費入力Control2_運賃2_燃料費A1.SetData(store.運賃2_燃料費[0]);
            運航費入力Control2_運賃2_燃料費A2.SetData(store.運賃2_燃料費[1]);
            運航費入力Control2_運賃2_燃料費C1.SetData(store.運賃2_燃料費[2]);
            運航費入力Control2_運賃2_燃料費C2.SetData(store.運賃2_燃料費[3]);
            運航費入力Control2_運賃2_港費1.SetData(store.運賃2_港費[0]);
            運航費入力Control2_運賃2_港費2.SetData(store.運賃2_港費[1]);
            運航費入力Control2_運賃2_港費3.SetData(store.運賃2_港費[2]);
            運航費入力Control2_運賃2_港費4.SetData(store.運賃2_港費[3]);

            // 2012.12 : Add 6Lines
            if (store.運賃2_追加費 == null)
            {
                store.Add追加運賃();
            }
            運航費入力Control_単数項1.SetData(store.運賃2_追加費[0]);
            運航費入力Control_単数項2.SetData(store.運賃2_追加費[1]);

            // 燃料費
            運航費入力Control2_燃料費1.SetData(store.燃料費[0]);
            運航費入力Control2_燃料費2.SetData(store.燃料費[1]);
            運航費入力Control2_燃料費3.SetData(store.燃料費[2]);
            運航費入力Control2_燃料費4.SetData(store.燃料費[3]);

            // 港費
            運航費入力Control2_港費1.SetData(store.港費[0]);
            運航費入力Control2_港費2.SetData(store.港費[1]);
            運航費入力Control2_港費3.SetData(store.港費[2]);
            運航費入力Control2_港費4.SetData(store.港費[3]);

            // 貨物費
            textBox貨物費_計.Text = store.貨物費.ToString();

            // その他運航費
            textBoxその他運航費_計.Text = store.その他運航費.ToString();
        }


        internal void BuildData()
        {
            // 運賃(1) or 運賃(2)
            if (radioButton運賃1.Checked)
            {
                store.運賃1_Checked = true;
            }
            else
            {
                store.運賃1_Checked = false;
            }
            
            // 運賃(1)
            運航費入力Control1_運賃1_1.BuildData();
            運航費入力Control1_運賃1_2.BuildData();
            運航費入力Control1_運賃1_3.BuildData();
            運航費入力Control1_運賃1_4.BuildData();

            // 運賃(2)
            decimal i1 = 0;
            decimal.TryParse(textBox運賃2_固定費.Text, out i1);
            store.運賃2_固定費 = i1;
            運航費入力Control2_運賃2_燃料費A1.BuildData();
            運航費入力Control2_運賃2_燃料費A2.BuildData();
            運航費入力Control2_運賃2_燃料費C1.BuildData();
            運航費入力Control2_運賃2_燃料費C2.BuildData();
            運航費入力Control2_運賃2_港費1.BuildData();
            運航費入力Control2_運賃2_港費2.BuildData();
            運航費入力Control2_運賃2_港費3.BuildData();
            運航費入力Control2_運賃2_港費4.BuildData();

            // 2012.12 : Add 2Lines
            運航費入力Control_単数項1.BuildData();
            運航費入力Control_単数項2.BuildData();

            // 燃料費
            運航費入力Control2_燃料費1.BuildData();
            運航費入力Control2_燃料費2.BuildData();
            運航費入力Control2_燃料費3.BuildData();
            運航費入力Control2_燃料費4.BuildData();

            // 港費
            運航費入力Control2_港費1.BuildData();
            運航費入力Control2_港費2.BuildData();
            運航費入力Control2_港費3.BuildData();
            運航費入力Control2_港費4.BuildData();

            // 貨物費
            decimal i2 = 0;
            decimal.TryParse(textBox貨物費_計.Text, out i2);
            store.貨物費 = i2;

            // その他運航費
            decimal i3 = 0;
            decimal.TryParse(textBoxその他運航費_計.Text, out i3);
            store.その他運航費 = i3;
        }


        internal void SetReadOnly()
        {
            // 貨物運賃
            運航費入力Control1_運賃1_1.SetReadOnly();
            運航費入力Control1_運賃1_2.SetReadOnly();
            運航費入力Control1_運賃1_3.SetReadOnly();
            運航費入力Control1_運賃1_4.SetReadOnly();

            textBox運賃2_固定費.ReadOnly = true;

            運航費入力Control2_運賃2_燃料費A1.SetReadOnly();
            運航費入力Control2_運賃2_燃料費A2.SetReadOnly();
            運航費入力Control2_運賃2_燃料費C1.SetReadOnly();
            運航費入力Control2_運賃2_燃料費C2.SetReadOnly();

            運航費入力Control2_運賃2_港費1.SetReadOnly();
            運航費入力Control2_運賃2_港費2.SetReadOnly();
            運航費入力Control2_運賃2_港費3.SetReadOnly();
            運航費入力Control2_運賃2_港費4.SetReadOnly();

            // 2012.12 : Add 2Lines
            運航費入力Control_単数項1.SetReadOnly();
            運航費入力Control_単数項2.SetReadOnly();

            radioButton運賃1.Enabled = false;
            radioButton運賃2.Enabled = false;

            // 燃料費
            運航費入力Control2_燃料費1.SetReadOnly();
            運航費入力Control2_燃料費2.SetReadOnly();
            運航費入力Control2_燃料費3.SetReadOnly();
            運航費入力Control2_燃料費4.SetReadOnly();

            // 港費
            運航費入力Control2_港費1.SetReadOnly();
            運航費入力Control2_港費2.SetReadOnly();
            運航費入力Control2_港費3.SetReadOnly();
            運航費入力Control2_港費4.SetReadOnly();

            textBox貨物費_計.ReadOnly = true;
            textBoxその他運航費_計.ReadOnly = true;
        }


        internal void CalcTotal(運航費入力Control YenControl, 運航費入力Control DollarControl, decimal rate)
        {
            // 貨物運賃
            運航費入力Control1_運賃1_1.SetTotalAmount(YenControl.運航費入力Control1_運賃1_1.GetAmount() + DollarControl.運航費入力Control1_運賃1_1.GetAmount() * rate);
            運航費入力Control1_運賃1_2.SetTotalAmount(YenControl.運航費入力Control1_運賃1_2.GetAmount() + DollarControl.運航費入力Control1_運賃1_2.GetAmount() * rate);
            運航費入力Control1_運賃1_3.SetTotalAmount(YenControl.運航費入力Control1_運賃1_3.GetAmount() + DollarControl.運航費入力Control1_運賃1_3.GetAmount() * rate);
            運航費入力Control1_運賃1_4.SetTotalAmount(YenControl.運航費入力Control1_運賃1_4.GetAmount() + DollarControl.運航費入力Control1_運賃1_4.GetAmount() * rate);

            textBox運賃2_固定費.Text = NBaseCommon.Common.金額出力(NBaseCommon.Common.金額表示を数値へ変換(YenControl.textBox運賃2_固定費.Text) + NBaseCommon.Common.金額表示を数値へ変換(DollarControl.textBox運賃2_固定費.Text) * rate);

            運航費入力Control2_運賃2_燃料費A1.SetTotalAmount(YenControl.運航費入力Control2_運賃2_燃料費A1.GetAmount() + DollarControl.運航費入力Control2_運賃2_燃料費A1.GetAmount() * rate);
            運航費入力Control2_運賃2_燃料費A2.SetTotalAmount(YenControl.運航費入力Control2_運賃2_燃料費A2.GetAmount() + DollarControl.運航費入力Control2_運賃2_燃料費A2.GetAmount() * rate);
            運航費入力Control2_運賃2_燃料費C1.SetTotalAmount(YenControl.運航費入力Control2_運賃2_燃料費C1.GetAmount() + DollarControl.運航費入力Control2_運賃2_燃料費C1.GetAmount() * rate);
            運航費入力Control2_運賃2_燃料費C2.SetTotalAmount(YenControl.運航費入力Control2_運賃2_燃料費C2.GetAmount() + DollarControl.運航費入力Control2_運賃2_燃料費C2.GetAmount() * rate);

            運航費入力Control2_運賃2_港費1.SetTotalAmount(YenControl.運航費入力Control2_運賃2_港費1.GetAmount() + DollarControl.運航費入力Control2_運賃2_港費1.GetAmount() * rate);
            運航費入力Control2_運賃2_港費2.SetTotalAmount(YenControl.運航費入力Control2_運賃2_港費2.GetAmount() + DollarControl.運航費入力Control2_運賃2_港費2.GetAmount() * rate);
            運航費入力Control2_運賃2_港費3.SetTotalAmount(YenControl.運航費入力Control2_運賃2_港費3.GetAmount() + DollarControl.運航費入力Control2_運賃2_港費3.GetAmount() * rate);
            運航費入力Control2_運賃2_港費4.SetTotalAmount(YenControl.運航費入力Control2_運賃2_港費4.GetAmount() + DollarControl.運航費入力Control2_運賃2_港費4.GetAmount() * rate);

            // 2012.12 : Add 2Lines
            運航費入力Control_単数項1.SetTotalAmount(YenControl.運航費入力Control_単数項1.GetAmount() + DollarControl.運航費入力Control_単数項1.GetAmount() * rate);
            運航費入力Control_単数項2.SetTotalAmount(YenControl.運航費入力Control_単数項2.GetAmount() + DollarControl.運航費入力Control_単数項2.GetAmount() * rate);

            // 燃料費
            運航費入力Control2_燃料費1.SetTotalAmount(YenControl.運航費入力Control2_燃料費1.GetAmount() + DollarControl.運航費入力Control2_燃料費1.GetAmount() * rate);
            運航費入力Control2_燃料費2.SetTotalAmount(YenControl.運航費入力Control2_燃料費2.GetAmount() + DollarControl.運航費入力Control2_燃料費2.GetAmount() * rate);
            運航費入力Control2_燃料費3.SetTotalAmount(YenControl.運航費入力Control2_燃料費3.GetAmount() + DollarControl.運航費入力Control2_燃料費3.GetAmount() * rate);
            運航費入力Control2_燃料費4.SetTotalAmount(YenControl.運航費入力Control2_燃料費4.GetAmount() + DollarControl.運航費入力Control2_燃料費4.GetAmount() * rate);

            // 港費
            運航費入力Control2_港費1.SetTotalAmount(YenControl.運航費入力Control2_港費1.GetAmount() + DollarControl.運航費入力Control2_港費1.GetAmount() * rate);
            運航費入力Control2_港費2.SetTotalAmount(YenControl.運航費入力Control2_港費2.GetAmount() + DollarControl.運航費入力Control2_港費2.GetAmount() * rate);
            運航費入力Control2_港費3.SetTotalAmount(YenControl.運航費入力Control2_港費3.GetAmount() + DollarControl.運航費入力Control2_港費3.GetAmount() * rate);
            運航費入力Control2_港費4.SetTotalAmount(YenControl.運航費入力Control2_港費4.GetAmount() + DollarControl.運航費入力Control2_港費4.GetAmount() * rate);

            textBox貨物費_計.Text = NBaseCommon.Common.金額出力(NBaseCommon.Common.金額表示を数値へ変換(YenControl.textBox貨物費_計.Text) + NBaseCommon.Common.金額表示を数値へ変換(DollarControl.textBox貨物費_計.Text) * rate);
            textBoxその他運航費_計.Text = NBaseCommon.Common.金額出力(NBaseCommon.Common.金額表示を数値へ変換(YenControl.textBoxその他運航費_計.Text) + NBaseCommon.Common.金額表示を数値へ変換(DollarControl.textBoxその他運航費_計.Text) * rate);
        }

        // 2012.12 : Add Method
        internal void Set項目(運航費入力Control YenControl, 運航費入力Control DollarControl)
        {
            string 項目1 = "";
            string 項目2 = "";
            if (YenControl != null)
            {
                項目1 = YenControl.運航費入力Control_単数項1.Get項目();
                項目2 = YenControl.運航費入力Control_単数項2.Get項目();
            }
            else if (DollarControl != null)
            {
                項目1 = DollarControl.運航費入力Control_単数項1.Get項目();
                項目2 = DollarControl.運航費入力Control_単数項2.Get項目();
            }
            運航費入力Control_単数項1.Set項目(項目1);
            運航費入力Control_単数項2.Set項目(項目2);
        }
    }
}
