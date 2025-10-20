using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using System.Drawing;
using LidorSystems.IntegralUI.Lists.Style;
using LidorSystems.IntegralUI.Style;
using NBaseData.DS;

namespace Senin.util
{
    internal class TreeListViewDelegate配乗表 : TreeListViewDelegate
    {
        private ListItemColorStyle evenNodeStyle;
        private ListItemColorStyle oddNodeStyle;


        internal TreeListViewDelegate配乗表(TreeListView treeListView)
            : base(treeListView)
        {
            // Create the even color style
            evenNodeStyle = new ListItemColorStyle();
            evenNodeStyle.BackColor = Color.Gainsboro;

            // Create the odd color style
            oddNodeStyle = new ListItemColorStyle();
            oddNodeStyle.BackColor = Color.Silver;

            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.Columns[0].Fixed = ColumnFixedType.Left;

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            //h.headerContent = "<div><p indent=\"115\">職名</p><p>船名　定員</p></div>";
            //h.width = 150;
            h.headerContent = "<div><p indent=\"115\">職名</p><p>船名　定員　乗船数</p></div>";
            h.width = 165;
            columns.Add(h);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                h = new TreeListViewDelegate.Column();
                h.headerContent = s.Name;
                h.width = 120;
                h.headerColor = Color.LightBlue;
                columns.Add(h);

                h = new TreeListViewDelegate.Column();
                h.headerContent = "<div><p>乗船</p><p>日数</p></div>";
                h.width = 50;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = Color.LightGreen;
                columns.Add(h);

                h = new TreeListViewDelegate.Column();
                h.headerContent = "<div><p>休暇</p><p>残日</p></div>";
                h.width = 50;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = Color.LightPink;
                columns.Add(h);
            }

            return columns;
        }


        internal void SetRows(SiHaijou haijou, CheckedListBox.CheckedItemCollection checkedVessels)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<Dictionary<int, Dictionary<int, List<SiHaijouItem>>>> haijouDicList = CreateHaijouItemDic(haijou);
            SetRows_船(haijouDicList[0], checkedVessels);
            SetRows_乗船以外(haijouDicList[1]);

            treeListView.ResumeUpdate();
        }


        private void SetRows_船(Dictionary<int, Dictionary<int, List<SiHaijouItem>>> haijouDic, CheckedListBox.CheckedItemCollection checkedVessels)
        {
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                int count = 0;
                if (!checkedVessels.Contains(v))
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                string vesselStr = v.Tel + "            " + v.Capacity + "\n\n" + v.VesselName;
                AddSubItem(node, vesselStr, true);

                foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
                {
                    if (!haijouDic.ContainsKey(v.MsVesselID) || !haijouDic[v.MsVesselID].ContainsKey(s.MsSiShokumeiID))
                    {
                        AddSubItem(node, "", true);
                        AddSubItem(node, "", true);
                        AddSubItem(node, "", true);
                    }
                    else
                    {
                        List<SiHaijouItem> items = haijouDic[v.MsVesselID][s.MsSiShokumeiID];

                        string seninStr = string.Empty;
                        string workDaysStr = string.Empty;
                        string holiDaysStr = string.Empty;

                        for (int i = 0; i < items.Count; i++)
                        {
                            SiHaijouItem it = items[i];

                            if (i > 0)
                            {
                                seninStr += "\n";
                                workDaysStr += "\n";
                                holiDaysStr += "\n";
                            }

                            seninStr += it.GetItemKindString();

                            seninStr += it.SeninName;
                            workDaysStr += it.WorkDays;
                            holiDaysStr += it.HoliDays;

                            count++;
                        }

                        AddSubItem(node, seninStr, true);
                        AddSubItem(node, workDaysStr, true);
                        AddSubItem(node, holiDaysStr, true);
                    }
                }

                vesselStr = v.Tel + "           " + String.Format("{0,2}", v.Capacity) + "  " +String.Format("{0,2}", count) + "\n\n" + v.VesselName;
                node.SubItems[0].Text = vesselStr;

                node.Tag = v;

                treeListView.Nodes.Add(node);

                node.StyleFromParent = false;
                if (node.Index % 2 == 0)
                {
                    node.NormalStyle = evenNodeStyle;
                }
                else
                {
                    node.NormalStyle = oddNodeStyle;
                }
            }
        }


        private void SetRows_乗船以外(Dictionary<int, Dictionary<int, List<SiHaijouItem>>> haijouDic)
        {
            foreach (MsSiShubetsu shu in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                int count = 0;
                if (shu.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船) ||
                    SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, shu.MsSiShubetsuID))
                {
                    continue;
                }

                // 2014.09.24 旅行日（乗下船）、旅行日（転船）、旅行日（研修・講習）を、やっぱり、それぞれで扱いたい
                //// 2012.03
                //// 「旅行日(転船)」は、「旅行日」として扱うため、無視する
                //if (shu.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船))
                //{
                //    continue;
                //}
                //// 2014.08.06：201408月度改造
                //// 「旅行日(研修・講習)」は、「旅行日」として扱うため、無視する
                //if (shu.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_旅行日_研修講習ID(NBaseCommon.Common.LoginUser))
                //{
                //    continue;
                //}


                TreeListViewNode node = CreateNode();

                AddSubItem(node, shu.Name, true);

                foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
                {
                    if (!haijouDic.ContainsKey(shu.MsSiShubetsuID) || !haijouDic[shu.MsSiShubetsuID].ContainsKey(s.MsSiShokumeiID))
                    {
                        AddSubItem(node, "", true);
                        AddSubItem(node, "", true);
                        AddSubItem(node, "", true);
                    }
                    else
                    {
                        List<SiHaijouItem> items = haijouDic[shu.MsSiShubetsuID][s.MsSiShokumeiID];

                        string seninStr = string.Empty;
                        string workDaysStr = string.Empty;
                        string holiDaysStr = string.Empty;

                        for (int i = 0; i < items.Count; i++)
                        {
                            SiHaijouItem it = items[i];

                            if (i > 0)
                            {
                                seninStr += "\n";
                                workDaysStr += "\n";
                                holiDaysStr += "\n";
                            }

                            seninStr += it.SeninName;
                            workDaysStr += it.WorkDays;
                            holiDaysStr += it.HoliDays;

                            count++;
                        }

                        AddSubItem(node, seninStr, true);
                        AddSubItem(node, workDaysStr, true);
                        AddSubItem(node, holiDaysStr, true);
                    }
                }

                string shuStr = shu.Name + "     "  + String.Format("{0,2}", count);
                node.SubItems[0].Text = shuStr;

                node.Tag = shu;

                treeListView.Nodes.Add(node);

                node.StyleFromParent = false;
                if (node.Index % 2 == 0)
                {
                    node.NormalStyle = evenNodeStyle;
                }
                else
                {
                    node.NormalStyle = oddNodeStyle;
                }
            }
        }


        private List<Dictionary<int, Dictionary<int, List<SiHaijouItem>>>> CreateHaijouItemDic(SiHaijou haijou)
        {
            List<Dictionary<int, Dictionary<int, List<SiHaijouItem>>>> result = new List<Dictionary<int, Dictionary<int, List<SiHaijouItem>>>>();

            // <MsVesselID, Dictionary<MsSiShokumeiID, List<SiHaijouItem>
            Dictionary<int, Dictionary<int, List<SiHaijouItem>>> dic船 = new Dictionary<int, Dictionary<int, List<SiHaijouItem>>>();
            // <MsSiShubetsuID, Dictionary<MsSiShokumeiID, List<SiHaijouItem>
            Dictionary<int, Dictionary<int, List<SiHaijouItem>>> dic乗船以外 = new Dictionary<int, Dictionary<int, List<SiHaijouItem>>>();

            foreach (SiHaijouItem item in haijou.SiHaijouItems)
            {
                if (item.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    if (!dic船.ContainsKey(item.MsVesselID))
                    {
                        dic船[item.MsVesselID] = new Dictionary<int, List<SiHaijouItem>>();
                    }

                    if (!dic船[item.MsVesselID].ContainsKey(item.MsSiShokumeiID))
                    {
                        dic船[item.MsVesselID][item.MsSiShokumeiID] = new List<SiHaijouItem>();
                    }

                    dic船[item.MsVesselID][item.MsSiShokumeiID].Add(item);
                }
                else
                {
                    if (!dic乗船以外.ContainsKey(item.MsSiShubetsuID))
                    {
                        dic乗船以外[item.MsSiShubetsuID] = new Dictionary<int, List<SiHaijouItem>>();
                    }

                    if (!dic乗船以外[item.MsSiShubetsuID].ContainsKey(item.MsSiShokumeiID))
                    {
                        dic乗船以外[item.MsSiShubetsuID][item.MsSiShokumeiID] = new List<SiHaijouItem>();
                    }

                    dic乗船以外[item.MsSiShubetsuID][item.MsSiShokumeiID].Add(item);
                }
            }

            result.Add(dic船);
            result.Add(dic乗船以外);

            return result;
        }
    }
}
