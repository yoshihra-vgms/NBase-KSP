using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseCommon;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.BLC.動静基幹連携.ResultMessage> BLC_動静基幹連携_Kick(MsUser loginUser, List<DjDousei> SelectedDouseis);
    }
    public partial class Service
    {
        public List<NBaseData.BLC.動静基幹連携.ResultMessage> BLC_動静基幹連携_Kick(MsUser loginUser, List<DjDousei> SelectedDouseis)
        {
            #region 元のコード
            //NBaseData.BLC.動静基幹連携 logic = new NBaseData.BLC.動静基幹連携();
            //logic.Kick(loginUser, loginUser,SelectedDouseis);

            //return logic.ResultMessages;
            #endregion

            LogFile.Write(loginUser.FullName, "NBaseService:動静基幹連携");

            List<NBaseData.BLC.動静基幹連携.ResultMessage> resultMessages = new List<NBaseData.BLC.動静基幹連携.ResultMessage>();
            //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //{
            //    resultMessages = serviceClient.BLC_基幹システム連携書き込み処理_動静連携(loginUser, SelectedDouseis);
            //}

            LogFile.Write(loginUser.FullName, "NBaseService:CoreServerからの結果数 = " + resultMessages .Count().ToString());

            // 2018.01 ２０１７年度改造
            List<DjDousei> fixDjDouseis = new List<DjDousei>();

            foreach (NBaseData.BLC.動静基幹連携.ResultMessage result in resultMessages)
            {
                if (result.TKJNAIPLANIF == null)
                {
                    //LogFile.Write(loginUser.FullName, "NBaseService:[" + result.Dousei.DjDouseiID + "]:TKJNAIPLANIFがない");
                    continue;
                }

                //処理結果がエラーの場合は基幹連携済みにしない
                if (result.result == false)
                {
                    //DJ_DOUSEIを更新し基幹連携済みにする
                    result.Dousei.KikanRenkeiFlag = 1;
                    result.Dousei.UpdateDetailRecords(null, loginUser);

                    LogFile.Write(loginUser.FullName, "NBaseService:[" + result.Dousei.DjDouseiID + "]:連携済み");
                    //System.Diagnostics.Debug.WriteLine("NBaseService:[" + result.Dousei.DjDouseiID + "]:連携済み");

                    // 2018.01 ２０１７年度改造
                    fixDjDouseis.Add(result.Dousei);
                }
                else
                {
                    //DJ_DOUSEIを更新し基幹連携済みにする
                    result.Dousei.UpdateDetailRecords(null, loginUser);

                    LogFile.Write(loginUser.FullName, "NBaseService:[" + result.Dousei.DjDouseiID + "]:エラー");
                    //System.Diagnostics.Debug.WriteLine("NBaseService:[" + result.Dousei.DjDouseiID + "]:エラー");
                }
            }

            // 2018.01 ２０１７年度改造
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            船員経験処理.BLC_積荷実績登録(loginUser, seninTableCache, fixDjDouseis.OrderBy(obj => obj.DouseiDate).ToList());
            船員経験処理.BLC_外航実績登録(loginUser, seninTableCache, fixDjDouseis.OrderBy(obj => obj.DouseiDate).ToList());


            return resultMessages;
        }
    }
}
