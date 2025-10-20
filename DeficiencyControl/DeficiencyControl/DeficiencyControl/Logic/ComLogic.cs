using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;


using DeficiencyControl.Util;
using DeficiencyControl.Forms;
using DeficiencyControl.Controls;
using CIsl.DB;

namespace DeficiencyControl.Logic
{
    /// <summary>
    /// 汎用ロジッククラス
    /// </summary>
    public class ComLogic
    {
        /// <summary>
        /// 色の変更
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool ChangeColor(List<Control> datalist, Color col)
        {
            foreach (Control c in datalist)
            {
                //色変更しないもの                
                if ((c as BaseControl) != null)
                {
                    continue;
                }
                //---------------

                c.BackColor = col;
            }

            return true;
        }


        /// <summary>
        /// コントロールの色を戻す
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public static bool ResetColor(List<Control> datalist)
        {
            foreach (Control c in datalist)
            {
                ///////////////////////////////////////////////////////////                                
                if ((c as CheckBox) != null)
                {
                    continue;
                }

                BaseControl bc = c as BaseControl;
                if (bc != null)
                {
                    bc.ResetError();
                    continue;
                }
                ///////////////////////////////////////////////////////////


                TextBox tb = c as TextBox;
                if (tb != null)
                {
                    tb.BackColor = Color.White;
                    continue;
                }

                SingleLineCombo sl = c as SingleLineCombo;
                if (sl != null)
                {
                    sl.BackColor = DeficiencyControlColor.SingleLineColorCombo; ;
                    continue;
                }

                Button bu = (c as Button);
                if (bu != null)
                {
                    bu.BackColor = SystemColors.Control;
                    bu.UseVisualStyleBackColor = true;
                    continue;
                }

                Label labe = (c as Label);
                if (labe != null)
                {
                    labe.BackColor = SystemColors.Control;
                    continue;
                }

                Panel pane = (c as Panel);
                if (pane != null)
                {
                    //pane.BackColor = SystemColors.Control;
                    pane.BackColor = pane.Parent.BackColor;
                    continue;
                }



                c.BackColor = Color.White;


            }

            return true;
        }


        /// <summary>
        /// 入力可否をチェックするもの 返り値はエラーになったもの一覧。countが0なら問題なし。ComboBoxのエラーチェックできません、個別で行うこと。非表示の物体はチェックしません
        /// <param name="contvec">チェック対象まとめ</param>
        /// <returns></returns>
        /// </summary>
        public static List<Control> CheckInput(Control[] contvec)
        {
            List<Control> ansllist = new List<Control>();

            foreach (Control con in contvec)
            {
                //---------------------------------------------------------------------
                //SingleLineCombo入力チェック
                SingleLineCombo si = con as SingleLineCombo;
                if (si != null)
                {
                    if (si.Visible == true)
                    {
                        if (si.SelectedItem == null)
                        {
                            ansllist.Add(con);
                        }
                    }
                    continue;
                }

                //---------------------------------------------------------------------
                //Textbox
                TextBox tb = con as TextBox;
                if (tb != null)
                {
                    //表示されているものだけ
                    if (tb.Visible == true)
                    {
                        if (tb.Text.Trim().Length <= 0)
                        {
                            ansllist.Add(con);
                        }
                    }
                }


            }

            return ansllist;
        }

        /// <summary>
        /// ファイルを選択し、ADDする
        /// </summary>
        /// <param name="dialog">ダイアログ</param>
        /// <param name="ex">ADDする場所</param>
        public static void OpenFileAttachment(OpenFileDialog dialog, FileViewControlEx cont)
        {
            //ダイアログ起動
            dialog.Multiselect = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.FileName = "";

            DialogResult dret = dialog.ShowDialog();
            if (dret != DialogResult.OK)
            {
                return;
            }

            //ADDする
            foreach (string filename in dialog.FileNames)
            {
                cont.ReadAddFile(filename);
            }
        }

        /// <summary>
        /// 対象をダウンロードして保存する、中でwaitstateしています ダウンロード失敗時はExceptionが出ます
        /// </summary>
        /// <param name="pf">waitstate表示フォーム</param>
        /// <param name="data">保存元</param>
        /// <param name="savedialog">ダイアログ</param>
        /// <returns></returns>
        public static bool DownloadSaveAttachment(Form pf, DcAttachment data, SaveFileDialog savedialog)
        {
            try
            {

                //保存名の取得
                savedialog.FileName = data.filename;
                DialogResult dret = savedialog.ShowDialog();
                if (dret != DialogResult.OK)
                {
                    return false;
                }

                string filename = savedialog.FileName;

                //ファイルデータのダウンロード
                using (WaitingState es = new WaitingState(pf))
                {
                    DcAttachment file = SvcManager.SvcMana.DcAttachment_DownloadAttachment(data.attachment_id);
                    if (file == null)
                    {
                        throw new Exception("DcAttachment_DownloadAttachment NULL");
                    }

                    //保存
                    DcGlobal.ByteArrayToFile(filename, file.file_data);

                    DcGlobal.ExecuteDefaultApplication(filename);
                }


            }
            catch (Exception e)
            {
                throw new Exception("DownloadSaveAttachment", e);
            }


            return true;


        }


        /// <summary>
        /// 一番適したアラーム色を返却 nullならアラームなし。通常色で表示せよ
        /// </summary>
        /// <param name="today">今日の日付</param>
        /// <param name="aldate">アラーム設定日</param>
        /// <returns></returns>
        public static MsAlarmColor GetAlarmColor(DateTime today, DateTime aldate)
        {
            List<MsAlarmColor> collist = DcGlobal.Global.DBCache.AlarmColorList;

            //データ無し
            if (collist.Count <= 0)
            {
                return null;
            }


            TimeSpan sp = aldate - today;
            int restday = Convert.ToInt32(sp.TotalDays);

            MsAlarmColor ans = null;

            //対象の検索
            foreach (MsAlarmColor ainfo in collist)
            {
                //残り日数に引っかかるか？
                if (ainfo.day_count < restday)
                {
                    //小さいのは引っかからない。残り30日で、15日前のアラーム色は引っかからない。
                    continue;
                }

                //初めてなら設定して次
                if (ans == null)
                {
                    ans = ainfo;
                    continue;
                }

                //自分より小さいなら入れ替え
                //残り日数に引っかかるのが複数あるなら一番小さいのが適用。残り8日で10 20 5の時はじめ10、次20が来たら10のほうを採用する。
                //最後の5は初めの残り日数チェックではじかれるのでここには来ない。
                if (ans.day_count > ainfo.day_count)
                {
                    ans = ainfo;
                }
            }

            return ans;
        }


        /// <summary>
        /// Databaseの更新リストを作成する
        /// </summary>
        /// <typeparam name="T">DACクラス</typeparam>
        /// <param name="srclist">元、ないときはnull</param>
        /// <param name="nowlist">現在のリスト</param>
        /// <returns></returns>
        public static List<T> CreateDBUpdateList<T>(List<T> srclist, List<T> nowlist) where T : BaseDac
        {
            List<T> anslist = new List<T>();

            //今のリストはそのままＡＤＤ
            anslist.AddRange(nowlist);


            //元がないなら変更はない
            if (srclist == null)
            {
                return anslist;
            }

            //既存のリストになく、srclistにあるなら削除されている。
            //削除用のADDをする

            //元を検索
            foreach (T src in srclist)
            {
                //元のIDはいますか？
                var sel = from f in anslist where f.ID == src.ID select f;

                if (sel.Count() <= 0)
                {
                    //いないなら削除ＡＤＤ
                    src.delete_flag = true;
                    anslist.Add(src);
                }
            }

            return anslist;
        }


        /// <summary>
        /// 現在のファイルリスト元のリストからDB更新用のデータを作り出す
        /// </summary>
        /// /// <param name="srclist">元のリスト ないときはnull</param>
        /// <param name="filelist">現在の選択ファイルリスト</param>        
        /// <returns></returns>
        public static List<DcAttachment> CreateAttachmentList(List<DcAttachment> srclist, List<DcAttachment> filelist, EAttachmentType attype)
        {

            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {

                //今リストをADD
                foreach (DcAttachment f in filelist)
                {
                    //一致する？
                    if (f.AttachmentType == attype)
                    {
                        anslist.Add(f);
                    }
                }


                //元がないならここまで
                if (srclist == null)
                {
                    return anslist;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////
                //元があるときは削除されたもの削除としてADDする
                foreach (DcAttachment src in srclist)
                {
                    //同じ種別の時のみ！
                    if (src.AttachmentType != attype)
                    {
                        continue;
                    }

                    //現在のリストを検索
                    var sel = from f in anslist where f.attachment_id == src.attachment_id select f;

                    //現在のリストでない=削除された
                    if (sel.Count() <= 0)
                    {
                        //削除通知
                        src.delete_flag = true;
                        anslist.Add(src);
                    }
                }


            }
            catch (Exception e)
            {
                throw new Exception("CreateAttachmentList exception", e);
            }


            return anslist;

        }





        /// <summary>
        /// チェックがついているコントロールを全て取得する。
        /// </summary>
        /// <param name="pane"></param>
        /// <returns></returns>
        private static List<Control> GetDeleteChildControlList(FlowLayoutPanel pane)
        {
            //削除するもの一式
            List<Control> delconlist = new List<Control>();

            //削除対象の選出を行う
            foreach (Control cont in pane.Controls)
            {
                BaseChildItemControl item = cont as BaseChildItemControl;
                if (item == null)
                {
                    continue;
                }

                //削除チェックがついていた
                if (item.DeleteCheck == true)
                {
                    delconlist.Add(item);
                }
            }

            return delconlist;
        }

        /// <summary>
        /// チェックがついているコントロールの数を取得する
        /// </summary>
        /// <param name="pane"></param>
        public static int CalcuDeleteChildControlCount(FlowLayoutPanel pane)
        {
            //削除するもの一式を取得
            List<Control> delconlist = ComLogic.GetDeleteChildControlList(pane);

            return delconlist.Count;
        }
        /// <summary>
        /// チェックがついているコントロールを取得する。
        /// </summary>
        /// <param name="pane"></param>
        public static void DeleteChildControl(FlowLayoutPanel pane)
        {
            //削除するもの一式を取得
            List<Control> delconlist = ComLogic.GetDeleteChildControlList(pane);

            //外す
            foreach (Control delcon in delconlist)
            {
                pane.Controls.Remove(delcon);
            }
        }

    }
}
