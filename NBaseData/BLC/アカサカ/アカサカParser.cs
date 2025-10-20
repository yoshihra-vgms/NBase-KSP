using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WingData.DAC;
using WingData.DAC.DJ;

namespace WingData.BLC.アカサカ
{
    public class アカサカParser
    {
        public List<アカサカ連携.Info> Infos = new List<アカサカ連携.Info>();
        private string[] SplitParseDatas;

        public アカサカParser(string ParseData)
        {
            //カンマでスプリットする
            SplitParseDatas = ParseData.Split(',');
        }

        public List<DjDousei> Parse(MsUser loginUser, string TargetHonsenID)
        {
            DjDousei[] Douseis = new DjDousei[2];

            if (NAVI_CODE == "")
            {
                return null;
            }
            #region パース開始ログ
            アカサカ連携.Info StartInfo = new アカサカ連携.Info();
            StartInfo.InfoKind = 0;
            StartInfo.Msg = "パース開始 HONSENID:" + TargetHonsenID + " NAVICODE:" + NAVI_CODE;
            Infos.Add(StartInfo);
            #endregion
            //貨物が最大２個なので２回ループする
            for (int i = 0; i < 2; i++)
            {
                string ItemNo = GET_ITEM_NO(ITEM_NAME( i));
                //if (ItemNo != "")
                //{
                    DjDousei dousei;
                    if (ITEM_STATUS(i) == 0)
                    {
                        //積み
                        if (Douseis[0] == null)
                        {
                            dousei = DjDousei.GetRecordByNaviCode(loginUser, NAVI_CODE);
                            if (dousei == null)
                            {
                                #region DOUSEIの新規作成
                                dousei = new DjDousei();

                                dousei.DjDouseiID = Guid.NewGuid().ToString();
                                dousei.NaveCode = NAVI_CODE;
                                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.積みID;
                                #endregion
                            }
                            Douseis[0] = dousei;

                        }
                        else
                        {
                            dousei = Douseis[0];
                        }
                    }
                    else
                    {
                        //揚げ
                        if (Douseis[1] == null)
                        {
                            dousei = DjDousei.GetRecordByNaviCode(loginUser, NAVI_CODE);
                            if (dousei == null)
                            {
                                #region DOUSEIの新規作成
                                dousei = new DjDousei();

                                dousei.DjDouseiID = Guid.NewGuid().ToString();
                                dousei.NaveCode = NAVI_CODE;
                                dousei.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetu.揚げID;
                                #endregion
                            }
                            Douseis[1] = dousei;
                        }
                        else
                        {
                            dousei = Douseis[0];
                        }
                    }

                    #region 船を検索
                    MsVessel msVessel = MsVessel.GetRecordByAkasakaVesselNo(loginUser, HONSENID);
                    if (msVessel == null)
                    {
                        アカサカ連携.Info info = new アカサカ連携.Info();
                        info.InfoKind = 1;
                        info.Msg = "船が見つかりません HONSENID:" + HONSENID;
                        Infos.Add(info);
                        return null;
                    }
                    dousei.MsVesselID = msVessel.MsVesselID;
                    #endregion

                    #region 港を検索
                    MsBasho Basho = MsBasho.GetRecordsByAkasakaPortNo(loginUser, PORTCODE);
                    if (Basho == null)
                    {
                        アカサカ連携.Info info = new アカサカ連携.Info();
                        info.InfoKind = 1;
                        info.Msg = "港が見つかりません PORTCODE:" + PORTCODE;
                        Infos.Add(info);
                        return null;
                    }
                    dousei.MsBashoID = Basho.MsBashoId;
                    #endregion

                    #region 基地を検索
                    if (BERTHCODE != "")
                    {
                        MsBerth berth = MsBerth.GetRecordByBerthCode(loginUser, BERTHCODE);
                        if (berth == null)
                        {
                            アカサカ連携.Info info = new アカサカ連携.Info();
                            info.InfoKind = 1;
                            info.Msg = "バースが見つかりません BERTHCODE:" + BERTHCODE;
                            Infos.Add(info);
                            return null;
                        }
                        dousei.MsKichiID = berth.MsKichiId;
                    }
                    #endregion

                    dousei.PlanNyuko = ToTime(PLAN_ARRIVAL);
                    dousei.PlanChakusan = ToTime(PLAN_ATTACH);
                    dousei.PlanNiyakuStart = ToTime(PLAN_OPE_START);
                    dousei.PlanNiyakuEnd = ToTime(PLAN_OPE_END);
                    dousei.PlanRisan = ToTime(PLAN_DETACH);
                    dousei.PlanShukou = ToTime(PLAN_LEAVE);

                    dousei.ResultNyuko = ToTime(RESULT_ARRIVAL);
                    dousei.ResultChakusan = ToTime(RESULT_ATTACH);
                    dousei.ResultNiyakuStart = ToTime(RESULT_OPE_START);
                    dousei.ResultNiyakuEnd = ToTime(RESULT_OPE_END);
                    dousei.ResultRisan = ToTime(RESULT_DETACH);
                    dousei.ResultShukou = ToTime(RESULT_LEAVE);
                    dousei.VoyageNo = VOYAGENO;
                    dousei.RecordDateTime = RECORD_DATETIME;

                    #region DouseiDate
                    if (dousei.ResultNyuko != "")
                    {
                        try
                        {
                            string ss = RESULT_ARRIVAL.Substring(0, 4) + "/" + RESULT_ARRIVAL.Substring(4, 2) + "/" + RESULT_ARRIVAL.Substring(6, 2);
                            DateTime ret = DateTime.Parse(ss);
                            dousei.DouseiDate = ret;
                        }
                        catch
                        {
                            dousei.DouseiDate = DateTime.MinValue;
                        }

                    }
                    else if (dousei.PlanNyuko != "")
                    {
                        try
                        {
                            string ss = PLAN_ARRIVAL.Substring(0, 4) + "/" + PLAN_ARRIVAL.Substring(4, 2) + "/" + PLAN_ARRIVAL.Substring(6, 2);
                            DateTime ret = DateTime.Parse(ss);
                            dousei.DouseiDate = ret;
                        }
                        catch
                        {
                            dousei.DouseiDate = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    #endregion

                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;

                    if (ITEM_NO(i) != "")
                    {
                        #region 貨物データ作成
                        DjDouseiCargo cargo = new DjDouseiCargo();

                        cargo.DjDouseiCargoID = Guid.NewGuid().ToString();
                        cargo.DjDouseiID = dousei.DjDouseiID;
                        cargo.Qtty = (decimal)ITEM_QUANTITY(i);

                        #region 貨物を検索

                        string CargoNo = ITEM_NO(i);
                        if (CargoNo != "")
                        {
                            MsCargo msCargo = MsCargo.GetRecord(loginUser, CargoNo);
                            if (msCargo == null)
                            {
                                アカサカ連携.Info info = new アカサカ連携.Info();
                                info.InfoKind = 1;
                                info.Msg = "船が見つかりません CargoNo:" + CargoNo;
                                Infos.Add(info);
                                return null;
                            }
                            cargo.MsCargoID = msCargo.MsCargoID;
                            dousei.DjDouseiCargos.Add(cargo);
                        }
                        #endregion

                        #endregion
                    }
                //}
            }

            #region パース完了ログ
            アカサカ連携.Info EndInfo = new アカサカ連携.Info();
            EndInfo.InfoKind = 0;
            EndInfo.Msg = "パース完了";
            Infos.Add(EndInfo);
            #endregion

            return Douseis.ToList<DjDousei>();
        }

        private string GET_ITEM_NO(string ITEM_NAME)
        {
            try{
                string[] SplitedData = ITEM_NAME.Split(':');
                if (SplitedData.Length == 2)
                {
                    return SplitedData[0];
                }
            }catch{
                return "";
            }
            return "";
        }

        private string ToTime(string data)
        {
            if (data.Length == 14)
            {
                return data.Substring(8, 4);
            }

            return "";
        }

        #region プロパティ

        private string CMD
        {
            get
            {
                string Data = FindData( "CMD");
                return Data;
            }
        }

        public string NAVI_CODE
        {
            get
            {
                string Data = FindData("NAVI_CODE");
                return Data;
            }
        }

        private string HONSENID
        {
            get
            {
                string Data = FindData( "HONSENID");
                return Data;
            }
        }

        private string PORTCODE
        {
            get
            {
                string Data = FindData( "PORTCODE");
                return Data;
            }
        }

        private string BERTHCODE
        {
            get
            {
                string Data = FindData( "BERTHCODE");
                return Data;
            }
        }

        #region 予定
        private string PLAN_ARRIVAL
        {
            get
            {
                string Data = FindData( "PLAN_ARRIVAL");
                return Data;
            }
        }

        private string PLAN_ATTACH
        {
            get
            {
                string Data = FindData( "PLAN_ATTACH");
                return Data;
            }
        }

        private string PLAN_OPE_START
        {
            get
            {
                string Data = FindData( "PLAN_OPE_START");
                return Data;
            }
        }

        private string PLAN_OPE_END
        {
            get
            {
                string Data = FindData( "PLAN_OPE_END");
                return Data;
            }
        }

        private string PLAN_DETACH
        {
            get
            {
                string Data = FindData( "PLAN_DETACH");
                return Data;
            }
        }

        private string PLAN_LEAVE
        {
            get
            {
                string Data = FindData( "PLAN_LEAVE");
                return Data;
            }
        }
        #endregion

        #region 実績
        private string RESULT_ARRIVAL
        {
            get
            {
                string Data = FindData( "RESULT_ARRIVAL");
                return Data;
            }
        }

        private string RESULT_ATTACH
        {
            get
            {
                string Data = FindData( "RESULT_ATTACH");
                return Data;
            }
        }

        private string RESULT_OPE_START
        {
            get
            {
                string Data = FindData( "RESULT_OPE_START");
                return Data;
            }
        }

        private string RESULT_OPE_END
        {
            get
            {
                string Data = FindData( "RESULT_OPE_END");
                return Data;
            }
        }

        private string RESULT_DETACH
        {
            get
            {
                string Data = FindData( "RESULT_DETACH");
                return Data;
            }
        }

        private string RESULT_LEAVE
        {
            get
            {
                string Data = FindData( "RESULT_LEAVE");
                return Data;
            }
        }
        #endregion

        private string VOYAGENO
        {
            get
            {
                string Data = FindData( "VOYAGENO");
                return Data;
            }
        }

        private string ITEM_NAME(int index)
        {
            string Data = FindData("ITEM_NAME_" + index.ToString());
            return Data;
        }

        private string ITEM_NO(int index)
        {
            string Data = FindData("ITEM_NAME_" + index.ToString());

            if (Data.Length >= 4)
            {
                string[] s = Data.Split(':');
                if (s.Length == 2)
                {
                    return s[0];
                }
            }
            return "";
        }

        private int ITEM_QUANTITY(int index)
        {
            string Data = FindData("ITEM_QUANTITY_" + index.ToString());
            try
            {
                int ret = int.Parse(Data);
                return ret;
            }
            catch
            {
                return int.MinValue;
            }
        }

        private int ITEM_STATUS(int index)
        {
            string Data = FindData("ITEM_STATUS_" + index.ToString());
            try
            {
                int ret = int.Parse(Data);
                return ret;
            }
            catch
            {
                return int.MinValue;
            }
        }

        private string RECORD_DATETIME
        {
            get
            {
                string Data = FindData("RECORD_DATETIME");
                return Data;
            }
        }

        #endregion

        private string FindData(string Command)
        {
            foreach (string SplitParseData in SplitParseDatas)
            {
                if (SplitParseData.Length >= Command.Length)
                {
                    if (SplitParseData.Substring(0, Command.Length) == Command)
                    {
                        return SplitParseData.Substring(Command.Length + 1, SplitParseData.Length - Command.Length -1);
                    }
                }
            }

            return "";
        }


    }
}
