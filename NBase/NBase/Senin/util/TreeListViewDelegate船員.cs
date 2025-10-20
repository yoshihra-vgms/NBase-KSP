using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseCommon.Senin;
using System.Diagnostics;
using NBaseData.BLC;

namespace Senin.util
{
    internal class TreeListViewDelegate船員 : TreeListViewDelegate
    {
        internal TreeListViewDelegate船員(TreeListView treeListView)
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
            h.headerContent = "No";
            h.width = 40;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 64;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 85;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "社員区分";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 87;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名（カナ）";
            h.width = 97;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "従業員番号";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "保険番号";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "船";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "合計乗船";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "合計休暇";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日数";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "休暇日数";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日";
            h.width = 80;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<MsSenin> senins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<TreeListViewUtils.MsSeninRow> rows = TreeListViewUtils.CreateRowData(senins, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            int i = 0;
            foreach (TreeListViewUtils.MsSeninRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++i).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.senin.MsSiShubetsuID), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.MsSiShokumeiID), true);
                AddSubItem(node, r.senin.KubunStr, true);
                AddSubItem(node, r.senin.Sei + " " + r.senin.Mei, true);
                AddSubItem(node, r.senin.SeiKana + " " + r.senin.MeiKana, true);
                AddSubItem(node, r.senin.ShimeiCode, true);
                AddSubItem(node, r.senin.HokenNo, true);
                AddSubItem(node, SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, r.senin.MsVesselID), true);

                int 合計乗船 = 0;
                if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)))
                {
                    合計乗船 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)];
                }

                AddSubItem(node, 合計乗船.ToString(), true);

                int 合計休暇 = 0;
                if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser)))
                {
                    合計休暇 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser)];
                }

                AddSubItem(node, 合計休暇.ToString(), true);

                // 乗船日数
                if (r.senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                {
                    AddSubItem(node, StringUtils.ToStr(r.senin.StartDate, DateTime.Now), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                // 休暇日数
                if (r.senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser))
                {
                    AddSubItem(node, StringUtils.ToStr(r.senin.StartDate, DateTime.Now), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                AddSubItem(node, r.乗船日, true);

                node.Tag = r.senin;
                
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }

        public void SetColumns()
        {
            base.treeListView.Columns.Clear();
            SetColumns(CreateColumns());
        }

        public void SetColumns(List<SiPresentaionItem> presentationItemList)
        {
            base.treeListView.Columns.Clear();
            SetColumns(CreateAdvancedColumns(presentationItemList));
        }

        private List<Column> CreateAdvancedColumns(List<SiPresentaionItem> presentationItemList)
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = null;

            // 固定カラム分
            #region

            h = new TreeListViewDelegate.Column();
            h.headerContent = "No";
            h.width = 40;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 64;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 85;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "社員区分";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 87;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名（カナ）";
            h.width = 97;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "従業員番号";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "保険番号";
            h.width = 60;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "船";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "合計乗船";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "合計休暇";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日数";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "休暇日数";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日";
            h.width = 80;
            columns.Add(h);

            #endregion

            // 条件に応じて対応
            #region
            foreach(SiPresentaionItem item in presentationItemList)
            {
                if (item.SetNo > 1 && item.OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_ON)
                {
                    if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_OPERATOR || 
                        item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_RANK)
                    {
                        string[] splitName = item.Name.Split(' ');
                        string name = "";
                        for (int i = 0; i < splitName.Length; i ++ )
                        {
                            if ( i != 0 )
                                name += System.Environment.NewLine;
                            name += splitName[i];
                        }
                        h = new TreeListViewDelegate.Column();
                        h.headerContent = name;
                        h.width = 80;
                        h.headerAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        h.alignment = HorizontalAlignment.Right;
                        columns.Add(h);
                    }
                    else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_OF_TANKER)
                    {
                        foreach(MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                        {
                            string name = "Years" + System.Environment.NewLine + "of " + System.Environment.NewLine + "type of Tanker" + System.Environment.NewLine;

                            h = new TreeListViewDelegate.Column();
                            h.headerContent = name + "(" + type.TypeName + ")";
                            h.width = 80;
                            h.headerAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            h.alignment = HorizontalAlignment.Right;
                            columns.Add(h);
                        }
                    }
                    else
                    {
                        h = new TreeListViewDelegate.Column();
                        h.headerContent = item.Name;
                        h.width = 80;

                        columns.Add(h);
                    }
                }
            }
            #endregion


            return columns;
        }


        internal void SetRows(List<SiPresentaionItem> presentationItemList, List<MsSeninPlus> senins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            Debug.WriteLine("senins: " + senins.Count().ToString());

            List<TreeListViewUtils.MsSeninPlusRow> rows = TreeListViewUtils.CreateRowData(senins, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            Debug.WriteLine("rows: " + rows.Count().ToString());

            int i = 0;
            foreach (TreeListViewUtils.MsSeninPlusRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                node.Tag = r.senin.Senin;

                // 固定カラム分
                #region

                AddSubItem(node, (++i).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsSiShubetsuID), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsSiShokumeiID), true);
                AddSubItem(node, r.senin.Senin.KubunStr, true);
                AddSubItem(node, r.senin.Senin.Sei + " " + r.senin.Senin.Mei, true);
                AddSubItem(node, r.senin.Senin.SeiKana + " " + r.senin.Senin.MeiKana, true);
                AddSubItem(node, r.senin.Senin.ShimeiCode, true);
                AddSubItem(node, r.senin.Senin.HokenNo, true);
                AddSubItem(node, SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, r.senin.Senin.MsVesselID), true);

                int 合計乗船 = 0;
                if (r.senin.Senin.合計日数 != null && r.senin.Senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)))
                {
                    合計乗船 = r.senin.Senin.合計日数[SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)];
                }

                AddSubItem(node, 合計乗船.ToString(), true);

                int 合計休暇 = 0;
                if (r.senin.Senin.合計日数 != null && r.senin.Senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser)))
                {
                    合計休暇 = r.senin.Senin.合計日数[SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser)];
                }

                AddSubItem(node, 合計休暇.ToString(), true);

                // 乗船日数
                if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                {
                    AddSubItem(node, StringUtils.ToStr(r.senin.Senin.StartDate, DateTime.Now), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                // 休暇日数
                if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser))
                {
                    AddSubItem(node, StringUtils.ToStr(r.senin.Senin.StartDate, DateTime.Now), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                //AddSubItem(node, r.乗船日, true);
                if (r.senin.Senin.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                {
                    AddSubItem(node, r.senin.Senin.StartDate.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                #endregion

                // 条件に応じて対応
                #region
                foreach (SiPresentaionItem item in presentationItemList)
                {
                    if (item.SetNo > 1 && item.OnOffFlag == SiPresentaionItem.ON_OFF_FLAG_ON)
                    {
                        if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_AGE)
                        {
                            AddSubItem(node, DateTimeUtils.年齢計算(r.senin.Senin.Birthday).ToString(), true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_ON_RANK)
                        {
                            AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.Card.CardMsSiShokumeiID), true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_ON)
                        {
                            if (r.senin.Card != null)
                            {
                                AddSubItem(node, r.senin.Card.StartDate.ToShortDateString(), true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_SIGN_OFF)
                        {
                            if (r.senin.Card == null)
                            {
                                AddSubItem(node, "", true);
                            }
                            else
                            {
                                if (r.senin.Card.EndDate != DateTime.MinValue)
                                {
                                    AddSubItem(node, r.senin.Card.EndDate.ToShortDateString(), true);
                                }
                                else
                                {
                                    AddSubItem(node, "", true);
                                }
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_VESSEL_ALL)
                        {
                            if (r.senin.Card != null)
                            {
                                AddSubItem(node, r.senin.Card.VesselName, true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_DAYS)
                        {
                            if (r.senin.Card != null)
                            {
                                AddSubItem(node, r.senin.Card.Days.ToString(), true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TYPE)
                        {
                            if (r.senin.Menjou != null)
                            {
                                AddSubItem(node, SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, r.senin.Menjou.MsSiMenjouID), true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_GRADE)
                        {
                            if (r.senin.Menjou != null)
                            {
                                AddSubItem(node, SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, r.senin.Menjou.MsSiMenjouKindID), true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_ISSUE_DATE)
                        {
                            string dateStr = "";
                            if (r.senin.Menjou != null)
                            {
                                dateStr = r.senin.Menjou.ShutokuDate != DateTime.MinValue ? r.senin.Menjou.ShutokuDate.ToShortDateString() : ""; 
                            }
                            AddSubItem(node, dateStr, true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_EXPIRY_DATE)
                        {
                            string dateStr = "";
                            if (r.senin.Menjou != null)
                            {
                                dateStr = r.senin.Menjou.Kigen != DateTime.MinValue ? r.senin.Menjou.Kigen.ToShortDateString() : "";
                            }
                            AddSubItem(node, dateStr, true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_OPERATOR)
                        {
                            if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_OPERATOR))
                            {
                                var cm = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_OPERATOR).First();
                                AddSubItem(node, 船員.CalcYears(cm.Days).ToString("0.0") + " Y", true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_IN_RANK)
                        {
                            if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_RANK))
                            {
                                var cm = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_IN_RANK).First();
                                AddSubItem(node, 船員.CalcYears(cm.Days).ToString("0.0") + " Y", true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_YEARS_OF_TANKER)
                        {
                            if (r.senin.CrewMatrixList.Any(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_ON_THIS_TYPE_OF_TANKER))
                            {
                                var tmplist = r.senin.CrewMatrixList.Where(obj => obj.Type == NBaseData.BLC.CrewMatrix.CREW_MATRIX_TYPE.YEARS_ON_THIS_TYPE_OF_TANKER);

                                foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                                {
                                    if (tmplist.Any(obj => obj.TypeOfTanker == type.TypeName))
                                    {
                                        var cm = tmplist.Where(obj => obj.TypeOfTanker == type.TypeName).First();
                                        AddSubItem(node, 船員.CalcYears(cm.Days).ToString("0.0") + " Y", true);
                                    }
                                }
                            }
                            else
                            {
                                foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
                                {
                                    AddSubItem(node, "", true);
                                }
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_KIND)
                        {
                            string kind = "";
                            if (r.senin.Shobyo != null && r.senin.Shobyo.SiShobyoID != null && r.senin.Shobyo.SiShobyoID.Length > 0)
                            {
                                kind = SiShobyo.KIND[r.senin.Shobyo.Kind];
                            }
                            AddSubItem(node, kind, true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_STATUS)
                        {
                            string status = "";
                            if (r.senin.Shobyo != null && r.senin.Shobyo.SiShobyoID != null && r.senin.Shobyo.SiShobyoID.Length > 0)
                            {
                                status = SiShobyo.STATUS[r.senin.Shobyo.Kind];
                            }
                            AddSubItem(node, status, true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_INJURIES_DATE)
                        {
                            string dateStr = "";
                            if (r.senin.Shobyo != null && r.senin.Shobyo.SiShobyoID != null && r.senin.Shobyo.SiShobyoID.Length > 0)
                            {
                                dateStr = r.senin.Shobyo.FromDate.ToShortDateString() + "～";
                                if (r.senin.Shobyo.ToDate != DateTime.MinValue)
                                {
                                    dateStr += r.senin.Shobyo.ToDate.ToShortDateString();
                                }
                            }
                            AddSubItem(node, dateStr, true);
                        }

                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING)
                        {
                            if (r.senin.Koushu != null)
                            {
                                AddSubItem(node, SeninTableCache.instance().GetMsSiKoushuName(NBaseCommon.Common.LoginUser, r.senin.Koushu.MsSiKoushuID), true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING_START)
                        {
                            string dateStr = "";
                            if (r.senin.Koushu != null)
                            {
                                dateStr = r.senin.Koushu.JisekiFrom != DateTime.MinValue ? r.senin.Koushu.JisekiFrom.ToShortDateString() : "";
                            }
                            AddSubItem(node, dateStr, true);
                        }
                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_TRAINING_END)
                        {
                            string dateStr = "";
                            if (r.senin.Koushu != null)
                            {
                                dateStr = r.senin.Koushu.JisekiTo != DateTime.MinValue ? r.senin.Koushu.JisekiTo.ToShortDateString() : "";
                            }
                            AddSubItem(node, dateStr, true);
                        }


                        else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_EXPERIENCE_CARGO)
                        {
                            if (r.senin.experienceCargoList != null)
                            {
                                string expStr = "";
                                foreach (SiExperienceCargo expCargo in r.senin.experienceCargoList)
                                {
                                    if (expStr.Length > 0)
                                    {
                                        expStr += " ";
                                    }
                                    expStr += SeninTableCache.instance().GetMsCargoGroupName(NBaseCommon.Common.LoginUser, expCargo.MsCargoGroupID) + "(" + expCargo.Count.ToString() + ")";
                                }
                                AddSubItem(node, expStr, true);
                            }
                            else
                            {
                                AddSubItem(node, "", true);
                            }
                        }


                        //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_KIND)
                        //{
                        //    if (r.senin.Kenshin != null)
                        //    {
                        //        AddSubItem(node, SiKenshin.KIND[r.senin.Kenshin.Kind], true);
                        //    }
                        //    else
                        //    {
                        //        AddSubItem(node, "", true);
                        //    }
                        //}
                        //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_CONSULTATION)
                        //{
                        //    string dateStr = "";
                        //    if (r.senin.Kenshin != null)
                        //    {
                        //        dateStr = r.senin.Kenshin.ConsultationDate != DateTime.MinValue ? r.senin.Kenshin.ConsultationDate.ToShortDateString() : "";
                        //    }
                        //    AddSubItem(node, dateStr, true);
                        //}
                        //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_EXPIRE)
                        //{
                        //    string dateStr = "";
                        //    if (r.senin.Kenshin != null)
                        //    {
                        //        dateStr = r.senin.Kenshin.ExpirationDate != DateTime.MinValue ? r.senin.Kenshin.ExpirationDate.ToShortDateString() : "";
                        //    }
                        //    AddSubItem(node, dateStr, true);
                        //}
                        //else if (item.MsSiPresentaionItemID == MsSiPresentationItem.ID_MEDICAL_RESULT)
                        //{
                        //    if (r.senin.Kenshin != null)
                        //    {
                        //        AddSubItem(node, SiKenshin.RESULT[r.senin.Kenshin.Result], true);
                        //    }
                        //    else
                        //    {
                        //        AddSubItem(node, "", true);
                        //    }
                        //}
                    }
                }
                #endregion

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}

