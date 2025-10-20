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


namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船新規作成画面ロジッククラス
    /// </summary>
    public class MoiCreateFormLogic : BaseFormLogic
    {
        public MoiCreateFormLogic(MoiCreateForm f, MoiCreateForm.MoiCreateFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }

        /// <summary>
        /// 管理画面
        /// </summary>
        MoiCreateForm Form = null;

        /// <summary>
        /// データ
        /// </summary>
        MoiCreateForm.MoiCreateFormData FData = null;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 指摘事項件数が変更されたとき
        /// </summary>
        /// <param name="count"></param>
        private int ChangeObservationCount(int count)
        {
            int ans = this.AddDetail(count);
            return ans;
        }

        /// <summary>
        /// 受検日が変更されたとき
        /// </summary>
        /// <param name="dateTime"></param>
        private void ChangeDateTimePickerDate(DateTime dateTime)
        {
            // 受検日からVIQ Versionを取得する。
            MsViqVersion ver = MoiManager.SearchViqVersion(dateTime);

            // 詳細リスト取得
            List<MoiDetailControl> detaillist = this.GetDetailControlList();
            foreach (MoiDetailControl detail in detaillist)
            {
                // 詳細リストにVIQ Versionを通知してコードリスト変更
                detail.ChangeViqVersion(ver);
            }
        }

        /// <summary>
        /// コントロール番号の再割り当て
        /// </summary>
        private void AllocateObservationNo()
        {
            int no = 1;
            FlowLayoutPanel fpanel = this.Form.flowLayoutPanelDetail;

            //現在のCommentItem詳細コントロールに対して一意な番号を割り当てる
            foreach (Control con in fpanel.Controls)
            {
                MoiDetailControl decon = con as MoiDetailControl;
                if (decon == null)
                {
                    continue;
                }

                decon.ObservationNo = no;
                no++;
            }
        }

        /// <summary>
        /// 詳細コントロールの追加
        /// </summary>
        private void AddDetailControl()
        {
            FlowLayoutPanel fpane = this.Form.flowLayoutPanelDetail;

            //詳細コントロールの作成
            MoiDetailControl decon = new MoiDetailControl();
            decon.BackColor = DcGlobal.GetControlListColor();
            // VIQ Versionの設定
            MoiHeaderControl.MoiHeaderControlOutputData odata = this.Form.moiHeaderControl1.GetInputData();
            decon.SetViqVersion(MoiManager.SearchViqVersion(odata.Date));
            //
            decon.InitControl(null);

            //
            fpane.Controls.Add(decon);

            //番号の割り当てをし直す
            this.AllocateObservationNo();
        }

        /// <summary>
        /// 詳細コントロールのADD ret=最終的な数
        /// </summary>
        /// <param name="count">件数</param>
        private int AddDetail(int count)
        {
            FlowLayoutPanel fpane = this.Form.flowLayoutPanelDetail;

            //現在の詳細数
            int detailcount = fpane.Controls.Count;

            //追加、削除する数を割り出す
            int sacount = count - detailcount;

            //詳細のADD
            if (sacount > 0)
            {
                for (int i = 0; i < sacount; i++)
                {

                    this.AddDetailControl();
                }
            }
            //詳細を指定件数削除
            if (sacount < 0)
            {                
                //データ削減確認
                DialogResult dret = DcMes.ShowMessage(this.Form, EMessageID.MI_71, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (dret != System.Windows.Forms.DialogResult.Yes)
                {
                    return detailcount;
                }


                int rvcount = Math.Abs(sacount);

                //データを後ろから消していく
                for (int i = 0; i < rvcount; i++)
                {
                    fpane.Controls.RemoveAt(fpane.Controls.Count - 1);

                }
            }

            return count;
        }


        /// <summary>
        /// 画面の初期化
        /// </summary>
        public void Init()
        {
            //ヘッダーの初期化
            MoiCreateForm f = this.Form;
            f.moiHeaderControl1.ChangeObservationCountDelegateProc = this.ChangeObservationCount;
            f.moiHeaderControl1.ChangeDateTimePickerDateDelegateProc = this.ChangeDateTimePickerDate;
            f.moiHeaderControl1.InitControl(null);




        }

        /// <summary>
        /// 現在の詳細コントロール一覧を取得する
        /// </summary>
        /// <returns></returns>
        private List<MoiDetailControl> GetDetailControlList()
        {
            List<MoiDetailControl> anslist = new List<MoiDetailControl>();

            FlowLayoutPanel fpane = this.Form.flowLayoutPanelDetail;

            //ADDされている詳細コントロールを取得
            foreach (Control con in fpane.Controls)
            {
                MoiDetailControl de = con as MoiDetailControl;
                if (de == null)
                {
                    continue;
                }

                anslist.Add(de);
            }

            return anslist;

        }

        /// <summary>
        /// データ登録処理
        /// </summary>
        public void RegistData()
        {
            //詳細リスト作成
            List<MoiDetailControl> detaillist = this.GetDetailControlList();

            try
            {

                //エラーチェック
                MoiManager.CheckError(this.Form.moiHeaderControl1, detaillist);

                //データ挿入処理
                MoiManager.Insert(this.Form.moiHeaderControl1, detaillist);


            }
            catch (ControlInputErrorException cie)
            {
                //エラー位置までスクロールを行う。
                this.ScrollErrorPosition(this.Form.moiHeaderControl1, detaillist);

                DcMes.ShowMessage(this.Form, EMessageID.MI_36);
                MoiManager.ResetError(this.Form.moiHeaderControl1, detaillist);
                throw cie;
            }
            catch (FileViewControlEx.FileDataException fex)
            {
                //添付ファイルエラー
                DcLog.WriteLog(fex, "MOI FileError");
                DcMes.ShowMessage(this.Form, EMessageID.MI_75);
                throw fex;
            }
            catch (Exception e)
            {
                DcMes.ShowMessage(this.Form, EMessageID.MI_37);
                throw new Exception("RegistData", e);
            }
        }




        /// <summary>
        /// エラーとなった位置を表示する
        /// </summary>
        /// <param name="headercontrol"></param>
        /// <param name="detaillist"></param>
        private void ScrollErrorPosition(MoiHeaderControl headercontrol, List<MoiDetailControl> detaillist)
        {
            //ヘッダーコントロール
            Control hcon = headercontrol.GetErrorFirstControl();
            if (hcon != null)
            {
                this.Form.panelHeaderControl.ScrollControlIntoView(hcon);
            }


            //詳細一覧
            foreach (MoiDetailControl detail in detaillist)
            {
                Control decon = detail.GetErrorFirstControl();
                if (decon != null)
                {
                    this.Form.flowLayoutPanelDetail.ScrollControlIntoView(decon);
                    break;
                }
            }
            
        }







    }
}
