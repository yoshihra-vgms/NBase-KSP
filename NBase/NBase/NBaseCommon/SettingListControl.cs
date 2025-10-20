using NBaseData.DAC;
using NBaseData.DS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBaseCommon
{
    public partial class SettingListControl : UserControl
    {

        public delegate void ClickEventHandler(object id);
        public event ClickEventHandler ClickEvent;

        public List<UserListItems> SelectedUserListItemsList;
        public List<SettingListRowInfo> RowInfoList;

        public DataGridViewColumnSortMode SortMode = DataGridViewColumnSortMode.Automatic;

        private bool ListDrawing = false;

        public SettingListControl()
        {
            InitializeComponent();
        }




        public void SetFontSize( int size )
        {
            Font f = dataGridView.DefaultCellStyle.Font;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(f.FontFamily, size, f.Style);
            dataGridView.DefaultCellStyle.Font = new Font(f.FontFamily, size, f.Style);
        }

        public bool RowHeaderVisible
        {
            set 
            {
                dataGridView.RowHeadersVisible = value;
            }
        }

        public bool AllowUserToResizeColumns
        {
            set
            {
                dataGridView.AllowUserToResizeColumns = value;
            }
        }



        public void DrawList()
        {
            ListDrawing = true;

            try
            {
                // 一覧をクリア
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();

                // カラムの設定
                var sortedListItemList = SelectedUserListItemsList.OrderBy(o => o.DisplayOrder);
                foreach (UserListItems item in sortedListItemList)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = item.ListItem.DispName;
                    column.HeaderText = item.ListItem.DispName; ;
                    column.DataPropertyName = item.ListItem.ClassName + "_" + item.ListItem.PropertyName;

                    column.Visible = (item.ListItem.IsVisible == 1);

                    column.Width = item.ListItem.Width;

                    if (item.ListItem.Alignment == "right")
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    }
                    else if (item.ListItem.Alignment == "center")
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                    }
                    else
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    }

                    column.SortMode = SortMode;

                    dataGridView.Columns.Add(column);
                }

                // データ
                if (RowInfoList == null || RowInfoList.Count() == 0)
                    return;

                foreach (SettingListRowInfo data in RowInfoList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView);

                    SetRow(row, data);

                    dataGridView.Rows.Add(row);

                    if (data.normalColor != null)
                    {
                        dataGridView.Rows[dataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = data.normalColor;
                        dataGridView.Rows[dataGridView.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = data.selectedColor;
                    }
                }

                dataGridView.ClearSelection();
            }
            finally
            {
                ListDrawing = false;
            }

        }

        public void OutputList(string filePath)
        {
            try
            {

                int row = dataGridView.RowCount;
                int colunms = dataGridView.ColumnCount;

                StringBuilder header = new StringBuilder();
                for (int k = 0; k < colunms; k++)
                {
                    if (dataGridView.Columns[k].Visible)
                    {
                        header.Append(dataGridView.Columns[k].HeaderCell.Value.ToString());
                        header.Append(",");
                    }
                }
                StringBuilder data = new StringBuilder();
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < colunms; j++)
                    {
                        if (dataGridView.Columns[j].Visible)
                        {
                            data.Append((dataGridView[j, i].Value == null ? "" : dataGridView[j, i].Value.ToString()));
                            data.Append(",");
                        }
                    }
                    data.Append(System.Environment.NewLine);
                }


                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
                sw.WriteLine(header);
                sw.Write(data.ToString());
                sw.Close();
                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ファイルの出力に失敗しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void ClearSelection()
        {
            dataGridView.ClearSelection();
        }

        public void Select(SettingListRowInfo data)
        {
            int index = RowInfoList.IndexOf(data);
            if (index > 0)
            {
                dataGridView.Rows[index].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = index;
            }
        }


        public void UpdateSelectedRow(SettingListRowInfo data)
        {
            if (dataGridView.SelectedRows != null)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];

                foreach(DataGridViewCell cell in row.Cells)
                    cell.Value = null;

                SetRow(row, data);

                if (data.normalColor != null)
                {
                    dataGridView.Rows[dataGridView.SelectedRows[0].Index].DefaultCellStyle.BackColor = data.normalColor;
                    dataGridView.Rows[dataGridView.SelectedRows[0].Index].DefaultCellStyle.SelectionBackColor = data.selectedColor;
                }
            }
        }

        public void RemoveSelectedRow()
        {
            if (dataGridView.SelectedRows != null)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    dataGridView.Rows.Remove(row);
            }
        }

        public SettingListRowInfo GetSelectedInfo()
        {
            SettingListRowInfo info = null;
            if (dataGridView.SelectedRows != null && dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;

                info = RowInfoList[index];
            }
            return info;
        }

        public void MoveScrollbarLast()
        {
            if (dataGridView.Rows.Count > 1)
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
        }

        public int RowCount
        {
            get
            {
                return dataGridView.Rows.Count;
            }
        }

        public void ClearSelect()
        {
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.ClearSelection();
            }
        }






        private void SetRow(DataGridViewRow row, SettingListRowInfo data)
        {
            var sortedListItemList = SelectedUserListItemsList.OrderBy(o => o.DisplayOrder);

            var dataProperties = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            int colNo = 0;
            foreach (UserListItems item in sortedListItemList)
            {
                foreach (PropertyInfo property in dataProperties)
                {
                    if (property.Name == item.ListItem.ClassName)
                    {
                        var classObject = property.GetValue(data);
                        if (classObject == null)
                            continue;

                        var classObjectProperties = classObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                        foreach (PropertyInfo classProperty in classObjectProperties)
                        {
                            if (classProperty.Name == item.ListItem.PropertyName)
                            {
                                var obj = classProperty.GetValue(classObject);
                                if (obj != null)
                                {
                                    object valObj = null;

                                    if (item.ListItem.MasterName.Length > 0)
                                    {
                                        valObj = GetValueObject(obj, item.ListItem.MasterName, item.ListItem.MasterPropertyName);
                                    }
                                    else
                                    {
                                        valObj = obj;
                                    }

                                    {
                                        if (valObj != null)
                                        {
                                            if (valObj.GetType() == typeof(int))
                                            {
                                                if (((int)valObj) != Common.EVal && ((int)valObj) != int.MinValue)
                                                {
                                                    row.Cells[colNo].Value = ((int)valObj).ToString();
                                                }
                                            }
                                            if (valObj.GetType() == typeof(decimal) && ((decimal)valObj) != int.MinValue)
                                            {
                                                if (((decimal)valObj) != Common.EVal)
                                                    row.Cells[colNo].Value = ((decimal)valObj).ToString();
                                            }
                                            if (valObj.GetType() == typeof(DateTime))
                                            {
                                                if (((DateTime)valObj) != DateTime.MinValue)
                                                    row.Cells[colNo].Value = ((DateTime)valObj).ToShortDateString();
                                            }
                                            else if (valObj.GetType() == typeof(string))
                                            {
                                                row.Cells[colNo].Value = valObj;
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        }
                    }

                }
                colNo += 1;
            }
        }


        private Object GetValueObject(Object id, string masterName, string proretyName)
        {
            object mstObj = null;
            object valObj = null;


            if (masterName == "MsSiShubetsu")
            {
                mstObj = SeninTableCache.instance().GetMsSiShubetsu(NBaseCommon.Common.LoginUser, (int)id);
            }
            else if (masterName == "MsSiShokumei")
            {
                mstObj = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, (int)id);
            }
            else if (masterName == "MsVessel")
            {
                mstObj = SeninTableCache.instance().GetMsVessel(NBaseCommon.Common.LoginUser, (int)id);
            }
            else if (masterName == "MsSeninCompany")
            {
                mstObj = SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser).Where(o => o.MsSeninCompanyID == (string)id).FirstOrDefault();
            }
            else if (masterName == "MsSiOptions")
            {
                mstObj = SeninTableCache.instance().GetMsSiOptions(NBaseCommon.Common.LoginUser, (string)id);
            }
            else if (masterName == "MsSiMenjou")
            {
                mstObj = SeninTableCache.instance().GetMsSiMenjou(NBaseCommon.Common.LoginUser, (int)id);
            }
            else if (masterName == "MsSiMenjouKind")
            {
                mstObj = SeninTableCache.instance().GetMsSiMenjouKind(NBaseCommon.Common.LoginUser, (int)id);
            }


            if (mstObj != null)
            {
                var mstProperties = mstObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo mstProperty in mstProperties)
                {
                    if (mstProperty.Name == proretyName)
                    {
                        valObj = mstProperty.GetValue(mstObj);
                    }
                }
            }

            return valObj;
        }


        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }




        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            selectRow();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ListDrawing)
                return;

            selectRow();
        }

        private void selectRow()
        {
            if (ClickEvent == null)
                return;

            if (dataGridView.SelectedRows == null || dataGridView.SelectedRows.Count == 0)
                return;

            DataGridViewRow obj = dataGridView.SelectedRows[0];

            ClickEvent(obj.Cells[0].Value);
        }

    }
}
