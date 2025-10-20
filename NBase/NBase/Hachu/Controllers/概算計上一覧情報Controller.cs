using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
//using LidorSystems.IntegralUI.Lists;
using Hachu.Models;
using Hachu.HachuManage;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseData.BLC;
using System.Data;

namespace Hachu.Controllers
{
    public class 概算計上一覧情報Controller
    {
        private Form MdiForm;
        private DataGridView 概算計上一覧DataGridView = null;

        public Hashtable 概算計上一覧InfoHash = null;
        public Hashtable 概算計上一覧FormHash = null;
        public Hashtable 概算計上一覧NodeHash = null;
        
        /// <summary>
        /// 対象データ
        /// </summary>
        private List<概算計上一覧Row> Rows = null;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="treeListView"></param>
        #region public 概算計上一覧情報Controller(Form form, DataGridView dataGridView)
        public 概算計上一覧情報Controller(Form form, DataGridView dataGridView)
        {
            // MainForm
            MdiForm = form;

            // 対象のTreeListView
            概算計上一覧DataGridView = dataGridView;

            // 情報管理用
            概算計上一覧InfoHash = new Hashtable();
            概算計上一覧FormHash = new Hashtable();
            概算計上一覧NodeHash = new Hashtable();
        }
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        #region public void 初期化(int width)
        public void 初期化(int width)
        {
            クリア();
        }
        #endregion

        /// <summary>
        /// 一覧情報をクリアする
        /// </summary>
        #region public void クリア()
        public void クリア()
        {
            Rows = new List<概算計上一覧Row>();
            //概算計上一覧DataGridView.Rows.Clear();
        }
        #endregion

        /// <summary>
        /// 情報を検索し、一覧表示する
        /// </summary>
        #region public void 一覧更新(概算計上一覧検索条件 検索条件)
        public void 一覧更新(概算計上一覧検索条件 検索条件)
        {
            クリア();

            概算計上一覧InfoHash.Clear();
            概算計上一覧FormHash.Clear();
            概算計上一覧NodeHash.Clear();

            if (検索条件 != null)
            {
                OdJryFilter filter = new OdJryFilter();
                // 検索条件 → Filter にセット
                #region
                filter.YearMonthOnly = 検索条件.YearMonthOnly;
                if (検索条件.YearMonth != null && 検索条件.YearMonth != "")
                {
                    filter.YearMonth = 検索条件.YearMonth;
                }
                if (検索条件.Vessel != null && 検索条件.Vessel.MsVesselID != -1)
                {
                    filter.MsVesselID = 検索条件.Vessel.MsVesselID;
                }
                if (検索条件.User != null && 検索条件.User.MsUserID != "")
                {
                    filter.JimTantouID = 検索条件.User.MsUserID;
                }
                if (検索条件.msThiIraiSbt != null && 検索条件.msThiIraiSbt.MsThiIraiSbtID != "")
                {
                    filter.MsThiIraiSbtID = 検索条件.msThiIraiSbt.MsThiIraiSbtID;
                }
                if (検索条件.msThiIraiShousai != null && 検索条件.msThiIraiShousai.MsThiIraiShousaiID != "")
                {
                    filter.MsThiIraiShousaiID = 検索条件.msThiIraiShousai.MsThiIraiShousaiID;
                }
                filter.MiKeijyo = 検索条件.status未計上;
                filter.KeijyoZumi = 検索条件.status計上済;
                #endregion

                // 検索
                #region
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    Rows = serviceClient.BLC_概算計上一覧Row_SearchRecords(NBaseCommon.Common.LoginUser, filter);
                }
                #endregion
            }

            一覧更新();
        }

        public void 一覧更新()
        {
            DataTable dt = new DataTable();

            #region カラムの設定
            //if (概算計上一覧DataGridView.Columns.Count == 0)
            //{
                dt.Columns.Add(new DataColumn("obj", typeof(概算計上一覧Row)));
                dt.Columns.Add(new DataColumn("船", typeof(string)));
                dt.Columns.Add(new DataColumn("種別", typeof(string)));
                dt.Columns.Add(new DataColumn("詳細種別", typeof(string)));
                dt.Columns.Add(new DataColumn("件名", typeof(string)));
                dt.Columns.Add(new DataColumn("金額", typeof(decimal)));
                dt.Columns.Add(new DataColumn("受領日", typeof(string)));
                dt.Columns.Add(new DataColumn("受領番号", typeof(string)));
                dt.Columns.Add(new DataColumn("status", typeof(string)));
                dt.Columns.Add(new DataColumn("取引先", typeof(string)));
                dt.Columns.Add(new DataColumn("事務担当者", typeof(string)));
            //}
            #endregion


            if (Rows != null)
            {
                var rows = from p in Rows
                           orderby p.MsVesselId, p.種別, p.詳細種別, p.StatusOrder, p.受領番号
                           select p;
                foreach (概算計上一覧Row row in rows)
                {
                    if (row.金額 <= 0)
                    {
                        continue;
                    }

                    ////ノードを作成
                    if (row.odjry != null)
                    {
                        ListInfo受領 jryInfo = new ListInfo受領();
                        jryInfo.info = row.odjry;
                        SetHash(row, jryInfo);
                    }
                    else
                    {
                        ListInfo支払 shrInfo = new ListInfo支払();
                        shrInfo.info = row.odshr;
                        SetHash(row, shrInfo);
                    }

                    #region 情報を一覧にセットする


                    DataRow dr = dt.NewRow();
                    dr["obj"] = row;
                    dr["船"] = row.船;
                    dr["種別"] = row.種別;
                    dr["詳細種別"] = row.詳細種別;
                    dr["件名"] = row.件名;
                    dr["金額"] = row.金額;// NBaseCommon.Common.金額出力2(row.金額);
                    dr["受領日"] = row.受領日;
                    dr["受領番号"] = row.受領番号;
                    dr["Status"] = row.Status;
                    dr["取引先"] = row.取引先;
                    dr["事務担当者"] = row.事務担当者;
                    
                    dt.Rows.Add(dr);

                    #endregion

                }

                概算計上一覧DataGridView.DataSource = dt;

                概算計上一覧DataGridView.Columns[0].Visible = false;
                概算計上一覧DataGridView.Columns[1].Width = 130;   //船
                概算計上一覧DataGridView.Columns[2].Width = 80;    //種別
                概算計上一覧DataGridView.Columns[3].Width = 80;    //詳細種別
                概算計上一覧DataGridView.Columns[4].Width = 250;   //件名
                概算計上一覧DataGridView.Columns[5].Width = 90;    //金額
                概算計上一覧DataGridView.Columns[5].DefaultCellStyle.Format = "c";
                概算計上一覧DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                概算計上一覧DataGridView.Columns[6].Width = 80;    //受領日
                概算計上一覧DataGridView.Columns[7].Width = 100;   //受領番号
                概算計上一覧DataGridView.Columns[8].Width = 75;    //status
                概算計上一覧DataGridView.Columns[9].Width = 250;   //取引先
                概算計上一覧DataGridView.Columns[10].Width = 150;  //事務担当者
                
               
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region public int 未計上の情報数()
        public int 未計上の情報数()
        {
            int count = 0;
            foreach (概算計上一覧Row row in Rows)
            {
                if (row.StatusOrder <= 0)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region public List<string> 未計上の受領ID()
        public List<string> 未計上の受領ID()
        {
            List<string> ret = new List<string>();
            foreach (概算計上一覧Row row in Rows)
            {
                if (row.odjry != null)
                {
                    if (row.odjry.GaisanKeijoID <= 0)
                    {
                        ret.Add(row.odjry.OdJryID);
                    }
                }
            }
            return ret;
        }
        #endregion

        #region public List<OdGaisanKeijo> 概算計上対象取得()
        public List<OdGaisanKeijo> 概算計上対象取得()
        {
            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            var rows = from p in Rows
                       orderby p.MsVesselId, p.種別, p.詳細種別, p.StatusOrder, p.受領番号
                       select p;
            foreach (概算計上一覧Row row in rows)
            {
                // 2010.12.17 概算計上は都度すべてを対象とする(川崎さんに確認)
                //if (row.odjry != null && row.odjry.GaisanKeijoID <= 0)
                if (row.odjry != null)
                {
                    OdGaisanKeijo gk = new OdGaisanKeijo();
                    gk.OdJryID = row.odjry.OdJryID;
                    gk.ThiIraiSbtID = row.odjry.MsThiIraiSbtID;
                    gk.ThiIraiShousaiID = row.odjry.OdThi_MsThiIraiShousaiID;
                    //gk.Amount = row.odjry.Amount;
                    //gk.NebikiAmount = row.odjry.NebikiAmount;
                    if (row.金額 == (row.odjry.Amount + row.odjry.Carriage - row.odjry.NebikiAmount))
                    {
                        gk.Amount = row.odjry.Amount + row.odjry.Carriage;
                        gk.NebikiAmount = row.odjry.NebikiAmount;
                    }
                    else
                    {
                        gk.Amount = row.金額;
                        gk.NebikiAmount = 0;
                    }
                    gk.Tax = row.odjry.Tax;
                    gk.KamokuNo = row.odjry.KamokuNo;
                    gk.UtiwakeKamokuNo = row.odjry.UtiwakeKamokuNo;
                    gk.Naiyou = row.odjry.OdThiNaiyou;
                    ret.Add(gk);
                }
                else if (row.odshr != null)
                {
                    OdGaisanKeijo gk = new OdGaisanKeijo();
                    gk.OdShrID = row.odshr.OdShrID;
                    gk.ThiIraiSbtID = row.odshr.OdThi_MsThiIraiSbtID;
                    gk.ThiIraiShousaiID = row.odshr.OdThi_MsThiIraiShousaiID;
                    //gk.Amount = row.odshr.Amount;
                    gk.Amount = row.odshr.Amount + row.odshr.Carriage;
                    gk.NebikiAmount = row.odshr.NebikiAmount;
                    gk.Tax = row.odshr.Tax;
                    gk.KamokuNo = row.odshr.KamokuNo;
                    gk.UtiwakeKamokuNo = row.odshr.UtiwakeKamokuNo;
                    gk.Naiyou = row.odshr.OdThiNaiyou;
                    ret.Add(gk);
                }
            }
            return ret;
        }
        #endregion

        #region public decimal Get合計金額()
        public decimal Get合計金額()
        {
            decimal 合計金額 = 0;
            if (Rows != null)
            {
                foreach (概算計上一覧Row row in Rows)
                {
                    //if (row.odjry != null)
                    //{
                    //    //合計金額 += row.odjry.Amount - row.odjry.NebikiAmount; // +row.odjry.Tax;
                    //    合計金額 += row.odjry.Amount - row.odjry.NebikiAmount + row.odjry.Carriage; // +row.odjry.Tax;
                    //}
                    //else
                    //{
                    //    //合計金額 += row.odshr.Amount - row.odshr.NebikiAmount; // +row.odshr.Tax;
                    //    合計金額 += row.odshr.Amount - row.odshr.NebikiAmount + row.odshr.Carriage; // +row.odshr.Tax;
                    //}

                    合計金額 += row.金額;
                }
            }
            return 合計金額;
        }
        #endregion

        public List<概算計上一覧Row> GetRows()
        {
            return Rows;
        }

        /// <summary>
        /// 「概算計上一覧」Form の TreeListView クリック
        /// ・クリックしたノードに対応する詳細Formを開く
        /// </summary>
        #region public void 詳細Formを開く()
        public void 詳細Formを開く()
        {
            if (概算計上一覧DataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            概算計上一覧Row row = 概算計上一覧DataGridView.SelectedRows[0].Cells[0].Value as 概算計上一覧Row;

            BaseForm 詳細Form = null;
            if (IsExistsForm(row))
            {
                // 既存のフォームをアクティブにする
                詳細Form = Get詳細Form(row);
                詳細Form.Activate();
            }
            else
            {
                // フォームを作成する
                詳細Form = Create詳細Form(row);
                if (詳細Form == null)
                    return;
                詳細Form.MdiParent = MdiForm;
                詳細Form.Show();

                // EventHandlerをセットする
                詳細Form.FormActiveEvent += new BaseForm.FormActiveEventHandler(詳細Form_FormActiveEvent);
                詳細Form.FormClosingEvent += new BaseForm.FormClosingEventHandler(詳細Form_FormClosingEvent);
            }
        }
        #endregion


        public void SetHash(概算計上一覧Row row, ListInfoBase info)
        {
            if (概算計上一覧InfoHash.Contains(row) == false)
            {
                概算計上一覧InfoHash.Add(row, info);
            }
        }

        /// <summary>
        /// ノードに対応する詳細Formがあるかを調べる
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsExistsForm(概算計上一覧Row row)
        {
            return 概算計上一覧FormHash.Contains(row);
        }

        /// <summary>
        /// 詳細Formに対応するノードがあるかを調べる
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private bool IsExistsNode(BaseForm form)
        {
            return 概算計上一覧NodeHash.Contains(form);
        }

        /// <summary>
        /// ノードに対応する詳細Formをかえす
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private BaseForm Get詳細Form(概算計上一覧Row row)
        {
            BaseForm form = null;
            try
            {
                form = (BaseForm)概算計上一覧FormHash[row];
            }
            catch
            {
            }
            return form;
        }

        /// <summary>
        /// ノードに対応する詳細Formを作成する
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private BaseForm Create詳細Form(概算計上一覧Row row)
        {
            ListInfoBase info = 概算計上一覧InfoHash[row] as ListInfoBase;
            BaseForm form = info.CreateForm((int)BaseForm.WINDOW_STYLE.概算);
            if (form == null)
                return null;
            概算計上一覧FormHash.Add(row, form);
            概算計上一覧NodeHash.Add(form, row);
            return form;
        }

        //===============================================================================
        // デリゲートを経由して呼び出されるメソッド郡
        //===============================================================================

        /// <summary>
        /// 詳細Formがアクティブになったときのイベント
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_FormActiveEvent(詳細BaseForm senderForm)
        public void 詳細Form_FormActiveEvent(BaseForm senderForm)
        {
            ノード選択(senderForm);
        }
        #endregion

        /// <summary>
        /// 詳細Formがクローズされた時に発生するイベントを受け付ける
        /// </summary>
        /// <param name="SenderForm"></param>
        #region public void 詳細Form_FormClosingEvent(詳細BaseForm senderForm)
        public void 詳細Form_FormClosingEvent(BaseForm senderForm)
        {
            管理情報削除(senderForm);
        }
        #endregion


        //===============================================================================
        // デリゲートを経由して呼び出されるメソッドから呼び出されるメソッド
        //===============================================================================
        
        /// <summary>
        /// 詳細Formに対応したノードを選択する
        /// </summary>
        /// <param name="form"></param>
        #region private void ノード選択(詳細BaseForm form)
        private void ノード選択(BaseForm form)
        {
            try
            {
                // フォームに対応するツリーノードを選択する
                概算計上一覧Row row = 概算計上一覧NodeHash[form] as 概算計上一覧Row;
                
                //dataGridView1.Rows[selectedRowIndex].Selected = false;
                //dataGridView1.Rows[selectedRowIndex - 1].Selected = true;         
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 詳細情報Formの管理情報を削除する
        /// </summary>
        /// <param name="form"></param>
        #region private void 管理情報削除(詳細BaseForm form)
        private void 管理情報削除(BaseForm form)
        {
            try
            {
                // ハッシュから削除する
                概算計上一覧Row row = 概算計上一覧NodeHash[form] as 概算計上一覧Row;

                //row.Selected = false;
                概算計上一覧NodeHash.Remove(form);
                概算計上一覧FormHash.Remove(row);
            }
            catch
            {
            }
        }
        #endregion

    }
}
