using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon;
using NBaseData.DS;

namespace NBaseCommon
{
    public partial class DocumentGroupCheckBox : UserControl
    {
        private Hashtable ItemHash;

        public List<CheckItem> Items;
        public List<船選択Form.ListItem> 船List;
        public List<int> vesselIds = new List<int>();
        public const int 船Pos = 0;
        public const int 管理責任者Pos = 2;
        private static bool callbackFlag = true;

        private List<DocumentGroupPanel> panels;
        private List<CheckBox> checkBoxes;

        public override string Text
        {
            set { groupBox1.Text = value; }
            get { return groupBox1.Text; }
        }

        public List<船選択Form.ListItem> Set船List
        {
            set
            {
                船List = value;
                選択されている船をテキスト表示();
            }
        }
        public void Check(bool check)
        {
            foreach (CheckItem item in Items)
            {
                item.Checked = check;
            }
        }
        public void Check船(bool check)
        {
            foreach (CheckItem item in Items)
            {
                if (item.Item.enumClass == DocConstants.ClassEnum.VESSEL)
                {
                    item.Checked = check;
                    break;
                }
            }
        }

        public void Check要員(int role, bool check)
        {
            foreach (CheckItem item in Items)
            {
                if ((int)item.Item.enumRole == role)
                {
                    item.Checked = check;
                    break;
                }
            }
        }
        public void Check部門(string bumonId, bool check)
        {
            foreach (CheckItem item in Items)
            {
                if (item.Item.enumClass == DocConstants.ClassEnum.BUMON && item.Item.bumonId == bumonId)
                {
                    item.Checked = check;
                    break;
                }
            }
        }

        public bool IsChecked管理責任者()
        {
            bool ret = false;
            foreach (CheckItem item in Items)
            {
                if (item.Item.enumRole == DocConstants.RoleEnum.管理責任者)
                {
                    ret = item.Checked;
                    break;
                }
            }
            return ret;
        }
        public bool IsCheckedOnly管理責任者()
        {
            if (IsChecked管理責任者() == false)
            {
                // 管理責任者がチェックされていない
                return false;
            }

            bool isChecked = false;

            // 管理責任者以外のチェック確認
            foreach (CheckItem item in Items)
            {
                if (item.Item.enumRole != DocConstants.RoleEnum.管理責任者 && item.Checked)
                {
                    isChecked = true;
                    break;
                }
            }

            if (isChecked)
            {
                // 管理責任者以外にチェックがされている
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetHonsenSize()
        {
            tableLayoutPanel1.Width -= 8;
        }


        public override void Refresh()
        {
            base.Refresh();

            foreach (CheckBox cb in ItemHash.Keys)
            {
                cb.Checked = (ItemHash[cb] as CheckItem).Checked;
            }

            選択されている船をテキスト表示();
        }

        public DocumentGroupCheckBox()
        {
            InitializeComponent();

            panels = new List<DocumentGroupPanel>();
            panels.Add(documentGroupPanel1);
            panels.Add(documentGroupPanel2);
            panels.Add(documentGroupPanel3);
            panels.Add(documentGroupPanel4);
            panels.Add(documentGroupPanel5);
            panels.Add(documentGroupPanel6);
            panels.Add(documentGroupPanel7);
            panels.Add(documentGroupPanel8);
            panels.Add(documentGroupPanel9);
            panels.Add(documentGroupPanel10);
            //panels.Add(documentGroupPanel11);
            //panels.Add(documentGroupPanel12);
            //panels.Add(documentGroupPanel13);
            //panels.Add(documentGroupPanel14);
            //panels.Add(documentGroupPanel15);

            checkBoxes = new List<CheckBox>();
            checkBoxes.Add(checkBox1);
            checkBoxes.Add(checkBox2);
            checkBoxes.Add(checkBox3);
            checkBoxes.Add(checkBox4);
            checkBoxes.Add(checkBox5);
            checkBoxes.Add(checkBox6);
            checkBoxes.Add(checkBox7);
            checkBoxes.Add(checkBox8);
            checkBoxes.Add(checkBox9);
            checkBoxes.Add(checkBox10);
            //checkBoxes.Add(checkBox11);
            //checkBoxes.Add(checkBox12);
            //checkBoxes.Add(checkBox13);
            //checkBoxes.Add(checkBox14);
            //checkBoxes.Add(checkBox15);


            ItemHash = new Hashtable();
            Items = new List<CheckItem>();

            int index = 0;
            var classItems = DocConstants.ClassItemList();
            DocConstants.ClassItem itemVessel = new DocConstants.ClassItem(DocConstants.ClassEnum.VESSEL, DocConstants.RoleEnum.船, null, "本船", "本船");
            AddItem(panels[index], checkBoxes[index], itemVessel, true);
            index++;

            foreach (DocConstants.ClassItem item in classItems)
            {
                AddItem(panels[index], checkBoxes[index], item, true);
                index++;
            }

            Refresh();
        }

        private void AddItem(DocumentGroupPanel p, CheckBox cb, DocConstants.ClassItem cItem, bool check)
        {
            if (cItem.viewName2 != null)
            {
                p.Set項目名(cItem.viewName1, cItem.viewName2);
            }
            else
            {
                p.Set項目名(cItem.viewName1);
            }
            CheckItem item = new CheckItem(cItem, check);
            Items.Add(item);
            ItemHash.Add(cb, item);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (callbackFlag == false)
            {
                callbackFlag = true;
                return;
            }
            try
            {
                CheckBox cb = sender as CheckBox;
                CheckItem item = ItemHash[cb] as CheckItem;
                if (item.ReadOnly == false)
                {
                    item.Checked = cb.Checked;
                    return;
                }

                // ReadOnly指定の場合
                callbackFlag = false;
                if (cb.Checked)
                {
                    cb.Checked = false;
                }
                else
                {
                    cb.Checked = true;
                }
            }
            catch
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox) == checkBox1)
            {
                if (checkBox1.Checked == true)
                {
                    button_船選択.Enabled = true;
                    選択されている船をテキスト表示();

                    Items[船Pos].Checked = true;
                }
                else
                {
                    button_船選択.Enabled = false;
                    textBox_船選択.Text = "";

                    Items[船Pos].Checked = false;
                }
            }
        }

        private void button_船選択_Click(object sender, EventArgs e)
        {
            船選択Form form = new 船選択Form(船List);
            if (form.ShowDialog() == DialogResult.OK)
            {
                船List = form.GetCheckedList();
                vesselIds = form.CheckedVesselIds();
                List<string> vesselNames = form.CheckedVesselNames();

                選択されている船をテキスト表示();
            }
        }

        private void 選択されている船をテキスト表示()
        {
            if (船List == null)
            {
                return;
            }

            string 選択船 = "";
            foreach (船選択Form.ListItem listItem in 船List)
            {
                if (listItem.Checked)
                {
                    vesselIds.Add(listItem.Value);
                    選択船 += " , " + listItem.Text;
                }
            }
            textBox_船選択.Text = "";
            if (選択船.Length > 0)
            {
                textBox_船選択.Text = 選択船.Substring(3);
            }
        }


        public List<NBaseData.DAC.DmPublisher> ConvertPublisherList(NBaseData.DS.DocConstants.LinkSakiEnum linkSaki)
        {
            List<NBaseData.DAC.DmPublisher> retList = new List<NBaseData.DAC.DmPublisher>();

            NBaseData.DAC.DmPublisher publisher = null;
            foreach (CheckBox cb in ItemHash.Keys)
            {
                if (cb.Checked)
                {
                    CheckItem item = ItemHash[cb] as CheckItem;

                    if (item.Item.enumClass == DocConstants.ClassEnum.VESSEL)
                    {
                        foreach (船選択Form.ListItem litem in 船List)
                        {
                            if (litem.Checked)
                            {
                                publisher = new NBaseData.DAC.DmPublisher();
                                publisher.LinkSaki = (int)linkSaki;
                                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                                publisher.MsVesselID = litem.Value;

                                retList.Add(publisher);
                            }
                        }
                    }
                    else if (item.Item.enumClass == DocConstants.ClassEnum.USER)
                    {
                        publisher = new NBaseData.DAC.DmPublisher();
                        publisher.LinkSaki = (int)linkSaki;
                        publisher.KoukaiSaki = (int)item.Item.enumRole;
                        retList.Add(publisher);
                    }
                    else
                    {
                        publisher = new NBaseData.DAC.DmPublisher();
                        publisher.LinkSaki = (int)linkSaki;
                        publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.部門;
                        publisher.MsBumonID = item.Item.bumonId;
                        retList.Add(publisher);
                    }

                }
            }

            return retList;
        }
        public List<NBaseData.DAC.DmKoukaiSaki> ConvertKoukaiSakiList(NBaseData.DS.DocConstants.LinkSakiEnum linkSaki)
        {
            List<NBaseData.DAC.DmKoukaiSaki> retList = new List<NBaseData.DAC.DmKoukaiSaki>();

            NBaseData.DAC.DmKoukaiSaki koukaiSaki = null;
            foreach (CheckBox cb in ItemHash.Keys)
            {
                if (cb.Checked)
                {
                    CheckItem item = ItemHash[cb] as CheckItem;

                    if (item.Item.enumClass == DocConstants.ClassEnum.VESSEL)
                    {
                        foreach (船選択Form.ListItem litem in 船List)
                        {
                            if (litem.Checked)
                            {
                                koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                                koukaiSaki.LinkSaki = (int)linkSaki;
                                koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                                koukaiSaki.MsVesselID = litem.Value;

                                retList.Add(koukaiSaki);
                            }
                        }
                    }
                    else if (item.Item.enumClass == DocConstants.ClassEnum.USER)
                    {
                        koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                        koukaiSaki.LinkSaki = (int)linkSaki;
                        koukaiSaki.KoukaiSaki = (int)item.Item.enumRole;
                        retList.Add(koukaiSaki);
                    }
                    else
                    {
                        koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                        koukaiSaki.LinkSaki = (int)linkSaki;
                        koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.部門;
                        koukaiSaki.MsBumonID = item.Item.bumonId;
                        retList.Add(koukaiSaki);
                    }

                }
            }

            return retList;
        }

        public class CheckItem
        {
            public DocConstants.ClassItem Item;
            public bool Checked;
            public bool ReadOnly = false;

            public CheckItem(DocConstants.ClassItem item, bool check)
            {
                Item = item;
                Checked = check;
            }

        }
    }
}
