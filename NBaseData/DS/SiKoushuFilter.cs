using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class SiKoushuFilter
    {

        /// <summary>
        /// 講習情報ID
        /// </summary>
        [DataMember]
        public string SiKoushuID { get; set; }

        /// <summary>
        /// 講習名
        /// </summary>
        [DataMember]
        public string KoushuName { get; set; }

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        public int MsSeninID { get; set; }
        
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
        /// 氏名（カナ）
        /// </summary>
        [DataMember]
        public string NameKana { get; set; }

        /// <summary>
        /// 職名
        /// </summary>
        [DataMember]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 受講予定（開始日）
        /// </summary>
        [DataMember]
        public DateTime YoteiFrom { get; set; }

        /// <summary>
        /// 受講予定（開始日の範囲の終わり）
        /// </summary>
        [DataMember]
        public DateTime YoteiTo { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Is受講済み { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Is受講予定 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Is期限切れ { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        public string Bikou { get; set; }


        // 2014.02 2013年度改造
        /// <summary>
        /// 講習マスタID
        /// </summary>
        [DataMember]
        public int MsSiKoushuID { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 受講実績（開始日）
        /// </summary>
        [DataMember]
        public DateTime JisekiFrom { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 受講実績（終了日）
        /// </summary>
        [DataMember]
        public DateTime JisekiTo { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Is未受講 { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 退職者を除く
        /// </summary>
        [DataMember]
        public bool Is退職者を除く { get; set; }
        
        // 2014.02 2013年度改造
        /// <summary>
        /// フラグ 0:講習管理および船員詳細、1:未受講者抽出
        /// </summary>
        [DataMember]
        public int Flag { get; set; }


        public SiKoushuFilter()
        {
            YoteiFrom = DateTime.MinValue;
            Is受講済み = false;
            Is受講予定 = false;
            Is期限切れ = false;

            MsSiKoushuID = int.MinValue;
            JisekiFrom = DateTime.MinValue;
            JisekiTo = DateTime.MinValue;
            Is未受講 = false;
            Is退職者を除く = false;
        }
    }
}
