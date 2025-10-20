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
using DeficiencyControl.Grid;

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// Accident一覧Grid
    /// </summary>
    public class AccidentListGrid : BaseEditCheckGrid
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dv"></param>
        public AccidentListGrid(DataGridView dv)
            : base(dv)
        {
        }



        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="objlist">List DcAccident</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<DcAccident> datalist = objlist as List<DcAccident>;
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
            foreach (DcAccident data in datalist)
            {

                int pos = 0;

                //data
                this.Grid[pos, i].Value = data;
                pos++;


                //青モードのチェック可否
                this.Grid[pos, i].Value = (data.CheckComplete());
                pos++;


                //No
                //this.Grid[pos, i].Value = (i + 1);
                this.Grid[pos, i].Value = (data.accident_id);
                pos++;

                //Vessel
                MsVessel ves = db.GetMsVessel(data.ms_vessel_id);
                if (ves != null)
                {
                    this.Grid[pos, i].Value = ves.ToString();
                }
                pos++;

                //Date
                this.Grid[pos, i].Value = DcGlobal.DateTimeToString(data.date);
                pos++;

                //Port
                MsBasho ba = db.GetMsBasho(data.ms_basho_id);
                if (ba != null)
                {
                    this.Grid[pos, i].Value = ba.ToString();
                }
                pos++;

                //Kind
                MsAccidentKind ak = db.GetMsAccidentKind(data.accident_kind_id);
                if (ak != null)
                {
                    this.Grid[pos, i].Value = ak.ToString();
                }
                pos++;

                //事故分類
                MsKindOfAccident ks = db.GetMsKindOfAccident(data.kind_of_accident_id);
                if (ks != null)
                {
                    this.Grid[pos, i].Value = ks.ToString();
                }
                pos++;

                //タイトル
                this.Grid[pos, i].Value = data.title;
                pos++;


                //Status
                MsAccidentStatus ast = db.GetMsAccidentStatus(data.accident_status_id);
                if (ast != null)
                {
                    this.Grid[pos, i].Value = ast.ToString();
                }
                pos++;


                //PIC
                MsUser usr = db.GetMsUser(data.ms_user_id);
                if (usr != null)
                {
                    this.Grid[pos, i].Value = usr.ToString();
                }
                pos++;


                i++;

            }


            return true;
        }

        /// <summary>
        /// 変更があったデータを取得する DcAccident
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override List<T> GetEditCheckList<T>()
        {
            List<T> anslist = new List<T>();

            for (int i = 0; i < this.Grid.Rows.Count; i++)
            {
                //元データ取得
                DcAccident ac = this.Grid[0, i].Value as DcAccident;
                if (ac == null)
                {
                    continue;
                }

                bool flag = (bool)this.Grid[CheckColIndex, i].Value;
                //同じなら問題なし。
                if (ac.CheckComplete() == flag)
                {
                    continue;
                }

                //違った場合、値をセットして取得対象へ
                if (flag == true)
                {
                    ac.accident_status_id = (int)EAccidentStatus.Complete;
                }
                else
                {
                    ac.accident_status_id = (int)EAccidentStatus.Pending;
                }


                //ADDする
                T a = (T)(object)ac;
                anslist.Add(a);


            }

            return anslist;
        }
    }
}
