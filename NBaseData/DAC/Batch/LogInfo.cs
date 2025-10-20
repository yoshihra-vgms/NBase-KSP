using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DAC.Batch
{
    /// <summary>
    /// バッチで使うログメッセージのクラス
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 種別
        /// ０→情報
        /// １→エラー
        /// </summary>
        public int InfoKind;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Msg;
       
    }
}
