using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace NBaseUtil
{
    public class SSLCertificateVaridation
    {
        public SSLCertificateVaridation()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);

        }

        // 信頼できないSSL証明書を「問題なし」にするメソッド
        private bool OnRemoteCertificateValidationCallback(
          Object sender,
          X509Certificate certificate,
          X509Chain chain,
          SslPolicyErrors sslPolicyErrors)
        {
            return true;  // 「SSL証明書の使用は問題なし」と示す
        }
    }

}
