using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo新規発注 : ListInfoBase
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

        /// <summary>
        /// 「新規発注Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override BaseForm CreateForm(int windowStyle)
        //public override BaseForm CreateForm(int windowStyle)
        //{
        //    BaseForm baseForm = new BaseForm();
        //    baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規発注", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

        //    新規発注Form form = new 新規発注Form(windowStyle, this);
        //    form.Dock = DockStyle.Fill;
        //    form.ClosingEvent += new 新規発注Form.ClosingEventHandler(baseForm.BaseFormClose);

        //    baseForm.Size = form.Size;


        //    baseForm.Controls.Add(form);

        //    return baseForm;
        //}
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            BaseForm baseForm = new BaseForm();
            baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規発注", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            baseControl.Dock = DockStyle.Fill;
            (baseControl as 新規発注Form).ClosingEvent += new 新規発注Form.ClosingEventHandler(baseForm.BaseFormClose);

            baseForm.Size = baseControl.Size;

            baseForm.Controls.Add(baseControl);

            return baseForm;
        }
        #endregion

        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            新規発注Form form = new 新規発注Form(windowStyle, this, false);

            return form;
        }
        #endregion
    }
}
