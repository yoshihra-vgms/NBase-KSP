using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo手配依頼 : ListInfoBase
    {
        /// <summary>
        /// 手配依頼
        /// </summary>
        public OdThi info;

        /// <summary>
        /// 見積依頼(子)
        /// </summary>
        public OdMm child;

        /// <summary>
        /// 見積回答(２個下)
        /// </summary>
        public OdMk child2;

        /// <summary>
        /// 受領(３個下)
        /// </summary>
        public OdJry child3;

        public bool isExists船受領 = false;

        #region 色設定

        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public override Color NormalColor()
        {
            return Color.FromArgb(255, 255, 192);
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public override Color SelectedColor()
        {
            return Color.FromArgb(255, 255, 100);
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public override Color BorderColor()
        {
            return Color.FromArgb(255, 255, 050);
        }

        #endregion

        /// <summary>
        /// 発注状況一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        #region public override string 発注状況一覧用文字列()
        //public override string 発注状況一覧用文字列()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<div><table><tr>");

        //    sb.Append(Brack("手配依頼"));
        //    sb.Append(Brack(","));
        //    if (info.Status != info.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
        //    {
        //        // 「事務所未手配」は「未手配」と表示する　(aki)
        //        //sb.Append(Red(55, info.OdStatusValue.Values[(int)OdThi.STATUS.事務所未手配].Name));
        //        sb.Append(Red(55, "未手配"));
        //    }
        //    else
        //    {
        //        sb.Append(Red(55, info.OrderThiIraiStatus));
        //    }
        //    sb.Append(Brack(","));

        //    string SbtName = info.ThiIraiSbtName;
        //    if (info.MsThiIraiShousaiID != null && info.MsThiIraiShousaiID.Length > 0)
        //    {
        //        SbtName += "-" + info.ThiIraiShousaiName;
        //    }
        //    sb.Append(Blue(115, SbtName));
        //    sb.Append(Brack(200,",船(" + info.VesselName + ")"));
        //    if (info.TehaiIraiNo.Length > 0)
        //    {
        //        sb.Append(Brack(135, ",依頼番号(" + info.TehaiIraiNo.ToString() + ")"));
        //        sb.Append(Brack(130, ",依頼日(" + info.ThiIraiDate.ToShortDateString() + ")"));
        //    }
        //    else
        //    {
        //        sb.Append(Brack(136, ","));
        //        sb.Append(Brack(131, ","));
        //    }
        //    sb.Append(Brack(150, ",依頼者(" + info.ThiUserName + ")"));
        //    sb.Append(Brack(",件名(" + info.Naiyou + ")"));
            
        //    sb.Append("</tr></table></div>");
        //    return sb.ToString();
        //}
        #endregion
        #region public override string 発注状況一覧用文字列()
        //public string 発注状況一覧用文字列(bool isExists船受領)
        public string 発注状況一覧用文字列(string title)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            //sb.Append(Brack("手配依頼"));
            sb.Append(Brack(70, title));
            sb.Append(Brack(","));
            if (info.Status != info.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
            {
                // 「事務所未手配」は「未手配」と表示する　(aki)
                //sb.Append(Red(55, info.OdStatusValue.Values[(int)OdThi.STATUS.事務所未手配].Name));
                sb.Append(Red(55, "未手配"));
            }
            else if (isExists船受領)
            {
                sb.Append(Red(55, "船受領"));
            }
            else
            {
                sb.Append(Red(55, info.OrderThiIraiStatus));
            }
            sb.Append(Brack(","));

            string SbtName = info.ThiIraiSbtName;
            if (info.MsThiIraiShousaiID != null && info.MsThiIraiShousaiID.Length > 0)
            {
                SbtName += "-" + info.ThiIraiShousaiName;
            }
            sb.Append(Blue(115, SbtName));
            sb.Append(Brack(200, ",船(" + info.VesselName + ")"));
            if (info.TehaiIraiNo.Length > 0)
            {
                sb.Append(Brack(135, ",依頼番号(" + info.TehaiIraiNo.ToString() + ")"));
                sb.Append(Brack(130, ",依頼日(" + info.ThiIraiDate.ToShortDateString() + ")"));
            }
            else
            {
                sb.Append(Brack(136, ","));
                sb.Append(Brack(131, ","));
            }
            sb.Append(Brack(150, ",依頼者(" + info.ThiUserName + ")"));
            sb.Append(Brack(",件名(" + info.Naiyou + ")"));

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 「手配依頼Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override BaseForm CreateForm(int windowStyle)
        //public override BaseForm CreateForm(int windowStyle)
        //{
        //    BaseForm baseForm = new BaseForm();
        //    baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規手配依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

        //    手配依頼Form form = new 手配依頼Form(windowStyle, this);
        //    form.Dock = DockStyle.Fill;
        //    form.ClosingEvent += new 手配依頼Form.ClosingEventHandler(baseForm.BaseFormClose);

        //    baseForm.Size = form.Size;
        //    baseForm.MinimumSize = form.Size;

        //    baseForm.Controls.Add(form);

        //    return baseForm;
        //}
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            BaseForm baseForm = new BaseForm();
            baseForm.Text = NBaseCommon.Common.WindowTitle("番号不明", "新規手配依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            baseControl.Dock = DockStyle.Fill;
            (baseControl as 手配依頼Form).ClosingEvent += new 手配依頼Form.ClosingEventHandler(baseForm.BaseFormClose);

            baseForm.Size = baseControl.Size;
            baseForm.MinimumSize = baseControl.Size;

            baseForm.Controls.Add(baseControl);

            return baseForm;
        }
        #endregion


        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            手配依頼Form form = new 手配依頼Form(windowStyle, this,false);

            return form;
        }
        #endregion
    }
}
