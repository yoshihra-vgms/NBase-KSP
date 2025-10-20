using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo年度変更 : ListInfoBase
    {
        public int no;


        /// <summary>
        /// 手配依頼
        /// </summary>
        public OdThi thi;

        /// <summary>
        /// 見積依頼
        /// </summary>
        public OdMm mm;

        /// <summary>
        /// 見積回答
        /// </summary>
        public OdMk mk;

        /// <summary>
        /// 受領
        /// </summary>
        public OdJry jry;

        /// <summary>
        /// 支払
        /// </summary>
        public OdShr shr;


        #region public override BaseForm CreateForm(BaseUserControl baseControl)
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            return null;
        }
        #endregion

        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            return null;
        }
        #endregion
    }
}
