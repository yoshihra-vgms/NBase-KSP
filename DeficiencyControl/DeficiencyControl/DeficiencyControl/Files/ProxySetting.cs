using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using DcCommon;

namespace DeficiencyControl.Files
{
    public class ProxySetting
    {
        private string _proxyUse;
        private string _proxyURL;
        private string _proxyUserID;
        private string _proxyPassword;

        public string ProxyUse
        {
            get { return _proxyUse; }
            set { _proxyUse = value; }
        }
        public string ProxyURL
        {
            get { return _proxyURL; }
            set { _proxyURL = value; }
        }
        public string ProxyUserID
        {
            get { return _proxyUserID; }
            set { _proxyUserID = value; }
        }
        public string ProxyPassword
        {
            get { return _proxyPassword; }
            set { _proxyPassword = value; }
        }

        public static void SetUseProxy(bool useProxy)
        {
            ProxySetting settings = ProxySetting.Read();
            settings._SetUseProxy(useProxy);

            ProxySetting.Write(settings);
        }
        public void _SetUseProxy(bool useProxy)
        {
            if (useProxy)
            {
                _proxyUse = "TRUE";
            }
            else
            {
                _proxyUse = "FALSE";
            }
        }
        public bool IsUseProxy()
        {
            if (_proxyUse == "TRUE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Http設定ファイル名の取得
        /// </summary>
        /// <returns></returns>
        private static string GetHttpSettingFilename()
        {
            string ans = "";

            string path = DcGlobal.Global.Env.ClickonceDataPath;
            ans = path + "\\" + "HttpSettings.xml";

            return ans;
        }


        public static bool Write(ProxySetting settings)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProxySetting));

                // カレントディレクトリに"HttpSettings.xml"というファイルで書き出す
                //FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "HttpSettings.xml", FileMode.Create);

                string filename = GetHttpSettingFilename();
                DcLog.WriteLog("ProxySetting Write() filename=" + filename);
                FileStream fs = new FileStream(filename, FileMode.Create);

                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, settings);
                fs.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        public static ProxySetting Read()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProxySetting));
 
            // XMLをTwitSettingsオブジェクトに読み込む
            ProxySetting settings = new ProxySetting();
            
            //FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "HttpSettings.xml", FileMode.Open);

            string filename = GetHttpSettingFilename();
            //AmLog.WriteLog("ProxySetting Read() filename=" + filename);
            
            FileStream fs = new FileStream(filename, FileMode.Open);
 
            // XMLファイルを読み込み、逆シリアル化（復元）する
            settings = (ProxySetting)serializer.Deserialize(fs);      
            fs.Close();

            return settings;
        }
    }
}
