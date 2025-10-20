using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB;
using DcCommon.DB.DAC;
using CIsl.DB.WingDAC;

using DeficiencyControl.Controls;
using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic.CommentItem;

namespace DeficiencyControl.Logic
{
    

    /// <summary>
    /// CommentItemの作成をサポートするもの
    /// </summary>
    public class CommentItemCreator
    {
        /// <summary>
        /// CommentItemと詳細コントロールタイプの関連
        /// </summary>
        private static Dictionary<ECommentItemKind, Type> DetailControlDic = null;

        /// <summary>
        /// CommentItemとヘッドコントロールタイプの関連
        /// </summary>
        private static Dictionary<ECommentItemKind, Type> HeadControlDic = null;

        /// <summary>
        /// CommentItemとそれの管理の関連付け
        /// </summary>
        private static Dictionary<ECommentItemKind, Type> ManagerDic = null;

        //マネージャーを作成するもの
        //**************************************************************************************************
        //**************************************************************************************************
        /// <summary>
        /// コントロールと検査種別の対応作成 検査種別を追加したらここに追加すること
        /// </summary>
        private static void CreateControlDic()
        {
            //コントロール
            CommentItemCreator.DetailControlDic = new Dictionary<ECommentItemKind, Type>();
            Dictionary<ECommentItemKind, Type> detaildic = CommentItemCreator.DetailControlDic;
            
            //ヘッダーコントロール
            CommentItemCreator.HeadControlDic = new Dictionary<ECommentItemKind, Type>();
            Dictionary<ECommentItemKind, Type> headdic = CommentItemCreator.HeadControlDic;

            //管理クラス
            CommentItemCreator.ManagerDic = new Dictionary<ECommentItemKind, Type>();
            Dictionary<ECommentItemKind, Type> manadic = CommentItemCreator.ManagerDic;
            //==================================================================================================

            //PSC Inspection
            detaildic.Add(ECommentItemKind.PSC_Inspection, typeof(PscDetailControl));
            headdic.Add(ECommentItemKind.PSC_Inspection, typeof(PscHeaderControl));
            manadic.Add(ECommentItemKind.PSC_Inspection, typeof(PscInspectionManager));

        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// コメントアイテム対応コントロールの型取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetDetailControlType(ECommentItemKind item)
        {
            if (DetailControlDic == null)
            {
                CreateControlDic();
            }

            //対象のTypeを特定
            bool ret = DetailControlDic.ContainsKey(item);
            if (ret == false)
            {
                throw new Exception("ControlDicに格納されていません item=" + item.ToString());
            }
                        
            Type ans = DetailControlDic[item];
            return ans;
        }

        /// <summary>
        /// コメントアイテム対応ヘッドコントロール取得
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Type GetHeadControlType(ECommentItemKind item)
        {
            if (HeadControlDic == null)
            {
                CreateControlDic();
            }

            //対象のTypeを特定
            bool ret = HeadControlDic.ContainsKey(item);
            if (ret == false)
            {
                throw new Exception("HeadControlDicに格納されていません item=" + item.ToString());
            }

            Type ans = HeadControlDic[item];
            return ans;
        }

        /// <summary>
        /// コメントアイテム対応管理クラスタイプの取得
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Type GetManagerType(ECommentItemKind item)
        {
            if (ManagerDic == null)
            {
                CreateControlDic();
            }

            //対象のTypeを特定
            bool ret = ManagerDic.ContainsKey(item);
            if (ret == false)
            {
                throw new Exception("ManagerDicに格納されていません item=" + item.ToString());
            }

            Type ans = ManagerDic[item];
            return ans;
        }

        
        /// <summary>
        /// 対象の詳細コントロールの作成
        /// </summary>
        /// <param name="item">作成タイプ</param>
        /// <returns></returns>
        public static BaseCommentItemDetailControl CreateCommentItemDetailControl(ECommentItemKind item)
        {
            BaseCommentItemDetailControl ans = null;

            try
            {
                //作成
                Type citype = CommentItemCreator.GetDetailControlType(item);        //対象の型を取得
                ans = (BaseCommentItemDetailControl)Activator.CreateInstance(citype);

            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "CreateCommentItemControl Exception");
                return null;
            }


            return ans;
        }

        /// <summary>
        /// 対象ヘッダーコントロールの作成
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BaseCommentItemHeaderControl CreateCommentItemHeadControl(ECommentItemKind item)
        {
            BaseCommentItemHeaderControl ans = null;

            try
            {
                //作成
                Type citype = CommentItemCreator.GetHeadControlType(item);        //対象の型を取得
                ans = (BaseCommentItemHeaderControl)Activator.CreateInstance(citype);

            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "CreateCommentItemHeadControl Exception");
                return null;
            }


            return ans;
        }



        /// <summary>
        /// 対象管理クラスの取得
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BaseCommentItemManager CreateCommentItemManager(ECommentItemKind item)
        {
            BaseCommentItemManager ans = null;

            try
            {
                //作成
                Type citype = CommentItemCreator.GetManagerType(item);        //対象の型を取得
                ans = (BaseCommentItemManager)Activator.CreateInstance(citype);

            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "CreateCommentItemManager Exception");
                return null;
            }


            return ans;
        }

    }
}
