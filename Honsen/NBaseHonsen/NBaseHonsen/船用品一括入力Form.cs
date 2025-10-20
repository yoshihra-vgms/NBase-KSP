using SyncClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseHonsen
{
    public partial class 船用品一括入力Form : Form
    {
        private readonly 手配依頼Form tehaiIraiForm;

        private bool saved;

        List<MsVesselItemCategory> vesselItemCategorys = null;
        List<MsVesselItem> vesselItems = null;
        List<MsVesselItemVessel> vesselItemVessels = null;
        Dictionary<string, MsVesselItemVessel> MsVesselItemVesselDic = new Dictionary<string, MsVesselItemVessel>();

        /// <summary>
        /// 在庫数量
        /// </summary>
        List<OdChozoShousai> odChozoShousais = null;


        List<OdThiShousaiItem> shousaiItemListFromMaster = null;
        List<OdThiShousaiItem> shousaiItemListForInput = null;


        public 船用品一括入力Form(手配依頼Form tehaiIraiForm)
        {
            this.tehaiIraiForm = tehaiIraiForm;

            InitializeComponent();

            Text = NBaseCommon.Common.WindowTitle("", "船用品リスト入力", WcfServiceWrapper.ConnectedServerID);
        }

        private void 特定品リスト入力Form_Load(object sender, EventArgs e)
        {
            singleLineComboEx1.Width = 850;

            vesselItemCategorys = MsVesselItemCategory.GetRecords(NBaseCommon.Common.LoginUser);
            vesselItems = MsVesselItem.GetRecords(NBaseCommon.Common.LoginUser);
            vesselItemVessels = MsVesselItemVessel.GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, 同期Client.LOGIN_VESSEL.MsVesselID);
            odChozoShousais = NBaseData.BLC.船用品.BLC_Get特定船用品在庫(SyncClient.同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);

            var sortedList = vesselItemVessels.OrderBy(obj => obj.CategoryNumber).ThenBy(obj => obj.VesselItemName).ThenBy(obj => obj.MsTaniID).ThenBy(obj => obj.SpecificFlag);
            foreach (MsVesselItemVessel viv in sortedList)
            {
                // 対象となるのは「ペイント」以外
                if (viv.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                    continue;

                string value = "";

                var vic = vesselItemCategorys.Where(obj => obj.CategoryNumber == viv.CategoryNumber);
                var vi = vesselItems.Where(obj => obj.MsVesselItemID == viv.MsVesselItemID);
                if (vic.Count() > 0)
                {
                    value += vic.First().CategoryName.Substring(0, 1);
                }
                value += " ：" + (viv.SpecificFlag == 1 ? "(特)" : "　　 ")　+ vi.First().VesselItemName;
                value += " ： " + vi.First().TaniName;
                value += " ： " + vi.First().Bikou.Replace('\n', ' ');


                //singleLineComboEx1.AutoCompleteCustomSource.Add(value);

                SingleLineComboEx.SingleLineComboExItem item = new SingleLineComboEx.SingleLineComboExItem();
                item.Key1 = viv.VesselItemName;
                item.Key2 = viv.Bikou;
                item.Value = value;
                singleLineComboEx1.Items.Add(item);

                if (MsVesselItemVesselDic.ContainsKey(value) == false)
                {
                    MsVesselItemVesselDic.Add(value, viv);
                }
            }

            singleLineComboEx1.selected += new SingleLineComboEx.SelectedEventHandler(詳細品目選択);



            shousaiItemListFromMaster = new List<OdThiShousaiItem>();
            SetRows(dataGridView1, shousaiItemListFromMaster);


            shousaiItemListForInput = new List<OdThiShousaiItem>();
            SetRows(dataGridView2, shousaiItemListForInput);



            button追加.Enabled = true;
            button複写.Enabled = true;
            button削除.Enabled = true;

            button複写_追加詳細.Enabled = true;
            button削除_追加詳細.Enabled = true;

            button登録.Enabled = true;

        }

        private void button登録_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                BuildOdThiItem();
                saved = true;
                Close();
            }
        }

        public bool ValidateFields()
        {
            // 部署名
            if (textBoxヘッダ.Text.Length == 0)
            {
                MessageBox.Show("部署名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void BuildOdThiItem()
        {
            var categorys1 = shousaiItemListFromMaster.Select(obj => obj.CategoryNumber).Distinct();
            var categorys2 = shousaiItemListForInput.Select(obj => obj.CategoryNumber).Distinct();
            var categorys = categorys1.Union(categorys2);

            foreach(int category in categorys)
            {
                OdThiItem odThiItem = new OdThiItem();

                odThiItem.OdThiItemID = System.Guid.NewGuid().ToString();
                odThiItem.Header = StringUtils.Escape(textBoxヘッダ.Text);

                odThiItem.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                odThiItem.DataNo = 0;

                var shousai1 = shousaiItemListFromMaster.Where(obj => obj.CategoryNumber == category && obj.Count > 0);
                if (shousai1.Count() > 0)
                {
                    odThiItem.ItemName = shousai1.First().CategoryName;

                    foreach(OdThiShousaiItem shousai in shousai1)
                    {
                        shousai.CategoryNumber = category;
                        shousai.CategoryName = odThiItem.ItemName;

                        shousai.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
                        shousai.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                        shousai.DataNo = 0;
                        shousai.Sateisu = shousai.Count;

                        odThiItem.OdThiShousaiItems.Add(shousai);
                    }
                }

                var shousai2 = shousaiItemListForInput.Where(obj => obj.CategoryNumber == category);
                if (shousai2.Count() > 0)
                {
                    odThiItem.ItemName = shousai2.First().CategoryName;

                    foreach (OdThiShousaiItem shousai in shousai2)
                    {
                        shousai.CategoryNumber = category;
                        shousai.CategoryName = odThiItem.ItemName;

                        shousai.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
                        shousai.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                        shousai.DataNo = 0;
                        shousai.Sateisu = shousai.Count;

                        odThiItem.OdThiShousaiItems.Add(shousai);
                    }
                }

                if (shousai1.Count() > 0 || shousai2.Count() > 0)
                {
                    tehaiIraiForm.AddOdThiItem(odThiItem);
                }
            }

        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 特定品リスト入力Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            同期Client.SYNC_SUSPEND = false;
        }

        private void 特定品リスト入力Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("このウィンドウを閉じてよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }





        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        #region private void SetRows(DataGridView dataGridView,List<OdThiShousaiItem> shousaiItemList)
        private void SetRows(DataGridView dataGridView, List<OdThiShousaiItem> shousaiItemList)
        {
            dataGridView.Rows.Clear();

            #region カラムの設定
            if (dataGridView.Columns.Count == 0)
            {

                DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                Column.HeaderText = "OBJ";
                Column.Visible = false;
                dataGridView.Columns.Add(Column);

                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "No";
                textColumn.Width = 60;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "仕様・型式";
                textColumn.Width = 155;
                textColumn.ReadOnly = true;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "詳細品目";
                textColumn.Width = 350;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "備考";
                textColumn.Width = 350;
                //textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "種別";
                textColumn.Width = 75;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "単位";
                textColumn.Width = 75;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "在庫数";
                textColumn.Width = 95;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "依頼数";
                textColumn.Width = 95;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns.Add(textColumn);

            }
            #endregion

            int rowNo = 0;
            foreach (OdThiShousaiItem row in shousaiItemList)
            {
                if (row.CancelFlag == 1)
                    continue;

                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[9];

                rowDatas[colNo] = row;
                colNo++;
                rowDatas[colNo] = (rowNo+1);
                colNo++;
                rowDatas[colNo] = row.CategoryName;
                colNo++;
                rowDatas[colNo] = row.ShousaiItemName;
                colNo++;
                rowDatas[colNo] = row.Bikou;
                colNo++;
                rowDatas[colNo] = row.SpecificFlag == 1 ? "特定" : (row.MsVesselItemID != null && row.MsVesselItemID != string.Empty) ? "ﾘｽﾄ" : "追加";
                colNo++;
                rowDatas[colNo] = row.MsTaniName;
                colNo++;

                // 在庫数
                rowDatas[colNo] = row.ZaikoCount;
                colNo++;

                // 依頼数
                rowDatas[colNo] = row.Count;
                colNo++;


                dataGridView.Rows.Add(rowDatas);


                if (row.SpecificFlag == 1)
                {
                    dataGridView[7, rowNo].ReadOnly = true;
                }

                rowNo++;

                #endregion
            }

            // 選択行はなしにしておく
            dataGridView.CurrentCell = null;
        }
        #endregion



        internal void 詳細品目選択(object sender, EventArgs e)
        {
            string value = singleLineComboEx1.Text;

            if (MsVesselItemVesselDic.ContainsKey(value))
            {
                MsVesselItemVessel viv = MsVesselItemVesselDic[value];

                var vic = vesselItemCategorys.Where(obj => obj.CategoryNumber == viv.CategoryNumber);

                OdThiShousaiItem shousaiItem = CreateShousaiItem(vic.First(), viv);

                Add(dataGridView1, shousaiItemListFromMaster, shousaiItem);

                singleLineComboEx1.Text = "";
            }
        }


        private void button追加_Click(object sender, EventArgs e)
        {
            船用品追加Form form = new 船用品追加Form();
            if (form.ShowDialog() == DialogResult.OK)
            {
                OdThiShousaiItem shousaiItem = form.ShousaiItem;

                Add(dataGridView2, shousaiItemListForInput, shousaiItem);
            }
        }

        private void button複写_Click(object sender, EventArgs e)
        {
            Copy(dataGridView1, shousaiItemListFromMaster);
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            Remove(dataGridView1, shousaiItemListFromMaster);
        }


        private void button複写_追加詳細_Click(object sender, EventArgs e)
        {
            Copy(dataGridView2, shousaiItemListForInput);
        }

        private void button削除_追加詳細_Click(object sender, EventArgs e)
        {
            Remove(dataGridView2, shousaiItemListForInput);
        }


        private void Add(DataGridView gridView, List<OdThiShousaiItem> shousaiItemList, OdThiShousaiItem shousaiItem)
        {
            shousaiItemList.Add(shousaiItem);

            SetRows(gridView, shousaiItemList);

            if (gridView.Rows.Count > 0)
            {
                // グリッドの一番上の行
                gridView.FirstDisplayedCell = gridView[1, gridView.Rows.Count - 1];

                // 選択
                gridView.CurrentCell = gridView.Rows[gridView.Rows.Count - 1].Cells[1];
            }
        }
            
        private void Copy(DataGridView gridView, List<OdThiShousaiItem> shousaiItemList)
        {
            if (gridView.SelectedRows.Count == 0)
            {
                return;
            }

            // 選択行の詳細情報をコピーして、一覧再表示
            OdThiShousaiItem org = gridView.SelectedRows[0].Cells[0].Value as OdThiShousaiItem;
            OdThiShousaiItem shousaiItem = CopyShousaiItem(org);

            shousaiItemList.Add(shousaiItem);

            SetRows(gridView, shousaiItemList);


            if (gridView.Rows.Count > 0)
            {
                // グリッドの一番上の行
                gridView.FirstDisplayedCell = gridView[1, gridView.Rows.Count - 1];

                // 選択
                gridView.CurrentCell = gridView.Rows[gridView.Rows.Count - 1].Cells[1];
            }
        }

        private void Remove(DataGridView gridView, List<OdThiShousaiItem> shousaiItemList)
        {
            if (gridView.SelectedRows.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("選択されている詳細品目を削除してよろしいですか？", "削除の確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // グリッドの一番上の行
            //int firstRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int firstRowIndex = gridView.FirstDisplayedScrollingRowIndex;

            // 現在の選択
            //DataGridViewCell currentCell = dataGridView1.CurrentCell;
            DataGridViewCell currentCell = gridView.CurrentCell;
            int selectRowIndex = currentCell.RowIndex;

            // 選択行の詳細情報
            OdThiShousaiItem sel = gridView.SelectedRows[0].Cells[0].Value as OdThiShousaiItem;

            // 対象データのCancelFlagを立てて、一覧再表示
            if (shousaiItemList.Any(obj => obj.OdThiShousaiItemID == sel.OdThiShousaiItemID))
            {
                var item = shousaiItemList.Where(obj => obj.OdThiShousaiItemID == sel.OdThiShousaiItemID);
                item.First().CancelFlag = 1;

                SetRows(gridView, shousaiItemList);
            }


            if (gridView.Rows.Count > 0)
            {
                // グリッドの一番上の行
                if (gridView.Rows.Count <= firstRowIndex)
                {
                    gridView.FirstDisplayedCell = gridView[1, gridView.Rows.Count - 1];
                }
                else
                {
                    gridView.FirstDisplayedCell = gridView[1, firstRowIndex];
                }

                // 選択
                if (gridView.Rows.Count <= selectRowIndex)
                {
                    gridView.CurrentCell = gridView.Rows[gridView.Rows.Count - 1].Cells[1];
                }
                else
                {
                    gridView.CurrentCell = gridView.Rows[selectRowIndex].Cells[1];
                }
            }
        }




        private void radioButton_甲板部特定品_Click(object sender, EventArgs e)
        {
            if (radioButton_甲板部特定品.Checked)
                return;

            if (shousaiItemListFromMaster.Count != 0)
            {
                if (MessageBox.Show("特定品をクリックすると、現在登録されている詳細品目を置き換えます。" + System.Environment.NewLine + "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            radioButton_甲板部特定品.Checked = true;
            radioButton_機関部特定品.Checked = false;

            var vesselItemVesselList = vesselItemVessels.Where(obj => obj.CategoryNumber == (int)MsVesselItemCategory.MsVesselItemCategoryEnum.甲板部特定品);
            if (vesselItemVesselList.Count() == 0)
            {
                MessageBox.Show("甲板部特定品が設定されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            セット特定品(vesselItemVesselList.ToList());
        }

        private void radioButton_機関部特定品_Click(object sender, EventArgs e)
        {
            if (radioButton_機関部特定品.Checked)
                return;

            if (shousaiItemListFromMaster.Count != 0)
            {
                if (MessageBox.Show("特定品をクリックすると、現在登録されている詳細品目を置き換えます。" + System.Environment.NewLine + "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            radioButton_機関部特定品.Checked = true;
            radioButton_甲板部特定品.Checked = false;

            var vesselItemVesselList = vesselItemVessels.Where(obj => obj.CategoryNumber == (int)MsVesselItemCategory.MsVesselItemCategoryEnum.機関部特定品);
            if (vesselItemVesselList.Count() == 0)
            {
                MessageBox.Show("機関部特定品が設定されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            セット特定品(vesselItemVesselList.ToList());

        }

        private void セット特定品(List<MsVesselItemVessel> vesselItemVesselList)
        {
            shousaiItemListFromMaster.Clear();

            foreach (MsVesselItemVessel viv in vesselItemVesselList)
            {
                var vic = vesselItemCategorys.Where(obj => obj.CategoryNumber == viv.CategoryNumber);

                OdThiShousaiItem shousaiItem = CreateShousaiItem(vic.First(), viv);

                shousaiItemListFromMaster.Add(shousaiItem);

            }

            SetRows(dataGridView1, shousaiItemListFromMaster);
        }




        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //1回のクリックでエディットモードにする処置 
            DataGridView gridView = sender as DataGridView;

            if (e.ColumnIndex == 4 || e.ColumnIndex > 6)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");

                    gridView.BeginEdit(true);
                }
            }
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //1回のクリックでエディットモードにする処置 
            DataGridView gridView = sender as DataGridView;

            if (e.ColumnIndex == 4 || e.ColumnIndex > 6)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");

                    gridView.BeginEdit(true);
                }
            }
        }

        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            ////エラーテキストを消す
            //dgv.Rows[e.RowIndex].ErrorText = null;
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            string inputStrValue = "";
            int inputIntValue = 0;

            if (e.ColumnIndex == 4)
            {
                inputStrValue = e.FormattedValue.ToString();
                if (inputStrValue.Length > 500)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "備考は500文字までで入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                if (e.FormattedValue.ToString() != "" && int.TryParse(e.FormattedValue.ToString(), out inputIntValue) == false)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "数値を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
                if (inputIntValue > 999)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "半角数字３桁を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
            }

            if (e.Cancel == false)
            {
                OdThiShousaiItem rowShousai = dgv.Rows[e.RowIndex].Cells[0].Value as OdThiShousaiItem;
                List<OdThiShousaiItem> shousaiItemList = null;
                if ((DataGridView)sender == dataGridView1)
                {
                    shousaiItemList = shousaiItemListFromMaster;
                }
                else if ((DataGridView)sender == dataGridView1)
                {
                    shousaiItemList = shousaiItemListFromMaster;
                }

                var saveShousai = shousaiItemListFromMaster.Where(obj => obj.OdThiShousaiItemID == rowShousai.OdThiShousaiItemID);
                if (e.ColumnIndex == 4)
                {
                    rowShousai.Bikou = inputStrValue;
                    saveShousai.First().Bikou = rowShousai.Bikou;
                }
                else if (e.ColumnIndex == 7)
                {
                    rowShousai.ZaikoCount = inputIntValue;
                    saveShousai.First().ZaikoCount = rowShousai.ZaikoCount;
                }
                else if (e.ColumnIndex == 8)
                {
                    rowShousai.Count = inputIntValue;
                    saveShousai.First().Count = rowShousai.Count;
                }
            }
        }


        private OdThiShousaiItem CopyShousaiItem(OdThiShousaiItem src)
        {
            OdThiShousaiItem shousaiItem = new OdThiShousaiItem();

            shousaiItem.OdThiShousaiItemID = "";
            shousaiItem.ShousaiItemName = "";
            shousaiItem.MsTaniID = "";
            shousaiItem.MsTaniName = "";
            shousaiItem.ZaikoCount = int.MinValue;
            shousaiItem.Count = int.MinValue;
            shousaiItem.Sateisu = int.MinValue;
            shousaiItem.Bikou = "";
            shousaiItem.OdAttachFileID = null;
            shousaiItem.OdAttachFileName = null;

            shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
            shousaiItem.CategoryNumber = src.CategoryNumber;
            shousaiItem.CategoryName = src.CategoryName;
            shousaiItem.MsVesselItemID = src.MsVesselItemID;
            shousaiItem.MsVesselItemName = src.MsVesselItemName;
            shousaiItem.ShousaiItemName = src.ShousaiItemName;
            shousaiItem.MsTaniID = src.MsTaniID;
            shousaiItem.MsTaniName = src.MsTaniName;
            shousaiItem.Bikou = src.Bikou;
            shousaiItem.SpecificFlag = src.SpecificFlag;

            shousaiItem.ZaikoCount = src.ZaikoCount;
            shousaiItem.Count = src.Count;

            return shousaiItem;
        }

        private OdThiShousaiItem CreateShousaiItem(MsVesselItemCategory category, MsVesselItemVessel vesselItemVessel)
        {
            OdThiShousaiItem shousaiItem = new OdThiShousaiItem();

            shousaiItem.OdThiShousaiItemID = "";
            shousaiItem.ShousaiItemName = "";
            shousaiItem.MsTaniID = "";
            shousaiItem.MsTaniName = "";
            shousaiItem.ZaikoCount = int.MinValue;
            shousaiItem.Count = int.MinValue;
            shousaiItem.Sateisu = int.MinValue;
            shousaiItem.Bikou = "";
            shousaiItem.OdAttachFileID = null;
            shousaiItem.OdAttachFileName = null;

            shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
            shousaiItem.CategoryNumber = category.CategoryNumber;
            shousaiItem.CategoryName = category.CategoryName;
            shousaiItem.MsVesselItemID = vesselItemVessel.MsVesselItemID;
            shousaiItem.MsVesselItemName = vesselItemVessel.VesselItemName;
            shousaiItem.ShousaiItemName = vesselItemVessel.VesselItemName;
            shousaiItem.MsTaniID = vesselItemVessel.MsTaniID;
            shousaiItem.MsTaniName = vesselItemVessel.MsTaniName;
            shousaiItem.Bikou = vesselItemVessel.Bikou;
            shousaiItem.SpecificFlag = vesselItemVessel.SpecificFlag;

            int zaikoCount = 0;
            if (vesselItemVessel.SpecificFlag == 1)
            {
                var chozoShousai = odChozoShousais.Where(o => o.MsVesselItemID == vesselItemVessel.MsVesselItemID);
                if (chozoShousai.Count() > 0)
                {
                    zaikoCount = (chozoShousai.First()).Count;
                }
            }
            shousaiItem.ZaikoCount = zaikoCount;
            shousaiItem.Count = 0;

            return shousaiItem;
        }



    }
}
