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
                if (item.Type == CheckItem.typeEnum.船)
                {
                    item.Checked = check;
                    break;
                }
            }
        }
        public void Check会長社長(bool check)
        {
            foreach (CheckItem item in Items)
            {
                if (item.Type == CheckItem.typeEnum.会長社長)
                {
                    item.Checked = check;
                    break;
                }
            }
        }
        public void Check管理責任者(bool check)
        {
            foreach (CheckItem item in Items)
            {
                if (item.Type == CheckItem.typeEnum.管理責任者)
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
                if (item.Type == CheckItem.typeEnum.部門 && item.BumonId == bumonId)
                {
                    item.Checked = check;
                    break;
                }
            }
        }
        //public void ReadOnly安全管理_海務Ｇ()
        public void ReadOnly海務安全Ｇ()
        {
            CheckItem item = ItemHash[checkBox4] as CheckItem;
            item.ReadOnly = true;
        }

        public bool IsChecked管理責任者()
        {
            return checkBox3.Checked;
        }
        public bool IsCheckedOnly管理責任者()
        {
            bool isChecked = false;
            // 管理責任者以外のチェック確認
            if (checkBox1.Checked ||
                checkBox2.Checked ||
                checkBox4.Checked ||
                checkBox5.Checked ||
                checkBox6.Checked ||
                checkBox7.Checked ||
                checkBox8.Checked ||
                checkBox9.Checked)
            {
                isChecked = true;
            }
            if (IsChecked管理責任者() == false)
            {
                // 管理責任者がチェックされていない
                return false;
            }
            else if (isChecked)
            {
                // 管理責任者以外にチェックがされている
                return false;
            }
            else
            {
                return true;
            }
        }


        public override void Refresh()
        {
            base.Refresh();

            checkBox1.Checked = Items[0].Checked;
            checkBox2.Checked = Items[1].Checked;
            checkBox3.Checked = Items[2].Checked;
            checkBox4.Checked = Items[3].Checked;
            checkBox5.Checked = Items[4].Checked;
            checkBox6.Checked = Items[5].Checked;
            checkBox7.Checked = Items[6].Checked;
            checkBox8.Checked = Items[7].Checked;
            checkBox9.Checked = Items[8].Checked;

            選択されている船をテキスト表示();
        }

        public DocumentGroupCheckBox()
        {
            InitializeComponent();

            ItemHash = new Hashtable();
            Items = new List<CheckItem>();

            AddItem(checkBox1, CheckItem.typeEnum.船, null, true);
            AddItem(checkBox2, CheckItem.typeEnum.会長社長, null, true);
            AddItem(checkBox3, CheckItem.typeEnum.管理責任者, null, true);
            //AddItem(checkBox4, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.安全管理・海務G).ToString(), true);
            AddItem(checkBox4, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.海務安全G).ToString(), true);
            AddItem(checkBox5, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.工務G).ToString(), true);
            AddItem(checkBox6, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.船員G).ToString(), true);
            AddItem(checkBox7, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.営業G).ToString(), true);
            AddItem(checkBox8, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.管理G).ToString(), true);
            AddItem(checkBox9, CheckItem.typeEnum.部門, ((int)NBaseData.DAC.MsBumon.MsBumonIdEnum.システム).ToString(), true);

            Refresh();
        }
        private void AddItem(CheckBox cb, CheckItem.typeEnum type, string bumonId, bool check)
        {
            CheckItem item = new CheckItem(type, bumonId, check);
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
                    if (item.Type == CheckItem.typeEnum.船)
                    {
                        foreach (船選択Form.ListItem litem in 船List)
                        {
                            if (litem.Checked)
                            {
                                publisher = new NBaseData.DAC.DmPublisher();
                                publisher.LinkSaki = (int)linkSaki;
                                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.船;
                                publisher.MsVesselID = litem.Value;

                                retList.Add(publisher);
                            }
                        }
                    }
                    else if (item.Type == CheckItem.typeEnum.会長社長)
                    {
                        publisher = new NBaseData.DAC.DmPublisher();
                        publisher.LinkSaki = (int)linkSaki;
                        publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.会長社長;
                        retList.Add(publisher);
                    }
                    else if (item.Type == CheckItem.typeEnum.管理責任者)
                    {
                        publisher = new NBaseData.DAC.DmPublisher();
                        publisher.LinkSaki = (int)linkSaki;
                        publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.管理責任者;
                        retList.Add(publisher);
                    }
                    else
                    {
                        publisher = new NBaseData.DAC.DmPublisher();
                        publisher.LinkSaki = (int)linkSaki;
                        publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.部門;
                        publisher.MsBumonID = item.BumonId;
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
                    if (item.Type == CheckItem.typeEnum.船)
                    {
                        foreach (船選択Form.ListItem litem in 船List)
                        {
                            if (litem.Checked)
                            {
                                koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                                koukaiSaki.LinkSaki = (int)linkSaki;
                                koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.船;
                                koukaiSaki.MsVesselID = litem.Value;

                                retList.Add(koukaiSaki);
                            }
                        }
                    }
                    else if (item.Type == CheckItem.typeEnum.会長社長)
                    {
                        koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                        koukaiSaki.LinkSaki = (int)linkSaki;
                        koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.会長社長;
                        retList.Add(koukaiSaki);
                    }
                    else if (item.Type == CheckItem.typeEnum.管理責任者)
                    {
                        koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                        koukaiSaki.LinkSaki = (int)linkSaki;
                        koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.管理責任者;
                        retList.Add(koukaiSaki);
                    }
                    else
                    {
                        koukaiSaki = new NBaseData.DAC.DmKoukaiSaki();
                        koukaiSaki.LinkSaki = (int)linkSaki;
                        koukaiSaki.KoukaiSaki = (int)NBaseData.DS.DocConstants.KoukaiSakiEnum.部門;
                        koukaiSaki.MsBumonID = item.BumonId;
                        retList.Add(koukaiSaki);
                    }

                }
            }

            return retList;
        }

        public class CheckItem
        {
            public enum typeEnum { 船, 会長社長, 管理責任者, 部門 };
            public typeEnum Type;
            public string BumonId;
            public bool Checked;
            public bool ReadOnly = false;

            public CheckItem(typeEnum type, string bumonId, bool check)
            {
                Type = type;
                BumonId = bumonId;
                Checked = check;
            }
        }
    }
}
