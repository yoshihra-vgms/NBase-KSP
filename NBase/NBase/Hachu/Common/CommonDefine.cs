using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Hachu.Common
{
    [Serializable]
    public class CommonDefine
    {
        // TreeListView の単価、値引きの桁数
    　　public static int TreeListViewColumnLength消費税 = 7;
        public static int TreeListViewColumnLength単価 = 9;

        //
        public static int ExpandBoxSize = 17;

        public static string MsThiIraiStatus_未対応 = "0";
        public static string MsThiIraiStatus_見積中 = "1";
        public static string MsThiIraiStatus_発注済 = "2";
        public static string MsThiIraiStatus_受領済 = "3";
        public static string MsThiIraiStatus_完了 = "4";

        public static string Prefix新規 = "NEW_";
        public static string Prefix変更 = "CHG_";
        public static string Prefix削除 = "DEL_";

        public static string 新規ID()
        {
            return 新規ID(true);
        }

        public static string 新規ID(bool prefixPlus)
        {
            string newID = "";
            if (prefixPlus)
            {
                newID = Prefix新規 + System.Guid.NewGuid().ToString();
            }
            else
            {
                newID = System.Guid.NewGuid().ToString();
            }
            return newID;
        }
        public static bool Is新規(string id)
        {
            if (id.Length < Prefix新規.Length)
                return false;
            if (id.Substring(0, Prefix新規.Length) == Prefix新規)
                return true;
            else
                return false;
        }
        public static bool Is変更(string id)
        {
            if (id.Length < Prefix変更.Length)
                return false;
            if (id.Substring(0, Prefix変更.Length) == Prefix変更)
                return true;
            else
                return false;
        }
        public static bool Is削除(string id)
        {
            if (id.Length < Prefix削除.Length)
                return false;
            if (id.Substring(0, Prefix削除.Length) == Prefix削除)
                return true;
            else
                return false;
        }
        public static string RemovePrefix(string id)
        {
            string srcId = id;
            string dstId = "";

            if (Is新規(srcId))
            {
                dstId = srcId.Substring(Prefix新規.Length);
            }
            else if (Is変更(srcId))
            {
                dstId = srcId.Substring(Prefix変更.Length);
            }
            else if (Is削除(srcId))
            {
                dstId = srcId.Substring(Prefix削除.Length);
            }
            else
            {
                dstId = srcId;
            }
            return dstId;
        }

        public static readonly Color FOCUS_BACK_COLOR = Color.PaleTurquoise;
    }
}
