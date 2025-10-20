using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
//using Npgsql;

namespace NBaseData.BLC
{

    /// <summary>
    /// 動静情報を基幹連携する
    /// </summary>
    [DataContract()]
    public class 動静基幹連携
    {
        private const string 外部システム識別ID = "WING";
        private const int 処理番号 = 0;
        private const string 消費税区分 = "";
        private const string 荷主NO = "9990000";//諸口
        private const string 運賃算定基準区分 = "4";

        private int DetailLineNo = 1;
        private DjDousei parentDousei;
        private MsUser loginUser;
        private string VoyageNo { get; set; }

        public List<TKJNAIPLANIF> Headers = new List<TKJNAIPLANIF>();
        public List<ResultMessage> ResultMessages = new List<ResultMessage>();

        public List<string> ErrorMessages = new List<string>();

        public bool Kick(MsUser loginUser,MsUser OperationUser,List<DjDousei> parentDouseis)
        {
            this.loginUser = loginUser;

            foreach (DjDousei d in parentDouseis)
            {
                DetailLineNo = 1;

                this.parentDousei = d;

                VoyageNo = parentDousei.DjDouseis[0].DouseiDate.ToString("yyMM");

                // 2013.05.14: 荷役開始日と違う日付の値がある動静情報を分割する
                DjDousei convDjDousei = 分割処理(d);

                #region TKJNAIPLANIF(スケジュール明細IFテーブル）、TKJNAIPLAN_AMTIF(スケジュール明細金額IFテーブル）を作成、更新
                //foreach (DjDousei dousei in parentDousei.DjDouseis)
                foreach (DjDousei dousei in convDjDousei.DjDouseis)
                {
                    // 2013.05.14: 予定は連携しない
                    if (dousei.予定 == true)
                        continue;


                    TKJNAIPLANIF Header = MakeHeader(loginUser, dousei, OperationUser);

                    if (Header.SgKbn == "8")
                    {
                        TKJNAIPLANIF checkHeader = TKJNAIPLANIF.GetSameRecord(loginUser, Header);
                        if (checkHeader != null)
                        {
                            continue;
                        }
                    }

                    Headers.Add(Header);
                    #region すでにデータがある場合は削除する
                    TKJNAIPLANIF ExistHeader = TKJNAIPLANIF.GetRecord(loginUser,Header, dousei);
                    if (ExistHeader != null)
                    {
                        Header.KomaNo = ExistHeader.KomaNo;
                        ExistHeader.DeleteRecord(loginUser);
                    }
                    #endregion
                    // 2013.05.14: 動静情報を分割したので、元の動静がセットされているものだけを対象にする
                    //Header.djDousei.KomaNo = Header.KomaNo;
                    //Header.djDousei.KikanVoyageNo = Header.JikoNo;
                    if (Header.djDousei != null)
                    {
                        Header.djDousei.KomaNo = Header.KomaNo;
                        Header.djDousei.KikanVoyageNo = Header.JikoNo;
                    }
                    Header.InsertRecord(loginUser);

                    foreach (DjDouseiCargo cargo in dousei.積荷)
                    {
                        TKJNAIPLAN_AMTIF Detail = MakeDetail(loginUser, Header, dousei, cargo, OperationUser);
                        #region すでにデータがある場合はデータを削除する
                        TKJNAIPLAN_AMTIF ExistDetail = TKJNAIPLAN_AMTIF.GetRecord(loginUser, Detail,dousei, cargo);
                        if (ExistDetail != null)
                        {
                            Detail.LineNo = ExistDetail.LineNo;
                            ExistDetail.DeleteRecord(loginUser);
                        }
                        #endregion
                        Detail.DouseiCargo.LineNo = Detail.LineNo;
                        Detail.InsertRecord(loginUser);
                    }
                }
                #endregion


                try
                {
                    //取込依頼のストアドプロシージャを呼ぶ
                    TKJNAIPLANIF.内航動静データ取込ストアド呼び出し(loginUser,OperationUser, parentDouseis[0].MsVesselNo);


                    #region TKJNAIPLANIF連携の結果を読み込み、動静情報を更新する
                    //foreach (TKJNAIPLANIF Header in Headers)
                    //{
                    //    // 2013.05.14: 動静情報を分割したので、元の動静がセットされているものだけを対象にする
                    //    if (Header.djDousei == null)
                    //        continue;

                    //    bool err = false;
                    //    ResultMessage HeaderResultMessage = new ResultMessage(Header.djDousei, TKJNAIPLANIF.GetRecord(loginUser, Header, Header.djDousei));
                    //    ResultMessages.Add(HeaderResultMessage);

                    //    //処理結果を取得
                    //    if (HeaderResultMessage.StatusType == ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
                    //    {
                    //        err = true;
                    //    }

                    //    //TKJNAIPLAN_AMTIF(スケジュール明細金額IFテーブル）を更新
                    //    foreach (TKJNAIPLAN_AMTIF Detail in Header.TKJNAIPLAN_AMTIFs)
                    //    {
                    //        ResultMessage DetailMessage = new ResultMessage(Header.djDousei, Detail.DouseiCargo, TKJNAIPLAN_AMTIF.GetRecord(loginUser, Detail, Header.djDousei, Detail.DouseiCargo));
                    //        ResultMessages.Add(DetailMessage);

                    //        //処理結果を取得
                    //        if (DetailMessage.StatusType == ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
                    //        {
                    //            err = true;
                    //        }
                    //    }

                    //    ////処理結果がエラーの場合は基幹連携済みにしない
                    //    //if (err == false)
                    //    //{
                    //    //    //DJ_DOUSEIを更新し基幹連携済みにする
                    //    //    HeaderResultMessage.Dousei.KikanRenkeiFlag = 1;
                    //    //    HeaderResultMessage.Dousei.UpdateDetailRecords(null, loginUser);
                    //    //}
                    //    //else
                    //    //{
                    //    //    //DJ_DOUSEIを更新し基幹連携済みにする
                    //    //    HeaderResultMessage.Dousei.UpdateDetailRecords(null, loginUser);
                    //    //}
                    //    if (err == false)
                    //    {
                    //        System.Diagnostics.Debug.WriteLine("WingCoreServer:[" + HeaderResultMessage.Dousei.DjDouseiID + "]:連携済み");
                    //    }
                    //    else
                    //    {
                    //        System.Diagnostics.Debug.WriteLine("WingCoreServer:[" + HeaderResultMessage.Dousei.DjDouseiID + "]:エラー");
                    //    }
                    //    HeaderResultMessage.result = err;
                    //}
                    #endregion
                }
                catch (Exception e)
                {
                    ErrorMessages.Add(e.Message);
                }

            }

            #region TKJNAIPLANIF連携の結果を読み込み、動静情報を更新する
            foreach (TKJNAIPLANIF Header in Headers)
            {
                // 2013.05.14: 動静情報を分割したので、元の動静がセットされているものだけを対象にする
                if (Header.djDousei == null)
                    continue;

                bool err = false;
                ResultMessage HeaderResultMessage = new ResultMessage(Header.djDousei, TKJNAIPLANIF.GetRecord(loginUser, Header, Header.djDousei));
                ResultMessages.Add(HeaderResultMessage);

                //処理結果を取得
                if (HeaderResultMessage.StatusType == ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
                {
                    err = true;
                }

                //TKJNAIPLAN_AMTIF(スケジュール明細金額IFテーブル）を更新
                foreach (TKJNAIPLAN_AMTIF Detail in Header.TKJNAIPLAN_AMTIFs)
                {
                    ResultMessage DetailMessage = new ResultMessage(Header.djDousei, Detail.DouseiCargo, TKJNAIPLAN_AMTIF.GetRecord(loginUser, Detail, Header.djDousei, Detail.DouseiCargo));
                    ResultMessages.Add(DetailMessage);

                    //処理結果を取得
                    if (DetailMessage.StatusType == ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
                    {
                        err = true;
                    }
                }

                ////処理結果がエラーの場合は基幹連携済みにしない
                //if (err == false)
                //{
                //    //DJ_DOUSEIを更新し基幹連携済みにする
                //    HeaderResultMessage.Dousei.KikanRenkeiFlag = 1;
                //    HeaderResultMessage.Dousei.UpdateDetailRecords(null, loginUser);
                //}
                //else
                //{
                //    //DJ_DOUSEIを更新し基幹連携済みにする
                //    HeaderResultMessage.Dousei.UpdateDetailRecords(null, loginUser);
                //}
                if (err == false)
                {
                    System.Diagnostics.Debug.WriteLine("WingCoreServer:[" + HeaderResultMessage.Dousei.DjDouseiID + "]:連携済み");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("WingCoreServer:[" + HeaderResultMessage.Dousei.DjDouseiID + "]:エラー");
                }
                HeaderResultMessage.result = err;
            }
            #endregion

            return true;
        }

        private TKJNAIPLANIF MakeHeader(MsUser loginUser, DjDousei dousei, MsUser OperationUser)
        {
            TKJNAIPLANIF ret = new TKJNAIPLANIF();

            // 2013.05.14: 分割してしまうので、元の動静情報を保持したものを利用する
            //ret.djDousei = dousei;
            ret.djDousei = dousei.orgDjDousei;
            ret.FuneNo = dousei.MsVesselNo;
            ret.JikoNo = VoyageNo;
            // 2013.05.14 : 年月日のみであるとコメントを受けたので改造
            //ret.ScYmd = dousei.DouseiDate;
            ret.ScYmd = DateTime.Parse(dousei.DouseiDate.ToShortDateString());
            ret.KomaNo = TKJNAIPLANIF.GetKoma(loginUser, ret).ToString();
            ret.OpeNo = 処理番号;
            //ret.OpeSetKey = MakeOpeSetKey;
            ret.OpeSetKey = MakeOpeSetKey(dousei);
            ret.PortNo = dousei.ResultMsBashoNO;
            ret.BaseNo = dousei.ResultMsKichiNO;

            // 2013.05.14 : 荷役開始日のあるレコード以外は"8"とコメントを受けたので改造
            //ret.SgKbn = dousei.KikanNo;
            //if (dousei.荷役開始.Length > 0)
            // 2013.07.23 : 荷役終了日のあるレコード以外は"8"とコメントを受けたので改造
            if (dousei.荷役終了.Length > 0)
            {
                ret.SgKbn = dousei.KikanNo;
            }
            else
            {
                ret.SgKbn = "8";
            }
            ret.Area = "";

            ret.AgencyContactFlag = "0";
            ret.AgencyReContactFlag = "0";
            ret.CaptainContactFlag = "0";
            // 2013.05.14 : "0"であるとコメントを受けたので改造
            //ret.DsInputFlag = "1";
            ret.DsInputFlag = "0";

            // 2013.05.14 : 4桁出力 "mmdd" であるとコメントを受けたので改造
            if (dousei.入港時間.Length > 0)
            {
                ret.InportTime = dousei.入港時間.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.InportTime = "";
            }
            if (dousei.着桟時間.Length > 0)
            {
                ret.ArriveTime = dousei.着桟時間.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.ArriveTime = "";
            }
            if (dousei.荷役開始.Length > 0)
            {
                ret.StartTime = dousei.荷役開始.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.StartTime = "";
            }
            if (dousei.荷役終了.Length > 0)
            {
                ret.EndTime = dousei.荷役終了.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.EndTime = "";
            }
            if (dousei.離桟時間.Length > 0)
            {
                ret.DepartureTime = dousei.離桟時間.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.DepartureTime = "";
            }
            if (dousei.出港時間.Length > 0)
            {
                ret.OutportTime = dousei.出港時間.Replace(":", "").PadLeft(4, '0');
            }
            else
            {
                ret.OutportTime = "";
            }
            ret.ThroughTime = "";

            ret.CreateDate = DateTime.Now;
            ret.TrkmTntsyCD = OperationUser.MsUserID;
            ret.TrkmDate = DateTime.Now;
            ret.SyoriStatus = "00";
            ret.StatusRemark = "";
            ret.ExtSystemCD = 外部システム識別ID;
            ret.UpdateYMDHMS = 更新日時;

            return ret;
        }

        //private TKJNAIPLAN_AMTIF MakeDetail(MsUser loginUser,TKJNAIPLANIF Header, DjDouseiCargo cargo, MsUser OperationUser)
        private TKJNAIPLAN_AMTIF MakeDetail(MsUser loginUser, TKJNAIPLANIF Header, DjDousei dousei, DjDouseiCargo cargo, MsUser OperationUser)
        {
            TKJNAIPLAN_AMTIF ret = new TKJNAIPLAN_AMTIF();

            ret.DouseiCargo = cargo;
            ret.FuneNo = Header.FuneNo;
            ret.JikoNo = VoyageNo;
            ret.ScYmd = Header.ScYmd;
            ret.KomaNo = Header.KomaNo;
            ret.OpeNo = 処理番号;
            ret.LineNo = TKJNAIPLAN_AMTIF.GetRecordLineNo(loginUser, ret).ToString();
            ret.OpeSetKey = Header.OpeSetKey;
            ret.CargoNo = cargo.MsCargoNo;
            //荷主
            //ret.ChartererNo = 荷主NO;
            if ( dousei.荷主 != null)
            {
                ret.ChartererNo = dousei.荷主;
            }
            else
            {
                ret.ChartererNo = 荷主NO;
            }
            //数量
            ret.Qtty = (double)cargo.Qtty;
            //単位
            ret.KztaniCd = "";
            //運賃算定基準区分
            ret.UskjnKbn = 運賃算定基準区分;
            //計算区分
            ret.KsnKbn = "";
            //単価(Freight Rate)
            ret.FrTanka = 0;
            //金額
            ret.GakYen = 0;
            //消費税区分
            ret.SyohizeiKbn = 消費税区分;
            //消費税額
            ret.SyohizeiGak = 0;
            //合計金額
            ret.TtlGaku = 0;
            //確定区分
            ret.KKbn = "1";

            ret.CreateDate = DateTime.Now;
            ret.TrkmTntsyCD = OperationUser.MsUserID;
            ret.TrkmDate = DateTime.Now;
            ret.SyoriStatus = "00";

            ret.StatusRemark = "";
            ret.ExtSystemCD = 外部システム識別ID;
            ret.UpdateYMDHMS = 更新日時;

            Header.TKJNAIPLAN_AMTIFs.Add(ret);

            DetailLineNo++;

            return ret;
        }


        private Int64 更新日時
        {
            get
            {
                DateTime now = DateTime.Now;

                Int64 ret = Int64.Parse(now.ToString("yyyyMMddHHmmssFFF"));

                return ret;
            }
        }


        /// <summary>
        /// 処理セットキーを返す(MS_VESSEL_ID + VOYAGE_NO)
        /// </summary>
        /// <returns></returns>
        //private string MakeOpeSetKey
        //{
        //    get
        //    {
        //        return parentDousei.MsVesselID.ToString("d3") + parentDousei.VoyageNo;
        //    }
        //}
        private string MakeOpeSetKey(DjDousei dousei)
        {
            return parentDousei.MsVesselID.ToString("d4") + parentDousei.VoyageNo.PadLeft(7, '0') + dousei.setKey.ToString().PadLeft(2,'0');
        }



        private DjDousei 分割処理(DjDousei parentDousei)
        {
            DjDousei retDjDousei = new DjDousei();
            int setKey = 1;

            foreach (DjDousei dousei in parentDousei.DjDouseis)
            {
                // 日付ごとに動静情報を分割するので、Dictionaryを準備する
                Dictionary<DateTime, DjDousei> djDouseiDic = new Dictionary<DateTime, DjDousei>();

                // 基準となる日付（荷役開始日）→　（荷役終了日）
                //DateTime douseiDate = DateTime.Parse(dousei.DouseiDate.ToShortDateString());
                DateTime douseiDate = DateTime.Parse(dousei.DouseiDate.ToShortDateString());

                // 2013.07.23 荷役終了のデータをヘッダーとする
                //// 元データをコピー（荷役開始日のデータ）
                //DjDousei niyakuStartDousei = new DjDousei();
                //niyakuStartDousei.orgDjDousei = dousei;
                //niyakuStartDousei.DouseiDate = DateTime.Parse(dousei.ResultNiyakuStart.ToShortDateString());
                //niyakuStartDousei.MsVesselNo = dousei.MsVesselNo;
                //niyakuStartDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                //niyakuStartDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                //niyakuStartDousei.KikanNo = dousei.KikanNo;
                //niyakuStartDousei.ResultNinushiID = dousei.ResultNinushiID;
                //// 荷役開始のOPE_SET_KEY は、分割番号をセットしない
                ////niyakuStartDousei.setKey = setKey;
                //niyakuStartDousei.ResultNiyakuStart = dousei.ResultNiyakuStart;
                //niyakuStartDousei.ResultDjDouseiCargos.AddRange(dousei.ResultDjDouseiCargos);
                DjDousei niyakuEndDousei = new DjDousei();
                niyakuEndDousei.orgDjDousei = dousei;
                niyakuEndDousei.DouseiDate = DateTime.Parse(dousei.ResultNiyakuEnd.ToShortDateString());
                niyakuEndDousei.MsVesselNo = dousei.MsVesselNo;
                niyakuEndDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                niyakuEndDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                niyakuEndDousei.KikanNo = dousei.KikanNo;
                niyakuEndDousei.ResultNinushiID = dousei.ResultNinushiID;

                niyakuEndDousei.ResultNiyakuEnd = dousei.ResultNiyakuEnd;

                niyakuEndDousei.ResultDjDouseiCargos.AddRange(dousei.ResultDjDouseiCargos);


                //djDouseiDic.Add(douseiDate, niyakuStartDousei);
                djDouseiDic.Add(douseiDate, niyakuEndDousei);


                // 入港日
                if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultNyuko.ToShortDateString())))
                {
                    djDouseiDic[DateTime.Parse(dousei.ResultNyuko.ToShortDateString())].ResultNyuko = dousei.ResultNyuko;
                }
                else
                {
                    DjDousei nyukoDousei = new DjDousei();
                    nyukoDousei.DouseiDate = DateTime.Parse(dousei.ResultNyuko.ToShortDateString());
                    nyukoDousei.MsVesselNo = dousei.MsVesselNo;
                    nyukoDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                    nyukoDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                    nyukoDousei.ResultNyuko = dousei.ResultNyuko;
                    nyukoDousei.setKey = setKey;
                    setKey++;

                    djDouseiDic.Add(nyukoDousei.DouseiDate, nyukoDousei);
                }

                // 着棧日
                if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultChakusan.ToShortDateString())))
                {
                    djDouseiDic[DateTime.Parse(dousei.ResultChakusan.ToShortDateString())].ResultChakusan = dousei.ResultChakusan;
                }
                else
                {
                    DjDousei chakusanDousei = new DjDousei();
                    chakusanDousei.DouseiDate = DateTime.Parse(dousei.ResultChakusan.ToShortDateString());
                    chakusanDousei.MsVesselNo = dousei.MsVesselNo;
                    chakusanDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                    chakusanDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                    chakusanDousei.ResultChakusan = dousei.ResultChakusan;
                    chakusanDousei.setKey = setKey;
                    setKey++;

                    djDouseiDic.Add(chakusanDousei.DouseiDate, chakusanDousei);
                }

                // 荷役開始日
                if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultNiyakuStart.ToShortDateString())))
                {
                    djDouseiDic[DateTime.Parse(dousei.ResultNiyakuStart.ToShortDateString())].ResultNiyakuStart = dousei.ResultNiyakuStart;
                }
                else
                {
                    DjDousei niyakuStartDousei = new DjDousei();
                    niyakuStartDousei.DouseiDate = DateTime.Parse(dousei.ResultNiyakuStart.ToShortDateString());
                    niyakuStartDousei.MsVesselNo = dousei.MsVesselNo;
                    niyakuStartDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                    niyakuStartDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                    niyakuStartDousei.ResultNiyakuStart = dousei.ResultNiyakuStart;
                    niyakuStartDousei.setKey = setKey;
                    setKey++;

                    djDouseiDic.Add(niyakuStartDousei.DouseiDate, niyakuStartDousei);
                }

                //// 荷役終了日
                //if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultNiyakuEnd.ToShortDateString())))
                //{
                //    djDouseiDic[DateTime.Parse(dousei.ResultNiyakuEnd.ToShortDateString())].ResultNiyakuEnd = dousei.ResultNiyakuEnd;
                //}
                //else
                //{
                //    DjDousei niyakuEndDousei = new DjDousei();
                //    niyakuEndDousei.DouseiDate = DateTime.Parse(dousei.ResultNiyakuEnd.ToShortDateString());
                //    niyakuEndDousei.MsVesselNo = dousei.MsVesselNo;
                //    niyakuEndDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                //    niyakuEndDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                //    niyakuEndDousei.ResultNiyakuEnd = dousei.ResultNiyakuEnd;
                //    niyakuEndDousei.setKey = setKey;
                //    setKey++;

                //    djDouseiDic.Add(niyakuEndDousei.DouseiDate, niyakuEndDousei);
                //}

                // 離棧日
                if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultRisan.ToShortDateString())))
                {
                    djDouseiDic[DateTime.Parse(dousei.ResultRisan.ToShortDateString())].ResultRisan = dousei.ResultRisan;
                }
                else
                {
                    DjDousei risanDousei = new DjDousei();
                    risanDousei.DouseiDate = DateTime.Parse(dousei.ResultRisan.ToShortDateString());
                    risanDousei.MsVesselNo = dousei.MsVesselNo;
                    risanDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                    risanDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                    risanDousei.ResultRisan = dousei.ResultRisan;
                    risanDousei.setKey = setKey;
                    setKey++;

                    djDouseiDic.Add(risanDousei.DouseiDate, risanDousei);
                }

                // 出港日
                if (dousei.ResultShukou != DateTime.MinValue)
                {
                    if (djDouseiDic.ContainsKey(DateTime.Parse(dousei.ResultShukou.ToShortDateString())))
                    {
                        djDouseiDic[DateTime.Parse(dousei.ResultShukou.ToShortDateString())].ResultShukou = dousei.ResultShukou;
                    }
                    else
                    {
                        DjDousei shukouDousei = new DjDousei();
                        shukouDousei.DouseiDate = DateTime.Parse(dousei.ResultShukou.ToShortDateString());
                        shukouDousei.MsVesselNo = dousei.MsVesselNo;
                        shukouDousei.ResultMsBashoNO = dousei.ResultMsBashoNO;
                        shukouDousei.ResultMsKichiNO = dousei.ResultMsKichiNO;
                        shukouDousei.ResultShukou = dousei.ResultShukou;
                        shukouDousei.setKey = setKey;
                        setKey++;

                        djDouseiDic.Add(shukouDousei.DouseiDate, shukouDousei);
                    }
                }

                foreach (DjDousei d in djDouseiDic.Values)
                {
                    retDjDousei.DjDouseis.Add(d);
                }
            }

            return retDjDousei;
        }


        [DataContract()]
        public class ResultMessage
        {
            [DataMember]
            public List<SYORI_STATUS> SYORI_STATUS_MASTER = new List<SYORI_STATUS>();
            
            [DataMember]
            public bool result = true;

            [DataMember]
            public DjDousei Dousei;
            [DataMember]
            public DjDouseiCargo DouseiCargo;
            [DataMember]
            public TKJNAIPLANIF TKJNAIPLANIF;
            [DataMember]
            public TKJNAIPLAN_AMTIF TKJNAIPLAN_AMTIF;

            public ResultMessage(DjDousei dousei,TKJNAIPLANIF tkjnaiplanif)
            {
                Init();
                Dousei = dousei;
                TKJNAIPLANIF = tkjnaiplanif;
            }

            public ResultMessage(DjDousei dousei,DjDouseiCargo cargo, TKJNAIPLAN_AMTIF tkjnaiplanamtif)
            {
                Init();
                Dousei = dousei;
                DouseiCargo = cargo;
                TKJNAIPLAN_AMTIF = tkjnaiplanamtif;
            }

            public SYORI_STATUS.STATUS_TYPE StatusType
            {
                get
                {
                    if (TKJNAIPLANIF != null)
                    {
                        var result = (from master in SYORI_STATUS_MASTER
                                     where master.StatusCode == TKJNAIPLANIF.SyoriStatus
                                      select master).Single<SYORI_STATUS>();

                        return result.StatusType;
                    }
                    else
                    {
                        var result = (from master in SYORI_STATUS_MASTER
                                     where master.StatusCode == TKJNAIPLAN_AMTIF.SyoriStatus
                                      select master).Single<SYORI_STATUS>();

                        return result.StatusType;
                    }
                }
            }
            public string StatusMessage
            {
                get
                {
                    if (TKJNAIPLANIF != null)
                    {
                        var result = (from master in SYORI_STATUS_MASTER
                                      where master.StatusCode == TKJNAIPLANIF.SyoriStatus
                                      select master).Single<SYORI_STATUS>();

                        return result.StatusName;
                    }
                    else
                    {
                        var result = (from master in SYORI_STATUS_MASTER
                                      where master.StatusCode == TKJNAIPLAN_AMTIF.SyoriStatus
                                      select master).Single<SYORI_STATUS>();

                        return result.StatusName;
                    }
                }
            }

            public string StatusToString()
            {
                StringBuilder sb = new StringBuilder();
                if (TKJNAIPLANIF != null)
                {
                    sb.AppendFormat("動静:日付: {0} 場所: {1} エラー内容:{2}", Dousei.DouseiDate.ToString("yyyy/MM/dd"), Dousei.BashoName, StatusMessage);
                }
                else
                {
                    sb.AppendFormat("積荷:日付: {0} 積荷: {1} エラー内容:{2}", Dousei.DouseiDate.ToString("yyyy/MM/dd"), DouseiCargo.MsCargoName, StatusMessage);
                }
                return sb.ToString();
            }

            private void Init()
            {
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("00", "作成済み", SYORI_STATUS.STATUS_TYPE.ERROR));
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("10", "取込済み", SYORI_STATUS.STATUS_TYPE.SUCCESS));
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("90", "項目エラー", SYORI_STATUS.STATUS_TYPE.ERROR));
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("91", "参照エラー", SYORI_STATUS.STATUS_TYPE.ERROR));
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("92", "登録済みエラー", SYORI_STATUS.STATUS_TYPE.ERROR));
                SYORI_STATUS_MASTER.Add(new SYORI_STATUS("99", "その他エラー", SYORI_STATUS.STATUS_TYPE.ERROR));
            }


            [DataContract()]
            public class SYORI_STATUS
            {
                public enum STATUS_TYPE
                {
                    SUCCESS,
                    ERROR
                };

                /// <summary>
                /// ステータスコード
                /// </summary>
                [DataMember]
                public string StatusCode;
                /// <summary>
                /// ステータス名
                /// </summary>
                [DataMember]
                public string StatusName;
                /// <summary>
                /// 種別
                /// </summary>
                [DataMember]
                public STATUS_TYPE StatusType;

                public SYORI_STATUS(string statusCode, string statusName, STATUS_TYPE statusType)
                {
                    StatusCode = statusCode;
                    StatusName = statusName;
                    StatusType = statusType;
                }
            }
        }
    }
}
