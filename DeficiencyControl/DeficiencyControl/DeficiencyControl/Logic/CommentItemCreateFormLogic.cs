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

namespace DeficiencyControl.Logic
{
    /// <summary>
    /// CommentItem作成画面ロジック
    /// </summary>
    public class CommentItemCreateFormLogic : BaseFormLogic
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">管理画面</param>
        /// <param name="fdata">管理データ</param>
        public CommentItemCreateFormLogic(CommentItemCreateForm f, CommentItemCreateForm.CommentItemCreateFormData fdata)
        {
            this.Form = f;
            this.FData = fdata;
        }

        /// <summary>
        /// これの画面
        /// </summary>
        private CommentItemCreateForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        CommentItemCreateForm.CommentItemCreateFormData FData = null;




        /// <summary>
        /// コントロール番号の再割り当て
        /// </summary>
        private void AllocateDeficiencyNo()
        {
            int no = 1;
            FlowLayoutPanel fpanel = this.Form.flowLayoutPanelDetail;

            //現在のCommentItem詳細コントロールに対して一意な番号を割り当てる
            foreach (Control con in fpanel.Controls)
            {
                BaseCommentItemDetailControl detailcon = con as BaseCommentItemDetailControl;
                if (detailcon == null)
                {
                    continue;
                }

                detailcon.SetDeficiencyNo(no);
                no++;
            }
        }


        /// <summary>
        /// 詳細コントロールのADD ret=変更後の数
        /// </summary>
        /// <param name="count"></param>
        private int AddDetail(int count)
        {

            FlowLayoutPanel fpane = this.Form.flowLayoutPanelDetail;

            //現在の詳細数
            int detailcount = fpane.Controls.Count;

            //追加、削除する数を割り出す
            int sacount = count - detailcount;
            if (sacount == 0)
            {
                //変更なき場合は終了
                return count;
            }

            //詳細のADD
            if (sacount > 0)
            {
                for (int i = 0; i < sacount; i++)
                {
                    //DetailをADDせよ
                    this.AddDetailControl(this.FData.ItemKind);
                }
            }
            //詳細を指定件数削除
            if (sacount < 0)
            {
                //データ確認
                DialogResult dret = DcMes.ShowMessage(this.Form, EMessageID.MI_64, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
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
        /// 指摘事項件数が変更されたとき
        /// </summary>
        /// <param name="count">件数</param>
        private int ChangeDeficiencyCountEvent(int count)
        {
            //変更
            return this.AddDetail(count);

        }




        /// <summary>
        /// コメント種別が変更されたとき
        /// </summary>
        /// <param name="kind">入れ替え先Kind</param>
        public void ChangeItemKind(ECommentItemKind kind)
        {
            //管理クラスの作成
            this.FData.CIManager = CommentItemCreator.CreateCommentItemManager(kind);
            this.FData.CIManager.Init(DcCommentItem.EVal);

            //ヘッダーコントロールの再作成とADD
            this.FData.HeaderControl = CommentItemCreator.CreateCommentItemHeadControl(kind);

            //初期化
            this.FData.HeaderControl.InitControl(null);
            this.FData.HeaderControl.ChangeDeficiencyCountDelegateProc = this.ChangeDeficiencyCountEvent;

            //既存の物体を削除してADD
            this.Form.panelHeaderControl.Controls.Clear();
            this.Form.panelHeaderControl.Controls.Add(this.FData.HeaderControl);

        }



        /// <summary>
        /// 詳細コントロールの作成
        /// </summary>
        /// <param name="kind">AddするKind</param>
        public void AddDetailControl(ECommentItemKind kind)
        {
            FlowLayoutPanel fpanel = this.Form.flowLayoutPanelDetail;

            //詳細コントロール作成
            BaseCommentItemDetailControl detailcon = CommentItemCreator.CreateCommentItemDetailControl(kind);
            {
                //新しい色を取得
                detailcon.BackColor = DcGlobal.GetControlListColor();

                //初期化
                detailcon.InitControl(null);
            }

            fpanel.Controls.Add(detailcon);


            //番号の割り当てをし直す
            this.AllocateDeficiencyNo();
        }


        /// <summary>
        /// 現在登録されている詳細コントロールを一覧取得する
        /// </summary>
        /// <returns></returns>
        private List<BaseCommentItemDetailControl> GetDetailControlList()
        {
            List<BaseCommentItemDetailControl> anslist = new List<BaseCommentItemDetailControl>();
            FlowLayoutPanel fpane = this.Form.flowLayoutPanelDetail;

            //現在のADDされているコントロールを調べ、CommentItem詳細コントロールなら追加する
            foreach (Control con in fpane.Controls)
            {
                BaseCommentItemDetailControl detail = con as BaseCommentItemDetailControl;
                if (detail == null)
                {
                    continue;
                }

                anslist.Add(detail);

            }

            return anslist;

        }


        /// <summary>
        /// データ登録処理
        /// </summary>
        public void RegistData()
        {
            //詳細リスト作成
            List<BaseCommentItemDetailControl> detaillist = this.GetDetailControlList();

            try
            {

                //データ挿入処理
                this.FData.CIManager.InsertData(this.FData.HeaderControl, detaillist);

            }
            catch (ControlInputErrorException cie)
            {
                //エラー位置までスクロールする
                this.ScrollErrorPosition(this.FData.HeaderControl, detaillist);

                DcMes.ShowMessage(this.Form, EMessageID.MI_20);
                this.FData.CIManager.ResetError(this.FData.HeaderControl, detaillist);
                throw cie;
            }
            catch (FileViewControlEx.FileDataException fex)
            {
                DcLog.WriteLog(fex, "PSC FileError");
                DcMes.ShowMessage(this.Form, EMessageID.MI_72);
                throw fex;

            }
            catch (Exception e)
            {
                DcMes.ShowMessage(this.Form, EMessageID.MI_21);
                throw new Exception("RegistData", e);
            }
        }



        /// <summary>
        /// エラーとなった位置を表示する
        /// </summary>
        /// <param name="headercontrol"></param>
        /// <param name="detaillist"></param>
        private void ScrollErrorPosition(BaseCommentItemHeaderControl headercontrol, List<BaseCommentItemDetailControl> detaillist)
        {
            //ヘッダーコントロール
            Control hcon = headercontrol.GetErrorFirstControl();
            if (hcon != null)
            {
                this.Form.panelHeaderControl.ScrollControlIntoView(hcon);
            }


            //詳細一覧
            foreach (BaseCommentItemDetailControl detail in detaillist)
            {
                Control decon = detail.GetErrorFirstControl();
                if (decon != null)
                {
                    this.Form.flowLayoutPanelDetail.ScrollControlIntoView(decon);

                    //程よい位置に表示できるように調整する
                    if (this.Form.flowLayoutPanelDetail.VerticalScroll.Value > 30)
                    {
                        this.Form.flowLayoutPanelDetail.VerticalScroll.Value -= 30;
                    }
                    break;
                }
            }
        }
        
    }
}
