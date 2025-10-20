using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;
using NBaseData.BLC;

namespace Yojitsu.DA
{
    class DirectAccessorFactory : DbAccessorFactory
    {
        public override List<NBaseData.DAC.MsVessel> MsVessel_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsVessel.GetRecordsByYojitsuEnabled(loginUser);
        }
        
        public override List<NBaseData.DAC.MsVesselType> MsVesselType_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsVesselType.GetRecords(loginUser);
        }

        public override List<BgYosanHead> BgYosanHead_GetRecords(MsUser loginUser)
        {
            return BgYosanHead.GetRecords(loginUser);
        }

        public override List<MsBumon> MsBumon_GetRecords(MsUser loginUser)
        {
            return MsBumon.GetRecords(loginUser);
        }

        public override List<MsHimoku> MsHimoku_GetRecordsWithMsKamoku(MsUser loginUser)
        {
            return MsHimoku.GetRecordsWithMsKamoku(loginUser);
        }

        public override bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            return 予実.BLC_予算作成(loginUser, yosanHead, kadouVessels);
        }

        public override bool BLC_予算Fix(MsUser loginUser, BgYosanHead yosanHead)
        {
            return yosanHead.UpdateRecord(loginUser);
        }

        public override bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead)
        {
            return 予実.BLC_予算Revアップ(loginUser, yosanHead);
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_年単位_船(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

        public override List<MsUserBumon> MsUserBumon_GetRecordsByUserID(MsUser msUser, string msUserId)
        {
            return MsUserBumon.GetRecordsByUserID(msUser, msUserId);
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_月単位(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

        public override bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> yosanItems, BgNrkKanryou nrkKanryou)
        {
            return 予実.BLC_予算保存(loginUser, yosanItems, nrkKanryou);
        }

        public override List<BgNrkKanryou> BgNrkKanryou_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            return BgNrkKanryou.GetRecordsByYosanHeadID(loginUser, yosanHeadId);
        }

        public override bool BgNrkKanryou_UpdateRecord(MsUser loginUser, BgNrkKanryou k)
        {
            return k.UpdateRecord(loginUser);
        }

        public override List<BgYosanMemo> BgYosanMemo_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            return BgYosanMemo.GetRecordsByYosanHeadID(loginUser, yosanHeadId);
        }

        public override bool BgYosanMemo_UpdateRecord(MsUser loginUser, BgYosanMemo yosanMemo)
        {
            return yosanMemo.UpdateRecord(loginUser);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
            return BgJiseki.GetRecords_年単位_船(loginUser, msVesselId, jisekiDateStart, jisekiDateEnd);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
            return BgJiseki.GetRecords_月単位_船(loginUser, msVesselId, jisekiDateStart, jisekiDateEnd);
        }

        public override List<BgRate> BgRate_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            return BgRate.GetRecordsByYosanHeadID(loginUser, yosanHeadId);
        }

        public override bool BgRate_UpdateRecords(MsUser loginUser, List<BgRate> rates)
        {
            return BgRate.UpdateRecords(loginUser, rates);
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_年単位_全社(loginUser, yosanHeadId, yearStart, yearEnd);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            return BgJiseki.GetRecords_年単位_全社(loginUser, yearStart, yearEnd);
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_年単位_グループ(loginUser, yosanHeadId, msVesselTypeId, yearStart, yearEnd);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
            return BgJiseki.GetRecords_年単位_グループ(loginUser, msVesselTypeId, yearStart, yearEnd);
        }



        public override BgHankanhi BgHankanhi_GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year)
        {
            return BgHankanhi.GetRecordByYosanHeadIDYear(loginUser, headid, year);
        }

        public override BgYosanItem BgYosanItem_GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid) 
        {
            return BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, yosanheadid, year, himokuid, vesselid);
        }


        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
        {
            return BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselID(loginUser, yosanHeadId, msVesselId);
        }


		//予算種別取得
		public override List<BgYosanSbt> BgYosanSbt_GetRecords(MsUser loginUser)
		{
			return BgYosanSbt.GetRecords(loginUser);
		}
		//部門を指定してデータを取得する
		public override List<MsHimoku> MsHimoku_GetRecordsByMsBumonID(MsUser loginUser, int bumonid)
		{
			return MsHimoku.GetRecordsByMsBumonID(loginUser, bumonid);
		}

		//船種別ごとの合計を費目別に取得
		public override List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid)
		{
			return BgYosanItem.GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
				loginUser, yosanheadid, start, end, vesseltype, bumonid);
		}

		//実績を船種別ごとの合計を費目別に取得
		public override List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid)
		{
			return BgJiseki.GetRecordsByVesselTypePriodBumonHimokus
				(loginUser, vesseltype, start, end, bumonid);
		}



		//予算頭と年を指定して稼動を取得する
        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
            MsUser loginUser, int yosanhead, int yearStart, int yearEnd)
        {
            return BgKadouVessel.GetRecordsByYosanHeadAndYearRange(loginUser, yosanhead, yearStart, yearEnd);
        }
		//全費目の取得
		public override List<MsHimoku> MsHimoku_GetRecords(MsUser loginUser)
		{
			return MsHimoku.GetRecords(loginUser);
		}

		//計画を船種別ごとの合計を費目別に取得(全部)
		public override List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype)
		{
			return BgYosanItem.GetRecordsByYosanHeadPriodVesselTypeHimokus(
				loginUser, yosanheadid, start, end, vesseltype);
			
		}


		//実績を船種別ごとの合計を費目別に取得(全部)
		public override List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end)
		{
			return BgJiseki.GetRecordsByVesselTypePriodHimokus(
				loginUser, vesseltype, start, end);
		}



		//全部の船を取得する
		public override List<MsVessel> MsVessel_GetRecordsAll(MsUser loginUser)
		{
			return MsVessel.GetRecords(loginUser);
		}


        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_月単位_グループ(loginUser, yosanHeadId,
                msVesselTypeId, yearStart, yearEnd);
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_月単位_全社(
                loginUser, yosanHeadId, yearStart, yearEnd);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
            return BgJiseki.GetRecords_月単位_グループ(loginUser, msVesselTypeId,
                yearStart, yearEnd);
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            return BgJiseki.GetRecords_月単位_全社(loginUser, yearStart, yearEnd);
        }


        public override bool BgKadouVessel_UpdateRecords(MsUser loginUser, List<BgKadouVessel> list)
        {
            return BgKadouVessel.UpdateRecords(loginUser, list);
        }

        public override bool BgYosanItem_UpdateRecords(MsUser loginUser, List<BgYosanItem> yosanItems)
        {
            return BgYosanItem.UpdateRecords(loginUser, yosanItems);
        }

        public override bool BgHankanhi_InsertRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            return hankanhi.InsertRecord(loginUser);
        }

        public override bool BgHankanhi_UpdateRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            return hankanhi.UpdateRecord(loginUser);
        }

        public override bool BLC_販管費保存(MsUser loginUser, int year, BgYosanHead bgYosanHead, int eigyo, int kanri, int nenkan, int keiei, List<int> msVesselIds, List<decimal> amounts)
        {
            return 予実.BLC_販管費保存(loginUser, year, bgYosanHead, eigyo, kanri, nenkan, keiei, msVesselIds, amounts);
        }

        public override BgUnkouhi BgUnkouhi_GetRecord(MsUser loginUser, int yosanHeadId, int msVesselId, int year)
        {
            return BgUnkouhi.GetRecordByYosanHeadIdAndMsVesselIdAndYear(loginUser, yosanHeadId, msVesselId, year);
        }

        public override bool BgUnkouhi_UpdateRecord(MsUser loginUser, BgUnkouhi unkouhi)
        {
            return unkouhi.UpdateRecord(loginUser);
        }

        public override bool BLC_運航費保存(MsUser loginUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy)
        {
            return 予実.BLC_運航費保存(loginUser, yosanHeadId, msVesselId, year, unkouhi, doCopy);
        }

        public override List<BgUchiwakeYosanItem> BgUchiwakeYosanItem_GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            return BgUchiwakeYosanItem.GetRecords_入渠(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

        public override BgVesselYosan BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser loginUser, int year, int yosanHeadId, int msVesselId)
        {
            return BgVesselYosan.GetRecordByYearAndYosanHeadIdAndMsVesselId(loginUser, year, yosanHeadId, msVesselId);
        }

        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            return BgKadouVessel.GetRecordsByYosanHeadID(loginUser, yosanHeadId);
        }

        public override BgYosanBikou BgYosanBikou_GetRecoreByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
        {
            return BgYosanBikou.GetRecordByYosanHeadIDAndMsVesselID(loginUser, yosanHeadId, msVesselId);
        }

        public override bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou)
        {
            return 予実.BLC_修繕費保存(loginUser, uchiwakeYosanItems, yosanBikou);
        }

        public override BgYosanExcel BgYosanExcel_GetRecordsByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu)
        {
            return BgYosanExcel.GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(loginUser, yosanHeadId, msVesselId, shubetsu);
        }

        public override bool BgYosanExcel_InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel)
        {
            return BgYosanExcel.InsertOrUpdate(loginUser, yosanExcel);
        }

        public override BgYosanHead BgYosanHead_GetRecordByYear(MsUser loginUser, string year)
        {
            return BgYosanHead.GetRecordByYear(loginUser, year);
        }

        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadId, int msVesselId, int yearStart, int yearEnd)
        {
            return BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

        public override bool BLC_実績取込(MsUser loginUser)
        {
            return 予実.BLC_実績取込(loginUser);
        }

        //public override BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser, int yosanSbtId)
        public override BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser)
        {
            //return BgYosanHead.GetRecord_直近(loginUser, yosanSbtId);
            return BgYosanHead.GetRecord_直近(loginUser);
        }
    }
}
