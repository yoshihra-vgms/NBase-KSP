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

using DeficiencyControl.Forms;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// AccidnetDetailコントロールロジック
    /// </summary>
    public class AccidentDetailControlLogic : BaseFormLogic
    {
        public AccidentDetailControlLogic(AccidentDetailControl con, AccidentDetailControl.AccidentDetailControlData fdata)
        {
            this.Con = con;
            this.FData = fdata;
        }

        /// <summary>
        /// 管理コントロール
        /// </summary>
        AccidentDetailControl Con = null;

        /// <summary>
        /// データ
        /// </summary>
        AccidentDetailControl.AccidentDetailControlData FData = null;


        /// <summary>
        /// 現在の進捗コントロール一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<AccidentProgressControl> GetProgressControlList()
        {

            List<AccidentProgressControl> anslist = new List<AccidentProgressControl>();
            FlowLayoutPanel pane = this.Con.flowLayoutPanelProgress;
            
            //一覧取得
            foreach (Control con in pane.Controls)
            {
                AccidentProgressControl procon = con as AccidentProgressControl;
                if (procon == null)
                {
                    continue;
                }

                anslist.Add(procon);

            }


            return anslist;
        }



        /// <summary>
        /// Accident進捗データの取得
        /// </summary>        
        /// <returns></returns>
        public List<AccidentProgressData> GetProgressInput()
        {
            List<AccidentProgressData> anslist = new List<AccidentProgressData>();

            //現在の進捗一覧取得
            List<AccidentProgressControl> proconlist = GetProgressControlList();

            //データ一覧取得
            foreach (AccidentProgressControl procon in proconlist)
            {
                //入力データ取得
                AccidentProgressData pdata = procon.GetInputData();
                anslist.Add(pdata);

            }


            //ここで削除を算出する
            if (this.FData.SrcData != null)
            {
                foreach (AccidentProgressData srcpg in this.FData.SrcData.ProgressList)
                {
                    bool f = false;
                    anslist.ForEach(x =>
                    {
                        if (x.Progress.accident_progress_id == srcpg.Progress.accident_progress_id)
                        {
                            f = true;
                        }
                    });

                    //同じIDがないなら削除されているので削除としてADDする
                    if (f == false)
                    {
                        srcpg.Progress.delete_flag = true;
                        anslist.Add(srcpg);
                    }
                }
            }

            return anslist;
        }




        /// <summary>
        /// 現在の報告書提出コントロール一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<AccidentReportsControl> GetReportsControlList()
        {

            List<AccidentReportsControl> anslist = new List<AccidentReportsControl>();
            FlowLayoutPanel pane = this.Con.flowLayoutPanelReports;

            //一覧取得
            foreach (Control con in pane.Controls)
            {
                AccidentReportsControl repcon = con as AccidentReportsControl;
                if (repcon == null)
                {
                    continue;
                }

                anslist.Add(repcon);

            }


            return anslist;
        }


        /// <summary>
        /// Accident報告書提出の取得
        /// </summary>        
        /// <returns></returns>
        public List<AccidentReportsData> GetReportsInput()
        {
            List<AccidentReportsData> anslist = new List<AccidentReportsData>();

            //報告書コントロール一覧取得
            List<AccidentReportsControl> arconlist = this.GetReportsControlList();

            //データ一覧取得
            int no = 0;
            foreach (AccidentReportsControl arcon in arconlist)
            {
                //入力データ取得
                AccidentReportsData ardata = arcon.GetInputData(no);
                anslist.Add(ardata);

                no++;

            }


            //ここで削除を算出する
            if (this.FData.SrcData != null)
            {
                foreach (AccidentReportsData srcrep in this.FData.SrcData.ReportsList)
                {
                    bool f = false;
                    anslist.ForEach(x =>
                    {
                        if (x.Reports.accident_reports_id == srcrep.Reports.accident_reports_id)
                        {
                            f = true;
                        }
                    });

                    //同じIDがないなら削除されているので削除としてADDする
                    if (f == false)
                    {
                        srcrep.Reports.delete_flag = true;
                        anslist.Add(srcrep);
                    }
                }
            }

            return anslist;
        }
    }
}
