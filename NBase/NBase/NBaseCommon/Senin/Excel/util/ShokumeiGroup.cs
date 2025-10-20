using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBaseCommon.Senin.Excel.util
{
    class ShokumeiGroup
    {
        public static string[] Group =
        {
            //"船長", "一航士", "二航士", "三航士", "機関長", "一機士", "二機士", "三機士",
            //"通信長", "甲板長", "甲板手・員", "操機長・手・員", "司厨長・手・員"
            "船長", "一航士", "二航士", "機関長", "一機士", "二機士",
            "通信長", "甲板長", "甲板手・員", "操機長・手・員", "司厨長・手・員"
        };

        public static string[] Group職別海技免許等資格一覧用 =
        {
            "船長", "一航士", "二航士", "機関長", "一機士", "二機士",
            "通信長", "甲板長・手・員", "操機長・手・員", "司厨長・手・員"
        };

        public static Dictionary<int, string> GetShokumeiGroupDic(MsUser loginUser, SeninTableCache seninTableCache)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                if (s.Name == "船長")
                {
                    dic[s.MsSiShokumeiID] = Group[0];
                }
                else if (s.Name == "一等航海士" || s.Name == "次席一等航海士")
                {
                    dic[s.MsSiShokumeiID] = Group[1];
                }
                else if (s.Name == "二等航海士" || s.Name == "次席二等航海士")
                {
                    dic[s.MsSiShokumeiID] = Group[2];
                }
                else if (s.Name == "三等航海士" || s.Name == "次席三等航海士")
                {
                    //dic[s.MsSiShokumeiID] = Group[3];
                    dic[s.MsSiShokumeiID] = Group[2]; // 「二航士」枠に
                }
                else if (s.Name == "機関長")
                {
                    //dic[s.MsSiShokumeiID] = Group[4];
                    dic[s.MsSiShokumeiID] = Group[3];
                }
                else if (s.Name == "一等機関士" || s.Name == "次席一等機関士")
                {
                    //dic[s.MsSiShokumeiID] = Group[5];
                    dic[s.MsSiShokumeiID] = Group[4];
                }
                else if (s.Name == "二等機関士" || s.Name == "次席二等機関士")
                {
                    //dic[s.MsSiShokumeiID] = Group[6];
                    dic[s.MsSiShokumeiID] = Group[5];
                }
                else if (s.Name == "三等機関士" || s.Name == "次席三等機関士")
                {
                    //dic[s.MsSiShokumeiID] = Group[7];
                    dic[s.MsSiShokumeiID] = Group[5]; // 「二機士」枠に
                }
                else if (s.Name == "通信長")
                {
                    //dic[s.MsSiShokumeiID] = Group[8];
                    dic[s.MsSiShokumeiID] = Group[6];
                }
                else if (s.Name == "甲板長")
                {
                    //dic[s.MsSiShokumeiID] = Group[9];
                    dic[s.MsSiShokumeiID] = Group[7];
                }
                else if (s.Name == "甲板手" || s.Name == "甲板員")
                {
                    //dic[s.MsSiShokumeiID] = Group[10];
                    dic[s.MsSiShokumeiID] = Group[8];
                }
                // 2014.04.14 操機員→機関員
                //else if (s.Name == "操機長" || s.Name == "操機手" || s.Name == "操機員")
                else if (s.Name == "操機長" || s.Name == "操機手" || s.Name == "機関員")
                {
                    //dic[s.MsSiShokumeiID] = Group[11];
                    dic[s.MsSiShokumeiID] = Group[9];
                }
                else if (s.Name == "司厨長" || s.Name == "司厨手" || s.Name == "司厨員")
                {
                    //dic[s.MsSiShokumeiID] = Group[12];
                    dic[s.MsSiShokumeiID] = Group[10];
                }
            }

            return dic;
        }

        public static Dictionary<int, string> GetShokumeiGroupDic職別海技免許等資格一覧用(MsUser loginUser, SeninTableCache seninTableCache)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (MsSiShokumei s in seninTableCache.GetMsSiShokumeiList(loginUser))
            {
                if (s.Name == "船長")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[0];
                }
                else if (s.Name == "一等航海士" || s.Name == "次席一等航海士")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[1];
                }
                else if (s.Name == "二等航海士" || s.Name == "次席二等航海士" || s.Name == "三等航海士" || s.Name == "次席三等航海士")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[2];
                }
                else if (s.Name == "機関長")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[3];
                }
                else if (s.Name == "一等機関士" || s.Name == "次席一等機関士")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[4];
                }
                else if (s.Name == "二等機関士" || s.Name == "次席二等機関士" || s.Name == "三等機関士" || s.Name == "次席三等機関士")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[5];
                }
                else if (s.Name == "通信長")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[6];
                }
                else if (s.Name == "甲板長" || s.Name == "甲板手" || s.Name == "甲板員")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[7];
                }
                else if (s.Name == "操機長" || s.Name == "操機手" || s.Name == "機関員")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[8];
                }
                else if (s.Name == "司厨長" || s.Name == "司厨手" || s.Name == "司厨員")
                {
                    dic[s.MsSiShokumeiID] = Group職別海技免許等資格一覧用[9];
                }
            }

            return dic;
        }
    }
}
