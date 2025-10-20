using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;
using NBaseData.DAC;

namespace Senin.util
{
    public partial class AdvancedConditionItem : UserControl
    {
        //public delegate void SelectedEventHandler(object sender, EventArgs e);
        //public event SelectedEventHandler selected;

        //private int PresentationItemSetNo = -1;
        //public int GetPresentationItemSetNo()
        //{
        //    return PresentationItemSetNo;
        //}

        private Dictionary<int, List<string>> ValueDic;

        public AdvancedConditionItem()
        {
            InitializeComponent();
        }

        private void AdvancedConditionItem_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            if (comboBox_Tab.Items == null || comboBox_Tab.Items.Count == 0)
            {
                List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);

                comboBox_Tab.Items.Add("");
                foreach (MsSiAdvancedSearchCondition obj in conditionAll.Where(obj => obj.Layer == 1))
                {
                    comboBox_Tab.Items.Add(obj);
                }


                ValueDic = new Dictionary<int, List<string>>();
                List<string> valueList;

                valueList = new List<string>();
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_EXISTENCE_EXISTS);
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_EXISTENCE_NOT_EXISTS);
                ValueDic.Add(MsSiAdvancedSearchCondition.ID_INJURIES_EXISTENCE, valueList);

                valueList = new List<string>();
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_LICENSE_EXISTS);
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_LICENSE_NOT_EXISTS);
                ValueDic.Add(MsSiAdvancedSearchCondition.ID_LICENSE_EXISTENCE, valueList);

                valueList = new List<string>();
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_TRAINING_EXISTS);
                valueList.Add(SiAdvancedSearchConditionValue.VALUE_TRAINING_NOT_EXISTS);
                ValueDic.Add(MsSiAdvancedSearchCondition.ID_TRAINING_NAME, valueList);
            }
        }


        #region クリア操作

        public void Clear()
        {
            comboBox_Tab.SelectedIndex = -1;
            ClearItem1Controls();
            ClearItem2Controls();
            ClearItem3Controls();
            ClearValueControls();
        }

        public void ClearItem1Controls()
        {
            comboBox_Item1.Text = "";
            comboBox_Item1.Items.Clear();
            comboBox_Item1.Visible = false;

            checkedComboBox_Item1.Text = "";
            checkedComboBox_Item1.Items.Clear();
            checkedComboBox_Item1.Visible = false;
        }
        public void ClearItem2Controls()
        {
            comboBox_Item2.Text = "";
            comboBox_Item2.Items.Clear();
            comboBox_Item2.Visible = false;

            checkedComboBox_Item2.Text = "";
            checkedComboBox_Item2.Items.Clear();
            checkedComboBox_Item2.Visible = false;
        }

        public void ClearItem3Controls()
        {
            comboBox_Item3.Text = "";
            comboBox_Item3.Items.Clear();
            comboBox_Item3.Visible = false;
        }

        public void ClearValueControls()
        {
            comboBox_Item1.Text = "";
            comboBox_Value.Items.Clear();
            comboBox_Value.Visible = false;

            checkedComboBox_Value.Text = null;
            checkedComboBox_Value.Items.Clear();
            checkedComboBox_Value.Visible = false;

            textBox_Value.Text = "";
            textBox_Value.Visible = false;

            textBox_Value1.Text = "";
            textBox_Value2.Text = "";
            flowLayoutPanel_Value.Visible = false;

            nullableDateTimePicker1.Value = DateTime.Today;
            nullableDateTimePicker2.Value = DateTime.Today;
            nullableDateTimePicker1.Value = null;
            nullableDateTimePicker2.Value = null;
            flowLayoutPanel_Date.Visible = false;
        }

        #endregion

        /// <summary>
        /// 「TAB」の選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Tab_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearItem1Controls();
            ClearItem2Controls();
            ClearItem3Controls();
            ClearValueControls();

            if (!(comboBox_Tab.SelectedItem is MsSiAdvancedSearchCondition))
            {
                Clear();
                //PresentationItemSetNo = -1;
                //selected(this, new EventArgs());
            }
            else
            {
                MsSiAdvancedSearchCondition tab = comboBox_Tab.SelectedItem as MsSiAdvancedSearchCondition;
                List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);

                if (tab.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    comboBox_Item1.Items.Clear();
                    foreach (MsSiAdvancedSearchCondition obj in conditionAll.Where(obj =>(obj.Layer == 2 && obj.ParentID == tab.MsSiAdvancedSearchConditionID)))
                    {
                        comboBox_Item1.Items.Add(obj);
                    }
                    comboBox_Item1.Visible = true;

                    if (comboBox_Item1.Items.Count == 1)
                    {
                        comboBox_Item1.SelectedIndex = 0;
                    }
                    //else
                    //{
                    //    PresentationItemSetNo = -1;
                    //    selected(this, new EventArgs());
                    //}
                }
                else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE)
                {
                    checkedComboBox_Item1.SelectedItem = null;
                    checkedComboBox_Item1.Text = null;
                    checkedComboBox_Item1.Items.Clear();
                    foreach (MsSiMenjou o in SeninTableCache.instance().GetMsSiMenjouList(NBaseCommon.Common.LoginUser))
                    {
                        checkedComboBox_Item1.Items.Add(o);
                    }
                    checkedComboBox_Item1.Visible = true;

                    //PresentationItemSetNo = -1;
                    //selected(this, new EventArgs());
                }
                else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO)
                {
                    checkedComboBox_Item1.SelectedItem = null;
                    checkedComboBox_Item1.Text = null;
                    checkedComboBox_Item1.Items.Clear();
                    foreach (MsCargoGroup o in SeninTableCache.instance().GetMsCargoGroupList(NBaseCommon.Common.LoginUser))
                    {
                        checkedComboBox_Item1.Items.Add(o);
                    }
                    checkedComboBox_Item1.Visible = true;

                    //PresentationItemSetNo = -1;
                    //selected(this, new EventArgs());
                }
                else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_TRAINING)
                {
                    checkedComboBox_Item1.SelectedItem = null;
                    checkedComboBox_Item1.Text = null;
                    checkedComboBox_Item1.Items.Clear();
                    foreach (MsSiKoushu o in SeninTableCache.instance().GetMsSiKoushuList(NBaseCommon.Common.LoginUser))
                    {
                        checkedComboBox_Item1.Items.Add(o);
                    }
                    checkedComboBox_Item1.Visible = true;

                    //PresentationItemSetNo = -1;
                    //selected(this, new EventArgs());
                }
                else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL)
                {
                    checkedComboBox_Item1.SelectedItem = null;
                    checkedComboBox_Item1.Text = null;
                    checkedComboBox_Item1.Items.Clear();
                    foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.検診_検査名))
                    {
                        checkedComboBox_Item1.Items.Add(o);
                    }
                    checkedComboBox_Item1.Visible = true;

                    //PresentationItemSetNo = -1;
                    //selected(this, new EventArgs());
                }
            }

        }
        #endregion


        /// <summary>
        /// 「Item1」の選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Item1_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Item1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearItem2Controls();
            ClearItem3Controls();
            ClearValueControls();

            MsSiAdvancedSearchCondition tab = comboBox_Tab.SelectedItem as MsSiAdvancedSearchCondition;
            List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);
            

            if (tab.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
            {
                MsSiAdvancedSearchCondition item1 = comboBox_Item1.SelectedItem as MsSiAdvancedSearchCondition;
                if (item1.ChildrenComponent.Length > 0)
                {
                }
                else
                {
                    if (item1.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                    {
                        comboBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
                    {
                        checkedComboBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT)
                    {
                        textBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT_PANEL)
                    {
                        flowLayoutPanel_Value.Visible = true;
                    }
                    else if (item1.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL)
                    {
                        flowLayoutPanel_Date.Visible = true;
                    }

                    if (item1.ValueComponent2 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                    {
                        comboBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent2 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
                    {
                        checkedComboBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent2 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT)
                    {
                        textBox_Value.Visible = true;
                    }
                    else if (item1.ValueComponent2 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT_PANEL)
                    {
                        flowLayoutPanel_Value.Visible = true;
                    }
                    else if (item1.ValueComponent2 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL)
                    {
                        flowLayoutPanel_Date.Visible = true;
                    }



                    if (item1.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_RANK)
                    {
                        foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
                        {
                            checkedComboBox_Value.Items.Add(s);
                        }
                    }
                    else if (item1.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_VESSEL)
                    {
                        List<MsVessel> vesselList = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser);
                        var orderByName = vesselList.OrderBy(obj => obj.VesselName);
                        foreach (MsVessel o in orderByName)
                        {
                            if (o.SeninEnabled == 1)
                                checkedComboBox_Value.Items.Add(o);
                        }
                    }
                    else if (item1.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_VESSEL_ALL)
                    {
                        List<MsVessel> vesselList = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser);
                        var orderByName = vesselList.OrderBy(obj => obj.VesselName);
                        foreach (MsVessel o in orderByName)
                        {
                            checkedComboBox_Value.Items.Add(o);
                        }
                    }
                    else if (item1.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_INJURIES_EXISTENCE)
                    {
                        foreach (string obj in ValueDic[item1.MsSiAdvancedSearchConditionID])
                        {
                            comboBox_Value.Items.Add(obj);
                        }
                    }
                    else if (item1.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_YEARS_OF_TANKER)
                    {
                        foreach (MsCrewMatrixType o in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                        {
                            checkedComboBox_Value.Items.Add(o.TypeName);
                        }
                    }
                }

                //if (selected != null)
                //{
                //    PresentationItemSetNo = -1;
                //    if (item1.PresentationItemSetNo > 0)
                //    {
                //        PresentationItemSetNo = item1.PresentationItemSetNo;
                //    }
                //    selected(this, new EventArgs());
                //}
            }

        }
        #endregion


        /// <summary>
        /// 「ITEM2」の選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Item2_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Item2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsSiAdvancedSearchCondition item2 = comboBox_Item2.SelectedItem as MsSiAdvancedSearchCondition;
            valueControlsActive(item2);
        }
        #endregion

        /// <summary>
        /// 「ITEM3」の選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Item3_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Item3_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsSiAdvancedSearchCondition item3 = comboBox_Item3.SelectedItem as MsSiAdvancedSearchCondition;
            valueControlsActive(item3);
        }
        #endregion

        private void valueControlsActive(MsSiAdvancedSearchCondition item)
        {
            ClearValueControls();

            if (item.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
            {
                comboBox_Value.Items.Clear();
                foreach (string obj in ValueDic[item.MsSiAdvancedSearchConditionID])
                {
                    comboBox_Value.Items.Add(obj);
                }
                comboBox_Value.Visible = true;
            }
            else if (item.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL)
            {
                nullableDateTimePicker1.Value = DateTime.Today;
                nullableDateTimePicker2.Value = DateTime.Today;
                nullableDateTimePicker1.Value = null;
                nullableDateTimePicker2.Value = null;

                flowLayoutPanel_Date.Visible = true;
            }
            else if (item.ValueComponent1 == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
            {
                if (item.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL_RESULT)
                {
                    foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.検診_判定))
                    {
                        checkedComboBox_Value.Items.Add(o);
                    }
                }

                checkedComboBox_Value.Visible = true;
            }

            //if (item.PresentationItemSetNo > 0 && selected != null)
            //{
            //    PresentationItemSetNo = item.PresentationItemSetNo;
            //    selected(this, new EventArgs());
            //}
        }


        /// <summary>
        /// 「ITEM1」のCheckedComboのDropDownが閉じたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void checkedComboBox_Item1_DropDownClosed(object sender, EventArgs e)
        private void checkedComboBox_Item1_DropDownClosed(object sender, EventArgs e)
        {
            MsSiAdvancedSearchCondition tab = comboBox_Tab.SelectedItem as MsSiAdvancedSearchCondition;
            List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);
            MsSiAdvancedSearchCondition item1 = conditionAll.Where(obj => (obj.Layer == 2 && obj.ParentID == tab.MsSiAdvancedSearchConditionID)).First();

            if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE)
            {
  　            checkedComboBox_Item2.SelectedItem = null;
                checkedComboBox_Item2.Text = null;
                checkedComboBox_Item2.Items.Clear();
                checkedComboBox_Item2.Visible = true;

                if (checkedComboBox_Item1.CheckedItems.Count == 1)
                {
                    MsSiMenjou menjou = checkedComboBox_Item1.CheckedItems[0] as MsSiMenjou;

                    checkedComboBox_Item2.SelectedItem = null;
                    List<MsSiMenjouKind> all = SeninTableCache.instance().GetMsSiMenjouKindList(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouID);
                    foreach (MsSiMenjouKind o in all)
                    {
                        checkedComboBox_Item2.Items.Add(o);
                    }
                    checkedComboBox_Item2.Enabled = true;
                }
                else
                {
                    checkedComboBox_Item2.Enabled = false;
                }

                MsSiAdvancedSearchCondition item2 = conditionAll.Where(obj => obj.Layer == 3 && obj.ParentID == item1.MsSiAdvancedSearchConditionID).First();

                comboBox_Item3.Items.Clear();
                foreach (MsSiAdvancedSearchCondition obj in conditionAll.Where(obj => (obj.Layer == 4 && obj.ParentID == item2.MsSiAdvancedSearchConditionID)))
                {
                    comboBox_Item3.Items.Add(obj);
                }

                comboBox_Item3.Visible = true;
            }
            else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO)
            {
                textBox_Value.Visible = true;

                //if (selected != null)
                //{
                //    PresentationItemSetNo = -1;
                //    if (item1.PresentationItemSetNo > 0)
                //    {
                //        PresentationItemSetNo = item1.PresentationItemSetNo;
                //    }
                //    selected(this, new EventArgs());
                //}
            }
            else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL)
            {
                comboBox_Item2.Items.Clear();
                foreach (MsSiAdvancedSearchCondition obj in conditionAll.Where(obj => (obj.Layer == 3 && obj.ParentID == item1.MsSiAdvancedSearchConditionID)))
                {
                    comboBox_Item2.Items.Add(obj);
                }

                comboBox_Item2.Visible = true;

                //if (selected != null)
                //{
                //    PresentationItemSetNo = -1;
                //    if (item1.PresentationItemSetNo > 0)
                //    {
                //        PresentationItemSetNo = item1.PresentationItemSetNo;
                //    }
                //    selected(this, new EventArgs());
                //}
            }
            else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_TRAINING)
            {
                comboBox_Value.Items.Clear();
                foreach (string obj in ValueDic[MsSiAdvancedSearchCondition.ID_TRAINING_NAME])
                {
                    comboBox_Value.Items.Add(obj);
                }
                comboBox_Value.Visible = true;

                //if (selected != null)
                //{
                //    PresentationItemSetNo = -1;
                //    if (item1.PresentationItemSetNo > 0)
                //    {
                //        PresentationItemSetNo = item1.PresentationItemSetNo;
                //    }
                //    selected(this, new EventArgs());
                //}
            }

        }
        #endregion



        public MsSeninAdvancedFilter GetConditions(int AndOr, int showOrder)
        {
            if (!(comboBox_Tab.SelectedItem is MsSiAdvancedSearchCondition))
            {
                return null;
            }

            MsSeninAdvancedFilter ret = new MsSeninAdvancedFilter();

            ret.ItemList = new List<SiAdvancedSearchConditionItem>();
            ret.ValueList = new List<SiAdvancedSearchConditionValue>();


            List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);
            MsSiAdvancedSearchCondition tab = comboBox_Tab.SelectedItem as MsSiAdvancedSearchCondition;
            MsSiAdvancedSearchCondition item1 = null;
            MsSiAdvancedSearchCondition item2 = null;
            MsSiAdvancedSearchCondition item3 = null;
            if (tab.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
            {
                if (!(comboBox_Item1.SelectedItem is MsSiAdvancedSearchCondition))
                {
                    return null;
                }
                item1 = comboBox_Item1.SelectedItem as MsSiAdvancedSearchCondition;
            }
            else
            {
                var tmp = conditionAll.Where(obj => obj.ParentID == tab.MsSiAdvancedSearchConditionID);
                if (tmp.Count() == 0)
                {
                    return null;
                }
                item1 = tmp.First();
            }

            if (item1.ChildrenComponent.Length != 0)
            {
                if (item1.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    if (!(comboBox_Item2.SelectedItem is MsSiAdvancedSearchCondition))
                    {
                        return null;
                    }
                    item2 = comboBox_Item2.SelectedItem as MsSiAdvancedSearchCondition;
                }
                else
                {
                    var tmp = conditionAll.Where(obj => obj.ParentID == item1.MsSiAdvancedSearchConditionID);
                    if (tmp.Count() == 0)
                    {
                        return null;
                    }
                    item2 = tmp.First();
                }
            }

            if (item2 != null && item2.ChildrenComponent.Length != 0)
            {
                if (item2.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    if (!(comboBox_Item3.SelectedItem is MsSiAdvancedSearchCondition))
                    {
                        return null;
                    }
                    item3 = comboBox_Item3.SelectedItem as MsSiAdvancedSearchCondition;
                }
                else
                {
                    var tmp = conditionAll.Where(obj => obj.ParentID == item2.MsSiAdvancedSearchConditionID);
                    if (tmp.Count() == 0)
                    {
                        return null;
                    }
                    item3 = tmp.First();
                }
            }


            SiAdvancedSearchConditionItem conditionItem = null;
            SiAdvancedSearchConditionValue conditionValue = null;


            //
            // TAB
            //
            conditionItem = new SiAdvancedSearchConditionItem();
            conditionItem.SiAdvancedSearchConditionItemID = NBaseUtil.Common.NewID();
            conditionItem.MsSiAdvancedSearchConditionID = tab.MsSiAdvancedSearchConditionID;
            //conditionItem.SiAdvancedSearchConditionHeadID
            conditionItem.AndOrFlag = AndOr;
            conditionItem.ShowOrder = showOrder;

            ret.ItemList.Add(conditionItem);

            if (item1 != null)
            {
                // TABの子供としての値
                conditionValue = new SiAdvancedSearchConditionValue();
                //conditionValue.SiAdvancedSearchConditionValueID
                conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM;
                conditionValue.ShowOrder = showOrder;
                conditionValue.Value = item1.MsSiAdvancedSearchConditionID.ToString();

                ret.ValueList.Add(conditionValue);

                if (checkedComboBox_Item1.Visible)
                {
                    for (int i = 0; i < checkedComboBox_Item1.CheckedItems.Count; i++)
                    {
                        conditionValue = new SiAdvancedSearchConditionValue();
                        //conditionValue.SiAdvancedSearchConditionValueID
                        conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                        conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                        conditionValue.ShowOrder = showOrder;
                        if (checkedComboBox_Item1.CheckedItems[i] is MsSiMenjou)
                        {
                            conditionValue.Value = (checkedComboBox_Item1.CheckedItems[i] as MsSiMenjou).MsSiMenjouID.ToString();
                        }
                        else if (checkedComboBox_Item1.CheckedItems[i] is MsCargoGroup)
                        {
                            conditionValue.Value = (checkedComboBox_Item1.CheckedItems[i] as MsCargoGroup).MsCargoGroupID.ToString();
                        }
                        else if (checkedComboBox_Item1.CheckedItems[i] is MsSiKoushu)
                        {
                            conditionValue.Value = (checkedComboBox_Item1.CheckedItems[i] as MsSiKoushu).MsSiKoushuID.ToString();
                        }
                        else
                        {
                            conditionValue.Value = (checkedComboBox_Item1.CheckedItems[i] as string);
                        }
                        ret.ValueList.Add(conditionValue);
                    }
                }
                //if (comboBox_Item1.Visible)
                //{
                //    conditionValue.Value = comboBox_Item1.SelectedIndex.ToString();
                //    ret.ValueList.Add(conditionValue);
                //}

                conditionItem = new SiAdvancedSearchConditionItem();
                conditionItem.SiAdvancedSearchConditionItemID = NBaseUtil.Common.NewID();
                conditionItem.MsSiAdvancedSearchConditionID = item1.MsSiAdvancedSearchConditionID;
                //conditionItem.SiAdvancedSearchConditionHeadID
                conditionItem.AndOrFlag = AndOr;
                conditionItem.ShowOrder = showOrder;

                ret.ItemList.Add(conditionItem);
            }


            if (item2 != null)
            {
                // ITEM1の子供としての値
                conditionValue = new SiAdvancedSearchConditionValue();
                //conditionValue.SiAdvancedSearchConditionValueID
                conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM;
                conditionValue.Value = item2.MsSiAdvancedSearchConditionID.ToString();

                ret.ValueList.Add(conditionValue);

                if (checkedComboBox_Item2.Visible)
                {
                    for (int i = 0; i < checkedComboBox_Item2.CheckedItems.Count; i++)
                    {
                        conditionValue = new SiAdvancedSearchConditionValue();
                        //conditionValue.SiAdvancedSearchConditionValueID
                        conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                        conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                        conditionValue.ShowOrder = showOrder;
                        if (checkedComboBox_Item2.CheckedItems[i] is MsSiMenjouKind)
                        {
                            conditionValue.Value = (checkedComboBox_Item2.CheckedItems[i] as MsSiMenjouKind).MsSiMenjouKindID.ToString();
                        }

                        ret.ValueList.Add(conditionValue);
                    }
                }
                //if (comboBox_Item2.Visible)
                //{
                //    conditionValue.Value = comboBox_Item2.SelectedIndex.ToString();
                //    ret.ValueList.Add(conditionValue);
                //}

                conditionItem = new SiAdvancedSearchConditionItem();
                conditionItem.SiAdvancedSearchConditionItemID = NBaseUtil.Common.NewID();
                conditionItem.MsSiAdvancedSearchConditionID = item2.MsSiAdvancedSearchConditionID;
                //conditionItem.SiAdvancedSearchConditionHeadID
                conditionItem.AndOrFlag = AndOr;
                conditionItem.ShowOrder = showOrder;

                ret.ItemList.Add(conditionItem);

            }

            if (item3 != null)
            {
                // ITEM2の子供としての値
                conditionValue = new SiAdvancedSearchConditionValue();
                //conditionValue.SiAdvancedSearchConditionValueID
                conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM;
                conditionValue.Value = item3.MsSiAdvancedSearchConditionID.ToString();

                ret.ValueList.Add(conditionValue);


                conditionItem = new SiAdvancedSearchConditionItem();
                conditionItem.SiAdvancedSearchConditionItemID = NBaseUtil.Common.NewID();
                conditionItem.MsSiAdvancedSearchConditionID = item3.MsSiAdvancedSearchConditionID;
                //conditionItem.SiAdvancedSearchConditionHeadID
                conditionItem.AndOrFlag = AndOr;
                conditionItem.ShowOrder = showOrder;

                ret.ItemList.Add(conditionItem);
            }
 
            if (checkedComboBox_Value.Visible)
            {
                for (int i = 0; i < checkedComboBox_Value.CheckedItems.Count; i++)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO;

                    if (checkedComboBox_Value.CheckedItems[i] is MsSiShokumei)
                    {
                        conditionValue.Value = (checkedComboBox_Value.CheckedItems[i] as MsSiShokumei).MsSiShokumeiID.ToString();
                    }
                    else if (checkedComboBox_Value.CheckedItems[i] is MsVessel)
                    {
                        conditionValue.Value = (checkedComboBox_Value.CheckedItems[i] as MsVessel).MsVesselID.ToString();
                    }
                    else 
                    {
                        conditionValue.Value = (checkedComboBox_Value.CheckedItems[i] as string);
                    }

                    ret.ValueList.Add(conditionValue);
                }

            }
            if (comboBox_Value.Visible)
            {
                conditionValue = new SiAdvancedSearchConditionValue();
                //conditionValue.SiAdvancedSearchConditionValueID
                conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                conditionValue.ShowOrder = showOrder;
                conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO;

                if (comboBox_Value.SelectedItem is MsSiShubetsu)
                {
                    conditionValue.Value = (comboBox_Value.SelectedItem as MsSiShubetsu).MsSiShubetsuID.ToString();
                    ret.ValueList.Add(conditionValue);
                }
                else
                {
                    conditionValue.Value = (comboBox_Value.SelectedItem as string);
                    ret.ValueList.Add(conditionValue);
                }
            }
            if (flowLayoutPanel_Value.Visible)
            {
                if (textBox_Value1.Text.Length > 0)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT_PANEL;

                    conditionValue.Value = "F" + textBox_Value1.Text;
                    ret.ValueList.Add(conditionValue);
                }

                if (textBox_Value2.Text.Length > 0)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT_PANEL;

                    conditionValue.Value = "T" + textBox_Value2.Text;
                    ret.ValueList.Add(conditionValue);
                }
            }
            if (textBox_Value.Visible)
            {
                if (textBox_Value.Text.Length > 0)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT;

                    conditionValue.Value = textBox_Value.Text;
                    ret.ValueList.Add(conditionValue);
                }
            }
            if (flowLayoutPanel_Date.Visible)
            {
                if (nullableDateTimePicker1.Value != null)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL;

                    conditionValue.Value = "F" + ((DateTime)nullableDateTimePicker1.Value).ToShortDateString();
                    ret.ValueList.Add(conditionValue);
                }

                if (nullableDateTimePicker2.Value != null)
                {
                    conditionValue = new SiAdvancedSearchConditionValue();
                    //conditionValue.SiAdvancedSearchConditionValueID
                    conditionValue.SiAdvancedSearchConditionItemID = conditionItem.SiAdvancedSearchConditionItemID;
                    conditionValue.ItemValueFlag = SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE;
                    conditionValue.ShowOrder = showOrder;
                    conditionValue.ComponentType = MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL;

                    conditionValue.Value = "T" + ((DateTime)nullableDateTimePicker2.Value).ToShortDateString();
                    ret.ValueList.Add(conditionValue);
                }
            }
            return ret;
        }


        public void SetCondition(List<SiAdvancedSearchConditionItem> itemList, List<SiAdvancedSearchConditionValue> valueList)
        {
            MsSiAdvancedSearchCondition tab = null;
            MsSiAdvancedSearchCondition item1 = null;
            MsSiAdvancedSearchCondition item2 = null;
            MsSiAdvancedSearchCondition item3 = null;
            List<MsSiAdvancedSearchCondition> conditionAll = SeninTableCache.instance().GetMsSiAdvancedSearchConditionList(NBaseCommon.Common.LoginUser);
            string itemId = null;

            if (itemList.Any(obj => obj.Layer == 1))
            {
                int id = itemList.Where(obj => obj.Layer == 1).First().MsSiAdvancedSearchConditionID;
                tab = conditionAll.Where(obj => obj.MsSiAdvancedSearchConditionID == id).First();

                comboBox_Tab.SelectedItem = tab;

                itemId = itemList.Where(obj => obj.Layer == 1).First().SiAdvancedSearchConditionItemID;
            }

            if (itemList.Any(obj => obj.Layer == 2))
            {
                string strId = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM && obj.SiAdvancedSearchConditionItemID == itemId).First().Value;
                item1 = conditionAll.Where(obj => obj.MsSiAdvancedSearchConditionID == int.Parse(strId)).First();
                var values = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && obj.SiAdvancedSearchConditionItemID == itemId);

                if (tab.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    comboBox_Item1.SelectedItem = item1;
                }
                else if (tab.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
                {
                    if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_LICENSE)
                    {
                        List<int> checkedIndexList = new List<int>();
                        for (int idx = 0; idx < checkedComboBox_Item1.Items.Count; idx++)
                        {
                            if (values.Any(obj => int.Parse(obj.Value) == (checkedComboBox_Item1.Items[idx] as MsSiMenjou).MsSiMenjouID))
                            {
                                checkedComboBox_Item1.SetItemChecked(idx, true);
                            }
                        }
                    }
                    else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_EXPERIENCE_CARGO)
                    {
                        for (int idx = 0; idx < checkedComboBox_Item1.Items.Count; idx++)
                        {
                            if (values.Any(obj => int.Parse(obj.Value) == (checkedComboBox_Item1.Items[idx] as MsCargoGroup).MsCargoGroupID))
                            {
                                checkedComboBox_Item1.SetItemChecked(idx, true);
                            }
                        }
                    }
                    else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_TRAINING)
                    {
                        for (int idx = 0; idx < checkedComboBox_Item1.Items.Count; idx++)
                        {
                            if (values.Any(obj => int.Parse(obj.Value) == (checkedComboBox_Item1.Items[idx] as MsSiKoushu).MsSiKoushuID))
                            {
                                checkedComboBox_Item1.SetItemChecked(idx, true);
                            }
                        }
                    }
                    else if (tab.MsSiAdvancedSearchConditionID == MsSiAdvancedSearchCondition.ID_MEDICAL)
                    {
                        for (int idx = 0; idx < checkedComboBox_Item1.Items.Count; idx++)
                        {
                            if (values.Any(obj => (string)obj.Value == (checkedComboBox_Item1.Items[idx] as string)))
                            {
                                checkedComboBox_Item1.SetItemChecked(idx, true);
                            }
                        }
                    }
                    checkedComboBox_Item1_DropDownClosed(null, null);
                }

                itemId = itemList.Where(obj => obj.Layer == 2).First().SiAdvancedSearchConditionItemID;
            }

            if (itemList.Any(obj => obj.Layer == 3))
            {
                string strId = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM && obj.SiAdvancedSearchConditionItemID == itemId).First().Value;
                item2 = conditionAll.Where(obj => obj.MsSiAdvancedSearchConditionID == int.Parse(strId)).First();
                var values = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && obj.SiAdvancedSearchConditionItemID == itemId);

                if (item1.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
                {
                    List<int> checkedIndexList = new List<int>();
                    for (int idx = 0; idx < checkedComboBox_Item2.Items.Count; idx++)
                    {
                        if (values.Any(obj => int.Parse(obj.Value) == (checkedComboBox_Item2.Items[idx] as MsSiMenjouKind).MsSiMenjouKindID))
                        {
                            checkedComboBox_Item2.SetItemChecked(idx, true);
                        }
                    }
                }
                else if (item1.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    comboBox_Item2.SelectedItem = item2;
                }

                itemId = itemList.Where(obj => obj.Layer == 3).First().SiAdvancedSearchConditionItemID;
            }

            if (itemList.Any(obj => obj.Layer == 4))
            {
                string strId = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_ITEM && obj.SiAdvancedSearchConditionItemID == itemId).First().Value;
                item3 = conditionAll.Where(obj => obj.MsSiAdvancedSearchConditionID == int.Parse(strId)).First();
                if (item2.ChildrenComponent == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                {
                    comboBox_Item3.SelectedItem = item3;
                }

                itemId = itemList.Where(obj => obj.Layer == 4).First().SiAdvancedSearchConditionItemID;
            }

            {
                var values = valueList.Where(obj => obj.ItemValueFlag == SiAdvancedSearchConditionValue.ITEM_VALUE_FLAG_VALUE && obj.SiAdvancedSearchConditionItemID == itemId);

                string valueComponent1 = "";
                string valueComponent2 = "";
                if (item3 != null)
                {
                    if (item3.ValueComponent1 != null && item3.ValueComponent1.Length > 0)
                    {
                        valueComponent1 = item3.ValueComponent1;
                    }
                    if (item3.ValueComponent2 != null && item3.ValueComponent2.Length > 0)
                    {
                        valueComponent2 = item3.ValueComponent2;
                    }
                }
                else if (item2 != null)
                {
                    if (item2.ValueComponent1 != null && item2.ValueComponent1.Length > 0)
                    {
                        valueComponent1 = item2.ValueComponent1;
                    }
                    if (item2.ValueComponent2 != null && item2.ValueComponent2.Length > 0)
                    {
                        valueComponent2 = item2.ValueComponent2;
                    }
                }
                else if (item1 != null)
                {
                    if (item1.ValueComponent1 != null && item1.ValueComponent1.Length > 0)
                    {
                        valueComponent1 = item1.ValueComponent1;
                    }
                    if (item1.ValueComponent2 != null && item1.ValueComponent2.Length > 0)
                    {
                        valueComponent2 = item1.ValueComponent2;
                    }
                }
                else if (tab != null)
                {
                    if (tab.ValueComponent1 != null && tab.ValueComponent1.Length > 0)
                    {
                        valueComponent1 = tab.ValueComponent1;
                    }
                    if (tab.ValueComponent2 != null && tab.ValueComponent2.Length > 0)
                    {
                        valueComponent2 = tab.ValueComponent2;
                    }
                }

                List<string> componentList = new List<string>();
                componentList.Add(valueComponent1);
                componentList.Add(valueComponent2);
                foreach(string componentType in componentList)
                {
                    var valuesOfComponentType = values.Where(obj => obj.ComponentType == componentType);

                    if (componentType == MsSiAdvancedSearchCondition.COMPONENT_TYPE_COMBO)
                    {
                        string value = valuesOfComponentType.First().Value;
                        if (comboBox_Value.Items[0] is MsSiShubetsu)
                        {
                            for(int i = 0; i < comboBox_Value.Items.Count; i ++)
                            {
                                if ((comboBox_Value.Items[i] as MsSiShubetsu).MsSiShubetsuID == int.Parse(value))
                                {
                                    comboBox_Value.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            comboBox_Value.SelectedItem = value;
                        }
                    }
                    else if (componentType == MsSiAdvancedSearchCondition.COMPONENT_TYPE_CHECKED_COMBO)
                    {
                        foreach(SiAdvancedSearchConditionValue v in valuesOfComponentType)
                        {
                            for(int i = 0; i < checkedComboBox_Value.Items.Count; i++)
                            {
                                if (checkedComboBox_Value.Items[i] is MsSiShokumei)
                                {
                                    if ((checkedComboBox_Value.Items[i] as MsSiShokumei).MsSiShokumeiID == int.Parse(v.Value))
                                    {
                                        checkedComboBox_Value.SetItemChecked(i, true);
                                    }
                                }
                                else if (checkedComboBox_Value.Items[i] is MsVessel)
                                {
                                    if ((checkedComboBox_Value.Items[i] as MsVessel).MsVesselID == int.Parse(v.Value))
                                    {
                                        checkedComboBox_Value.SetItemChecked(i, true);
                                    }
                                }
                                else
                                {
                                    if ((checkedComboBox_Value.Items[i] as string) == v.Value)
                                    {
                                        checkedComboBox_Value.SetItemChecked(i, true);
                                    }
                                }
                            }
                        }

                    }
                    else if (componentType == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT)
                    {
                        string value = valuesOfComponentType.First().Value;
                        textBox_Value.Text = value;
                    }
                    else if (componentType == MsSiAdvancedSearchCondition.COMPONENT_TYPE_TEXT_PANEL)
                    {
                        string value1 = valuesOfComponentType.First().Value;
                        string value2 = valuesOfComponentType.Last().Value;

                        if (value1.IndexOf('F') == 0)
                        {
                            textBox_Value1.Text = value1.Substring(1);
                        }
                        if (value1.IndexOf('T') == 0)
                        {
                            textBox_Value2.Text = value1.Substring(1);
                        }
                        if (value2.IndexOf('F') == 0)
                        {
                            textBox_Value1.Text = value2.Substring(1);
                        }
                        if (value2.IndexOf('T') == 0)
                        {
                            textBox_Value2.Text = value2.Substring(1);
                        }
                    }
                    else if (componentType == MsSiAdvancedSearchCondition.COMPONENT_TYPE_DATE_PANEL)
                    {
                        string value1 = valuesOfComponentType.First().Value;
                        string value2 = valuesOfComponentType.Last().Value;

                        if (value1.IndexOf('F') == 0)
                        {
                            nullableDateTimePicker1.Value = DateTime.Parse(value1.Substring(1));
                        }
                        if (value1.IndexOf('T') == 0)
                        {
                            nullableDateTimePicker2.Value = DateTime.Parse(value1.Substring(1));
                        }
                        if (value2.IndexOf('F') == 0)
                        {
                            nullableDateTimePicker1.Value = DateTime.Parse(value2.Substring(1));
                        }
                        if (value2.IndexOf('T') == 0)
                        {
                            nullableDateTimePicker2.Value = DateTime.Parse(value2.Substring(1));
                        }
                    }

                }

            }
        }
    }
}
