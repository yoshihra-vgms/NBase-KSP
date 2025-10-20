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

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("DJ_DOUSEI")]
    public class DjDousei : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 動静情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DJ_DOUSEI_ID", true)]
        public string DjDouseiID { get; set; }

        /// <summary>
        /// 簡易動静情報種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KANIDOUSEI_INFO_SHUBETU_ID")]
        public string MsKanidouseiInfoShubetuID { get; set; }

        /// <summary>
        /// 簡易動静情報種別(基幹NO)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_NO")]
        public string KikanNo { get; set; }

        /// <summary>
        /// 簡易動静情報種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANIDOUSEI_INFO_SHUBETU_NAME")]
        public string KanidouseiInfoShubetuName { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 船NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NO")]
        public string MsVesselNo { get; set; }


        /// <summary>
        /// 航海報告コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAVICODE")]
        public string NaveCode { get; set; }

        /// <summary>
        /// 次航海番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("VOYAGE_NO")]
        public string VoyageNo { get; set; }

        /// <summary>
        /// 次航海番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_RENKEI_FLAG")]
        public int KikanRenkeiFlag { get; set; }

        /// <summary>
        /// 場所ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID")]
        public string MsBashoID { get; set; }

        /// <summary>
        /// 場所NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_NO")]
        public string MsBashoNO { get; set; }

        /// <summary>
        /// 場所名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_NAME")]
        public string BashoName { get; set; }

        /// <summary>
        /// 基地マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KICHI_ID")]
        public string MsKichiID { get; set; }

        /// <summary>
        /// 基地マスタNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NO")]
        public string MsKichiNO { get; set; }


        /// <summary>
        /// 基地名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NAME")]
        public string KichiName { get; set; }

        /// <summary>
        /// 動静日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("DOUSEI_DATE")]
        public DateTime DouseiDate { get; set; }

        #region 予定
        /// <summary>
        /// 予定入港時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_NYUKO")]
        public DateTime PlanNyuko { get; set; }
        /// <summary>
        /// 予定着桟時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_CHAKUSAN")]
        public DateTime PlanChakusan { get; set; }
        /// <summary>
        /// 予定荷役開始時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_NIYAKU_START")]
        public DateTime PlanNiyakuStart { get; set; }
        /// <summary>
        /// 予定荷役終了時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_NIYAKU_END")]
        public DateTime PlanNiyakuEnd { get; set; }
        /// <summary>
        /// 予定離桟時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_RISAN")]
        public DateTime PlanRisan { get; set; }
        /// <summary>
        /// 予定出港時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_SHUKOU")]
        public DateTime PlanShukou { get; set; }
        #endregion

        #region 実績
        /// <summary>
        /// 実績入港時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_NYUKO")]
        public DateTime ResultNyuko { get; set; }
        /// <summary>
        /// 実績着桟時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_CHAKUSAN")]
        public DateTime ResultChakusan { get; set; }
        /// <summary>
        /// 実績荷役開始時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_NIYAKU_START")]
        public DateTime ResultNiyakuStart { get; set; }
        /// <summary>
        /// 実績荷役終了時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_NIYAKU_END")]
        public DateTime ResultNiyakuEnd { get; set; }
        /// <summary>
        /// 実績離桟時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_RISAN")]
        public DateTime ResultRisan { get; set; }
        /// <summary>
        /// 実績出港時間
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_SHUKOU")]
        public DateTime ResultShukou { get; set; }
        #endregion

        /// <summary>
        /// データ発生日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("RECORD_DATETIME")]
        public string RecordDateTime { get; set; }
        /// <summary>
        /// 基幹次航番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_VOYAGE_NO")]
        public string KikanVoyageNo { get; set; }

        /// <summary>
        /// コマ番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOMA_NO")]
        public string KomaNo { get; set; }


        /// <summary>
        /// 場所ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_MS_BASHO_ID")]
        public string ResultMsBashoID { get; set; }

        /// <summary>
        /// 場所NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_MS_BASHO_NO")]
        public string ResultMsBashoNO { get; set; }

        /// <summary>
        /// 場所名
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_BASHO_NAME")]
        public string ResultBashoName { get; set; }

        /// <summary>
        /// 基地マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_MS_KICHI_ID")]
        public string ResultMsKichiID { get; set; }

        /// <summary>
        /// 基地マスタNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_KICHI_NO")]
        public string ResultMsKichiNO { get; set; }

        /// <summary>
        /// 基地名
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_KICHI_NAME")]
        public string ResultKichiName { get; set; }

        /// <summary>
        /// 代理店ID（MS_CUSTOMER_ID）
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAIRITEN_ID")]
        public string DairitenID { get; set; }

        /// <summary>
        /// 荷主ID（MS_CUSTOMER_ID）
        /// </summary>
        [DataMember]
        [ColumnAttribute("NINUSHI_ID")]
        public string NinushiID { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 代理店ID（MS_CUSTOMER_ID）
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_DAIRITEN_ID")]
        public string ResultDairitenID { get; set; }

        /// <summary>
        /// 荷主ID（MS_CUSTOMER_ID）
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_NINUSHI_ID")]
        public string ResultNinushiID { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_BIKOU")]
        public string ResultBikou { get; set; }


        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }



        [DataMember]
        public List<DjDousei> DjDouseis = new List<DjDousei>();

        [DataMember]
        public List<DjDouseiCargo> DjDouseiCargos = new List<DjDouseiCargo>();
        
        [DataMember]
        public List<DjDouseiCargo> ResultDjDouseiCargos = new List<DjDouseiCargo>();

        //public bool 予定
        //{
        //    get
        //    {
        //        if (NaveCode == "")
        //        {
        //            //NaviCodeが空の場合はアカサカ連携のデータではないので実績扱いにする
        //            return false;
        //        }

        //        if (ResultNyuko != "" ||
        //            ResultChakusan != "" ||
        //            ResultNiyakuStart != "" ||
        //            ResultNiyakuEnd != "" ||
        //            ResultRisan != "" ||
        //            ResultShukou != "")
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //}
        public bool 予定
        {
            get
            {
                if (ResultNyuko != DateTime.MinValue ||
                    ResultChakusan != DateTime.MinValue ||
                    ResultNiyakuStart != DateTime.MinValue ||
                    ResultNiyakuEnd != DateTime.MinValue ||
                    ResultRisan != DateTime.MinValue ||
                    ResultShukou != DateTime.MinValue)
                {
                    return false;
                }
                return true;
            }
        }
        public string 場所
        {
            get
            {
                string bashoName = "";
                if (予定 == true)
                {
                    bashoName = BashoName;
                }
                else
                {
                    bashoName = ResultBashoName;
                }
                return bashoName;
            }
        }
        public string 基地
        {
            get
            {
                string kichiName = "";
                if (予定 == true)
                {
                    kichiName = KichiName;
                }
                else
                {
                    kichiName = ResultKichiName;
                }
                return kichiName;
            }
        }
        public string 入港時間
        {
            get
            {
                DateTime retTime;
                if (予定 == true)
                {
                    retTime =  PlanNyuko;
                }
                else
                {
                    retTime =  ResultNyuko;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }

            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanNyuko = value;
            //    }
            //    else
            //    {
            //        ResultNyuko = value;
            //    }
            //}
        }
        public string 着桟時間
        {
            get
            {
                DateTime retTime;
                if (予定 == true)
                {
                    retTime =  PlanChakusan;
                }
                else
                {
                    retTime = ResultChakusan;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanChakusan = value;
            //    }
            //    else
            //    {
            //        ResultChakusan = value;
            //    }
            //}
        }
        public string 荷役開始
        {
            get
            {
                DateTime retTime;
                if (予定 == true)
                {
                    retTime = PlanNiyakuStart;
                }
                else
                {
                    retTime = ResultNiyakuStart;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanNiyakuStart = value;
            //    }
            //    else
            //    {
            //        ResultNiyakuStart = value;
            //    }
            //}
        }
        public string 荷役終了
        {
            get
            {
                DateTime retTime;
                if (予定 == true)
                {
                    retTime = PlanNiyakuEnd;
                }
                else
                {
                    retTime = ResultNiyakuEnd;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanNiyakuEnd = value;
            //    }
            //    else
            //    {
            //        ResultNiyakuEnd = value;
            //    }
            //}
        }
        public string 離桟時間
        {
            get
            {
                DateTime retTime;
                if (予定 == true)
                {
                    retTime = PlanRisan;
                }
                else
                {
                    retTime = ResultRisan;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanRisan = value;
            //    }
            //    else
            //    {
            //        ResultRisan = value;
            //    }
            //}
        }
        public string 出港時間
        {
            get
            {
                DateTime retTime;            
                if (予定 == true)
                {
                    retTime = PlanShukou;
                }
                else
                {
                    retTime = ResultShukou;
                }
                if (retTime != DateTime.MinValue)
                {
                    return retTime.ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
            //set
            //{
            //    if (予定 == true)
            //    {
            //        PlanShukou = value;
            //    }
            //    else
            //    {
            //        ResultShukou = value;
            //    }
            //}
        }
        public string 荷主
        {
            get
            {
                string retNinushiID = null;
                if (予定 == true)
                {
                    if (NinushiID != null && NinushiID != "")
                    {
                        retNinushiID = NinushiID;
                    }
                }
                else
                {
                    if (ResultNinushiID != null && ResultNinushiID != "")
                    {
                        retNinushiID = ResultNinushiID;
                    }
                }
                return retNinushiID;
            }
        }
        public List<DjDouseiCargo> 積荷
        {
            get
            {
                List<DjDouseiCargo> douseiCargos;
                if (予定 == true)
                {
                    douseiCargos = DjDouseiCargos;
                }
                else
                {
                    douseiCargos = ResultDjDouseiCargos;
                }
                return douseiCargos;
            }
        }


        //
        // 基幹連携時に、荷役開始日と、入港、着棧、荷役終了、離棧、出港が違う場合、
        // 別のレコードを作成する必要がある
        // その際に、元々のDjDouseiを保持する
        //
        public DjDousei orgDjDousei = null;

        // 基幹連携時に、セットするOPE_SET_KEYのため、
        // 分割番号を保持する
        public int setKey = 0;


        #endregion

        /// <summary>
        /// 貨物名をカンマ区切りで返す
        /// </summary>
        public string CargoNames
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                List<DjDouseiCargo> douseiCargos;
                if (予定 == true)
                {
                    douseiCargos = DjDouseiCargos;
                }
                else
                {
                    douseiCargos = ResultDjDouseiCargos;
                }

                for (int i = 0; i < douseiCargos.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.AppendFormat(",{0}", douseiCargos[i].MsCargoName);
                    }
                    else
                    {
                        sb.AppendFormat("{0}", douseiCargos[i].MsCargoName);
                    }
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 数量の合計を返す
        /// </summary>
        public decimal SumQtty
        {
            get
            {
                decimal ret = 0;
                List<DjDouseiCargo> douseiCargos;
                if (予定 == true)
                {
                    douseiCargos = DjDouseiCargos;
                }
                else
                {
                    douseiCargos = ResultDjDouseiCargos;
                }
                for (int i = 0; i < douseiCargos.Count; i++)
                {
                    ret += douseiCargos[i].Qtty;
                }
                return ret;
            }
        }


        /// <summary>
        /// 動静情報として有効か無効か
        /// </summary>
        public bool IsValid { set; get; }



        public DjDousei()
        {
            DjDouseiID = "";
            NaveCode = "";
        }

        public DjDousei 積み(int index)
        {
            int FindCount = 0;
            foreach (DjDousei dj in DjDouseis)
            {
                if (dj.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID && dj.DeleteFlag == 0)
                {
                    FindCount++;
                    if (FindCount == index)
                    {
                        return dj;
                    }
                }
            }
            return null;
        }

        public DjDousei 揚げ(int index)
        {
            int FindCount = 0;
            foreach (DjDousei dj in DjDouseis)
            {
                if (dj.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID && dj.DeleteFlag == 0)
                {
                    FindCount++;
                    if (FindCount == index)
                    {
                        return dj;
                    }
                }
            }
            return null;
        }

        public static List<DjDousei> GetRecordsByHead(MsUser loginUser, MsVessel Vessel, DateTime FromDate, DateTime ToDate, int KikanrenkeiJisseki,
                                                 List<int> cargoIds, List<string> bashoIds, List<string> kichiIds, List<string> dairitenIds, List<string> ninushiIds)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GroupByMsVesselIDVoyageNo");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", Vessel.MsVesselID));
            Params.Add(new DBParameter("FROM_DATE", FromDate.ToString("yyyy/MM/dd")));
            // between A and B なので、ToDateの23:59:59　まで対象にする
            //Params.Add(new DBParameter("TO_DATE", ToDate.ToString("yyyy/MM/dd")));
            Params.Add(new DBParameter("TO_DATE", DateTime.Parse(ToDate.ToString("yyyy/MM/dd 23:59:59")))); 

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            List<DjDousei> Heads = new List<DjDousei>();
            Heads = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            List<DjDousei> all = new List<DjDousei>();
            foreach (DjDousei Head in Heads)
            {
                DjDousei TopDousei = DjDousei.GetRecordOrderByTIME(loginUser, Head.MsVesselID, Head.VoyageNo, KikanrenkeiJisseki);
                if (TopDousei != null)
                {
                    all.Add(DjDousei.GetRecord(loginUser, TopDousei.DjDouseiID));
                }
            }


            {
                //VOYAGE_NOが入っていない行を取得
                List<DjDousei> NoVoyages = GetRecordsByNoVoyage(loginUser, Vessel, FromDate, ToDate, KikanrenkeiJisseki);
                foreach (DjDousei NoVoyage in NoVoyages)
                {
                    all.Add(NoVoyage);
                }
            }

            if (all.Count > 0)
            {
                foreach (DjDousei djDousei in all)
                {
                    //djDousei.DjDouseiCargos = DjDouseiCargo.GetRecords(loginUser, djDousei);
                    List<DjDouseiCargo> tmp = DjDouseiCargo.GetRecords(loginUser, djDousei);
                    var plans = from ddc in tmp
                                where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN
                                orderby ddc.LineNo
                                select ddc;
                    djDousei.DjDouseiCargos = plans.ToList<DjDouseiCargo>();
                    var results = from ddc in tmp
                                  where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT
                                  orderby ddc.LineNo
                                  select ddc;
                    djDousei.ResultDjDouseiCargos = results.ToList<DjDouseiCargo>();
                }
            }

            //動静情報の詳細を取得する
            foreach (DjDousei dj in all)
            {
                //dj.DjDouseis = NBaseData.DAC.DjDousei.GetRecordsByVoaygeNo(loginUser, dj);
                List<DjDousei> sameVoyageNoDousei = NBaseData.DAC.DjDousei.GetRecordsByVoaygeNo(loginUser, dj);
                if (KikanrenkeiJisseki == 0)
                {
                    var notRenkeiDousei = from douseis in sameVoyageNoDousei
                                          where douseis.KikanRenkeiFlag == KikanrenkeiJisseki
                                          select douseis;
                    dj.DjDouseis = notRenkeiDousei.ToList<DjDousei>();
                }
                else
                {
                    dj.DjDouseis = sameVoyageNoDousei;
                }

                if (dj.DjDouseis.Count == 0)
                {
                    dj.DjDouseis.Add(DjDousei.GetRecord(loginUser, dj.DjDouseiID));
                }
            }



            //======================================================================================
            //List<int> cargoIds
            //List<string> bashoIds
            //List<string> kichiIds
            //List<string> dairitenIds
            //List<string> ninushiIds

            List<DjDousei> ret = new List<DjDousei>();
            if (cargoIds.Count == 0 && bashoIds.Count == 0 && kichiIds.Count == 0 && dairitenIds.Count == 0 && ninushiIds.Count == 0)
            {
                ret = all;
            }
            else
            {
                foreach (DjDousei dj in all)
                {
                    if (cargoIds.Count > 0)
                    {
                        bool isExists = false;
                        foreach (DjDousei child in dj.DjDouseis)
                        {
                            if (child.DjDouseiCargos.Any(obj => cargoIds.Contains(obj.MsCargoID)))
                            {
                                isExists = true;
                            }
                            if (child.ResultDjDouseiCargos.Any(obj => cargoIds.Contains(obj.MsCargoID)))
                            {
                                isExists = true;
                            }
                        }
                        if (isExists == false)
                        {
                            continue;
                        }
                    }
                    if (bashoIds.Count > 0)
                    {
                        //if (dj.DjDouseis.Any(obj => bashoIds.Contains(obj.MsBashoID)) == false)
                        if (dj.DjDouseis.Any(obj => bashoIds.Contains(obj.MsBashoID) || bashoIds.Contains(obj.ResultMsBashoID)) == false)
                        {
                                continue;
                        }
                    }
                    if (kichiIds.Count > 0)
                    {
                        //if (dj.DjDouseis.Any(obj => kichiIds.Contains(obj.MsKichiID)) == false)
                        if (dj.DjDouseis.Any(obj => kichiIds.Contains(obj.MsKichiID) || kichiIds.Contains(obj.ResultMsKichiID)) == false)
                        {
                            continue;
                        }
                    }
                    if (dairitenIds.Count > 0)
                    {
                        //if (dj.DjDouseis.Any(obj => dairitenIds.Contains(obj.DairitenID)) == false)
                        if (dj.DjDouseis.Any(obj => dairitenIds.Contains(obj.DairitenID) || dairitenIds.Contains(obj.ResultDairitenID)) == false)
                        {
                                continue;
                        }
                    }
                    if (ninushiIds.Count > 0)
                    {
                        //if (dj.DjDouseis.Any(obj => ninushiIds.Contains(obj.NinushiID)) == false)
                        if (dj.DjDouseis.Any(obj => ninushiIds.Contains(obj.NinushiID) || ninushiIds.Contains(obj.ResultNinushiID)) == false)
                        {
                                continue;
                        }
                    }

                    ret.Add(dj);
                }
            }


            return ret;
        }

        private static List<DjDousei> GetRecordsByNoVoyage(MsUser loginUser, MsVessel Vessel, DateTime FromDate, DateTime ToDate, int KikanrenkeiJisseki)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecordsByNoVoyage");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", Vessel.MsVesselID));
            Params.Add(new DBParameter("FROM_DATE", FromDate.ToString("yyyy/MM/dd")));
            Params.Add(new DBParameter("TO_DATE", ToDate.ToString("yyyy/MM/dd")));
            Params.Add(new DBParameter("KIKAN_RENKEI_FLAG", KikanrenkeiJisseki));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        private static DjDousei GetRecordOrderByTIME(MsUser loginUser, int msVesselID, string voyageNo, int KikanrenkeiJisseki)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "OrderByTIME");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));
            Params.Add(new DBParameter("KIKAN_RENKEI_FLAG", KikanrenkeiJisseki));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        public static List<DjDousei> GetRecords(MsUser loginUser, int msVesselID, DateTime date)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "WhereByDate");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("DATE", date.ToString("yyyy/MM/dd")));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        // 2013.05.28: 利用していない
        public static List<DjDousei> GetRecords(MsUser loginUser, int msVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DjDousei GetRecord(MsUser loginUser, string DjDouseiID)
        {
            return GetRecord(null, loginUser, DjDouseiID);
        }

        public static DjDousei GetRecord(DBConnect dbConnect, MsUser loginUser, string DjDouseiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "Where");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                List<DjDouseiCargo> tmp = DjDouseiCargo.GetRecords(dbConnect, loginUser, ret[0]);
                var plans = from ddc in tmp
                            where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN
                            orderby ddc.LineNo
                            select ddc;
                ret[0].DjDouseiCargos = plans.ToList<DjDouseiCargo>();
                var results = from ddc in tmp
                              where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT
                              orderby ddc.LineNo
                              select ddc;
                ret[0].ResultDjDouseiCargos = results.ToList<DjDouseiCargo>();
                return ret[0];
            }
            return null;
        }

        public static DjDousei GetRecordByMaxRecordDateTime(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecordByMaxRecordDateTime");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                //ret[0].DjDouseiCargos = DjDouseiCargo.GetRecords(loginUser, ret[0]);
                List<DjDouseiCargo> tmp = DjDouseiCargo.GetRecords(loginUser, ret[0]);
                var plans = from ddc in tmp
                            where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN
                            orderby ddc.LineNo
                            select ddc;
                ret[0].DjDouseiCargos = plans.ToList<DjDouseiCargo>();
                var results = from ddc in tmp
                              where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT
                              orderby ddc.LineNo
                              select ddc;
                ret[0].ResultDjDouseiCargos = results.ToList<DjDouseiCargo>();
                return ret[0];
            }
            return null;
        }

        public static DjDousei GetRecordByNaviCode(MsUser loginUser, string NaviCode)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecord");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "ByNaviCode");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAVICODE", NaviCode));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                //ret[0].DjDouseiCargos = DjDouseiCargo.GetRecords(loginUser, ret[0]);
                List<DjDouseiCargo> tmp = DjDouseiCargo.GetRecords(loginUser, ret[0]);
                var plans = from ddc in tmp
                            where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN
                            orderby ddc.LineNo
                            select ddc;
                ret[0].DjDouseiCargos = plans.ToList<DjDouseiCargo>();
                var results = from ddc in tmp
                              where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT
                              orderby ddc.LineNo
                              select ddc;
                ret[0].ResultDjDouseiCargos = results.ToList<DjDouseiCargo>();
                return ret[0];
            }
            return null;
        }

        public static List<DjDousei> GetRecordsByVoaygeNo(MsUser loginUser, DjDousei dj)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecord");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "WhereByVoaygeNo");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "OrderBy");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", dj.MsVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", dj.VoyageNo));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (DjDousei dousei in ret)
            {
                List<DjDouseiCargo> tmp = DjDouseiCargo.GetRecords(loginUser, dousei);
                var plans = from ddc in tmp
                            where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.PLAN
                            orderby ddc.LineNo
                            select ddc;
                dousei.DjDouseiCargos = plans.ToList<DjDouseiCargo>();
                var results = from ddc in tmp
                            where ddc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT
                            orderby ddc.LineNo
                            select ddc;
                dousei.ResultDjDouseiCargos = results.ToList<DjDouseiCargo>();
            }
            return ret;
        }


        /// <summary>
        /// 船毎の最大＋１のVoyageNoを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msVesselID"></param>
        /// <returns></returns>
        #region public static string GetNextVoyageNo(MsUser loginUser, int msVesselID)
        public static string GetNextVoyageNo(MsUser loginUser, int msVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            decimal voyNoDec = Convert.ToDecimal(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

            string voyNo = "1";
            if (voyNoDec > 0)
            {
                voyNoDec++;
                voyNo = voyNoDec.ToString();
            }
            return voyNo;
        }
        #endregion


        /// <summary>
        /// 同じ次航番号の情報を削除する
        /// </summary>
        /// <param name="dbConnect"></param>
        /// <param name="loginUser"></param>
        /// <param name="msVesselID"></param>
        /// <param name="voyageNo"></param>
        /// <returns></returns>
        #region public static bool DeleteByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        public static bool DeleteByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            return true;
        }
        #endregion



        //Mskichiに関連するデータの取得
        public static List<DjDousei> GetRecordsByMsKichiID(MsUser loginUser, string ms_kichi_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecord");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "ByMsKichiID");
            

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();


            Params.Add(new DBParameter("MS_KICHI_ID", ms_kichi_id));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //MS_BASHO_IDでデータを取得
        public static List<DjDousei> GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecord");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "ByMsBashoID");


            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();


            Params.Add(new DBParameter("MS_BASHO_ID", ms_basho_id));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<DjDousei> GetVesselIds(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(MsUser loginUser)
        {
            DBConnect dbConnect = new DBConnect();
            dbConnect.ConnectionOpen();
            dbConnect.BeginTransaction();
            
            bool ret = InsertRecord(dbConnect, loginUser);

            dbConnect.Commit();

            return ret;
        }

        public bool InsertRecord(DBConnect dbConnect,MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("NAVICODE",NaveCode));
            Params.Add(new DBParameter("VOYAGE_NO",VoyageNo));
            Params.Add(new DBParameter("KIKAN_RENKEI_FLAG",KikanRenkeiFlag));
            Params.Add(new DBParameter("MS_BASHO_ID",MsBashoID));
            Params.Add(new DBParameter("MS_KICHI_ID",MsKichiID));
            Params.Add(new DBParameter("DOUSEI_DATE",DouseiDate));
            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID",MsKanidouseiInfoShubetuID));
            Params.Add(new DBParameter("PLAN_NYUKO",PlanNyuko));
            Params.Add(new DBParameter("PLAN_CHAKUSAN",PlanChakusan));
            Params.Add(new DBParameter("PLAN_NIYAKU_START",PlanNiyakuStart));
            Params.Add(new DBParameter("PLAN_NIYAKU_END",PlanNiyakuEnd));
            Params.Add(new DBParameter("PLAN_RISAN",PlanRisan));
            Params.Add(new DBParameter("PLAN_SHUKOU",PlanShukou));
            Params.Add(new DBParameter("RESULT_NYUKO",ResultNyuko));
            Params.Add(new DBParameter("RESULT_CHAKUSAN",ResultChakusan));
            Params.Add(new DBParameter("RESULT_NIYAKU_START",ResultNiyakuStart));
            Params.Add(new DBParameter("RESULT_NIYAKU_END",ResultNiyakuEnd));
            Params.Add(new DBParameter("RESULT_RISAN",ResultRisan));
            Params.Add(new DBParameter("RESULT_SHUKOU",ResultShukou));
            Params.Add(new DBParameter("RECORD_DATETIME",RecordDateTime));
            Params.Add(new DBParameter("KIKAN_VOYAGE_NO", KikanVoyageNo));
            Params.Add(new DBParameter("KOMA_NO", KomaNo));
            Params.Add(new DBParameter("RESULT_MS_BASHO_ID", ResultMsBashoID));
            Params.Add(new DBParameter("RESULT_MS_KICHI_ID", ResultMsKichiID));
            Params.Add(new DBParameter("DAIRITEN_ID", DairitenID));
            Params.Add(new DBParameter("NINUSHI_ID", NinushiID));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("RESULT_DAIRITEN_ID", ResultDairitenID));
            Params.Add(new DBParameter("RESULT_NINUSHI_ID", ResultNinushiID));
            Params.Add(new DBParameter("RESULT_BIKOU", ResultBikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = 0;
            try
            {
                cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            }
            catch (Exception ex)
            {
                Console.Write("Error:" + ex.Message);
            }

            if ((DjDouseiCargos != null && DjDouseiCargos.Count > 0) || (ResultDjDouseiCargos != null && ResultDjDouseiCargos.Count > 0))
            {
                //DjDouseiCargo.DeleteRecords(loginUser, dbConnect, this);
                foreach (DjDouseiCargo cargo in DjDouseiCargos)
                {
                    cargo.DjDouseiID = DjDouseiID;
                    cargo.VesselID = MsVesselID;
                    cargo.InsertRecord(dbConnect, loginUser);
                }
                foreach (DjDouseiCargo cargo in ResultDjDouseiCargos)
                {
                    cargo.DjDouseiID = DjDouseiID;
                    cargo.VesselID = MsVesselID;
                    cargo.InsertRecord(dbConnect, loginUser);
                }
            }
            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            DBConnect dbConnect = new DBConnect();
            dbConnect.ConnectionOpen();
            dbConnect.BeginTransaction();

            bool ret = UpdateRecord(dbConnect, loginUser);
            
            dbConnect.Commit();

            return true;
        }

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

            if (DjDouseis == null | DjDouseis.Count == 0)
            {
                UpdateDetailRecords(dbConnect, loginUser);
            }
            else
            {
                foreach (DjDousei dj in DjDouseis)
                {
                    if (dj.Exists(dbConnect, loginUser) == false)
                    {
                        dj.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        dj.UpdateDetailRecords(dbConnect, loginUser);
                    }
                }
            }

            return true;
        }

        public bool UpdateDetailRecords(DBConnect dbConnect,MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "UpdateRecord");

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("NAVICODE", NaveCode));
            Params.Add(new DBParameter("VOYAGE_NO", VoyageNo));
            Params.Add(new DBParameter("KIKAN_RENKEI_FLAG", KikanRenkeiFlag));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));
            Params.Add(new DBParameter("MS_KICHI_ID", MsKichiID));
            Params.Add(new DBParameter("DOUSEI_DATE", DouseiDate));
            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID", MsKanidouseiInfoShubetuID));
            Params.Add(new DBParameter("PLAN_NYUKO", PlanNyuko));
            Params.Add(new DBParameter("PLAN_CHAKUSAN", PlanChakusan));
            Params.Add(new DBParameter("PLAN_NIYAKU_START", PlanNiyakuStart));
            Params.Add(new DBParameter("PLAN_NIYAKU_END", PlanNiyakuEnd));
            Params.Add(new DBParameter("PLAN_RISAN", PlanRisan));
            Params.Add(new DBParameter("PLAN_SHUKOU", PlanShukou));
            Params.Add(new DBParameter("RESULT_NYUKO", ResultNyuko));
            Params.Add(new DBParameter("RESULT_CHAKUSAN", ResultChakusan));
            Params.Add(new DBParameter("RESULT_NIYAKU_START", ResultNiyakuStart));
            Params.Add(new DBParameter("RESULT_NIYAKU_END", ResultNiyakuEnd));
            Params.Add(new DBParameter("RESULT_RISAN", ResultRisan));
            Params.Add(new DBParameter("RESULT_SHUKOU", ResultShukou));
            Params.Add(new DBParameter("RECORD_DATETIME", RecordDateTime));
            Params.Add(new DBParameter("KIKAN_VOYAGE_NO", KikanVoyageNo));
            Params.Add(new DBParameter("KOMA_NO", KomaNo));
            Params.Add(new DBParameter("RESULT_MS_BASHO_ID", ResultMsBashoID));
            Params.Add(new DBParameter("RESULT_MS_KICHI_ID", ResultMsKichiID));
            Params.Add(new DBParameter("DAIRITEN_ID", DairitenID));
            Params.Add(new DBParameter("NINUSHI_ID", NinushiID));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("RESULT_DAIRITEN_ID", ResultDairitenID));
            Params.Add(new DBParameter("RESULT_NINUSHI_ID", ResultNinushiID));
            Params.Add(new DBParameter("RESULT_BIKOU", ResultBikou));
            
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if ((DjDouseiCargos != null && DjDouseiCargos.Count > 0) || (ResultDjDouseiCargos != null && ResultDjDouseiCargos.Count > 0))
            {
                foreach (DjDouseiCargo cargo in DjDouseiCargos)
                {
                    if(cargo.DeleteFlag == 1)
                    {
                        cargo.DeleteRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        cargo.DjDouseiID = DjDouseiID;
                        cargo.VesselID = MsVesselID;
                        if (cargo.Exists(dbConnect, loginUser))
                        {
                            cargo.UpdateRecord(dbConnect, loginUser);
                        }
                        else
                        {
                            cargo.InsertRecord(dbConnect, loginUser);
                        }
                    }
                }
                foreach (DjDouseiCargo cargo in ResultDjDouseiCargos)
                {
                    if (cargo.DeleteFlag == 1)
                    {
                        cargo.DeleteRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        cargo.DjDouseiID = DjDouseiID;
                        cargo.VesselID = MsVesselID;
                        if (cargo.Exists(dbConnect, loginUser))
                        {
                            cargo.UpdateRecord(dbConnect, loginUser);
                        }
                        else
                        {
                            cargo.InsertRecord(dbConnect, loginUser);
                        }
                    }
                }
            }
            return true;
        }


        ///// <summary>
        ///// DJ_DOUSEI_IDを持つデータがあるか調べる
        ///// </summary>
        ///// <param name="dbConnect"></param>
        ///// <param name="loginUser"></param>
        ///// <returns>存在する場合はtrueを返す</returns>
        //private bool Exists(DBConnect dbConnect, MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), MethodBase.GetCurrentMethod());

        //    #region パラメーター設定
        //    ParameterConnection Params = new ParameterConnection();

        //    Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
        //    #endregion

        //    List<DjDousei> ret = new List<DjDousei>();
        //    MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    if (ret.Count == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        // 2013.05.28: 利用していない
        //public void Copy(DjDousei copyto)
        //{
        //    copyto.DjDouseiID = DjDouseiID;
        //    copyto.MsVesselID = MsVesselID;
        //    copyto.NaveCode = NaveCode;
        //    copyto.VoyageNo = VoyageNo;
        //    copyto.KikanRenkeiFlag = KikanRenkeiFlag;
        //    copyto.MsBashoID = MsBashoID;
        //    copyto.MsKichiID = MsKichiID;
        //    copyto.DouseiDate = DouseiDate;
        //    copyto.MsKanidouseiInfoShubetuID = MsKanidouseiInfoShubetuID;
        //    copyto.PlanNyuko = PlanNyuko;
        //    copyto.PlanChakusan = PlanChakusan;
        //    copyto.PlanNiyakuStart = PlanNiyakuStart;
        //    copyto.PlanNiyakuEnd = PlanNiyakuEnd;
        //    copyto.PlanRisan = PlanRisan;
        //    copyto.PlanShukou = PlanShukou;
        //    copyto.ResultNyuko = ResultNyuko;
        //    copyto.ResultChakusan = ResultChakusan;
        //    copyto.ResultNiyakuStart = ResultNiyakuStart;
        //    copyto.ResultNiyakuEnd = ResultNiyakuEnd;
        //    copyto.ResultRisan = ResultRisan;
        //    copyto.ResultShukou = ResultShukou;
        //    copyto.RecordDateTime = RecordDateTime;
        //    copyto.Bikou = Bikou;
            
        //    copyto.RenewDate = RenewDate;
        //    copyto.RenewUserID = RenewUserID;

        //}


        public static List<DjDousei> GetRecordsByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDousei), "ByVoyageNo");

            List<DjDousei> ret = new List<DjDousei>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));

            MappingBase<DjDousei> mapping = new MappingBase<DjDousei>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }



        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", DjDouseiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
