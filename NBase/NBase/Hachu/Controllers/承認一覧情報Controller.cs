using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Hachu.Models;
using Hachu.HachuManage;
using NBaseData.DAC;
using NBaseData.DS;

namespace Hachu.Controllers
{
    public class 承認一覧情報Controller
    {
        private Form MdiForm;

        private SplitContainer 承認一覧エリア = null;
        private SyoninListControl 承認一覧Control = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 承認一覧情報Controller(Form form, SplitContainer splitContainer, SyoninListControl syoninListControl)
        public 承認一覧情報Controller(Form form, SplitContainer splitContainer, SyoninListControl syoninListControl)
        {
            // MainForm
            MdiForm = form;

            // 一覧、および、詳細表示部分
            承認一覧エリア = splitContainer;
            承認一覧エリア.Panel2Collapsed = true;

            // 対象のTreeListView
            承認一覧Control = syoninListControl;
            承認一覧Control.ClickEvent += new SyoninListControl.ClickEventHandler(詳細Formを開く);

        }
        #endregion


        /// <summary>
        /// 検索して一覧に表示する
        /// </summary>
        /// <param name="検索条件"></param>
        #region public void 一覧更新(承認一覧検索条件 検索条件)
        public void 一覧更新(承認一覧検索条件 検索条件)
        {
            // 検索条件 → Filter にセット
            OdThiFilter filter = new OdThiFilter();
            #region
            if (検索条件.Vessel != null && 検索条件.Vessel.MsVesselID != -1)
            {
                filter.MsVesselID = 検索条件.Vessel.MsVesselID;
            }
            if (検索条件.User != null && 検索条件.User.MsUserID != "")
            {
                filter.JimTantouID = 検索条件.User.MsUserID;
            }
            #endregion

            // 検索
            List<OdMk> OdMks = null;
            List<OdJry> OdJrys = null;
            List<OdShr> OdShrs = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMks = serviceClient.OdMk_GetRecordsByFilter(NBaseCommon.Common.LoginUser, (int)OdMk.STATUS.発注承認依頼中, filter);             
                OdJrys = serviceClient.OdJry_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, (int)OdJry.STATUS.受領承認依頼中, filter);
                OdShrs = serviceClient.OdShr_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, (int)OdShr.STATUS.落成承認依頼中, filter);               
            }

            承認一覧エリア.Panel2Collapsed = true;

            承認一覧Control.DrawList(OdMks, OdJrys, OdShrs);
        }
        #endregion


        /// <summary>
        /// 「承認一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する詳細Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く(ListInfoBase info)
        {
            BaseUserControl form = info.CreatePanel((int)BaseUserControl.WINDOW_STYLE.承認);
            form.InfoUpdateEvent += new BaseUserControl.InfoUpdateEventHandler(詳細Form_InfoUpdateEvent);

            承認一覧エリア.Panel2.Controls.Clear();
            承認一覧エリア.Panel2.Controls.Add(form);
            承認一覧エリア.SplitterDistance = 承認一覧エリア.Width - form.Width;

            承認一覧エリア.Panel2Collapsed = false;

            承認一覧エリア.Parent.Cursor = Cursors.Default;
        }
        #endregion


        /// <summary>
        /// 詳細Formで情報を更新したときのイベント
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_InfoUpdateEvent(BaseUserControl senderForm, 一覧情報 senderInfo)
        public void 詳細Form_InfoUpdateEvent(BaseUserControl senderForm, ListInfoBase senderInfo)
        {
            if (senderInfo.Remove)
            {
                承認一覧Control.RemoveSelectedNode();
            }

            承認一覧エリア.Panel2Collapsed = true;
            承認一覧エリア.Panel2.Controls.Clear();
        }
        #endregion

    }
}
