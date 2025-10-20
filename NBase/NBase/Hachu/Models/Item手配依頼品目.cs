using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class Item手配依頼品目
    {
        public OdThiItem 品目 = null;
        public List<OdThiShousaiItem> 詳細品目s = null;
        public List<OdThiShousaiItem> 削除詳細品目s = null;
        public List<OdAttachFile> 添付Files = null;

        public Item手配依頼品目()
        {
            品目 = new OdThiItem();
            詳細品目s = new List<OdThiShousaiItem>();
            削除詳細品目s = new List<OdThiShousaiItem>();
            添付Files = new List<OdAttachFile>();
        }

        /// <summary>
        /// 手配品目をＤＢから取得しリストにセットする
        /// </summary>
        /// <param name="OdThiID"></param>
        /// <returns></returns>
        #region public static List<Item手配依頼品目> GetRecords(string OdThiID)
        public static List<Item手配依頼品目> GetRecords(string OdThiID)
        {
            List<OdThiItem> OdThiItems = null;
            List<OdThiShousaiItem> OdThiShousaiItems = null;
            List<OdAttachFile> OdAttachFiles = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdThiItems = serviceClient.OdThiItem_GetRecordByOdThiID(NBaseCommon.Common.LoginUser,OdThiID);
                OdThiShousaiItems = serviceClient.OdThiShousaiItem_GetRecordByOdThiID(NBaseCommon.Common.LoginUser,OdThiID);
                OdAttachFiles = serviceClient.OdAttachFile_GetRecordsByOdThiID(NBaseCommon.Common.LoginUser, OdThiID);
            }

            List<Item手配依頼品目> ret = new List<Item手配依頼品目>();
            OdAttachFile tmp = null;
            if (OdThiItems != null)
            {
                foreach (OdThiItem thiItem in OdThiItems)
                {
                    Item手配依頼品目 retThiItem = new Item手配依頼品目();
                    retThiItem.品目 = thiItem;
                    tmp = GetAttachFile(OdAttachFiles, thiItem.OdAttachFileID);
                    if (tmp != null)
                    {
                        thiItem.OdAttachFileName = tmp.FileName;
                        retThiItem.添付Files.Add(tmp);
                    }

                    if (OdThiShousaiItems != null)
                    {
                        foreach (OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                        {
                            if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                            {
                                tmp = GetAttachFile(OdAttachFiles, thiShousaiItem.OdAttachFileID);
                                if (tmp != null)
                                {
                                    retThiItem.添付Files.Add(tmp);
                                    thiShousaiItem.OdAttachFileName = tmp.FileName;
                                }
                                retThiItem.詳細品目s.Add(thiShousaiItem);
                            }
                        }
                        foreach (OdThiShousaiItem thiShousaiItem in retThiItem.詳細品目s)
                        {
                            OdThiShousaiItems.Remove(thiShousaiItem);
                        }
                    }

                    ret.Add(retThiItem);
                }
            }
            return ret;
        }

        private static OdAttachFile GetAttachFile(List<OdAttachFile> attachFiles, string id)
        {
            if (attachFiles.Count == 0)
                return null;
            if (id == null || id.Length == 0)
                return null;
            var trgs = from af in attachFiles
                       where af.OdAttachFileID == id
                       select af;
            if (trgs.Count<OdAttachFile>() > 0)
                return trgs.First<OdAttachFile>();
            else
                return null;

        }
        #endregion

        /// <summary>
        /// 手配品目をＤＢから取得しリストにセットする（潤滑油） 
        /// </summary>
        /// <param name="MsVesselID"></param>
        /// <param name="OdThiID"></param>
        /// <returns></returns>
        #region public static List<Item手配依頼品目> GetRecords(int MsVesselID, string OdThiID)
        public static List<Item手配依頼品目> GetRecords(int MsVesselID, string OdThiID)
        {
            List<Item手配依頼品目> ret = new List<Item手配依頼品目>();

            List<OdThiItem> OdThiItems = null;
            List<OdThiShousaiItem> OdThiShousaiItems = null;
            List<MsLoVessel> MsLoVessels = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdThiItems = serviceClient.OdThiItem_GetRecordByOdThiID(NBaseCommon.Common.LoginUser,OdThiID);
                OdThiShousaiItems = serviceClient.OdThiShousaiItem_GetRecordByOdThiID(NBaseCommon.Common.LoginUser,OdThiID);
                MsLoVessels = serviceClient.MsLoVessel_GetRecordsByMsVesselID(NBaseCommon.Common.LoginUser, MsVesselID);
            }

            Dictionary<string, OdThiItem> OdThiItemDic = new Dictionary<string, OdThiItem>();
            foreach (OdThiItem i in OdThiItems)
            {
                OdThiItemDic.Add(i.ItemName, i);
            }
            Dictionary<string, OdThiShousaiItem> FO_OdThiShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();
            Dictionary<string, OdThiShousaiItem> LO_OdThiShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();
            Dictionary<string, OdThiShousaiItem> ETC_OdThiShousaiItemDic = new Dictionary<string, OdThiShousaiItem>();
            foreach (OdThiShousaiItem si in OdThiShousaiItems)
            {
                if (si.MsLoID == null || si.MsLoID.Length == 0)
                {
                    FO_OdThiShousaiItemDic.Add(si.ShousaiItemName, si);
                }
                else if ( si.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_LO) )
                {
                    LO_OdThiShousaiItemDic.Add(si.ShousaiItemName, si);
                }
                else if (si.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_ETC))
                {
                    ETC_OdThiShousaiItemDic.Add(si.ShousaiItemName, si);
                }
            }

            Item手配依頼品目 retThiItem = null;
            OdThiShousaiItem shousai = null;

            // FO
            retThiItem = new Item手配依頼品目();
            #region
            if (OdThiItemDic.ContainsKey(NBaseCommon.Common.MsLo_FO_String))
            {
                retThiItem.品目 = OdThiItemDic[NBaseCommon.Common.MsLo_FO_String];
                foreach (MsLo fo in NBaseCommon.Common.MsLo_Fos())
                {
                    if (FO_OdThiShousaiItemDic.ContainsKey(fo.LoName))
                    {
                        shousai = FO_OdThiShousaiItemDic[fo.LoName];
                        retThiItem.詳細品目s.Add(shousai);
                    }
                    else
                    {
                        shousai = MakeOdThiShousaiItem(retThiItem.品目.OdThiItemID, fo.MsLoID, fo.LoName, fo.MsTaniID, fo.MsTaniName, MsVesselID);
                        retThiItem.詳細品目s.Add(shousai);
                    }
                }
            }
            else
            {
                retThiItem.品目 = MakeOdThiItem(OdThiID, NBaseCommon.Common.MsLo_FO_String, MsVesselID);
                foreach (MsLo fo in NBaseCommon.Common.MsLo_Fos())
                {
                    shousai = MakeOdThiShousaiItem(retThiItem.品目.OdThiItemID, fo.MsLoID, fo.LoName, fo.MsTaniID, fo.MsTaniName, MsVesselID);
                    retThiItem.詳細品目s.Add(shousai);
                }
            }
            #endregion
            ret.Add(retThiItem);

            // LO
            retThiItem = new Item手配依頼品目();
            #region
            if (OdThiItemDic.ContainsKey(NBaseCommon.Common.MsLo_LO_String))
            {
                retThiItem.品目 = OdThiItemDic[NBaseCommon.Common.MsLo_LO_String];
            }
            else
            {
                retThiItem.品目 = MakeOdThiItem(OdThiID, NBaseCommon.Common.MsLo_LO_String, MsVesselID);
            }
            foreach (MsLoVessel loVessel in MsLoVessels)
            {
                if (loVessel.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_LO))
                {
                    if (LO_OdThiShousaiItemDic.ContainsKey(loVessel.MsLoName))
                    {
                        shousai = LO_OdThiShousaiItemDic[loVessel.MsLoName];
                        retThiItem.詳細品目s.Add(shousai);
                    }
                    else
                    {
                        shousai = MakeOdThiShousaiItem(retThiItem.品目.OdThiItemID, loVessel.MsLoID, loVessel.MsLoName, loVessel.MsTaniID, loVessel.MsTaniName, MsVesselID);
                        retThiItem.詳細品目s.Add(shousai);
                    }
                }
            }
            #endregion
            ret.Add(retThiItem);

            // ETC
            retThiItem = new Item手配依頼品目();
            #region
            if (OdThiItemDic.ContainsKey(NBaseCommon.Common.MsLo_ETC_String))
            {
                retThiItem.品目 = OdThiItemDic[NBaseCommon.Common.MsLo_ETC_String];
            }
            else
            {
                retThiItem.品目 = MakeOdThiItem(OdThiID, NBaseCommon.Common.MsLo_ETC_String, MsVesselID);
            }
            foreach (MsLoVessel loVessel in MsLoVessels)
            {
                if (loVessel.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_ETC))
                {
                    if (ETC_OdThiShousaiItemDic.ContainsKey(loVessel.MsLoName))
                    {
                        shousai = ETC_OdThiShousaiItemDic[loVessel.MsLoName];
                        retThiItem.詳細品目s.Add(shousai);
                    }
                    else
                    {
                        shousai = MakeOdThiShousaiItem(retThiItem.品目.OdThiItemID, loVessel.MsLoID, loVessel.MsLoName, loVessel.MsTaniID, loVessel.MsTaniName, MsVesselID);
                        retThiItem.詳細品目s.Add(shousai);
                    }
                }
            }
            #endregion
            ret.Add(retThiItem);

            return ret;
        }
        #endregion

        /// <summary>
        /// ドックオーダから読み取った情報をリストにセットする
        /// </summary>
        /// <param name="odThiITems"></param>
        /// <returns></returns>
        #region public static List<Item手配依頼品目> ConvertRecords(int msVesselId, List<OdThiItem> odThiItems)
        public static List<Item手配依頼品目> ConvertRecords(int msVesselId, List<OdThiItem> odThiItems)
        {
            Dictionary<string, string> MsShoushuriItemDic = MakeMsShoushuriItemDic(msVesselId);
            Dictionary<string, string> MsSsShousaiItemDic = MakeMsSsShousaiItemDic(msVesselId);

            List<Item手配依頼品目> ret = new List<Item手配依頼品目>();
            if (odThiItems != null)
            {
                foreach (OdThiItem thiItem in odThiItems)
                {
                    thiItem.OdThiItemID = Hachu.Common.CommonDefine.新規ID();
                    if (MsShoushuriItemDic.ContainsKey(thiItem.ItemName) == false)
                    {
                        thiItem.SaveDB = true;
                    }

                    Item手配依頼品目 手配依頼品目 = new Item手配依頼品目();
                    手配依頼品目.品目 = thiItem;
                    
                    foreach (OdThiShousaiItem otsi in thiItem.OdThiShousaiItems)
                    {
                        otsi.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                        otsi.OdThiItemID = thiItem.OdThiItemID;
                        otsi.Sateisu = otsi.Count;
                        if (MsSsShousaiItemDic.ContainsKey(otsi.ShousaiItemName) == false)
                        {
                            otsi.SaveDB = true;
                        }

                        手配依頼品目.詳細品目s.Add(otsi);
                    }


                    int insertPos = 0;
                    bool sameHeader = false;
                    foreach (Item手配依頼品目 品目 in ret)
                    {
                        if (品目.品目.Header == 手配依頼品目.品目.Header)
                        {
                            sameHeader = true;
                        }
                        else if (sameHeader)
                        {
                            break;
                        }
                        insertPos++;
                    }
                    if (insertPos >= ret.Count)
                    {
                        ret.Add(手配依頼品目);
                    }
                    else
                    {
                        ret.Insert(insertPos, 手配依頼品目);
                    }
                }
            }
            return ret;
        }
        #endregion
        
        #region public static List<Item手配依頼品目> ConvertRecords船用品(int msVesselId, List<OdThiItem> odThiItems)
        public static List<Item手配依頼品目> ConvertRecords船用品(int msVesselId, List<OdThiItem> odThiItems)
        {
            Dictionary<string, string> MsShoushuriItemDic = MakeMsShoushuriItemDic(msVesselId);
            Dictionary<string, string> MsSsShousaiItemDic = MakeMsSsShousaiItemDic(msVesselId);

            List<Item手配依頼品目> ret = new List<Item手配依頼品目>();
            if (odThiItems != null)
            {
                foreach (OdThiItem thiItem in odThiItems)
                {
                    thiItem.OdThiItemID = Hachu.Common.CommonDefine.新規ID();
                    if (MsShoushuriItemDic.ContainsKey(thiItem.ItemName) == false)
                    {
                        thiItem.SaveDB = true;
                    }

                    Item手配依頼品目 手配依頼品目 = new Item手配依頼品目();
                    手配依頼品目.品目 = thiItem;

                    foreach (OdThiShousaiItem otsi in thiItem.OdThiShousaiItems)
                    {
                        otsi.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID();
                        otsi.OdThiItemID = thiItem.OdThiItemID;
                        //otsi.Sateisu = otsi.Count; // 船用品の場合、査定数は、査定数のまま
                        if (MsSsShousaiItemDic.ContainsKey(otsi.ShousaiItemName) == false)
                        {
                            otsi.SaveDB = true;
                        }

                        手配依頼品目.詳細品目s.Add(otsi);
                    }


                    int insertPos = 0;
                    bool sameHeader = false;
                    foreach (Item手配依頼品目 品目 in ret)
                    {
                        if (品目.品目.Header == 手配依頼品目.品目.Header)
                        {
                            sameHeader = true;
                        }
                        else if (sameHeader)
                        {
                            break;
                        }
                        insertPos++;
                    }
                    if (insertPos >= ret.Count)
                    {
                        ret.Add(手配依頼品目);
                    }
                    else
                    {
                        ret.Insert(insertPos, 手配依頼品目);
                    }
                }
            }
            return ret;
        }
        #endregion
        public static Dictionary<string, string> MakeMsShoushuriItemDic(int msVesselId)
        {
            List<MsShoushuriItem> MsShoushuriItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsShoushuriItems = serviceClient.MsShoushuriItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, msVesselId);
            }
            Dictionary<string, string> MsShoushuriItemDic = new Dictionary<string, string>();
            foreach (MsShoushuriItem item in MsShoushuriItems)
            {
                if (MsShoushuriItemDic.ContainsKey(item.ItemName) == false)
                {
                    MsShoushuriItemDic.Add(item.ItemName, item.ItemName);
                }
            }
            return MsShoushuriItemDic;
        }
        public static Dictionary<string, string> MakeMsSsShousaiItemDic(int msVesselId)
        {
            List<MsSsShousaiItem> MsSsShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsSsShousaiItems = serviceClient.MsSsShousaiItem_GetRecordByMsVesselID(NBaseCommon.Common.LoginUser, msVesselId);
            }
            Dictionary<string, string> MsSsShousaiItemDic = new Dictionary<string, string>();
            foreach (MsSsShousaiItem item in MsSsShousaiItems)
            {
                if (MsSsShousaiItemDic.ContainsKey(item.ShousaiItemName) == false)
                {
                    MsSsShousaiItemDic.Add(item.ShousaiItemName, item.ShousaiItemName);
                }
            }
            return MsSsShousaiItemDic;
        }

        #region privateメソッド
        private static OdThiItem MakeOdThiItem(string OdThiID, string Name, int MsVesselID)
        {
            OdThiItem item = new OdThiItem();
            item.OdThiItemID = Hachu.Common.CommonDefine.新規ID(true);
            item.OdThiID = OdThiID;
            item.ItemName = Name;
            item.VesselID = MsVesselID;
            return item;
        }
        private static OdThiShousaiItem MakeOdThiShousaiItem(string OdThiItemID, string MsLoID, string Name, string MsTaniID, string MsTaniName, int MsVesselID)
        {
            OdThiShousaiItem item = new OdThiShousaiItem();
            item.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID(true);
            item.OdThiItemID = OdThiItemID;
            item.MsLoID = MsLoID;
            item.ShousaiItemName = Name;
            item.MsTaniID = MsTaniID;
            item.MsTaniName = MsTaniName;
            item.VesselID = MsVesselID;
            return item;
        }
        #endregion
    }
}
