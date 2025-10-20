using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Yojitsu.DA;
using NBaseUtil;

namespace Yojitsu.util
{
    public class 販菅費処理
    {
        /// <summary>
        /// 販菅費のデータを検索する
        /// </summary>
        /// <returns></returns>
		public static List<販菅費TreeListData> Search販菅費Data(BgYosanHead head, int year)
        {
            List<販菅費TreeListData> datalist = new List<販菅費TreeListData>();

            //大本のMsVesselを取得する
            List<MsVessel> vessellist =
                DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);

			//稼動船を取得する
			List<BgKadouVessel> kadoulist =
				DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
				NBaseCommon.Common.LoginUser, head.YosanHeadID, year, year);

            //データをまわして変換する
            foreach( MsVessel vessel in vessellist)
            {
                販菅費TreeListData data = new 販菅費TreeListData();

                //設定できるデータを設定する
                data.船名 = vessel.VesselName;
                data.DWT = vessel.DWT;

                data.VesselData = vessel;

				//自分の稼動を探す
				foreach (BgKadouVessel kadou in kadoulist)
				{
					//IDが一致した時
					if (kadou.MsVesselID == vessel.MsVesselID)
					{
						//関係稼動を一応保存
						data.KadouVessel = kadou;

						data.稼動率 = util.販菅費処理.稼働率計算(kadou);
					}
				}
                

                //add
                datalist.Add(data);

            }


            return datalist;
        }

        

        /// <summary>
        /// 割り掛け計算処理
        /// 引数：営業基礎割り掛け、管理基礎割り掛け、年度販菅費、年度経営指導料、計算リスト、総計データ元
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public static 販菅費TreeListData 割り掛け処理( int eigyo, int kanri, int nekan, int keiei,
            ref List<販菅費TreeListData> datalist, 販菅費TreeListData soukei)
        {
            //営業、管理データを設定する
            販菅費処理.Set営業基礎管理基礎(ref datalist, eigyo, kanri);

            //総計計算①
            販菅費処理.Calcu総計(datalist, ref soukei);

            //全行計算
            for( int i=0; i<datalist.Count; i++)
            {
                販菅費処理.割り掛け計算(datalist[i], soukei, nekan);
            }


            //総計計算②
            販菅費処理.Calcu総計(datalist, ref soukei);

            //割合を求める
            for( int i=0; i<datalist.Count; i++)
            {
                if (Convert.ToDecimal(soukei.計) == 0.0m)
                {
                    datalist[i].総額に対する割合 = 0.0m;
                    continue;
                }

                datalist[i].総額に対する割合 =
                    Convert.ToDecimal(datalist[i].計) / Convert.ToDecimal(soukei.計);
            }


            return soukei;
        }



        /// <summary>
        /// 割り掛けの保存処理
        /// 引数：年、関連頭、営業割り掛け、管理割り掛け、販菅
        /// 返り値：成功したか？
        /// </summary>
        /// <returns></returns>
        public static bool 割り掛けの保存(int year, BgYosanHead head, int eigyo, int kanri, int nekan)
        {
            BgHankanhi hankanhi = null;

            //今回のデータを取得する
            hankanhi = DbAccessorFactory.FACTORY.BgHankanhi_GetRecordByYosanHeadIDYear(NBaseCommon.Common.LoginUser, head.YosanHeadID, year);


            //無い時は新たにinsertする
            if (hankanhi == null)
            {
                hankanhi = new BgHankanhi();

				#region 挿入データ作成
				hankanhi.YosanHeadID = head.YosanHeadID;
                hankanhi.Year = year;

                hankanhi.EigyoKiso = eigyo;
                hankanhi.KanriKiso = kanri;
                hankanhi.NendoHankanhi = nekan;
                hankanhi.RenewDate = DateTime.Now;
                hankanhi.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                hankanhi.Ts = "0";
				#endregion

				DbAccessorFactory.FACTORY.BgHankanhi_InsertRecord(NBaseCommon.Common.LoginUser, hankanhi);

            }
            //あるならUpDateを掛ける
            else
			{
				#region 更新データ作成
				hankanhi.YosanHeadID = head.YosanHeadID;
                hankanhi.Year = year;

                hankanhi.EigyoKiso = eigyo;
                hankanhi.KanriKiso = kanri;
                hankanhi.NendoHankanhi = nekan;
				#endregion
                DbAccessorFactory.FACTORY.BgHankanhi_UpdateRecord(NBaseCommon.Common.LoginUser, hankanhi);
            }

            return true;
        }


        /// <summary>
        /// 販菅費の保存処理
        /// 船ごとのデータを保存する
        /// 引数：保存する年、関連頭、保存リスト
        /// 返り値：成功したか？
        /// </summary>
        /// <param name="year"></param>
        /// <param name="head"></param>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public static bool 販菅費の保存(int year, BgYosanHead head, List<販菅費TreeListData> datalist)
        {
            List<BgYosanItem> yosanItems = new List<BgYosanItem>();
            
            //保存データ分だけまわす
            foreach (販菅費TreeListData data in datalist)
            {
                //データを取得する
                //BgYosanItem yosanitem = DbAccessorFactory.FACTORY.BgYosanItem_GetRecordByYearHimokuIDMsVesselID(NBaseCommon.Common.LoginUser,
                //    head.YosanHeadID, year, DA.Constants.MS_HIMOKU_ID_人件費, data.VesselData.MsVesselID);
                BgYosanItem yosanitem = DbAccessorFactory.FACTORY.BgYosanItem_GetRecordByYearHimokuIDMsVesselID(NBaseCommon.Common.LoginUser,
                    head.YosanHeadID, year, DA.Constants.MS_HIMOKU_ID_販管費, data.VesselData.MsVesselID);

                //無かった
                if (yosanitem == null)
                {
                    continue;
                }

                //データを入れる
                yosanitem.Amount = data.計;
                yosanItems.Add(yosanitem);
            }

            //アップデート
            DbAccessorFactory.FACTORY.BgYosanItem_UpdateRecords(NBaseCommon.Common.LoginUser, yosanItems);

            return true;
        }


        //*****************************************************************
        //private**********************************************************

        /// <summary>
        /// /営業基礎割掛と管理基礎割掛をリストに設定する
        /// 引数：設定するリスト、営業基礎、管理基礎
        /// 返り値：なし
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="eigyo"></param>
        /// <param name="kanri"></param>
        private static void Set営業基礎管理基礎(ref List<販菅費TreeListData> datalist, int eigyo, int kanri)
		{
			foreach (販菅費TreeListData data in datalist)
            {
                data.DWT_割掛 = data.DWT * data.稼動率;

                if (data.KadouVessel == null)
                {
                    data.営業基礎割掛 = 0;
                    data.管理基礎割掛 = 0;
                }
                else
                {
                    if (data.KadouVessel.EigyouKisoFlag == 0)
                    {
                        data.営業基礎割掛 = Convert.ToDecimal(eigyo) * data.稼動率;
                    }
                    else
                    {
                        data.営業基礎割掛 = 0;
                    }

                    if (data.KadouVessel.KanriKisoFlag == 0)
                    {
                        data.管理基礎割掛 = Convert.ToDecimal(kanri) * data.稼動率;
                    }
                    else
                    {
                        data.管理基礎割掛 = 0;
                    }
                }
            }
        }


        /// <summary>
        /// リストの総計を計算する
        /// 引数：計算するリスト、計算場所
        /// 返り値：なし
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="ans"></param>
        private static void Calcu総計(List<販菅費TreeListData> datalist, ref 販菅費TreeListData ans)
        {
            ans.SetZero();

            foreach (販菅費TreeListData data in datalist)
            {
                ans.DWT_割掛 += data.DWT_割掛;
                ans.営業基礎割掛 += data.営業基礎割掛;
                ans.管理基礎割掛 += data.管理基礎割掛;
                ans.差額DWT割掛 += data.差額DWT割掛;
                ans.計 += data.計;
                //ans.総額に対する割合 += data.総額に対する割合;
                
            }           

        }

        /// <summary>
        /// 一列のデータを計算する
        /// 引数：計算列データ、総計、年販菅費
        /// </summary>
        /// <param name="caldata"></param>
        /// <param name="soukei"></param>
        /// <param name="nenkan"></param>
        private static void 割り掛け計算(販菅費TreeListData caldata, 販菅費TreeListData soukei, int nenkan)
        {

            //差額DWT割掛
			caldata.差額DWT割掛 = (nenkan - soukei.営業基礎割掛 - soukei.管理基礎割掛);
            caldata.差額DWT割掛 *= caldata.DWT_割掛;
            caldata.差額DWT割掛 /= soukei.DWT_割掛;


            //計
            caldata.計 = caldata.営業基礎割掛 + caldata.管理基礎割掛 + caldata.差額DWT割掛;

        }


        private static decimal 稼働率計算(BgKadouVessel kv)
        {
            // 2013.03 : 稼働率の考え方を下記のようにする（販管費について）
            //           １日でも稼働日があれば、1ヶ月と考える

            DateTime start = kv.KadouStartDate;
            DateTime end = kv.KadouEndDate;

            //decimal startMonthRatio = Math.Round((30.0m - start.Day + 1.0m) / 30.0m, 1, MidpointRounding.AwayFromZero);
            //decimal endMonthRatio = Math.Round(end.Day / 30.0m, 1, MidpointRounding.AwayFromZero);

            //decimal result = (startMonthRatio + (DateTimeUtils.月数(start, end) - 2) + endMonthRatio) / 12;

            int months = DateTimeUtils.月数(start, end);
            decimal result = months / 12m;

            return result;
        }    
    }



    //////////////////////////////////////////////////////////////////////

    //販菅費データ表示用
    public class 販菅費TreeListData
    {
        public 販菅費TreeListData()
        {
        }

        public 販菅費TreeListData(string name, bool flag, Color col)
        {
            this.船名 = name;
            this.SpecialFlag = flag;
            this.SpecialColor = col;

        }

        /// <summary>
        /// 中身0で初期化する
        /// </summary>
        public void SetZero()
        {
            this.DWT = 0.0m;
            this.DWT_割掛 = 0.0m;
            this.営業基礎割掛 = 0.0m;
			this.管理基礎割掛 = 0.0m;
			this.差額DWT割掛 = 0.0m;
			this.計 = 0.0m;
            this.総額に対する割合 = 0.0m;

        }

        public string 船名 = "";

        public decimal DWT = 0.0m;
        public decimal DWT_割掛 = 0.0m;
        public decimal 営業基礎割掛 = 0;
		public decimal 管理基礎割掛 = 0;
		public decimal 差額DWT割掛 = 0;
		public decimal 計 = 0;
        public decimal 総額に対する割合 = 0.0m;

        /// <summary>
		/// 自分の関連するMsVessel
        /// </summary>
        public MsVessel VesselData = null;

		/// <summary>
		/// 自分に関係するBgKadouVessel
		/// </summary>
		public BgKadouVessel KadouVessel = null;

		/// <summary>
		/// 稼働率
		/// </summary>
		public decimal 稼動率;

        /// <summary>
        /// 色を変えるかどうか？
        /// </summary>
        public bool SpecialFlag = false;

        /// <summary>
        /// 変更する時の色
        /// </summary>
        public Color SpecialColor = Color.YellowGreen;

    
    }
}
