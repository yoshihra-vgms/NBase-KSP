using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseHonsen.Document.Contorol
{
    public partial class JokyoKakuninTableRow : UserControl
    {
        private const int 最大情報数 = 3;
        List<JokyoKakuninTableCell> cells = new List<JokyoKakuninTableCell>();
        public JokyoKakuninTableRow()
        {
            InitializeComponent();

            cells.Add(jokyoKakuninTableCell1);
            cells.Add(jokyoKakuninTableCell2);
            cells.Add(jokyoKakuninTableCell3);
            cells.Add(jokyoKakuninTableCell4);
        }

        public void Clear()
        {
            label_Multi1.Visible = false;
            label_Multi2.Visible = false;
            label_Single.Visible = false;

            jokyoKakuninTableCell1.clearItem();
            jokyoKakuninTableCell2.clearItem();
            jokyoKakuninTableCell3.clearItem();
            jokyoKakuninTableCell4.clearItem();

            jokyoKakuninTableCell4.Dock = DockStyle.Fill;
            jokyoKakuninTableCell4.Dock = DockStyle.None;
        }
        public void Set項目名(string koumokuName)
        {
            label_Multi1.Visible = false;
            label_Multi2.Visible = false;
            label_Single.Visible = true;

            label_Single.Text = koumokuName;

            int x = (panel_項目.Width - label_Single.Width) / 2;
            label_Single.Location = new Point(x, label_Single.Location.Y);
        }
        public void Set項目名(string koumokuName1, string koumokuName2)
        {
            label_Multi1.Visible = true;
            label_Multi2.Visible = true;
            label_Single.Visible = false;

            label_Multi1.Text = koumokuName1;
            label_Multi2.Text = koumokuName2;

            int x1 = (panel_項目.Width - label_Multi1.Width) / 2;
            label_Multi1.Location = new Point(x1, label_Multi1.Location.Y);
            int x2 = (panel_項目.Width - label_Multi2.Width) / 2;
            label_Multi2.Location = new Point(x2, label_Multi2.Location.Y);
        }

        public void Set状況(List<DmPublisher> publishers)
        {
            int cellNo = 0;
            int infoNo = 0;
            foreach (DmPublisher publisher in publishers)
            {
                if (infoNo == 最大情報数)
                {
                    infoNo = 0;
                    if (cells.Count > (cellNo + 1))
                    {
                        cellNo++;
                    }
                }
                cells[cellNo].AddItem(publisher.RenewDate, publisher.FullName);
                infoNo++;
            }
        }
        public void Set状況(bool isConfirm, IEnumerable<DmKakuninJokyo> kakuninJokyos, List<DmDocComment> comments)
        {
            int cellNo = 0;
            int infoNo = 0;
            if (isConfirm)
            {
                foreach (DmKakuninJokyo kakuninJokyo in kakuninJokyos)
                {
                    DmDocComment ddc = IsExistsComment(comments, kakuninJokyo.MsUserID);

                    if (kakuninJokyo.KakuninDate != null && kakuninJokyo.KakuninDate > DateTime.MinValue)
                    {
                        if (infoNo == 最大情報数)
                        {
                            infoNo = 0;
                            if (cells.Count > (cellNo + 1))
                            {
                                cellNo++;
                            }
                        }
                        DateTime kakuninDate = kakuninJokyo.KakuninDate;
                        string fullName = kakuninJokyo.FullName;
                        if (ddc != null)
                        {
                            kakuninDate = ddc.RegDate;
                            fullName += "[C]";
                        }

                        cells[cellNo].AddItem(kakuninDate, fullName);
                        infoNo++;
                    }
                }
            }
            else
            {
                cells[cellNo].AddItem("(確認不要)");
            }
        }
        private DmDocComment IsExistsComment(List<DmDocComment> comments, string userId)
        {
            DmDocComment ret = null;

            var c = from p in comments
                    where p.MsUserID == userId
                   orderby p.RegDate descending
                   select p;
            if (c.Count<DmDocComment>() > 0)
            {
                foreach (DmDocComment ddc in c)
                {
                    ret = ddc;
                    break;
                }
            }

            return ret;
        }
    }
}
