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
    /// 検船詳細画面ロジック
    /// </summary>
    public class MoiDetailFormLogic : BaseFormLogic
    {
        public MoiDetailFormLogic(MoiDetailForm f, MoiDetailForm.MoiDetailFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }

        /// <summary>
        /// 管理画面
        /// </summary>
        MoiDetailForm Form = null;

        /// <summary>
        /// 画面データ
        /// </summary>
        MoiDetailForm.MoiDetailFormData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            MoiDetailForm f = this.Form;

            //ヘッダー
            f.moiHeaderControl1.InitControl(this.FData.SrcData);
            // 受検日変更イベント登録
            f.moiHeaderControl1.ChangeDateTimePickerDateDelegateProc = this.ChangeDateTimePickerDate;

            //各コントロールの初期化
            //Itemタブ
            // VIQ Versionの設定
            MoiHeaderControl.MoiHeaderControlOutputData odata = f.moiHeaderControl1.GetInputData();
            f.moiDetailControl1.SetViqVersion(MoiManager.SearchViqVersion(odata.Date));
            f.moiDetailControl1.InitControl(this.FData.SrcData);
            //0件の時は詳細を表示しない
            if (this.FData.SrcData.Header.Moi.observation <= 0)
            {
                f.moiDetailControl1.Visible = false;
            }

            //Attachmentタブ
            f.moiDetailAttachmentControl1.InitControl(this.FData.SrcData);
        }

        /// <summary>
        /// 受検日が変更されたとき
        /// </summary>
        /// <param name="dateTime"></param>
        private void ChangeDateTimePickerDate(DateTime dateTime)
        {
            // 受検日からVIQ Versionを取得する。
            MsViqVersion ver = MoiManager.SearchViqVersion(dateTime);

            // 詳細リストにVIQ Versionを通知してコードリスト変更
            this.Form.moiDetailControl1.ChangeViqVersion(ver);
        }
    }
}
