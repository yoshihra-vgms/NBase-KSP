using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;
using DeficiencyControl.Controls;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;


namespace DeficiencyControl.Schedule.ListGrid
{
    public partial class ScheduleListHeaderControl : ScheduleVesselMonthControl
    {
        public ScheduleListHeaderControl()
        {
            InitializeComponent();


            DcGlobal.EnableDoubleBuffering(this.panelHeader);
            DcGlobal.EnableDoubleBuffering(this.labelMonth);
            DcGlobal.EnableDoubleBuffering(this.tableLayoutPanelHeader);
            DcGlobal.EnableDoubleBuffering(this.tableLayoutPanelData);
        }



        private void InitTableControl()
        {
            //テーブルの参考基準となるサイズを計算する
            ScheduleItemControl item = new ScheduleItemControl();

            //縦のデータ数と横のデータ数を計算
            //横は船数 + その他分
            int w = this.FData.VesselList.Count + 1;

            //縦はデータ数
            this.FData.MaxDataCount = 1;
            int h = this.FData.MaxDataCount;


            //データオフセットを計算しておく            
            this.FData.DataOffset.Y = 0;
            this.FData.DataOffset.X = 0;

            //ヘッダー
            {
                TableLayoutPanel tpane = this.tableLayoutPanelHeader;
                tpane.ColumnCount = 1;
                tpane.RowCount = this.FData.MaxDataCount;

                tpane.Width = item.Width * 1;
                tpane.Height = item.Height * h;
            }

            //データ
            {
                this.InitTable(item, w, h);

            }

        }


        /// <summary>
        /// ヘッダーの表示
        /// </summary>
        private void DispHeader()
        {

            TableLayoutPanel tpane = this.tableLayoutPanelHeader;
            MonthVesselData vdata = this.FData.VData;

            //表示
            int ypos = this.FData.DataOffset.Y;
            for (int i = 0; i < this.FData.MaxDataCount; i++)
            {
                ypos += i;

                ScheduleItemControl rh = new ScheduleItemControl();
                rh.TitleText = "船名";
                rh.DetailText = "竣工/船齢";
                tpane.Controls.Add(rh, 0, ypos);

            }


        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            TableLayoutPanel tpane = this.tableLayoutPanelData;
            tpane.Controls.Clear();
            MonthVesselData vdata = this.FData.VData;

            //位置計算
            int colpos = this.FData.DataOffset.X;

            //全船データ
            foreach (MsVessel ves in this.FData.VesselList)
            {

                //船名
                ScheduleItemControl item = new ScheduleItemControl();
                string vestext = ves.ToString();

                MsVesselCategory vcate = DcGlobal.Global.DBCache.GetMsVesselCategory(ves.ms_vessel_category_id);
                if (vcate != null)
                {
                    vestext += Environment.NewLine;
                    vestext += vcate.ToString();
                }
                item.TitleText = vestext;

                {
                    //船齢の計算
                    decimal age = DcCommon.CommonLogic.CalcuVesselAge(DateTime.Now.Date, ves.completion_date);
                    if (age < 0)
                    {
                        //エラーなら船齢平均計算に含めない
                        item.DetailText = string.Format("{0}", ves.completion_date.ToString("yyyy/MM/dd"));
                    }
                    else
                    {
                        //追加
                        //item.DetailText = string.Format("{0} {1}", ves.completion_date.ToString("yyyy/MM/dd"), age.ToString("F1"));
                        item.DetailText += ves.completion_date.ToString("yyyy/MM/dd");
                        item.DetailText += Environment.NewLine;
                        item.DetailText += age.ToString("F1");
                    }
                }

                tpane.Controls.Add(item, colpos, 0);

                colpos += 1;
            }

            //平均の計算
            decimal aveage = DcCommon.CommonLogic.CalcuAverageVesselAge(DateTime.Now.Date, this.FData.VesselList);

            //最後にその他ヘッダー
            ScheduleItemControl other = new ScheduleItemControl();
            other.TitleText = "その他イベント";
            other.DetailText = "船齢Ave " + aveage.ToString("F1");
            tpane.Controls.Add(other, colpos, 0);


        }

        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="veslist"></param>
        /// <param name="ddata"></param>
        public override void Display(List<MsVessel> veslist, MonthVesselData ddata)
        {
            this.FData = new ScheduleVesselMonthControlData();
            this.FData.VesselList = veslist;
            this.FData.VData = ddata;

            //月の描画
            

            //描画領域のおぜん立て
            this.InitTableControl();

            //ヘッダーの表示
            this.DispHeader();

            //表示
            this.DispData();
        }

        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleListHeaderControl_Load(object sender, EventArgs e)
        {

        }
    }
}
