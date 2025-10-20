using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo受領 : ListInfoBase
    {
        /// <summary>
        /// 見積回答（親）
        /// </summary>
        public OdMk parent;

        /// <summary>
        /// 受領
        /// </summary>
        public OdJry info;

        /// <summary>
        /// 落成or支払(子)
        /// </summary>
        public OdShr child;


        #region 色設定

        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public override Color NormalColor()
        {
            return Color.FromArgb(255, 192, 255);
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public override Color SelectedColor()
        {
            return Color.FromArgb(255, 100, 255);
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public override Color BorderColor()
        {
            return Color.FromArgb(255, 050, 255);
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

            sb.Append(Brack("受領"));
            sb.Append(Brack(","));
            sb.Append(Red(110, info.StatusName));
            sb.Append(Brack(",受領番号(" + info.JryNo + ")"));
            sb.Append(Brack(",納期(" + info.OdMkNouki.ToShortDateString() + ")"));
            if (info.Status != info.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value)
            {
                sb.Append(Brack(",受領日(" + info.JryDate.ToShortDateString() + ")"));
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

            sb.Append(Brack("受領"));
            sb.Append(Brack(",受領番号(" + info.JryNo + ")"));
            sb.Append(Brack(",件名(" + info.OdThiNaiyou + ")"));

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 概算計上一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        #region public override string 概算計上一覧用文字列()
        public override string 概算計上一覧用文字列()
        {
            decimal kingaku = info.Amount + info.Tax - info.NebikiAmount;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("受領"));
            sb.Append(Brack(155, ",受領番号(" + info.JryNo + ")"));
            sb.Append(Brack(",受領日(" + info.JryDate.ToShortDateString() + ")"));
            sb.Append(Brack(200, ",納品金額(" + NBaseCommon.Common.金額出力(kingaku) + ")"));
            if (info.GaisanKeijoDate != DateTime.MinValue)
            {
                sb.Append(Brack(130, ",計上処理日(" + info.GaisanKeijoDate.ToShortDateString() + ")"));
            }
            sb.Append(Brack(200, ",船(" + info.OdThiVesselName + ")"));
            sb.Append(Brack(",件名(" + info.OdThiNaiyou + ")"));

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 「受領Form」を作成する
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
            受領Form form = new 受領Form(windowStyle, this);

            return form;
        }
        #endregion
    }
}
