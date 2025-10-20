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
    public partial class 運航費入力Control_単数項 : UserControl
    {
        public delegate void TextChangeHandler(object sender, EventArgs e);
        public event TextChangeHandler textChange;
        public delegate void CalcEventHandler(object sender, EventArgs e);
        public event CalcEventHandler calc;

        private BlobUnkouhi.Line3 line3;

        private Yojitsu.運航費入力Control.Amount amountType;

        private bool readOnly;


        public 運航費入力Control_単数項()
        {
            InitializeComponent();

            textBox項目.TextChanged += new EventHandler(textBox項目_TextChanged);
            textBox単価.TextChanged += new EventHandler(textBox_TextChanged);
            textBox数量.TextChanged += new EventHandler(textBox_TextChanged);
        }


        internal void SetAmountType(Yojitsu.運航費入力Control.Amount amountType)
        {
            this.amountType = amountType;
        }


        private void textBox項目_TextChanged(object sender, EventArgs e)
        {
            if (textChange != null)
            {
                textChange(this, new EventArgs());
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            textBox小計.Text = 金額出力(GetAmount());

            if (calc != null)
            {
                calc(this, new EventArgs());
            }
        }


        private string 金額出力(decimal d)
        {
            if (amountType == Yojitsu.運航費入力Control.Amount.円)
            {
                return NBaseCommon.Common.金額出力(d);
            }
            else
            {
                return NBaseCommon.Common.ドル金額出力(d);
            }
        }


        public void SetId(string id)
        {
            label単価.Text = "単価(" + id + ")";
            label数量.Text = "数量(" + id + ")";
        }


        public void SetReadOnly(bool readOnly)
        {
            textBox項目.ReadOnly = readOnly;
            textBox単価.ReadOnly = readOnly;
            textBox数量.ReadOnly = readOnly;
        }


        internal decimal GetAmount()
        {
            if (!readOnly)
            {
                decimal i1 = 0;
                decimal.TryParse(textBox単価.Text, out i1);
                decimal i2 = 0;
                decimal.TryParse(textBox数量.Text, out i2);

                return i1 * i2;
            }
            else
            {
                return NBaseCommon.Common.金額表示を数値へ変換(textBox小計.Text);
            }
        }

        public void Set項目(string val)
        {
            textBox項目.Text = val;
        }

        public string Get項目()
        {
            return textBox項目.Text;
        }

        internal void SetData(BlobUnkouhi.Line3 line3)
        {
            this.line3 = line3;

            textBox項目.Text = line3.項目;
            textBox単価.Text = line3.単価.ToString();
            textBox数量.Text = line3.数量.ToString();
        }


        internal void BuildData()
        {
            decimal i1 = 0;
            decimal.TryParse(textBox単価.Text, out i1);
            decimal i2 = 0;
            decimal.TryParse(textBox数量.Text, out i2);

            line3.項目 = textBox項目.Text;
            line3.単価 = i1;
            line3.数量 = i2;
        }


        internal void SetReadOnly()
        {
            this.readOnly = true;

            textBox項目.ReadOnly = true;
            textBox単価.ReadOnly = true;
            textBox数量.ReadOnly = true;

            //textBox単価.Text = string.Empty;
            //textBox数量.Text = string.Empty;
        }


        internal void SetTotalAmount(decimal amount)
        {
            textBox小計.Text = 金額出力(amount);

            if (calc != null)
            {
                calc(this, new EventArgs());
            }
        }
    }
}
