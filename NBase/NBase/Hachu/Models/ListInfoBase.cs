using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Hachu.HachuManage;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    abstract public class ListInfoBase
    {
        public bool Remove = false;
        public bool RemoveTop = false;
        public bool ChangeStatus = false;
        public bool ChangeParent = false;
        public MsThiIraiStatus SetStatus = null;
        public bool NextStatus = false;
        public bool AddNode = false;
        public int FontSize = 10;
        public List<ListInfoBase> children;
        public List<ListInfoBase> Children
        {
            get
            {
                if (children == null)
                {
                    children = new List<ListInfoBase>();
                }

                return children;
            }
        }

        #region 一覧表示の文字色設定
        public string Brack(string orgStr)
        {
            return Brack(0, orgStr);
        }
        public string Brack(int width, string orgStr)
        {
            StringBuilder retStr = new StringBuilder();
            if (width > 0)
            {
                retStr.Append("<td width=\"" + width + "px\">");
            }
            else
            {
                retStr.Append("<td>");
            }
            retStr.Append("<font size=\"" + FontSize + "\">" + orgStr + "</font>");
            retStr.Append("</td>");
            return retStr.ToString();
        }
        public string Red(string orgStr)
        {
            return Red(0, orgStr);
        }
        public string Red(int width, string orgStr)
        {
            StringBuilder retStr = new StringBuilder();
            if (width > 0)
            {
                retStr.Append("<td width=\"" + width + "px\">");
            }
            else
            {
                retStr.Append("<td>");
            }
            retStr.Append("<b><font size=\"" + FontSize + "\" color=\"#FF0000\">" + orgStr + "</font></b>");
            retStr.Append("</td>");
            return retStr.ToString();
        }
        public string Blue(string orgStr)
        {
            return Blue(0, orgStr);
        }
        public string Blue(int width, string orgStr)
        {
            StringBuilder retStr = new StringBuilder();
            if (width > 0)
            {
                retStr.Append("<td width=\"" + width + "px\">");
            }
            else
            {
                retStr.Append("<td>");
            }
            retStr.Append("<b><font size=\"" + FontSize + "\" color=\"#0000FF\">" + orgStr + "</font></b>");
            retStr.Append("</td>");
            return retStr.ToString();
        }
        #endregion

        #region 一覧表示の色設定（派生クラスでoverrideしてください）
        /// <summary>
        /// 一覧表示の通常時背景色
        /// </summary>
        /// <returns></returns>
        public virtual Color NormalColor()
        {
            return Color.White;
        }

        /// <summary>
        /// 一覧表示の選択時背景色
        /// </summary>
        /// <returns></returns>
        public virtual Color SelectedColor()
        {
            return Color.White;
        }

        /// <summary>
        /// 一覧表示の枠色
        /// </summary>
        /// <returns></returns>
        public virtual Color BorderColor()
        {
            return Color.Black;
        }
        #endregion

        #region 一覧に表示する情報文字列（派生クラスでoverrideしてください）
        /// <summary>
        /// 発注状況一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        public virtual string 発注状況一覧用文字列()
        {
            return "";
        }

        /// <summary>
        /// 承認一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        public virtual string 承認一覧用文字列()
        {
            return "";
        }

        /// <summary>
        /// 概算計上一覧用に情報を加工して文字列として返す
        /// </summary>
        /// <returns></returns>
        public virtual string 概算計上一覧用文字列()
        {
            return "";
        }
        #endregion
        
        //abstract public BaseForm CreateForm(int windowStyle);
        abstract public BaseForm CreateForm(BaseUserControl baseControl);
        abstract public BaseUserControl CreatePanel(int windowStyle);
    }
}
