using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseUtil
{
    public class Common
    {
        public static bool Gray()
        {
            string str = System.Configuration.ConfigurationManager.AppSettings["Gray"];

            if (str != null)
            {
                bool b;
                bool.TryParse(str, out b);

                return b;
            }

            return false;
        }

        public static string NewID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}
