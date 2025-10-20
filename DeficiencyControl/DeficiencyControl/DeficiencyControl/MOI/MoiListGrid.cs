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

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船一覧グリッド
    /// </summary>
    public class MoiListGrid : BaseEditCheckGrid
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dv"></param>
        public MoiListGrid(DataGridView dv)
            : base(dv)
        {
        }



        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="objlist">List MoiData</param>
        /// <returns></returns>
        public override bool DispData(object objlist)
        {
            List<MoiData> datalist = objlist as List<MoiData>;
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
            foreach (MoiData moidata in datalist)
            {

                DcMoi data = moidata.Header.Moi;
                DcMoiObservation obs = moidata.Observation.Observation;

                int pos = 0;

                //data
                this.Grid[pos, i].Value = moidata;
                pos++;

                //青モード
                this.Grid[pos, i].Value = obs.CheckComplete();
                pos++;


                //No
                //this.Grid[pos, i].Value = (i + 1);
                this.Grid[pos, i].Value = obs.moi_observation_id;
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


                //検船種別
                MsInspectionCategory ins = db.GetMsInspectionCategory(data.inspection_category_id);
                if (ins != null)
                {
                    this.Grid[pos, i].Value = ins.ToString();
                }
                pos++;


                //検船会社
                MsCustomer incu = db.GetMsCustomerInspection(data.inspection_ms_customer_id);
                if (incu != null)
                {
                    this.Grid[pos, i].Value = incu.ToString();
                }
                pos++;


                //検船員
                this.Grid[pos, i].Value = data.inspection_name;
                pos++;


                //VIQNo
                MsViqNo vno = db.GetMsViqNo(obs.viq_no_id);
                if (vno != null)
                {
                    this.Grid[pos, i].Value = vno.ToString();
                }
                pos++;

                //指摘事項
                this.Grid[pos, i].Value = obs.observation;
                pos++;

                //状態
                MsMoiStatus mst = db.GetMsMoiStatus(obs.moi_status_id);
                if (mst != null)
                {
                    this.Grid[pos, i].Value = mst.ToString();
                }
                pos++;

                //---------------------

                i++;

            }


            return true;
        }

        /// <summary>
        /// チェックが変更された物体を取得する MoiData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override List<T> GetEditCheckList<T>()
        {
            List<T> anslist = new List<T>();

            for (int i = 0; i < this.Grid.Rows.Count; i++)
            {
                //元データ取得
                MoiData moidata = this.Grid[0, i].Value as MoiData;
                if (moidata == null)
                {
                    continue;
                }

                bool flag = (bool)this.Grid[CheckColIndex, i].Value;
                //同じなら問題なし。
                if (moidata.Observation.Observation.CheckComplete() == flag)
                {
                    continue;
                }

                //違った場合、値をセットして取得対象へ
                if (flag == true)
                {
                    moidata.Observation.Observation.moi_status_id = (int)EMoiStatus.Complete;
                }
                else
                {
                    moidata.Observation.Observation.moi_status_id = (int)EMoiStatus.Pending;
                }


                //ADDする
                T a = (T)(object)moidata;
                anslist.Add(a);


            }

            return anslist;
        }
    }
}
