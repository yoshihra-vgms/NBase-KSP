using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Senin.util;
using NBaseUtil;
using NBaseData.DAC;
using NBaseData.DS;
using System.IO;

namespace Senin
{
    public partial class 免許免状一覧Form : Form
    {
        private List<SiMenjou> menjouList = null;
        private SiMenjouFilter filter = null;

        private TreeListViewDelegate免状免許一覧 treeListViewDelegate免状免許一覧;
        private TreeListViewDelegate職名一覧 treeListViewDelegate職名一覧;

        private static 免許免状一覧Form instance;

        public 免許免状一覧Form()
        {
            InitializeComponent();
        }

        public static 免許免状一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new 免許免状一覧Form();
            }

            return instance;
        }

        private void 免許免状一覧Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void 免許免状一覧Form_Load(object sender, EventArgs e)
        {
            Init();
            SetDataGridView(new List<RowData>());
        }

        private void Init()
        {
            treeListViewDelegate免状免許一覧 = new TreeListViewDelegate免状免許一覧(treeListView免許免状, true);
            treeListViewDelegate職名一覧 = new TreeListViewDelegate職名一覧(treeListView職名, true);

            treeListViewDelegate免状免許一覧.AddNodes(
                SeninTableCache.instance().GetMsSiMenjouList(NBaseCommon.Common.LoginUser),
                SeninTableCache.instance().GetMsSiMenjouKindList(NBaseCommon.Common.LoginUser));
            treeListViewDelegate職名一覧.AddNodes(
                SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser));

            List<Yukokigen> yukoKigens = Yukokigen.GetRecords();
            comboBox有効期限.Items.Add("");
            foreach (Yukokigen kigen in yukoKigens)
            {
                comboBox有効期限.Items.Add(kigen);
            }

            label対象件数.Text = "      件";
        }

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonクリア_Click(object sender, EventArgs e)
        private void buttonクリア_Click(object sender, EventArgs e)
        {
            ResetSearchKey();
        }
        #endregion

        /// <summary>
        /// 一覧の行をダブルクリック
        /// ・「免許/免状詳細」画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiMenjou menjou = dataGridView1.SelectedRows[0].Cells[8].Value as SiMenjou;
            if (menjou.DeleteFlag == 1)
            {
                MessageBox.Show("その情報は削除されています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            免状免許詳細Form form = new 免状免許詳細Form(this, "", menjou, false);
            form.ShowDialog();
        }
        #endregion


        /// <summary>
        /// 詳細画面で変更が発生したときの処理
        /// </summary>
        #region public void ReSearch()
        public void ReSearch()
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「Excel出力」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "免許・免状一覧_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_免許免状一覧出力(NBaseCommon.Common.LoginUser, filter, menjouList);
                        }
                    }, "免許・免状一覧を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("免許・免状一覧の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "未受講者抽出", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show("免許・免状一覧の出力に失敗しました。\n (Err:" + ex.Message + ")", "未受講者抽出", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion


        /// <summary>
        /// 検索条件の初期化
        /// </summary>
        #region private void ResetSearchKey()
        private void ResetSearchKey()
        {
            treeListViewDelegate免状免許一覧.AllUncheck();
            treeListViewDelegate職名一覧.AllUncheck();
            textBox氏名コード.Text = null;
            textBox氏名.Text = null;
            comboBox有効期限.SelectedIndex = -1;
            radioButton未取得.Checked = true;
        }
        #endregion


        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        #region internal void Search()
        internal void Search()
        {          
            List<RowData> rowDataList = null;

            // 検索条件の確認および取得
            if (CreateFilter() == false)
                return;

            // 検索
            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    List<MsSenin> seninList = serviceClient.MsSenin_GetRecords(NBaseCommon.Common.LoginUser);
                    menjouList = serviceClient.BLC_免許免状管理_検索(NBaseCommon.Common.LoginUser, filter);

                    rowDataList = RowData.BuildRowData(menjouList, seninList);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            label対象件数.Text = String.Format("{0,5}", rowDataList.Count()) + " 件";

            // データ表示
            SetDataGridView(rowDataList);
        }
        #endregion

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        #region private void SetDataGridView(List<RowData> rowDataList)
        private void SetDataGridView(List<RowData> rowDataList)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名" + Environment.NewLine + "コード";
                textColumn.Width = 65;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "免許・免状名";
                textColumn.Width = 150;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "種別";
                textColumn.Width = 225;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "番号";
                textColumn.Width = 125;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "有効期限";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "取得日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                DataGridView免許免状RowColumn column = new DataGridView免許免状RowColumn();
                column.Visible = false;
                dataGridView1.Columns.Add(column);
            }
            #endregion

            int rowNo = 0;
            foreach (RowData row in rowDataList)
            {
                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[9];
                rowDatas[colNo] = row.msSenin.ShimeiCode;
                colNo++;
                rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, row.msSenin.MsSiShokumeiID);
                colNo++;
                rowDatas[colNo] = row.msSenin.Sei + " " + row.msSenin.Mei;
                colNo++;
                rowDatas[colNo] = row.msSiMenjou.Name;
                colNo++;
                rowDatas[colNo] = row.msSiMenjouKind == null ? "" : row.msSiMenjouKind.Name;
                colNo++;
                rowDatas[colNo] = row.menjou.No;
                colNo++;
                rowDatas[colNo] = (row.menjou.Kigen == DateTime.MinValue ? "" : row.menjou.Kigen.ToShortDateString());
                colNo++;
                rowDatas[colNo] = (row.menjou.ShutokuDate == DateTime.MinValue ? "" : row.menjou.ShutokuDate.ToShortDateString());
                colNo++;
                rowDatas[colNo] = row.menjou;

                dataGridView1.Rows.Add(rowDatas);

                rowNo++;

                #endregion
            }

            Cursor = Cursors.Default;

        }
        #endregion

        /// <summary>
        /// 検索条件の確認および取得
        /// </summary>
        /// <returns></returns>
        #region private bool CreateFilter()
        private bool CreateFilter()
        {
            bool ret = true;

            filter = new SiMenjouFilter();

            List<MsSiMenjou> checkedMenjou = treeListViewDelegate免状免許一覧.GetCheckedNodes();
            foreach (MsSiMenjou menjou in checkedMenjou)
            {
                if (menjou.menjouKinds.Count == 0)
                {
                    filter.MsSiMenjouIDs.Add(menjou.MsSiMenjouID);
                }
                else
                {
                    foreach (MsSiMenjouKind kind in menjou.menjouKinds)
                    {
                        filter.MsSiMenjouKindIDs.Add(kind.MsSiMenjouKindID);
                    }
                }
            }
            if (checkedMenjou.Count == 0)
            {
                MessageBox.Show("免許・免状/種別を一つ以上選択して下さい", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            List<MsSiShokumei> checkedShokumei = treeListViewDelegate職名一覧.GetCheckedNodes();
            foreach (MsSiShokumei shokumei in checkedShokumei)
            {
                filter.MsSiShokumeiIDs.Add(shokumei.MsSiShokumeiID);
            }

            if (textBox氏名コード.Text.Length > 0)
            {
                filter.ShimeiCode = textBox氏名コード.Text;
            }
            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text;
            }

            if (comboBox有効期限.SelectedItem is Yukokigen)
            {
                filter.Yukokigen = (comboBox有効期限.SelectedItem as Yukokigen).kigen;
            }

            if (radioButton取得済.Checked)
            {
                filter.is取得済 = true;
            }
            if (radioButton未取得.Checked)
            {
                filter.is未取得 = true;
            }

            filter.Is退職者を除く = true;// 退職者を除く

            return ret;
        }
        #endregion



        #region 内部クラス
        private class Yukokigen
        {
            public int kigen;
            public string name;

            public override string ToString()
            {
                return name;
            }

            public Yukokigen(int k, string n)
            {
                kigen = k;
                name = n;
            }

            public static List<Yukokigen> GetRecords()
            {
                List<Yukokigen> ret = new List<Yukokigen>();

                ret.Add(new Yukokigen(12, "１２ヶ月"));
                ret.Add(new Yukokigen(6, "６ヶ月"));
                ret.Add(new Yukokigen(3, "３ヶ月"));
                ret.Add(new Yukokigen(1, "１ヶ月"));

                return ret;
            }
        }

        private class RowData
        {
            public SiMenjou menjou = null;

            public MsSenin msSenin = null;
            public MsSiMenjou msSiMenjou = null;
            public MsSiMenjouKind msSiMenjouKind = null;

            public static List<RowData> BuildRowData(List<SiMenjou> menjouList, List<MsSenin> seninList)
            {
                List<RowData> ret = new List<RowData>();

                Dictionary<int, MsSenin> seninDic = new Dictionary<int, MsSenin>();
                foreach (MsSenin senin in seninList)
                {
                    seninDic.Add(senin.MsSeninID, senin);
                }

                foreach (SiMenjou menjou in menjouList)
                {
                    if (seninDic.ContainsKey(menjou.MsSeninID) == false)
                        continue;

                    RowData row = new RowData();

                    row.menjou = menjou;
                    row.msSenin = seninDic[menjou.MsSeninID];
                    row.msSiMenjou = SeninTableCache.instance().GetMsSiMenjou(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouID);
                    row.msSiMenjouKind = SeninTableCache.instance().GetMsSiMenjouKind(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouKindID);
                    if (row.msSiMenjouKind == null)
                        row.msSiMenjouKind = new MsSiMenjouKind();

                    ret.Add(row);
                }

                return ret.OrderBy(obj => obj.msSenin.MsSiShokumeiID).ThenBy(obj => obj.msSenin.SeiKana).ThenBy(obj => obj.msSenin.MeiKana).ThenBy(obj => obj.msSiMenjou.ShowOrder).ThenBy(obj => obj.msSiMenjouKind.ShowOrder).ThenBy(obj => obj.menjou.ShutokuDate).ToList();
            }
        }

        public class DataGridView免許免状RowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView免許免状RowColumn()
            {
                this.CellTemplate = new DataGridView免許免状RowCell();
            }

            //CellTemplateの取得と設定
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    //DataGridViewProgressBarCell以外はホストしない
                    if (!(value is DataGridView免許免状RowCell))
                    {
                        throw new InvalidCastException(
                            "DataGridViewProgressBarCellオブジェクトを" +
                            "指定してください。");
                    }
                    base.CellTemplate = value;
                }
            }

            /// <summary>
            /// ProgressBarの最大値
            /// </summary>
            public int Maximum
            {
                get
                {
                    return ((DataGridView免許免状RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView免許免状RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView免許免状RowCell)r.Cells[this.Index]).Maximum =
                            value;
                    }
                }
            }

            /// <summary>
            /// ProgressBarの最小値
            /// </summary>
            public int Mimimum
            {
                get
                {
                    return ((DataGridView免許免状RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView免許免状RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView免許免状RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView免許免状RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView免許免状RowCell()
            {
                this.maximumValue = 100;
                this.mimimumValue = 0;
            }

            private int maximumValue;
            public int Maximum
            {
                get
                {
                    return this.maximumValue;
                }
                set
                {
                    this.maximumValue = value;
                }
            }

            private int mimimumValue;
            public int Mimimum
            {
                get
                {
                    return this.mimimumValue;
                }
                set
                {
                    this.mimimumValue = value;
                }
            }

            //セルの値のデータ型を指定する
            //ここでは、整数型とする
            public override Type ValueType
            {
                get
                {
                    return typeof(int);
                }
            }

        }
        #endregion
    }
}
