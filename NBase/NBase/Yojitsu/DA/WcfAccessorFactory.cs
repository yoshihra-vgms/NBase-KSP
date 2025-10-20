using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Yojitsu;
using ORMapping;

namespace Yojitsu.DA
{
    class WcfAccessorFactory : DbAccessorFactory
    {
        public override List<MsVessel> MsVessel_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsVessel_GetRecordsByYojitsuEnabled(loginUser);
            }
        }

        public override List<MsVesselType> MsVesselType_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsVesselType_GetRecords(loginUser);
            }
        }

        public override List<BgYosanHead> BgYosanHead_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanHead_GetRecords(loginUser);
            }
        }

        public override List<MsBumon> MsBumon_GetRecords(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.MsBumon_GetRecords(loginUser);
            }
        }

        public override List<MsHimoku> MsHimoku_GetRecordsWithMsKamoku(MsUser loginUser)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.MsHimoku_GetRecordsWithMsKamoku(loginUser);
			}
        }

        public override bool BLC_予算作成(MsUser loginUser, BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_予算作成(loginUser, yosanHead, kadouVessels);
            }
        }

        public override bool BLC_予算Fix(MsUser loginUser, BgYosanHead yosanHead)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanHead_UpdateRecord(loginUser, yosanHead);
            }
        }

        public override bool BLC_予算Revアップ(MsUser loginUser, BgYosanHead yosanHead)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_予算Revアップ(loginUser, yosanHead);
            }
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanItem_GetRecords_年単位_船(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
            }
        }

        public override List<MsUserBumon> MsUserBumon_GetRecordsByUserID(MsUser msUser, string msUserId)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.MsUserBumon_GetRecordsByUserIDList(msUser, msUserId);
			}
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位(MsUser msUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecords_月単位(msUser, yosanHeadId, msVesselId, yearStart, yearEnd);
			}
        }

        public override bool BLC_予算保存(MsUser loginUser, List<BgYosanItem> list, BgNrkKanryou nrkKanryou)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BLC_予算保存(loginUser, list, nrkKanryou);
			}
        }

        public override List<BgNrkKanryou> BgNrkKanryou_GetRecordsByYosanHeadID(MsUser msUser, int p)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgNrkKanryou_GetRecordsByYosanHeadID(msUser, p);
			}
        }

        public override bool BgNrkKanryou_UpdateRecord(MsUser msUser, BgNrkKanryou k)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgNrkKanryou_UpdateRecord(msUser, k);
			}
        }

        public override List<BgYosanMemo> BgYosanMemo_GetRecordsByYosanHeadID(MsUser msUser, int p)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanMemo_GetRecordsByYosanHeadID(msUser, p);
			}
        }

        public override bool BgYosanMemo_UpdateRecord(MsUser msUser, BgYosanMemo editedYosanMemo)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanMemo_UpdateRecord(msUser, editedYosanMemo);
			}
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecords_年単位_船(loginUser, msVesselId,
					jisekiDateStart, jisekiDateEnd);
			}
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecords_月単位_船(loginUser, msVesselId,
					jisekiDateStart, jisekiDateEnd);
			}
        }

        public override List<BgRate> BgRate_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgRate_GetRecordsByYosanHeadID(loginUser, yosanHeadId);
			}
        }

        public override bool BgRate_UpdateRecords(MsUser loginUser, List<BgRate> rates)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgRate_UpdateRecords(loginUser, rates);
			}
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecords_年単位_全社(loginUser, yosanHeadId, yearStart, yearEnd);
			}
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecords_年単位_全社(loginUser, yearStart, yearEnd);
			}
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecords_年単位_グループ(
					loginUser, yosanHeadId, msVesselTypeId, yearStart, yearEnd);
			}
        }

        public override List<BgJiseki> BgJiseki_GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecords_年単位_グループ(
					loginUser, msVesselTypeId, yearStart, yearEnd);
			}
        }

        public override BgHankanhi BgHankanhi_GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgHankanhi_GetRecordByYosanHeadIDYear(
					loginUser, headid, year);
			}
        }

        public override BgYosanItem BgYosanItem_GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecordByYearHimokuIDMsVesselID(
					loginUser, yosanheadid, year, himokuid, vesselid);
			}
        }


        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
        {
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID
					(loginUser, yosanHeadId, msVesselId);
			}
        }


		//予算種別取得
		public override List<BgYosanSbt> BgYosanSbt_GetRecords(MsUser loginUser)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanSbt_GetRecords(loginUser);
			}
		}

		//部門を指定してデータを取得する
		public override List<MsHimoku> MsHimoku_GetRecordsByMsBumonID(MsUser loginUser, int bumonid)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.MsHimoku_GetRecordsByMsBumonID(loginUser, bumonid);
			}
		}

		//船種別ごとの合計を費目別に取得
		public override List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus
					(loginUser, yosanheadid, start, end, vesseltype, bumonid);
			}
		}


		//実績を船種別ごとの合計を費目別に取得
		public override List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecordsByVesselTypePriodBumonHimokus
					(loginUser, vesseltype, start, end, bumonid);
			}
		}

		//予算頭と年を指定して稼動を取得する
		public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
			MsUser loginUser, int yosanhead, int yearStart, int yearEnd)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgKadouVessel_GetRecordsByYosanHeadAndYearRange
					(loginUser, yosanhead, yearStart, yearEnd);
			}
		}


		//全費目の取得
		public override List<MsHimoku> MsHimoku_GetRecords(MsUser loginUser)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.MsHimoku_GetRecords(loginUser);
			}
		}

		//計画を船種別ごとの合計を費目別に取得(全部)
		public override List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus
					(loginUser, yosanheadid, start, end, vesseltype);
			}
		}

		//実績を船種別ごとの合計を費目別に取得(全部)
		public override List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.BgJiseki_GetRecordsByVesselTypePriodHimokus
					(loginUser, vesseltype, start, end);
			}
		}

		//全部の船を取得する
		public override List<MsVessel> MsVessel_GetRecordsAll(MsUser loginUser)
		{
			using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
			{
				return serviceClient.MsVessel_GetRecords(loginUser);
			}
		}


        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanItem_GetRecords_月単位_グループ(
                    loginUser, yosanHeadId, msVesselTypeId, yearStart, yearEnd);
            }            
        }

        public override List<BgYosanItem> BgYosanItem_GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanItem_GetRecords_月単位_全社(
                    loginUser, yosanHeadId, yearStart, yearEnd);
            }
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgJiseki_GetRecords_月単位_グループ(
                    loginUser, msVesselTypeId, yearStart, yearEnd);
            }
        }

        public override List<BgJiseki> BgJiseki_GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgJiseki_GetRecords_月単位_全社(
                    loginUser, yearStart, yearEnd);
            }
        }

        public override bool BgKadouVessel_UpdateRecords(MsUser loginUser, List<BgKadouVessel> list)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgKadouVessel_UpdateRecords(loginUser, list);
            }
        }

        public override bool BgYosanItem_UpdateRecords(MsUser loginUser, List<BgYosanItem> yosanItems)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanItem_UpdateRecords(loginUser, yosanItems);
            }
        }

        public override bool BgHankanhi_InsertRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgHankanhi_InsertRecord(loginUser, hankanhi);
            }
        }

        public override bool BgHankanhi_UpdateRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgHankanhi_UpdateRecord(loginUser, hankanhi);
            }
        }

        public override bool BLC_販管費保存(MsUser loginUser, int year, BgYosanHead bgYosanHead, int eigyo, int kanri, int nenkan, int keiei, List<int> msVesselIds, List<decimal> amounts)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_販管費保存(loginUser, year, bgYosanHead, eigyo, kanri, nenkan, keiei, msVesselIds, amounts);
            }
        }

        public override BgUnkouhi BgUnkouhi_GetRecord(MsUser loginUser, int yosanHeadId, int msVesselId, int year)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgUnkouhi_GetRecord(loginUser, yosanHeadId, msVesselId, year);
            }
        }

        public override bool BgUnkouhi_UpdateRecord(MsUser loginUser, BgUnkouhi unkouhi)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgUnkouhi_UpdateRecord(loginUser, unkouhi);
            }
        }

        public override bool BLC_運航費保存(MsUser loginUser, int yosanHeadId, int msVesselId, int year, BgUnkouhi unkouhi, bool doCopy)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_運航費保存(loginUser, yosanHeadId, msVesselId, year, unkouhi, doCopy);
            }
        }

        public override List<BgUchiwakeYosanItem> BgUchiwakeYosanItem_GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgUchiwakeYosanItem_GetRecords_入渠(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
            }
        }

        public override BgVesselYosan BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(MsUser loginUser, int year, int yosanHeadId, int msVesselId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(loginUser, year, yosanHeadId, msVesselId);
            }
        }

        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgKadouVessel_GetRecordsByYosanHeadID(loginUser, yosanHeadId);
            }
        }

        public override BgYosanBikou BgYosanBikou_GetRecoreByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanBikou_GetRecordByYosanHeadIDAndMsVesselID(loginUser, yosanHeadId, msVesselId);
            }
        }

        public override bool BLC_修繕費保存(MsUser loginUser, List<BgUchiwakeYosanItem> uchiwakeYosanItems, BgYosanBikou yosanBikou)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_修繕費保存(loginUser, uchiwakeYosanItems, yosanBikou);
            }
        }

        public override BgYosanExcel BgYosanExcel_GetRecordsByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanExcel_GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(loginUser, yosanHeadId, msVesselId, shubetsu);
            }
        }

        public override bool BgYosanExcel_InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanExcel_InsertOrUpdate(loginUser, yosanExcel);
            }
        }

        public override BgYosanHead BgYosanHead_GetRecordByYear(MsUser loginUser, string year)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgYosanHead_GetRecordByYear(loginUser, year);
            }
        }

        public override List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadId, int msVesselId, int yearStart, int yearEnd)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
            }
        }

        public override bool BLC_実績取込(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                return serviceClient.BLC_実績取込(loginUser);
            }
        }

        //public override BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser, int yosanSbtId)
        public override BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //return serviceClient.BgYosanHead_GetRecord_直近(loginUser, yosanSbtId);
                return serviceClient.BgYosanHead_GetRecord_直近(loginUser);
            }
        }
    }
}
