using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBaseData.DS
{
    public class Shokumei
    {
        public static int フェリー = 0;
        public static int 内航 = 1;

        /// <summary>
        /// 職名（乗船職）ID
        /// </summary>

        public int MsSiShokumeiShousaiID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 職名
        public string Name { get; set; }

        /// <summary>
        /// 職名略称
        /// </summary>
        public string NameAbbr { get; set; }

        /// <summary>
        /// 職名(英語)
        /// </summary>
        public string NameEng { get; set; }


        /// <summary>
        /// 表示順序（職位）
        /// </summary>
        public int ShowOrder { get; set; }


        public Shokumei()
        {
            MsSiShokumeiShousaiID = 0;
            MsSiShokumeiID = 0;
            ShowOrder = 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
