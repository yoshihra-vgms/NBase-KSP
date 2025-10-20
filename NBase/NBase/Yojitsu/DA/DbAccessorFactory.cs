using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace Yojitsu.DA
{
	public abstract class DbAccessorFactory
	{
		public static DbAccessorFactory FACTORY;


		public static void InitFactory(string scheme)
		{
			if (scheme == "wcf")
			{
				FACTORY = new WcfAccessorFactory();
			}
			else if (scheme == "direct")
			{
				FACTORY = new DirectAccessorFactory();
			}
		}

		public abstract List<MsVessel> MsVessel_GetRecords(MsUser loginUser);

		public abstract List<MsVesselType> MsVesselType_GetRecords(MsUser loginUser);

		public abstract List<BgYosanHead> BgYosanHead_GetRecords(MsUser loginUser);

		public abstract List<MsBumon> MsBumon_GetRecords(MsUser loginUser);

		public abstract List<MsHimoku> MsHimoku_GetRecordsWithMsKamoku(MsUser loginUser);

		public abstract bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels);

		public abstract bool BLC_予算Fix(MsUser loginUser, BgYosanHead yosanHead);

		public abstract bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead);

		public abstract List<BgYosanItem> BgYosanItem_GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);

		public abstract List<BgYosanItem> BgYosanItem_GetRecords_月単位(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);

		public abstract List<MsUserBumon> MsUserBumon_GetRecordsByUserID(MsUser loginUser, string msUserId);

		public abstract bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> list, BgNrkKanryou nrkKanryou);

		public abstract List<BgNrkKanryou> BgNrkKanryou_GetRecordsByYosanHeadID(MsUser msUser, int p);

		public abstract bool BgNrkKanryou_UpdateRecord(MsUser loginUser, BgNrkKanryou k);

		public abstract List<BgYosanMemo> BgYosanMemo_GetRecordsByYosanHeadID(MsUser loginUser, int p);

		public abstract bool BgYosanMemo_UpdateRecord(MsUser loginUser, BgYosanMemo editedYosanMemo);

		public abstract List<BgJiseki> BgJiseki_GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd);

		public abstract List<BgJiseki> BgJiseki_GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd);

		public abstract List<BgRate> BgRate_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId);

		public abstract bool BgRate_UpdateRecords(MsUser loginUser, List<BgRate> rates);

		public abstract List<BgYosanItem> BgYosanItem_GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd);

		public abstract List<BgJiseki> BgJiseki_GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd);

		public abstract List<BgYosanItem> BgYosanItem_GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd);

		public abstract List<BgJiseki> BgJiseki_GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd);


		public abstract BgHankanhi BgHankanhi_GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year);



		public abstract BgYosanItem BgYosanItem_GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid);


        public abstract List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId);


        public abstract List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadId, int msVesselId, int yearStart, int yearEnd);


        //予算種別取得
		public abstract List<BgYosanSbt> BgYosanSbt_GetRecords(MsUser loginUser);


		//部門を指定してデータを取得する
		public abstract List<MsHimoku> MsHimoku_GetRecordsByMsBumonID(MsUser loginUser, int bumonid);


		//計画を船種別ごとの合計を費目別に取得(部門指定)
		public abstract List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid);

		//実績を船種別ごとの合計を費目別に取得(部門指定)
		public abstract List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid);

		//予算頭と年を指定して稼動を取得する
        public abstract List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
            MsUser loginUser, int yosanhead, int startYear, int endYear);

		//全費目の取得
		public abstract List<MsHimoku> MsHimoku_GetRecords(MsUser loginUser);


		//計画を船種別ごとの合計を費目別に取得(全部)
		public abstract List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype);


		//実績を船種別ごとの合計を費目別に取得(全部)
		public abstract List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end);


		//全部の船を取得する
		public abstract List<MsVessel> MsVessel_GetRecordsAll(MsUser loginUser);


        public abstract List<BgYosanItem> BgYosanItem_GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd);

        public abstract List<BgYosanItem> BgYosanItem_GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd);

        public abstract List<BgJiseki> BgJiseki_GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd);

        public abstract List<BgJiseki> BgJiseki_GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd);

        public abstract bool BgKadouVessel_UpdateRecords(MsUser loginUser, List<BgKadouVessel> list);

        public abstract bool BgYosanItem_UpdateRecords(MsUser msUser, List<BgYosanItem> yosanItems);

        public abstract bool BgHankanhi_InsertRecord(MsUser msUser, BgHankanhi hankanhi);

        public abstract bool BgHankanhi_UpdateRecord(MsUser msUser, BgHankanhi hankanhi);

        public abstract bool BLC_販管費保存(MsUser msUser, int year, BgYosanHead bgYosanHead, int p, int p_5, int p_6, int p_7, List<int> msVesselIds, List<decimal> amounts);

        public abstract BgUnkouhi BgUnkouhi_GetRecord(MsUser loginUser, int yosanHeadId, int msVesselId, int year);

        public abstract bool BgUnkouhi_UpdateRecord(MsUser msUser, BgUnkouhi unkouhi);

        public abstract bool BLC_運航費保存(MsUser msUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy);

        public abstract List<BgUchiwakeYosanItem> BgUchiwakeYosanItem_GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);

        public abstract BgVesselYosan BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser msUser, int year, int yosanHeadId, int msVesselId);

        public abstract List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadID(MsUser msUser, int yosanHeadId);

        public abstract BgYosanBikou BgYosanBikou_GetRecoreByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId);

        public abstract bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou);

        public abstract BgYosanExcel BgYosanExcel_GetRecordsByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu);

        public abstract bool BgYosanExcel_InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel);

        public abstract BgYosanHead BgYosanHead_GetRecordByYear(MsUser loginUser, string year);

        public abstract bool BLC_実績取込(MsUser loginUser);

        //public abstract BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser, int yosanSbtId);
        public abstract BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser);
    }
}

