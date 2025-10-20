using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using DcCommon.DB;
using DcCommon.DB.DAC;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Util
{

    /// <summary>
    /// ファイル表示コントロール
    /// <remarks>案件管理を元により進化したファイル一覧コントロール</remarks>
    /// </summary>
    public partial class FileViewControlEx : UserControl
    {
        public FileViewControlEx()
        {
            InitializeComponent();
        }


        /// <summary>
        /// FileControlのエラー
        /// </summary>
        public class FileDataException : Exception
        {
            public FileDataException(string message)
                : base(message)
            {
            }

            public FileDataException(string message, Exception innerexception)
                : base(message, innerexception)
            {
            }
        }

        /// <summary>
        /// ファイル選択イベント
        /// </summary>
        /// <param name="text">表示文字列</param>
        /// <param name="data">FileDispData</param>
        /// <returns></returns>
        public delegate bool FileItemSelectDelegate(string text, object data);

        #region プロパティ
              

        /// <summary>
        /// ファイル選択時
        /// </summary>
        [Description("アイテムの選択時に発生します")]
        public event FileItemSelectDelegate FileItemSelected = null;

        /// <summary>
        /// 削除有効可否
        /// </summary>
        [Description("アイテムの削除有効可否")]
        public bool EnableDelete
        {
            get;
            set;
        }

        /// <summary>
        /// ドラッグアンドドロップ有効可否
        /// </summary>
        [Description("ドラッグアンドドロップ有効可否")]
        public bool EnableDragDrop
        {
            get;
            set;
        }
        #endregion


        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 選択データの取得
        /// </summary>
        /// <returns></returns>
        private FileDispData GetSelectData()
        {
            ListViewItem sel = null;

            //一つあれば十分
            ListView.SelectedListViewItemCollection vec = this.listViewFile.SelectedItems;
            foreach (ListViewItem item in vec)
            {
                sel = item;
                break;
            }
            if (sel == null)
            {
                return null;
            }
            
            //取得
            FileDispData ans = sel.Tag as FileDispData;
            return ans;
        }

        /// <summary>
        /// 選択対象の削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteSelectItem()
        {
            //選択の取得と削除
            ListView.SelectedListViewItemCollection vec = this.listViewFile.SelectedItems;
            foreach (ListViewItem item in vec)
            {
                this.listViewFile.Items.Remove(item);
            }
            
            return true;
        }

        /// <summary>
        /// 現在の一覧を取得する
        /// </summary>
        /// <returns></returns>
        private List<FileDispData> GetItemList()
        {
            List<FileDispData> anslist = new List<FileDispData>();

            //全アイテムで取得
            foreach (ListViewItem item in this.listViewFile.Items)
            {
                FileDispData data = item.Tag as FileDispData;
                if (data != null)
                {
                    anslist.Add(data);
                }
            }


            return anslist;
        }

        //===========================================================================
        /// <summary>
        /// ファイルパスからアイテムを追加する 
        /// </summary>
        /// <param name="filepath">読み込みファイル名</param>
        /// <returns></returns>
        /// <exception cref="AttachmentFileSizeOverException：これが発生したら基本的に通知を行うこと"></exception>
        public bool ReadAddFile(string filepath)
        {
            try
            {
                                
                //ファイルある？
                bool fext = File.Exists(filepath);
                if (fext == false)
                {
                    throw new Exception("ファイルがありません");
                }

                //ファイルサイズの確認
                FileInfo finfo = new FileInfo(filepath);
                if (finfo.Length >= AppConfig.Config.ConfigData.MaxAttachmentSizeB)
                {
                    throw new AttachmentFileSizeOverException("");
                }

                //ファイルアイコンの取得と追加
                Icon icon = Icon.ExtractAssociatedIcon(filepath);
                this.imageListFile.Images.Add(icon);
                
                //表示データの作成
                FileDispData fdata = new FileDispData() { FilePath = filepath, DispText = Path.GetFileName(filepath), Attachment = null, IconData = icon };

                //リストアイテムの作成
                ListViewItem item = new ListViewItem();
                item.Text = fdata.DispText;
                item.ImageIndex = this.imageListFile.Images.Count - 1;
                item.Tag = fdata;
                item.ToolTipText = fdata.FilePath;

                this.listViewFile.Items.Add(item);

            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        /// <summary>
        /// 既存のデータをリストに追加する
        /// </summary>
        /// <param name="attach">追加したいデータ</param>
        /// <returns>成功可否</returns>
        public bool ReadAddList(DcAttachment attach)
        {
            try
            {
                
                Icon icon = null;
                //アイコンの読み込み
                using (MemoryStream ms = new MemoryStream(attach.icon_data))
                {
                    //icon = new Icon(ms);
                    Image image = Image.FromStream(ms);
                    this.imageListFile.Images.Add(image);
                }


                //表示データの作成
                FileDispData fdata = new FileDispData() { FilePath = "", DispText = attach.filename, Attachment = attach, IconData = icon };

                //リストアイテムの作成
                ListViewItem item = new ListViewItem();
                item.Text = fdata.DispText;
                item.ImageIndex = this.imageListFile.Images.Count - 1;
                item.Tag = fdata;
                item.ToolTipText = fdata.DispText;

                this.listViewFile.Items.Add(item);

            }

            catch(Exception e)
            {
                throw e;
            }

            return true;
        }

        
        /// <summary>
        /// ADD物の全クリア
        /// </summary>
        /// <returns></returns>
        public bool ClearItem()
        {
            this.listViewFile.Items.Clear();
            return true;
        }

        /// <summary>
        /// 現在のリストと過去のリストを比較し、DB更新用のデータを作成する。
        /// </summary>
        /// <param name="srclist">元 ないときはnull</param>
        /// <param name="attype">付加する添付ファイル種別</param>
        /// <returns></returns>
        public List<DcAttachment> CreateInsertUpdateAttachmentList(List<DcAttachment> srclist, EAttachmentType attype)
        {
            List<FileDispData> filelist = this.GetItemList();

            return this.CreateAttachmentList(filelist, srclist, attype);
        }

        /// <summary>
        /// 現在のファイルリスト元のリストからDB更新用のデータを作り出す
        /// </summary>        
        /// <param name="filelist">現在の選択ファイルリスト</param>
        /// <param name="srclist">元のリスト ないときはnull</param>        
        /// <returns></returns>
        public List<DcAttachment> CreateAttachmentList(List<FileDispData> filelist, List<DcAttachment> srclist, EAttachmentType attype)
        {

            List<DcAttachment> anslist = new List<DcAttachment>();

            try
            {
                List<DcAttachment> nowlist = new List<DcAttachment>();

                //挿入、更新項目を確認
                foreach (FileDispData fd in filelist)
                {
                    //元がある場合はなにもしない
                    //ファイル更新はありえない。挿入か削除のみのため
                    if (fd.Attachment != null)
                    {
                        //削除確認のため既存の項目をマーク
                        nowlist.Add(fd.Attachment);
                        continue;
                    }

                    //ここにきたら新規なので作成してADD
                    DcAttachment attach = new DcAttachment();

                    #region Attachment作成
                    try
                    {
                        //ファイル名
                        attach.filename = Path.GetFileName(fd.FilePath);
                        //アイコン
                        attach.icon_data = DcGlobal.IconToByteArray(fd.IconData);
                        //ファイルデータ
                        attach.file_data = DcGlobal.FileToByteArray(fd.FilePath);

                        //種別
                        //attach.AttachmentType = attype;
                        attach.attachment_type_id = (int)attype;

                        //削除してない
                        attach.delete_flag = false;
                    }
                    catch (Exception ef)
                    {
                        throw new FileDataException("CreateAttachmentList AttachmentCreate", ef);
                    }
                    #endregion

                    anslist.Add(attach);

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
                    var sel = from f in nowlist where f.attachment_id == src.attachment_id select f;

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
                //throw new Exception("CreateInsertUpdateAttachmentList exception", e);
                throw e;
            }


            return anslist;

        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileViewControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// リストが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_DoubleClick(object sender, EventArgs e)
        {
            //選択の取得
            FileDispData fdata = this.GetSelectData();
            if (fdata == null)
            {
                return;
            }

            if (this.FileItemSelected != null)
            {
                this.FileItemSelected(fdata.DispText, fdata);
            }
        }

        /// <summary>
        /// 何かキーが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_KeyDown(object sender, KeyEventArgs e)
        {
            //削除有効？
            if (this.EnableDelete == false)
            {
                return;
            }

            //DELETE?
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteSelectItem();
            }

        }

        /// <summary>
        /// ドラッグドロップされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_DragDrop(object sender, DragEventArgs e)
        {
            //許可する？
            if (this.EnableDragDrop == false)
            {
                return;
            }

            //ドロップされてきたファイル名の取得
            string[] fvec = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            try
            {
                //全ファイルのADD
                foreach (string f in fvec)
                {
                    this.ReadAddFile(f);
                }
            }
            catch (AttachmentFileSizeOverException ex)
            {
                DcMes.ShowMessage(this.ParentForm, EMessageID.MI_5);
            }
            catch
            {
            }
        }

        /// <summary>
        /// ドラッグアンドドロップが領域に入ったとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFile_DragEnter(object sender, DragEventArgs e)
        {
            //許可する？
            if (this.EnableDragDrop == false)
            {
                return;
            }


            e.Effect = DragDropEffects.None;

            //ファイルなら許可
            bool ret = e.Data.GetDataPresent(DataFormats.FileDrop);
            if (ret == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }

    

}
