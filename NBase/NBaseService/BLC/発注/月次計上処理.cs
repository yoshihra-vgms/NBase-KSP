using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC月次計上処理_実行(
            NBaseData.DAC.MsUser loginUser,
            string YearMonth);
    }
    public partial class Service
    {
        public bool BLC月次計上処理_実行(
            NBaseData.DAC.MsUser loginUser,
            string YearMonth
            )
        {

            // 月次計上を確定するときは、確定月＋１の月のテーブルデータを作成する
            DateTime thisYM = DateTime.Parse(YearMonth.Substring(0, 4) + "/" + YearMonth.Substring(4, 2) + "/01");
            DateTime nextYM = thisYM.AddMonths(1);
            string TrgYM = nextYM.Year.ToString() + nextYM.Month.ToString("00");


//// 2016.06.09 TEST
//TrgYM = "201604";

            List<NBaseData.DAC.OdChozo> chozoList = new List<NBaseData.DAC.OdChozo>();
            List<NBaseData.DAC.OdChozoShousai> chozoShousaiList = new List<NBaseData.DAC.OdChozoShousai>();
            NBaseData.BLC.貯蔵品処理.貯蔵品詳細テーブルデータ作成(loginUser, TrgYM, ref chozoList, ref chozoShousaiList);

//// 2016.06.09 TEST
//return true;

            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();
                foreach (NBaseData.DAC.OdChozo chozo in chozoList)
                {
                    ret = chozo.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
                if (ret == true)
                {
                    foreach (NBaseData.DAC.OdChozoShousai chozoShousai in chozoShousaiList)
                    {
                        ret = chozoShousai.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
                if (ret == true)
                {
                    // 締めは、指定された年月で実行
                    NBaseData.DAC.OdGetsujiShime getsujiShime = new NBaseData.DAC.OdGetsujiShime();

                    getsujiShime.OdGetsujiShimeID = System.Guid.NewGuid().ToString();
                    getsujiShime.NenGetsu = YearMonth;
                    getsujiShime.MsUserID = loginUser.MsUserID;
                    getsujiShime.RenewDate = DateTime.Now;
                    getsujiShime.RenewUserID = loginUser.MsUserID;

                    ret = getsujiShime.InsertRecord(dbConnect, loginUser);
                }
                if (ret == false)
                {
                    dbConnect.RollBack();
                }
                else
                {
                    dbConnect.Commit();
                }
            }
            return ret;
        }
    }
 }
