using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Senin.util;
using NBaseData.DS;
using System.IO;
using NBaseUtil;

namespace Senin
{
    public partial class 配乗表Form : Form
    {
        private static 配乗表Form instance;

        private TreeListViewDelegate配乗表 treeListViewDelegate;

        private SiHaijou haijou;


        private 配乗表Form()
        {
            InitializeComponent();
            Init();
        }


        public static 配乗表Form Instance()
        {
            if (instance == null)
            {
                instance = new 配乗表Form();
            }

            return instance;
        }


        private void Init()
        {
            // 2018.01 ２０１７年度改造
            dateTimePicker日付.Value = DateTime.Today;
            //dateTimePicker日付.MinDate = DateTime.Today;


            InitCheckedListBox船();
            InitLabel前回配信日();

            treeListViewDelegate = new TreeListViewDelegate配乗表(treeListView1);

            Search();
        }


        private void InitLabel前回配信日()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                SiHaijou haijou = serviceClient.SiHaijou_GetRecord_前回配信(NBaseCommon.Common.LoginUser);

                if (haijou == null)
                {
                    label前回配信日.Text = "----/--/--";
                }
                else
                {
                    label前回配信日.Text = haijou.HaishinDate.ToShortDateString();
                }
            }
        }


        private void InitCheckedListBox船()
        {
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            //{
            //    checkedListBox船.Items.Add(v);
            //    checkedListBox船.SetItemChecked(checkedListBox船.Items.Count - 1, true);
            //}

            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselListBySeninEnabled(NBaseCommon.Common.LoginUser))//m.yoshihara miho 2017/5/16
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                checkedListBox船.Items.Add(v);
                if (v.SeninEnabled == 1)
                {
                    checkedListBox船.SetItemChecked(checkedListBox船.Items.Count - 1, true);
                }
                else if (v.SeninEnabled == 0)
                {
                    checkedListBox船.SetItemChecked(checkedListBox船.Items.Count - 1, false);
                }
            }
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            CreateSiHaijou();
            treeListViewDelegate.SetRows(haijou, checkedListBox船.CheckedItems);
        }


        private void CreateSiHaijou()
        {
            haijou = new SiHaijou();
            //List<SiCard> cards = null;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    SiCardFilter filter = new SiCardFilter();

                    filter.IncludeNullVessel = true;

                    //filter.Start = filter.End = DateTime.Now;
                    //filter.Start = filter.End = dateTimePicker日付.Value; // 2018.01 ２０１７年度改造
                    filter.Start = filter.End = (dateTimePicker日付.Value).AddSeconds(1);
                    filter.RetireFlag = 0;

                    foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
                    {
                        if (!SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                        {
                            filter.MsSiShubetsuIDs.Add(s.MsSiShubetsuID);
                        }
                    }

                    //cards = serviceClient.BLC_船員カード検索(NBaseCommon.Common.LoginUser, filter);
                    haijou = serviceClient.BLC_配乗表作成(NBaseCommon.Common.LoginUser, filter);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            // 2012.03 サーバ側で、配乗データを作成するように改造したので使用しない
            #region
            //foreach (SiCard c in cards)
            //{
            //    if (SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, c.MsSiShubetsuID))
            //    {
            //        continue;
            //    }

            //    SiHaijouItem item = new SiHaijouItem();
            //    item.MsVesselID = c.MsVesselID;

            //    if (c.SiLinkShokumeiCards.Count > 0)
            //    {
            //        foreach (SiLinkShokumeiCard link in c.SiLinkShokumeiCards)
            //        {
            //            item.MsSiShokumeiID = link.MsSiShokumeiID;
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        // 
            //        item.MsSiShokumeiID = c.SeninMsSiShokumeiID;
            //    }

            //    item.MsSiShubetsuID = c.MsSiShubetsuID;
            //    item.MsSeninID = c.MsSeninID;
            //    item.SeninName = c.SeninName;

            //    SetItemKind(c, item);

            //    if (c.MsSiShubetsuID == (int)SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船))
            //    {
            //        if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
            //        {
            //            item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
            //        }
            //        else
            //        {
            //            item.WorkDays = int.Parse(StringUtils.ToStr(c.StartDate, c.EndDate));
            //        }
            //    }
            //    else if (c.合計日数 != null && c.合計日数.ContainsKey(c.MsSiShubetsuID))
            //    {
            //        item.WorkDays = c.合計日数[c.MsSiShubetsuID];
            //    }

            //    item.HoliDays = c.休暇残日;

            //    haijou.SiHaijouItems.Add(item);
            //}
            #endregion
        }


        // 2012.03 サーバ側で、配乗データを作成するように改造したので使用しない
        #region
        //private static void SetItemKind(SiCard c, SiHaijouItem item)
        //{
        //    bool Is_兼 = false;
        //    bool Is_執 = false;
        //    bool Is_融 = false;

        //    if (c.SiLinkShokumeiCards.Count > 1)
        //    {
        //        Is_兼 = true;
        //    }

        //    if (c.SeninMsSiShokumeiID > item.MsSiShokumeiID)
        //    {
        //        Is_執 = true;
        //    }

        //    if (c.SeninKubun == 1)
        //    {
        //        Is_融 = true;
        //    }

        //    item.SetItemKind(Is_兼, Is_執, Is_融);
        //}
        #endregion

        private void button配信_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "配信してよろしいですか？",
                                            "配乗表配信",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                FillInstance();

                if (Save())
                {
                    MessageBox.Show(this, "配信しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitLabel前回配信日();
                }
                else
                {
                    MessageBox.Show(this, "配信に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool Save()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.BLC_配乗表配信(NBaseCommon.Common.LoginUser, haijou);
            }

            return result;
        }


        private void FillInstance()
        {
            haijou.HaishinUserID = NBaseCommon.Common.LoginUser.MsUserID;
            haijou.HaishinDate = DateTime.Now;
        }


        private void button配乗表出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "配乗表_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
			    try
			    {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    
                    //2013/12/18 追加 m.y
                    //サーバーエラー時のフラグ
                    bool serverError = false;

                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            //--------------------------------
                            //2013/12/18 コメントアウト m.y
                            //result = serviceClient.BLC_Excel_配乗表出力(NBaseCommon.Common.LoginUser, haijou);
                            //--------------------------------
                            try
                            {
                                result = serviceClient.BLC_Excel_配乗表出力(NBaseCommon.Common.LoginUser, haijou);
                            }
                            catch (Exception exp)
                            {
                                //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                            }
                            //--------------------------------
                        }
                    }, "配乗表を出力中です...");
                    progressDialog.ShowDialog();

                    //--------------------------------
                    //2013/12/18 追加 m.y 
                    if (serverError == true)
                        return;
                    //--------------------------------

                    if (result == null)
                    {
                        MessageBox.Show("配乗表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "配乗表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show("配乗表の出力に失敗しました。\n (Err:" + ex.Message + ")", "配乗表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void 配乗表Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }


        private void dateTimePicker日付_ValueChanged(object sender, EventArgs e)
        {
            // 2018.01 「配信」ボタンは、操作日当日の時のみ有効とする
            if (dateTimePicker日付.Value == DateTime.Today)
            {
                button配信.Enabled = true;
            }
            else
            {
                button配信.Enabled = false;
            }
        }
    }
}
