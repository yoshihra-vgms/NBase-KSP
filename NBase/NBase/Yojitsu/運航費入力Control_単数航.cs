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
    public partial class 運航費入力Control_単数航 : UserControl
    {
        public delegate void CalcEventHandler(object sender, EventArgs e);
        public event CalcEventHandler calc;

        private BlobUnkouhi.Line1 line1;

        private Yojitsu.運航費入力Control.Amount amountType;

        private bool readOnly;
        
        
        public 運航費入力Control_単数航()
        {
            InitializeComponent();

            textBox単価.TextChanged += new EventHandler(textBox_TextChanged);
            textBox数量.TextChanged += new EventHandler(textBox_TextChanged);
            textBox航海数.TextChanged += new EventHandler(textBox_TextChanged);
        }


        internal void SetAmountType(Yojitsu.運航費入力Control.Amount amountType)
        {
            this.amountType = amountType;
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
            label航海数.Text = "航海数(" + id + ")";
        }


        public void SetReadOnly(bool readOnly)
        {
            textBox単価.ReadOnly = readOnly;
            textBox数量.ReadOnly = readOnly;
            textBox航海数.ReadOnly = readOnly;
        }


        internal decimal GetAmount()
        {
            if (!readOnly)
            {
                decimal i1 = 0;
                decimal.TryParse(textBox単価.Text, out i1);
                decimal i2 = 0;
                decimal.TryParse(textBox数量.Text, out i2);
                decimal i3 = 0;
                decimal.TryParse(textBox航海数.Text, out i3);

                return i1 * i2 * i3;
            }
            else
            {
                return NBaseCommon.Common.金額表示を数値へ変換(textBox小計.Text);
            }
        }


        internal void SetData(BlobUnkouhi.Line1 line1)
        {
            this.line1 = line1;

            textBox単価.Text = line1.単価.ToString();
            textBox数量.Text = line1.数量.ToString();
            textBox航海数.Text = line1.航海数.ToString();
        }


        internal void BuildData()
        {
            decimal i1 = 0;
            decimal.TryParse(textBox単価.Text, out i1);
            decimal i2 = 0;
            decimal.TryParse(textBox数量.Text, out i2);
            decimal i3 = 0;
            decimal.TryParse(textBox航海数.Text, out i3);

            line1.単価 = i1;
            line1.数量 = i2;
            line1.航海数 = i3;
        }


        internal void SetReadOnly()
        {
            this.readOnly = true;
            
            textBox単価.ReadOnly = true;
            textBox数量.ReadOnly = true;
            textBox航海数.ReadOnly = true;

            //textBox単価.Text = string.Empty;
            //textBox数量.Text = string.Empty;
            //textBox航海数.Text = string.Empty;
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
