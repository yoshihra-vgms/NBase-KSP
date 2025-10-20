using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using WingData.DAC;
using WingData.DS;
using Oracle.DataAccess.Client;

namespace WingData.BLC
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

        public bool Kick(MsUser loginUser,MsUser OperationUser,List<DjDousei> parentDouseis)
        {
            this.loginUser = loginUser;

            foreach (DjDousei d in parentDouseis)
            {
                DetailLineNo = 1;

                this.parentDousei = d;

                VoyageNo = parentDousei.DjDouseis[0].DouseiDate.ToString("yyMM");

                #region TKJNAIPLANIF(スケジュール明細IFテーブル）、TKJNAIPLAN_AMTIF(スケジュール明細金額IFテーブル）を作成、更新
                foreach (DjDousei dousei in parentDousei.DjDouseis)
                {
                    TKJNAIPLANIF Header = MakeHeader(loginUser, dousei, OperationUser);
                    Headers.Add(Header);
                    #region すでにデータがある場合は削除する
                    TKJNAIPLANIF ExistHeader = TKJNAIPLANIF.GetRecord(loginUser,Header, dousei);
                    if (ExistHeader != null)
                    {
                        Header.KomaNo = ExistHeader.KomaNo;
                        ExistHeader.DeleteRecord(loginUser);
                    }
                    #endregion
                    Header.djDousei.KomaNo = Header.KomaNo;
                    Header.djDousei.KikanVoyageNo = Header.JikoNo;
                    Header.InsertRecord(loginUser);

                    foreach (DjDouseiCargo cargo in dousei.DjDouseiCargos)
                    {
                        TKJNAIPLAN_AMTIF Detail = MakeDetail(loginUser,Header, cargo, OperationUser);
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

                //取込依頼のストアドプロシージャを呼ぶ
                TKJNAIPLANIF.内航動静データ取込ストアド呼び出し(loginUser,OperationUser, parentDouseis[0].MsVesselNo);

                #region TKJNAIPLANIF連携の結果を読み込み、動静情報を更新する
                foreach (TKJNAIPLANIF Header in Headers)
                {
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
                        ResultMessage DetailMessage = new ResultMessage(Header.djDousei,Detail.DouseiCargo, TKJNAIPLAN_AMTIF.GetRecord(loginUser, Detail,Header.djDousei,Detail.DouseiCargo));
                        ResultMessages.Add(DetailMessage);

                        //処理結果を取得
                        if (DetailMessage.StatusType == ResultMessage.SYORI_STATUS.STATUS_TYPE.ERROR)
                        {
                            err = true;
                        }
                    }

                    //処理結果がエラーの場合は基幹連携済みにしない
                    if (err == false)
                    {
                        //DJ_DOUSEIを更新し基幹連携済みにする
                        HeaderResultMessage.Dousei.KikanRenkeiFlag = 1;
                        HeaderResultMessage.Dousei.UpdateDetailRecords(null, loginUser);
                    }
                    else
                    {
                        //DJ_DOUSEIを更新し基幹連携済みにする
                        HeaderResultMessage.Dousei.UpdateDetailRecords(null,loginUser);
                    }
                }
                #endregion
            }
            return true;
        }

        private TKJNAIPLANIF MakeHeader(MsUser loginUser, DjDousei dousei, MsUser OperationUser)
        {
            TKJNAIPLANIF ret = new TKJNAIPLANIF();

            ret.djDousei = dousei;
            ret.FuneNo = dousei.MsVesselNo;
            ret.JikoNo = VoyageNo;
            ret.ScYmd = dousei.DouseiDate;
            ret.KomaNo = TKJNAIPLANIF.GetKoma(loginUser,ret).ToString();
            ret.OpeNo = 処理番号;
            ret.OpeSetKey = MakeOpeSetKey;
            ret.PortNo = dousei.MsBashoNO;
            ret.BaseNo = dousei.MsKichiNO;
            ret.SgKbn = dousei.KikanNo;
            ret.Area = "";

            ret.AgencyContactFlag = "0";
            ret.AgencyReContactFlag = "0";
            ret.CaptainContactFlag = "0";
            ret.DsInputFlag = "1";

            ret.InportTime = dousei.入港時間;
            ret.ArriveTime = dousei.着桟時間;
            ret.StartTime = dousei.荷役開始;
            ret.EndTime = dousei.荷役終了;
            ret.DepartureTime = dousei.離桟時間;
            ret.OutportTime = dousei.出港時間;
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

        private TKJNAIPLAN_AMTIF MakeDetail(MsUser loginUser,TKJNAIPLANIF Header, DjDouseiCargo cargo, MsUser OperationUser)
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
            ret.ChartererNo = 荷主NO;
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
        private string MakeOpeSetKey
        {
            get
            {
                return parentDousei.MsVesselID.ToString("d3") + parentDousei.VoyageNo;
            }
        }

        [DataContract()]
        public class ResultMessage
        {
            [DataMember]
            public List<SYORI_STATUS> SYORI_STATUS_MASTER = new List<SYORI_STATUS>();

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
