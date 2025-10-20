using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    internal class TreeListViewDelegate家族情報 : TreeListViewDelegate
    {
        internal TreeListViewDelegate家族情報(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();


            h = new TreeListViewDelegate.Column();
            h.headerContent = "";
            h.width = 125;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名(カナ)";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "性別";
            h.width = 55;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "続柄";
            h.width = 75;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "生年月日";
            h.width = 90;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "年齢";
            h.width = 40;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "扶養";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "同居";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "電話番号";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職業";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 200;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiKazoku> kazokus)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            for (int i = 0; i < kazokus.Count; i++)
            {
                SiKazoku k = kazokus[i];

                if (k.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                string val = "";
                val += (k.Kind == 0) ? "家族" : "家族外";
                
                MsSiOptions opt = SeninTableCache.instance().GetMsSiOptions(NBaseCommon.Common.LoginUser, k.EmergencyKind);
                if (opt.ShowOrder != 1)
                {
                    val += "(" + opt.Name + ")";
                }
                AddSubItem(node, val, true);

                AddSubItem(node, k.Sei + " " + k.Mei, true);
                AddSubItem(node, k.SeiKana + " " + k.MeiKana, true);
                AddSubItem(node, k.SexStr, true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiOptionsName(NBaseCommon.Common.LoginUser, k.Tuzukigara), true);
                if (k.Birthday == DateTime.MinValue)
                {
                    AddSubItem(node, "", true);
                    AddSubItem(node, "", true);
                }
                else
                {
                    AddSubItem(node, k.Birthday.ToShortDateString(), true);
                    AddSubItem(node, DateTimeUtils.年齢計算(k.Birthday).ToString(), true);
                }
                AddSubItem(node, k.DependentStr, true);
                AddSubItem(node, k.LivingTogetherStr, true);
                AddSubItem(node, k.Tel, true);
                AddSubItem(node, k.Occupation, true);
                AddSubItem(node, k.Remarks, true);

                node.Tag = k;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
