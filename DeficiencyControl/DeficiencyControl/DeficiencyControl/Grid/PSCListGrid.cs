using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Logic;

namespace DeficiencyControl.Grid
{
    /// <summary>
    /// PSC一覧グリッド管理
    /// </summary>
    public class PSCListGrid : BaseEditCheckGrid
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dv"></param>
        public PSCListGrid(DataGridView dv)
            : base(dv)
        {
        }


        /// <summary>
        /// ActionCode表示文字列を作成する
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        private string CreateActionCodeString(DcActionCodeHistory ac)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            //表示文字列の作成
            string ans = "";
            MsActionCode code = db.GetMsActionCode(ac.action_code_id);
            if (code != null)
            {
                ans += code.ToString();
            }


            //ans += " ";
            //ans += ac.action_code_text;

            //-----
            return ans;
        }

        /// <summary>
        /// ActionCode表示文字列を作成する 
        /// </summary>
        /// <param name="aclist"></param>
        /// <returns></returns>
        private string CreateActionCodeGridString(List<DcActionCodeHistory> aclist)
        {
            string ans = "";
            if (aclist.Count <= 0)
            {
                return ans;
            }

            //最大三件
            for (int i = 0; i < 3; i++)
            {
                if (aclist.Count <= i)
                {
                    continue;
                }

                ans += this.CreateActionCodeString(aclist[i]);
                ans += "/";
            }

            ans = ans.Remove(ans.Length - 1);

            return ans;
        }

        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="objlist">List PSCInspectionData</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<PSCInspectionData> datalist = objlist as List<PSCInspectionData>;
            if (datalist == null)
            {
                return false;
            }

            DBDataCache db = DcGlobal.Global.DBCache;

            //----------------------------------------------------------
            //クリア
            this.Grid.Rows.Clear();

            
            //表示数設定
            this.Grid.RowCount = datalist.Count;
            
            int i = 0;
            foreach (PSCInspectionData data in datalist)
            {
                DcCiPscInspection psc = data.PscInspection;


                int pos = 0;

                //data
                this.Grid[pos, i].Value = data;
                pos++;

                //Check
                bool fchk = data.CheckComplete();
                this.Grid[pos, i].Value = fchk;
                pos++;

                //No
                //this.Grid[pos, i].Value = (i + 1);
                this.Grid[pos, i].Value = (psc.comment_item_id);
                pos++;

                //Vessel
                MsVessel ves = db.GetMsVessel(psc.ms_vessel_id);
                if (ves != null)
                {
                    this.Grid[pos, i].Value = ves.ToString();
                }
                pos++;

                //Date
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(psc.date);
                pos++;

                //Port
                MsBasho ba = db.GetMsBasho(psc.ms_basho_id);
                if (ba != null)
                {
                    this.Grid[pos, i].Value = ba.ToString();
                }
                pos++;

                //Kind
                MsItemKind ki = db.GetMsItemKind(psc.item_kind_id);
                if (ki != null)
                {
                    this.Grid[pos, i].Value = ki.ToString();
                }
                pos++;

                //DeficiencyCode
                MsDeficiencyCode de = db.GetMsDeficiencyCode(psc.deficiency_code_id);
                if (de != null)
                {
                    this.Grid[pos, i].Value = de.ToString();
                }
                pos++;

                //Deficiency
                this.Grid[pos, i].Value = psc.deficiency;
                pos++;

                //ActionCode
                {
                    string acs = this.CreateActionCodeGridString(data.ActionCodeHistoryList);
                    this.Grid[pos, i].Value = acs;
                }
                pos++;


                //Status
                this.Grid[pos, i].Value = db.GetMsStatus(psc.status_id);
                pos++;

                //PIC
                MsUser us = db.GetMsUser(psc.ms_user_id);
                if (us != null)
                {
                    this.Grid[pos, i].Value = us.ToString();
                }
                pos++;



                i++;
            }
            
            return true;
        }



        /// <summary>
        /// チェックが変更されたデータを一括取得する PSCInspectionData
        /// </summary>
        /// <typeparam name="T">PSCInspectionData</typeparam>
        /// <returns></returns>
        public override List<T> GetEditCheckList<T>()
        {
            List<T> anslist = new List<T>();

            for (int i = 0; i < this.Grid.Rows.Count; i++)
            {
                //元データ取得
                PSCInspectionData pscdata = this.Grid[0, i].Value as PSCInspectionData;
                if (pscdata == null)
                {
                    continue;
                }

                bool flag = (bool)this.Grid[CheckColIndex, i].Value;
                //同じなら問題なし。
                if (pscdata.CheckComplete() == flag)
                {
                    continue;
                }

                //違った場合、値をセットして取得対象へ
                if (flag == true)
                {
                    pscdata.PscInspection.status_id = (int)EStatus.Complete;
                }
                else
                {
                    pscdata.PscInspection.status_id = (int)EStatus.Pending;
                }


                //ADDする
                T a = (T)(object)pscdata;
                anslist.Add(a);

                
            }

            return anslist;
        }
    }
}
