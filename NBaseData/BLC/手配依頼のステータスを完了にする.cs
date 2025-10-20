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
using NBaseData.DS;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 手配依頼のステータスを完了にする
    {
        DBConnect dbConnect;
        MsUser loginUser;

        bool 完了にする;
        public List<string>処理メッセージ = new List<string>();

        public void Kick(DBConnect dbConnect,MsUser loginUser, List<OdShr> 実績になった支払s)
        {
            Kick(dbConnect, loginUser, 実績になった支払s, true);
        }
        public void Kick(DBConnect dbConnect, MsUser loginUser, List<OdShr> 実績になった支払s, bool Is発注承認ON)
        {
            this.dbConnect = dbConnect;
            this.loginUser = loginUser;

            foreach (OdShr 実績になった支払 in 実績になった支払s)
            {
                List<string> 手配依頼IDs = 完了以外の手配依頼取得_支払合算対応(実績になった支払);
                if (手配依頼IDs != null)
                {
                    foreach(string 手配依頼ID in 手配依頼IDs)
                    {
                        完了Logic(手配依頼ID, Is発注承認ON);
                    }
                }
            }
        }

        /// <summary>
        /// 完了になっていない手配依頼を抽出し完了Loigcに流し、ステータスを更新する
        /// </summary>
        /// <param name="dbConnect"></param>
        /// <param name="loginUser"></param>
        public void 洗い替え(MsUser loginUser)
        {
            this.loginUser = loginUser;
            //全手配依頼を取得
            List<OdThi> odThis = OdThi.GetRecords(loginUser);

            int iStatus = (int)MsThiIraiStatus.THI_IRAI_STATUS.完了;
            string status = iStatus.ToString();

            int 処理すべき手配依頼の数 = 0;
            foreach (OdThi odThi in odThis)
            {
                if (odThi.MsThiIraiStatusID != status)
                {
                    処理すべき手配依頼の数++;
                }
            }


            int 処理数 = 1;
            using (DBConnect db = new DBConnect())
            {
                dbConnect = db;
                dbConnect.BeginTransaction();


                foreach (OdThi odThi in odThis)
                {
                    if (odThi.MsThiIraiStatusID != status)
                    {
                        処理メッセージ.Add("処理手配依頼番号: " + odThi.TehaiIraiNo);
                        完了Logic(odThi.OdThiID, true);
                        Console.WriteLine(処理数.ToString() + "/" + 処理すべき手配依頼の数.ToString());
                        処理数++;
                    }
                }

                dbConnect.Commit();
            }
        }


        private void 完了Logic(string 手配依頼ID, bool Is発注承認ON)
        {
            完了にする = true;
            if (手配依頼ID != "")
            {
                List<OdMk> 発注データs = 発注取得(手配依頼ID);
                if (発注データs.Count > 0)
                {
                    foreach (OdMk 発注データ in 発注データs)
                    {
                        List<OdJry> 未受領データ = new List<OdJry>();
                        List<OdJry> 受領済みデータ = new List<OdJry>();

                        int 受領総数 = 受領取得(発注データ, Is発注承認ON, ref 未受領データ, ref 受領済みデータ);

                        if (未受領データ.Count > 0)
                        {
                            完了にする = false;
                            break;
                        }

                        foreach (OdJry jry in 受領済みデータ)
                        {
                            List<OdShr> 未落成データ = new List<OdShr>();
                            List<OdShr> 落成済みデータ = new List<OdShr>();

                            int 落成総数 = 落成取得(jry, ref 未落成データ, ref 落成済みデータ);

                            if (落成総数 == 0)
                            {
                                bool bRet = 支払チェック(jry);
                                if (bRet == false)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (未落成データ.Count > 0)
                                {
                                    完了にする = false;
                                    break;
                                }

                                bool bRet = 支払チェック(jry);
                                if (bRet == false)
                                {
                                    break;
                                }

                            }
                        }
                    }

                    if (完了にする == true)
                    {
                        発注アラーム処理.手配アラーム削除(dbConnect, loginUser, 手配依頼ID);

                        手配依頼を完了にする(手配依頼ID);
                        処理メッセージ.Add("手配依頼ID: " + 手配依頼ID + " を完了にしました");
                    }
                    else
                    {
                        処理メッセージ.Add("手配依頼ID: " + 手配依頼ID + " は完了していません");
                    }

                }
            }
        }

        private bool 支払チェック(OdJry jry)
        {
            List<OdShr> 未払データ = new List<OdShr>();
            List<OdShr> 支払済みデータ = new List<OdShr>();

            int 支払総数 = 支払取得(jry, ref 未払データ, ref 支払済みデータ);

            if (支払総数 == 0 || 未払データ.Count > 0)
            {
                完了にする = false;
                return false;
            }
            return true;
        }

        private List<OdMk> 発注取得(string 手配依頼ID)
        {
            List<OdMk> ret = new List<OdMk>();
            List<OdMk> tmp = new List<OdMk>();

            tmp = OdMk.GetRecordsByOdThiID(dbConnect, loginUser, 手配依頼ID);
            if (tmp == null)
                return ret;

            foreach (OdMk odmk in tmp)
            {
                if (odmk.Status == (int)OdMk.STATUS.発注済み)
                {
                    ret.Add(odmk);
                }
            }

            return ret;
        }

        private int 受領取得(OdMk odMk, bool Is発注承認ON, ref List<OdJry> 未受領, ref List<OdJry> 受領済み)
        {
            List<OdJry> tmp = OdJry.GetRecordsByOdMkId(dbConnect, loginUser, odMk.OdMkID);
            if (tmp == null)
                return 0;

            foreach (OdJry jry in tmp)
            {
                if (Is発注承認ON)
                {
                    if (jry.Status == (int)OdJry.STATUS.受領承認済み || jry.Status == (int)OdJry.STATUS.船受領)
                    {
                        受領済み.Add(jry);
                    }
                    else
                    {
                        未受領.Add(jry);
                    }
                }
                else
                {
                    if (jry.Status == (int)OdJry.STATUS.未受領)
                    {
                        未受領.Add(jry);
                    }
                    else
                    {
                        受領済み.Add(jry);
                    }
                }
            }

            return tmp.Count;
        }

        private int 支払取得(OdJry odJry, ref List<OdShr> 未払, ref List<OdShr> 支払済み)
        {
            List<OdShr> tmp = OdShr.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);

            // 2010.03:aki 支払合算対応のため、以下５行追加
            List<OdShr> tmp2 = OdShr.GetRecordByGassanItem(dbConnect, loginUser, odJry.OdJryID);
            if (tmp2 != null)
            {
                tmp.AddRange(tmp2);
            }

            if (tmp == null)
                return 0;

            foreach (OdShr shr in tmp)
            {
                if (shr.Status == (int)OdShr.STATUS.支払済)
                {
                    支払済み.Add(shr);
                }
                else if (shr.Status == (int)OdShr.STATUS.支払作成済み || shr.Status == (int)OdShr.STATUS.支払依頼済み || shr.Status == (int)OdShr.STATUS.支払依頼基幹連携済み)
                {
                    未払.Add(shr);
                }
            }

            return 支払済み.Count + 未払.Count;
        }

        private int 落成取得(OdJry odJry, ref List<OdShr> 未落成, ref List<OdShr> 落成済み)
        {
            List<OdShr> tmp = OdShr.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);
            if (tmp == null)
                return 0;

            foreach (OdShr shr in tmp)
            {
                if (shr.Status == (int)OdShr.STATUS.落成済み)
                {
                    落成済み.Add(shr);
                }
                else if (shr.Status == (int)OdShr.STATUS.未落成 || 
                    shr.Status == (int)OdShr.STATUS.落成承認依頼中 || 
                    shr.Status == (int)OdShr.STATUS.落成承認済み)
                {
                    未落成.Add(shr);
                }
            }

            return 落成済み.Count + 未落成.Count;
        }


        private void 手配依頼を完了にする(string 手配依頼ID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(手配依頼のステータスを完了にする), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("OD_THI_ID",手配依頼ID));

            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

        }

        private string 完了以外の手配依頼取得(OdShr 実績になった支払)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(手配依頼のステータスを完了にする), MethodBase.GetCurrentMethod());

            List<OdThi> odThis = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();

            int iStatus = (int)MsThiIraiStatus.THI_IRAI_STATUS.完了;
            string status = iStatus.ToString();
            Params.Add(new DBParameter("SHR_NO",実績になった支払.ShrNo));
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", status));

            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            odThis = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            if (odThis.Count > 0)
            {
                return odThis[0].OdThiID;
            }
            else{
                return "";
            }
        }

        private List<string> 完了以外の手配依頼取得_支払合算対応(OdShr 実績になった支払)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(手配依頼のステータスを完了にする), MethodBase.GetCurrentMethod());

            List<OdThi> odThis = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();

            int iStatus = (int)MsThiIraiStatus.THI_IRAI_STATUS.完了;
            string status = iStatus.ToString();
            Params.Add(new DBParameter("SHR_NO", 実績になった支払.ShrNo));
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", status));

            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            odThis = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (odThis.Count > 0)
            {
                return odThis.Select(obj => obj.OdThiID).ToList();
            }
            else
            {
                return null;
            }
        }

    }
}
