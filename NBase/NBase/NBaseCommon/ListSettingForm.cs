using NBaseData.DAC;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBaseCommon
{
    public partial class ListSettingForm : Form
    {
        public delegate void SelectEventHandler(string title);
        public event SelectEventHandler SelectEvent = null;

        public delegate bool RegistEventHandler(bool isModify, string title, List<UserListItems> userListItemsList);
        public event RegistEventHandler RegistEvent = null;

        public delegate bool RemoveEventHandler(List<UserListItems> userListItemsList);
        public event RemoveEventHandler RemoveEvent = null;



        public static MsListItem ChoiceItem;

        public string ListItemTitle;
        public List<MsListItem> ListItemList;
        public List<UserListItems> UserListItemsList;
        public List<UserListItems> SelectedUserListItemsList;


        public ListSettingForm()
        {
            InitializeComponent();
        }



        private void ListSettingForm_Load(object sender, EventArgs e)
        {
            if (ListItemTitle == null || ListItemTitle == "")
            {
                ListItemTitle = UserListItemsList.Select(o => o.Title).FirstOrDefault();
            }
            SelectedUserListItemsList = UserListItemsList.Where(o => o.Title == ListItemTitle).OrderBy(o => o.DisplayOrder).ToList();

            InitTitleDDL();
            DrawList();
        }


        private void button選択_Click(object sender, EventArgs e)
        {
            SelectList();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button登録_Click(object sender, EventArgs e)
        {
            string title = comboBoxTitle.Text;
            if (title == Common.DefaultListTitle)
            {
                MessageBox.Show("\"" + Common.DefaultListTitle + "\"" + "には上書き登録できません。別の名前を入力してください。");
                return;
            }

            if (SaveList() == false)
            {
                // 登録エラー


                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            string title = comboBoxTitle.Text;
            if (title == Common.DefaultListTitle)
            {
                MessageBox.Show("\"" + Common.DefaultListTitle + "\"" + "は削除できません。");
                return;
            }

            if (RemoveList() == false)
            {
                // 登録エラー


                return;
            }
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void buttonCoice_Click(object sender, EventArgs e)
        {
            if ((listBox_Item.SelectedItem is MsListItem) == false)
                return;

            ChoiceItem = (listBox_Item.SelectedItem as MsListItem);

            Choice();
            DrawList();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if ((listBox_Selected.SelectedItem is MsListItem) == false)
                return;

            ChoiceItem = (listBox_Selected.SelectedItem as MsListItem);

            Remove();
            DrawList();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if ((listBox_Selected.SelectedItem is MsListItem) == false)
                return;

            ChoiceItem = (listBox_Selected.SelectedItem as MsListItem);

            Up();
            DrawList();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if ((listBox_Selected.SelectedItem is MsListItem) == false)
                return;

            ChoiceItem = (listBox_Selected.SelectedItem as MsListItem);

            Down();
            DrawList();
        }

        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItemTitle = comboBoxTitle.Text;
            SelectedUserListItemsList = UserListItemsList.Where(o => o.Title == ListItemTitle).OrderBy(o => o.DisplayOrder).ToList();

            ChoiceItem = null;
            DrawList();
        }





        public void Init(string listItemTitle, List<MsListItem> listItemList, List<UserListItems> userListItemsList)
        {
            this.ListItemTitle = listItemTitle;

            this.ListItemList = new List<MsListItem>();
            this.ListItemList.AddRange(listItemList);

            this.UserListItemsList = new List<UserListItems>();
            this.UserListItemsList.AddRange(userListItemsList);

            if (this.Visible)
            {
                InitTitleDDL();
                DrawList();
            }
        }


        /// <summary>
        /// 表示セット名のコンボボックスの初期化
        /// </summary>
        #region private void InitTitleDDL()
        private void InitTitleDDL()
        {
            comboBoxTitle.Items.Clear();
            var titles = UserListItemsList.Select(o => o.Title).Distinct();
            foreach(var t in titles)
            {
                comboBoxTitle.Items.Add(t);
                if (t == ListItemTitle)
                    comboBoxTitle.SelectedItem = t;
            }
        }
        #endregion


        private void LoadData()
        {
        }

        /// <summary>
        /// リスト設定
        /// </summary>
        #region private void DrawList()
        private void DrawList()
        {
            // Default表示でないもののみ一覧に表示する
            listBox_Selected.Items.Clear();
            var selectedList = SelectedUserListItemsList.Where(o => o.ListItem.AlwaysSelected == 0).OrderBy(o => o.DisplayOrder);
            foreach (UserListItems item in selectedList)
            {
                listBox_Selected.Items.Add(item.ListItem);

                if (ChoiceItem != null && ChoiceItem.MsListItemID == item.ListItem.MsListItemID)
                    listBox_Selected.SelectedItem = ChoiceItem;
            }


            // 候補一覧
            listBox_Item.Items.Clear();
            var selected = selectedList.Select(o => o.MsListItemID); // 選択済みの表示項目ID
            var itemList = ListItemList.Where(o => o.AlwaysSelected == 0 && selected.Contains(o.MsListItemID) == false);
            foreach (MsListItem item in itemList)
            {
                listBox_Item.Items.Add(item);

                if (ChoiceItem != null && ChoiceItem.MsListItemID == item.MsListItemID)
                    listBox_Item.SelectedItem = ChoiceItem;
            }
        }
        #endregion



        //==============================================================
        //
        // 画面の「→」「←」「↑」「↓」ボタンの実処理
        //
        //==============================================================
        #region private void Choice()
        private void Choice()
        {
            UserListItems uItem = new UserListItems();

            uItem.UserID = NBaseCommon.Common.LoginUser.MsUserID;
            uItem.MsListItemID = ChoiceItem.MsListItemID;
            uItem.ListItem = ChoiceItem;
            uItem.DisplayOrder = SelectedUserListItemsList.Max(o => o.DisplayOrder) + 1;

            SelectedUserListItemsList.Add(uItem);
        }
        #endregion

        #region private void Remove()
        private void Remove()
        {
            UserListItems uItem = SelectedUserListItemsList.Where(o => o.MsListItemID == ChoiceItem.MsListItemID).FirstOrDefault();

            if (uItem != null)
            {
                SelectedUserListItemsList.Remove(uItem);

                RenumberingDispleyOrder();
            }
        }
        #endregion

        #region private void Up()
        private void Up()
        {
            // 選択されている表示項目の表示順を一つ前にする
            UserListItems uItem = SelectedUserListItemsList.Where(o => o.MsListItemID == ChoiceItem.MsListItemID).FirstOrDefault();

            if (uItem == null)
                return;

            int minOrder = SelectedUserListItemsList.Where(o => o.ListItem.AlwaysSelected == 0).Min(o => o.DisplayOrder);
            if (uItem.DisplayOrder == minOrder)
                return;

            int order = uItem.DisplayOrder;
            UserListItems uItem2 = SelectedUserListItemsList.Where(o => o.DisplayOrder == (order - 1)).FirstOrDefault();

            uItem.DisplayOrder -= 1;
            if (uItem2 != null)
                uItem2.DisplayOrder += 1;

        }
        #endregion

        #region private void Down()
        private void Down()
        {
            // 選択されている表示項目の表示順を一つ後にする
            UserListItems uItem = SelectedUserListItemsList.Where(o => o.MsListItemID == ChoiceItem.MsListItemID).FirstOrDefault();

            if (uItem == null)
                return;

            int maxOrder = SelectedUserListItemsList.Where(o => o.ListItem.AlwaysSelected == 0).Max(o => o.DisplayOrder);
            if (uItem.DisplayOrder == maxOrder)
                return;

            int order = uItem.DisplayOrder;
            UserListItems uItem2 = SelectedUserListItemsList.Where(o => o.DisplayOrder == (order + 1)).FirstOrDefault();

            uItem.DisplayOrder += 1;
            if (uItem2 != null)
                uItem2.DisplayOrder -= 1;
        }
        #endregion



        private void SelectList()
        {
            if (SelectEvent != null)
            {
                string title = comboBoxTitle.Text;

                SelectEvent(title);
            }
        }

        private bool SaveList()
        {



            bool ret = true;
            if (RegistEvent != null)
            {
                RenumberingDispleyOrder();

                string title = comboBoxTitle.Text;
                ret = RegistEvent(ListItemTitle == title, title, SelectedUserListItemsList);

            }
            return ret;
        }

        private bool RemoveList()
        {
            bool ret = true;
            if (RemoveEvent != null)
            {
                ret = RemoveEvent(SelectedUserListItemsList);
            }
            return ret;
        }

        private void RenumberingDispleyOrder()
        {
            int orderNumber = SelectedUserListItemsList.Where(o => o.ListItem.AlwaysSelected == 1).Count() + 1;
            var selectedList = SelectedUserListItemsList.Where(o => o.ListItem.AlwaysSelected == 0).OrderBy(o => o.DisplayOrder);
            foreach (UserListItems item in selectedList)
            {
                item.DisplayOrder = orderNumber;
                orderNumber++;
            }
        }

    }
}
