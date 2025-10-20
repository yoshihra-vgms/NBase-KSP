using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo支払 : ListInfoBase
    {
        /// <summary>
        /// 受領（親）
        /// </summary>
        public OdJry parent;

        /// <summary>
        /// 支払
        /// </summary>
        public OdShr info;

        #region 色設定

        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public override Color NormalColor()
        {
            return Color.FromArgb(192, 255, 255);
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public override Color SelectedColor()
        {
            return Color.FromArgb(100, 255, 255);
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public override Color BorderColor()
        {
            return Color.FromArgb(050, 255, 255);
        }

        #endregion

        /// <summary>
        /// 発注状況一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        #region public override string 発注状況一覧用文字列()
        public override string 発注状況一覧用文字列()
        {
            string str = "";
            if (info.Sbt == (int)OdShr.SBT.支払)
            {
                str = 支払発注状況一覧用文字列();
            }
            else if (info.Sbt == (int)OdShr.SBT.落成)
            {
                str = 落成発注状況一覧用文字列();
            }
            return str;
        }

        private string 支払発注状況一覧用文字列()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("支払"));
            sb.Append(Brack(","));
            if (info.Status == info.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value)
            {
                if (info.SyoriStatus == ((int)支払実績連携IF.STATUS.エラー).ToString())
                {
                    sb.Append(Red(155, "基幹却下"));
                }
                else if (info.SyoriStatus == ((int)支払実績連携IF.STATUS.差し戻し).ToString())
                {
                    sb.Append(Red(155, "基幹取込エラー"));
                }
                else
                {
                    sb.Append(Red(155, info.StatusName));
                }
            }
            else if (info.Status == info.OdStatusValue.Values[(int)OdShr.STATUS.支払依頼済み].Value)
            {
                // 2009.11.18:aki 支払依頼済みでとまっているものは、基幹連携に失敗しているもの
                sb.Append(Red(155, "基幹取込エラー"));
            }
            else
            {
                sb.Append(Red(155, info.StatusName));
            }
            sb.Append(Brack(",支払番号(" + info.ShrNo + ")"));
            if (info.Status != info.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value)
            {
                // 2009.12.14:aki W090235対応
                //sb.Append(Brack(",支払予定日(" + info.ShrIraiDate.ToShortDateString() + ")"));
                sb.Append(Brack(",請求書日(" + info.ShrIraiDate.ToShortDateString() + ")"));
            }
            if (info.Status == info.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value && info.ShrDate != DateTime.MinValue)
            {
                sb.Append(Brack(",支払日(" + info.ShrDate.ToShortDateString() + ")"));
            }

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        private string 落成発注状況一覧用文字列()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("落成"));
            sb.Append(Brack(","));
            sb.Append(Red(155, info.StatusName));
            sb.Append(Brack(",落成番号(" + info.ShrNo + ")"));
            if (info.Status == info.OdStatusValue.Values[(int)OdShr.STATUS.落成済み].Value)
            {
                sb.Append(Brack(",落成日(" + info.ShrDate.ToShortDateString() + ")"));
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

            sb.Append(Brack("落成"));
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
            string str = "";
            if (info.Sbt == (int)OdShr.SBT.支払)
            {
                str = 支払概算計上一覧用文字列();
            }
            else if (info.Sbt == (int)OdShr.SBT.落成)
            {
                str = 落成概算計上一覧用文字列();
            }
            return str;
        }

        private string 支払概算計上一覧用文字列()
        {
            decimal kingaku = info.Amount + info.Tax - info.NebikiAmount;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("支払"));
            sb.Append(Brack(180, ",支払番号(" + info.ShrNo + ")"));
            if (info.ShrIraiDate != DateTime.MinValue)
            {
                // 2009.12.14:aki W090235対応
                //sb.Append(Brack(160, ",支払予定日(" + info.ShrIraiDate.ToShortDateString() + ")"));
                sb.Append(Brack(160, ",請求書日(" + info.ShrIraiDate.ToShortDateString() + ")"));
            }
            else
            {
                sb.Append(Brack(161, ","));
            }
            sb.Append(Brack(200, ",支払金額(" + NBaseCommon.Common.金額出力(kingaku) + ")"));
            sb.Append(Brack(200, ",船(" + info.MsVessel_VesselName + ")"));
            sb.Append(Brack(",件名(" + info.OdThiNaiyou + ")"));

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        private string 落成概算計上一覧用文字列()
        {
            decimal kingaku = info.Amount + info.Tax - info.NebikiAmount;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr>");

            sb.Append(Brack("落成"));
            sb.Append(Brack(180, ",落成番号(" + info.ShrNo + ")"));
            if (info.ShrDate != DateTime.MinValue)
            {
                sb.Append(Brack(150, ",落成日(" + info.ShrDate.ToShortDateString() + ")"));
            }
            else
            {
                sb.Append(Brack(151, ","));
            }
            sb.Append(Brack(200, ",落成金額(" + NBaseCommon.Common.金額出力(kingaku) + ")"));
            sb.Append(Brack(200, ",船(" + info.MsVessel_VesselName + ")"));
            sb.Append(Brack(",件名(" + info.OdThiNaiyou + ")"));

            sb.Append("</tr></table></div>");
            return sb.ToString();
        }
        #endregion


        /// <summary>
        /// 「支払Form」を作成する
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
            BaseUserControl form = null;
            if (info.Sbt == (int)OdShr.SBT.支払)
            {
                form = new 支払Form(windowStyle, this);
            }
            else if (info.Sbt == (int)OdShr.SBT.落成)
            {
                form = new 落成Form(windowStyle, this);
            }

            return form;
        }
        #endregion
    }
}
