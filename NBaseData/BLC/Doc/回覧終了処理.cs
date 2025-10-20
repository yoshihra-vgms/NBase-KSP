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

namespace NBaseData.BLC
{
    public class 回覧終了処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool 登録(MsUser loginUser, DmKanryoInfo kanryoInfo)
        {
            bool ret = true;
            DateTime 処理日時 = DateTime.Now;
            string 更新者 = loginUser.MsUserID;
            DmKanriKiroku kanriKiroku = null;
            DmKoubunshoKisoku koubunshoKisoku = null;

            if (kanryoInfo.LinkSaki == (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録)
            {
                kanriKiroku = DmKanriKiroku.GetRecord(loginUser, kanryoInfo.LinkSakiID);
            }
            else
            {
                koubunshoKisoku = DmKoubunshoKisoku.GetRecord(loginUser, kanryoInfo.LinkSakiID);
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 完了情報の登録
                kanryoInfo.DmKanryoInfoID = 新規ID();
                kanryoInfo.RenewDate = 処理日時;
                kanryoInfo.RenewUserID = 更新者;

                ret = kanryoInfo.InsertRecord(dbConnect, loginUser);

                if (ret)
                {
                    if (kanriKiroku != null)
                    {
                        ret = 管理記録更新(dbConnect, 処理日時, 更新者, loginUser, kanriKiroku);
                    }
                    if (koubunshoKisoku != null)
                    {
                        ret = 公文書規則更新(dbConnect, 処理日時, 更新者, loginUser, koubunshoKisoku);
                    }
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }

            }

            return ret;
        }

        private static bool 管理記録更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKanriKiroku kanriKiroku)
        {
            bool ret = true;

            kanriKiroku.Status = (int)NBaseData.DS.DocConstants.StatusEnum.完了;
            kanriKiroku.RenewDate = 処理日時;
            kanriKiroku.RenewUserID = 更新者;
            ret = kanriKiroku.UpdateRecord(dbConnect, loginUser);

            return ret;
        }

        private static bool 公文書規則更新(DBConnect dbConnect, DateTime 処理日時, string 更新者, MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku)
        {
            bool ret = true;

            koubunshoKisoku.Status = (int)NBaseData.DS.DocConstants.StatusEnum.完了;
            koubunshoKisoku.RenewDate = 処理日時;
            koubunshoKisoku.RenewUserID = 更新者;
            ret = koubunshoKisoku.UpdateRecord(dbConnect, loginUser);

            return ret;
        }
    }
}
