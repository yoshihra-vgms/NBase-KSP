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
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.BLC.概算計上一覧Row> BLC_概算計上一覧Row_SearchRecords(MsUser loginUser, NBaseData.DS.OdJryFilter filter);
        
        [OperationContract]
        byte[] BLC_Excel概算計上一覧_取得(NBaseData.DAC.MsUser loginUser, int year, int month, List<NBaseData.BLC.概算計上一覧Row> rows);
       
        [OperationContract]
        bool BLC概算計上処理_実行(
            NBaseData.DAC.MsUser loginUser,
            string YearMonth,
            List<OdGaisanKeijo> odGaisanKeijos
            );
    }
    public partial class Service
    {
        public List<NBaseData.BLC.概算計上一覧Row> BLC_概算計上一覧Row_SearchRecords(MsUser loginUser, NBaseData.DS.OdJryFilter filter)
        {
            return NBaseData.BLC.概算計上一覧Row.SearchRecords(loginUser, filter);
        }

        public byte[] BLC_Excel概算計上一覧_取得(NBaseData.DAC.MsUser loginUser, int year, int month, List<NBaseData.BLC.概算計上一覧Row> rows)
        {
            NBaseData.BLC.Doc.概算計上一覧出力 report = new NBaseData.BLC.Doc.概算計上一覧出力();
            return report.概算計上一覧取得(loginUser, year, month, rows);
        }

        public bool BLC概算計上処理_実行(
            NBaseData.DAC.MsUser loginUser,
            string YearMonth,
            List<OdGaisanKeijo> odGaisanKeijos
            )
        {
            bool ret = false;
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);
            int year = int.Parse(YearMonth.Substring(0, 4));
            int month = int.Parse(YearMonth.Substring(4, 2));
            List<NBaseData.BLC.貯蔵品リスト> 潤滑油リストALL = NBaseData.BLC.貯蔵品リスト.GetRecords(loginUser, year, month, NBaseData.BLC.貯蔵品リスト.対象Enum.潤滑油);
            List<NBaseData.BLC.貯蔵品リスト> 船用品リストALL = NBaseData.BLC.貯蔵品リスト.GetRecords(loginUser, year, month, NBaseData.BLC.貯蔵品リスト.対象Enum.船用品);
            //List<NBaseData.BLC.貯蔵品リスト> 潤滑油リストALL = new List<NBaseData.BLC.貯蔵品リスト>();
            //List<NBaseData.BLC.貯蔵品リスト> 船用品リストALL = new List<NBaseData.BLC.貯蔵品リスト>();

            NBaseCommon.LogFile.Write(loginUser.FullName, "BLC概算計上処理_実行");

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (OdGaisanKeijo gaisanKeijo in odGaisanKeijos)
                {
                    gaisanKeijo.GaisanKeijoZuki = YearMonth;
                    gaisanKeijo.MsUserID = loginUser.MsUserID;
                    gaisanKeijo.RenewUserID = loginUser.MsUserID;
                    gaisanKeijo.RenewDate = DateTime.Now;
                    ret = gaisanKeijo.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
                if (ret == true)
                {
                    NBaseCommon.LogFile.Write(loginUser.FullName, "BLC_基幹システム連携書き込み処理_概算連携 Call");

                    ret = BLC_基幹システム連携書き込み処理_概算連携(dbConnect, loginUser, odGaisanKeijos, YearMonth, msVesselList, 潤滑油リストALL, 船用品リストALL);
                }

                NBaseCommon.LogFile.Write(loginUser.FullName, "BLC_基幹システム連携書き込み処理_概算連携 Call ret = " + ret.ToString());
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
