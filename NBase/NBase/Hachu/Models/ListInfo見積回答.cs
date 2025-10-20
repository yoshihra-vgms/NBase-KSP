using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo見積回答 : ListInfoBase
    {
        /// <summary>
        /// 見積依頼（親）
        /// </summary>
        public OdMm parent;

        /// <summary>
        /// 見積回答
        /// </summary>
        public OdMk info;

        /// <summary>
        /// 受領(子)
        /// </summary>
        public OdJry child;

        #region 色設定

        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public override Color NormalColor()
        {
            return Color.FromArgb(192, 192, 255);
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public override Color SelectedColor()
        {
            return Color.FromArgb(125, 125, 255);
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public override Color BorderColor()
        {
            return Color.FromArgb(050, 050, 255);
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

            if (info.Status == info.OdStatusValue.MaxValue)
            {
                sb.Append(Brack(65, "発注"));
                sb.Append(Brack(","));
                sb.Append(Red(110, info.StatusName));
                sb.Append(Brack(",回答番号(" + info.MkNo + ")"));
                sb.Append(Brack(250, ",発注先(" + info.MsCustomerName + ")"));
                sb.Append(Brack(140, ",発注日(" + info.HachuDate.ToShortDateString() + ")"));
            }
            else
            {
                sb.Append(Brack(65, "見積回答"));
                sb.Append(Brack(","));
                sb.Append(Red(110, info.StatusName));
                sb.Append(Brack(",回答番号(" + info.MkNo + ")"));
                sb.Append(Brack(250, ",見積先(" + info.MsCustomerName + ")"));
                if (info.Status != info.OdStatusValue.DefaultValue)
                {
                    sb.Append(Brack(140, ",回答日(" + info.MkDate.ToShortDateString() + ")"));
                }
            }

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 承認一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        #region public override string 承認一覧用文字列()
        public override string 承認一覧用文字列()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("発注"));
            sb.Append(Brack(",回答番号(" + info.MkNo + ")"));
            sb.Append(Brack(250, ",見積先(" + info.MsCustomerName + ")"));
            sb.Append(Brack(140, ",回答日(" + info.MkDate.ToShortDateString() + ")"));
            sb.Append(Brack(",件名(" + info.OdThiNaiyou + ")"));
            
            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 「見積回答Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override BaseForm CreateForm(BaseUserControl baseControl)
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            return null;
        }
        #endregion

        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            見積回答Form form = new 見積回答Form(windowStyle, this);
            //New見積回答Form form = new New見積回答Form(windowStyle, this);

            return form;

        }
        #endregion
    }
}
