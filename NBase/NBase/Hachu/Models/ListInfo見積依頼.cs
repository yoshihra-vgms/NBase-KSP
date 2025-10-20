using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo見積依頼 : ListInfoBase
    {
        /// <summary>
        /// 手配依頼（親）
        /// </summary>
        public OdThi parent;

        /// <summary>
        /// 見積依頼
        /// </summary>
        public OdMm info;
        
        /// <summary>
        /// 見積回答(子)
        /// </summary>
        public OdMk child;

        #region 色設定

        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public override Color NormalColor()
        {
            return Color.FromArgb(192, 255, 192);
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public override Color SelectedColor()
        {
            return Color.FromArgb(100, 255, 100);
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public override Color BorderColor()
        {
            return Color.FromArgb(050, 255, 050);
        }

        #endregion

        /// <summary>
        /// 発注状況一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        #region public override string 発注状況一覧用文字列()
        public override string 発注状況一覧用文字列()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("見積依頼"));
            sb.Append(Brack(",依頼番号(" + info.MmNo.ToString() + ")"));
            if (info.MmDate != DateTime.MinValue)
            {
                sb.Append(Brack(",依頼日(" + info.MmDate.ToShortDateString() + ")"));
            }

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 「見積依頼Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override BaseForm CreateForm(int windowStyle)
        //public override BaseForm CreateForm(int windowStyle)
        //{
        //    BaseForm baseForm = new BaseForm();
        //    baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規見積依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

        //    新規見積依頼Form form = new 新規見積依頼Form(windowStyle, this);
        //    form.Dock = DockStyle.Fill;
        //    form.ClosingEvent += new 新規見積依頼Form.ClosingEventHandler(baseForm.BaseFormClose);

        //    baseForm.Size = form.Size;


        //    baseForm.Controls.Add(form);

        //    return baseForm;
        //}
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            BaseForm baseForm = new BaseForm();
            baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規見積依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            baseControl.Dock = DockStyle.Fill;
            (baseControl as 新規見積依頼Form).ClosingEvent += new 新規見積依頼Form.ClosingEventHandler(baseForm.BaseFormClose);

            baseForm.Size = baseControl.Size;


            baseForm.Controls.Add(baseControl);

            return baseForm;
        }
        #endregion

        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            見積依頼Form form = new 見積依頼Form(windowStyle, this);

            return form;
        }
        #endregion
    }
}
