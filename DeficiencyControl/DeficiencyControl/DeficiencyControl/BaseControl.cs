using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Logic;

namespace DeficiencyControl
{
    /// <summary>
    /// カスタムコントロール基底クラス
    /// <remarks>色変更とメッセージは別関数にするべき</remarks>
    /// </summary>
    public partial class BaseControl : UserControl
    {
        public BaseControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        #region 定義

        /// <summary>
        /// 削除Delegate 削除ボタンが押されたとき上に通知するためのもの
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public delegate void DeleteControlDelegate(BaseControl control);

        #endregion


        #region メンバ変数



        /// <summary>
        /// エラーリスト
        /// </summary>
        protected List<Control> ErList = new List<Control>();


        /// <summary>
        /// 削除通知用デリゲート
        /// </summary>
        public DeleteControlDelegate DeleteCallbackDelegate = null;

        #endregion 

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //メンバ関数
        
        

        

        /// <summary>
        /// エラーチェック関数 falseでエラー有
        /// </summary>
        /// <param name="chcol">コントロール色変更可否</param>
        /// <returns></returns>
        public virtual bool CheckError(bool chcol)
        {
            return false;
        }

        /// <summary>
        /// エラー表示
        /// </summary>
        /// <returns></returns>
        public virtual bool DispError()
        {
            return Logic.ComLogic.ChangeColor(this.ErList, DeficiencyControlColor.EColor);
        }

        /// <summary>
        /// エラー表示リセット
        /// </summary>
        /// <returns></returns>
        public virtual bool ResetError()
        {
            return Logic.ComLogic.ResetColor(this.ErList);            
        }


        /// <summary>
        /// コントロールの初期化関数
        /// </summary>
        /// <param name="inputdata">入力データ</param>
        /// <returns></returns>
        public virtual bool InitControl(object inputdata)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// ファイルが選択されたとき 中でWaiteStateします
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool FileViewItemSelect(string text, object data, SaveFileDialog savedialog)
        {
            FileDispData fd = data as FileDispData;
            if (fd == null)
            {
                return false;
            }
            if (fd.Attachment == null)
            {
                return false;
            }

            try
            {
                ///保存
                ComLogic.DownloadSaveAttachment(this.ParentForm, fd.Attachment, savedialog);
            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "BaseControl EfileViewControlEx_FileItemSelected");
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_6);
                return false;
            }


            return true;
        }


        /// <summary>
        /// コントロール有効可否設定
        /// </summary>
        /// <param name="contvec"></param>
        /// <param name="enable"></param>
        protected void ChangeEnableControl(Control[] contvec, bool enable)
        {
            foreach (Control con in contvec)
            {
                con.Enabled = enable;
            }
        }


        /// <summary>
        /// 最初のエラーコントロールを取得する null=なし
        /// </summary>
        /// <returns></returns>
        public Control GetErrorFirstControl()
        {
            if (this.ErList.Count <= 0)
            {
                return null;
            }

            return this.ErList[0];
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //イベント
    }
}
