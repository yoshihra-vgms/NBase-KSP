using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseHonsen.Controls
{
    public partial class KaniDouseiControl2 : UserControl
    {
        public delegate void AfterEditDelegate(DateTime EventDate, int MsVesselID, int Koma, PtKanidouseiInfo PtKanidouseiInfo);
        public delegate void AfterEditHandler();
        public event AfterEditHandler AfterEdit;

        /// <summary>
        /// 船の数
        /// </summary>
        public int VesselCount = 0;
        /// <summary>
        /// 表示日数
        /// </summary>
        public int MaxDate = 11;

        public List<PtKanidouseiInfo> PtKanidouseiInfos;
        public List<MsVessel> msVessel_list;
        public List<PortUserControl> Ports = new List<PortUserControl>();
        
        private DateTime BaseDay = DateTime.Today;

        public KaniDouseiControl2()
        {
            InitializeComponent();
        }

        public void Init(DateTime baseDay)
        {
            this.BaseDay = baseDay;

            tableLayoutPanel_船.SuspendLayout();
            tableLayoutPanel_日.SuspendLayout();
            tableLayoutPanel_動静.SuspendLayout();

            tableLayoutPanel_船.Controls.Clear();
            tableLayoutPanel_日.Controls.Clear();
            tableLayoutPanel_動静.Controls.Clear();

            tableLayoutPanel_動静.ColumnCount = MaxDate;
            tableLayoutPanel_動静.RowCount = msVessel_list.Count;

            Ports.Clear();
            MakeHeader(BaseDay);
            int row = 0;
            foreach (MsVessel msVessel in msVessel_list)
            {
                VesselData vessel = new VesselData();
                vessel.船名 = msVessel.VesselName;
                vessel.船長 = msVessel.CaptainName;
                vessel.電話 = msVessel.Tel;
                vessel.携帯 = msVessel.HpTel;
                vessel.MsVesselID = msVessel.MsVesselID;

                vessel.営業 = msVessel.SalesPersonName;
                vessel.工務 = msVessel.MarineSuperintendentName;

                MakeVessel(row++, vessel);
            }

            tableLayoutPanel_船.ResumeLayout();
            tableLayoutPanel_日.ResumeLayout();
            tableLayoutPanel_動静.ResumeLayout();

            SetScrollBarValues();
        }


        private void SetScrollBarValues()
        {
            vScrollBar1.Maximum = tableLayoutPanel_動静.Height;
            vScrollBar1.LargeChange = panel3.Height;

            hScrollBar1.Maximum = tableLayoutPanel_動静.Width;
            hScrollBar1.LargeChange = panel3.Width;

        }

        private void MakeVessel(int row, VesselData VesselData)
        {
            Controls.VesselUserControl VesselUserControl
                = new NBaseHonsen.Controls.VesselUserControl(VesselData.船名, VesselData.船長, VesselData.電話, VesselData.携帯, VesselData.営業, VesselData.工務);

            tableLayoutPanel_船.Controls.Add(VesselUserControl, 0, row);

            int columnIndex = 0;

            //コマは上下あるので3つづつ作成する
            for (int i = 0; i < MaxDate; i++)
            {
                Controls.DouseiUserControl DouseiUserControl = new NBaseHonsen.Controls.DouseiUserControl(BaseDay.AddDays(i), VesselData.MsVesselID, VesselData.船名);
                DouseiUserControl.AfterEdit += new AfterEditDelegate(On_AfterEdit);
                Ports.Add(DouseiUserControl.puc1);
                Ports.Add(DouseiUserControl.puc2);
                Ports.Add(DouseiUserControl.puc3);
                tableLayoutPanel_動静.Controls.Add(DouseiUserControl, columnIndex++, row);
            }
        }

        public void KandidouseiRefresh(List<PtKanidouseiInfo> ptKanidouseiInfos)
        {
            PtKanidouseiInfos = ptKanidouseiInfos;

            foreach (PtKanidouseiInfo p in PtKanidouseiInfos)
            {
                var SearchResult = from port in Ports
                                   where port.EventDate.ToShortDateString() == p.EventDate.ToShortDateString()
                                   && port.MsVesselID == p.MsVesselID
                                   && port.IsSetInfo == false
                                   select port;
                foreach (PortUserControl puc in SearchResult)
                {
                    puc.PtKanidouseiInfo = p;
                    puc.IsSetInfo = true;
                    puc.SetValue();
                    break;
                }
            }
        }

        public void KandidouseiRefresh(int msVesselId, List<PtKanidouseiInfo> ptKanidouseiInfos)
        {
            // 現在、保持している簡易動静情報から、対象の船の情報を削除する
            var kanidouseiInfos = from info in PtKanidouseiInfos
                                  where info.MsVesselID == msVesselId
                                  select info;
            List<PtKanidouseiInfo> removeList = new List<PtKanidouseiInfo>();
            foreach (PtKanidouseiInfo kanidouseiInfo in kanidouseiInfos)
            {
                removeList.Add(kanidouseiInfo);
            }
            foreach (PtKanidouseiInfo kanidouseiInfo in removeList)
            {
                PtKanidouseiInfos.Remove(kanidouseiInfo);
            }

            // 引数で渡された簡易情報を、保持リストに追加する
            PtKanidouseiInfos.AddRange(ptKanidouseiInfos);

            // 一旦、対象の船の簡易動静情報の描画をクリアする
            var SearchResult = from port in Ports
                               where port.MsVesselID == msVesselId
                               && port.IsSetInfo == true
                               select port;
            foreach (PortUserControl puc in SearchResult)
            {
                puc.PtKanidouseiInfo = new PtKanidouseiInfo();
                puc.IsSetInfo = false;
                puc.SetValue();
            }

            // 簡易動静を描画する
            foreach (PtKanidouseiInfo p in ptKanidouseiInfos)
            {
                var SearchResult2 = from port in Ports
                                   where port.EventDate.ToShortDateString() == p.EventDate.ToShortDateString()
                                   && port.MsVesselID == p.MsVesselID
                                   && port.IsSetInfo == false
                                   select port;
                foreach (PortUserControl puc in SearchResult2)
                {
                    puc.PtKanidouseiInfo = p;
                    puc.IsSetInfo = true;
                    puc.SetValue();
                    break;
                }
            }
        }


        /// <summary>
        /// 簡易動静の編集後に呼ばれるイベント
        /// </summary>
        /// <param name="EventDate"></param>
        /// <param name="MsVesselID"></param>
        /// <param name="Koma"></param>
        /// <param name="PtKanidouseiInfo"></param>
        private void On_AfterEdit(DateTime EventDate, int MsVesselID, int Koma, PtKanidouseiInfo PtKanidouseiInfo)
        {
            //// 元の位置（クリックした位置）をクリアする
            //var SearchResult = from port in Ports
            //                   where port.EventDate.ToShortDateString() == EventDate.ToShortDateString()
            //                   && port.MsVesselID == MsVesselID
            //                   && port.Koma == Koma
            //                   select port;

            //foreach (PortUserControl puc in SearchResult)
            //{
            //    puc.PtKanidouseiInfo = new PtKanidouseiInfo();
            //    puc.IsSetInfo = false;
            //    puc.SetValue();
            //}

            //// 変更後の情報をセットする
            //if (PtKanidouseiInfo.DeleteFlag != NBaseCommon.Common.DeleteFlag_削除)
            //{
            //    var SearchResult2 = from port in Ports
            //                        where port.EventDate.ToShortDateString() == PtKanidouseiInfo.EventDate.ToShortDateString()
            //                        && port.MsVesselID == PtKanidouseiInfo.MsVesselID
            //                        && port.IsSetInfo == false
            //                        select port;

            //    foreach (PortUserControl puc in SearchResult2)
            //    {
            //        puc.PtKanidouseiInfo = PtKanidouseiInfo;
            //        puc.IsSetInfo = true;
            //        puc.SetValue();
            //        break;
            //    }
            //}

            AfterEdit();
        }


        /// <summary>
        /// 引数の日付からのヘッダーを作成する。
        /// </summary>
        /// <param name="dateTime"></param>
        private void MakeHeader(DateTime dateTime)
        {
            DateTime toDay = DateTime.Today;

            int columnIndex = 0;
            while (columnIndex < MaxDate)
            {
                Controls.DateUserControl Header = new NBaseHonsen.Controls.DateUserControl();
                Header.Value = dateTime;

                // 過去は実績とする。
                if (dateTime < toDay)
                {
                    Header.IsJiseki = true;
                }
                else
                {
                    Header.IsJiseki = false;
                }
                tableLayoutPanel_日.Controls.Add(Header, columnIndex++, 0);

                dateTime = dateTime.AddDays(1);
            }
        }

        public class VesselData
        {
            public int MsVesselID { get; set; }
            public string 船名 { get; set; }
            public string 船長 { get; set; }
            public string 電話 { get; set; }
            public string 携帯 { get; set; }

            public string 営業 { get; set; }
            public string 工務 { get; set; }
        }


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            tableLayoutPanel_動静.Location = new Point(tableLayoutPanel_動静.Location.X, -e.NewValue);
            tableLayoutPanel_船.Location = new Point(tableLayoutPanel_船.Location.X, -e.NewValue);
        }


        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            tableLayoutPanel_動静.Location = new Point(-e.NewValue, tableLayoutPanel_動静.Location.Y);
            tableLayoutPanel_日.Location = new Point(-e.NewValue, tableLayoutPanel_日.Location.Y);
        }


        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            SetScrollBarValues();
        }

    }
}
