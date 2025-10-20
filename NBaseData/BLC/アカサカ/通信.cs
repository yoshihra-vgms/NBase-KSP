using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;


namespace WingData.BLC.アカサカ
{
    public class 通信
    {
        private Encoding Enc = Encoding.GetEncoding("Shift_JIS");
        public string Body;
        public string[] SplitedBody;

        public void Kick(string Url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
            WebResponse res = req.GetResponse();


            Stream st = res.GetResponseStream();
            using (StreamReader sr = new StreamReader(st, Enc))
            {

                string html = sr.ReadToEnd();
                int start = html.IndexOf("<body>");
                start += 6;//<body>分をプラス
                int finish = html.IndexOf("</body>");
                //finish -= 7;//</body>分をプラス
                Body = html.Substring(start, finish - start);
                Body = Body.Replace("\n","");
                Body = Body.Replace("\r", "");

                SplitedBody = Body.Split(',');
            }
        }

        public List<string> CmdData
        {
            get
            {
                string TempBody = Body;
                List<string> ret = new List<string>();

                string CMD = "CMD=IGT_GETNAVIREPORT";
                int start = 0;
                int end = 0;

                while (start < TempBody.Length)
                {
                    start = TempBody.IndexOf(CMD, start);
                    end = TempBody.IndexOf(CMD, start + CMD.Length);

                    if (end == -1)
                    {
                        break;
                    }
                    ret.Add(TempBody.Substring(start, end - start));
                    start = end;
                }
                if (TempBody.Length > start)
                {
                    ret.Add(TempBody.Substring(start, TempBody.Length - start));
                }

                return ret;
            }
        }

        public string FindData(string FindString)
        {
            foreach (string s in SplitedBody)
            {
                if (s.StartsWith(FindString) == true)
                {
                    string str = s.Substring(FindString.Length, s.Length - FindString.Length);
                    return str;
                }
            }
            return "";
        }
    }
}
