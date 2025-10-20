using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Drawing;
using NBaseData.DAC;

namespace NBaseCommon
{
    public class ExcelCreatorException : ApplicationException
    { }

    public class Common
    {
        public static readonly int EVal = -1;

        public static int MAX_BINARY_SIZE = 10485760;

        private static string 発注承認 = "On";  // On/Off
        private static string 承認しきい値 = "0";

        public static string 顧客名 = "";
        public static string 船員_自社名 = "不明";
        public static int 配乗計画TYPE = -1;
        static Common()
        {
            try
            {
                顧客名 = System.Configuration.ConfigurationManager.AppSettings["顧客名"];
            }
            catch
            {
            } 
            try
            {
                MAX_BINARY_SIZE = int.Parse(System.Configuration.ConfigurationManager.AppSettings["maxDocumentLength"]);
            }
            catch
            {
            }
            try
            {
                発注承認 = System.Configuration.ConfigurationManager.AppSettings["発注承認機能"];
            }
            catch
            {
            }
            try
            {
                承認しきい値 = System.Configuration.ConfigurationManager.AppSettings["承認しきい値"];
            }
            catch
            {
            }
            try
            {
                船員_自社名 = System.Configuration.ConfigurationManager.AppSettings["船員_自社名"];
            }
            catch
            {
            }
            try
            {
                配乗計画TYPE = int.Parse(System.Configuration.ConfigurationManager.AppSettings["配乗計画TYPE"]);
            }
            catch
            {
            }
        }

        public static NBaseData.DAC.MsUser LoginUser;
        public static NBaseData.DAC.SiCard siCard;

        public static bool Is本番環境 = false;
        public static bool Is開発中 = false;
        public static string AppName;

        public static int FiscalYearStartMonth = 4;


        public static string WindowTitle(string FormNo, string FunctionName, string conectedServerId)
        {
            return WindowTitle(FunctionName);
        }
        public static string WindowTitle(string FunctionName)
        {
            string retStr = FunctionName;
            if (LoginUser != null)
            {
                retStr += "[" + LoginUser.FullName + "]";
            }
            return retStr;
        }

        public static string ドル金額出力(decimal kingaku)
        {
            string dollarStr = kingaku.ToString("C", new CultureInfo("en-us"));
            dollarStr = dollarStr.Replace("(", "-").Replace(")", "");
            
            return dollarStr;
        }

        public static string 金額出力(decimal kingaku)
        {
            return kingaku.ToString("C");
        }

        public static string 金額出力2(decimal kingaku)
        {
            string kigou = @"\";
            string value = kigou + kingaku.ToString("###,###,###,###,###");
            return value;
        }
       
        public static decimal 金額表示を数値へ変換(string kingaku)
        {
            return decimal.Parse(kingaku, System.Globalization.NumberStyles.Currency);
        }

        public static int CancelFlag_有効 = 0;
        public static int CancelFlag_キャンセル = 1;

        public static int DeleteFlag_有効 = 0;
        public static int DeleteFlag_削除 = 1;

        public static string MsThiIraiSbt_修繕ID = "1";
        public static string MsThiIraiSbt_燃料潤滑油ID = "2";
        public static string MsThiIraiSbt_船用品ID = "3";

        public static string MsThiIraiShousai_小修理ID = "1";
        public static string MsThiIraiShousai_入渠ID = "2";

        public static string MsKanidouseiInfoShubetu_積み = "0";
        public static string MsKanidouseiInfoShubetu_揚げ = "1";

        //public static int MsVesselItemCatogoryNumber_ペイント = 0;
       
        public static string PrefixMsLoID_LO = "LO-";
        public static string PrefixMsLoID_ETC = "ETC-";
        public static string MsLo_FO_String = "ＦＯ";
        public static string MsLo_LO_String = "ＬＯ";
        public static string MsLo_ETC_String = "その他";

        public static List<NBaseData.DAC.MsLo> MsLo_Fos()
        {
            List<NBaseData.DAC.MsLo> ret = new List<NBaseData.DAC.MsLo>();
            NBaseData.DAC.MsLo fo = new NBaseData.DAC.MsLo();
            fo.MsLoID = "";
            fo.LoName = "A (SUL 0.5%以下)";
            fo.MsTaniID = "12";
            fo.MsTaniName = "KL";
            ret.Add(fo);

            fo = new NBaseData.DAC.MsLo();
            fo.MsLoID = "";
            fo.LoName = "C";
            fo.MsTaniID = "12";
            fo.MsTaniName = "KL";
            ret.Add(fo);

            fo = new NBaseData.DAC.MsLo();
            fo.MsLoID = "";
            fo.LoName = "LSA (SUL 0.1%以下)";
            fo.MsTaniID = "12";
            fo.MsTaniName = "KL";
            ret.Add(fo);

            fo = new NBaseData.DAC.MsLo();
            fo.MsLoID = "";
            fo.LoName = "LSC (SUL 0.5%以下)";
            fo.MsTaniID = "12";
            fo.MsTaniName = "KL";
            ret.Add(fo);


            return ret;
        }


        public static bool Is発注承認ON
        {
            get
            {
                if (発注承認 == "On")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static int 発注承認しきい値
        {
            get
            {
                if (発注承認 == "On")
                {
                    int ret = 0;
                    if (int.TryParse(承認しきい値, out ret))
                    {
                        return ret;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }





        public static List<NBaseData.DAC.MsUserSettings> UserSettingsList;

        public static List<NBaseData.DAC.MsListItem> SeninListItemList;
        public static List<NBaseData.DAC.UserListItems> SeninListItemUserList;

        public static List<NBaseData.DAC.MsListItem> HachuListItemList;
        public static List<NBaseData.DAC.UserListItems> HachuListItemUserList;


        public static string DefaultListTitle = "default";

        private static string seninListTitle = null;
        private static string hachuListTitle = null;

        public static string SeninListTitle
        {
            get
            {
                if (seninListTitle == null)
                {
                    if (UserSettingsList != null)
                    {
                        var settings = UserSettingsList.Where(o => o.Key == "SeninListTitle").FirstOrDefault();
                        if (settings != null)
                        {
                            seninListTitle = settings.Value;
                        }
                    }
                }
                if (seninListTitle == null)
                {
                    seninListTitle = "default";
                }

                return seninListTitle;
            }

            set
            {
                seninListTitle = value;
            }
        }
        public static string HachuListTitle
        {
            get
            {
                if (hachuListTitle == null)
                {
                    if (UserSettingsList != null)
                    {
                        var settings = UserSettingsList.Where(o => o.Key == "HachuListTitle").FirstOrDefault();
                        if (settings != null)
                        {
                            hachuListTitle = settings.Value;
                        }
                    }
                }
                if (hachuListTitle == null)
                {
                    hachuListTitle = "default";
                }
                return hachuListTitle;
            }

            set
            {
                hachuListTitle = value;
            }
        }


        public static Color ColorTumi = Color.Gold;
        public static Color ColorAge = Color.Orange;
        public static Color ColorTaiki = Color.LavenderBlush;
        public static Color ColorHihaku = Color.Pink;
        public static Color ColorPurge = Color.Violet;
        public static Color ColorEtc = Color.WhiteSmoke;


        public static Color ColorTumiageBorder = Color.Chocolate;






        public static List<MsVessel> VesselList { set; get; }
        public static List<MsSiShokumei> ShokumeiList { set; get; }
        public static List<MsSenin> SeninList { set; get; }




        public static bool IsLocal { set; get; }


        public static void Read()
        {
            IsLocal = GetBool("IsLocal");
        }

        public static bool GetBool(string key, bool def = false)
        {
            bool ans = true;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                ans = Convert.ToBoolean(s);
            }
            catch (Exception e)
            {
                return def;
            }

            return ans;
        }

        private static string _HostName = null;
        public static string HostName {
            get 
            {
                if (_HostName == null)
                    _HostName = System.Net.Dns.GetHostName();

                return _HostName;
            }
        }
    }
}
