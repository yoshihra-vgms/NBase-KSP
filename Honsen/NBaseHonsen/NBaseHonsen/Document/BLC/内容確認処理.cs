using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping.Atts;
using NBaseUtil;
using NBaseHonsen.util;

namespace NBaseHonsen.Document.BLC
{
    public class 内容確認処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool Honsen登録(MsUser loginUser, int vesselId, int linkSaki, string linkSakiId, List<string>checkedUserIds)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            int koukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;

            DmKanriKiroku kanriKiroku = null;
            DmKoubunshoKisoku koubunshoKisoku = null;
            PtHonsenkoushinInfo honsenkoushinInfo = null;

            if (linkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録)
            {
                kanriKiroku = DmKanriKiroku.GetRecord(loginUser, linkSakiId);
                honsenkoushinInfo = 本船更新情報.CreateInstance(kanriKiroku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.内容確認);
            }
            else
            {
                koubunshoKisoku = DmKoubunshoKisoku.GetRecord(loginUser, linkSakiId);
                honsenkoushinInfo = 本船更新情報.CreateInstance(koubunshoKisoku, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.内容確認);
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                bool isExistsCheckedUser = false;
                int showOrder = 1;
                if (checkedUserIds != null)
                {
                    foreach (string uid in checkedUserIds)
                    {
                        if (uid == loginUser.MsUserID)
                        {
                            isExistsCheckedUser = true;
                        }
                        DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(dbConnect, loginUser, linkSaki, linkSakiId, koukaiSaki, uid);
                        MsUser user = MsUser.GetRecordsByUserID(dbConnect, loginUser, uid);
                        if (kakuninJokyo == null)
                        {
                            kakuninJokyo = new DmKakuninJokyo();
                            kakuninJokyo.DmKakuninJokyoID = System.Guid.NewGuid().ToString();
                            kakuninJokyo.ViewDate = 処理日時;
                            kakuninJokyo.LinkSaki = linkSaki;
                            kakuninJokyo.LinkSakiID = linkSakiId;
                        }
                        kakuninJokyo.KakuninDate = 処理日時;
                        kakuninJokyo.MsUserID = uid;
                        kakuninJokyo.ShowOrder = showOrder;
                        if (user != null)
                        {
                            kakuninJokyo.DocFlag_CEO = user.DocFlag_CEO;
                            kakuninJokyo.DocFlag_Admin = user.DocFlag_Admin;
                            kakuninJokyo.DocFlag_MsiFerry = user.DocFlag_MsiFerry;
                            kakuninJokyo.DocFlag_CrewFerry = user.DocFlag_CrewFerry;
                            kakuninJokyo.DocFlag_TsiFerry = user.DocFlag_TsiFerry;
                            //kakuninJokyo.DocFlag_MsiCargo = user.DocFlag_MsiCargo;
                            //kakuninJokyo.DocFlag_CrewCargo = user.DocFlag_CrewCargo;
                            //kakuninJokyo.DocFlag_TsiCargo = user.DocFlag_TsiCargo;
                            kakuninJokyo.DocFlag_Officer = user.DocFlag_Officer;
                            //kakuninJokyo.DocFlag_SdManager = user.DocFlag_SdManager;
                            kakuninJokyo.DocFlag_GL = user.DocFlag_GL;
                            kakuninJokyo.DocFlag_TL = user.DocFlag_TL;
                        }
                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                        kakuninJokyo.MsVesselID = vesselId;

                        kakuninJokyo.RenewDate = 処理日時;
                        kakuninJokyo.RenewUserID = loginUser.MsUserID;

                        ret = SyncTableSaver.InsertOrUpdate2(kakuninJokyo, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                        if (ret == false)
                        {
                            break;
                        }
                        showOrder++;
                    }
                }
                if (ret && isExistsCheckedUser == false)
                {
                    DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(dbConnect, loginUser, linkSaki, linkSakiId, koukaiSaki, loginUser.MsUserID);
                    if (kakuninJokyo == null)
                    {
                        kakuninJokyo = new DmKakuninJokyo();
                        kakuninJokyo.DmKakuninJokyoID = System.Guid.NewGuid().ToString();
                        kakuninJokyo.ViewDate = 処理日時;
                        kakuninJokyo.LinkSaki = linkSaki;
                        kakuninJokyo.LinkSakiID = linkSakiId;
                    }
                    kakuninJokyo.KakuninDate = 処理日時;
                    kakuninJokyo.MsUserID = loginUser.MsUserID;
                    kakuninJokyo.ShowOrder = showOrder;
                    kakuninJokyo.DocFlag_CEO = loginUser.DocFlag_CEO;
                    kakuninJokyo.DocFlag_Admin = loginUser.DocFlag_Admin;
                    kakuninJokyo.DocFlag_MsiFerry = loginUser.DocFlag_MsiFerry;
                    kakuninJokyo.DocFlag_CrewFerry = loginUser.DocFlag_CrewFerry;
                    kakuninJokyo.DocFlag_TsiFerry = loginUser.DocFlag_TsiFerry;
                    //kakuninJokyo.DocFlag_MsiCargo = loginUser.DocFlag_MsiCargo;
                    //kakuninJokyo.DocFlag_CrewCargo = loginUser.DocFlag_CrewCargo;
                    //kakuninJokyo.DocFlag_TsiCargo = loginUser.DocFlag_TsiCargo;
                    kakuninJokyo.DocFlag_Officer = loginUser.DocFlag_Officer;
                    //kakuninJokyo.DocFlag_SdManager = loginUser.DocFlag_SdManager;
                    kakuninJokyo.DocFlag_GL = loginUser.DocFlag_GL;
                    kakuninJokyo.DocFlag_TL = loginUser.DocFlag_TL;
                    kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                    kakuninJokyo.MsVesselID = vesselId;

                    kakuninJokyo.RenewDate = 処理日時;
                    kakuninJokyo.RenewUserID = loginUser.MsUserID;

                    ret = SyncTableSaver.InsertOrUpdate2(kakuninJokyo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                }

                if (ret)
                {
                    ret = SyncTableSaver.InsertOrUpdate2(honsenkoushinInfo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }

                return ret;
            }
        }
    }
}
