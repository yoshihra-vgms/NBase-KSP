using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.DA;
using System.Drawing;

namespace Yojitsu.util
{
    class TreeListViewDelegate船稼働設定 : TreeListViewDelegate
    {
        private Dictionary<TreeListViewSubItem, BgKadouVessel> kadouVesselDic = new Dictionary<TreeListViewSubItem, BgKadouVessel>();

        
        internal TreeListViewDelegate船稼働設定(TreeListView treeListView)
            : base(treeListView)
        {
        }


         internal void CreateTable(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
         {
             treeListView.SuspendUpdate();

             treeListView.Columns.Clear();
             treeListView.Nodes.Clear();

             SetColumns(CreateColumns(yosanHead));
             CreateRowData(BuildCellItemDictionary(yosanHead, kadouVessels));

             treeListView.Columns[0].Fixed = ColumnFixedType.Left;
             treeListView.ResumeUpdate();
         }


         private List<Column> CreateColumns(BgYosanHead yosanHead)
         {
             List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

             TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
             h.headerContent = "船名";
             h.width = 200;
             h.fixedWidth = true;
             h.alignment = HorizontalAlignment.Left;
             columns.Add(h);

             for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
             {
                 h = new TreeListViewDelegate.Column();
                 h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度" + "</p><p>稼働期間</p><p>検査種別</p></div>";
                 h.width = 250;
                 h.fixedWidth = true;
                 h.alignment = HorizontalAlignment.Left;
                 columns.Add(h);
             }

             return columns;
         }

        
         private void CreateRowData(Dictionary<int, List<CellItem>> itemsDic)
         {
             treeListView.SuspendLayout();
                
             foreach (KeyValuePair<int, List<CellItem>> pair in itemsDic)
             {
                 TreeListViewNode node1 = CreateNode(Color.Gainsboro);

                 for (int i = 0; i < pair.Value.Count; i++)
                 {
                     BgKadouVessel kv = pair.Value[i].KadouVessel;
                        
                     if (i == 0)
                     {
                         AddSubItem(node1, kv.VesselName, true);
                     }

                     TreeListViewSubItem subItem = AddSubItem(node1, Create船稼働String(kv), true);
                     kadouVesselDic[subItem] = kv;
                 }

                 treeListView.Nodes.Add(node1);
             }

             treeListView.ResumeLayout();
         }


         internal static string Create船稼働String(BgKadouVessel kadouVessel)
         {
             if (kadouVessel.KadouStartDate == DateTime.MinValue || kadouVessel.KadouEndDate == DateTime.MinValue)
             {
                 return "不稼働";
             }
             else
             {
                 return Create稼働期間Str(kadouVessel) + "\n" + Create定期点検Str(kadouVessel);
             }
         }
        
        
         private static string Create稼働期間Str(BgKadouVessel bgKadouVessel)
         {
             StringBuilder buff = new StringBuilder();
             buff.Append(bgKadouVessel.KadouStartDate.ToShortDateString());
             buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate));
             buff.Append(" ～ ");
             buff.Append(bgKadouVessel.KadouEndDate.ToShortDateString());
             buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate));

             if (bgKadouVessel.EigyouKisoFlag == 1)
             {
                 buff.Append("(営)");
             }

             if (bgKadouVessel.KanriKisoFlag == 1)
             {
                 buff.Append("(管)");
             }

             return buff.ToString();
         }

        
         private static string Create定期点検Str(BgKadouVessel bgKadouVessel)
         {
             if (bgKadouVessel.NyukyoKind == null || bgKadouVessel.NyukyoKind == string.Empty)
             {
                 return "　";
             }
                
             StringBuilder buff = new StringBuilder();
             buff.Append(bgKadouVessel.NyukyoKind);
             buff.Append(" ");
             buff.Append(bgKadouVessel.NyukyoMonth);
             buff.Append(" 月");
             buff.Append(" 入渠前 ");
             buff.Append(bgKadouVessel.Fukadoubi1);
             buff.Append(" 入渠 ");
             buff.Append(bgKadouVessel.Fukadoubi2);
             buff.Append(" 出渠後 ");
             buff.Append(bgKadouVessel.Fukadoubi3);

             return buff.ToString();
         }


         internal BgKadouVessel GetBgKadouVessel(TreeListViewSubItem treeListViewSubItem)
         {
             if (treeListViewSubItem == null || !kadouVesselDic.ContainsKey(treeListViewSubItem))
             {
                 return null;
             }
                
             return kadouVesselDic[treeListViewSubItem];
         }


         private Dictionary<int, List<CellItem>> BuildCellItemDictionary(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
         {
             var itemsDic = new Dictionary<int, List<CellItem>>();

             foreach (BgKadouVessel kv in kadouVessels)
             {
                 if (!itemsDic.ContainsKey(kv.MsVesselID))
                 {
                     var items = new List<CellItem>();
                     itemsDic.Add(kv.MsVesselID, items);
                 }


                 itemsDic[kv.MsVesselID].Add(new CellItem(kv));
             }

             return itemsDic;
         }



         public class CellItem
         {
             public BgKadouVessel KadouVessel { get; private set; }
                
                
             internal CellItem(BgKadouVessel kadouVessel)
             {
                 this.KadouVessel = kadouVessel;
             }
         }
    }
}
