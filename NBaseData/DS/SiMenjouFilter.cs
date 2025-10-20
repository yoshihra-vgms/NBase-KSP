using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class SiMenjouFilter
    {
        /// <summary>
        /// 免状
        /// </summary>
        [DataMember]
        private List<int> msSiMenjouIDs;
        public List<int> MsSiMenjouIDs
        {
            get
            {
                if (msSiMenjouIDs == null)
                {
                    msSiMenjouIDs = new List<int>();
                }

                return msSiMenjouIDs;
            }
        }

        /// <summary>
        /// 免状種別
        /// </summary>
        [DataMember]
        private List<int> msSiMenjouKindIDs;
        public List<int> MsSiMenjouKindIDs
        {
            get
            {
                if (msSiMenjouKindIDs == null)
                {
                    msSiMenjouKindIDs = new List<int>();
                }

                return msSiMenjouKindIDs;
            }
        }

        /// <summary>
        /// 職名
        /// </summary>
        [DataMember]
        private List<int> msSiShokumeiIDs;
        public List<int> MsSiShokumeiIDs
        {
            get
            {
                if (msSiShokumeiIDs == null)
                {
                    msSiShokumeiIDs = new List<int>();
                }

                return msSiShokumeiIDs;
            }
        }

        /// <summary>
        /// 氏名コード
        /// </summary>
        [DataMember]
        public string ShimeiCode { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        
        /// <summary>
        /// 有効期限
        /// </summary>
        [DataMember]
        public int Yukokigen { get; set; }


        [DataMember]
        public bool is取得済 { get; set; }

        [DataMember]
        public bool is未取得 { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 退職者を除く
        /// </summary>
        [DataMember]
        public bool Is退職者を除く { get; set; }


        public SiMenjouFilter()
        {
            ShimeiCode = null;
            Name = null;
            Yukokigen = int.MinValue;

            is取得済 = false;
            is未取得 = false;
            Is退職者を除く = false;
        }
    }
}
