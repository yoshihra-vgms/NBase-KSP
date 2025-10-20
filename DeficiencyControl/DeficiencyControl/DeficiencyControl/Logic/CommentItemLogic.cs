using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;
using DeficiencyControl.Util;


namespace DeficiencyControl.Logic
{


    

    /// <summary>
    /// コメントアイテム統括
    /// </summary>
    public class CommentItemLogic
    {
        /// <summary>
        /// 添付ファイルの表示
        /// </summary>
        /// <param name="dic">添付ファイル種別とそれ対応FileViewコントロールのデータ</param>
        /// <param name="datalist">表示したいもの、dicにない種別は無視されます</param>
        /// <returns></returns>
        public static bool DispAttachment(Dictionary<EAttachmentType, FileViewControlEx> dic, List<DcAttachment> datalist)
        {
            foreach (FileViewControlEx con in dic.Values)
            {
                con.ClearItem();
            }

            foreach (DcAttachment at in datalist)
            {
                //添付タイプ取得
                EAttachmentType type = at.AttachmentType;                
                bool ret = dic.ContainsKey(type);
                if (ret == false)
                {
                    //一致するものがないなら放置
                    continue;
                }

                //ADD
                FileViewControlEx con = dic[type];
                con.ReadAddList(at);
            }

            return true;
        }


        
    }
}
