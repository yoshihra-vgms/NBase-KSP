using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using WtmModelBase;

namespace WTM
{
    public sealed partial class Template1 : Template
    {
        private int BaseCellWidth = 35;//55;
        private int OneCharWidth_全 = 15;
        private int OneCharWidth_半 = 7;
        public int BaseCellX = 210;
        public int BaseCellY = 10;

        public Template1()
        {
            InitializeComponent();

        }

        public CellStyle GetCellStyle_WorkContent(Color forecolor, Color backcolor)
        {
            RoundedBorder roundedBorder = new RoundedBorder();
            roundedBorder.AllCornerRadius = 0.2F;
            roundedBorder.Outline = new Line(LineStyle.Thin, backcolor);

            CellStyle cellStyle = new CellStyle();
            cellStyle.Border = roundedBorder;
            cellStyle.BackColor = backcolor;
            cellStyle.ForeColor = forecolor;
            cellStyle.Font = new Font(labelCellDate.Style.Font.FontFamily, 12);

            return cellStyle;
        }


        public Size GetCellWidth(string str)
        {
            int charnum = str.Length;
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

            int strwidth = 0;
            for (int i = 0; i < str.Length; i++)
            {
                string wk = str.Substring(i, 1);

                int num = sjisEnc.GetByteCount(wk);
                if (num == 2)
                {
                    //全角
                    strwidth += OneCharWidth_全;
                }
                else
                {
                    //半角
                    strwidth += OneCharWidth_半;

                    //System.Diagnostics.Debug.WriteLine("半--" + wk + "--");
                }
            }

            return new Size(strwidth + BaseCellWidth, 30);
        }

    }
}

