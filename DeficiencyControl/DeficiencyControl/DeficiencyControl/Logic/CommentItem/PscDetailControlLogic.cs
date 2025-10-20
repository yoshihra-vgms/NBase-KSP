using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.Logic.CommentItem
{
    /// <summary>
    /// PSC詳細のロジッククラス
    /// </summary>
    public class PscDetailControlLogic
    {
        public PscDetailControlLogic(PscDetailControl con, PscDetailControl.PscDetailControlData fdata)
        {
            this.Con = con;
            this.FData = fdata;
        }


        /// <summary>
        /// これの親コントロール
        /// </summary>
        PscDetailControl Con = null;
        /// <summary>
        /// データ
        /// </summary>
        PscDetailControl.PscDetailControlData FData = null;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// アクションコードが削除されるとき
        /// </summary>
        /// <param name="control"></param>
        private void ActionCodeDeleteControlDelegateProc(BaseControl control)
        {
            FlowLayoutPanel fpane = this.Con.flowLayoutPanelActionCode;
            fpane.Controls.Remove(control);
        }

        /// <summary>
        /// アクションコードの追加
        /// </summary>
        /// <param name="ac"></param>
        public void AddActionCode(DcActionCodeHistory ac = null)
        {
            FlowLayoutPanel fpane = this.Con.flowLayoutPanelActionCode;

            //コントロール作成と初期化
            ActionCodeControl con = new ActionCodeControl();
            {

                ActionCodeControl.InputData idata = new ActionCodeControl.InputData();
                idata.SrcAc = ac;
                con.InitControl(idata);
                con.DeleteCallbackDelegate = this.ActionCodeDeleteControlDelegateProc;
            }

            int sno = 0;
            if (ac != null)
            {
                sno = ac.order_no;
            }

            //ADD
            fpane.Controls.Add(con);
            fpane.Controls.SetChildIndex(con, sno);
            con.TabIndex = 0;
            
        }



        /// <summary>
        /// データの取得
        /// </summary>
        /// <returns></returns>
        public List<DcActionCodeHistory> GetActionCodeList()
        {
            FlowLayoutPanel fpane = this.Con.flowLayoutPanelActionCode;
            List<DcActionCodeHistory> anslist = new List<DcActionCodeHistory>();


            int no = 0;

            //ADDされている物体を取得し、設定
            foreach (Control con in fpane.Controls)
            {
                ActionCodeControl acon = con as ActionCodeControl;
                if (acon == null)
                {
                    continue;
                }

                //ADD
                DcActionCodeHistory adata = acon.GetInputData(no);
                anslist.Add(adata);

                no++;
            }

            //ここで削除データのADDをせよ。
            if (this.FData.SrcData != null)
            {
                anslist = ComLogic.CreateDBUpdateList<DcActionCodeHistory>(this.FData.SrcData.ActionCodeHistoryList, anslist);
            }

            return anslist;
        }

    }
}
