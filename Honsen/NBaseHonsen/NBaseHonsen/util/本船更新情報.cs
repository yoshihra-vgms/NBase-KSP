using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using SyncClient;

namespace NBaseHonsen.util
{
    internal class 本船更新情報
    {
        internal static PtHonsenkoushinInfo CreateInstance(object obj, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum type, params object[] args)
        {
            PtHonsenkoushinInfo info = new PtHonsenkoushinInfo();

            info.PtHonsenkoushinInfoId = System.Guid.NewGuid().ToString();

            info.MsPortalInfoKubunId = ((int)MsPortalInfoKubun.MsPortalInfoKubunIdEnum.本船更新).ToString();
            info.MsPortalInfoKoumokuId = ((int)type).ToString();


            if (obj is SiCard)
            {
                SiCard card = obj as SiCard;

                info.Shubetsu = "船員管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                info.SanshoumotoId = card.SiCardID;
                info.EventDate = DateTime.Now;

                // 乗船・下船
                if (args.Length == 0)
                {
                    info.KoushinNaiyou = String.Format(infoFormat.Shousai, card.SeninName);
                }
                // 交代
                else
                {
                    info.KoushinNaiyou = String.Format(infoFormat.Shousai, args[0], card.SeninName);
                }
            }
            else if (obj is SiSoukin)
            {
                SiSoukin soukin = obj as SiSoukin;

                info.Shubetsu = "船員管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                info.SanshoumotoId = soukin.SiSoukinID;
                //info.EventDate = soukin.UkeireDate;
                info.EventDate = DateTime.Now; // 20101022:川崎さんの指示

                info.KoushinNaiyou = String.Format(infoFormat.Shousai, NBaseCommon.Common.金額出力(soukin.Kingaku));
            }
            else if (obj is SiJunbikin)
            {
                SiJunbikin junbikin = obj as SiJunbikin;

                info.Shubetsu = "船員管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                info.SanshoumotoId = junbikin.SiJunbikinID;
                //info.EventDate = junbikin.JunbikinDate;
                info.EventDate = DateTime.Now; // 20101022:川崎さんの指示

                MsSiMeisai meisai = args[0] as MsSiMeisai;

                if (meisai.KashiKariFlag == 0)
                {
                    info.KoushinNaiyou = String.Format(infoFormat.Shousai, meisai.Name, NBaseCommon.Common.金額出力(junbikin.KingakuIn), NBaseCommon.Common.金額出力(junbikin.TaxIn));
                }
                else
                {
                    info.KoushinNaiyou = String.Format(infoFormat.Shousai, meisai.Name, NBaseCommon.Common.金額出力(junbikin.KingakuOut), NBaseCommon.Common.金額出力(junbikin.TaxOut));
                }
            }
            else if (obj is OdThi)
            {
                OdThi thi = obj as OdThi;

                info.Shubetsu = "発注管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                info.SanshoumotoId = thi.OdThiID;
                //info.EventDate = thi.ThiIraiDate;
                info.EventDate = DateTime.Now; // 20101022:川崎さんの指示

                info.KoushinNaiyou = String.Format(infoFormat.Shousai, "{0}", thi.Naiyou);
            }
            else if (obj is OdJry)
            {
                OdJry jry = obj as OdJry;

                info.Shubetsu = "発注管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                info.SanshoumotoId = jry.OdJryID;
                //info.EventDate = jry.JryDate;
                info.EventDate = DateTime.Now; // 船で受領としたその日

                info.KoushinNaiyou = String.Format(infoFormat.Shousai, jry.JryNo);
            }
            else if (obj is OdChozo)
            {
                OdChozo chozo = obj as OdChozo;

                info.Shubetsu = "発注管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                //info.SanshoumotoId 　// 参照元はなし
                info.EventDate = DateTime.Now; // 船で保存としたその日

                if (chozo.Shubetsu == 0)
                {
                    info.KoushinNaiyou = OdChozo.ShubetsuEnum.潤滑油.ToString();
                }
                else
                {
                    info.KoushinNaiyou = OdChozo.ShubetsuEnum.船用品.ToString();
                }
            }
            else if (obj is DmKanriKiroku)
            {
                DmKanriKiroku kanriKiroku = obj as DmKanriKiroku;

                info.Shubetsu = "文書管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                //info.SanshoumotoId 　// 参照元はなし
                info.EventDate = DateTime.Now; // 船で登録としたその日

                info.KoushinNaiyou = String.Format(infoFormat.Shousai, kanriKiroku.BunruiName, kanriKiroku.BunshoName);
            }
            else if (obj is DmKoubunshoKisoku)
            {
                DmKoubunshoKisoku koubunshoKisoku = obj as DmKoubunshoKisoku;

                info.Shubetsu = "文書管理";
                info.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
                PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
                info.Naiyou = infoFormat.Naiyou;
                //info.SanshoumotoId 　// 参照元はなし
                info.EventDate = DateTime.Now; // 船で登録としたその日

                info.KoushinNaiyou = String.Format(infoFormat.Shousai, koubunshoKisoku.BunruiName, koubunshoKisoku.BunshoName);
            }




            info.MsVesselId = (short)同期Client.LOGIN_VESSEL.MsVesselID;
            info.HonsenkoushinInfoUser = 同期Client.LOGIN_USER.MsUserID;

            if (!(obj is SiCard))
            {
                info.VesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
            }

            info.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
            info.RenewDate = DateTime.Now;

            return info;
        }
    }
}
